USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[usp_Import_Contract1CheckEveryColmn]    Script Date: 08/27/2012 11:06:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		zhangbsh
-- Create date:20110406
-- Description:	合同导入
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract1CheckEveryColmn]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;
		
	--合同序号
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'ContractIndexIsNull',ic.合同序号,'合同序号为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.合同序号 IS NULL
	
	--公司编号	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CompanyCodeIsNull',IC.公司编号,'公司编号为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.公司编号 IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CompanyCodeError',IC.公司编号,'公司编号错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.公司编号
	WHERE ic.公司编号 IS NOT NULL AND c.CompanyCode IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CompanyInactive',IC.公司编号,'公司状态不是Active'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.公司编号
	WHERE ic.公司编号 IS NOT NULL AND c.CompanyName IS NOT NULL
		AND c.Status = 'I'	

	--公司名称
	INSERT INTO dbo.Import_ContractMessage 
	SELECT ic.Excel行号,ic.合同序号,'CompanyNameNotMatch',ic.公司名称,'公司名称不符'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.公司编号 AND c.CompanyName = ic.公司名称
	WHERE IC.公司编号 IS NOT NULL AND c.CompanyName IS NULL
	
	--业主编号
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'VendorNoError',IC.业主编号,'业主编号错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Vendor AS STMV
		ON IC.业主编号 = STMV.VendorNo
	WHERE IC.业主编号 IS NOT NULL AND STMV.VendorNo IS NULL
	
	--业主名称
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'VendorNameIsNull',IC.业主名称,'业主名称为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.业主名称 IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'VendorNameError',IC.业主名称,'业主名称错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Vendor AS STMV
		ON IC.业主编号 = STMV.VendorNo 
			AND IC.业主名称 = STMV.VendorName AND STMV.VendorNo IS NOT NULL
	WHERE IC.业主编号 IS NOT NULL AND STMV.VendorName IS NULL
	
	--实体类型
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'EntityTypeIsNull',IC.实体类型,'实体类型为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.实体类型 IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'EntityTypeError',IC.实体类型,'实体类型错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.EntityType AS et ON et.EntityTypeName = ic.实体类型
	WHERE et.EntityTypeName IS NULL
	
	--实体名称
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'EntityNameIsNull',IC.实体名称,'实体名称为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.实体名称 IS NULL	
	
	--不按KioskNo校验，先将该字段置空，然后按照名称更新，
	UPDATE dbo.Import_Contract SET Kiosk编号=NULL
	UPDATE dbo.Import_Contract SET Kiosk编号=k.KioskNo
	FROM dbo.Import_Contract ic,dbo.SRLS_TB_Master_Kiosk k
	WHERE ic.实体类型='甜品店' AND ic.实体名称 = k.KioskName
	
	--如果是甜品店类型，则KioskNo必须不为空，否则就是没有对应的
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'KioskNameError',IC.实体名称,'甜品店名称错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS k ON ic.实体名称 = k.KioskName
	WHERE ic.实体类型 = '甜品店' AND k.KioskName IS NULL
	
--	INSERT INTO dbo.Import_ContractMessage 
--	SELECT IC.Excel行号,IC.合同序号,'KioskNameError',IC.Kiosk编号,'甜品店未生效'
--	FROM dbo.Import_Contract AS IC
--	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS k ON ic.实体名称 = k.KioskName
--	WHERE ic.实体类型 = '甜品店' AND k.KioskName IS NOT NULL AND ic.Kiosk编号 IS NULL

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'KioskNoNotActive',IC.Kiosk编号,'甜品店不是已生效状态'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS STMK
		ON ic.Kiosk编号 = STMK.KioskNo AND ic.Kiosk编号 IS NOT NULL AND STMK.KioskName IS NOT NULL
	WHERE ic.实体类型 = '甜品店' AND STMK.Status <> '已生效'

	 
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'StoreNameError',IC.实体名称,'餐厅编号与餐厅名称不对应'
	FROM  dbo.Import_Contract AS IC  
	LEFT JOIN dbo.SRLS_TB_Master_Store AS s 
		ON IC.餐厅部门编号 = s.StoreNo AND ic.实体名称 = s.StoreName AND s.StoreNo IS NOT NULL
	WHERE ic.实体类型 = '餐厅' AND s.StoreName IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'StoreIsInactive',IC.实体名称,'餐厅的状态不是Active'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Store AS s
		ON IC.餐厅部门编号 = s.StoreNo AND ic.实体名称 = s.StoreName AND s.StoreNo IS NOT NULL
	WHERE ic.实体类型 = '餐厅' AND s.StoreName IS NOT NULL AND s.Status = 'I'
	
	--餐厅部门编号
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'StoreDeptNoIsNull',IC.餐厅部门编号,'餐厅/部门编号为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.餐厅部门编号 IS NULL	
							
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'StoreDeptNoError',IC.餐厅部门编号,'餐厅/部门编号错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN (
		SELECT StoreNo FROM dbo.SRLS_TB_Master_Store
		UNION ALL
		SELECT DeptCode AS StoreDeptNo FROM dbo.SRLS_TB_Master_Department
		       ) AS stms
		ON ic.餐厅部门编号 = stms.StoreNo
	WHERE stms.StoreNo IS NULL
	
--	--Kiosk编号	
--	INSERT INTO dbo.Import_ContractMessage 
--	SELECT IC.Excel行号,IC.合同序号,'KioskNoError',IC.Kiosk编号,'Kiosk编号错误'
--	FROM dbo.Import_Contract AS IC
--	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS STMK
--		ON ic.Kiosk编号 = STMK.KioskNo AND ic.Kiosk编号 IS NOT NULL
--	WHERE ic.实体类型 = '甜品店' AND STMK.KioskName IS NULL
		
	--开业日	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'OpenDateIsNull',IC.开业日,'开业日为空'
	FROM dbo.Import_Contract AS IC
	WHERE (ic.实体类型 = '餐厅' OR ic.实体类型 = '甜品店') AND ic.开业日 IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'OpenDateFormatError',IC.开业日,'开业日格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE (ic.实体类型 = '餐厅' OR ic.实体类型 = '甜品店') 
		AND ic.开业日 IS NOT NULL AND ISDATE(IC.开业日) = 0
	
	--起租日
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'RentStartDateIsNull',IC.起租日,'起租日为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.起租日 IS NULL	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'RentStartDateFormatError',IC.起租日,'起租日格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.起租日 IS NOT NULL	AND ISDATE(IC.起租日) = 0

	--租赁到期日
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'RentEndDateIsNull',IC.租赁到期日,'租赁到期日为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租赁到期日 IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'RentEndDateFormatError',IC.租赁到期日,'租赁到期日格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租赁到期日 IS NOT NULL	AND ISDATE(IC.租赁到期日) = 0

	--是否出AP
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'IsCalcAPIsNull',IC.是否出AP,'是否出AP为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.是否出AP IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'IsCalcAPValueError',IC.是否出AP,'是否出AP不是“是/否”'
	FROM dbo.Import_Contract AS IC
	WHERE ic.是否出AP IS NOT NULL AND ic.是否出AP <> '是' AND ic.是否出AP <> '否'
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CalcAPDateIsNull',IC.出AP日期,'当“是否出AP”为“是”时，“出AP日期”为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.是否出AP = '是' AND ic.出AP日期 IS NULL
	
	--出AP日期
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CalcAPDateFormatError',IC.出AP日期,'出AP日期格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.出AP日期 IS NOT NULL AND ISDATE(IC.出AP日期) = 0
	
	--地产部预估年SALES
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'DCYGIsNull',IC.地产部预估年SALES,'地产部预估年SALES为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.实体类型='餐厅' AND (ic.地产部预估年SALES IS NULL OR LEN(ic.地产部预估年SALES)=0) 	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'DCYGIsNotNumeric',IC.地产部预估年SALES,'地产部预估年SALES不是数字'
	FROM dbo.Import_Contract AS IC
	WHERE ic.地产部预估年SALES IS NOT NULL AND LEN(ic.地产部预估年SALES)>0 AND ISNUMERIC(IC.地产部预估年SALES) = 0
	
	--保证金
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'MarginFormatError',IC.保证金,'保证金不是数字'
	FROM dbo.Import_Contract AS IC
	WHERE ic.保证金 IS NOT NULL AND LEN(ic.保证金)>0 AND ISNUMERIC(IC.保证金) = 0
	
	--税率
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'TaxRateIsNull',IC.税率,'税率为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.税率 IS NULL OR LEN(ic.税率) = 0
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'TaxRateIsNotNumeric',IC.税率,'税率不是数字'
	FROM dbo.Import_Contract AS IC
	WHERE ic.税率 IS NOT NULL AND LEN(ic.税率) > 0 AND ISNUMERIC(IC.税率) = 0

	--租金类型
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel行号,IC.合同序号,'RentTypeIsNull',IC.租金类型,'租金类型为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租金类型 IS NULL	

	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel行号,IC.合同序号,'RentTypeError',IC.租金类型,'租金类型错误'
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 NOT IN(
		SELECT RentTypeName+'大周期' FROM dbo.RentType WHERE RentTypeName LIKE '%百分比%'
		UNION ALL
		SELECT RentTypeName+'小周期' FROM dbo.RentType WHERE RentTypeName LIKE '%百分比%'
		UNION ALL
		SELECT RentTypeName FROM dbo.RentType WHERE RentTypeName LIKE '%固定%')
		
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel行号,IC.合同序号,'RentTypeNotMatchTypeCode',IC.租金类型,'租金类型没有对应有效的TypeCode'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.TypeCode AS TC
		ON TC.EntityTypeName = IC.实体类型 AND (TC.RentTypeName = IC.租金类型
			OR TC.RentTypeName+'大周期'=IC.租金类型 OR TC.RentTypeName+'小周期'=IC.租金类型)
			AND TC.Status = '已生效' AND TC.SnapshotCreateTime IS NULL
	WHERE TC.TypeCodeName IS NULL
	
	DECLARE @TempSummaryRemark TABLE(
	ContractIndex NVARCHAR(500),CompanyNo NVARCHAR(500),VendorNo NVARCHAR(500),
	VendorName NVARCHAR(500),EntityType NVARCHAR(500),StoreDeptNo NVARCHAR(500),
	EntityName NVARCHAR(500),RentType NVARCHAR(500),
	Summary NVARCHAR(500),Remark NVARCHAR(500))
	
	--更新摘要和备注，便于分组
	UPDATE dbo.Import_Contract SET 摘要='' WHERE 摘要 IS NULL
	UPDATE dbo.Import_Contract SET 备注='' WHERE 备注 IS NULL
	
	--校验租金规则是否存在多个备注或摘要
	INSERT INTO @TempSummaryRemark
	SELECT IC.合同序号,IC.公司编号,IC.业主编号,IC.业主名称,IC.实体类型,IC.餐厅部门编号,IC.实体名称,
		CASE
			WHEN IC.租金类型 LIKE '百分比%' THEN LEFT(IC.租金类型,LEN(IC.租金类型)-3)
			ELSE IC.租金类型
		END AS RentType,IC.摘要,IC.备注 
	FROM dbo.Import_Contract AS IC--找出租金类型合理的
	WHERE IC.Excel行号 NOT IN 
		(SELECT ICM.ExcelIndex FROM dbo.Import_ContractMessage AS ICM
		WHERE ICM.CheckMessage = 'RentTypeIsNull' 
			OR ICM.CheckMessage = 'RentTypeError' 
			OR ICM.CheckMessage = 'RentTypeNotMatchTypeCode')
	GROUP BY IC.合同序号,IC.公司编号,IC.业主编号,IC.业主名称,IC.实体类型,IC.餐厅部门编号,
		IC.实体名称,IC.租金类型,IC.摘要,IC.备注
		
--	SELECT * FROM @TempSummaryRemark

	INSERT INTO dbo.Import_ContractMessage 
	SELECT NULL,t.ContractIndex,'SummaryOrRemarkError',
	t.VendorName+'-'+t.EntityType+'-'+t.EntityName+'-'+t.RentType,
	'同一个租金规则存在多个备注或摘要'--,SUM(t.DiffCount)
	FROM (
		SELECT t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.RentType,t.EntityName--,COUNT(DISTINCT t.Summary) AS DiffCount
		FROM @TempSummaryRemark AS t
		GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType
		HAVING COUNT(DISTINCT t.Summary) > 1--检查摘要是否重复
		UNION ALL
		SELECT t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.RentType,t.EntityName--,COUNT(DISTINCT t.Remark) AS DiffCount
		FROM @TempSummaryRemark AS t
		GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType--检查备注是否重复
		HAVING COUNT(DISTINCT t.Remark) > 1) t
	GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
	t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType
	HAVING COUNT(*) > 0
	
	
	--是否纯百分比			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'IsPureRatioIsNull',IC.是否纯百分比,'是否纯百分比为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 LIKE '%百分比%' AND ic.是否纯百分比 IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'IsPureRatioValueError',IC.是否纯百分比,'是否纯百分比取值错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租金类型 LIKE '%百分比%' AND ic.是否纯百分比 IS NOT NULL AND ic.是否纯百分比 <> '是' and ic.是否纯百分比 <> '否'
	
	--直线租金计算起始日
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel行号,IC.合同序号,'ZXStartDateIsNull',IC.直线租金计算起始日,'直线租金计算起始日为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 = '固定租金' 
		AND (IC.实体类型 = '餐厅' OR IC.实体类型 = '甜品店')
		AND (IC.直线租金计算起始日 IS NULL OR LEN(IC.直线租金计算起始日)=0)
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'ZXStartDateFormatError',IC.直线租金计算起始日,'直线租金计算起始日格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 = '固定租金' 
		AND (IC.实体类型 = '餐厅' OR IC.实体类型 = '甜品店')
		AND IC.直线租金计算起始日 IS NOT NULL AND LEN(IC.直线租金计算起始日)>0 
		AND ISDATE(IC.直线租金计算起始日) = 0
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'ZXStartDateValueError',IC.直线租金计算起始日,'直线租金计算起始日必须是开业日'+IC.开业日+'或起租日'+IC.起租日
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 = '固定租金' 
		AND (IC.实体类型 = '餐厅' OR IC.实体类型 = '甜品店')
		AND IC.直线租金计算起始日 IS NOT NULL AND LEN(IC.直线租金计算起始日) > 0
		AND ISDATE(IC.直线租金计算起始日) = 1
		AND ISDATE(IC.开业日) = 1 
		AND dbo.fn_GetDate(IC.直线租金计算起始日) <> dbo.fn_GetDate(IC.开业日)
		AND ISDATE(IC.起租日) = 1 
		AND dbo.fn_GetDate(IC.直线租金计算起始日) <> dbo.fn_GetDate(IC.起租日)	
	
	--直线常数			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'ZXConstantIsNull',IC.直线常数,'直线常数为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.租金类型 = '固定租金' 
		AND (IC.实体类型 = '餐厅' OR IC.实体类型 = '甜品店')
		AND IC.直线租金计算起始日 IS NOT NULL 
		AND (IC.直线常数 IS NULL OR (IC.直线常数 IS NOT NULL AND LEN(IC.直线常数)=0))
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'ZXConstantIsNotNumeric',IC.直线常数,'直线常数不是数字'
	FROM dbo.Import_Contract AS IC
	WHERE  IC.租金类型 = '固定租金' 
		AND (IC.实体类型 = '餐厅' OR IC.实体类型 = '甜品店')
		AND IC.直线租金计算起始日 IS NOT NULL 
		AND ic.直线常数 IS NOT NULL AND LEN(IC.直线常数)>0 AND ISNUMERIC(IC.直线常数) = 0
	
	--支付类型			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'PaymentTypeIsNull',IC.支付类型,'支付类型为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.支付类型 IS NULL	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'PaymentTypeError',IC.支付类型,'支付类型取值错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.支付类型 IS NOT NULL AND ic.支付类型 NOT IN ('预付','实付','延付')	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'PaymentTypeRatioError',IC.支付类型,'支付类型在租金类型为百分比时必须选择延付'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租金类型 LIKE '%百分比%' AND ic.支付类型 IS NOT NULL 
		AND ic.支付类型 <>'延付'	
		
	--结算周期	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleIsNull',IC.结算周期,'结算周期为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.结算周期 IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleError',IC.结算周期,'结算周期错误'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.CycleItem AS ci ON ic.结算周期 = ci.CycleItemName AND ic.租金类型 LIKE '%'+ci.CycleType+'%'
	WHERE ci.CycleItemName IS NULL	
	
	--租赁公历	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleTypeIsNull',IC.租赁公历,'租赁公历为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租赁公历 IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleTypeError',IC.租赁公历,'租赁公历错误'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租赁公历 IS NOT NULL AND ic.租赁公历 NOT IN ('租赁','公历')
	
	--Commented by Eric -- Begin
	----首次DueDate
	--INSERT INTO dbo.Import_ContractMessage 
	--SELECT IC.Excel行号,IC.合同序号,'FirstDueDateIsNull',IC.首次DueDate,'首次DueDate为空'
	--FROM dbo.Import_Contract AS IC
	--WHERE IC.首次DueDate IS NULL
	
	--INSERT INTO dbo.Import_ContractMessage 
	--SELECT IC.Excel行号,IC.合同序号,'FirstDueDateFormatError',IC.首次DueDate,'首次DueDate格式错误'
	--FROM dbo.Import_Contract AS IC
	--WHERE IC.首次DueDate IS NOT NULL AND ISDATE(IC.首次DueDate) = 0
	--Commented by Eric -- End
	
	--规则时间段
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleStartDateIsNull',IC.时间段开始,'时间段开始为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.时间段开始 IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleStartDateFormatError',IC.时间段开始,'时间段开始格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE IC.时间段开始 IS NOT NULL AND ISDATE(IC.时间段开始) = 0
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleEndDateIsNull',IC.时间段结束,'时间段结束为空'
	FROM dbo.Import_Contract AS IC
	WHERE IC.时间段结束 IS NULL
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'CycleEndDateFormatError',IC.时间段结束,'时间段结束格式错误'
	FROM dbo.Import_Contract AS IC
	WHERE IC.时间段结束 IS NOT NULL AND ISDATE(IC.时间段结束) = 0
	
	--公式					
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'FormulaIsNull',IC.公式,'公式为空'
	FROM dbo.Import_Contract AS IC
	WHERE ic.公式 IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel行号,IC.合同序号,'FormulaErrorWhenFixRull',IC.公式,'租金类型为固定时公式必须是数值'
	FROM dbo.Import_Contract AS IC
	WHERE ic.租金类型 LIKE '%固定%' AND ic.公式 IS NOT NULL AND ISNUMERIC(ic.公式) = 0 	
	
    
END


GO


