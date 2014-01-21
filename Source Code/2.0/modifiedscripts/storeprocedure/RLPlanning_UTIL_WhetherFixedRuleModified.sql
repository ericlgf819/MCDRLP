USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherFixedRuleModified]    Script Date: 07/28/2012 14:11:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherFixedRuleModified] 
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
	--先判断相同RentType的Rule总数是否相同
	DECLARE @FixedRuleSum int, @LastFixedRuleSum int
	SELECT @FixedRuleSum=COUNT(*) FROM FixedRuleSetting WHERE EntityInfoSettingID=@EntityInfoSettingID AND RentType=@RentType
	SELECT @LastFixedRuleSum=COUNT(*) FROM FixedRuleSetting WHERE EntityInfoSettingID=@LastEntityInfoSettingID AND RentType=@RentType
	
	--数量不同，表明有变化
	IF @FixedRuleSum<>@LastFixedRuleSum
	BEGIN
		SET @Result=1
		RETURN
	END
	
	--逐条比较内容
	DECLARE @FixedRuleCursor CURSOR
	SET @FixedRuleCursor = CURSOR SCROLL FOR
	SELECT ISNULL(FirstDueDate,''), ISNULL(PayType,''), ISNULL(ZXStartDate,''), ISNULL(ZXConstant,0),
	ISNULL(Cycle,''), ISNULL(Calendar,''), ISNULL(Description,''), ISNULL(Remark,''), RuleSnapshotID
	FROM FixedRuleSetting WHERE EntityInfoSettingID=@EntityInfoSettingID AND RentType=@RentType
	
	DECLARE @FirstDueDate datetime, @PayType nvarchar(50), @ZXStartDate datetime,
	@ZXConstant decimal(18,2), @Cycle nvarchar(50), @Calendar nvarchar(50), @Description nvarchar(2000),
	@Remark nvarchar(2000), @RuleSnapshotID nvarchar(50)
	
	DECLARE @LastFirstDueDate datetime, @LastPayType nvarchar(50), @LastZXStartDate datetime,
	@LastZXConstant decimal(18,2), @LastCycle nvarchar(50), @LastCalendar nvarchar(50), 
	@LastDescription nvarchar(2000), @LastRemark nvarchar(2000), @LastRuleSnapshotID nvarchar(50)
	
	OPEN @FixedRuleCursor
	FETCH NEXT FROM @FixedRuleCursor INTO @FirstDueDate ,@PayType ,@ZXStartDate,
	@ZXConstant ,@Cycle ,@Calendar ,@Description ,@Remark, @RuleSnapshotID
	
	WHILE @@FETCH_STATUS=0
	BEGIN
		SELECT @LastFirstDueDate=ISNULL(FirstDueDate,''), @LastPayType=ISNULL(PayType,''), 
		@LastZXStartDate=ISNULL(ZXStartDate,''), @LastZXConstant=ISNULL(ZXConstant,0), 
		@LastCycle=ISNULL(Cycle,''), @LastCalendar=ISNULL(Calendar,''),
		@LastDescription=ISNULL(Description,''), @LastRemark=ISNULL(Remark,''), @LastRuleSnapshotID=RuleSnapshotID
		FROM FixedRuleSetting WHERE EntityInfoSettingID=@LastEntityInfoSettingID AND RentType=@RentType
	
		--如果有一条不一样，说明就是变动过的
		IF @LastFirstDueDate<>@FirstDueDate OR @LastPayType<>@PayType OR @LastZXStartDate<>@ZXStartDate
		OR @LastZXConstant<>@ZXConstant OR @LastCycle<>@Cycle OR @LastCalendar<>@Calendar
		OR @LastDescription<>@Description OR @LastRemark<>@Remark
		BEGIN
			CLOSE @FixedRuleCursor
			DEALLOCATE @FixedRuleCursor
			SET @Result=1
			RETURN
		END

		--如果一样则比较规则中的时间段以及条件是否相同
		EXEC RLPlanning_UTIL_WhetherFixTimeIntervalModified @RuleSnapshotID, @LastRuleSnapshotID, @Result OUTPUT
		
		--如果时间段条件不同，也表面规则有做过变动
		IF @Result=1
		BEGIN
			CLOSE @FixedRuleCursor
			DEALLOCATE @FixedRuleCursor			
			RETURN
		END
		
		FETCH NEXT FROM @FixedRuleCursor INTO @FirstDueDate ,@PayType ,@ZXStartDate,
		@ZXConstant ,@Cycle ,@Calendar ,@Description ,@Remark, @RuleSnapshotID
	END
	
	CLOSE @FixedRuleCursor
	DEALLOCATE @FixedRuleCursor
END




GO


