USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_ValidateContract]    Script Date: 08/17/2012 12:49:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By 
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE PROCEDURE [dbo].[RLPlanning_Cal_ValidateContract] 
@StoreNo nvarchar(50),
@KioskNo nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier,
@Result int output --0为无错误，1为有错误
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--清理工作不需要了，这部分清理代码放在RLPlanning_Cal_CheckContractAndGenInfo里
	--清理工作--Begin
	--将历史版本的合同且IsSolve=0的任务项给删除
	--DELETE FROM Forecast_CheckResult
	--WHERE IsSolved = 0 AND TaskType=1
	--AND ContractSnapShotID IN (SELECT ContractSnapShotID FROM Contract WHERE SnapshotCreateTime IS NOT NULL)
	--清理工作--End
	
	DECLARE @CurDate datetime
	DECLARE @NeedInsert bit
	SET @CurDate = GETDATE()
	SET @Result = 0
	
	--检查是甜品店还是餐厅
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0	--餐厅
	ELSE
	BEGIN
		SET @IsKiosk = 1	--甜品店
		
		--获取甜品店当前挂靠的餐厅no
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND @CurDate BETWEEN StartDate AND EndDate
	END

	DECLARE @ContractTbl TABLE(ContractSnapshotID nvarchar(50), ContractID nvarchar(50))
	--1.检查餐厅或甜品站是否与有效合同相关联--Begin
	--餐厅
	IF @IsKiosk=0
	BEGIN
		INSERT INTO @ContractTbl
		SELECT C.ContractSnapshotID, C.ContractID 
		FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '')
		AND C.SnapshotCreateTime IS NULL AND C.Status='已生效'
	END
	--甜品店
	ELSE
	BEGIN
		INSERT INTO @ContractTbl
		SELECT C.ContractSnapshotID, C.ContractID 
		FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE E.KioskNo = @KioskNo
		AND C.SnapshotCreateTime IS NULL AND C.Status='已生效'
	END
	
	--餐厅或者甜品店没有生效的合同存在
	IF NOT EXISTS (SELECT * FROM @ContractTbl)
	BEGIN
		--如果已经存在该餐厅或甜品店的合同缺失错误信息，则只需要update一下createtime与UserOrGroupID即可
		SET @NeedInsert = 1
		--餐厅
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				SET @NeedInsert=0
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND StoreNo=@StoreNo
				AND (KioskNo IS NULL OR KioskNo='')
			END
		END
		--甜品店
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND KioskNo=@KioskNo)
			BEGIN
				--设置不需要插入新错误的标志
				SET @NeedInsert=0
				--更新createtime与UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate,
				StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND KioskNo=@KioskNo
			END
		END
		
		--需要插入新数据
		IF @NeedInsert = 1
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
			'合同缺失' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
			NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
		
		SET @Result = 1
		--没有合同，后面的验证也没有必要做了，可以直接返回
		RETURN;
	END
	--如果有相关合同存在，如果之前有合同不存在的错误，则需要把IsSolved设置为1
	ELSE
	BEGIN
		--餐厅
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--设置已解决
				UPDATE Forecast_CheckResult SET IsSolved=1
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND StoreNo=@StoreNo
				AND (KioskNo IS NULL OR KioskNo='')
			END
		END
		--甜品店
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND KioskNo=@KioskNo)
			BEGIN
				--设置已解决
				UPDATE Forecast_CheckResult SET IsSolved=1, StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='合同缺失' AND KioskNo=@KioskNo
			END
		END
	END
	
	--1.检查餐厅或甜品站是否与有效合同相关联--End
	
	--合同期限不全算是I类问题
	----2.检查餐厅或甜品站对应的有效合同的合同结束日期是否大于预测结束时间--Begin
	--DECLARE @ContractSnapshotID nvarchar(50), @ContractSnapshotIDCursor CURSOR
	--DECLARE @RentEndDate datetime, @EntityID nvarchar(50), @ContractNo nvarchar(50), @ContractID nvarchar(50)
	--SET @ContractSnapshotIDCursor = CURSOR SCROLL FOR
	--SELECT ContractSnapshotID, ContractID FROM @ContractTbl --这里的合同都是"已生效"
	--OPEN @ContractSnapshotIDCursor
	--FETCH NEXT FROM @ContractSnapshotIDCursor INTO @ContractSnapshotID, @ContractID
	--WHILE @@FETCH_STATUS=0
	--BEGIN
	--	--餐厅
	--	IF @IsKiosk=0
	--	BEGIN
	--		SELECT @EntityID=E.EntityID, @RentEndDate=E.RentEndDate, @ContractNo=C.ContractNO FROM
	--		Contract C INNER JOIN Entity E ON C.ContractSnapshotID = E.ContractSnapshotID
	--		WHERE C.ContractSnapshotID=@ContractSnapshotID 
	--		AND E.StoreOrDeptNo=@StoreNo 
	--		AND (E.KioskNo IS NULL OR E.KioskNo='')
	--	END
	--	--甜品店
	--	ELSE
	--	BEGIN
	--		SELECT @EntityID=E.EntityID, @RentEndDate=E.RentEndDate, @ContractNo=C.ContractNO FROM
	--		Contract C INNER JOIN Entity E ON C.ContractSnapshotID = E.ContractSnapshotID
	--		WHERE C.ContractSnapshotID=@ContractSnapshotID 
	--		AND E.StoreOrDeptNo=@StoreNo 
	--		AND E.KioskNo=@KioskNo
	--	END

	--	--如果合同里的实体租约结束时间小于计算时间，则需要重新更改合同
	--	IF @RentEndDate < @CalEndDate
	--	BEGIN
	--		--如果有相同snapshotid的合同存在期限不全的错误，则只需要update一下createtime与UserOrGroupID即可
	--		SET @NeedInsert=1
			
	--		--分两种情况，合同没改和合同改了但没改对
	--		--情况1. 合同没改--Begin
	--		IF EXISTS(SELECT * FROM Forecast_CheckResult 
	--			WHERE IsSolved=0 AND ErrorType='合同期限不全' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID)
	--		BEGIN
	--			--设置不需要插入新错误的标志
	--			SET @NeedInsert=0
	--			--更新createtime与UserOrGroupID
	--			UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
	--			WHERE IsSolved=0 AND ErrorType='合同期限不全' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID
	--		END
	--		--情况1. 合同没改--End
			
	--		--情况2. 合同改了，但没改对--Begin
	--		--因为存储过程开始时，会首先将历史版本的合同且IsSolved=0的任务项给删除，
	--		--所以情况2这种情况就只需要将新的错误合同存入任务项中就行了
	--		--情况2. 合同改了，但没改对--End
			
	--		--需要插入
	--		IF @NeedInsert=1
	--		BEGIN
	--			INSERT INTO Forecast_CheckResult
	--			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
	--			'合同期限不全' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, @ContractNo AS ContractNo,
	--			@ContractSnapshotID AS ContractSnapShotID, @EntityID AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
	--			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
	--		END
			
	--		SET @Result = 1
	--	END
	--	--如果合同实体租约时间合法，先查找有木有对应错误类型，StoreNo,kioskNo,ContractSnapShotID与EntityID，有的话
	--	--更新IsSolved为1
	--	ELSE
	--	BEGIN
	--		IF EXISTS(SELECT * FROM Forecast_CheckResult 
	--			WHERE IsSolved=0 AND ErrorType='合同期限不全' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID)
	--		BEGIN
	--			--更新IsSolved=1
	--			UPDATE Forecast_CheckResult SET IsSolved=1
	--			WHERE IsSolved=0 AND ErrorType='合同期限不全' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID
	--		END
	--	END
		
	--	FETCH NEXT FROM @ContractSnapshotIDCursor INTO @ContractSnapshotID, @ContractID
	--END
	--CLOSE @ContractSnapshotIDCursor
	--DEALLOCATE @ContractSnapshotIDCursor
	----2.检查餐厅或甜品站对应的有效合同的合同结束日期是否大于预测结束时间--End
END














GO


