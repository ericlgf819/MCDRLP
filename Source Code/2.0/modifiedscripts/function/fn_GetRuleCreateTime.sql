USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleCreateTime]    Script Date: 07/12/2012 11:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		zhangbsh
-- Create date: 20110720
-- Description:	��ȡ�����ͬ����ʱ��,����GL�������ʼ�·�
-- =============================================
CREATE FUNCTION [dbo].[fn_GetRuleCreateTime]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

--	-- Add the T-SQL statements to compute the return value here
--	SELECT @ResultVar=MIN(cc.CreateTime) FROM 
--	(SELECT * FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) r 
--	INNER JOIN dbo.EntityInfoSetting eis ON r.EntityInfoSettingID = eis.EntityInfoSettingID
--	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
--	INNER JOIN dbo.[Contract] c ON e.ContractSnapshotID = c.ContractSnapshotID
--	INNER JOIN dbo.[Contract] cc ON c.ContractID = cc.ContractID
--	
--	IF @ResultVar IS NULL 
--		SELECT @ResultVar=MIN(cc.CreateTime) FROM 
--		(SELECT * FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID) rcs 
--		INNER JOIN dbo.RatioRuleSetting r ON rcs.RatioID = r.RatioID
--		INNER JOIN dbo.EntityInfoSetting eis ON r.EntityInfoSettingID = eis.EntityInfoSettingID
--		INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
--		INNER JOIN dbo.[Contract] c ON e.ContractSnapshotID = c.ContractSnapshotID
--		INNER JOIN dbo.[Contract] cc ON c.ContractID = cc.ContractID
	--�ɿ�ʼ�ĺ�ͬ�����͵���ʱ��, �ĳ�δ���ʵ�ʱ��
	
	
	DECLARE @RuleID NVARCHAR(50),@MinGLCycleDate DATETIME
	SELECT @RuleID=RuleID FROM 
	(
		SELECT RuleID FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT RuleID FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID 
	) x
	WHERE RuleID IS NOT NULL 
	
	SELECT @MinGLCycleDate=MIN(CycleStartDate) FROM dbo.GLRecord WHERE RuleID=@RuleID
	
	
	--�ѷ����GL��, ��С�·ݾ���GL��ʼ��
	IF @MinGLCycleDate IS NOT NULL
	BEGIN
		SELECT @ResultVar=@MinGLCycleDate
	END
	ELSE
	--Modified by Eric
	BEGIN --û�з����GL�ģ�������Сʱ�����GL��ʼ��
	
		SELECT @ResultVar=MIN(StartDate) FROM(
			SELECT StartDate FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
			UNION
			SELECT StartDate FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID 
		) T
	END
	/*BEGIN  --û�з����GL��, ��ǰδ���ʵ��·ݾ���GL��ʼ��
		DECLARE @MaxClosedMonth NVARCHAR(50)
		SELECT @MaxClosedMonth=MAX(ClosedMonth) FROM dbo.ClosedRecord WHERE FromSRLS=1
		SELECT @ResultVar=@MaxClosedMonth+'01'
		SELECT @ResultVar=DATEADD(MONTH,1,@ResultVar)
	END*/
	
	
	-- Return the result of the function
	RETURN @ResultVar

END





GO


