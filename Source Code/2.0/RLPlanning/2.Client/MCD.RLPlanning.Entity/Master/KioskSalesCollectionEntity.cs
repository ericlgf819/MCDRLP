using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class KioskSalesCollectionEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "KioskSalesCollection.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CollectionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string WorkflowRelationID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? Sales { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? ZoneStartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? ZoneEndDate { get; set; }

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
		public string InputSalseUserEnglishName { get; set; }
	}
}