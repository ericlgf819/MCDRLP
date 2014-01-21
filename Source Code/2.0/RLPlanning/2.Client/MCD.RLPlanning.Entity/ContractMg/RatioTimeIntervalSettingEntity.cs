using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 百分比时间段
    /// </summary>
    public class RatioTimeIntervalSettingEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "RatioTimeIntervalSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// 时间段ID
        /// </summary>
        [DataMember]
        public string TimeIntervalID { get; set; }

        /// <summary>
        /// 规则快照ID
        /// </summary>
        [DataMember]
        public string RuleSnapshotID { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [DataMember]
        public System.DateTime StartDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [DataMember]
        public System.DateTime EndDate { get; set; }

        bool enabled = true;
        /// <summary>
        /// 时间段是否启用。
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
    }
}