USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Import_Contract]    Script Date: 08/03/2012 12:56:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






















-- =============================================
-- Author:		zhangbsh
-- Create date:20110406
-- Description:	��ͬ����
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract]
	-- Add the parameters for the stored procedure here
	@ImportType NVARCHAR(50), --����/Ԥ��
	@CurrentUserID NVARCHAR(50),
	@TotalCount INT OUTPUT,
	@SuccessCount INT OUTPUT,
	@FailCount INT OUTPUT 
AS
BEGIN

	
	--����ʼ
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
				EXEC dbo.usp_Helper_SetEmptyStringNull 'Import_Contract'
				DELETE FROM  Import_ContractMessage
				
				EXEC dbo.usp_Import_Contract1CheckEveryColmn
			    
				DECLARE @ContractIndex NVARCHAR(50),@ContractSnapshotID NVARCHAR(50),@ProcID NVARCHAR(50)
				DECLARE @MyCursor CURSOR
				SET  @MyCursor = CURSOR  SCROLL FOR
				SELECT DISTINCT ��ͬ��� FROM dbo.Import_Contract
				
				DECLARE @UserName NVARCHAR(50)
				SELECT @UserName = UserName FROM dbo.SRLS_TB_Master_User
				WHERE ID = @CurrentUserID 

				OPEN @MyCursor;
				FETCH NEXT FROM @MyCursor INTO @ContractIndex
					WHILE @@FETCH_STATUS = 0
					   BEGIN
							--���������ͬû�д�����Ϣ
							IF NOT EXISTS(SELECT * FROM dbo.Import_ContractMessage WHERE ContractIndex=@ContractIndex)
							BEGIN
--								PRINT @ContractIndex
								--�嵽��ʵ��ͬ��
								EXEC dbo.usp_Import_Contract2SplitToTable @ContractIndex,@UserName,@ContractSnapshotID OUTPUT 
								--����µĺ�ͬ
								EXEC dbo.usp_Import_Contract3CheckEveryContract @ContractIndex,@ContractSnapshotID, @CurrentUserID
							END
							
							--Commented by Eric
--							--���������ͬû�д�����Ϣ, ��ô�����½���ͬ��������
--							IF NOT EXISTS(SELECT * FROM dbo.Import_ContractMessage WHERE ContractIndex=@ContractIndex)
--							BEGIN
----								PRINT 'CREATE workflow'
--								EXEC dbo.usp_Workflow_CreateAndRun 9,'��ͬ�����½�����',@ContractSnapshotID,@CurrentUserID,@ProcID OUTPUT ,NULL
--							END
							--ѭ����һ��
							FETCH NEXT FROM @MyCursor INTO @ContractIndex
					   END;
				CLOSE @MyCursor;
				DEALLOCATE @MyCursor;	
				
				SELECT ExcelIndex,
					   ContractIndex,
					   Enflag,
					   RelationData,
					   CheckMessage,
					   ISNULL(RelationData,'') + ' : '+ ISNULL(CheckMessage,'') AS MessageInfo 
				FROM dbo.Import_ContractMessage ORDER BY ContractIndex ASC
				
				--����
				SELECT @TotalCount=COUNT(*) FROM
				(SELECT DISTINCT ��ͬ��� FROM dbo.Import_Contract ) x
				--ʧ����
				SELECT @FailCount=COUNT(*) FROM
				(SELECT DISTINCT ContractIndex FROM dbo.Import_ContractMessage
				WHERE ContractIndex IS NOT NULL ) x
				--�ɹ���
				SELECT @SuccessCount=@TotalCount-@FailCount
				
				IF @ImportType='Ԥ��'
				BEGIN
					RAISERROR('Ԥ��ʱ�ع�����',16,1)
				END
				
				IF @ImportType='����' AND EXISTS(SELECT * FROM dbo.Import_ContractMessage)
				BEGIN
					SELECT @SuccessCount=0
					SELECT @FailCount=@TotalCount
					RAISERROR('ֻ��һ����ͬ��ͨ�������ܵ���,֮ǰ����ȫ������',16,1)
				END
		IF @@TRANCOUNT <> 0
	    COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
	    IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION;
				
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'�����ͬ����',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
		DELETE FROM  SRLS_TB_System_Log WHERE  LogMessage = 'Ԥ��ʱ�ع�����'
		DELETE FROM  SRLS_TB_System_Log WHERE  LogMessage = 'ֻ��һ����ͬ��ͨ�������ܵ���,֮ǰ����ȫ������'
	END CATCH 
	--�������
	
	

   

    
    
END






















GO


