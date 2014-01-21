using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using MCD.RLPlanning.IServices.APGL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using MCD.RLPlanning.Entity.Workflow;
using MCD.Common.SRLS;
using System.Data.SqlClient;

namespace MCD.RLPlanning.Services.APGL
{
    /// <summary>
    /// 
    /// </summary>
    public class GLService : IGLService
    {
        /// <summary>
        /// 获取GL待办任务列表。
        /// </summary>
        /// <param name="currentUserID"></param>
        /// <param name="workflowTaskType"></param>
        /// <param name="workflowDoingType"></param>
        /// <param name="insCreateDate"></param>
        /// <returns></returns>
        public DataTable GetAllGLList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Workflow_SelectTaskInfoList");
            db.AddInParameter(cmd, "CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "TaskType", DbType.String, workflowTaskType.ToString());
            db.AddInParameter(cmd, "DoingType", DbType.String, workflowDoingType.ToString());
            db.AddInParameter(cmd, "InsCreateDate", DbType.DateTime, insCreateDate);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        /// <summary>
        /// 获取指定GL的所有计算相关的记录(GLRecord、GLProcessParameterValue、GLCheckResult、GLMessageResult、GLException)。
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <returns></returns>
        public DataSet GetGLAllData(string glRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_APGL_SelectGLAllData");
            db.AddInParameter(cmd, "GLRecordID", DbType.String, glRecordID);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 生成GL凭证。
        /// </summary>
        /// <param name="GLRecordID"></param>
        public void GenarateGLCertificate(string procID, string currentUserID, string glRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Cal_CalFixedGLCertificate");
            db.AddInParameter(cmd, "ProcID", DbType.String, procID);
            db.AddInParameter(cmd, "CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "GLRecordID", DbType.String, glRecordID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取指定GL的凭证。
        /// </summary>
        /// <param name="GLRecordID"></param>
        /// <returns></returns>
        public DataTable GetGLCertificate(string GLRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = string.Format("SELECT c.*,r.PaymentType FROM GLCertificate c INNER JOIN GLRecord r ON c.GLRecordID=r.GLRecordID WHERE c.GLRecordID='{0}'", GLRecordID);
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获取单条GL记录详情。
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <returns></returns>
        public DataTable GetGLRecordDetail(string glRecordID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = string.Format("SELECT *,dbo.fn_Cal_IsNullGLExceptionRemark(GLRecordID) AS IsNullGLExceptionRemark FROM GLRecord WHERE GLRecordID='{0}'", glRecordID);
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 批量更新GLMessage表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateMessageRemark(DataTable dt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand comm = db.GetSqlStringCommand("UPDATE GLMessageResult SET Remark=@Remark WHERE MessageID=@MessageID");
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
        /// 批量更新GLCheckResult表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateCheckResultRemark(DataTable dt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand comm = db.GetSqlStringCommand("UPDATE GLCheckResult SET Remark=@Remark WHERE CheckResultID=@CheckResultID");
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

        /// <summary>
        /// 批量更新GLException表中的Remark字段。
        /// </summary>
        /// <param name="dt"></param>
        public void BatchUpdateExceptionRemark(DataTable dt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand comm = db.GetSqlStringCommand("UPDATE GLException SET Remark=@Remark WHERE GLExceptionID=@GLExceptionID");
                comm.Connection = conn;
                comm.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 2000, "Remark"));
                comm.Parameters.Add(new SqlParameter("@GLExceptionID", SqlDbType.NVarChar, 50, "GLExceptionID"));
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