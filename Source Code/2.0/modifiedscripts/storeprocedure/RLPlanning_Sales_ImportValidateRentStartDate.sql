USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_ImportValidateRentStartDate]    Script Date: 08/17/2012 14:20:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_ImportValidateRentStartDate]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--����Import_SalesValidation, 
		--��ȡSales����ڿ���ǰ����Ŀ
		DECLARE @NeedToDeletedTbl 
		TABLE(StoreNo nvarchar(50), StoreName nvarchar(500), KioskName nvarchar(500), Year nvarchar(50))
		
		DECLARE @ValidateItemCursor CURSOR, @StoreNo nvarchar(50), 
		@StoreName nvarchar(500), @KioskName nvarchar(500)
		DECLARE @Year int
		SET @ValidateItemCursor = CURSOR SCROLL FOR
		SELECT �������, Store, Kiosk, ��� FROM Import_SalesValidation
		
		DECLARE @OpenYear int
		
		OPEN @ValidateItemCursor
		FETCH NEXT FROM @ValidateItemCursor INTO @StoreNo, @StoreName, @KioskName, @Year
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @OpenYear=NULL
			--����
			IF ISNULL(@KioskName,'')=''
			BEGIN
				SELECT @OpenYear=YEAR(OpenDate) FROM SRLS_TB_Master_Store WHERE StoreNo=@StoreNo
			END
			--��Ʒ��
			ELSE
			BEGIN
				SELECT @OpenYear=YEAR(ActiveDate) FROM SRLS_TB_Master_Kiosk 
				WHERE KioskName=@KioskName
			END
			
			IF @OpenYear > @Year
				INSERT INTO @NeedToDeletedTbl SELECT @StoreNo, @StoreName, @KioskName, @Year
				
			FETCH NEXT FROM @ValidateItemCursor INTO @StoreNo, @StoreName, @KioskName, @Year
		END
		CLOSE @ValidateItemCursor
		DEALLOCATE @ValidateItemCursor
		
		--��С�ڿ������ǰ������ɾ��
		DECLARE @NeedDeleteItemCursor CURSOR
		SET @NeedDeleteItemCursor=CURSOR SCROLL FOR
		SELECT StoreNo, StoreName, KioskName, Year FROM @NeedToDeletedTbl
		
		OPEN @NeedDeleteItemCursor
		FETCH NEXT FROM @NeedDeleteItemCursor INTO @StoreNo, @StoreName, @KioskName, @Year
		WHILE @@FETCH_STATUS=0
		BEGIN
			DELETE FROM Import_SalesValidation
			WHERE �������=@StoreNo AND Store=@StoreName AND 
			ISNULL(Kiosk,'')=ISNULL(@KioskName,'') AND ���=@Year
			FETCH NEXT FROM @NeedDeleteItemCursor INTO @StoreNo, @StoreName, @KioskName, @Year
		END
		CLOSE @NeedDeleteItemCursor
		DEALLOCATE @NeedDeleteItemCursor
		
		--��ʾ�޳�����ȷ���ݺ����ȷ����
		SELECT * FROM Import_SalesValidation
		
		--��ʾ����ȷ����
		SELECT * FROM @NeedToDeletedTbl
		
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
		VALUES (NEWID(),'��֤����Sales����Ƿ��ǿ�����ǰ����',NULL,NULL,@MSG,GETDATE(),NULL,NULL)			
	END CATCH
END







GO


