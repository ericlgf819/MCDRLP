using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Common
{
    /// <summary>
    /// 系统字典数据项实体。
    /// </summary>
    [DataContract]
    public class DictionaryItem : BaseEntity
    {
        /// <summary>
        /// 在派生类中重写以指定数据库操作配置文件名。
        /// </summary>
        public override string ConfigFileName
        {
            get { return "DictionaryItem.xml"; }
            set { }
        }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// KeyID
        /// </summary>
        [DataMember]
        public string KeyID { get; set; }

        /// <summary>
        /// ItemNameEnglish
        /// </summary>
        [DataMember]
        public string ItemNameEnglish { get; set; }

        /// <summary>
        /// ItemNameTChinese
        /// </summary>
        [DataMember]
        public string ItemNameTChinese { get; set; }

        /// <summary>
        /// ItemNameSChinese
        /// </summary>
        [DataMember]
        public string ItemNameSChinese { get; set; }

        /// <summary>
        /// ItemValue
        /// </summary>
        [DataMember]
        public string ItemValue { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// OrderIndex
        /// </summary>
        [DataMember]
        public int? OrderIndex { get; set; }
    }
}