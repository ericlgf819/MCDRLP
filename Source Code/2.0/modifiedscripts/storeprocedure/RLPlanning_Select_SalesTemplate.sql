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
	
	DECLARE @ResultTbl TABLE(Company nvarchar(50), ������� nvarchar(50), Store nvarchar(50), Kiosk nvarchar(50),
			��� nvarchar(50), [1��] nvarchar(500), [2��] nvarchar(500), [3��] nvarchar(500), [4��] nvarchar(500), 
			[5��] nvarchar(500), [6��] nvarchar(500), [7��] nvarchar(500), [8��] nvarchar(500), [9��] nvarchar(500), 
			[10��] nvarchar(500), [11��] nvarchar(500), [12��] nvarchar(500))

	--��ȡ��ǰ�û���Ȩ�޵�����CompanyCode
	DECLARE @CompanyCodeTbl TABLE(CompanyCode nvarchar(50))
	INSERT INTO @CompanyCodeTbl
	SELECT DISTINCT CompanyCode FROM RLP_UserCompany WHERE UserId = @UserID
	
	--����ÿһ��CompanyCode����ȡ��صĲ�������Ʒ��
	DECLARE @CompanyCodeCursor CURSOR, @CompanyCode nvarchar(50)
	SET @CompanyCodeCursor = CURSOR SCROLL FOR SELECT CompanyCode FROM @CompanyCodeTbl
	OPEN @CompanyCodeCursor
	FETCH NEXT FROM @CompanyCodeCursor INTO @CompanyCode
	WHILE @@FETCH_STATUS=0
	BEGIN
		--����
		INSERT INTO @ResultTbl (Company, �������, Store, Kiosk)
		SELECT CompanyCode, StoreNo, StoreName, NULL
		FROM SRLS_TB_Master_Store
		WHERE CompanyCode = @CompanyCode AND Status='A'
		
		--��Ʒ��
		INSERT INTO @ResultTbl (Company, �������, Store, Kiosk)
		SELECT S.CompanyCode, S.StoreNo, S.StoreName, K.KioskName
		FROM SRLS_TB_Master_Store S
		INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
		WHERE S.CompanyCode = @CompanyCode AND K.Status='A'
		
		FETCH NEXT FROM @CompanyCodeCursor INTO @CompanyCode
	END
	CLOSE @CompanyCodeCursor
	DEALLOCATE @CompanyCodeCursor
	
	--������ʾ
	SELECT * FROM @ResultTbl ORDER BY ������� DESC, Company ASC
END


GO


