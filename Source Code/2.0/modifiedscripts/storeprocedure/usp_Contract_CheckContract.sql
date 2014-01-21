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
-- Description:	����ͬ
-- =============================================
CREATE PROCEDURE [dbo].[usp_Contract_CheckContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50)
AS
BEGIN
	
		--����ͬ����������Ϣ�ݴ�
		DECLARE @ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
		INSERT INTO @ContractKeyInfo 
		SELECT * FROM v_ContractKeyInfo WHERE ContractSnapshotID=@ContractSnapshotID
		--����ͬ����������Ϣ�ݴ�

		
	--�������
	DECLARE @CheckResultTable TABLE (IndexId int IDENTITY (1, 1) PRIMARY KEY,Code NVARCHAR(500), RelationData NVARCHAR(500),CheckMessage NVARCHAR(500))
	IF NOT EXISTS(SELECT * FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		VALUES('CheckNotExistVendor','','��ͬ�в�����ҵ��')
	END
	
	IF NOT EXISTS(SELECT * FROM [Entity] WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		VALUES('CheckNotExistEntity','','��ͬ�в�����ʵ��')
	END
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckVendorHasNoEntity',VendorName,'ҵ��û�й���ʵ��' 
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
	SELECT  'CheckVendorStatus',VendorName,'ҵ��״̬����Active' 
	FROM SRLS_TB_Master_Vendor 
	WHERE Status <> 'A'
	AND VendorNo IN
	(
		SELECT VendorVendorNo FROM @ContractKeyInfo WHERE VendorVendorNo IS NOT NULL
	)
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckEntityRentDate',EntityName,'�����մ������޵�����' 
	FROM [Entity] 
	WHERE EntityID IN
	(
		SELECT DISTINCT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate > RentEndDate
	
	IF EXISTS(SELECT 1 FROM dbo.[Contract] WHERE PartComment='���' AND ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO @CheckResultTable
		SELECT  'CheckEntityRentEndDate',EntityName,'���޵�����С�ڵ�ǰ�ѷ���AP������' 
		FROM [Entity] 
		WHERE EntityID IN
		(
			SELECT DISTINCT EntityID FROM @ContractKeyInfo 
			WHERE EntityID IS NOT NULL
		)
		AND RentEndDate < dbo.fn_Contract_GetEntityAPMaxCycleEndDate(EntityID)
	END
	
	INSERT INTO @CheckResultTable
	SELECT  'CheckEntityHasNoVendor',EntityName,'ʵ��û�й���ҵ��' 
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
	SELECT  'CheckVirtualVendorAP',x.EntityName,'����ҵ��ѡ���˳�AP,�뽫�Ƿ��APѡ��: ��' 
	FROM
	(
		SELECT * FROM [Entity] 
		WHERE EntityID IN
		(
			SELECT DISTINCT EntityID FROM @ContractKeyInfo 
			WHERE EntityID IS NOT NULL
		) AND IsCalculateAP=1 --�Ƿ��AP
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
		) AND IsVirtual = 1 --����
	) z ON y.VendorNo=z.VendorNo

	
	INSERT INTO @CheckResultTable
	SELECT  'CheckStoreDeptNotInTheCompany',StoreDeptNo,'�������ű�Ų��ں�ͬ������˾��' 
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
	SELECT 'CheckEntityInfoSettingNotExist',VC.VendorName+' '+E.EntityName,'������ʵ����Ϣ��������'
	FROM 
	(SELECT DISTINCT EntityID,VendorContractID FROM @ContractKeyInfo 
		WHERE VendorVendorNo IS NOT NULL 
		AND EntityVendorNo IS NOT NULL
		AND EntityInfoSettingID IS NULL) X 
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckAllRuleDisEnable',EntityName,'���������������򶼽���'
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
	SELECT 'CheckAllRatioCycleDisEnable',EntityName,'�ٷֱ���𲻿��Դ�С���ڹ��򶼽���'
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
	--SELECT 'CheckFirstDueDateError','�״�DueDate: '+FirstDueDate+'������ʱ���: '+RightDueDateZone+' ��','�״�DueDate����' FROM fn_GetFirstDueDateError(@ContractSnapshotID)
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�̶����ʱ���ȱʧ'
	FROM 
	(SELECT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�̶����ʱ���ȱʧ'
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckFixedTimeIntervalError',VC.VendorNo+' '+E.EntityName,'�̶����ʱ��β��պϻ������ص����������Сֵ������ʵ�嵽���պ�������'
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	 WHERE FixedRuleSnapshotID IS NOT NULL AND dbo.fn_CheckFixedTimeInterval(FixedRuleSnapshotID) = 0) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	

	 INSERT INTO @CheckResultTable
	 SELECT 'CheckRatioCycleError',e.EntityName,'�ٷֱ���������: С�����ۺ��������ڵ��ڴ������ۺ�����'
	 FROM
		(
			SELECT * FROM 
			(
				SELECT x.RatioID,x.CycleType AS SmallCycleType,y.CycleType AS BigCycleType,x.CycleMonthCount AS SmallCycleMonthCount,y.CycleMonthCount AS BigCycleMonthCount FROM 
				(
				SELECT * FROM RatioCycleSetting 
				WHERE CycleType='С����' AND RuleSnapshotID IN (SELECT RatioRuleSnapshotID FROM @ContractKeyInfo WHERE RatioRuleSnapshotID IS NOT NULL)
				)  x INNER JOIN 
				(
				SELECT * FROM RatioCycleSetting 
				WHERE CycleType='������' AND RuleSnapshotID IN (SELECT RatioRuleSnapshotID FROM @ContractKeyInfo WHERE RatioRuleSnapshotID IS NOT NULL)
				) y ON x.RatioID=y.RatioID
			) z WHERE SmallCycleMonthCount>=BigCycleMonthCount
		) c INNER JOIN @ContractKeyInfo k ON c.RatioID=k.RatioID
			INNER JOIN Entity e ON e.EntityID=k.EntityID
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�ٷֱ�ʱ��β�����'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL
			AND RatioTimeIntervalID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioTimeIntervalError',VC.VendorNo+' '+E.EntityName,'�ٷֱ����ʱ��β��պϻ������ص����������Сֵ������ʵ�嵽���պ�������'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL AND 
			dbo.fn_CheckRatioTimeInterval(RatioRuleSnapshotID)= 0) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		
		INSERT INTO @CheckResultTable
		SELECT 'CheckRatioConditionNotExist',VC.VendorNo+' '+E.EntityName,'�ٷֱ�������ʽȱʧ'
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioTimeIntervalID IS NOT NULL
			AND ConditionID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		--��鹫ʽ�Ƿ���ȷ
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
		   				SELECT 'CheckFormulaError',@SQLAmountFormula,'��ʽ����ȷ'
		   			END
		   		
		   		EXEC usp_Cal_CheckSQLCondition @SQLCondition,@CheckResult OUTPUT
				IF @CheckResult = 0
				BEGIN
					INSERT INTO @CheckResultTable
		   			SELECT 'CheckConditionError',@ConditionDesc,'��������ȷ'
				END
				
				FETCH NEXT FROM @MyCursor1 INTO @ConditionID,@ConditionDesc,@SQLCondition,@AmountFormulaDesc,@SQLAmountFormula;
		   END;
		CLOSE @MyCursor1;
		DEALLOCATE @MyCursor1;
		
		
		
	--�ֶ��Ƿ����֤��ʼ
	INSERT INTO @CheckResultTable
	SELECT 'NULLRentStartDate',EntityName,'ʵ�������տ�'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate IS NULL
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLRentEndDate',EntityName,'ʵ�����޵����տ�'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentEndDate IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLIsCalculateAP',EntityName,'ʵ���Ƿ��AP��'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLAPStartDate',EntityName,'ʵ���Ƿ��APΪ��ʱ,��AP���ڿ�'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP = 1 AND APStartDate IS NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLEntityName','','����ʵ������Ϊ�յ�ʵ��'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND EntityName IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStoreOrDeptNo',EntityName,'ʵ�����/���ű�ſ�'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND StoreOrDeptNo IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLKioskNo',EntityName,'ʵ��Ϊ��Ʒ��ʱ,Kiosk��Ų���Ϊ��'
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND KioskNo IS  NULL AND EntityTypeName='��Ʒ��'
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLRealestateSalse',EntityName,'ʵ��Ϊ����/��Ʒ��ʱ�ز��ṩ��SALES����Ϊ��'
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND RealestateSales IS  NULL AND (e.EntityTypeName='����'  or e.EntityTypeName='��Ʒ��')
	
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLTaxRate',EntityName,'ʵ��Ϊ����/��Ʒ��ʱ˰�ʲ���Ϊ��'
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND TaxRate IS  NULL AND (e.EntityTypeName='����'  or e.EntityTypeName='��Ʒ��')
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLZXStartDate',RentType,'�̶����ֱ�������ʼ��Ϊ��'
	FROM 
	(
		SELECT * FROM FixedRuleSetting  
		WHERE RuleSnapshotID IN 
		(
			SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
			WHERE FixedRuleSnapshotID IS NOT NULL
		)
		AND ZXStartDate IS  NULL 
		AND RentType='�̶����'
	) x INNER JOIN dbo.EntityInfoSetting eis ON x.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE (e.EntityTypeName='����' OR e.EntityTypeName='��Ʒ��')
	
	
	INSERT INTO @CheckResultTable
	SELECT 'CheckZXStartDate',e.EntityName+x.RentType+' '+x.ZXStartDate,'�̶����ֱ�������ʼ�Ȳ��ǿ�ҵ��Ҳ����������'
	FROM 
	(
		SELECT * FROM FixedRuleSetting  
		WHERE RuleSnapshotID IN 
		(
			SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
			WHERE FixedRuleSnapshotID IS NOT NULL
		)
		AND RentType='�̶����'
	) x INNER JOIN dbo.EntityInfoSetting eis ON x.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE (dbo.fn_GetDate(x.ZXStartDate) <> dbo.fn_GetDate(e.OpeningDate) AND dbo.fn_GetDate(x.ZXStartDate)<>dbo.fn_GetDate(e.RentStartDate))
	AND  (e.EntityTypeName='����' OR e.EntityTypeName='��Ʒ��')
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLFirstDueDate',RentType,'�������״�DUEDATEΪ��'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND FirstDueDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLPayType',RentType,'������֧������DUEDATEΪ��'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND PayType IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLDescription',RentType,'������̶����ժҪΪ��'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStartDate',RentType,'������ʱ��ο�ʼʱ��Ϊ��'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLEndDate',RentType,'������ʱ��ν���ʱ��Ϊ��'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLAmount',RentType,'��������Ϊ��'
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND Amount IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCycle',RentType,'�������������Ϊ��'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCalendar',RentType,'����������/����Ϊ��'
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLDescription',RentType,'������ٷֱ����ժҪΪ��'
	FROM RatioRuleSetting
	WHERE RatioID IN 
	(
		SELECT RatioID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLIsPure',RentType,'�������Ƿ񴿰ٷֱ�Ϊ��'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND IsPure IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLFirstDueDate',RentType,'�������״�DUTDATEΪ��'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND FirstDueDate IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCycle',RentType,'�������������Ϊ��'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLCalendar',RentType,'����������/����Ϊ��'
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO @CheckResultTable
	SELECT 'NULLStartDate',RentType,'������ʱ��ο�ʼʱ��Ϊ��'
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
	SELECT 'NULLEndDate',RentType,'������ʱ��ν���ʱ��Ϊ��'
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
	SELECT 'NULLAmountFormulaDesc',RentType,'������ʽΪ��'
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
	--�ֶ��Ƿ�Ϊ�������
	
	--�жϺ�ͬ�ڵ�ʵ���������ֹʱ���Ƿ���������ͬ����ͬʵ���������ֹʱ���н��� -- Begin
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
		SELECT 'RentStartEndDateIntersection', C.ContractNO, 'ʵ��������ֹʱ������Ч��ͬ������ʱ���ص�'
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
	--�жϺ�ͬ�ڵ�ʵ���������ֹʱ���Ƿ���������ͬ����ͬʵ���������ֹʱ���н��� -- End
	
	
	--���ؼ����
	SELECT * FROM @CheckResultTable
END




GO


