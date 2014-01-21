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
-- Description:	��ͬ����
-- =============================================
CREATE PROCEDURE [dbo].[usp_Import_Contract1CheckEveryColmn]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;
		
	--��ͬ���
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'ContractIndexIsNull',ic.��ͬ���,'��ͬ���Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.��ͬ��� IS NULL
	
	--��˾���	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CompanyCodeIsNull',IC.��˾���,'��˾���Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.��˾��� IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CompanyCodeError',IC.��˾���,'��˾��Ŵ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.��˾���
	WHERE ic.��˾��� IS NOT NULL AND c.CompanyCode IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CompanyInactive',IC.��˾���,'��˾״̬����Active'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.��˾���
	WHERE ic.��˾��� IS NOT NULL AND c.CompanyName IS NOT NULL
		AND c.Status = 'I'	

	--��˾����
	INSERT INTO dbo.Import_ContractMessage 
	SELECT ic.Excel�к�,ic.��ͬ���,'CompanyNameNotMatch',ic.��˾����,'��˾���Ʋ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Company AS c
		ON c.CompanyCode = ic.��˾��� AND c.CompanyName = ic.��˾����
	WHERE IC.��˾��� IS NOT NULL AND c.CompanyName IS NULL
	
	--ҵ�����
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'VendorNoError',IC.ҵ�����,'ҵ����Ŵ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Vendor AS STMV
		ON IC.ҵ����� = STMV.VendorNo
	WHERE IC.ҵ����� IS NOT NULL AND STMV.VendorNo IS NULL
	
	--ҵ������
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'VendorNameIsNull',IC.ҵ������,'ҵ������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.ҵ������ IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'VendorNameError',IC.ҵ������,'ҵ�����ƴ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Vendor AS STMV
		ON IC.ҵ����� = STMV.VendorNo 
			AND IC.ҵ������ = STMV.VendorName AND STMV.VendorNo IS NOT NULL
	WHERE IC.ҵ����� IS NOT NULL AND STMV.VendorName IS NULL
	
	--ʵ������
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'EntityTypeIsNull',IC.ʵ������,'ʵ������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.ʵ������ IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'EntityTypeError',IC.ʵ������,'ʵ�����ʹ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.EntityType AS et ON et.EntityTypeName = ic.ʵ������
	WHERE et.EntityTypeName IS NULL
	
	--ʵ������
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'EntityNameIsNull',IC.ʵ������,'ʵ������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.ʵ������ IS NULL	
	
	--����KioskNoУ�飬�Ƚ����ֶ��ÿգ�Ȼ�������Ƹ��£�
	UPDATE dbo.Import_Contract SET Kiosk���=NULL
	UPDATE dbo.Import_Contract SET Kiosk���=k.KioskNo
	FROM dbo.Import_Contract ic,dbo.SRLS_TB_Master_Kiosk k
	WHERE ic.ʵ������='��Ʒ��' AND ic.ʵ������ = k.KioskName
	
	--�������Ʒ�����ͣ���KioskNo���벻Ϊ�գ��������û�ж�Ӧ��
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'KioskNameError',IC.ʵ������,'��Ʒ�����ƴ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS k ON ic.ʵ������ = k.KioskName
	WHERE ic.ʵ������ = '��Ʒ��' AND k.KioskName IS NULL
	
--	INSERT INTO dbo.Import_ContractMessage 
--	SELECT IC.Excel�к�,IC.��ͬ���,'KioskNameError',IC.Kiosk���,'��Ʒ��δ��Ч'
--	FROM dbo.Import_Contract AS IC
--	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS k ON ic.ʵ������ = k.KioskName
--	WHERE ic.ʵ������ = '��Ʒ��' AND k.KioskName IS NOT NULL AND ic.Kiosk��� IS NULL

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'KioskNoNotActive',IC.Kiosk���,'��Ʒ�겻������Ч״̬'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS STMK
		ON ic.Kiosk��� = STMK.KioskNo AND ic.Kiosk��� IS NOT NULL AND STMK.KioskName IS NOT NULL
	WHERE ic.ʵ������ = '��Ʒ��' AND STMK.Status <> '����Ч'

	 
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'StoreNameError',IC.ʵ������,'���������������Ʋ���Ӧ'
	FROM  dbo.Import_Contract AS IC  
	LEFT JOIN dbo.SRLS_TB_Master_Store AS s 
		ON IC.�������ű�� = s.StoreNo AND ic.ʵ������ = s.StoreName AND s.StoreNo IS NOT NULL
	WHERE ic.ʵ������ = '����' AND s.StoreName IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'StoreIsInactive',IC.ʵ������,'������״̬����Active'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.SRLS_TB_Master_Store AS s
		ON IC.�������ű�� = s.StoreNo AND ic.ʵ������ = s.StoreName AND s.StoreNo IS NOT NULL
	WHERE ic.ʵ������ = '����' AND s.StoreName IS NOT NULL AND s.Status = 'I'
	
	--�������ű��
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'StoreDeptNoIsNull',IC.�������ű��,'����/���ű��Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�������ű�� IS NULL	
							
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'StoreDeptNoError',IC.�������ű��,'����/���ű�Ŵ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN (
		SELECT StoreNo FROM dbo.SRLS_TB_Master_Store
		UNION ALL
		SELECT DeptCode AS StoreDeptNo FROM dbo.SRLS_TB_Master_Department
		       ) AS stms
		ON ic.�������ű�� = stms.StoreNo
	WHERE stms.StoreNo IS NULL
	
--	--Kiosk���	
--	INSERT INTO dbo.Import_ContractMessage 
--	SELECT IC.Excel�к�,IC.��ͬ���,'KioskNoError',IC.Kiosk���,'Kiosk��Ŵ���'
--	FROM dbo.Import_Contract AS IC
--	LEFT JOIN dbo.SRLS_TB_Master_Kiosk AS STMK
--		ON ic.Kiosk��� = STMK.KioskNo AND ic.Kiosk��� IS NOT NULL
--	WHERE ic.ʵ������ = '��Ʒ��' AND STMK.KioskName IS NULL
		
	--��ҵ��	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'OpenDateIsNull',IC.��ҵ��,'��ҵ��Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE (ic.ʵ������ = '����' OR ic.ʵ������ = '��Ʒ��') AND ic.��ҵ�� IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'OpenDateFormatError',IC.��ҵ��,'��ҵ�ո�ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE (ic.ʵ������ = '����' OR ic.ʵ������ = '��Ʒ��') 
		AND ic.��ҵ�� IS NOT NULL AND ISDATE(IC.��ҵ��) = 0
	
	--������
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'RentStartDateIsNull',IC.������,'������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������ IS NULL	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'RentStartDateFormatError',IC.������,'�����ո�ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������ IS NOT NULL	AND ISDATE(IC.������) = 0

	--���޵�����
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'RentEndDateIsNull',IC.���޵�����,'���޵�����Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.���޵����� IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'RentEndDateFormatError',IC.���޵�����,'���޵����ո�ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE ic.���޵����� IS NOT NULL	AND ISDATE(IC.���޵�����) = 0

	--�Ƿ��AP
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'IsCalcAPIsNull',IC.�Ƿ��AP,'�Ƿ��APΪ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�Ƿ��AP IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'IsCalcAPValueError',IC.�Ƿ��AP,'�Ƿ��AP���ǡ���/��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�Ƿ��AP IS NOT NULL AND ic.�Ƿ��AP <> '��' AND ic.�Ƿ��AP <> '��'
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CalcAPDateIsNull',IC.��AP����,'�����Ƿ��AP��Ϊ���ǡ�ʱ������AP���ڡ�Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�Ƿ��AP = '��' AND ic.��AP���� IS NULL
	
	--��AP����
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CalcAPDateFormatError',IC.��AP����,'��AP���ڸ�ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE ic.��AP���� IS NOT NULL AND ISDATE(IC.��AP����) = 0
	
	--�ز���Ԥ����SALES
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'DCYGIsNull',IC.�ز���Ԥ����SALES,'�ز���Ԥ����SALESΪ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.ʵ������='����' AND (ic.�ز���Ԥ����SALES IS NULL OR LEN(ic.�ز���Ԥ����SALES)=0) 	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'DCYGIsNotNumeric',IC.�ز���Ԥ����SALES,'�ز���Ԥ����SALES��������'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�ز���Ԥ����SALES IS NOT NULL AND LEN(ic.�ز���Ԥ����SALES)>0 AND ISNUMERIC(IC.�ز���Ԥ����SALES) = 0
	
	--��֤��
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'MarginFormatError',IC.��֤��,'��֤��������'
	FROM dbo.Import_Contract AS IC
	WHERE ic.��֤�� IS NOT NULL AND LEN(ic.��֤��)>0 AND ISNUMERIC(IC.��֤��) = 0
	
	--˰��
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'TaxRateIsNull',IC.˰��,'˰��Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.˰�� IS NULL OR LEN(ic.˰��) = 0
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'TaxRateIsNotNumeric',IC.˰��,'˰�ʲ�������'
	FROM dbo.Import_Contract AS IC
	WHERE ic.˰�� IS NOT NULL AND LEN(ic.˰��) > 0 AND ISNUMERIC(IC.˰��) = 0

	--�������
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel�к�,IC.��ͬ���,'RentTypeIsNull',IC.�������,'�������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������� IS NULL	

	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel�к�,IC.��ͬ���,'RentTypeError',IC.�������,'������ʹ���'
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� NOT IN(
		SELECT RentTypeName+'������' FROM dbo.RentType WHERE RentTypeName LIKE '%�ٷֱ�%'
		UNION ALL
		SELECT RentTypeName+'С����' FROM dbo.RentType WHERE RentTypeName LIKE '%�ٷֱ�%'
		UNION ALL
		SELECT RentTypeName FROM dbo.RentType WHERE RentTypeName LIKE '%�̶�%')
		
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel�к�,IC.��ͬ���,'RentTypeNotMatchTypeCode',IC.�������,'�������û�ж�Ӧ��Ч��TypeCode'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.TypeCode AS TC
		ON TC.EntityTypeName = IC.ʵ������ AND (TC.RentTypeName = IC.�������
			OR TC.RentTypeName+'������'=IC.������� OR TC.RentTypeName+'С����'=IC.�������)
			AND TC.Status = '����Ч' AND TC.SnapshotCreateTime IS NULL
	WHERE TC.TypeCodeName IS NULL
	
	DECLARE @TempSummaryRemark TABLE(
	ContractIndex NVARCHAR(500),CompanyNo NVARCHAR(500),VendorNo NVARCHAR(500),
	VendorName NVARCHAR(500),EntityType NVARCHAR(500),StoreDeptNo NVARCHAR(500),
	EntityName NVARCHAR(500),RentType NVARCHAR(500),
	Summary NVARCHAR(500),Remark NVARCHAR(500))
	
	--����ժҪ�ͱ�ע�����ڷ���
	UPDATE dbo.Import_Contract SET ժҪ='' WHERE ժҪ IS NULL
	UPDATE dbo.Import_Contract SET ��ע='' WHERE ��ע IS NULL
	
	--У���������Ƿ���ڶ����ע��ժҪ
	INSERT INTO @TempSummaryRemark
	SELECT IC.��ͬ���,IC.��˾���,IC.ҵ�����,IC.ҵ������,IC.ʵ������,IC.�������ű��,IC.ʵ������,
		CASE
			WHEN IC.������� LIKE '�ٷֱ�%' THEN LEFT(IC.�������,LEN(IC.�������)-3)
			ELSE IC.�������
		END AS RentType,IC.ժҪ,IC.��ע 
	FROM dbo.Import_Contract AS IC--�ҳ�������ͺ����
	WHERE IC.Excel�к� NOT IN 
		(SELECT ICM.ExcelIndex FROM dbo.Import_ContractMessage AS ICM
		WHERE ICM.CheckMessage = 'RentTypeIsNull' 
			OR ICM.CheckMessage = 'RentTypeError' 
			OR ICM.CheckMessage = 'RentTypeNotMatchTypeCode')
	GROUP BY IC.��ͬ���,IC.��˾���,IC.ҵ�����,IC.ҵ������,IC.ʵ������,IC.�������ű��,
		IC.ʵ������,IC.�������,IC.ժҪ,IC.��ע
		
--	SELECT * FROM @TempSummaryRemark

	INSERT INTO dbo.Import_ContractMessage 
	SELECT NULL,t.ContractIndex,'SummaryOrRemarkError',
	t.VendorName+'-'+t.EntityType+'-'+t.EntityName+'-'+t.RentType,
	'ͬһ����������ڶ����ע��ժҪ'--,SUM(t.DiffCount)
	FROM (
		SELECT t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.RentType,t.EntityName--,COUNT(DISTINCT t.Summary) AS DiffCount
		FROM @TempSummaryRemark AS t
		GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType
		HAVING COUNT(DISTINCT t.Summary) > 1--���ժҪ�Ƿ��ظ�
		UNION ALL
		SELECT t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.RentType,t.EntityName--,COUNT(DISTINCT t.Remark) AS DiffCount
		FROM @TempSummaryRemark AS t
		GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
		t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType--��鱸ע�Ƿ��ظ�
		HAVING COUNT(DISTINCT t.Remark) > 1) t
	GROUP BY t.ContractIndex,t.CompanyNo,t.VendorNo,t.VendorName,
	t.EntityType,t.StoreDeptNo,t.EntityName,t.RentType
	HAVING COUNT(*) > 0
	
	
	--�Ƿ񴿰ٷֱ�			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'IsPureRatioIsNull',IC.�Ƿ񴿰ٷֱ�,'�Ƿ񴿰ٷֱ�Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� LIKE '%�ٷֱ�%' AND ic.�Ƿ񴿰ٷֱ� IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'IsPureRatioValueError',IC.�Ƿ񴿰ٷֱ�,'�Ƿ񴿰ٷֱ�ȡֵ����'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������� LIKE '%�ٷֱ�%' AND ic.�Ƿ񴿰ٷֱ� IS NOT NULL AND ic.�Ƿ񴿰ٷֱ� <> '��' and ic.�Ƿ񴿰ٷֱ� <> '��'
	
	--ֱ����������ʼ��
	INSERT INTO dbo.Import_ContractMessage
	SELECT IC.Excel�к�,IC.��ͬ���,'ZXStartDateIsNull',IC.ֱ����������ʼ��,'ֱ����������ʼ��Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� = '�̶����' 
		AND (IC.ʵ������ = '����' OR IC.ʵ������ = '��Ʒ��')
		AND (IC.ֱ����������ʼ�� IS NULL OR LEN(IC.ֱ����������ʼ��)=0)
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'ZXStartDateFormatError',IC.ֱ����������ʼ��,'ֱ����������ʼ�ո�ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� = '�̶����' 
		AND (IC.ʵ������ = '����' OR IC.ʵ������ = '��Ʒ��')
		AND IC.ֱ����������ʼ�� IS NOT NULL AND LEN(IC.ֱ����������ʼ��)>0 
		AND ISDATE(IC.ֱ����������ʼ��) = 0
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'ZXStartDateValueError',IC.ֱ����������ʼ��,'ֱ����������ʼ�ձ����ǿ�ҵ��'+IC.��ҵ��+'��������'+IC.������
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� = '�̶����' 
		AND (IC.ʵ������ = '����' OR IC.ʵ������ = '��Ʒ��')
		AND IC.ֱ����������ʼ�� IS NOT NULL AND LEN(IC.ֱ����������ʼ��) > 0
		AND ISDATE(IC.ֱ����������ʼ��) = 1
		AND ISDATE(IC.��ҵ��) = 1 
		AND dbo.fn_GetDate(IC.ֱ����������ʼ��) <> dbo.fn_GetDate(IC.��ҵ��)
		AND ISDATE(IC.������) = 1 
		AND dbo.fn_GetDate(IC.ֱ����������ʼ��) <> dbo.fn_GetDate(IC.������)	
	
	--ֱ�߳���			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'ZXConstantIsNull',IC.ֱ�߳���,'ֱ�߳���Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.������� = '�̶����' 
		AND (IC.ʵ������ = '����' OR IC.ʵ������ = '��Ʒ��')
		AND IC.ֱ����������ʼ�� IS NOT NULL 
		AND (IC.ֱ�߳��� IS NULL OR (IC.ֱ�߳��� IS NOT NULL AND LEN(IC.ֱ�߳���)=0))
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'ZXConstantIsNotNumeric',IC.ֱ�߳���,'ֱ�߳�����������'
	FROM dbo.Import_Contract AS IC
	WHERE  IC.������� = '�̶����' 
		AND (IC.ʵ������ = '����' OR IC.ʵ������ = '��Ʒ��')
		AND IC.ֱ����������ʼ�� IS NOT NULL 
		AND ic.ֱ�߳��� IS NOT NULL AND LEN(IC.ֱ�߳���)>0 AND ISNUMERIC(IC.ֱ�߳���) = 0
	
	--֧������			
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'PaymentTypeIsNull',IC.֧������,'֧������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.֧������ IS NULL	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'PaymentTypeError',IC.֧������,'֧������ȡֵ����'
	FROM dbo.Import_Contract AS IC
	WHERE ic.֧������ IS NOT NULL AND ic.֧������ NOT IN ('Ԥ��','ʵ��','�Ӹ�')	
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'PaymentTypeRatioError',IC.֧������,'֧���������������Ϊ�ٷֱ�ʱ����ѡ���Ӹ�'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������� LIKE '%�ٷֱ�%' AND ic.֧������ IS NOT NULL 
		AND ic.֧������ <>'�Ӹ�'	
		
	--��������	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleIsNull',IC.��������,'��������Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.�������� IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleError',IC.��������,'�������ڴ���'
	FROM dbo.Import_Contract AS IC
	LEFT JOIN dbo.CycleItem AS ci ON ic.�������� = ci.CycleItemName AND ic.������� LIKE '%'+ci.CycleType+'%'
	WHERE ci.CycleItemName IS NULL	
	
	--���޹���	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleTypeIsNull',IC.���޹���,'���޹���Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.���޹��� IS NULL	

	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleTypeError',IC.���޹���,'���޹�������'
	FROM dbo.Import_Contract AS IC
	WHERE ic.���޹��� IS NOT NULL AND ic.���޹��� NOT IN ('����','����')
	
	--Commented by Eric -- Begin
	----�״�DueDate
	--INSERT INTO dbo.Import_ContractMessage 
	--SELECT IC.Excel�к�,IC.��ͬ���,'FirstDueDateIsNull',IC.�״�DueDate,'�״�DueDateΪ��'
	--FROM dbo.Import_Contract AS IC
	--WHERE IC.�״�DueDate IS NULL
	
	--INSERT INTO dbo.Import_ContractMessage 
	--SELECT IC.Excel�к�,IC.��ͬ���,'FirstDueDateFormatError',IC.�״�DueDate,'�״�DueDate��ʽ����'
	--FROM dbo.Import_Contract AS IC
	--WHERE IC.�״�DueDate IS NOT NULL AND ISDATE(IC.�״�DueDate) = 0
	--Commented by Eric -- End
	
	--����ʱ���
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleStartDateIsNull',IC.ʱ��ο�ʼ,'ʱ��ο�ʼΪ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.ʱ��ο�ʼ IS NULL
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleStartDateFormatError',IC.ʱ��ο�ʼ,'ʱ��ο�ʼ��ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE IC.ʱ��ο�ʼ IS NOT NULL AND ISDATE(IC.ʱ��ο�ʼ) = 0
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleEndDateIsNull',IC.ʱ��ν���,'ʱ��ν���Ϊ��'
	FROM dbo.Import_Contract AS IC
	WHERE IC.ʱ��ν��� IS NULL
		
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'CycleEndDateFormatError',IC.ʱ��ν���,'ʱ��ν�����ʽ����'
	FROM dbo.Import_Contract AS IC
	WHERE IC.ʱ��ν��� IS NOT NULL AND ISDATE(IC.ʱ��ν���) = 0
	
	--��ʽ					
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'FormulaIsNull',IC.��ʽ,'��ʽΪ��'
	FROM dbo.Import_Contract AS IC
	WHERE ic.��ʽ IS NULL	
	
	INSERT INTO dbo.Import_ContractMessage 
	SELECT IC.Excel�к�,IC.��ͬ���,'FormulaErrorWhenFixRull',IC.��ʽ,'�������Ϊ�̶�ʱ��ʽ��������ֵ'
	FROM dbo.Import_Contract AS IC
	WHERE ic.������� LIKE '%�̶�%' AND ic.��ʽ IS NOT NULL AND ISNUMERIC(ic.��ʽ) = 0 	
	
    
END


GO


