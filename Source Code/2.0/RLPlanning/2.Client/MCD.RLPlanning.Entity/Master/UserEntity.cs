using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public override string ConfigFileName
        {
            get { return "User.xml"; }
            set { ; }
        }

        /// <summary>
        /// 主键 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string EcoinUserName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DataMember]
        public string DeptCode { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        [DataMember]
        public string DeptName { get; set; }
        /// <summary>
        /// 用户组 ID
        /// </summary>
        [DataMember]
        public Guid GroupID { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [DataMember]
        public string ChineseName { get; set; }
        /// <summary>
        /// 直属上级领导ID
        /// </summary>
        [DataMember]
        public Guid ImmediateSupervisorUserID { get; set; }
        /// <summary>
        /// 直属上级领导 英文名
        /// </summary>
        [DataMember]
        public string ImmediateSupervisorEnglishName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }


        /// <summary>
        /// 外键审核员ID
        /// </summary>
        [DataMember]
        public Guid CheckUserID { get; set; }
        /// <summary>
        /// 审核员英文名
        /// </summary>
        [DataMember]
        public string CheckEnglishName { get; set; }

        /// <summary>
        /// 外键复核员ID
        /// </summary>
        [DataMember]
        public Guid ReCheckUserID { get; set; }
        /// <summary>
        /// 复核员英文名
        /// </summary>
        [DataMember]
        public string ReCheckEnglishName { get; set; }

        /// <summary>
        /// 是否禁用，0表示禁用，1表示启用
        /// </summary>
        [DataMember]
        public Boolean Disabled { get; set; }
        /// <summary>
        /// 是否锁定，0表示锁定，1表示不锁定
        /// </summary>
        [DataMember]
        public Boolean Locked { get; set; }
        /// <summary>
        /// 是否删除，0表示已删除，1表示没有被删除
        /// </summary>
        [DataMember]
        public Boolean Deleted { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 密码错误次数
        /// </summary>
        [DataMember]
        public int WrongTimes { get; set; }
        /// <summary>
        /// 上次密码修改日期
        /// </summary>
        [DataMember]
        public DateTime PasswordUpdateDate { get; set; }
        /// <summary>
        /// 是否需要重置密码
        /// </summary>
        [DataMember]
        public bool IsChangePWD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public byte Status { get; set; }


        /// <summary>
        /// 数据来源
        /// </summary>
        [DataMember]
        public Boolean? FromSRLS { get; set; }

        [DataMember]
        public Guid OperationID { get; set; }

        [DataMember]
        public int Res { get; set; }

        /// <summary>
        /// 是否为系统设置的管理员。
        /// </summary>
        [DataMember]
        public bool IsSysAdmin { get; set; }
        /// <summary>
        /// 是否为管理员组用户。
        /// </summary>
        [DataMember]
        public bool IsAdminGroupUser { get; set; }
        /// <summary>
        /// 用户能查阅的公司列表, CompanyCode以,分割
        /// </summary>
        [DataMember]
        public string CompanyCodes { get; set; }
    }
}