USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Cal_CalRatioGL]    Script Date: 07/12/2012 11:20:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










-- =============================================
-- Author:		zhangbsh
-- Create date: 20110223
-- Description:	����AP/GL
-- =============================================
CREATE PROCEDURE [dbo].[usp_Cal_CalRatioGL]
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
			--EXEC  dbo.usp_Cal_CalRatioGL1CheckResult  @GLRecordID
			EXEC  dbo.usp_Cal_CalRatioGL2ProcessParameterValue  @GLRecordID
			EXEC  dbo.usp_Cal_CalRatioGL3MessageResult  @GLRecordID
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
		VALUES (NEWID(),'����ٷֱ�GL����',@GLRecordID,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
	--�������
END





















GO


