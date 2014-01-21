USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_UpdateSingleContract]    Script Date: 06/29/2012 10:17:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- ========================================
-- Author:     liujj
-- Create Date:20101221
-- Description:更新单个合同信息
-- ========================================
ALTER PROCEDURE [dbo].[usp_Contract_UpdateSingleContract]
	@ContractSnapshotID nvarchar(50),
	@CompanyCode nvarchar(32),
	@ContractID nvarchar(50),
	@ContractNO nvarchar(50),
	@Version nvarchar(50),
	@ContractName nvarchar(50),
	@CompanyName nvarchar(50),
	@CompanySimpleName nvarchar(50),
	@CompanyRemark nvarchar(2000),
	@Status nvarchar(50),
	@Remark nvarchar(2000),
	@UpdateInfo nvarchar(2000),
	@IsLocked bit,
	@CreateTime datetime,
	@CreatorName nvarchar(50),
	@LastModifyTime datetime,
	@LastModifyUserName nvarchar(50),
	@SnapshotCreateTime datetime,
	@IsSave BIT,
	@OperationID uniqueidentifier = NULL,
	@Res int output  -- 0表示执行错误,1表示执行成功
AS

SET NOCOUNT ON

Begin
	Declare @Xml xml,
	@OperationEnglishName nvarchar(32)
	Select @OperationEnglishName=EnglishName 
	from dbo.SRLS_TB_Master_User
	Where ID=@OperationID
	Set @Res = 0
 
	Begin
		BEGIN TRAN
			--Added by Eric
			--判断当前合同是否有ContractNO，如果没有则生成正确的ContractNO--Begin
			SELECT @ContractNO = ContractNO FROM Contract WHERE ContractSnapshotID = @ContractSnapshotID
			IF @ContractNO IS NULL
			BEGIN
				SELECT @ContractNO = dbo.fn_SerialNumber_Contract(@CompanyCode)
			END
			--判断当前合同是否有ContractNO，如果没有则生成正确的ContractNO--End
			UPDATE [dbo].[Contract] SET
				[CompanyCode] = @CompanyCode,
				[ContractID] = @ContractID,
				[ContractNO] = @ContractNO,
				--[Version] = @Version,
				[ContractName] = @ContractName,
				[CompanyName] = @CompanyName,
				[CompanySimpleName] = @CompanySimpleName,
				[CompanyRemark] = @CompanyRemark,
				[Status] = @Status,
				[Remark] = @Remark,
				[UpdateInfo] = @UpdateInfo,
				[IsLocked] = @IsLocked,
				[CreateTime] = @CreateTime,
				[CreatorName] = @CreatorName,
				[LastModifyTime] = GETDATE(),
				[LastModifyUserName] = @LastModifyUserName,
				[SnapshotCreateTime] = @SnapshotCreateTime,
				[IsSave] = @IsSave
			WHERE
				[ContractSnapshotID] = @ContractSnapshotID
			
			SET @Xml = (SELECT ContractSnapshotID,
							   ContractID,
							   ContractNO,
							   Version,
							   ContractName,
							   CompanyName,
							   Status,
							   UpdateInfo,
							   IsLocked,
							   SnapshotCreateTime,
							   IsSave
			FROM [Contract] WHERE [ContractSnapshotID] = @ContractSnapshotID FOR XML AUTO)
			--将日志插入到日志表中
			INSERT INTO SRLS_TB_System_UserLog(ID,UserID,EnglishName,SourceID,TableName,DataInfo,UpdateTime,LogType,CompanyCode)
			VALUES(NewID(),@OperationID,@OperationEnglishName,@ContractSnapshotID,'Contract',@Xml,GETDATE(),1,@CompanyCode)--0新增，1修改，2删除
			IF @@ERROR > 0 
				BEGIN
				ROLLBACK TRAN
				END
			ELSE
				BEGIN
				COMMIT TRAN
				SET @Res = 1
		END
	END
END










GO


