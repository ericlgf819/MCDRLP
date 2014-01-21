using System;
using System.Runtime.Serialization;

namespace MCD.UUP.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class GroupEntity : BaseEntity
    {
        /// <summary>
        /// 主键 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        [DataMember]
        public Guid SystemID { get; set; }
        /// <summary>
        /// 系统编码，唯一标识
        /// </summary>
        [DataMember]
        public string SystemCode { get; set; }

        /// <summary>
        /// 用户组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public byte IsEnable { get; set; }
    }
}