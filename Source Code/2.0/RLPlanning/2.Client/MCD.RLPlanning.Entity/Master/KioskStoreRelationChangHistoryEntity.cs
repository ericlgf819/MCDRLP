using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class KioskStoreRelationChangHistoryEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "KioskStoreRelationChangHistory.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ChangeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string KioskID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public int? StoreNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? ActiveDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string CreateUserEnglishName { get; set; }
	}
}