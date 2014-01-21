USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_DeleteGLRecordByRuleSnapShotID]    Script Date: 08/03/2012 15:31:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By 
 
修改标识：
修改描述：
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
	
	--如果是同一份合同重新计算，需要将相同RuleSnapshotID的GLRecord给删除--Begin
	IF @IsZXType=1
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord WHERE RuleSnapShotID=@RuleSnapShotID AND FromSRLS=0 AND GLType='直线租金'
	END
	ELSE
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord WHERE RuleSnapShotID=@RuleSnapShotID AND FromSRLS=0
	END
	--如果是同一份合同重新计算，需要将相同RuleSnapshotID的GLRecord给删除--End
	
	--如果要计算的合同有历史版本，并且历史版本的是我们系统中产生的，则需要将历史版本对应的GLRecord都删除--Begin
	DECLARE @RuleID nvarchar(50)
	
	--先获取计算规则的RuleID
	--固定
	IF EXISTS (SELECT * FROM FixedRuleSetting WHERE RuleSnapshotID = @RuleSnapShotID)
		SELECT @RuleID=RuleID FROM FixedRuleSetting WHERE RuleSnapshotID = @RuleSnapShotID
	--百分比
	ELSE
		SELECT @RuleID=RuleID FROM RatioCycleSetting WHERE RuleSnapshotID = @RuleSnapShotID
	
	--直线
	IF @IsZXType = 1
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord
		WHERE RuleID = @RuleID AND RuleSnapshotID <> @RuleSnapShotID
		AND FromSRLS = 0 AND GLType='直线租金'
	END
	--非直线
	ELSE
	BEGIN
		INSERT INTO @GLRecordIDTbl
		SELECT GLRecordID FROM GLRecord
		WHERE RuleID = @RuleID AND RuleSnapshotID <> @RuleSnapShotID
		AND FromSRLS = 0
	END
	--如果要计算的合同有历史版本，并且历史版本的是我们系统中产生的，则需要将历史版本对应的GLRecord都删除--End
	
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


