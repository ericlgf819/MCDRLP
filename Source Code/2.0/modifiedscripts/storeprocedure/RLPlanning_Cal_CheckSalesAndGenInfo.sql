USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_CheckSalesAndGenInfo]    Script Date: 08/31/2012 19:12:35 ******/
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
CREATE PROCEDURE [dbo].[RLPlanning_Cal_CheckSalesAndGenInfo] 
@EntityID nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--如果只有固定租金，则不需要产生Sales为0的Information
	IF NOT EXISTS 
		(SELECT * FROM Entity E 
		 INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
		 INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
		 WHERE E.EntityID=@EntityID
		 )
	BEGIN
		RETURN
	END
	
	--计算起租日，餐厅和kiosk的number
	DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
	DECLARE @RentStartDate datetime	
	SELECT @RentStartDate = RentStartDate, @StoreNo = StoreOrDeptNo, @KioskNo = KioskNo 
	FROM Entity WHERE EntityID=@EntityID
	
	--餐厅还是甜品店
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0
	ELSE
		SET @IsKiosk = 1
		
	--获取当前时间
	DECLARE @CurDate datetime
	SET @CurDate = GETDATE()
	
	--甜品店
	IF @IsKiosk = 1
	BEGIN
		--将起租日和计算到期日的时间格式改为YYYY-MM-01, 因为下面的数据是来自于RLPlanning_RealSales表
		SET @RentStartDate = STR(YEAR(@RentStartDate))+'-'+STR(MONTH(@RentStartDate))+'-1'
		SET @CalEndDate = STR(YEAR(@CalEndDate))+'-'+STR(MONTH(@CalEndDate))+'-1'
		
		DECLARE @KioskSumSales decimal(18,2)
		SELECT @KioskSumSales=SUM(Sales) FROM RLPlanning_RealSales 
		WHERE KioskNo=@KioskNo AND SalesDate BETWEEN @RentStartDate AND @CalEndDate
		--已经有同一个KioskNo的记录在Forecast_CheckResult中 -- Begin
		IF EXISTS(SELECT * FROM Forecast_CheckResult 
				  WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				  AND TaskType=0 AND ErrorType='Kiosk Sales 总额为0'
				 )
		BEGIN
			--如果@KioskSumSales为0，更新CreateTime IsRead IsSolved UserOrGroupID FixUserID CalEndDate TaskFinishTime
			IF @KioskSumSales=0
			BEGIN
				UPDATE Forecast_CheckResult 
				SET CreateTime=@CurDate, IsRead=0, IsSolved=0, UserOrGroupID=@UserOrGroupID,
				FixUserID=NULL, CalEndDate=@CalEndDate, TaskFinishTime=NULL
				WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				AND TaskType=0 AND ErrorType='Kiosk Sales 总额为0'
			END
			--如果@KioskSumSales不为0表面修复完成，更新IsRead IsSolved FixUserID CalEndDate TaskFinishTime
			ELSE
			BEGIN
				UPDATE Forecast_CheckResult 
				SET IsRead=0, IsSolved=1, FixUserID=@UserOrGroupID, 
				CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
				WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				AND TaskType=0 AND ErrorType='Kiosk Sales 总额为0'				
			END
		END
		--已经有同一个KioskNo的记录在Forecast_CheckResult中 -- End
		
		--Forecast_CheckResult中没有该KioskNo对应的记录 -- Begin
		ELSE
		BEGIN
			--如果@KioskSumSales为0，则插入一条Information
			IF @KioskSumSales = 0
			BEGIN
				INSERT INTO Forecast_CheckResult
				SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
				'Kiosk Sales 总额为0' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
				NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
				0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
			END
		END
		--Forecast_CheckResult中没有该KioskNo对应的记录 -- End
	END
	--餐厅
	ELSE
	BEGIN
		--已经有同一个StoreNo的记录在Forecast_CheckResult中 -- Begin
		IF EXISTS(SELECT * FROM Forecast_CheckResult 
				  WHERE StoreNo=@StoreNo AND ISNULL(KioskNo,'')=''
				  AND TaskType=0 AND ErrorType='Store Sales 有0数据'
				 )
		BEGIN
			--存在有一天的Sales为0，更新CreateTime IsRead IsSolved UserOrGroupID FixUserID CalEndDate TaskFinishTime
			IF EXISTS (
					SELECT * FROM StoreSales WHERE StoreNo = @StoreNo 
					AND Sales=0
					AND SalesDate BETWEEN @RentStartDate AND @CalEndDate
				)
			BEGIN
				UPDATE Forecast_CheckResult 
				SET CreateTime=@CurDate, IsRead=0, IsSolved=0, UserOrGroupID=@UserOrGroupID,
				FixUserID=NULL, CalEndDate=@CalEndDate, TaskFinishTime=NULL
				WHERE StoreNo=@StoreNo AND ISNULL(KioskNo,'')=''
				AND TaskType=0 AND ErrorType='Store Sales 有0数据'
			END
			--不存在表面修复完成，更新IsRead IsSolved FixUserID CalEndDate TaskFinishTime
			ELSE
			BEGIN
				UPDATE Forecast_CheckResult 
				SET IsRead=0, IsSolved=1, FixUserID=@UserOrGroupID, 
				CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
				WHERE StoreNo=@StoreNo AND ISNULL(KioskNo,'')=''
				AND TaskType=0 AND ErrorType='Store Sales 有0数据'
			END
		END
		--已经有同一个StoreNo的记录在Forecast_CheckResult中 -- End
		
		--Forecast_CheckResult中没有该StoreNo对应的记录 -- Begin
		ELSE
		BEGIN
			--存在有一天的Sales为0，则插入一条Information
			IF EXISTS (
					SELECT * FROM StoreSales WHERE StoreNo = @StoreNo 
					AND Sales=0
					AND SalesDate BETWEEN @RentStartDate AND @CalEndDate
				)
			BEGIN
				DECLARE @ZeroTimeFirstDate DATE, @FirstDayOfMonth DATE
				
				SELECT TOP 1 @ZeroTimeFirstDate = SalesDate 
				FROM StoreSales WHERE StoreNo = @StoreNo 
				AND Sales=0 AND SalesDate BETWEEN @RentStartDate AND @CalEndDate
				ORDER BY SalesDate ASC
				
				SET @FirstDayOfMonth = STR(YEAR(@ZeroTimeFirstDate)) + '-' + STR(MONTH(@ZeroTimeFirstDate)) + '-' + '1'
				
				-- 如果该月的第一天有同步过来的sales，则后面的0 sales 数据无需关心
				IF NOT EXISTS (SELECT * FROM StoreSales WHERE StoreNo = @StoreNo
				AND SalesDate = @FirstDayOfMonth AND FromSRLS = 1)
				BEGIN
					INSERT INTO Forecast_CheckResult
					SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
					'Store Sales 有0数据' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
					NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
					0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
				END
			
			END
		END
		--Forecast_CheckResult中没有该StoreNo对应的记录 -- End
	END
END


GO


