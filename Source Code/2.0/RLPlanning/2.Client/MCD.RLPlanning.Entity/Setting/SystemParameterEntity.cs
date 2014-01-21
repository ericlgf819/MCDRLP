using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemParameterEntity : BaseEntity
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
        /// 系统编码
        /// </summary>
        [DataMember]
        public string ParamCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember]
        public string ParamName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        public string ParamValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}