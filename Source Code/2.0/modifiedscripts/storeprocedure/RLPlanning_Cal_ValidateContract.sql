USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_ValidateContract]    Script Date: 08/17/2012 12:49:00 ******/
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
CREATE PROCEDURE [dbo].[RLPlanning_Cal_ValidateContract] 
@StoreNo nvarchar(50),
@KioskNo nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier,
@Result int output --0Ϊ�޴���1Ϊ�д���
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--����������Ҫ�ˣ��ⲿ������������RLPlanning_Cal_CheckContractAndGenInfo��
	--������--Begin
	--����ʷ�汾�ĺ�ͬ��IsSolve=0���������ɾ��
	--DELETE FROM Forecast_CheckResult
	--WHERE IsSolved = 0 AND TaskType=1
	--AND ContractSnapShotID IN (SELECT ContractSnapShotID FROM Contract WHERE SnapshotCreateTime IS NOT NULL)
	--������--End
	
	DECLARE @CurDate datetime
	DECLARE @NeedInsert bit
	SET @CurDate = GETDATE()
	SET @Result = 0
	
	--�������Ʒ�껹�ǲ���
	DECLARE @IsKiosk BIT
	IF @KioskNo IS NULL OR @KioskNo=''
		SET @IsKiosk = 0	--����
	ELSE
	BEGIN
		SET @IsKiosk = 1	--��Ʒ��
		
		--��ȡ��Ʒ�굱ǰ�ҿ��Ĳ���no
		SELECT @StoreNo=StoreNo FROM v_KioskStoreZoneInfo WHERE KioskNo=@KioskNo
		AND @CurDate BETWEEN StartDate AND EndDate
	END

	DECLARE @ContractTbl TABLE(ContractSnapshotID nvarchar(50), ContractID nvarchar(50))
	--1.����������Ʒվ�Ƿ�����Ч��ͬ�����--Begin
	--����
	IF @IsKiosk=0
	BEGIN
		INSERT INTO @ContractTbl
		SELECT C.ContractSnapshotID, C.ContractID 
		FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '')
		AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
	END
	--��Ʒ��
	ELSE
	BEGIN
		INSERT INTO @ContractTbl
		SELECT C.ContractSnapshotID, C.ContractID 
		FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE E.KioskNo = @KioskNo
		AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
	END
	
	--����������Ʒ��û����Ч�ĺ�ͬ����
	IF NOT EXISTS (SELECT * FROM @ContractTbl)
	BEGIN
		--����Ѿ����ڸò�������Ʒ��ĺ�ͬȱʧ������Ϣ����ֻ��Ҫupdateһ��createtime��UserOrGroupID����
		SET @NeedInsert = 1
		--����
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				SET @NeedInsert=0
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND StoreNo=@StoreNo
				AND (KioskNo IS NULL OR KioskNo='')
			END
		END
		--��Ʒ��
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND KioskNo=@KioskNo)
			BEGIN
				--���ò���Ҫ�����´���ı�־
				SET @NeedInsert=0
				--����createtime��UserOrGroupID
				UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate,
				StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND KioskNo=@KioskNo
			END
		END
		
		--��Ҫ����������
		IF @NeedInsert = 1
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
			'��ͬȱʧ' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, NULL AS ContractNo,
			NULL AS ContractSnapShotID, NULL AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
		
		SET @Result = 1
		--û�к�ͬ���������֤Ҳû�б�Ҫ���ˣ�����ֱ�ӷ���
		RETURN;
	END
	--�������غ�ͬ���ڣ����֮ǰ�к�ͬ�����ڵĴ�������Ҫ��IsSolved����Ϊ1
	ELSE
	BEGIN
		--����
		IF @IsKiosk=0
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND StoreNo=@StoreNo 
				AND (@KioskNo IS NULL OR @KioskNo=''))
			BEGIN
				--�����ѽ��
				UPDATE Forecast_CheckResult SET IsSolved=1
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND StoreNo=@StoreNo
				AND (KioskNo IS NULL OR KioskNo='')
			END
		END
		--��Ʒ��
		ELSE
		BEGIN
			IF EXISTS(SELECT * FROM Forecast_CheckResult 
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND KioskNo=@KioskNo)
			BEGIN
				--�����ѽ��
				UPDATE Forecast_CheckResult SET IsSolved=1, StoreNo=@StoreNo
				WHERE IsSolved=0 AND ErrorType='��ͬȱʧ' AND KioskNo=@KioskNo
			END
		END
	END
	
	--1.����������Ʒվ�Ƿ�����Ч��ͬ�����--End
	
	--��ͬ���޲�ȫ����I������
	----2.����������Ʒվ��Ӧ����Ч��ͬ�ĺ�ͬ���������Ƿ����Ԥ�����ʱ��--Begin
	--DECLARE @ContractSnapshotID nvarchar(50), @ContractSnapshotIDCursor CURSOR
	--DECLARE @RentEndDate datetime, @EntityID nvarchar(50), @ContractNo nvarchar(50), @ContractID nvarchar(50)
	--SET @ContractSnapshotIDCursor = CURSOR SCROLL FOR
	--SELECT ContractSnapshotID, ContractID FROM @ContractTbl --����ĺ�ͬ����"����Ч"
	--OPEN @ContractSnapshotIDCursor
	--FETCH NEXT FROM @ContractSnapshotIDCursor INTO @ContractSnapshotID, @ContractID
	--WHILE @@FETCH_STATUS=0
	--BEGIN
	--	--����
	--	IF @IsKiosk=0
	--	BEGIN
	--		SELECT @EntityID=E.EntityID, @RentEndDate=E.RentEndDate, @ContractNo=C.ContractNO FROM
	--		Contract C INNER JOIN Entity E ON C.ContractSnapshotID = E.ContractSnapshotID
	--		WHERE C.ContractSnapshotID=@ContractSnapshotID 
	--		AND E.StoreOrDeptNo=@StoreNo 
	--		AND (E.KioskNo IS NULL OR E.KioskNo='')
	--	END
	--	--��Ʒ��
	--	ELSE
	--	BEGIN
	--		SELECT @EntityID=E.EntityID, @RentEndDate=E.RentEndDate, @ContractNo=C.ContractNO FROM
	--		Contract C INNER JOIN Entity E ON C.ContractSnapshotID = E.ContractSnapshotID
	--		WHERE C.ContractSnapshotID=@ContractSnapshotID 
	--		AND E.StoreOrDeptNo=@StoreNo 
	--		AND E.KioskNo=@KioskNo
	--	END

	--	--�����ͬ���ʵ����Լ����ʱ��С�ڼ���ʱ�䣬����Ҫ���¸��ĺ�ͬ
	--	IF @RentEndDate < @CalEndDate
	--	BEGIN
	--		--�������ͬsnapshotid�ĺ�ͬ�������޲�ȫ�Ĵ�����ֻ��Ҫupdateһ��createtime��UserOrGroupID����
	--		SET @NeedInsert=1
			
	--		--�������������ͬû�ĺͺ�ͬ���˵�û�Ķ�
	--		--���1. ��ͬû��--Begin
	--		IF EXISTS(SELECT * FROM Forecast_CheckResult 
	--			WHERE IsSolved=0 AND ErrorType='��ͬ���޲�ȫ' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID)
	--		BEGIN
	--			--���ò���Ҫ�����´���ı�־
	--			SET @NeedInsert=0
	--			--����createtime��UserOrGroupID
	--			UPDATE Forecast_CheckResult SET CreateTime=@CurDate, UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate
	--			WHERE IsSolved=0 AND ErrorType='��ͬ���޲�ȫ' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID
	--		END
	--		--���1. ��ͬû��--End
			
	--		--���2. ��ͬ���ˣ���û�Ķ�--Begin
	--		--��Ϊ�洢���̿�ʼʱ�������Ƚ���ʷ�汾�ĺ�ͬ��IsSolved=0���������ɾ����
	--		--�������2���������ֻ��Ҫ���µĴ����ͬ�����������о�����
	--		--���2. ��ͬ���ˣ���û�Ķ�--End
			
	--		--��Ҫ����
	--		IF @NeedInsert=1
	--		BEGIN
	--			INSERT INTO Forecast_CheckResult
	--			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 1 AS TaskType,
	--			'��ͬ���޲�ȫ' AS ErrorType, @StoreNo AS StoreNo, @KioskNo AS KioskNo, @ContractNo AS ContractNo,
	--			@ContractSnapshotID AS ContractSnapShotID, @EntityID AS EntityID, NULL AS Remark, @CurDate AS CreateTime,
	--			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
	--		END
			
	--		SET @Result = 1
	--	END
	--	--�����ͬʵ����Լʱ��Ϸ����Ȳ�����ľ�ж�Ӧ�������ͣ�StoreNo,kioskNo,ContractSnapShotID��EntityID���еĻ�
	--	--����IsSolvedΪ1
	--	ELSE
	--	BEGIN
	--		IF EXISTS(SELECT * FROM Forecast_CheckResult 
	--			WHERE IsSolved=0 AND ErrorType='��ͬ���޲�ȫ' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID)
	--		BEGIN
	--			--����IsSolved=1
	--			UPDATE Forecast_CheckResult SET IsSolved=1
	--			WHERE IsSolved=0 AND ErrorType='��ͬ���޲�ȫ' 
	--			AND ContractSnapShotID=@ContractSnapshotID
	--			AND EntityID=@EntityID
	--		END
	--	END
		
	--	FETCH NEXT FROM @ContractSnapshotIDCursor INTO @ContractSnapshotID, @ContractID
	--END
	--CLOSE @ContractSnapshotIDCursor
	--DEALLOCATE @ContractSnapshotIDCursor
	----2.����������Ʒվ��Ӧ����Ч��ͬ�ĺ�ͬ���������Ƿ����Ԥ�����ʱ��--End
END














GO


