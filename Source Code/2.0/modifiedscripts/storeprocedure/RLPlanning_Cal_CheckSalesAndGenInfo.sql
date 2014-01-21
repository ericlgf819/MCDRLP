USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_CheckSalesAndGenInfo]    Script Date: 08/31/2012 19:12:35 ******/
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
CREATE PROCEDURE [dbo].[RLPlanning_Cal_CheckSalesAndGenInfo] 
@EntityID nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--���ֻ�й̶��������Ҫ����SalesΪ0��Information
	IF NOT EXISTS 
		(SELECT * FROM Entity E 
		 INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
		 INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID
		 WHERE E.EntityID=@EntityID
		 )
	BEGIN
		RETURN
	END
	
	--���������գ�������kiosk��number
	DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
	DECLARE @RentStartDate datetime	
	SELECT @RentStartDate = RentStartDate, @StoreNo = StoreOrDeptNo, @KioskNo = KioskNo 
	FROM Entity WHERE EntityID=@EntityID
	
	--����������Ʒ��
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0
	ELSE
		SET @IsKiosk = 1
		
	--��ȡ��ǰʱ��
	DECLARE @CurDate datetime
	SET @CurDate = GETDATE()
	
	--��Ʒ��
	IF @IsKiosk = 1
	BEGIN
		--�������պͼ��㵽���յ�ʱ���ʽ��ΪYYYY-MM-01, ��Ϊ�����������������RLPlanning_RealSales��
		SET @RentStartDate = STR(YEAR(@RentStartDate))+'-'+STR(MONTH(@RentStartDate))+'-1'
		SET @CalEndDate = STR(YEAR(@CalEndDate))+'-'+STR(MONTH(@CalEndDate))+'-1'
		
		DECLARE @KioskSumSales decimal(18,2)
		SELECT @KioskSumSales=SUM(Sales) FROM RLPlanning_RealSales 
		WHERE KioskNo=@KioskNo AND SalesDate BETWEEN @RentStartDate AND @CalEndDate
		--�Ѿ���ͬһ��KioskNo�ļ�¼��Forecast_CheckResult�� -- Begin
		IF EXISTS(SELECT * FROM Forecast_CheckResult 
				  WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				  AND TaskType=0 AND ErrorType='Kiosk Sales �ܶ�Ϊ0'
				 )
		BEGIN
			--���@KioskSumSalesΪ0������CreateTime IsRead IsSolved UserOrGroupID FixUserID CalEndDate TaskFinishTime
			IF @KioskSumSales=0
			BEGIN
				UPDATE Forecast_CheckResult 
				SET CreateTime=@CurDate, IsRead=0, IsSolved=0, UserOrGroupID=@UserOrGroupID,
				FixUserID=NULL, CalEndDate=@CalEndDate, TaskFinishTime=NULL
				WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				AND TaskType=0 AND ErrorType='Kiosk Sales �ܶ�Ϊ0'
			END
			--���@KioskSumSales��Ϊ0�����޸���ɣ�����IsRead IsSolved FixUserID CalEndDate TaskFinishTime
			ELSE
			BEGIN
				UPDATE Forecast_CheckResult 
				SET IsRead=0, IsSolved=1, FixUserID=@UserOrGroupID, 
				CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
				WHERE KioskNo=@KioskNo AND StoreNo=@StoreNo
				AND TaskType=0 AND ErrorType='Kiosk Sales �ܶ�Ϊ0'				
			END
		END
		--�Ѿ���ͬһ��KioskNo�ļ�¼��Forecast_CheckResult�� -- End
		
		--Forecast_CheckResult��û�и�KioskNo��Ӧ�ļ�¼ -- Begin
		ELSE
		BEGIN
			--���@KioskSumSalesΪ0�������һ��Information
			IF @KioskSumSales = 0
			BEGIN
				INSERT INTO Forecast_CheckResult
				SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
				'Kiosk Sales �ܶ�Ϊ0' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
				NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
				0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
			END
		END
		--Forecast_CheckResult��û�и�KioskNo��Ӧ�ļ�¼ -- End
	END
	--����
	ELSE
	BEGIN
		--�Ѿ���ͬһ��StoreNo�ļ�¼��Forecast_CheckResult�� -- Begin
		IF EXISTS(SELECT * FROM Forecast_CheckResult 
				  WHERE StoreNo=@StoreNo AND ISNULL(KioskNo,'')=''
				  AND TaskType=0 AND ErrorType='Store Sales ��0����'
				 )
		BEGIN
			--������һ���SalesΪ0������CreateTime IsRead IsSolved UserOrGroupID FixUserID CalEndDate TaskFinishTime
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
				AND TaskType=0 AND ErrorType='Store Sales ��0����'
			END
			--�����ڱ����޸���ɣ�����IsRead IsSolved FixUserID CalEndDate TaskFinishTime
			ELSE
			BEGIN
				UPDATE Forecast_CheckResult 
				SET IsRead=0, IsSolved=1, FixUserID=@UserOrGroupID, 
				CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
				WHERE StoreNo=@StoreNo AND ISNULL(KioskNo,'')=''
				AND TaskType=0 AND ErrorType='Store Sales ��0����'
			END
		END
		--�Ѿ���ͬһ��StoreNo�ļ�¼��Forecast_CheckResult�� -- End
		
		--Forecast_CheckResult��û�и�StoreNo��Ӧ�ļ�¼ -- Begin
		ELSE
		BEGIN
			--������һ���SalesΪ0�������һ��Information
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
				
				-- ������µĵ�һ����ͬ��������sales��������0 sales �����������
				IF NOT EXISTS (SELECT * FROM StoreSales WHERE StoreNo = @StoreNo
				AND SalesDate = @FirstDayOfMonth AND FromSRLS = 1)
				BEGIN
					INSERT INTO Forecast_CheckResult
					SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
					'Store Sales ��0����' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
					NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
					0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
				END
			
			END
		END
		--Forecast_CheckResult��û�и�StoreNo��Ӧ�ļ�¼ -- End
	END
END


GO


