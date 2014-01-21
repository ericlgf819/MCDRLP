USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetKioskRelationEndDate]    Script Date: 08/17/2012 11:11:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By 
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_Cal_GetKioskRelationEndDate]
(
	-- Add the parameters for the function here
	@KioskID NVARCHAR(50),
	@StartDate DATETIME 
)
RETURNS DATETIME 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME;
	--
	SELECT TOP 1 @ResultVar=DATEADD(DAY, -1, ActiveDate) FROM dbo.[KioskStoreRelationChangHistory]
	WHERE KioskID=@KioskID AND ActiveDate>@StartDate
	ORDER BY ActiveDate ASC;
	--
	SELECT @ResultVar=ISNULL(@ResultVar, '21001231');
	RETURN @ResultVar;
END

GO


