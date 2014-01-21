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
-- Description:	合同导入
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract]
	-- Add the parameters for the stored procedure here
	@ImportType NVARCHAR(50), --导入/预览
	@CurrentUserID NVARCHAR(50),
	@TotalCount INT OUTPUT,
	@SuccessCount INT OUTPUT,
	@FailCount INT OUTPUT 
AS
BEGIN

	
	--事务开始
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
				EXEC dbo.usp_Helper_SetEmptyStringNull 'Import_Contract'
				DELETE FROM  Import_ContractMessage
				
				EXEC dbo.usp_Import_Contract1CheckEveryColmn
			    
				DECLARE @ContractIndex NVARCHAR(50),@ContractSnapshotID NVARCHAR(50),@ProcID NVARCHAR(50)
				DECLARE @MyCursor CURSOR
				SET  @MyCursor = CURSOR  SCROLL FOR
				SELECT DISTINCT 合同序号 FROM dbo.Import_Contract
				
				DECLARE @UserName NVARCHAR(50)
				SELECT @UserName = UserName FROM dbo.SRLS_TB_Master_User
				WHERE ID = @CurrentUserID 

				OPEN @MyCursor;
				FETCH NEXT FROM @MyCursor INTO @ContractIndex
					WHILE @@FETCH_STATUS = 0
					   BEGIN
							--如果单个合同没有错误信息
							IF NOT EXISTS(SELECT * FROM dbo.Import_ContractMessage WHERE ContractIndex=@ContractIndex)
							BEGIN
--								PRINT @ContractIndex
								--插到真实合同表
								EXEC dbo.usp_Import_Contract2SplitToTable @ContractIndex,@UserName,@ContractSnapshotID OUTPUT 
								--检查新的合同
								EXEC dbo.usp_Import_Contract3CheckEveryContract @ContractIndex,@ContractSnapshotID, @CurrentUserID
							END
							
							--Commented by Eric
--							--如果单个合同没有错误信息, 那么可以新建合同审批流程
--							IF NOT EXISTS(SELECT * FROM dbo.Import_ContractMessage WHERE ContractIndex=@ContractIndex)
--							BEGIN
----								PRINT 'CREATE workflow'
--								EXEC dbo.usp_Workflow_CreateAndRun 9,'合同导入新建流程',@ContractSnapshotID,@CurrentUserID,@ProcID OUTPUT ,NULL
--							END
							--循环下一个
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
				
				--总数
				SELECT @TotalCount=COUNT(*) FROM
				(SELECT DISTINCT 合同序号 FROM dbo.Import_Contract ) x
				--失败数
				SELECT @FailCount=COUNT(*) FROM
				(SELECT DISTINCT ContractIndex FROM dbo.Import_ContractMessage
				WHERE ContractIndex IS NOT NULL ) x
				--成功数
				SELECT @SuccessCount=@TotalCount-@FailCount
				
				IF @ImportType='预览'
				BEGIN
					RAISERROR('预览时回滚数据',16,1)
				END
				
				IF @ImportType='导入' AND EXISTS(SELECT * FROM dbo.Import_ContractMessage)
				BEGIN
					SELECT @SuccessCount=0
					SELECT @FailCount=@TotalCount
					RAISERROR('只有一条合同不通过都不能导入,之前导入全部回流',16,1)
				END
		IF @@TRANCOUNT <> 0
	    COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
	    IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION;
				
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'导入合同出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
		DELETE FROM  SRLS_TB_System_Log WHERE  LogMessage = '预览时回滚数据'
		DELETE FROM  SRLS_TB_System_Log WHERE  LogMessage = '只有一条合同不通过都不能导入,之前导入全部回流'
	END CATCH 
	--事务结束
	
	

   

    
    
END






















GO


