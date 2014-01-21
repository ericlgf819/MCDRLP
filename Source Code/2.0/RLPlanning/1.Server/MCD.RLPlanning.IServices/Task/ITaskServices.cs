using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "ITaskServices"，也必须更新 Web.config 中对 "ITaskServices" 的引用。
    /// </summary>
    [ServiceContract]
    public interface ITaskServices : IBaseService
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
        [OperationContract]
        byte[] SelectRemindUser(Guid typeID, int module, string mattersName, string siteDeptNo, string remindNo, int status, Guid userID);

        /// <summary>
        /// 设置审核退回
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="opinion"></param>
        /// <returns></returns>
        [OperationContract]
        bool SetWorkFlowReturn(Guid remindID, string opinion);

        /// <summary>
        /// 设置审核提交
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="opinion"></param>
        /// <param name="reviewerUserID"></param>
        /// <param name="englishName"></param>
        /// <returns></returns>
        [OperationContract]
        bool SetWorkFlowSubmit(Guid remindID, string opinion, Guid reviewerUserID, string englishName);

        /// <summary>
        /// 查找历史处理任务
        /// </summary>
        /// <param name="typeID">事项主题(类型)ID</param>
        /// <param name="module">模块名称</param>
        /// <param name="mattersName">事项名称</param>
        /// <param name="siteDeptNo">餐厅/部门编号</param>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelecHistoryTask(Guid typeID, int module, string mattersName, string siteDeptNo, Guid userID, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 只查找属于用户自己的待处理任务，不包括代理别人的
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserTask(Guid userID);

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
        [OperationContract]
        byte[] SelectUnFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
            string strTaskType, string strErrorType, string strIsRead, int iTimeZoneFlag, Guid UserID);

        /// <summary>
        /// 更新任务备注信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="strRemark"></param>
        [OperationContract]
        void UpdateTaskRemark(Guid taskID, string strRemark);

        /// <summary>
        /// 将任务设置为已读
        /// </summary>
        /// <param name="taskID"></param>
        [OperationContract]
        void UpdateTaskIsRead(Guid taskID);

        /// <summary>
        /// 更新任务修正人ID与完成时间
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="UserID"></param>
        /// <param name="FinishTime"></param>
        [OperationContract]
        void UpdateTaskFinishUserIDAndTime(Guid taskID, Guid UserID, DateTime FinishTime);

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
        [OperationContract]
        byte[] SelectFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
            string strTaskType, string strErrorType, int iTimeZoneFlag, Guid UserID);
    }
}