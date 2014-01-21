using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserCompanyEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public override string ConfigFileName
        {
            get { return "UserCompany.xml"; }
            set { ; }
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [DataMember]
        public int? ID { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public Guid? UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CompanyCode { get; set; }


        /// <summary>
        /// 区域Id
        /// </summary>
        [DataMember]
        public Guid? AreaId { get; set; }
        /// <summary>
        /// 公司状态 A/I
        /// </summary>
        [DataMember]
        public Char? Status { get; set; }
    }
}