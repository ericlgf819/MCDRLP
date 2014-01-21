USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherContractEntityModified]    Script Date: 07/27/2012 15:12:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherContractEntityModified] 
@ContractSnapshotID nvarchar(50),
@LastContractSnapshotID nvarchar(50),
@Result BIT OUTPUT	--0表示没变化，1表示有变化
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	
	--比较Entity数量是否相同
	DECLARE @EntitySum INT, @LastEntitySum INT
	SELECT @EntitySum=COUNT(*) FROM Entity WHERE ContractSnapshotID=@ContractSnapshotID
	SELECT @LastEntitySum=COUNT(*) FROM Entity WHERE ContractSnapshotID=@LastContractSnapshotID
	
	--如果Entity数量不相同，表明实体有变化
	IF @EntitySum<>@LastEntitySum
	BEGIN
		SET @Result = 1
		RETURN
	END
	
	--Entity数量相同，则需要逐条比较内容
	DECLARE @EntityCursor CURSOR
	SET @EntityCursor=CURSOR SCROLL FOR
	SELECT EntityTypeName, EntityNo, EntityName, StoreOrDept, StoreOrDeptNo, KioskNo,
	OpeningDate, RentStartDate, RentEndDate, IsCalculateAP, APStartDate, Remark 
	FROM Entity WHERE ContractSnapshotID=@ContractSnapshotID
	
	DECLARE @EntityTypeName nvarchar(50), @EntityNo nvarchar(50), @EntityName nvarchar(50),
	@StoreOrDept nvarchar(50), @StoreOrDeptNo nvarchar(50), @KioskNo nvarchar(50),
	@OpeningDate datetime, @RentStartDate datetime, @RentEndDate datetime, @IsCalculateAP bit,
	@APStartDate datetime, @Remark nvarchar(2000)
	
	DECLARE @LastEntityTypeName nvarchar(50), @LastEntityNo nvarchar(50), @LastEntityName nvarchar(50),
	@LastStoreOrDept nvarchar(50), @LastOpeningDate datetime, @LastRentStartDate datetime,
	@LastRentEndDate datetime, @LastIsCalculateAP bit, @LastAPStartDate datetime, @LastRemark nvarchar(2000)
	
	OPEN @EntityCursor
	FETCH NEXT FROM @EntityCursor INTO @EntityTypeName, @EntityNo, @EntityName,
	@StoreOrDept, @StoreOrDeptNo, @KioskNo, @OpeningDate, @RentStartDate, 
	@RentEndDate, @IsCalculateAP, @APStartDate, @Remark
	WHILE @@FETCH_STATUS=0
	BEGIN
		--NULL处理
		SET @EntityTypeName = ISNULL(@EntityTypeName, '')
		SET @EntityNo = ISNULL(@EntityNo, '')
		SET @EntityName = ISNULL(@EntityName, '')
		SET @StoreOrDept = ISNULL(@StoreOrDept, '')
		SET @StoreOrDeptNo = ISNULL(@StoreOrDeptNo, '')
		SET @KioskNo = ISNULL(@KioskNo, '')
		SET @OpeningDate = ISNULL(@OpeningDate, '')
		SET @RentStartDate = ISNULL(@RentStartDate, '')
		SET @RentEndDate = ISNULL(@RentEndDate, '')
		SET @IsCalculateAP = ISNULL(@IsCalculateAP, '')
		SET @APStartDate = ISNULL(@APStartDate, '')
		SET @Remark = ISNULL(@Remark, '')
		
		--餐厅
		IF @KioskNo=''
		BEGIN
			SELECT @LastEntityTypeName=ISNULL(EntityTypeName,''), @LastEntityNo=ISNULL(EntityNo,''),
			@LastEntityName=ISNULL(EntityName,''), @LastStoreOrDept=ISNULL(StoreOrDept,''),
			@LastOpeningDate=ISNULL(OpeningDate,''), @LastRentStartDate=ISNULL(RentStartDate,''),
			@LastRentEndDate=ISNULL(RentEndDate,''), @LastIsCalculateAP=ISNULL(IsCalculateAP,''),
			@LastAPStartDate=ISNULL(APStartDate,''), @LastRemark=ISNULL(Remark,'')
			FROM Entity WHERE ContractSnapshotID=@LastContractSnapshotID
			AND StoreOrDeptNo=@StoreOrDeptNo AND (KioskNo='' OR KioskNo IS NULL)
		END
		--甜品店
		ELSE
		BEGIN
			SELECT @LastEntityTypeName=ISNULL(EntityTypeName,''), @LastEntityNo=ISNULL(EntityNo,''),
			@LastEntityName=ISNULL(EntityName,''), @LastStoreOrDept=ISNULL(StoreOrDept,''),
			@LastOpeningDate=ISNULL(OpeningDate,''), @LastRentStartDate=ISNULL(RentStartDate,''),
			@LastRentEndDate=ISNULL(RentEndDate,''), @LastIsCalculateAP=ISNULL(IsCalculateAP,''),
			@LastAPStartDate=ISNULL(APStartDate,''), @LastRemark=ISNULL(Remark,'')
			FROM Entity WHERE ContractSnapshotID=@LastContractSnapshotID
			AND StoreOrDeptNo=@StoreOrDeptNo AND KioskNo=@KioskNo
		END
		
		--有一条不一样，则表明有变动
		IF @LastEntityTypeName<>@EntityTypeName OR @LastEntityNo<>@EntityNo OR @LastEntityName<>@EntityName
		OR @LastStoreOrDept<>@StoreOrDept OR @LastOpeningDate<>@OpeningDate OR @LastRentStartDate<>@RentStartDate
		OR @LastRentEndDate<>@RentEndDate OR @LastIsCalculateAP<>@IsCalculateAP OR @LastAPStartDate<>@APStartDate
		OR @LastRemark<>@Remark
		BEGIN
			CLOSE @EntityCursor
			DEALLOCATE @EntityCursor
			SET @Result = 1
			RETURN
		END
	
		FETCH NEXT FROM @EntityCursor INTO @EntityTypeName, @EntityNo, @EntityName,
		@StoreOrDept, @StoreOrDeptNo, @KioskNo, @OpeningDate, @RentStartDate, 
		@RentEndDate, @IsCalculateAP, @APStartDate, @Remark
	END
	CLOSE @EntityCursor
	DEALLOCATE @EntityCursor
END

GO


