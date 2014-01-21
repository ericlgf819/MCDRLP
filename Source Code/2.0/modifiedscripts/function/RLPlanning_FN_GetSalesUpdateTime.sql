USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesUpdateTime]    Script Date: 08/17/2012 14:35:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetSalesUpdateTime] 
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @UpdateTime DATETIME
	--²ÍÌü
	IF ISNULL(@KioskNo,'')=''
	BEGIN
		SELECT @UpdateTime=MAX(UpdateTime) FROM StoreSales WHERE StoreNo=@StoreNo
	END
	--ÌðÆ·µê
	ELSE
	BEGIN
		SELECT @UpdateTime=MAX(KC.CreateTime) 
		FROM KioskSalesCollection KC INNER JOIN SRLS_TB_Master_Kiosk K
		ON KC.KioskID=K.KioskID
		WHERE K.KioskNo=@KioskNo
	END
	
	RETURN @UpdateTime
END



GO


