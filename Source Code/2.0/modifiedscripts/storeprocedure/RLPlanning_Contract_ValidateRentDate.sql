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

	--将所有租赁时间上有交集的Entity都先存起来--Begin
	SELECT * INTO #EntityTbl FROM Entity WHERE StoreOrDeptNo=@StoreOrDeptNo AND
	KioskNo=@KioskNo AND 
	EntityID<>@EntityID AND
	((@RentStartDate BETWEEN RentStartDate AND RentEndDate) OR 
	(@RentEndDate BETWEEN RentStartDate AND RentEndDate) OR
	(RentStartDate BETWEEN @RentStartDate AND @RentEndDate) OR
	(RentEndDate BETWEEN @RentStartDate AND @RentEndDate))
	--将所有租赁时间上有交集的Entity都先存起来--End
	
	--将临时表中的Entity相关的Contract无效的条目删除--Begin
	DELETE FROM #EntityTbl where #EntityTbl.ContractSnapshotID IN 
	(SELECT ContractSnapshotID FROM Contract WHERE SnapshotCreateTime IS NOT NULL OR
	Status<>'已生效')
	--将临时表中的Entity相关的Contract无效的条目删除--End
	
	--如果临时表中有数据，说明时间上有交集
	IF EXISTS(SELECT * FROM #EntityTbl)
	BEGIN
		SELECT CAST(0 AS BIT)	--有交集，验证不通过
	END
	ELSE
	BEGIN
		SELECT CAST(1 AS BIT)	--无交集
	END
	
	DROP TABLE #EntityTbl
END






GO


