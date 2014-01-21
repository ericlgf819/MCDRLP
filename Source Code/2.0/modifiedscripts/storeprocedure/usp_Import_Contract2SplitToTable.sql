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
-- Description:	合同导入
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

		--定义表变量
		DECLARE @Import_Contract TABLE 
		([Excel行号] [nvarchar] (500),[合同序号] [nvarchar] (500),[合同备注] [nvarchar] (500),
		[公司编号] [nvarchar] (500),[公司名称] [nvarchar] (500),[公司备注] [nvarchar] (500),
		[业主编号] [nvarchar] (500),[业主名称] [nvarchar] (500),[实体类型] [nvarchar] (500),
		[实体名称] [nvarchar] (500),[餐厅部门编号] [nvarchar] (500),[Kiosk编号] [nvarchar] (500),
		[开业日] [nvarchar] (500),[起租日] [nvarchar] (500),[租赁到期日] [nvarchar] (500),
		[是否出AP] [nvarchar] (500),[出AP日期] [nvarchar] (500),[地产部预估年Sales] [nvarchar] (500),
		[保证金] [nvarchar] (500),[税率] [nvarchar] (500),[保证金备注] [nvarchar] (500),
		[租金类型] [nvarchar] (500),[是否纯百分比] [nvarchar](50) NULL,
		[摘要] [nvarchar] (500),[备注] [nvarchar] (500),
		[直线租金计算起始日] [nvarchar] (500),[直线常数] [nvarchar](500) NULL,
		[支付类型] [nvarchar] (500),[结算周期] [nvarchar] (500),
		[租赁公历] [nvarchar] (500),[首次DueDate] [nvarchar] (500),[时间段开始] [nvarchar] (500),
		[时间段结束] [nvarchar] (500),[条件] [nvarchar] (500),[公式] [nvarchar] (500),
		[SQL条件] [nvarchar] (500),[SQL公式] [nvarchar] (500),
		[条件数字串] NVARCHAR(500) NULL, [公式数字串] NVARCHAR(500) NULL,IsVirtual BIT)

		DECLARE  @Contract TABLE--合同
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

		DECLARE @VendorContract TABLE--业主合同关系
		([VendorContractID] [dbo].[ObjID] NOT NULL,[ContractSnapshotID] [dbo].[ObjID] NULL,
		[VendorNo] [dbo].[ObjID] NOT NULL,[VendorName] [nvarchar] (200)  NULL,
		[PayMentType] [nvarchar] (50)  NULL,[IsVirtual] [bit] NOT NULL)

		DECLARE @Entity TABLE--实体
		([EntityID] [dbo].[ObjID] NOT NULL,[EntityTypeName] [dbo].[ObjID] NOT NULL,
		[ContractSnapshotID] [dbo].[ObjID] NOT NULL,[EntityNo] [dbo].[ObjID] NULL,
		[EntityName] [dbo].[ObjName] NOT NULL,[StoreOrDept] [nvarchar] (50)  NULL,
		[StoreOrDeptNo] [nvarchar] (50)  NULL,[KioskNo] [nvarchar] (50)  NULL,
		[OpeningDate] [datetime] NULL,[RentStartDate] [datetime] NULL,
		[RentEndDate] [datetime] NULL,[IsCalculateAP] [bit] NULL,
		[APStartDate] [datetime] NULL,[Remark] [nvarchar] (2000)  NULL)

		DECLARE @VendorEntity TABLE--业主实体关系
		([VendorEntityID] [dbo].[ObjID] NOT NULL,
		[EntityID] [dbo].[ObjID] NULL,[VendorNo] [dbo].[ObjID] NOT NULL)

		DECLARE @EntityInfoSetting TABLE--实体信息设置区
		([EntityInfoSettingID] [dbo].[ObjID] NOT NULL,
		[EntityID] [dbo].[ObjID] NOT NULL,[VendorNo] [dbo].[ObjID] NOT NULL,
		[RealestateSales] [decimal] (18, 2) NULL,[MarginAmount] [decimal] (18, 2) NULL,
		[MarginRemark] [nvarchar] (2000)  NULL,[TaxRate] [decimal] (12, 8) NULL)

		DECLARE @FixedRuleSetting TABLE--固定规则
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

		DECLARE @FixedTimeIntervalSetting TABLE--固定租金时间段设置
		([TimeIntervalID] [dbo].[ObjID] NOT NULL,[RuleSnapshotID] [dbo].[ObjID] NULL,
		[StartDate] [datetime] NULL,[EndDate] [datetime] NULL,
		[Amount] [decimal] (18, 2) NULL,[Cycle] [dbo].[ObjName] NULL,
		[CycleMonthCount] [int] NULL,[Calendar] [dbo].[ObjName] NULL)

		DECLARE @RatioRuleSetting TABLE--百分比规则
		([RatioID] [dbo].[ObjID] NOT NULL,[EntityInfoSettingID] [dbo].[ObjID] NULL,
		[RentType] [dbo].[ObjID] NOT NULL,[Description] [nvarchar] (2000) NULL,
		[Remark] [nvarchar] (2000) NULL)

		DECLARE @RatioCycleSetting TABLE--百分比周期
		([RatioID] [dbo].[ObjID] NULL,[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
		[RuleID] [dbo].[ObjID] NOT NULL,[IsPure] [bit] NULL,[FirstDueDate] [datetime] NULL,
		[NextDueDate] [datetime] NULL,[NextAPStartDate] [datetime] NULL,
		[NextAPEndDate] [datetime] NULL,[NextGLStartDate] [datetime] NULL,
		[NextGLEndDate] [datetime] NULL,[PayType] [dbo].[ObjName] NULL,
		[ZXStartDate] [datetime] NULL,[Cycle] [dbo].[ObjName] NULL,
		[CycleMonthCount] [int] NULL,[Calendar] [dbo].[ObjName] NULL,
		[CycleType] [nvarchar] (50) NULL,[SnapshotCreateTime] [datetime] NULL)

		DECLARE @RatioTimeIntervalSetting TABLE--百分比时间段设置
		([TimeIntervalID] [dbo].[ObjID] NOT NULL,[RuleSnapshotID] [dbo].[ObjID] NULL,
		[StartDate] [datetime] NULL,[EndDate] [datetime] NULL)

		DECLARE @ConditionAmount TABLE--时间段条件金额
		([ConditionID] [dbo].[ObjID] NOT NULL,[TimeIntervalID] [dbo].[ObjID] NULL,
		[ConditionDesc] [nvarchar] (1000) NULL,[AmountFormulaDesc] [nvarchar] (1000) NULL,
		[SQLCondition] [nvarchar] (1000) NULL,[SQLAmountFormula] [nvarchar] (1000) NULL,
		ConditionNumberValue NVARCHAR(500) NULL, FormulaNumberValue NVARCHAR(500) NULL)



		SELECT @ContractSnapshotID = NEWID()
		
		INSERT INTO @Import_Contract--提取出一份合同记录
		SELECT ic.[Excel行号],ic.[合同序号],ic.[合同备注] ,
			ic.[公司编号],ic.[公司名称],ic.[公司备注],
			x.业主编号,--ic.[业主编号],
			ic.[业主名称],ic.[实体类型],
			ic.[实体名称],ic.[餐厅部门编号],ic.[Kiosk编号],
			ic.[开业日],ic.[起租日],ic.[租赁到期日],
			ic.[是否出AP],ic.[出AP日期],ic.[地产部预估年Sales],
			ic.[保证金],ic.[税率],ic.[保证金备注],
			ic.[租金类型],ic.[是否纯百分比],
			ic.[摘要],ic.[备注],
			ic.[直线租金计算起始日],ic.[直线常数],
			ic.[支付类型],ic.[结算周期],
			ic.[租赁公历],ic.[首次DueDate],ic.[时间段开始],
			ic.[时间段结束],ic.[条件],ic.[公式],
			ic.[SQL条件],ic.[SQL公式],ic.[条件数字串],ic.[公式数字串],
			CASE WHEN ic.业主编号 IS NULL THEN 1 ELSE 0 END
		FROM dbo.Import_Contract AS ic
		LEFT JOIN 
		(SELECT  DISTINCT ic.业主名称,ic.业主编号
				FROM dbo.Import_Contract AS ic
				WHERE ic.合同序号 = @ContractIndex
		) x
			ON ic.业主名称 = x.业主名称
		WHERE ic.合同序号 = @ContractIndex		
		
		
		UPDATE x SET x.业主编号=y.业主编号
		FROM @Import_Contract x INNER JOIN 
		(
			SELECT 业主名称,MAX(业主编号) AS 业主编号 FROM 
			(
			SELECT  DISTINCT ic.业主名称,ISNULL(ic.业主编号,NEWID()) AS 业主编号
					FROM dbo.Import_Contract AS ic
					WHERE ic.合同序号 = @ContractIndex
					) temp GROUP BY 业主名称
		) y  ON x.业主名称=y.业主名称
		WHERE x.业主编号 IS NULL 
		
		

		
		INSERT INTO @Contract--合同OK
		SELECT TOP 1 @ContractSnapshotID,ic.公司编号,NEWID(),NULL,1,
			NULL,ic.公司名称,c.SimpleName,ic.公司备注,
			'草稿',ic.合同备注,NULL,0,GETDATE(),
			@UserName,GETDATE(),@UserName,
			NULL,1,'新增',0 --Added by Eric
		FROM @Import_Contract AS ic
		LEFT JOIN dbo.SRLS_TB_Master_Company c
			ON ic.公司编号=c.CompanyCode
		--Added by Eric
		--产生新的合同流水号，并将状态设置为'已生效'--Begin
		--SET @ContractNO = dbo.fn_SerialNumber_Contract(@CompanyCode)
		UPDATE @Contract SET Status='已生效', ContractNO = dbo.fn_SerialNumber_Contract(CompanyCode) WHERE ContractSnapshotID = @ContractSnapshotID
		--产生新的合同流水号，并将状态设置为'已生效'--End

--		WHERE ic.Excel行号 = @ExcelIndex
		
--		SELECT * FROM @Contract
		
		DECLARE @VirtualVendorNo NVARCHAR(50)
		SET @VirtualVendorNo = CAST(NEWID() AS NVARCHAR(50))
		INSERT INTO @VendorContract--业主合同关系OK
		SELECT NEWID(),@ContractSnapshotID,
			ic.业主编号,ic.业主名称,stmv.PayMentType,ic.IsVirtual
		FROM (SELECT  DISTINCT ic.业主编号,ic.业主名称,ic.IsVirtual
			FROM @Import_Contract AS ic) ic
		LEFT JOIN dbo.SRLS_TB_Master_Vendor AS stmv	ON stmv.VendorNo = ic.业主编号
			
--		SELECT * FROM  @VendorContract
		
		INSERT INTO @Entity--实体OK
		SELECT NEWID(),ic.实体类型,@ContractSnapshotID,
			NULL,ic.实体名称,stms.StoreName,ic.餐厅部门编号,
			ic.KIOSK编号,ic.开业日,ic.起租日,ic.租赁到期日,
			CASE WHEN ic.是否出AP = '是' THEN 1 ELSE 0 END,
			ic.出AP日期,NULL
		FROM (SELECT DISTINCT ic.餐厅部门编号,ic.实体类型,ic.实体名称,
			ic.KIOSK编号,ic.开业日,ic.起租日,ic.租赁到期日,
			ic.是否出AP,ic.出AP日期
			FROM @Import_Contract AS ic) ic
		LEFT JOIN 
		(
			SELECT StoreNo,StoreName FROM dbo.SRLS_TB_Master_Store
			UNION ALL 
			SELECT DeptCode,DeptName FROM dbo.SRLS_TB_Master_Department
		) AS stms ON stms.StoreNo = ic.餐厅部门编号
		
		--SELECT * FROM  @Entity

		--业主实体关系OK
		INSERT INTO @VendorEntity
		SELECT NEWID(), e.EntityID,ic.业主编号
		FROM @Import_Contract AS ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.实体名称
		GROUP BY e.EntityID,ic.业主编号
			
		--SELECT * FROM  @VendorEntity	

		--实体信息设置区OK
		INSERT INTO @EntityInfoSetting
		SELECT NEWID(),e.EntityID,ic.业主编号,
			ic.地产部预估年SALES,ic.保证金,ic.保证金备注,ic.税率
		FROM @Entity AS e
		INNER JOIN @Import_Contract AS ic ON e.EntityName = ic.实体名称
		GROUP BY e.EntityID, ic.业主编号,
			ic.地产部预估年SALES,ic.保证金,ic.保证金备注,ic.税率
		
		--SELECT * FROM @EntityInfoSetting

		--固定规则OK
		INSERT INTO @FixedRuleSetting
		SELECT NEWID(),eis.EntityInfoSettingID,NEWID(),ic.租金类型,
			ic.首次DueDate,ic.首次DueDate,--NextDueDate和首次DueDate一样
			NULL,NULL,NULL,NULL,--未插入NextAPStartDate,NextAPEndDate,NextGLStartDate,NextGLEndDate
			ic.支付类型,ic.直线租金计算起始日,
			ic.直线常数,ic.结算周期,ci.CycleMonthCount,ic.租赁公历,ic.摘要,
			ic.备注,NULL
		FROM @Import_Contract AS ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.实体名称
		LEFT JOIN @EntityInfoSetting AS eis ON eis.EntityID = e.EntityID AND ic.业主编号 = eis.VendorNo
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.结算周期
		WHERE ic.租金类型 LIKE '%固定%'	AND ci.CycleType = '固定'
		GROUP BY eis.EntityInfoSettingID,ic.租金类型,ic.首次DueDate,
			ic.支付类型,ic.直线租金计算起始日,ic.直线常数,--ic.起租日,ic.租赁到期日,
			ic.结算周期,ci.CycleMonthCount,ic.租赁公历,ic.摘要,ic.备注
		
		--SELECT * FROM  @FixedRuleSetting
			
		--固定租金时间段设置OK
		INSERT INTO @FixedTimeIntervalSetting 
		SELECT NEWID(), frs.RuleSnapshotID,ic.时间段开始,ic.时间段结束,
			ic.公式,ic.结算周期,ci.CycleMonthCount,ic.租赁公历
		FROM @Import_Contract AS ic
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.结算周期
		LEFT JOIN @Entity e ON e.EntityName = ic.实体名称
		LEFT JOIN @EntityInfoSetting eis ON eis.EntityID = e.EntityID
		LEFT JOIN @FixedRuleSetting AS frs ON ic.租金类型 = frs.RentType 
			AND ISNULL(ic.摘要,'')=ISNULL(frs.Description,'')
			AND ISNULL(ic.备注,'') = ISNULL(frs.Remark,'')
			AND eis.EntityInfoSettingID = frs.EntityInfoSettingID
		WHERE ic.租金类型 LIKE '%固定%'
		GROUP BY frs.RuleSnapshotID,ic.时间段开始,ic.时间段结束,
			ic.公式,ic.结算周期,ci.CycleMonthCount,ic.租赁公历
			
--		SELECT * FROM  @FixedTimeIntervalSetting

--		--百分比规则OK
		INSERT INTO @RatioRuleSetting
		SELECT NEWID(),x.EntityInfoSettingID,x.RentType,x.摘要,x.备注
		FROM 
		(SELECT eis.EntityInfoSettingID,
			SUBSTRING(ic.租金类型,1,LEN(ic.租金类型)-3) AS RentType,
			ic.摘要,ic.备注
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.实体名称
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID AND ic.业主编号 = eis.VendorNo
		WHERE ic.租金类型 LIKE '%百分比%') x
		GROUP BY x.EntityInfoSettingID,x.RentType,x.摘要,x.备注
		
		--SELECT * FROM @RatioRuleSetting
		
--		--百分比周期
		INSERT INTO @RatioCycleSetting 
		SELECT rrs.RatioID,NEWID(),NEWID(),
			CASE WHEN ic.是否纯百分比 = '是' THEN 1 ELSE 0 END,
			ic.首次DueDate,
			NULL,NULL,NULL,NULL,NULL,--????未插入NextDueDate,NextAPStartDate,NextAPEndDate,NextGLStartDate,NextGLEndDate
			ic.支付类型,ic.直线租金计算起始日,
			ic.结算周期,ci.CycleMonthCount,ic.租赁公历,RIGHT(ic.租金类型,3),NULL
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e ON e.EntityName = ic.实体名称
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN dbo.CycleItem AS ci ON ci.CycleItemName = ic.结算周期
		WHERE ic.租金类型 LIKE '%百分比%' AND ci.CycleType = '百分比'
				 AND rrs.RentType+RIGHT(ic.租金类型,3)=ic.租金类型
		GROUP BY rrs.RatioID,ic.是否纯百分比,ic.首次DueDate,
			ic.支付类型,ic.直线租金计算起始日,
			ic.结算周期,ci.CycleMonthCount,ic.租赁公历,ic.租金类型

		--SELECT * FROM @RatioCycleSetting
		

--		--百分比时间段设置OK
		INSERT INTO @RatioTimeIntervalSetting
		SELECT NEWID(),rcs.RuleSnapshotID,ic.时间段开始,ic.时间段结束
		FROM @Import_Contract ic 
		LEFT JOIN @VendorContract v ON ic.业主编号=v.VendorNo
		LEFT JOIN @Entity AS e ON e.EntityName = ic.实体名称 
		LEFT JOIN @VendorEntity ve ON e.EntityID=ve.EntityID AND v.VendorNo=ve.VendorNo
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN @RatioCycleSetting rcs ON rrs.RatioID = rcs.RatioID
		WHERE rrs.RentType LIKE '%百分比%' AND rrs.RentType+rcs.CycleType=ic.租金类型
		GROUP BY rcs.RuleSnapshotID,ic.时间段开始,ic.时间段结束
		ORDER BY rcs.RuleSnapshotID,ic.时间段开始
		--SELECT * FROM @RatioTimeIntervalSetting
		
--		--时间段条件金额
		INSERT INTO @ConditionAmount
		SELECT NEWID(),rtis.TimeIntervalID,ic.条件,
			ic.公式,ic.SQL条件,ic.SQL公式,ic.条件数字串,ic.公式数字串
			--,rtis.StartDate,rtis.EndDate,rrs.RentType,rcs.CycleType
		FROM @Import_Contract ic
		LEFT JOIN @Entity AS e	ON e.EntityName = ic.实体名称
		LEFT JOIN @EntityInfoSetting AS eis	ON eis.EntityID = e.EntityID
		LEFT JOIN @RatioRuleSetting rrs	ON rrs.EntityInfoSettingID = eis.EntityInfoSettingID
		LEFT JOIN @RatioCycleSetting rcs 
			ON rrs.RatioID = rcs.RatioID
				AND rcs.CycleType = RIGHT(ic.租金类型,3)
		LEFT JOIN @RatioTimeIntervalSetting rtis
			ON	rcs.RuleSnapshotID = rtis.RuleSnapshotID
				AND rtis.StartDate = ic.时间段开始 AND rtis.EndDate = ic.时间段结束
				AND ic.租金类型 = rrs.RentType + rcs.CycleType
		WHERE ic.租金类型 LIKE '%百分比%'
		GROUP BY rtis.TimeIntervalID,ic.条件,ic.公式,ic.SQL条件,ic.SQL公式,ic.条件数字串,ic.公式数字串
		--SELECT * FROM  @ConditionAmount
		
--		插入实际表中
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
				
		--添加了从条件和公式中提取数字的表，需要增加以下操作
		DECLARE @ConditionID NVARCHAR(50),@ConditionNumberValue NVARCHAR(500),@FormulaNumberValue NVARCHAR(500)
		DECLARE @MyCursor CURSOR
		SET  @MyCursor = CURSOR  SCROLL FOR
		SELECT ConditionID,ConditionNumberValue,FormulaNumberValue FROM @ConditionAmount
		OPEN @MyCursor
		FETCH NEXT FROM @MyCursor INTO @ConditionID,@ConditionNumberValue,@FormulaNumberValue
		WHILE @@FETCH_STATUS = 0
		BEGIN
			--删除之前的数据
			DELETE FROM dbo.ConditionAmountNumbers
			WHERE ConditionID = @ConditionID			
			
			--插入条件值
			INSERT INTO dbo.ConditionAmountNumbers (NumberValue,NumberID,ConditionID,NumberType)
			SELECT DISTINCT v.TokenVal,NEWID(),@ConditionID,'条件' 
			FROM dbo.fn_Helper_Split(@ConditionNumberValue,';') AS v
			
			--插入公式值
			INSERT INTO dbo.ConditionAmountNumbers (NumberValue,NumberID,ConditionID,NumberType)
			SELECT DISTINCT v.TokenVal,NEWID(),@ConditionID,'公式' 
			FROM dbo.fn_Helper_Split(@FormulaNumberValue,';') AS v
			
			FETCH NEXT FROM @MyCursor INTO @ConditionID,@ConditionNumberValue,@FormulaNumberValue
		END

		CLOSE @MyCursor
		DEALLOCATE	@MyCursor
		
	
    
END
























GO


