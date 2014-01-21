using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.APGL;
using MCD.RLPlanning.Entity.Workflow;
using MCD.RLPlanning.IServices.APGL;

namespace MCD.RLPlanning.BLL.APGL
{
    /// <summary>
    /// 
    /// </summary>
    public class GLBLL : BaseBLL<IGLService>
    {
        /// <summary>
        /// 获取待办已办GL列表
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="workflowTaskType">待办/已办类型</param>
        /// <param name="workflowDoingType">待办/已办</param>
        /// <param name="insCreateDate">流程实例发起日期</param>
        /// <returns>待办已经列表</returns>
        public DataTable SelectGLList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate)
        {
            return base.WCFService.GetAllGLList(currentUserID, workflowTaskType, workflowDoingType, insCreateDate);
        }
        
        /// <summary>
        /// 获取指定GL的所有计算相关的记录(GLRecord、GLProcessParameterValue、GLCheckResult、GLMessageResult、GLException)。
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <returns></returns>
        public DataSet GetGLAllData(string glRecordID)
        {
            return base.WCFService.GetGLAllData(glRecordID);
        }

        /// <summary>
        /// 保存Message的备注
        /// </summary>
        /// <param name="procId">信息ID</param>
        /// <param name="taskID">备注</param>

        /// <summary>
        /// 批量更新GLMessage表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateMessageRemark(DataTable dt)
        {
            base.WCFService.BatchUpdateMessageRemark(dt);
        }

        /// <summary>
        /// 批量更新GLCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateCheckResultRemark(DataTable dt)
        {
            base.WCFService.BatchUpdateCheckResultRemark(dt);
        }

        /// <summary>
        /// 批量更新GLException表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateExceptionRemark(DataTable dt)
        {
            base.WCFService.BatchUpdateExceptionRemark(dt);
        }

        /// <summary>
        /// 获取指定GL记录生成的GL凭证。
        /// </summary>
        /// <param name="GLRecordID"></param>
        /// <returns></returns>
        public DataTable GetGLCertificate(string GLRecordID)
        {
            return base.WCFService.GetGLCertificate(GLRecordID);
        }

        /// <summary>
        /// 对指定的GL记录生成GL凭证后结束流程。
        /// </summary>
        /// <param name="GLRecordID"></param>
        public void GenarateGLCertificate(string procID, string currentUserID, string glRecordID)
        {
            base.WCFService.GenarateGLCertificate(procID, currentUserID, glRecordID);
        }

        /// <summary>
        /// 获取单条GL记录详情。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        public GLRecordEntity GetGLRecordDetail(string glRecordID)
        {
            DataRow dr = base.WCFService.GetGLRecordDetail(glRecordID).Rows[0];
            if (dr != null)
            {
                GLRecordEntity entity = new GLRecordEntity();
                ReflectHelper.SetPropertiesByDataRow(ref entity, dr);
                return entity;
            }
            return null;
        }
    }
}