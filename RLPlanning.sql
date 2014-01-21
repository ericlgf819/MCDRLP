--登录相关
--use SRLS
--delete from SRLS_TB_System_Login where OutTime IS NULL

select * from SRLS_TB_System_Login
select * from SRLS_TB_Master_User where EnglishName='administrator'
--模块权限相关 btnTemplateDownload
select * from UUP_TB_Module where ModuleCode like '%ContractImport%'
select * from UUP_TB_Function where ModuleID='680EAE5C-37DC-4349-AA08-C0E0D62C941C'
select * from UUP_TB_Module where ModuleCode='MCD.RLPlanning.Client.Workflow.TaskDetail'
--insert into UUP_TB_Function values(NEWID(), '680EAE5C-37DC-4349-AA08-C0E0D62C941C', '模版下载', 'btnTemplate', '操作')
select * from UUP_TB_UserFunction where UserOrGroupID='CA6FCD63-D102-437D-A55E-2363B9FDF983' AND FunctionID='024F50FC-343A-45B1-B855-92588FB48620'
--insert into UUP_TB_UserFunction values(NEWID(),'CA6FCD63-D102-437D-A55E-2363B9FDF983','024F50FC-343A-45B1-B855-92588FB48620')
select * from UUP_TB_System
exec UUP_FUN_SelectGroupFunction 'CA6FCD63-D102-437D-A55E-2363B9FDF983', 'MCD.RLPlanning.Client.Master.StoreInfo', 'SRLS'
--insert into UUP_TB_Function values(NEWID(),'F2877336-1CC5-4D7B-B887-807766DDF796','创建新合同','btnCreateContract','操作')
--insert into UUP_TB_UserFunction values(NEWID(),'CA6FCD63-D102-437D-A55E-2363B9FDF983','35ED496B-98F8-46B2-9772-3835B9D6E824')
--合同相关
--delete from VendorContract where VendorName='virtual vendor name'
select * from VendorContract where VendorName='virtual vendor name'
select * from SRLS_TB_Master_Kiosk order by TemStoreNo
select * from SRLS_TB_Master_Store where Status='A' order by CompanyCode
select * from SRLS_TB_Master_Vendor order by VendorNo
select * from SRLS_TB_Master_Store where StoreNo='1450033'
select * from VendorEntity

select * from Contract where ContractSnapshotID='vc-65572776-d132-495d-8858-6745716e8b72'
select * from Contract where CompanyCode=110 order by CreateTime desc
select * from Contract order by CreateTime desc

--合同相关信息的检查
select * from Contract where ContractSnapshotID='vc-71372c5a-97d9-4d8e-ae58-dd88fdee92de'
select * from Entity where ContractSnapshotID='vc-71372c5a-97d9-4d8e-ae58-dd88fdee92de'
select * from VendorEntity where EntityID='dff01205-fb95-4fcb-8006-25fc7b082354'
select * from VendorContract where ContractSnapshotID='vc-71372c5a-97d9-4d8e-ae58-dd88fdee92de' 
select * from EntityInfoSetting where EntityID='dff01205-fb95-4fcb-8006-25fc7b082354'
select * from FixedRuleSetting where EntityInfoSettingID='d878ad61-4f0f-4f2a-8d84-208ea203e19d'
select * from FixedTimeIntervalSetting where RuleSnapshotID='c851cbe6-67e1-423f-a03f-29b98afe4847'
---------------------------------------------------------------------------------------------------------

--update Contract set ContractNO='v110.000001', Status='已生效' where ContractSnapshotID='vc-65572776-d132-495d-8858-6745716e8b72'
--update Contract set Status='已生效' where ContractNO='v105.000002'


select * from FixedRuleSetting
select * from Contract where ContractSnapshotID='0f189520-9516-4f3f-bbac-56091a97cff8' 
select * from Contract where CompanyCode = 110 order by CreateTime desc

With tmp AS (
		SELECT C.ContractSnapshotID, C.CompanyCode, C.ContractID, C.ContractNO, C.Version, 
			C.ContractName, C.CompanyName, C.Status, C.IsLocked, 
			C.CreateTime,C.CreatorName,C.LastModifyTime, c.LastModifyUserName,
			E.EntityTypeName, E.EntityNo, E.EntityName, E.StoreOrDeptNo, 
			E.KioskNo, CONVERT(NVARCHAR(10),E.OpeningDate,25) as OpeningDate, CONVERT(NVARCHAR(10),E.RentStartDate,25) as RentStartDate, CONVERT(NVARCHAR(10),E.RentEndDate,25) as RentEndDate, VC.VendorName, 
			VC.IsVirtual, VC.VendorNo, A.ID AS AreaID, A.AreaName,
			ROW_NUMBER() OVER(ORDER BY C.CreateTime DESC) AS RowIndex
		FROM dbo.Contract AS C  
			INNER JOIN dbo.SRLS_TB_Master_Company AS CP ON C.CompanyCode = CP.CompanyCode 
			LEFT JOIN dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID 
			LEFT JOIN dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID 
			LEFT JOIN dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID AND VE.VendorNo = VC.VendorNo 
			LEFT JOIN dbo.SRLS_TB_Master_Area AS A ON CP.AreaID = A.ID
			WHERE C.SnapshotCreateTime IS NULL AND C.CompanyCode LIKE '%105%'
			)select * from tmp 

SELECT * FROM dbo.Contract AS C  
			INNER JOIN dbo.SRLS_TB_Master_Company AS CP ON C.CompanyCode = CP.CompanyCode 
			LEFT JOIN dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID 
			LEFT JOIN dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID 
			LEFT JOIN dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID AND VE.VendorNo = VC.VendorNo 
			LEFT JOIN dbo.SRLS_TB_Master_Area AS A ON CP.AreaID = A.ID 
			WHERE C.SnapshotCreateTime IS NULL AND C.CompanyCode LIKE '%105%'
			

-- 测试虚拟合同流水号
DECLARE @CurrentNumber NVARCHAR(50)

SELECT @CurrentNumber=MAX(CAST(RIGHT(ContractNo,6) AS INT)) + 1
FROM [Contract] 
WHERE (ContractNo IS NOT NULL AND  ContractNo<>'' AND CHARINDEX('v', ContractNO)=1) AND CompanyCode=105

print @CurrentNumber
----------------------------------------


select * from Contract where CHARINDEX('v', ContractNO)=1
select * from Contract where CHARINDEX('vc-', ContractSnapshotID)=1 order by CreateTime desc

--exec usp_Contract_SelectContract '', '', '', 'v'

--declare @ContractSnapshotID nvarchar(50)
--select @ContractSnapshotID='vc-8462d320-1b78-4c05-b2e6-1c78791e3edf';
--select * from fn_SelectContractKeyInfo(@ContractSnapshotID)

--select * from SYS_Attachments


--select * into #tmpcontract from Contract
--alter table #tmpcontract
--add FromSRLS BIT null default 1
--insert into #tmpcontract (ContractSnapShotID, ContractID)values (NEWID(), '0')
--select * from #tmpcontract order by CompanyCode
--drop table #tmpcontract

--给Contract增加新列
--alter table Contract
--add FromSRLS BIT not null default 1
--update Contract set FromSRLS=0 where CHARINDEX('vc-', ContractID)<>0
--select * from Contract order by FromSRLS

select * from Import_Contract
select * from Import_ContractMessage

--select * into #tmpcontract from Contract
--update #tmpcontract set Status='aa', ContractNO=dbo.fn_SerialNumber_Contract(105) where ContractSnapshotID='99725760-4514-47fe-9270-c149c6430934'
--select * from #tmpcontract where ContractSnapshotID='99725760-4514-47fe-9270-c149c6430934'
--drop table #tmpcontract

select * from SRLS_TB_Master_Store
select * from SRLS_TB_Master_Kiosk

--日志相关
select * from SRLS_TB_System_Log

--kiosk
select * from srls_tb_master_kiosk where KioskName ='L1甜品站'
--Sales
select * from StoreSales where StoreNo = 1400002
select * from SRLS_TB_Master_Store where StoreNo = 1400002
select * from SRLS_TB_Master_Kiosk where KioskNo = 'K132.0007'--KioskID = 'fdfd4730-8f46-467e-8189-0eab98706c9c'
select * from KioskSales
select * from KioskSalesCollection where KioskID='fdfd4730-8f46-467e-8189-0eab98706c9c' order by ZoneStartDate
select * from Import_Sales
select * from Forecast_Sales
exec RLPlanning_Import_Sales
declare @KioskNo nvarchar(50);
select @KioskNo = KioskNo from SRLS_TB_Master_Kiosk where KioskName='随便'
select * from SRLS_TB_Master_Kiosk k1 inner join SRLS_TB_Master_Kiosk k2 on k1.KioskNo = k2.KioskNo and k1.KioskID <> k2.KioskID

select * from Import_SalesMessage


--
--根据KioskName获取KioskNo
SELECT @KioskNo = KioskNo FROM SRLS_TB_Master_Kiosk WHERE KioskName = @KioskName

declare @tmptbl table([1月] nvarchar(50))
insert into @tmptbl values ('a')
select * from @tmptbl

declare @tstDate0 date, @tstDate1 date
set @tstDate0 = '2012-8-1'
set @tstDate1 = '2012'

if day(@tstDate0) = 1
print day(@tstDate0)

print dbo.RLPlanning_FN_GetSalesByYearAndMonth(1200002, '', 2012, 1)

select Status from SRLS_TB_Master_Company group by Status

declare @kiosk nvarchar(50)
set @kiosk = ''
set @kiosk = null
if @kiosk = null
print 'yeah'
select @kiosk = Kiosk from Import_Sales
if @kiosk = ''
print 'yeah'

select * from Forecast_Sales order by StoreNo
select * from Import_SalesMessage
select * from StoreSales where StoreNo = '1050002'
select * from KioskSalesCollection where FromSRLS = 0

select * from SRLS_TB_Master_Store where StoreNo = '1050002'
select * from SRLS_TB_Master_Kiosk
select * from TypeCode
select * from GLRecord order by ContractSnapshotID
select * from Entity where KioskNo <>''

select * from Contract where ContractNO='v105.000011'
select * from Entity where ContractSnapshotID='B8DFF87E-BC76-4230-BF59-FC83BAED4728'
select * from EntityInfoSetting where EntityID='A076CC33-CCD4-41EC-9536-138D7903FFB3'
select * from FixedRuleSetting where EntityInfoSettingID='56AD8B5F-3502-4666-9D72-4089826AF07B'

select * from SRLS_TB_Master_Store where CompanyCode=107

select * from GLRecord where ContractNO='v105.000011' --where RuleID='85D71057-1FE5-4C77-9EFD-7C1F51F846AF'
--delete from GLRecord where ContractNO='v105.000011'
--delete from GLProcessParameterValue where GLRecordID IN (select GLRecordID from GLRecord where ContractNO='v105.000011')
select * from GLException order by CreateTime desc

select * from FixedRuleSetting where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1'

--exec RLPlanning_Cal_FixedGL 'E17FB008-CD5E-4A61-9071-3824F49E4FD1', '2013-7-9'
select * from GLRecord where FromSRLS = 0 order by CycleEndDate
select * from GLException where GLRecordID IN (select GLRecordID from GLRecord where FromSRLS = 0)
select * from GLMessageResult where GLRecordID IN (select GLRecordID from GLRecord where FromSRLS = 0)
select * from GLCheckResult where GLRecordID IN (select GLRecordID from GLRecord where FromSRLS = 0)

DECLARE @RuleSnapshotID nvarchar(50), @RuleID nvarchar(50)
set @RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1'

select dbo.fn_GetRuleCreateTime(@RuleSnapshotID)

select * from dbo.fn_Cal_GetGLALLStartEndDateInfo(@RuleSnapshotID,'实际')
select * from fn_Cal_GetRuleAPGLNextStartEndDate(@RuleSnapshotID)

--EXEC dbo.usp_Cal_UpdateFixedAPGLNextStartEndDate @RuleSnapshotID
select * from GLRecord where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1'
select * from GLException where GLRecordID IN (select GLRecordID from GLRecord where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1')



--删除错误的GLRecord用
--DECLARE @MyCursor CURSOR, @GLRecordID nvarchar(50)
--SET @MyCursor = CURSOR SCROLL FOR SELECT GLRecordID FROM GLRecord WHERE FromSRLS = 0
--OPEN @MyCursor
--FETCH NEXT FROM @MyCursor INTO @GLRecordID
--WHILE @@FETCH_STATUS = 0
--BEGIN
--	exec usp_Cal_DeleteOneGLRecord @GLRecordID
--	FETCH NEXT FROM @MyCursor INTO @GLRecordID
--END
--CLOSE @MyCursor
--DEALLOCATE @MyCursor

select * from RentType
select * from GLRecord where FromSRLS = 0 order by GLType, CycleStartDate

select * from Contract where ContractNO = 'v105.000014'
select * from Entity where ContractSnapshotID='ADEEB60A-35D5-4974-8BE8-FA86290D370E'
select * from EntityInfoSetting where EntityID='05F53E77-A987-4C69-9162-BD5B0523D4AE'
select * from RatioRuleSetting where EntityInfoSettingID='BDC45A98-5460-430E-869F-CA2BA33F7134'
select * from RatioCycleSetting where RatioID='40bb66f3-844a-4b90-a491-775695647d7a'

--exec RLPlanning_Cal_RatioGL '39c2e238-f812-447a-9d29-aa7eb689c716', '2014-7-9'
select * from GLRecord where RuleSnapshotID='39c2e238-f812-447a-9d29-aa7eb689c716'
select * from GLException where GLRecordID IN (select GLRecordID from GLRecord where RuleSnapshotID='39c2e238-f812-447a-9d29-aa7eb689c716')
select * from GLMessageResult where GLRecordID IN (select GLRecordID from GLRecord where RuleSnapshotID='39c2e238-f812-447a-9d29-aa7eb689c716')
select * from GLCheckResult where GLRecordID IN (select GLRecordID from GLRecord where RuleSnapshotID='39c2e238-f812-447a-9d29-aa7eb689c716')

		SELECT * FROM dbo.GLRecord gl
		INNER JOIN dbo.GLTimeIntervalInfo info ON gl.GLRecordID = info.GLRecordID
		WHERE gl.GLRecordID='5EBEF657-3C28-4655-B1C9-C78C91BE56B6'
		AND (gl.EntityTypeName='甜品店' OR gl.EntityTypeName='餐厅')
		AND info.KioskEstimateSales = 0

select * from GLTimeIntervalInfo where GLRecordID='5EBEF657-3C28-4655-B1C9-C78C91BE56B6'
select * from GLProcessParameterValue where GLRecordID='5EBEF657-3C28-4655-B1C9-C78C91BE56B6' order by SortIndex

	SELECT 	e.StoreOrDeptNo,e.KioskNo
	FROM dbo.GLRecord gl INNER JOIN dbo.Entity e ON gl.EntityID = e.EntityID
			INNER JOIN dbo.EntityInfoSetting eis ON gl.EntityInfoSettingID = eis.EntityInfoSettingID
			INNER JOIN dbo.RatioCycleSetting c ON gl.RuleSnapshotID=c.RuleSnapshotID
	WHERE gl.GLRecordID='5EBEF657-3C28-4655-B1C9-C78C91BE56B6'
	
select * from KioskStoreRelationChangHistory where StoreNo='1450126'


select * from CashCloseInfo
select * from ClosedRecord

select RentType from GLRecord group by RentType

--exec RLPlanning_Cal_ZXGL 'E17FB008-CD5E-4A61-9071-3824F49E4FD1', '2013-7-9'
--select * from GLRecord where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1'
select * from SRLS_TB_Master_Area
select * from SRLS_TB_Master_Company
select * from SRLS_TB_Master_Store
select * from SRLS_TB_Master_Kiosk
select * from UserArea
select * from SRLS_TB_Master_User where UserName = 'administrator'

--DECLARE @UserID nvarchar(50), @Where nvarchar(max)
--set @Where=''
--set @UserID='C75C2715-5E8E-4750-9522-C1DCBC2D8C25'
----将该用户相关的Area条件都放在表变量中
----'AND (AreaName='xxx' OR AreaName='yyy' OR ...) '这种形式
--DECLARE @WhereAreaNameConditionTbl TABLE(Condition nvarchar(max), [Index] INT IDENTITY(1,1))
--INSERT INTO @WhereAreaNameConditionTbl
--SELECT 'AreaName='''+ A.AreaName + ''' ' 
--FROM SRLS_TB_Master_Area A INNER JOIN UserArea UA ON A.ID = UA.AreaID
--WHERE UA.UserID = @UserID

--DECLARE @AreaSum int
--SELECT @AreaSum=COUNT(*) FROM @WhereAreaNameConditionTbl

--IF @AreaSum = 1
--BEGIN
--	UPDATE @WhereAreaNameConditionTbl SET Condition='AND ' + Condition WHERE [INDEX]=1
--END

--IF @AreaSum > 1
--BEGIN
--	UPDATE @WhereAreaNameConditionTbl SET Condition='OR ' + Condition WHERE [INDEX]<>1 AND [INDEX]<>@AreaSum
--	UPDATE @WhereAreaNameConditionTbl SET Condition='AND (' + Condition WHERE [INDEX]=1
--	UPDATE @WhereAreaNameConditionTbl SET Condition='OR ' + Condition +') ' WHERE [INDEX]=@AreaSum
--END


----读取表变量中的Area条件，来构建Where条件
--DECLARE @AreaCursor CURSOR, @AreaCondition nvarchar(max)
--SET @AreaCursor = CURSOR SCROLL FOR SELECT Condition FROM @WhereAreaNameConditionTbl
--OPEN @AreaCursor
--FETCH NEXT FROM @AreaCursor INTO @AreaCondition
--WHILE @@FETCH_STATUS=0
--BEGIN
--	SET @Where += @AreaCondition
--	FETCH NEXT FROM @AreaCursor INTO @AreaCondition
--END
--CLOSE @AreaCursor
--DEALLOCATE @AreaCursor

--select * from @WhereAreaNameConditionTbl
--print @Where

select * from GLRecord where FromSRLS=0 order by GLType, CycleStartDate

select StartDate from FixedTimeIntervalSetting where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1'

select StartDate from RatioTimeIntervalSetting where RuleSnapshotID='39c2e238-f812-447a-9d29-aa7eb689c716'

select * from GLRecord where RuleSnapshotID='E17FB008-CD5E-4A61-9071-3824F49E4FD1' order by CycleStartDate
print dbo.fn_GetRuleCreateTime('E17FB008-CD5E-4A61-9071-3824F49E4FD1')

select * from Entity
select * from FixedRuleSetting order by ZXStartDate
select * from Contract order by SnapshotCreateTime

declare @tstdata date, @nullval date
set @tstdata='2013-1-1'
set @nullval = [dbo].[fn_GetMonthFirstDate](@nullval)
if @nullval is null
print 'yooo'

set @nullval = dbo.fn_GetMaxDate(null, @tstdata)
if @nullval is null
print 'null again'


select * from KioskStoreRelationChangHistory
select * from SRLS_TB_Master_Kiosk where KioskNo='K133.0001'

select * from Entity
select * from Contract

select * from KioskSalesCollection K1 INNER JOIN KioskSalesCollection K2
ON K1.KioskID = K2.KioskID AND K1.CollectionID <> K2.CollectionID 
AND MONTH(K1.ZoneStartDate)=MONTH(K2.ZoneStartDate)
AND YEAR(K1.ZoneStartDate)=YEAR(K2.ZoneStartDate)

select * from SRLS_TB_Matters_Attach
select C.ContractNO from SYS_Attachments S INNER JOIN Contract C
ON S.ObjectID = C.ContractSnapshotID
WHERE S.Category='Contract'

select * from UserArea order by UserID
select * from srls_tb_master_area
select * from SRLS_TB_Master_Company
select * from SRLS_TB_Master_Store
select * from SRLS_TB_Master_Kiosk

select * from SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode=C.CompanyCode
INNER JOIN SRLS_TB_Master_Area A ON C.AreaID=A.ID

--exec RLPlanning_Select_SalesTemplate '1FD39FB3-CB62-4C38-AF43-CDB265530374'

select /*distinct SalesDate*/* from StoreSales where IsCASHSHEETClosed=0 order by StoreNo, SalesDate


declare @t date, @v date
set @t='2011-12-1'
set @v='2012-1-31'
print datediff(month, @t, @v)
set @t-=1
print @t

select * from  Forecast_Sales where StoreNo='1050002' AND KioskNo IS NULL AND SalesDate BETWEEN '2012-1-1' AND '2012-12-1'

DECLARE @StoreNo nvarchar(50), @StartRealSalesDate datetime
SET @StoreNo='1050002'
SET @StartRealSalesDate='2012-01-01'

SELECT @StoreNo, NULL, @StartRealSalesDate, SUM(Sales)
FROM StoreSales 
WHERE SalesDate BETWEEN @StartRealSalesDate AND DATEADD(DAY, -1, DATEADD(MONTH,1,@StartRealSalesDate))
AND StoreNo=@StoreNo

exec RLPlanning_Sales_Restaurant2RealSales '1400002'
exec RLPlanning_Sales_Kiosk2RealSales '3450028', 'K124.0003'

truncate table RLPlanning_RealSales
select * from StoreSales WHERE StoreNo='1400002' AND FromSRLS=1 
select * from RLPlanning_RealSales
select * from KioskSalesCollection where KioskID='1448b394-2271-469d-a0b8-df495c19933d' order by ZoneStartDate

select * from SRLS_TB_Master_Kiosk where KioskID='1448b394-2271-469d-a0b8-df495c19933d'

select KSC.Sales + CASE WHEN KS.Sales IS NULL THEN 0 ELSE KS.Sales END, * from KioskSalesCollection KSC LEFT JOIN KioskSales KS ON KSC.CollectionID = KS.CollectionID
where KSC.KioskID='1448b394-2271-469d-a0b8-df495c19933d'
order by KSC.ZoneStartDate

declare @KioskID nvarchar(50)
declare @StartRealSalesDate datetime
set @KioskID='1448b394-2271-469d-a0b8-df495c19933d'
set @StartRealSalesDate='2011-11-1'
SELECT KC.Sales + CASE WHEN K.Sales IS NULL THEN 0 ELSE K.Sales END, *
FROM KioskSalesCollection KC LEFT JOIN KioskSales K ON KC.CollectionID = K.CollectionID
WHERE FromSRLS=1 AND KC.KioskID = @KioskID
AND KC.ZoneStartDate BETWEEN @StartRealSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@StartRealSalesDate))

--SELECT SUM(KC.Sales)
--FROM KioskSalesCollection KC
--WHERE FromSRLS=1 AND KC.KioskID = @KioskID
--AND KC.ZoneStartDate BETWEEN @StartRealSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@StartRealSalesDate))

SELECT SUM(K.Sales)
FROM KioskSalesCollection KC INNER JOIN KioskSales K ON KC.CollectionID = K.CollectionID
WHERE FromSRLS=1 AND KC.KioskID = @KioskID
AND KC.ZoneStartDate BETWEEN @StartRealSalesDate AND DATEADD(DAY,-1,DATEADD(MONTH,1,@StartRealSalesDate))


declare @StartRealSalesDate datetime
set @StartRealSalesDate='2011-9-21'
print LTRIM(RTRIM(STR(YEAR(@StartRealSalesDate))))+'-'+LTRIM(RTRIM(STR(MONTH(@StartRealSalesDate))))
IF DAY(@StartRealSalesDate)<>1
	SET @StartRealSalesDate=STR(YEAR(@StartRealSalesDate))+'-'+STR(MONTH(@StartRealSalesDate))+'-1'

print @StartRealSalesDate	

select * from Forecast_Sales
select * from StoreSales where FromSRLS=1 and StoreNo='1050002'

update Forecast_Sales set StoreNo='1400002' where StoreNo='1200002'

select * from Forecast_Sales where StoreNo='1400002'
select * from StoreSales WHERE StoreNo='1400002' AND FromSRLS=1 
select * from RLPlanning_RealSales
select * from Forecast_Sales where StoreNo='1400002'
delete from Forecast_Sales where StoreNo=1400002 and SalesDate < '2012-5-1'

select * from dbo.RLPlanning_FUN_BuildImportStyleSalesRecord('2012-4-1', '1400002', null)

select * from SRLS_TB_Master_Kiosk
select * from SRLS_TB_Master_Store

select * from StoreSales where StoreNo='1400002'


--declare @storenocursor cursor, @storeno nvarchar(50)
--set @storenocursor = cursor scroll for
--select StoreNo from SRLS_TB_Master_Store
--open @storenocursor
--fetch next from @storenocursor into @storeno
--while @@FETCH_STATUS = 0
--begin
--	exec RLPlanning_Sales_Restaurant2RealSales @storeno
--	fetch next from @storenocursor into @storeno
--end
--close @storenocursor
--deallocate @storenocursor
declare @tbl table(col int)
insert into @tbl select 1
insert into @tbl select null
select * from @tbl
select SUM(col) from @tbl


select * from SRLS_TB_Master_Kiosk where KioskNo is null

--declare @storenocursor cursor, @storeno nvarchar(50), @kioskno nvarchar(50)
--set @storenocursor = cursor scroll for
--select S.StoreNo, K.KioskNo from SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Kiosk K ON S.StoreNo=K.StoreNo
--where K.KioskNo is not null
--open @storenocursor
--fetch next from @storenocursor into @storeno, @kioskno
--while @@FETCH_STATUS = 0
--begin
--	exec RLPlanning_Sales_Kiosk2RealSales @storeno, @kioskno
--	fetch next from @storenocursor into @storeno, @kioskno
--end
--close @storenocursor
--deallocate @storenocursor

select * from RLPlanning_RealSales WHERE KioskNo='K120.0011' order by SalesDate desc
select * from KioskSales KS inner join SRLS_TB_Master_Kiosk K ON KS.KioskID = K.KioskID where K.KioskNo='K120.0011' order by KS.ZoneStartDate
select * from KioskSalesCollection KS inner join SRLS_TB_Master_Kiosk K ON KS.KioskID = K.KioskID where K.KioskNo='K120.0011' order by KS.ZoneStartDate

select * from RlPlanning_RealSales where StoreNo='1050002'
select * from StoreSales where StoreNo='1950243'
select StoreNo from RlPlanning_RealSales group by StoreNo
select StoreNo from SRLS_TB_Master_Store where StoreNo not in (select StoreNo from RlPlanning_RealSales group by StoreNo) order by StoreNo
select COUNT(StoreNo), StoreNo from StoreSales where IsCASHSHEETClosed=1 AND FromSRLS=1 AND StoreNo in (select StoreNo from SRLS_TB_Master_Store where StoreNo not in (select StoreNo from RlPlanning_RealSales group by StoreNo))group by StoreNo

exec RLPlanning_Sales_SelectStoreOrKiosk '', ''

select * from StoreSales where StoreNo='1960057'
select * from RLPlanning_RealSales where StoreNo='1960057'

select * from StoreSales where Sales is null
select * from SRLS_TB_System_Log order by LogTime desc

select * from StoreSales where StoreNo='1400146'
select * from RLPlanning_RealSales where StoreNo='1400146' AND KioskNo is null
exec RLPlanning_Sales_Restaurant2RealSales '1400146'

select * from Forecast_CheckResult

declare @t1 nvarchar(50), @d1 date, @t2 nvarchar(50)
set @t2 = '20120101'
set @t1 = '30001.'
set @d1 = '2012-1-1'
set @t1 = cast(@d1 as nvarchar(50))
set @t1 = SUBSTRING(@t1,1,4) + SUBSTRING(@t1,6,2) + SUBSTRING(@t1,9,2)
print len(@t1)
if @t1=@t2
	print 'yeah'
print @t1
print charindex('.', @t1)
print substring(@t1,1,5)

select * from Forecast_CheckResult
insert into Forecast_CheckResult
select '1','20120716.002222','1','1','1','1','1','1','1','1',GETDATE(),'1','1',NEWID()
print dbo.RLPlanning_CalTaskNo(getdate())

select * from RLPlanning_RealSales where KioskNo is not null

select Status from Contract group by Status

----合同状态修改--Begin
----TODO
--select * from SRLS_TB_System_DictionaryKey
--select * from SRLS_TB_System_DictionaryItem  where KeyID='80C91A22-0DFC-4B2F-9C53-7D11702CAF45'
----合同状态修改--End

WITH Tmp AS(SELECT CASE WHEN FC.IsRead=1 THEN '已读' ELSE '未读' END AS IsRead,
CASE WHEN FC.TaskType=1 THEN 'E' ELSE 'I' END AS TaskType,
FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
CASE WHEN FC.TaskType=1 THEN '问题修复' ELSE '-' END AS Operation,
C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark
FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID)SELECT * FROM Tmp

WITH Tmp AS(SELECT CASE WHEN FC.TaskType=1 THEN 'E' ELSE 'I' END AS TaskType,
FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.FixUserID, FC.CreateTime AS FinishTime,
C.CompanyCode, A.AreaName
FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID)SELECT * FROM Tmp

alter table Forecast_CheckResult
add TaskFinishTime datetime null default null

INSERT INTO Forecast_CheckResult
SELECT NEWID(), dbo.RLPlanning_CalTaskNo(GETDATE()), 1, 'Sales数据不全', '10700091', null,
null, null, null, null, GETDATE(), 0, 0, '81F72072-EDAE-4BA0-A52A-0A4DA07DA6A0', null, '2013-3-8'

select * from Forecast_CheckResult
select * from SRLS_TB_Master_Store
select * from SRLS_TB_Master_User

exec RLPlanning_Task_SelectUnFinishedTasks null, null, null, null, null, null, 1, '2602DA1E-C202-497A-B678-4A1FAFD71EFB'
exec RLPlanning_Task_SelectFinishedTasks '北区', null, null, null, null, 1, '2602DA1E-C202-497A-B678-4A1FAFD71EFB'
select * from SRLS_TB_Master_User where UserName='administrator'

--update Forecast_CheckResult set ContractNo='108.000002' where CheckID='8331D301-EA2F-4F5C-9A76-474A6CCEE9F8'
select * from UserArea
select * from SRLS_TB_Master_Company

select * from SRLS_TB_Master_Area A INNER JOIN UserArea UA ON A.ID = UA.AreaID
INNER JOIN SRLS_TB_Master_User U ON UA.UserID = U.ID
WHERE U.UserName = 'administrator'

SELECT E.StoreOrDeptNo, E.KioskNo FROM Entity E 
INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
INNER JOIN SRLS_TB_Master_Store S ON E.StoreOrDeptNo = S.StoreNo
WHERE C.SnapshotCreateTime IS NULL AND C.Status='已生效'

select * from dbo.RLPlanning_FN_GetRentStartEndDate('1400024', 'K132.0005')
select * from dbo.RLPlanning_FN_GetSalesStartEndDate('1400024', '')

DECLARE @RentStartDate datetime, @RentEndDate datetime, @SalesStartDate datetime, @SalesEndDate datetime
DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
SET @StoreNo = '1400024'
SET @KioskNo = 'K132.0005'

SELECT @RentStartDate = RentStartDate, @RentEndDate = RentEndDate 
FROM dbo.RLPlanning_FN_GetRentStartEndDate(@StoreNo, @KioskNo)

SELECT @SalesStartDate = SalesStartDate, @SalesEndDate = SalesEndDate 
FROM dbo.RLPlanning_FN_GetSalesStartEndDate(@StoreNo, @KioskNo)

SELECT A.CompanyCode, A.CompanyName, S.StoreNo, '甜品店' AS EntityType, K.KioskName AS EntityName,
@RentStartDate AS RentStartDate, @RentEndDate AS RentEndDate, 
@SalesStartDate AS SalesStartDate, @SalesEndDate AS SalesEndDate
FROM SRLS_TB_Master_Store S 
INNER JOIN SRLS_TB_Master_Company A ON S.CompanyCode = A.CompanyCode
INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
WHERE S.StoreNo = @StoreNo AND K.KioskNo = @KioskNo

SELECT S.StoreNo, K.KioskNo, K.KioskName 
FROM SRLS_TB_Master_Store S 
INNER JOIN SRLS_TB_Master_Kiosk K ON K.StoreNo = S.StoreNo
WHERE S.CompanyCode IN (SELECT CompanyCode FROM RLPlanning_Cal_TmpCompanyCodeTbl)

exec RLPlanning_Cal_SelectEntityByNameOrNo '','中关村','',''
select * from dbo.RLPlanning_FN_Cal_GetSelectStoreOrKiosk(null,null) order by StoreNo

declare @StoreNo nvarchar(50), @StoreName nvarchar(200)
SET @StoreName = '中关村'
SET @StoreNo = null
select * from SRLS_TB_Master_Store where StoreNo like '%' + @StoreNo + '%' OR StoreName like '%' + @StoreName + '%'
select * from RLPlanning_Cal_TmpCompanyCodeTbl
select * from RLPlanning_Cal_TmpTbl

select * from Forecast_CheckResult order by CreateTime

SELECT C.CompanyCode, C.CompanyName, S.StoreNo, 
CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN '餐厅' ELSE '甜品店' END AS EntityType,
CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN S.SimpleName ELSE K.KioskName END AS EntityName,
FC.ErrorType,
CASE WHEN FC.ErrorType='合同缺失' OR FC.ErrorType='合同期限不全' THEN '修改合同' 
WHEN FC.ErrorType='Sales数据不全' THEN '录入销售数据' ELSE NULL END AS Operation
FROM Forecast_CheckResult FC
INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo=S.StoreNo
INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode=C.CompanyCode
LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
WHERE FC.IsSolved=0
ORDER BY FC.StoreNo


		SELECT DISTINCT AreaName FROM (
			SELECT C.AreaID, A.AreaName, A.ShowOrder, C.CompanyCode, C.CompanyName 
			FROM (  
					SELECT ISNULL(AreaID, '4AD15C85-ADEC-4348-9A15-9DCC2B1C5D41') AS AreaID,
					CompanyCode, CompanyName 
					FROM SRLS_TB_Master_Company
				 ) AS C 
			INNER JOIN SRLS_TB_Master_Area AS A ON A.ID=C.AreaID 
			WHERE (
					C.CompanyCode IN (
										SELECT CompanyCode FROM RLP_UserCompany WHERE UserId='2602DA1E-C202-497A-B678-4A1FAFD71EFB')
									 )
				AND (
						C.CompanyCode IN 
						(SELECT CompanyCode FROM SRLS_TB_Master_Company WHERE [Status]='A')
					)
	   ) AS AA



select * from SRLS_TB_Master_Area
select distinct CompanyCode from rlp_usercompany  order by CompanyCode
select * from SRLS_TB_Master_User where UserName='administrator'
select * from SRLS_TB_Master_Company

select * from rlp_usercompany
delete from rlp_usercompany where UserId='2602DA1E-C202-497A-B678-4A1FAFD71EFB'
insert into rlp_usercompany
select '2602DA1E-C202-497A-B678-4A1FAFD71EFB', CompanyCode from SRLS_TB_Master_Company

print dbo.RLPlanning_FN_GetAreaSqlConditionStr('2602DA1E-C202-497A-B678-4A1FAFD71EFB', 1)

select * from Forecast_CheckResult where CheckID='7E84B3CC-DB55-4D57-BF69-5A234ECEF7F7'
select * from Forecast_CheckResult where UserOrGroupID='2602DA1E-C202-497A-B678-4A1FAFD71EFB' 
and IsSolved = 0

exec RLPlanning_Sales_SelectStoreOrKiosk '', '', '', '', '2602DA1E-C202-497A-B678-4A1FAFD71EFB', '', 1, 60


select * from Contract where ContractNO='117.000010'
select * from SRLS_TB_Master_Store
exec RLPlanning_Select_SalesTemplate '2602DA1E-C202-497A-B678-4A1FAFD71EFB'
select * from Forecast_Sales where StoreNo = '1980063' and KioskNo is null order by StoreNo, SalesDate

select * from RLPlanning_RealSales where StoreNo = '1980063' and KioskNo is null order by StoreNo, SalesDate

select * from Forecast_CheckResult where IsSolved = 1
select * from RLP_UserCompany RU INNER JOIN SRLS_TB_Master_User U ON RU.UserId=U.ID
where U.EnglishName='administrator'

select * from RLP_UserCompany where UserId='2602DA1E-C202-497A-B678-4A1FAFD71EFB'

select * from Entity


select * from SRLS_TB_Master_Store where CompanyCode=105

select * from SRLS_TB_Master_Store where CompanyCode like '%' + '' + '%'
select * from SRLS_TB_Master_Store

declare @num nvarchar(50)
set @num = 3

BEGIN TRY
set @num -= 1
if @num <= 2
print 'a'
END TRY
BEGIN CATCH
print 'b'
END CATCH

declare @last nvarchar(50)
declare @cur nvarchar(50)

set @cur = 'FA3EEE26-B959-47C8-99FE-75D97DF17D9E'
exec RLPlanning_UTIL_GetContractLastVersionSnapshotID @cur, @last output
select * from Contract where ContractSnapshotID=@cur
select * from Contract where ContractSnapshotID=@last

select * from Contract where ContractID='BE2F7E36-7B6B-444C-9DF9-D8E2EF1DB05D'

select RentType from FixedRuleSetting group by RentType
select RentType from RatioRuleSetting group by RentType
select * from Entity 

select * from ConditionAmount where ConditionDesc IS NOT NULL AND SQLCondition IS NULL

select RentType from GLRecord group by RentType
select GLType from GLRecord group by GLType

select * from RatioTimeIntervalSetting

select E1.KioskNo, E2.KioskNo from Entity E1 INNER JOIN Entity E2
ON E1.EntityID='0E928FDC-D3B2-404E-9CC5-B3658DFDAC2B' AND E2.EntityID='5F1A9430-8DFD-4030-B613-F8735AF429AC'
WHERE ISNULL(E1.KioskNo,'') = ISNULL(E2.KioskNo,'')

select * from Entity order by ContractSnapshotID

select E1.EntityID, E2.EntityID from Entity E1 INNER JOIN Entity E2
ON E1.ContractSnapshotID='012F1CC5-13E1-4E13-A883-4DBCD9BD7D39' AND E2.ContractSnapshotID='225F4B2D-1B65-49E0-A4E1-C7DC0F49DE1A'

declare @res int
exec RLPlanning_UTIL_WhetherRatioRuleModified 'cfd6dc1c-63ac-404f-b660-934d894548cb', 'cfd6dc1c-63ac-404f-b660-934d894548cb', '百分比租金', @res output
print @res

select * from EntityInfoSetting
declare @t bit
print ISNULL(@t,'')

select * from RatioCycleSetting

select * from RatioRuleSetting R inner join EntityInfoSetting E ON R.EntityInfoSettingID=E.EntityInfoSettingID
where E.EntityInfoSettingID='d87fcaad-74c2-4fea-aa80-473dce2cb294'

select * from SRLS_TB_Master_Area
select * from Contract

declare @out int
exec RLPlanning_Report_SelectChange '', '', '', '', '', 0, 50, @out output, '256EF07B-8490-4BC6-B8D1-3395FECBBD5D'
print @out

declare @out int
exec RLPlanning_Report_SelectChange '', '', '1955-1-1', '2055-1-1', '', 0, 50, @out output, NULL
print @out

select RentType from GLRecord group by RentType
select GLType from GLRecord group by GLType
select RentTypeName from TypeCode group by RentTypeName

--delete from RLP_UserCompany where UserId='2602DA1E-C202-497A-B678-4A1FAFD71EFB'
--insert into RLP_UserCompany
--select '2602DA1E-C202-497A-B678-4A1FAFD71EFB', CompanyCode from SRLS_TB_Master_Company

delete from RLP_UserCompany where ID=3500 


select * from SRLS_TB_Master_User where UserName='administrator'

exec RLPlanning_Sales_SelectStoreOrKiosk '','','','','2602da1e-c202-497a-b678-4a1fafd71efb','',0,60

if dbo.RLPlanning_FN_GetAreaSqlConditionStr('2602da1e-c202-497a-b678-4a1fafd71efb', 1) = ''
print 'a'


		
declare @uniq uniqueidentifier, @areaid uniqueidentifier
set @areaid='00000000-0000-0000-0000-000000000000'
set @uniq = '2602da1e-c202-497a-b678-4a1fafd71efb'
exec [dbo].[usp_Contract_SelectContract] '', '', '', '', @areaid, '', @uniq, 0, 60

select * from Forecast_Sales



exec [dbo].[RLPlanning_Report_SelectSales] '', '105', '2012', '餐厅', 0, 60

declare @StoreNo nvarchar(50)
set @StoreNo=''
SELECT StoreNo, NULL FROM SRLS_TB_Master_Store WHERE StoreNo LIKE '%'+@StoreNo+'%'	


select StoreNo, KioskNo, Year(SalesDate) AS Year from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo
order by StoreNo



print [dbo].[RLPlanning_FN_ReportGetCashCloseSales]('1950060', '', '', '')

DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
set @StoreNo='1450126'
 SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							NULL AS Kiosk, VS.Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12月] FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from Forecast_SalesHistory group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '') WHERE VS.StoreNo = @StoreNo 

select * from Forecast_SalesHistory


exec [dbo].[RLPlanning_Report_SelectSales] '', '105', '', '', 0, 0, 60

select StoreNo, KioskNo, Year(SalesDate) from Forecast_SalesHistory group by StoreNo, KioskNo, Year(SalesDate)
order by StoreNo, KioskNo

select * from Forecast_SalesHistory where StoreNo='1050002' OR StoreNo='V001585'

select * from Forecast_Sales where StoreNo='1050002' OR StoreNo='V001585' order by StoreNo, SalesDate
select * from RLPlanning_RealSales where StoreNo='1050002' OR StoreNo='V001585'


print dbo.RLPlanning_FN_ReportGetSales('1050002', '', '', '')

DECLARE @StoreNo nvarchar(50), @KioskNo nvarchar(50)
set @StoreNo='1050002'
SELECT DISTINCT S.CompanyCode AS Company, @StoreNo AS 餐厅编号, S.StoreName AS Store,
							NULL AS Kiosk, VS.Year AS 年度, 
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 1, NULL) AS [1月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 2, NULL) AS [2月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 3, NULL) AS [3月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 4, NULL) AS [4月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 5, NULL) AS [5月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 6, NULL) AS [6月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 7, NULL) AS [7月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 8, NULL) AS [8月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 9, NULL) AS [9月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 10,NULL) AS [10月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 11, NULL) AS [11月],
							dbo.RLPlanning_FN_GetSalesByYearAndMonth(@StoreNo, @KioskNo, VS.Year, 12, NULL) AS [12月] FROM SRLS_TB_Master_Store S 
						INNER JOIN 
						(select StoreNo, KioskNo, Year(SalesDate) AS Year 
						from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) VS 
						ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '') WHERE VS.StoreNo = @StoreNo 

print dbo.RLPlanning_FN_GetSalesByYearAndMonth('1050002', NULL, 2012, 4, NULL)


SELECT * FROM SRLS_TB_Master_Store S 
INNER JOIN 
(select StoreNo, KioskNo, Year(SalesDate) AS Year 
from v_RLPlanning_Sales group by Year(SalesDate), StoreNo, KioskNo) VS 
ON S.StoreNo = VS.StoreNo AND (VS.KioskNo IS NULL OR VS.KioskNo = '') WHERE VS.StoreNo = '1050002' 

select * from SRLS_TB_Master_Store where Status='A'

select K.StoreNo, S.StoreName from SRLS_TB_Master_Kiosk K INNER JOIN SRLS_TB_Master_Store S
ON K.StoreNo = S.StoreNo
WHERE S.CompanyCode = 105

select * from SRLS_TB_Master_Company 

select * from entity

select * from Forecast_CheckResult order by CalEndDate desc
 
exec RLPlanning_Task_SelectUnFinishedTasks NULL, NULL, NULL, NULL, NULL, NULL, 3, '2602da1e-c202-497a-b678-4a1fafd71efb'
select * from Contract

select * from Forecast_CheckResult where StoreNo = '1450039' order by CreateTime desc
select * from RLPlanning_Cal_TmpTbl

select * from GLRecord where FromSRLS=0 order by CreateTime desc;


WITH Tmp AS(SELECT CASE WHEN FC.IsRead=1 THEN '已读' ELSE '未读' END AS IsRead,
				CASE WHEN FC.TaskType=1 THEN 'E' ELSE 'I' END AS TaskType,
				FC.StoreNo, FC.KioskNo, FC.ErrorType, FC.CreateTime,
				CASE WHEN FC.TaskType=1 THEN '问题修复' ELSE '-' END AS Operation,
				C.CompanyCode, A.AreaName, FC.CheckID, FC.TaskNo, FC.Remark, FC.IsSolved, 
				FC.CalEndDate, FC.ContractNo, UC.UserId AS UserID, FC.ContractSnapShotID AS ContractSnapShotID,
				FC.EntityID AS EntityID, C.CompanyName AS CompanyName,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN '餐厅' ELSE '甜品店' END AS EntityType,
				CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN S.SimpleName ELSE K.KioskName END AS EntityName
				FROM Forecast_CheckResult FC INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo = S.StoreNo
				INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
				LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
				INNER JOIN SRLS_TB_Master_Area A ON C.AreaID = A.ID
				INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode)SELECT * FROM Tmp 

select * from SRLS_TB_Master_Store where StoreNo='V00080'

select * from Forecast_CheckResult where StoreNo='V00080'
select * from Contract order by ContractID

declare @out int
exec RLPlanning_Cal_ValidateContract 'V00080', '', '2012-9-1', '2602da1e-c202-497a-b678-4a1fafd71efb', @out output
print @out

exec usp_Contract_DeleteSingleContract '30d21065-0b8e-4c88-84b0-daea4f9ace4e','2602da1e-c202-497a-b678-4a1fafd71efb'
exec usp_Contract_UndoContract 'ad48cef4-4c4c-458c-9a9f-62df9fed5b39', '2602da1e-c202-497a-b678-4a1fafd71efb'

select Status from Contract group by Status

select * from Contract where ContractSnapshotID='85b904ef-2f61-4157-8361-667d15880189'

select top 1 * from SRLS_TB_Master_Kiosk where KioskNo='VK00047'
select * from SRLS_TB_Master_Kiosk where KioskNo='K108.0004'

select * from SRLS_TB_Master_Store where StoreNo='1980030'

DECLARE @test bit
set @test = 1
print 'From SRLS=''' + cast(@test as nvarchar(1)) + ''' '

select * from RLPlanning_RealSales where StoreNo='1450011'
select * from Forecast_Sales where StoreNo='1450011'

select CompanyCode from SRLS_TB_Master_Company order by CompanyCode

select * from [dbo].[RLPlanning_FN_GetSalesStartEndDate]('1400002', '')

DECLARE @SalesStartDate datetime
set @SalesStartDate = '2012-12-1'
SET @SalesStartDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @SalesStartDate))
select @SalesStartDate

select * from RLP_ClosePlanning

select * from SRLS_TB_Master_Company
select * from SRLS_TB_Master_Store where StoreNo='V000087'

exec RLPlanning_Sales_SelectStoreOrKiosk '', '', '', '', '2602da1e-c202-497a-b678-4a1fafd71efb', '', -1, 60 


select * from Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
Where C.ContractSnapshotID='751F0598-99B1-4C6A-99D4-900B847B0356'

select * from Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID 
WHere E.EntityID='98a5ffb8-4cc1-4493-951d-29f9a6424501'

select * from Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID 
WHere E.EntityID='9A621873-7BF7-4D3D-A2A4-725CF284D50B'

select * from SRLS_TB_Master_Store where StoreNo='10500020'

exec RLPlanning_Cal_SelectEntityByNameOrNo '10500020', '', '', ''


select Entity.*,Contract.PartComment from Entity,Contract

 where '3230004'=StoreOrDeptNo
 and Entity.ContractSnapshotID=Contract.ContractSnapshotID
 
select * from [RLP_ClosePlanning]

select * from SRLS_TB_Master_Store where StoreNo like '%V%'

select * from GLRecord

select * from SRLS_TB_Master_User where UserName='administrator'

select * from Contract

select * from RLP_UserCompany
--delete from RLP_UserCompany where UserId='671A0C11-698B-443B-B501-BDCDDB6876E6'
--insert into RLP_UserCompany
--select '671A0C11-698B-443B-B501-BDCDDB6876E6', CompanyCode  from SRLS_TB_Master_Company group by CompanyCode


select * from Forecast_CheckResult

select * from Contract C INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
WHERE UC.UserId='671A0C11-698B-443B-B501-BDCDDB6876E6' 
AND C.CompanyCode='120'
AND C.ContractSnapshotID='6730677f-2d29-4f51-8ecd-9f147288d2ad'

DECLARE @UserID nvarchar(50), @ContractSnapshotID nvarchar(50)
SET @UserID='671A0C11-698B-443B-B501-BDCDDB6876E6'
SET @ContractSnapshotID='6730677f-2d29-4f51-8ecd-9f147288d2ad'
SELECT * FROM Contract C INNER JOIN RLP_UserCompany UC ON C.CompanyCode=UC.CompanyCode
WHERE UC.UserId=@UserID AND C.ContractSnapshotID=@ContractSnapshotID



select * from Contract


--select * from RLPlanning_Cal_TmpTbl

--exec RLPlanning_Cal_SalesCalculate '671A0C11-698B-443B-B501-BDCDDB6876E6'

--EXEC RLPlanning_Cal_GLByStoreOrKioskNo 'V000097', '', '2012-09-30', '671A0C11-698B-443B-B501-BDCDDB6876E6'

--EXEC dbo.RLPlanning_Cal_FixedGL 'e960b3a5-1b68-4f56-831d-9c1eea17c8d1', '2012-09-30'

--exec usp_Cal_UpdateFixedAPGLNextStartEndDate 'e960b3a5-1b68-4f56-831d-9c1eea17c8d1'

select * from SRLS_TB_Master_Kiosk where KioskNo='VK00097'

select * from Entity

select * from GLRecord where ContractNO='v777.000001'
select * from GLRecord order by CreateTime desc

--671a0c11-698b-443b-b501-bdcddb6876e6
select * from RLPlanning_Cal_TmpTbl
DECLARE @OperatorUserID nvarchar(50), @StoreNo nvarchar(50), @KioskNo nvarchar(50), @CalEndDate datetime
set @OperatorUserID = '671a0c11-698b-443b-b501-bdcddb6876e6'
set @StoreNo = 'V000105'
set @KioskNo = ''
set @CalEndDate = '2012-10-31'
EXEC RLPlanning_Cal_GLByStoreOrKioskNo @StoreNo, @KioskNo, @CalEndDate, @OperatorUserID

exec RLPlanning_Sales_SelectStoreOrKiosk '', '', '', '', '671a0c11-698b-443b-b501-bdcddb6876e6'

select * from StoreSales
select * from KioskSalesCollection
select * from GLRecord where GLType like '%百分比%'
select * from GLRecord order by CreateTime desc

--select * into #tmp from Import_Sales
--truncate table Import_Sales
--drop table #tmp

--insert into Import_Sales select * from #tmp
--select * from Import_Sales

select * from Forecast_Sales where KioskNo='VK00098' order by SalesDate
delete from Forecast_Sales where KioskNo='VK00098'

exec RLPlanning_Select_Sales 'V000100','VK00098'

select * from StoreSales where StoreNo='V000111'

select * from RLPlanning_Cal_TmpTbl
exec RLPlanning_Cal_RatioGL 'c03fd6bc-aa23-4c78-bf79-2b7a55e19a16', '2012-10-31 00:00:00.000'

--DECLARE @RuleSnapShotID nvarchar(50), @CalEndDate datetime --预测结束时间
--SET @RuleSnapshotID='c03fd6bc-aa23-4c78-bf79-2b7a55e19a16'
--SET @CalEndDate = '2012-10-31 00:00:00.000'

--		--先将之前计算过该snapshot的glrecord给删除
--		EXEC dbo.RLPlanning_Cal_DeleteGLRecordByRuleSnapShotID @RuleSnapShotID
		
--		--初始化NextGLStartDate与NextGLEndDate
--		EXEC dbo.usp_Cal_UpdateRatioAPGLNextStartEndDate @RuleSnapshotID
		
--		--GL记录参数
--		DECLARE @TypeCodeSnapshotID NVARCHAR(50),@TypeCodeName NVARCHAR(50)
--		DECLARE @PayMentType NVARCHAR(50),@AccountingCycle DATETIME
--		SELECT @AccountingCycle = dbo.fn_Cal_GetNextGLCreateDate(@RuleSnapshotID)
			
--		SELECT	@TypeCodeSnapshotID=TC.TypeCodeSnapshotID, @TypeCodeName=TC.TypeCodeName
--		FROM  (SELECT x.*,y.EntityInfoSettingID,y.RentType FROM 
--		(SELECT * FROM  RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID) x 
--		INNER JOIN dbo.RatioRuleSetting y ON x.RatioID = y.RatioID) AS FR INNER JOIN
--		dbo.EntityInfoSetting AS EI ON FR.EntityInfoSettingID = EI.EntityInfoSettingID INNER JOIN
--		dbo.Entity AS E ON EI.EntityID = E.EntityID INNER JOIN
--		(SELECT * FROM dbo.TypeCode WHERE  SnapshotCreateTime IS NULL ) AS TC ON E.EntityTypeName = TC.EntityTypeName AND FR.RentType = TC.RentTypeName
		      
--		SELECT @PayMentType=PayMentType
--		FROM dbo.v_Cal_RatioRule
--		WHERE RuleSnapshotID=@RuleSnapshotID
		
--		DECLARE @GLType NVARCHAR(50)
--		IF EXISTS(SELECT IsPure FROM dbo.RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID AND IsPure=1)
--		BEGIN
--			SELECT @GLType='纯百分比租金'
--		END
--		ELSE
--		BEGIN
--			SELECT @GLType='非纯百分比租金'
--		END
		
--		--NextGLStartDate如果在预测结束时间之外，则循环出GL
--		DECLARE @NextGLStartDate DATETIME
--		SELECT @NextGLStartDate=NextGLStartDate FROM RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapShotID
		

--			DECLARE @GLRecordID NVARCHAR(50)
--			SELECT @GLRecordID=NEWID()

--			--创建GL记录  
--			INSERT INTO dbo.GLRecord 
--				(GLRecordID,GLType,TypeCodeSnapshotID,TypeCodeName,ContractSnapshotID,ContractNO,CompanyCode,CompanyName,
--				VendorNo,VendorName,EntityID,EntityTypeName,EntityInfoSettingID,RuleSnapshotID,RuleID,RentType,
--				AccountingCycle,IsCalculateSuccess,HasMessage,IsChange,CycleStartDate,CycleEndDate,PayMentType,
--				StoreEstimateSales,KioskEstimateSales,GLAmount,CreatedCertificateTime,
--				IsCASHSHEETClosed,IsAjustment,CreateTime,UpdateTime,FromSRLS)
--			SELECT @GLRecordID,@GLType,@TypeCodeSnapshotID,@TypeCodeName,C.ContractSnapshotID,C.ContractNO,C.CompanyCode,C.CompanyName,
--				EI.VendorNo,VC.VendorName,EI.EntityID,E.EntityTypeName,EI.EntityInfoSettingID,FR.RuleSnapshotID,FR.RuleID,FR.RentType,
--				@AccountingCycle,0 AS IsCalculateSuccess,0 AS HasMessage,0 AS IsChange,FR.NextGLStartDate AS CycleStartDate,FR.NextGLEndDate AS CycleEndDate,VC.PayMentType,
--				0 AS StoreEstimateSales,0 AS KioskEstimateSales,0 AS GLAmount,NULL AS CreatedCertificateTime,
--				0 AS IsCASHSHEETClosed,0 AS IsAjustment,GETDATE() AS CreateTime,GETDATE() AS UpdateTime,0 AS FromSRLS
--			FROM  dbo.[Contract] AS C INNER JOIN
--				  dbo.Entity AS E ON C.ContractSnapshotID = E.ContractSnapshotID INNER JOIN
--				  dbo.VendorContract AS VC ON C.ContractSnapshotID = VC.ContractSnapshotID INNER JOIN
--				  dbo.VendorEntity AS VE ON E.EntityID = VE.EntityID AND VC.VendorNo = VE.VendorNo INNER JOIN
--				  dbo.EntityInfoSetting AS EI ON E.EntityID = EI.EntityID AND VE.VendorNo = EI.VendorNo INNER JOIN
--				  (SELECT x.*,y.EntityInfoSettingID,y.RentType FROM (SELECT * FROM  RatioCycleSetting WHERE RuleSnapshotID=@RuleSnapshotID) x 
--					INNER JOIN dbo.RatioRuleSetting y ON x.RatioID = y.RatioID) AS FR ON EI.EntityInfoSettingID = FR.EntityInfoSettingID
			
--			print @GLRecordID	 
--			--开始计算	
--			EXEC dbo.usp_Cal_CalRatioGL @GLRecordID
			

--exec usp_Cal_DeleteOneGLRecord 'E3476932-09D3-4E34-9163-3DD98B7BC2C3'

select * from APProcessParameterValue

select * from RLP_UserCompany where UserId='671a0c11-698b-443b-b501-bdcddb6876e6'
select * from SRLS_TB_System_DictionaryKey

select * from Contract Where CreatorName = 'administrator'
select * from SRLS_TB_Master_User where UserName='administrator'

exec usp_Contract_CheckContract '40C05844-79C9-4453-A2D9-397E692794A0'
select * from [v_ContractKeyInfo] where ContractSnapshotID='40C05844-79C9-4453-A2D9-397E692794A0'
select * from Entity where EntityID='9C977B8E-4647-4723-968D-ABEE61F30415'

select * from fn_GetFirstDueDateError('47B2979E-E1AB-4338-A02A-752670E41FA8')

select * from fn_GetRuleFirstAPCycleDateZone('91BFEA2C-4E12-4B53-82EE-9409B2FBF01D')

select * from FixedRuleSetting where RuleSnapshotID='91BFEA2C-4E12-4B53-82EE-9409B2FBF01D'

print dbo.fn_CheckFixedTimeInterval('96517667-55C7-42C3-A4FB-52733EC9E398');
select * from FixedTimeIntervalSetting where RuleSnapshotID='96517667-55C7-42C3-A4FB-52733EC9E398'

select * from Import_Sales
select * from Forecast_Sales_ImportPart
exec RLPlanning_Import_Sales
select * from Forecast_Sales where StoreNo='V000136' order by KioskNo
select MAX(UpdateTime) from StoreSales where StoreNo='v000126'
select MAX(CreateTime) from SRLS_TB_Master_Kiosk where KioskName like '%VK00097%'
select * from KioskSalesCollection where KioskID = '5a9fb5a9-124d-4fe7-9394-72f60e5b2a51'

select * from Import_SalesValidation

select * from Entity
select * from SRLS_TB_Master_Kiosk

exec RLPlanning_Sales_ImportGetMinRentStartDateOrOpenningDate
SELECT * FROM SRLS_TB_Master_Kiosk WHERE KioskNo = 'VK00102'

select * from SRLS_TB_Master_Kiosk where KioskNo = 'vk00102'

select * from RLPlanning_Cal_TmpTbl
select * from GLRecord order by CreateTime desc


select * from Contract where ContractNO='v123.000002'

select * from RLPlanning_Cal_TmpTbl
truncate table RLPlanning_Cal_TmpTbl
insert into RLPlanning_Cal_TmpTbl select * from #tmp
exec RLPlanning_Cal_SalesCalculate '671A0C11-698B-443B-B501-BDCDDB6876E6'
select * from GLRecord where ContractNO='v120.000021' order by CycleEndDate desc

exec RLPlanning_Cal_ZXGL '7920daef-03aa-415f-bda1-7350158f02ad', '2013-12-31'
select * from dbo.fn_Cal_GetZXGLCycleInfo('7920daef-03aa-415f-bda1-7350158f02ad')

select * from StoreSales
select * from KioskSalesCollection

print dbo.RLPlanning_FN_GetSalesUpdateTime('V000002','')


select * from SRLS_TB_Master_Store

exec RLPlanning_Sales_Restaurant2RealSales '1400002'

select * FROM RLPlanning_RealSales where KioskNo IS NOT NULL

select * from StoreSales where StoreNo='1407002'

select * from SRLS_TB_Master_Kiosk

select * from RLPlanning_Cal_TmpTbl
select * from GLRecord where ContractNO='130.000195' AND GLType='非纯百分比租金' order by CycleEndDate
select * from GLRecord where ContractNO='130.000195' AND GLType='固定租金' order by CycleEndDate
select * from GLRecord where ContractNO='121.000042'
select * from GLRecord where ContractNO='107.000080'
select * from GLRecord where ContractNO='120.000116'
select * from GLRecord where ContractNO='107.000083' AND GLType='非纯百分比租金' order by CycleEndDate
select * from GLRecord where ContractNO='119.000047' AND GLType='纯百分比租金' order by CycleEndDate
select * from GLRecord where ContractNO='v119.000004' AND GLType='非纯百分比租金' order by CycleEndDate
select * from GLRecord where ContractNO='120.000116' AND GLType='固定租金' order by TypeCodeName, CycleEndDate
select * from SRLS_TB_Master_User where UserName='administrator'
select * from RLP_UserCompany

select * from RLPlanning_Cal_TmpTbl

truncate table RLPlanning_Cal_TmpTbl
insert into RLPlanning_Cal_TmpTbl select * from #tmp
exec RLPlanning_Cal_SalesCalculate '671A0C11-698B-443B-B501-BDCDDB6876E6'

--67BCA29C-6577-4912-926D-7DD617AC8EF4
select dbo.fn_Cal_GetZXNextGLCreateDate('1BA1CA9B-0193-45F1-9A37-98D105C7B9B6')

EXEC dbo.RLPlanning_Cal_RatioGL '131A9C35-E44F-4067-8FD8-FF0A88C1E0E6', '2012-08-10'


select * from Contract where ContractID='0AC964EF-B8A1-49DF-BE7E-584AAED1F26A'
select * from Entity where ContractSnapshotID='4284165F-F4D8-4EB2-B44D-B984DB68909A'
select * from EntityInfoSetting where EntityID='96A9605F-2D8F-41F7-B4AA-0B64B129B084'

select * from GLAdjustment where EntityInfoSettingID='7AF6658A-2FD4-4F14-B04A-BADC1F31FE09'

use RLPlanning
select * from GLProcessParameterValue where GLRecordID='4FB19582-6E40-4B47-889C-BC827A403509'

use SRLS
--select * from GLRecord where ContractNO='119.000025'
select * from GLRecord where ContractNO='119.000025'
select * from GLProcessParameterValue where GLRecordID='0E711829-93D4-4067-93D0-5E13399EC6F2'
use RLPlanning

select * from Entity E INNER JOIN EntityInfoSetting ES ON E.EntityID=ES.EntityID
INNER JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=ES.EntityInfoSettingID

exec [RLPlanning_Import_Sales]

select COUNT(*) from GLRecord where FromSRLS=0

select * from SRLS_TB_Master_User where UserName='administrator'

select * from Contract where Status='草稿'
select FS.RuleSnapshotID, FIS.TimeIntervalID, RS.RatioID from Entity E INNER JOIN EntityInfoSetting EIS ON E.EntityID=EIS.EntityID
LEFT JOIN FixedRuleSetting FS ON FS.EntityInfoSettingID=EIS.EntityInfoSettingID
LEFT JOIN FixedTimeIntervalSetting FIS ON FIS.RuleSnapshotID=FS.RuleSnapshotID
LEFT JOIN RatioRuleSetting RS ON RS.EntityInfoSettingID=EIS.EntityInfoSettingID
WHERE E.ContractSnapshotID IN (select ContractSnapshotID from Contract where Status='草稿')

select * from RLPlanning_Cal_TmpTbl

exec RLPlanning_Cal_SalesCalculate '671A0C11-698B-443B-B501-BDCDDB6876E6'

exec RLPlanning_Task_SelectFinishedTasks null, null, null, null, null, 0, '671A0C11-698B-443B-B501-BDCDDB6876E6'

select * from Forecast_CheckResult order by CreateTime desc
	
delete from Forecast_CheckResult where KioskNo='VK00002'

select * from GLRecord

select * from Forecast_Sales_ImportPart

truncate table Import_Sales
insert into Import_Sales
select CompanyCode, StoreNo, StoreName, '', '2013', '310000', '310000',
'310000', '310000', '310000', '310000', '310000', '310000', '310000',
'310000', '310000', '310000'
from SRLS_TB_Master_Store WHERE Status='A'
insert into Import_Sales
select S.CompanyCode, S.StoreNo, S.StoreName, K.KioskName, '2013', '310000',
'310000', '310000', '310000', '310000', '310000', '310000', '310000',
'310000', '310000', '310000', '310000'
from SRLS_TB_Master_Kiosk K INNER JOIN SRLS_TB_Master_Store S ON
K.StoreNo=S.StoreNo
WHERE K.Status='A'

exec RLPlanning_Import_Sales

rollback transaction
print @@TRANCOUNT

select COUNT(*) from StoreSales where YEAR(SalesDate)=2013


select * from FixedRuleSetting
select * from FixedTimeIntervalSetting

select CA.* from contract C inner join Entity E on C.ContractSnapshotID=E.ContractSnapshotID
inner join EntityInfoSetting EIS  on EIS.EntityID=E.EntityID
left join FixedRuleSetting FS on FS.EntityInfoSettingID=EIS.EntityInfoSettingID
left join FixedTimeIntervalSetting FTI on FTI.RuleSnapshotID=FS.RuleSnapshotID
left join RatioRuleSetting RS on RS.EntityInfoSettingID=EIS.EntityInfoSettingID
left join RatioCycleSetting RCS on RCS.RatioID=RS.RatioID
left join RatioTimeIntervalSetting RTI on RTI.RuleSnapshotID=RCS.RuleSnapshotID
left join ConditionAmount CA on CA.TimeIntervalID=RTI.TimeIntervalID
where C.SnapshotCreateTime is null and C.Status='已生效'

exec usp_Contract_GetCycleItem '百分比'

select * from CycleItem
select * from Contract where ContractSnapshotID='1f194df2-9bcf-4674-b661-3b9964fc2f3d'
or ContractSnapshotID='ccf505b9-d186-44e2-b8d6-99afdcff2ebe'
select * from Entity where StoreOrDeptNo='v000002'

--exec usp_Contract_DeleteSingleContract 'ccf505b9-d186-44e2-b8d6-99afdcff2ebe', '671A0C11-698B-443B-B501-BDCDDB6876E6'

select * from SRLS_TB_Master_User where UserName='administrator'

select * from RLPlanning_RealSales

select * from SRLS_TB_Master_User where UserName='Administrator'
update SRLS_TB_Master_User set Locked=1, WrongTimes=0 where ID='671A0C11-698B-443B-B501-BDCDDB6876E6'

select * from contract where PartComment='续租'

print @@TRANCOUNT

select COUNT(*) from SRLS_TB_Master_Kiosk
select * from SRLS_TB_Master_Kiosk

select MIN(ZXConstant), MAX(ZXConstant) from FixedRuleSetting

select * from SRLS_TB_Master_Kiosk where StoreNo='1400181'

select * from GLRecord where ContractNO='132.000084' AND YEAR(CycleStartDate)=2012

select * from Contract 

select * from RLPlanning_RealSales where StoreNo='1400181'

select * from SRLS_TB_Master_Vendor where VendorNo='00000'

use RLPlanning
select * from StoreSales where StoreNo='1400181'

use SRLS
select * from StoreSales where StoreNo='1400181'

select * from StoreSales where FromSRLS=1

use RLPlanning_0817
select * from StoreSales where FromSRLS=1

select * from SRLS_TB_Master_Store where FromSRLS=0

select * from SRLS_TB_Master_User where UserName='M142ELI'
exec RLPlanning_Select_SalesTemplate '95E1F7CE-D6EB-45CE-ABDC-8BA35B1082A6'

select * from RLPlanning_RealSales
select * from KioskSalesCollection where FromSRLS=1

select * from SRLS_TB_Master_Store where StoreNo='1400002'
select * from SRLS_TB_Master_Company where CompanyCode='132'

select * from RLPlanning_RealSales where StoreNo='1400002'

select * from SRLS_TB_Master_User where UserName='Administrator'

select * from RLP_UserCompany where UserId='671A0C11-698B-443B-B501-BDCDDB6876E6'

USE SRLS
select * from GLRecord where EntityID='D030A674-6FE4-420C-A487-F9957C4F0A70' AND GLType='纯百分比租金'

select * from Entity where StoreOrDeptNo='1950003'

select * from SRLS_TB_Master_Store

select * from Forecast_Sales where StoreNo='3560025'


select * from SRLS_TB_Master_Vendor where VendorNo='1'

select * from RLPlanning_RealSales where StoreNo='1450039' and KioskNo is null order by SalesDate
select * from Entity where StoreOrDeptNo='1450039'

select * from GLRecord where EntityID='DB5684F3-4919-4D43-8E3A-F2010F2DA44F'
and TypeCodeName like '%百分比%' order by CycleStartDate

SELECT * FROM StoreSales WHERE StoreNo = '1450039' 
AND Sales=0
AND SalesDate BETWEEN '2001-01-19' AND '2012-7-31'

select * from SRLS_TB_Master_User

select * from 
select * from Contract where ContractNo='107.000202'
select * from GLRecord where ContractSnapshotID='DFD28193-D76E-492C-AC0D-CCD586809E68'
and GLType like '%非纯%'

select * from Forecast_Sales

select * from SRLS_TB_Master_User order by UpdateTime

select * from GLRecord where FromSRLS=1 and GLType like '%直线%' order by EntityID, CycleStartDate
select * from SRLS_TB_Master_Store
select * from SRLS_TB_Master_Area
select * from SRLS_TB_Master_Company

DECLARE @StoreNo nvarchar(50), @CompanyCode nvarchar(50), @AreaID nvarchar(50)
SET @CompanyCode='107'
SELECT S.StoreNo, NULL 
FROM SRLS_TB_Master_Store S INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode = C.CompanyCode
WHERE S.StoreNo LIKE '%'+ISNULL(@StoreNo,'')+'%'
AND S.CompanyCode LIKE '%' + ISNULL(@CompanyCode,'') + '%'
AND C.AreaID LIKE '%' + ISNULL(@AreaID,'') + '%'

select * from Forecast_Sales where YEAR(SalesDate)=2013
select E.* from Contract C INNER JOIN Entity E ON E.ContractSnapshotID=C.ContractSnapshotID
where C.ContractNO='132.000115' OR C.ContractNO='132.000116'
select *  from GLRecord where (EntityID='6E79132D-5718-4D7A-BF6D-7B0B89831264' OR EntityID='1504AA1F-9B4C-4069-A328-47E252D71B2B')
and FromSRLS=1 order by GLType, CycleStartDate

select * from Entity where StoreOrDeptNo='1950012'
select * from GLRecord where EntityID='EB5810DB-1EE0-4D8C-BFC3-B1C1F944F921'
AND GLType like '%非纯%'

select * from GLRecord where EntityID='EB5810DB-1EE0-4D8C-BFC3-B1C1F944F921'
AND GLType = '纯百分比租金'

select * from GLProcessParameterValue where GLRecordID IN 
(
select TOP 1000 GLRecordID from GLRecord where EntityID='D0693AE7-8D27-4D72-9F19-889ADBDF2F9A'
AND GLType like '%非纯%' order by CycleStartDate
)

select * from GLRecord where GLRecordID='721D650C-52A6-4B11-8E5F-BD0861EAD5AB'


select * from Entity Where ContractSnapshotID='B34939A6-F124-4B30-BC89-AA5CFB203F33'
select * from Contract where ContractNo='132.000100'

select * from GLProcessParameterValue where GLRecordID = '0DCD2E1B-9F4C-44FA-978E-33FDA93487D2'

select * from GLProcessParameterValue where GLRecordID IN 
(
select TOP 1000 GLRecordID from GLRecord where EntityID='EB5810DB-1EE0-4D8C-BFC3-B1C1F944F921'
AND GLType = '纯百分比租金' order by CycleStartDate
)

select * from RLPlanning_Cal_TmpTbl

exec RLPlanning_Cal_SalesCalculate '671A0C11-698B-443B-B501-BDCDDB6876E6'

select * from SRLS_TB_Master_User where UserName='Administrator'

print @@TRANCOUNT

select * from Forecast_Sales where StoreNo='1950012'


print dbo.fn_Cal_GetBeforeSumGLAmount('228D757A-162C-4EEA-9940-9DB84C131346', '2012-4-2','2012-7-31')

---
DECLARE 	@RecordID NVARCHAR(50), --APGLID
	@StartDate DATETIME,
	@EndDate DATETIME

SET @RecordID='228D757A-162C-4EEA-9940-9DB84C131346'
SET @StartDate='2012-4-2'
SET @EndDate='2012-7-31'
	DECLARE @ResultVar DECIMAL(18,2)
	
	DECLARE @RuleID NVARCHAR(50),@RuleSnapshotID NVARCHAR(50)
	SELECT @RuleID=RuleID,@RuleSnapshotID=RuleSnapshotID 
	FROM 
	(
		SELECT RuleID,RuleSnapshotID FROM dbo.APRecord WHERE APRecordID=@RecordID
		UNION ALL 
		SELECT RuleID,RuleSnapshotID FROM dbo.GLRecord WHERE GLRecordID=@RecordID
	)  x WHERE RuleID IS NOT NULL 
	
	print @RuleID
		--累计已预提
		SELECT @ResultVar=SUM(GLAmount) 
		FROM
		(
			SELECT glt.GLAmount FROM dbo.GLRecord gl 
			INNER JOIN dbo.GLTimeIntervalInfo glt ON gl.GLRecordID = glt.GLRecordID
			WHERE gl.RuleID=@RuleID 
			AND (glt.CycleStartDate BETWEEN @StartDate AND @EndDate)
			AND gl.GLType<>'直线租金'
			UNION ALL 
			SELECT Amount
			FROM dbo.GLAdjustment 
			WHERE (dbo.fn_GetDate(AdjustmentDate) BETWEEN @StartDate AND @EndDate)
			AND  RuleID=@RuleID
		) x
		
			SELECT glt.GLAmount, glt.CycleStartDate, @StartDate, @EndDate FROM dbo.GLRecord gl 
			INNER JOIN dbo.GLTimeIntervalInfo glt ON gl.GLRecordID = glt.GLRecordID
			WHERE gl.RuleID=@RuleID 
			--AND (glt.CycleStartDate BETWEEN @StartDate AND @EndDate)
			AND gl.GLType<>'直线租金'
	
	IF @ResultVar IS NULL
		print 'no'
	SELECT @ResultVar=ISNULL(@ResultVar,0)
	PRINT @ResultVar

SELECT glt.GLAmount, glt.CycleStartDate, @StartDate, @EndDate FROM dbo.GLRecord gl 
INNER JOIN dbo.GLTimeIntervalInfo glt ON gl.GLRecordID = glt.GLRecordID
WHERE gl.RuleID='3DB2985B-89A2-4645-809C-8FC33A72CDD7' 
--AND (glt.CycleStartDate BETWEEN @StartDate AND @EndDate)
AND gl.GLType<>'直线租金'

select * from GLRecord where RuleID='3DB2985B-89A2-4645-809C-8FC33A72CDD7'

SELECT glt.GLAmount, glt.CycleStartDate, * FROM dbo.GLRecord gl 
INNER JOIN dbo.GLTimeIntervalInfo glt ON gl.GLRecordID = glt.GLRecordID
WHERE gl.RuleID='3DB2985B-89A2-4645-809C-8FC33A72CDD7' 
--AND (glt.CycleStartDate BETWEEN @StartDate AND @EndDate)
AND gl.GLType<>'直线租金'

select * from GLTimeIntervalInfo where GLRecordID='4E22906A-1F15-4BE3-BBD6-C29E5DEF4EDC'


select * from SRLS_TB_Master_Kiosk where StoreNo='1980002' OR StoreNo='1980057'
select * from KioskSalesCollection where KioskID='72fa3ddc-0f42-4ee0-9f1b-bd0ffdd79c6a'
select * from KioskSales where KioskID='72fa3ddc-0f42-4ee0-9f1b-bd0ffdd79c6a'
exec [RLPlanning_Sales_Kiosk2RealSales] '1980002', 'K108.0005'
select * from RLPlanning_RealSales where StoreNo='1980002' AND KioskNo='K108.0005'

delete from RLPlanning_RealSales where StoreNo='1980002' AND KioskNo='K108.0005'


SELECT SUM(KC.Sales)
FROM KioskSalesCollection KC
WHERE FromSRLS=1 AND KC.KioskID = '72fa3ddc-0f42-4ee0-9f1b-bd0ffdd79c6a'
AND KC.ZoneStartDate BETWEEN '2012-2-1' AND DATEADD(DAY,-1,DATEADD(MONTH,1,'2012-2-1'))

select * from KioskSalesCollection where DATEDIFF(MONTH, ZoneStartDate, ZoneEndDate)>1
select * from KioskSalesCollection where KioskID='0961afd5-6eb2-4b91-af70-0b44aa48c9a3' order by ZoneStartDate

select * from Forecast_Sales

select * from Entity where StoreOrDeptNo='1400192'
select * from GLRecord where EntityID='D0693AE7-8D27-4D72-9F19-889ADBDF2F9A'
where

select SUM(Sales) from StoreSales where StoreNo='1400192' and YEAR(SalesDate)=2012 AND MONTH(SalesDate)=1

select * from SRLS_TB_Master_Kiosk where StoreNo='1400192'

select * from Import_Sales
exec RLPlanning_Import_Sales 

select * from StoreSales where FromSRLS=1

select * from Forecast_Sales

select * from StoreSales where StoreNo='3560025' and YEAR(SalesDate)=2013

select distinct EntityID from v_ContractKeyInfo where ContractSnapshotID='075D3D35-E953-45EC-AA7E-3098E368475C'
select * from Entity E1 INNER JOIN Entity E2 ON E1.EntityID <> E2.EntityID AND E1.ContractSnapshotID=E2.ContractSnapshotID

select * from Import_SalesValidation
exec RLPlanning_Sales_ImportGetMinRentStartDateOrOpenningDate

select * from SRLS_TB_Master_Store where StoreNo='V000138'

select * from Forecast_Sales

select * from Entity where StoreOrDeptNo='1400184'
select * from GLRecord where EntityID='64B28361-C43A-4808-8CD7-091558CC3F95'

exec usp_Contract_CheckContract '74E4B482-BC0C-45D9-9FF3-A81DE38782BA'

select * from Import_Contract
select * from SRLS_TB_Master_Vendor where VendorName like '%虚拟%'

select * from Contract where PartComment like '%续%'

select * from StoreSales where StoreNo='1400217' OR StoreNo='1400190'
select * from 

--v107.000014

select * from entity

exec RLPlanning_Cal_CalculateResult '671a0c11-698b-443b-b501-bdcddb6876e6', '2012/8/31 18:02:15'

select * from Forecast_CheckResult

SELECT FC.CreateTime, C.CompanyCode, C.CompanyName, S.StoreNo,
CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN '餐厅' ELSE '甜品店' END AS EntityType,
CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN S.SimpleName ELSE K.KioskName END AS EntityName,
FC.ErrorType,
CASE WHEN FC.ErrorType='合同缺失' THEN '修改合同' 
WHEN FC.ErrorType='Sales数据不全' THEN '录入销售数据' ELSE NULL END AS Operation,
FC.KioskNo, FC.ContractNo, FC.CheckID, FC.ContractSnapShotID, FC.EntityID --不显示，用于弹出修正界面用
FROM Forecast_CheckResult FC
INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo=S.StoreNo
INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode=C.CompanyCode
LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
WHERE FC.IsSolved=0 AND FC.UserOrGroupID='671a0c11-698b-443b-b501-bdcddb6876e6' 
AND FC.CreateTime>'2012/8/31 17:48:57'
ORDER BY FC.StoreNo

delete from Forecast_CheckResult where ErrorType='Store Sales 有0数据'
delete from 

SELECT * FROM StoreSales WHERE StoreNo = '1450039' 
AND Sales=0
AND SalesDate BETWEEN '2001-01-19' AND '2012-12-31 00:00:00.000'

SELECT * FROM StoreSales WHERE StoreNo = '1450039' 
AND YEAR(SalesDate)=2012 AND MONTH(SalesDate)=8

select * from Entity where StoreOrDeptNo='1450039'

select * from StoreSales

DECLARE @ZeroTimeFirstDate DATE, @FirstDayOfMonth DATE
SET @ZeroTimeFirstDate = GETDATE()
SET @FirstDayOfMonth = STR(YEAR(@ZeroTimeFirstDate)) + '-' + STR(MONTH(@ZeroTimeFirstDate)) + '-' + '1'
SELECT @FirstDayOfMonth


select count(*) from RLPlanning_Cal_EntityInCal
select * from RLPlanning_Cal_EntityInCal
select * from RLPlanning_Cal_TmpTbl

truncate table RLPlanning_Cal_EntityInCal
select S.* from RLPlanning_Cal_EntityInCal R INNER JOIN SRLS_TB_Master_Store S ON R.StoreNo=S.StoreNo
where S.CompanyCode=105

select S.* from RLPlanning_Cal_EntityInCal R INNER JOIN SRLS_TB_Master_Store S ON R.StoreNo=S.StoreNo
where S.CompanyCode=107

select * from GLRecord where 


select DISTINCT U.UserName from RLP_UserCompany R INNER JOIN SRLS_TB_Master_User U ON R.UserID=U.ID
where R.CompanyCode = '107' order by U.UserName


select * from Entity where StoreOrDeptNo='105.000137'
select * from Contract where ContractNo='119.000047'

select * from GLRecord where ContractSnapshotID='DA513754-9DE0-4615-A138-17B9249739D7'
and GLType='直线租金' order by CycleStartDate

SELECT * FROM RLPlanning_Env WHERE ValName='ImportMuxResInUse'
SELECT * FROM RLPlanning_Env WHERE ValName='ImportPartMuxResInUse'
SELECT * FROM RLPlanning_Import_EntityInImport

select * from Entity E INNER JOIN Contract C ON E.ContractSnapshotID = C.ContractSnapshotID
where E.StoreOrDeptNo='3330070' AND C.SnapshotCreateTime IS NULL

select * from GLRecord where EntityID='CEE3B18F-3E04-4395-887E-0755F7262310'

select * from SRLS_TB_Master_Vendor where VendorName like '%虚%'

select * from SRLS_TB_System_DictionaryKey
select * from SRLS_TB_System_DictionaryItem