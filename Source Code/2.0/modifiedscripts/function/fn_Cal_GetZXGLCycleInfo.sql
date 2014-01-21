USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetZXGLCycleInfo]    Script Date: 08/10/2012 09:42:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	获取直线GL周期开始结束时间
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetZXGLCycleInfo]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS @ResultTable TABLE (RuleSnapshotID NVARCHAR(50),CycleStartDate DATETIME,CycleEndDate DATETIME)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
			DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME,@RuleID NVARCHAR(50)
			DECLARE @MaxDate DATETIME,@MinDate DATETIME
			
			--最小的时间为直线起始日
			SELECT @RuleID=RuleID,@MinDate=dbo.fn_GetDate(ZXStartDate) 
			FROM dbo.FixedRuleSetting 
			WHERE RuleSnapshotID=@RuleSnapshotID
			
			--最大的时间为时间段结束日
			SELECT @MaxDate=MAX(EndDate) 
			FROM dbo.FixedTimeIntervalSetting  
			WHERE RuleSnapshotID=@RuleSnapshotID
			
			IF EXISTS(SELECT * FROM dbo.GLRecord WHERE RuleID=@RuleID AND GLType='直线租金')
			BEGIN
				--Modified by Eric --Begin
				SELECT @CycleStartDate=MAX(CycleEndDate) FROM dbo.GLRecord WHERE RuleID=@RuleID AND GLType='直线租金'
				IF @CycleStartDate IS NOT NULL
					SET @CycleStartDate+=1
				--Modified by Eric --End
				SELECT @CycleEndDate=dbo.fn_GetMinDate(dbo.fn_GetMonthLastDate(@CycleStartDate),@MaxDate)
			END
			ELSE
			BEGIN
				DECLARE @RuleCreateDate DATETIME
				SELECT @RuleCreateDate = dbo.fn_GetDate(dbo.fn_GetRuleCreateTime(@RuleSnapshotID))
				SELECT @CycleStartDate=dbo.fn_GetMaxDate(dbo.fn_GetMonthFirstDate(@RuleCreateDate),@MinDate)
				SELECT @CycleEndDate=dbo.fn_GetMinDate(dbo.fn_GetMonthLastDate(@CycleStartDate),@MaxDate)
			END
			
			INSERT INTO @ResultTable
			SELECT @RuleSnapshotID,@CycleStartDate,@CycleEndDate
			
	RETURN 
END







GO


