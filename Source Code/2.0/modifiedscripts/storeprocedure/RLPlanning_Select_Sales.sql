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
	
	--记录KioskName
	DECLARE @KioskName nvarchar(500)
	IF @KioskNo IS NOT NULL
	BEGIN
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo
	END

	DECLARE @Result TABLE(Company nvarchar(50), 餐厅编号 nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
	年度 nvarchar(50), [1月] nvarchar(500), [2月] nvarchar(500), [3月] nvarchar(500), [4月] nvarchar(500),
	[5月] nvarchar(500), [6月] nvarchar(500), [7月] nvarchar(500), [8月] nvarchar(500), [9月] nvarchar(500),
	[10月] nvarchar(500), [11月] nvarchar(500), [12月] nvarchar(500))
	
	--餐厅和甜品店要分开Select
	
	--sales数据
	--餐厅--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,NULL AS Kiosk,
		FS.Year AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 1, NULL) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 2, NULL) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 3, NULL) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 4, NULL) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 5, NULL) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 6, NULL) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 7, NULL) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 8, NULL) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 9, NULL) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 10, NULL) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 11, NULL) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, FS.Year, 12, NULL) AS [12月]
		FROM (select StoreNo, KioskNo, Year(SalesDate) AS Year 
		from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) FS 
		INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='')
	END
	--餐厅--End
	
	--甜品店--Begin
	ELSE
	BEGIN
		--设置当前时间点挂靠的餐厅编号
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND GETDATE() BETWEEN StartDate AND EndDate
	
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,K.KioskName AS Kiosk,
		FS.Year AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 1, NULL) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 2, NULL) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 3, NULL) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 4, NULL) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 5, NULL) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 6, NULL) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 7, NULL) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 8, NULL) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 9, NULL) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 10, NULL) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 11, NULL) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, FS.Year, 12, NULL) AS [12月]
		FROM (select StoreNo, KioskNo, Year(SalesDate) AS Year 
		from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) FS 
		INNER JOIN SRLS_TB_Master_Store S ON S.StoreNo = @StoreNo
		INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.KioskNo = @KioskNo
	END
	--甜品店--End
	
	--记录真实sales数据最大时间，给客户端区分预测数据时使用
	DECLARE @RealSalesMaxDate DATE, @RealKioskSalesMaxDate DATE
	SELECT @RealSalesMaxDate=MAX(SalesDate) 
	FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND (KioskNo IS NULL OR KioskNo='')
	
	SELECT @RealKioskSalesMaxDate=MAX(SalesDate) 
	FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
	

	--Sales 数据
	SELECT * FROM @Result ORDER BY 年度
	
	--在KeyInBox中不必填写的数据
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
	
	--真实Sales的最大年和月的数据
	IF @KioskNo IS NULL
		SELECT YEAR(@RealSalesMaxDate) AS Year, MONTH(@RealSalesMaxDate) AS Month
	ELSE
		SELECT YEAR(@RealKioskSalesMaxDate) AS Year, MONTH(@RealKioskSalesMaxDate) AS Month
	
	--开店年和月的数据
	IF @KioskNo IS NULL
		SELECT YEAR(OpenDate) AS Year, MONTH(OpenDate) AS Month FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo
	ELSE
		SELECT YEAR(ActiveDate) AS Year, MONTH(ActiveDate) AS Month FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
END















GO


