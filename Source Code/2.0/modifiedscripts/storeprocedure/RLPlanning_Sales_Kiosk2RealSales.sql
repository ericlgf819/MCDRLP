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
	    
		--���KioskNo������ֱ�ӷ���
		IF @KioskID IS NULL
		BEGIN
			RETURN
		END
			
		--���û��Sales���ݣ���ֱ�ӷ���
		IF NOT EXISTS (SELECT * FROM KioskSalesCollection WHERE KioskID=@KioskID AND FromSRLS=1)
		BEGIN
			RETURN
		END
			
		--��ȡ�Ѿ�Dispatch����sales���ݵ����ʱ��
		DECLARE @MaxSalesDate DATETIME
		SELECT @MaxSalesDate=MAX(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
		
		--��ȡ����KioskSales�����ݵ����ʱ��
		DECLARE @MaxRealSalesDate DATETIME
		SELECT @MaxRealSalesDate=MAX(ZoneEndDate) FROM KioskSalesCollection
		WHERE KioskID=@KioskID AND FromSRLS=1
		
		--���û����ʵ��sales���ݣ����ü�����
		IF @MaxRealSalesDate IS NULL
		BEGIN
			RETURN
		END
		
		--�Ͳ�����ȡsales��ͬ��ÿ��dispatch��ʱ�򣬶��Ὣ�ϴ�dispatch�����һ���µ����������ڻ�ȡ��
		--��Ϊ�п��ܸ������ϴβ���һ���£�����β���һ������
		DECLARE @StartRealSalesDate DATETIME
		
		--����û��Dispatch��
		IF @MaxSalesDate IS NULL
		BEGIN
			SELECT @StartRealSalesDate = MIN(ZoneStartDate) FROM KioskSalesCollection
			WHERE KioskID=@KioskID AND FromSRLS=1
			
			--�����ʼʱ�䲻�Ǵ�1�ſ�ʼ�ģ���ʱ������Ϊ��1�ſ�ʼ
			IF DAY(@StartRealSalesDate)<>1
				SET @StartRealSalesDate=STR(YEAR(@StartRealSalesDate))+'-'+STR(MONTH(@StartRealSalesDate))+'-1'			
		END
		--����Dispatch������Ҫ���ϴ�Dispatch�����һ����updateһ�£��ټ��������insert
		ELSE
		BEGIN
			--����@MaxSalesDate��Sales����
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
			
			--����Insert����ʼʱ��
			SET @StartRealSalesDate = DATEADD(MONTH,1,@MaxSalesDate)
		END
		
		--��ʵ��sales��realsales���е����salesʱ��һ��������dispatch
		IF YEAR(@MaxSalesDate)=YEAR(@MaxRealSalesDate) AND MONTH(@MaxSalesDate)=MONTH(@MaxRealSalesDate)
		BEGIN
			RETURN
		END
		
		BEGIN TRANSACTION MYTRANSACTION --����ʼ
		
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
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'��Kiosk����ʵSales��Ϣ���뵽RLPlanning_RealSales���г���',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	
		PRINT @MSG
	END CATCH
END








GO


