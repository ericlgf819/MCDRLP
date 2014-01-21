using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class LogEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public override string ConfigFileName
        {
            get { return "SysLog.xml"; }
            set { ; }
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
        /// 日志记录模块
        /// </summary>
        [DataMember]
        public string LogSource { get; set; }

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

        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMember]
        public Guid UserID { get; set; }

        /// <summary>
        /// 操作人英文名
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }
    }
}