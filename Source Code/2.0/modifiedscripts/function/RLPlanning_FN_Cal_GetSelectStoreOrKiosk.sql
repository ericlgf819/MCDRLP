USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_Cal_GetSelectStoreOrKiosk]    Script Date: 08/17/2012 12:17:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_Cal_GetSelectStoreOrKiosk]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS 
@Result 
TABLE(CompanyCode nvarchar(50), CompanyName nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50),
	EntityType nvarchar(10), EntityName nvarchar(200), 
	RentStartDate datetime, RentEndDate datetime,
	SalesStartDate datetime, SalesEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @CurDate datetime
	SET @CurDate=GETDATE()
	
	DECLARE @KioskName nvarchar(200)
	
	DECLARE @RentStartDate datetime, @RentEndDate datetime
	DECLARE @SalesStartDate datetime, @SalesEndDate datetime
	
	--指定搜索特定餐厅或甜品店时，只查找一家餐厅或者甜品店--Begin
	--餐厅--Begin
	IF @StoreNo IS NOT NULL AND @StoreNo <> '' AND (@KioskNo IS NULL OR @KioskNo = '')
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, NULL)	
		
		--显示单个餐厅的相关数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'餐厅' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
		
		RETURN;
	END
	--餐厅--End
	
	--甜品店--Begin
	IF @KioskNo IS NOT NULL AND @KioskNo <> ''
	BEGIN
		--获取甜品店当前挂靠的StoreNo
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo AND 
		@CurDate BETWEEN StartDate AND EndDate
		
		--获取甜品店的名称
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo
		
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, @KioskNo)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, @KioskNo)	
		
		--显示甜品店当前时间挂靠的餐厅的相关数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, @KioskNo AS KioskNo,
		'甜品店' AS EntityType, @KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
	
		RETURN;
	END
	--甜品店--End
	--指定搜索特定餐厅或甜品店时，只查找一家餐厅或者甜品店--End
	
	--指定公司搜索，返回该公司下所有的餐厅与甜品店信息--Begin
	
	--先将所选公司下所有的餐厅与甜品店给找出来，放入临时表中
	DECLARE @StoreNoTbl TABLE(StoreNo nvarchar(50))
	DECLARE @StoreNoKioskNoTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	INSERT INTO @StoreNoTbl
	SELECT StoreNo FROM SRLS_TB_Master_Store 
	WHERE CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND Status='A'

	INSERT INTO @StoreNoKioskNoTbl
	SELECT S.StoreNo, K.KioskNo
	FROM v_KioskStoreZoneInfo V 
	INNER JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=V.KioskNo
	INNER JOIN SRLS_TB_Master_Store S ON S.StoreNo=V.StoreNo
	WHERE S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND K.Status='A' AND @CurDate BETWEEN V.StartDate AND V.EndDate
	
	
	DECLARE @StoreNoTmp nvarchar(50), @KioskNoTmp nvarchar(50)
	--餐厅
	DECLARE @StoreNoCursor CURSOR
	SET @StoreNoCursor = CURSOR SCROLL FOR SELECT StoreNo FROM @StoreNoTbl
	OPEN @StoreNoCursor
	FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, NULL)	
		
		--插入数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'餐厅' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNoTmp
		
		FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	END
	CLOSE @StoreNoCursor
	DEALLOCATE @StoreNoCursor
	
	--甜品店
	DECLARE @KioskNoCursor CURSOR
	SET @KioskNoCursor = CURSOR SCROLL FOR SELECT StoreNo, KioskNo FROM @StoreNoKioskNoTbl
	OPEN @KioskNoCursor
	FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--获取甜品店当前挂靠的StoreNo
		SELECT @StoreNoTmp=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNoTmp AND 
		@CurDate BETWEEN StartDate AND EndDate
		
		--获取甜品店的名称
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNoTmp
		
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, @KioskNoTmp)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, @KioskNoTmp)	
		
		--插入数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, @KioskNoTmp AS KioskNo,
		'甜品店' AS EntityType, @KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNoTmp
	
		FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	END
	CLOSE @KioskNoCursor
	DEALLOCATE @KioskNoCursor
	--指定公司搜索，返回该公司下所有的餐厅与甜品店信息--End
	
	RETURN 
END






GO


