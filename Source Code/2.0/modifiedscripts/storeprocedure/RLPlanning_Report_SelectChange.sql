USE [RLPlanning_0904]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Report_SelectChange]    Script Date: 09/05/2012 16:33:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Report_SelectChange]
@AreaID nvarchar(50),
@CompanyCode nvarchar(50),
@StoreOrKioskNo nvarchar(50),
@StartDate date,
@EndDate date,
@ChangeType nvarchar(50),
@PageIndex int,
@PageSize int=50,
@RecordCount int=0 OUTPUT,
@Debug nvarchar(50)=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--因为@EndDate的类型是date，而合同修改日期的类型是datetime，所以为了能够查询当天的记录，必须将@EndDate加1天
	SET @EndDate = DATEADD(DAY,1,@EndDate)

	--先将合同都筛选出来存储在临时表中
	DECLARE @ContractTbl TABLE(ContractSnapshotID nvarchar(50), ContractNo nvarchar(50), 
								AreaName nvarchar(50), CompanyName nvarchar(50),
								LastModifyUserName nvarchar(50), LastModifyTime datetime)
	
	IF @Debug IS NOT NULL
	BEGIN
		INSERT INTO @ContractTbl
		SELECT DISTINCT C.ContractSnapshotID, C.ContractNO, A.AreaName, 
		CM.CompanyName, C.LastModifyUserName, C.LastModifyTime 
		FROM Contract C INNER JOIN SRLS_TB_Master_Company CM ON C.CompanyCode=CM.CompanyCode
		LEFT JOIN SRLS_TB_Master_Area A ON A.ID = CM.AreaID
		WHERE C.ContractSnapshotID=@Debug
	END
	ELSE
	BEGIN
		--根据CompanyCode或者AreaID和StartDate与EndDate来筛选合同
		--优先看CompanyCode
		IF ISNULL(@CompanyCode,'') <> ''
		BEGIN
			INSERT INTO @ContractTbl
			SELECT DISTINCT C.ContractSnapshotID, C.ContractNO, A.AreaName, 
			CM.CompanyName, C.LastModifyUserName, C.LastModifyTime 
			FROM Contract C INNER JOIN SRLS_TB_Master_Company CM ON C.CompanyCode=CM.CompanyCode
			LEFT JOIN SRLS_TB_Master_Area A ON A.ID = CM.AreaID
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
			AND C.CompanyCode=@CompanyCode
			AND C.LastModifyTime BETWEEN @StartDate AND @EndDate	
		END
		ELSE IF ISNULL(@AreaID,'') <> ''
		BEGIN
			INSERT INTO @ContractTbl
			SELECT DISTINCT C.ContractSnapshotID, C.ContractNO, A.AreaName, 
			CM.CompanyName, C.LastModifyUserName, C.LastModifyTime 
			FROM Contract C INNER JOIN SRLS_TB_Master_Company CM ON C.CompanyCode=CM.CompanyCode
			LEFT JOIN SRLS_TB_Master_Area A ON A.ID = CM.AreaID
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
			AND CM.AreaID=@AreaID
			AND C.LastModifyTime BETWEEN @StartDate AND @EndDate	
		END
		ELSE
		BEGIN
			INSERT INTO @ContractTbl
			SELECT DISTINCT C.ContractSnapshotID, C.ContractNO, A.AreaName, 
			CM.CompanyName, C.LastModifyUserName, C.LastModifyTime 
			FROM Contract C INNER JOIN SRLS_TB_Master_Company CM ON C.CompanyCode=CM.CompanyCode
			LEFT JOIN SRLS_TB_Master_Area A ON A.ID = CM.AreaID
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
			AND C.LastModifyTime BETWEEN @StartDate AND @EndDate
		END
		

		
		--如果指定了某个餐厅或者甜品店，则需要将合同中没有该餐厅或者甜品店的合同删除
		IF ISNULL(@StoreOrKioskNo, '')<>''
		BEGIN
			DELETE @ContractTbl WHERE ContractSnapshotID NOT IN 
			(SELECT CT.ContractSnapshotID FROM Entity E 
			INNER JOIN @ContractTbl CT ON E.ContractSnapshotID=CT.ContractSnapshotID
			WHERE E.StoreOrDeptNo LIKE '%'+@StoreOrKioskNo+'%' OR E.KioskNo LIKE '%'+@StoreOrKioskNo+'%')
		END
	END
	
	--声明临时表，用来存储合同的变更信息
	DECLARE @ContractChangeTbl 
	TABLE(ContractSnapshotID nvarchar(50), ChangeInfo nvarchar(50), EntityInfoSettingID nvarchar(50))
	--声明临时表，来存储要查找的变更条件
	DECLARE @ChangeTypeTbl TABLE(ChangeType nvarchar(50))
	
	--如果ChangeType是空，说明所有的条件都要搜索
	IF ISNULL(@ChangeType,'')=''
	BEGIN
		INSERT INTO @ChangeTypeTbl SELECT '合同新增'
		INSERT INTO @ChangeTypeTbl SELECT '实体'
		INSERT INTO @ChangeTypeTbl SELECT '出租方'
		INSERT INTO @ChangeTypeTbl SELECT '固定租金'
		INSERT INTO @ChangeTypeTbl SELECT '固定管理费'
		INSERT INTO @ChangeTypeTbl SELECT '百分比服务费'
		INSERT INTO @ChangeTypeTbl SELECT '百分比管理费'
		INSERT INTO @ChangeTypeTbl SELECT '百分比租金'
	END
	ELSE
		INSERT INTO @ChangeTypeTbl SELECT @ChangeType
	
	--根据ChangeType来查找变更情况，将结果存入@ContractChangeTbl中
	DECLARE @Result INT
	DECLARE @ContractCursor CURSOR
	DECLARE @ContractSnapshotID nvarchar(50), @LastContractSnapshotID nvarchar(50)
	DECLARE @EntityInfoSettingID nvarchar(50), @LastEntityInfoSettingID nvarchar(50)
	DECLARE @EntityInfoSettingCursor CURSOR
	DECLARE @ChangeTypeCursor CURSOR
	
	SET @ContractCursor = CURSOR SCROLL FOR SELECT ContractSnapshotID FROM @ContractTbl
	OPEN @ContractCursor
	FETCH NEXT FROM @ContractCursor INTO @ContractSnapshotID
	WHILE @@FETCH_STATUS=0
	BEGIN
		--获取对应合同的上一个版本的snapshotid
		EXEC RLPlanning_UTIL_GetContractLastVersionSnapshotID @ContractSnapshotID, @LastContractSnapshotID OUTPUT
		
		--新增合同 -- Begin
		IF EXISTS(SELECT * FROM @ChangeTypeTbl WHERE ChangeType = '合同新增')
		BEGIN
			IF EXISTS(
						SELECT * FROM Contract WHERE ContractSnapshotID = @ContractSnapshotID 
						AND (PartComment LIKE '%新%' OR PartComment = '续租') --'新增'是通过导入，'新建'是通过程序建立的
						AND SnapshotCreateTime IS NULL
					 )
			BEGIN
				INSERT INTO @ContractChangeTbl
				SELECT @ContractSnapshotID, '合同新增', ''
			END
		END
		--新增合同 -- End
		
		--没有上一个版本，则继续循环
		IF @LastContractSnapshotID IS NULL
		BEGIN
			FETCH NEXT FROM @ContractCursor INTO @ContractSnapshotID
			CONTINUE
		END
		
		--根据ChangeTypeTbl中的内容来执行
		SET @ChangeTypeCursor = CURSOR SCROLL FOR SELECT ChangeType FROM @ChangeTypeTbl
		OPEN @ChangeTypeCursor
		FETCH NEXT FROM @ChangeTypeCursor INTO @ChangeType
		WHILE @@FETCH_STATUS=0
		BEGIN
			IF @ChangeType='实体'
			BEGIN
				EXEC RLPlanning_UTIL_WhetherContractEntityModified @ContractSnapshotID, @LastContractSnapshotID, @Result OUTPUT
				IF @Result=1
				BEGIN
					INSERT INTO @ContractChangeTbl
					SELECT @ContractSnapshotID, '实体变更', ''
				END
			END
			ELSE IF @ChangeType='出租方'
			BEGIN
				EXEC RLPlanning_UTIL_WhetherContractVendorModified @ContractSnapshotID, @LastContractSnapshotID, @Result OUTPUT
				IF @Result=1
				BEGIN
					INSERT INTO @ContractChangeTbl
					SELECT @ContractSnapshotID, '出租方变更', ''
				END		
			END
			--剩下的5中情况都需要先获取相关的EntitiyInfoSettingID
			ELSE
			BEGIN
				SET @EntityInfoSettingCursor = CURSOR SCROLL FOR
				SELECT EntityInfoSettingID, LastEntityInfoSettingID FROM 
				dbo.RLPlanning_FN_GetEntityInfosettingWithSameContent(@ContractSnapshotID, @LastContractSnapshotID)
				
				OPEN @EntityInfoSettingCursor
				FETCH NEXT FROM @EntityInfoSettingCursor INTO @EntityInfoSettingID, @LastEntityInfoSettingID
				WHILE @@FETCH_STATUS=0
				BEGIN
					IF @ChangeType='固定租金'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherFixedRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'变更', @EntityInfoSettingID
						END	
					END
					ELSE IF @ChangeType='固定管理费'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherFixedRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'变更', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='百分比服务费'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'变更', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='百分比管理费'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'变更', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='百分比租金'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'变更', @EntityInfoSettingID
						END					
					END
					
					FETCH NEXT FROM @EntityInfoSettingCursor INTO @EntityInfoSettingID, @LastEntityInfoSettingID
				END
				CLOSE @EntityInfoSettingCursor
				DEALLOCATE @EntityInfoSettingCursor
			END
			
			FETCH NEXT FROM @ChangeTypeCursor INTO @ChangeType
		END
		CLOSE @ChangeTypeCursor
		DEALLOCATE @ChangeTypeCursor
		
		FETCH NEXT FROM @ContractCursor INTO @ContractSnapshotID
	END
	CLOSE @ContractCursor
	DEALLOCATE @ContractCursor
	
	--记录总行数
	SELECT @RecordCount=COUNT(*) FROM @ContractTbl CT 
	INNER JOIN @ContractChangeTbl CC ON CT.ContractSnapshotID=CC.ContractSnapshotID
	LEFT JOIN EntityInfoSetting EI ON CC.EntityInfoSettingID = EI.EntityInfoSettingID
	LEFT JOIN Entity E ON EI.EntityID = E.EntityID;
	
	--将结果组织成报表格式
	WITH Tmp AS(
		SELECT CT.ContractNo AS ContractNo, CT.AreaName AS AreaName, CT.CompanyName AS CompanyName,
		E.StoreOrDeptNo AS StoreNo, E.KioskNo AS KioskNo, E.RentEndDate AS RentEndDate,
		CC.ChangeInfo AS ChangeInfo, CT.LastModifyUserName AS LastModifyUserName, CT.LastModifyTime AS LastModifyTime,
		ROW_NUMBER() OVER (ORDER BY CT.ContractNo) AS RowIndex
		FROM @ContractTbl CT INNER JOIN @ContractChangeTbl CC ON CT.ContractSnapshotID=CC.ContractSnapshotID
		LEFT JOIN EntityInfoSetting EI ON CC.EntityInfoSettingID = EI.EntityInfoSettingID
		LEFT JOIN Entity E ON EI.EntityID = E.EntityID
	)SELECT * FROM Tmp WHERE RowIndex BETWEEN @PageIndex * @PageSize + 1 AND (@PageIndex + 1) * @PageSize
	 ORDER BY LastModifyTime DESC
END



GO


