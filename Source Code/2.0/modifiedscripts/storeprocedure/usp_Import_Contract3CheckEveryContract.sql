USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Import_Contract3CheckEveryContract]    Script Date: 08/31/2012 17:13:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		zhangbsh
-- Create date:20110406
-- Description:	合同导入
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract3CheckEveryContract]
	-- Add the parameters for the stored procedure here
	@ContractIndex NVARCHAR(50),
	@ContractSnapshotID NVARCHAR(50),
	@UserID NVARCHAR(50)
AS
BEGIN
	--Added by Eric -- Begin
	--如果用户没有导入该公司的权限，则插入错误消息，并返回
	DECLARE @CompanyCode nvarchar(20)
	IF NOT EXISTS(
		SELECT * FROM Contract C INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
		WHERE UC.UserId=@UserID AND C.ContractSnapshotID=@ContractSnapshotID
	)
	BEGIN
		SELECT @CompanyCode=CompanyCode FROM Contract WHERE ContractSnapshotID=@ContractSnapshotID
	
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNoAuthority',@CompanyCode,'您没有合同中公司的可见权限',@ContractIndex)
		RETURN
	END
	--Added by Eric -- End

	--将合同所有主键信息暂存
	DECLARE @ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
	INSERT INTO @ContractKeyInfo 
	SELECT * FROM v_ContractKeyInfo WHERE ContractSnapshotID=@ContractSnapshotID
	--将合同所有主键信息暂存

	
	--检查结果表
	IF NOT EXISTS(SELECT * FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNotExistVendor','','合同中不存在业主',@ContractIndex)
	END
	
	IF NOT EXISTS(SELECT * FROM [Entity] WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNotExistEntity','','合同中不存在实体',@ContractIndex)
	END
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckVendorHasNoEntity',VendorName,'业主没有关联实体' ,@ContractIndex
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
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckVendorStatus',VendorName,'业主状态必须Active' ,@ContractIndex
	FROM SRLS_TB_Master_Vendor 
	WHERE Status <> 'A'
	AND VendorNo IN
	(
		SELECT VendorVendorNo FROM @ContractKeyInfo WHERE VendorVendorNo IS NOT NULL
	)
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckEntityHasNoVendor',EntityName,'实体没有关联业主' ,@ContractIndex
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
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckVirtualVendorAP',x.EntityName,'虚拟业主选择了出AP,请将是否出AP选择: 否',@ContractIndex
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
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckStoreDeptNotInTheCompany',StoreDeptNo,'餐厅部门编号不在合同所属公司内' ,@ContractIndex
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
	

	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckEntityInfoSettingNotExist',VC.VendorName+' '+E.EntityName,'不存在实体信息设置数据',@ContractIndex
	FROM 
	(SELECT DISTINCT EntityID,VendorContractID FROM @ContractKeyInfo 
		WHERE VendorVendorNo IS NOT NULL 
		AND EntityVendorNo IS NOT NULL
		AND EntityInfoSettingID IS NULL) X 
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckAllRuleDisEnable',EntityName,'不可以所有租金规则都禁用',@ContractIndex
	FROM dbo.Entity 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL 
		AND FixedRuleSnapshotID IS NULL 
		AND RatioID IS NULL 
		AND EntityID IS NOT NULL 
	)


	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckAllRatioCycleDisEnable',EntityName,'百分比租金不可以大小周期规则都禁用',@ContractIndex
	FROM dbo.Entity 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL 
		AND RatioRuleSnapshotID IS NULL 
		AND EntityID IS NOT NULL 
	)


	--Commented by Eric -- Begin
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'CheckFirstDueDateError','首次DueDate: '+FirstDueDate,'必须在时间段: '+RightDueDateZone+' 内',@ContractIndex FROM fn_GetFirstDueDateError(@ContractSnapshotID)
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'固定租金时间段缺失',@ContractIndex
	FROM 
	(SELECT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'固定租金时间段缺失',@ContractIndex
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalError',VC.VendorNo+' '+E.EntityName,'固定租金时间段不闭合或者有重叠或者最大最小值不等于实体到租日和起租日',@ContractIndex
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	 WHERE FixedRuleSnapshotID IS NOT NULL AND dbo.fn_CheckFixedTimeInterval(FixedRuleSnapshotID) = 0) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	

	 INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	 SELECT 'CheckRatioCycleError',e.EntityName,'百分比周期有误: 小周期折合月数大于等于大周期折合月数',@ContractIndex
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
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'百分比时间段不存在',@ContractIndex
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL
			AND RatioTimeIntervalID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioTimeIntervalError',VC.VendorNo+' '+E.EntityName,'百分比租金时间段不闭合或者有重叠或者最大最小值不等于实体到租日和起租日',@ContractIndex
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL AND 
			dbo.fn_CheckRatioTimeInterval(RatioRuleSnapshotID)= 0) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioConditionNotExist',VC.VendorNo+' '+E.EntityName,'百分比条件公式缺失',@ContractIndex
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
		   				INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		   				SELECT 'CheckFormulaError',@SQLAmountFormula,'公式不正确',@ContractIndex
		   			END
		   		
		   		EXEC usp_Cal_CheckSQLCondition @SQLCondition,@CheckResult OUTPUT
				IF @CheckResult = 0
				BEGIN
					INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		   			SELECT 'CheckConditionError',@ConditionDesc,'条件不正确',@ContractIndex
				END
				
				FETCH NEXT FROM @MyCursor1 INTO @ConditionID,@ConditionDesc,@SQLCondition,@AmountFormulaDesc,@SQLAmountFormula;
		   END;
		CLOSE @MyCursor1;
		DEALLOCATE @MyCursor1;
		
		
		
	--字段是否空验证开始
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRentStartDate',EntityName,'实体起租日空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate IS NULL
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRentEndDate',EntityName,'实体租赁到期日空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentEndDate IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLIsCalculateAP',EntityName,'实体是否出AP空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLAPStartDate',EntityName,'实体是否出AP为是时,出AP日期空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP = 1 AND APStartDate IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLEntityName','','存在实体名称为空的实体',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND EntityName IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStoreOrDeptNo',EntityName,'实体餐厅/部门编号空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND StoreOrDeptNo IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLKioskNo',EntityName,'实体为甜品店时,Kiosk编号不能为空',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND KioskNo IS  NULL AND EntityTypeName='甜品店'
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRealestateSalse',EntityName,'实体为餐厅/甜品店时地产提供年SALES不能为空',@ContractIndex
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND RealestateSales IS  NULL AND (e.EntityTypeName='餐厅'  or e.EntityTypeName='甜品店')
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLTaxRate',EntityName,'实体为餐厅/甜品店时税率不能为空',@ContractIndex
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND TaxRate IS  NULL AND (e.EntityTypeName='餐厅'  or e.EntityTypeName='甜品店')
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLZXStartDate',RentType,'固定租金直线租金起始日为空',@ContractIndex
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
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckZXStartDate',e.EntityName+x.RentType+' '+CONVERT(NVARCHAR(10),ZXStartDate,20),'固定租金直线租金起始即不是开业日也不是起租日',@ContractIndex
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
	
	
	--Commented by Eric -- Begin
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLFirstDueDate',RentType,'租金规则首次DUEDATE为空',@ContractIndex
	--FROM FixedRuleSetting
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE FixedRuleSnapshotID IS NOT NULL
	--)
	--AND FirstDueDate IS  NULL
	
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLPayType',RentType,'租金规则支付类型DUEDATE为空',@ContractIndex
	--FROM FixedRuleSetting
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE FixedRuleSnapshotID IS NOT NULL
	--)
	--AND PayType IS  NULL
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLDescription',RentType,'租金规则固定租金摘要为空',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStartDate',RentType,'租金规则时间段开始时间为空',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLEndDate',RentType,'租金规则时间段结束时间为空',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLAmount',RentType,'租金规则金额为空',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND Amount IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCycle',RentType,'租金规则结算周期为空',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCalendar',RentType,'租金规则租赁/公历为空',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLDescription',RentType,'租金规则百分比租金摘要为空',@ContractIndex
	FROM RatioRuleSetting
	WHERE RatioID IN 
	(
		SELECT RatioID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLIsPure',RentType,'租金规则是否纯百分比为空',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND IsPure IS  NULL
	
	
	----Commented by Eric -- Begin
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLFirstDueDate',RentType,'租金规则首次DUTDATE为空',@ContractIndex
	--FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE RatioRuleSnapshotID IS NOT NULL
	--)
	--AND FirstDueDate IS  NULL
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCycle',RentType,'租金规则结算周期为空',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCalendar',RentType,'租金规则租赁/公历为空',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStartDate',RentType,'租金规则时间段开始时间为空',@ContractIndex
	FROM RatioTimeIntervalSetting rtis INNER JOIN  RatioCycleSetting rcs
	ON rcs.RuleSnapshotID = rtis.RuleSnapshotID
	INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE TimeIntervalID IN 
	(
		SELECT RatioTimeIntervalID FROM @ContractKeyInfo 
		WHERE RatioTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLEndDate',RentType,'租金规则时间段结束时间为空',@ContractIndex
	FROM RatioTimeIntervalSetting rtis INNER JOIN  RatioCycleSetting rcs
	ON rcs.RuleSnapshotID = rtis.RuleSnapshotID
	INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE TimeIntervalID IN 
	(
		SELECT RatioTimeIntervalID FROM @ContractKeyInfo 
		WHERE RatioTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLAmountFormulaDesc',RentType,'租金规则公式为空',@ContractIndex
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
		INSERT INTO dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'RentStartEndDateIntersection', C.ContractNO, '实体租赁起止时间与生效合同的租赁时间重叠', @ContractIndex
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

    
END





GO


