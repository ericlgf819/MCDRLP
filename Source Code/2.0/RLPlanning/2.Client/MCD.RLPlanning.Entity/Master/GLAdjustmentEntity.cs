using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class GLAdjustmentEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "GLAdjustment.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string AdjustmentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string EntityInfoSettingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string RuleSnapshotID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string RuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string RentType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string AccountingCycle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
        public DateTime? AdjustmentDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? Amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CreatorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string GLDateZone { get; set; }
	}
}