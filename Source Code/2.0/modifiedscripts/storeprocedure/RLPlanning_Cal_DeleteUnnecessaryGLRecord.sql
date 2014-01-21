USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_DeleteUnnecessaryGLRecord]    Script Date: 08/07/2012 15:57:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_DeleteUnnecessaryGLRecord]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--删除已经没有合同的GLRecord
	DECLARE @GLRecordTbl TABLE(GLRecordID nvarchar(50))
	INSERT INTO @GLRecordTbl
	SELECT DISTINCT GLRecordID FROM GLRecord 
	WHERE ContractSnapshotID NOT IN (SELECT ContractSnapshotID FROM Contract)
	
	DECLARE @GLRecordCursor CURSOR, @GLRecordID nvarchar(50)
	SET @GLRecordCursor = CURSOR SCROLL FOR SELECT GLRecordID FROM @GLRecordTbl
	
	OPEN @GLRecordCursor
	FETCH NEXT FROM @GLRecordCursor INTO @GLRecordID
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC dbo.usp_Cal_DeleteOneGLRecord @GLRecordID
		FETCH NEXT FROM @GLRecordCursor INTO @GLRecordID
	END
	CLOSE @GLRecordCursor
	DEALLOCATE @GLRecordCursor
END

GO


