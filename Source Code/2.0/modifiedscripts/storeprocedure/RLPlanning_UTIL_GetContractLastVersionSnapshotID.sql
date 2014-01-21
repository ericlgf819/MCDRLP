USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_GetContractLastVersionSnapshotID]    Script Date: 07/27/2012 14:34:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_GetContractLastVersionSnapshotID]
@ContractSnapshotID nvarchar(50),
@LastContractSnapshotID nvarchar(50) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		--先获取Version与ContractID
	DECLARE @Version nvarchar(50), @ContractID nvarchar(50)
	
	SELECT @Version=Version, @ContractID=ContractID FROM Contract 
	WHERE ContractSnapshotID=@ContractSnapshotID
	BEGIN TRY
		--版本1表面没有历史版本，所以合同没有变更
		IF @Version <= 1
			SET @LastContractSnapshotID = NULL
		
		--将@Version减1，然后以该条件获得上一个版本的合同ContractSnapshotID
		SET @Version -= 1
		SELECT DISTINCT @LastContractSnapshotID=ContractSnapshotID FROM Contract
		WHERE ContractID=@ContractID AND Version=@Version
	END TRY
	BEGIN CATCH
		--如果Version不能转型为数字，则直接返回了，表面没有历史版本
		SET @LastContractSnapshotID = NULL
	END CATCH
	--获取该合同上一个Version的ContractSnapshotID--End
END

GO


