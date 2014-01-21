using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;
using MCD.Common;

namespace MCD.RLPlanning.Entity.ContractMg
{
    /// <summary>
    /// 百分比周期
    /// </summary>
    public class RatioCycleSettingEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "RatioCycleSetting.xml";
            }
            set { }
        }

        /// <summary>
        /// RatioID
        /// </summary>
        [DataMember]
        public string RatioID { get; set; }

        /// <summary>
        /// 规则快照ID
        /// </summary>
        [DataMember]
        public string RuleSnapshotID { get; set; }

        /// <summary>
        /// 计算规则设置ID
        /// </summary>
        [DataMember]
        public string RuleID { get; set; }

        /// <summary>
        /// 是否纯百分比
        /// </summary>
        [DataMember]
        public bool IsPure { get; set; }

        /// <summary>
        /// 首次DueDate
        /// </summary>
        [DataMember]
        public System.DateTime? FirstDueDate { get; set; }

        /// <summary>
        /// 下次DueDate
        /// </summary>
        [DataMember]
        public System.DateTime? NextDueDate { get; set; }

        /// <summary>
        /// 下次AP周期开始日期
        /// </summary>
        [DataMember]
        public System.DateTime? NextAPStartDate { get; set; }

        /// <summary>
        /// 下次AP周期结束日期
        /// </summary>
        [DataMember]
        public System.DateTime? NextAPEndDate { get; set; }

        /// <summary>
        /// 下次GL周期开始日期
        /// </summary>
        [DataMember]
        public System.DateTime? NextGLStartDate { get; set; }

        /// <summary>
        ///下次GL周期结束日期
        /// </summary>
        [DataMember]
        public System.DateTime? NextGLEndDate { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        [DataMember]
        public string PayType { get; private set; }

        /// <summary>
        /// 直线租金计算起始日
        /// </summary>
        [DataMember]
        public System.DateTime? ZXStartDate { get; set; }

        /// <summary>
        /// 计算周期
        /// </summary>
        [DataMember]
        public string Cycle { get; set; }

        /// <summary>
        /// 周期折算月数
        /// </summary>
        [DataMember]
        public int CycleMonthCount { get; set; }

        /// <summary>
        /// 公历
        /// </summary>
        [DataMember]
        public string Calendar { get; set; }

        /// <summary>
        /// 周期类型
        /// </summary>
        [DataMember]
        public string CycleType { get; set; }

        /// <summary>
        /// 快照生成时间
        /// </summary>
        [DataMember]
        public System.DateTime? SnapshotCreateTime { get; set; }


        bool enabled = true;
        /// <summary>
        /// 周期是否启用。
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        #region ctor

        public RatioCycleSettingEntity()
        {
            this.PayType = RentPaymentType.延付.ToString();//强制为延付，不允许修改
            this.FirstDueDate = DateTime.Now;
            this.IsPure = false;
            this.Cycle = "月";
            this.CycleMonthCount = 1;
        }
        #endregion

    }
}