USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetEntityInfosettingWithSameContent]    Script Date: 07/28/2012 11:47:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetEntityInfosettingWithSameContent]
(
	@ContractSnapshotID nvarchar(50),
	@LastContractSnapshotID nvarchar(50)
)
RETURNS @Result TABLE(EntityInfoSettingID nvarchar(50), LastEntityInfoSettingID nvarchar(50))
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	
	--将两份合同的里面的EntityInfo重新组织并存入临时表中
	DECLARE @EntityInfo TABLE(EntityInfoSettingID nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50), VendorNo nvarchar(50))
	DECLARE @LastEntityInfo TABLE(EntityInfoSettingID nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50), VendorNo nvarchar(50))
	
	INSERT INTO @EntityInfo
	SELECT ES.EntityInfoSettingID, ISNULL(E.StoreOrDeptNo,''), ISNULL(E.KioskNo,''), ES.VendorNo
	FROM Entity E INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
	WHERE E.ContractSnapshotID=@ContractSnapshotID
	
	INSERT INTO @LastEntityInfo
	SELECT ES.EntityInfoSettingID, ISNULL(E.StoreOrDeptNo,''), ISNULL(E.KioskNo,''), ES.VendorNo 
	FROM Entity E INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
	WHERE E.ContractSnapshotID=@LastContractSnapshotID
	
	--将有相同StoreNo,KioskNo与VendorNo的EntityInfoSettingID返回出来
	INSERT INTO @Result
	SELECT E1.EntityInfoSettingID, E2.EntityInfoSettingID  
	FROM @EntityInfo E1 
	INNER JOIN @LastEntityInfo E2 ON E1.StoreNo = E2.StoreNo
	AND E1.KioskNo = E2.KioskNo AND E1.VendorNo = E2.VendorNo
	
	RETURN
END

GO


