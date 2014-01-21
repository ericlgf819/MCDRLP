using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreEntity : BaseEntity
    {
        [DataMember]
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
        /// 所属区域
        /// </summary>
        [DataMember]
        public Guid AreaID { get; set; }
        /// <summary>
        /// 公司编号
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
        public string CompanySimpleName { get; set; }
        /// <summary>
        /// 餐厅编号
        /// </summary>
        [DataMember]
        public string StoreNo { get; set; }
        /// <summary>
        /// 餐厅名称
        /// </summary>
        [DataMember]
        public string StoreName { get; set; }
        /// <summary>
        /// 餐厅简称
        /// </summary>
        [DataMember]
        public string SimpleName { get; set; }
        /// <summary>
        /// 开业日期
        /// </summary>
        [DataMember]
        public DateTime? OpenDate { get; set; }
        /// <summary>
        /// 关店日期
        /// </summary>
        [DataMember]
        public DateTime? CloseDate { get; set; }
        /// <summary>
        /// 郵箱地址
        /// </summary>
        [DataMember]
        public string EmailAddress { get; set; }
        /// <summary>
        /// 餐厅状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// 是否特许店
        /// </summary>
        [DataMember]
        public string SpecialStore { get; set; }
        /// <summary>
        /// 临时关店
        /// </summary>
        [DataMember]
        public string TempCloseDate { get; set; }
        /// <summary>
        /// 租赁结束日期
        /// </summary>
        [DataMember]
        public DateTime? RentEndDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
    }
}