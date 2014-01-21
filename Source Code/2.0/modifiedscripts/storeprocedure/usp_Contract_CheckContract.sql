USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_CheckContract]    Script Date: 08/31/2012 17:13:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	检查合同
-- =============================================
CREATE PROCEDURE [dbo].[usp_Contract_CheckContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50)
AS
BEGIN
	
		--将合同所有主键信息暂存
		DECLARE @ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
		INSERT INTO @ContractKeyInfo 
		SELECT * FROM v_ContractKeyInfo WHERE ContractSnapshotID=@ContractSnapshotID
		--将合同所有主键信息暂存

		
	--检查结果表
	DECLARE @CheckResultTable TABLE (IndexId int IDENTITY (1, 1) PRIMARY KEY,Code NVARCHAR(500), RelationData NVARCHAR(500),CheckMessage NVARCHAR(500))
	IF NOT EXISTS(SELECT * FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		VALUES('CheckNotExistVendor','','合同中不存在业主')
	END
	
	IF NOT EXISTS(SELECT * FROM [Entity] WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		VALUES('CheckNotExistEntity','','合同中不存在实体')
	END
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckVendorHasNoEntity',VendorName,'业主没有关联实体' 
	FROM VendorContract 
	WHERE VendorContractID IN
	(
		SELECT DISTINCT VendorContractID FROM @ContractKeyInfo 
		WHERE  VendorVendorNo IS NOT NULL
		AND EntityVendorNo IS NULL
		AND VendorContractID IS NOT NULL
	)
	AND VendorContractID NOT  IN
	(
		SELECT DISTINCT VendorContractID FROM @ContractKeyInfo 
		WHERE  VendorVendorNo IS NOT NULL
		AND EntityVendorNo  IS NOT  NULL
	)
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckVendorStatus',VendorName,'业主状态必须Active' 
	FROM SRLS_TB_Master_Vendor 
	WHERE Status <> 'A'
	AND VendorNo IN
	(
		SELECT VendorVendorNo FROM @ContractKeyInfo WHERE VendorVendorNo IS NOT NULL
	)
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckEntityRentDate',EntityName,'起租日大于租赁到期日' 
	FROM [Entity] 
	WHERE EntityID IN
	(
		SELECT DISTINCT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate > RentEndDate
	
	IF EXISTS(SELECT 1 FROM dbo.[Contract] WHERE PartComment='变更' AND ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		SELECT  'CheckEntityRentEndDate',EntityName,'租赁到期日小于当前已发起AP的日期' 
		FROM [Entity] 
		WHERE EntityID IN
		(
			SELECT DISTINCT EntityID FROM @ContractKeyInfo 
			WHERE EntityID IS NOT NULL
		)
		AND RentEndDate < dbo.fn_Contract_GetEntityAPMaxCycleEndDate(EntityID)
	END
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckEntityHasNoVendor',EntityName,'实体没有关联业主' 
	FROM [Entity] 
	WHERE EntityID IN
	(
		SELECT DISTINCT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
		AND EntityVendorNo IS NULL
	)
	AND EntityID NOT  IN 
	(
		SELECT DISTINCT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
		AND EntityVendorNo IS NOT  NULL
	)
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckVirtualVendorAP',x.EntityName,'虚拟业主选择了出AP,请将是否出AP选择: 否' 
	FROM
	(
		SELECT * FROM [Entity] 
		WHERE EntityID IN
		(
			SELECT DISTINCT EntityID FROM @ContractKeyInfo 
			WHERE EntityID IS NOT NULL
		) AND IsCalculateAP=1 --是否出AP
	) x INNER JOIN 
	(
		SELECT *
		FROM  dbo.VendorEntity
		WHERE VendorEntityID IN 
		(
			SELECT DISTINCT VendorEntityID FROM @ContractKeyInfo 
			WHERE VendorEntityID IS NOT NULL
		)
	) y ON x.EntityID = y.EntityID
	INNER JOIN 
	(
		SELECT VendorNo,IsVirtual 
		FROM dbo.VendorContract 
		WHERE VendorContractID IN 
		(
			SELECT DISTINCT VendorContractID FROM @ContractKeyInfo 
			WHERE VendorContractID IS NOT NULL
		) AND IsVirtual = 1 --虚拟
	) z ON y.VendorNo=z.VendorNo

	
	INSERT INTO @CheckResultTable
	SELECT  'CheckStoreDeptNotInTheCompany',StoreDeptNo,'餐厅部门编号不在合同所属公司内' 
	FROM 
	(
		SELECT * FROM 
		(
			Select StoreNo as StoreDeptNo,StoreName as StoreDeptName,CompanyCode from dbo.SRLS_TB_Master_Store
			UNION ALL 
			Select DeptCode as StoreDeptNo,CompanyCode +':'+ DeptName as StoreDeptName,CompanyCode from dbo.SRLS_TB_Master_Department
		) x WHERE StoreDeptNo IN 
		(
			SELECT StoreOrDeptNo FROM dbo.Entity WHERE EntityID IN 
			(
				SELECT EntityID FROM @ContractKeyInfo
				WHERE EntityID IS NOT NULL 
			)
		)
	) y WHERE CompanyCode NOT IN 
	( 
		SELECT CompanyCode FROM dbo.[Contract] WHERE ContractSnapshotID IN 
		(
			SELECT ContractSnapshotID FROM @ContractKeyInfo
			WHERE ContractSnapshotID IS NOT NULL 
		)
	)
	

	INSERT INTO @CheckResultTable
	SELECT 'CheckEntityInfoSettingNotExist',VC.VendorName+' '+E.EntityName,'不存在实体信息设置数据'
	FROM 
	(SELECT DISTINCT EntityID,VendorContractID FROM @ContractKeyInfo 
		WHERE VendorVendorNo IS NOT NULL 
		AND EntityVendorNo IS NOT NULL
		AND EntityInfoSettingID IS NULL) X 
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckAllRuleDisEnable',EntityName,'不可以所有租金规则都禁用'
	FROM dbo.Entity 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL 
		AND FixedRuleSnapshotID IS NULL 
		AND RatioID IS NULL 
		AND EntityID IS NOT NULL 
	)


	INSERT INTO @CheckResultTable
	SELECT 'CheckAllRatioCycleDisEnable',EntityName,'百分比租金不可以大小周期规则都禁用'
	FROM dbo.Entity 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL 
		AND RatioRuleSnapshotID IS NULL 
		AND EntityID IS NOT NULL 
	)


	--Commented By Eric
	--INSERT INTO @CheckResultTable
	--SELECT 'CheckFirstDueDateError','首次DueDate: '+FirstDueDate+'必须在时间段: '+RightDueDateZone+' 内','首次DueDate错误' FROM fn_GetFirstDueDateError(@ContractSnapshotID)
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'固定租金时间段缺失'
	FROM 
	(SELECT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'固定租金时间段缺失'
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalError',VC.VendorNo+' '+E.EntityName,'固定租金时间段不闭合或者有重叠或者最大最小值不等于实体到租日和起租日'
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	 WHERE FixedRuleSnapshotID IS NOT NULL AND dbo.fn_CheckFixedTimeInterval(FixedRuleSnapshotID) = 0) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	

	 INSERT INTO @CheckResultTable
	 SELECT 'CheckRatioCycleError',e.EntityName,'百分比周期有误: 小周期折合月数大于等于大周期折合月数'
	 FROM
		(
			SELECT * FROM 
			(
				SELECT x.RatioID,x.CycleType AS SmallCycleType,y.CycleType AS BigCycleType,x.CycleMonthCount AS SmallCycleMonthCount,y.CycleMonthCount AS BigCycleMonthCount FROM 
				(
				SELECT * FROM RatioCycleSetting 
				WHERE CycleType='小周期' AND RuleSnapshotID IN (SELECT RatioRuleSnapshotID FROM @ContractKeyInfo WHERE RatioRuleSnapshotID IS NOT NULL)
				)  x INNER JOIN 
				(
				SELECT * FROM RatioCycleSetting 
				WHERE CycleType='大周期' AND RuleSnapshotID IN (SELECT RatioRuleSnapshotID FROM @ContractKeyInfo WHERE RatioRuleSnapshotID IS NOT NULL)
				) y ON x.RatioID=y.RatioID
			) z WHERE SmallCycleMonthCount>=BigCycleMonthCount
		) c INNER JOIN @ContractKeyInfo k ON c.RatioID=k.RatioID
			INNER JOIN Entity e ON e.EntityID=k.EntityID
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'百分比时间段不存在'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL
			AND RatioTimeIntervalID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioTimeIntervalError',VC.VendorNo+' '+E.EntityName,'百分比租金时间段不闭合或者有重叠或者最大最小值不等于实体到租日和起租日'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL AND 
			dbo.fn_CheckRatioTimeInterval(RatioRuleSnapshotID)= 0) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioConditionNotExist',VC.VendorNo+' '+E.EntityName,'百分比条件公式缺失'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioTimeIntervalID IS NOT NULL
			AND ConditionID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		--检查公式是否正确
		DECLARE @CheckResult BIT
		DECLARE @ConditionDesc NVARCHAR(1000)
		DECLARE @SQLCondition NVARCHAR(1000)
		DECLARE @AmountFormulaDesc NVARCHAR(1000)
		DECLARE @SQLAmountFormula NVARCHAR(1000)
		DECLARE @ConditionID NVARCHAR(50)
		DECLARE @MyCursor1 CURSOR
		SET  @MyCursor1 = CURSOR  SCROLL FOR
		SELECT ConditionID,ConditionDesc,SQLCondition,AmountFormulaDesc,SQLAmountFormula 
		FROM  ConditionAmount 
		WHERE ConditionID IN
		( 
			SELECT DISTINCT ConditionID FROM @ContractKeyInfo  WHERE ConditionID IS NOT NULL
		)
		OPEN @MyCursor1;
		FETCH NEXT FROM @MyCursor1 INTO @ConditionID,@ConditionDesc,@SQLCondition,@AmountFormulaDesc,@SQLAmountFormula;
		WHILE @@FETCH_STATUS = 0
		   BEGIN
		   		EXEC usp_Cal_CheckFormulaString @ConditionID,@SQLAmountFormula,@CheckResult OUTPUT
		   		IF @CheckResult=0
		   			BEGIN
		   				INSERT INTO @CheckResultTable
		   				SELECT 'CheckFormulaError',@SQLAmountFormula,'公式不正确'
		   			END
		   		
		   		EXEC usp_Cal_CheckSQLCondition @SQLCondition,@CheckResult OUTPUT
				IF @CheckResult = 0
				BEGIN
					INSERT INTO @CheckResultTable
		   			SELECT 'CheckConditionError',@ConditionDesc,'条件不正确'
				END
				
				FETCH NEXT FROM @MyCursor1 INTO @ConditionID,@ConditionDesc,@SQLCondition,@AmountFormulaDesc,@SQLAmountFormula;
		   END;
		CLOSE @MyCursor1;
		DEALLOCATE @MyCursor1;
		
		
		
	--字段是否空验证开始
	INSERT INTO @CheckResultTable
	SELECT 'NULLRentStartDate',EntityName,'实体起租日空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate IS NULL
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLRentEndDate',EntityName,'实体租赁到期日空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentEndDate IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLIsCalculateAP',EntityName,'实体是否出AP空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLAPStartDate',EntityName,'实体是否出AP为是时,出AP日期空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP = 1 AND APStartDate IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLEntityName','','存在实体名称为空的实体'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND EntityName IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStoreOrDeptNo',EntityName,'实体餐厅/部门编号空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND StoreOrDeptNo IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLKioskNo',EntityName,'实体为甜品店时,Kiosk编号不能为空'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND KioskNo IS  NULL AND EntityTypeName='甜品店'
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLRealestateSalse',EntityName,'实体为餐厅/甜品店时地产提供年SALES不能为空'
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND RealestateSales IS  NULL AND (e.EntityTypeName='餐厅'  or e.EntityTypeName='甜品店')
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLTaxRate',EntityName,'实体为餐厅/甜品店时税率不能为空'
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND TaxRate IS  NULL AND (e.EntityTypeName='餐厅'  or e.EntityTypeName='甜品店')
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLZXStartDate',RentType,'固定租金直线租金起始日为空'
	FROM 
	(
		SELECT * FROM FixedRuleSetting  
		WHERE RuleSnapshotID IN 
		(
			SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
			WHERE FixedRuleSnapshotID IS NOT NULL
		)
		AND ZXStartDate IS  NULL 
		AND RentType='固定租金'
	) x INNER JOIN dbo.EntityInfoSetting eis ON x.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE (e.EntityTypeName='餐厅' OR e.EntityTypeName='甜品店')
	
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckZXStartDate',e.EntityName+x.RentType+' '+x.ZXStartDate,'固定租金直线租金起始既不是开业日也不是起租日'
	FROM 
	(
		SELECT * FROM FixedRuleSetting  
		WHERE RuleSnapshotID IN 
		(
			SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
			WHERE FixedRuleSnapshotID IS NOT NULL
		)
		AND RentType='固定租金'
	) x INNER JOIN dbo.EntityInfoSetting eis ON x.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE (dbo.fn_GetDate(x.ZXStartDate) <> dbo.fn_GetDate(e.OpeningDate) AND dbo.fn_GetDate(x.ZXStartDate)<>dbo.fn_GetDate(e.RentStartDate))
	AND  (e.EntityTypeName='餐厅' OR e.EntityTypeName='甜品店')
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLFirstDueDate',RentType,'租金规则首次DUEDATE为空'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND FirstDueDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLPayType',RentType,'租金规则支付类型DUEDATE为空'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND PayType IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLDescription',RentType,'租金规则固定租金摘要为空'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStartDate',RentType,'租金规则时间段开始时间为空'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLEndDate',RentType,'租金规则时间段结束时间为空'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLAmount',RentType,'租金规则金额为空'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND Amount IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCycle',RentType,'租金规则结算周期为空'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCalendar',RentType,'租金规则租赁/公历为空'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLDescription',RentType,'租金规则百分比租金摘要为空'
	FROM RatioRuleSetting
	WHERE RatioID IN 
	(
		SELECT RatioID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLIsPure',RentType,'租金规则是否纯百分比为空'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND IsPure IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLFirstDueDate',RentType,'租金规则首次DUTDATE为空'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND FirstDueDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCycle',RentType,'租金规则结算周期为空'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCalendar',RentType,'租金规则租赁/公历为空'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStartDate',RentType,'租金规则时间段开始时间为空'
	FROM RatioTimeIntervalSetting rtis INNER JOIN  RatioCycleSetting rcs
	ON rcs.RuleSnapshotID = rtis.RuleSnapshotID
	INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE TimeIntervalID IN 
	(
		SELECT RatioTimeIntervalID FROM @ContractKeyInfo 
		WHERE RatioTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLEndDate',RentType,'租金规则时间段结束时间为空'
	FROM RatioTimeIntervalSetting rtis INNER JOIN  RatioCycleSetting rcs
	ON rcs.RuleSnapshotID = rtis.RuleSnapshotID
	INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE TimeIntervalID IN 
	(
		SELECT RatioTimeIntervalID FROM @ContractKeyInfo 
		WHERE RatioTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLAmountFormulaDesc',RentType,'租金规则公式为空'
	FROM ConditionAmount ca INNER JOIN RatioTimeIntervalSetting rtis ON rtis.TimeIntervalID = ca.TimeIntervalID
	INNER JOIN  RatioCycleSetting rcs
	ON rcs.RuleSnapshotID = rtis.RuleSnapshotID
	INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE ConditionID IN 
	(
		SELECT ConditionID FROM @ContractKeyInfo 
		WHERE ConditionID IS NOT NULL
	)
	AND (AmountFormulaDesc IS  NULL OR ltrim(rtrim(AmountFormulaDesc)) = '')
	--字段是否为空骓结束
	
	--判断合同内的实体的租赁起止时间是否与其他合同中相同实体的租赁起止时间有交集 -- Begin
	DECLARE @EntityIDCursor CURSOR, @EntityID nvarchar(50), @StoreOrDeptNo nvarchar(50), @KioskNo nvarchar(50)
	DECLARE @RentStartDate DATE, @RentEndDate DATE, @EntityTypeName nvarchar(50)
	SET @EntityIDCursor = CURSOR SCROLL FOR 
	SELECT EntityID, EntityTypeName, StoreOrDeptNo, KioskNo, RentStartDate, RentEndDate FROM Entity 
	WHERE EntityID IN (SELECT DISTINCT EntityID FROM @ContractKeyInfo)
	
	OPEN @EntityIDCursor
	FETCH NEXT FROM @EntityIDCursor INTO @EntityID, @EntityTypeName, @StoreOrDeptNo, @KioskNo, @RentStartDate, @RentEndDate
	WHILE @@FETCH_STATUS=0
	BEGIN
		INSERT INTO @CheckResultTable
		SELECT 'RentStartEndDateIntersection', C.ContractNO, '实体租赁起止时间与生效合同的租赁时间重叠'
		FROM Entity E INNER JOIN Contract C 
		ON E.ContractSnapshotID = C.ContractSnapshotID AND C.ContractSnapshotID <> @ContractSnapshotID
		WHERE C.SnapshotCreateTime IS NULL AND ISNULL(E.StoreOrDeptNo,'') = ISNULL(@StoreOrDeptNo,'') 
		AND ISNULL(E.KioskNo,'')=ISNULL(@KioskNo,'')
		AND (
				(RentStartDate BETWEEN @RentStartDate AND @RentEndDate) OR
				(RentEndDate BETWEEN @RentStartDate AND @RentEndDate) OR
				(@RentStartDate BETWEEN RentStartDate AND RentEndDate) OR
				(@RentEndDate BETWEEN RentStartDate AND RentEndDate)
			)
		AND E.EntityTypeName = @EntityTypeName
		
		FETCH NEXT FROM @EntityIDCursor INTO @EntityID, @EntityTypeName, @StoreOrDeptNo, @KioskNo, @RentStartDate, @RentEndDate
	END
	CLOSE @EntityIDCursor
	DEALLOCATE @EntityIDCursor
	--判断合同内的实体的租赁起止时间是否与其他合同中相同实体的租赁起止时间有交集 -- End
	
	
	--返回检查结果
	SELECT * FROM @CheckResultTable
END




GO


