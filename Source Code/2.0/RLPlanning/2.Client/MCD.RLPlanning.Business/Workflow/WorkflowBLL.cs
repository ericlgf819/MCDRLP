using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Workflow;
using MCD.RLPlanning.IServices.Workflow;

namespace MCD.RLPlanning.BLL.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkflowBLL : BaseBLL<IWorkflowService>
    {
        /// <summary>
        /// 是否新建,用户新建时需要调用Create方法, 保存后将它置为FALSE 即编辑状态时不需要调用Create方法
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>
        public WorkflowType CurrentType { get; set; }
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string ProcID { get; set; }
        /// <summary>
        /// 当前任务ID
        /// </summary>
        public int CurrentTaskID { get; set; }
        /// <summary>
        /// 相关业务数据ID
        /// </summary>
        public string DataLocator { get; set; }
        /// <summary>
        /// 当前处理人, 可以是处理人也可以是代理人
        /// </summary>
        public string CurrentTaskUserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TaskEntity CurrentTask { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InstanceEntity CurrentInstance { get; set; }

        /// <summary>
        /// 初始化指定流程实例的最新任务。
        /// </summary>
        /// <param name="procID"></param>
        public void Init(string procID)
        {
            this.Init(procID, null);
        }
        /// <summary>
        /// 初始化指定流程实例的指定任务。
        /// </summary>
        /// <param name="procID"></param>
        public void Init(string procID, string taskID)
        {
            this.CurrentInstance = this.GetInstanceEntity(procID);
            this.CurrentTask = this.GetTaskEntity(procID, taskID);
            if (this.CurrentInstance != null)
            {
                this.ProcID = this.CurrentInstance.ProcID;
                this.DataLocator = this.CurrentInstance.DataLocator;
            }
            if (this.CurrentTask != null)
            {
                this.CurrentTaskID = this.CurrentTask.TaskID;
                this.CurrentTaskUserID = string.IsNullOrEmpty(this.CurrentTask.AssigneeID) ? this.CurrentTask.PartID : this.CurrentTask.AssigneeID;
            }
        }
        /// <summary>
        /// 根据流程类型以及流程关联的数据主键初始化流程的最新任务。
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="datalocator"></param>
        public void Init(WorkflowType workflowType, string datalocator)
        {
            string procID = base.WCFService.SelectProcID(datalocator);
            if (!string.IsNullOrEmpty(procID))
            {
                this.Init(procID, null);
            }
        }

        /// <summary>
        /// 获取指定指定流程指定任务的详情。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public TaskEntity GetTaskEntity(string procID, string taskID)
        {
            TaskEntity task = null;
            DataTable dt = base.WCFService.GetTask(procID, taskID);
            if (dt != null && dt.Rows.Count > 0)
            {
                task = new TaskEntity();
                ReflectHelper.SetPropertiesByDataRow<TaskEntity>(ref task, dt.Rows[0]);
            }
            return task;
        }

        /// <summary>
        /// 获取指定的流程实例详情。
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        public InstanceEntity GetInstanceEntity(string procID)
        {
            InstanceEntity instance = null;
            DataTable dt = base.WCFService.GetInst(procID);
            if (dt != null && dt.Rows.Count > 0)
            {
                instance = new InstanceEntity();
                ReflectHelper.SetPropertiesByDataRow<InstanceEntity>(ref instance, dt.Rows[0]);
            }
            return instance;
        }

        /// <summary>
        /// 创建工作流
        /// </summary>
        /// <param name="workflowType">工作流类型</param>
        /// <param name="procName">流程名称,可为空</param>
        /// <param name="dataLocator">相关业务数据主键,不可为空</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <returns>新建流程ID</returns>
        public string Create(WorkflowType workflowType, string procName, string dataLocator, string currentUserID)
        {
            return base.WCFService.Create(workflowType, procName, dataLocator, currentUserID);
        }

        /// <summary>
        /// 运行工作流,工作流往下一步流转
        /// </summary>
        /// <param name="procID">流程ID</param>
        /// <param name="userChoice">审批项</param>
        /// <param name="partComment">审批意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>
        /// <param name="nextUserID">下一步处理人,后台创建以下三个流程的时不可为空:KioskSalse收集 GL计算 AP计算</param>

        public void Run(string procID, WorkflowUserChoice userChoice, string partComment, string rejectTypeName,string currentUserID, string nextUserIDList)
        {
            base.WCFService.Run(procID, userChoice, partComment, rejectTypeName,currentUserID, nextUserIDList);
        }

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

        public string CreateAndRun(WorkflowType workflowType, string procName, string dataLocator, string currentUserID, string nextUserID)
        {
            return base.WCFService.CreateAndRun(workflowType, procName, dataLocator, currentUserID, nextUserID);
        }

        /// <summary>
        /// 保存审批意见
        /// </summary>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>
        /// <param name="partComment">审批意见</param>
        /// <param name="rejectTypeName">拒绝类型</param>

        public void SavePartComment(string procID, int taskID, string partComment, string rejectTypeName)
        {
            base.WCFService.SavePartComment(procID, taskID, partComment, rejectTypeName);
        }

        /// <summary>
        /// 更新阅读时间
        /// </summary>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>

        public void UpdateReadTime(string procID, int taskID)
        {
            base.WCFService.UpdateReadTime(procID, taskID);
        }

        /// <summary>
        /// 转派任务
        /// </summary>
        /// <param name="toUserID">转派给某用户的ID</param>
        /// <param name="procId">流程ID</param>
        /// <param name="taskID">任务ID</param>

        public void ForwardTo(string toUserID, string procID, int taskID)
        {
            base.WCFService.ForwardTo(toUserID, procID, taskID);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="procID">流程ID</param>

        public void DeleteInst(string procID)
        {
            base.WCFService.DeleteInst(procID);
        }

        /// <summary>
        /// 获取流程任务项,用于显示流程历史审批记录
        /// </summary>
        /// <param name="procID">流程ID</param>

        public DataTable SelectTaskList(string procID)
        {
            return base.WCFService.SelectTaskList(procID);
        }

        /// <summary>
        /// 获取待办已办列表
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="workflowTaskType">待办/已办类型</param>
        /// <param name="workflowDoingType">待办/已办</param>
        /// <param name="insCreateDate">流程实例发起日期</param>
        /// <returns>待办已经列表</returns>
        public DataTable SelectTaskInfoList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate)
        {
            return base.WCFService.SelectTaskInfoList(currentUserID, workflowTaskType, workflowDoingType, insCreateDate);
        }

        /// <summary>
        /// 获取已经生成凭证的待办任务。
        /// </summary>
        /// <param name="workflowTaskType"></param>
        /// <param name="currentUserID"></param>
        /// <param name="procID"></param>
        /// <param name="contractNo"></param>
        /// <param name="storeOrDeptNo"></param>
        /// <param name="rentType"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public DataTable SelectCreatedCertTaskList(WorkflowTaskType workflowTaskType, string currentUserID, string procID, string contractNo, string storeOrDeptNo, string rentType, string companyCode, int isRead)
        {
            return base.WCFService.SelectCreatedCertTaskList(workflowTaskType, currentUserID, procID, contractNo, storeOrDeptNo, rentType, companyCode,  isRead);
        }

        /// <summary>
        /// 获取流程实例编号
        /// </summary>
        /// <param name="workflowType">流程类型</param>
        /// <param name="dataLocator">相关主键</param>
        /// <returns>ProcID</returns>
        public string SelectProcID(string dataLocator)
        {
            return base.WCFService.SelectProcID(dataLocator);
        }

        /// <summary>
        /// 获取待办已办导航树的任务条数
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="doingType">待办/已办</param>
        /// <returns></returns>
        public DataSet SelectTaskCount(string currentUserID, WorkflowDoingType doingType)
        {
            return base.WCFService.SelectTaskCount(currentUserID, doingType);
        }

       /// <summary>
       /// 强制结束
       /// </summary>
       /// <param name="procID"></param>
       /// <param name="currentUserID"></param>
        public void ForceToEnd(string procID, string currentUserID)
        {
            base.WCFService.ForceToEnd(procID, currentUserID);
        }

        /// <summary>
        /// 获取已办列表
        /// </summary>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public DataTable SelectDoneTaskInfoList(string currentUserID, string procID, string storeOrDeptNo, string appCode, string appName, bool isForceToEnd, string contractNo, string rentType)
        {
            return base.WCFService.SelectDoneTaskInfoList(currentUserID, procID, storeOrDeptNo, appCode, appName, isForceToEnd, contractNo, rentType);
        }
 
         /// <summary>
        /// 获取指定流程指定步骤的参与者英文名信息。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public string GetPartUserEnglishName(string procID, string taskName)
        {
            return base.WCFService.GetPartUserEnglishName(procID, taskName);
        }

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
        public void BatchRunWorkflow(string procIDList, WorkflowUserChoice userChoice, string partComment, string rejectTypeName, string currentUserID, string nextUserIDList, out int successCount, out int failCount)
        {
            base.WCFService.BatchRunWorkflow(procIDList, userChoice, partComment, rejectTypeName, currentUserID, nextUserIDList, out successCount, out failCount);
        }

        /// <summary>
        /// 通过AppCode获取步骤信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public DataTable SelectStepInfoByAppCode(string appCode)
        {
            return base.WCFService.SelectStepInfoByAppCode(appCode);
        }

        /// <summary>
        /// 获取服务器当前时间。
        /// </summary>
        public DateTime Now
        {
            get { return base.WCFService.TestMethod(); }
        }
    }
}