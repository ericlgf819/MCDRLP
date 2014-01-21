using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;
using MCD.Common.SRLS;

namespace MCD.RLPlanning.Entity.Synchronization
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "SynchronizationSchedule.xml";
            }
            set { }
        }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// SycDate
        /// </summary>
        [DataMember]
        public DateTime SycDate { get; set; }
        /// <summary>
        /// CalculateEndDate
        /// </summary>
        [DataMember]
        public DateTime? CalculateEndDate { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [DataMember]
        public string Status { get; set; }
        /// <summary>
        /// SynDetail
        /// </summary>
        [DataMember]
        public string SynDetail { get; set; }
        /// <summary>
        /// AddedBy
        /// </summary>
        [DataMember]
        public Guid AddedBy { get; set; }
        /// <summary>
        /// EnglishName
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }
        /// <summary>
        /// AddedDate
        /// </summary>
        [DataMember]
        public DateTime AddedDate { get; set; }
        /// <summary>
        /// LastModifiedBy
        /// </summary>
        [DataMember]
        public Guid LastModifiedBy { get; set; }
        /// <summary>
        /// LastModifiedDate
        /// </summary>
        [DataMember]
        public DateTime LastModifiedDate { get; set; }
    }
}