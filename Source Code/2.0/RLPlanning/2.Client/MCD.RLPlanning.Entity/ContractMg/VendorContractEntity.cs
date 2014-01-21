using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class VendorContractEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "VendorContract.xml";
            }
            set { }
        }

        /// <summary>
        /// VendorContractID
        /// </summary>
        [DataMember]
        public string VendorContractID { get; set; }

        /// <summary>
        /// 合同快照ID
        /// </summary>
        [DataMember]
        public string ContractSnapshotID { get; set; }

        /// <summary>
        /// Vendor编号
        /// </summary>
        [DataMember]
        public string VendorNo { get; set; }

        /// <summary>
        /// Vendor名称
        /// </summary>
        [DataMember]
        public string VendorName { get; set; }

        /// <summary>
        /// 付款类型
        /// </summary>
        [DataMember]
        public string PayMentType { get; set; }

        /// <summary>
        /// 此业主是否虚拟
        /// </summary>
        [DataMember]
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 支付
        /// </summary>
        [DataMember]
        public string BlockPayMent { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        public override string ToString()
        {
            return this.VendorName;
        }
    }
}