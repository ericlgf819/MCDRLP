USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Contract_ValidateRentDate]    Script Date: 07/01/2012 17:13:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		Eric
-- Create date: 2012/07/01
-- Description:	Validate the begin rent time and the end rent time of the entity
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Contract_ValidateRentDate]
(
	@EntityID nvarchar(50),
	@StoreOrDeptNo nvarchar(50),
	@KioskNo nvarchar(50),
	@RentStartDate datetime,
	@RentEndDate datetime
) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--����������ʱ�����н�����Entity���ȴ�����--Begin
	SELECT * INTO #EntityTbl FROM Entity WHERE StoreOrDeptNo=@StoreOrDeptNo AND
	KioskNo=@KioskNo AND 
	EntityID<>@EntityID AND
	((@RentStartDate BETWEEN RentStartDate AND RentEndDate) OR 
	(@RentEndDate BETWEEN RentStartDate AND RentEndDate) OR
	(RentStartDate BETWEEN @RentStartDate AND @RentEndDate) OR
	(RentEndDate BETWEEN @RentStartDate AND @RentEndDate))
	--����������ʱ�����н�����Entity���ȴ�����--End
	
	--����ʱ���е�Entity��ص�Contract��Ч����Ŀɾ��--Begin
	DELETE FROM #EntityTbl where #EntityTbl.ContractSnapshotID IN 
	(SELECT ContractSnapshotID FROM Contract WHERE SnapshotCreateTime IS NOT NULL OR
	Status<>'����Ч')
	--����ʱ���е�Entity��ص�Contract��Ч����Ŀɾ��--End
	
	--�����ʱ���������ݣ�˵��ʱ�����н���
	IF EXISTS(SELECT * FROM #EntityTbl)
	BEGIN
		SELECT CAST(0 AS BIT)	--�н�������֤��ͨ��
	END
	ELSE
	BEGIN
		SELECT CAST(1 AS BIT)	--�޽���
	END
	
	DROP TABLE #EntityTbl
END






GO


