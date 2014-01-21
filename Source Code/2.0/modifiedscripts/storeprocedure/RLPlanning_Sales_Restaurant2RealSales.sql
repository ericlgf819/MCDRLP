USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_Restaurant2RealSales]    Script Date: 08/10/2012 10:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	不关帐的sales数据不用dispatch过来，所以所有的餐厅Sales数据都是满月的
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_Restaurant2RealSales]
@StoreNo nvarchar(50)
AS
BEGIN
	BEGIN TRY
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;
		
		--不存在的StoreNo不用处理，直接返回
		IF NOT EXISTS(SELECT * FROM SRLS_TB_Master_Store WHERE StoreNo=@StoreNo)
		BEGIN
			RETURN;
		END
			
		--不存在Sales数据，直接返回
		IF NOT EXISTS(SELECT * FROM StoreSales WHERE IsCASHSHEETClosed=1 AND FromSRLS=1)
		BEGIN
			RETURN;
		END
		
		--获取已经Dispatch过来的Sales数据的最大时间
		--MaxSalesDate是RLPlanning_RealSales中的SalesDate，都是代表一个月的，eg.2012-1-1，表示2012年1月的sales
		DECLARE @MaxSalesDate DATETIME, @MaxRealSalesDate DATETIME
		SELECT @MaxSalesDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo='')
		
		SELECT @MaxRealSalesDate = MAX(SalesDate)
		FROM StoreSales WHERE StoreNo=@StoreNo AND IsCASHSHEETClosed=1 AND FromSRLS=1
		
		--计算要开始dispatch的真实sales数据的起始时间点--Begin
		DECLARE @StartRealSalesDate DATETIME
		IF @MaxSalesDate IS NULL
		BEGIN
			SELECT @StartRealSalesDate = MIN(SalesDate)
			FROM StoreSales WHERE StoreNo=@StoreNo AND IsCASHSHEETClosed=1 AND FromSRLS=1
			--如果数据的SalesDate不是从1号开始的，把时间强制性修改为xxxx-x-1的形式
			IF DAY(@StartRealSalesDate)<>1
				SET @StartRealSalesDate=STR(YEAR(@StartRealSalesDate))+'-'+STR(MONTH(@StartRealSalesDate))+'-1'
		END
		ELSE
		BEGIN
			SET @StartRealSalesDate=DATEADD(MONTH,1,@MaxSalesDate) --肯定是从1号开始的，因为@MaxSalesDate是xxxx-x-1的形式
		END
		--计算要开始dispatch的真实sales数据的起始时间点--End
		
		--如果@StartRealSalesDate为空，说明没有真实sales数据，不用dispatch
		IF @StartRealSalesDate IS NULL
		BEGIN
			RETURN;
		END
		
		--如果已经dispatch的sales数据的最大年月与系统中真实已关帐sales的最大年月相同，说明已经dispatch过了
		IF YEAR(@MaxSalesDate)=YEAR(@MaxRealSalesDate) AND MONTH(@MaxSalesDate)=MONTH(@MaxRealSalesDate)
		BEGIN
			RETURN;
		END
		
		--将日sales的数据按月相加存储--Begin
		BEGIN TRANSACTION MYTRANSACTION --事务开始
		DECLARE @SumSales DECIMAL(18,2)
		WHILE @StartRealSalesDate < @MaxRealSalesDate
		BEGIN	
			SELECT @SumSales = SUM(Sales)
			FROM StoreSales 
			WHERE SalesDate BETWEEN @StartRealSalesDate AND DATEADD(DAY, -1, DATEADD(MONTH,1,@StartRealSalesDate))
			AND StoreNo=@StoreNo
			AND FromSRLS=1
			AND IsCASHSHEETClosed=1
			
			--放置真实Sales数据有不连续的情况发生
			IF @SumSales IS NULL
				SET @SumSales = 0
			
			INSERT INTO RLPlanning_RealSales
			SELECT @StoreNo, NULL, @StartRealSalesDate, @SumSales
			
			SET @StartRealSalesDate = DATEADD(MONTH,1,@StartRealSalesDate)
		END
		--将日sales的数据按月相加存储--End
		
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
		VALUES (NEWID(),'将餐厅的真实Sales信息导入到RLPlanning_RealSales表中出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
		
		DECLARE @ERRLine nvarchar(max)
		SELECT @ERRLine=ERROR_LINE()
		PRINT @MSG + ' line:' + @ERRLine + ' StoreNo: ' + @StoreNo
	END CATCH
END











GO


