USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_GLByStoreOrKioskNo]    Script Date: 08/09/2012 13:06:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*-------------------------------------------------------
Copyright(c) 2012 VIT��

����������
������ʶ��Create By 
 
�޸ı�ʶ��
�޸�������
------------------------------------------------------*/
CREATE PROCEDURE [dbo].[RLPlanning_Cal_GLByStoreOrKioskNo] 
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@CalEndDate	datetime,
	@UserOrGroupID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--����StoreNo��KioskNo��ȡ��FixedRule��RatioRule
	DECLARE @FixedRuleTbl TABLE(RuleSnapShotID nvarchar(50))
	DECLARE @RatioRuleTbl TABLE(RuleSnapShotID nvarchar(50)) -- ����ŵ���RatioRuleSnapShotID
	DECLARE @EntityIDTbl TABLE(EntityID nvarchar(50))	--����ŵ���EntityID, ��������Sales informationʹ��
	--����
	IF @KioskNo IS NULL OR @KioskNo=''
	BEGIN
		INSERT INTO @FixedRuleTbl
		SELECT FR.RuleSnapshotID FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		INNER JOIN EntityInfoSetting EI ON EI.EntityID = E.EntityID
		INNER JOIN FixedRuleSetting FR ON FR.EntityInfoSettingID = EI.EntityInfoSettingID
		WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo='')

		INSERT INTO @RatioRuleTbl
		SELECT RC.RuleSnapshotID FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		INNER JOIN EntityInfoSetting EI ON EI.EntityID = E.EntityID
		INNER JOIN RatioRuleSetting RR ON RR.EntityInfoSettingID = EI.EntityInfoSettingID
		INNER JOIN RatioCycleSetting RC ON RC.RatioID = RR.RatioID
		WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo='')
		
		INSERT INTO @EntityIDTbl
		SELECT DISTINCT E.EntityID FROM Entity E
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		WHERE E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo='')
	END
	
	--��Ʒ��
	ELSE
	BEGIN
		INSERT INTO @FixedRuleTbl
		SELECT FR.RuleSnapshotID FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		INNER JOIN EntityInfoSetting EI ON EI.EntityID = E.EntityID
		INNER JOIN FixedRuleSetting FR ON FR.EntityInfoSettingID = EI.EntityInfoSettingID
		WHERE E.StoreOrDeptNo = @StoreNo AND E.KioskNo = @KioskNo

		INSERT INTO @RatioRuleTbl
		SELECT RC.RuleSnapshotID FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		INNER JOIN EntityInfoSetting EI ON EI.EntityID = E.EntityID
		INNER JOIN RatioRuleSetting RR ON RR.EntityInfoSettingID = EI.EntityInfoSettingID
		INNER JOIN RatioCycleSetting RC ON RC.RatioID = RR.RatioID
		WHERE E.StoreOrDeptNo = @StoreNo AND E.KioskNo = @KioskNo
		
		INSERT INTO @EntityIDTbl
		SELECT DISTINCT E.EntityID FROM Entity E
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID AND C.SnapshotCreateTime IS NULL AND C.Status='����Ч'
		WHERE E.StoreOrDeptNo = @StoreNo AND E.KioskNo = @KioskNo
	END
	
	DECLARE @SnapShotID nvarchar(50)
	
	--Information������
	DECLARE @EntityIDCursor CURSOR, @EntityID nvarchar(50)
	SET @EntityIDCursor = CURSOR SCROLL FOR
	SELECT EntityID FROM @EntityIDTbl
	OPEN @EntityIDCursor
	FETCH NEXT FROM @EntityIDCursor INTO @EntityID
	WHILE @@FETCH_STATUS=0
	BEGIN
		--���ɰٷֱ����������©��Ϣ���
		EXEC dbo.RLPlanning_Cal_CheckSalesAndGenInfo @EntityID, @CalEndDate, @UserOrGroupID
		
		--��ͬ���޲�ȫ�ļ��
		EXEC dbo.RLPlanning_Cal_CheckContractAndGenInfo @EntityID, @CalEndDate, @UserOrGroupID
		
		FETCH NEXT FROM @EntityIDCursor INTO @EntityID
	END
	CLOSE @EntityIDCursor
	DEALLOCATE @EntityIDCursor

	
	--���������ͷֱ����
	
	--�̶����
	DECLARE @FixedSnapShotIDCursor CURSOR
	SET @FixedSnapShotIDCursor = CURSOR SCROLL FOR
	SELECT RuleSnapshotID FROM @FixedRuleTbl
	OPEN @FixedSnapShotIDCursor
	FETCH NEXT FROM @FixedSnapShotIDCursor INTO @SnapShotID
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC dbo.RLPlanning_Cal_FixedGL @SnapShotID, @CalEndDate
		FETCH NEXT FROM @FixedSnapShotIDCursor INTO @SnapShotID
	END
	CLOSE @FixedSnapShotIDCursor
	DEALLOCATE @FixedSnapShotIDCursor
	
	--�ٷֱ����
	DECLARE @RatioSnapShotIDCursor CURSOR
	SET @RatioSnapShotIDCursor = CURSOR SCROLL FOR
	SELECT RuleSnapshotID FROM @RatioRuleTbl
	OPEN @RatioSnapShotIDCursor
	FETCH NEXT FROM @RatioSnapShotIDCursor INTO @SnapShotID
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC dbo.RLPlanning_Cal_RatioGL @SnapShotID, @CalEndDate
		FETCH NEXT FROM @RatioSnapShotIDCursor INTO @SnapShotID
	END
	CLOSE @RatioSnapShotIDCursor
	DEALLOCATE @RatioSnapShotIDCursor
	
	--ֱ�����
	DECLARE @ZXSnapShotIDCursor CURSOR
	SET @ZXSnapShotIDCursor = CURSOR SCROLL FOR
	SELECT RuleSnapshotID FROM @FixedRuleTbl
	OPEN @ZXSnapShotIDCursor
	FETCH NEXT FROM @ZXSnapShotIDCursor INTO @SnapShotID
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC dbo.RLPlanning_Cal_ZXGL @SnapShotID, @CalEndDate
		FETCH NEXT FROM @ZXSnapShotIDCursor INTO @SnapShotID
	END
	CLOSE @ZXSnapShotIDCursor
	DEALLOCATE @ZXSnapShotIDCursor
END






GO


