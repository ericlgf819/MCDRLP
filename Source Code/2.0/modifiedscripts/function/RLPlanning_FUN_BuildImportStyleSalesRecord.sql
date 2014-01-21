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
-- Description:	������ϳɵ���sales���������Ŀ
-- =============================================
CREATE FUNCTION RLPlanning_FUN_BuildImportStyleSalesRecord
(
	@MaxRealSalesDate date,
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
--����˼·����ʵ���ݺ�Ԥ�������ڸ��궼����12���£��ֱ����2����¼��Ȼ����update��������sales��column����������һ����¼����
RETURNS @Result TABLE(Company nvarchar(50), ������� nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
��� nvarchar(50), [1��] nvarchar(500), [2��] nvarchar(500), [3��] nvarchar(500), [4��] nvarchar(500),
[5��] nvarchar(500), [6��] nvarchar(500), [7��] nvarchar(500), [8��] nvarchar(500), [9��] nvarchar(500),
[10��] nvarchar(500), [11��] nvarchar(500), [12��] nvarchar(500))
AS
BEGIN
	--������ʱ������12�·ݵĻ����Ͳ���Ϊ��merge������������
	IF YEAR(@MaxRealSalesDate) = 12
		RETURN
		
	--��ʵsales����
	--����--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 0) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 0) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 0) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 0) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 0) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 0) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 0) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 0) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 0) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 0) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 0) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 0) AS [12��]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='')
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--����--End
	
	--��Ʒ��--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 0) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 0) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 0) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 0) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 0) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 0) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 0) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 0) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 0) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 0) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 0) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 0) AS [12��]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--��Ʒ��--End
	
	--Ԥ������
	--����--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 1) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 1) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 1) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 1) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 1) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 1) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 1) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 1) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 1) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 1) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 1) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 1) AS [12��]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='') 
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--����--End
	
	--��Ʒ��--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS ���, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 1) AS [1��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 1) AS [2��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 1) AS [3��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 1) AS [4��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 1) AS [5��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 1) AS [6��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 1) AS [7��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 1) AS [8��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 1) AS [9��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 1) AS [10��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 1) AS [11��],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 1) AS [12��]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--��Ʒ��--End	
	
	--���л���update
	DECLARE @month1 nvarchar(500),  @month2 nvarchar(500), @month3 nvarchar(500), @month4 nvarchar(500),
	@month5 nvarchar(500), @month6 nvarchar(500), @month7 nvarchar(500), @month8 nvarchar(500),
	@month9 nvarchar(500), @month10 nvarchar(500), @month11 nvarchar(500), @month12 nvarchar(500)
	
	SELECT @month1=[1��] FROM @Result WHERE [1��] IS NOT NULL
	SELECT @month2=[2��] FROM @Result WHERE [2��] IS NOT NULL
	SELECT @month3=[3��] FROM @Result WHERE [3��] IS NOT NULL
	SELECT @month4=[4��] FROM @Result WHERE [4��] IS NOT NULL
	SELECT @month5=[5��] FROM @Result WHERE [5��] IS NOT NULL
	SELECT @month6=[6��] FROM @Result WHERE [6��] IS NOT NULL
	SELECT @month7=[7��] FROM @Result WHERE [7��] IS NOT NULL
	SELECT @month8=[8��] FROM @Result WHERE [8��] IS NOT NULL
	SELECT @month9=[9��] FROM @Result WHERE [9��] IS NOT NULL
	SELECT @month10=[10��] FROM @Result WHERE [10��] IS NOT NULL
	SELECT @month11=[11��] FROM @Result WHERE [11��] IS NOT NULL
	SELECT @month12=[12��] FROM @Result WHERE [12��] IS NOT NULL
	
	UPDATE @Result SET [1��]=@month1
	UPDATE @Result SET [2��]=@month2
	UPDATE @Result SET [3��]=@month3
	UPDATE @Result SET [4��]=@month4
	UPDATE @Result SET [5��]=@month5
	UPDATE @Result SET [6��]=@month6
	UPDATE @Result SET [7��]=@month7
	UPDATE @Result SET [8��]=@month8
	UPDATE @Result SET [9��]=@month9
	UPDATE @Result SET [10��]=@month10
	UPDATE @Result SET [11��]=@month11
	UPDATE @Result SET [12��]=@month12
	
	RETURN
END
GO