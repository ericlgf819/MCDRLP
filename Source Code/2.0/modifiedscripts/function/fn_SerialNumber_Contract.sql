USE [RLPlanning]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_SerialNumber_Contract]    Script Date: 06/29/2012 10:18:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		zhangbsh
-- Create date: 20110109
-- Description:	��ȡ��ˮ��  ��ͬ����ں�ͬ�������������������ͨ����˺����ɡ�����Ϊ����˾���.��ˮ�� ��ˮ��Ϊ6λ
-- =============================================
ALTER FUNCTION [dbo].[fn_SerialNumber_Contract]
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


