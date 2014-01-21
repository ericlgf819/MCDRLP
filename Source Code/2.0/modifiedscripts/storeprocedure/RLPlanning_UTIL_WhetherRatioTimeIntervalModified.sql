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
@Result BIT OUTPUT	--0表示没变化，1表示有变化
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	
	--比较两条 Rule的时间段是否数量一致
	DECLARE @RatioTimeIntervalSum int, @LastRatioTimeIntervalSum int
	SELECT @RatioTimeIntervalSum=COUNT(*) FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	SELECT @LastRatioTimeIntervalSum=COUNT(*) FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@LastRuleSnapshotID
	--数量不一致说明有变化
	IF @RatioTimeIntervalSum<>@LastRatioTimeIntervalSum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--逐条内容比较
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
		
		--如果结束日期不一样，说明有变更
		IF @LastEndDate<>@EndDate
		BEGIN
			CLOSE @RatioTimeIntervalCursor
			DEALLOCATE @RatioTimeIntervalCursor
			SET @Result=1
			RETURN
		END
		
		--如果时间段相同，需要检测该时间段下对应的规则是否相同
		EXEC RLPlanning_UTIL_WhetherRatioConditionAmountModified @TimeIntervalID, @LastTimeIntervalID, @Result OUTPUT
		
		--如果返回值是1，说明规则有变更，则直接返回
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


