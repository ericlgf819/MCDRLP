using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class DeptEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        [DataMember]
        public override string ConfigFileName
        {
            get { return "Dept.xml"; }
            set { ; }
        }

        /// <summary>
        /// 区域
        /// </summary>
        [DataMember]
        public Guid? AreaId { get; set; }
        
        /// <summary>
        /// 公司编码
        /// </summary>
        [DataMember]
        public string CompanyCode { get; set; }
        /// <summary>
        /// 部门编码，主键
        /// </summary>
        [DataMember]
        public string DeptCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string DeptName { get; set; }
        /// <summary>
        /// 部门状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }


        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
    }
}