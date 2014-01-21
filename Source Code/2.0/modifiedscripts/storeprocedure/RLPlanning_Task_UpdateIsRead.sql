USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Task_UpdateIsRead]    Script Date: 07/18/2012 17:46:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Task_UpdateIsRead]
@TaskID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		UPDATE Forecast_CheckResult SET IsRead=1 WHERE CheckID=@TaskID
		
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
		VALUES (NEWID(),'更新任务已读标记出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	
	END CATCH
END

GO


