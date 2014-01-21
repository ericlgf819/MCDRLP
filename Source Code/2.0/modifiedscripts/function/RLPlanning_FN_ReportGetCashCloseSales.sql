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
	
	--����Sql
	DECLARE @SqlSelect nvarchar(max), @SqlFrom nvarchar(max), @SqlWhere nvarchar(max)
	
	--��Year������SqlSelect����--Begin
	IF @Year IS NULL OR @Year=''
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,
							@KioskName AS Kiosk, VS.Year AS ���, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12��] '
	END
	ELSE
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS �������, S.StoreName AS Store,
							@KioskName AS Kiosk, @Year AS ���, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 1, NULL) AS [1��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 2, NULL) AS [2��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 3, NULL) AS [3��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 4, NULL) AS [4��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 5, NULL) AS [5��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 6, NULL) AS [6��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 7, NULL) AS [7��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 8, NULL) AS [8��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 9, NULL) AS [9��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 10,NULL) AS [10��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 11, NULL) AS [11��],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 12, NULL) AS [12��] '
	END
	--��Year������SqlSelect����--End
	
	--From����--Begin
	--����
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '''') '
	END
	--��Ʒ��
	ELSE
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN  
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = @StoreNo AND VS.KioskNo = @KioskNo '
	END
	--From����--End
	
	--Where����--Begin
	--����
	IF ISNULL(@KioskNo,'')=''
		SET @SqlWhere = N'WHERE VS.StoreNo = @StoreNo '
	--��Ʒ��
	ELSE
		SET @SqlWhere = N'WHERE 1=1 '
	--��
	IF @Year IS NOT NULL AND @Year<>''
		SET @SqlWhere += N'AND VS.Year=@Year '
	--Company
	IF @CompanyCode IS NOT NULL AND @CompanyCode<>''
		SET @SqlWhere += N'AND S.CompanyCode=@CompanyCode'
	--Where����--End
	
	--��ϳ����յ�Sql
	DECLARE @Sql nvarchar(max)
	SET @Sql = @SqlSelect + @SqlFrom + @SqlWhere
	
	RETURN @Sql

END





GO


