using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using MCD.Common.SRLS;

namespace MCD.RLPlanning.IServices.APGL
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IGLService : IBaseService
    {
        /// <summary>
        /// 获取全部GL记录。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllGLList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate);

        /// <summary>
        /// 获取指定GL记录的所有关联数据。
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetGLAllData(string glRecordID);

        /// <summary>
        /// 获取指定AP的凭证。
        /// </summary>
        /// <param name="GLRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetGLCertificate(string glRecordID);

        /// <summary>
        /// 获取单条GL记录详情。
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetGLRecordDetail(string glRecordID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void BatchUpdateMessageRemark(DataTable dt);

        /// <summary>
        /// 批量更新GLCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void BatchUpdateCheckResultRemark(DataTable dt);

        /// <summary>
        /// 批量更新GLException表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void BatchUpdateExceptionRemark(DataTable dt);

        /// <summary>
        /// 重写生成AP凭证。
        /// </summary>
        /// <param name="GLRecordID"></param>
        [OperationContract]
        void GenarateGLCertificate(string procID, string currentUserID, string glRecordID);
    }
}