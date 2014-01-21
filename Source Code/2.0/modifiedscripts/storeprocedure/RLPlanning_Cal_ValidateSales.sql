USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_ValidateSales]    Script Date: 08/17/2012 13:10:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










/*-------------------------------------------------------
Copyright(c) 2012 VIT��

����������
������ʶ��Create By 
 
�޸ı�ʶ��
�޸�������
------------------------------------------------------*/
CREATE PROCEDURE [dbo].[RLPlanning_Cal_ValidateSales]
@StoreNo nvarchar(50),
@KioskNo nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier,
@Result int output --0Ϊ�޴���1Ϊ�д���
--˼·���鿴��ʵsales���ݺ�Ԥ��sales���ݵ����ʱ��(YYYY-MM-01)�Ƿ�С��@CalEndDate��������ϣ�������
--С�ڣ�˵��Sales���ݲ���ȫ������1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET @Result = 0;
	
	--�������Ʒ�껹�ǲ���
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0	--����
	ELSE
	BEGIN
		SET @IsKiosk = 1	--��Ʒ��
		
		--������Ʒ�굱ǰʱ��ιҿ���StoreNo
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND GETDATE() BETWEEN StartDate AND EndDate
	END
	
	DECLARE @ForecastSalesMaxEndDate datetime, @RealSalesMaxEndDate datetime
	--����
	IF @IsKiosk=0
	BEGIN
		SELECT @RealSalesMaxEndDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE StoreNo = @StoreNo AND KioskNo IS NULL
		
		SELECT @ForecastSalesMaxEndDate = MAX(SalesDate) 
		FROM Forecast_Sales WHERE StoreNo = @StoreNo AND KioskNo IS NULL
	END
	--��Ʒ��
	ELSE
	BEGIN
		SELECT @RealSalesMaxEndDate = MAX(SalesDate) 
		FROM RLPlanning_RealSales WHERE KioskNo = @KioskNo
		
		SELECT @ForecastSalesMaxEndDate = MAX(SalesDate) 
		FROM Forecast_Sales WHERE KioskNo = @KioskNo
	END
	
	--�ж�
	--��ûֵ��˵��û������
	IF @ForecastSalesMaxEndDate IS NULL AND @RealSalesMaxEndDate IS NULL
		SET @Result = 1
	
	--Ԥ��sales��ʱ��϶�����ʵsales��ʱ�䳤
	IF @ForecastSalesMaxEndDate IS NOT NULL
	BEGIN
		--������С��˵��sales����С
		IF YEAR(@ForecastSalesMaxEndDate) < YEAR(@CalEndDate)
			SET @Result = 1
		
		--ͬ�꣬�����������ж�
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
	
	--Ԥ���������û�У��ٿ���ʵ����
	ELSE IF @RealSalesMaxEndDate IS NOT NULL
	BEGIN
		--������С��˵��sales����С
		IF YEAR(@RealSalesMaxEndDate) < YEAR(@CalEndDate)
			SET @Result = 1
		
		--ͬ�꣬�����������ж�
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
	
	--���Ԥ��Salesȱʧ�����Ƕ�Ӧ�ĺ�ֻͬ�й̶����
	--������ʾ������Ϊ����̶�����ǲ���Ҫsales��
	IF @Result = 1
	BEGIN
		--����
		IF @IsKiosk=0
		BEGIN
			IF NOT EXISTS (
				SELECT * FROM Entity E 
				INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID 
				AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
				INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
				INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
				WHERE E.StoreOrDeptNo=@StoreNo AND (E.KioskNo = '' OR E.KioskNo IS NULL)
			)
			BEGIN
				SET @Result = 0
			END
		END
		--��Ʒ��
		ELSE
		BEGIN
			IF NOT EXISTS (
				SELECT * FROM Entity E 
				INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID 
				AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
				INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
				INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
				WHERE E.KioskNo = @KioskNo
			)
			BEGIN
				SET @Result = 0
			END
		END
	END
	
	--��������Ϣ�����������
	IF @Result = 1
	BEGIN
		DECLARE @CurDate datetime
		DECLARE @NeedInsert bit
		SET @CurDate = GETDATE()
	
		--����Ѿ����ڸò�������Ʒ���Sales���ݲ�ȫ������Ϣ����ֻ��Ҫupdateһ��createtime��UserOrGroupID����
		SET @NeedInsert = 1
		--����
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--���ò���Ҫ�����´���ı�־
				SET @NeedInsert=0
				--����createtime��UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND StoreNo=@StoreNo
				AND (@KioskNo IS NULL OR @KioskNo='')
			END
		END
		--��Ʒ��
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND KioskNo=@KioskNo)
			BEGIN
				--���ò���Ҫ�����´���ı�־
				SET @NeedInsert=0
				--����createtime��UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate,
				StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND KioskNo=@KioskNo
			END
		END
	
		--��Ҫ����������
		IF @NeedInsert=1
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
			'Sales���ݲ�ȫ' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
			NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
	END
	--���Sales��Ϣ��ȷ����鿴��û�е�ǰStoreNo,kioskNo�Ĵ���������Ϣ
	--����У�������IsSolved=1
	ELSE
	BEGIN
		--����
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--����IsSolved
				UPDATE Forecast_CheckResult SET IsSolved=1
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND StoreNo=@StoreNo
				AND (@KioskNo IS NULL OR @KioskNo='')
			END
		END
		--��Ʒ��
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND KioskNo=@KioskNo)
			BEGIN
				--����IsSolved
				UPDATE Forecast_CheckResult SET IsSolved=1, StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='Sales���ݲ�ȫ' AND KioskNo=@KioskNo
			END
		END
	END
END












GO


