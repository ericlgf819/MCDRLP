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
@Result BIT OUTPUT	--0��ʾû�б仯��1��ʾ�б仯
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET @Result = 0
	--���ж���ͬRentType��Rule�����Ƿ���ͬ
	DECLARE @FixedRuleSum int, @LastFixedRuleSum int
	SELECT @FixedRuleSum=COUNT(*) FROM FixedRuleSetting WHERE EntityInfoSettingID=@EntityInfoSettingID AND RentType=@RentType
	SELECT @LastFixedRuleSum=COUNT(*) FROM FixedRuleSetting WHERE EntityInfoSettingID=@LastEntityInfoSettingID AND RentType=@RentType
	
	--������ͬ�������б仯
	IF @FixedRuleSum<>@LastFixedRuleSum
	BEGIN
		SET @Result=1
		RETURN
	END
	
	--�����Ƚ�����
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
	
		--�����һ����һ����˵�����Ǳ䶯����
		IF @LastFirstDueDate<>@FirstDueDate OR @LastPayType<>@PayType OR @LastZXStartDate<>@ZXStartDate
		OR @LastZXConstant<>@ZXConstant OR @LastCycle<>@Cycle OR @LastCalendar<>@Calendar
		OR @LastDescription<>@Description OR @LastRemark<>@Remark
		BEGIN
			CLOSE @FixedRuleCursor
			DEALLOCATE @FixedRuleCursor
			SET @Result=1
			RETURN
		END

		--���һ����ȽϹ����е�ʱ����Լ������Ƿ���ͬ
		EXEC RLPlanning_UTIL_WhetherFixTimeIntervalModified @RuleSnapshotID, @LastRuleSnapshotID, @Result OUTPUT
		
		--���ʱ���������ͬ��Ҳ��������������䶯
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


