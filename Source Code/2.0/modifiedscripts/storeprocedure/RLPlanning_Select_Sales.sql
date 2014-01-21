USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Select_Sales]    Script Date: 08/17/2012 15:12:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO















-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Select_Sales]
@StoreNo nvarchar(50),
@KioskNo nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--��¼KioskName
	DECLARE @KioskName nvarchar(500)
	IF @KioskNo IS NOT NULL
	BEGIN
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo
	END

	DECLARE @Result TABLE(Company nvarchar(50), ������� nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
	��� nvarchar(50), [1��] nvarchar(500), [2��] nvarchar(500), [3��] nvarchar(500), [4��] nvarchar(500),
	[5��] nvarchar(500), [6��] nvarchar(500), [7��] nvarchar(500), [8��] nvarchar(500), [9��] nvarchar(500),
	[10��] nvarchar(500), [11��] nvarchar(500), [12��] nvarchar(500))
	
	--��������Ʒ��Ҫ�ֿ�Select
	
	--sales����
	--����--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,NULL AS Kiosk,
		FS.Year AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 1, NULL) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 2, NULL) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 3, NULL) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 4, NULL) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 5, NULL) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 6, NULL) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 7, NULL) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 8, NULL) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 9, NULL) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 10, NULL) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 11, NULL) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 12, NULL) AS [12��]
		FROM (select StoreNo, KioskNo, Year(SalesDate) AS Year 
		from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) FS 
		INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='')
	END
	--����--End
	
	--��Ʒ��--Begin
	ELSE
	BEGIN
		--���õ�ǰʱ���ҿ��Ĳ������
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND GETDATE() BETWEEN StartDate AND EndDate
	
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,K.KioskName AS Kiosk,
		FS.Year AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 1, NULL) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 2, NULL) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 3, NULL) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 4, NULL) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 5, NULL) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 6, NULL) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 7, NULL) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 8, NULL) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 9, NULL) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 10, NULL) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 11, NULL) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 12, NULL) AS [12��]
		FROM (select StoreNo, KioskNo, Year(SalesDate) AS Year 
		from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) FS 
		INNER JOIN SRLS_TB_Master_Store S ON S.StoreNo = @StoreNo
		INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.KioskNo = @KioskNo
	END
	--��Ʒ��--End
	
	--��¼��ʵsales�������ʱ�䣬���ͻ�������Ԥ������ʱʹ��
	DECLARE @RealSalesMaxDate DATE, @RealKioskSalesMaxDate DATE
	SELECT @RealSalesMaxDate=MAX(SalesDate) 
	FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND (KioskNo IS NULL OR KioskNo='')
	
	SELECT @RealKioskSalesMaxDate=MAX(SalesDate) 
	FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
	

	--Sales ����
	SELECT * FROM @Result ORDER BY ���
	
	--��KeyInBox�в�����д������
	IF @KioskNo IS NULL
	BEGIN
		SELECT CompanyCode AS CompanyCode, StoreName AS StoreName, NULL AS KioskName
		FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo
	END
	ELSE
	BEGIN
		SELECT S.CompanyCode AS CompanyCode, S.StoreName AS StoreName, K.KioskName AS KioskName
		FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Kiosk K ON S.StoreNo = @StoreNo
		WHERE K.KioskNo = @KioskNo
	END
	
	--��ʵSales���������µ�����
	IF @KioskNo IS NULL
		SELECT YEAR(@RealSalesMaxDate) AS Year, MONTH(@RealSalesMaxDate) AS Month
	ELSE
		SELECT YEAR(@RealKioskSalesMaxDate) AS Year, MONTH(@RealKioskSalesMaxDate) AS Month
	
	--��������µ�����
	IF @KioskNo IS NULL
		SELECT YEAR(OpenDate) AS Year, MONTH(OpenDate) AS Month FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo
	ELSE
		SELECT YEAR(ActiveDate) AS Year, MONTH(ActiveDate) AS Month FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
END















GO


