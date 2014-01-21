using System;
using System.Runtime.Serialization;

namespace MCD.UUP.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public abstract class BaseEntity
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        [DataMember]
        public virtual string TableName { get; set; }
    }
}