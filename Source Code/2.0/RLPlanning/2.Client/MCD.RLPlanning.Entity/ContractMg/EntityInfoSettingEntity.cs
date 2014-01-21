using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 实体信息设置区
    /// </summary>
    public class EntityInfoSettingEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "EntityInfoSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// 实体信息ID
        /// </summary>
        [DataMember]
        public string EntityInfoSettingID { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        [DataMember]
        public string EntityID { get; set; }

        /// <summary>
        /// Vendor编号
        /// </summary>
        [DataMember]
        public string VendorNo { get; set; }

        /// <summary>
        /// 地产年Sales
        /// </summary>
        [DataMember]
        public decimal RealestateSales { get; set; }

        /// <summary>
        /// 保证金额
        /// </summary>
        [DataMember]
        public decimal MarginAmount { get; set; }

        /// <summary>
        /// 保证金额备注
        /// </summary>
        [DataMember]
        public string MarginRemark { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        [DataMember]
        public decimal TaxRate { get; set; }
    }
}