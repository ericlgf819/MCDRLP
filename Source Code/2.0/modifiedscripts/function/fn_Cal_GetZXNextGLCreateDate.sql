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
-- Description:	获取直线的下次GL日期
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
	WHERE RentTypeName='直线租金'
	
	SELECT @ZXStartDate=dbo.fn_GetDate(ZXStartDate), @RuleID=RuleID 
	FROM dbo.FixedRuleSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID AND RentType='固定租金'
	
	--如果不是固定租金, 那么不存在直线GL
	IF @ZXStartDate IS NULL 
	BEGIN
		RETURN NULL 
	END
	
	SELECT @MaxDate=dbo.fn_GetDate(MAX(EndDate)) 
	FROM dbo.FixedTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID 
	
	--取出最大的直线GL日期
	--SELECT @NextZXGLStartDate=ISNULL(MAX(CycleEndDate+1),@ZXStartDate) 
	--FROM dbo.GLRecord 
	--WHERE RuleID=@RuleID AND GLType='直线租金'
	
	--Modified by Eric--Begin
	DECLARE @MaxCycleEndDate DATETIME
	SELECT @MaxCycleEndDate=MAX(CycleEndDate) FROM dbo.GLRecord
	WHERE RuleID=@RuleID AND GLType='直线租金'
	
	IF @MaxCycleEndDate IS NULL
	BEGIN
		SET @NextZXGLStartDate=@ZXStartDate
	END
	ELSE
	BEGIN
		SET @NextZXGLStartDate=@MaxCycleEndDate+1
	END
	--Modified by Eric--End
	
	--如果算出来年下次直线周期开始日期比时间段最大日期还要大, 那么不用再算ZLGL了
	IF @NextZXGLStartDate > @MaxDate
	BEGIN
		RETURN NULL 
	END
	
	DECLARE @RuleCreateDate DATETIME 
	SELECT @RuleCreateDate=dbo.fn_GetRuleCreateTime(@RuleSnapshotID)
	
	
	--本月的话, 创建月份为下次周期开始日期月份
	IF @WhichMonth = '本月'
	BEGIN
		SELECT @CreateMonth =  @NextZXGLStartDate
	END
	ELSE
	BEGIN --上月的话, 创建月份为下次周期开始日期的后一个月
		SELECT @CreateMonth =  DATEADD(MONTH,1,@NextZXGLStartDate)
	END
	
	SELECT @CreateMonth=dbo.fn_GetMaxDate(@CreateMonth,@RuleCreateDate)
	
	SELECT @ResultVar = dbo.fn_Helper_GetPointDate(@CreateMonth,@GLStartDate)
	

	RETURN @ResultVar
	


END

















GO


