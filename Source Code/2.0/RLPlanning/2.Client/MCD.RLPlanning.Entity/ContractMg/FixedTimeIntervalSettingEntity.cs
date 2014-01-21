using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 固定租金时间段设置
    /// </summary>
    public class FixedTimeIntervalSettingEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "FixedTimeIntervalSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// 时间段ID
        /// </summary>
        [DataMember]
        public string TimeIntervalID { get; set; }

        /// <summary>
        /// RuleSnapshotID
        /// </summary>
        [DataMember]
        public string RuleSnapshotID { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [DataMember]
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [DataMember]
        public System.DateTime? EndDate { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// 计算周期
        /// </summary>
        [DataMember]
        public string Cycle { get; set; }

        /// <summary>
        /// 周期折算月数
        /// </summary>
        [DataMember]
        public int CycleMonthCount { get; set; }

        /// <summary>
        /// 公历
        /// </summary>
        [DataMember]
        public string Calendar { get; set; }

        bool enabled = true;
        /// <summary>
        /// 规则是否启用。
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
    }
}