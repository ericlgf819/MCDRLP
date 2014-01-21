using System;
using System.Runtime.Serialization;

namespace MCD.UUP.Entity
{
    /// <summary>
    /// 系统信息实体
    /// </summary>
    [DataContract]
    public class SystemEntity : BaseEntity
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        [DataMember]
        public override string TableName
        {
            get
            {
                return "UUP_TB_System";
            }
            set
            {
                base.TableName = value;
            }
        }
        /// <summary>
        /// 主键 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [DataMember]
        public string SystemName { get; set; }
        /// <summary>
        /// 系统编码，唯一标识
        /// </summary>
        [DataMember]
        public string SystemCode { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 应用系统数据库服务器地址
        /// </summary>
        [DataMember]
        public string DBSource { get; set; }
        /// <summary>
        /// 数据库帐号
        /// </summary>
        [DataMember]
        public string DBAccount { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        [DataMember]
        public string DBPassword { get; set; }
        /// <summary>
        /// 应用系统数据库名称
        /// </summary>
        [DataMember]
        public string DBName { get; set; }
        /// <summary>
        /// 应用系统数据库用户表名
        /// </summary>
        [DataMember]
        public string DBTable { get; set; }
        /// <summary>
        /// 应用系统数据库用户表帐号字段名
        /// </summary>
        [DataMember]
        public string AccountField { get; set; }
        /// <summary>
        /// 应用系统数据库用户表显示字段名
        /// </summary>
        [DataMember]
        public string DisplayField { get; set; }
        /// <summary>
        /// 查询过滤条件
        /// </summary>
        [DataMember]
        public string Filter { get; set; }
    }
}