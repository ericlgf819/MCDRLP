using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Workflow;

namespace MCD.RLPlanning.IServices.APGL
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IAPService : IBaseService
    {
        /// <summary>
        /// 获取全部AP记录。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllAPList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate);

        /// <summary>
        /// 获取指定AP记录的所有关联数据。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetAPAllData(string apRecordID);

        /// <summary>
        /// 获取单条AP记录详情。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAPRecordDetail(string apRecordID);

        /// <summary>
        /// 获取指定AP的凭证。
        /// </summary>
        /// <param name="APRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAPCertificate(string APRecordID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void BatchUpdateMessageRemark(DataTable dt);

        /// <summary>
        /// 批量更新APCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void BatchUpdateCheckResultRemark(DataTable dt);

        /// <summary>
        /// 重写生成AP凭证。
        /// </summary>
        /// <param name="APRecordID"></param>
        [OperationContract]
        void GenarateAPCertificate(string procID, string currentUserID, string aprecordID);
    }
}