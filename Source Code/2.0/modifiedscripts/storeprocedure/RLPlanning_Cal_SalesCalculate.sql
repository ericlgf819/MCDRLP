USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_SalesCalculate]    Script Date: 09/03/2012 15:36:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_SalesCalculate]
@OperatorUserID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--�Ƚ�û���κκ�ͬ������GLRecordɾ��
	EXEC RLPlanning_Cal_DeleteUnnecessaryGLRecord
	
	--��RLPlanning_Cal_TmpTbl�����ݷ�����ʱ��
	DECLARE @TmpCalTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), CalEndDate date)
	INSERT INTO @TmpCalTbl SELECT * FROM RLPlanning_Cal_TmpTbl
	
	--�����ȫ�ֱ�������Ϊ0
	IF EXISTS(SELECT * FROM RLPlanning_Env WHERE ValName='CalMuxResInUse')
	BEGIN
		UPDATE RLPlanning_Env SET Value='0' WHERE ValName='CalMuxResInUse'
	END
	ELSE
	BEGIN
		INSERT INTO RLPlanning_Env SELECT 'CalMuxResInUse', '0'
	END
	
	--�����ڼ����ʵ����Ϣ����RLPlanning_Cal_EntityInCal��
	INSERT INTO RLPlanning_Cal_EntityInCal SELECT * FROM @TmpCalTbl 
	
	DECLARE @Result int
	DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50), @CalEndDate datetime
	--��Ҫ�����Store��kiosk��Ϣ����@TmpCalTbl����
	DECLARE @CalItemCursor CURSOR
	SET @CalItemCursor = CURSOR SCROLL FOR 
	SELECT StoreNo, KioskNo, CalEndDate FROM @TmpCalTbl
	OPEN @CalItemCursor
	FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
	WHILE @@FETCH_STATUS=0
	BEGIN
		--����֤��ؼ������ʱ��ǿ�����õ����µ�1��
		SET @CalEndDate = CAST(YEAR(@CalEndDate) AS nvarchar(10)) + '-' + CAST(MONTH(@CalEndDate) AS nvarchar(10)) + '-1'
	
		--��֤��ͬ�Ϸ���
		EXEC RLPlanning_Cal_ValidateContract @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID, @Result OUTPUT
		IF @Result = 1 --�д���ֱ��continue
		BEGIN
			FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
			CONTINUE
		END
		
		EXEC RLPlanning_Cal_ValidateSales @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID, @Result OUTPUT
		IF @Result = 1 --�д���ֱ��continue
		BEGIN
			FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
			CONTINUE
		END

		--���������ʱ��ǿ�����õ��µ�
		--SET @CalEndDate = CAST(YEAR(@CalEndDate) AS nvarchar(10)) + '-' + CAST(MONTH(@CalEndDate) AS nvarchar(10)) + '-1'
		SET @CalEndDate = DATEADD(DAY,-1,DATEADD(MONTH,1,@CalEndDate))

		--���ռ���
		EXEC RLPlanning_Cal_GLByStoreOrKioskNo @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID
		
		FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
	END
	CLOSE @CalItemCursor
	DEALLOCATE @CalItemCursor
	
	--���������Ҫ���ղż�¼��RLPlanning_Cal_EntityInCal�е������Ϣɾ��
	--����
	DELETE FROM RLPlanning_Cal_EntityInCal WHERE ISNULL(KioskNo, '') = ''
	AND ISNULL(StoreNo, '') IN (SELECT StoreNo FROM @TmpCalTbl WHERE ISNULL(KioskNo, '') = '')
	
	--��Ʒ��
	DELETE FROM RLPlanning_Cal_EntityInCal WHERE KioskNo <> ''
	AND KioskNo IN (SELECT KioskNo FROM @TmpCalTbl WHERE KioskNo <> '')
END



GO


