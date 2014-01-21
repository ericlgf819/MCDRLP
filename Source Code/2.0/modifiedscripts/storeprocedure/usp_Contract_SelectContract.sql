USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_SelectContract]    Script Date: 08/01/2012 15:43:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO














-- ========================================
-- Author:     liujj
-- Create Date:20101221
-- Description:获取合同信息
-- ========================================
CREATE Procedure [dbo].[usp_Contract_SelectContract]
(
	@StoreOrDeptNo NVARCHAR(100)='',    --餐厅/部门编号
	@VendorNo NVARCHAR(50)='',   --业主编号
	@CompanyNo NVARCHAR(100)='',  --公司编号
	@ContractNo NVARCHAR(100)='', --合同编号
	@Area UNIQUEIDENTIFIER='00000000-0000-0000-0000-000000000000', --区域
	@Status NVARCHAR(100)='',       -- 状态\
	@FromSRLS bit = NULL,			--是否来自SRLS
	@UserID uniqueidentifier=NULL,
	@PageIndex int=-1,              -- 页索引
	@PageSize  int=50,              -- 每页显示的条数
	@RecordCount int=0 OUTPUT       -- 总记录数
)
As
BEGIN
	Declare @Sql nvarchar(MAX)
	Declare @Where nvarchar(MAX)
	--Modified by Eric
	SET @Sql = 'With tmp AS (
		SELECT C.ContractSnapshotID, C.CompanyCode, C.ContractID, C.ContractNO, C.Version, 
			C.ContractName, C.CompanyName, C.Status, C.IsLocked, 
			C.CreateTime,C.CreatorName,C.LastModifyTime, c.LastModifyUserName, C.FromSRLS,
			E.EntityTypeName, E.EntityNo, E.EntityName, E.StoreOrDeptNo, 
			E.KioskNo, CONVERT(NVARCHAR(10),E.OpeningDate,25) as OpeningDate, CONVERT(NVARCHAR(10),E.RentStartDate,25) as RentStartDate, CONVERT(NVARCHAR(10),E.RentEndDate,25) as RentEndDate, VC.VendorName, 
			VC.IsVirtual, VC.VendorNo, A.ID AS AreaID, A.AreaName, UC.UserId,
			ROW_NUMBER() OVER (ORDER BY C.CreateTime DESC,C.ContractNO ASC) AS RowIndex
		FROM dbo.Contract AS C  
			INNER JOIN dbo.SRLS_TB_Master_Company AS CP ON C.CompanyCode = CP.CompanyCode 
			INNER JOIN dbo.RLP_UserCompany AS UC ON CP.CompanyCode = UC.CompanyCode
			LEFT JOIN dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID 
			LEFT JOIN dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID 
			LEFT JOIN dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID AND VE.VendorNo = VC.VendorNo 
			LEFT JOIN dbo.SRLS_TB_Master_Area AS A ON CP.AreaID = A.ID '
		
	SET @Where = 'WHERE C.SnapshotCreateTime IS NULL '
	IF LTRIM(RTRIM(@StoreOrDeptNo)) <> ''
		SET @Where = @Where + 'AND E.StoreOrDeptNo LIKE ''%'+ @StoreOrDeptNo + '%'' '--查询餐厅/部门编号
	IF LTRIM(RTRIM(@VendorNo)) <> ''
		SET @Where = @Where + 'AND VC.VendorNo LIKE ''%' + @VendorNo + '%'' '--查询业主编号
	IF LTRIM(RTRIM(@CompanyNo)) <> ''
		SET @Where = @Where + 'AND C.CompanyCode LIKE ''%' + @CompanyNo + '%'' '--查询公司编号
	IF LTRIM(RTRIM(@ContractNo)) <> ''
		SET @Where = @Where + 'AND C.ContractNO LIKE ''%' + @ContractNo + '%'' '--查询合同编号
	IF LTRIM(RTRIM(@Area)) IS NOT NULL AND @Area<>'00000000-0000-0000-0000-000000000000'
		SET @Where = @Where + 'AND A.ID=''' + CAST(@Area AS VARCHAR(36)) + ''' '--查询区域
	IF LTRIM(RTRIM(@Status)) <> ''
		SET @Where = @Where + 'AND C.Status = ''' + @Status + ''' '--查询状态
	IF @UserID IS NOT NULL
		SET @Where = @Where + 'AND UC.UserId = ''' + CAST(@UserID AS nvarchar(50)) + ''' '--用户权限控制
	IF @FromSRLS IS NOT NULL
		SET @Where = @Where + 'AND C.FromSRLS=''' + CAST(@FromSRLS AS nvarchar(1)) + ''' '
	
	Set @Sql = @Sql + @Where +  ') Select * from tmp '
	if @PageIndex > -1
		Set @Sql = @Sql + ' Where RowIndex BETWEEN '+ STR(@PageIndex * @PageSize + 1) +' AND '+ STR((@PageIndex + 1) * @PageSize)
	PRINT @Sql
	EXEC(@Sql)

    DECLARE @RowCount INT
	SET @Sql = 'SELECT @RecordCount=COUNT(*) FROM dbo.Contract AS C  
			INNER JOIN dbo.SRLS_TB_Master_Company AS CP ON C.CompanyCode = CP.CompanyCode 
			INNER JOIN dbo.RLP_UserCompany AS UC ON CP.CompanyCode = UC.CompanyCode
			LEFT JOIN dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID 
			LEFT JOIN dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID 
			LEFT JOIN dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID AND VE.VendorNo = VC.VendorNo 
			LEFT JOIN dbo.SRLS_TB_Master_Area AS A ON CP.AreaID = A.ID '+ @Where
	EXECUTE sp_executesql @Sql,N'@RecordCount int OUTPUT',@RecordCount OUTPUT
END
	
--test
--EXEC usp_Contract_SelectContract '','','','',NULL,'',-1, 50,0







GO


