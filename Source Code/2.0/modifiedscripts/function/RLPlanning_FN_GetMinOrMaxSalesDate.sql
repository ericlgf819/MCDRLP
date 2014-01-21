USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetMinOrMaxSalesDate]    Script Date: 08/04/2012 16:45:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetMinOrMaxSalesDate]
(
	@StoreNo nvarchar(50)='',
	@KioskNo nvarchar(50)='',
	@IsMin BIT=1
)
RETURNS DATE
AS
BEGIN
	DECLARE @RetVal DATE
	DECLARE @ForecastDate DATE, @RealDate DATE
	IF @KioskNo <> ''
	BEGIN
		IF @IsMin=1
		BEGIN
			SELECT @ForecastDate = MIN(SalesDate) FROM Forecast_Sales WHERE KioskNo=@KioskNo
			SELECT @RealDate = MIN(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
			
			--如果真实数据有值，肯定取真实数据值作为租金最小时间
			IF @RealDate IS NOT NULL
				SET @RetVal = @RealDate
			ELSE 
				SET @RetVal = @ForecastDate
		END
		ELSE
		BEGIN
			SELECT @ForecastDate = MAX(SalesDate) FROM Forecast_Sales WHERE KioskNo=@KioskNo
			SELECT @RealDate = MAX(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
			
			--如果预测数据有值，肯定取预测时间最大值作为租金最大时间
			IF @ForecastDate IS NOT NULL
				SET @RetVal = @ForecastDate
			ELSE 
				SET @RetVal = @RealDate
		END
	END
	ELSE IF @StoreNo <> ''
	BEGIN
		IF @IsMin=1
		BEGIN
			SELECT @ForecastDate = MIN(SalesDate) FROM Forecast_Sales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			SELECT @RealDate = MIN(SalesDate) FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			
			--如果真实数据有值，肯定取真实数据值作为租金最小时间
			IF @RealDate IS NOT NULL
				SET @RetVal = @RealDate
			ELSE 
				SET @RetVal = @ForecastDate
		END
		ELSE
		BEGIN
			SELECT @ForecastDate = MAX(SalesDate) FROM Forecast_Sales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			SELECT @RealDate = MAX(SalesDate) FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			
			--如果预测数据有值，肯定取预测时间最大值作为租金最大时间
			IF @ForecastDate IS NOT NULL
				SET @RetVal = @ForecastDate
			ELSE 
				SET @RetVal = @RealDate
		END
	END
	
	--如果是要取sales最大值，则需要将时间设置到月底
	IF @IsMin=0
		SET @RetVal = DATEADD(DAY,-1,DATEADD(MONTH,1,@RetVal))
		
	RETURN @RetVal
END







GO


