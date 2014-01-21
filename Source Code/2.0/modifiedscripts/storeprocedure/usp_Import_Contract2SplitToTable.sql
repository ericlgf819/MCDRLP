USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Import_Contract2SplitToTable]    Script Date: 07/02/2012 15:53:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
























-- =============================================
-- Author:		zhangbsh
-- Create date:20110406
-- Description:	��ͬ����
-- =============================================
ALTER PROCEDURE [dbo].[usp_Import_Contract2SplitToTable]
	-- Add the parameters for the stored procedure here
	@ContractIndex NVARCHAR(50),
	@UserName NVARCHAR(50),
	@ContractSnapshotID NVARCHAR(50) OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
		SET NOCOUNT ON;

		--��������
		DECLARE @Import_Contract TABLE 
		([Excel�к�] [nvarchar] (500),[��ͬ���] [nvarchar] (500),[��ͬ��ע] [nvarchar] (500),
		[��˾���] [nvarchar] (500),[��˾����] [nvarchar] (500),[��˾��ע] [nvarchar] (500),
		[ҵ�����] [nvarchar] (500),[ҵ������] [nvarchar] (500),[ʵ������] [nvarchar] (500),
		[ʵ������] [nvarchar] (500),[�������ű��] [nvarchar] (500),[Kiosk���] [nvarchar] (500),
		[��ҵ��] [nvarchar] (500),[������] [nvarchar] (500),[���޵�����] [nvarchar] (500),
		[�Ƿ��AP] [nvarchar] (500),[��AP����] [nvarchar] (500),[�ز���Ԥ����Sales] [nvarchar] (500),
		[��֤��] [nvarchar] (500),[˰��] [nvarchar] (500),[��֤��ע] [nvarchar] (500),
		[�������] [nvarchar] (500),[�Ƿ񴿰ٷֱ�] [nvarchar](50) NULL,
		[ժҪ] [nvarchar] (500),[��ע] [nvarchar] (500),
		[ֱ����������ʼ��] [nvarchar] (500),[ֱ�߳���] [nvarchar](500) NULL,
		[֧������] [nvarchar] (500),[��������] [nvarchar] (500),
		[���޹���] [nvarchar] (500),[�״�DueDate] [nvarchar] (500),[ʱ��ο�ʼ] [nvarchar] (500),
		[ʱ��ν���] [nvarchar] (500),[����] [nvarchar] (500),[��ʽ] [nvarchar] (500),
		[SQL����] [nvarchar] (500),[SQL��ʽ] [nvarchar] (500),
		[�������ִ�] NVARCHAR(500) NULL, [��ʽ���ִ�] NVARCHAR(500) NULL,IsVirtual BIT)

		DECLARE  @Contract TABLE--��ͬ
		([ContractSnapshotID] [dbo].[ObjID] NOT NULL,
		[CompanyCode] [nvarchar] (32)  NULL,
		[ContractID] [dbo].[ObjID] NOT NULL,[ContractNO] [dbo].[ObjName] NULL,
		[Version] [nvarchar] (50)  NULL,
		[ContractName] [dbo].[ObjName] NULL,[CompanyName] [dbo].[ObjName] NULL,
		[CompanySimpleName] [nvarchar] (50)  NULL,
		[CompanyRemark] [nvarchar] (2000)  NULL,
		[Status] [nvarchar] (50)  NULL,
		[Remark] [nvarchar] (2000)  NULL,
		[UpdateInfo] [nvarchar] (2000)  NULL,
		[IsLocked] [bit] NULL,[CreateTime] [datetime] NULL,
		[CreatorName] [nvarchar] (50)  NULL,
		[LastModifyTime] [datetime] NULL,
		[LastModifyUserName] [nvarchar] (50)  NULL,
		[SnapshotCreateTime] [datetime] NULL,[IsSave] [bit] NULL,
		[PartComment] [dbo].[TString_1000] NULL,
		[FromSRLS] BIT NOT NULL DEFAULT 1) --Added by Eric

		DECLARE @VendorContract TABLE--ҵ����ͬ��ϵ
		([VendorContractID] [dbo].[ObjID] NOT NULL,[ContractSnapshotID] [dbo].[ObjID] NULL,
		[VendorNo] [dbo].[ObjID] NOT NULL,[VendorName] [nvarchar] (200)  NULL,
		[PayMentType] [nvarchar] (50)  NULL,[IsVirtual] [bit] NOT NULL)

		DECLARE @Entity TABLE--ʵ��
		([EntityID] [dbo].[ObjID] NOT NULL,[EntityTypeName] [dbo].[ObjID] NOT NULL,
		[ContractSnapshotID] [dbo].[ObjID] NOT NULL,[EntityNo] [dbo].[ObjID] NULL,
		[EntityName] [dbo].[ObjName] NOT NULL,[StoreOrDept] [nvarchar] (50)  NULL,
		[StoreOrDeptNo] [nvarchar] (50)  NULL,[KioskNo] [nvarchar] (50)  NULL,
		[OpeningDate] [datetime] NULL,[RentStartDate] [datetime] NULL,
		[RentEndDate] [datetime] NULL,[IsCalculateAP] [bit] NULL,
		[APStartDate] [datetime] NULL,[Remark] [nvarchar] (2000)  NULL)

		DECLARE @VendorEntity TABLE--ҵ��ʵ���ϵ
		([VendorEntityID] [dbo].[ObjID] NOT NULL,
		[EntityID] [dbo].[ObjID] NULL,[VendorNo] [dbo].[ObjID] NOT NULL)

		DECLARE @EntityInfoSetting TABLE--ʵ����Ϣ������
		([EntityInfoSettingID] [dbo].[ObjID] NOT NULL,
		[EntityID] [dbo].[ObjID] NOT NULL,[VendorNo] [dbo].[ObjID] NOT NULL,
		[RealestateSales] [decimal] (18, 2) NULL,[MarginAmount] [decimal] (18, 2) NULL,
		[MarginRemark] [nvarchar] (2000)  NULL,[TaxRate] [decimal] (12, 8) NULL)

		DECLARE @FixedRuleSetting TABLE--�̶�����
		([RuleSnapshotID] [dbo].[ObjID] NOT NULL,[EntityInfoSettingID] [dbo].[ObjID] NULL,
		[RuleID] [dbo].[ObjID] NOT NULL,[RentType] [dbo].[ObjID] NOT NULL,
		[FirstDueDate] [datetime] NULL,[NextDueDate] [datetime] NULL,
		[NextAPStartDate] [datetime] NULL,[NextAPEndDate] [datetime] NULL,
		[NextGLStartDate] [datetime] NULL,[NextGLEndDate] [datetime] NULL,
		[PayType] [dbo].[ObjName] NULL,[ZXStartDate] [datetime] NULL,
		[ZXConstant] [decimal] (18, 2) NULL,[Cycle] [dbo].[ObjName] NULL,
		[CycleMonthCount] [int] NULL,[Calendar] [dbo].[ObjName] NULL,
		[Description] [nvarchar] (2000)  NULL,[Remark] [nvarchar] (2000)  NULL,
		[SnapshotCreateTime] [datetime] NULL)

		DECLARE @FixedTimeIntervalSetting TABLE--�̶����ʱ�������
		([TimeIntervalID] [dbo].[ObjID] NOT NULL,[RuleSnapshotID] [dbo].[ObjID] NULL,
		[StartDate] [datetime] NULL,[EndDate] [datetime] NULL,
		[Amount] [decimal] (18, 2) NULL,[Cycle] [dbo].[ObjName] NULL,
		[CycleMonthCount] [int] NULL,[Calendar] [dbo].[ObjName] NULL)

		DECLARE @RatioRuleSetting TABLE--�ٷֱȹ���
		([RatioID] [dbo].[ObjID] NOT NULL,[EntityInfoSettingID] [dbo].[ObjID] NULL,
		[RentType] [dbo].[ObjID] NOT NULL,[Description] [nvarchar] (2000) NULL,
		[Remark] [nvarchar] (2000) NULL)

		DECLARE @RatioCycleSetting TABLE--�ٷֱ�����
		([RatioID] [dbo].[ObjID] NULL,[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
		[RuleID] [dbo].[ObjID] NOT NULL,[IsPure] [bit] NULL,[FirstDueDate] [datetime] NULL,
		[NextDueDate] [datetime] NULL,[NextAPStartDate] [datetime] NULL,
		[NextAPEndDate] [datetime] NULL,[NextGLStartDate] [datetime] NULL,
		[NextGLEndDate] [datetime] NULL,[PayType] [dbo].[ObjName] NULL,
		[ZXStartDate] [datetime] NULL,[Cycle] [dbo].[ObjName] NULL,
		[CycleMonthCount] [int] NULL,[Calendar] [dbo].[ObjName] NULL,
		[CycleType] [nvarchar] (50) NULL,[SnapshotCreateTime] [datetime] NULL)

		DECLARE @RatioTimeIntervalSetting TABLE--�ٷֱ�ʱ�������
		([TimeIntervalID] [dbo].[ObjID] NOT NULL,[RuleSnapshotID] [dbo].[ObjID] NULL,
		[StartDate] [datetime] NULL,[EndDate] [datetime] NULL)

		DECLARE @ConditionAmount TABLE--ʱ����������
		([ConditionID] [dbo].[ObjID] NOT NULL,[TimeIntervalID] [dbo].[ObjID] NULL,
		[ConditionDesc] [nvarchar] (1000) NULL,[AmountFormulaDesc] [nvarchar] (1000) NULL,
		[SQLCondition] [nvarchar] (1000) NULL,[SQLAmountFormula] [nvarchar] (1000) NULL,
		ConditionNumberValue NVARCHAR(500) NULL, FormulaNumberValue NVARCHAR(500) NULL)



		SELECT @ContractSnapshotID = NEWID()
		
		INSERT INTO @Import_Contract--��ȡ��һ�ݺ�ͬ��¼
		SELECT ic.[Excel�к�],ic.[��ͬ���],ic.[��ͬ��ע] ,
			ic.[��˾���],ic.[��˾����],ic.[��˾��ע],
			x.ҵ�����,--ic.[ҵ�����],
			ic.[ҵ������],ic.[ʵ������],
			ic.[ʵ������],ic.[�������ű��],ic.[Kiosk���],
			ic.[��ҵ��],ic.[������],ic.[���޵�����],
			ic.[�Ƿ��AP],ic.[��AP����],ic.[�ز���Ԥ����Sales],
			ic.[��֤��],ic.[˰��],ic.[��֤��ע],
			ic.[�������],ic.[�Ƿ񴿰ٷֱ�],
			ic.[ժҪ],ic.[��ע],
			ic.[ֱ����������ʼ��],ic.[ֱ�߳���],
			ic.[֧������],ic.[��������],
			ic.[���޹���],ic.[�״�DueDate],ic.[ʱ��ο�ʼ],
			ic.[ʱ��ν���],ic.[����],ic.[��ʽ],
			ic.[SQL����],ic.[SQL��ʽ],ic.[�������ִ�],ic.[��ʽ���ִ�],
			CASE WHEN ic.ҵ����� IS NULL THEN 1 ELSE 0 END
		FROM dbo.Import_Contract AS ic
		LEFT JOIN 
		(SELECT  DISTINCT ic.ҵ������,ic.ҵ�����
				FROM dbo.Import_Contract AS ic
				WHERE ic.��ͬ��� = @ContractIndex
		) x
			ON ic.ҵ������ = x.ҵ������
		WHERE ic.��ͬ��� = @ContractIndex		
		
		
		UPDATE x SET x.ҵ�����=y.ҵ�����
		FROM @Import_Contract x INNER JOIN 
		(
			SELECT ҵ������,MAX(ҵ�����) AS ҵ����� FROM 
			(
			SELECT  DISTINCT ic.ҵ������,ISNULL(ic.ҵ�����,NEWID()) AS ҵ�����
					FROM dbo.Import_Contract AS ic
					WHERE ic.��ͬ��� = @ContractIndex
					) temp GROUP BY ҵ������
		) y  ON x.ҵ������=y.ҵ������
		WHERE x.ҵ����� IS NULL 
		
		

		
		INSERT INTO @Contract--��ͬOK
		SELECT TOP 1 @ContractSnapshotID,ic.��˾���,NEWID(),NULL,1,
			NULL,ic.��˾����,c.SimpleName,ic.��˾��ע,
			'�ݸ�',ic.��ͬ��ע,NULL,0,GETDATE(),
			@UserName,GETDATE(),@UserName,
			NULL,1,'����',0 --Added by Eric
		FROM @Import_Contract AS ic
		LEFT JOIN dbo.SRLS_TB_Master_Company c
			ON ic.��˾���=c.CompanyCode
		--Added by Eric
		--�����µĺ�ͬ��ˮ�ţ�����״̬����Ϊ'����Ч'--Begin
		--SET @ContractNO = dbo.fn_SerialNumber_Contract(@CompanyCode)
		UPDATE @Contract SET Status='����Ч', ContractNO = dbo.fn_SerialNumber_Contract(CompanyCode) WHERE ContractSnapshotID = @ContractSnapshotID
		--�����µĺ�ͬ��ˮ�ţ�����״̬����Ϊ'����Ч'--End

--		WHERE ic.Excel�к� = @ExcelIndex
		
--		SELECT * FROM @Contract
		
		DECLARE @VirtualVendorNo NVARCHAR(50)
		SET @VirtualVendorNo = CAST(NEWID() AS NVARCHAR(50))
		INSERT INTO @VendorContract--ҵ����ͬ��ϵOK
		SELECT NEWID(),@ContractSnapshotID,
			ic.ҵ�����,ic.ҵ������,stmv.PayMentType,ic.IsVirtual
		FROM (SELECT  DISTINCT ic.ҵ�����,ic.ҵ������,ic.IsVirtual
			FROM @Import_Contract AS ic) ic
		LEFT JOIN dbo.SRLS_TB_Master_Vendor AS stmv	ON stmv.VendorNo = ic.ҵ�����
			
--		SELECT * FROM  @VendorContract
		
		INSERT INTO @Entity--ʵ��OK
		SELECT NEWID(),ic.ʵ������,@ContractSnapshotID,
			NULL,ic.ʵ������,stms.StoreName,ic.�������ű��,
			ic.KIOSK���,ic.��ҵ��,ic.������,ic.���޵�����,
			CASE WHEN ic.�Ƿ��AP = '��' THEN 1 ELSE 0 END,
			ic.��AP����,NULL
		FROM (SELECT DISTINCT ic.�������ű��,ic.ʵ������,ic.ʵ������,
			ic.KIOSK���,ic.��ҵ��,ic.������,ic.���޵�����,
			ic.�Ƿ��AP,ic.��AP����
			FROM @Import_Contract AS ic) ic
		LEFT JOIN 
		(
			SELECT StoreNo,StoreName FROM dbo.SRLS_TB_Master_Store
			UNION ALL 
			SELECT DeptCode,DeptName FROM dbo.SRLS_TB_Master_Department
		) AS stms ON stms.StoreNo = ic.�������ű��
		
		--SELECT * FROM  @Entity

		--ҵ��ʵ���ϵOK
		INSERT INTO @VendorEntity
		SELECT NEWID(), e.EntityID,ic.ҵ�����
		FROM @Import_Contract AS ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.ʵ������
		GROUP BY e.EntityID,ic.ҵ�����
			
		--SELECT * FROM  @VendorEntity	

		--ʵ����Ϣ������OK
		INSERT INTO @EntityInfoSetting
		SELECT NEWID(),e.EntityID,ic.ҵ�����,
			ic.�ز���Ԥ����SALES,ic.��֤��,ic.��֤��ע,ic.˰��
		FROM @Entity AS e
		INNER JOIN @Import_Contract AS ic ON e.EntityName = ic.ʵ������
		GROUP BY e.EntityID, ic.ҵ�����,
			ic.�ز���Ԥ����SALES,ic.��֤��,ic.��֤��ע,ic.˰��
		
		--SELECT * FROM @EntityInfoSetting

		--�̶�����OK
		INSERT INTO @FixedRuleSetting
		SELECT NEWID(),eis.EntityInfoSettingID,NEWID(),ic.�������,
			ic.�״�DueDate,ic.�״�DueDate,--NextDueDate���״�DueDateһ��
			NULL,NULL,NULL,NULL,--δ����NextAPStartDate,NextAPEndDate,NextGLStartDate,NextGLEndDate
			ic.֧������,ic.ֱ����������ʼ��,
			ic.ֱ�߳���,ic.��������,ci.CycleMonthCount,ic.���޹���,ic.ժҪ,
			ic.��ע,NULL
		FROM @Import_Contract AS ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.ʵ������
		LEFT JOIN @EntityInfoSetting AS eis ON eis.EntityID = e.EntityID AND ic.ҵ����� = eis.VendorNo
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.��������
		WHERE ic.������� LIKE '%�̶�%'	AND ci.CycleType = '�̶�'
		GROUP BY eis.EntityInfoSettingID,ic.�������,ic.�״�DueDate,
			ic.֧������,ic.ֱ����������ʼ��,ic.ֱ�߳���,--ic.������,ic.���޵�����,
			ic.��������,ci.CycleMonthCount,ic.���޹���,ic.ժҪ,ic.��ע
		
		--SELECT * FROM  @FixedRuleSetting
			
		--�̶����ʱ�������OK
		INSERT INTO @FixedTimeIntervalSetting 
		SELECT NEWID(), frs.RuleSnapshotID,ic.ʱ��ο�ʼ,ic.ʱ��ν���,
			ic.��ʽ,ic.��������,ci.CycleMonthCount,ic.���޹���
		FROM @Import_Contract AS ic
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.��������
		LEFT JOIN @Entity e ON e.EntityName = ic.ʵ������
		LEFT JOIN @EntityInfoSetting eis ON eis.EntityID = e.EntityID
		LEFT JOIN @FixedRuleSetting AS frs ON ic.������� = frs.RentType 
			AND ISNULL(ic.ժҪ,'')=ISNULL(frs.Description,'')
			AND ISNULL(ic.��ע,'') = ISNULL(frs.Remark,'')
			AND eis.EntityInfoSettingID = frs.EntityInfoSettingID
		WHERE ic.������� LIKE '%�̶�%'
		GROUP BY frs.RuleSnapshotID,ic.ʱ��ο�ʼ,ic.ʱ��ν���,
			ic.��ʽ,ic.��������,ci.CycleMonthCount,ic.���޹���
			
--		SELECT * FROM  @FixedTimeIntervalSetting

--		--�ٷֱȹ���OK
		INSERT INTO @RatioRuleSetting
		SELECT NEWID(),x.EntityInfoSettingID,x.RentType,x.ժҪ,x.��ע
		FROM 
		(SELECT eis.EntityInfoSettingID,
			SUBSTRING(ic.�������,1,LEN(ic.�������)-3) AS RentType,
			ic.ժҪ,ic.��ע
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.ʵ������
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID AND ic.ҵ����� = eis.VendorNo
		WHERE ic.������� LIKE '%�ٷֱ�%') x
		GROUP BY x.EntityInfoSettingID,x.RentType,x.ժҪ,x.��ע
		
		--SELECT * FROM @RatioRuleSetting
		
--		--�ٷֱ�����
		INSERT INTO @RatioCycleSetting 
		SELECT rrs.RatioID,NEWID(),NEWID(),
			CASE WHEN ic.�Ƿ񴿰ٷֱ� = '��' THEN 1 ELSE 0 END,
			ic.�״�DueDate,
			NULL,NULL,NULL,NULL,NULL,--????δ����NextDueDate,NextAPStartDate,NextAPEndDate,NextGLStartDate,NextGLEndDate
			ic.֧������,ic.ֱ����������ʼ��,
			ic.��������,ci.CycleMonthCount,ic.���޹���,RIGHT(ic.�������,3),NULL
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.ʵ������
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.��������
		WHERE ic.������� LIKE '%�ٷֱ�%' AND ci.CycleType = '�ٷֱ�'
				 AND rrs.RentType+RIGHT(ic.�������,3)=ic.�������
		GROUP BY rrs.RatioID,ic.�Ƿ񴿰ٷֱ�,ic.�״�DueDate,
			ic.֧������,ic.ֱ����������ʼ��,
			ic.��������,ci.CycleMonthCount,ic.���޹���,ic.�������

		--SELECT * FROM @RatioCycleSetting
		

--		--�ٷֱ�ʱ�������OK
		INSERT INTO @RatioTimeIntervalSetting
		SELECT NEWID(),rcs.RuleSnapshotID,ic.ʱ��ο�ʼ,ic.ʱ��ν���
		FROM @Import_Contract ic 
		LEFT JOIN @VendorContract v ON ic.ҵ�����=v.VendorNo
		LEFT JOIN @Entity AS e ON e.EntityName = ic.ʵ������ 
		LEFT JOIN @VendorEntity ve ON e.EntityID=ve.EntityID AND v.VendorNo=ve.VendorNo
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN @RatioCycleSetting rcs ON rrs.RatioID = rcs.RatioID
		WHERE rrs.RentType LIKE '%�ٷֱ�%' AND rrs.RentType+rcs.CycleType=ic.�������
		GROUP BY rcs.RuleSnapshotID,ic.ʱ��ο�ʼ,ic.ʱ��ν���
		ORDER BY rcs.RuleSnapshotID,ic.ʱ��ο�ʼ
		--SELECT * FROM @RatioTimeIntervalSetting
		
--		--ʱ����������
		INSERT INTO @ConditionAmount
		SELECT NEWID(),rtis.TimeIntervalID,ic.����,
			ic.��ʽ,ic.SQL����,ic.SQL��ʽ,ic.�������ִ�,ic.��ʽ���ִ�
			--,rtis.StartDate,rtis.EndDate,rrs.RentType,rcs.CycleType
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e	ON e.EntityName = ic.ʵ������
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN @RatioCycleSetting rcs 
			ON rrs.RatioID = rcs.RatioID
				AND rcs.CycleType = RIGHT(ic.�������,3)
		LEFT JOIN @RatioTimeIntervalSetting rtis
			ON	rcs.RuleSnapshotID = rtis.RuleSnapshotID
				AND rtis.StartDate = ic.ʱ��ο�ʼ AND rtis.EndDate = ic.ʱ��ν���
				AND ic.������� = rrs.RentType + rcs.CycleType
		WHERE ic.������� LIKE '%�ٷֱ�%'
		GROUP BY rtis.TimeIntervalID,ic.����,ic.��ʽ,ic.SQL����,ic.SQL��ʽ,ic.�������ִ�,ic.��ʽ���ִ�
		--SELECT * FROM  @ConditionAmount
		
--		����ʵ�ʱ���
		INSERT INTO dbo.[Contract] SELECT * FROM @Contract
		INSERT INTO dbo.VendorContract SELECT * FROM @VendorContract
		INSERT INTO dbo.Entity SELECT * FROM @Entity
		INSERT INTO VendorEntity SELECT * FROM @VendorEntity
		INSERT INTO EntityInfoSetting SELECT * FROM @EntityInfoSetting
		INSERT INTO FixedRuleSetting SELECT * FROM @FixedRuleSetting
		INSERT INTO FixedTimeIntervalSetting SELECT * FROM @FixedTimeIntervalSetting
		INSERT INTO RatioRuleSetting SELECT * FROM @RatioRuleSetting
		INSERT INTO RatioCycleSetting SELECT * FROM @RatioCycleSetting
		INSERT INTO RatioTimeIntervalSetting SELECT * FROM @RatioTimeIntervalSetting
		INSERT INTO ConditionAmount 
		SELECT ConditionID,TimeIntervalID,ConditionDesc,AmountFormulaDesc,
				SQLCondition,SQLAmountFormula FROM @ConditionAmount	
				
		--����˴������͹�ʽ����ȡ���ֵı���Ҫ�������²���
		DECLARE @ConditionID NVARCHAR(50),@ConditionNumberValue NVARCHAR(500),@FormulaNumberValue NVARCHAR(500)
		DECLARE @MyCursor CURSOR
		SET  @MyCursor = CURSOR  SCROLL FOR
		SELECT ConditionID,ConditionNumberValue,FormulaNumberValue FROM @ConditionAmount
		OPEN @MyCursor
		FETCH NEXT FROM @MyCursor INTO @ConditionID,@ConditionNumberValue,@FormulaNumberValue
		WHILE @@FETCH_STATUS = 0
		BEGIN
			--ɾ��֮ǰ������
			DELETE FROM dbo.ConditionAmountNumbers
			WHERE ConditionID = @ConditionID			
			
			--��������ֵ
			INSERT INTO dbo.ConditionAmountNumbers (NumberValue,NumberID,ConditionID,NumberType)
			SELECT DISTINCT v.TokenVal,NEWID(),@ConditionID,'����' 
			FROM dbo.fn_Helper_Split(@ConditionNumberValue,';') AS v
			
			--���빫ʽֵ
			INSERT INTO dbo.ConditionAmountNumbers (NumberValue,NumberID,ConditionID,NumberType)
			SELECT DISTINCT v.TokenVal,NEWID(),@ConditionID,'��ʽ' 
			FROM dbo.fn_Helper_Split(@FormulaNumberValue,';') AS v
			
			FETCH NEXT FROM @MyCursor INTO @ConditionID,@ConditionNumberValue,@FormulaNumberValue
		END

		CLOSE @MyCursor
		DEALLOCATE	@MyCursor
		
	
    
END
























GO


