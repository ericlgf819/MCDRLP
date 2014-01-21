USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_SelectStoreOrKiosk]    Script Date: 08/17/2012 14:52:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




















-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_SelectStoreOrKiosk]
(
	@Type nvarchar(50)='',
	@StoreNoOrName nvarchar(50)='',
	@CompanyCode nvarchar(50)='',
	@Status nvarchar(10)='',
	@UserID nvarchar(50)='',
	@AreaID nvarchar(50)='',
	@PageIndex int=-1,
	@PageSize int=50,
	@RecordCount int=0 OUTPUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Sql nvarchar(max), @Select nvarchar(max), @SelectTmp nvarchar(max), @ClearTmp nvarchar(max)
    DECLARE @SelectCount nvarchar(max), @Where nvarchar(max), @PageCondition nvarchar(max)
    SET @Sql=
	'WITH tmp AS (SELECT A.AreaName AS AreaName, ''甜品店'' AS Type, C.CompanyCode AS CompanyCode, C.SimpleName AS CompanyName,
	S.StoreNo AS StoreNo, S.StoreName AS StoreName,
	K.KioskNo AS KioskNo, K.KioskName AS KioskName, 
	K.Status AS Status, dbo.RLPlanning_FN_GetSalesUpdateTime(K.StoreNo, K.KioskNo) AS UpdateTime,
	dbo.RLPlanning_FN_GetMinOrMaxSalesDate('''',K.KioskNo,1) AS SalesBeginDate, 
	dbo.RLPlanning_FN_GetMinOrMaxSalesDate('''',K.KioskNo,0) AS SalesEndDate,
	UC.UserId AS UserID
	FROM SRLS_TB_Master_Store S 
	INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
	INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
	INNER JOIN v_KioskStoreZoneInfo V ON V.StoreNo = S.StoreNo
	INNER JOIN SRLS_TB_Master_Kiosk K ON V.KioskNo = K.KioskNo
	INNER JOIN RLP_UserCompany UC ON UC.CompanyCode=C.CompanyCode
	WHERE V.IsNeedSubtractSalse=1 AND GETDATE() BETWEEN V.StartDate AND V.EndDate
	UNION ALL
	SELECT A.AreaName AS AreaName, ''餐厅'' AS Type, C.CompanyCode AS CompanyCode, C.SimpleName AS CompanyName,
	S.StoreNo AS StoreNo, S.StoreName AS StoreName,
	NULL AS KioskNo, NULL AS KioskName, 
	S.Status AS Status, dbo.RLPlanning_FN_GetSalesUpdateTime(S.StoreNo, '''') AS UpdateTime,
	dbo.RLPlanning_FN_GetMinOrMaxSalesDate(S.StoreNo,'''',1) AS SalesBeginDate, 
	dbo.RLPlanning_FN_GetMinOrMaxSalesDate(S.StoreNo,'''',0) AS SalesEndDate,
	UC.UserId AS UserID
	FROM SRLS_TB_Master_Store S 
	INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode 
	INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
	INNER JOIN RLP_UserCompany UC ON UC.CompanyCode=C.CompanyCode)'
	
	--SELECT 部分
	SET @Select = 'SELECT *, ROW_NUMBER() OVER(ORDER BY UpdateTime DESC) AS RowIndex INTO #tmptbl FROM tmp '
	SET @SelectTmp = ';SELECT * FROM #tmptbl '
	SET @ClearTmp = ';DROP TABLE #tmptbl'
	
	--SELECT Count部分
	SET @SelectCount = N'SELECT @RecordCount=COUNT(*) FROM tmp '
	
	--为了AND的方便添加
	SET @Where='WHERE Type IS NOT NULL '
	
	--Where语句
	IF LTRIM(RTRIM(@Type)) <> ''
		SET @Where += 'AND Type=' + '''' + @Type + '''' + ' '
	IF LTRIM(RTRIM(@StoreNoOrName)) <> ''
		SET @Where += 'AND( StoreNo LIKE ''%' + @StoreNoOrName +'%'' ' + 'OR StoreName LIKE ''%' + @StoreNoOrName +'%'') '
	IF LTRIM(RTRIM(@CompanyCode)) <> ''
		SET @Where += 'AND CompanyCode=''' + @CompanyCode +''' '
	IF LTRIM(RTRIM(@Status)) <> ''
		SET @Where += 'AND Status=''' + @Status +''' '
	--ID
	SET @Where += 'AND UserID=''' + CAST(@UserID AS nvarchar(50)) + ''' '
	--Area
	IF LTRIM(RTRIM(@AreaID)) <> ''
	BEGIN
		DECLARE @SpecifiedAreaName nvarchar(50)
		SELECT @SpecifiedAreaName=AreaName FROM SRLS_TB_Master_Area WHERE ID=@AreaID
		SET @Where += 'AND AreaName='''+ @SpecifiedAreaName + ''' '
	END
	ELSE
	BEGIN
		DECLARE @AreaSqlConditionStr nvarchar(max)
		SET @AreaSqlConditionStr = dbo.RLPlanning_FN_GetAreaSqlConditionStr(@UserID, 1)
		
		IF @AreaSqlConditionStr<>''
			SET @Where += 'AND ' + dbo.RLPlanning_FN_GetAreaSqlConditionStr(@UserID, 1)
	END
	
	--和翻页有关的Where语句
	SET @PageCondition = ''
	IF @PageIndex > -1
	BEGIN
		SET @PageCondition = 'WHERE RowIndex BETWEEN ' + STR(@PageIndex * @PageSize + 1) + ' AND ' + STR((@PageIndex + 1)* @PageSize)
	END
	
	
	--执行
	DECLARE @SqlSelectOnly nvarchar(max), @SqlRecordCount nvarchar(max)
	SET @SqlSelectOnly = @Sql + @Select + @Where + @SelectTmp + @PageCondition + @ClearTmp
	SET @SqlRecordCount = @Sql + @SelectCount + @Where
	
	print @SqlSelectOnly
	EXEC(@SqlSelectOnly)
	print @SqlRecordCount
	EXECUTE sp_executesql @SqlRecordCount, N'@RecordCount INT OUTPUT', @RecordCount OUTPUT 
END




















GO


