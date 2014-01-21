USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Cal_DeleteOneGLRecord]    Script Date: 08/04/2012 10:25:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	É¾³ýÒ»¸öReocrd¼ÇÂ¼
-- =============================================
CREATE  PROCEDURE [dbo].[usp_Cal_DeleteOneGLRecord]
	-- Add the parameters for the stored procedure here
	@GLRecordID NVARCHAR(50)
AS
BEGIN
	DELETE FROM GLException WHERE GLRecordID = @GLRecordID
	DELETE FROM GLProcessParameterValue WHERE GLRecordID = @GLRecordID
	DELETE FROM GLMessageResult WHERE GLRecordID = @GLRecordID
	DELETE FROM GLCheckResult WHERE GLRecordID = @GLRecordID
	DELETE FROM GLCertificate WHERE GLRecordID = @GLRecordID
	DELETE FROM GLTimeIntervalInfo WHERE GLRecordID = @GLRecordID
	DELETE FROM GLRecord WHERE GLRecordID = @GLRecordID
	
	--Commented by Eric
	--DECLARE @ProcID NVARCHAR(50)
	--SELECT @ProcID=ProcID FROM dbo.WF_Proc_Inst WHERE AppCode=13 AND DataLocator=@GLRecordID
	--DELETE FROM dbo.WF_Work_Items WHERE ProcID=@ProcID
	--DELETE FROM dbo.WF_Proc_Inst WHERE ProcID=@ProcID 
END
















GO


