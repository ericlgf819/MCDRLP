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
@IsCashYear BIT=0,	--�Ƿ��ǹ����꣬Ĭ�ϲ��ǹ�����
@PageIndex int,
@PageSize int=50,
@RecordCount int=0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--�����
	DECLARE @Result TABLE(Company nvarchar(50), ������� nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
	��� nvarchar(50), [1��] nvarchar(500), [2��] nvarchar(500), [3��] nvarchar(500), [4��] nvarchar(500),
	[5��] nvarchar(500), [6��] nvarchar(500), [7��] nvarchar(500), [8��] nvarchar(500), [9��] nvarchar(500),
	[10��] nvarchar(500), [11��] nvarchar(500), [12��] nvarchar(500))
	
	--��Ʒ������
	DECLARE @KioskName nvarchar(200)
	
	--Ҫ�����Ĳ�������Ʒ��ļ���
	DECLARE @StoreKioskTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	--�������������Ϊ�գ������������������У������ɱ������ݣ��������еĲ�������Ʒ��������ʾ����--Begin
	IF LTRIM(RTRIM(@EntityType))=''
	BEGIN
		--����
		INSERT INTO @StoreKioskTbl
		SELECT S.StoreNo, NULL 
		FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE S.StoreNo LIKE '%'+ISNULL(@StoreNo,'')+'%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
		
		--��Ӧ��Ʒ��
		INSERT INTO @StoreKioskTbl
		SELECT K.StoreNo, K.KioskNo FROM SRLS_TB_Master_Kiosk K
		INNER JOIN SRLS_TB_Master_Store S ON K.StoreNo = S.StoreNo
		INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE K.StoreNo LIKE '%'+ ISNULL(@StoreNo,'') + '%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	
	ELSE IF LTRIM(RTRIM(@EntityType))='����'
	BEGIN
		--����
		INSERT INTO @StoreKioskTbl
		SELECT S.StoreNo, NULL 
		FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE S.StoreNo LIKE '%'+ISNULL(@StoreNo,'')+'%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	
	ELSE IF LTRIM(RTRIM(@EntityType))='��Ʒ��'
	BEGIN
		--��Ӧ��Ʒ��
		INSERT INTO @StoreKioskTbl
		SELECT K.StoreNo, K.KioskNo FROM SRLS_TB_Master_Kiosk K
		INNER JOIN SRLS_TB_Master_Store S ON K.StoreNo = S.StoreNo
		INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
		WHERE K.StoreNo LIKE '%'+ ISNULL(@StoreNo,'') + '%'
		AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
		AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'
	END
	--�������������Ϊ�գ������������������У������ɱ������ݣ��������еĲ�������Ʒ��������ʾ����--End
	
	--��ȡsales����
	DECLARE @StoreKioskTblCursor CURSOR, @TmpStoreNo nvarchar(50), @TmpKioskNo nvarchar(50)
	SET @StoreKioskTblCursor = CURSOR SCROLL FOR
	SELECT StoreNo, KioskNo FROM @StoreKioskTbl
	
	--����Paramers
	DECLARE @params nvarchar(max), @sql nvarchar(max)
	SET @params = N'@StoreNo nvarchar(50), @KioskNo nvarchar(50), @KioskName nvarchar(200), 
					@Year nvarchar(20), @CompanyCode nvarchar(50)'

	OPEN @StoreKioskTblCursor
	FETCH NEXT FROM @StoreKioskTblCursor INTO @TmpStoreNo, @TmpKioskNo
	WHILE @@FETCH_STATUS=0
	BEGIN
		--����KioskName
		SET @KioskName = NULL
		SELECT @KioskName = KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @TmpKioskNo
		--��ȡsql
		--0 ��ȡsales����
		IF @IsCashYear=0
			SET @sql = dbo.RLPlanning_FN_ReportGetSales(@TmpStoreNo, @TmpKioskNo, '', @Year)
		--1 ��ȡ����sales����
		ELSE 
			SET @sql = dbo.RLPlanning_FN_ReportGetCashCloseSales(@TmpStoreNo, @TmpKioskNo, '', @Year)
		--ִ��
		INSERT INTO @Result EXEC sp_executesql @sql, @params, @TmpStoreNo, @TmpKioskNo, @KioskName, @Year, @CompanyCode
		FETCH NEXT FROM @StoreKioskTblCursor INTO @TmpStoreNo, @TmpKioskNo
	END
	CLOSE @StoreKioskTblCursor
	DEALLOCATE @StoreKioskTblCursor

	--��ѯ�������
	SELECT @RecordCount = COUNT(*) FROM @Result;
	
	--����������ɸѡ��ʾ���
	WITH tmp AS(
		SELECT ROW_NUMBER() OVER (ORDER BY �������) AS RowIndex, * FROM @Result
	)SELECT * FROM tmp 
	WHERE RowIndex BETWEEN @PageIndex * @PageSize + 1 AND (@PageIndex + 1) * @PageSize
	ORDER BY �������, Kiosk, ���
END


GO


