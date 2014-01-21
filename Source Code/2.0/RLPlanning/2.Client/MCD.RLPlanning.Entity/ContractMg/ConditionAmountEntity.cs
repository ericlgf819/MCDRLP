using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;
using MCD.Common;

namespace MCD.RLPlanning.Entity.ContractMg
{
   /// <summary>
    /// 时间段条件金额
    /// </summary>
    public class ConditionAmountEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "ConditionAmount.xml";
            }
            set { }
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        [DataMember]
        public string ConditionID { get; set; }

        /// <summary>
        /// TimeIntervalID
        /// </summary>
        [DataMember]
        public string TimeIntervalID { get; set; }

        /// <summary>
        /// 条件描述
        /// </summary>
        [DataMember]
        public string ConditionDesc { get; set; }

        /// <summary>
        /// 金额公式
        /// </summary>
        [DataMember]
        public string AmountFormulaDesc { get; set; }

        /// <summary>
        /// SQL条件解释
        /// </summary>
        [DataMember]
        public string SQLCondition { get; set; }

        /// <summary>
        /// SQL金额公式解释
        /// </summary>
        [DataMember]
        public string SQLAmountFormula { get; set; }

        /// <summary>
        /// SQL条件中的数字的值
        /// </summary>
        public string ConditionNumberValue 
        {
            get 
            {
                if (this.SQLCondition != string.Empty && this.SQLCondition != null)
                {
                    return this.SQLCondition.ExtractDigital();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 公式中的数字的值
        /// </summary>
        public string FormulaNumberValue
        {
            get 
            {
                if (this.SQLAmountFormula != string.Empty && this.SQLAmountFormula != null)
                {
                    return this.SQLAmountFormula.ExtractDigital();
                }
                else
                {
                    return null;
                }
            }
        }

        bool enabled = true;
        /// <summary>
        /// 条件是否启用。
        /// </summary>
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }
    }
}