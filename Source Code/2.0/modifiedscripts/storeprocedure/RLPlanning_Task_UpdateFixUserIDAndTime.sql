USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Task_UpdateFixUserIDAndTime]    Script Date: 07/23/2012 13:51:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Task_UpdateFixUserIDAndTime]
@TaskID nvarchar(50),
@FixUserID uniqueidentifier,
@FixTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Forecast_CheckResult SET FixUserID=@FixUserID, TaskFinishTime=@FixTime
	WHERE CheckID=@TaskID
END

GO


