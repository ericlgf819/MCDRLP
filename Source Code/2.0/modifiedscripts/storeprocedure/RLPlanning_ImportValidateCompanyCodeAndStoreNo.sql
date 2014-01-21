USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_ImportValidateCompanyCodeAndStoreNo]    Script Date: 08/14/2012 16:57:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_ImportValidateCompanyCodeAndStoreNo]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--将合同编号与餐厅编号没有关联的实体，并存入临时表
		DECLARE @InvalidEntityTbl TABLE(CompanyCode nvarchar(50), StoreNo nvarchar(50))
		DECLARE @ItemCursor CURSOR, @StoreNo nvarchar(50), @CompanyCode nvarchar(50), @RealCompanyCode nvarchar(50)
		SET @ItemCursor = CURSOR SCROLL FOR SELECT Company, 餐厅编号 FROM Import_SalesValidation
		OPEN @ItemCursor
			FETCH NEXT FROM @ItemCursor INTO @CompanyCode, @StoreNo
			WHILE @@FETCH_STATUS=0
			BEGIN
				SELECT @RealCompanyCode=CompanyCode FROM SRLS_TB_Master_Store WHERE StoreNo=@StoreNo
				IF ISNULL(@RealCompanyCode,'')<>@CompanyCode
					INSERT INTO @InvalidEntityTbl SELECT @CompanyCode, @StoreNo
				FETCH NEXT FROM @ItemCursor INTO @CompanyCode, @StoreNo
			END
		CLOSE @ItemCursor
		DEALLOCATE @ItemCursor
		
		--删除无效实体
		DECLARE @NeedToDelItemCursor CURSOR
		SET @NeedToDelItemCursor = CURSOR SCROLL FOR SELECT CompanyCode, StoreNo FROM @InvalidEntityTbl
		OPEN @NeedToDelItemCursor
		FETCH NEXT FROM @NeedToDelItemCursor INTO @CompanyCode, @StoreNo
		WHILE @@FETCH_STATUS=0
		BEGIN
			DELETE FROM Import_SalesValidation WHERE Company=@CompanyCode AND 餐厅编号=@StoreNo
			FETCH NEXT FROM @NeedToDelItemCursor INTO @CompanyCode, @StoreNo
		END
		CLOSE @NeedToDelItemCursor
		DEALLOCATE @NeedToDelItemCursor
		
		--显示筛选后的有效实体
		SELECT * FROM Import_SalesValidation
		
		--显示被删除的实体
		SELECT CompanyCode, StoreNo FROM @InvalidEntityTbl
		
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


