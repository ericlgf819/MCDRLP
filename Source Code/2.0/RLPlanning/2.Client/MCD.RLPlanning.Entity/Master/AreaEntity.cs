using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class AreaEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public override string ConfigFileName
        {
            get
            {
                return "Area.xml";
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [DataMember]
        public string AreaName { get; set; }

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

        [DataMember]
        public Guid OperationID { get; set; }
    }
}