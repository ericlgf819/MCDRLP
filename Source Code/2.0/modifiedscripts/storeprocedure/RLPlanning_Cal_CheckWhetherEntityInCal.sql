USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_CheckWhetherEntityInCal]    Script Date: 09/04/2012 10:43:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_CheckWhetherEntityInCal]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ResultTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	--����
	INSERT INTO @ResultTbl 
	SELECT StoreNo, KioskNo FROM RLPlanning_Cal_TmpTbl 
	WHERE ISNULL(KioskNo,'') = '' 
	AND StoreNo IN (SELECT StoreNo FROM RLPlanning_Cal_EntityInCal WHERE ISNULL(KioskNo,'') = '')
	
	--��Ʒ��
	INSERT INTO @ResultTbl 
	SELECT StoreNo, KioskNo FROM RLPlanning_Cal_TmpTbl 
	WHERE ISNULL(KioskNo,'') <> '' 
	AND KioskNo IN (SELECT KioskNo FROM RLPlanning_Cal_EntityInCal WHERE ISNULL(KioskNo,'') <> '')
	
	--��ʾ
	SELECT * FROM @ResultTbl
END

GO


