USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_GetContractLastVersionSnapshotID]    Script Date: 07/27/2012 14:34:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_GetContractLastVersionSnapshotID]
@ContractSnapshotID nvarchar(50),
@LastContractSnapshotID nvarchar(50) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		--�Ȼ�ȡVersion��ContractID
	DECLARE @Version nvarchar(50), @ContractID nvarchar(50)
	
	SELECT @Version=Version, @ContractID=ContractID FROM Contract 
	WHERE ContractSnapshotID=@ContractSnapshotID
	BEGIN TRY
		--�汾1����û����ʷ�汾�����Ժ�ͬû�б��
		IF @Version <= 1
			SET @LastContractSnapshotID = NULL
		
		--��@Version��1��Ȼ���Ը����������һ���汾�ĺ�ͬContractSnapshotID
		SET @Version -= 1
		SELECT DISTINCT @LastContractSnapshotID=ContractSnapshotID FROM Contract
		WHERE ContractID=@ContractID AND Version=@Version
	END TRY
	BEGIN CATCH
		--���Version����ת��Ϊ���֣���ֱ�ӷ����ˣ�����û����ʷ�汾
		SET @LastContractSnapshotID = NULL
	END CATCH
	--��ȡ�ú�ͬ��һ��Version��ContractSnapshotID--End
END

GO


