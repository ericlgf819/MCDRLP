USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_Kiosk2RealSales]    Script Date: 08/10/2012 10:45:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_Kiosk2RealSales]
@StoreNo nvarchar(50),
@KioskNo nvarchar(50)
AS
BEGIN
	BEGIN TRY
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		DECLARE @SalesSum decimal(18,2), @SalesAdjustmentSum decimal(18,2)
		DECLARE @KioskID nvarchar(50)
		SELECT @KioskID=KioskID FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo
	    
		--如果KioskNo错误，则直接返回
		IF @KioskID IS NULL
		BEGIN
			RETURN
		END
			
		--如果没有Sales数据，则直接返回
		IF NOT EXISTS (SELECT * FROM KioskSalesCollection WHERE KioskID=@KioskID AND FromSRLS=1)
		BEGIN
			RETURN
		END
			
		--获取已经Dispatch过来sales数据的最大时间
		DECLARE @MaxSalesDate DATETIME
		SELECT @MaxSalesDate=MAX(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
		
		--获取真正KioskSales的数据的最大时间
		DECLARE @MaxRealSalesDate DATETIME
		SELECT @MaxRealSalesDate=MAX(ZoneEndDate) FROM KioskSalesCollection
		WHERE KioskID=@KioskID AND FromSRLS=1
		
		--如果没有真实的sales数据，则不用继续了
		IF @MaxRealSalesDate IS NULL
		BEGIN
			RETURN
		END
		
		--和餐厅获取sales不同，每次dispatch的时候，都会将上次dispatch的最后一个月的数据重新在获取下
		--因为有可能该数据上次不足一个月，而这次补足一个月了
		DECLARE @StartRealSalesDate DATETIME
		
		--从来没有Dispatch过
		IF @MaxSalesDate IS NULL
		BEGIN
			SELECT @StartRealSalesDate = MIN(ZoneStartDate) FROM KioskSalesCollection
			WHERE KioskID=@KioskID AND FromSRLS=1
			
			--如果起始时间不是从1号开始的，将时间设置为从1号开始
			IF DAY(@StartRealSalesDate)<>1
				SET @StartRealSalesDate=STR(YEAR(@StartRealSalesDate))+'-'+STR(MONTH(@StartRealSalesDate))+'-1'			
		END
		--曾经Dispatch过，需要将上次Dispatch的最后一个月update一下，再继续后面的insert
		ELSE
		BEGIN
			--更新@MaxSalesDate的Sales数据
			SELECT @SalesSum = SUM(KC.Sales)
			FROM KioskSalesCollection KC
			WHERE FromSRLS=1 AND KC.KioskID = @KioskID
			AND KC.ZoneStartDate BETWEEN @MaxSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@MaxSalesDate))
		
			SELECT @SalesAdjustmentSum = SUM(K.Sales)
			FROM KioskSalesCollection KC INNER JOIN KioskSales K ON KC.CollectionID = K.CollectionID
			WHERE KC.FromSRLS=1 AND KC.KioskID = @KioskID
			AND KC.ZoneStartDate BETWEEN @MaxSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@MaxSalesDate))
			
			IF @SalesSum IS NULL
				SET @SalesSum=0
			
			UPDATE RLPlanning_RealSales SET 
			Sales=(@SalesSum + CASE WHEN @SalesAdjustmentSum IS NULL THEN 0 ELSE @SalesAdjustmentSum END)
			WHERE KioskNo=@KioskNo AND SalesDate=@MaxSalesDate
			
			--计算Insert的起始时间
			SET @StartRealSalesDate = DATEADD(MONTH,1,@MaxSalesDate)
		END
		
		--真实的sales和realsales表中的最大sales时间一样，则不用dispatch
		IF YEAR(@MaxSalesDate)=YEAR(@MaxRealSalesDate) AND MONTH(@MaxSalesDate)=MONTH(@MaxRealSalesDate)
		BEGIN
			RETURN
		END
		
		BEGIN TRANSACTION MYTRANSACTION --事务开始
		
		WHILE @StartRealSalesDate < @MaxRealSalesDate
		BEGIN		
			SELECT @SalesSum = SUM(KC.Sales)
			FROM KioskSalesCollection KC
			WHERE FromSRLS=1 AND KC.KioskID = @KioskID
			AND KC.ZoneStartDate BETWEEN @StartRealSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@StartRealSalesDate))
		
			SELECT @SalesAdjustmentSum = SUM(K.Sales)
			FROM KioskSalesCollection KC INNER JOIN KioskSales K ON KC.CollectionID = K.CollectionID
			WHERE KC.FromSRLS=1 AND KC.KioskID = @KioskID
			AND KC.ZoneStartDate BETWEEN @StartRealSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@StartRealSalesDate))
			
			IF @SalesSum IS NULL
				SET @SalesSum=0
						
			INSERT INTO RLPlanning_RealSales 
			SELECT @StoreNo, @KioskNo, @StartRealSalesDate, 
			@SalesSum + CASE WHEN @SalesAdjustmentSum IS NULL THEN 0 ELSE @SalesAdjustmentSum END
					
			SET @StartRealSalesDate = DATEADD(MONTH,1,@StartRealSalesDate)
		END
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION MYTRANSACTION
		
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'将Kiosk的真实Sales信息导入到RLPlanning_RealSales表中出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	
		PRINT @MSG
	END CATCH
END








GO


