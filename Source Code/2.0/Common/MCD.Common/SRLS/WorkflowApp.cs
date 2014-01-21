using System;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 
    /// </summary>
    public enum WorkflowType
    {
        NULL = 0,
        说明事项 = 1,
        跟进事项 = 2,
        CheckList事项 = 3,
        LeaseMaster事项 = 4,
        删除事项=18,
        TypeCode新增 = 5,
        TypeCode修改 = 6,
        Kiosk新增 = 7,
        Kiosk修改 = 8,
        合同新增 = 9,
        合同变更 = 10,
        合同续租 = 11,
        合同删除 = 19,
        KioskSalse收集 = 12,
        GL计算 = 13,
        AP计算 = 14,
        AP差异 =15,
        Kiosk删除=16,
        事项提醒=17
    }
    /// <summary>
    /// 
    /// </summary>
    public enum WorkflowUserChoice
    {
        NULL,
        通过,
        拒绝
    }
    /// <summary>
    /// 
    /// </summary>
    public enum WorkflowTaskType
    {
        待转派,
        待处理,
        待审核,
        被拒绝,
        甜品店Salse,
        租金AP,
        租金AP差异,
        租金GL,
        租金AP审核审批,
        租金AP差异审核审批,
        租金GL审核审批,
        租金AP凭证生成,
        租金AP差异凭证生成,
        租金GL凭证生成
    }
    /// <summary>
    /// 
    /// </summary>
    public enum WorkflowDoingType
    {
        待办,
        已办
    }
    /// <summary>
    /// 流程相关业务数据状态
    /// </summary>
    public enum WorkflowBizStatus
    {
        草稿 = 0,
        审核中 = 1,
        已生效 = 2,
        审核退回 = 3,
        已失效 = 4
    }


    /// <summary>
    /// 
    /// </summary>
    public enum RentType
    {
        //全部,
        百分比租金,
        固定租金,
        百分比服务费,
        固定管理费,
        百分比管理费
    }

    /// <summary>
    /// 
    /// </summary>
    public enum RemindImportType
    {
        导入,
        预览
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BizType
    {
        事项,
        TypeCode,
        甜品店,
        合同,
        撤销删除合同,
        AP,
        GL
    }
}