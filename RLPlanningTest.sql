--Contract相关
select * from Contract where CHARINDEX('vc-',ContractSnapshotID)=1 order by CreateTime
select * from Contract where CompanyCode=105 order by LastModifyTime
select * from Contract where ContractSnapshotID='413224FE-5AA1-4A6E-9B85-A21344775F27' order by ContractID
select * from Contract where ContractSnapshotID='89AA9ADC-4764-4990-9FDC-14B08288C27B' OR ContractSnapshotID = '5E7E9ECA-F613-49FB-AE1C-C840DCF500E8'
select * from FixedRuleSetting
select * from EntityInfoSetting a inner join EntityInfoSetting b ON
a.EntityID = b.EntityID and a.VendorNo <> b.VendorNo
select * from Entity where EntityName like '%打浦桥%'
select * from SRLS_TB_Master_Vendor order by VendorNo
--insert into SRLS_TB_Master_Vendor values('00000', '虚拟Vendor', 'N', 'N', 'T', 'A', GETDATE())
select * from SRLS_TB_Master_Store where StoreNo=1400033--StoreName like '%打浦桥%'  order by CompanyCode
select * from SRLS_TB_Master_Kiosk order by KioskNo
select * from SRLS_TB_Master_Company
select * from Entity where StoreOrDeptNo='1400038' and KioskNo=''
select * from Contract order by Version
--删除已经登录的用户信息
delete from SRLS_TB_System_Login where OutTime IS NULL
select * from SRLS_TB_System_DictionaryKey
select * from SRLS_TB_System_DictionaryItem where KeyID='076D7E1B-C5EE-4B58-AA37-C94710410AE5'

declare @entityid nvarchar(50)
set @entityid=NEWID()--'23F0693B-6997-48CD-A7DB-AF8986DE528C'--NEWID()
exec [dbo].[RLPlanning_Contract_ValidateRentDate] @entityid, '1400041','','2000-11-14 00:00:00.000','2020-08-07 00:00:00.000'

select * into #testtbl from SRLS_TB_Master_Area
select * from #testtbl
drop table #testtbl

select * from Import_Contract
select * from Import_ContractMessage

--sales相关
exec RLPlanning_Dispatch_Sales
select * from StoreSales where StoreNo='1050002'
select * from SRLS_TB_Master_Kiosk where KioskNo = 'K133.0001'
select * from KioskSalesCollection where KioskID = '9c92649a-53a4-4316-8215-859567d67466'
select ROW_NUMBER() over(order by SalesDate), * from Forecast_Sales_ImportPart


select DATEDIFF(DAY,'2001-2-1','2001-3-1')
select DATEDIFF(DAY,'2000','2001')
select DATEADD(DAY,0,'2001-5-1')
declare @table table(t datetime)
insert into @table select '2001'
select * from @table

--权限相关
select * from UUP_TB_Module order by SortIndex;
--insert into UUP_TB_Module values (NEWID(), 
--'B00C074F-FCE0-4228-BC6E-CE7E46B78743', '预测Sales box录入',
-- 'MCD.RLPlanning.Client.ForcastSales.KeyInBox', '3021')
--btnReset 重置
select * from UUP_TB_Function where  ModuleID = '215A0F2A-A372-4F1D-BC90-EE856A2B4E3B';
--insert into UUP_TB_Function values (NEWID(), '215A0F2A-A372-4F1D-BC90-EE856A2B4E3B', '录入预测Sales', 'btnKeyInSales', '操作')
select * from UUP_TB_UserFunction;
--insert into UUP_TB_UserFunction values(NEWID(), 'CA6FCD63-D102-437D-A55E-2363B9FDF983', '054D6626-5766-4832-BF6E-A4BCF884211F')
select * from UUP_TB_Group;
select * from SRLS_TB_System_Login;
select * from SRLS_TB_Master_User where UserName = 'administrator'

WITH tmp1 AS (SELECT '甜品店' AS Type, C.CompanyCode AS CompanyCode, C.SimpleName AS CompanyName,
S.StoreNo AS StoreNo, S.StoreName AS StoreName,
K.KioskNo AS KioskNo, K.KioskName AS KioskName, 
K.Status AS Status, K.LastModifyTime AS UpdateTime
FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
RIGHT JOIN SRLS_TB_Master_Kiosk K ON S.StoreNo = K.StoreNo
WHERE K.KioskID IS NOT NULL
UNION ALL
SELECT '餐厅' AS Type, C.CompanyCode AS CompanyCode, C.SimpleName AS CompanyName,
S.StoreNo AS StoreNo, S.StoreName AS StoreName,
NULL AS KioskNo, NULL AS KioskName, 
S.Status AS Status, S.UpdateTime AS UpdateTime
FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode)
SELECT *, ROW_NUMBER() OVER(Order BY UpdateTime) AS RowIndex INTO #tmptbl FROM tmp1

SELECT * FROM SRLS_TB_Master_Store WHERE StoreNo not in(select S.StoreNo from SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
									 LEFT JOIN SRLS_TB_Master_Kiosk K ON S.StoreNo = K.StoreNo
									 WHERE K.KioskID IS NULL)

select * from SRLS_TB_Master_Kiosk WHERE KioskID NOT IN (select K.KioskID from SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
									 LEFT JOIN SRLS_TB_Master_Kiosk K ON S.StoreNo = K.StoreNo
									 WHERE K.KioskID IS NOT NULL)

declare @res int
exec RLPlanning_Sales_SelectStoreOrKiosk '餐厅', '民航', 0, 100, @res OUT
print @res

