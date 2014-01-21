USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_FN_GetAreaSqlConditionStr]    Script Date: 07/24/2012 11:01:59 ******/
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


