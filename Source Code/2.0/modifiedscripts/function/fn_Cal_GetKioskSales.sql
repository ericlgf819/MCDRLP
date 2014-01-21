USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetKioskSales]    Script Date: 08/09/2012 10:35:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











-- =============================================
-- Author:		zhangbsh
-- Create date: 20110309
-- Description:	��ȡKIOSKSALES
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetKioskSales]
(
	-- Add the parameters for the function here
	@KioskOrStoreNo NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	
	DECLARE @SalesType NVARCHAR(50)
	IF EXISTS(SELECT * FROM dbo.SRLS_TB_Master_Kiosk 
	WHERE KioskNo=@KioskOrStoreNo)
	BEGIN
		SELECT @SalesType='��Ʒ��'
	END
	ELSE
	BEGIN
		SELECT @SalesType='����'
	END
	
	IF @SalesType='��Ʒ��'
	BEGIN
		SELECT @ResultVar=0
		
		--Sales����֮��
		SELECT @ResultVar=ISNULL(SUM(Sales),0) 
		FROM dbo.KioskSales s 
		INNER JOIN dbo.SRLS_TB_Master_Kiosk k ON s.KioskID=k.KioskID
		WHERE (dbo.fn_GetDate(s.SalesDate) BETWEEN @StartDate AND @EndDate)
		AND k.KioskNo=@KioskOrStoreNo
		AND s.Sales IS NOT NULL 
		
		--Sales�ռ�֮��
		SELECT @ResultVar=@ResultVar+ISNULL(SUM(Sales),0) 
		FROM dbo.KioskSalesCollection c 
		INNER JOIN dbo.SRLS_TB_Master_Kiosk k ON c.KioskID=k.KiosKID
		WHERE (c.ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (c.ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND k.KioskNo=@KioskOrStoreNo
			AND c.Sales IS NOT NULL 
	END
	
	
	IF @SalesType='����'
	BEGIN
		SELECT @ResultVar=0
		
		--�Ȼ�ȡ�ҿ�ʱ���
		DECLARE @KioskStoreZoneInfoTable TABLE(KioskID NVARCHAR(50),KioskNo NVARCHAR(50),IsNeedSubtractSalse BIT,StoreNo NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
		INSERT INTO @KioskStoreZoneInfoTable
		SELECT KioskID, KioskNo, IsNeedSubtractSalse, StoreNo, StartDate, EndDate FROM v_KioskStoreZoneInfo 
		WHERE IsNeedSubtractSalse=1 
			AND StoreNo=@KioskOrStoreNo
			AND dbo.fn_IsDateZoneIntersection(@StartDate,@EndDate,StartDate,EndDate)=1
		
		SELECT @ResultVar=0
		
		--Sales����֮��
		--�������ڱ����ڱ����ռ������������ڹҿ���ϵ��������������
		SELECT @ResultVar=ISNULL(SUM(Sales),0) 
		FROM 
		(
			SELECT DISTINCT k.KioskSalesID,k.Sales 
			FROM dbo.KioskSales k 
			INNER JOIN @KioskStoreZoneInfoTable z ON k.KioskID = z.KioskID
			AND  (dbo.fn_GetDate(k.SalesDate) BETWEEN z.StartDate AND z.EndDate)
			AND (dbo.fn_GetDate(k.SalesDate) BETWEEN @StartDate AND @EndDate)
			AND k.Sales IS NOT NULL 
		) x
			
		--Sales�ռ�֮��
		--�ռ���ʼ�������ڱ����ڱ����ռ������������ڹҿ���ϵ��������������
		SELECT @ResultVar=@ResultVar+ISNULL(SUM(Sales),0) 
		FROM 
		(
			SELECT k.CollectionID,k.Sales 
			FROM dbo.KioskSalesCollection k
			INNER JOIN @KioskStoreZoneInfoTable z ON k.KioskID = z.KioskID
			AND (ZoneStartDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneEndDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND k.Sales IS NOT NULL 
		) x
	END
		
	SELECT @ResultVar = ISNULL(@ResultVar,0)
	RETURN @ResultVar


END










GO


