using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class RentTypeEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public override string ConfigFileName
        {
            get
            {
                return "Store.xml";
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// 租金类型名称
        /// </summary>
        [DataMember]
        public string RentTypeName { get; set; }

        /// <summary>
        /// 上月/本月
        /// </summary>
        [DataMember]
        public string WhichMonth { get; set; }

        /// <summary>
        /// GL计算日期
        /// </summary>
        [DataMember]
        public int GLStartDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMember]
        public string CreatorName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public System.DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [DataMember]
        public string LastModifyUserName { get; set; }
    }
}