using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        [DataMember]
        public override string ConfigFileName
        {
            get { return "Company.xml"; }
            set { ; }
        }
        
        /// <summary>
        /// 区域 ID
        /// </summary>
        [DataMember]
        public Guid? AreaID { get; set; }
        /// <summary>
        /// 公司编码，主键
        /// </summary>
        [DataMember]
        public string CompanyCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        [DataMember]
        public string SimpleName { get; set; }

        /// <summary>
        /// 公司负责人
        /// </summary>
        [DataMember]
        public string UserID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [DataMember]
        public Guid OperationID { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }

        #region  二期新增字段

        /// <summary>
        /// 固定租金source code
        /// </summary>
        [DataMember]
        public string FixedSourceCode { get; set; }

        /// <summary>
        /// 固定管理费SOURCECODE
        /// </summary>
        [DataMember]
        public string FixedManageSourceCode { get; set; }

        /// <summary>
        /// 固定服务费SOURCECODE
        /// </summary>
        [DataMember]
        public string FixedServiceSourceCode { get; set; }

        /// <summary>
        /// 直线租金SOURCECODE
        /// </summary>
        [DataMember]
        public string StraightLineSourceCode { get; set; }

        /// <summary>
        /// 百分比租金SOURCECODE
        /// </summary>
        [DataMember]
        public string RatioSourceCode { get; set; }

        /// <summary>
        /// 百分比管理费SOURCECODE
        /// </summary>
        [DataMember]
        public string RatioManageSourceCode { get; set; }

        /// <summary>
        /// 百分比服务费SOURCECODE
        /// </summary>
        [DataMember]
        public string RatioServiceSourceCode { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [DataMember]
        public string ResponsibleUserID { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [DataMember]
        public string ResponsibleEnglishName { get; set; }

        #endregion

        /// <summary>
        /// 是否来源于RL
        /// </summary>
        [DataMember]
        public bool? FromSRLS { get; set; }
    }
}