USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_GetLatestEntityID]    Script Date: 08/02/2012 15:30:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_GetLatestEntityID]
@EntityID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--获取用来比较的EntityTypeName与EntityName
	DECLARE @EntityTypeName nvarchar(20), @EntityName nvarchar(200)
	SELECT @EntityTypeName=EntityTypeName, @EntityName = EntityName FROM Entity 
	WHERE EntityID=@EntityID
	
	DECLARE @LatestEntityID nvarchar(50), @ContractID nvarchar(50)
	--获取ContractID
	SELECT @ContractID=C.ContractID FROM Contract C 
	INNER JOIN Entity E ON C.ContractSnapshotID=E.ContractSnapshotID
	WHERE E.EntityID=@EntityID
	
	--根据ContractID获取最新的ContractSnapshotID
	DECLARE @LatestContractSnapshotID nvarchar(50)
	SELECT TOP 1 @LatestContractSnapshotID=ContractSnapshotID FROM Contract 
	WHERE ContractID=@ContractID AND SnapshotCreateTime IS NULL --千万不要指定status='已生效'
	ORDER BY LastModifyTime DESC	--为了防止有多个SnapshotCreateTime IS NULL的情况
	
	--比较EntityType与EntityName来获取需要的EntityID
	SELECT @LatestEntityID=E.EntityID FROM Contract C 
	INNER JOIN Entity E ON C.ContractSnapshotID=E.ContractSnapshotID
	WHERE C.ContractSnapshotID = @LatestContractSnapshotID
	AND E.EntityTypeName=@EntityTypeName
	AND E.EntityName=@EntityName
	
	--返回所得值
	SELECT @LatestEntityID AS EntityID
END


GO


