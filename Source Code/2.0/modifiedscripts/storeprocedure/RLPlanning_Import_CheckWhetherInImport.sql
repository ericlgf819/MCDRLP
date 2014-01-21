USE [RLPlanning_0904]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Import_CheckWhetherInImport]    Script Date: 09/05/2012 15:47:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Import_CheckWhetherInImport]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ResultTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	--����
	INSERT INTO @ResultTbl 
	SELECT StoreNo, KioskNo FROM RLPlanning_Import_EntityInImport 
	WHERE ISNULL(KioskNo,'') = '' 
	AND StoreNo IN (SELECT [�������] FROM Import_Sales WHERE ISNULL(Kiosk,'') = '')
	
	--��Ʒ��
	INSERT INTO @ResultTbl 
	SELECT StoreNo, KioskNo FROM RLPlanning_Import_EntityInImport 
	WHERE ISNULL(KioskNo,'') <> '' 
	AND KioskNo IN 
	(
		SELECT K.KioskNo FROM Import_Sales I 
		INNER JOIN SRLS_TB_Master_Kiosk K ON I.Kiosk = K.KioskName 
		WHERE ISNULL(I.Kiosk,'') <> ''
	)
	
	--��ʾ
	SELECT * FROM @ResultTbl
END

GO


