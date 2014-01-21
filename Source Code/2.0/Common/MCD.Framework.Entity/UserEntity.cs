using System;
using System.Runtime.Serialization;

namespace MCD.Framework.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        [DataMember]
        public override string ConfigFileName
        {
            get { return "User.xml"; }
            set { }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public Guid UserID { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        [DataMember]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [DataMember]
        public string ChineseName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public int Sex { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [DataMember]
        public int AcademicDegree { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [DataMember]
        public string Tel { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [DataMember]
        public string Nation { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [DataMember]
        public string Post { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [DataMember]
        public string Department { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public DateTime Birth { get; set; }
    }
}