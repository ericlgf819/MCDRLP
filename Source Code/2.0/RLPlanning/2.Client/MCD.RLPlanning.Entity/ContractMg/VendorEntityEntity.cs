using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 业主实体关系
    /// </summary>
    public class VendorEntityEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "VendorEntity.xml";
            }
            set { }
        }

        /// <summary>
        /// VendorEntityID
        /// </summary>
        [DataMember]
        public string VendorEntityID { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        [DataMember]
        public string EntityID { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember]
        public string EntityName { get; set; }

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


        public override string ToString()
        {
            return this.EntityName;
        }
    }
}