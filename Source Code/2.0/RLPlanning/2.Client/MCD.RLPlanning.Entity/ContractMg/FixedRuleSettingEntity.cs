using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 固定规则
    /// </summary>
    public class FixedRuleSettingEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "FixedRuleSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// RuleSnapshotID
        /// </summary>
        [DataMember]
        public string RuleSnapshotID { get; set; }

        /// <summary>
        /// EntityInfoSettingID
        /// </summary>
        [DataMember]
        public string EntityInfoSettingID { get; set; }

        /// <summary>
        /// RuleID
        /// </summary>
        [DataMember]
        public string RuleID { get; set; }

        /// <summary>
        /// RentType
        /// </summary>
        [DataMember]
        public string RentType { get; set; }

        /// <summary>
        /// FirstDueDate
        /// </summary>
        [DataMember]
        public System.DateTime? FirstDueDate { get; set; }

        /// <summary>
        /// NextDueDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextDueDate { get; set; }

        /// <summary>
        /// NextAPStartDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextAPStartDate { get; set; }

        /// <summary>
        /// NextAPEndDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextAPEndDate { get; set; }

        /// <summary>
        /// NextGLStartDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextGLStartDate { get; set; }

        /// <summary>
        /// NextGLEndDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextGLEndDate { get; set; }

        /// <summary>
        /// PayType
        /// </summary>
        [DataMember]
        public string PayType { get; set; }

        /// <summary>
        /// ZXStartDate
        /// </summary>
        [DataMember]
        public System.DateTime? ZXStartDate { get; set; }

        /// <summary>
        /// ZXConstant
        /// </summary>
        [DataMember]
        public System.Decimal? ZXConstant { get; set; }

        [DataMember]
        public System.String Cycle{get;set;}

        [DataMember]
        public System.Int32 CycleMonthCount{get;set;}

        [DataMember]
        public System.String Calendar {get;set;}

        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// SnapshotCreateTime
        /// </summary>
        [DataMember]
        public System.DateTime? SnapshotCreateTime { get; set; }

        bool enabled = true;
        /// <summary>
        /// 规则是否启用。
        /// </summary>
        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }

        #region ctor

        public FixedRuleSettingEntity()
        {
            this.FirstDueDate = DateTime.Now;
            this.Cycle = "月";
            this.CycleMonthCount = 1;
        }
        #endregion
    }
}