USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Select_SalesTemplate]    Script Date: 08/25/2012 11:55:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Select_SalesTemplate] 
@UserID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ResultTbl TABLE(Company nvarchar(50), 餐厅编号 nvarchar(50), Store nvarchar(50), Kiosk nvarchar(50),
			年度 nvarchar(50), [1月] nvarchar(500), [2月] nvarchar(500), [3月] nvarchar(500), [4月] nvarchar(500), 
			[5月] nvarchar(500), [6月] nvarchar(500), [7月] nvarchar(500), [8月] nvarchar(500), [9月] nvarchar(500), 
			[10月] nvarchar(500), [11月] nvarchar(500), [12月] nvarchar(500))

	--获取当前用户有权限的所有CompanyCode
	DECLARE @CompanyCodeTbl TABLE(CompanyCode nvarchar(50))
	INSERT INTO @CompanyCodeTbl
	SELECT DISTINCT CompanyCode FROM RLP_UserCompany WHERE UserId = @UserID
	
	--根据每一个CompanyCode，获取相关的餐厅和甜品店
	DECLARE @CompanyCodeCursor CURSOR, @CompanyCode nvarchar(50)
	SET @CompanyCodeCursor = CURSOR SCROLL FOR SELECT CompanyCode FROM @CompanyCodeTbl
	OPEN @CompanyCodeCursor
	FETCH NEXT FROM @CompanyCodeCursor INTO @CompanyCode
	WHILE @@FETCH_STATUS=0
	BEGIN
		--餐厅
		INSERT INTO @ResultTbl (Company, 餐厅编号, Store, Kiosk)
		SELECT CompanyCode, StoreNo, StoreName, NULL
		FROM SRLS_TB_Master_Store
		WHERE CompanyCode = @CompanyCode AND Status='A'
		
		--甜品店
		INSERT INTO @ResultTbl (Company, 餐厅编号, Store, Kiosk)
		SELECT S.CompanyCode, S.StoreNo, S.StoreName, K.KioskName
		FROM SRLS_TB_Master_Store S
		INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
		WHERE S.CompanyCode = @CompanyCode AND K.Status='A'
		
		FETCH NEXT FROM @CompanyCodeCursor INTO @CompanyCode
	END
	CLOSE @CompanyCodeCursor
	DEALLOCATE @CompanyCodeCursor
	
	--数据显示
	SELECT * FROM @ResultTbl ORDER BY 餐厅编号 DESC, Company ASC
END


GO


