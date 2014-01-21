USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_UndoContract]    Script Date: 08/07/2012 11:38:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO














-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	未点击保存时撤回操作
-- =============================================
CREATE PROCEDURE [dbo].[usp_Contract_UndoContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50),--合同快照ID
	@CopyType NVARCHAR(50) --变更/继租/新建                         
AS
BEGIN
	--事务开始
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--一堆事务操作
			DECLARE @IsSave BIT, @Status nvarchar(20)
		
			SELECT @IsSave=IsSave, @Status=Status
			FROM [Contract] 
			WHERE ContractSnapshotID=@ContractSnapshotID
			
			--@Status Added by Eric
			IF @IsSave=0 OR @Status IS NULL OR @Status = '草稿'
			BEGIN
				DECLARE @AdministratorID NVARCHAR(50) 
				SELECT @AdministratorID  = dbo.fn_GetSysParamValueByCode('AdministratorID')
				EXEC usp_Contract_DeleteSingleContract @ContractSnapshotID,@AdministratorID
			END
			
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


