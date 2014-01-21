using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "Contract.xml";
            }
            set { }
        }

        /// <summary>
        /// 合同快照ID
        /// </summary>
        [DataMember]
        public string ContractSnapshotID { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        [DataMember]
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同ID
        /// </summary>
        [DataMember]
        public string ContractID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        [DataMember]
        public string ContractNO { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        [DataMember]
        public string ContractName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        [DataMember]
        public string CompanySimpleName { get; set; }

        /// <summary>
        /// 公司备注
        /// </summary>
        [DataMember]
        public string CompanyRemark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 更新说明
        /// </summary>
        [DataMember]
        public string UpdateInfo { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [DataMember]
        public bool IsLocked { get; set; }

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

        /// <summary>
        /// 快照生成时间
        /// </summary>
        [DataMember]
        public DateTime? SnapshotCreateTime { get; set; }

        /// <summary>
        /// 是否点击了保存
        /// </summary>
        [DataMember]
        public bool IsSave { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMember]
        public Guid OperationID { get; set; }

        /// <summary>
        /// 合同操作记录，新建/变更
        /// </summary>
        public string PartComment { get; set; }
    }
}