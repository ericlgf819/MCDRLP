USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Task_SelectFinishedTasks]    Script Date: 08/12/2012 12:00:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


















-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Task_SelectFinishedTasks]
@AreaName nvarchar(50)=NULL,
@CompanyCode nvarchar(50)=NULL,
@StoreOrKioskNo nvarchar(50)=NULL,
@TaskType nvarchar(50)=NULL,
@ErrorType nvarchar(100)=NULL,
@TimeZoneFlag int,	--0当日, 1当月, 2今年, 3往年
@UserID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Sql nvarchar(max)
	
 	--Sql主体
	SET @Sql = 'WITH Tmp AS(SELECT ''E'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, U.UserName AS FixUserName, FC.CreateTime AS CreateTime,
				C.CompanyCode, A.AreaName, FC.CalEndDate, FC.IsSolved, FC.Remark, FC.TaskNo, FC.TaskFinishTime,
				UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID, FC.EntityID AS EntityID,
				NULL AS Operation, FC.CheckID
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_User U ON FC.FixUserID = U.ID
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=1 AND FC.IsSolved=1
				UNION ALL
				SELECT ''I'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, U.UserName AS FixUserName, FC.CreateTime AS CreateTime,
				C.CompanyCode, A.AreaName, FC.CalEndDate, FC.IsSolved, FC.Remark, FC.TaskNo, FC.TaskFinishTime,
				UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID, FC.EntityID AS EntityID,
				CASE WHEN FC.IsSolved=1 THEN NULL ELSE ''问题修复'' END AS Operation, FC.CheckID
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_User U ON FC.FixUserID = U.ID
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=0 
				AND (FC.IsSolved=1 OR (FC.IsSolved=0 AND (YEAR(FC.CreateTime)<>YEAR(GETDATE())
				OR MONTH(FC.CreateTime)<>MONTH(GETDATE()) OR DAY(FC.CreateTime)<>DAY(GETDATE()))))
				)SELECT * FROM Tmp '
				
	--Where从句
	DECLARE @Where nvarchar(max)
	SET @Where = 'WHERE 1 = 1 '
	
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
	
	--排序语句
	DECLARE @OrderClause nvarchar(max)
	SET @OrderClause = 'ORDER BY CreateTime DESC'
	
	--组合成最终的sql语句并执行
	SET @Sql = @Sql + @Where + @OrderClause
	print @Sql
	EXEC(@Sql)
END


















GO


