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

	--��Forecast_Sales_ImportPart�е����ݱ��浽����
	DECLARE @Forecast_Sales_ImportPart 
	TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), SalesDate date, Sales decimal(18,2))
	
	INSERT INTO @Forecast_Sales_ImportPart SELECT * FROM Forecast_Sales_ImportPart
	
	--�ͷ�ȫ����Դ������
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
			
			--��ʱ����Ҫ�ˣ���Ϊ�Ѿ�������������
			/*--��¼������Ϣ��Ϊ����������GLʱ�����������δ���ʴ���--BEGIN
			--����
			DECLARE @iYear INT, @iMonth INT
			SET @iYear = YEAR(@SalesDate)
			SET @iMonth = MONTH(@SalesDate)
			EXEC RLPlanning_Sales_InsertVirtualClosedRecord @iYear, @iMonth
			
			--����
			IF @iMonth = 1
			BEGIN
				SET @iMonth = 12
				SET @iYear -= 1
			END
			ELSE
				SET @iMonth -= 1
				
			EXEC RLPlanning_Sales_InsertVirtualClosedRecord @iYear, @iMonth
			--��¼������Ϣ��Ϊ����������GLʱ�����������δ���ʴ���--END*/
			
			--����Forecast Sales�е���ĩʱ��
			SET @LastDayOfMonth = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesDate))
			
			--��ȡ��ʵSales�����У�Salesʱ�����ֵ
			DECLARE @MaxStoreSalesDate DATETIME
			SELECT @MaxStoreSalesDate = MAX(SalesDate) FROM StoreSales WHERE StoreNo = @StoreNo AND FromSRLS = 1
			
			--1. ��ʵSalesDate���ֵС�ڵ�ǰForecast Sales Date����û����ʵ��Sales����, ��������Forecast Sales�����²�ֲ���--Begin
			IF @MaxStoreSalesDate IS NULL OR @MaxStoreSalesDate < @SalesDate
			BEGIN
				SET @DayCount = DATEDIFF(DAY, @SalesDate, DATEADD(MONTH, 1, @SalesDate))
				SET @DaySales = @StoreSales / @DayCount
				
				--ɾ���ظ���Ԥ��sales
				DELETE FROM StoreSales WHERE FromSRLS = 0 
				AND YEAR(SalesDate) = YEAR(@SalesDate) 
				AND MONTH(SalesDate) = MONTH(@SalesDate) 
				AND StoreNo = @StoreNo

				SET @Index = 0
				WHILE @Index < @DayCount
				BEGIN
					SET @StoreSalesDate = DATEADD(DAY, @Index, @SalesDate)
					--ɾ���ظ���Ԥ��sales
					--DELETE FROM StoreSales WHERE FromSRLS = 0 AND SalesDate = @StoreSalesDate AND StoreNo = @StoreNo
					--����ֵ
					INSERT INTO StoreSales VALUES(NEWID(), @StoreNo, @DaySales, @StoreSalesDate, 1, GETDATE(), 0)
					SET @Index += 1
				END
			END
			--1. ��ʵSalesDate���ֵС�ڵ�ǰForecast Sales Date, ��������Forecast Sales�����²�ֲ���--End
			
			--2. ��ʵSalesDate���ֵ���ڵ�ǰForecast Sales Date, ����Forecast Sales Date�����У���������Forecast Sales�����²�ּ�ȥ������ʵSales��ʱ��ֵ����--Begin
			IF @MaxStoreSalesDate >= @SalesDate AND @MaxStoreSalesDate < @LastDayOfMonth
			BEGIN
				--��������ʵ����ʱ��ε�sales֮�ͼ������
				SELECT @ExistingSalesSum = SUM(Sales) FROM StoreSales WHERE StoreNo = @StoreNo AND SalesDate BETWEEN @SalesDate AND @MaxStoreSalesDate
				SET @DayCount = DATEDIFF(DAY, @MaxStoreSalesDate, @LastDayOfMonth)
				SET @DaySales = (@StoreSales - @ExistingSalesSum) / @DayCount
				--ȷ���������������sales
				IF @DaySales < 0
					SET @DaySales = 0
				
				--ɾ���ظ���Ԥ��sales
				DELETE FROM StoreSales WHERE FromSRLS = 0 
				AND YEAR(SalesDate) = YEAR(@MaxStoreSalesDate) 
				AND MONTH(SalesDate) = MONTH(@MaxStoreSalesDate) 
				AND StoreNo = @StoreNo
				
				SET @Index = 1
				WHILE @Index <= @DayCount
				BEGIN
					SET @StoreSalesDate = DATEADD(DAY, @Index, @MaxStoreSalesDate)
					--ɾ���ظ���Ԥ��sales
					--DELETE FROM StoreSales WHERE FromSRLS = 0 AND SalesDate = @StoreSalesDate AND StoreNo = @StoreNo
					INSERT INTO StoreSales VALUES(NEWID(), @StoreNo, @DaySales, @StoreSalesDate, 1, GETDATE(), 0)
					SET @Index += 1
				END
			END
			--2. ��ʵSalesDate���ֵ���ڵ�ǰForecast Sales Date, ����Forecast Sales Date�����У���������Forecast Sales�����²�ּ�ȥ������ʵSales��ʱ��ֵ����--End
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
			--����KioskNo����ȡKioskID
			SELECT @KioskID = KioskID FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
			
			--��ȡ����KioskSalesʱ���
			SELECT @MaxZoneStartDate = MAX(ZoneStartDate), @MaxZoneEndDate = MAX(ZoneEndDate) FROM KioskSalesCollection
			WHERE KioskID = @KioskID AND FromSRLS = 1
			
			--����Forecast Sales�е���ĩʱ��
			SET @LastDayOfMonth = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesDate))
			
			--1. ���ʱ��α�Forecastʱ���С����û��Sales��Ϣ����Salesֵȫ������--Begin
			IF @MaxZoneEndDate IS NULL OR @MaxZoneEndDate < @SalesDate
			BEGIN
				--ɾ���ظ�ʱ���Ԥ������
				DELETE FROM KioskSalesCollection WHERE FromSRLS = 0 AND 
												KioskID = @KioskID AND 
												ZoneStartDate = @SalesDate AND
												ZoneEndDate = @LastDayOfMonth
				--����Ԥ������
				INSERT INTO KioskSalesCollection 
				VALUES(NEWID(), @KioskID, NULL, @StoreSales, @SalesDate, @LastDayOfMonth, NULL, GETDATE(), 'RLPlanning', 0)
			END
			--1. ���ʱ��α�Forecast��Сʱ���С����Salesֵȫ������--End
			
			--2. ���ʱ��α�Forecast��Сʱ����С��Forecast���ʱ���, ��Sales����ֵ��Date����ֵ����--Begin
			IF @MaxZoneEndDate >= @SalesDate AND @MaxZoneEndDate < @LastDayOfMonth
			BEGIN
				DECLARE @MonthDayCount int
				SET @MonthDayCount = DATEDIFF(DAY, @SalesDate, @LastDayOfMonth) + 1
				SET @DayCount = DATEDIFF(DAY, @MaxZoneEndDate, @LastDayOfMonth)
				SET @StoreSales = @StoreSales / @MonthDayCount * @DayCount
				--ɾ���ظ�ʱ���Ԥ������
				SET @StoreSalesDate = DATEADD(DAY, 1, @MaxZoneEndDate)
				DELETE FROM KioskSalesCollection WHERE FromSRLS = 0 AND
												KioskID = @KioskID AND 
												ZoneStartDate = @StoreSalesDate AND
												ZoneEndDate = @LastDayOfMonth
				--����Ԥ������
				INSERT INTO KioskSalesCollection 
				VALUES(NEWID(), @KioskID, NULL, @StoreSales, @StoreSalesDate, @LastDayOfMonth, NULL, GETDATE(), 'RLPlanning', 0)
			END
			--2. ���ʱ��α�Forecast��Сʱ����С��Forecast���ʱ���, ��Sales����ֵ��Date����ֵ����--End
			FETCH NEXT FROM @ForecastKioskSalesCursor INTO @StoreNo, @KioskNo, @SalesDate, @StoreSales
		END
		CLOSE @ForecastKioskSalesCursor
		DEALLOCATE @ForecastKioskSalesCursor
		--Dispatch Forecast Sales to KioskSalesCollection--End
		
		--���������ʵ����Ϣ��RLPlanning_Import_EntityInImport���Ƴ�
		--����
		DELETE FROM RLPlanning_Import_EntityInImport 
		WHERE ISNULL(KioskNo,'')='' 
		AND StoreNo 
		IN (SELECT StoreNo FROM @Forecast_Sales_ImportPart WHERE ISNULL(KioskNo,'')='' GROUP BY StoreNo)
		
		--��Ʒ��
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
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'DispatchSales����',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END






GO


