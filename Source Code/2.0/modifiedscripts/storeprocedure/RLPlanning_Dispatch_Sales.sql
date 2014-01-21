USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Dispatch_Sales]    Script Date: 09/06/2012 16:10:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Dispatch_Sales]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--将Forecast_Sales_ImportPart中的数据保存到本地
	DECLARE @Forecast_Sales_ImportPart 
	TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), SalesDate date, Sales decimal(18,2))
	
	INSERT INTO @Forecast_Sales_ImportPart SELECT * FROM Forecast_Sales_ImportPart
	
	--释放全局资源锁变量
	IF EXISTS(SELECT * FROM RLPlanning_Env WHERE ValName='ImportPartMuxResInUse')
	BEGIN
		UPDATE RLPlanning_Env SET Value='0' WHERE ValName='ImportPartMuxResInUse'
	END
	ELSE
	BEGIN
		INSERT INTO RLPlanning_Env SELECT 'ImportPartMuxResInUse', '0'
	END
		
	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
	
		DECLARE @StoreNo ObjID, @KioskNo ObjID, @SalesDate DATE, @StoreSales DECIMAL(18,2)
		DECLARE @LastDayOfMonth DATETIME
		DECLARE @StoreSalesDate DATETIME
		DECLARE @ExistingSalesSum DECIMAL(18,2)
		--Dispatch Forecast Sales to StoreSales table--Begin
		
		--declare @test int
		--select @test=COUNT(*) from Forecast_Sales_ImportPart WHERE KioskNo IS NULL
		--print '-------------------'
		--print @test
		--set @test=0
		--print '-----------------------'
		
		--Fetch the forecast sales from forecast sales table
		DECLARE @ForecastStoreSalesCursor CURSOR
		
		SET @ForecastStoreSalesCursor = CURSOR SCROLL FOR
		SELECT StoreNo, SalesDate, Sales FROM @Forecast_Sales_ImportPart WHERE KioskNo IS NULL
		OPEN @ForecastStoreSalesCursor
		FETCH NEXT FROM @ForecastStoreSalesCursor INTO @StoreNo, @SalesDate, @StoreSales
		WHILE @@FETCH_STATUS = 0
		BEGIN
			--set @test+=1
			--print @test
			--print @StoreNo
			
			
			DECLARE @DayCount int, @Index int
			DECLARE @DaySales decimal(18,2)
			
			--暂时不需要了，因为已经不做这个检查了
			/*--记录关帐信息，为了消除计算GL时候产生的上月未关帐错误--BEGIN
			--本月
			DECLARE @iYear INT, @iMonth INT
			SET @iYear = YEAR(@SalesDate)
			SET @iMonth = MONTH(@SalesDate)
			EXEC RLPlanning_Sales_InsertVirtualClosedRecord @iYear, @iMonth
			
			--上月
			IF @iMonth = 1
			BEGIN
				SET @iMonth = 12
				SET @iYear -= 1
			END
			ELSE
				SET @iMonth -= 1
				
			EXEC RLPlanning_Sales_InsertVirtualClosedRecord @iYear, @iMonth
			--记录关帐信息，为了消除计算GL时候产生的上月未关帐错误--END*/
			
			--计算Forecast Sales中的月末时间
			SET @LastDayOfMonth = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesDate))
			
			--获取真实Sales数据中，Sales时间最大值
			DECLARE @MaxStoreSalesDate DATETIME
			SELECT @MaxStoreSalesDate = MAX(SalesDate) FROM StoreSales WHERE StoreNo = @StoreNo AND FromSRLS = 1
			
			--1. 真实SalesDate最大值小于当前Forecast Sales Date或者没有真实的Sales数据, 则将完整的Forecast Sales按该月拆分插入--Begin
			IF @MaxStoreSalesDate IS NULL OR @MaxStoreSalesDate < @SalesDate
			BEGIN
				SET @DayCount = DATEDIFF(DAY, @SalesDate, DATEADD(MONTH, 1, @SalesDate))
				SET @DaySales = @StoreSales / @DayCount
				
				--删除重复的预测sales
				DELETE FROM StoreSales WHERE FromSRLS = 0 
				AND YEAR(SalesDate) = YEAR(@SalesDate) 
				AND MONTH(SalesDate) = MONTH(@SalesDate) 
				AND StoreNo = @StoreNo

				SET @Index = 0
				WHILE @Index < @DayCount
				BEGIN
					SET @StoreSalesDate = DATEADD(DAY, @Index, @SalesDate)
					--删除重复的预测sales
					--DELETE FROM StoreSales WHERE FromSRLS = 0 AND SalesDate = @StoreSalesDate AND StoreNo = @StoreNo
					--插入值
					INSERT INTO StoreSales VALUES(NEWID(), @StoreNo, @DaySales, @StoreSalesDate, 1, GETDATE(), 0)
					SET @Index += 1
				END
			END
			--1. 真实SalesDate最大值小于当前Forecast Sales Date, 则将完整的Forecast Sales按该月拆分插入--End
			
			--2. 真实SalesDate最大值大于当前Forecast Sales Date, 但在Forecast Sales Date该月中，则将完整的Forecast Sales按该月拆分减去已有真实Sales的时间值插入--Begin
			IF @MaxStoreSalesDate >= @SalesDate AND @MaxStoreSalesDate < @LastDayOfMonth
			BEGIN
				--将已有真实数据时间段的sales之和计算出来
				SELECT @ExistingSalesSum = SUM(Sales) FROM StoreSales WHERE StoreNo = @StoreNo AND SalesDate BETWEEN @SalesDate AND @MaxStoreSalesDate
				SET @DayCount = DATEDIFF(DAY, @MaxStoreSalesDate, @LastDayOfMonth)
				SET @DaySales = (@StoreSales - @ExistingSalesSum) / @DayCount
				--确保不会减出负数的sales
				IF @DaySales < 0
					SET @DaySales = 0
				
				--删除重复的预测sales
				DELETE FROM StoreSales WHERE FromSRLS = 0 
				AND YEAR(SalesDate) = YEAR(@MaxStoreSalesDate) 
				AND MONTH(SalesDate) = MONTH(@MaxStoreSalesDate) 
				AND StoreNo = @StoreNo
				
				SET @Index = 1
				WHILE @Index <= @DayCount
				BEGIN
					SET @StoreSalesDate = DATEADD(DAY, @Index, @MaxStoreSalesDate)
					--删除重复的预测sales
					--DELETE FROM StoreSales WHERE FromSRLS = 0 AND SalesDate = @StoreSalesDate AND StoreNo = @StoreNo
					INSERT INTO StoreSales VALUES(NEWID(), @StoreNo, @DaySales, @StoreSalesDate, 1, GETDATE(), 0)
					SET @Index += 1
				END
			END
			--2. 真实SalesDate最大值大于当前Forecast Sales Date, 但在Forecast Sales Date该月中，则将完整的Forecast Sales按该月拆分减去已有真实Sales的时间值插入--End
			FETCH NEXT FROM @ForecastStoreSalesCursor INTO @StoreNo, @SalesDate, @StoreSales
		END
		CLOSE @ForecastStoreSalesCursor
		DEALLOCATE @ForecastStoreSalesCursor
		--Dispatch Forecast Sales to StoreSales table--End
		
		--Dispatch Forecast Sales to KioskSalesCollection--Begin

		--Fetch the forecast sales from forecast sales table
		DECLARE @ForecastKioskSalesCursor CURSOR
		DECLARE @KioskID ObjID
		DECLARE @MaxZoneStartDate datetime, @MaxZoneEndDate datetime
		SET @ForecastKioskSalesCursor = CURSOR SCROLL FOR
		SELECT * FROM @Forecast_Sales_ImportPart WHERE KioskNo IS NOT NULL
		OPEN @ForecastKioskSalesCursor
		FETCH NEXT FROM @ForecastKioskSalesCursor INTO @StoreNo, @KioskNo, @SalesDate, @StoreSales
		
		--select @test=COUNT(*) from Forecast_Sales_ImportPart WHERE KioskNo IS NOT NULL
		--print '----------------------kiosk'
		--print @test
		--print '--------------------------------'
		--set @test=0
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			--set @test+=1
			--print @test
			--print @KioskNo
			--根据KioskNo来获取KioskID
			SELECT @KioskID = KioskID FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
			
			--获取最大的KioskSales时间段
			SELECT @MaxZoneStartDate = MAX(ZoneStartDate), @MaxZoneEndDate = MAX(ZoneEndDate) FROM KioskSalesCollection
			WHERE KioskID = @KioskID AND FromSRLS = 1
			
			--计算Forecast Sales中的月末时间
			SET @LastDayOfMonth = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesDate))
			
			--1. 最大时间段比Forecast时间段小或者没有Sales信息，则将Sales值全部插入--Begin
			IF @MaxZoneEndDate IS NULL OR @MaxZoneEndDate < @SalesDate
			BEGIN
				--删除重复时间的预测数据
				DELETE FROM KioskSalesCollection WHERE FromSRLS = 0 AND 
												KioskID = @KioskID AND 
												ZoneStartDate = @SalesDate AND
												ZoneEndDate = @LastDayOfMonth
				--插入预测数据
				INSERT INTO KioskSalesCollection 
				VALUES(NEWID(), @KioskID, NULL, @StoreSales, @SalesDate, @LastDayOfMonth, NULL, GETDATE(), 'RLPlanning', 0)
			END
			--1. 最大时间段比Forecast最小时间点小，则将Sales值全部插入--End
			
			--2. 最大时间段比Forecast最小时间点大但小于Forecast最大时间点, 则将Sales折算值和Date折算值插入--Begin
			IF @MaxZoneEndDate >= @SalesDate AND @MaxZoneEndDate < @LastDayOfMonth
			BEGIN
				DECLARE @MonthDayCount int
				SET @MonthDayCount = DATEDIFF(DAY, @SalesDate, @LastDayOfMonth) + 1
				SET @DayCount = DATEDIFF(DAY, @MaxZoneEndDate, @LastDayOfMonth)
				SET @StoreSales = @StoreSales / @MonthDayCount * @DayCount
				--删除重复时间的预测数据
				SET @StoreSalesDate = DATEADD(DAY, 1, @MaxZoneEndDate)
				DELETE FROM KioskSalesCollection WHERE FromSRLS = 0 AND
												KioskID = @KioskID AND 
												ZoneStartDate = @StoreSalesDate AND
												ZoneEndDate = @LastDayOfMonth
				--插入预测数据
				INSERT INTO KioskSalesCollection 
				VALUES(NEWID(), @KioskID, NULL, @StoreSales, @StoreSalesDate, @LastDayOfMonth, NULL, GETDATE(), 'RLPlanning', 0)
			END
			--2. 最大时间段比Forecast最小时间点大但小于Forecast最大时间点, 则将Sales折算值和Date折算值插入--End
			FETCH NEXT FROM @ForecastKioskSalesCursor INTO @StoreNo, @KioskNo, @SalesDate, @StoreSales
		END
		CLOSE @ForecastKioskSalesCursor
		DEALLOCATE @ForecastKioskSalesCursor
		--Dispatch Forecast Sales to KioskSalesCollection--End
		
		--将计算完的实体信息从RLPlanning_Import_EntityInImport中移除
		--餐厅
		DELETE FROM RLPlanning_Import_EntityInImport 
		WHERE ISNULL(KioskNo,'')='' 
		AND StoreNo 
		IN (SELECT StoreNo FROM @Forecast_Sales_ImportPart WHERE ISNULL(KioskNo,'')='' GROUP BY StoreNo)
		
		--甜品店
		DELETE FROM RLPlanning_Import_EntityInImport 
		WHERE ISNULL(KioskNo,'')<>'' 
		AND ISNULL(KioskNo,'') 
		IN (SELECT KioskNo FROM @Forecast_Sales_ImportPart WHERE ISNULL(KioskNo,'')<>'' GROUP BY KioskNo)
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION	
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION
		
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    print @MSG
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'DispatchSales出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END






GO


