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
		--将Store和Kiosk的编号分别保存到两张临时表中分开操作 
		DECLARE @StoreTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), 
								KioskName nvarchar(200),MinDate date)
		
		DECLARE @KioskTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), 
								KioskName nvarchar(200), MinDate date)
		
		INSERT INTO @StoreTbl SELECT 餐厅编号, NULL, NULL, NULL FROM Import_SalesValidation
		WHERE Kiosk IS NULL OR Kiosk = ''
		
		INSERT INTO @KioskTbl SELECT 餐厅编号, K.KioskNo, Kiosk, NULL 
		FROM Import_SalesValidation ISV INNER JOIN SRLS_TB_Master_Kiosk K
		ON ISV.Kiosk = K.KioskName
		WHERE ISV.Kiosk IS NOT NULL AND ISV.Kiosk <> ''
		
		DECLARE @MinDate date --存放餐厅或甜品店，sales有效的最小时间
		--对Store的处理 --Begin
		DECLARE @StoreNoCursor Cursor, @StoreNo nvarchar(50)
		SET @StoreNoCursor = CURSOR SCROLL FOR
		SELECT StoreNo FROM @StoreTbl
		OPEN @StoreNoCursor
		FETCH NEXT FROM @StoreNoCursor INTO @StoreNo
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @MinDate = NULL;
			
			-- 如果有对应合同，则直接取合同的最小租赁开始时间
			SELECT @MinDate = MIN(E.RentStartDate) FROM Entity E INNER JOIN Contract C
			ON E.ContractSnapshotID = C.ContractSnapshotID 
			WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '') 
			AND C.Status = '已生效' AND C.SnapshotCreateTime IS NULL
			
			-- 如果没有合同，则取开店日期
			IF @MinDate IS NULL
				SELECT @MinDate = OpenDate FROM SRLS_TB_Master_Store WHERE StoreNo = @StoreNo
			
			-- 将数据更新到对应的记录中
			UPDATE @StoreTbl SET MinDate = @MinDate WHERE StoreNo = @StoreNo AND KioskNo IS NULL
			
			FETCH NEXT FROM @StoreNoCursor INTO @StoreNo
		END
		CLOSE @StoreNoCursor
		DEALLOCATE @StoreNoCursor
		--对Store的处理 --End
		
		--对Kiosk的处理 --Begin
		DECLARE @KioskNoCursor CURSOR, @KioskNo nvarchar(50)
		SET @KioskNoCursor = CURSOR SCROLL FOR
		SELECT StoreNo, KioskNo FROM @KioskTbl
		OPEN @KioskNoCursor
		FETCH NEXT FROM @KioskNoCursor INTO @StoreNo, @KioskNo
		WHILE @@FETCH_STATUS=0
		BEGIN
			SET @MinDate = NULL;
			
			-- 如果有对应合同，则直接取合同的最小租赁开始时间
			SELECT @MinDate = MIN(E.RentStartDate) FROM Entity E INNER JOIN Contract C
			ON E.ContractSnapshotID = C.ContractSnapshotID 
			WHERE E.KioskNo = @KioskNo
			AND C.Status = '已生效' AND C.SnapshotCreateTime IS NULL
			
			-- 如果没有合同，则取开店日期
			IF @MinDate IS NULL
				SELECT @MinDate = OpenDate FROM SRLS_TB_Master_Kiosk WHERE KioskNo = @KioskNo
				
			-- 将数据更新到对应的记录中
			UPDATE @KioskTbl SET MinDate = @MinDate WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
			
			FETCH NEXT FROM @KioskNoCursor INTO @StoreNo, @KioskNo
		END
		CLOSE @KioskNoCursor
		DEALLOCATE @KioskNoCursor
		--对Kiosk的处理 --End
		
		-- 将结果返回给用户
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
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'获取导入Sales店的最小合同时期出错',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END




GO


