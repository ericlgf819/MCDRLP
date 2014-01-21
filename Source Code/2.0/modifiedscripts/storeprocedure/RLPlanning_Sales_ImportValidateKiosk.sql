USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_ImportValidateKiosk]    Script Date: 08/10/2012 11:00:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_ImportValidateKiosk] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--����Import_SalesValidation����֤Kiosk�Ƿ��Ƕ����������
		DECLARE @KioskNameTbl TABLE(KioskName nvarchar(200))
		DECLARE @KioskName nvarchar(200)
		DECLARE @ValidateItemCursor CURSOR, @IsNeedSubstract BIT
		SET @ValidateItemCursor = CURSOR SCROLL FOR
		SELECT Kiosk FROM Import_SalesValidation
		OPEN @ValidateItemCursor
		FETCH NEXT FROM @ValidateItemCursor INTO @KioskName
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @IsNeedSubstract=NULL	
			SELECT @IsNeedSubstract=IsNeedSubtractSalse 
			FROM SRLS_TB_Master_Kiosk WHERE KioskName=@KioskName
			
			--�Ƕ�������sales��kiosk��Ҫ�޳���������ʾ�û�
			IF @IsNeedSubstract=0
			BEGIN
				INSERT INTO @KioskNameTbl SELECT @KioskName
			END
			FETCH NEXT FROM @ValidateItemCursor INTO @KioskName
		END
		CLOSE @ValidateItemCursor
		DEALLOCATE @ValidateItemCursor
		
		--���Ƕ�������sales��kiosk��Import_SalesValidation��ɾ��
		DELETE FROM Import_SalesValidation WHERE Kiosk IN
		(SELECT KioskName FROM @KioskNameTbl)
		
		--����ȷ�Ľ������
		SELECT * FROM Import_SalesValidation
		
		--���޳���Kiosk�������
		SELECT KioskName FROM @KioskNameTbl
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION
			
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'��֤����Sales��Ʒ���Ƿ��������Sales����',NULL,NULL,@MSG,GETDATE(),NULL,NULL)			
	END CATCH
END

GO


