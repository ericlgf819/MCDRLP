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
-- Description:	��ͬ����
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract3CheckEveryContract]
	-- Add the parameters for the stored procedure here
	@ContractIndex NVARCHAR(50),
	@ContractSnapshotID NVARCHAR(50),
	@UserID NVARCHAR(50)
AS
BEGIN
	--Added by Eric -- Begin
	--����û�û�е���ù�˾��Ȩ�ޣ�����������Ϣ��������
	DECLARE @CompanyCode nvarchar(20)
	IF NOT EXISTS(
		SELECT * FROM Contract C INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
		WHERE UC.UserId=@UserID AND C.ContractSnapshotID=@ContractSnapshotID
	)
	BEGIN
		SELECT @CompanyCode=CompanyCode FROM Contract WHERE ContractSnapshotID=@ContractSnapshotID
	
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNoAuthority',@CompanyCode,'��û�к�ͬ�й�˾�Ŀɼ�Ȩ��',@ContractIndex)
		RETURN
	END
	--Added by Eric -- End

	--����ͬ����������Ϣ�ݴ�
	DECLARE @ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
	INSERT INTO @ContractKeyInfo 
	SELECT * FROM v_ContractKeyInfo WHERE ContractSnapshotID=@ContractSnapshotID
	--����ͬ����������Ϣ�ݴ�

	
	--�������
	IF NOT EXISTS(SELECT * FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNotExistVendor','','��ͬ�в�����ҵ��',@ContractIndex)
	END
	
	IF NOT EXISTS(SELECT * FROM [Entity] WHERE ContractSnapshotID=@ContractSnapshotID)
	BEGIN
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		VALUES('CheckNotExistEntity','','��ͬ�в�����ʵ��',@ContractIndex)
	END
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckVendorHasNoEntity',VendorName,'ҵ��û�й���ʵ��' ,@ContractIndex
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
	SELECT  'CheckVendorStatus',VendorName,'ҵ��״̬����Active' ,@ContractIndex
	FROM SRLS_TB_Master_Vendor 
	WHERE Status <> 'A'
	AND VendorNo IN
	(
		SELECT VendorVendorNo FROM @ContractKeyInfo WHERE VendorVendorNo IS NOT NULL
	)
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckEntityHasNoVendor',EntityName,'ʵ��û�й���ҵ��' ,@ContractIndex
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
	SELECT  'CheckVirtualVendorAP',x.EntityName,'����ҵ��ѡ���˳�AP,�뽫�Ƿ��APѡ��: ��',@ContractIndex
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
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT  'CheckStoreDeptNotInTheCompany',StoreDeptNo,'�������ű�Ų��ں�ͬ������˾��' ,@ContractIndex
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
	SELECT 'CheckEntityInfoSettingNotExist',VC.VendorName+' '+E.EntityName,'������ʵ����Ϣ��������',@ContractIndex
	FROM 
	(SELECT DISTINCT EntityID,VendorContractID FROM @ContractKeyInfo 
		WHERE VendorVendorNo IS NOT NULL 
		AND EntityVendorNo IS NOT NULL
		AND EntityInfoSettingID IS NULL) X 
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckAllRuleDisEnable',EntityName,'���������������򶼽���',@ContractIndex
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
	SELECT 'CheckAllRatioCycleDisEnable',EntityName,'�ٷֱ���𲻿��Դ�С���ڹ��򶼽���',@ContractIndex
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
	--SELECT 'CheckFirstDueDateError','�״�DueDate: '+FirstDueDate,'������ʱ���: '+RightDueDateZone+' ��',@ContractIndex FROM fn_GetFirstDueDateError(@ContractSnapshotID)
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�̶����ʱ���ȱʧ',@ContractIndex
	FROM 
	(SELECT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�̶����ʱ���ȱʧ',@ContractIndex
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	WHERE FixedRuleSnapshotID IS NOT NULL
		AND FixedTimeIntervalID IS NULL) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckFixedTimeIntervalError',VC.VendorNo+' '+E.EntityName,'�̶����ʱ��β��պϻ������ص����������Сֵ������ʵ�嵽���պ�������',@ContractIndex
	FROM 
	(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
	 WHERE FixedRuleSnapshotID IS NOT NULL AND dbo.fn_CheckFixedTimeInterval(FixedRuleSnapshotID) = 0) X
	INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
	INNER JOIN [Entity] E ON X.EntityID = E.EntityID
	
	

	 INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	 SELECT 'CheckRatioCycleError',e.EntityName,'�ٷֱ���������: С�����ۺ��������ڵ��ڴ������ۺ�����',@ContractIndex
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
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioTimeIntervalNotExist',VC.VendorNo+' '+E.EntityName,'�ٷֱ�ʱ��β�����',@ContractIndex
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL
			AND RatioTimeIntervalID IS NULL) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioTimeIntervalError',VC.VendorNo+' '+E.EntityName,'�ٷֱ����ʱ��β��պϻ������ص����������Сֵ������ʵ�嵽���պ�������',@ContractIndex
		FROM 
		(SELECT DISTINCT VendorContractID,EntityID FROM @ContractKeyInfo 
		 WHERE RatioRuleSnapshotID IS NOT NULL AND 
			dbo.fn_CheckRatioTimeInterval(RatioRuleSnapshotID)= 0) X
		INNER JOIN VendorContract VC ON X.VendorContractID=VC.VendorContractID 
		INNER JOIN [Entity] E ON X.EntityID = E.EntityID
		
		
		INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'CheckRatioConditionNotExist',VC.VendorNo+' '+E.EntityName,'�ٷֱ�������ʽȱʧ',@ContractIndex
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
		   				INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		   				SELECT 'CheckFormulaError',@SQLAmountFormula,'��ʽ����ȷ',@ContractIndex
		   			END
		   		
		   		EXEC usp_Cal_CheckSQLCondition @SQLCondition,@CheckResult OUTPUT
				IF @CheckResult = 0
				BEGIN
					INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		   			SELECT 'CheckConditionError',@ConditionDesc,'��������ȷ',@ContractIndex
				END
				
				FETCH NEXT FROM @MyCursor1 INTO @ConditionID,@ConditionDesc,@SQLCondition,@AmountFormulaDesc,@SQLAmountFormula;
		   END;
		CLOSE @MyCursor1;
		DEALLOCATE @MyCursor1;
		
		
		
	--�ֶ��Ƿ����֤��ʼ
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRentStartDate',EntityName,'ʵ�������տ�',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentStartDate IS NULL
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRentEndDate',EntityName,'ʵ�����޵����տ�',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND RentEndDate IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLIsCalculateAP',EntityName,'ʵ���Ƿ��AP��',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLAPStartDate',EntityName,'ʵ���Ƿ��APΪ��ʱ,��AP���ڿ�',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND IsCalculateAP = 1 AND APStartDate IS NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLEntityName','','����ʵ������Ϊ�յ�ʵ��',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND EntityName IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStoreOrDeptNo',EntityName,'ʵ�����/���ű�ſ�',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND StoreOrDeptNo IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLKioskNo',EntityName,'ʵ��Ϊ��Ʒ��ʱ,Kiosk��Ų���Ϊ��',@ContractIndex
	FROM [Entity] 
	WHERE EntityID IN 
	(
		SELECT EntityID FROM @ContractKeyInfo 
		WHERE EntityID IS NOT NULL
	)
	AND KioskNo IS  NULL AND EntityTypeName='��Ʒ��'
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLRealestateSalse',EntityName,'ʵ��Ϊ����/��Ʒ��ʱ�ز��ṩ��SALES����Ϊ��',@ContractIndex
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND RealestateSales IS  NULL AND (e.EntityTypeName='����'  or e.EntityTypeName='��Ʒ��')
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLTaxRate',EntityName,'ʵ��Ϊ����/��Ʒ��ʱ˰�ʲ���Ϊ��',@ContractIndex
	FROM EntityInfoSetting eis INNER JOIN Entity e ON e.EntityID = eis.EntityID 
	WHERE eis.EntityInfoSettingID IN 
	(
		SELECT EntityInfoSettingID FROM @ContractKeyInfo 
		WHERE EntityInfoSettingID IS NOT NULL
	)
	AND TaxRate IS  NULL AND (e.EntityTypeName='����'  or e.EntityTypeName='��Ʒ��')
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLZXStartDate',RentType,'�̶����ֱ�������ʼ��Ϊ��',@ContractIndex
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
	
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'CheckZXStartDate',e.EntityName+x.RentType+' '+CONVERT(NVARCHAR(10),ZXStartDate,20),'�̶����ֱ�������ʼ�����ǿ�ҵ��Ҳ����������',@ContractIndex
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
	
	
	--Commented by Eric -- Begin
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLFirstDueDate',RentType,'�������״�DUEDATEΪ��',@ContractIndex
	--FROM FixedRuleSetting
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE FixedRuleSnapshotID IS NOT NULL
	--)
	--AND FirstDueDate IS  NULL
	
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLPayType',RentType,'������֧������DUEDATEΪ��',@ContractIndex
	--FROM FixedRuleSetting
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE FixedRuleSnapshotID IS NOT NULL
	--)
	--AND PayType IS  NULL
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLDescription',RentType,'������̶����ժҪΪ��',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStartDate',RentType,'������ʱ��ο�ʼʱ��Ϊ��',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND StartDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLEndDate',RentType,'������ʱ��ν���ʱ��Ϊ��',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND EndDate IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLAmount',RentType,'��������Ϊ��',@ContractIndex
	FROM FixedTimeIntervalSetting ftis inner join FixedRuleSetting frs ON frs.RuleSnapshotID = ftis.RuleSnapshotID
	WHERE TimeIntervalID IN 
	(
		SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
		WHERE FixedTimeIntervalID IS NOT NULL
	)
	AND Amount IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCycle',RentType,'�������������Ϊ��',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCalendar',RentType,'����������/����Ϊ��',@ContractIndex
	FROM FixedRuleSetting
	WHERE RuleSnapshotID IN 
	(
		SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
		WHERE FixedRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLDescription',RentType,'������ٷֱ����ժҪΪ��',@ContractIndex
	FROM RatioRuleSetting
	WHERE RatioID IN 
	(
		SELECT RatioID FROM @ContractKeyInfo 
		WHERE RatioID IS NOT NULL
	)
	AND [Description] IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLIsPure',RentType,'�������Ƿ񴿰ٷֱ�Ϊ��',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND IsPure IS  NULL
	
	
	----Commented by Eric -- Begin
	--INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	--SELECT 'NULLFirstDueDate',RentType,'�������״�DUTDATEΪ��',@ContractIndex
	--FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	--WHERE RuleSnapshotID IN 
	--(
	--	SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
	--	WHERE RatioRuleSnapshotID IS NOT NULL
	--)
	--AND FirstDueDate IS  NULL
	--Commented by Eric -- End
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCycle',RentType,'�������������Ϊ��',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Cycle IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLCalendar',RentType,'����������/����Ϊ��',@ContractIndex
	FROM RatioCycleSetting rcs INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = rcs.RatioID
	WHERE RuleSnapshotID IN 
	(
		SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
		WHERE RatioRuleSnapshotID IS NOT NULL
	)
	AND Calendar IS  NULL
	
	INSERT INTO  dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
	SELECT 'NULLStartDate',RentType,'������ʱ��ο�ʼʱ��Ϊ��',@ContractIndex
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
	SELECT 'NULLEndDate',RentType,'������ʱ��ν���ʱ��Ϊ��',@ContractIndex
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
	SELECT 'NULLAmountFormulaDesc',RentType,'������ʽΪ��',@ContractIndex
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
		INSERT INTO dbo.Import_ContractMessage(Enflag,RelationData,CheckMessage,ContractIndex)
		SELECT 'RentStartEndDateIntersection', C.ContractNO, 'ʵ��������ֹʱ������Ч��ͬ������ʱ���ص�', @ContractIndex
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

    
END





GO


