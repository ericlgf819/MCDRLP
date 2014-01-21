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
-- Description:	�����ʵ�sales���ݲ���dispatch�������������еĲ���Sales���ݶ������µ�
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_Restaurant2RealSales]
@StoreNo nvarchar(50)
AS
BEGIN
	BEGIN TRY
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;
		
		--�����ڵ�StoreNo���ô���ֱ�ӷ���
		IF NOT EXISTS(SELECT * FROM SRLS_TB_Master_Store WHERE StoreNo=@StoreNo)
		BEGIN
			RETURN;
		END
			
		--������Sales���ݣ�ֱ�ӷ���
		IF NOT EXISTS(SELECT * FROM StoreSales WHERE IsCASHSHEETClosed=1 AND FromSRLS=1)
		BEGIN
			RETURN;
		END
		
		--��ȡ�Ѿ�Dispatch������Sales���ݵ����ʱ��
		--MaxSalesDate��RLPlanning_RealSales�е�SalesDate�����Ǵ���һ���µģ�eg.2012-1-1����ʾ2012��1�µ�sales
		DECLARE @MaxSalesDate DATETIME, @MaxRealSalesDate DATETIME
		SELECT @MaxSalesDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo='')
		
		SELECT @MaxRealSalesDate = MAX(SalesDate)
		FROM StoreSales WHERE StoreNo=@StoreNo AND IsCASHSHEETClosed=1 AND FromSRLS=1
		
		--����Ҫ��ʼdispatch����ʵsales���ݵ���ʼʱ���--Begin
		DECLARE @StartRealSalesDate DATETIME
		IF @MaxSalesDate IS NULL
		BEGIN
			SELECT @StartRealSalesDate = MIN(SalesDate)
			FROM StoreSales WHERE StoreNo=@StoreNo AND IsCASHSHEETClosed=1 AND FromSRLS=1
			--������ݵ�SalesDate���Ǵ�1�ſ�ʼ�ģ���ʱ��ǿ�����޸�Ϊxxxx-x-1����ʽ
			IF DAY(@StartRealSalesDate)<>1
				SET @StartRealSalesDate=STR(YEAR(@StartRealSalesDate))+'-'+STR(MONTH(@StartRealSalesDate))+'-1'
		END
		ELSE
		BEGIN
			SET @StartRealSalesDate=DATEADD(MONTH,1,@MaxSalesDate) --�϶��Ǵ�1�ſ�ʼ�ģ���Ϊ@MaxSalesDate��xxxx-x-1����ʽ
		END
		--����Ҫ��ʼdispatch����ʵsales���ݵ���ʼʱ���--End
		
		--���@StartRealSalesDateΪ�գ�˵��û����ʵsales���ݣ�����dispatch
		IF @StartRealSalesDate IS NULL
		BEGIN
			RETURN;
		END
		
		--����Ѿ�dispatch��sales���ݵ����������ϵͳ����ʵ�ѹ���sales�����������ͬ��˵���Ѿ�dispatch����
		IF YEAR(@MaxSalesDate)=YEAR(@MaxRealSalesDate) AND MONTH(@MaxSalesDate)=MONTH(@MaxRealSalesDate)
		BEGIN
			RETURN;
		END
		
		--����sales�����ݰ�����Ӵ洢--Begin
		BEGIN TRANSACTION MYTRANSACTION --����ʼ
		DECLARE @SumSales DECIMAL(18,2)
		WHILE @StartRealSalesDate < @MaxRealSalesDate
		BEGIN	
			SELECT @SumSales = SUM(Sales)
			FROM StoreSales 
			WHERE SalesDate BETWEEN @StartRealSalesDate AND DATEADD(DAY, -1, DATEADD(MONTH,1,@StartRealSalesDate))
			AND StoreNo=@StoreNo
			AND FromSRLS=1
			AND IsCASHSHEETClosed=1
			
			--������ʵSales�����в��������������
			IF @SumSales IS NULL
				SET @SumSales = 0
			
			INSERT INTO RLPlanning_RealSales
			SELECT @StoreNo, NULL, @StartRealSalesDate, @SumSales
			
			SET @StartRealSalesDate = DATEADD(MONTH,1,@StartRealSalesDate)
		END
		--����sales�����ݰ�����Ӵ洢--End
		
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
		VALUES (NEWID(),'����������ʵSales��Ϣ���뵽RLPlanning_RealSales���г���',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
		
		DECLARE @ERRLine nvarchar(max)
		SELECT @ERRLine=ERROR_LINE()
		PRINT @MSG + ' line:' + @ERRLine + ' StoreNo: ' + @StoreNo
	END CATCH
END











GO


