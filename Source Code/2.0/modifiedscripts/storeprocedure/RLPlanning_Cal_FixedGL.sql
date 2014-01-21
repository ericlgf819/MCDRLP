USE [RLP]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_FixedGL]    Script Date: 09/07/2012 17:00:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------
Copyright(c) 2012 VIT��

����������
������ʶ��Create By 
 
�޸ı�ʶ��
�޸�������
------------------------------------------------------*/
CREATE PROCEDURE [dbo].[RLPlanning_Cal_FixedGL]
@RuleSnapShotID nvarchar(50),
@CalEndDate datetime --Ԥ�����ʱ��
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --BEGIN TRANSACTION MYTRANSACTION
    BEGIN TRY
		--�Ƚ�֮ǰ�������snapshot��glrecord��ɾ��
		EXEC dbo.RLPlanning_Cal_DeleteGLRecordByRuleSnapShotID @RuleSnapShotID

		--��ʼ��NextGLStartDate��NextGLEndDate
		EXEC dbo.usp_Cal_UpdateFixedAPGLNextStartEndDate @RuleSnapshotID

		--GL��¼����
		DECLARE @TypeCodeSnapshotID NVARCHAR(50),@TypeCodeName NVARCHAR(50)
		DECLARE @AccountingCycle DATETIME
		SELECT @AccountingCycle = dbo.fn_Cal_GetNextGLCreateDate(@RuleSnapshotID)
			
		SELECT @TypeCodeSnapshotID=TC.TypeCodeSnapshotID, @TypeCodeName=TC.TypeCodeName
		FROM  (SELECT * FROM  dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) AS FR INNER JOIN
		dbo.EntityInfoSetting AS EI ON FR.EntityInfoSettingID = EI.EntityInfoSettingID INNER JOIN
		dbo.Entity AS E ON EI.EntityID = E.EntityID INNER JOIN
		(SELECT * FROM dbo.TypeCode WHERE  SnapshotCreateTime IS NULL ) AS TC ON E.EntityTypeName = TC.EntityTypeName AND FR.RentType = TC.RentTypeName

		
		--NextGLStartDate�����Ԥ�����ʱ��֮�⣬��ѭ����GL
		DECLARE @NextGLStartDate DATETIME
		SELECT @NextGLStartDate=NextGLStartDate FROM FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapShotID

		WHILE @NextGLStartDate IS NOT NULL AND @NextGLStartDate < @CalEndDate
		BEGIN
			--��ʼ����GL
			DECLARE @GLRecordID NVARCHAR(50)

			SELECT @GLRecordID=NEWID()
	
			--����GL��¼  
			INSERT INTO dbo.GLRecord 
				(GLRecordID,GLType,TypeCodeSnapshotID,TypeCodeName,ContractSnapshotID,ContractNO,CompanyCode,CompanyName,
				VendorNo,VendorName,EntityID,EntityTypeName,EntityInfoSettingID,RuleSnapshotID,RuleID,RentType,
				AccountingCycle,IsCalculateSuccess,HasMessage,IsChange,CycleStartDate,CycleEndDate,PayMentType,
				StoreEstimateSales,KioskEstimateSales,GLAmount,CreatedCertificateTime,
				IsCASHSHEETClosed,IsAjustment,CreateTime,UpdateTime,FromSRLS)
			SELECT @GLRecordID,'�̶����' AS GLType,@TypeCodeSnapshotID,@TypeCodeName,C.ContractSnapshotID,C.ContractNO,C.CompanyCode,C.CompanyName,
				EI.VendorNo,VC.VendorName,EI.EntityID,E.EntityTypeName,EI.EntityInfoSettingID,FR.RuleSnapshotID,FR.RuleID,FR.RentType,
				@AccountingCycle,0 AS IsCalculateSuccess,0 AS HasMessage,0 AS IsChange,FR.NextGLStartDate AS CycleStartDate,FR.NextGLEndDate AS CycleEndDate,VC.PayMentType,
				0 AS StoreEstimateSales,0 AS KioskEstimateSales,0 AS GLAmount,NULL AS CreatedCertificateTime,
				0 AS IsCASHSHEETClosed,0 AS IsAjustment,GETDATE() AS CreateTime,GETDATE() AS UpdateTime, 0 AS FromSRLS
			FROM  dbo.[Contract] AS C INNER JOIN
				  dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID INNER JOIN
				  dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID INNER JOIN
				  dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID AND VC.VendorNo = VE.VendorNo INNER JOIN
				  dbo.EntityInfoSetting AS EI ON E.EntityID = EI.EntityID AND VE.VendorNo = EI.VendorNo INNER JOIN
				  (SELECT * FROM  dbo.FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapshotID) AS FR ON EI.EntityInfoSettingID = FR.EntityInfoSettingID
				 
			--��ʼ����
			EXEC dbo.usp_Cal_CalFixedGL @GLRecordID

			--ÿ�μ�����һ��ʱ��ε�GL�󶼸�����NextGLStartDate��NextGLEndDate
			EXEC dbo.usp_Cal_UpdateFixedAPGLNextStartEndDate @RuleSnapshotID
			SELECT @NextGLStartDate=NextGLStartDate FROM FixedRuleSetting WHERE RuleSnapshotID=@RuleSnapShotID
		END
		
		
		--IF @@TRANCOUNT <> 0
		--	COMMIT TRANSACTION MYTRANSACTION
    END TRY
    BEGIN CATCH
		--IF @@TRANCOUNT <> 0
		--	ROLLBACK TRANSACTION;
		DECLARE @MSG NVARCHAR(max)
		SELECT @MSG=ERROR_MESSAGE()
		--д��־
		INSERT INTO dbo.SRLS_TB_System_Log 
		(ID,LogType,LogSource,LogTitle,LogMessage,LogTime,UserID,EnglishName) 
		VALUES (NEWID(),'�����̶�GL���̳���',@RuleSnapshotID,NULL,@MSG,GETDATE(),NULL,NULL)
    END CATCH
END


GO


