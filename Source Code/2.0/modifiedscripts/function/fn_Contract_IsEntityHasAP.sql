USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Contract_IsEntityHasAP]    Script Date: 08/03/2012 10:09:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	实体是否已经出过AP
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_IsEntityHasAP]
(
	-- Add the parameters for the function here
	@EntityID NVARCHAR(50)
)
RETURNS BIT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT 
	
	--added by Eric
	SET @ResultVar = 0
	
	--commented by Eric
	--DECLARE @ContractID NVARCHAR(50)
	--SELECT @ContractID=dbo.fn_Contract_GetContractID(ContractSnapshotID) 
	--FROM dbo.Entity 
	--WHERE EntityID=@EntityID

	---- Add the T-SQL statements to compute the return value here
	--IF EXISTS
	--(
	--	SELECT 1 FROM 
	--	(
	--		SELECT 
	--			e.EntityTypeName,e.StoreOrDeptNo,e.EntityName,
	--			r.CycleStartDate,r.CycleEndDate
	--		FROM dbo.APRecord r 
	--		INNER JOIN dbo.Entity e ON r.EntityID = e.EntityID
	--		WHERE dbo.fn_Contract_GetContractID(r.ContractSnapshotID) = @ContractID
	--	) x INNER JOIN dbo.Entity e ON x.EntityTypeName = e.EntityTypeName 
	--		AND x.StoreOrDeptNo = e.StoreOrDeptNo 
	--		AND x.EntityName = e.EntityName
	--)
	--BEGIN
	--	SELECT @ResultVar=1
	--END
	--ELSE
	--BEGIN
	--	SELECT @ResultVar=0
	--END
		
		

	-- Return the result of the function
	RETURN @ResultVar

END




GO


