using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    public class AccountEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        [DataMember]
        public override string ConfigFileName
        {
            get { return "Account.xml"; }
            set { ;}
        }

        /// <summary>
        /// 科目编号
        /// </summary>
        [DataMember]
        public string AccountNo { get; set; }
        /// <summary>
        /// AC_Desc
        /// </summary>
        [DataMember]
        public string AC_Desc { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }
        /// <summary>
        /// Open_Item_AC
        /// </summary>
        [DataMember]
        public string Open_Item_AC { get; set; }
        /// <summary>
        /// StoreDept_Type
        /// </summary>
        [DataMember]
        public string StoreDept_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorName { get; set; }


        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        [DataMember]
        public Guid OperationID { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime? UpdateTime { get; set; }
    }
}