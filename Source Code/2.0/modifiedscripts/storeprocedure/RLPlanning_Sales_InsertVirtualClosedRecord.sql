USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Sales_InsertVirtualClosedRecord]    Script Date: 07/12/2012 11:36:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Called by RLPlanning_Dispatch_Sales
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Sales_InsertVirtualClosedRecord]
	@Year INT,
	@Month INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--记录关帐信息，为了消除计算GL时候产生的上月未关帐错误--BEGIN
	DECLARE @ClosedMonth nvarchar(50) --格式是YYYYMM
	IF @Month<10
		SET @ClosedMonth=LTRIM(RTRIM(STR(@Year)))+'0'+LTRIM(RTRIM(STR(@Month)))
	ELSE
		SET @ClosedMonth=LTRIM(RTRIM(STR(@Year)))+LTRIM(RTRIM(STR(@Month)))
		
	IF NOT EXISTS (SELECT * FROM ClosedRecord WHERE ClosedMonth=@ClosedMonth)
	BEGIN
		INSERT INTO ClosedRecord VALUES(NEWID(), @ClosedMonth, NULL, 0)
	END
	--记录关帐信息，为了消除计算GL时候产生的上月未关帐错误--END
END


GO


