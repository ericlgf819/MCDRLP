USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_ReportGetCashCloseSales]    Script Date: 08/17/2012 16:05:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_ReportGetCashCloseSales]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@CompanyCode nvarchar(50),
	@Year nvarchar(50)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	-- Fill the table variable with the rows for your result set
	
	--构建Sql
	DECLARE @SqlSelect nvarchar(max), @SqlFrom nvarchar(max), @SqlWhere nvarchar(max)
	
	--有Year条件的SqlSelect部分--Begin
	IF @Year IS NULL OR @Year=''
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, VS.Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12月] '
	END
	ELSE
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, @Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 12, NULL) AS [12月] '
	END
	--有Year条件的SqlSelect部分--End
	
	--From部分--Begin
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '''') '
	END
	--甜品店
	ELSE
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN  
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = @StoreNo AND VS.KioskNo = @KioskNo '
	END
	--From部分--End
	
	--Where部分--Begin
	--餐厅
	IF ISNULL(@KioskNo,'')=''
		SET @SqlWhere = N'WHERE VS.StoreNo = @StoreNo '
	--甜品店
	ELSE
		SET @SqlWhere = N'WHERE 1=1 '
	--年
	IF @Year IS NOT NULL AND @Year<>''
		SET @SqlWhere += N'AND VS.Year=@Year '
	--Company
	IF @CompanyCode IS NOT NULL AND @CompanyCode<>''
		SET @SqlWhere += N'AND S.CompanyCode=@CompanyCode'
	--Where部分--End
	
	--组合成最终的Sql
	DECLARE @Sql nvarchar(max)
	SET @Sql = @SqlSelect + @SqlFrom + @SqlWhere
	
	RETURN @Sql

END





GO


