USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[RLPlanning_CalTaskNo]    Script Date: 07/16/2012 13:55:45 ******/
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


