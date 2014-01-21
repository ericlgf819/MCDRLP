USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_ImportGetMinRentStartDateOrOpenningDate]    Script Date: 08/17/2012 14:13:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_ImportGetMinRentStartDateOrOpenningDate]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--��Store��Kiosk�ı�ŷֱ𱣴浽������ʱ���зֿ����� 
		DECLARE @StoreTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), 
								KioskName nvarchar(200),MinDate date)
		
		DECLARE @KioskTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), 
								KioskName nvarchar(200), MinDate date)
		
		INSERT INTO @StoreTbl SELECT �������, NULL, NULL, NULL FROM Import_SalesValidation
		WHERE Kiosk IS NULL OR Kiosk = ''
		
		INSERT INTO @KioskTbl SELECT �������, K.KioskNo, Kiosk, NULL 
		FROM Import_SalesValidation ISV INNER JOIN SRLS_TB_Master_Kiosk K
		ON ISV.Kiosk = K.KioskName
		WHERE ISV.Kiosk IS NOT NULL AND ISV.Kiosk <> ''
		
		DECLARE @MinDate date --��Ų�������Ʒ�꣬sales��Ч����Сʱ��
		--��Store�Ĵ��� --Begin
		DECLARE @StoreNoCursor Cursor, @StoreNo nvarchar(50)
		SET @StoreNoCursor = CURSOR SCROLL FOR
		SELECT StoreNo FROM @StoreTbl
		OPEN @StoreNoCursor
		FETCH NEXT FROM @StoreNoCursor INTO @StoreNo
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @MinDate = NULL;
			
			-- ����ж�Ӧ��ͬ����ֱ��ȡ��ͬ����С���޿�ʼʱ��
			SELECT @MinDate = MIN(E.RentStartDate) FROM Entity E INNER JOIN Contract C
			ON E.ContractSnapshotID = C.ContractSnapshotID 
			WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '') 
			AND C.Status = '����Ч' AND C.SnapshotCreateTime IS NULL
			
			-- ���û�к�ͬ����ȡ��������
			IF @MinDate IS NULL
				SELECT @MinDate = OpenDate FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo
			
			-- �����ݸ��µ���Ӧ�ļ�¼��
			UPDATE @StoreTbl SET MinDate = @MinDate WHERE StoreNo = @StoreNo AND KioskNo IS NULL
			
			FETCH NEXT FROM @StoreNoCursor INTO @StoreNo
		END
		CLOSE @StoreNoCursor
		DEALLOCATE @StoreNoCursor
		--��Store�Ĵ��� --End
		
		--��Kiosk�Ĵ��� --Begin
		DECLARE @KioskNoCursor CURSOR, @KioskNo nvarchar(50)
		SET @KioskNoCursor = CURSOR SCROLL FOR
		SELECT StoreNo, KioskNo FROM @KioskTbl
		OPEN @KioskNoCursor
		FETCH NEXT FROM @KioskNoCursor INTO @StoreNo, @KioskNo
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @MinDate = NULL;
			
			-- ����ж�Ӧ��ͬ����ֱ��ȡ��ͬ����С���޿�ʼʱ��
			SELECT @MinDate = MIN(E.RentStartDate) FROM Entity E INNER JOIN Contract C
			ON E.ContractSnapshotID = C.ContractSnapshotID 
			WHERE E.KioskNo = @KioskNo
			AND C.Status = '����Ч' AND C.SnapshotCreateTime IS NULL
			
			-- ���û�к�ͬ����ȡ��������
			IF @MinDate IS NULL
				SELECT @MinDate = OpenDate FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
				
			-- �����ݸ��µ���Ӧ�ļ�¼��
			UPDATE @KioskTbl SET MinDate = @MinDate WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
			
			FETCH NEXT FROM @KioskNoCursor INTO @StoreNo, @KioskNo
		END
		CLOSE @KioskNoCursor
		DEALLOCATE @KioskNoCursor
		--��Kiosk�Ĵ��� --End
		
		-- ��������ظ��û�
		SELECT * FROM @StoreTbl
		UNION ALL
		SELECT * FROM @KioskTbl
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
	    IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION;
				
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'��ȡ����Sales�����С��ͬʱ�ڳ���',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END




GO


