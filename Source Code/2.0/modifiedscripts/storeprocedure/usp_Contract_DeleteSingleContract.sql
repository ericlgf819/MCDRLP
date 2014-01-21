USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_DeleteSingleContract]    Script Date: 08/01/2012 14:06:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









-- ========================================
-- Author:     liujj
-- Create Date:20101221
-- Description:删除单个合同信息
-- ========================================
CREATE PROCEDURE [dbo].[usp_Contract_DeleteSingleContract]
	@ContractSnapshotID nvarchar(50),
	@OperationID NVARCHAR(50) = NULL

AS

SET NOCOUNT ON
BEGIN
		--事务开始
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
			--一堆事务操作
			DECLARE @ContractID NVARCHAR(50)  --合同ID用于回滚旧版本
			SELECT @ContractID=ContractID
			FROM [Contract] 
			WHERE ContractSnapshotID=@ContractSnapshotID

			Declare @Xml xml,@OperationEnglishName nvarchar(32),@CompanyCode NVARCHAR(50)
			Select @OperationEnglishName=EnglishName 
			from dbo.SRLS_TB_Master_User 
			Where ID=@OperationID
			
			SELECT @CompanyCode = CompanyCode 
			FROM dbo.[Contract] 
			WHERE ContractSnapshotID = @ContractSnapshotID
		

			--合同相关ID表
			DECLARE @ContractKeyInfo TABLE (IndexId INT  PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
			INSERT INTO @ContractKeyInfo
			SELECT * FROM dbo.fn_SelectContractKeyInfo(@ContractSnapshotID)
			
			DELETE FROM  dbo.ConditionAmountNumbers 
			WHERE ConditionID IN 
			(
				SELECT ConditionID FROM @ContractKeyInfo 
				WHERE  ConditionID IS NOT NULL
			)
			

			--条件公式
			DELETE FROM ConditionAmount WHERE ConditionID IN
			(
				SELECT ConditionID FROM @ContractKeyInfo 
				WHERE  ConditionID IS NOT NULL
			)
			--百分比时间段
			DELETE FROM RatioTimeIntervalSetting WHERE TimeIntervalID IN
			(
				SELECT RatioTimeIntervalID FROM @ContractKeyInfo 
				WHERE  RatioTimeIntervalID IS NOT NULL
			)
			--百分比周期
			DELETE FROM RatioCycleSetting WHERE RuleSnapshotID IN
			(
				SELECT RatioRuleSnapshotID FROM @ContractKeyInfo 
				WHERE  RatioRuleSnapshotID IS NOT NULL
			)
			--百分比规则
			DELETE FROM RatioRuleSetting WHERE RatioID IN
			(
				SELECT RatioID FROM @ContractKeyInfo 
				WHERE  RatioID IS NOT NULL
			)
			--固定时间段
			DELETE FROM FixedTimeIntervalSetting WHERE TimeIntervalID IN
			(
				SELECT FixedTimeIntervalID FROM @ContractKeyInfo 
				WHERE  FixedTimeIntervalID IS NOT NULL
			)
			--固定规则
			DELETE FROM FixedRuleSetting WHERE RuleSnapshotID IN
			(
				SELECT FixedRuleSnapshotID FROM @ContractKeyInfo 
				WHERE  FixedRuleSnapshotID IS NOT NULL
			)
			--实体信息设置
			DELETE FROM EntityInfoSetting WHERE EntityInfoSettingID IN
			(
				SELECT EntityInfoSettingID FROM @ContractKeyInfo 
				WHERE  EntityInfoSettingID IS NOT NULL
			)
			--业主实体
			DELETE FROM VendorEntity WHERE VendorEntityID IN
			(
				SELECT VendorEntityID FROM @ContractKeyInfo 
				WHERE  VendorEntityID IS NOT NULL
			)
			--实体
			DELETE FROM [Entity] WHERE EntityID IN
			(
				SELECT EntityID FROM @ContractKeyInfo 
				WHERE  EntityID IS NOT NULL
			)
			--业主合同
			DELETE FROM VendorContract WHERE VendorContractID IN
			(
				SELECT VendorContractID FROM @ContractKeyInfo 
				WHERE  VendorContractID IS NOT NULL
			)

			SET @Xml = (SELECT ContractSnapshotID,
							   ContractID,
							   ContractNO,
							   Version,
							   ContractName,
							   CompanyName,
							   Status,
							   UpdateInfo,
							   IsLocked,
							   SnapshotCreateTime,
							   IsSave
			FROM [Contract] WHERE [ContractSnapshotID] = @ContractSnapshotID FOR XML AUTO)



			DELETE FROM [dbo].[Contract]
			WHERE
				[ContractSnapshotID] = @ContractSnapshotID
			--删除附件
			DELETE FROM dbo.SYS_Attachments
			WHERE Category='Contract' AND ObjectID=@ContractSnapshotID

			--将日志插入到日志表中
			INSERT INTO SRLS_TB_System_UserLog(ID,UserID,EnglishName,SourceID,TableName,DataInfo,UpdateTime,LogType,CompanyCode)
			VALUES(NewID(),@OperationID,@OperationEnglishName,@ContractSnapshotID,'Contract',@Xml,GETDATE(),2,@CompanyCode)--0新增，1修改，2删除
			
			--Commented by Eric--Begin
			--删除流程数据
			--DECLARE @ProcID NVARCHAR(50)
			--SELECT @ProcID=ProcID FROM dbo.WF_Proc_Inst WHERE DataLocator=@ContractSnapshotID AND EndTime IS NULL
			--IF @ProcID IS NOT NULL 
			--BEGIN
			--	EXEC dbo.usp_Workflow_DeleteInst @ProcID
			--END
			--Commented by Eric--End
			
			
			--回滚旧合同版本
			DECLARE @LastContractSnapshotID NVARCHAR(50)
			SELECT TOP 1 @LastContractSnapshotID=ContractSnapshotID 
			FROM dbo.[Contract] 
			WHERE ContractID=@ContractID
			ORDER BY LastModifyTime DESC
			EXEC usp_Contract_RollbackSnapshot @LastContractSnapshotID

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


