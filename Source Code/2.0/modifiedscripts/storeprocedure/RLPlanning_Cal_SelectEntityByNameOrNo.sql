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
--�����KioskkNo,KioskName��û���ˣ�StoreNameָ����ʵ�����ƣ�����
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
	
	--�����
	DECLARE @Result 
	TABLE(CompanyCode nvarchar(50), CompanyName nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50),
	EntityType nvarchar(10), EntityName nvarchar(200), 
	RentStartDate datetime, RentEndDate datetime,
	SalesStartDate datetime, SalesEndDate datetime)
	
	--�Ƚ�StoreNo��KioskNoɸѡһ��
	
	--��ֹ����ȫ����ɸѡ�����������Ҫ�ѿ��ַ�������ΪNULL
	--�����ʱҲû����
	--IF @StoreNo = ''
	--	SET @StoreNo = NULL
	--IF @StoreName = ''
	--	SET @StoreName = NULL
	--IF @KioskNo = ''
	--	SET @KioskNo = NULL
	--IF @KioskName = ''
	--	SET @KioskName = NULL
	
	--�Ƚ���������ɸѡ����
	INSERT INTO @StoreNoKioskNoTbl
	SELECT S.StoreNo, NULL FROM SRLS_TB_Master_Store S 
	WHERE (S.StoreNo LIKE '%' + @StoreNo + '%' 
	AND S.StoreName LIKE '%' + @StoreName + '%')
	AND S.Status='A'
	AND S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	
	--�ٽ���Ʒ�������ɸѡ����
	INSERT INTO @StoreNoKioskNoTbl
	SELECT V.StoreNo, V.KioskNo FROM v_KioskStoreZoneInfo V 
	INNER JOIN SRLS_TB_Master_Kiosk K ON V.KioskNo=K.KioskNo
	INNER JOIN SRLS_TB_Master_Store S ON V.StoreNo=S.StoreNo
	WHERE (V.StoreNo LIKE '%' + @StoreNo + '%' 
	--OR K.KioskNo LIKE '%' + @KioskNo + '%'
	--��Ʒ��Ĳ�ѯ������ʱ��Ҫ��
	--OR S.StoreName LIKE '%' + @StoreName + '%')
	AND K.KioskName LIKE '%' + @StoreName + '%')
	AND K.Status='A'
	AND S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND @CurDate BETWEEN V.StartDate AND V.EndDate
	
	--���û�����ݣ�����û���κ��������룬��ֱ�Ӵ�NULLֵ����һ���洢���̣��������companyȫ������
	IF NOT EXISTS (SELECT * FROM @StoreNoKioskNoTbl) AND @StoreNo IS NULL
	AND @StoreName IS NULL AND @KioskNo IS NULL AND @KioskName IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT * FROM dbo.RLPlanning_FN_Cal_GetSelectStoreOrKiosk(NULL,NULL)
	END
	
	--�������ݵ�����һ���洢����
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
	
	--��companycode��storeno����
	SELECT * FROM @Result ORDER BY CompanyCode, StoreNo
END












GO


