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
	@IsForecastSales bit=1 --取预测sales数据还是真实sales数据的标志位，默认为取预测sales数据, 如果是NULL表示从两边都取数值
)
RETURNS decimal(18,2)
AS
BEGIN
	DECLARE @Sales DECIMAL(18,2)
	DECLARE @ForecastSales DECIMAL(18,2), @RealSales DECIMAL(18,2)
	
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo=''
	BEGIN
		--预测数据
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
		--真实数据
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
	END
	--甜品店
	ELSE
	BEGIN
		--预测数据
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND KioskNo = @KioskNo
		--真实数据
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND KioskNo = @KioskNo	
	END	
	
	--两者都取，如果都有数据，以真实数据为准
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
	--取预测数据
	ELSE IF @IsForecastSales = 1
	BEGIN
		SET @Sales = @ForecastSales
	END
	--取真实数据
	ELSE IF @IsForecastSales = 0
	BEGIN
		SET @Sales = @RealSales
	END

	RETURN @Sales
END






GO


