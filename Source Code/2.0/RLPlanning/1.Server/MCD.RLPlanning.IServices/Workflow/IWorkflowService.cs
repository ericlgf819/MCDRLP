using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Workflow;

namespace MCD.RLPlanning.IServices.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IWorkflowService :IBaseService
    {
        /// <summary>
        /// 创建工作流
        /// </summary>
        /// <param name="workflowType">工作流类型</param>
        /// <param name="procName">流程名称,可为空</param>
        /// <param name="dataLocator">相关业务数据主键,不可为空</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <returns>新建流程ID</returns>
        [OperationContract]
        string  Create(WorkflowType workflowType,string procName,string dataLocator,string currentUserID);

        /// <summary>
        /// 运行工作流,工作流往下一步流转
        /// </summary>
        /// <param name="procID">流程ID</param>
        /// <param name="userChoice">审批项</param>
        /// <param name="partComment">审批意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>
        /// <param name="nextUserID">下一步处理人,后台创建以下三个流程的时不可为空:KioskSalse收集 GL计算 AP计算</param>
        [OperationContract]
        void Run(string procID,WorkflowUserChoice userChoice,string partComment,string rejectTypeName,string currentUserID,string nextUserIDList);

        /// <summary>
        /// 创建并自动流转
        /// </summary>
        /// <param name="workflowType">工作流类型</param>
        /// <param name="procName">流程名称,可为空</param>
        /// <param name="dataLocator">相关业务数据主键,不可为空</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="userChoice">审批项</param>
        /// <param name="partComment">审批意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>
        /// <param name="nextUserID">下一步处理人,后台创建以下三个流程的时不可为空:KioskSalse收集 GL计算 AP计算</param>
        /// <returns>流程ID</returns>
        [OperationContract]
        string CreateAndRun(WorkflowType workflowType, string procName, string dataLocator, string currentUserID,  string nextUserID);

        /// <summary>
        /// 保存审批意见
        /// </summary>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>
        /// <param name="partComment">审批意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>
        [OperationContract]
        void SavePartComment(string procID,int taskID,string partComment,string rejectTypeName);

        /// <summary>
        /// 更新阅读时间
        /// </summary>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>
        [OperationContract]
        void UpdateReadTime(string procID, int taskID);

        /// <summary>
        /// 转派任务
        /// </summary>
        /// <param name="toUserID">转派给某用户的ID</param>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>
        [OperationContract]
        void ForwardTo(string toUserID,string procID,int taskID);

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="procID">流程ID</param>
        [OperationContract]
        void DeleteInst(string procID);

        /// <summary>
        /// 获取流程任务项,用于显示流程历史审批记录
        /// </summary>
        /// <param name="procID">流程ID</param>
        [OperationContract]
        DataTable SelectTaskList(string procID);

        /// <summary>
        /// 获取待办已办列表
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="workflowTaskType">待办/已办类型</param>
        /// <param name="workflowDoingType">待办/已办</param>
        /// <param name="insCreateDate">流程实例发起日期</param>
        /// <returns>待办已经列表</returns>
        [OperationContract]
        DataTable SelectTaskInfoList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate);

        /// <summary>
        /// 获取已办列表
        /// </summary>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectDoneTaskInfoList(string currentUserID, string procID, string storeOrDeptNo, string appCode, string appName, bool isForceToEnd, string contractNo, string rentType);

        [OperationContract]
        DataTable SelectCreatedCertTaskList(WorkflowTaskType workflowTaskType, string currentUserID, string procID, string contractNo, string storeOrDeptNo, string rentType, string companyCode, int isRead);

        /// <summary>
        /// 获取流程实例编号
        /// </summary>
        /// <param name="workflowType">流程类型</param>
        /// <param name="dataLocator">相关主键</param>
        /// <returns>ProcID</returns>
        [OperationContract]
        string SelectProcID(string dataLocator);

        /// <summary>
        /// 获取待办已办导航树的任务条数
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="doingType">待办/已办</param>
        /// <returns></returns>
        [OperationContract]
        DataSet SelectTaskCount(string currentUserID, WorkflowDoingType doingType);

        /// <summary>
        /// 初始化当前实例
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetInst(string procID);

        /// <summary>
        /// 获取指定任务实体。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetTask(string procID, string taskID);

        /// <summary>
        /// 强制结束
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        [OperationContract]
        void ForceToEnd(string procID, string currentUserID);

        /// <summary>
        /// 获取指定流程指定步骤的参与者英文名信息。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        [OperationContract]
        string GetPartUserEnglishName(string procID, string taskName);

        /// <summary>
        /// 批量运行流程。
        /// </summary>
        /// <param name="procIDList">流程ID字符串，多个之间用";"隔开</param>
        /// <param name="userChoice">通过还是拒绝</param>
        /// <param name="partComment">意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>
        /// <param name="currentUserID">流程当前处理人</param>
        /// <param name="nextUserIDList">下一步处理人</param>
        /// <param name="successCount">返回运行成功的数目</param>
        /// <param name="failCount">返回运行成功的数目</param>
        [OperationContract]
        void BatchRunWorkflow(string procIDList, WorkflowUserChoice userChoice, string partComment, string rejectTypeName, string currentUserID, string nextUserIDList, out int successCount, out int failCount);

        /// <summary>
        /// 通过AppCode获取步骤信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectStepInfoByAppCode(string appCode);
    }
}