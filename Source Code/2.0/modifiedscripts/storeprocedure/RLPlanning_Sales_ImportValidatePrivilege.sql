-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE RLPlanning_Sales_ImportValidatePrivilege
@UserID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION MYTRANSACTION
	BEGIN TRY
		--首先，将没有权限的记录筛选出来
		SELECT * FROM Import_SalesValidation
		WHERE Company NOT IN (SELECT CompanyCode FROM RLP_UserCompany WHERE UserId = @UserID)
		
		--最后，将没有权限的记录给删除，并且把正确的记录返回
		DELETE FROM Import_SalesValidation
		WHERE Company NOT IN (SELECT CompanyCode FROM RLP_UserCompany WHERE UserId = @UserID)
		
		SELECT * FROM Import_SalesValidation
		
		IF @@TRANCOUNT <> 0
			COMMIT TRANSACTION MYTRANSACTION
	END TRY
	BEGIN CATCH
	    IF @@TRANCOUNT <> 0
			ROLLBACK TRANSACTION;
				
	    DECLARE @MSG NVARCHAR(max)
	    SELECT @MSG=ERROR_MESSAGE()
	    --写日志
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'验证导入合同权限',NULL,NULL,@MSG,GETDATE(),NULL,NULL)
	END CATCH
END
GO
