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
    public class APBLL : BaseBLL<IAPService>
    {
        /// <summary>
        /// 获取待办已办列表
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="workflowTaskType">待办/已办类型</param>
        /// <param name="workflowDoingType">待办/已办</param>
        /// <param name="insCreateDate">流程实例发起日期</param>
        /// <returns>待办已经列表</returns>
        public DataTable SelectAPList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate)
        {
            return base.WCFService.GetAllAPList(currentUserID, workflowTaskType, workflowDoingType, insCreateDate);
        }

        /// <summary>
        /// 获取指定AP的所有计算相关的记录（表0:计算过程及结果Tab中的列表，表1:APProcessParameterValue计算过程及结果-计算过程公式，表2:APCheckResult，表3:APMessageResult）。
        /// </summary>
        /// <param name="apRecordID">待获取的AP记录ID</param>
        /// <returns>返回关联的四个表的数据</returns>
        public DataSet GetAPAllData(string apRecordID)
        {
            return base.WCFService.GetAPAllData(apRecordID);
        }

        /// <summary>
        /// 批量更新APMessage表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateMessageRemark(DataTable dt)
        {
            base.WCFService.BatchUpdateMessageRemark(dt);
        }

        /// <summary>
        /// 批量更新APCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateCheckResultRemark(DataTable dt)
        {
            base.WCFService.BatchUpdateCheckResultRemark(dt);
        }

        /// <summary>
        /// 获取指定AP记录生成的AP凭证。
        /// </summary>
        /// <param name="APRecordID"></param>
        /// <returns></returns>
        public DataTable GetAPCertificate(string APRecordID)
        {
            return base.WCFService.GetAPCertificate(APRecordID);
        }

        /// <summary>
        /// 对指定的AP记录生成AP凭证后结束流程。
        /// </summary>
        /// <param name="APRecordID"></param>
        public void GenarateAPCertificate(string procID, string currentUserID, string aprecordID)
        {
            base.WCFService.GenarateAPCertificate(procID, currentUserID, aprecordID);
        }

        /// <summary>
        /// 获取单条AP记录详情。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        public APRecordEntity GetAPRecordDetail(string apRecordID)
        {
            DataRow dr = base.WCFService.GetAPRecordDetail(apRecordID).Rows[0];
            if (dr != null)
            {
                APRecordEntity entity = new APRecordEntity();
                ReflectHelper.SetPropertiesByDataRow(ref entity, dr);
                return entity;
            }
            return null;
        }
    }
}