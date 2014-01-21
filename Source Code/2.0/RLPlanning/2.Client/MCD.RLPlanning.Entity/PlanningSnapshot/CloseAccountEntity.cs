using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;
using MCD.Common.SRLS;

namespace MCD.RLPlanning.Entity.PlanningSnapshot
{
    /// <summary>
    /// 附件管理
    /// </summary>
    public class CloseAccountEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "CloseAccount.xml";
            }
            set { }
        }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// CloseYear
        /// </summary>
        [DataMember]
        public int CloseYear { get; set; }

        /// <summary>
        /// IsDetected
        /// </summary>
        [DataMember]
        public bool IsDetected { get; set; }

        /// <summary>
        /// IsColsed
        /// </summary>
        [DataMember]
        public bool IsColsed { get; set; }

        /// <summary>
        /// ClosedBy
        /// </summary>
        [DataMember]
        public Guid? ClosedBy { get; set; }

        /// <summary>
        /// ClosedDate
        /// </summary>
        [DataMember]
        public DateTime? ClosedDate { get; set; }
    }
}