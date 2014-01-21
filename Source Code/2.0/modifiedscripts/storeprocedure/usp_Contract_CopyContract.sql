USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_CopyContract]    Script Date: 06/29/2012 10:18:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO














-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	����һ����ͬ
-- =============================================
ALTER PROCEDURE [dbo].[usp_Contract_CopyContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50),--��ͬ����ID
	@OperationID NVARCHAR(50),--��ǰ������ID
	@CopyType NVARCHAR(50), --���/����                         
	@NewContractSnapshotID nvarchar(50) OUTPUT --�º�ͬ����ID
AS
BEGIN
	--����ʼ
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--һ���������
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		--��ͬ���ID��
		DECLARE @ContractKeyInfo TABLE (IndexId INT  PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
		INSERT INTO @ContractKeyInfo
		SELECT * FROM dbo.fn_SelectContractKeyInfo(@ContractSnapshotID)
		
		--Modified by Eric
		DECLARE @TmpGUIDStr NVARCHAR(50)
		SET @TmpGUIDStr = NEWID()
		SELECT @NewContractSnapshotID = /*'vc-' +*/ @TmpGUIDStr
		--End
		
		DECLARE @OperationUserName NVARCHAR(32)
		SELECT @OperationUserName=UserName FROM dbo.SRLS_TB_Master_User Where ID=@OperationID
		--�����,���ڴ洢���ƹ����еľ���ID
		DECLARE @TempTable Table (OldKey NVARCHAR(50) PRIMARY KEY,NewKey NVARCHAR(50),TableName NVARCHAR(50)) 

		--��ʱ�����¾�ID��Ӧ��ϵ��ʼ
		INSERT INTO @TempTable
		SELECT ContractSnapshotID,@NewContractSnapshotID,'Contract'
		FROM (SELECT DISTINCT ContractSnapshotID FROM @ContractKeyInfo 
		WHERE  ContractSnapshotID IS NOT NULL) X 
		
		INSERT INTO @TempTable
		SELECT VendorContractID,NEWID(),'VendorContract'
		FROM (SELECT DISTINCT VendorContractID FROM @ContractKeyInfo
		WHERE  VendorContractID IS NOT NULL ) X
		
		INSERT INTO @TempTable
		SELECT VendorEntityID,NEWID(),'VendorEntity'
		FROM (SELECT DISTINCT VendorEntityID FROM @ContractKeyInfo
		WHERE  VendorEntityID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT EntityID,NEWID(),'Entity'
		FROM (SELECT DISTINCT EntityID FROM @ContractKeyInfo
		WHERE  EntityID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT EntityInfoSettingID,NEWID(),'EntityInfoSetting'
		FROM (SELECT DISTINCT EntityInfoSettingID FROM @ContractKeyInfo
		WHERE  EntityInfoSettingID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT FixedRuleSnapshotID,NEWID(),'FixedRuleSetting'
		FROM (SELECT DISTINCT FixedRuleSnapshotID FROM @ContractKeyInfo
		WHERE  FixedRuleSnapshotID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT FixedTimeIntervalID,NEWID(),'FixedTimeIntervalSetting'
		FROM ( SELECT DISTINCT FixedTimeIntervalID FROM @ContractKeyInfo
		WHERE  FixedTimeIntervalID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT RatioID,NEWID(),'RatioRuleSetting'
		FROM (SELECT DISTINCT RatioID FROM @ContractKeyInfo
		WHERE  RatioID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT RatioRuleSnapshotID,NEWID(),'RatioCycleSetting'
		FROM (SELECT DISTINCT RatioRuleSnapshotID FROM @ContractKeyInfo
		WHERE  RatioRuleSnapshotID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT RatioTimeIntervalID,NEWID(),'RatioTimeIntervalSetting'
		FROM (SELECT DISTINCT RatioTimeIntervalID FROM @ContractKeyInfo
		WHERE  RatioTimeIntervalID IS NOT NULL) X
		
		INSERT INTO @TempTable
		SELECT ConditionID,NEWID(),'ConditionAmount'
		FROM (SELECT DISTINCT ConditionID FROM @ContractKeyInfo
		WHERE  ConditionID IS NOT NULL) X
		--��ʱ�����¾�ID��Ӧ��ϵ����

		--����ǰ��ͬ��ɿ���
		IF @CopyType='���'
		BEGIN
			UPDATE [Contract] SET SnapshotCreateTime=GETDATE() 
			WHERE ContractSnapshotID=@ContractSnapshotID
		END
	
		--���Ƹ�����
		INSERT INTO dbo.SYS_Attachments (
			ID,Category,ObjectID,[FileName],FileType,FileSize,FilePath,
			Extend1,Extend2,Extend3,Extend4,Extend5,
			CreateUserID,CreateUserName,CreateTime,ModifyUserID,ModifyUserName,ModifyTime)
		SELECT 
			NEWID(),Category,@NewContractSnapshotID,[FileName],FileType,FileSize,FilePath,
			Extend1,Extend2,Extend3,Extend4,Extend5,
			CreateUserID,CreateUserName,CreateTime,ModifyUserID,ModifyUserName,ModifyTime
		FROM dbo.SYS_Attachments
		WHERE Category='Contract' AND ObjectID=@ContractSnapshotID
		
		--���ƺ�ͬ��
		--Modified by Eric
		SET @TmpGUIDStr = NEWID()
		SET @TmpGUIDStr = /*'vc-' +*/ @TmpGUIDStr
		INSERT INTO [Contract]
		SELECT @NewContractSnapshotID,[CompanyCode],
		CASE WHEN @CopyType='���' THEN ContractID WHEN @CopyType='����' THEN @TmpGUIDStr ELSE @TmpGUIDStr END AS [ContractID],
		--End
		NULL AS [ContractNO],
		CASE WHEN @CopyType='���' THEN CAST(ISNULL([Version],1) AS INT)+1 WHEN @CopyType='����' THEN 1 ELSE 1 END AS Version,
			[ContractName],[CompanyName],[CompanySimpleName],[CompanyRemark],'�ݸ�' AS [Status],[Remark],
			[UpdateInfo],0,GETDATE(),CreatorName,
			GETDATE() AS LastModifyTime,@OperationUserName AS LastModifyUserName,
			NULL AS SnapshotCreateTime,0 AS IsSave,@CopyType AS PartComment, 0 AS FromSRLS
		FROM [dbo].[Contract]
		WHERE ContractSnapshotID=@ContractSnapshotID
		
		--����ҵ����ͬ��ϵ��
		INSERT INTO [VendorContract]
		SELECT NEWID() AS [VendorContractID],@NewContractSnapshotID,[VendorNo],[VendorName],[PayMentType],[IsVirtual]
		FROM [dbo].[VendorContract]
		WHERE ContractSnapshotID=@ContractSnapshotID


		--����ʵ���
		INSERT INTO [Entity]
		SELECT y.NewKey AS [EntityID],[EntityTypeName],@NewContractSnapshotID,[EntityNo],[EntityName],
			[StoreOrDept],[StoreOrDeptNo],[KioskNo],[OpeningDate],[RentStartDate],[RentEndDate],
			[IsCalculateAP],[APStartDate],[Remark]  
		FROM [dbo].[Entity] x INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='Entity') y ON x.EntityID=y.OldKey
		WHERE x.ContractSnapshotID=@ContractSnapshotID
		
		
		--����ҵ��ʵ���
		INSERT INTO [VendorEntity]
		SELECT NEWID(),y.NewKey AS [EntityID],[VendorNo]
		FROM [dbo].[VendorEntity]  x INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='Entity') y ON x.EntityID=y.OldKey
		WHERE x.VendorEntityID IN 
		(
			SELECT VendorEntityID 
			FROM @ContractKeyInfo 
			WHERE  VendorEntityID IS NOT NULL
		)
		
		--����ʵ����Ϣ������
		INSERT INTO EntityInfoSetting
		SELECT y.NewKey AS [EntityInfoSettingID],z.NewKey AS [EntityID],[VendorNo],[RealestateSales]
		  ,[MarginAmount],[MarginRemark],[TaxRate]
		FROM [dbo].[EntityInfoSetting]  x 
		INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') y  ON x.EntityInfoSettingID=y.OldKey
		INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='Entity') z  ON x.EntityID=z.OldKey
	
		
		--���ƹ̶�����
		INSERT INTO [FixedRuleSetting]
		SELECT y.NewKey AS [RuleSnapshotID],z.NewKey AS [EntityInfoSettingID],
		CASE WHEN @CopyType='���' THEN [RuleID] WHEN @CopyType='����' THEN NEWID() ELSE NEWID() END AS RuleID,[RentType]
		  ,[FirstDueDate],[NextDueDate],[NextAPStartDate],[NextAPEndDate]
		  ,[NextGLStartDate],[NextGLEndDate],[PayType],[ZXStartDate],[ZXConstant],[Cycle],[CycleMonthCount],
			[Calendar],[Description],[Remark]
		  ,NULL AS [SnapshotCreateTime]
		FROM [dbo].[FixedRuleSetting] x
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedRuleSetting') y  ON x.RuleSnapshotID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') z  ON x.EntityInfoSettingID=z.OldKey
	
		
		IF @CopyType='���'
		BEGIN
			UPDATE [FixedRuleSetting] SET SnapshotCreateTime = GETDATE()
			WHERE RuleSnapshotID IN
			(
				SELECT FixedRuleSnapshotID 
				FROM @ContractKeyInfo 
				WHERE  FixedRuleSnapshotID IS NOT NULL
			)
		END
	
		
		--���ƹ̶�ʱ���
		INSERT INTO [FixedTimeIntervalSetting]
		SELECT y.NewKey AS [TimeIntervalID],z.NewKey AS [RuleSnapshotID],[StartDate],[EndDate]
		  ,[Amount],[Cycle],[CycleMonthCount],[Calendar]
		FROM [dbo].[FixedTimeIntervalSetting] x
		 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedTimeIntervalSetting') y  ON x.TimeIntervalID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedRuleSetting') z  ON x.RuleSnapshotID=z.OldKey

		
		--���ưٷֱȹ���
		INSERT INTO [RatioRuleSetting]
		SELECT y.NewKey AS [RatioID],z.NewKey AS [EntityInfoSettingID],[RentType],[Description],[Remark]
		FROM [dbo].[RatioRuleSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioRuleSetting') y  ON x.RatioID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') z  ON x.EntityInfoSettingID=z.OldKey
	

		--���ưٷֱ�����
		INSERT INTO [RatioCycleSetting]
		SELECT z.NewKey AS [RatioID],y.NewKey AS [RuleSnapshotID],
		CASE WHEN @CopyType='���' THEN [RuleID] WHEN @CopyType='����' THEN NEWID() ELSE NEWID() END AS RuleID,[IsPure]
		  ,[FirstDueDate],[NextDueDate],[NextAPStartDate],[NextAPEndDate]
		  ,[NextGLStartDate],[NextGLEndDate],[PayType],[ZXStartDate],[Cycle]
		  ,[CycleMonthCount] ,[Calendar],[CycleType],NULL AS [SnapshotCreateTime]
		FROM [dbo].[RatioCycleSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioCycleSetting') y  ON x.RuleSnapshotID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioRuleSetting') z  ON x.RatioID=z.OldKey
	
		
		IF @CopyType='���'
		BEGIN
			UPDATE [RatioCycleSetting] SET SnapshotCreateTime = GETDATE() 
			WHERE RuleSnapshotID IN
			(
				SELECT RatioRuleSnapshotID
				FROM @ContractKeyInfo 
				WHERE  RatioRuleSnapshotID IS NOT NULL
			)
		END
		
		
		--���ưٷֱ�ʱ���
		INSERT INTO [RatioTimeIntervalSetting]
		SELECT y.NewKey AS [TimeIntervalID],z.NewKey AS [RuleSnapshotID],[StartDate],[EndDate]
		FROM [dbo].[RatioTimeIntervalSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioTimeIntervalSetting') y  ON x.TimeIntervalID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioCycleSetting') z  ON x.RuleSnapshotID=z.OldKey

		
		--���ưٷֱ�������ʽ
		INSERT INTO [ConditionAmount]
		SELECT y.NewKey AS [ConditionID], z.NewKey AS [TimeIntervalID]
		  ,[ConditionDesc],[AmountFormulaDesc],[SQLCondition],[SQLAmountFormula]
		FROM [dbo].[ConditionAmount] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='ConditionAmount') y  ON x.ConditionID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioTimeIntervalSetting') z  ON x.TimeIntervalID=z.OldKey
	
		
		
		--�ύ����
		IF @@TRANCOUNT <> 0
	    COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
	    ROLLBACK TRANSACTION
	    DECLARE @MSG nvarchar(max);SELECT @MSG=ERROR_MESSAGE();Raiserror(@msg, 16, 1);
	END CATCH
	--�������
END































GO


