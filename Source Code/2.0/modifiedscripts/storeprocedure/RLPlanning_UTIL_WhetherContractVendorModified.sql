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
@Result BIT OUTPUT	--0��ʾû�仯��1��ʾ�б仯
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	
	--�Ƚϳ��ⷽ�����Ƿ���ͬ
	DECLARE @VendorContractSum int, @LastVendorContractSum int
	SELECT @VendorContractSum=COUNT(*) FROM VendorContract WHERE ContractSnapshotID=@ContractSnapshotID
	SELECT @LastVendorContractSum=COUNT(*) FROM VendorContract WHERE ContractSnapshotID=@LastContractSnapshotID
	--���ⷽ������ͬ��������б䶯
	IF @VendorContractSum <> @LastVendorContractSum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--����Ƚϳ��ⷽ��Ϣ�Ƿ���ȫ��ͬ
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
		--NULL����
		SET @VendorName=ISNULL(@VendorName,'')
		SET @PayMentType=ISNULL(@PayMentType,'')
	
		SELECT @LastVendorName=ISNULL(VendorName,''), @LastPayMentType=ISNULL(PayMentType,''), 
		@LastIsVirtual=IsVirtual FROM VendorContract 
		WHERE ContractSnapshotID=@LastContractSnapshotID AND VendorNo=@VendorNo
		
		--ֻҪ��һ����¼����ͬ����������ⷽ�б䶯
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


