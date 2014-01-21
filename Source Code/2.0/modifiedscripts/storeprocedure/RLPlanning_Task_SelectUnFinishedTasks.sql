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
@TimeZoneFlag int,	--0����, 1����, 2����, 3����
@UserID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Sql nvarchar(max)
	
	--Sql����
	SET @Sql = 'WITH Tmp AS(SELECT CASE WHEN FC.IsRead=1 THEN ''�Ѷ�'' ELSE ''δ��'' END AS IsRead,
				''E'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
				''�����޸�'' AS Operation, C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark, FC.IsSolved, 
				FC.CalEndDate, FC.ContractNo, UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID,
				FC.EntityID AS EntityID, C.CompanyName AS CompanyName,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN ''����'' ELSE ''��Ʒ��'' END AS EntityType,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN S.SimpleName ELSE K.KioskName END AS EntityName
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=1
				UNION ALL
				SELECT CASE WHEN FC.IsRead=1 THEN ''�Ѷ�'' ELSE ''δ��'' END AS IsRead,
				''I'' AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
				''�����޸�'' AS Operation, C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark, FC.IsSolved, 
				FC.CalEndDate, FC.ContractNo, UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID,
				FC.EntityID AS EntityID, C.CompanyName AS CompanyName,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN ''����'' ELSE ''��Ʒ��'' END AS EntityType,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='''' THEN S.SimpleName ELSE K.KioskName END AS EntityName
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
				WHERE FC.TaskType=0 AND YEAR(FC.CreateTime)=YEAR(GETDATE())
				AND MONTH(FC.CreateTime)=MONTH(GETDATE()) AND DAY(FC.CreateTime)=DAY(GETDATE())
				)SELECT * FROM Tmp '
	
	--Where�Ӿ�
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
	
	--ʱ��γ��
	DECLARE @PivotDate datetime, @CurDate datetime
	SET @CurDate = GETDATE()
	IF @TimeZoneFlag = 0 --����
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-' + STR(MONTH(@CurDate)) + '-' + STR(DAY(@CurDate))
	END
	ELSE IF @TimeZoneFlag = 1 --����
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-' + STR(MONTH(@CurDate)) + '-1'
	END
	ELSE IF @TimeZoneFlag = 2 --����
	BEGIN
		SET @PivotDate = STR(YEAR(@CurDate)) + '-1-1'
	END
	--�������ȫ��ʱ��ζ���Ҫ
	
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
	
	--�������
	DECLARE @OrderClause nvarchar(max)
	SET @OrderClause = 'ORDER BY CreateTime DESC'
	
	--��ϳ����յ�sql��䲢ִ��
	SET @Sql = @Sql + @Where + @OrderClause
	print @Sql
	EXEC(@Sql)
END





















GO


