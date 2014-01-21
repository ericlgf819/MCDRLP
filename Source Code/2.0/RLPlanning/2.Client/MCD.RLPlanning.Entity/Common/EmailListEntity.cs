using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace  MCD.RLPlanning.Entity.Master
{
	/// <summary>
	/// 实体。
	/// </summary>
	[DataContract]
	public class EmailListEntity : BaseEntity
	{
		/// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "EmailList.xml"; }
            set { }
        }
		
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ListID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string SentEmailAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string SentorEnglisthName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ReceiveEmailAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string ReceiverEnglishName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string EmailTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public string EmailContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public DateTime? SentTime { get; set; }
	}
}