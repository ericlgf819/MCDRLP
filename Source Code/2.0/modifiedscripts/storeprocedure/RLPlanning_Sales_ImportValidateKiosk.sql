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
		--遍历Import_SalesValidation，验证Kiosk是否是独立结算租金
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
			
			--非独立结算sales的kiosk需要剔除，并且提示用户
			IF @IsNeedSubstract=0
			BEGIN
				INSERT INTO @KioskNameTbl SELECT @KioskName
			END
			FETCH NEXT FROM @ValidateItemCursor INTO @KioskName
		END
		CLOSE @ValidateItemCursor
		DEALLOCATE @ValidateItemCursor
		
		--将非独立结算sales的kiosk从Import_SalesValidation中删除
		DELETE FROM Import_SalesValidation WHERE Kiosk IN
		(SELECT KioskName FROM @KioskNameTbl)
		
		--将正确的结果返回
		SELECT * FROM Import_SalesValidation
		
		--将剔除的Kiosk结果返回
		SELECT KioskName FROM @KioskNameTbl
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION
			
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'验证导入Sales甜品店是否独立结算Sales错误',NULL,NULL,@MSG,GETDATE(),NULL,NULL)			
	END CATCH
END

GO


