using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL.Task
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskBLL : BaseBLL<ITaskServices>
    {
        /// <summary>
        /// 查找当前用户的待处理任务
        /// </summary>
        /// <param name="typeID">事项主题(类型)ID</param>
        /// <param name="module">模块名称</param>
        /// <param name="mattersName">事项名称</param>
        /// <param name="siteDeptNo">餐厅/部门编号</param>
        /// <param name="remindNo">事项编号</param>
        /// <param name="status">任务状态</param>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public DataSet SelectRemindUser(Guid typeID, int module, string mattersName, string siteDeptNo, string remindNo, int status, Guid userID)
        {
            return base.DeSerilize(base.WCFService.SelectRemindUser(typeID, module, mattersName, siteDeptNo, remindNo, status, userID));
        }

        /// <summary>
        /// 设置审核退回
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="opinion"></param>
        /// <returns></returns>
        public bool SetWorkFlowReturn(Guid remindID, string opinion)
        {
            return base.WCFService.SetWorkFlowReturn(remindID, opinion);
        }

        /// <summary>
        /// 设置审核提交
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="opinion"></param>
        /// <param name="reviewerUserID"></param>
        /// <param name="englishName"></param>
        /// <returns></returns>
        public bool SetWorkFlowSubmit(Guid remindID, string opinion, Guid reviewerUserID, string englishName)
        {
            return base.WCFService.SetWorkFlowSubmit(remindID, opinion, reviewerUserID, englishName);
        }

        /// <summary>
        /// 查找历史处理任务
        /// </summary>
        /// <param name="typeID">事项主题(类型)ID</param>
        /// <param name="module">模块名称</param>
        /// <param name="mattersName">事项名称</param>
        /// <param name="siteDeptNo">餐厅/部门编号</param>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public DataSet SelecHistoryTask(Guid typeID, int module, string mattersName, string siteDeptNo, Guid userID, DateTime startDate, DateTime endDate)
        {
            return base.DeSerilize(base.WCFService.SelecHistoryTask(typeID, module, mattersName, siteDeptNo, userID, startDate, endDate));
        }

        /// <summary>
        /// 只查找属于用户自己的待处理任务，不包括代理别人的
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet SelectUserTask(Guid userID)
        {
            return base.DeSerilize(base.WCFService.SelectUserTask(userID));
        }

        /// <summary>
        /// 查找未完成的任务
        /// </summary>
        /// <param name="strArea"></param>
        /// <param name="strCompanyCode"></param>
        /// <param name="strStoreOrKioskNo"></param>
        /// <param name="strTaskType"></param>
        /// <param name="strErrorType"></param>
        /// <param name="strIsRead"></param>
        /// <param name="iTimeZoneFlag"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet SelectUnFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
             string strTaskType, string strErrorType, string strIsRead, int iTimeZoneFlag, Guid UserID)
        {
            if (null == WCFService)
                return null;

            return base.DeSerilize(base.WCFService.SelectUnFinishedTask(strArea, strCompanyCode, strStoreOrKioskNo,
                strTaskType, strErrorType, strIsRead, iTimeZoneFlag, UserID));
        }

        /// <summary>
        /// 更新任务备注
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="strRemark"></param>
        public void UpdateTaskRemark(Guid taskID, string strRemark)
        {
            if (null == WCFService)
                return;

            WCFService.UpdateTaskRemark(taskID,strRemark);
        }

        /// <summary>
        /// 更新已读标志
        /// </summary>
        /// <param name="taskID"></param>
        public void UpdateTaskIsRead(Guid taskID)
        {
            if (null == WCFService)
                return;

            WCFService.UpdateTaskIsRead(taskID);
        }

        /// <summary>
        /// 更新任务修正人ID与完成时间
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="UserID"></param>
        /// <param name="FinishTime"></param>
        public void UpdateTaskFinishUserIDAndTime(Guid taskID, Guid UserID, DateTime FinishTime)
        {
            if (null == WCFService)
                return;

            WCFService.UpdateTaskFinishUserIDAndTime(taskID, UserID, FinishTime);
        }

        /// <summary>
        /// 查找已完成的任务
        /// </summary>
        /// <param name="strArea"></param>
        /// <param name="strCompanyCode"></param>
        /// <param name="strStoreOrKioskNo"></param>
        /// <param name="strTaskType"></param>
        /// <param name="strErrorType"></param>
        /// <param name="iTimeZoneFlag"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet SelectFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
             string strTaskType, string strErrorType, int iTimeZoneFlag, Guid UserID)
        {
            if (null == WCFService)
                return null;

            return base.DeSerilize(base.WCFService.SelectFinishedTask(strArea, strCompanyCode, strStoreOrKioskNo,
                strTaskType, strErrorType, iTimeZoneFlag, UserID));
        }
    }
}