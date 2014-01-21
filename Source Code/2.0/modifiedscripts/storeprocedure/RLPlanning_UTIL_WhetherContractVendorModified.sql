USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherContractVendorModified]    Script Date: 07/27/2012 14:49:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherContractVendorModified]
@ContractSnapshotID nvarchar(50),
@LastContractSnapshotID nvarchar(50),
@Result BIT OUTPUT	--0表示没变化，1表示有变化
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	
	--比较出租方数量是否相同
	DECLARE @VendorContractSum int, @LastVendorContractSum int
	SELECT @VendorContractSum=COUNT(*) FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID
	SELECT @LastVendorContractSum=COUNT(*) FROM VendorContract WHERE ContractSnapshotID=@LastContractSnapshotID
	--出租方数量不同，则表明有变动
	IF @VendorContractSum <> @LastVendorContractSum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--逐个比较出租方信息是否完全相同
	DECLARE @VendorContractCurosr CURSOR
	DECLARE @VendorNo nvarchar(50), @VendorName nvarchar(200), @PayMentType nvarchar(50), @IsVirtual BIT
	DECLARE @LastVendorName nvarchar(200), @LastPayMentType nvarchar(50), @LastIsVirtual BIT
	
	SET @VendorContractCurosr = CURSOR SCROLL FOR
	SELECT VendorNo, VendorName, PayMentType, IsVirtual FROM VendorContract
	WHERE ContractSnapshotID=@ContractSnapshotID
	
	
	OPEN @VendorContractCurosr
	FETCH NEXT FROM @VendorContractCurosr INTO @VendorNo, @VendorName, @PayMentType, @IsVirtual
	WHILE @@FETCH_STATUS=0
	BEGIN
		--NULL处理
		SET @VendorName=ISNULL(@VendorName,'')
		SET @PayMentType=ISNULL(@PayMentType,'')
	
		SELECT @LastVendorName=ISNULL(VendorName,''), @LastPayMentType=ISNULL(PayMentType,''), 
		@LastIsVirtual=IsVirtual FROM VendorContract 
		WHERE ContractSnapshotID=@LastContractSnapshotID AND VendorNo=@VendorNo
		
		--只要有一条记录不相同，则表明出租方有变动
		IF @LastVendorName<>@VendorName OR @LastPayMentType<>@PayMentType OR @LastIsVirtual<>@IsVirtual
		BEGIN
			CLOSE @VendorContractCurosr
			DEALLOCATE @VendorContractCurosr
			SET @Result = 1
			RETURN
		END
		
		FETCH NEXT FROM @VendorContractCurosr INTO @VendorNo, @VendorName, @PayMentType, @IsVirtual
	END
	CLOSE @VendorContractCurosr
	DEALLOCATE @VendorContractCurosr
END


GO


