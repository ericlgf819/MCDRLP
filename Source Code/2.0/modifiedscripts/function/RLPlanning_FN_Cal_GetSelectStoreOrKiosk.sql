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
	
	--ָ�������ض���������Ʒ��ʱ��ֻ����һ�Ҳ���������Ʒ��--Begin
	--����--Begin
	IF @StoreNo IS NOT NULL AND @StoreNo <> '' AND (@KioskNo IS NULL OR @KioskNo = '')
	BEGIN
		--��ȡ��Լ��ʼʱ����Sales��ʼʱ��
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, NULL)	
		
		--��ʾ�����������������
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'����' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
		
		RETURN;
	END
	--����--End
	
	--��Ʒ��--Begin
	IF @KioskNo IS NOT NULL AND @KioskNo <> ''
	BEGIN
		--��ȡ��Ʒ�굱ǰ�ҿ���StoreNo
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo AND 
		@CurDate BETWEEN StartDate AND EndDate
		
		--��ȡ��Ʒ�������
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo
		
		--��ȡ��Լ��ʼʱ����Sales��ʼʱ��
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, @KioskNo)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, @KioskNo)	
		
		--��ʾ��Ʒ�굱ǰʱ��ҿ��Ĳ������������
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, @KioskNo AS KioskNo,
		'��Ʒ��' AS EntityType, @KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
	
		RETURN;
	END
	--��Ʒ��--End
	--ָ�������ض���������Ʒ��ʱ��ֻ����һ�Ҳ���������Ʒ��--End
	
	--ָ����˾���������ظù�˾�����еĲ�������Ʒ����Ϣ--Begin
	
	--�Ƚ���ѡ��˾�����еĲ�������Ʒ����ҳ�����������ʱ����
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
	--����
	DECLARE @StoreNoCursor CURSOR
	SET @StoreNoCursor = CURSOR SCROLL FOR SELECT StoreNo FROM @StoreNoTbl
	OPEN @StoreNoCursor
	FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--��ȡ��Լ��ʼʱ����Sales��ʼʱ��
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, NULL)	
		
		--��������
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'����' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNoTmp
		
		FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	END
	CLOSE @StoreNoCursor
	DEALLOCATE @StoreNoCursor
	
	--��Ʒ��
	DECLARE @KioskNoCursor CURSOR
	SET @KioskNoCursor = CURSOR SCROLL FOR SELECT StoreNo, KioskNo FROM @StoreNoKioskNoTbl
	OPEN @KioskNoCursor
	FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--��ȡ��Ʒ�굱ǰ�ҿ���StoreNo
		SELECT @StoreNoTmp=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNoTmp AND 
		@CurDate BETWEEN StartDate AND EndDate
		
		--��ȡ��Ʒ�������
		SELECT @KioskName=KioskName FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNoTmp
		
		--��ȡ��Լ��ʼʱ����Sales��ʼʱ��
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, @KioskNoTmp)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, @KioskNoTmp)	
		
		--��������
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, @KioskNoTmp AS KioskNo,
		'��Ʒ��' AS EntityType, @KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNoTmp
	
		FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	END
	CLOSE @KioskNoCursor
	DEALLOCATE @KioskNoCursor
	--ָ����˾���������ظù�˾�����еĲ�������Ʒ����Ϣ--End
	
	RETURN 
END






GO


