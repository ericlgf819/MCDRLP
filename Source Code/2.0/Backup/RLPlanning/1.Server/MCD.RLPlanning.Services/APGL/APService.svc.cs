using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Workflow;
using MCD.RLPlanning.IServices.APGL;
using MCD.RLPlanning.IServices.Workflow;

namespace MCD.RLPlanning.Services.APGL
{
    /// <summary>
    /// 
    /// </summary>
    public class APService : IAPService
    {
        public DataTable GetAllAPList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, 
            DateTime insCreateDate)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            return db.ExecuteDataSet("dbo.usp_Workflow_SelectTaskInfoList", currentUserID, workflowTaskType.ToString(), 
                workflowDoingType.ToString(), insCreateDate).Tables[0];
        }

        /// <summary>
        /// 获取指定AP的所有计算相关的记录。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        public DataSet GetAPAllData(string apRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_APGL_SelectAPAllData");
            db.AddInParameter(cmd, "APRecordID", DbType.String, apRecordID);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 生成AP凭证。
        /// </summary>
        /// <param name="APRecordID"></param>
        public void GenarateAPCertificate(string procID, string currentUserID, string aprecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Cal_CalFixedAPCertificate");
            db.AddInParameter(cmd, "ProcID", DbType.String, procID);
            db.AddInParameter(cmd, "CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "APRecordID", DbType.String, aprecordID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取指定AP的所有凭证记录。
        /// </summary>
        /// <param name="APRecordID"></param>
        /// <returns></returns>
        public DataTable GetAPCertificate(string APRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = string.Format("SELECT c.*,r.PaymentType FROM APCertificate c INNER JOIN APRecord r ON c.APRecordID=r.APRecordID WHERE c.APRecordID='{0}'", APRecordID);
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获取单条AP记录详情。
        /// </summary>
        /// <param name="apRecordID"></param>
        /// <returns></returns>
        public DataTable GetAPRecordDetail(string apRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = string.Format("SELECT * FROM APRecord WHERE APRecordID='{0}'", apRecordID);
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 批量更新APMessage表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateMessageRemark(DataTable dt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand comm = db.GetSqlStringCommand("UPDATE APMessageResult SET Remark=@Remark WHERE MessageID=@MessageID");
                comm.Connection = conn;
                comm.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 2000, "Remark"));
                comm.Parameters.Add(new SqlParameter("@MessageID", SqlDbType.NVarChar, 50, "MessageID"));
                comm.UpdatedRowSource = UpdateRowSource.None;

                DbDataAdapter adp = db.GetDataAdapter();
                adp.UpdateCommand = comm;
                adp.UpdateBatchSize = 0;
                adp.Update(dt);
                conn.Close();
                adp.Dispose();
            }
        }

        /// <summary>
        /// 批量更新APCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateCheckResultRemark(DataTable dt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand comm = db.GetSqlStringCommand("UPDATE APCheckResult SET Remark=@Remark WHERE CheckResultID=@CheckResultID");
                comm.Connection = conn;
                comm.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 2000, "Remark"));
                comm.Parameters.Add(new SqlParameter("@CheckResultID", SqlDbType.NVarChar, 50, "CheckResultID"));
                comm.UpdatedRowSource = UpdateRowSource.None;

                DbDataAdapter adp = db.GetDataAdapter();
                adp.UpdateCommand = comm;
                adp.UpdateBatchSize = 0;
                adp.Update(dt);
                conn.Close();
                adp.Dispose();
            }
        }

        #region IBaseService

        public DateTime TestMethod()
        {
            return DateTime.Now;
        }
        #endregion
    }
}