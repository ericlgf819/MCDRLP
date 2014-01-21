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
	
	--先将没有任何合同关联的GLRecord删除
	EXEC RLPlanning_Cal_DeleteUnnecessaryGLRecord
	
	--将RLPlanning_Cal_TmpTbl的内容放入临时表
	DECLARE @TmpCalTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50), CalEndDate date)
	INSERT INTO @TmpCalTbl SELECT * FROM RLPlanning_Cal_TmpTbl
	
	--放完后将全局变量设置为0
	IF EXISTS(SELECT * FROM RLPlanning_Env WHERE ValName='CalMuxResInUse')
	BEGIN
		UPDATE RLPlanning_Env SET Value='0' WHERE ValName='CalMuxResInUse'
	END
	ELSE
	BEGIN
		INSERT INTO RLPlanning_Env SELECT 'CalMuxResInUse', '0'
	END
	
	--将正在计算的实体信息放入RLPlanning_Cal_EntityInCal中
	INSERT INTO RLPlanning_Cal_EntityInCal SELECT * FROM @TmpCalTbl 
	
	DECLARE @Result int
	DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50), @CalEndDate datetime
	--需要计算的Store和kiosk信息都在@TmpCalTbl表中
	DECLARE @CalItemCursor CURSOR
	SET @CalItemCursor = CURSOR SCROLL FOR 
	SELECT StoreNo, KioskNo, CalEndDate FROM @TmpCalTbl
	OPEN @CalItemCursor
	FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
	WHILE @@FETCH_STATUS=0
	BEGIN
		--将验证相关计算结束时间强制设置到当月的1号
		SET @CalEndDate = CAST(YEAR(@CalEndDate) AS nvarchar(10)) + '-' + CAST(MONTH(@CalEndDate) AS nvarchar(10)) + '-1'
	
		--验证合同合法性
		EXEC RLPlanning_Cal_ValidateContract @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID, @Result OUTPUT
		IF @Result = 1 --有错误直接continue
		BEGIN
			FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
			CONTINUE
		END
		
		EXEC RLPlanning_Cal_ValidateSales @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID, @Result OUTPUT
		IF @Result = 1 --有错误直接continue
		BEGIN
			FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
			CONTINUE
		END

		--将计算结束时间强制设置到月底
		--SET @CalEndDate = CAST(YEAR(@CalEndDate) AS nvarchar(10)) + '-' + CAST(MONTH(@CalEndDate) AS nvarchar(10)) + '-1'
		SET @CalEndDate = DATEADD(DAY,-1,DATEADD(MONTH,1,@CalEndDate))

		--最终计算
		EXEC RLPlanning_Cal_GLByStoreOrKioskNo @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID
		
		FETCH NEXT FROM @CalItemCursor INTO @StoreNo, @KioskNo, @CalEndDate
	END
	CLOSE @CalItemCursor
	DEALLOCATE @CalItemCursor
	
	--计算完后需要将刚才记录在RLPlanning_Cal_EntityInCal中的相关信息删除
	--餐厅
	DELETE FROM RLPlanning_Cal_EntityInCal WHERE ISNULL(KioskNo, '') = ''
	AND ISNULL(StoreNo, '') IN (SELECT StoreNo FROM @TmpCalTbl WHERE ISNULL(KioskNo, '') = '')
	
	--甜品店
	DELETE FROM RLPlanning_Cal_EntityInCal WHERE KioskNo <> ''
	AND KioskNo IN (SELECT KioskNo FROM @TmpCalTbl WHERE KioskNo <> '')
END



GO


