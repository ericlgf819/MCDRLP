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
-- Description:	δ�������ʱ���ز���
-- =============================================
CREATE PROCEDURE [dbo].[usp_Contract_UndoContract]
	-- Add the parameters for the stored procedure here
	@ContractSnapshotID nvarchar(50),--��ͬ����ID
	@CopyType NVARCHAR(50) --���/����/�½�                         
AS
BEGIN
	--����ʼ
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--һ���������
			DECLARE @IsSave BIT, @Status nvarchar(20)
		
			SELECT @IsSave=IsSave, @Status=Status
			FROM [Contract] 
			WHERE ContractSnapshotID=@ContractSnapshotID
			
			--@Status Added by Eric
			IF @IsSave=0 OR @Status IS NULL OR @Status = '�ݸ�'
			BEGIN
				DECLARE @AdministratorID NVARCHAR(50) 
				SELECT @AdministratorID  = dbo.fn_GetSysParamValueByCode('AdministratorID')
				EXEC usp_Contract_DeleteSingleContract @ContractSnapshotID,@AdministratorID
			END
			
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


