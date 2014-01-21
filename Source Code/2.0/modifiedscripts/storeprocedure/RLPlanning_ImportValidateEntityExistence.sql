USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_ImportValidateEntityExistence]    Script Date: 08/13/2012 17:07:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_ImportValidateEntityExistence]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--将无效的实体找出，并存入临时表
		DECLARE @InvalidEntityTbl TABLE(StoreNo nvarchar(50), StoreName nvarchar(200), KioskName nvarchar(200))
		DECLARE @ItemCursor CURSOR, @StoreNo nvarchar(50), @StoreName nvarchar(200), @KioskName nvarchar(200)
		SET @ItemCursor = CURSOR SCROLL FOR SELECT 餐厅编号, Store, Kiosk FROM Import_SalesValidation
		OPEN @ItemCursor
			FETCH NEXT FROM @ItemCursor INTO @StoreNo, @StoreName, @KioskName
			WHILE @@FETCH_STATUS=0
			BEGIN
				--餐厅
				IF ISNULL(@KioskName,'')=''
				BEGIN
					IF NOT EXISTS(SELECT * FROM SRLS_TB_Master_Store WHERE StoreNo=@StoreNo AND Status='A')
					BEGIN
						INSERT INTO @InvalidEntityTbl SELECT @StoreNo, @StoreName, @KioskName
					END
				END
				--甜品店
				ELSE
				BEGIN
					IF NOT EXISTS(SELECT * FROM SRLS_TB_Master_Kiosk WHERE KioskName=@KioskName AND Status='A')
					BEGIN
						INSERT INTO @InvalidEntityTbl SELECT @StoreNo, @StoreName, @KioskName
					END
				END
				FETCH NEXT FROM @ItemCursor INTO @StoreNo, @StoreName, @KioskName
			END
		CLOSE @ItemCursor
		DEALLOCATE @ItemCursor
		
		--删除无效实体
		DELETE FROM Import_SalesValidation WHERE 餐厅编号 IN 
			(SELECT StoreNo FROM @InvalidEntityTbl WHERE ISNULL(KioskName,'')='')
			AND ISNULL(Kiosk,'')=''
		DELETE FROM Import_SalesValidation WHERE Kiosk IN
			(SELECT KioskName FROM @InvalidEntityTbl WHERE KioskName=@KioskName)
		
		--显示筛选后的有效实体
		SELECT * FROM Import_SalesValidation
		
		--显示被删除的实体
		SELECT StoreNo, StoreName, KioskName FROM @InvalidEntityTbl
		
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
		VALUES (NEWID(),'验证导入Sales实体是否存在',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END

GO


