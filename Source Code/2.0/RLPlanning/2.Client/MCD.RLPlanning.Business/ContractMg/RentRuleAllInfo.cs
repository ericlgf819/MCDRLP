using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.Common;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;

namespace MCD.RLPlanning.BLL.ContractMg
{
    /// <summary>
    /// 租金规则所有相关信息
    /// </summary>
    public class RentRuleAllInfo
    {
        #region ctor

        public RentRuleAllInfo(DataSet dataSource, string contractSnapshotID, ContractBLL contractBLL)
        {
            this.DataSource = dataSource;
            this.ContractSnapshotID = contractSnapshotID;
            this.ContractBLL = contractBLL;
        }
        #endregion

        #region 字段和属性声明

        private static Dictionary<string, string> s_TypeCodeDescription = new Dictionary<string, string>();

        private string m_ContractSnapshotID;
        public string ContractSnapshotID
        {
            get
            {
                return this.m_ContractSnapshotID;
            }
            set
            {
                if (this.m_ContractSnapshotID != value)
                {
                    this.m_ContractSnapshotID = value;
                }
            }
        }

        private DataSet m_DataSource;
        public DataSet DataSource
        {
            get { return this.m_DataSource; }
            set
            {
                if (this.m_DataSource != value)
                {
                    this.m_DataSource = value;
                    //
                    this.ConvertDataSetToEntity();
                }
            }
        }

        private ContractEntity m_Contract;
        public ContractEntity Contract
        {
            get
            {
                return this.m_Contract;
            }
            set
            {
                if (this.m_Contract != value)
                {
                    this.m_Contract = value;
                }
            }
        }

        private List<VendorContractEntity> m_VendorContractList;
        public List<VendorContractEntity> VendorContractList
        {
            get
            {
                return this.m_VendorContractList;
            }
            set
            {
                if (this.m_VendorContractList != value)
                {
                    this.m_VendorContractList = value;
                }
            }
        }

        private List<EntityEntity> m_EntityList;
        public List<EntityEntity> EntityList
        {
            get
            {
                return this.m_EntityList;
            }
            set
            {
                if (this.m_EntityList != value)
                {
                    this.m_EntityList = value;
                }
            }
        }

        private List<VendorEntityEntity> m_VendorEntityList;
        public List<VendorEntityEntity> VendorEntityList
        {
            get
            {
                return this.m_VendorEntityList;
            }
            set
            {
                if (this.m_VendorEntityList != value)
                {
                    this.m_VendorEntityList = value;
                }
            }
        }

        private List<EntityInfoSettingEntity> m_EntityInfoSettingList;
        public List<EntityInfoSettingEntity> EntityInfoSettingList
        {
            get
            {
                return this.m_EntityInfoSettingList;
            }
            set
            {
                if (this.m_EntityInfoSettingList != value)
                {
                    this.m_EntityInfoSettingList = value;
                }
            }
        }

        private List<FixedRuleSettingEntity> m_FixedRuleSettingList;
        public List<FixedRuleSettingEntity> FixedRuleSettingList
        {
            get
            {
                return this.m_FixedRuleSettingList;
            }
            set
            {
                if (this.m_FixedRuleSettingList != value)
                {
                    this.m_FixedRuleSettingList = value;
                }
            }
        }

        private List<FixedTimeIntervalSettingEntity> m_FixedTimeIntervalSettingList;
        public List<FixedTimeIntervalSettingEntity> FixedTimeIntervalSettingList
        {
            get
            {
                return this.m_FixedTimeIntervalSettingList;
            }
            set
            {
                if (this.m_FixedTimeIntervalSettingList != value)
                {
                    this.m_FixedTimeIntervalSettingList = value;
                }
            }
        }

        private List<RatioRuleSettingEntity> m_RatioRuleSettingList;
        public List<RatioRuleSettingEntity> RatioRuleSettingList
        {
            get
            {
                return this.m_RatioRuleSettingList;
            }
            set
            {
                if (this.m_RatioRuleSettingList != value)
                {
                    this.m_RatioRuleSettingList = value;
                }
            }
        }

        private List<RatioCycleSettingEntity> m_RatioCycleSettingList;
        public List<RatioCycleSettingEntity> RatioCycleSettingList
        {
            get
            {
                return this.m_RatioCycleSettingList;
            }
            set
            {
                if (this.m_RatioCycleSettingList != value)
                {
                    this.m_RatioCycleSettingList = value;
                }
            }
        }

        private List<RatioTimeIntervalSettingEntity> m_RatioTimeIntervalSettingList;
        public List<RatioTimeIntervalSettingEntity> RatioTimeIntervalSettingList
        {
            get
            {
                return this.m_RatioTimeIntervalSettingList;
            }
            set
            {
                if (this.m_RatioTimeIntervalSettingList != value)
                {
                    this.m_RatioTimeIntervalSettingList = value;
                }
            }
        }

        private List<ConditionAmountEntity> m_ConditionAmountList;
        public List<ConditionAmountEntity> ConditionAmountList
        {
            get
            {
                return this.m_ConditionAmountList;
            }
            set
            {
                if (this.m_ConditionAmountList != value)
                {
                    this.m_ConditionAmountList = value;
                }
            }
        }
        
        public ContractBLL ContractBLL { get; set; }

        public FixedRuleBLL FixedRuleBLL { get; set; }

        public RatioRuleBLL RatioRuleBLL { get; set; }

        public EntityBLL EntityBLL { get; set; }

        public TypeCodeBLL TypeCodeBLL { get; set; }


        private void ConvertDataSetToEntity()
        {
            this.Contract = null;
            this.m_VendorContractList = null;
            this.m_EntityList = null;
            this.m_VendorEntityList = null;
            this.m_EntityInfoSettingList = null;
            this.m_FixedRuleSettingList = null;
            this.m_FixedTimeIntervalSettingList = null;
            this.m_RatioRuleSettingList = null;
            this.m_RatioCycleSettingList = null;
            this.m_RatioTimeIntervalSettingList = null;
            this.m_ConditionAmountList = null;

            if (this.m_DataSource != null)
            {
                if (this.m_DataSource.Tables.Count >= 1 && this.m_DataSource.Tables[0].Rows.Count > 0)
                {
                    this.Contract = new ContractEntity();
                    ReflectHelper.SetPropertiesByDataRow<ContractEntity>(ref this.m_Contract, this.m_DataSource.Tables[0].Rows[0]);
                }
                if (this.m_DataSource.Tables.Count >= 2)
                {
                    this.m_VendorContractList = ReflectHelper.ConvertDataTableToEntityList<VendorContractEntity>(this.m_DataSource.Tables[1]);
                }
                if (this.m_DataSource.Tables.Count >= 3)
                {
                    this.m_EntityList = ReflectHelper.ConvertDataTableToEntityList<EntityEntity>(this.m_DataSource.Tables[2]);
                }
                if (this.m_DataSource.Tables.Count >= 4)
                {
                    this.m_VendorEntityList = ReflectHelper.ConvertDataTableToEntityList<VendorEntityEntity>(this.m_DataSource.Tables[3]);
                }
                if (this.m_DataSource.Tables.Count >= 5)
                {
                    this.m_EntityInfoSettingList = ReflectHelper.ConvertDataTableToEntityList<EntityInfoSettingEntity>(this.m_DataSource.Tables[4]);
                }
                if (this.m_DataSource.Tables.Count >= 6)
                {
                    this.m_FixedRuleSettingList = ReflectHelper.ConvertDataTableToEntityList<FixedRuleSettingEntity>(this.m_DataSource.Tables[5]);
                    this.m_FixedRuleSettingList.ForEach(item => { item.Enabled = true; });//fxh新增
                }
                if (this.m_DataSource.Tables.Count >= 7)
                {
                    this.m_FixedTimeIntervalSettingList = ReflectHelper.ConvertDataTableToEntityList<FixedTimeIntervalSettingEntity>(this.m_DataSource.Tables[6]);
                    this.m_FixedTimeIntervalSettingList.ForEach(item => { item.Enabled = true; });//fxh新增
                }
                if (this.m_DataSource.Tables.Count >= 8)
                {
                    this.m_RatioRuleSettingList = ReflectHelper.ConvertDataTableToEntityList<RatioRuleSettingEntity>(this.m_DataSource.Tables[7]);
                    this.m_RatioRuleSettingList.ForEach(item => { item.Enabled = true; });//fxh新增
                }
                if (this.m_DataSource.Tables.Count >= 9)
                {
                    this.m_RatioCycleSettingList = ReflectHelper.ConvertDataTableToEntityList<RatioCycleSettingEntity>(this.m_DataSource.Tables[8]);
                    this.m_RatioCycleSettingList.ForEach(item => { item.Enabled = true; });//fxh新增
                }

                if (this.m_DataSource.Tables.Count >= 10)
                {
                    this.m_RatioTimeIntervalSettingList = ReflectHelper.ConvertDataTableToEntityList<RatioTimeIntervalSettingEntity>(this.m_DataSource.Tables[9]);
                    this.m_RatioTimeIntervalSettingList.ForEach(item => { item.Enabled = true; });//fxh新增
                }

                if (this.m_DataSource.Tables.Count >= 11)
                {
                    this.m_ConditionAmountList = ReflectHelper.ConvertDataTableToEntityList<ConditionAmountEntity>(this.m_DataSource.Tables[10]);
                    this.m_ConditionAmountList.ForEach(item =>
                    {
                        item.Enabled = true;

                        if (item.ConditionDesc != null && item.ConditionDesc != string.Empty)
                        {
                            item.ConditionDesc = item.ConditionDesc.ToNumberString();
                        }
                        if (item.AmountFormulaDesc != null && item.AmountFormulaDesc != string.Empty)
                        {
                            item.AmountFormulaDesc = item.AmountFormulaDesc.ToNumberString();
                        }
                    });//fxh新增
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 通过业主编号获取关联的实体信息
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        public List<EntityEntity> GetEntitiesByVendorNo(string vendorNo)
        {
            List<string> entityIDList = new List<string>();
            //
            var vendorEntity = this.VendorEntityList.Where(item => item.VendorNo == vendorNo);
            vendorEntity.ToList().ForEach(item => entityIDList.Add(item.EntityID));
            List<EntityEntity> result = this.EntityList.Where(item => entityIDList.Contains(item.EntityID)).ToList();
            return result;
        }

        /// <summary>
        /// 通过业主和实体ID获取实体信息设置
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public EntityInfoSettingEntity GetEntityInfoSetting(string vendorNo, string entityID)
        {
            EntityInfoSettingEntity result = this.EntityInfoSettingList.FirstOrDefault(item => item.VendorNo == vendorNo && item.EntityID == entityID);
            if (result == null)
            {
                result = new EntityInfoSettingEntity() {
                    EntityID = entityID,
                    EntityInfoSettingID = Guid.NewGuid().ToString(),
                    TaxRate = 0.055M,
                    VendorNo = vendorNo,
                };
                //
                this.EntityBLL.InsertSingleEntityInfoSetting(result);
                this.EntityInfoSettingList.Add(result);
            }
            return result;
        }

        /// <summary>
        /// 获取固定租金设置信息
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <returns></returns>
        public List<FixedRuleSettingEntity> GetFixedRuleSetting(string entityInfoSettingID, string entityTypeName, List<string> rentTypeList)
        {
            List<FixedRuleSettingEntity> fixedRentRuleList = this.FixedRuleSettingList.Where(
                item => item.EntityInfoSettingID == entityInfoSettingID).ToList();//取消快照时间筛选，否则历史版本无法展示 && !item.SnapshotCreateTime.HasValue
            //
            List<string> fixedRentType = new List<string>();
            rentTypeList.ForEach((string item) => { if (item.StartsWith("固定")) fixedRentType.Add(item); });
            foreach (string rentType in fixedRentType)
            {
                FixedRuleSettingEntity rule = fixedRentRuleList.FirstOrDefault(item => item.RentType == rentType);
                if (rule == null)
                {
                    rule = new FixedRuleSettingEntity() {
                        EntityInfoSettingID = entityInfoSettingID,
                        RentType = rentType,
                        RuleID = Guid.NewGuid().ToString(),
                        RuleSnapshotID = Guid.NewGuid().ToString(),
                        Description = this.GetDescriptioin(rentType, entityTypeName),
                        //Modified by Eric --Begin
                        PayType = "实付",//固定租金及管理费默认为实付
                        //Modified by Eric --End
                        Enabled = false,//fxh新增属性
                        Calendar = "公历",
                    };
                    this.FixedRuleSettingList.Add(rule);
                    //
                    fixedRentRuleList.Add(rule);
                }
            }
            return fixedRentRuleList;
        }

        /// <summary>
        /// 通过租金类型名称获取摘要信息
        /// </summary>
        /// <param name="rentTypeName"></param>
        /// <returns></returns>
        private string GetDescriptioin(string rentTypeName, string entityTypeName)
        {
            string key = string.Format("{0}-{1}", entityTypeName, rentTypeName);
            if (!RentRuleAllInfo.s_TypeCodeDescription.ContainsKey(key))
            {
                TypeCodeEntity entity = this.TypeCodeBLL.SelectTypeCodeByRentTypeName(rentTypeName, entityTypeName);
                if (entity != null)
                {
                    RentRuleAllInfo.s_TypeCodeDescription[key] = entity.Description;
                }
                else
                {
                    RentRuleAllInfo.s_TypeCodeDescription[key] = entityTypeName + rentTypeName;
                }
            }
            return RentRuleAllInfo.s_TypeCodeDescription[key];
        }

        /// <summary>
        /// 获取固定租金时间段设置
        /// </summary>
        /// <param name="fixedRuleID"></param>
        /// <returns></returns>
        public List<FixedTimeIntervalSettingEntity> GetFixedTimeIntervalSetting(FixedRuleSettingEntity rule, EntityEntity entity)
        {
            List<FixedTimeIntervalSettingEntity> intervalList =
                this.FixedTimeIntervalSettingList.Where(item => item.RuleSnapshotID == rule.RuleSnapshotID).OrderBy(item => item.StartDate).ToList();
            //
            if (intervalList.Count == 0)
            {
                FixedTimeIntervalSettingEntity interval = new FixedTimeIntervalSettingEntity() {
                    TimeIntervalID = Guid.NewGuid().ToString(),
                    RuleSnapshotID = rule.RuleSnapshotID,
                    Calendar = "公历",
                    StartDate = entity.RentStartDate,
                    EndDate = entity.RentEndDate,
                    Enabled = false,//fxh新增
                };
                this.FixedTimeIntervalSettingList.Add(interval);
                //
                intervalList.Add(interval);
            }
            return intervalList;
        }

        /// <summary>
        /// 获取百分比租金设置信息
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <param name="rentTypeList"></param>
        /// <returns></returns>
        public List<RatioRuleSettingEntity> GetRatioRuleSetting(string entityInfoSettingID, string entityType,  List<string> rentTypeList)
        {
            List<RatioRuleSettingEntity> ratioRentRuleList = this.RatioRuleSettingList.Where(
                item => item.EntityInfoSettingID == entityInfoSettingID).ToList();
            //
            List<string> ratioRentType = new List<string>();
            rentTypeList.ForEach((string item) => { if (item.StartsWith("百分比")) ratioRentType.Add(item); });
            foreach (string rentType in ratioRentType)
            {
                RatioRuleSettingEntity rule = ratioRentRuleList.FirstOrDefault(item => item.RentType == rentType);
                if (rule == null)
                {
                    rule = new RatioRuleSettingEntity() {
                        RatioID = Guid.NewGuid().ToString(),
                        EntityInfoSettingID = entityInfoSettingID,
                        RentType = rentType,
                        Description = this.GetDescriptioin(rentType, entityType),
                        Enabled = false,//fxh新增属性
                    };
                    this.RatioRuleSettingList.Add(rule);
                    //
                    ratioRentRuleList.Add(rule);
                }
            }
            return ratioRentRuleList;
        }

        /// <summary>
        /// 获取百分比周期
        /// </summary>
        /// <param name="ratioID"></param>
        /// <returns></returns>
        public List<RatioCycleSettingEntity> GetRatioCycleSetting(string ratioID)
        {
            List<RatioCycleSettingEntity> cycleList = new List<RatioCycleSettingEntity>();
            //
            cycleList = this.RatioCycleSettingList
                .Where(item => item.RatioID == ratioID && item.Enabled)//取消快照时间筛选 && !item.SnapshotCreateTime.HasValue
                .OrderBy(item => item.CycleMonthCount).ToList();

            //必须获取两个，一个长周期，一个短周期
            while (cycleList.Count < 2)
            {
                RatioCycleSettingEntity cycle = new RatioCycleSettingEntity() {
                    RatioID = ratioID,
                    RuleSnapshotID = Guid.NewGuid().ToString(),
                    RuleID = Guid.NewGuid().ToString(),
                    Calendar = CalendarType.公历.ToString(),
                    CycleType = cycleList.Count == 0 ? "小周期" : "大周期",//CycleType.百分比.ToString(),
                    Cycle = (cycleList.Count == 0
                        ? this.ContractBLL.GetCycleItems(CycleType.百分比)[0].CycleItemName
                        : this.ContractBLL.GetCycleItems(CycleType.百分比)[1].CycleItemName),
                    Enabled = false,//fxh新增
                };
                cycle.CycleMonthCount = this.ContractBLL.GetCycleItems(CycleType.百分比)
                    .FirstOrDefault(item => item.CycleItemName == cycle.Cycle).CycleMonthCount;
                this.RatioCycleSettingList.Add(cycle);
                //
                cycleList.Add(cycle);
            }
            return cycleList;
        }

        /// <summary>
        /// 获取百分比时间段设置
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        public List<RatioTimeIntervalSettingEntity> GetRatioTimeIntervalSetting(string ruleSnapshotID, EntityEntity entity)
        {
            List<RatioTimeIntervalSettingEntity> intervalList = new List<RatioTimeIntervalSettingEntity>();
            //
            intervalList = this.RatioTimeIntervalSettingList.Where(item => item.RuleSnapshotID == ruleSnapshotID && item.Enabled).OrderBy(item => item.StartDate).ToList();
            if (intervalList.Count == 0)
            {
                RatioTimeIntervalSettingEntity interval = new RatioTimeIntervalSettingEntity() {
                    TimeIntervalID = Guid.NewGuid().ToString(),
                    RuleSnapshotID = ruleSnapshotID,
                    StartDate = entity.RentStartDate,
                    EndDate = entity.RentEndDate,
                    Enabled = true,
                };
                this.RatioTimeIntervalSettingList.Add(interval);
                //
                intervalList.Add(interval);
            }
            return intervalList;
        }

        /// <summary>
        /// 获取时间段条件金额
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        public List<ConditionAmountEntity> GetConditionAmount(string timeIntervalID)
        {
            List<ConditionAmountEntity> conditionList = new List<ConditionAmountEntity>();
            //
            conditionList = this.ConditionAmountList.Where(item => item.TimeIntervalID == timeIntervalID && item.Enabled).ToList();
            if (conditionList.Count == 0)
            {
                ConditionAmountEntity condition = new ConditionAmountEntity() {
                    TimeIntervalID = timeIntervalID,
                    ConditionID = Guid.NewGuid().ToString(),
                    Enabled = true,
                };
                this.ConditionAmountList.Add(condition);
                //
                conditionList.Add(condition);
            }
            return conditionList;
        }

        /// <summary>
        /// 获取指定的实体设置信息所有已启用的租金类型。
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <returns></returns>
        public List<EntityRentType> GetRentTypeListOfEntity(string entityInfoSettingID)
        {
            List<EntityRentType> result = new List<EntityRentType>();
            //固定租金
            List<FixedRuleSettingEntity> fixedRentRuleList = this.FixedRuleSettingList.Where(
                item => item.EntityInfoSettingID == entityInfoSettingID && !item.SnapshotCreateTime.HasValue && item.Enabled).ToList();
            EntityRentType rentTypeEntity = null;
            foreach (FixedRuleSettingEntity fix in fixedRentRuleList)
            {
                rentTypeEntity = new EntityRentType() { EntityInfoSettingID = entityInfoSettingID, 
                    RentType = fix.RentType, 
                    RuleID = fix.RuleID, 
                    RuleSnapshotID = fix.RuleSnapshotID, 
                    CycleType = string.Empty };
                result.Add(rentTypeEntity);
            }
            //百分比租金
            List<RatioRuleSettingEntity> ratioRentRuleList = this.RatioRuleSettingList.Where(
                item => item.EntityInfoSettingID == entityInfoSettingID && item.Enabled).ToList();
            List<RatioCycleSettingEntity> ratioCycleList = null;
            foreach (RatioRuleSettingEntity ratio in ratioRentRuleList)
            {
                ratioCycleList = this.RatioCycleSettingList.Where(item => item.RatioID == ratio.RatioID && item.Enabled).ToList();
                foreach (RatioCycleSettingEntity cycle in ratioCycleList)
                {
                    rentTypeEntity = new EntityRentType() { EntityInfoSettingID = entityInfoSettingID, 
                        RentType = ratio.RentType, 
                        RuleID = cycle.RuleID, 
                        RuleSnapshotID = cycle.RuleSnapshotID, 
                        CycleType = cycle.CycleType };
                    result.Add(rentTypeEntity);
                }
            }
            return result;
        }
        #endregion

        #region 校验有效性

        public bool IsDateBetween(DateTime start, DateTime end, DateTime date)
        {
            bool result = true;
            //
            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            end = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);
            result = (date >= start && date <= end);
            return result;
        }

        public bool IsDateEqual(DateTime date1, DateTime date2)
        {
            return date1.ToString("yyyyMMdd").Equals(date2.ToString("yyyyMMdd"));
        }
        #endregion
    }

    /// <summary>
    /// 表示单个的租金类型。
    /// </summary>
    public class EntityRentType
    {
        //Properties
        public string EntityInfoSettingID { get; set; }
        public string RuleSnapshotID { get; set; }
        public string RuleID { get; set; }
        public string RentType { get; set; }
        public string CycleType { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0}{1}", this.RentType, this.CycleType);
        }
    }
}