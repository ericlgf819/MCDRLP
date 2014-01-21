using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 结算周期
    /// </summary>
    public class CycleItemEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "CycleItem.xml";
            }
            set { }
        }

        /// <summary>
        /// CycleID
        /// </summary>
        [DataMember]
        public string CycleID { get; set; }

        /// <summary>
        /// SortIndex
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// CycleItemName
        /// </summary>
        [DataMember]
        public string CycleItemName { get; set; }

        /// <summary>
        /// CycleMonthCount
        /// </summary>
        [DataMember]
        public int CycleMonthCount { get; set; }

        /// <summary>
        /// CycleType
        /// </summary>
        [DataMember]
        public string CycleType { get; set; }
    }
}