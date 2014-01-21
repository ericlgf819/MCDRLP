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
-- Description:	��ȡ����ʱ���TaskNo
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
	
	--TaskNo��ʽ��YYYYMMDD.XXXXXX,���Ժ�׺Number��substring��substring(@str,10,6)
	--Dateת��nvarchar�ĸ�ʽ��YYYY-MM-DD
	--YYYY��substring��substring(@str,1,4)
	--MM��substring��substring(@str,6,2)
	--DD��substring��substring(@str,9,2)
	--YYYYMMDD��substring��substring(@str,1,8)
	
	--��YYYY-MM-DDת����YYYYMMDD
	SET @StrDate = CAST(@Date AS nvarchar(50))
	SET @StrDate = SUBSTRING(@StrDate,1,4) + SUBSTRING(@StrDate,6,2) + SUBSTRING(@StrDate,9,2)
	
	--��ͬһʱ���µ�TaskSerial���洢���������Һ�׺No�����ֵ��Ȼ��ת���ַ�������Ϊ�µ�TaskNo
	INSERT INTO @TmpTable
	SELECT CAST(SUBSTRING(TaskNo,10,6) AS INT) FROM Forecast_CheckResult
	WHERE @StrDate = SUBSTRING(TaskNo,1,8)
	
	--���û����ͬһʱ���²�������ˮ�ţ�������µ���ˮ��
	IF NOT EXISTS (SELECT * FROM @TmpTable)
	BEGIN
		SET @Result = @StrDate + '.000001'
	END
	--�Ѿ�����ˮ���ˣ���ȡ������ˮ�ż�1
	ELSE
	BEGIN
		SELECT @SerialNo = MAX(TaskSerialNo) FROM @TmpTable
		SET @SerialNo += 1
		
		--ת��nvarchar��������Ȳ���6λ�����Զ���ǰ���0
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


