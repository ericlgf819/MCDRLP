using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "Entity.xml";
            }
            set { }
        }

        /// <summary>
        /// 实体ID
        /// </summary>
        [DataMember]
        public string EntityID { get; set; }

        /// <summary>
        /// 实体类型名称
        /// </summary>
        [DataMember]
        public string EntityTypeName { get; set; }

        /// <summary>
        /// 合同快照ID
        /// </summary>
        [DataMember]
        public string ContractSnapshotID { get; set; }

        /// <summary>
        /// 实体编号
        /// </summary>
        [DataMember]
        public string EntityNo { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember]
        public string EntityName { get; set; }

        /// <summary>
        /// 餐厅/部门
        /// </summary>
        [DataMember]
        public string StoreOrDept { get; set; }

        /// <summary>
        /// 餐厅/部门编号
        /// </summary>
        [DataMember]
        public string StoreOrDeptNo { get; set; }

        /// <summary>
        /// Kiosk编号
        /// </summary>
        [DataMember]
        public string KioskNo { get; set; }

        /// <summary>
        /// 开业日
        /// </summary>
        [DataMember]
        public System.DateTime? OpeningDate { get; set; }

        /// <summary>
        /// 租金开始日期
        /// </summary>
        [DataMember]
        public System.DateTime RentStartDate { get; set; }

        /// <summary>
        /// 租金结束日期
        /// </summary>
        [DataMember]
        public System.DateTime RentEndDate { get; set; }

        /// <summary>
        /// 是否出AP
        /// </summary>
        [DataMember]
        public bool IsCalculateAP { get; set; }

        /// <summary>
        /// 开始出AP日期
        /// </summary>
        [DataMember]
        public System.DateTime? APStartDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 实体是否已出AP
        /// </summary>
        public bool IsEntityHasAP { get; set; }
    }
}