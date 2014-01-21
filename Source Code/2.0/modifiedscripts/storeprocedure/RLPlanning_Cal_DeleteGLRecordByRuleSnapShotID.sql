USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_DeleteGLRecordByRuleSnapShotID]    Script Date: 08/03/2012 15:31:45 ******/
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
CREATE PROCEDURE [dbo].[RLPlanning_Cal_DeleteGLRecordByRuleSnapShotID]
@RuleSnapShotID nvarchar(50),
@IsZXType bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @GLRecordIDTbl TABLE(GLRecordID nvarchar(50))
	
	--�����ͬһ�ݺ�ͬ���¼��㣬��Ҫ����ͬRuleSnapshotID��GLRecord��ɾ��--Begin
	IF @IsZXType=1
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord WHERE RuleSnapShotID=@RuleSnapShotID AND FromSRLS=0 AND GLType='ֱ�����'
	END
	ELSE
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord WHERE RuleSnapShotID=@RuleSnapShotID AND FromSRLS=0
	END
	--�����ͬһ�ݺ�ͬ���¼��㣬��Ҫ����ͬRuleSnapshotID��GLRecord��ɾ��--End
	
	--���Ҫ����ĺ�ͬ����ʷ�汾��������ʷ�汾��������ϵͳ�в����ģ�����Ҫ����ʷ�汾��Ӧ��GLRecord��ɾ��--Begin
	DECLARE @RuleID nvarchar(50)
	
	--�Ȼ�ȡ��������RuleID
	--�̶�
	IF EXISTS (SELECT * FROM FixedRuleSetting WHERE RuleSnapshotID = @RuleSnapShotID)
		SELECT @RuleID=RuleID FROM FixedRuleSetting WHERE RuleSnapshotID = @RuleSnapShotID
	--�ٷֱ�
	ELSE
		SELECT @RuleID=RuleID FROM RatioCycleSetting WHERE RuleSnapshotID = @RuleSnapShotID
	
	--ֱ��
	IF @IsZXType = 1
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord
		WHERE RuleID = @RuleID AND RuleSnapshotID <> @RuleSnapShotID
		AND FromSRLS = 0 AND GLType='ֱ�����'
	END
	--��ֱ��
	ELSE
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord
		WHERE RuleID = @RuleID AND RuleSnapshotID <> @RuleSnapShotID
		AND FromSRLS = 0
	END
	--���Ҫ����ĺ�ͬ����ʷ�汾��������ʷ�汾��������ϵͳ�в����ģ�����Ҫ����ʷ�汾��Ӧ��GLRecord��ɾ��--End
	
	DECLARE @MyCursor CURSOR, @GLRecordID nvarchar(50)
	SET @MyCursor = CURSOR SCROLL FOR SELECT GLRecordID FROM @GLRecordIDTbl 
	OPEN @MyCursor
	FETCH NEXT FROM @MyCursor INTO @GLRecordID
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC dbo.usp_Cal_DeleteOneGLRecord @GLRecordID
		FETCH NEXT FROM @MyCursor INTO @GLRecordID
	END
	CLOSE @MyCursor
	DEALLOCATE @MyCursor
END


GO


