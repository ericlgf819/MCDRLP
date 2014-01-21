USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Contract_DeleteContract]    Script Date: 08/07/2012 16:03:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- ========================================
-- Author:     ZHANGBSH
-- Create Date:20110118
-- Description:删除合同
-- ========================================
CREATE PROCEDURE [dbo].[usp_Contract_DeleteContract]
	@ContractSnapshotID nvarchar(50),
	@CurrentUserID NVARCHAR(50)
AS

SET NOCOUNT ON
BEGIN
	EXEC usp_Contract_DeleteSingleContract @ContractSnapshotID,@CurrentUserID
	
	--Added By Eric --Begin
	--删除该合同相关GLRecord
	DECLARE @GLRecordTbl TABLE(GLRecordID nvarchar(50))
	INSERT INTO @GLRecordTbl
	SELECT DISTINCT GLRecordID FROM GLRecord 
	WHERE ContractSnapshotID = @ContractSnapshotID
	
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
	--Added By Eric --End
END













GO


