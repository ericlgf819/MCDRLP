using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;
using MCD.Common.SRLS;

namespace MCD.RLPlanning.Entity.Setting
{
    /// <summary>
    /// 附件管理
    /// </summary>
    public class AttachmentsEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "SYS_Attachments.xml";
            }
            set { }
        }

        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        [DataMember]
        public CategoryType Category { get; set; }

        /// <summary>
        /// ObjectID
        /// </summary>
        [DataMember]
        public string ObjectID { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// FileType
        /// </summary>
        [DataMember]
        public string FileType { get; set; }

        /// <summary>
        /// FileSize
        /// </summary>
        [DataMember]
        public int FileSize { get; set; }

        /// <summary>
        /// FilePath
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// Extend1
        /// </summary>
        [DataMember]
        public string Extend1 { get; set; }

        /// <summary>
        /// Extend2
        /// </summary>
        [DataMember]
        public string Extend2 { get; set; }

        /// <summary>
        /// Extend3
        /// </summary>
        [DataMember]
        public string Extend3 { get; set; }

        /// <summary>
        /// Extend4
        /// </summary>
        [DataMember]
        public string Extend4 { get; set; }

        /// <summary>
        /// Extend5
        /// </summary>
        [DataMember]
        public string Extend5 { get; set; }

        /// <summary>
        /// CreateUserID
        /// </summary>
        [DataMember]
        public string CreateUserID { get; set; }

        /// <summary>
        /// CreateUserName
        /// </summary>
        [DataMember]
        public string CreateUserName { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        [DataMember]
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// ModifyUserID
        /// </summary>
        [DataMember]
        public string ModifyUserID { get; set; }

        /// <summary>
        /// ModifyUserName
        /// </summary>
        [DataMember]
        public string ModifyUserName { get; set; }

        /// <summary>
        /// ModifyTime
        /// </summary>
        [DataMember]
        public System.DateTime ModifyTime { get; set; }
    }
}