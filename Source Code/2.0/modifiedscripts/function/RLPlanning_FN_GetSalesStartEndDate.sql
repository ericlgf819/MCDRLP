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
	--����
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		--�����ʵsales������û�����ݣ���ȥԤ�����ݱ��в���
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
		
		--�������ݽ���ʱ����Ҫ���õ��µ�
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	--��Ʒ��
	ELSE
	BEGIN
		--�����ʵsales������û�����ݣ���ȥԤ�����ݱ��в���
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
		
		--�������ݽ���ʱ����Ҫ���õ��µ�
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	
	--������
	INSERT INTO @Result SELECT @SalesStartDate, @SalesEndDate
	RETURN
END





GO


