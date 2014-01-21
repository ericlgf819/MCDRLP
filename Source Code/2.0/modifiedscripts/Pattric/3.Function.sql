USE [RLPlanning]
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_ReportGetSales]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_ReportGetSales]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@CompanyCode nvarchar(50),
	@Year nvarchar(50)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	-- Fill the table variable with the rows for your result set
	
	--构建Sql
	DECLARE @SqlSelect nvarchar(max), @SqlFrom nvarchar(max), @SqlWhere nvarchar(max)
	
	--有Year条件的SqlSelect部分--Begin
	IF @Year IS NULL OR @Year=''
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, VS.Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12月] '
	END
	ELSE
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, @Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 12, NULL) AS [12月] '
	END
	--有Year条件的SqlSelect部分--End
	
	--From部分--Begin
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '''') '
	END
	--甜品店
	ELSE
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND VS.KioskNo = @KioskNo '
	END
	--From部分--End
	
	--Where部分--Begin
	SET @SqlWhere = N'WHERE VS.StoreNo = @StoreNo '
	--年
	IF @Year IS NOT NULL AND @Year<>''
		SET @SqlWhere += N'AND VS.Year=@Year '
	--Company
	IF @CompanyCode IS NOT NULL AND @CompanyCode<>''
		SET @SqlWhere += N'AND S.CompanyCode=@CompanyCode'
	--Where部分--End
	
	--组合成最终的Sql
	DECLARE @Sql nvarchar(max)
	SET @Sql = @SqlSelect + @SqlFrom + @SqlWhere
	
	RETURN @Sql

END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_ReportGetCashCloseSales]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_ReportGetCashCloseSales]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@CompanyCode nvarchar(50),
	@Year nvarchar(50)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	-- Fill the table variable with the rows for your result set
	
	--构建Sql
	DECLARE @SqlSelect nvarchar(max), @SqlFrom nvarchar(max), @SqlWhere nvarchar(max)
	
	--有Year条件的SqlSelect部分--Begin
	IF @Year IS NULL OR @Year=''
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, VS.Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12月] '
	END
	ELSE
	BEGIN
		SET @SqlSelect = N' SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							@KioskName AS Kiosk, @Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, @Year, 12, NULL) AS [12月] '
	END
	--有Year条件的SqlSelect部分--End
	
	--From部分--Begin
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '''') '
	END
	--甜品店
	ELSE
	BEGIN
		SET @SqlFrom = N'FROM SRLS_TB_Master_Store S 
						INNER JOIN  
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_CloseSales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND VS.KioskNo = @KioskNo '
	END
	--From部分--End
	
	--Where部分--Begin
	SET @SqlWhere = N'WHERE VS.StoreNo = @StoreNo '
	--年
	IF @Year IS NOT NULL AND @Year<>''
		SET @SqlWhere += N'AND VS.Year=@Year '
	--Company
	IF @CompanyCode IS NOT NULL AND @CompanyCode<>''
		SET @SqlWhere += N'AND S.CompanyCode=@CompanyCode'
	--Where部分--End
	
	--组合成最终的Sql
	DECLARE @Sql nvarchar(max)
	SET @Sql = @SqlSelect + @SqlFrom + @SqlWhere
	
	RETURN @Sql

END
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_WorkFlow_GetTrackingIndex]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_WorkFlow_GetTrackingIndex]
(
	@StepIndex int, 			-- 当前步骤
	@ExpirationDate datetime    -- 过期日期
)
/*
	设置待处理任务的排序方式，
	过期事项放置于最前面
*/
Returns int As
Begin
	Declare @Res int
	-- 5 表示是事项提醒类型
	if @StepIndex = 5 And DateDiff(dd,@ExpirationDate,getdate())>0
	-- 过期日期小于当前日期，表示已经过期
		Set @Res = 10 
	else
		Set @Res = 0
	
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_System_Split]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[SRLS_FUN_System_Split]
 (@origStr varchar(8000),  --待拆分的字符串
  @markStr varchar(100))   --拆分标记，如','
 RETURNS @splittable table
 (
 id   varchar(4000) NOT NULL,   --编号ID
 item   varchar(2000) NOT NULL  --拆分后的字符串
 )
AS 
BEGIN
 DECLARE @strlen int,@postion int,@start int,@sublen int,@TEMPstr varchar(200),@TEMPid int
 SELECT @strlen=LEN(@origStr),@start=1,@sublen=0,@postion=1,@TEMPstr='',@TEMPid=0
 
 if(RIGHT(@origStr,1)<>@markStr )
 BEGIN
  SET @origStr = @origStr + @markStr
 END
 WHILE((@postion<=@strlen) and (@postion !=0))
  BEGIN
   IF(CHARINDEX(@markStr,@origStr,@postion)!=0)
    BEGIN
     SET @sublen=CHARINDEX(@markStr,@origStr,@postion)-@postion; 
    END
   ELSE
    BEGIN
     SET @sublen=@strlen-@postion+1;
    END
   IF(@postion<=@strlen)
    BEGIN
     SET @TEMPid=@TEMPid+1;
     SET @TEMPstr=SUBSTRING(@origStr,@postion,@sublen);
     INSERT INTO @splittable(id, item) values(@TEMPid,@TEMPstr)
     IF(CHARINDEX(@markStr,@origStr,@postion)!=0)
      BEGIN
       SET @postion=CHARINDEX(@markStr,@origStr,@postion)+1
      END
     ELSE
      BEGIN
       SET @postion=@postion+1
      END
    END
  END
 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_System_GetTimeValue]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_System_GetTimeValue]
(
	@StartTime DateTime,
	@EndTime DateTime
)
Returns nvarchar(16)
As
/*
	获取两个时间之间差的表达式
*/
Begin
	Declare @Res nvarchar(16),
			@Int int,
			@Expect int
	Set @Res = ''
	SET @Int = DateDiff(MINUTE,@StartTime,@EndTime)
	IF @Int > 1440  -- 天
	BEGIN
		Set @Res = cast(@Int/1440 as varchar(4)) + '天'
		Set @Expect = @Int % 1440
		Set @Res =@Res + cast(@Expect/60 as varchar(4)) + '小时'
		Set @Expect = @Expect % 60
		Set @Res =@Res + cast(@Expect as varchar(4)) + '分钟'
	END
	ELSE IF @Int > 60
	BEGIN
		Set @Res =@Res + cast(@Int/60 as varchar(4)) + '小时'
		Set @Expect = @Int % 60
		Set @Res =@Res + cast(@Expect as varchar(4)) + '分钟'
	END
	ELSE 
		Set @Res =@Res + cast(@Int as varchar(4)) + '分钟'
	
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_GetDateByString]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_GetDateByString]
(
	@StringValue nvarchar(16)
)
Returns DateTime
As
/*
	由字符串推算得出日期
*/
Begin
	Declare @Res DateTime
	Set @Res = NULL
	Set @StringValue = RTRIM(LTRIM(@StringValue))
	if(ISDATE(@StringValue)=1)
		Set @Res = Cast(@StringValue as DateTime)
	else if(LEN(@StringValue) = 8)
		IF(ISDATE(SUBString(@StringValue,1,4)+'-'+SUBString(@StringValue,5,2)+'-'+SUBString(@StringValue,7,2))=1)
			Set @Res = Cast(SUBString(@StringValue,1,4)+'-'+SUBString(@StringValue,5,2)+'-'+SUBString(@StringValue,7,2) as DateTime)
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Trim]    Script Date: 08/16/2012 19:28:27 ******/
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
CREATE FUNCTION [dbo].[fn_Trim]
(
	@String NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS 
BEGIN
	DECLARE @Result NVARCHAR(1000);
	--
	IF @String IS NULL
		SET @Result = NULL;
	ELSE 
		SET @Result = LTRIM(RTRIM(REPLACE(@String,CHAR(32), '')));
	RETURN @Result;
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCheckYearMonth]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[GetCheckYearMonth] (
	@startDate datetime,
	@endDate datetime
)
RETURNS @ResultTable TABLE (CheckYearMonth nvarchar(10))
AS
BEGIN
	declare @CurDate datetime=@startDate;
	while @CurDate<@endDate
	begin
		INSERT INTO @ResultTable	
			select cast(year(@CurDate) as CHAR(4)) + '-' + substring(cast((MONTH(@CurDate)+100) as CHAR(3)),2,2);
		--
		select @CurDate=dateadd(mm,1,@CurDate);
	end
	RETURN;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Remind_GetTypeUnderCount]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	计算事项主题下有多少信事项正在走流程
-- =============================================
CREATE FUNCTION [dbo].[fn_Remind_GetTypeUnderCount]
(
	-- Add the parameters for the function here
	@RemindTypeID NVARCHAR(50)
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT
	SELECT @ResultVar=COUNT(*) FROM (SELECT * FROM dbo.SRLS_TB_Matters_Remind WHERE Status<>'已生效') r 
		INNER JOIN (SELECT * FROM SRLS_TB_Matters_Type WHERE ID=@RemindTypeID) t ON	r.TypeID=t.ID
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Remind_GetNextRemindDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110215
-- Description:	获取提醒事项的提醒时间点
--RemindCycle  0:每周 1:每月 2:每年
-- =============================================
CREATE FUNCTION [dbo].[fn_Remind_GetNextRemindDate]
(
	@RemindID NVARCHAR(50)
)
RETURNS DATETIME 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	DECLARE @RemindCycle INT 
	DECLARE @StartDate DATETIME
	-- Add the T-SQL statements to compute the return value here
	SELECT @RemindCycle=RemindCycle,@StartDate=StartDate 
	FROM SRLS_TB_Matters_Remind 
	WHERE ID=@RemindID
	
	SELECT @ResultVar = @StartDate
	
	IF @RemindCycle=0
	BEGIN
		WHILE DATEDIFF(DAY,@ResultVar,GETDATE()) > 0
		BEGIN
			SELECT @ResultVar = DATEADD(DAY,7,@ResultVar)
		END
	END
	IF @RemindCycle=1
	BEGIN
		WHILE DATEDIFF(DAY,@ResultVar,GETDATE()) > 0
		BEGIN
			SELECT @ResultVar = DATEADD(MONTH,1,@ResultVar)
		END
	END
	IF @RemindCycle=2
	BEGIN
		WHILE DATEDIFF(DAY,@ResultVar,GETDATE()) > 0
		BEGIN
			SELECT @ResultVar = DATEADD(YEAR,1,@ResultVar)
		END
	END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[usp_GetTimeValue]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[usp_GetTimeValue]
(
	@StartTime DateTime,
	@EndTime DateTime
)
Returns nvarchar(16)
As
/*
	获取两个时间之间差的表达式
*/
Begin
	Declare @Res nvarchar(16),
			@Int int,
			@Expect int
	Set @Res = ''
	SET @Int = DateDiff(MINUTE,@StartTime,@EndTime)
	IF @Int > 1440  -- 天
	BEGIN
		Set @Res = cast(@Int/1440 as varchar(4)) + '天'
		Set @Expect = @Int % 1440
		Set @Res =@Res + cast(@Expect/60 as varchar(4)) + '小时'
		Set @Expect = @Expect % 60
		Set @Res =@Res + cast(@Expect as varchar(4)) + '分钟'
	END
	ELSE IF @Int > 60
	BEGIN
		Set @Res =@Res + cast(@Int/60 as varchar(4)) + '小时'
		Set @Expect = @Int % 60
		Set @Res =@Res + cast(@Expect as varchar(4)) + '分钟'
	END
	ELSE 
		Set @Res =@Res + cast(@Int as varchar(4)) + '分钟'
	
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_IsRemindDay]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_Matters_IsRemindDay]
(
	@StartDate datetime,  -- 开始提醒日期
	@RemindCycle int      -- 提醒周期,0:表示1周;1:表示1月;2:表示1年
)
/*
	判断是否是提醒日期
*/
Returns int As
/*
0 表示未到提醒时间
1 表示已到提醒时间
*/
Begin
	Declare @Res int
	Set @Res = 0
	If DateDiff(dd,@StartDate,getdate())=0
		Set @Res = 1 -- 到初始提醒日期
	ELse IF DateDiff(dd,@StartDate,getdate())<0
		Set @Res = 0 -- 未到初始提醒日期
	Else
	Begin
		IF @RemindCycle=0
		Begin
			IF DateDiff(dd,@StartDate,getdate()) % 7 = 0
				Set @Res = 1 -- 每周提醒，判断日期差是否为7整除
		End
		Else IF @RemindCycle=1
		Begin
			-- 每月提醒，判断 Day 是否相同
			IF Day(getdate()) = Day(@StartDate)
				Set @Res=1 
		End
		Else If @RemindCycle=2
		Begin
			-- 每年提醒,判断 Month 和 Day 是否相同
			IF Day(getdate()) = Day(@StartDate) AND MONTH(getdate())=MONTH(@StartDate)
				Set @Res = 1
		End
	End
	
	Return @Res
	
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_GetSpanDays]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_Matters_GetSpanDays]
(
	@StartDate datetime,  -- 开始提醒日期
	@EndDate datetime,    -- 过期时间,0:表示1天;1:表示3天;2:表示1周;3:表示1月
	@Type int             -- 0 表示查询剩余天数，1表示查询过期天数
)
/*
	对于周期提醒事件，获取当前周期的到期日期
*/
Returns nvarchar(4) As
Begin
	DECLARE @Res nvarchar(4)
	IF DATEDIFF(dd,@StartDate,@EndDate)>=0
		SET @Res = CAST(DateDiff(dd,@StartDate,@EndDate) as varchar(4))
	ELSE
		SET @Res = ''
	IF @Type = 1 and @Res='0'
		SET @Res = ''
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_GetExpirationDate]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_Matters_GetExpirationDate]
(
	@StartDate datetime,  -- 开始提醒日期
	@DealDays int,        -- 过期时间,0:表示1天;1:表示3天;2:表示1周;3:表示1月
	@IsCycle bit,         -- 是否周期提醒,0表示否，1表示是
	@RemindCycle int      -- 提醒周期,0:每周；1:每月；2:每年
)
/*
	返回到期日期
*/
Returns DateTime As
Begin
	Declare @RemindDate datetime,     -- 最近一次提醒日期
			@ExpirationDate DateTime  -- 本次到期日期
	IF @IsCycle = 0 
	BEGIN
		If @DealDays = 0
			Set @ExpirationDate =  DateAdd(dd,1,@StartDate)
		Else if(@DealDays = 1)
			Set @ExpirationDate =  DateAdd(dd,3,@StartDate)
		Else if(@DealDays = 2)
			Set @ExpirationDate =  DateAdd(dd,7,@StartDate)
		Else --if(@DealDays = 3)
			Set @ExpirationDate =  DateAdd(mm,1,@StartDate)

		
	END
	ELSE
	BEGIN
		-- 获取到离当前日期最近的一次提醒日期
		Set @RemindDate = @StartDate
		IF @RemindCycle = 0
		BEGIN
			While(DateAdd(dd,7,@RemindDate)<=getdate())
			Begin
				Set @RemindDate = DateAdd(dd,7,@RemindDate)
			End
		END
		Else IF @RemindCycle = 1
		BEGIN
			While(DateAdd(mm,1,@RemindDate)<=getdate())
			Begin
				Set @RemindDate = DateAdd(mm,1,@RemindDate)
			End
		END
		Else IF @RemindCycle = 1
		BEGIN
			While(DateAdd(yy,1,@RemindDate)<=getdate())
			Begin
				Set @RemindDate = DateAdd(yy,1,@RemindDate)
			End
		END

		-- 获取过期天数
		If @DealDays = 0
			Set @ExpirationDate =  DateAdd(dd,1,@RemindDate)
		Else if(@DealDays = 1)
			Set @ExpirationDate =  DateAdd(dd,3,@RemindDate)
		Else if(@DealDays = 2)
			Set @ExpirationDate =  DateAdd(dd,7,@RemindDate)
		Else --if(@DealDays = 3)
			Set @ExpirationDate =  DateAdd(mm,1,@RemindDate)
	END

	-- 获取真实的到期日期，超过该天后，就算过期
	Set @ExpirationDate = DateAdd(dd,-1,@ExpirationDate)

	Return @ExpirationDate
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_GetCycleExpirationDate]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_Matters_GetCycleExpirationDate]
(
	@StartDate datetime,  -- 开始提醒日期
	@DealDays int,        -- 过期时间,0:表示1天;1:表示3天;2:表示1周;3:表示1月
	@RemindCycle int      -- 提醒周期,0:每周；1:每月；2:每年
)
/*
	对于周期提醒事件，获取当前周期的到期日期
*/
Returns DateTime As
Begin
	Declare @RemindDate datetime,     -- 最近一次提醒日期
			@ExpirationDate DateTime  -- 本次到期日期
	-- 获取到离当前日期最近的一次提醒日期
	Set @RemindDate = @StartDate
	IF @RemindCycle = 0
	BEGIN
		While(DateAdd(dd,7,@RemindDate)<=getdate())
		Begin
			Set @RemindDate = DateAdd(dd,7,@RemindDate)
		End
	END
	Else IF @RemindCycle = 1
	BEGIN
		While(DateAdd(mm,1,@RemindDate)<=getdate())
		Begin
			Set @RemindDate = DateAdd(mm,1,@RemindDate)
		End
	END
	Else IF @RemindCycle = 1
	BEGIN
		While(DateAdd(yy,1,@RemindDate)<=getdate())
		Begin
			Set @RemindDate = DateAdd(yy,1,@RemindDate)
		End
	END

	-- 获取过期天数
	If @DealDays = 0
		Set @ExpirationDate =  DateAdd(dd,1,@RemindDate)
	Else if(@DealDays = 1)
		Set @ExpirationDate =  DateAdd(dd,3,@RemindDate)
	Else if(@DealDays = 2)
		Set @ExpirationDate =  DateAdd(dd,7,@RemindDate)
	Else --if(@DealDays = 3)
		Set @ExpirationDate =  DateAdd(mm,1,@RemindDate)

	-- 获取真实的到期日期，超过该天后，就算过期
	Set @ExpirationDate = DateAdd(dd,-1,@ExpirationDate)

	Return @ExpirationDate
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_GetReviewerRejectReason]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[SRLS_FUN_Matters_GetReviewerRejectReason]
(
@RemindID uniqueidentifier
)
RETURNS nvarchar(2048)
AS
BEGIN
	-- 审核人 Reject 原因
	Declare @Msg nvarchar(2048)
	SET @Msg = ''
	SELECT @Msg=@Msg + ';' + Opinion FROM dbo.SRLS_TB_WorkFlow_Tracking 
		WHERE RemindID=@RemindID
			  AND StepIndex = 2
			  And Status=2
	IF @Msg <> '' 
		Set @Msg = Substring(@Msg,2,Len(@Msg)-1)
	Return @Msg
END
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_GetCheckerRejectReason]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[SRLS_FUN_Matters_GetCheckerRejectReason]
(
@RemindID uniqueidentifier
)
RETURNS nvarchar(2048)
AS
BEGIN
	-- 审核人 Reject 原因
	Declare @Msg nvarchar(2048)
	SET @Msg = ''
	SELECT @Msg=@Msg + ';' + Opinion FROM dbo.SRLS_TB_WorkFlow_Tracking 
		WHERE RemindID=@RemindID
			  AND (StepIndex = 1 or StepIndex=4)
			  And Status=2
	IF @Msg <> '' 
		Set @Msg = Substring(@Msg,2,Len(@Msg)-1)
	Return @Msg
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Remind_GetFeedbackInfo]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	事项反馈信息
-- =============================================
CREATE  FUNCTION [dbo].[fn_Remind_GetFeedbackInfo]
(
	-- Add the parameters for the function here
	@RemindID NVARCHAR(50)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)
	SELECT @ResultVar=''
	SELECT @ResultVar=@ResultVar+CONVERT(varchar(16), UpdateTime, 25)+' '+EnglishName+': '+DealProcess+';'+'\r\n'
	FROM (
		SELECT TOP 10 * FROM SRLS_TB_Matters_Feedback 
		WHERE RemindID=@RemindID 
		AND DealProcess IS NOT NULL 
		AND DealProcess<>'' 
		ORDER BY UpdateTime DESC
	) X
	ORDER BY UpdateTime ASC 

	RETURN  @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Kiosk_TempTypeChangeToNumber]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Kiosk_TempTypeChangeToNumber]
(
	-- Add the parameters for the function here
	@TempType NVARCHAR(100)
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	SELECT @ResultVar=SortIndex 
	FROM v_KioskTempTypeSortIndex 
	WHERE TempType=@TempType


	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_IsStringNullOrTrimEmpty]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		liujj
-- Create date: 2011-3-23
-- Description:	判断字符串是否是空或者只包含空格
-- =============================================
CREATE FUNCTION [dbo].[fn_IsStringNullOrTrimEmpty]
(
	@String NVARCHAR(1000)
)
RETURNS BIT
AS 
BEGIN
	DECLARE @Result BIT
	IF @String IS NULL OR LEN(LTRIM(RTRIM(@String))) = 0
		SET @Result = 1
	ELSE
		SET @Result = 0
	
	RETURN @Result	
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetWeek]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetWeek]
(
	-- Add the parameters for the function here
	@DateTimeValue DATETIME
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	SELECT @ResultVar= DATEPART(WEEKDAY, @DateTimeValue+@@DATEFIRST-1)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMaxDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110112
-- Description:	获取两个时间大的那个
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMaxDate]
(
	-- Add the parameters for the function here
	@DateTime1 DATETIME,
	@DateTime2 DATETIME 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	IF DATEDIFF(DAY,@DateTime1,@DateTime2)>0
	BEGIN
		SELECT @ResultVar=@DateTime2
	END
	ELSE
	BEGIN
		SELECT @ResultVar=@DateTime1
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Helper_SplitWithIndex]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:拆分字符串
-- =============================================
CREATE FUNCTION [dbo].[fn_Helper_SplitWithIndex]
(
	@ntext NVARCHAR(1000),--要拆分的字符串
	@delim char(1)--分隔符,如';'
)
RETURNS @TokenVals table (IndexId int IDENTITY (0, 1) NOT NULL, TokenVal nvarchar(255) COLLATE database_default)
AS
BEGIN
  declare @maxStrLen int
  declare @s nvarchar(4000)
  declare @textLen int
  declare @textPos int
  declare @nextTextDelim int
  declare @strLen int
  declare @strPos int
  declare @nextStrDelim nvarchar(255)
  declare @val nvarchar(255)

  set @maxStrLen = 4000

  set @textLen = datalength(@ntext) / 2 
  set @textPos = 1

  if (@textLen = 0) return

  While (@textPos <= @textLen)
  Begin
     if ((@textLen - @textPos + 1) > @maxStrLen)
       begin
        set @s = substring(@ntext, @textPos, @maxStrLen)
        set @nextTextDelim = charindex(@delim, @s, @maxStrLen - 10)
        if (@nextTextDelim > 0)
           set @s = substring(@s, 1, @nextTextDelim - 1)
        else
           set @nextTextDelim = @maxStrLen + 1	
        set @textPos = @textPos + @nextTextDelim
       end
     Else
       begin
        set @s = substring(@ntext, @textPos, @textLen - @textPos + 1)
        set @textPos = @textLen + 1
       end

     set @strLen = len(@s)
     set @strPos = 0
     While @strPos <= @strLen
     Begin
        set @nextStrDelim = charindex(@delim, @s, @strPos)
        if @nextStrDelim = 0
           set @nextStrDelim = @strLen + 1
        set @val = cast(substring(@s, @strPos, @nextStrDelim - @strPos) as nvarchar(255))
        insert @TokenVals values (@val)
        set @strPos = @nextStrDelim + 1
     End 
  End 
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Helper_Split]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:拆分字符串
-- =============================================
CREATE FUNCTION [dbo].[fn_Helper_Split]
(
	@ntext NVARCHAR(4000),--要拆分的字符串
	@delim char(1)--分隔符,如';'
)
RETURNS @TokenVals table (TokenVal nvarchar(255) COLLATE database_default)
AS
BEGIN
  declare @maxStrLen int
  declare @s nvarchar(4000)
  declare @textLen int
  declare @textPos int
  declare @nextTextDelim int
  declare @strLen int
  declare @strPos int
  declare @nextStrDelim nvarchar(255)
  declare @val nvarchar(255)
  set @maxStrLen = 4000
  set @textLen = datalength(@ntext) / 2 
  set @textPos = 1
  if (@textLen = 0) return

  While (@textPos <= @textLen)
  Begin
     if ((@textLen - @textPos + 1) > @maxStrLen)
       begin
        set @s = substring(@ntext, @textPos, @maxStrLen)
        
        set @nextTextDelim = charindex(@delim, @s, @maxStrLen - 10)

        
        if (@nextTextDelim > 0)
           set @s = substring(@s, 1, @nextTextDelim - 1)
        else
           set @nextTextDelim = @maxStrLen + 1	

        set @textPos = @textPos + @nextTextDelim
       end
     Else
       begin
        set @s = substring(@ntext, @textPos, @textLen - @textPos + 1)
        set @textPos = @textLen + 1
       end

     set @strLen = len(@s)
     set @strPos = 0
     While @strPos <= @strLen
     Begin
        set @nextStrDelim = charindex(@delim, @s, @strPos)
        if @nextStrDelim = 0
           set @nextStrDelim = @strLen + 1
        set @val = cast(substring(@s, @strPos, @nextStrDelim - @strPos) as nvarchar(255))
        insert @TokenVals values (@val)
        set @strPos = @nextStrDelim + 1
     End 
  End 
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Helper_GetTimeDifference]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: <Create Date, ,>
-- Description:	获取两个时间点的时间差
-- =============================================
CREATE FUNCTION [dbo].[fn_Helper_GetTimeDifference]
(
	-- Add the parameters for the function here
	@DateTime1 DATETIME,
	@DateTime2 DATETIME
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)
	
	DECLARE @Days INT,@Hours INT,@Minutes INT,@Seconds INT 
	-- Add the T-SQL statements to compute the return value here
	DECLARE @SCount INT 
	SELECT @SCount = DATEDIFF(s,@DateTime1,@DateTime2)
	SELECT @Days=@SCount/(60*60*24)
	SELECT @SCount=@SCount%(60*60*24)
	SELECT @Hours=@SCount/(60*60)
	SELECT @SCount=@SCount%(60*60)
	SELECT @Minutes=@SCount/(60)
	SELECT @Seconds=@SCount%60
	
	SELECT @ResultVar=''
	IF @Days<>0
	BEGIN
		SELECT @ResultVar=@ResultVar+CAST(@Days AS NVARCHAR(50))+'天'
	END
	IF @Hours<>0
	BEGIN
		SELECT @ResultVar=@ResultVar+CAST(@Hours AS NVARCHAR(50))+'小时'
	END
	IF @Minutes<>0
	BEGIN
		SELECT @ResultVar=@ResultVar+CAST(@Minutes AS NVARCHAR(50))+'分钟'
	END
	IF @Seconds<>0
	BEGIN
		SELECT @ResultVar=@ResultVar+CAST(@Seconds AS NVARCHAR(50))+'秒'
	END
	
	IF LEN(@ResultVar)=0
	BEGIN
		SELECT @ResultVar='0秒'
	END

	SELECT @ResultVar=' '+@ResultVar+' '

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_FormatCondition]    Script Date: 08/16/2012 19:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：返回where 条件, 如果为空, 则返回 1=1 
创建标识：Create By 
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE  FUNCTION [dbo].[fn_Cal_FormatCondition]
(
	-- Add the parameters for the function here
	@SQLCondition NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000);
	--
	IF @SQLCondition IS NULL OR LTRIM(RTRIM(@SQLCondition))='' BEGIN
		SELECT @ResultVar='1=1';
	END ELSE BEGIN
		SELECT @ResultVar=@SQLCondition;
	END
	--
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_InitStrFormula]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	初始化公式串
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_InitStrFormula]
(
	-- Add the parameters for the function here
	@StrFormula NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=LOWER(@StrFormula)

	SELECT @ResultVar=REPLACE(@ResultVar,' ','')
	SELECT @ResultVar=REPLACE(@ResultVar,'[','(')
	SELECT @ResultVar=REPLACE(@ResultVar,']',')')
	SELECT @ResultVar=REPLACE(@ResultVar,'--','+')
	SELECT @ResultVar=REPLACE(@ResultVar,'%','/100.00')
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_IsEntityHasAP]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	实体是否已经出过AP
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_IsEntityHasAP]
(
	-- Add the parameters for the function here
	@EntityID NVARCHAR(50)
)
RETURNS BIT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT 
	
	--added by Eric
	SET @ResultVar = 0
	
	--commented by Eric
	--DECLARE @ContractID NVARCHAR(50)
	--SELECT @ContractID=dbo.fn_Contract_GetContractID(ContractSnapshotID) 
	--FROM dbo.Entity 
	--WHERE EntityID=@EntityID

	---- Add the T-SQL statements to compute the return value here
	--IF EXISTS
	--(
	--	SELECT 1 FROM 
	--	(
	--		SELECT 
	--			e.EntityTypeName,e.StoreOrDeptNo,e.EntityName,
	--			r.CycleStartDate,r.CycleEndDate
	--		FROM dbo.APRecord r 
	--		INNER JOIN dbo.Entity e ON r.EntityID = e.EntityID
	--		WHERE dbo.fn_Contract_GetContractID(r.ContractSnapshotID) = @ContractID
	--	) x INNER JOIN dbo.Entity e ON x.EntityTypeName = e.EntityTypeName 
	--		AND x.StoreOrDeptNo = e.StoreOrDeptNo 
	--		AND x.EntityName = e.EntityName
	--)
	--BEGIN
	--	SELECT @ResultVar=1
	--END
	--ELSE
	--BEGIN
	--	SELECT @ResultVar=0
	--END
		
		

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetRatioRuleID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110223
--获取RULEID
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetRatioRuleID]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar  NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=NULL

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetFixedRuleID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110223
--获取RULEID
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetFixedRuleID]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar  NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
--	DECLARE   @VendorNo NVARCHAR(50), @EntityTypeName  NVARCHAR(50), @EntityNo NVARCHAR(50), @EntityName NVARCHAR(50), @StoreOrDeptNo NVARCHAR(50), @ContractID NVARCHAR(50), @SnapshotCreateTime DATETIME, @ContractSnapshotID NVARCHAR(50), 
--                      @CreateTime DATETIME
--	
--	SELECT    
--					@VendorNo=EIS.VendorNo, 
--					@EntityTypeName=E.EntityTypeName, 
--					@EntityNo=E.EntityNo, 
--					@EntityName=E.EntityName, 
--					@StoreOrDeptNo=E.StoreOrDeptNo, 
--					@ContractID=C.ContractID, 
--					@SnapshotCreateTime=C.SnapshotCreateTime, 
--					@ContractSnapshotID=C.ContractSnapshotID, 
--					@CreateTime=C.CreateTime
--	FROM       (SELECT * FROM  dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) AS FR INNER JOIN
--                      dbo.EntityInfoSetting AS EIS ON FR.EntityInfoSettingID = EIS.EntityInfoSettingID INNER JOIN
--                      dbo.Entity AS E ON EIS.EntityID = E.EntityID INNER JOIN
--                      dbo.Contract AS C ON E.ContractSnapshotID = C.ContractSnapshotID INNER JOIN
--                      dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID INNER JOIN
--                      dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID AND EIS.VendorNo = VE.VendorNo AND VC.VendorNo = VE.VendorNo
--    
--    
--    	SELECT    
--			TOP 1 @ResultVar=FR.RuleID
--	FROM       (SELECT * FROM  dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) AS FR INNER JOIN
--                      dbo.EntityInfoSetting AS EIS ON FR.EntityInfoSettingID = EIS.EntityInfoSettingID INNER JOIN
--                      dbo.Entity AS E ON EIS.EntityID = E.EntityID INNER JOIN
--                      (SELECT * FROM dbo.CONTRACT WHERE SnapshotCreateTime IS NOT NULL) AS C ON E.ContractSnapshotID = C.ContractSnapshotID INNER JOIN
--                      dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID INNER JOIN
--                      dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID AND EIS.VendorNo = VE.VendorNo AND VC.VendorNo = VE.VendorNo
--        WHERE 
--					EIS.VendorNo= @VendorNo 
--					AND E.EntityTypeName= @EntityTypeName
--					AND E.EntityNo=@EntityNo 
--					AND E.EntityName= @EntityName
--					AND E.StoreOrDeptNo= @StoreOrDeptNo
--					AND C.ContractID=@ContractID 
--					ORDER BY C.CreateTime DESC
	SELECT @ResultVar = NULL 

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_FilterDateTime]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		BDY
-- Create date: 2010/01/05
-- Description:	过滤不合法的日期
-- =============================================
CREATE FUNCTION [dbo].[fn_FilterDateTime] 
(
	@value nvarchar(30)
)
RETURNS nvarchar(30)
AS
BEGIN
    DECLARE @temp NVARCHAR(30)
    SET @temp = RTRIM(LTRIM(@value))
	IF ISDATE(@temp) = 1
		RETURN @temp
	RETURN NULL
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_DateFormat]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	日期格式
-- =============================================
CREATE FUNCTION [dbo].[fn_DateFormat]
(	
	
)
RETURNS TABLE 
AS
RETURN 
(
	Select CONVERT(varchar(100), GETDATE(), 0) AS Format, 0 AS DateType UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 1),1 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 2),2 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 3),3 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 4),4 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 5),5 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 6),6 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 7),7 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 8),8 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 9),9 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 10),10 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 11),11 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 12),12 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 13),13 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 14),14 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 20),20 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 21),21 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 22),22 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 23),23 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 24),24 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 25),25 UNION ALL 
	Select CONVERT(varchar(100), GETDATE(), 100),100 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 101),101 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 102),102 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 103),103 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 104),104 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 105),105 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 106),106 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 107),107 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 108),108 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 109),109 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 110),110 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 111),111 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 112),112 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 113),113 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 114),114 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 120),120 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 121),121 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 126),126 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 130),130 UNION ALL
	Select CONVERT(varchar(100), GETDATE(), 131),131 
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetMaxInnerString]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取MAX中的字符串
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetMaxInnerString]
(
	-- Add the parameters for the function here
	@StrFormula NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)

	DECLARE @StartIndex INT
	DECLARE @EndIndex INT
	IF CHARINDEX('max',@StrFormula)>0
	BEGIN
		SELECT @StartIndex=CHARINDEX('max{',@StrFormula)+4
		SELECT @EndIndex=CHARINDEX('}',@StrFormula)
		SELECT @ResultVar=SUBSTRING(@StrFormula,@StartIndex,@EndIndex-@StartIndex)
	END
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetMaxFormulaString]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取MAX公式字符串
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetMaxFormulaString]
(
	-- Add the parameters for the function here
	@StrFormula NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)

	DECLARE @StartIndex INT
	DECLARE @EndIndex INT
	IF CHARINDEX('max',@StrFormula)>0
	BEGIN
		SELECT @StartIndex=CHARINDEX('max{',@StrFormula)
		SELECT @EndIndex=CHARINDEX('}',@StrFormula)+1
		SELECT @ResultVar=SUBSTRING(@StrFormula,@StartIndex,@EndIndex-@StartIndex)
	END
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonth]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMonth]
(
	-- Add the parameters for the function here
	@DateValue DATETIME 
)
RETURNS NVARCHAR(6) 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(6) 

	SELECT @ResultVar=CONVERT(nvarchar(6), @DateValue, 112)
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMinDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110112
-- Description:	获取两个时间小的那个
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetMinDate]
(
	-- Add the parameters for the function here
	@DateTime1 DATETIME,
	@DateTime2 DATETIME 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	IF DATEDIFF(DAY,@DateTime1,@DateTime2)>0
	BEGIN
		SELECT @ResultVar=@DateTime1
	END
	ELSE
	BEGIN
		SELECT @ResultVar=@DateTime2
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetIDbyUser]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetIDbyUser]
(	
	-- Add the parameters for the function here
	@userId nvarchar(50)='2602DA1E-C202-497A-B678-4A1FAFD71EFB', 
	@ObjectType nvarchar(50)='AreaID'
)
RETURNS @IDS Table(ID uniqueidentifier ) 
AS

begin


if(@ObjectType='AreaID')
	insert into @IDS
	select AreaID from UserArea where  UserID=@userId
	return
	
end
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEmailContentByTemplate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================
-- Author:     fengxh
-- Create Date:20110317
-- Description:根据邮件模板内容返回邮件内容
-- ========================================
CREATE FUNCTION [dbo].[fn_GetEmailContentByTemplate]
(
	@Template NVARCHAR(255),--模板内容
	@ProcID NVARCHAR(50), --流程编号
	@PartUserEnglishName NVARCHAR(50),--原处理人英文名
	@AppName NVARCHAR(50),--流程名称
	@TaskName NVARCHAR(50),--任务名称
	@ReceTime DATETIME --接收时间
)
RETURNS NVARCHAR(255)
AS
BEGIN
	IF @ProcID IS NULL 
		SET @ProcID = ''
	IF @PartUserEnglishName IS NULL 
		SET @PartUserEnglishName = ''
	IF @AppName IS NULL 
		SET @AppName = ''
	IF @TaskName IS NULL 
		SET @TaskName = ''
		
	DECLARE @EmailContent NVARCHAR(255)
	SET @EmailContent = @Template
	SET @EmailContent = REPLACE(@EmailContent,'{@任务编号}',@ProcID)
	SET @EmailContent = REPLACE(@EmailContent,'{@处理人}',@PartUserEnglishName)
	SET @EmailContent = REPLACE(@EmailContent,'{@流程名称}',@AppName)
	SET @EmailContent = REPLACE(@EmailContent,'{@任务名称}',@TaskName)
	SET @EmailContent = REPLACE(@EmailContent,'{@发起时间}',CONVERT(VARCHAR(16), @ReceTime,120))
	RETURN @EmailContent
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetDateZoneString]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetDateZoneString]
(
	-- Add the parameters for the function here
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=CONVERT(nvarchar(10), @StartDate, 23)+'~'+CONVERT(nvarchar(10), @EndDate, 23)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetDateTimeMonth]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取时间月份
-- =============================================
CREATE FUNCTION [dbo].[fn_GetDateTimeMonth]
(
	-- Add the parameters for the function here
	@DateTimeValue DATETIME 
)
RETURNS NVARCHAR(6)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(6)
	
	SELECT @ResultVar=CONVERT(NVARCHAR(6), @DateTimeValue, 112)
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date:20110320
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetDate]
(
	-- Add the parameters for the function here
	@DateTimeValue DATETIME
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME;

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = CONVERT(DATETIME, CONVERT(NVARCHAR(10), @DateTimeValue, 112));

	-- Return the result of the function
	RETURN @ResultVar;

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetStrArrayStrOfIndex]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================
-- Author:     liujj
-- Create Date:20110109
-- Description:获取字符串数组中指定序号的字符串，从1开始
-- ========================================
CREATE function [dbo].[fn_GetStrArrayStrOfIndex]
(
	@str VARCHAR(8000), --要分割的字符串
	@split VARCHAR(10), --分隔符号
	@index INT --取第几个元素
)
RETURNS VARCHAR(1024)
AS
BEGIN
DECLARE @location INT
DECLARE @start INT
DECLARE @next INT
DECLARE @seed INT

SET @str=LTRIM(RTRIM(@str))
SET @start=1
SET @next=1
SET @seed=LEN(@split)

SET @location=CHARINDEX(@split,@str)
WHILE @location<>0 and @index>@next
BEGIN
   SET @start=@location+@seed
   SET @location=CHARINDEX(@split,@str,@start)
   SET @next=@next+1
END
IF @location =0 SELECT @location =LEN(@str)+1 
--这儿存在两种情况：1、字符串不存在分隔符号 2、字符串中存在分隔符号，跳出WHILE循环后，@location为0，那默认为字符串后边有一个分隔符号。

RETURN substring(@str,@start,@location-@start)
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetStrArrayLength]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================
-- Author:     liujj
-- Create Date:20110109
-- Description:获取字符串数组的长度
-- ========================================
CREATE FUNCTION [dbo].[fn_GetStrArrayLength]
(
	@str VARCHAR(8000), --要分割的字符串
	@split VARCHAR(10) --分隔符号
)
RETURNS INT
AS
BEGIN
DECLARE @location INT
DECLARE @start INT
DECLARE @length INT

SET @str=LTRIM(RTRIM(@str))
SET @location=CHARINDEX(@split,@str)
SET @length=1
WHILE @location<>0
BEGIN
   SET @start=@location+1
   SET @location=CHARINDEX(@split,@str,@start)
   SET @length=@length+1
END
RETURN @length
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetPeriodString]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		liujj
-- Create date: 2011-3-23
-- Description:	获取日期区间的字符串形式
-- =============================================
CREATE FUNCTION [dbo].[fn_GetPeriodString]
(
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS NVARCHAR(50)
AS 
BEGIN
	RETURN CONVERT(VARCHAR(10),@StartDate,111)+ '-' + CONVERT(VARCHAR(10),@EndDate,111)
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonthLastDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: 20110107
-- Description:	获取本月最后一天的日期
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMonthLastDate]
(
	@DateTimeValue DATETIME 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar  = CONVERT(DATETIME ,CONVERT(nvarchar(6), DATEADD(MONTH,1,@DateTimeValue), 112)+'01')-1

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonthFirstDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: 20110107
-- Description:	获取本月最前的日期
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMonthFirstDate]
(
	@DateTimeValue DATETIME 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar  = CONVERT(DATETIME ,CONVERT(nvarchar(6), @DateTimeValue, 112)+'01')

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonthDayCount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取一个月里面的天数
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMonthDayCount]
(
	-- Add the parameters for the function here
	@YearMonth VARCHAR(6)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar int


	SELECT @ResultVar=Day(DATEADD(month,1,@YearMonth+'01')-1)
	
	
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonthDateDetail]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	日期
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetMonthDateDetail]
(	
	@StartDate DATETIME,
	@EndDate DATETIME 
)
RETURNS @ResultTable TABLE(ValueIndex INT IDENTITY(1,1),ZoneStartDate DATETIME,ZoneEndDate DATETIME)  
AS
BEGIN
	DECLARE @TempTable TABLE(ValueIndex INT IDENTITY(1,1),DateValue DATETIME)  
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	
	DECLARE @TempStartDate DATETIME 
	SELECT @TempStartDate=@StartDate
	WHILE @TempStartDate<=@EndDate
	BEGIN
		INSERT INTO @TempTable
		SELECT @TempStartDate
		
		IF @TempStartDate<>@EndDate
		BEGIN
			INSERT INTO @TempTable
			SELECT dbo.fn_GetMinDate(dbo.fn_GetMonthLastDate(@TempStartDate),@EndDate)
		END
		SELECT @TempStartDate=dbo.fn_GetMonthLastDate(@TempStartDate)+1
	END
	
	INSERT INTO @ResultTable
	SELECT x.DateValue,y.DateValue 
	FROM (SELECT * FROM @TempTable WHERE ValueIndex%2=1)  x 
	INNER JOIN (SELECT * FROM @TempTable WHERE ValueIndex%2=0) y 
	ON x.ValueIndex+1 = y.ValueIndex
	
	

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMonthCount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取两个日期的月份数
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetMonthCount]
(
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,8)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,8),@MonthCount INT,@StartDayCout INT ,@EndDayCout INT,@StartMonthDayCout DECIMAL(18,8) ,@EndMonthDayCout DECIMAL(18,8) 
	--两日期相差月份
	SELECT @MonthCount=DATEDIFF(MONTH,@StartDate,@EndDate)-1
	--将前后的折算成月
	SELECT @StartDayCout=DATEDIFF(DAY,@StartDate,dbo.fn_GetMonthLastDate(@StartDate))+1
	SELECT @EndDayCout=DAY(@EndDate)
	SELECT @StartMonthDayCout=DAY(dbo.fn_GetMonthLastDate(@StartDate))
	SELECT @EndMonthDayCout=DAY(dbo.fn_GetMonthLastDate(@EndDate))
	
	SELECT @ResultVar=@StartDayCout/@StartMonthDayCout+@MonthCount+@EndDayCout/@EndMonthDayCout
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetCurrentCycle]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取GL周期
-- =============================================
CREATE FUNCTION [dbo].[fn_GetCurrentCycle] 
(
	@WhichMonth NVARCHAR(50),
	@GLStartDate INT
)
RETURNS VARCHAR(6)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar VARCHAR(6)
	DECLARE @MonthDayCount INT
	
	IF @WhichMonth = '本月'
	BEGIN
		SELECT  @MonthDayCount = dbo.fn_GetMonthDayCount(CONVERT(varchar(6), GETDATE(), 112))
		IF DAY(GETDATE()) = @GLStartDate --如果到了日期点
			BEGIN
				SELECT @ResultVar=CONVERT(varchar(6), GETDATE(), 112)
			END
		ELSE IF @MonthDayCount<@GLStartDate AND @MonthDayCount=DAY(GETDATE()) --如果没有到日期点且为本月最后一天
			BEGIN
				SELECT @ResultVar=CONVERT(varchar(6), GETDATE(), 112)
			END
	END

	IF @WhichMonth = '上月'
	BEGIN
		SELECT  @MonthDayCount = dbo.fn_GetMonthDayCount(CONVERT(varchar(6), GETDATE(), 112))
		IF DAY(GETDATE()) = @GLStartDate--如果到了日期点
			BEGIN
				SELECT @ResultVar=CONVERT(varchar(6), DATEADD(MONTH,-1,GETDATE()), 112)
			END
		ELSE IF @MonthDayCount<@GLStartDate AND @MonthDayCount=DAY(GETDATE())--如果没有到日期点且为本月最后一天
			BEGIN
				SELECT @ResultVar=CONVERT(varchar(6), DATEADD(MONTH,-1,GETDATE()), 112)
			END
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetDateMonthDayCount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetDateMonthDayCount]
(
	-- Add the parameters for the function here
	@DateTimeValue DATETIME
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=DATEDIFF(DAY,dbo.fn_GetMonthFirstDate(@DateTimeValue),dbo.fn_GetMonthLastDate(@DateTimeValue))+1

	-- Return the result of the function
	RETURN @ResultVar 

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_Formula]    Script Date: 08/16/2012 19:28:24 ******/
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
CREATE FUNCTION [dbo].[fn_Cal_Formula] (
	-- Add the parameters for the function here
	@StrFormula NVARCHAR(1000)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2);
	-- 
	SELECT @StrFormula=dbo.fn_Cal_InitStrFormula(@StrFormula);
	DECLARE @sql nvarchar(1000)=N'SET @ResultVar=CASE(' + @StrFormula + ' AS DECIMAL(18,2))';
	EXEC sp_executesql @sql, N'@ResultVar DECIMAL(18,2) out', @ResultVar;
	--
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetKioskRelationEndDate]    Script Date: 08/16/2012 19:28:25 ******/
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
CREATE FUNCTION [dbo].[fn_Cal_GetKioskRelationEndDate]
(
	-- Add the parameters for the function here
	@KioskID NVARCHAR(50),
	@StartDate DATETIME 
)
RETURNS DATETIME 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME;
	--
	SELECT TOP 1 @ResultVar=DATEADD(DAY, -1, ActiveDate) FROM dbo.[KioskStoreRelationChangHistory]
	WHERE KioskID=@KioskID AND ActiveDate>@StartDate
	ORDER BY ActiveDate ASC;
	--
	--SELECT @ResultVar=ISNULL(@ResultVar, '21001231');
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ConvertFileSize]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110107
-- Description:	显示附件大小
-- =============================================
CREATE FUNCTION [dbo].[fn_ConvertFileSize]
(
	@FileSize TInt32
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar  NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	IF @FileSize < 1024
	BEGIN
		SELECT @ResultVar = CAST (@FileSize AS NVARCHAR(49)) + 'B'
	END
	ELSE IF @FileSize < (1024*1024)
	BEGIN
		SELECT @ResultVar = CAST (CAST(@FileSize/1024.00 AS DECIMAL(18,2)) AS NVARCHAR(49)) + 'K'
	END
	ELSE IF @FileSize < (1024*1024*1024)
	BEGIN
		SELECT @ResultVar = CAST (CAST(@FileSize/(1024.00*1024.00) AS DECIMAL(18,2)) AS NVARCHAR(49)) + 'M'
	END
	ELSE
	BEGIN
		SELECT @ResultVar = CAST (CAST(@FileSize/(1024.00*1024.00*1024.00) AS DECIMAL(18,2)) AS NVARCHAR(49)) + 'G'
	END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAppCodeByCategory]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================
-- Author:     liujj
-- Create Date:20110401
-- Description:通过分类获取AppCode
-- ========================================
CREATE FUNCTION [dbo].[fn_GetAppCodeByCategory]
( @Category VARCHAR(50))
RETURNS @ResultTable TABLE (AppCode dbo.TInt32)
AS
BEGIN
	IF @Category = '0' --1=普通审核（含一期任务）
		INSERT INTO @ResultTable 
		SELECT AppCode FROM dbo.WF_AppDict
	IF @Category = '1' --1=普通审核（含一期任务）
		INSERT INTO @ResultTable 
		SELECT AppCode FROM dbo.WF_AppDict
		WHERE AppCode IN (1,2,3,4,5,6,7,8,9,10,11,17)
	ELSE IF @Category = '2'--2=AP凭证计算（含AP adjust）
		INSERT INTO @ResultTable 
		SELECT AppCode FROM dbo.WF_AppDict
		WHERE AppCode IN (14,15)
	ELSE IF @Category = '3'--3=GL凭证计算
		INSERT INTO @ResultTable 
		SELECT AppCode FROM dbo.WF_AppDict
		WHERE AppCode IN (13)
	ELSE IF @Category = '4'--4=Kiosk sales 收集
		INSERT INTO @ResultTable 
		SELECT AppCode FROM dbo.WF_AppDict
		WHERE AppCode IN (12)
  RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAdministratorGroupID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	获取管理USERID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAdministratorGroupID]
(
	
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	SET @ResultVar=(SELECT TOP 1 gp.ID FROM dbo.UUP_TB_Group gp INNER JOIN dbo.UUP_TB_System sys ON gp.SystemID = sys.ID WHERE sys.SystemCode='SRLS' AND gp.GroupName='管理员组')
	
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Helper_GetPointDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	获取某月指定某天的日期
-- =============================================
CREATE FUNCTION [dbo].[fn_Helper_GetPointDate]
(
	-- Add the parameters for the function here
	@DateTimeValue DATETIME,
	@PointDate INT 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME

	-- Add the T-SQL statements to compute the return value here
	IF dbo.fn_GetMonthDayCount(dbo.fn_GetMonth(@DateTimeValue)) < @PointDate
	BEGIN
		SELECT @ResultVar=dbo.fn_GetMonthLastDate(@DateTimeValue)
	END
	ELSE
	BEGIN
		DECLARE @DayStr NVARCHAR(2)
		IF LEN(@PointDate)=1
		BEGIN
			SELECT @DayStr='0'+CAST(@PointDate AS NVARCHAR(1))
		END
		ELSE
		BEGIN
			SELECT  @DayStr=CAST(@PointDate AS NVARCHAR(2))
		END
		
		SELECT @ResultVar=(dbo.fn_GetDateTimeMonth(@DateTimeValue)+@DayStr)
	END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Helper_GetDateList]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date,,>
-- Description:	获取两个日期点的所有日期
-- =============================================
CREATE FUNCTION [dbo].[fn_Helper_GetDateList]
(
	-- Add the parameters for the function here
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS 
@ResultTable TABLE
(
	-- Add the column definitions for the TABLE variable here
	IndexID INT  IDENTITY(1,1),
	ListDate DATETIME
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	
	WHILE @StartDate<=@EndDate
	BEGIN
		INSERT INTO @ResultTable
		SELECT @StartDate
		
		SELECT @StartDate=DATEADD(DAY,1,@StartDate)
	END
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetZXMonthCount]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取直线的两个日期的月份数
-- =============================================
CREATE   FUNCTION [dbo].[fn_GetZXMonthCount]
(
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,8)
AS
BEGIN
	-- Declare the return variable here
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	--以租赁月来计算, 最后不够一个月的算一个月
	DECLARE @ResultVar DECIMAL(18,8)
	
	DECLARE @TempStartDate DATETIME,@TempCount INT 
	SELECT @TempCount=0
	SELECT @TempStartDate=@StartDate
	
	WHILE @TempStartDate <= @EndDate
	BEGIN
		SELECT @TempCount=@TempCount+1
		SELECT @TempStartDate=DATEADD(MONTH,1*@TempCount,@StartDate)
	END
	
	SELECT @ResultVar=@TempCount
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_IsDateZoneIntersection]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	两个时间段是否有交集
-- =============================================
CREATE FUNCTION [dbo].[fn_IsDateZoneIntersection]
(
	-- Add the parameters for the function here
	@Date1Start DATETIME,
	@Date1End DATETIME,
	@Date2Start DATETIME,
	@Date2End DATETIME
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT
	
	SELECT @Date1Start=dbo.fn_GetDate(@Date1Start)
	SELECT @Date1End=dbo.fn_GetDate(@Date1End)
	SELECT @Date2Start=dbo.fn_GetDate(@Date2Start)
	SELECT @Date2End=dbo.fn_GetDate(@Date2End)

	-- Add the T-SQL statements to compute the return value here
	IF EXISTS
	(
		SELECT 1 WHERE (@Date1Start BETWEEN @Date2Start AND @Date2End)
			OR (@Date1End BETWEEN @Date2Start AND @Date2End)
			OR (@Date2Start BETWEEN @Date1Start AND @Date1End)
			OR (@Date2End BETWEEN @Date1Start AND @Date1End)
	)
	BEGIN
		SELECT @ResultVar=1
	END
	ELSE
	BEGIN
		SELECT @ResultVar=0
	END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetWorkDateCout]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取两个日期的工作日天数
-- =============================================
CREATE FUNCTION [dbo].[fn_GetWorkDateCout]
(
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT
	
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	DECLARE @IsWorkDate BIT
	SELECT @ResultVar=0
	IF @StartDate IS NULL OR @EndDate IS NULL
	BEGIN
		SELECT @ResultVar=NULL 
	END
	
		WHILE @EndDate>=@StartDate
		BEGIN
			SELECT @IsWorkDate = NULL
			SELECT @IsWorkDate = IsWorkDate   FROM WorkDateSetting WHERE dbo.fn_GetDate([Date])=@StartDate
			IF @IsWorkDate IS NULL
				BEGIN
					IF EXISTS(SELECT 1 WHERE dbo.fn_GetWeek(@StartDate) BETWEEN 1 AND 5)
					BEGIN
						SELECT @ResultVar=@ResultVar+1
					END
				END
			ELSE
				BEGIN
					IF @IsWorkDate = 1
					BEGIN
								SELECT @ResultVar=@ResultVar+1
					END
				END
	
			SELECT @StartDate=@StartDate+1
		END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_Matters_ActivedUsedTime]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SRLS_FUN_Matters_ActivedUsedTime]
(
	@RemindID uniqueidentifier,
	@Status int,
	@CreateTime DateTime
)
Returns nvarchar(16) As
/*
	获取从发起事项到审核通过总耗时
*/
Begin
	Declare @Res nvarchar(16),
			@ActivedDate DateTime
	IF @Status <> 2  -- 狀態為2表示審核已通過
		Set @Res = ''
	ELSE
	BEGIN
		Select @ActivedDate=DealTime from SRLS_TB_WorkFlow_Tracking
			 where StepIndex=2 and Status=1 AND RemindID=@RemindID
		IF @ActivedDate is NULL
			Set @Res = ''
		ELSE
			Set @Res = [dbo].SRLS_FUN_System_GetTimeValue(@CreateTime,@ActivedDate)
	END
	
	Return @Res
End
GO
/****** Object:  UserDefinedFunction [dbo].[GetCheckDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		mingyongcheng
-- Create date: 20100108
-- Description:获得检测的日期
-- =============================================
CREATE FUNCTION [dbo].[GetCheckDate]
(
	@nYear int
)
RETURNS @ResultTable TABLE (CheckDate DATETIME)
AS
BEGIN

INSERT INTO @ResultTable	
select cast(cast(@nYear as CHAR(4))+[MONTH]+'01' as datetime) from AllMonth
	
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_GetUserCompany]    Script Date: 08/16/2012 19:28:27 ******/
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
CREATE FUNCTION [dbo].[fn_RLP_GetUserCompany]
(
	@UserId uniqueidentifier
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(MAX) = '', @CompanyCode nvarchar(32) = '';

	-- Add the T-SQL statements to compute the return value here
	DECLARE icursor CURSOR FOR SELECT CompanyCode FROM dbo.[RLP_UserCompany] WHERE UserId=@UserId;
	OPEN icursor;
	FETCH NEXT FROM icursor INTO @CompanyCode;
	WHILE @@FETCH_STATUS = 0 BEGIN
		IF @ResultVar = '' BEGIN
			SET @ResultVar = @CompanyCode;
		END ELSE BEGIN
			SET @ResultVar = @ResultVar + ',' + @CompanyCode;
		END
		--
		FETCH NEXT FROM icursor INTO @CompanyCode;
		--err
		if @@error <> 0	BEGIN
			BREAK;
		END
	END
	----
	CLOSE icursor;
	DEALLOCATE icursor;
	
	-- Return the result of the function
	RETURN @ResultVar;

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SerialNumber_Workflow]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	获取流水号  --工作流规则: 年月日+4位流水号
-- =============================================
CREATE FUNCTION [dbo].[fn_SerialNumber_Workflow]
(
	
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @DayLength INT --流水号长度
	SELECT @DayLength=6
	
	DECLARE @CurrentNumber NVARCHAR(50),@PiontIndex INT,@MaxProcID NVARCHAR(50)
	SELECT @MaxProcID=MAX(ProcID) 
	FROM dbo.WF_Proc_Inst 
	WHERE dbo.fn_GetDate(StartTime)=dbo.fn_GetDate(GETDATE())
	
	IF @MaxProcID IS NULL 
	BEGIN
		SELECT @CurrentNumber='1'
	END
	ELSE
	BEGIN
		SELECT @CurrentNumber=CAST(SUBSTRING(@MaxProcID,CHARINDEX('.',@MaxProcID)+1,LEN(@MaxProcID)) AS INT)+1
	END
	
	WHILE LEN(@CurrentNumber)<@DayLength
	BEGIN
		SELECT @CurrentNumber='0'+@CurrentNumber
	END
	
	
	SELECT @ResultVar = CONVERT(varchar(8), GETDATE(), 112)+'.'+@CurrentNumber
		
	RETURN @ResultVar 

END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetAreaSqlConditionStr]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetAreaSqlConditionStr]
(
	@UserID uniqueidentifier,
	@IsAreaName bit		--要返回的是AreaName还是AreaID的Sql语句
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Sql nvarchar(max), @AreaNameOrID nvarchar(50)
	
	--根据条件来确定列名
	IF @IsAreaName=1
		SET @AreaNameOrID='AreaName'
	ELSE
		SET @AreaNameOrID='AreaID'
	
	DECLARE @ConditionTbl TABLE(AreaNameOrID nvarchar(max), [Index] int IDENTITY(1,1))
	
	--根据条件来插入数据
	IF @IsAreaName=1
	BEGIN
		INSERT INTO @ConditionTbl
		SELECT DISTINCT AreaName FROM (
			SELECT C.AreaID, A.AreaName, A.ShowOrder, C.CompanyCode, C.CompanyName 
			FROM (  
					SELECT ISNULL(AreaID, '4AD15C85-ADEC-4348-9A15-9DCC2B1C5D41') AS AreaID,
					CompanyCode, CompanyName 
					FROM SRLS_TB_Master_Company
				 ) AS C 
			INNER JOIN SRLS_TB_Master_Area AS A ON A.ID=C.AreaID 
			WHERE (
					@UserID IS NULL OR 
					C.CompanyCode IN (
										SELECT CompanyCode FROM RLP_UserCompany WHERE UserId=@UserID)
									 )
				AND (
						C.CompanyCode IN 
						(SELECT CompanyCode FROM SRLS_TB_Master_Company WHERE [Status]='A')
					)
	   ) AS AA

	END
	ELSE
	BEGIN
		INSERT INTO @ConditionTbl
		SELECT DISTINCT AreaID FROM (
			SELECT C.AreaID, A.AreaName, A.ShowOrder, C.CompanyCode, C.CompanyName 
			FROM (  
					SELECT ISNULL(AreaID, '4AD15C85-ADEC-4348-9A15-9DCC2B1C5D41') AS AreaID,
					CompanyCode, CompanyName 
					FROM SRLS_TB_Master_Company
				 ) AS C 
			INNER JOIN SRLS_TB_Master_Area AS A ON A.ID=C.AreaID 
			WHERE (
					@UserID IS NULL OR 
					C.CompanyCode IN (
										SELECT CompanyCode FROM RLP_UserCompany WHERE UserId=@UserID)
									 )
				AND (
						C.CompanyCode IN 
						(SELECT CompanyCode FROM SRLS_TB_Master_Company WHERE [Status]='A')
					)
	   ) AS AA	
	END

	DECLARE @ConditionSum int
	SELECT @ConditionSum = COUNT(*) FROM @ConditionTbl
	
	--如果没有任何区域权限，则返回空
	IF @ConditionSum=0
		RETURN ''
	
	--如果只有一条，则不需要处理括号
	IF @ConditionSum=1
	BEGIN
		UPDATE @ConditionTbl SET AreaNameOrID = @AreaNameOrID + '=''' + AreaNameOrID + '''' WHERE [INDEX]=1
	END
	
	--如果大于一条，则需要把括号和OR给加上
	--形式如(AreaName='xxx' OR AreaName='yyy' OR ...)
	IF @ConditionSum>1
	BEGIN
		--第一条需要加上最左边的括号
		UPDATE @ConditionTbl SET AreaNameOrID = '('+ @AreaNameOrID +'=''' + AreaNameOrID + '''' WHERE [INDEX]=1
		--中间的需要加上空格与OR
		UPDATE @ConditionTbl SET AreaNameOrID = ' OR ' + @AreaNameOrID + '=''' + AreaNameOrID + ''''
		WHERE [INDEX]<>1 AND [INDEX]<>@ConditionSum
		--最后条需要加上空格与OR与最右边的括号
		UPDATE @ConditionTbl SET AreaNameOrID =  ' OR ' + @AreaNameOrID + '=''' + AreaNameOrID + ''')'
		WHERE [INDEX]=@ConditionSum
	END
	
	--组合成完整的语句
	SET @Sql = ''
	DECLARE @ConditionCursor CURSOR, @Condition nvarchar(max)
	SET @ConditionCursor = CURSOR SCROLL FOR 
	SELECT AreaNameOrID FROM @ConditionTbl ORDER BY [INDEX] ASC
	OPEN @ConditionCursor
	FETCH NEXT FROM @ConditionCursor INTO @Condition
	WHILE @@FETCH_STATUS=0
	BEGIN
		SET @Sql = @Sql + @Condition
		FETCH NEXT FROM @ConditionCursor INTO @Condition
	END
	CLOSE @ConditionCursor
	DEALLOCATE @ConditionCursor
	
	RETURN @Sql
END
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_System_GetSerialNumber]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[SRLS_FUN_System_GetSerialNumber]
(
	@SerialCode nvarchar(16)
)
Returns nvarchar(16)
As
/*
获取编号信息
*/
Begin
	DECLARE @SerialNumber nvarchar(8),
			@SerialRule nvarchar(16),
			@SerialLength int

	IF NOT EXISTS(SELECT 1 FROM SRLS_TB_System_SerialNumber where SerialCode=@SerialCode)
		Set @SerialRule = ''
	ELSE
	BEGIN
		SELECT @SerialNumber=cast(SerialNumber as varchar(8)),
			 @SerialRule=SerialRule,
			 @SerialLength=SerialLength
			FROM SRLS_TB_System_SerialNumber 
			WHERE SerialCode=@SerialCode
		
		WHILE Len(@SerialNumber) < @SerialLength
		Begin
			SET @SerialNumber = '0'+@SerialNumber
		END
		Set @SerialRule = Replace(@SerialRule,'S',@SerialNumber)
		-- UPDATE SRLS_TB_System_SerialNumber SET SerialNumber=SerialNumber+1 where SerialCode=@SerialCode
	END

	Return @SerialRule
End
GO
/****** Object:  UserDefinedFunction [dbo].[SRLS_FUN_System_GetLastDataInfo]    Script Date: 08/16/2012 19:28:28 ******/
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
CREATE Function [dbo].[SRLS_FUN_System_GetLastDataInfo](
	@LogType int,
	@SourceID nvarchar(64),
	@TableName nvarchar(64),
	@UpdateTime datetime)
Returns xml
As
Begin
	Declare @Res xml;
	--
	IF @LogType=0 or @LogType=2
		Set @Res = NULL;
	ELSE BEGIN
		SELECT TOP 1 @Res=DataInfo From dbo.SRLS_TB_System_UserLog
		WHERE TableName=@TableName AND SourceID=@SourceID AND UpdateTime<@UpdateTime
		ORDER BY UpdateTime DESC;
	END
	--
	Return @Res;
End
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetMinOrMaxSalesDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetMinOrMaxSalesDate]
(
	@StoreNo nvarchar(50)='',
	@KioskNo nvarchar(50)='',
	@IsMin BIT=1
)
RETURNS DATE
AS
BEGIN
	DECLARE @RetVal DATE
	DECLARE @ForecastDate DATE, @RealDate DATE
	IF @KioskNo <> ''
	BEGIN
		IF @IsMin=1
		BEGIN
			SELECT @ForecastDate = MIN(SalesDate) FROM Forecast_Sales WHERE KioskNo=@KioskNo
			SELECT @RealDate = MIN(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
			
			--如果真实数据有值，肯定取真实数据值作为租金最小时间
			IF @RealDate IS NOT NULL
				SET @RetVal = @RealDate
			ELSE 
				SET @RetVal = @ForecastDate
		END
		ELSE
		BEGIN
			SELECT @ForecastDate = MAX(SalesDate) FROM Forecast_Sales WHERE KioskNo=@KioskNo
			SELECT @RealDate = MAX(SalesDate) FROM RLPlanning_RealSales WHERE KioskNo=@KioskNo
			
			--如果预测数据有值，肯定取预测时间最大值作为租金最大时间
			IF @ForecastDate IS NOT NULL
				SET @RetVal = @ForecastDate
			ELSE 
				SET @RetVal = @RealDate
		END
	END
	ELSE IF @StoreNo <> ''
	BEGIN
		IF @IsMin=1
		BEGIN
			SELECT @ForecastDate = MIN(SalesDate) FROM Forecast_Sales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			SELECT @RealDate = MIN(SalesDate) FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			
			--如果真实数据有值，肯定取真实数据值作为租金最小时间
			IF @RealDate IS NOT NULL
				SET @RetVal = @RealDate
			ELSE 
				SET @RetVal = @ForecastDate
		END
		ELSE
		BEGIN
			SELECT @ForecastDate = MAX(SalesDate) FROM Forecast_Sales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			SELECT @RealDate = MAX(SalesDate) FROM RLPlanning_RealSales WHERE StoreNo=@StoreNo AND KioskNo IS NULL
			
			--如果预测数据有值，肯定取预测时间最大值作为租金最大时间
			IF @ForecastDate IS NOT NULL
				SET @RetVal = @ForecastDate
			ELSE 
				SET @RetVal = @RealDate
		END
	END
	
	--如果是要取sales最大值，则需要将时间设置到月底
	IF @IsMin=0
		SET @RetVal = DATEADD(DAY,-1,DATEADD(MONTH,1,@RetVal))
		
	RETURN @RetVal
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesStartEndDate]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetSalesStartEndDate]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS @Result TABLE(SalesStartDate datetime, SalesEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @SalesStartDate datetime, @SalesEndDate datetime
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		--如果真实sales数据里没有数据，则去预测数据表中查找
		SELECT @SalesStartDate = MIN(SalesDate) FROM RLPlanning_RealSales 
		WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		
		IF @SalesStartDate IS NULL
		BEGIN
			SELECT @SalesStartDate = MIN(SalesDate) FROM Forecast_Sales 
			WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		END
		
		SELECT @SalesEndDate = MAX(SalesDate) FROM Forecast_Sales 
		WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')
		
		IF @SalesEndDate IS NULL
		BEGIN
			SELECT @SalesEndDate = MAX(SalesDate) FROM RLPlanning_RealSales 
			WHERE StoreNo = @StoreNo AND (KioskNo IS NULL OR KioskNo = '')	
		END
		
		--销售数据结束时间需要设置到月底
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	--甜品店
	ELSE
	BEGIN
		--如果真实sales数据里没有数据，则去预测数据表中查找
		SELECT @SalesStartDate = MIN(SalesDate) FROM RLPlanning_RealSales 
		WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
		
		IF @SalesStartDate IS NULL
		BEGIN
			SELECT @SalesStartDate = MIN(SalesDate) FROM Forecast_Sales 
			WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
		END
		
		SELECT @SalesEndDate = MAX(SalesDate) FROM Forecast_Sales 
		WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
		
		IF @SalesEndDate IS NULL
		BEGIN
			SELECT @SalesEndDate = MAX(SalesDate) FROM RLPlanning_RealSales 
			WHERE StoreNo = @StoreNo AND KioskNo = @KioskNo
		END
		
		--销售数据结束时间需要设置到月底
		SET @SalesEndDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesEndDate))	
	END
	
	--结果输出
	INSERT INTO @Result SELECT @SalesStartDate, @SalesEndDate
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesByYearAndMonth]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION  [dbo].[RLPlanning_FN_GetSalesByYearAndMonth]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50),
	@Year nvarchar(50),
	@Month nvarchar(50),
	@IsForecastSales bit=1 --取预测sales数据还是真实sales数据的标志位，默认为取预测sales数据, 如果是NULL表示从两边都取数值
)
RETURNS decimal(18,2)
AS
BEGIN
	DECLARE @Sales DECIMAL(18,2)
	DECLARE @ForecastSales DECIMAL(18,2), @RealSales DECIMAL(18,2)
	
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo=''
	BEGIN
		--预测数据
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
		--真实数据
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND (KioskNo IS NULL OR KioskNo='')
	END
	--甜品店
	ELSE
	BEGIN
		--预测数据
		SELECT @ForecastSales = Sales FROM Forecast_Sales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND KioskNo = @KioskNo
		--真实数据
		SELECT @RealSales = Sales FROM RLPlanning_RealSales 
		WHERE SalesDate = @Year + '-' + @Month + '-1' AND StoreNo = @StoreNo
													  AND KioskNo = @KioskNo	
	END	
	
	--两者都取，如果都有数据，以真实数据为准
	IF @IsForecastSales IS NULL
	BEGIN
		IF @ForecastSales IS NOT NULL AND @RealSales IS NOT NULL
			SET @Sales = @RealSales
		ELSE IF @ForecastSales IS NULL AND @RealSales IS NOT NULL
			SET @Sales = @RealSales
		ELSE IF @ForecastSales IS NOT NULL AND @RealSales IS NULL
			SET @Sales = @ForecastSales
		ELSE
			SET @Sales = NULL
	END
	--取预测数据
	ELSE IF @IsForecastSales = 1
	BEGIN
		SET @Sales = @ForecastSales
	END
	--取真实数据
	ELSE IF @IsForecastSales = 0
	BEGIN
		SET @Sales = @RealSales
	END

	RETURN @Sales
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SerialNumber_Contract]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	获取流水号  合同编号在合同新增、变更、续租流程通过审核后生成。规则为：公司编号.流水号 流水号为6位
-- =============================================
CREATE FUNCTION [dbo].[fn_SerialNumber_Contract]
(
	@CompanyCode NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @CurrentNumber NVARCHAR(50)
	
	SELECT @CurrentNumber=MAX(CAST(RIGHT(ContractNo,6) AS INT)) + 1
	FROM [Contract] 
	-- Modified by Eric
	WHERE (ContractNo IS NOT NULL AND  ContractNo<>'' AND CHARINDEX('v', ContractNO)=1) AND CompanyCode=@CompanyCode
	-- End
	IF @CurrentNumber IS NULL 
	BEGIN
		SELECT @CurrentNumber='1'
	END
		
		WHILE LEN(@CurrentNumber)<6
			BEGIN
				SELECT @CurrentNumber = '0'+@CurrentNumber
			END
		-- Modified by Eric
		SELECT @ResultVar = 'v'+@CompanyCode+'.'+@CurrentNumber
		-- End
		
	RETURN @ResultVar 

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Remind_GetFeedbackUserIDs]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取事项反馈的相关人的ID串
-- =============================================
CREATE FUNCTION [dbo].[fn_Remind_GetFeedbackUserIDs]
(
	-- Add the parameters for the function here
	@RemindID NVARCHAR(50)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)
	SELECT @ResultVar=''
	SELECT @ResultVar=@ResultVar+CAST(UserID AS NVARCHAR(50))+';'
	FROM (
		SELECT DISTINCT k.UserID FROM SRLS_TB_Matters_Feedback k
		INNER JOIN dbo.SRLS_TB_Master_User u ON k.UserID=u.ID 
		WHERE k.RemindID=@RemindID AND u.Deleted=1
	) x
	
	SET @ResultVar=SUBSTRING(@ResultVar,0,LEN(@ResultVar))
	RETURN  @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_CalTaskNo]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取给定时间的TaskNo
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_CalTaskNo]
(
	@Date DATE
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @TmpTable table(TaskSerialNo int)
	DECLARE @Result nvarchar(50), @SerialNo int
	DECLARE @StrDate nvarchar(50)
	
	--TaskNo格式是YYYYMMDD.XXXXXX,所以后缀Number的substring是substring(@str,10,6)
	--Date转成nvarchar的格式是YYYY-MM-DD
	--YYYY的substring是substring(@str,1,4)
	--MM的substring是substring(@str,6,2)
	--DD的substring是substring(@str,9,2)
	--YYYYMMDD的substring是substring(@str,1,8)
	
	--将YYYY-MM-DD转换成YYYYMMDD
	SET @StrDate = CAST(@Date AS nvarchar(50))
	SET @StrDate = SUBSTRING(@StrDate,1,4) + SUBSTRING(@StrDate,6,2) + SUBSTRING(@StrDate,9,2)
	
	--将同一时间下的TaskSerial都存储下来，再找后缀No的最大值，然后转成字符串，即为新的TaskNo
	INSERT INTO @TmpTable
	SELECT CAST(SUBSTRING(TaskNo,10,6) AS INT) FROM Forecast_CheckResult
	WHERE @StrDate = SUBSTRING(TaskNo,1,8)
	
	--如果没有在同一时间下产生的流水号，则产生新的流水号
	IF NOT EXISTS (SELECT * FROM @TmpTable)
	BEGIN
		SET @Result = @StrDate + '.000001'
	END
	--已经有流水号了，则取最大的流水号加1
	ELSE
	BEGIN
		SELECT @SerialNo = MAX(TaskSerialNo) FROM @TmpTable
		SET @SerialNo += 1
		
		--转成nvarchar，如果长度不满6位，则自动在前面加0
		DECLARE @StrSerialNo nvarchar(50)
		SET @StrSerialNo = CAST(@SerialNo AS nvarchar(50))
		
		DECLARE @Length int
		SET @Length = LEN(@StrSerialNo)
		WHILE @Length < 6
		BEGIN
			SET @StrSerialNo = '0' + @StrSerialNo
			SET @Length += 1	
		END	
		
		SET @Result = @StrDate + '.' + @StrSerialNo
	END

	RETURN @Result
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_GetStoreKioskYearSales]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_RLP_GetStoreKioskYearSales] (
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(32),
	@KioskNo NVARCHAR(32)=NULL,
	@year int
)
RETURNS decimal(18, 2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar decimal(18, 2)=0;
	--
	SELECT @ResultVar=SUM([Sales]) FROM (
		SELECT StoreNo, KioskNo, SalesDate, Sales
		FROM dbo.[RLPlanning_RealSales] WHERE StoreNo=@StoreNo AND YEAR(SalesDate)=@year 
			AND ((@KioskNo IS NULL AND KioskNo IS NULL) OR (@KioskNo IS NOT NULL AND KioskNo=@KioskNo))
		--
		union
		--
		SELECT StoreNo, KioskNo, SalesDate, Sales
		FROM dbo.[Forecast_Sales] AS FS WHERE FS.StoreNo=@StoreNo AND YEAR(FS.SalesDate)=@year 
			AND ((@KioskNo IS NULL AND KioskNo IS NULL) OR (@KioskNo IS NOT NULL AND KioskNo=@KioskNo))
			AND not exists(select 1 from dbo.[RLPlanning_RealSales] AS RS where RS.StoreNo=FS.StoreNo AND RS.SalesDate=FS.SalesDate
				AND ((FS.KioskNo is null and RS.KioskNo is null) or (FS.KioskNo is not null and RS.KioskNo=FS.KioskNo)))
	) AS tbl;
	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetSysParamValueByCode]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetSysParamValueByCode]
(
	@ParamCode NVARCHAR(50)
)
RETURNS NVARCHAR(2000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(2000)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=ParamValue FROM SYS_Parameters WHERE ParamCode=@ParamCode

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetPointWorkDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取工作日后的日期
-- =============================================
CREATE FUNCTION [dbo].[fn_GetPointWorkDate]
(
	@StartDate DATETIME,
	@WorkDateCount INT 
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	DECLARE @TempDate DATETIME
	SELECT @TempDate=@StartDate
	IF @WorkDateCount > 0
	BEGIN
		WHILE(dbo.fn_GetWorkDateCout(@StartDate,@TempDate))<@WorkDateCount
		BEGIN
			SELECT @TempDate=@TempDate+1
		END
	END 
	ELSE IF @WorkDateCount < 0
	BEGIN
		WHILE(dbo.fn_GetWorkDateCout(@TempDate,@StartDate))<-@WorkDateCount
		BEGIN
			SELECT @TempDate=@TempDate-1
		END
	END
	ELSE
	BEGIN
		SELECT @TempDate=@StartDate
	END
	
	
	SELECT @ResultVar=dbo.fn_GetDate(@TempDate)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetKioskSalesDatePointByKioskID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<获取某个餐厅挂靠时间段内的KioskSales时间点>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetKioskSalesDatePointByKioskID]
(	
	@KioskID NVARCHAR(50),    --待获取的KioskID
	@StoreNo NVARCHAR(50),    --当前时间段内挂靠的餐厅
	@StartDate DATETIME,      --时间段开始日期
	@EndDate DATETIME,        --时间段结束日期
	@IsNeedSubtractSalse BIT  --是否从母店中扣除
)
RETURNS @Result TABLE(DatePoint DATETIME ,IsStart BIT)
AS
BEGIN
	--定义时间点表
	DECLARE @DatePointTable TABLE(DatePoint DATETIME ,IsStart BIT)

	--从时间段临时表中获取时间点
	IF @IsNeedSubtractSalse = 1
	BEGIN
		--若当前Kiosk在该时间段需要从母店中扣除，则时间点中需要包括餐厅的时间点
		INSERT INTO @DatePointTable (DatePoint,IsStart)
		SELECT DatePoint,IsStart FROM
		(
			SELECT DISTINCT StartDate AS DatePoint, 1 AS IsStart FROM dbo.KioskSalesTempTable WHERE (KioskID=@KioskID AND TempType='餐厅' AND RelationData=@StoreNo) OR (KioskID=@KioskID AND TempType='甜品店')
			UNION 
			SELECT DISTINCT EndDate AS DatePoint, 0 AS IsStart FROM dbo.KioskSalesTempTable WHERE (KioskID=@KioskID AND TempType='餐厅' AND RelationData=@StoreNo) OR (KioskID=@KioskID AND TempType='甜品店')
		) AS t0 ORDER BY DatePoint ASC, IsStart DESC
	END
	ELSE
	BEGIN
		--若当前时间段无需从母店中扣除，则时间点中无需要包括餐厅的时间点
		INSERT INTO @DatePointTable (DatePoint,IsStart)
		SELECT DatePoint,IsStart FROM
		(
			SELECT DISTINCT StartDate AS DatePoint, 1 AS IsStart FROM dbo.KioskSalesTempTable WHERE KioskID=@KioskID AND TempType='甜品店'
			UNION 
			SELECT DISTINCT EndDate AS DatePoint, 0 AS IsStart FROM dbo.KioskSalesTempTable WHERE KioskID=@KioskID AND TempType='甜品店'
		) AS t0 ORDER BY DatePoint ASC, IsStart DESC
	END
	
	--插入起始时间点
	INSERT INTO @DatePointTable (DatePoint,IsStart) VALUES (@StartDate, 1)
	INSERT INTO @DatePointTable (DatePoint,IsStart) VALUES (@EndDate, 0)
	
	--返回最终时间点结果
	INSERT INTO @Result (DatePoint,IsStart)
	SELECT DatePoint,IsStart FROM @DatePointTable WHERE DatePoint >= @StartDate AND DatePoint <= @EndDate
	RETURN 
END
--SELECT * FROM dbo.fn_GetKioskSalesDatePointByKioskID('71a8b09b-add0-49a7-9993-7581171f7fe9', '1900-1-1','2011-4-26',0)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPPaidFixedAmount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取返回金额
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAPPaidFixedAmount]
(
	-- Add the parameters for the function here
	@APRecordID NVARCHAR(50)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	DECLARE @EntityInfoSettingID NVARCHAR(50),@CycleStartDate DATETIME,@CycleEndDate DATETIME
	DECLARE @RuleSnapshotID NVARCHAR(50),@RentType NVARCHAR(50)

	
	SELECT @EntityInfoSettingID=EntityInfoSettingID,
		@RuleSnapshotID=RuleSnapshotID,
		@RentType=RentType,
		@CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate
	FROM dbo.APRecord 
	WHERE APRecordID=@APRecordID
	


	--只返回对应类型的AP金额, 如当前租金是百分比管理费,对应返回的为固定管理费
	SELECT @ResultVar=SUM(APAmount) 
	FROM dbo.APRecord 
	WHERE EntityInfoSettingID=@EntityInfoSettingID
		AND ((CycleStartDate BETWEEN @CycleStartDate AND @CycleEndDate) OR (CycleEndDate BETWEEN @CycleStartDate AND @CycleEndDate))
		AND RentType = REPLACE(@RentType,'百分比','固定')
		AND APRecordID<>@APRecordID

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=ISNULL(@ResultVar,0)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPPaidRatioAmount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取返回金额
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAPPaidRatioAmount]
(
	-- Add the parameters for the function here
	@APRecordID NVARCHAR(50)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	DECLARE @EntityInfoSettingID NVARCHAR(50),@CycleStartDate DATETIME,@CycleEndDate DATETIME
	DECLARE @RuleSnapshotID NVARCHAR(50),@RentType NVARCHAR(50)

	
	SELECT @EntityInfoSettingID=EntityInfoSettingID,
		@RuleSnapshotID=RuleSnapshotID,
		@RentType=RentType,
		@CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate
	FROM dbo.APRecord 
	WHERE APRecordID=@APRecordID
	


	--只返回对应类型的AP金额, 如当前租金是百分比管理费,对应返回的为固定管理费
	SELECT @ResultVar=SUM(APAmount) 
	FROM dbo.APRecord 
	WHERE EntityInfoSettingID=@EntityInfoSettingID
		AND ((CycleStartDate BETWEEN @CycleStartDate AND @CycleEndDate) OR (CycleEndDate BETWEEN @CycleStartDate AND @CycleEndDate))
		AND RentType = @RentType
		AND APRecordID<>@APRecordID

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=ISNULL(@ResultVar,0)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetEntityAPGLIsRunning]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		liujj
-- Create date:20110422
-- Description:	获取实体是否有关联的AP/GL
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetEntityAPGLIsRunning]
(
	@EntityID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	DECLARE @ResultVar BIT
	IF EXISTS	(		SELECT 1 FROM 			(				SELECT 1 AS Running FROM dbo.APRecord AS apr 				WHERE apr.EntityID=@EntityID AND apr.CreatedCertificateTime IS NULL				UNION ALL				SELECT 1 AS Running FROM dbo.GLRecord AS glr 				WHERE glr.EntityID=@EntityID AND glr.CreatedCertificateTime IS NULL			) x
	)
		SELECT @ResultVar = 1
	ELSE
		SELECT @ResultVar = 0	

	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetContractID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetContractID]
(
	-- Add the parameters for the function here
	@ContractSnapshotID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=ContractID FROM dbo.[Contract] WHERE ContractSnapshotID=@ContractSnapshotID

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetCompanyCodeByStoreOrDeptNo]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	根据餐厅部门编号获取公司编号
-- =============================================
CREATE FUNCTION [dbo].[fn_GetCompanyCodeByStoreOrDeptNo] 
(
	@StoreOrDeptNo NVARCHAR(50)
)
RETURNS NVARCHAR(32)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(32)
	SET @ResultVar=(SELECT TOP 1 CompanyCode FROM dbo.SRLS_TB_Master_Store WHERE StoreNo=@StoreOrDeptNo)
	IF @ResultVar IS NULL
	BEGIN
		SET @ResultVar=(SELECT TOP 1 CompanyCode FROM dbo.SRLS_TB_Master_Department WHERE DeptCode=@StoreOrDeptNo)
	END
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLScCd]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <2011-06-29>
-- Description:	获取GL凭证的ScCd
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetGLScCd]
(
	@GLRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ScCd NVARCHAR(50)
	DECLARE @Co NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	SELECT TOP 1  @Co=CompanyCode, @PayType=RentType FROM dbo.GLRecord WHERE GLRecordID=@GLRecordID
	IF @Co IS NOT NULL AND @PayType IS NOT NULL
	BEGIN
		IF CHARINDEX('固定租金',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT FixedSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('固定管理费',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT FixedManageSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('固定服务费',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT FixedServiceSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('直线租金',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT StraightLineSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('百分比租金',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT RatioSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('百分比管理费',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT RatioManageSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
		IF CHARINDEX('百分比服务费',@PayType) > 0
		BEGIN
			SET @ScCd=(SELECT RatioServiceSourceCode FROM dbo.SRLS_TB_Master_Company WHERE CompanyCode=@Co)
		END
	END	
	RETURN @ScCd
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLRemainingDayCount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	获取GL结算周期剩余天数
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetGLRemainingDayCount]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50),
	@TempGLStartDate DATETIME,
	@APEndDate DATETIME
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	-- Add the T-SQL statements to compute the return value here
	DECLARE @RuleSnapshotID NVARCHAR(50),@TimeIntervalEndDate DATETIME
	
	SELECT @RuleSnapshotID=RuleSnapshotID 
	FROM dbo.GLRecord 
	WHERE GLRecordID=@GLRecordID
	
	--取出离GL开始时间最近的时间段点
	SELECT TOP 1 @TimeIntervalEndDate=dbo.fn_GetDate(EndDate) 
	FROM dbo.RatioTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
		AND (EndDate BETWEEN @TempGLStartDate AND @APEndDate)
	ORDER BY EndDate ASC 
	
	--中间没有被时间段切断
	IF @TimeIntervalEndDate IS NULL 
	BEGIN
		SELECT @ResultVar = DATEDIFF(DAY,@TempGLStartDate,@APEndDate)+1
	END
	ELSE --中间被时间段切断
	BEGIN
		SELECT @ResultVar = DATEDIFF(DAY,@TempGLStartDate,@TimeIntervalEndDate)+1
	END

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetCycleGLCount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取AP期间内GL条数
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetCycleGLCount]
(
	-- Add the parameters for the function here
	@APRecordID NVARCHAR(50)
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	-- Add the T-SQL statements to compute the return value here
	DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME,@RuleID NVARCHAR(50)
	SELECT @CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate,
		@RuleID=RuleID
	FROM dbo.APRecord 
	WHERE APRecordID=@APRecordID
	
	SELECT @ResultVar=COUNT(*) 
	FROM 
	(
		SELECT 1 AS record FROM dbo.GLRecord
		WHERE RuleID=@RuleID 
				AND (CycleStartDate BETWEEN @CycleStartDate AND @CycleEndDate)
				AND (CycleEndDate BETWEEN @CycleStartDate AND @CycleEndDate)
		UNION ALL 
		SELECT 1 
		FROM dbo.GLAdjustment
		WHERE RuleID=@RuleID
			AND dbo.fn_GetDate(AdjustmentDate) BETWEEN @CycleStartDate AND @CycleEndDate
	) x
	
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetGroupIDByUserID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	获取指定用户ID的管理员ID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetGroupIDByUserID]
(
	@ID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	SET @ResultVar=(SELECT GroupID FROM dbo.SRLS_TB_Master_User WHERE ID=@ID)
	
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAttCount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: 20110107
-- Description:	获取附件数量
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAttCount]
(
	@Category NVARCHAR(50),
	@ObjectID NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar  = COUNT(*) FROM SYS_Attachments WHERE Category=@Category AND ObjectID=@ObjectID

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetMaxMissCloseDateByStoreNo]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取餐厅最大的关帐日期
-- =============================================
CREATE FUNCTION [dbo].[fn_GetMaxMissCloseDateByStoreNo]
(
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=CONVERT(NVARCHAR(10),MAX(SalesDate),112)
	FROM dbo.StoreSales
	WHERE IsCASHSHEETClosed=1 AND StoreNo=@StoreNo

	SELECT @ResultVar=ISNULL(@ResultVar,'19000101')
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAdministratorUserID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	获取管理USERID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAdministratorUserID]
(
	
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	SELECT @ResultVar=dbo.fn_GetSysParamValueByCode('AdministratorUserID')
	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetStoreOrDeptNo]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: <Create Date, ,>
-- Description:	获取简单流程的餐厅部门编号
-- =============================================
CREATE FUNCTION [dbo].[fn_GetStoreOrDeptNo]
(
	-- Add the parameters for the function here
	@ProcID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
--1	说明事项
--2	跟进事项
--3	Check List事项
--4	Lease Master事项
--5	TypeCode新增
--6	TypeCode修改
--7	Kiosk新增
--8	Kiosk修改
--9	合同新增
--10	合同变更
--11	合同续租
--12	KioskSalse收集
--13	GL计算
--14	AP计算
--15	AP差异
--16	Kiosk删除
--17	事项提醒

	DECLARE @AppCode INT,@AppName NVARCHAR(50),@DataLocator NVARCHAR(50)
	SELECT @AppCode=AppCode,@AppName=AppName,@DataLocator=DataLocator
	FROM dbo.WF_Proc_Inst 
	WHERE ProcID=@ProcID
	
	--事项
	IF (@AppCode BETWEEN 1 AND 4) OR (@AppCode BETWEEN 17 AND 18)
	BEGIN
		SELECT @ResultVar=SiteDeptNo 
		FROM dbo.SRLS_TB_Matters_Remind 
		WHERE ID=@DataLocator
	END
	
	--TypeCode
	IF @AppCode BETWEEN 5 AND 6
	BEGIN
		SET @ResultVar=NULL 
	END
	
	--Kiosk
	IF (@AppCode BETWEEN 7 AND 8) OR @AppCode=12 OR @AppCode=16
	BEGIN
		SELECT @ResultVar=TemStoreNo 
		FROM dbo.SRLS_TB_Master_Kiosk 
		WHERE KioskID=@DataLocator
	END
	
	--合同
	IF (@AppCode BETWEEN 9 AND 11) OR @AppCode = 19
	BEGIN
		SELECT TOP 1  @ResultVar=e.StoreOrDeptNo FROM dbo.[Contract] c 
		INNER JOIN dbo.Entity e ON c.ContractSnapshotID = e.ContractSnapshotID
		WHERE c.ContractSnapshotID=@DataLocator
	END
	

	--GL
	IF @AppCode = 13
	BEGIN
		SELECT @ResultVar=e.StoreOrDeptNo FROM dbo.GLRecord gl
		INNER JOIN dbo.Entity e ON gl.EntityID = e.EntityID
		WHERE gl.GLRecordID=@DataLocator
	END
	
	
	--AP
	IF @AppCode BETWEEN 14 AND 15
	BEGIN
		SELECT @ResultVar=e.StoreOrDeptNo FROM dbo.APRecord gl
		INNER JOIN dbo.Entity e ON gl.EntityID = e.EntityID
		WHERE gl.APRecordID=@DataLocator
	END
	
	
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetBeforeSumGLAmount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	计算四则表达式
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetBeforeSumGLAmount]
(
	-- Add the parameters for the function here
	@RecordID NVARCHAR(50), --APGLID
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	
	DECLARE @RuleID NVARCHAR(50),@RuleSnapshotID NVARCHAR(50)
	SELECT @RuleID=RuleID,@RuleSnapshotID=RuleSnapshotID 
	FROM 
	(
		SELECT RuleID,RuleSnapshotID FROM dbo.APRecord WHERE APRecordID=@RecordID
		UNION ALL 
		SELECT RuleID,RuleSnapshotID FROM dbo.GLRecord WHERE GLRecordID=@RecordID
	)  x WHERE RuleID IS NOT NULL 
		--累计已预提
		SELECT @ResultVar=SUM(GLAmount) 
		FROM
		(
			SELECT glt.GLAmount FROM dbo.GLRecord gl 
			INNER JOIN dbo.GLTimeIntervalInfo glt ON gl.GLRecordID = glt.GLRecordID
			WHERE gl.RuleID=@RuleID 
			AND (glt.CycleStartDate BETWEEN @StartDate AND @EndDate)
			AND gl.GLType<>'直线租金'
			UNION ALL 
			SELECT Amount
			FROM dbo.GLAdjustment 
			WHERE (dbo.fn_GetDate(AdjustmentDate) BETWEEN @StartDate AND @EndDate)
			AND  RuleID=@RuleID
		) x
	

	SELECT @ResultVar=ISNULL(@ResultVar,0)
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetStoreSales]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110309
-- Description:	获取餐厅SALES
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetStoreSales]
(
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)

	SELECT @ResultVar=ISNULL(SUM(Sales),0) FROM dbo.StoreSales
	WHERE StoreNo=@StoreNo
		AND DATEDIFF(DAY, @StartDate,SalesDate)>=0 AND DATEDIFF(DAY,SalesDate,@EndDate)>=0
	
	RETURN @ResultVar


END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ExistsAPGLRunningWorkflow]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取指定的Kiosk或其所属母店是否存在运行的APGL流程
-- =============================================
CREATE FUNCTION [dbo].[fn_ExistsAPGLRunningWorkflow]
(
	@KioskNo NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
    DECLARE @APGLRecordID NVARCHAR(50)
    DECLARE @StoreNo NVARCHAR(50)
    
    SET @StoreNo=(SELECT TOP 1 StoreNo FROM dbo.SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskNo)
    SET @APGLRecordID=(SELECT TOP 1 APRecordID FROM dbo.APRecord WHERE EntityID=(SELECT TOP 1 EntityID FROM dbo.Entity WHERE EntityTypeName='甜品店' AND KioskNo=@KioskNo))
	
	IF @APGLRecordID IS NULL
		SET @APGLRecordID=(SELECT TOP 1 GLRecordID FROM dbo.GLRecord WHERE EntityID=(SELECT TOP 1 EntityID FROM dbo.Entity WHERE EntityTypeName='甜品店' AND KioskNo=@KioskNo))
	
	IF @APGLRecordID IS NULL
	BEGIN
		SET @APGLRecordID=(SELECT TOP 1 APRecordID FROM dbo.APRecord WHERE EntityID=(SELECT TOP 1 EntityID FROM dbo.Entity WHERE EntityTypeName='餐厅' AND StoreOrDeptNo=@StoreNo))
		IF @APGLRecordID IS NULL
			SET @APGLRecordID=(SELECT TOP 1 GLRecordID FROM dbo.GLRecord WHERE EntityID=(SELECT TOP 1 EntityID FROM dbo.Entity WHERE EntityTypeName='餐厅' AND StoreOrDeptNo=@StoreNo))
	END
	
	IF @APGLRecordID IS NULL OR EXISTS(SELECT * FROM dbo.v_TaskInfo vt WHERE vt.DataLocator=@APGLRecordID AND vt.TaskName='结束')
		RETURN 0
	RETURN 1
END
--SELECT dbo.fn_ExistsAPGLRunningWorkflow('2cb34250-2327-474b-926b-b84cefbeb8b1')
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetEntityAPMaxCycleEndDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	获取现创建APGL周期结束时间的最大值
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetEntityAPMaxCycleEndDate]
(
	-- Add the parameters for the function here
	@EntityID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	
	DECLARE @ContractID NVARCHAR(50)
	SELECT @ContractID=dbo.fn_Contract_GetContractID(ContractSnapshotID) 
	FROM dbo.Entity 
	WHERE EntityID=@EntityID

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=MAX(CycleEndDate) FROM 
	(
		SELECT 
			e.EntityTypeName,e.StoreOrDeptNo,e.EntityName,
			r.CycleStartDate,r.CycleEndDate
		FROM dbo.APRecord r 
		INNER JOIN dbo.Entity e ON r.EntityID = e.EntityID
		WHERE dbo.fn_Contract_GetContractID(r.ContractSnapshotID) = @ContractID
	) x INNER JOIN dbo.Entity e ON x.EntityTypeName = e.EntityTypeName 
		AND x.StoreOrDeptNo = e.StoreOrDeptNo 
		AND x.EntityName = e.EntityName
		
		

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Contract_GetEntityAPGLMaxCycleEndDate]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <Create Date, ,>
-- Description:	获取现创建APGL周期结束时间的最大值
-- =============================================
CREATE FUNCTION [dbo].[fn_Contract_GetEntityAPGLMaxCycleEndDate]
(
	-- Add the parameters for the function here
	@EntityID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	
	DECLARE @ContractID NVARCHAR(50)
	SELECT @ContractID=dbo.fn_Contract_GetContractID(ContractSnapshotID) 
	FROM dbo.Entity 
	WHERE EntityID=@EntityID

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=MAX(CycleEndDate) FROM 
	(
		SELECT 
			e.EntityTypeName,e.StoreOrDeptNo,e.EntityName,
			r.CycleStartDate,r.CycleEndDate
		FROM dbo.GLRecord r 
		INNER JOIN dbo.Entity e ON r.EntityID = e.EntityID
		WHERE dbo.fn_Contract_GetContractID(r.ContractSnapshotID) = @ContractID
		UNION ALL
		SELECT 
			e.EntityTypeName,e.StoreOrDeptNo,e.EntityName,
			r.CycleStartDate,r.CycleEndDate
		FROM dbo.APRecord r 
		INNER JOIN dbo.Entity e ON r.EntityID = e.EntityID
		WHERE dbo.fn_Contract_GetContractID(r.ContractSnapshotID) = @ContractID
	) x INNER JOIN dbo.Entity e ON x.EntityTypeName = e.EntityTypeName 
		AND x.StoreOrDeptNo = e.StoreOrDeptNo 
		AND x.EntityName = e.EntityName
		
		

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_IsNullGLExceptionRemark]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_IsNullGLExceptionRemark]
(
	@GLRecordID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	DECLARE @Result BIT
	IF EXISTS(SELECT GLRecordID FROM dbo.GLException WHERE GLRecordID=@GLRecordID AND (Remark IS NULL OR Remark='') AND IsMeetRule=1) 
		SET @Result=1
	ELSE
		SET @Result=0
	RETURN @Result
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_SelectNullRemarkGLException]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_SelectNullRemarkGLException]
(
	-- Add the parameters for the function here
	@GLRecordIDList NVARCHAR(4000) --流程串 分号分隔
)
RETURNS 
@ResultTable TABLE
(
	[GLExceptionID] [dbo].[ObjID]  ,
	[GLRecordID] [dbo].[ObjID] ,
	[GLType] [nvarchar](100)  ,
	[EnFlag] [nvarchar](50)  ,
	[SettingRule] [nvarchar](1000)  ,
	[DisplayValue] [nvarchar](1000)  ,
	[ValueItem] [nvarchar](1000)  ,
	[PreMonthValue] [decimal](18, 2) ,
	[ThisMonthValue] [decimal](18, 2) ,
	[IsMeetRule] [bit] ,
	[Remark] [nvarchar](2000)  ,
	[CreateTime] [datetime] 
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	INSERT INTO @ResultTable
	SELECT * FROM dbo.GLException WHERE Remark IS NULL AND IsMeetRule=1 AND 
	GLRecordID IN 
	(
			SELECT TokenVal FROM dbo.fn_Helper_Split(@GLRecordIDList,';')
	)
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AverageCompare1]    Script Date: 08/16/2012 19:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 20100108
-- Description: 年度销售额对比
-- =============================================
CREATE FUNCTION [dbo].[AverageCompare1]
(
	@RentYear nvarchar(50)
   ,@MinYear nvarchar(50)
)
RETURNS @ResultTable TABLE (

EntityID nvarchar(50)
,GLType nvarchar(50)
,GLAmount int
,Sales int

)
AS
BEGIN
	
insert into @ResultTable

select Entity.EntityID
		,gl.GLType
		,GL.GLAmount
		,sales.Sales
from Entity 	
left  join
(
		select
		EntityID
		,GLAmount
		,GLType
		from GLRecord
		where year(CycleStartDate)=year(@RentYear)

) gl
on Entity.EntityID=GL.EntityID 	 
left join
(
		select   
		StoreNo,KioskNo,	'Sales'=sum(Sales)
		from 
		(
			select * from RLPlanning_RealSales
			where YEAR(SalesDate)=year(@RentYear)
			union
			select * from Forecast_Sales where YEAR(SalesDate)=year(@RentYear)
		) m
		group by StoreNo,KioskNo
) sales 
on sales.StoreNo=Entity.StoreOrDeptNo and isnull(sales.KioskNo,'')=isnull(Entity.KioskNo,'')
where (Entity.OpeningDate >=@MinYear)

	
	
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetKioskResponsibleUserID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	甜品店负责人ID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetKioskResponsibleUserID]
(
	@KioskID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=c.ResponsibleUserID
	FROM         dbo.SRLS_TB_Master_Kiosk AS k INNER JOIN
                      dbo.SRLS_TB_Master_Store AS s ON k.StoreNo = s.StoreNo INNER JOIN
                      dbo.SRLS_TB_Master_Company AS c ON s.CompanyCode = c.CompanyCode
    WHERE k.KioskID=@KioskID

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_GetStoreKioskSalesDaysRatio]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_RLP_GetStoreKioskSalesDaysRatio] (
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(32),
	@KioskNo NVARCHAR(32)=NULL,
	@year INT
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18, 2)=0, @RentStartDate DATE=null, @RentEndDate DATE=null, 
		@syear DATE=(CAST(CAST(@year AS VARCHAR(4)) + '-01-01' AS DATE)), @eyear DATE=(CAST(CAST(@year AS VARCHAR(4)) + '-12-31' AS DATE));
	-- 基于合同
	SELECT @RentStartDate=MIN(RentStartDate), @RentEndDate=MAX(RentEndDate)
	FROM [RLPlanning].[dbo].[Entity]
	WHERE StoreOrDeptNo=@StoreNo 
		AND ((@KioskNo is null and (KioskNo is null or KioskNo='')) or (@KioskNo is not null and KioskNo=@KioskNo))
		AND ((RentStartDate<@RentStartDate and YEAR(RentEndDate)>=@year) or YEAR(RentStartDate)=@year)
		--
	IF @RentStartDate is not null BEGIN
		IF @RentStartDate<=@syear and @RentEndDate>=@eyear -- 覆盖一整年
			SET @ResultVar=1/12;
		ELSE -- 不满一年
			SET @ResultVar=30/DATEDIFF(DD, @RentStartDate, @RentEndDate);
	END
	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_GetStoreKioskRentTypeDaysRatio]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_RLP_GetStoreKioskRentTypeDaysRatio] (
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(32),
	@KioskNo NVARCHAR(32)=NULL,
	@RentType NVARCHAR(50),
	@year int
)
RETURNS decimal(18, 2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18, 2)=0, @CycleStartDate DATE=null, @CycleEndDate DATE=null, 
		@syear DATE=(CAST(CAST(@year AS VARCHAR(4)) + '-01-01' AS DATE)), @eyear DATE=(CAST(CAST(@year AS VARCHAR(4)) + '-12-31' AS DATE));
	-- 基于租金
	SELECT @CycleStartDate=MIN(CycleStartDate), @CycleEndDate=MAX(CycleEndDate)
	FROM dbo.[GLRecord] AS R
		INNER JOIN dbo.[Entity] AS E ON E.EntityID=R.EntityID
	WHERE E.StoreOrDeptNo=@StoreNo 
		AND ((@KioskNo is null and (E.KioskNo is null or E.KioskNo='')) or (@KioskNo is not null and E.KioskNo=@KioskNo))
		AND (CycleStartDate>=@syear and CycleEndDate<=@eyear)
		AND ((@RentType = '直线租金' AND GLType='直线租金') OR (@RentType<>'直线租金' AND GLType<>'直线租金' AND RentType=@RentType))
		--
	IF @CycleStartDate is not null BEGIN
		IF @CycleStartDate<=@syear and @CycleEndDate>=@eyear -- 覆盖一整年
			SET @ResultVar=1/12;
		ELSE -- 不满一年
			SET @ResultVar=30/DATEDIFF(DD, @CycleStartDate, @CycleEndDate);
	END
	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_CheckStoreOrKoiskContractStatus]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_RLP_CheckStoreOrKoiskContractStatus]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)=null,
	@YearA int,
	@YearB int,
	@ContainContractChange bit=null,
	@ContainContractExtend bit=null
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar bit=0, @cnt int=0;
	--
	DECLARE @tempTable TABLE (
		ContractID dbo.[ObjID],
		cnt int
	)
	INSERT INTO @tempTable SELECT C.ContractID, COUNT(E.ContractSnapshotID) AS cnt
		FROM dbo.[Entity] AS E 
			INNER JOIN dbo.[Contract] AS C ON C.ContractSnapshotID=E.ContractSnapshotID
		WHERE E.StoreOrDeptNo=@StoreNo AND
			((@KioskNo is null and (E.KioskNo is null or E.KioskNo='')) or (@KioskNo is not null and @KioskNo <> '' and E.KioskNo=@KioskNo))
			AND YEAR(E.RentStartDate)<=@YearA AND YEAR(E.RentEndDate)>=@YearB AND (@ContainContractExtend is null OR
				(@ContainContractExtend=0 and C.PartComment<>'续租') OR (@ContainContractExtend=1 and C.PartComment='续租'))
		GROUP BY C.ContractID;
	--
	if @ContainContractChange is not null begin
		SELECT @cnt=(CASE COUNT(*) when 0 then 1 else MAX(cnt) end) FROM @tempTable;
		--
		if @ContainContractChange=1 begin
			if @cnt=1
				SET @ResultVar=0;
			else
				SET @ResultVar=1;
		end else begin
			if @cnt=1
				SET @ResultVar=1;
			else
				SET @ResultVar=0;
		end
	end else begin
		SELECT @cnt=count(ContractID) FROM @tempTable;
		--
		if @cnt is not null begin
			if @cnt>0
				SET @ResultVar=1;
			else
				SET @ResultVar=0;
		end
	end
	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Report_GetGLException]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	GLException
-- =============================================
CREATE  FUNCTION [dbo].[fn_Report_GetGLException]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(4000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)
	SELECT @ResultVar=''
	SELECT @ResultVar=@ResultVar+SettingRule+'\r\n;'
	FROM (
		SELECT GLExceptionID,
			   GLRecordID,
			   GLType,
			   EnFlag,
			   SettingRule,
			   DisplayValue,
			   ValueItem,
			   PreMonthValue,
			   ThisMonthValue,
			   IsMeetRule,
			   Remark,
			   CreateTime FROM dbo.GLException WHERE GLRecordID=@GLRecordID AND IsMeetRule=1
	) X
	ORDER BY CreateTime ASC 
	
	IF LEN(@ResultVar)=0
	BEGIN
		SELECT @ResultVar = NULL 
	END
	

	RETURN  @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_IsTypeCodeAllAccountActive]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110304
-- Description:	判断某TYPECODE下是否所有科目都有效
-- =============================================
CREATE FUNCTION [dbo].[fn_IsTypeCodeAllAccountActive]
(
	-- Add the parameters for the function here
	@TypeCodeNameOrSnapshotID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT
	
	DECLARE @TypeCodeName NVARCHAR(50),@TypeCodeSnapshotID NVARCHAR(50)
	SELECT @TypeCodeName=TypeCodeName 
	FROM dbo.TypeCode 
	WHERE (TypeCodeName=@TypeCodeNameOrSnapshotID OR TypeCodeSnapshotID=@TypeCodeNameOrSnapshotID)
	AND SnapshotCreateTime IS NULL 
	
	--取出最新的快照ID
	SELECT @TypeCodeSnapshotID=TypeCodeSnapshotID 
	FROM dbo.TypeCode 
	WHERE TypeCodeName=@TypeCodeName
	AND SnapshotCreateTime IS NULL 

	-- Add the T-SQL statements to compute the return value here
	IF EXISTS(
	
			SELECT * FROM  
			(
			SELECT 	  YTGLDebit AS AccountNo FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YTGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YTAPNormal FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YTAPDifferences FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YFGLDebit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YFGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YFAPNormal FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   YFAPDifferences FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   BFGLDebit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   BFGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   ZXGLDebit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			UNION ALL  SELECT 	   ZXGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			) x  INNER JOIN dbo.Account a ON x.AccountNo = a.AccountNo
			WHERE x.AccountNo IS NOT NULL AND a.Status<>'A'
			)
	BEGIN
		SELECT @ResultVar=0
	END
	ELSE
	BEGIN
		SELECT @ResultVar=1
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetGLAmountSales]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 20100108
-- Description: 年度销售额对比
-- =============================================
CREATE FUNCTION [dbo].[GetGLAmountSales]
(
	@RentYear nvarchar(50)
   ,@MinYear nvarchar(50)
)
RETURNS @ResultTable TABLE (

EntityID nvarchar(50)
,GLType nvarchar(50)
,GLAmount int
,Sales int

)
AS
BEGIN
	
insert into @ResultTable

select Entity.EntityID
		,gl.GLType
		,GL.GLAmount
		,sales.Sales
from Entity 	
left  join
(
		select
		EntityID
		,GLAmount
		,GLType
		from GLRecord
		where year(CycleStartDate)=year(@RentYear)

) gl
on Entity.EntityID=GL.EntityID 	 
left join
(
		select   
		StoreNo,KioskNo,	'Sales'=sum(Sales)
		from 
		(
			select * from RLPlanning_RealSales
			where YEAR(SalesDate)=year(@RentYear)
			union
			select * from Forecast_Sales where YEAR(SalesDate)=year(@RentYear)
		) m
		group by StoreNo,KioskNo
) sales 
on sales.StoreNo=Entity.StoreOrDeptNo and isnull(sales.KioskNo,'')=isnull(Entity.KioskNo,'')
where (Entity.OpeningDate >=@MinYear)

	
	
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetGLAmount]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 20100108
-- Description: 年度销售额对比
-- =============================================
CREATE FUNCTION [dbo].[GetGLAmount]
(
	@RentYear nvarchar(50)
   ,@MinYear nvarchar(50)
)
RETURNS @ResultTable TABLE (

 EntityID nvarchar(50)
,GLType nvarchar(50)
,GLAmount int

)
AS
BEGIN
	
insert into @ResultTable

select Entity.EntityID
		,gl.GLType
		,GL.GLAmount
from Entity 	
left  join
(
		select
		EntityID
		,GLAmount
		,GLType
		from GLRecord
		
		where year(CycleStartDate)=year(@RentYear) and (EntityTypeName='餐厅' or EntityTypeName='甜饼站')

) gl
on Entity.EntityID=GL.EntityID 	 
where (Entity.OpeningDate >=@MinYear)

	
	
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fun_GetContractStatus]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fun_GetContractStatus] (
	@StoreNo nvarchar(50),
	@nYear1 int,
	@nYear2 int
) returns nvarchar(50)
as
begin
	declare @Result nvarchar(50)
	set @Result ='合同无变更'

	declare @nResult int
	set @nResult=0
         
	select @nResult=sum(case when [Contract].SnapshotCreateTime is null then 100 else 1 end)
	from [Contract], Entity
	where  Entity.StoreOrDeptNo=@StoreNo
		and  [Contract].ContractSnapshotID=Entity.ContractSnapshotID
		and not(
          (@nYear1<year(Entity.RentStartDate) and @nYear2<year(Entity.RentStartDate))
           or
          (@nYear1>year(Entity.RentEndDate) and @nYear2>year(Entity.RentEndDate)))
    --
	if(@nResult/100>1) begin
		if(@nResult%100>1) begin
		  set @Result='合同变更';
		end
	end
	--
	return @Result;
end
GO
/****** Object:  UserDefinedFunction [dbo].[fun_GetContainContinue]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fun_GetContainContinue](

 @StoreNo nvarchar(50),
 @nYear1 int,
 @nYear2 int

)
returns nvarchar(50)
as

begin

--declare @StoreNo nvarchar(50)
--set  @StoreNo='V000134'
--declare @nYear1 int
--declare @nYear2 int
--select @nYear1 =2012,@nYear2=2013

declare @Result nvarchar(50)
set @Result =''
         

--print @Result

select 
@Result=@Result+case when count(1)>=1 then '合同续租' else '合同无续租' end

 from [Contract],Entity
where  
    [Contract].ContractSnapshotID=Entity.ContractSnapshotID
    
   and Entity.StoreOrDeptNo=@StoreNo
  --and not(
  --        ( @nYear1<year(Entity.RentStartDate) and @nYear2<year(Entity.RentStartDate))
  --         or
  --        ( @nYear1>year(Entity.RentEndDate) and @nYear2>year(Entity.RentEndDate))
  --       )
  and Entity.RentStartDate>=cast(@nYear1 as nvarchar(50))+'0101'
  and Entity.RentEndDate<cast(@nYear1+1 as nvarchar(50))+'0101'
  
  and [Contract].SnapshotCreateTime  is null
  and [Contract].PartComment='续租'


--print @Result


return @Result
end





--GO
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TypeCode_SelectRentTypeByEntityTypeName]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100110
-- Description:根据实体类型名称获取租金类型
-- =============================================
CREATE FUNCTION [dbo].[fn_TypeCode_SelectRentTypeByEntityTypeName]
(
	@EntityTypeName NVARCHAR(50)
)
RETURNS @TabResult table (EntityTypeName NVARCHAR(50), RentTypeName NVARCHAR(50))
AS
BEGIN
  INSERT INTO @TabResult
  SELECT DISTINCT @EntityTypeName, RentTypeName 
  FROM TypeCode 
  WHERE EntityTypeName=@EntityTypeName
		AND [Status]='已生效'
		AND SnapshotCreateTime IS NULL 
  RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TypeCode_SelectRentTypeByEntityID]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100110
-- Description:根据实体ID名称获取租金类型
-- =============================================
CREATE FUNCTION [dbo].[fn_TypeCode_SelectRentTypeByEntityID]
(
	@EntityID NVARCHAR(50)
)
RETURNS @TabResult table (EntityID NVARCHAR(50), RentTypeName NVARCHAR(50))
AS
BEGIN
  INSERT INTO @TabResult
  SELECT DISTINCT EntityID, T.RentTypeName  
  FROM [Entity] E INNER JOIN TypeCode T ON E.EntityTypeName=T.EntityTypeName
  WHERE E.EntityID=@EntityID
		AND T.[Status]='已生效'
		AND T.SnapshotCreateTime IS NULL 
  RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TypeCode_IsTypeCodeExist]    Script Date: 08/16/2012 19:28:27 ******/
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
CREATE FUNCTION [dbo].[fn_TypeCode_IsTypeCodeExist]
(
	-- Add the parameters for the function here
	@TypeCodeSnapshotID NVARCHAR(50),
	@TypeCodeName NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT;
	--
	IF EXISTS(
		SELECT * FROM TypeCode 
		WHERE [TypeCodeName]=@TypeCodeName AND TypeCodeSnapshotID<>@TypeCodeSnapshotID)
	BEGIN
		SELECT @ResultVar=1;
	END ELSE BEGIN
		SELECT @ResultVar=0;
	END
	--
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TypeCode_IsEntityRentTypeExist]    Script Date: 08/16/2012 19:28:27 ******/
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
CREATE FUNCTION [dbo].[fn_TypeCode_IsEntityRentTypeExist]
(
	-- Add the parameters for the function here
	@TypeCodeSnapshotID NVARCHAR(50),
	@EntityTypeName NVARCHAR(50),
	@RentTypeName NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT;
	--
	IF EXISTS(
		SELECT * FROM TypeCode 
	    WHERE EntityTypeName=@EntityTypeName AND RentTypeName=@RentTypeName AND TypeCodeSnapshotID<>@TypeCodeSnapshotID)
	BEGIN
		SELECT @ResultVar=1;
	END ELSE BEGIN
		SELECT @ResultVar=0;
	END
	--
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RLP_GetStoreOpenDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*-------------------------------------------------------
Copyright(c) 2012 VIT。

功能描述：
创建标识：Create By Pattric Zzh
 
修改标识：
修改描述：
------------------------------------------------------*/
CREATE FUNCTION [dbo].[fn_RLP_GetStoreOpenDate]
(
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(32)
)
RETURNS DATE
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATE=null;
	--
	SELECT TOP 1 @ResultVar=OpenDate FROM 
	(
		SELECT TOP 1 OpeningDate AS OpenDate FROM dbo.[Entity] WHERE StoreOrDeptNo=@StoreNo ORDER BY OpeningDate
		UNION
		SELECT OpenDate FROM dbo.[SRLS_TB_Master_Store] WHERE StoreNo=@StoreNo AND OpenDate IS NOT NULL
	) AS D ORDER BY OpenDate;
	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SerialNumber_Kiosk]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	获取流水号 K+公司编号+4位流水号
-- =============================================
CREATE FUNCTION [dbo].[fn_SerialNumber_Kiosk]
(
	@CompanyCode NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @CurrentNumber NVARCHAR(50)
	SELECT @CurrentNumber=MAX(CAST(RIGHT(K.KioskNo,4) AS INT)) + 1
	FROM         dbo.SRLS_TB_Master_Kiosk AS K INNER JOIN
                      dbo.SRLS_TB_Master_Store AS S ON K.StoreNo = S.StoreNo
	WHERE  (KioskNo IS NOT NULL AND  KioskNo<>'') AND S.CompanyCode=@CompanyCode
	IF @CurrentNumber IS NULL 
	BEGIN
		SELECT @CurrentNumber='1'
	END
		
		WHILE LEN(@CurrentNumber)<4
			BEGIN
				SELECT @CurrentNumber = '0'+@CurrentNumber
			END
		SELECT @ResultVar = 'K'+@CompanyCode+'.'+@CurrentNumber
		
	RETURN @ResultVar 

END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FUN_BuildImportStyleSalesRecord]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	用来组合成导入sales表的数据条目
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FUN_BuildImportStyleSalesRecord]
(
	@MaxRealSalesDate date,
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
--基本思路，真实数据和预测数据在该年都不满12个月，分别插入2条记录，然后互相update，将所有sales的column填满，返回一条记录即可
RETURNS @Result TABLE(Company nvarchar(50), 餐厅编号 nvarchar(50), Store nvarchar(500), Kiosk nvarchar(500),
年度 nvarchar(50), [1月] nvarchar(500), [2月] nvarchar(500), [3月] nvarchar(500), [4月] nvarchar(500),
[5月] nvarchar(500), [6月] nvarchar(500), [7月] nvarchar(500), [8月] nvarchar(500), [9月] nvarchar(500),
[10月] nvarchar(500), [11月] nvarchar(500), [12月] nvarchar(500))
AS
BEGIN
	--如果最大时间月是12月份的话，就不用为了merge来处理数据了
	IF YEAR(@MaxRealSalesDate) = 12
		RETURN
		
	--真实sales数据
	--餐厅--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 0) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 0) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 0) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 0) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 0) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 0) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 0) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 0) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 0) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 0) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 0) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 0) AS [12月]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='')
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--餐厅--End
	
	--甜品店--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 0) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 0) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 0) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 0) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 0) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 0) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 0) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 0) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 0) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 0) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 0) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 0) AS [12月]
		FROM RLPlanning_RealSales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--甜品店--End
	
	--预测数据
	--餐厅--Begin
	IF @KioskNo IS NULL
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,NULL AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 1, 1) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 2, 1) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 3, 1) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 4, 1) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 5, 1) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 6, 1) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 7, 1) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 8, 1) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 9, 1) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 10, 1) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 11, 1) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, NULL, YEAR(FS.SalesDate), 12, 1) AS [12月]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo 
		WHERE FS.StoreNo = @StoreNo AND (FS.KioskNo IS NULL OR FS.KioskNo='') 
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--餐厅--End
	
	--甜品店--Begin
	ELSE
	BEGIN
		INSERT INTO @Result
		SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,K.KioskName AS Kiosk,
		YEAR(FS.SalesDate) AS 年度, 
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 1, 1) AS [1月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 2, 1) AS [2月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 3, 1) AS [3月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 4, 1) AS [4月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 5, 1) AS [5月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 6, 1) AS [6月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 7, 1) AS [7月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 8, 1) AS [8月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 9, 1) AS [9月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 10, 1) AS [10月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 11, 1) AS [11月],
		dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, YEAR(FS.SalesDate), 12, 1) AS [12月]
		FROM Forecast_Sales FS INNER JOIN SRLS_TB_Master_Store S ON FS.StoreNo = S.StoreNo
							   INNER JOIN SRLS_TB_Master_Kiosk K ON FS.KioskNo = K.KioskNo
		WHERE FS.StoreNo = @StoreNo AND FS.KioskNo = @KioskNo
		AND YEAR(FS.SalesDate) = YEAR(@MaxRealSalesDate)
	END
	--甜品店--End	
	
	--两行互相update
	DECLARE @month1 nvarchar(500),  @month2 nvarchar(500), @month3 nvarchar(500), @month4 nvarchar(500),
	@month5 nvarchar(500), @month6 nvarchar(500), @month7 nvarchar(500), @month8 nvarchar(500),
	@month9 nvarchar(500), @month10 nvarchar(500), @month11 nvarchar(500), @month12 nvarchar(500)
	
	SELECT @month1=[1月] FROM @Result WHERE [1月] IS NOT NULL
	SELECT @month2=[2月] FROM @Result WHERE [2月] IS NOT NULL
	SELECT @month3=[3月] FROM @Result WHERE [3月] IS NOT NULL
	SELECT @month4=[4月] FROM @Result WHERE [4月] IS NOT NULL
	SELECT @month5=[5月] FROM @Result WHERE [5月] IS NOT NULL
	SELECT @month6=[6月] FROM @Result WHERE [6月] IS NOT NULL
	SELECT @month7=[7月] FROM @Result WHERE [7月] IS NOT NULL
	SELECT @month8=[8月] FROM @Result WHERE [8月] IS NOT NULL
	SELECT @month9=[9月] FROM @Result WHERE [9月] IS NOT NULL
	SELECT @month10=[10月] FROM @Result WHERE [10月] IS NOT NULL
	SELECT @month11=[11月] FROM @Result WHERE [11月] IS NOT NULL
	SELECT @month12=[12月] FROM @Result WHERE [12月] IS NOT NULL
	
	UPDATE @Result SET [1月]=@month1
	UPDATE @Result SET [2月]=@month2
	UPDATE @Result SET [3月]=@month3
	UPDATE @Result SET [4月]=@month4
	UPDATE @Result SET [5月]=@month5
	UPDATE @Result SET [6月]=@month6
	UPDATE @Result SET [7月]=@month7
	UPDATE @Result SET [8月]=@month8
	UPDATE @Result SET [9月]=@month9
	UPDATE @Result SET [10月]=@month10
	UPDATE @Result SET [11月]=@month11
	UPDATE @Result SET [12月]=@month12
	
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetRentStartEndDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetRentStartEndDate]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS @Result TABLE(RentStartDate datetime,RentEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @MinRentStartDate datetime, @MaxRentEndDate datetime
	
	--餐厅
	IF @KioskNo IS NULL OR @KioskNo = ''
	BEGIN
		SELECT @MinRentStartDate = MIN(E.RentStartDate), @MaxRentEndDate = MAX(E.RentEndDate) FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		INNER JOIN SRLS_TB_Master_Store S ON E.StoreOrDeptNo = S.StoreNo
		WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
		AND E.StoreOrDeptNo = @StoreNo AND (E.KioskNo IS NULL OR E.KioskNo = '')
	END
	--甜品店
	ELSE
	BEGIN
		SELECT @MinRentStartDate = MIN(E.RentStartDate), @MaxRentEndDate = MAX(E.RentEndDate) FROM Entity E 
		INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
		INNER JOIN SRLS_TB_Master_Store S ON E.StoreOrDeptNo = S.StoreNo
		WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'
		AND E.StoreOrDeptNo = @StoreNo AND E.KioskNo = @KioskNo
	END
	
	INSERT INTO @Result SELECT @MinRentStartDate, @MaxRentEndDate
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[YearlyCompareSales]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 20100108
-- Description: 年度销售额对比
-- =============================================
CREATE FUNCTION [dbo].[YearlyCompareSales]
(
	@nYear1 int
   ,@nYear2 int
)
RETURNS @ResultTable TABLE (
AreaID nvarchar(50)
,companyCode nvarchar(50)
,OpenYear int
,[Range] nvarchar(50)
,AreaName nvarchar(50)
,CompanyName nvarchar(50)
,StoreOrDeptNo nvarchar(50)
,KioskNo nvarchar(50)
,RentType nvarchar(50)
,CompareYear1 int
,CompareYear2 int


,ContainChange int
,ContainMinYear int
,ContainContinue int

,FixManagement int
,FixRent int
,RadioService int



,RadioManagement int
,RadioRent int
,StraightRent int

)
AS
BEGIN
	
insert into @ResultTable
select 
'AreaID'=MAX(cast(company.AreaID as nvarchar(50)))
,'companyCode'=max(company.CompanyCode)
,'OpenYear'=MAX(year(entity.OpeningDate))
,'Range' = max(gl.EntityTypeName)
,'AreaName'=max(area.AreaName)
,'CompanyName'=max(company.CompanyName)
,'StoreOrDeptNo'=max(entity.StoreOrDeptNo)
,'KioskNo'=max(entity.KioskNo)
,'RentType'=max(gl.RentType)
,'CompareYear1'=SUM(case when year(gl.CycleStartDate)=@nYear1 then gl.GLAmount else 0 end)
,'CompareYear1'=SUM(case when year(gl.CycleStartDate)=@nYear2 then gl.GLAmount else 0 end)


,ContainChange =max((case when contract.PartComment='变更' then 1 else 0 end))
,ContainMinYear =max((case when ( @nYear1>year(entity.RentStartDate) and @nYear2>YEAR(entity.RentStartDate) ) then 1 else 0 end))
,ContainContinue =max((case when contract.PartComment='续租' then 1 else 0 end))

,FixManagement =max((case when gl.RentType='固定管理费' then 1 else 0 end))
,FixRent =max((case when gl.RentType='固定租金' then 1 else 0 end))
,RadioService =max((case when gl.RentType='百分比服务费' then 1 else 0 end))



,RadioManagement =max((case when gl.RentType='百分比管理费' then 1 else 0 end))
,RadioRent =max((case when gl.RentType='百分比租金' then 1 else 0 end))
,StraightRent =max((case when gl.GLType='直线租金' then 1 else 0 end))
 from GLRecord gl,SRLS_TB_Master_Company company,SRLS_TB_Master_Area area,Entity entity,[Contract] [contract]
 where (gl.CompanyCode = company.CompanyCode) 
 and (company.AreaID=area.ID)
 AND (gl.EntityID=entity.EntityID)
 and (year(gl.CycleStartDate)=@nYear1 or  year(gl.CycleStartDate)=@nYear2)
 --计算已生效的餐厅，餐厅后来可能是后来不
 and (gl.EntityTypeName='餐厅' or gl.EntityTypeName='甜饼店')

 --有效的合同
 and (gl.ContractSnapshotID=contract.ContractSnapshotID) 
 --and ([contract].[Status]='已生效' and [contract].SnapshotCreateTime is NULL)
 group by gl.EntityID,gl.RentType
	
	
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetSalesUpdateTime]    Script Date: 08/16/2012 19:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetSalesUpdateTime] 
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @UpdateTime DATETIME
	--餐厅
	IF ISNULL(@KioskNo,'')=''
	BEGIN
		SELECT @UpdateTime=MAX(UpdateTime) FROM StoreSales WHERE StoreNo=@StoreNo
	END
	--甜品店
	ELSE
	BEGIN
		SELECT @UpdateTime=MAX(KC.CreateTime) 
		FROM KioskSalesCollection KC INNER JOIN SRLS_TB_Master_Kiosk K
		ON KC.KioskID=K.KioskID
		WHERE K.KioskNo=@KioskNo AND K.StoreNo=@StoreNo
	END
	
	RETURN @UpdateTime
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetEntityInfosettingWithSameContent]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_GetEntityInfosettingWithSameContent]
(
	@ContractSnapshotID nvarchar(50),
	@LastContractSnapshotID nvarchar(50)
)
RETURNS @Result TABLE(EntityInfoSettingID nvarchar(50), LastEntityInfoSettingID nvarchar(50))
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	
	--将两份合同的里面的EntityInfo重新组织并存入临时表中
	DECLARE @EntityInfo TABLE(EntityInfoSettingID nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50), VendorNo nvarchar(50))
	DECLARE @LastEntityInfo TABLE(EntityInfoSettingID nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50), VendorNo nvarchar(50))
	
	INSERT INTO @EntityInfo
	SELECT ES.EntityInfoSettingID, ISNULL(E.StoreOrDeptNo,''), ISNULL(E.KioskNo,''), ES.VendorNo
	FROM Entity E INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
	WHERE E.ContractSnapshotID=@ContractSnapshotID
	
	INSERT INTO @LastEntityInfo
	SELECT ES.EntityInfoSettingID, ISNULL(E.StoreOrDeptNo,''), ISNULL(E.KioskNo,''), ES.VendorNo 
	FROM Entity E INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
	WHERE E.ContractSnapshotID=@LastContractSnapshotID
	
	--将有相同StoreNo,KioskNo与VendorNo的EntityInfoSettingID返回出来
	INSERT INTO @Result
	SELECT E1.EntityInfoSettingID, E2.EntityInfoSettingID  
	FROM @EntityInfo E1 
	INNER JOIN @LastEntityInfo E2 ON E1.StoreNo = E2.StoreNo
	AND E1.KioskNo = E2.KioskNo AND E1.VendorNo = E2.VendorNo
	
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_Cal_GetSelectStoreOrKiosk]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[RLPlanning_FN_Cal_GetSelectStoreOrKiosk]
(
	@StoreNo nvarchar(50),
	@KioskNo nvarchar(50)
)
RETURNS 
@Result 
TABLE(CompanyCode nvarchar(50), CompanyName nvarchar(50), StoreNo nvarchar(50), KioskNo nvarchar(50),
	EntityType nvarchar(10), EntityName nvarchar(200), 
	RentStartDate datetime, RentEndDate datetime,
	SalesStartDate datetime, SalesEndDate datetime)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @RentStartDate datetime, @RentEndDate datetime
	DECLARE @SalesStartDate datetime, @SalesEndDate datetime
	
	--指定搜索特定餐厅或甜品店时，只查找一家餐厅或者甜品店--Begin
	--餐厅--Begin
	IF @StoreNo IS NOT NULL AND @StoreNo <> '' AND (@KioskNo IS NULL OR @KioskNo = '')
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, NULL)	
		
		--显示单个餐厅的相关数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'餐厅' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
		
		RETURN;
	END
	--餐厅--End
	
	--甜品店--Begin
	IF @StoreNo IS NOT NULL AND @StoreNo <> '' AND @KioskNo IS NOT NULL AND @KioskNo <> ''
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, @KioskNo)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, @KioskNo)	
		
		--显示单个甜品店的相关数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, K.KioskNo,
		'甜品店' AS EntityType, K.KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
		WHERE S.StoreNo = @StoreNo AND K.KioskNo = @KioskNo AND
		S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
		ORDER BY A.CompanyCode
	
		RETURN;
	END
	--甜品店--End
	--指定搜索特定餐厅或甜品店时，只查找一家餐厅或者甜品店--End
	
	--指定公司搜索，返回该公司下所有的餐厅与甜品店信息--Begin
	
	--先将所选公司下所有的餐厅与甜品店给找出来，放入临时表中
	DECLARE @StoreNoTbl TABLE(StoreNo nvarchar(50))
	DECLARE @StoreNoKioskNoTbl TABLE(StoreNo nvarchar(50), KioskNo nvarchar(50))
	
	INSERT INTO @StoreNoTbl
	SELECT StoreNo FROM SRLS_TB_Master_Store 
	WHERE CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND Status='A'

	INSERT INTO @StoreNoKioskNoTbl
	SELECT S.StoreNo, K.KioskNo
	FROM SRLS_TB_Master_Store S 
	INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
	WHERE S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)
	AND K.Status='A'
	
	
	DECLARE @StoreNoTmp nvarchar(50), @KioskNoTmp nvarchar(50)
	--餐厅
	DECLARE @StoreNoCursor CURSOR
	SET @StoreNoCursor = CURSOR SCROLL FOR SELECT StoreNo FROM @StoreNoTbl
	OPEN @StoreNoCursor
	FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, NULL)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, NULL)	
		
		--插入数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, NULL AS KioskNo, 
		'餐厅' AS EntityType, S.StoreName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		WHERE S.StoreNo = @StoreNoTmp
		
		FETCH NEXT FROM @StoreNoCursor INTO @StoreNoTmp
	END
	CLOSE @StoreNoCursor
	DEALLOCATE @StoreNoCursor
	
	--甜品店
	DECLARE @KioskNoCursor CURSOR
	SET @KioskNoCursor = CURSOR SCROLL FOR SELECT StoreNo, KioskNo FROM @StoreNoKioskNoTbl
	OPEN @KioskNoCursor
	FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	WHILE @@FETCH_STATUS=0
	BEGIN
		--获取租约起始时间与Sales起始时间
		SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
		FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNoTmp, @KioskNoTmp)

		SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
		FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNoTmp, @KioskNoTmp)	
		
		--插入数据
		INSERT INTO @Result
		SELECT A.CompanyCode, A.CompanyName, S.StoreNo, K.KioskNo,
		'甜品店' AS EntityType, K.KioskName AS EntityName,
		@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
		@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
		FROM SRLS_TB_Master_Store S 
		INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
		INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
		WHERE S.StoreNo = @StoreNoTmp AND K.KioskNo = @KioskNoTmp
	
		FETCH NEXT FROM @KioskNoCursor INTO @StoreNoTmp, @KioskNoTmp
	END
	CLOSE @KioskNoCursor
	DEALLOCATE @KioskNoCursor
	--指定公司搜索，返回该公司下所有的餐厅与甜品店信息--End
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetVendorNosByEntityID]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================
-- Author:     liujj
-- Create Date:20110106
-- Description:获取实体所属业主编号
-- ========================================
CREATE FUNCTION [dbo].[fn_GetVendorNosByEntityID]
(
	@EntityID NVARCHAR(50)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
DECLARE @VendorNo NVARCHAR(50)
DECLARE @VendorNoString NVARCHAR(1000)
SET @VendorNoString = ''

SELECT @VendorNoString=@VendorNoString+VendorNo+';' FROM VendorEntityWHERE dbo.VendorEntity.EntityID=@EntityIDSET @VendorNoString=SUBSTRING(@VendorNoString,0,LEN(@VendorNoString))

RETURN @VendorNoString
	
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_IsRelatedKiosk]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	是否关联甜品店
-- =============================================
CREATE FUNCTION [dbo].[fn_IsRelatedKiosk] 
(
	-- Add the parameters for the function here
	@StoreNo NVARCHAR(50)
)
RETURNS BIT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT

	-- Add the T-SQL statements to compute the return value here
	IF EXISTS(SELECT * FROM dbo.[v_KioskStoreZoneInfo] WHERE StoreNo=@StoreNo) BEGIN
		SELECT @ResultVar=1;
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM dbo.[SRLS_TB_Master_Kiosk] 
			WHERE [Status]='A' AND StoreNo=@StoreNo) BEGIN
			SELECT @ResultVar=1;
		END ELSE BEGIN
			SELECT @ResultVar=0;
		END
	END

	-- Return the result of the function
	RETURN @ResultVar;
END
GO
/****** Object:  UserDefinedFunction [dbo].[AverageCompare]    Script Date: 08/16/2012 19:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 20100108
-- Description: 年度销售额对比
-- =============================================
CREATE FUNCTION [dbo].[AverageCompare]
(
	@RentYear int
   ,@MinYear int
)
RETURNS @ResultTable TABLE (


 AreaID nvarchar(50)
,AreaName nvarchar(50)
,companyCode nvarchar(50)
,CompanyName  nvarchar(50)
,StoreNo  nvarchar(50)
,KioskNo  nvarchar(50)
,Range  nvarchar(50)
,RentTypeName  nvarchar(50)
,OpenYear  nvarchar(50)
,GLAmount int
,Sales int

,FixManagement int
,FixRent int
,RadioService int

,RadioManagement int
,RadioRent int
,StraightRent int

)
AS
BEGIN
	
insert into @ResultTable


--declare @RentYear int
--select @RentYear=2012

--declare @MinYear int
--select @MinYear= 2009


select 

 AreaID=max(AreaID)
,AreaName =MAX(AreaName)
,companyCode  =max(companyCode)
,CompanyName=max(CompanyName)
,StoreNo =max(StoreNo)
,KioskNo =max(KioskNo)
,Range =max(Range)
,RentTypeName =max(RentTypeName)
,OpenYear =max(OpenYear)
,GLAmount =sum(isnull(GLAmount,0))/12
,Sales =sum(isnull(Sales,0))/12


,FixManagement =max((case when RentTypeName='固定管理费' then 1 else 0 end))
,FixRent =max((case when RentTypeName='固定租金' then 1 else 0 end))
,RadioService =max((case when RentTypeName='百分比服务费' then 1 else 0 end))

,RadioManagement =max((case when RentTypeName='百分比管理费' then 1 else 0 end))
,RadioRent =max((case when RentTypeName='百分比租金' then 1 else 0 end))
,StraightRent =max((case when m.GLType='直线租金' then 1 else 0 end))

 from ( 
	select v_Entity.*
		  ,GL.GLAmount
		  ,sales.Sales
		  ,gl.GLType
	from v_Entity 
	 left join
	 (
		 select
		 EntityID
		,GLAmount
		,GLType
		 from GLRecord
		 where year(CycleStartDate)=@RentYear
		 
	 ) gl
	 on v_Entity.EntityID=GL.EntityID 	 
		--合同的结束日期应该包括 所需计算租金的年份
		--and (year(v_Entity.RentEndDate) >=@RentYear)
		--合同的开业日期 应该小于等于 最早开业年份
		and (v_Entity.OpenYear >=@MinYear)
	 
	 left join
	 (
		  select   
		  StoreNo,KioskNo,	'Sales'=sum(Sales)
		   from 
			  (
			  select * from RLPlanning_RealSales
			  where YEAR(SalesDate)=@RentYear
			  union
			  select * from Forecast_Sales where YEAR(SalesDate)=@RentYear
			  ) m
		   group by StoreNo,KioskNo
	  ) sales 
	  on sales.StoreNo=v_Entity.StoreNo and isnull(sales.KioskNo,'')=isnull(v_Entity.KioskNo,'')
		and (v_Entity.OpenYear >=@MinYear)
	 
	 ) m
	 WHERE (OpenYear >=@MinYear)
 
 group by 
 StoreNo
,KioskNo
,[Range]
,RentTypeName

having NOT (sum(isnull(GLAmount,0))=0 and sum(isnull(Sales,0))=0)
 


















--select 
--'AreaID'=MAX(cast(company.AreaID as nvarchar(50)))
--,'companyCode'=max(company.CompanyCode)
--,'OpenYear'=MAX(year(entity.OpeningDate))
--,'Range' = max(gl.EntityTypeName)
--,'AreaName'=max(area.AreaName)
--,'CompanyName'=max(company.CompanyName)
--,'StoreOrDeptNo'=max(entity.StoreOrDeptNo)
--,'KioskNo'=max(entity.KioskNo)
--,'TypeCodeName'=max(gl.TypeCodeName)
--,'CompareYear1'=SUM(case when year(gl.CycleStartDate)=@nYear1 then gl.GLAmount else 0 end)
--,'CompareYear1'=SUM(case when year(gl.CycleStartDate)=@nYear2 then gl.GLAmount else 0 end)


--,ContainChange =max((case when contract.PartComment='变更' then 1 else 0 end))
--,ContainMinYear =max((case when ( @nYear1>year(entity.RentStartDate) and @nYear2>YEAR(entity.RentStartDate) ) then 1 else 0 end))
--,ContainContinue =max((case when contract.PartComment='续租' then 1 else 0 end))

--,FixManagement =max((case when gl.RentType='固定管理费' then 1 else 0 end))
--,FixRent =max((case when gl.RentType='固定租金' then 1 else 0 end))
--,RadioService =max((case when gl.RentType='百分比服务费' then 1 else 0 end))



--,RadioManagement =max((case when gl.RentType='百分比管理费' then 1 else 0 end))
--,RadioRent =max((case when gl.RentType='百分比租金' then 1 else 0 end))
--,StraightRent =max((case when gl.RentType='直线租金' then 1 else 0 end))
-- from GLRecord gl,SRLS_TB_Master_Company company,SRLS_TB_Master_Area area,Entity entity,[Contract] [contract]
-- where (gl.CompanyCode = company.CompanyCode) 
-- and (company.AreaID=area.ID)
-- AND (gl.EntityID=entity.EntityID)
-- and (year(gl.CycleStartDate)=@nYear1 or  year(gl.CycleStartDate)=@nYear2)
-- --计算已生效的餐厅，餐厅后来可能是后来不
-- and (gl.EntityTypeName='餐厅' or gl.EntityTypeName='甜饼店')

-- --有效的合同
-- and (gl.ContractSnapshotID=contract.ContractSnapshotID) 
-- --and ([contract].[Status]='已生效' and [contract].SnapshotCreateTime is NULL)
-- group by gl.EntityID,gl.GLType
	
	
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_SelectUnFinishKioskNo]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取未完成的KIOSK收集流程
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_SelectUnFinishKioskNo]
(
	-- Add the parameters for the function here
	@RecordID NVARCHAR(50)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(100)

	DECLARE @RentType NVARCHAR(50),@EntityTypeName NVARCHAR(50),@StartDate DATETIME,@EndDate DATETIME,@StoreNo NVARCHAR(50),@KioskNo NVARCHAR(50)
	SELECT @RentType=RentType,
			@EntityTypeName=EntityTypeName,
		   @StartDate=CycleStartDate,
		   @EndDate=CycleEndDate,
		   @StoreNo=StoreOrDeptNo,
		   @KioskNo=KioskNo FROM 
	(
	SELECT ap.EntityTypeName,
			ap.RentType,
			ap.CycleStartDate,
			ap.CycleEndDate,
			e.StoreOrDeptNo,
			e.KioskNo 
	FROM dbo.APRecord ap 
	INNER JOIN dbo.Entity e ON ap.EntityID = e.EntityID
	WHERE ap.APRecordID=@RecordID
	UNION ALL 
	SELECT gl.EntityTypeName,
			gl.RentType,
			gl.CycleStartDate,
			gl.CycleEndDate,
			e.StoreOrDeptNo,
			e.KioskNo  
	FROM dbo.GLRecord gl
	INNER JOIN dbo.Entity e ON gl.EntityID = e.EntityID
	WHERE gl.GLRecordID=@RecordID
	) x WHERE EntityTypeName IS NOT NULL 
	
	IF @RentType NOT LIKE '%百分比%' OR (@EntityTypeName<>'甜品店' AND @EntityTypeName<>'餐厅')
	BEGIN
		RETURN '' 
	END
	
	IF @EntityTypeName='甜品店'
	BEGIN
		SET @ResultVar=''
		SELECT @ResultVar=@ResultVar+k.KioskNo+';' FROM dbo.KioskSalesCollection c 
		INNER JOIN dbo.SRLS_TB_Master_Kiosk k ON c.KioskID=k.KiosKID
		INNER JOIN (SELECT * FROM dbo.WF_Proc_Inst WHERE AppCode=12) p ON c.CollectionID=p.DataLocator
		WHERE (c.ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (c.ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND k.KioskNo=@KioskNo
			AND p.EndTime IS NULL 
	END
	
	
	IF @EntityTypeName='餐厅'
	BEGIN
		SET @ResultVar=''
		--先获取挂靠时间段
		DECLARE @KioskStoreZoneInfoTable TABLE(KioskID NVARCHAR(50),KioskNo NVARCHAR(50),IsNeedSubtractSalse BIT,StoreNo NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
		INSERT INTO @KioskStoreZoneInfoTable
		SELECT * FROM v_KioskStoreZoneInfo 
		WHERE IsNeedSubtractSalse=1 
			AND StoreNo=@StoreNo
			AND dbo.fn_IsDateZoneIntersection(@StartDate,@EndDate,StartDate,EndDate)=1


			SELECT @ResultVar=@ResultVar+z.KioskNo+';'
			FROM dbo.KioskSalesCollection k
			INNER JOIN @KioskStoreZoneInfoTable z ON k.KioskID = z.KioskID
			INNER JOIN (SELECT * FROM dbo.WF_Proc_Inst WHERE AppCode=12) p ON k.CollectionID=p.DataLocator
			AND (ZoneStartDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneEndDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND p.EndTime IS NULL 

	END
	
	
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_ReplaceByDiscount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh	
-- Create date: 20110412
-- Description:	折算条件公式
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_ReplaceByDiscount]
(
	-- Add the parameters for the function here
	@ConditionID NVARCHAR(50),
	@NumberType NVARCHAR(50),
	@Discount DECIMAL(18,8),
	@BeReplaceStr NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(1000)

	--不等于1时才需要折算
	IF @Discount <> 1
	BEGIN
		DECLARE @NumberInfoTable TABLE
		(IndexId int IDENTITY (1, 1),NumberValue NVARCHAR(50),DiscountNumberValue  NVARCHAR(50))
		
		INSERT INTO @NumberInfoTable
		SELECT NumberValue,CONVERT(DECIMAL(18,2),NumberValue*@Discount) 
		FROM dbo.ConditionAmountNumbers 
		WHERE ConditionID=@ConditionID AND NumberType=@NumberType
		
		
		DECLARE @NumberValue NVARCHAR(50),@DiscountNumberValue NVARCHAR(50)
		-- Add the T-SQL statements to compute the return value here
		IF  EXISTS(SELECT * FROM @NumberInfoTable) 
		BEGIN
			DECLARE @MyCursor CURSOR
			SET  @MyCursor = CURSOR  SCROLL FOR
			SELECT NumberValue,DiscountNumberValue FROM @NumberInfoTable

			OPEN @MyCursor;
			FETCH NEXT FROM @MyCursor INTO @NumberValue,@DiscountNumberValue;
				WHILE @@FETCH_STATUS = 0
				   BEGIN
						SELECT @BeReplaceStr=REPLACE(@BeReplaceStr,@NumberValue,@DiscountNumberValue)
						FETCH NEXT FROM @MyCursor INTO @NumberValue,@DiscountNumberValue;
				   END;
			CLOSE @MyCursor;
			DEALLOCATE @MyCursor;
		END
	END
	

	SELECT @ResultVar=@BeReplaceStr
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetKioskSales]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110309
-- Description:	获取KIOSKSALES
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetKioskSales]
(
	-- Add the parameters for the function here
	@KioskOrStoreNo NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	
	DECLARE @SalesType NVARCHAR(50)
	IF EXISTS(SELECT * FROM dbo.SRLS_TB_Master_Kiosk 
	WHERE KioskNo=@KioskOrStoreNo)
	BEGIN
		SELECT @SalesType='甜品店'
	END
	ELSE
	BEGIN
		SELECT @SalesType='餐厅'
	END
	
	IF @SalesType='甜品店'
	BEGIN
		SELECT @ResultVar=0
		
		--Sales调整之和
		SELECT @ResultVar=ISNULL(SUM(Sales),0) 
		FROM dbo.KioskSales s 
		INNER JOIN dbo.SRLS_TB_Master_Kiosk k ON s.KioskID=k.KioskID
		WHERE (dbo.fn_GetDate(s.SalesDate) BETWEEN @StartDate AND @EndDate)
		AND k.KioskNo=@KioskOrStoreNo
		AND s.Sales IS NOT NULL 
		
		--Sales收集之和
		SELECT @ResultVar=@ResultVar+ISNULL(SUM(Sales),0) 
		FROM dbo.KioskSalesCollection c 
		INNER JOIN dbo.SRLS_TB_Master_Kiosk k ON c.KioskID=k.KiosKID
		WHERE (c.ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (c.ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND k.KioskNo=@KioskOrStoreNo
			AND c.Sales IS NOT NULL 
	END
	
	
	IF @SalesType='餐厅'
	BEGIN
		SELECT @ResultVar=0
		
		--先获取挂靠时间段
		DECLARE @KioskStoreZoneInfoTable TABLE(KioskID NVARCHAR(50),KioskNo NVARCHAR(50),IsNeedSubtractSalse BIT,StoreNo NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
		INSERT INTO @KioskStoreZoneInfoTable
		SELECT KioskID, KioskNo, IsNeedSubtractSalse, StoreNo, StartDate, EndDate FROM v_KioskStoreZoneInfo 
		WHERE IsNeedSubtractSalse=1 
			AND StoreNo=@KioskOrStoreNo
			AND dbo.fn_IsDateZoneIntersection(@StartDate,@EndDate,StartDate,EndDate)=1
		
		SELECT @ResultVar=0
		
		--Sales调整之和
		--调整日期必须在本次收集的区间内且在挂靠关系本餐厅的区间内
		SELECT @ResultVar=ISNULL(SUM(Sales),0) 
		FROM 
		(
			SELECT DISTINCT k.KioskSalesID,k.Sales 
			FROM dbo.KioskSales k 
			INNER JOIN @KioskStoreZoneInfoTable z ON k.KioskID = z.KioskID
			AND  (dbo.fn_GetDate(k.SalesDate) BETWEEN z.StartDate AND z.EndDate)
			AND (dbo.fn_GetDate(k.SalesDate) BETWEEN @StartDate AND @EndDate)
			AND k.Sales IS NOT NULL 
		) x
			
		--Sales收集之和
		--收集开始结束日期必须在本次收集的区间内且在挂靠关系本餐厅的区间内
		SELECT @ResultVar=@ResultVar+ISNULL(SUM(Sales),0) 
		FROM 
		(
			SELECT k.CollectionID,k.Sales 
			FROM dbo.KioskSalesCollection k
			INNER JOIN @KioskStoreZoneInfoTable z ON k.KioskID = z.KioskID
			AND (ZoneStartDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneEndDate BETWEEN z.StartDate AND z.EndDate)
			AND (ZoneStartDate BETWEEN @StartDate AND @EndDate)
			AND (ZoneEndDate BETWEEN @StartDate AND @EndDate)
			AND k.Sales IS NOT NULL 
		) x
	END
		
	SELECT @ResultVar = ISNULL(@ResultVar,0)
	RETURN @ResultVar


END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetFixedNextAPCreateDate]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取下一个固定AP创建日期
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetFixedNextAPCreateDate]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	
	DECLARE @NextDueDate DATETIME,@NextAPEndDate DATETIME
	SELECT @NextDueDate=NextDueDate,
		   @NextAPEndDate=NextAPEndDate
	FROM dbo.FixedRuleSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
	
	DECLARE @FixedAPDays INT,@LessCycleDays INT,@FixedAPDaysDate DATETIME,@LessCycleDaysDate DATETIME
	SELECT @FixedAPDays=CAST(dbo.fn_GetSysParamValueByCode(N'FixedAPDays') AS INT)
	SELECT @LessCycleDays=CAST(dbo.fn_GetSysParamValueByCode(N'LessCycleDays') AS INT)
	SELECT @FixedAPDaysDate=dbo.fn_GetPointWorkDate(@NextDueDate,-@FixedAPDays)
	SELECT @LessCycleDaysDate=dbo.fn_GetPointWorkDate(@NextAPEndDate,@LessCycleDays)

	
	IF EXISTS
	(
		SELECT 1 FROM dbo.FixedRuleSetting r 
		INNER JOIN dbo.EntityInfoSetting eis ON r.EntityInfoSettingID=eis.EntityInfoSettingID
		INNER JOIN dbo.Entity e ON eis.EntityID=e.EntityID
		WHERE r.RuleSnapshotID=@RuleSnapshotID 
		AND dbo.fn_GetDate(e.RentEndDate)=@NextAPEndDate
	)
	BEGIN
		--如果是最后一个周期
		SELECT @ResultVar=dbo.fn_GetMinDate(@FixedAPDaysDate,@LessCycleDaysDate)
	END
	ELSE
	BEGIN
		SELECT @ResultVar=@FixedAPDaysDate
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetZXChangedCalTimes]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	直线租金变更后计算次数
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetZXChangedCalTimes]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 
	
	--取出规则相关信息
	DECLARE @RuleID NVARCHAR(50), @RuleSnapshotID NVARCHAR(50),@CycleStartDate DATETIME,@CycleEndDate DATETIME,@ZXConstant DECIMAL(18,2)
	SELECT @RuleID=RuleID,
	@RuleSnapshotID=RuleSnapshotID,
	@CycleStartDate=CycleStartDate,
	@CycleEndDate=CycleEndDate
	FROM dbo.GLRecord 
	WHERE GLRecordID=@GLRecordID
	
	--取出直线常数,如果是手动变更合同, 那么这个直线常数肯定为NULL
	SELECT @ZXConstant=ZXConstant 
	FROM dbo.FixedRuleSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
	
	--保存同一规则不同快照ID的GL条数
	DECLARE @TableCount INT
	DECLARE @GLCountTable TABLE(RuleID NVARCHAR(50),RuleSnapshotID NVARCHAR(50),CountNumber INT)
	INSERT INTO @GLCountTable
	SELECT RuleID,RuleSnapshotID,COUNT(*) CountNumber 
	FROM dbo.GLRecord 
	WHERE RuleID=@RuleID AND CycleEndDate<=@CycleEndDate AND GLType='直线租金'
	GROUP BY RuleID,RuleSnapshotID 

	
	SELECT @TableCount=COUNT(*) FROM @GLCountTable
	
	
	--如果目前此规则只有和条GL记录
	IF @TableCount=1
	BEGIN
		--变更数为0
		SELECT @ResultVar=0
	END
	ELSE	
	BEGIN
		--否则次数等于当前快照ID的GL条数
		SELECT @ResultVar=CountNumber FROM @GLCountTable WHERE RuleSnapshotID=@RuleSnapshotID
		
		--20110927变更合同后第一次计算时，若改了租期跟租金就按场景2，否则按场景1
		--IF @ResultVar = 1 and dbo.fn_Cal_GetGLRuleIsChanged(@GLRecordID)=0
		--BEGIN
		--	SELECT @ResultVar=0
		--END
	END
	
	--导入情况
	IF @ZXConstant <> 0
	BEGIN
		SELECT @ResultVar=CountNumber+1 FROM @GLCountTable WHERE RuleSnapshotID=@RuleSnapshotID
	END
	

	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_SelectDiscountConditionAmount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110412
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_SelectDiscountConditionAmount]
(	
	-- Add the parameters for the function here
	@TimeIntervalID NVARCHAR(50),
	@Discount DECIMAL(18,8)
)
RETURNS  @ResultTable TABLE
	(
		[ConditionID] [dbo].[ObjID],
		[TimeIntervalID] [dbo].[ObjID],
		[ConditionDesc] [nvarchar] (1000) ,
		[AmountFormulaDesc] [nvarchar] (1000) ,
		[SQLCondition] [nvarchar] (1000) ,
		[SQLAmountFormula] [nvarchar] (1000) 
	)
	
	 
AS
BEGIN
	-- Add the SELECT statement with parameter references here
	INSERT INTO @ResultTable
	SELECT ConditionID,
		   TimeIntervalID,
		   dbo.fn_Cal_ReplaceByDiscount(ConditionID,'条件',@Discount,ConditionDesc) AS ConditionDesc,
		   dbo.fn_Cal_ReplaceByDiscount(ConditionID,'公式',@Discount,AmountFormulaDesc) AS AmountFormulaDesc,
		   dbo.fn_Cal_ReplaceByDiscount(ConditionID,'条件',@Discount,SQLCondition) AS SQLCondition,
		   dbo.fn_Cal_ReplaceByDiscount(ConditionID,'公式',@Discount,SQLAmountFormula) AS SQLAmountFormula
	FROM ConditionAmount 
	WHERE TimeIntervalID=@TimeIntervalID
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetZoneSales]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取计算SALES
-- =============================================
CREATE FUNCTION [dbo].[fn_GetZoneSales]
(
	-- Add the parameters for the function here
	@RecordID NVARCHAR(50),
	@SelectType NVARCHAR(50) --选择类型: 最终;餐厅税前;餐厅税后;甜品店税前;甜品店税后
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	
	DECLARE @EntityTypeName NVARCHAR(50),@TaxRate DECIMAL(18,6)
	DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME
	DECLARE @StoreNo NVARCHAR(50),@KioskNo NVARCHAR(50)
	
	SELECT @EntityTypeName=EntityTypeName,
		@CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate,
		@TaxRate=TaxRate,
		@StoreNo=StoreOrDeptNo,
		@KioskNo=KioskNo
	FROM 
	(
	SELECT ap.EntityTypeName,ap.CycleStartDate,ap.CycleEndDate,eis.TaxRate,e.StoreOrDeptNo,e.KioskNo
	FROM dbo.GLRecord ap
	INNER JOIN dbo.EntityInfoSetting eis ON ap.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE ap.GLRecordID = @RecordID
	UNION ALL 
	SELECT ap.EntityTypeName,ap.CycleStartDate,ap.CycleEndDate,eis.TaxRate,e.StoreOrDeptNo,e.KioskNo
	FROM dbo.APRecord ap
	INNER JOIN dbo.EntityInfoSetting eis ON ap.EntityInfoSettingID = eis.EntityInfoSettingID
	INNER JOIN dbo.Entity e ON eis.EntityID = e.EntityID
	WHERE ap.APRecordID = @RecordID
	)  x WHERE EntityTypeName IS NOT NULL 
	
	DECLARE @StoreSales DECIMAL(18,2),@KioskSales DECIMAL(18,2)
	
	IF @EntityTypeName='甜品店'
	BEGIN
		SET @StoreSales=0
		SET @KioskSales=dbo.fn_Cal_GetKioskSales(@KioskNo,@CycleStartDate,@CycleEndDate)
	END
	ELSE IF @EntityTypeName='餐厅'
	BEGIN
		SET @StoreSales=dbo.fn_Cal_GetStoreSales(@StoreNo,@CycleStartDate,@CycleEndDate)
		SET @KioskSales=dbo.fn_Cal_GetKioskSales(@StoreNo,@CycleStartDate,@CycleEndDate)
	END
	ELSE
	BEGIN
		SET @StoreSales=0
		SET @KioskSales=0
	END
	
	
	SELECT @ResultVar=1
	IF @SelectType='最终'
	BEGIN
		SELECT @ResultVar=@StoreSales*(1-@TaxRate)-@KioskSales*(1-@TaxRate)
	END
	
	IF @SelectType='餐厅税前'
	BEGIN
		SELECT @ResultVar=@StoreSales
	END
	
	IF @SelectType='餐厅税后'
	BEGIN
		SELECT @ResultVar=@StoreSales*(1-@TaxRate)
	END
	
	IF @SelectType='甜品店税前'
	BEGIN
		SELECT @ResultVar=@KioskSales
	END
	
	IF @SelectType='甜品店税后'
	BEGIN
		SELECT @ResultVar=@KioskSales*(1-@TaxRate)
	END

	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN @ResultVar 

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SelectContractKeyInfo]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ZHANGBSH
-- Create date: <Create Date,,>
-- Description:	获取相当合同ID,比视图快
-- =============================================
CREATE FUNCTION [dbo].[fn_SelectContractKeyInfo]
(
	-- Add the parameters for the function here
	@ContractSnapshotID ObjID
)
RETURNS 
@ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)

AS
BEGIN
	-- Fill the table variable with the rows for your result set
	INSERT  INTO @ContractKeyInfo
	SELECT     C.ContractSnapshotID, VC.VendorContractID, VE.VendorEntityID, VC.VendorNo AS VendorVendorNo, VE.VendorNo AS EntityVendorNo, E.EntityID, ES.EntityInfoSettingID, 
                      FR.RuleSnapshotID AS FixedRuleSnapshotID, FT.TimeIntervalID AS FixedTimeIntervalID, RR.RatioID, RC.RuleSnapshotID AS RatioRuleSnapshotID, 
                      RT.TimeIntervalID AS RatioTimeIntervalID, CA.ConditionID
	FROM        (SELECT * FROM  dbo.[Contract] WHERE ContractSnapshotID=@ContractSnapshotID)  AS C LEFT OUTER JOIN
                      dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID LEFT OUTER JOIN
                      dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID LEFT OUTER JOIN
                      dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID AND VC.VendorNo = VE.VendorNo LEFT OUTER JOIN
                      dbo.EntityInfoSetting AS ES ON E.EntityID = ES.EntityID AND VE.VendorNo = ES.VendorNo LEFT OUTER JOIN
                      dbo.FixedRuleSetting AS FR ON ES.EntityInfoSettingID = FR.EntityInfoSettingID LEFT OUTER JOIN
                      dbo.FixedTimeIntervalSetting AS FT ON FR.RuleSnapshotID = FT.RuleSnapshotID LEFT OUTER JOIN
                      dbo.RatioRuleSetting AS RR ON ES.EntityInfoSettingID = RR.EntityInfoSettingID LEFT OUTER JOIN
                      dbo.RatioCycleSetting AS RC ON RR.RatioID = RC.RatioID LEFT OUTER JOIN
                      dbo.RatioTimeIntervalSetting AS RT ON RT.RuleSnapshotID = RC.RuleSnapshotID LEFT OUTER JOIN
                      dbo.ConditionAmount AS CA ON RT.TimeIntervalID = CA.TimeIntervalID
                      
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetKioskIDByRuleSnapshotID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<返回指定日期前与指定规则关联的KioskID>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetKioskIDByRuleSnapshotID]
(	
	@RuleSnapshotID NVARCHAR(50),
	@CreateDate DATETIME
)
RETURNS @Result TABLE(RowIndex INT IDENTITY(1,1),KioskID NVARCHAR(50), IsStore BIT) 
AS
BEGIN
	DECLARE @EntityInfoSettingID NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @EntityTypeName NVARCHAR(50)
	DECLARE @KioskOrStoreNo NVARCHAR(50)

	SELECT TOP 1 @EntityInfoSettingID=EntityInfoSettingID FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	IF @EntityInfoSettingID IS NULL
		SELECT TOP 1 @EntityInfoSettingID=EntityInfoSettingID FROM dbo.RatioRuleSetting WHERE RatioID=(SELECT TOP 1 RatioID FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID)
	IF @EntityInfoSettingID IS NULL
		RETURN 
		
	SELECT @EntityID=EntityID FROM dbo.EntityInfoSetting WHERE EntityInfoSettingID=@EntityInfoSettingID
	IF @EntityID IS NULL
		RETURN 
		
	SELECT TOP 1 @EntityTypeName=EntityTypeName FROM dbo.Entity WHERE EntityID=@EntityID
	IF @EntityTypeName IS NULL
		RETURN 
	
	IF @EntityTypeName='餐厅'
	BEGIN
		SELECT TOP 1 @KioskOrStoreNo=StoreOrDeptNo FROM dbo.Entity WHERE EntityID=@EntityID
		IF @KioskOrStoreNo IS NULL
			RETURN
		
		--获取当前日仍然挂靠在当前餐厅的所有KioskID
		INSERT INTO @Result (KioskID,IsStore) 
		SELECT DISTINCT KioskID,1 FROM dbo.v_KioskStoreZoneInfo WHERE StoreNo=@KioskOrStoreNo AND IsNeedSubtractSalse=1 AND DATEDIFF(DAY,StartDate, @CreateDate)>=0
	END
	
	IF @EntityTypeName='甜品店'
	BEGIN
		SELECT TOP 1 @KioskOrStoreNo=KioskNo FROM dbo.Entity WHERE EntityID=@EntityID
		IF @KioskOrStoreNo IS NULL
			RETURN
			
		INSERT INTO @Result (KioskID,IsStore) SELECT KioskID,0 FROM dbo.SRLS_TB_Master_Kiosk WHERE KioskNo=@KioskOrStoreNo
	END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPALLStartEndDateInfo]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:获取固定,所有周期开始结束日期信息
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAPALLStartEndDateInfo]
(
	@RuleSnapshotID NVARCHAR(50), --规则ID
	@SelectType NVARCHAR(50) --是否查看所有, '所有': 查看所有, '实际':将不出AP和小于出AP日期的筛选掉, NULL当所有处理
)
RETURNS @ResultTable TABLE (IndexId int IDENTITY (1, 1) PRIMARY KEY,RuleSnapshotID NVARCHAR(50),DueDate DATETIME,APStartDate DATETIME,APEndDate DATETIME)
AS
BEGIN
	DECLARE @TempResultTable TABLE (RuleSnapshotID NVARCHAR(50),DueDate DATETIME,APStartDate DATETIME,APEndDate DATETIME)
	DECLARE @Cycle NVARCHAR(50),@CycleMonthCount INT,@Calendar NVARCHAR(50)
	DECLARE @MinDate DATETIME,@MaxDate DATETIME,@FirstDueDate DATETIME
	DECLARE @EntityInfoSettingID NVARCHAR(50),@IsCalculateAP BIT,@EntityAPStartDate DATETIME
	DECLARE @IsVirtual BIT 
	
	SELECT @Cycle=Cycle,
		@CycleMonthCount=CycleMonthCount,
		@Calendar=Calendar,
		@FirstDueDate=dbo.fn_GetDate(FirstDueDate),
		@EntityInfoSettingID=EntityInfoSettingID
	FROM 
	(
		SELECT Cycle,CycleMonthCount,Calendar,FirstDueDate,EntityInfoSettingID
		FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT c.Cycle,c.CycleMonthCount,c.Calendar,c.FirstDueDate,r.EntityInfoSettingID
		FROM dbo.RatioCycleSetting c INNER JOIN dbo.RatioRuleSetting r ON c.RatioID=r.RatioID
		WHERE c.RuleSnapshotID=@RuleSnapshotID
	) x WHERE CycleMonthCount IS NOT NULL 
	
	SELECT @IsCalculateAP=IsCalculateAP,@EntityAPStartDate=APStartDate
	FROM dbo.EntityInfoSetting eis 
	INNER JOIN dbo.Entity e ON e.EntityID=eis.EntityID
	WHERE eis.EntityInfoSettingID=@EntityInfoSettingID
	
	--虚拟VENDOR的编号为GUID 36位
	IF EXISTS(SELECT * FROM dbo.EntityInfoSetting WHERE LEN(VendorNo)=36 AND EntityInfoSettingID=@EntityInfoSettingID)
	BEGIN
		SELECT @IsVirtual=1
	END
	ELSE
	BEGIN
		SELECT @IsVirtual=0
	END
	
	--不需要计算AP或者是虚拟业主就不出AP
	IF (@IsCalculateAP=0 OR @IsVirtual=1) AND @SelectType='实际'
	BEGIN
		RETURN
	END
	
	SELECT @MinDate=dbo.fn_GetDate(MinDate),@MaxDate=dbo.fn_GetDate(MaxDate) 
	FROM
	(
		SELECT MIN(StartDate) AS MinDate,MAX(EndDate) AS MaxDate FROM dbo.FixedTimeIntervalSetting  WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT MIN(StartDate) AS MinDate,MAX(EndDate) AS MaxDate  FROM dbo.RatioTimeIntervalSetting  WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE MinDate IS NOT NULL 

	
	DECLARE @TempMinDate DATETIME,@TempGongliStartDate DATETIME
	SELECT @TempMinDate=@MinDate
	IF @Calendar='公历'
	BEGIN
		SELECT @TempGongliStartDate='19000101'
		WHILE @TempGongliStartDate<@MaxDate
		BEGIN
			IF dbo.fn_IsDateZoneIntersection(@TempGongliStartDate,DATEADD(MONTH,@CycleMonthCount,@TempGongliStartDate)-1,@MinDate,@MaxDate)=1
			BEGIN
				INSERT INTO @TempResultTable
				SELECT @RuleSnapshotID,NULL,dbo.fn_GetMaxDate(@TempGongliStartDate,@MinDate),dbo.fn_GetMinDate(DATEADD(MONTH,@CycleMonthCount,@TempGongliStartDate)-1,@MaxDate)
			END
			SELECT @TempGongliStartDate=DATEADD(MONTH,@CycleMonthCount,@TempGongliStartDate)
		END
	END


	
	IF @Calendar='租赁'
	BEGIN
		DECLARE @CycleCount INT 
		SELECT @CycleCount=0
		SELECT @TempMinDate=@MinDate
		WHILE @TempMinDate <= @MaxDate
		BEGIN
			SELECT @CycleCount=@CycleCount+1
			INSERT INTO @TempResultTable
			SELECT @RuleSnapshotID,NULL,@TempMinDate,dbo.fn_GetMinDate(DATEADD(MONTH,@CycleCount*@CycleMonthCount,@MinDate)-1,@MaxDate)
			SELECT @TempMinDate=DATEADD(MONTH,@CycleCount*@CycleMonthCount,@MinDate)
		END
	END
	
	INSERT INTO @ResultTable 
	SELECT * 
	FROM @TempResultTable 
	ORDER BY APStartDate ASC 
	
	UPDATE @ResultTable 
	SET DueDate = DATEADD(MONTH,(IndexId-1)*@CycleMonthCount,@FirstDueDate)
	
	IF @SelectType='实际'
	BEGIN
		DELETE FROM  @ResultTable WHERE dbo.fn_GetDate(DueDate) < dbo.fn_GetDate(@EntityAPStartDate)
	END
	
	
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPAccountNumber]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	根据APRecordID返回AP科目编号
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetAPAccountNumber]
(
	@APRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeName NVARCHAR(50)
	DECLARE @RuleID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	DECLARE @APAmount DECIMAL(18,2)
	DECLARE @APCycleStartDate DATETIME,@APCycleEndDate DATETIME
	DECLARE @GLAmount DECIMAL(18,2)
	
	SELECT TOP 1 
		@RentType=RentType,
		@RuleSnapshotID=RuleSnapshotID,
		@TypeCodeSnapshotID=TypeCodeSnapshotID,
		@TypeCodeName=TypeCodeName,
		@RuleID=RuleID,
		@APAmount=APAmount,
		@APCycleStartDate=CycleStartDate,
		@APCycleEndDate=CycleEndDate
	FROM dbo.APRecord WHERE APRecordID=@APRecordID
	
	--获取该AP时间段下的GL金额之和，若不等则取差异科目
	SELECT @GLAmount=SUM(GLT.GLAmount)
		FROM dbo.GLTimeIntervalInfo GLT INNER JOIN dbo.GLRecord GL ON GLT.GLRecordID = GL.GLRecordID
	WHERE GL.RuleID=@RuleID
		AND GL.GLType <> '直线租金'
		AND GLT.CycleStartDate BETWEEN @APCycleStartDate AND @APCycleEndDate
		AND GLT.CycleEndDate BETWEEN  @APCycleStartDate AND @APCycleEndDate
	
	IF CHARINDEX('百分比', @RentType) > 0
		SELECT @PayType=PayType 
		FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
	ELSE 
		SELECT @PayType=PayType 
		FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
	
	IF @PayType='预付' OR  @PayType='实付'
	BEGIN
		SELECT @ResultVar=YFAPNormal FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
	END
	
	IF @PayType='延付'
	BEGIN
		--注：预提科目暂时都取正常
		--SELECT @ResultVar=(CASE WHEN @APAmount<>@GLAmount THEN YTAPDifferences ELSE YTAPNormal END) FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		SELECT @ResultVar=YTAPNormal FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
	END
	
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAccountRemark]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	根据APGLRecordID返回对应科目的摘要信息
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAccountRemark]
(
	-- Add the parameters for the function here
	@APGLRecordID NVARCHAR(50),
	@Type NVARCHAR(10)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeName NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @IsCalculateAP BIT --是否出AP
	DECLARE @GLType NVARCHAR(50)
	DECLARE @RuleID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	DECLARE @GLAmount DECIMAL(18,2)
	
	IF @Type='AP'
	BEGIN
		SELECT TOP 1 
			@RentType=RentType,
			@RuleSnapshotID=RuleSnapshotID,
			@TypeCodeSnapshotID=TypeCodeSnapshotID,
			@TypeCodeName=TypeCodeName,
			@RuleID=RuleID
		FROM dbo.APRecord WHERE APRecordID=@APGLRecordID
		
		IF CHARINDEX('百分比', @RentType) > 0
			SELECT @PayType=PayType 
			FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		ELSE 
			SELECT @PayType=PayType 
			FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		
		IF @PayType='预付' OR  @PayType='实付'
		BEGIN
			SELECT @ResultVar=YFRemak FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		END
		
		IF @PayType='延付'
		BEGIN
			SELECT @ResultVar=YTRemark FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		END
	END
	ELSE IF @Type='GL'
	BEGIN
		SELECT TOP 1 
			@RentType=RentType,
			@RuleSnapshotID=RuleSnapshotID,
			@TypeCodeName=TypeCodeName,
			@RuleID=RuleID,
			@GLType=GLType
		FROM dbo.GLRecord WHERE GLRecordID=@APGLRecordID
		IF CHARINDEX('直线', @GLType) > 0
			SELECT @ResultVar=ZXRemark FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		ELSE 
		BEGIN
			IF CHARINDEX('百分比', @RentType) > 0
				SELECT @PayType=PayType 
				FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
			ELSE 
				SELECT @PayType=PayType 
				FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
			
			IF @PayType='预付' OR @PayType='实付'
			BEGIN
				IF @IsCalculateAP=1
					SELECT @ResultVar=YFRemak FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
				ELSE 
					SELECT @ResultVar=BFRemak FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
			END
			
			IF @PayType='延付'
			BEGIN
				SELECT @ResultVar=YTRemark FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
			END
		END
	END
	RETURN @ResultVar
END
--SELECT  dbo.fn_Cal_AccountRemark('F4CDADEC-7F9D-4673-8C53-0C9764CC3EC3','AP')
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ExistsRelationKiosk]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<获取指定的规则是否存在关联的Kiosk>
-- =============================================
CREATE FUNCTION [dbo].[fn_ExistsRelationKiosk]
(	
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	DECLARE @EntityInfoSettingID NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @EntityTypeName NVARCHAR(50)

	SELECT TOP 1 @EntityInfoSettingID=EntityInfoSettingID FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	IF @EntityInfoSettingID IS NULL
		SELECT TOP 1 @EntityInfoSettingID=EntityInfoSettingID FROM dbo.RatioRuleSetting WHERE RatioID=(SELECT TOP 1 RatioID FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID)
	IF @EntityInfoSettingID IS NULL
		RETURN 0
		
	SELECT @EntityID=EntityID FROM dbo.EntityInfoSetting WHERE EntityInfoSettingID=@EntityInfoSettingID
	IF @EntityID IS NULL
		RETURN 0
		
	SELECT TOP 1 @EntityTypeName=EntityTypeName FROM dbo.Entity WHERE EntityID=@EntityID
	IF @EntityTypeName='餐厅' OR @EntityTypeName='甜品店'
		RETURN 1
		
	RETURN 0
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetCalendarMonthCount]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20101215
-- Description:	获取两个日期的月份数
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetCalendarMonthCount]
(
	@RuleSnapshotID NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS DECIMAL(18,8)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,8)
	
	SELECT @StartDate=dbo.fn_GetDate(@StartDate),@EndDate=dbo.fn_GetDate(@EndDate)
	
	DECLARE @Calendar NVARCHAR(50),@CycleMonthCount INT 
	SELECT @Calendar=Calendar,@CycleMonthCount=CycleMonthCount FROM
	(
	SELECT Calendar,CycleMonthCount FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	UNION ALL 
	SELECT Calendar,CycleMonthCount FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE CycleMonthCount IS NOT NULL
	
	
	--公历: 前后折加中间自然月
	IF @Calendar='公历'
	BEGIN
		DECLARE @MonthCount INT,@StartDayCout INT ,@EndDayCout INT,@StartMonthDayCout DECIMAL(18,8) ,@EndMonthDayCout DECIMAL(18,8) 
		--两日期相差月份
		SELECT @MonthCount=DATEDIFF(MONTH,@StartDate,@EndDate)-1
		--将前后的折算成月
		SELECT @StartDayCout=DATEDIFF(DAY,@StartDate,dbo.fn_GetMonthLastDate(@StartDate))+1
		SELECT @EndDayCout=DAY(@EndDate)
		SELECT @StartMonthDayCout=DAY(dbo.fn_GetMonthLastDate(@StartDate))
		SELECT @EndMonthDayCout=DAY(dbo.fn_GetMonthLastDate(@EndDate))
		
		SELECT @ResultVar=@StartDayCout/@StartMonthDayCout+@MonthCount+@EndDayCout/@EndMonthDayCout
		-- Return the result of the function
	END
	
	--租赁,够一个租赁月算一个,最后不够的用剩余天数/30折成月
	IF @Calendar='租赁'
	BEGIN
		SELECT @ResultVar=0
		DECLARE @TempDate DATETIME
		SELECT @TempDate=@StartDate
		WHILE DATEADD(MONTH,1,@TempDate)-1<@EndDate
		BEGIN
			SELECT @ResultVar=@ResultVar+1
			SELECT @TempDate=DATEADD(MONTH,@ResultVar,@StartDate)
		END
		
		IF DATEADD(MONTH,1,@TempDate)-1 = @EndDate --等于
		BEGIN
			SELECT @ResultVar=@ResultVar+1
		END
		ELSE --大于
		BEGIN
			DECLARE @CurrentMonthDayCount DECIMAL(18,2)
			SELECT @CurrentMonthDayCount=dbo.fn_GetDateMonthDayCount(@EndDate)
			SELECT @ResultVar=@ResultVar+(DATEDIFF(DAY,@TempDate,@EndDate)+1)/@CurrentMonthDayCount
		END
	END
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckRatioTimeInterval]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	计算四则表达式
-- =============================================
CREATE FUNCTION [dbo].[fn_CheckRatioTimeInterval]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT
	SELECT @ResultVar=1
	
	DECLARE @RentStartDate DATETIME
	DECLARE @RentEndDate DATETIME
	SELECT @RentStartDate=e.RentStartDate,@RentEndDate=e.RentEndDate 
	FROM 
		(SELECT * FROM RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID) frs
		INNER JOIN RatioRuleSetting rrs ON rrs.RatioID = frs.RatioID
		INNER JOIN EntityInfoSetting eis ON eis.EntityInfoSettingID = rrs.EntityInfoSettingID
		INNER JOIN Entity e ON e.EntityID=eis.EntityID
	
	--时间段的最小日期必须等于实体的起租日,最大日期必须等于实体到租日
	IF NOT EXISTS(SELECT * FROM 
			(
				SELECT MIN(StartDate) StartDate,MAX(EndDate) EndDate
				FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
			) X WHERE DATEDIFF(DAY,X.StartDate,@RentStartDate)=0 AND DATEDIFF(DAY,X.EndDate,@RentEndDate)=0 
	)
	BEGIN
			SELECT @ResultVar=0
			RETURN @ResultVar
	END
	
	--时间段必须闭合且不重叠, 这样JOIN出来的条数为实际条数-1
	DECLARE @NumCount INT 
	DECLARE @JoinCount INT
	SELECT @NumCount=COUNT(*) FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	
	SELECT @JoinCount=COUNT(*) FROM RatioTimeIntervalSetting s
	INNER JOIN RatioTimeIntervalSetting e ON DATEDIFF(DAY,s.EndDate,e.StartDate)=1
	WHERE s.RuleSnapshotID=@RuleSnapshotID AND e.RuleSnapshotID=@RuleSnapshotID
	
	IF @NumCount<>@JoinCount+1
	BEGIN
		SELECT @ResultVar=0
		RETURN @ResultVar
	END
	
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckFixedTimeInterval]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	计算四则表达式
-- =============================================
CREATE FUNCTION [dbo].[fn_CheckFixedTimeInterval]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT
	SELECT @ResultVar=1
	
	DECLARE @RentStartDate DATETIME
	DECLARE @RentEndDate DATETIME
	SELECT @RentStartDate=e.RentStartDate,@RentEndDate=e.RentEndDate 
	FROM 
		(SELECT * FROM FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) frs
		INNER JOIN EntityInfoSetting eis ON eis.EntityInfoSettingID = frs.EntityInfoSettingID
		INNER JOIN Entity e ON e.EntityID=eis.EntityID
	
	--时间段的最小日期必须等于实体的起租日,最大日期必须等于实体到租日
	IF NOT EXISTS(SELECT * FROM 
			(
				SELECT MIN(StartDate) StartDate,MAX(EndDate) EndDate
				FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
			) X WHERE DATEDIFF(DAY,X.StartDate,@RentStartDate)=0 AND DATEDIFF(DAY,X.EndDate,@RentEndDate)=0 
	)
	BEGIN
			SELECT @ResultVar=0
			RETURN @ResultVar
	END
	
	--时间段必须闭合且不重叠, 这样JOIN出来的条数为实际条数-1
	DECLARE @NumCount INT 
	DECLARE @JoinCount INT
	SELECT @NumCount=COUNT(*) FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	
	SELECT @JoinCount=COUNT(*) FROM FixedTimeIntervalSetting s
	INNER JOIN FixedTimeIntervalSetting e ON DATEDIFF(DAY,s.EndDate,e.StartDate)=1
	WHERE s.RuleSnapshotID=@RuleSnapshotID AND e.RuleSnapshotID=@RuleSnapshotID
	
	IF @NumCount<>@JoinCount+1
	BEGIN
		SELECT @ResultVar=0
		RETURN @ResultVar
	END
	
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLRuleIsChanged]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	直线租金是否改了租期跟租金
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetGLRuleIsChanged]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar BIT 
	
	--取出最新规则
	DECLARE @RuleID NVARCHAR(50), @RuleSnapshotID NVARCHAR(50)
	SELECT @RuleID=RuleID,
	@RuleSnapshotID=RuleSnapshotID
	FROM dbo.GLRecord 
	WHERE GLRecordID=@GLRecordID
	
	--取出前一次的规则
	DECLARE @PrevRuleSnapshotID NVARCHAR(50)
	SELECT TOP 1 @PrevRuleSnapshotID=RuleSnapshotID
	FROM dbo.FixedRuleSetting  
	WHERE RuleID=@RuleID and SnapshotCreateTime is not null
	order by SnapshotCreateTime desc
	
	--比较规则
	declare @Count int
	declare @AllCount int
	select @Count=COunt(*) from FixedTimeIntervalSetting where RuleSnapshotID=@RuleSnapshotID
	select @AllCount=count(*) from
	(
		select StartDate,EndDate,Amount,Cycle,Calendar from FixedTimeIntervalSetting where RuleSnapshotID=@RuleSnapshotID
		union 
		select StartDate,EndDate,Amount,Cycle,Calendar from FixedTimeIntervalSetting where RuleSnapshotID=@PrevRuleSnapshotID
	) x
	
	if @Count=@AllCount
	begin	
		--无变更
		set @ResultVar=0
	end
	else
	begin
		--有变更
		set @ResultVar=1
	end
	
	RETURN @ResultVar
END

--select dbo.fn_Cal_GetGLRuleIsChanged('8F5E45CF-772E-4DEC-BA3B-9B4FDD46324A')
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetRuleDateZoneString]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取某一APGL所属规则设置时间段字符串
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetRuleDateZoneString]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME
)
RETURNS NVARCHAR(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(100)

	SELECT @ResultVar=''
	SELECT @ResultVar=@ResultVar+dbo.fn_GetDateZoneString(StartDate,EndDate)+';'
	FROM  
	(
		SELECT DISTINCT dbo.fn_GetDate(StartDate) AS  StartDate,dbo.fn_GetDate(EndDate) AS  EndDate 
		FROM
		(
			SELECT StartDate,EndDate 
			FROM dbo.FixedTimeIntervalSetting 
			WHERE RuleSnapshotID=@RuleSnapshotID
			UNION ALL 
			SELECT StartDate,EndDate 
			FROM dbo.RatioTimeIntervalSetting
			WHERE RuleSnapshotID=@RuleSnapshotID
		) t WHERE StartDate IS NOT NULL 
	) x
	WHERE (@StartDate BETWEEN StartDate AND EndDate)
			OR (@EndDate BETWEEN StartDate AND EndDate)
			OR (StartDate BETWEEN @StartDate AND @EndDate)
			OR (EndDate BETWEEN @StartDate AND @EndDate)
	ORDER BY StartDate ASC 
	
	
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetNextGLCreateDate]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取下个GL创建日期
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetNextGLCreateDate]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	DECLARE @RuleID NVARCHAR(50),@RentTypeName NVARCHAR(50)
	DECLARE @WhichMonth NVARCHAR(50),@GLStartDate INT,@MaxGLEndDate DATETIME
	DECLARE @CreateMonth DATETIME,@NextGLStartDate DATETIME
	
	SELECT @RuleID=RuleID,@RentTypeName=RentType,@NextGLStartDate=NextGLStartDate
	FROM 
	(
		SELECT RuleID,RentType,NextGLStartDate
		FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT c.RuleID,r.RentType,c.NextGLStartDate
		FROM dbo.RatioCycleSetting c 
		INNER JOIN dbo.RatioRuleSetting r ON c.RatioID = r.RatioID
		WHERE c.RuleSnapshotID=@RuleSnapshotID
	) x 
	


		SELECT @WhichMonth=WhichMonth,@GLStartDate=GLStartDate 
		FROM dbo.RentType WHERE RentTypeName=@RentTypeName

		
		IF @WhichMonth = '本月'
		BEGIN
			SELECT @CreateMonth =  @NextGLStartDate
		END
		ELSE
		BEGIN --上月
			SELECT @CreateMonth =  DATEADD(MONTH,1,@NextGLStartDate)
		END
		
		SELECT @ResultVar = dbo.fn_Helper_GetPointDate(@CreateMonth,@GLStartDate)
		
		RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLAccountNumber]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	根据GLRecordID返回GL科目信息
-- =============================================
CREATE   FUNCTION [dbo].[fn_Cal_GetGLAccountNumber]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeName NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @IsCalculateAP BIT --是否出AP
	DECLARE @GLType NVARCHAR(50)
	DECLARE @RuleID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	
	SELECT TOP 1 
		@RentType=RentType,
		@RuleSnapshotID=RuleSnapshotID,
		@TypeCodeSnapshotID=TypeCodeSnapshotID,
		@TypeCodeName=@TypeCodeName,
		@RuleID=RuleID,
		@EntityID=EntityID,
		@GLType=GLType
	FROM dbo.GLRecord WHERE GLRecordID=@GLRecordID
	
	SELECT TOP 1 @IsCalculateAP=IsCalculateAP
	FROM dbo.Entity WHERE EntityID=@EntityID
	
	IF CHARINDEX('直线租金', @GLType) > 0
		SELECT @ResultVar=ZXGLCredit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL 
	ELSE 
	BEGIN
		IF CHARINDEX('百分比', @RentType) > 0
			SELECT @PayType=PayType 
			FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		ELSE 
			SELECT @PayType=PayType 
			FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		
		IF @PayType='延付'
		BEGIN
			SELECT @ResultVar=YTGLCredit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL 
		END
		
		IF @PayType='预付' OR @PayType='实付'
		BEGIN
			IF @IsCalculateAP=1
				SELECT @ResultVar=YFGLCredit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL 
			ELSE 
				SELECT @ResultVar=BFGLCredit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL 
		END
	END
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetDatePointInfoEstimateAP]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	获取APGL计算过程中的时间点
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetDatePointInfoEstimateAP]
(
	-- Add the parameters for the function here
	@GLRecordIDOrRuleID NVARCHAR(50),
	@APStartDate DATETIME,
	@APEndDate DATETIME
)
RETURNS @DatePointTable TABLE(IndexId int IDENTITY (1, 1) PRIMARY KEY,TimeIntervalID NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	--思路AP只被时间段截断, GL被AP和时间段截断
	
	
	DECLARE @TempTable TABLE(DataType NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
	DECLARE @StartTable TABLE(IndexId int IDENTITY (1, 1),StartDate DATETIME)
	DECLARE @EndTable TABLE(IndexId int IDENTITY (1, 1),EndDate DATETIME)
	
	
	DECLARE @RuleID NVARCHAR(50),@RuleSnapshotID NVARCHAR(50)
	
	SELECT @RuleID=RuleID,
			@RuleSnapshotID=RuleSnapshotID
	FROM 
	(
		SELECT RuleID,RuleSnapshotID 
		FROM dbo.RatioCycleSetting 
		WHERE RuleSnapshotID=@GLRecordIDOrRuleID
		UNION ALL 
		SELECT RuleID,RuleSnapshotID 
		FROM dbo.GLRecord 
		WHERE GLRecordID=@GLRecordIDOrRuleID
	) x WHERE RuleSnapshotID IS NOT NULL 

	
	INSERT INTO @TempTable
	SELECT '时间段',dbo.fn_GetDate(StartDate),dbo.fn_GetDate(EndDate)
	FROM dbo.RatioTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
	ORDER BY StartDate ASC 

	INSERT INTO @TempTable
	SELECT 'AP',@APStartDate,@APEndDate
	
	DELETE FROM  @TempTable 
	WHERE (StartDate NOT BETWEEN @APStartDate AND @APEndDate)
		AND (EndDate NOT BETWEEN @APStartDate AND @APEndDate)
		
	UPDATE @TempTable SET StartDate=@APStartDate WHERE StartDate < @APStartDate
	UPDATE @TempTable SET EndDate=@APEndDate WHERE EndDate > @APEndDate
	
	INSERT INTO @StartTable
	SELECT DISTINCT StartDate FROM @TempTable ORDER BY StartDate ASC 
	
	INSERT INTO @EndTable
	SELECT DISTINCT EndDate FROM @TempTable ORDER BY EndDate ASC 

	DELETE FROM  @TempTable
	INSERT INTO @TempTable 
	SELECT NULL,s.StartDate,e.EndDate 
	FROM @StartTable s INNER JOIN @EndTable e ON s.IndexId = e.IndexId
	
	INSERT INTO @DatePointTable
	SELECT t.TimeIntervalID,tt.StartDate,tt.EndDate 
	FROM @TempTable tt 
	INNER JOIN 
	(
		SELECT  TimeIntervalID,dbo.fn_GetDate(StartDate) StartDate,dbo.fn_GetDate(EndDate) EndDate 
		FROM dbo.RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) t ON tt.StartDate BETWEEN t.StartDate AND t.EndDate
	ORDER BY tt.StartDate ASC 
	
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetCycleUnFinishGLCount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取AP期间内GL条数
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetCycleUnFinishGLCount]
(
	-- Add the parameters for the function here
	@APRecordID NVARCHAR(50)
)
RETURNS INT 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar INT 

	-- Add the T-SQL statements to compute the return value here
	DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME,@RuleID NVARCHAR(50)
	SELECT @CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate,
		@RuleID=ap.RuleID
	FROM dbo.APRecord ap
	INNER JOIN dbo.RatioCycleSetting c ON ap.RuleSnapshotID = c.RuleSnapshotID
	WHERE APRecordID=@APRecordID AND c.IsPure=1
	
	SELECT @ResultVar=COUNT(*)
		FROM dbo.GLRecord gl
		INNER JOIN dbo.WF_Proc_Inst p ON gl.GLRecordID=p.DataLocator AND p.AppCode=13
		WHERE RuleID=@RuleID 
				AND (CycleStartDate BETWEEN @CycleStartDate AND @CycleEndDate)
				AND (CycleEndDate BETWEEN @CycleStartDate AND @CycleEndDate)
				AND (p.CurrentActi <> '生成凭证' AND p.CurrentActi <> '结束' AND p.CurrentActi <> '强制结束')
	
	
	SELECT @ResultVar=ISNULL(@ResultVar,0)
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLDebitAccountNumber]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	根据GLRecordID返回GL科目信息
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetGLDebitAccountNumber]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeName NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @IsCalculateAP BIT --是否出AP
	DECLARE @GLType NVARCHAR(50)
	DECLARE @RuleID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	
	SELECT TOP 1 
		@RentType=RentType,
		@RuleSnapshotID=RuleSnapshotID,
		@TypeCodeSnapshotID=TypeCodeSnapshotID,
		@TypeCodeName=TypeCodeName,
		@RuleID=RuleID,
		@EntityID=EntityID,
		@GLType=GLType
	FROM dbo.GLRecord WHERE GLRecordID=@GLRecordID
	
	SELECT TOP 1 
		@IsCalculateAP=IsCalculateAP
	FROM dbo.Entity WHERE EntityID=@EntityID
	
	IF CHARINDEX('直线', @GLType) > 0
		SELECT @ResultVar=ZXGLDebit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
	ELSE 
	BEGIN
		IF CHARINDEX('百分比', @RentType) > 0
			SELECT @PayType=PayType 
			FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		ELSE 
			SELECT @PayType=PayType 
			FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		
		IF @PayType='预付' OR @PayType='实付'
		BEGIN
			IF @IsCalculateAP=1
				SELECT @ResultVar=YFGLDebit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
			ELSE 
				SELECT @ResultVar=BFGLDebit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		END
		
		IF @PayType='延付'
		BEGIN
			SELECT @ResultVar=YTGLDebit FROM dbo.TypeCode WHERE TypeCodeName=@TypeCodeName AND SnapshotCreateTime IS NULL
		END
	END
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLCreditAccountNumber]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	根据GLRecordID返回GL科目信息
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetGLCreditAccountNumber]
(
	-- Add the parameters for the function here
	@GLRecordID NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(50)
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @TypeCodeSnapshotID NVARCHAR(50)
	DECLARE @EntityID NVARCHAR(50)
	DECLARE @IsCalculateAP BIT --是否出AP
	DECLARE @GLType NVARCHAR(50)
	DECLARE @RuleID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50)
	DECLARE @PayType NVARCHAR(50)
	
	SELECT TOP 1 
		@RentType=RentType,
		@RuleSnapshotID=RuleSnapshotID,
		@TypeCodeSnapshotID=TypeCodeSnapshotID,
		@RuleID=RuleID,
		@EntityID=EntityID,
		@GLType=GLType
	FROM dbo.GLRecord WHERE GLRecordID=@GLRecordID
	
	SELECT TOP 1 
		@IsCalculateAP=IsCalculateAP
	FROM dbo.Entity WHERE EntityID=@EntityID
	
	IF CHARINDEX('直线', @GLType) > 0
		SELECT @ResultVar=ZXGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
	ELSE 
	BEGIN
		IF CHARINDEX('百分比', @RentType) > 0
			SELECT @PayType=PayType 
			FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		ELSE 
			SELECT @PayType=PayType 
			FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND RuleID=@RuleID
		
		IF @PayType='预付' OR @PayType='实付'
		BEGIN
			IF @IsCalculateAP=1
				SELECT @ResultVar=YFGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
			ELSE 
				SELECT @ResultVar=BFGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
		END
		
		IF @PayType='延付'
		BEGIN
			SELECT @ResultVar=YTGLCredit FROM dbo.TypeCode WHERE TypeCodeSnapshotID=@TypeCodeSnapshotID
		END
	END
	RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleSnapshotIDByStoreNo]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<返回指定StoreNo关联的RuleSnapshotID>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetRuleSnapshotIDByStoreNo]
(	
	@StoreNo NVARCHAR(50)
)
RETURNS @Result TABLE(RuleSnapshotID NVARCHAR(50)) 
AS
BEGIN
	DECLARE @ResultTmp TABLE(RuleSnapshotID NVARCHAR(50)) 
	
	IF @StoreNo IS NOT NULL AND LTRIM(RTRIM(@StoreNo))<>''
	BEGIN
		INSERT INTO @ResultTmp(RuleSnapshotID)
		SELECT cs.RuleSnapshotID 
			FROM dbo.Entity en 
			INNER JOIN dbo.EntityInfoSetting es ON en.EntityID = es.EntityID
			INNER JOIN dbo.RatioRuleSetting rs ON es.EntityInfoSettingID = rs.EntityInfoSettingID
			INNER JOIN dbo.RatioCycleSetting cs ON rs.RatioID = cs.RatioID
			INNER JOIN dbo.[Contract] co ON en.ContractSnapshotID = co.ContractSnapshotID
		WHERE 
			en.EntityTypeName = '餐厅' AND en.StoreOrDeptNo=@StoreNo AND co.Status='已生效' AND cs.SnapshotCreateTime IS NULL
	END
	INSERT INTO @Result (RuleSnapshotID) SELECT DISTINCT RuleSnapshotID FROM @ResultTmp
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleSnapshotIDByKioskID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<返回指定KioskID关联的RuleSnapshotID>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetRuleSnapshotIDByKioskID]
(	
	@KioskID NVARCHAR(50)
)
RETURNS @Result TABLE(RuleSnapshotID NVARCHAR(50)) 
AS
BEGIN
	DECLARE @ResultTmp TABLE(RuleSnapshotID NVARCHAR(50)) 
	DECLARE @KioskNo NVARCHAR(50)
	
	SET @KioskNo= (SELECT TOP 1 KioskNo	FROM dbo.SRLS_TB_Master_Kiosk WHERE KioskID=@KioskID)
	IF @KioskNo IS NOT NULL AND LTRIM(RTRIM(@KioskNo))<>''
	BEGIN
		INSERT INTO @ResultTmp(RuleSnapshotID)
		SELECT cs.RuleSnapshotID FROM dbo.Entity en 
			INNER JOIN dbo.EntityInfoSetting es ON en.EntityID = es.EntityID
			INNER JOIN dbo.RatioRuleSetting rs ON es.EntityInfoSettingID = rs.EntityInfoSettingID
			INNER JOIN dbo.RatioCycleSetting cs ON rs.RatioID = cs.RatioID
			INNER JOIN dbo.[Contract] co ON en.ContractSnapshotID = co.ContractSnapshotID
		WHERE 
			en.EntityTypeName = '甜品店' AND en.KioskNo=@KioskNo AND co.Status='已生效' AND cs.SnapshotCreateTime IS NULL 
	END
	INSERT INTO @Result (RuleSnapshotID) SELECT DISTINCT RuleSnapshotID FROM @ResultTmp
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleLastAPCycleDateZone]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <20110321>
-- Description:	获取规则最后个AP周期开始结束时间
-- =============================================
CREATE FUNCTION [dbo].[fn_GetRuleLastAPCycleDateZone]
(	
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS @ResultTable TABLE(StartDate DATETIME,EndDate DATETIME)  
AS
BEGIN
	DECLARE @StartDate DATETIME,@EndDate DATETIME
	DECLARE @Calendar NVARCHAR(50),@CycleMonthCount INT
	DECLARE @MinStartDate DATETIME,@MaxEndDate DATETIME 
	SELECT @Calendar=Calendar,@CycleMonthCount=CycleMonthCount 
	FROM 
	(
		SELECT Calendar,CycleMonthCount FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT Calendar,CycleMonthCount FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE Calendar IS NOT NULL 
	
	SELECT @MaxEndDate=dbo.fn_GetDate(MaxEndDate),
		@MinStartDate=dbo.fn_GetDate(MinStartDate)
	FROM
	(
		SELECT MIN(StartDate) AS MinStartDate,MAX(EndDate) AS MaxEndDate FROM dbo.FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT MIN(StartDate),MAX(EndDate) FROM dbo.RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE MaxEndDate IS NOT NULL 
	
	IF @Calendar='公历'
	BEGIN
		DECLARE @TempEndDate DATETIME
		SELECT @TempEndDate = '19000101'
		WHILE @TempEndDate<=@MaxEndDate
		BEGIN
			SELECT @TempEndDate=DATEADD(MONTH,@CycleMonthCount,@TempEndDate)
		END
		SELECT @EndDate=@TempEndDate-1
		SELECT @StartDate=DATEADD(MONTH,-@CycleMonthCount,@TempEndDate)
	END
	ELSE --租赁
	BEGIN
		DECLARE @TempStartDate DATETIME
		SELECT @TempStartDate = @MinStartDate
		WHILE @TempStartDate<=@MaxEndDate
		BEGIN
			SELECT @TempStartDate=DATEADD(MONTH,@CycleMonthCount,@TempStartDate)
		END
		SELECT @EndDate=@TempStartDate-1
		SELECT @StartDate=DATEADD(MONTH,-@CycleMonthCount,@TempStartDate)
	END
	
	INSERT INTO @ResultTable
	SELECT @StartDate,@EndDate
	
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleFirstAPCycleDateZone]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <20110321>
-- Description:	获取规则第一个AP周期开始结束时间
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetRuleFirstAPCycleDateZone]
(	
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS @ResultTable TABLE(StartDate DATETIME,EndDate DATETIME)  
AS
BEGIN
	DECLARE @StartDate DATETIME,@EndDate DATETIME
	DECLARE @Calendar NVARCHAR(50),@CycleMonthCount INT,@MinStartDate DATETIME 
	SELECT @Calendar=Calendar,@CycleMonthCount=CycleMonthCount 
	FROM 
	(
		SELECT Calendar,CycleMonthCount FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT Calendar,CycleMonthCount FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE Calendar IS NOT NULL 
	
	SELECT @MinStartDate=dbo.fn_GetDate(MinStartDate) 
	FROM
	(
		SELECT MIN(StartDate) AS MinStartDate FROM dbo.FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT MIN(StartDate) FROM dbo.RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE MinStartDate IS NOT NULL 
	
	IF @Calendar='公历'
	BEGIN
		DECLARE @TempStartDate DATETIME
		SELECT @TempStartDate = '19000101'
		WHILE @TempStartDate<=@MinStartDate
		BEGIN
			SELECT @TempStartDate=DATEADD(MONTH,@CycleMonthCount,@TempStartDate)
		END
		SELECT @EndDate=@TempStartDate-1
		SELECT @StartDate=DATEADD(MONTH,-@CycleMonthCount,@TempStartDate)
	END
	ELSE --租赁
	BEGIN
		SELECT @StartDate=@MinStartDate
		SELECT @EndDate=DATEADD(MONTH,@CycleMonthCount,@StartDate)-1
	END
	
	INSERT INTO @ResultTable
	SELECT @StartDate,@EndDate
	
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetRuleCreateTime]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110720
-- Description:	获取规则合同创建时间,用于GL发起的起始月份
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
	--由开始的合同创建和导入时间, 改成未关帐的时间
	
	
	DECLARE @RuleID NVARCHAR(50),@MinGLCycleDate DATETIME
	SELECT @RuleID=RuleID FROM 
	(
		SELECT RuleID FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT RuleID FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID 
	) x
	WHERE RuleID IS NOT NULL 
	
	SELECT @MinGLCycleDate=MIN(CycleStartDate) FROM dbo.GLRecord WHERE RuleID=@RuleID
	
	
	--已发起过GL的, 最小月份就是GL起始月
	IF @MinGLCycleDate IS NOT NULL
	BEGIN
		SELECT @ResultVar=@MinGLCycleDate
	END
	ELSE
	--Modified by Eric
	BEGIN --没有发起过GL的，规则最小时间就是GL起始月
	
		SELECT @ResultVar=MIN(StartDate) FROM(
			SELECT StartDate FROM FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
			UNION
			SELECT StartDate FROM RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID 
		) T
	END
	/*BEGIN  --没有发起过GL的, 当前未关帐的月份就是GL起始月
		DECLARE @MaxClosedMonth NVARCHAR(50)
		SELECT @MaxClosedMonth=MAX(ClosedMonth) FROM dbo.ClosedRecord WHERE FromSRLS=1
		SELECT @ResultVar=@MaxClosedMonth+'01'
		SELECT @ResultVar=DATEADD(MONTH,1,@ResultVar)
	END*/
	
	
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetFirstDueDateError]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: <20110321>
-- Description:	获取FirstDueDate错误
-- =============================================
CREATE  FUNCTION [dbo].[fn_GetFirstDueDateError]
(	
	@ContractSnapshotID NVARCHAR(50)
)
RETURNS @ResultTable TABLE(FirstDueDate NVARCHAR(50),RightDueDateZone NVARCHAR(50))  
AS
BEGIN
	--将合同所有主键信息暂存
		DECLARE @ContractKeyInfo TABLE (IndexId int IDENTITY (0, 1) PRIMARY KEY,[ContractSnapshotID] ObjID,[VendorContractID] ObjID,[VendorEntityID] ObjID,[VendorVendorNo] ObjID,[EntityVendorNo] ObjID,[EntityID] ObjID,[EntityInfoSettingID] ObjID,[FixedRuleSnapshotID] ObjID,[FixedTimeIntervalID] ObjID,[RatioID] ObjID,[RatioRuleSnapshotID] ObjID,[RatioTimeIntervalID] ObjID,[ConditionID] ObjID)
		INSERT INTO @ContractKeyInfo 
		SELECT * FROM v_ContractKeyInfo WHERE ContractSnapshotID=@ContractSnapshotID
	
	DECLARE @RuleInfoTable TABLE(RuleSnapshotID NVARCHAR(50),PayType NVARCHAR(50),FirstDueDate DATETIME,RentType NVARCHAR(50),CycleMonthCount INT)
	INSERT INTO @RuleInfoTable
	SELECT RuleSnapshotID,PayType,FirstDueDate,NULL,CycleMonthCount
	FROM
	(
		SELECT RuleSnapshotID,PayType,dbo.fn_GetDate(FirstDueDate) AS FirstDueDate,CycleMonthCount FROM dbo.FixedRuleSetting 
		WHERE RuleSnapshotID IN 
		(
			SELECT FixedRuleSnapshotID FROM @ContractKeyInfo WHERE FixedRuleSnapshotID IS NOT NULL 
		)
		UNION ALL 
		SELECT RuleSnapshotID,PayType,dbo.fn_GetDate(FirstDueDate) AS FirstDueDate,CycleMonthCount  FROM dbo.RatioCycleSetting 
		WHERE RuleSnapshotID IN 
		(
			SELECT RatioRuleSnapshotID FROM @ContractKeyInfo WHERE RatioRuleSnapshotID IS NOT NULL 
		)
	) x
	
	DECLARE @CycleInfoTable TABLE(RuleSnapshotID NVARCHAR(50),PayType NVARCHAR(50),FirstDueDate DATETIME,RentType NVARCHAR(50),StartDate DATETIME,EndDate DATETIME,CycleMonthCount INT)
	
	INSERT INTO @CycleInfoTable
	SELECT RuleSnapshotID,
		   PayType,
		   dbo.fn_GetDate(FirstDueDate),
		   RentType,
		   (SELECT StartDate FROM dbo.fn_GetRuleFirstAPCycleDateZone(RuleSnapshotID)) StartDate,
		   (SELECT EndDate FROM dbo.fn_GetRuleFirstAPCycleDateZone(RuleSnapshotID)) EndDate,
		   CycleMonthCount
	FROM @RuleInfoTable
	
	INSERT INTO @ResultTable
	SELECT CONVERT(nvarchar(10),FirstDueDate,23),CONVERT(nvarchar(10),ZoneStartDate,23)+'至'+CONVERT(nvarchar(10),ZoneEndDate,23) FROM
	(
		SELECT RuleSnapshotID,
		FirstDueDate, 
		ZoneStartDate,
		ZoneEndDate
		FROM
		(
			SELECT RuleSnapshotID,
						FirstDueDate,
					   CASE PayType WHEN '预付' THEN DATEADD(MONTH,-CycleMonthCount,StartDate)
									WHEN '实付' THEN StartDate
									WHEN '延付' THEN EndDate+1 
						END AS ZoneStartDate,
					   CASE PayType WHEN '预付' THEN StartDate-1
									WHEN '实付' THEN EndDate
									WHEN '延付' THEN DATEADD(MONTH,CycleMonthCount,EndDate)
						END AS ZoneEndDate
			FROM @CycleInfoTable
		) x
	) x WHERE dbo.fn_GetDate(FirstDueDate) NOT BETWEEN ZoneStartDate AND ZoneEndDate
	
	
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetCompanyResponsibleUserIDByRuleID]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110219
-- Description:	通过合同规则获取分公司USERID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetCompanyResponsibleUserIDByRuleID]
(
	@RuleSnapshotID  NVARCHAR(50)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50)

	-- Add the T-SQL statements to compute the return value here

	SELECT @ResultVar=cp.ResponsibleUserID 
	FROM dbo.v_ContractKeyInfo v 
	INNER JOIN dbo.[Contract] c ON v.ContractSnapshotID = c.ContractSnapshotID
	INNER JOIN dbo.SRLS_TB_Master_Company cp ON c.CompanyCode = cp.CompanyCode
	WHERE v.FixedRuleSnapshotID=@RuleSnapshotID OR RatioRuleSnapshotID=@RuleSnapshotID
		
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetGLALLStartEndDateInfo]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:获取固定,所有周期开始结束日期信息
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetGLALLStartEndDateInfo]
(
	@RuleSnapshotID NVARCHAR(50),
	@SelectType NVARCHAR(50) --是否查看所有, '所有': 查看所有, '实际':将小于系统配置参数上线时间的GL筛选掉, NULL当所有处理
)
RETURNS @ResultTable TABLE (IndexId int IDENTITY (1, 1) PRIMARY KEY,RuleSnapshotID NVARCHAR(50),GLStartDate DATETIME,GLEndDate DATETIME)
AS
BEGIN

	DECLARE @TempResultTable TABLE (RuleSnapshotID NVARCHAR(50),GLStartDate DATETIME,GLEndDate DATETIME)
	DECLARE @MinDate DATETIME,@MaxDate DATETIME,@RuleCreateDate DATETIME
	
	SELECT @RuleCreateDate=dbo.fn_GetDate(dbo.fn_GetRuleCreateTime(@RuleSnapshotID))
	
	SELECT @MinDate=dbo.fn_GetDate(MinDate),@MaxDate=dbo.fn_GetDate(MaxDate) 
	FROM
	(
		SELECT MIN(StartDate) AS MinDate,MAX(EndDate) AS MaxDate FROM dbo.FixedTimeIntervalSetting  WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL 
		SELECT MIN(StartDate) AS MinDate,MAX(EndDate) AS MaxDate  FROM dbo.RatioTimeIntervalSetting  WHERE RuleSnapshotID=@RuleSnapshotID
	) x WHERE MinDate IS NOT NULL 
	
	DECLARE @TempDate DATETIME
	SELECT @TempDate=@MinDate
	WHILE dbo.fn_GetMonth(@TempDate)<=dbo.fn_GetMonth(@MaxDate)
	BEGIN
		INSERT INTO @TempResultTable
		SELECT @RuleSnapshotID,dbo.fn_GetMonthFirstDate(@TempDate),dbo.fn_GetMonthLastDate(@TempDate)
		SELECT @TempDate=DATEADD(MONTH,1,@TempDate)
	END
	
	UPDATE @TempResultTable SET GLStartDate=@MinDate WHERE GLStartDate=dbo.fn_GetMonthFirstDate(@MinDate)
	UPDATE @TempResultTable SET GLEndDate=@MaxDate WHERE GLEndDate=dbo.fn_GetMonthLastDate(@MaxDate)
	
	
	INSERT INTO @ResultTable
	SELECT * FROM @TempResultTable
	ORDER BY GLStartDate ASC 

	IF @SelectType='实际'
	BEGIN
		DELETE FROM @ResultTable
		WHERE dbo.fn_GetMonthFirstDate(GLStartDate) < dbo.fn_GetMonthFirstDate(@RuleCreateDate)
	END

	
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetFixedMonthRentAmount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	开始结束时间中固定租金的值
-- =============================================
CREATE   FUNCTION [dbo].[fn_Cal_GetFixedMonthRentAmount]
(
	@RuleSnapshotID NVARCHAR(50),
	@StartDate DATETIME,
	@EndDate DATETIME 
)
RETURNS  DECIMAL(18,2)
AS
BEGIN
	SELECT @StartDate=dbo.fn_GetDate(@StartDate)
	SELECT @EndDate=dbo.fn_GetDate(@EndDate)
	
	DECLARE @TimeZoneTable TABLE(StartDate DATETIME,EndDate DATETIME,Amount DECIMAL(18,2))
	
	INSERT INTO @TimeZoneTable
	SELECT dbo.fn_GetDate(StartDate) StartDate,dbo.fn_GetDate(EndDate) EndDate,Amount 
	FROM dbo.FixedTimeIntervalSetting 
	WHERE RuleSnapshotID=@RuleSnapshotID
	AND (@StartDate BETWEEN dbo.fn_GetDate(StartDate) AND dbo.fn_GetDate(EndDate)
		OR @EndDate BETWEEN dbo.fn_GetDate(StartDate) AND dbo.fn_GetDate(EndDate))
	ORDER BY StartDate ASC 
	
	DECLARE @MinDate DATETIME,@MaxDate DATETIME,@CycleMonthCount INT 
	
	SELECT @MinDate=MIN(StartDate),@MaxDate=MAX(EndDate) FROM @TimeZoneTable
	IF EXISTS(SELECT * FROM @TimeZoneTable WHERE  @StartDate BETWEEN StartDate AND EndDate)
	BEGIN
		UPDATE @TimeZoneTable SET StartDate=@StartDate WHERE StartDate=@MinDate
	END
	IF EXISTS(SELECT * FROM @TimeZoneTable WHERE  @EndDate BETWEEN StartDate AND EndDate)
	BEGIN
		UPDATE @TimeZoneTable SET EndDate=@EndDate WHERE EndDate=@MaxDate
	END
	
	SELECT @CycleMonthCount = CycleMonthCount FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	DECLARE @ResultValue DECIMAL(18,2)
	
	SELECT @ResultValue=SUM(Amount/CONVERT(DECIMAL(18,2), @CycleMonthCount)*dbo.fn_GetCalendarMonthCount(@RuleSnapshotID,StartDate,EndDate))	FROM @TimeZoneTable
	
	
	RETURN @ResultValue
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetFixedAllRentAmount]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20110108
-- Description:	获取固定租金整个租期的租金总额
-- =============================================
CREATE  FUNCTION [dbo].[fn_Cal_GetFixedAllRentAmount]
(
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS  DECIMAL(18,2)
AS
BEGIN
	DECLARE @TotalRentAmount DECIMAL(18,2)
	SET @TotalRentAmount = 0
	
	SELECT @TotalRentAmount=SUM(t.Amount/r.CycleMonthCount*dbo.fn_GetCalendarMonthCount(@RuleSnapshotID,t.StartDate,t.EndDate))	FROM dbo.FixedTimeIntervalSetting t
	INNER JOIN dbo.FixedRuleSetting r ON t.RuleSnapshotID = r.RuleSnapshotID
	WHERE r.RuleSnapshotID=@RuleSnapshotID
	
	RETURN @TotalRentAmount
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPRentStartEndDate]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:获取理论周期开始结束时间, 比如不够一个周期的算够一个周期
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAPRentStartEndDate]
(
	@RuleSnapshotID NVARCHAR(50),
	@APStartDate DATETIME,
	@APEndDate DATETIME 
)
RETURNS @ResultTable TABLE (IndexId int IDENTITY (1, 1) PRIMARY KEY,RuleSnapshotID NVARCHAR(50),RentStartDate DATETIME,RentEndDate DATETIME)
AS
BEGIN

	DECLARE @RentStartDate DATETIME,@RentEndDate DATETIME
	
	SELECT @RentStartDate=@APStartDate,@RentEndDate=@APEndDate
	
	--是否是第一个周期
	IF EXISTS (
	SELECT * 
	FROM dbo.fn_GetRuleFirstAPCycleDateZone(@RuleSnapshotID)
	WHERE @APEndDate BETWEEN StartDate AND EndDate
	)
	BEGIN
		SELECT @RentStartDate=StartDate,
			 @RentEndDate=EndDate
		FROM dbo.fn_GetRuleFirstAPCycleDateZone(@RuleSnapshotID)
	END
	
	--是否是最后一个周期
	IF EXISTS (
	SELECT * 
	FROM dbo.fn_GetRuleLastAPCycleDateZone(@RuleSnapshotID)
	WHERE @APEndDate BETWEEN StartDate AND EndDate
	)
	BEGIN
		SELECT @RentStartDate=StartDate,
			 @RentEndDate=EndDate
		FROM dbo.fn_GetRuleLastAPCycleDateZone(@RuleSnapshotID)
	END
	
	INSERT INTO @ResultTable
	SELECT @RuleSnapshotID,@RentStartDate,@RentEndDate
	
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetDatePointInfo]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	获取APGL计算过程中的时间点
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetDatePointInfo]
(
	-- Add the parameters for the function here
	@RecordID NVARCHAR(50)

)
RETURNS @DatePointTable TABLE(IndexId int IDENTITY (1, 1) PRIMARY KEY,TimeIntervalID NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	--思路AP只被时间段截断, GL被AP和时间段截断
	
	
	DECLARE @TempTable TABLE(DataType NVARCHAR(50),StartDate DATETIME,EndDate DATETIME)
	DECLARE @StartTable TABLE(IndexId int IDENTITY (1, 1),StartDate DATETIME)
	DECLARE @EndTable TABLE(IndexId int IDENTITY (1, 1),EndDate DATETIME)
	DECLARE @APGLType NVARCHAR(50)
	
	IF EXISTS(SELECT * FROM dbo.APRecord WHERE APRecordID=@RecordID)
	BEGIN
		SELECT @APGLType='AP'
	END
	ELSE
	BEGIN
		SELECT @APGLType='GL'
	END
	
	DECLARE @RuleID NVARCHAR(50),@RuleSnapshotID NVARCHAR(50)
	DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME
	
	SELECT @RuleID=RuleID,
			@RuleSnapshotID=RuleSnapshotID,
			@CycleStartDate=CycleStartDate,
			@CycleEndDate=CycleEndDate
	FROM 
	(
		SELECT RuleID,RuleSnapshotID,CycleStartDate,CycleEndDate 
		FROM dbo.APRecord WHERE APRecordID=@RecordID
		UNION ALL 
		SELECT RuleID,RuleSnapshotID,CycleStartDate,CycleEndDate 
		FROM dbo.GLRecord WHERE GLRecordID=@RecordID
	) x WHERE RuleID IS NOT NULL 

	
	INSERT INTO @TempTable
	SELECT '时间段',dbo.fn_GetDate(StartDate),dbo.fn_GetDate(EndDate)
	FROM 
	(
		SELECT  StartDate,EndDate
		FROM dbo.FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL
		SELECT StartDate,EndDate
		FROM dbo.RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) x
	WHERE StartDate IS NOT NULL 
	ORDER BY StartDate ASC 
	
	IF @APGLType='GL'
	BEGIN
		INSERT INTO @TempTable
		SELECT 'AP',APStartDate,APEndDate 
		FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'所有')
		ORDER BY APStartDate ASC
	END
	
	
	INSERT INTO @TempTable
	SELECT 'GL',@CycleStartDate,@CycleEndDate
	
	DELETE FROM  @TempTable 
	WHERE (StartDate NOT BETWEEN @CycleStartDate AND @CycleEndDate)
		AND (EndDate NOT BETWEEN @CycleStartDate AND @CycleEndDate)
		
	UPDATE @TempTable SET StartDate=@CycleStartDate WHERE StartDate < @CycleStartDate
	UPDATE @TempTable SET EndDate=@CycleEndDate WHERE EndDate > @CycleEndDate
	
	INSERT INTO @StartTable
	SELECT DISTINCT StartDate FROM @TempTable ORDER BY StartDate ASC 
	
	INSERT INTO @EndTable
	SELECT DISTINCT EndDate FROM @TempTable ORDER BY EndDate ASC 

	DELETE FROM  @TempTable
	INSERT INTO @TempTable 
	SELECT NULL,s.StartDate,e.EndDate 
	FROM @StartTable s INNER JOIN @EndTable e ON s.IndexId = e.IndexId
	
	INSERT INTO @DatePointTable
	SELECT t.TimeIntervalID,tt.StartDate,tt.EndDate 
	FROM @TempTable tt 
	INNER JOIN 
	(
		SELECT  TimeIntervalID,dbo.fn_GetDate(StartDate) StartDate,dbo.fn_GetDate(EndDate) EndDate 
		FROM dbo.FixedTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
		UNION ALL
		SELECT  TimeIntervalID,dbo.fn_GetDate(StartDate) StartDate,dbo.fn_GetDate(EndDate) EndDate 
		FROM dbo.RatioTimeIntervalSetting WHERE RuleSnapshotID=@RuleSnapshotID
	) t ON tt.StartDate BETWEEN t.StartDate AND t.EndDate
	WHERE t.StartDate IS NOT NULL 
	
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetRuleDescription]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取规则描述
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetRuleDescription]
(
	-- Add the parameters for the function here
	@RecordID NVARCHAR(50),
	@RecordType NVARCHAR(50)
)
RETURNS NVARCHAR(2000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(2000)
	DECLARE @RuleSnapshotID NVARCHAR(50),@GLType NVARCHAR(50)
	

	-- Add the T-SQL statements to compute the return value here
	IF @RecordType='AP'
	BEGIN
		SELECT @RuleSnapshotID=RuleSnapshotID 
		FROM dbo.APRecord WHERE APRecordID=@RecordID
		
		SELECT @ResultVar=Description FROM 
		(
			SELECT RuleSnapshotID,Description FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
			UNION ALL 
			SELECT c.RuleSnapshotID,r.Description FROM dbo.RatioRuleSetting r
			INNER JOIN dbo.RatioCycleSetting c ON r.RatioID=c.RatioID
			WHERE c.RuleSnapshotID=@RuleSnapshotID
		) x WHERE RuleSnapshotID IS NOT NULL 
		
	END
	
	IF @RecordType='GL'
	BEGIN
		SELECT @RuleSnapshotID=RuleSnapshotID,@GLType=GLType
		FROM dbo.GLRecord WHERE GLRecordID=@RecordID
		
		IF @GLType='直线租金'
		BEGIN
			SELECT @ResultVar=dbo.fn_Cal_GetAccountRemark(@RecordID,'GL')
		END
		ELSE
		BEGIN
			SELECT @ResultVar=Description FROM 
			(
				SELECT RuleSnapshotID,Description FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
				UNION ALL 
				SELECT c.RuleSnapshotID,r.Description FROM dbo.RatioRuleSetting r
				INNER JOIN dbo.RatioCycleSetting c ON r.RatioID=c.RatioID
				WHERE c.RuleSnapshotID=@RuleSnapshotID
			) x WHERE RuleSnapshotID IS NOT NULL 
		END
	END
	
	SELECT @ResultVar=ISNULL(@ResultVar,'')
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_MessageFormatValue]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	APGL检查提示信息的详细展示
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_MessageFormatValue]
(
	-- Add the parameters for the function here
	@APGLType NVARCHAR(50),
	@EnFlag NVARCHAR(50),
	@RecordID NVARCHAR(50),
	@IsSolved BIT 
)
RETURNS NVARCHAR(100)
AS
BEGIN


	--可能的值有
	--RenewalFirstTime
	--RuleChangeFirstTime
	--StoreSalesZero
	--AccountInactive
	--RenewalFirstTime
	--RuleChangeFirstTime
	--StoreSalesZero
	--AccountInactive
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(100)
	
	
--	--已解决
--	IF @IsSolved = 1
--	BEGIN
--		SELECT @ResultVar=''
--		RETURN @ResultVar
--	END
	--相关数据
	DECLARE @FormatValue NVARCHAR(1000)
	SELECT @FormatValue=''
	
	DECLARE @TypeCodeName NVARCHAR(50),@RuleID NVARCHAR(50)
	DECLARE @CycleStartDate DATETIME,@CycleEndDate DATETIME 
	DECLARE @StoreNo NVARCHAR(50)
	DECLARE @DateListTable TABLE(IndexID INT,ListDate DATETIME)
	DECLARE @StoreSalesListTable TABLE(StoreNo NVARCHAR(50),SalesDate NVARCHAR(50))
	DECLARE @GLType nvarchar(50)
	
	SELECT @TypeCodeName=TypeCodeName,
		@RuleID=RuleID,
		@CycleStartDate=CycleStartDate,
		@CycleEndDate=CycleEndDate,
		@StoreNo=StoreOrDeptNo,
		@GLType=GLType
	FROM
	(
		SELECT e.StoreOrDeptNo,ap.TypeCodeName,ap.EntityTypeName,ap.RuleID,ap.CycleStartDate,ap.CycleEndDate,NULL as GLType
		FROM dbo.APRecord ap 
		INNER JOIN dbo.Entity e ON ap.EntityID = e.EntityID
		WHERE ap.APRecordID=@RecordID
		UNION ALL 
		SELECT e.StoreOrDeptNo,GL.TypeCodeName,GL.EntityTypeName,GL.RuleID,GL.CycleStartDate,GL.CycleEndDate,GL.GLType 
		FROM dbo.GLRecord GL 
		INNER JOIN dbo.Entity e ON GL.EntityID = e.EntityID
		WHERE GL.GLRecordID=@RecordID
	) x WHERE RuleID IS NOT NULL 
	
	IF @APGLType = 'AP'
	BEGIN
		IF @EnFlag='KioskSalesFlowUnFinish'
		BEGIN
			SELECT @FormatValue=dbo.fn_Cal_SelectUnFinishKioskNo(@RecordID)
			SELECT @ResultVar=ISNULL(@FormatValue,'')
		END
	
		IF @EnFlag='AccountInactive'
		BEGIN
			SELECT @FormatValue=dbo.fn_Cal_GetAPAccountNumber(@RecordID)
			SELECT @ResultVar=ISNULL(@FormatValue,'')
		END
		
		IF @EnFlag='StoreSalesZero'
		BEGIN
			DELETE FROM  @StoreSalesListTable
			INSERT INTO @StoreSalesListTable 
			SELECT StoreNo,Sales FROM StoreSales
			WHERE StoreNo=@StoreNo
			AND (SalesDate BETWEEN @CycleStartDate AND @CycleEndDate)
			AND Sales=0 
			SELECT   @FormatValue = COUNT(*)
			FROM      @StoreSalesListTable
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='RuleChangeFirstTime'
		BEGIN
			
			SELECT TOP 1 @FormatValue=APAmount 
			FROM dbo.APRecord 
			WHERE RuleID=@RuleID AND CycleEndDate < @CycleStartDate
			ORDER BY CycleEndDate DESC
			SELECT @FormatValue = ISNULL(@FormatValue,'0')
			IF @FormatValue=''
			BEGIN
				SELECT @FormatValue='0'
			END
			
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='RenewalFirstTime'
		BEGIN
			SELECT @FormatValue='0'
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='StoreSalesMiss'
		BEGIN
			DELETE FROM  @DateListTable
			INSERT INTO @DateListTable
			SELECT IndexID,ListDate FROM dbo.fn_Helper_GetDateList(@CycleStartDate,@CycleEndDate)
			
			DELETE FROM @StoreSalesListTable
			INSERT INTO @StoreSalesListTable
			SELECT StoreNo,SalesDate FROM dbo.StoreSales 
			WHERE StoreNo=@StoreNo AND (SalesDate BETWEEN @CycleStartDate AND @CycleEndDate)
			
			SELECT @FormatValue=''
			SELECT @FormatValue=@FormatValue+CONVERT(NVARCHAR(10), ListDate, 23)+';' 
			FROM 
			(
				SELECT x.ListDate,y.SalesDate FROM @DateListTable x
				LEFT  JOIN @StoreSalesListTable y ON x.ListDate=y.SalesDate
			) z WHERE SalesDate IS NULL 
			ORDER BY ListDate ASC 
			
			SELECT @ResultVar=@FormatValue
		END
	END
	
	
	IF @APGLType = 'GL'
	BEGIN
		IF @EnFlag='KioskSalesFlowUnFinish'
		BEGIN
			SELECT @FormatValue=dbo.fn_Cal_SelectUnFinishKioskNo(@RecordID)
			SELECT @ResultVar=ISNULL(@FormatValue,'')
		END
		
		IF @EnFlag='AccountInactive'
		BEGIN
			SELECT @FormatValue=@FormatValue+dbo.fn_Cal_GetGLCreditAccountNumber(@RecordID)+';'+dbo.fn_Cal_GetGLDebitAccountNumber(@RecordID)
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='StoreSalesZero'
		BEGIN
			DELETE FROM  @StoreSalesListTable
			INSERT INTO @StoreSalesListTable 
			SELECT StoreNo,Sales FROM StoreSales
			WHERE StoreNo=@StoreNo
			AND (SalesDate BETWEEN @CycleStartDate AND @CycleEndDate)
			AND Sales=0 
			SELECT   @FormatValue = COUNT(*)
			FROM      @StoreSalesListTable
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='RuleChangeFirstTime'
		BEGIN
			--20110927:改成取对应具体规则上次计算的金额
			SELECT TOP 1 @FormatValue=GLAmount 
			FROM dbo.GLRecord 
			WHERE RuleID=@RuleID AND GLType=@GLType AND CycleEndDate < @CycleStartDate
			ORDER BY CycleEndDate DESC
			SELECT @FormatValue = ISNULL(@FormatValue,'0')
			IF @FormatValue=''
			BEGIN
				SELECT @FormatValue='0'
			END
			
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='RenewalFirstTime'
		BEGIN
			SELECT @FormatValue='0'
			SELECT @ResultVar=@FormatValue
		END
		
		IF @EnFlag='StoreSalesMiss'
		BEGIN
			DELETE FROM  @DateListTable
			INSERT INTO @DateListTable
			SELECT IndexID,ListDate FROM dbo.fn_Helper_GetDateList(@CycleStartDate,@CycleEndDate)
			
			DELETE FROM @StoreSalesListTable
			INSERT INTO @StoreSalesListTable
			SELECT StoreNo,SalesDate FROM dbo.StoreSales 
			WHERE StoreNo=@StoreNo AND (SalesDate BETWEEN @CycleStartDate AND @CycleEndDate)
			
			SELECT @FormatValue=''
			SELECT @FormatValue=@FormatValue+CONVERT(NVARCHAR(10), ListDate, 23)+';' 
			FROM 
			(
				SELECT x.ListDate,y.SalesDate FROM @DateListTable x
				LEFT  JOIN @StoreSalesListTable y ON x.ListDate=y.SalesDate
			) z WHERE SalesDate IS NULL 
			ORDER BY ListDate ASC 
			
			SELECT @ResultVar=@FormatValue
		
		END
	END 
		
		

	SELECT @ResultVar=ISNULL(@ResultVar,'')
	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetZXNextGLCreateDate]    Script Date: 08/16/2012 19:28:25 ******/
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
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetZXGLCycleInfo]    Script Date: 08/16/2012 19:28:25 ******/
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
/****** Object:  UserDefinedFunction [dbo].[fn_Kiosk_GetMinAPDate]    Script Date: 08/16/2012 19:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	获取KIOSK收集中最小的AP开始日期
-- =============================================
CREATE FUNCTION [dbo].[fn_Kiosk_GetMinAPDate] 
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME
	
	DECLARE @MinFactGLStartDate DATETIME,@MinAllAPStartDate DATETIME,@MinFactAPStartDate DATETIME
	DECLARE @APDateAllInfoTable TABLE(StartDate DATETIME,EndDate DATETIME)
	DECLARE @APDateFactInfoTable TABLE(StartDate DATETIME,EndDate DATETIME)
	DECLARE @GLDateFactInfoTable TABLE(StartDate DATETIME,EndDate DATETIME)
	
	INSERT INTO @APDateAllInfoTable
	SELECT 
		   APStartDate,
		   APEndDate FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'所有')
		   
	INSERT INTO @APDateFactInfoTable
	SELECT 
		   APStartDate,
		   APEndDate FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'实际')
	
	INSERT INTO @GLDateFactInfoTable	   
	SELECT 
		   GLStartDate,
		   GLEndDate FROM dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'实际')
	--AP实际的最小日期
	SELECT @MinFactAPStartDate=MIN(StartDate) FROM @APDateFactInfoTable
	--GL实际的最小日期
	SELECT @MinFactGLStartDate=MIN(StartDate) FROM @GLDateFactInfoTable
	
	--GL需要用到的AP最小日期
	SELECT @MinAllAPStartDate=StartDate FROM @APDateAllInfoTable
	WHERE @MinFactGLStartDate BETWEEN StartDate AND EndDate 
	
	--取两个日期中较小的
	SELECT @ResultVar=dbo.fn_GetMinDate(@MinFactAPStartDate,@MinAllAPStartDate)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAPGLDateZoneOfStore]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<获取某个餐厅在某个日期以前需要发起的所有KioskSales收集时间段>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAPGLDateZoneOfStore]
(	
	@StoreNo NVARCHAR(50), --待获取的StoreNo
	@CreateDate DATETIME   --日期
)
RETURNS @Result TABLE(ZoneStartDate DATETIME ,ZoneEndDate DATETIME)
AS
BEGIN
	--获取该Store关联的所有规则ID（包括历史关联的规则）
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @RuleSnapshotIDTable TABLE(RuleSnapshotID NVARCHAR(50))
	INSERT INTO @RuleSnapshotIDTable(RuleSnapshotID) SELECT RuleSnapshotID FROM dbo.fn_GetRuleSnapshotIDByStoreNo(@StoreNo)
	
	--遍历每个规则以获取理上论需要发起的时间段
	SET @RuleSnapshotID=(SELECT TOP 1 RuleSnapshotID FROM @RuleSnapshotIDTable)
	WHILE @RuleSnapshotID IS NOT NULL
	BEGIN
		INSERT INTO @Result(ZoneStartDate, ZoneEndDate)
		SELECT ZoneStartDate, ZoneEndDate
		FROM
		(
			SELECT APStartDate AS ZoneStartDate,APEndDate AS ZoneEndDate FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'只查询需要出AP的') WHERE DATEDIFF(DAY,APEndDate,@CreateDate)>=0
			UNION ALL
			SELECT GLStartDate AS ZoneStartDate,GLEndDate AS ZoneEndDate FROM dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'只查询系统上线后的') WHERE DATEDIFF(DAY,GLEndDate,@CreateDate)>=0
		) AS t0
		--从表变量中删除当前RuleSnapshotID，获取下个循环的RuleSnapshotID
		DELETE FROM @RuleSnapshotIDTable WHERE RuleSnapshotID=@RuleSnapshotID 
		SET @RuleSnapshotID=(SELECT TOP 1 RuleSnapshotID FROM @RuleSnapshotIDTable)
	END
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAPGLDateZoneOfKiosk]    Script Date: 08/16/2012 19:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fengxh>
-- Create date: <20110321>
-- Description:	<获取某个实体类型为指定的Kiosk在指定日期前发起的所有APGL时间段>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAPGLDateZoneOfKiosk]
(	
	@KioskID NVARCHAR(50), --待获取的KioskID
	@CreateDate DATETIME   
)
RETURNS @Result TABLE(ZoneStartDate DATETIME ,ZoneEndDate DATETIME)
AS
BEGIN
	--获取该Kiosk关联的所有规则ID（包括历史关联的规则）
	DECLARE @RuleSnapshotID NVARCHAR(50)
	DECLARE @RuleSnapshotIDTable TABLE(RuleSnapshotID NVARCHAR(50))
	INSERT INTO @RuleSnapshotIDTable(RuleSnapshotID) SELECT RuleSnapshotID FROM dbo.fn_GetRuleSnapshotIDByKioskID(@KioskID)
	
	--遍历每个规则以获取理上论需要发起的时间段
	SET @RuleSnapshotID=(SELECT TOP 1 RuleSnapshotID FROM @RuleSnapshotIDTable)
	WHILE @RuleSnapshotID IS NOT NULL
	BEGIN
		INSERT INTO @Result (ZoneStartDate, ZoneEndDate)
		SELECT ZoneStartDate, ZoneEndDate
		FROM
		(
			SELECT APStartDate AS ZoneStartDate,APEndDate AS ZoneEndDate FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'只查询需要出AP的') WHERE DATEDIFF(DAY,APEndDate,@CreateDate)>=0
			UNION ALL
			SELECT GLStartDate AS ZoneStartDate,GLEndDate AS ZoneEndDate FROM dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'只查询系统上线后的') WHERE DATEDIFF(DAY,GLEndDate,@CreateDate)>=0
		) AS t0
		--从表变量中删除当前RuleSnapshotID，获取下个循环的RuleSnapshotID
		DELETE FROM @RuleSnapshotIDTable WHERE RuleSnapshotID=@RuleSnapshotID 
		SET @RuleSnapshotID=(SELECT TOP 1 RuleSnapshotID FROM @RuleSnapshotIDTable)
	END
	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetRuleAPGLNextStartEndDate]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		zhangbsh
-- Create date: 20100108
-- Description:取下次周期日期
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetRuleAPGLNextStartEndDate]
(
	@RuleSnapshotID NVARCHAR(50)
)
RETURNS @ResultTable table (RuleSnapshotID NVARCHAR(50),NextDueDate DATETIME,NextAPStartDate DATETIME,NextAPEndDate DATETIME,NextGLStartDate DATETIME,NextGLEndDate DATETIME)
AS
BEGIN
	  DECLARE @NextDueDate DATETIME,@NextAPStartDate DATETIME,@NextAPEndDate DATETIME,@NextGLStartDate DATETIME,@NextGLEndDate DATETIME;
	  
	  DECLARE @RuleID NVARCHAR(50)
	  
	  SELECT @RuleID=RuleID
	  FROM 
	  (
		 SELECT RuleID FROM dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID
		 UNION ALL 
		 SELECT RuleID FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID
	  ) x WHERE RuleID IS NOT NULL  
	  
	  --没有发起过AP
	  IF NOT EXISTS(SELECT * FROM dbo.APRecord WHERE RuleID=@RuleID AND RelationAPRecordID IS NULL)
	  BEGIN
			--没有发起过,取第一个AP周期
			SELECT TOP 1 @NextDueDate=DueDate,@NextAPStartDate=APStartDate,@NextAPEndDate=APEndDate
			FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'实际')
			ORDER BY APStartDate ASC 
	  END--没有发起过AP
	  ELSE
	  BEGIN --发起过AP
			DECLARE @MaxAPEndDate DATETIME
			SELECT @MaxAPEndDate=MAX(CycleEndDate) 
			FROM dbo.APRecord  
			WHERE RuleID=@RuleID
			--发起过,取AP周期大于最后一个周期的
			SELECT TOP 1 @NextDueDate=DueDate,@NextAPStartDate=APStartDate,@NextAPEndDate=APEndDate
			FROM dbo.fn_Cal_GetAPALLStartEndDateInfo(@RuleSnapshotID,'实际')
			WHERE APEndDate > @MaxAPEndDate
			ORDER BY APStartDate ASC 

	  END--发起过AP
	  
	  --没有发起过GL
	  IF NOT EXISTS(SELECT * FROM dbo.GLRecord WHERE RuleID=@RuleID AND GLType<>'直线租金')
	  BEGIN
			--没有发起过,取第一个GL周期
  			SELECT TOP 1 @NextGLStartDate=GLStartDate,@NextGLEndDate=GLEndDate 
  			FROM dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'实际')
  			ORDER BY GLStartDate ASC 
	  END --没有发起过GL
	  ELSE
	  BEGIN --发起过GL
  			DECLARE @MaxGLEndDate DATETIME
  			SELECT @MaxGLEndDate=MAX(CycleEndDate)
  			FROM dbo.GLRecord 
  			WHERE RuleID=@RuleID AND GLType<>'直线租金'
  			
  			--发起过,取GL周期大于最后一个周期结束时间的
  			SELECT TOP 1 @NextGLStartDate=GLStartDate,@NextGLEndDate=GLEndDate 
  			FROM dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'实际')
  			WHERE GLEndDate>@MaxGLEndDate
  			ORDER BY GLStartDate ASC 

	  END--发起过GL
	  
	  
  INSERT INTO @ResultTable	
  SELECT @RuleSnapshotID,@NextDueDate,@NextAPStartDate,@NextAPEndDate,@NextGLStartDate,@NextGLEndDate
RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Cal_GetAPPaidFixedAmountByDate]    Script Date: 08/16/2012 19:28:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	计算百分比GL时的退回固定租金
-- =============================================
CREATE FUNCTION [dbo].[fn_Cal_GetAPPaidFixedAmountByDate]
(
	-- Add the parameters for the function here
	@RuleSnapshotID NVARCHAR(50),
	@APStartDate DATETIME,
	@APEndDate DATETIME
	
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DECIMAL(18,2)
	DECLARE @EntityInfoSettingID NVARCHAR(50)
	DECLARE @RentType NVARCHAR(50),@CycleType NVARCHAR(50)
	DECLARE @FixedRuleSnapshotID NVARCHAR(50)

	
	SELECT @EntityInfoSettingID=r.EntityInfoSettingID,
		@RentType=r.RentType,
		@CycleType=c.CycleType
	FROM dbo.RatioCycleSetting c 
	INNER JOIN dbo.RatioRuleSetting r ON c.RatioID = r.RatioID
	WHERE RuleSnapshotID=@RuleSnapshotID
	
	SELECT @FixedRuleSnapshotID=RuleSnapshotID FROM dbo.FixedRuleSetting 
	WHERE EntityInfoSettingID=@EntityInfoSettingID AND RentType=REPLACE(@RentType,'百分比','固定')
	
	SELECT @ResultVar=dbo.fn_Cal_GetFixedMonthRentAmount(@FixedRuleSnapshotID,@APStartDate,@APEndDate)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar=ISNULL(@ResultVar,0)

	-- Return the result of the function
	RETURN @ResultVar

END
GO
