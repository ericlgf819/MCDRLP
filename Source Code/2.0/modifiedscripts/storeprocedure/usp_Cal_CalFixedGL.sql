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
-- Description:	����AP/GL
-- =============================================
CREATE PROCEDURE [dbo].[usp_Cal_CalFixedGL]
	-- Add the parameters for the stored procedure here
	@GLRecordID NVARCHAR(50)
AS
BEGIN
	--����ʼ
    BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
			--Commented by Eric
			--���û�б�Ҫ�ˣ�ֱ�����óɹ���־λ
			--�����Ƿ����ɹ�
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
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'����̶�GL����',@GLRecordID,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
	--�������
	

END




















GO


