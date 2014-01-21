USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Cal_DeleteOneAPRecord]    Script Date: 08/10/2012 13:47:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	É¾³ýÒ»¸öReocrd¼ÇÂ¼
-- =============================================
CREATE  PROCEDURE [dbo].[usp_Cal_DeleteOneAPRecord]
	-- Add the parameters for the stored procedure here
	@APRecordID NVARCHAR(50)
AS
BEGIN
	DELETE FROM APStoreUpCloseSalse WHERE APRecordID = @APRecordID
	DELETE FROM APProcessParameterValue WHERE APRecordID = @APRecordID
	DELETE FROM APMessageResult WHERE APRecordID = @APRecordID
	DELETE FROM APCheckResult WHERE APRecordID = @APRecordID
	DELETE FROM APCertificate WHERE APRecordID = @APRecordID
	DELETE FROM APRecord WHERE APRecordID = @APRecordID
	
	--Commented by Eric--Begin
	--DECLARE @ProcID NVARCHAR(50)
	--SELECT @ProcID=ProcID FROM dbo.WF_Proc_Inst WHERE AppCode=14 AND DataLocator=@APRecordID
	--DELETE FROM dbo.WF_Work_Items WHERE ProcID=@ProcID
	--DELETE FROM dbo.WF_Proc_Inst WHERE ProcID=@ProcID 
	--Commented by Eric--End
END
















GO


