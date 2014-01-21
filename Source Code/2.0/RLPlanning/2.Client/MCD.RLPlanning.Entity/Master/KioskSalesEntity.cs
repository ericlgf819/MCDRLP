using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class KioskSalesEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "KioskSales.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskSalesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CollectionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? Sales { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? SalesDate { get; set; }

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
		public bool? IsAjustment { get; set; }

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