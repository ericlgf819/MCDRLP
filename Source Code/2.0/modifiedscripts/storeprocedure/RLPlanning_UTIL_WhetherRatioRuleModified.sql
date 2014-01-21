USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherRatioRuleModified]    Script Date: 07/28/2012 14:56:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherRatioRuleModified]
@EntityInfoSettingID nvarchar(50),
@LastEntityInfoSettingID nvarchar(50),
@RentType nvarchar(50),
@Result BIT OUTPUT	--0表示没有变化，1表示有变化
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	--先判断RentType下的Rule总数是否相同
	DECLARE @RatioRuleSum int, @LastRatioRuleSum int
	SELECT @RatioRuleSum=COUNT(*) FROM RatioRuleSetting RS 
	INNER JOIN RatioCycleSetting R ON RS.RatioID = R.RatioID
	WHERE RS.EntityInfoSettingID=@EntityInfoSettingID
	AND RS.RentType=@RentType
	
	SELECT @LastRatioRuleSum=COUNT(*) FROM RatioRuleSetting RS 
	INNER JOIN RatioCycleSetting R ON RS.RatioID = R.RatioID
	WHERE RS.EntityInfoSettingID=@LastEntityInfoSettingID
	AND RS.RentType=@RentType
	
	--如果Rule的数量都不同，说明变动过了
	IF @RatioRuleSum<>@LastRatioRuleSum
	BEGIN
		SET @Result=1
		RETURN
	END
	
	--逐条比较内容
	DECLARE @RatioRuleCursor CURSOR
	SET @RatioRuleCursor = CURSOR SCROLL FOR
	SELECT ISNULL(R.FirstDueDate,''), ISNULL(R.PayType,''), ISNULL(R.ZXStartDate,''),
	ISNULL(R.Cycle,''), ISNULL(R.Calendar,''), ISNULL(R.CycleType,''), ISNULL(R.IsPure,''),
	R.RuleSnapshotID
	FROM RatioCycleSetting R INNER JOIN RatioRuleSetting RS ON R.RatioID=RS.RatioID
	WHERE RS.EntityInfoSettingID=@EntityInfoSettingID AND RS.RentType=@RentType
	
	DECLARE @FirstDueDate datetime, @PayType nvarchar(50), @ZXStartDate datetime,
	@Cycle nvarchar(50), @Calendar nvarchar(50), @CycleType nvarchar(50), @IsPure bit,
	@RuleSnapshotID nvarchar(50)
	
	DECLARE @LastFirstDueDate datetime, @LastPayType nvarchar(50), @LastZXStartDate datetime,
	@LastCycle nvarchar(50), @LastCalendar nvarchar(50),
	@LastRuleSnapshotID nvarchar(50)
	
	OPEN @RatioRuleCursor
	FETCH NEXT FROM @RatioRuleCursor INTO @FirstDueDate, @PayType, @ZXStartDate,
	@Cycle, @Calendar, @CycleType, @IsPure, @RuleSnapshotID
	
	WHILE @@FETCH_STATUS=0
	BEGIN
		SELECT @LastFirstDueDate=ISNULL(FirstDueDate,''), @LastPayType=ISNULL(PayType,''),
		@LastZXStartDate=ISNULL(ZXStartDate,''), @LastCycle=ISNULL(Cycle,''), @LastCalendar=ISNULL(Calendar,''),
		@LastRuleSnapshotID=R.RuleSnapshotID
		FROM RatioCycleSetting R INNER JOIN RatioRuleSetting RS ON R.RatioID=RS.RatioID
		WHERE RS.EntityInfoSettingID=@LastEntityInfoSettingID AND RS.RentType=@RentType 
		AND R.CycleType=@CycleType AND R.IsPure=@IsPure
		
		--有一条不同，则直接返回
		IF @LastFirstDueDate<>@FirstDueDate OR @LastPayType<>@PayType OR @LastZXStartDate<>@ZXStartDate
		OR @LastCycle<>@Cycle OR @LastCalendar<>@Calendar
		BEGIN
			CLOSE @RatioRuleCursor
			DEALLOCATE @RatioRuleCursor
			SET @Result=1
			RETURN
		END
		
		--如果上面都相同，则需要验证对应的Rule计算规则是否相同
		EXEC RLPlanning_UTIL_WhetherRatioTimeIntervalModified @RuleSnapshotID, @LastRuleSnapshotID, @Result OUTPUT
		
		--如果规则不同，则直接返回
		IF @Result=1
		BEGIN
			CLOSE @RatioRuleCursor
			DEALLOCATE @RatioRuleCursor
			SET @Result=1
			RETURN			
		END
		
		FETCH NEXT FROM @RatioRuleCursor INTO @FirstDueDate, @PayType, @ZXStartDate,
		@Cycle, @Calendar, @CycleType, @IsPure, @RuleSnapshotID
	END
	CLOSE @RatioRuleCursor
	DEALLOCATE @RatioRuleCursor
END

GO


