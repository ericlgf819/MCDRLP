using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.Common
{
    /// <summary>
    /// 日历类型
    /// </summary>
    public enum CalendarType
    {
        公历,
        租赁,
    }

    /// <summary>
    /// 结算周期类型
    /// </summary>
    public enum CycleType
    {
        固定,
        百分比,
    }

    /// <summary>
    /// 合同复制类型，用于usp_Contract_UndoContract
    /// </summary>
    public enum ContractCopyType
    {
        变更,
        续租,
        新建
    }

    /// <summary>
    /// 描述租金支付类型，有三种情况：1.预付,2.实付,3.延付
    /// </summary>
    public enum RentPaymentType
    {
        预付 = 1,
        实付 = 2,
        延付 = 3, 
    }

    /// <summary>
    /// 合同状态
    /// </summary>
    public enum ContractStatus
    {
        草稿,
        审核中,
        已生效,
        审核退回,
    }
}