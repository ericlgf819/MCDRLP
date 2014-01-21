USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetZXNextGLCreateDate]    Script Date: 08/10/2012 16:06:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	��ȡֱ�ߵ��´�GL����
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetZXNextGLCreateDate]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS DATETIME 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	DECLARE @WhichMonth NVARCHAR(50),@GLStartDate INT,@ZXStartDate DATETIME
	DECLARE @NextZXGLStartDate DATETIME,@RuleID NVARCHAR(50),@CreateMonth DATETIME 
	DECLARE @MaxDate DATETIME
	
	SELECT @WhichMonth=WhichMonth,@GLStartDate=GLStartDate 
	FROM RentType 
	WHERE RentTypeName='ֱ�����'
	
	SELECT @ZXStartDate=dbo.fn_GetDate(ZXStartDate), @RuleID=RuleID 
	FROM dbo.FixedRuleSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID AND RentType='�̶����'
	
	--������ǹ̶����, ��ô������ֱ��GL
	IF @ZXStartDate IS NULL 
	BEGIN
		RETURN NULL 
	END
	
	SELECT @MaxDate=dbo.fn_GetDate(MAX(EndDate)) 
	FROM dbo.FixedTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID 
	
	--ȡ������ֱ��GL����
	--SELECT @NextZXGLStartDate=ISNULL(MAX(CycleEndDate+1),@ZXStartDate) 
	--FROM dbo.GLRecord 
	--WHERE RuleID=@RuleID AND GLType='ֱ�����'
	
	--Modified by Eric--Begin
	DECLARE @MaxCycleEndDate DATETIME
	SELECT @MaxCycleEndDate=MAX(CycleEndDate) FROM dbo.GLRecord
	WHERE RuleID=@RuleID AND GLType='ֱ�����'
	
	IF @MaxCycleEndDate IS NULL
	BEGIN
		SET @NextZXGLStartDate=@ZXStartDate
	END
	ELSE
	BEGIN
		SET @NextZXGLStartDate=@MaxCycleEndDate+1
	END
	--Modified by Eric--End
	
	--�����������´�ֱ�����ڿ�ʼ���ڱ�ʱ���������ڻ�Ҫ��, ��ô��������ZLGL��
	IF @NextZXGLStartDate > @MaxDate
	BEGIN
		RETURN NULL 
	END
	
	DECLARE @RuleCreateDate DATETIME 
	SELECT @RuleCreateDate=dbo.fn_GetRuleCreateTime(@RuleSnapshotID)
	
	
	--���µĻ�, �����·�Ϊ�´����ڿ�ʼ�����·�
	IF @WhichMonth = '����'
	BEGIN
		SELECT @CreateMonth =  @NextZXGLStartDate
	END
	ELSE
	BEGIN --���µĻ�, �����·�Ϊ�´����ڿ�ʼ���ڵĺ�һ����
		SELECT @CreateMonth =  DATEADD(MONTH,1,@NextZXGLStartDate)
	END
	
	SELECT @CreateMonth=dbo.fn_GetMaxDate(@CreateMonth,@RuleCreateDate)
	
	SELECT @ResultVar = dbo.fn_Helper_GetPointDate(@CreateMonth,@GLStartDate)
	

	RETURN @ResultVar
	


END

















GO


