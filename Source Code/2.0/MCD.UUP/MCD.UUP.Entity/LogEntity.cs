using System;
using System.Runtime.Serialization;

namespace MCD.UUP.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class LogEntity : BaseEntity
    {
        /// <summary>
        /// 数据表名称
        /// </summary>
        [DataMember]
        public override string TableName
        {
            get
            {
                return "UUP_TB_Logs";
            }
            set
            {
                base.TableName = value;
            }
        }
        /// <summary>
        /// 日志主键ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        [DataMember]
        public string LogTitle { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        [DataMember]
        public string LogType { get; set; }
        /// <summary>
        /// 日志详细信息
        /// </summary>
        [DataMember]
        public string LogMessage { get; set; }
        /// <summary>
        /// 日志记录时间
        /// </summary>
        [DataMember]
        public DateTime LogTime { get; set; }
    }
}