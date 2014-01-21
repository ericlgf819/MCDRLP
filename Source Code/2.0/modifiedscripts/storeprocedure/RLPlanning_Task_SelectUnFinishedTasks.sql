USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Task_SelectUnFinishedTasks]    Script Date: 08/11/2012 16:02:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





















-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Task_SelectUnFinishedTasks]
@AreaName nvarchar(50)=NULL,
@CompanyCode nvarchar(50)=NULL,
@StoreOrKioskNo nvarchar(50)=NULL,
@TaskType nvarchar(10)=NULL,
@ErrorType nvarchar(100)=NULL,
@IsRead nvarchar(10)=NULL,
@TimeZoneFlag int,	--0当日, 1当月, 2今年, 3往年
@UserID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Sql nvarchar(max)
	
	--Sql主体
	SET @Sql = 'WITH Tmp AS(SELECT CASE WHEN FC.IsRead=1 THEN ''已读'' ELSE ''未读'' END AS IsRead,
				''E'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
				''问题修复'' AS Operation, C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark, FC.IsSolved, 
				FC.CalEndDate, FC.ContractNo, UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID,
				FC.EntityID AS EntityID, C.CompanyName AS CompanyName,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN ''餐厅'' ELSE ''甜品店'' END AS EntityType,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN S.SimpleName ELSE K.KioskName END AS EntityName
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=1
				UNION ALL
				SELECT CASE WHEN FC.IsRead=1 THEN ''已读'' ELSE ''未读'' END AS IsRead,
				''I'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
				''问题修复'' AS Operation, C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark, FC.IsSolved, 
				FC.CalEndDate, FC.ContractNo, UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID,
				FC.EntityID AS EntityID, C.CompanyName AS CompanyName,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN ''餐厅'' ELSE ''甜品店'' END AS EntityType,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN S.SimpleName ELSE K.KioskName END AS EntityName
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=0 AND YEAR(FC.CreateTime)=YEAR(GETDATE())
				AND MONTH(FC.CreateTime)=MONTH(GETDATE()) AND DAY(FC.CreateTime)=DAY(GETDATE())
				)SELECT * FROM Tmp '
	
	--Where从句
	DECLARE @Where nvarchar(max)
	SET @Where = 'WHERE IsSolved = 0 '
	
	--ID
	SET @Where += 'AND UserID=''' + CAST(@UserID AS nvarchar(50)) + ''' '
	
	--Area
	IF @AreaName IS NULL
	BEGIN
		DECLARE @AreaCondition nvarchar(max)
		SET @AreaCondition = dbo.RLPlanning_FN_GetAreaSqlConditionStr(@UserID, 1)
		IF @AreaCondition <> ''
		BEGIN
			SET @Where += 'AND ' + @AreaCondition + ' '
		END
	END
	ELSE
	BEGIN
		SET @Where += 'AND AreaName=''' + @AreaName + ''' '
	END
	
	--时间纬度
	DECLARE @PivotDate datetime, @CurDate datetime
	SET @CurDate = GETDATE()
	IF @TimeZoneFlag = 0 --当天
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-' + STR(MONTH(@CurDate)) + '-' + STR(DAY(@CurDate))
	END
	ELSE IF @TimeZoneFlag = 1 --当月
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-' + STR(MONTH(@CurDate)) + '-1'
	END
	ELSE IF @TimeZoneFlag = 2 --今年
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-1-1'
	END
	--往年就是全部时间段都会要
	
	IF @PivotDate IS NOT NULL
	BEGIN
		SET @Where += 'AND CreateTime >= ''' + CAST(@PivotDate AS nvarchar(50)) + ''' '
	END
	
	
	--Company Code
	IF @CompanyCode IS NOT NULL
	BEGIN
		SET @Where += 'AND CompanyCode = ''' + @CompanyCode + ''' ' 
	END
	
	--StoreOrKioskNo
	IF @StoreOrKioskNo IS NOT NULL
	BEGIN
		SET @Where += 'AND (StoreNo like ''%' + @StoreOrKioskNo + '%'' OR KioskNo like ''%' + @StoreOrKioskNo + '%'') '
	END
	
	--TaskType
	IF @TaskType IS NOT NULL
	BEGIN
		SET @Where += 'AND TaskType=''' + @TaskType + ''' '
	END
	
	--ErrorType
	IF @ErrorType IS NOT NULL
	BEGIN
		SET @Where += 'AND ErrorType=''' + @ErrorType + ''' '
	END
	
	--IsRead
	IF @IsRead IS NOT NULL
	BEGIN
		SET @Where += 'AND IsRead=''' + @IsRead + ''' '
	END
	
	--排序语句
	DECLARE @OrderClause nvarchar(max)
	SET @OrderClause = 'ORDER BY CreateTime DESC'
	
	--组合成最终的sql语句并执行
	SET @Sql = @Sql + @Where + @OrderClause
	print @Sql
	EXEC(@Sql)
END





















GO


