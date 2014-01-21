using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class RatioRuleSettingEntity:BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "RatioRuleSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// 百分比ID
        /// </summary>
        [DataMember]
        public string RatioID { get; set; }

        /// <summary>
        /// EntityInfoSettingID
        /// </summary>
        [DataMember]
        public string EntityInfoSettingID { get; set; }

        /// <summary>
        /// 租金类型名称
        /// </summary>
        [DataMember]
        public string RentType { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }


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