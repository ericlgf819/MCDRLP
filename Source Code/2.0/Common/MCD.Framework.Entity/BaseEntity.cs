using System;
using System.Runtime.Serialization;

namespace MCD.Framework.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    [DataContract]
    public abstract class BaseEntity
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        [DataMember]
        public abstract string ConfigFileName { get; set; }


        /// <summary>
        /// 配置文件名
        /// </summary>
        [DataMember]
        public int ReturnInfo { get; set; }
    }
}