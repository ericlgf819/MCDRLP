USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesByYearAndMonth]    Script Date: 08/17/2012 15:19:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION  [dbo].[RLPlanning_FN_GetSalesByYearAndMonth]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@Year nvarchar(50),
	@Month nvarchar(50),
	@IsForecastSales bit=1 --ȡԤ��sales���ݻ�����ʵsales���ݵı�־λ��Ĭ��ΪȡԤ��sales����, �����NULL��ʾ�����߶�ȡ��ֵ
)
RETURNS decimal(18,2)
AS
BEGIN
	DECLARE @Sales DECIMAL(18,2)
	DECLARE @ForecastSales DECIMAL(18,2), @RealSales DECIMAL(18,2)
	
	--����
	IF @KioskNo IS NULL OR @KioskNo=''
	BEGIN
		--Ԥ������
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
		--��ʵ����
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
	END
	--��Ʒ��
	ELSE
	BEGIN
		--Ԥ������
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND KioskNo = @KioskNo
		--��ʵ����
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND KioskNo = @KioskNo	
	END	
	
	--���߶�ȡ������������ݣ�����ʵ����Ϊ׼
	IF @IsForecastSales IS NULL
	BEGIN
		IF @ForecastSales IS NOT NULL AND @RealSales IS NOT NULL
			SET @Sales = @RealSales
		ELSE IF @ForecastSales IS NULL AND @RealSales IS NOT NULL
			SET @Sales = @RealSales
		ELSE IF @ForecastSales IS NOT NULL AND @RealSales IS NULL
			SET @Sales = @ForecastSales
		ELSE
			SET @Sales = NULL
	END
	--ȡԤ������
	ELSE IF @IsForecastSales = 1
	BEGIN
		SET @Sales = @ForecastSales
	END
	--ȡ��ʵ����
	ELSE IF @IsForecastSales = 0
	BEGIN
		SET @Sales = @RealSales
	END

	RETURN @Sales
END






GO


