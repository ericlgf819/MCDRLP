using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.APGL
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class APRecordEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "APRecord.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string APRecordID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string RelationAPRecordID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string TypeCodeSnapshotID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
        public string TypeCodeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ContractSnapshotID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ContractNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CompanyCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CompanyName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string VendorNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string VendorName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string EntityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string EntityTypeName { get; set; }

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
		public bool? IsCalculateSuccess { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? HasMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsChange { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CycleStartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CycleEndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string PayMentType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? StoreSales { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? KioskSales { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public decimal? APAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CreatedCertificateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsCASHSHEETClosed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsAjustment { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? UpdateTime { get; set; }
	}
}