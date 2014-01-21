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
-- Description:	复制一个合同
-- =============================================
ALTER PROCEDURE [dbo].[usp_Contract_CopyContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50),--合同快照ID
	@OperationID NVARCHAR(50),--当前操作人ID
	@CopyType NVARCHAR(50), --变更/续租                         
	@NewContractSnapshotID nvarchar(50) OUTPUT --新合同快照ID
AS
BEGIN
	--事务开始
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--一堆事务操作
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		--合同相关ID表
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
		--表变量,用于存储复制过程中的旧新ID
		DECLARE @TempTable Table (OldKey NVARCHAR(50) PRIMARY KEY,NewKey NVARCHAR(50),TableName NVARCHAR(50)) 

		--临时保存新旧ID对应关系开始
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
		--临时保存新旧ID对应关系结束

		--将当前合同变成快照
		IF @CopyType='变更'
		BEGIN
			UPDATE [Contract] SET SnapshotCreateTime=GETDATE() 
			WHERE ContractSnapshotID=@ContractSnapshotID
		END
	
		--复制附件表
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
		
		--复制合同表
		--Modified by Eric
		SET @TmpGUIDStr = NEWID()
		SET @TmpGUIDStr = /*'vc-' +*/ @TmpGUIDStr
		INSERT INTO [Contract]
		SELECT @NewContractSnapshotID,[CompanyCode],
		CASE WHEN @CopyType='变更' THEN ContractID WHEN @CopyType='续租' THEN @TmpGUIDStr ELSE @TmpGUIDStr END AS [ContractID],
		--End
		NULL AS [ContractNO],
		CASE WHEN @CopyType='变更' THEN CAST(ISNULL([Version],1) AS INT)+1 WHEN @CopyType='续租' THEN 1 ELSE 1 END AS Version,
			[ContractName],[CompanyName],[CompanySimpleName],[CompanyRemark],'草稿' AS [Status],[Remark],
			[UpdateInfo],0,GETDATE(),CreatorName,
			GETDATE() AS LastModifyTime,@OperationUserName AS LastModifyUserName,
			NULL AS SnapshotCreateTime,0 AS IsSave,@CopyType AS PartComment, 0 AS FromSRLS
		FROM [dbo].[Contract]
		WHERE ContractSnapshotID=@ContractSnapshotID
		
		--复制业主合同关系表
		INSERT INTO [VendorContract]
		SELECT NEWID() AS [VendorContractID],@NewContractSnapshotID,[VendorNo],[VendorName],[PayMentType],[IsVirtual]
		FROM [dbo].[VendorContract]
		WHERE ContractSnapshotID=@ContractSnapshotID


		--复制实体表
		INSERT INTO [Entity]
		SELECT y.NewKey AS [EntityID],[EntityTypeName],@NewContractSnapshotID,[EntityNo],[EntityName],
			[StoreOrDept],[StoreOrDeptNo],[KioskNo],[OpeningDate],[RentStartDate],[RentEndDate],
			[IsCalculateAP],[APStartDate],[Remark]  
		FROM [dbo].[Entity] x INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='Entity') y ON x.EntityID=y.OldKey
		WHERE x.ContractSnapshotID=@ContractSnapshotID
		
		
		--复制业主实体表
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
		
		--复制实体信息设置区
		INSERT INTO EntityInfoSetting
		SELECT y.NewKey AS [EntityInfoSettingID],z.NewKey AS [EntityID],[VendorNo],[RealestateSales]
		  ,[MarginAmount],[MarginRemark],[TaxRate]
		FROM [dbo].[EntityInfoSetting]  x 
		INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') y  ON x.EntityInfoSettingID=y.OldKey
		INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='Entity') z  ON x.EntityID=z.OldKey
	
		
		--复制固定规则
		INSERT INTO [FixedRuleSetting]
		SELECT y.NewKey AS [RuleSnapshotID],z.NewKey AS [EntityInfoSettingID],
		CASE WHEN @CopyType='变更' THEN [RuleID] WHEN @CopyType='续租' THEN NEWID() ELSE NEWID() END AS RuleID,[RentType]
		  ,[FirstDueDate],[NextDueDate],[NextAPStartDate],[NextAPEndDate]
		  ,[NextGLStartDate],[NextGLEndDate],[PayType],[ZXStartDate],[ZXConstant],[Cycle],[CycleMonthCount],
			[Calendar],[Description],[Remark]
		  ,NULL AS [SnapshotCreateTime]
		FROM [dbo].[FixedRuleSetting] x
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedRuleSetting') y  ON x.RuleSnapshotID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') z  ON x.EntityInfoSettingID=z.OldKey
	
		
		IF @CopyType='变更'
		BEGIN
			UPDATE [FixedRuleSetting] SET SnapshotCreateTime = GETDATE()
			WHERE RuleSnapshotID IN
			(
				SELECT FixedRuleSnapshotID 
				FROM @ContractKeyInfo 
				WHERE  FixedRuleSnapshotID IS NOT NULL
			)
		END
	
		
		--复制固定时间段
		INSERT INTO [FixedTimeIntervalSetting]
		SELECT y.NewKey AS [TimeIntervalID],z.NewKey AS [RuleSnapshotID],[StartDate],[EndDate]
		  ,[Amount],[Cycle],[CycleMonthCount],[Calendar]
		FROM [dbo].[FixedTimeIntervalSetting] x
		 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedTimeIntervalSetting') y  ON x.TimeIntervalID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='FixedRuleSetting') z  ON x.RuleSnapshotID=z.OldKey

		
		--复制百分比规则
		INSERT INTO [RatioRuleSetting]
		SELECT y.NewKey AS [RatioID],z.NewKey AS [EntityInfoSettingID],[RentType],[Description],[Remark]
		FROM [dbo].[RatioRuleSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioRuleSetting') y  ON x.RatioID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='EntityInfoSetting') z  ON x.EntityInfoSettingID=z.OldKey
	

		--复制百分比周期
		INSERT INTO [RatioCycleSetting]
		SELECT z.NewKey AS [RatioID],y.NewKey AS [RuleSnapshotID],
		CASE WHEN @CopyType='变更' THEN [RuleID] WHEN @CopyType='续租' THEN NEWID() ELSE NEWID() END AS RuleID,[IsPure]
		  ,[FirstDueDate],[NextDueDate],[NextAPStartDate],[NextAPEndDate]
		  ,[NextGLStartDate],[NextGLEndDate],[PayType],[ZXStartDate],[Cycle]
		  ,[CycleMonthCount] ,[Calendar],[CycleType],NULL AS [SnapshotCreateTime]
		FROM [dbo].[RatioCycleSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioCycleSetting') y  ON x.RuleSnapshotID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioRuleSetting') z  ON x.RatioID=z.OldKey
	
		
		IF @CopyType='变更'
		BEGIN
			UPDATE [RatioCycleSetting] SET SnapshotCreateTime = GETDATE() 
			WHERE RuleSnapshotID IN
			(
				SELECT RatioRuleSnapshotID
				FROM @ContractKeyInfo 
				WHERE  RatioRuleSnapshotID IS NOT NULL
			)
		END
		
		
		--复制百分比时间段
		INSERT INTO [RatioTimeIntervalSetting]
		SELECT y.NewKey AS [TimeIntervalID],z.NewKey AS [RuleSnapshotID],[StartDate],[EndDate]
		FROM [dbo].[RatioTimeIntervalSetting] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioTimeIntervalSetting') y  ON x.TimeIntervalID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioCycleSetting') z  ON x.RuleSnapshotID=z.OldKey

		
		--复制百分比条件公式
		INSERT INTO [ConditionAmount]
		SELECT y.NewKey AS [ConditionID], z.NewKey AS [TimeIntervalID]
		  ,[ConditionDesc],[AmountFormulaDesc],[SQLCondition],[SQLAmountFormula]
		FROM [dbo].[ConditionAmount] x
			 INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='ConditionAmount') y  ON x.ConditionID=y.OldKey
		  INNER JOIN 
		(SELECT * FROM @TempTable WHERE TableName='RatioTimeIntervalSetting') z  ON x.TimeIntervalID=z.OldKey
	
		
		
		--提交事务
		IF @@TRANCOUNT <> 0
	    COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
	    ROLLBACK TRANSACTION
	    DECLARE @MSG nvarchar(max);SELECT @MSG=ERROR_MESSAGE();Raiserror(@msg, 16, 1);
	END CATCH
	--事务结束
END































GO


