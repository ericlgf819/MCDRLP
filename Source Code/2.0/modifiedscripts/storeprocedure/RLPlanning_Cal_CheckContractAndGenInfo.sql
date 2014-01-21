USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_CheckContractAndGenInfo]    Script Date: 08/12/2012 14:01:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_CheckContractAndGenInfo]
@EntityID nvarchar(50),
@CalEndDate datetime,
@UserOrGroupID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ContractSnapshotID nvarchar(50), @ContractNo nvarchar(50), @ContractID nvarchar(50),
			@CurDate datetime, @RentEndDate datetime
	DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
	
	-- ��ȡ��Ӧ��ContractSnapshotID��ContractNo����ǰʱ�䣬���޵�����
	SET @CurDate = GETDATE();
	
	SELECT @ContractID=C.ContractID, @ContractSnapshotID=E.ContractSnapshotID, @ContractNo=C.ContractNO, 
	@RentEndDate=E.RentEndDate, @StoreNo=E.StoreOrDeptNo, @KioskNo=E.KioskNo
	FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID
	WHERE E.EntityID=@EntityID
	
	-- ��������ֹ���ڴ��ں�ͬ���޽������ڣ�����Ҫ����Infomation
	IF @CalEndDate > @RentEndDate
	BEGIN
		-- �����ͬһ�ݺ�ͬContractsnapshotID��EntityID�Ѿ������Infomation�ˣ�ֻ��Ҫ���� IsSolved
		-- CreateTime IsRead UserOrGroupID FixUserID CalEndDate TaskFinishTime
		IF EXISTS(SELECT * FROM Forecast_CheckResult WHERE TaskType=0
					AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
				  )
		BEGIN
			UPDATE Forecast_CheckResult SET IsSolved=0, IsRead=0, CreateTime=@CurDate,
			UserOrGroupID=@UserOrGroupID, FixUserID=null, CalEndDate=@CalEndDate, TaskFinishTime=null
			WHERE TaskType=0 AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
		END
		
		-- ���û�����Infomationֱ�Ӳ�����Ϣ����
		ELSE
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
			'��ͬ: '+@ContractNo+' ʱ��С�ڼ������ʱ��' AS ErrorType, @StoreNo AS StoreNo,
			@KioskNo AS KioskNo, @ContractNo AS ContractNo, @ContractSnapshotID AS ContractSnapshotID,
			@EntityID AS EntityID, NULL AS Remark, @CurDate AS CreateTime, 
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
	END
	-- ���û�����⣬���Ѿ����ڵ�ContractsnapshotID��EntityID��CheckResult����Ϊ�Ѿ����
	-- ���Ҹ���IsRead FixUserID CalEndDate TaskFinishTime
	ELSE
	BEGIN
		--�����Ӧ��ContractSnapshotID�����������м�¼����ֱ�Ӹ���Ϊ�Ѿ����
		IF EXISTS (SELECT * FROM Forecast_CheckResult WHERE TaskType=0
				   AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
				   )
		BEGIN
			UPDATE Forecast_CheckResult SET IsSolved=1, IsRead=0,
			UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
			WHERE TaskType=0 AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID		
		END
		
		--���⻹��Ҫ���ú�ͬ����ʷ�汾����Ӧ��I���͵����񶼴���Ϊ�Ѿ����
		--��Ϊ��ʷ�汾��ͬ�Ĵ���������û������		
		UPDATE Forecast_CheckResult SET IsSolved=1, IsRead=0,
		UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
		WHERE TaskType=0 AND 
		ContractSnapShotID IN 
		(	
			SELECT ContractSnapShotID FROM Contract 
			WHERE ContractID=@ContractID AND ContractSnapShotID<>@ContractSnapshotID
		)
	END
END




GO


