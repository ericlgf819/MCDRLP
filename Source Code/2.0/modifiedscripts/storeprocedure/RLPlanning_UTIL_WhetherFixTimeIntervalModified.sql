USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherFixTimeIntervalModified]    Script Date: 07/28/2012 14:11:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherFixTimeIntervalModified]
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
	DECLARE @FixedTimeIntervalSum int, @LastFixedTimeIntervalSum int
	SELECT @FixedTimeIntervalSum=COUNT(*) FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	SELECT @LastFixedTimeIntervalSum=COUNT(*) FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@LastRuleSnapshotID
	--������һ��˵���б仯
	IF @FixedTimeIntervalSum<>@LastFixedTimeIntervalSum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--�������ݱȽ�
	DECLARE @FixedTimeIntervalCursor CURSOR
	
	SET @FixedTimeIntervalCursor = CURSOR SCROLL FOR
	SELECT ISNULL(StartDate,''), ISNULL(EndDate,''), ISNULL(Amount,0), ISNULL(Cycle,''),
	ISNULL(CycleMonthCount,''), ISNULL(Calendar,'')
	FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	
	DECLARE @StartDate datetime, @EndDate datetime, @Amount decimal(18,2), @Cycle nvarchar(50),
	@CycleMonthCount int, @Calendar nvarchar(50)
	
	DECLARE @LastEndDate datetime, @LastAmount decimal(18,2), @LastCycle nvarchar(50),
	@LastCycleMonthCount int, @LastCalendar nvarchar(50)
	

	
	OPEN @FixedTimeIntervalCursor
	FETCH NEXT FROM @FixedTimeIntervalCursor 
	INTO @StartDate ,@EndDate ,@Amount ,@Cycle ,@CycleMonthCount ,@Calendar
	WHILE @@FETCH_STATUS=0
	BEGIN
		SELECT @LastEndDate=ISNULL(EndDate,''), @LastAmount=ISNULL(Amount,0), @LastCycle=ISNULL(Cycle,''),
		@LastCycleMonthCount=ISNULL(CycleMonthCount,''), @LastCalendar=ISNULL(Calendar,'')
		FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@LastRuleSnapshotID AND StartDate=@StartDate
		
		--��һ��������ͷ���
		IF @LastEndDate<>@EndDate OR @LastAmount<>@Amount OR @LastCycle<>@Cycle
		OR @LastCycleMonthCount<>@CycleMonthCount OR @LastCalendar<>@Calendar
		BEGIN
			CLOSE @FixedTimeIntervalCursor
			DEALLOCATE @FixedTimeIntervalCursor		
			SET @Result=1
			RETURN
		END
		
		FETCH NEXT FROM @FixedTimeIntervalCursor 
		INTO @StartDate ,@EndDate ,@Amount ,@Cycle ,@CycleMonthCount ,@Calendar
	END
	CLOSE @FixedTimeIntervalCursor
	DEALLOCATE @FixedTimeIntervalCursor
END


GO


