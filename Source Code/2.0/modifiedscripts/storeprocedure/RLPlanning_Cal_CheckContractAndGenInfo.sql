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
	
	-- 获取对应的ContractSnapshotID，ContractNo，当前时间，租赁到期日
	SET @CurDate = GETDATE();
	
	SELECT @ContractID=C.ContractID, @ContractSnapshotID=E.ContractSnapshotID, @ContractNo=C.ContractNO, 
	@RentEndDate=E.RentEndDate, @StoreNo=E.StoreOrDeptNo, @KioskNo=E.KioskNo
	FROM Entity E INNER JOIN Contract C ON E.ContractSnapshotID=C.ContractSnapshotID
	WHERE E.EntityID=@EntityID
	
	-- 如果计算截止日期大于合同租赁结束日期，则需要产生Infomation
	IF @CalEndDate > @RentEndDate
	BEGIN
		-- 如果有同一份合同ContractsnapshotID与EntityID已经有这个Infomation了，只需要更新 IsSolved
		-- CreateTime IsRead UserOrGroupID FixUserID CalEndDate TaskFinishTime
		IF EXISTS(SELECT * FROM Forecast_CheckResult WHERE TaskType=0
					AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
				  )
		BEGIN
			UPDATE Forecast_CheckResult SET IsSolved=0, IsRead=0, CreateTime=@CurDate,
			UserOrGroupID=@UserOrGroupID, FixUserID=null, CalEndDate=@CalEndDate, TaskFinishTime=null
			WHERE TaskType=0 AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
		END
		
		-- 如果没有这个Infomation直接插入信息即可
		ELSE
		BEGIN
			INSERT INTO Forecast_CheckResult
			SELECT NEWID() AS CheckID, dbo.RLPlanning_CalTaskNo(@CurDate) AS TaskNo, 0 AS TaskType,
			'合同: '+@ContractNo+' 时间小于计算结束时间' AS ErrorType, @StoreNo AS StoreNo,
			@KioskNo AS KioskNo, @ContractNo AS ContractNo, @ContractSnapshotID AS ContractSnapshotID,
			@EntityID AS EntityID, NULL AS Remark, @CurDate AS CreateTime, 
			0 AS IsRead, 0 AS IsSolved, @UserOrGroupID AS UserOrGroupID, NULL AS FixUserID, @CalEndDate AS CalEndDate, NULL AS TaskFinishTime
		END
	END
	-- 如果没有问题，则将已经存在的ContractsnapshotID与EntityID的CheckResult更新为已经解决
	-- 并且更新IsRead FixUserID CalEndDate TaskFinishTime
	ELSE
	BEGIN
		--如果对应的ContractSnapshotID在任务里面有记录，则直接更新为已经解决
		IF EXISTS (SELECT * FROM Forecast_CheckResult WHERE TaskType=0
				   AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID
				   )
		BEGIN
			UPDATE Forecast_CheckResult SET IsSolved=1, IsRead=0,
			UserOrGroupID=@UserOrGroupID, CalEndDate=@CalEndDate, TaskFinishTime=@CurDate
			WHERE TaskType=0 AND ContractSnapShotID=@ContractSnapshotID AND EntityID=@EntityID		
		END
		
		--此外还需要将该合同的历史版本，对应的I类型的任务都处理为已经解决
		--因为历史版本合同的待处理任务没有意义		
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


