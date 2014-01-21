-- ================================================
-- Template generated from Template Explorer using:
-- Create Multi-Statement Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	用来组合成导入sales表的数据条目
-- =============================================
CREATE FUNCTION RLPlanning_FUN_BuildImportStyleSalesRecord
(
	@MaxRealSalesDate date,
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
--基本思路，真实数据和预测数据在该年都不满12个月，分别插入2条记录，然后互相update，将所有sales的column填满，返回一条记录即可
RETURNS @Result TABLE(Company nvarchar(50), 餐厅编号 nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
年度 nvarchar(50), [1月] nvarchar(500), [2月] nvarchar(500), [3月] nvarchar(500), [4月] nvarchar(500),
[5月] nvarchar(500), [6月] nvarchar(500), [7月] nvarchar(500), [8月] nvarchar(500), [9月] nvarchar(500),
[10月] nvarchar(500), [11月] nvarchar(500), [12月] nvarchar(500))
AS
BEGIN
	--如果最大时间月是12月份的话，就不用为了merge来处理数据了
	IF YEAR(@MaxRealSalesDate) = 12
		RETURN
		
	--真实sales数据
	--餐厅--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 0) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 0) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 0) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 0) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 0) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 0) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 0) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 0) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 0) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 0) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 0) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 0) AS [12月]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='')
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--餐厅--End
	
	--甜品店--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 0) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 0) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 0) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 0) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 0) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 0) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 0) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 0) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 0) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 0) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 0) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 0) AS [12月]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--甜品店--End
	
	--预测数据
	--餐厅--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 1) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 1) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 1) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 1) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 1) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 1) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 1) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 1) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 1) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 1) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 1) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 1) AS [12月]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='') 
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--餐厅--End
	
	--甜品店--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 1) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 1) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 1) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 1) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 1) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 1) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 1) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 1) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 1) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 1) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 1) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 1) AS [12月]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--甜品店--End	
	
	--两行互相update
	DECLARE @month1 nvarchar(500),  @month2 nvarchar(500), @month3 nvarchar(500), @month4 nvarchar(500),
	@month5 nvarchar(500), @month6 nvarchar(500), @month7 nvarchar(500), @month8 nvarchar(500),
	@month9 nvarchar(500), @month10 nvarchar(500), @month11 nvarchar(500), @month12 nvarchar(500)
	
	SELECT @month1=[1月] FROM @Result WHERE [1月] IS NOT NULL
	SELECT @month2=[2月] FROM @Result WHERE [2月] IS NOT NULL
	SELECT @month3=[3月] FROM @Result WHERE [3月] IS NOT NULL
	SELECT @month4=[4月] FROM @Result WHERE [4月] IS NOT NULL
	SELECT @month5=[5月] FROM @Result WHERE [5月] IS NOT NULL
	SELECT @month6=[6月] FROM @Result WHERE [6月] IS NOT NULL
	SELECT @month7=[7月] FROM @Result WHERE [7月] IS NOT NULL
	SELECT @month8=[8月] FROM @Result WHERE [8月] IS NOT NULL
	SELECT @month9=[9月] FROM @Result WHERE [9月] IS NOT NULL
	SELECT @month10=[10月] FROM @Result WHERE [10月] IS NOT NULL
	SELECT @month11=[11月] FROM @Result WHERE [11月] IS NOT NULL
	SELECT @month12=[12月] FROM @Result WHERE [12月] IS NOT NULL
	
	UPDATE @Result SET [1月]=@month1
	UPDATE @Result SET [2月]=@month2
	UPDATE @Result SET [3月]=@month3
	UPDATE @Result SET [4月]=@month4
	UPDATE @Result SET [5月]=@month5
	UPDATE @Result SET [6月]=@month6
	UPDATE @Result SET [7月]=@month7
	UPDATE @Result SET [8月]=@month8
	UPDATE @Result SET [9月]=@month9
	UPDATE @Result SET [10月]=@month10
	UPDATE @Result SET [11月]=@month11
	UPDATE @Result SET [12月]=@month12
	
	RETURN
END
GO