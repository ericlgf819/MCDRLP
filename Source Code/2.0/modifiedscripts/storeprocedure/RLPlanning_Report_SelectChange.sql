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
	
	--��Ϊ@EndDate��������date������ͬ�޸����ڵ�������datetime������Ϊ���ܹ���ѯ����ļ�¼�����뽫@EndDate��1��
	SET @EndDate = DATEADD(DAY,1,@EndDate)

	--�Ƚ���ͬ��ɸѡ�����洢����ʱ����
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
		--����CompanyCode����AreaID��StartDate��EndDate��ɸѡ��ͬ
		--���ȿ�CompanyCode
		IF ISNULL(@CompanyCode,'') <> ''
		BEGIN
			INSERT INTO @ContractTbl
			SELECT DISTINCT C.ContractSnapshotID, C.ContractNO, A.AreaName, 
			CM.CompanyName, C.LastModifyUserName, C.LastModifyTime 
			FROM Contract C INNER JOIN SRLS_TB_Master_Company CM ON C.CompanyCode=CM.CompanyCode
			LEFT JOIN SRLS_TB_Master_Area A ON A.ID = CM.AreaID
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
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
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
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
			WHERE C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
			AND C.LastModifyTime BETWEEN @StartDate AND @EndDate
		END
		

		
		--���ָ����ĳ������������Ʒ�꣬����Ҫ����ͬ��û�иò���������Ʒ��ĺ�ͬɾ��
		IF ISNULL(@StoreOrKioskNo, '')<>''
		BEGIN
			DELETE @ContractTbl WHERE ContractSnapshotID NOT IN 
			(SELECT CT.ContractSnapshotID FROM Entity E 
			INNER JOIN @ContractTbl CT ON E.ContractSnapshotID=CT.ContractSnapshotID
			WHERE E.StoreOrDeptNo LIKE '%'+@StoreOrKioskNo+'%' OR E.KioskNo LIKE '%'+@StoreOrKioskNo+'%')
		END
	END
	
	--������ʱ�������洢��ͬ�ı����Ϣ
	DECLARE @ContractChangeTbl 
	TABLE(ContractSnapshotID nvarchar(50), ChangeInfo nvarchar(50), EntityInfoSettingID nvarchar(50))
	--������ʱ�����洢Ҫ���ҵı������
	DECLARE @ChangeTypeTbl TABLE(ChangeType nvarchar(50))
	
	--���ChangeType�ǿգ�˵�����е�������Ҫ����
	IF ISNULL(@ChangeType,'')=''
	BEGIN
		INSERT INTO @ChangeTypeTbl SELECT '��ͬ����'
		INSERT INTO @ChangeTypeTbl SELECT 'ʵ��'
		INSERT INTO @ChangeTypeTbl SELECT '���ⷽ'
		INSERT INTO @ChangeTypeTbl SELECT '�̶����'
		INSERT INTO @ChangeTypeTbl SELECT '�̶������'
		INSERT INTO @ChangeTypeTbl SELECT '�ٷֱȷ����'
		INSERT INTO @ChangeTypeTbl SELECT '�ٷֱȹ����'
		INSERT INTO @ChangeTypeTbl SELECT '�ٷֱ����'
	END
	ELSE
		INSERT INTO @ChangeTypeTbl SELECT @ChangeType
	
	--����ChangeType�����ұ����������������@ContractChangeTbl��
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
		--��ȡ��Ӧ��ͬ����һ���汾��snapshotid
		EXEC RLPlanning_UTIL_GetContractLastVersionSnapshotID @ContractSnapshotID, @LastContractSnapshotID OUTPUT
		
		--������ͬ -- Begin
		IF EXISTS(SELECT * FROM @ChangeTypeTbl WHERE ChangeType = '��ͬ����')
		BEGIN
			IF EXISTS(
						SELECT * FROM Contract WHERE ContractSnapshotID = @ContractSnapshotID 
						AND (PartComment LIKE '%��%' OR PartComment = '����') --'����'��ͨ�����룬'�½�'��ͨ����������
						AND SnapshotCreateTime IS NULL
					 )
			BEGIN
				INSERT INTO @ContractChangeTbl
				SELECT @ContractSnapshotID, '��ͬ����', ''
			END
		END
		--������ͬ -- End
		
		--û����һ���汾�������ѭ��
		IF @LastContractSnapshotID IS NULL
		BEGIN
			FETCH NEXT FROM @ContractCursor INTO @ContractSnapshotID
			CONTINUE
		END
		
		--����ChangeTypeTbl�е�������ִ��
		SET @ChangeTypeCursor = CURSOR SCROLL FOR SELECT ChangeType FROM @ChangeTypeTbl
		OPEN @ChangeTypeCursor
		FETCH NEXT FROM @ChangeTypeCursor INTO @ChangeType
		WHILE @@FETCH_STATUS=0
		BEGIN
			IF @ChangeType='ʵ��'
			BEGIN
				EXEC RLPlanning_UTIL_WhetherContractEntityModified @ContractSnapshotID, @LastContractSnapshotID, @Result OUTPUT
				IF @Result=1
				BEGIN
					INSERT INTO @ContractChangeTbl
					SELECT @ContractSnapshotID, 'ʵ����', ''
				END
			END
			ELSE IF @ChangeType='���ⷽ'
			BEGIN
				EXEC RLPlanning_UTIL_WhetherContractVendorModified @ContractSnapshotID, @LastContractSnapshotID, @Result OUTPUT
				IF @Result=1
				BEGIN
					INSERT INTO @ContractChangeTbl
					SELECT @ContractSnapshotID, '���ⷽ���', ''
				END		
			END
			--ʣ�µ�5���������Ҫ�Ȼ�ȡ��ص�EntitiyInfoSettingID
			ELSE
			BEGIN
				SET @EntityInfoSettingCursor = CURSOR SCROLL FOR
				SELECT EntityInfoSettingID, LastEntityInfoSettingID FROM 
				dbo.RLPlanning_FN_GetEntityInfosettingWithSameContent(@ContractSnapshotID, @LastContractSnapshotID)
				
				OPEN @EntityInfoSettingCursor
				FETCH NEXT FROM @EntityInfoSettingCursor INTO @EntityInfoSettingID, @LastEntityInfoSettingID
				WHILE @@FETCH_STATUS=0
				BEGIN
					IF @ChangeType='�̶����'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherFixedRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'���', @EntityInfoSettingID
						END	
					END
					ELSE IF @ChangeType='�̶������'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherFixedRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'���', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='�ٷֱȷ����'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'���', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='�ٷֱȹ����'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'���', @EntityInfoSettingID
						END					
					END
					ELSE IF @ChangeType='�ٷֱ����'
					BEGIN
						EXEC RLPlanning_UTIL_WhetherRatioRuleModified @EntityInfoSettingID, @LastEntityInfoSettingID, @ChangeType, @Result OUTPUT
						IF @Result=1
						BEGIN
							INSERT INTO @ContractChangeTbl
							SELECT @ContractSnapshotID, @ChangeType+'���', @EntityInfoSettingID
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
	
	--��¼������
	SELECT @RecordCount=COUNT(*) FROM @ContractTbl CT 
	INNER JOIN @ContractChangeTbl CC ON CT.ContractSnapshotID=CC.ContractSnapshotID
	LEFT JOIN EntityInfoSetting EI ON CC.EntityInfoSettingID = EI.EntityInfoSettingID
	LEFT JOIN Entity E ON EI.EntityID = E.EntityID;
	
	--�������֯�ɱ����ʽ
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


