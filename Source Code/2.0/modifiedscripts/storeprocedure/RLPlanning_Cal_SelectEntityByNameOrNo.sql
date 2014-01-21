USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_SelectEntityByNameOrNo]    Script Date: 08/17/2012 12:40:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_SelectEntityByNameOrNo]
@StoreNo nvarchar(50),
@StoreName nvarchar(200),
--这里的KioskkNo,KioskName都没用了，StoreName指的是实体名称！！！
@KioskNo nvarchar(50),
@KioskName nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CurDate datetime
	SET @CurDate=GETDATE()
	
	DECLARE @StoreNoKioskNoTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	--结果集
	DECLARE @Result 
	TABLE(CompanyCode nvarchar(50), CompanyName nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50),
	EntityType nvarchar(10), EntityName nvarchar(200), 
	RentStartDate datetime, RentEndDate datetime,
	SalesStartDate datetime, SalesEndDate datetime)
	
	--先将StoreNo与KioskNo筛选一遍
	
	--防止发生全部都筛选的情况，所以要把空字符串设置为NULL
	--这个暂时也没用了
	--IF @StoreNo = ''
	--	SET @StoreNo = NULL
	--IF @StoreName = ''
	--	SET @StoreName = NULL
	--IF @KioskNo = ''
	--	SET @KioskNo = NULL
	--IF @KioskName = ''
	--	SET @KioskName = NULL
	
	--先将餐厅数据筛选出来
	INSERT INTO @StoreNoKioskNoTbl
	SELECT S.StoreNo, NULL FROM SRLS_TB_Master_Store S 
	WHERE (S.StoreNo LIKE '%' + @StoreNo + '%' 
	AND S.StoreName LIKE '%' + @StoreName + '%')
	AND S.Status='A'
	AND S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	
	--再将甜品店的数据筛选出来
	INSERT INTO @StoreNoKioskNoTbl
	SELECT V.StoreNo, V.KioskNo FROM v_KioskStoreZoneInfo V 
	INNER JOIN SRLS_TB_Master_Kiosk K ON V.KioskNo=K.KioskNo
	INNER JOIN SRLS_TB_Master_Store S ON V.StoreNo=S.StoreNo
	WHERE (V.StoreNo LIKE '%' + @StoreNo + '%' 
	--OR K.KioskNo LIKE '%' + @KioskNo + '%'
	--甜品店的查询条件暂时不要了
	--OR S.StoreName LIKE '%' + @StoreName + '%')
	AND K.KioskName LIKE '%' + @StoreName + '%')
	AND K.Status='A'
	AND S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND @CurDate BETWEEN V.StartDate AND V.EndDate
	
	--如果没有数据，并且没有任何条件输入，则直接传NULL值到下一级存储过程，表面根据company全区搜索
	IF NOT EXISTS (SELECT * FROM @StoreNoKioskNoTbl) AND @StoreNo IS NULL
	AND @StoreName IS NULL AND @KioskNo IS NULL AND @KioskName IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT * FROM dbo.RLPlanning_FN_Cal_GetSelectStoreOrKiosk(NULL,NULL)
	END
	
	--逐条数据调用下一级存储过程
	DECLARE @StoreNoKioskNoCursor CURSOR
	SET @StoreNoKioskNoCursor = CURSOR SCROLL FOR 
	SELECT DISTINCT StoreNo, KioskNo FROM @StoreNoKioskNoTbl ORDER BY StoreNo
	
	OPEN @StoreNoKioskNoCursor
	FETCH NEXT FROM @StoreNoKioskNoCursor INTO @StoreNo, @KioskNo
	WHILE @@FETCH_STATUS=0
	BEGIN
		INSERT INTO @Result
		SELECT * FROM dbo.RLPlanning_FN_Cal_GetSelectStoreOrKiosk(@StoreNo,@KioskNo)
		
		FETCH NEXT FROM @StoreNoKioskNoCursor INTO @StoreNo, @KioskNo
	END
	CLOSE @StoreNoKioskNoCursor
	DEALLOCATE @StoreNoKioskNoCursor
	
	--按companycode与storeno排序
	SELECT * FROM @Result ORDER BY CompanyCode, StoreNo
END












GO


