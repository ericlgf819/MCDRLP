USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetRentStartEndDate]    Script Date: 08/17/2012 10:37:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetRentStartEndDate]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS @Result TABLE(RentStartDate datetime,RentEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @MinRentStartDate datetime, @MaxRentEndDate datetime
	
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SELECT @MinRentStartDate = MIN(E.RentStartDate), @MaxRentEndDate = MAX(E.RentEndDate) FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
		AND E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '')
	END
	--甜品店
	ELSE
	BEGIN
		SELECT @MinRentStartDate = MIN(E.RentStartDate), @MaxRentEndDate = MAX(E.RentEndDate) FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
		AND E.KioskNo = @KioskNo AND ISNULL(E.KioskNo,'')<>''
	END
	
	INSERT INTO @Result SELECT @MinRentStartDate, @MaxRentEndDate
	RETURN 
END


GO


