using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeCodeEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "TypeCode.xml";
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// TypeCodeSnapshotID属性
        /// </summary>
        [DataMember]
        public String TypeCodeSnapshotID { get; set; }

        /// <summary>
        /// RentTypeName属性
        /// </summary>
        [DataMember]
        public String RentTypeName { get; set; }

        /// <summary>
        /// EntityTypeName属性
        /// </summary>
        [DataMember]
        public String EntityTypeName { get; set; }

        /// <summary>
        /// TypeCode属性
        /// </summary>
        [DataMember]
        public String TypeCodeName { get; set; }

        /// <summary>
        /// Status属性
        /// </summary>
        [DataMember]
        public String Status { get; set; }

        /// <summary>
        /// InvoicePrefix属性
        /// </summary>
        [DataMember]
        public String InvoicePrefix { get; set; }

        /// <summary>
        /// Description属性
        /// </summary>
        [DataMember]
        public String Description { get; set; }

        /// <summary>
        /// UpdateInfo属性
        /// </summary>
        [DataMember]
        public String UpdateInfo { get; set; }

        /// <summary>
        /// SortIndex属性
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// YTGLDebit属性
        /// </summary>
        [DataMember]
        public String YTGLDebit { get; set; }

        /// <summary>
        /// YTGLCredit属性
        /// </summary>
        [DataMember]
        public String YTGLCredit { get; set; }

        /// <summary>
        /// YTAPNormal属性
        /// </summary>
        [DataMember]
        public String YTAPNormal { get; set; }

        /// <summary>
        /// YTAPDifferences属性
        /// </summary>
        [DataMember]
        public String YTAPDifferences { get; set; }

        /// <summary>
        /// YTRemark属性
        /// </summary>
        [DataMember]
        public String YTRemark { get; set; }

        /// <summary>
        /// YFGLDebit属性
        /// </summary>
        [DataMember]
        public String YFGLDebit { get; set; }

        /// <summary>
        /// YFGLCredit属性
        /// </summary>
        [DataMember]
        public String YFGLCredit { get; set; }

        /// <summary>
        /// YFAPNormal属性
        /// </summary>
        [DataMember]
        public String YFAPNormal { get; set; }

        /// <summary>
        /// YFAPDifferences属性
        /// </summary>
        [DataMember]
        public String YFAPDifferences { get; set; }

        /// <summary>
        /// YFRemak属性
        /// </summary>
        [DataMember]
        public String YFRemak { get; set; }

        /// <summary>
        /// BFGLDebit属性
        /// </summary>
        [DataMember]
        public String BFGLDebit { get; set; }

        /// <summary>
        /// BFGLCredit属性
        /// </summary>
        [DataMember]
        public String BFGLCredit { get; set; }

        /// <summary>
        /// BFRemak属性
        /// </summary>
        [DataMember]
        public String BFRemak { get; set; }

        /// <summary>
        /// ZXGLDebit属性
        /// </summary>
        [DataMember]
        public String ZXGLDebit { get; set; }

        /// <summary>
        /// ZXGLCredit属性
        /// </summary>
        [DataMember]
        public String ZXGLCredit { get; set; }

        /// <summary>
        /// ZXRemark属性
        /// </summary>
        [DataMember]
        public String ZXRemark { get; set; }

        /// <summary>
        /// IsLocked属性
        /// </summary>
        [DataMember]
        public bool IsLocked { get; set; }

        /// <summary>
        /// CreateTime属性
        /// </summary>
        [DataMember]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// CreatorName属性
        /// </summary>
        [DataMember]
        public String CreatorName { get; set; }

        /// <summary>
        /// LastModifyTime属性
        /// </summary>
        [DataMember]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// LastModifyUserName属性
        /// </summary>
        [DataMember]
        public String LastModifyUserName { get; set; }

        /// <summary>
        /// SnapshotCreateTime属性
        /// </summary>
        [DataMember]
        public DateTime? SnapshotCreateTime { get; set; }

        [DataMember]
        public String OperationID { get; set; }

        /// <summary>
        /// 是否生成快照
        /// </summary>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 最后一次审批意见
        /// </summary>
        [DataMember]
        public string PartComment { get; set; }
    }
}