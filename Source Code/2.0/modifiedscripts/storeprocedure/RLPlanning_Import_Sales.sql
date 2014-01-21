USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Import_Sales]    Script Date: 09/07/2012 11:23:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Import_Sales] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--��Import_Sales��������ݸ��Ƶ�SP��
	DECLARE @Import_Sales TABLE([Company] [nvarchar](50) NULL,
	[�������] [nvarchar](50) NULL,
	[Store] [nvarchar](500) NULL,
	[Kiosk] [nvarchar](500) NULL,
	[���] [nvarchar](50) NULL,
	[1��] [nvarchar](500) NULL,
	[2��] [nvarchar](500) NULL,
	[3��] [nvarchar](500) NULL,
	[4��] [nvarchar](500) NULL,
	[5��] [nvarchar](500) NULL,
	[6��] [nvarchar](500) NULL,
	[7��] [nvarchar](500) NULL,
	[8��] [nvarchar](500) NULL,
	[9��] [nvarchar](500) NULL,
	[10��] [nvarchar](500) NULL,
	[11��] [nvarchar](500) NULL,
	[12��] [nvarchar](500) NULL)
	
	INSERT INTO @Import_Sales SELECT * FROM Import_Sales
	
	--�����ڵ���sales��ʵ����Ϣ��¼����
	INSERT INTO RLPlanning_Import_EntityInImport 
	SELECT [�������], K.KioskNo FROM @Import_Sales I LEFT JOIN SRLS_TB_Master_Kiosk K
	ON ISNULL(I.Kiosk,'') = K.KioskName
	
	--��ȫ�ֻ����־��0
	IF EXISTS(SELECT * FROM RLPlanning_Env WHERE ValName='ImportMuxResInUse')
	BEGIN
		UPDATE RLPlanning_Env SET Value='0' WHERE ValName='ImportMuxResInUse'
	END
	ELSE
	BEGIN
		INSERT INTO RLPlanning_Env SELECT 'ImportMuxResInUse', '0'
	END
		
	--BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--��¼������Ϣ
		DECLARE @Import_SalesMessage TABLE(CheckMessage nvarchar(1000))
		
		--��¼������Ϣ
		DECLARE @Forecast_Sales_ImportPart 
		TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), SalesDate date, Sales decimal(18,2))
		
		--������Ϣ��صı���
		DECLARE @ErrStoreNo nvarchar(50), @LastErrStoreNo nvarchar(50)
		DECLARE @ErrKioskName nvarchar(500), @LastErrKioskName nvarchar(500)
		
		DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50);
		DECLARE @CompanyCode nvarchar(50);
		DECLARE @StoreName nvarchar(500), @KioskName nvarchar(500);
		DECLARE @Year nvarchar(50);
		DECLARE @Month1_Sales nvarchar(500);
		DECLARE @Month2_Sales nvarchar(500);
		DECLARE @Month3_Sales nvarchar(500);
		DECLARE @Month4_Sales nvarchar(500);
		DECLARE @Month5_Sales nvarchar(500);
		DECLARE @Month6_Sales nvarchar(500);
		DECLARE @Month7_Sales nvarchar(500);
		DECLARE @Month8_Sales nvarchar(500);
		DECLARE @Month9_Sales nvarchar(500);
		DECLARE @Month10_Sales nvarchar(500);
		DECLARE @Month11_Sales nvarchar(500);
		DECLARE @Month12_Sales nvarchar(500);
		DECLARE @SalesDate date;
		DECLARE @cursor0 CURSOR;
		SET @cursor0 = CURSOR SCROLL FOR SELECT * FROM @Import_Sales;
		OPEN @cursor0;
		FETCH NEXT FROM @cursor0 INTO @CompanyCode, @StoreNo, @StoreName, @KioskName, @Year, @Month1_Sales, 
		@Month2_Sales, @Month3_Sales, @Month4_Sales, @Month5_Sales, @Month6_Sales, @Month7_Sales, @Month8_Sales, 
		@Month9_Sales, @Month10_Sales, @Month11_Sales, @Month12_Sales

		WHILE @@FETCH_STATUS = 0
		BEGIN		
			--��''��KioskName,����Ϊnull
			IF @KioskName = ''
			BEGIN
				SET @KioskName = NULL
			END
			
			--����KioskName��ȡKioskNo
			SET @KioskNo = NULL
			SELECT @KioskNo = KioskNo FROM SRLS_TB_Master_Kiosk WHERE KioskName = @KioskName
			
			--�ж�Kiosk�Ƿ����
			IF ISNULL(@KioskName,'')<>''
			AND NOT EXISTS(SELECT * FROM SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo)
			BEGIN
				SET @ErrKioskName = @KioskName
				IF(@LastErrKioskName IS NULL OR @ErrKioskName <> @LastErrKioskName)
				BEGIN
					SET @LastErrKioskName = @ErrKioskName
					print @StoreNo
					print @KioskNo
					print @KioskName
					INSERT INTO @Import_SalesMessage Values('�����ڻ�����Ч��KioskName: ' + ISNULL(@ErrKioskName,''))
				END
				FETCH NEXT FROM @cursor0 INTO @CompanyCode, @StoreNo, @StoreName, @KioskName, @Year, @Month1_Sales, 
				@Month2_Sales, @Month3_Sales, @Month4_Sales, @Month5_Sales, @Month6_Sales, @Month7_Sales, @Month8_Sales, 
				@Month9_Sales, @Month10_Sales, @Month11_Sales, @Month12_Sales
				CONTINUE;
			END

			--�ж�StoreNo�Ƿ����
			IF NOT EXISTS(SELECT * FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo)
			AND ISNULL(@KioskName,'')=''
			BEGIN
				SET @ErrStoreNo = @StoreNo
				IF(@LastErrStoreNo IS NULL OR @ErrStoreNo <> @LastErrStoreNo)
				BEGIN
					SET @LastErrStoreNo = @ErrStoreNo
					INSERT INTO @Import_SalesMessage Values('�����ڻ�����Ч��StoreNo: ' + @ErrStoreNo)
				END
				FETCH NEXT FROM @cursor0 INTO @CompanyCode, @StoreNo, @StoreName, @KioskName, @Year, @Month1_Sales, 
				@Month2_Sales, @Month3_Sales, @Month4_Sales, @Month5_Sales, @Month6_Sales, @Month7_Sales, @Month8_Sales, 
				@Month9_Sales, @Month10_Sales, @Month11_Sales, @Month12_Sales
				CONTINUE;
			END
			
			--��ƴ��ʱ��Σ��ٰ�ԭ�е����ݸ�ɾ����������
			IF @Month1_Sales IS NOT NULL AND @Month1_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-1-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;
				
				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month1_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month1_Sales)
			END

			IF @Month2_Sales IS NOT NULL AND @Month2_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-2-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month2_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month2_Sales)
			END
			
			IF @Month3_Sales IS NOT NULL AND @Month3_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-3-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month3_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month3_Sales)
			END
			
			IF @Month4_Sales IS NOT NULL AND @Month4_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-4-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month4_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month4_Sales)
			END
			
			IF @Month5_Sales IS NOT NULL AND @Month5_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-5-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month5_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month5_Sales)
			END
			
			IF @Month6_Sales IS NOT NULL AND @Month6_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-6-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month6_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month6_Sales)
			END
			
			IF @Month7_Sales IS NOT NULL AND @Month7_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-7-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month7_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month7_Sales)
			END
			
			IF @Month8_Sales IS NOT NULL AND @Month8_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-8-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month8_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month8_Sales)
			END
			
			IF @Month9_Sales IS NOT NULL AND @Month9_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-9-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month9_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month9_Sales)
			END
			
			IF @Month10_Sales IS NOT NULL AND @Month10_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-10-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month10_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month10_Sales)
			END
			
			IF @Month11_Sales IS NOT NULL AND @Month11_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-11-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month11_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month11_Sales)
			END
			
			IF @Month12_Sales IS NOT NULL AND @Month12_Sales<>''
			BEGIN
				SET @SalesDate = @Year + '-12-1'
				IF @KioskNo IS NULL
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo IS NULL;
				ELSE
					DELETE FROM Forecast_Sales WHERE SalesDate = @SalesDate AND StoreNo = @StoreNo AND KioskNo = @KioskNo;

				INSERT INTO Forecast_Sales VALUES(@StoreNo, @KioskNo, @SalesDate, @Month12_Sales)
				INSERT INTO @Forecast_Sales_ImportPart VALUES(@StoreNo, @KioskNo, @SalesDate, @Month12_Sales)
			END
			
			FETCH NEXT FROM @cursor0 INTO @CompanyCode, @StoreNo, @StoreName, @KioskName, @Year, @Month1_Sales, 
			@Month2_Sales, @Month3_Sales, @Month4_Sales, @Month5_Sales, @Month6_Sales, @Month7_Sales, @Month8_Sales, 
			@Month9_Sales, @Month10_Sales, @Month11_Sales, @Month12_Sales
		END
		CLOSE @cursor0;
		DEALLOCATE @cursor0;
		
		--��ʾ������Ϣ
		SELECT * FROM @Import_SalesMessage
		
		--�д�����Ϣ����ֱ�ӻع�����
		IF EXISTS(SELECT * FROM @Import_SalesMessage)
		BEGIN
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

			--RAISERROR('����Sales�������ݻع�',16,1)
		END
		--���򣬽�����Dispatch��sales��ر���ȥ
		ELSE
		BEGIN
			--��������Ҫ�����Sales��Ϣ���·��ظ��ͻ���
			SELECT * FROM @Forecast_Sales_ImportPart
		END
		
		--IF @@TRANCOUNT <> 0
		--	COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
		--IF @@TRANCOUNT <> 0
		--	ROLLBACK TRANSACTION
		
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    print @MSG
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'����Sales����',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
	
END






GO


