USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_ValidateSales]    Script Date: 08/17/2012 13:10:47 ******/
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
CREATE PROCEDURE [dbo].[RLPlanning_Cal_ValidateSales]
@StoreNo nvarchar(50),
@KioskNo nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier,
@Result int output --0为无错误，1为有错误
--思路：查看真实sales数据和预测sales数据的最大时间(YYYY-MM-01)是否小于@CalEndDate的年月组合，不看日
--小于，说明Sales数据不齐全，返回1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET @Result = 0;
	
	--检查是甜品店还是餐厅
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0	--餐厅
	ELSE
	BEGIN
		SET @IsKiosk = 1	--甜品店
		
		--设置甜品店当前时间段挂靠的StoreNo
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND GETDATE() BETWEEN StartDate AND EndDate
	END
	
	DECLARE @ForecastSalesMaxEndDate datetime, @RealSalesMaxEndDate datetime
	--餐厅
	IF @IsKiosk=0
	BEGIN
		SELECT @RealSalesMaxEndDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE StoreNo = @StoreNo AND KioskNo IS NULL
		
		SELECT @ForecastSalesMaxEndDate = MAX(SalesDate) 
		FROM Forecast_Sales WHERE StoreNo = @StoreNo AND KioskNo IS NULL
	END
	--甜品店
	ELSE
	BEGIN
		SELECT @RealSalesMaxEndDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE KioskNo = @KioskNo
		
		SELECT @ForecastSalesMaxEndDate = MAX(SalesDate) 
		FROM Forecast_Sales WHERE KioskNo = @KioskNo
	END
	
	--判断
	--都没值，说明没有数据
	IF @ForecastSalesMaxEndDate IS NULL AND @RealSalesMaxEndDate IS NULL
		SET @Result = 1
	
	--预测sales的时间肯定比真实sales的时间长
	IF @ForecastSalesMaxEndDate IS NOT NULL
	BEGIN
		--年数据小，说明sales数据小
		IF YEAR(@ForecastSalesMaxEndDate) < YEAR(@CalEndDate)
			SET @Result = 1
		
		--同年，根据月数据判断
		ELSE IF YEAR(@ForecastSalesMaxEndDate) = YEAR(@CalEndDate)
		BEGIN
			IF MONTH(@ForecastSalesMaxEndDate) < MONTH(@CalEndDate)
				SET @Result = 1
			ELSE
				SET @Result = 0
		END
		ELSE
			SET @Result = 0
	END
	
	--预测数据如果没有，再看真实数据
	ELSE IF @RealSalesMaxEndDate IS NOT NULL
	BEGIN
		--年数据小，说明sales数据小
		IF YEAR(@RealSalesMaxEndDate) < YEAR(@CalEndDate)
			SET @Result = 1
		
		--同年，根据月数据判断
		ELSE IF YEAR(@RealSalesMaxEndDate) = YEAR(@CalEndDate)
		BEGIN
			IF MONTH(@RealSalesMaxEndDate) < MONTH(@CalEndDate)
				SET @Result = 1
			ELSE
				SET @Result = 0
		END
		ELSE
			SET @Result = 0
	END
	
	--如果预测Sales缺失，但是对应的合同只有固定租金
	--则不用提示错误，因为计算固定租金是不需要sales的
	IF @Result = 1
	BEGIN
		--餐厅
		IF @IsKiosk=0
		BEGIN
			IF NOT EXISTS (
				SELECT * FROM Entity E 
				INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID 
				AND C.SnapshotCreateTime IS NULL AND C.Status='已生效'
				INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
				INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
				WHERE E.StoreOrDeptNo=@StoreNo AND (E.KioskNo = '' OR E.KioskNo IS NULL)
			)
			BEGIN
				SET @Result = 0
			END
		END
		--甜品店
		ELSE
		BEGIN
			IF NOT EXISTS (
				SELECT * FROM Entity E 
				INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID 
				AND C.SnapshotCreateTime IS NULL AND C.Status='已生效'
				INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
				INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
				WHERE E.KioskNo = @KioskNo
			)
			BEGIN
				SET @Result = 0
			END
		END
	END
	
	--将错误信息插入任务表中
	IF @Result = 1
	BEGIN
		DECLARE @CurDate datetime
		DECLARE @NeedInsert bit
		SET @CurDate = GETDATE()
	
		--如果已经存在该餐厅或甜品店的Sales数据不全错误信息，则只需要update一下createtime与UserOrGroupID即可
		SET @NeedInsert = 1
		--餐厅
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--设置不需要插入新错误的标志
				SET @NeedInsert=0
				--更新createtime与UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND StoreNo=@StoreNo
				AND (@KioskNo IS NULL OR @KioskNo='')
			END
		END
		--甜品店
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND KioskNo=@KioskNo)
			BEGIN
				--设置不需要插入新错误的标志
				SET @NeedInsert=0
				--更新createtime与UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate,
				StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND KioskNo=@KioskNo
			END
		END
	
		--需要插入新数据
		IF @NeedInsert=1
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
			'Sales数据不全' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
			NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
	END
	--如果Sales信息正确，则查看有没有当前StoreNo,kioskNo的错误类型信息
	--如果有，则设置IsSolved=1
	ELSE
	BEGIN
		--餐厅
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--更新IsSolved
				UPDATE Forecast_CheckResult SET IsSolved=1
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND StoreNo=@StoreNo
				AND (@KioskNo IS NULL OR @KioskNo='')
			END
		END
		--甜品店
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND KioskNo=@KioskNo)
			BEGIN
				--更新IsSolved
				UPDATE Forecast_CheckResult SET IsSolved=1, StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='Sales数据不全' AND KioskNo=@KioskNo
			END
		END
	END
END












GO


