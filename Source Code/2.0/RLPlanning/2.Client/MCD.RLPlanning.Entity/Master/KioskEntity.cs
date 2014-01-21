using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class KioskEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "Kiosk.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string StoreNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string TemStoreNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? ActiveDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string SimpleName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? OpenDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CloseDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsEnable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsLocked { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public bool? IsNeedSubtractSalse { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string PartComment { get; set; }

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
		public DateTime? LastModifyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string LastModifyUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string StoreName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool? FromSRLS { get; set; }
	}
}