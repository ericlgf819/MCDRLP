USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesStartEndDate]    Script Date: 08/17/2012 10:34:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetSalesStartEndDate]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS @Result TABLE(SalesStartDate datetime, SalesEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @SalesStartDate datetime, @SalesEndDate datetime
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		--如果真实sales数据里没有数据，则去预测数据表中查找
		SELECT @SalesStartDate = MIN(SalesDate) FROM RLPlanning_RealSales 
		WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		
		IF @SalesStartDate IS NULL
		BEGIN
			SELECT @SalesStartDate = MIN(SalesDate) FROM Forecast_Sales 
			WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		END
		
		SELECT @SalesEndDate = MAX(SalesDate) FROM Forecast_Sales 
		WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		
		IF @SalesEndDate IS NULL
		BEGIN
			SELECT @SalesEndDate = MAX(SalesDate) FROM RLPlanning_RealSales 
			WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')	
		END
		
		--销售数据结束时间需要设置到月底
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	--甜品店
	ELSE
	BEGIN
		--如果真实sales数据里没有数据，则去预测数据表中查找
		SELECT @SalesStartDate = MIN(SalesDate) FROM RLPlanning_RealSales 
		WHERE KioskNo = @KioskNo
		
		IF @SalesStartDate IS NULL
		BEGIN
			SELECT @SalesStartDate = MIN(SalesDate) FROM Forecast_Sales 
			WHERE KioskNo = @KioskNo
		END
		
		SELECT @SalesEndDate = MAX(SalesDate) FROM Forecast_Sales 
		WHERE KioskNo = @KioskNo
		
		IF @SalesEndDate IS NULL
		BEGIN
			SELECT @SalesEndDate = MAX(SalesDate) FROM RLPlanning_RealSales 
			WHERE KioskNo = @KioskNo
		END
		
		--销售数据结束时间需要设置到月底
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	
	--结果输出
	INSERT INTO @Result SELECT @SalesStartDate, @SalesEndDate
	RETURN
END





GO


