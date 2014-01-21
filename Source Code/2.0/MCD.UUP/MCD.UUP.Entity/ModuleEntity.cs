using System;
using System.Runtime.Serialization;

namespace MCD.UUP.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ModuleEntity : BaseEntity
    {
        /// <summary>
        /// 数据表名称
        /// </summary>
        [DataMember]
        public override string TableName
        {
            get
            {
                return "UUP_TB_Module";
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
        /// 系统主键
        /// </summary>
        [DataMember]
        public Guid SystemID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [DataMember]
        public string ModuleName { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        [DataMember]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 系统主键
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }
    }
}