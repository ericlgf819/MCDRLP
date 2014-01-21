using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class VendorEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "Vendor.xml";
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// 业主编号
        /// </summary>
        [DataMember]
        public string VendorNo { get; set; }
        /// <summary>
        /// 业主名称
        /// </summary>
        [DataMember]
        public string VendorName { get; set; }
        /// <summary>
        /// Block状态
        /// </summary>
        [DataMember]
        public string Flag { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        [DataMember]
        public string PayMentType { get; set; }
        /// <summary>
        /// 付款
        /// </summary>
        [DataMember]
        public string BlockPayMent { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 餐厅编号,多个餐厅之间使用 "," 分开
        /// </summary>
        [DataMember]
        public string StoreNo { get; set; }
        /// <summary>
        /// 合同编号,多个合同之间使用 "," 分开
        /// </summary>
        [DataMember]
        public string ContractNo { get; set; }
    }
}