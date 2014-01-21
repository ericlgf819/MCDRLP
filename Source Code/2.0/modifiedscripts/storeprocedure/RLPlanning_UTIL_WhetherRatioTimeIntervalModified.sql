USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherRatioTimeIntervalModified]    Script Date: 07/28/2012 10:54:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherRatioTimeIntervalModified] 
@RuleSnapshotID nvarchar(50),
@LastRuleSnapshotID nvarchar(50),
@Result BIT OUTPUT	--0��ʾû�仯��1��ʾ�б仯
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	
	--�Ƚ����� Rule��ʱ����Ƿ�����һ��
	DECLARE @RatioTimeIntervalSum int, @LastRatioTimeIntervalSum int
	SELECT @RatioTimeIntervalSum=COUNT(*) FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	SELECT @LastRatioTimeIntervalSum=COUNT(*) FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@LastRuleSnapshotID
	--������һ��˵���б仯
	IF @RatioTimeIntervalSum<>@LastRatioTimeIntervalSum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--�������ݱȽ�
	DECLARE @RatioTimeIntervalCursor CURSOR
	SET @RatioTimeIntervalCursor = CURSOR SCROLL FOR
	SELECT ISNULL(TimeIntervalID,''), ISNULL(StartDate,''), ISNULL(EndDate,'') FROM RatioTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
	
	DECLARE @TimeIntervalID nvarchar(50), @StartDate datetime, @EndDate datetime, 
	@LastTimeIntervalID nvarchar(50), @LastEndDate date
	
	OPEN @RatioTimeIntervalCursor
	FETCH NEXT FROM @RatioTimeIntervalCursor INTO @TimeIntervalID, @StartDate, @EndDate
	WHILE @@FETCH_STATUS=0
	BEGIN
		SELECT @LastTimeIntervalID=ISNULL(TimeIntervalID,''), @LastEndDate=ISNULL(EndDate,'') 
		FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@LastRuleSnapshotID
		AND StartDate=@StartDate
		
		--����������ڲ�һ����˵���б��
		IF @LastEndDate<>@EndDate
		BEGIN
			CLOSE @RatioTimeIntervalCursor
			DEALLOCATE @RatioTimeIntervalCursor
			SET @Result=1
			RETURN
		END
		
		--���ʱ�����ͬ����Ҫ����ʱ����¶�Ӧ�Ĺ����Ƿ���ͬ
		EXEC RLPlanning_UTIL_WhetherRatioConditionAmountModified @TimeIntervalID, @LastTimeIntervalID, @Result OUTPUT
		
		--�������ֵ��1��˵�������б������ֱ�ӷ���
		IF @Result = 1
		BEGIN
			CLOSE @RatioTimeIntervalCursor
			DEALLOCATE @RatioTimeIntervalCursor
			RETURN			
		END
		
		FETCH NEXT FROM @RatioTimeIntervalCursor INTO @TimeIntervalID, @StartDate, @EndDate
	END
	CLOSE @RatioTimeIntervalCursor
	DEALLOCATE @RatioTimeIntervalCursor
END

GO


