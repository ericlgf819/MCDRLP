USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_InsertContract]    Script Date: 06/29/2012 10:18:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- ========================================
-- Author:     liujj
-- Create Date:20101221
-- Description:新增合同信息
-- ========================================
ALTER PROCEDURE [dbo].[usp_Contract_InsertContract]
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
	
	--查询当前操作人的英文名
	Select @OperationEnglishName=EnglishName 
	from dbo.SRLS_TB_Master_User
	Where ID=@OperationID
	
	-- Modified by Eric
	SELECT @ContractNO = dbo.fn_SerialNumber_Contract(@CompanyCode)
	-- End
	
	SET @Version = '1'
	
	Set @Res = 0
	BEGIN TRANSACTION MYTRANSACTION
		BEGIN TRY
				INSERT INTO [dbo].[Contract] (
					[ContractSnapshotID],[CompanyCode],[ContractID],[ContractNO],
					[Version],[ContractName],[CompanyName],[CompanySimpleName],
					[CompanyRemark],--[Status],
					[Remark],[UpdateInfo],[IsLocked],
					[CreateTime],[CreatorName],[LastModifyTime],[LastModifyUserName],
					[SnapshotCreateTime],[IsSave],[PartComment],[FromSRLS]/*New column--Added by Eric*/) 
				VALUES (
					@ContractSnapshotID,@CompanyCode,@ContractID,@ContractNO,
					1,@ContractName,@CompanyName,@CompanySimpleName,
					@CompanyRemark,--@Status,
					@Remark,@UpdateInfo,@IsLocked,
					GETDATE(),@CreatorName,@LastModifyTime,@LastModifyUserName,
					@SnapshotCreateTime,@IsSave,'新建',0/*New column value--Added by Eric*/)
				
				SET @Xml = (SELECT ContractSnapshotID,ContractID,ContractNO,
								   Version,ContractName,CompanyName,Status,
								   UpdateInfo,IsLocked,SnapshotCreateTime,IsSave
				FROM [Contract] WHERE [ContractSnapshotID] = @ContractSnapshotID FOR XML AUTO)
				--将日志插入到日志表中
				INSERT INTO SRLS_TB_System_UserLog(ID,UserID,EnglishName,SourceID,TableName,DataInfo,UpdateTime,LogType,CompanyCode)
				VALUES(NewID(),@OperationID,@OperationEnglishName,@ContractSnapshotID,'Contract',@Xml,GETDATE(),0,@CompanyCode)--0新增，1修改，2删除 
			COMMIT TRANSACTION MYTRANSACTION
			SET @Res = 1
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION MYTRANSACTION
			DECLARE @MSG nvarchar(max);SELECT @MSG=ERROR_MESSAGE();Raiserror(@msg, 16, 1);
		END CATCH
END






GO


