USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Cal_CalFixedGL]    Script Date: 07/12/2012 11:18:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









-- =============================================
-- Author:		zhangbsh
-- Create date: 20110223
-- Description:	计算AP/GL
-- =============================================
CREATE PROCEDURE [dbo].[usp_Cal_CalFixedGL]
	-- Add the parameters for the stored procedure here
	@GLRecordID NVARCHAR(50)
AS
BEGIN
	--事务开始
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
			--Commented by Eric
			--检查没有必要了，直接设置成功标志位
			--更新是否计算成功
			EXEC dbo.usp_Cal_SetIsCalculateSuccess @GLRecordID
			--EXEC  dbo.usp_Cal_CalFixedGL1CheckResult   @GLRecordID
			EXEC  dbo.usp_Cal_CalFixedGL2ProcessParameterValue   @GLRecordID
			EXEC  dbo.usp_Cal_CalFixedGL3MessageResult   @GLRecordID
			EXEC dbo.usp_Cal_CalGLException @GLRecordID
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
		VALUES (NEWID(),'计算固定GL出错',@GLRecordID,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
	--事务结束
	

END




















GO


