USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Report_SelectSales]    Script Date: 08/28/2012 10:11:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Report_SelectSales]
@StoreNo nvarchar(50),
@AreaID nvarchar(50),
@CompanyCode nvarchar(50),
@Year nvarchar(20),
@EntityType nvarchar(20),
@IsCashYear BIT=0,	--是否是关帐年，默认不是关帐年
@PageIndex int,
@PageSize int=50,
@RecordCount int=0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--结果表
	DECLARE @Result TABLE(Company nvarchar(50), 餐厅编号 nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
	年度 nvarchar(50), [1月] nvarchar(500), [2月] nvarchar(500), [3月] nvarchar(500), [4月] nvarchar(500),
	[5月] nvarchar(500), [6月] nvarchar(500), [7月] nvarchar(500), [8月] nvarchar(500), [9月] nvarchar(500),
	[10月] nvarchar(500), [11月] nvarchar(500), [12月] nvarchar(500))
	
	--甜品店名称
	DECLARE @KioskName nvarchar(200)
	
	--要搜索的餐厅和甜品店的集合
	DECLARE @StoreKioskTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	--如果餐厅条件不为空，则将条件插入条件表中，来生成报表数据，否则将所有的餐厅和甜品店数据显示出来--Begin
	IF LTRIM(RTRIM(@EntityType))=''
	BEGIN
		--餐厅
		INSERT INTO @StoreKioskTbl
		SELECT S.StoreNo, NULL 
		FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE S.StoreNo LIKE '%'+ISNULL(@StoreNo,'')+'%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
		
		--对应甜品店
		INSERT INTO @StoreKioskTbl
		SELECT K.StoreNo, K.KioskNo FROM SRLS_TB_Master_Kiosk K
		INNER JOIN SRLS_TB_Master_Store S ON K.StoreNo = S.StoreNo
		INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE K.StoreNo LIKE '%'+ ISNULL(@StoreNo,'') + '%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	
	ELSE IF LTRIM(RTRIM(@EntityType))='餐厅'
	BEGIN
		--餐厅
		INSERT INTO @StoreKioskTbl
		SELECT S.StoreNo, NULL 
		FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE S.StoreNo LIKE '%'+ISNULL(@StoreNo,'')+'%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	
	ELSE IF LTRIM(RTRIM(@EntityType))='甜品店'
	BEGIN
		--对应甜品店
		INSERT INTO @StoreKioskTbl
		SELECT K.StoreNo, K.KioskNo FROM SRLS_TB_Master_Kiosk K
		INNER JOIN SRLS_TB_Master_Store S ON K.StoreNo = S.StoreNo
		INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE K.StoreNo LIKE '%'+ ISNULL(@StoreNo,'') + '%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	--如果餐厅条件不为空，则将条件插入条件表中，来生成报表数据，否则将所有的餐厅和甜品店数据显示出来--End
	
	--获取sales数据
	DECLARE @StoreKioskTblCursor CURSOR, @TmpStoreNo nvarchar(50), @TmpKioskNo nvarchar(50)
	SET @StoreKioskTblCursor = CURSOR SCROLL FOR
	SELECT StoreNo, KioskNo FROM @StoreKioskTbl
	
	--声明Paramers
	DECLARE @params nvarchar(max), @sql nvarchar(max)
	SET @params = N'@StoreNo nvarchar(50), @KioskNo nvarchar(50), @KioskName nvarchar(200), 
					@Year nvarchar(20), @CompanyCode nvarchar(50)'

	OPEN @StoreKioskTblCursor
	FETCH NEXT FROM @StoreKioskTblCursor INTO @TmpStoreNo, @TmpKioskNo
	WHILE @@FETCH_STATUS=0
	BEGIN
		--设置KioskName
		SET @KioskName = NULL
		SELECT @KioskName = KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @TmpKioskNo
		--获取sql
		--0 获取sales数据
		IF @IsCashYear=0
			SET @sql = dbo.RLPlanning_FN_ReportGetSales(@TmpStoreNo, @TmpKioskNo, '', @Year)
		--1 获取关帐sales数据
		ELSE 
			SET @sql = dbo.RLPlanning_FN_ReportGetCashCloseSales(@TmpStoreNo, @TmpKioskNo, '', @Year)
		--执行
		INSERT INTO @Result EXEC sp_executesql @sql, @params, @TmpStoreNo, @TmpKioskNo, @KioskName, @Year, @CompanyCode
		FETCH NEXT FROM @StoreKioskTblCursor INTO @TmpStoreNo, @TmpKioskNo
	END
	CLOSE @StoreKioskTblCursor
	DEALLOCATE @StoreKioskTblCursor

	--查询结果总数
	SELECT @RecordCount = COUNT(*) FROM @Result;
	
	--根据行索引筛选显示结果
	WITH tmp AS(
		SELECT ROW_NUMBER() OVER (ORDER BY 餐厅编号) AS RowIndex, * FROM @Result
	)SELECT * FROM tmp 
	WHERE RowIndex BETWEEN @PageIndex * @PageSize + 1 AND (@PageIndex + 1) * @PageSize
	ORDER BY 餐厅编号, Kiosk, 年度
END


GO


