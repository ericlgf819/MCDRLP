using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.Entity.Task;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace MCD.RLPlanning.Services.Task
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskServices : BaseDAL<TaskEntity>, ITaskServices
    {
        //互斥量
        static private object s_mutexobj = new object();

        //Fields
        private string connectionString = string.Empty;
        
        //Methods
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected override string ConnectionString
        {
            get
            {
                if (this.connectionString == string.Empty)
                {
                    this.connectionString = ConfigurationManager.ConnectionStrings["DBConnection"] + string.Empty;
                }
                return this.connectionString;
            }
        }

        /// <summary>
        /// 设置审核退回
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="opinion"></param>
        /// <returns></returns>
        public bool SetWorkFlowReturn(Guid remindID, string opinion)
        {
            return ExecuteProcedureBoolean((cmd) => {
                cmd.CommandText = "SRLS_USP_WorkFlow_Return";
                cmd.Parameters.Add(new SqlParameter("@RemindID", SqlDbType.UniqueIdentifier, 36) { Value = remindID });
                cmd.Parameters.Add(new SqlParameter("@Opinion", SqlDbType.NVarChar, 512) { Value = opinion });
            });
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
            return ExecuteProcedureBoolean((cmd) => {
                cmd.CommandText = "SRLS_USP_WorkFlow_Submit";
                cmd.Parameters.Add(new SqlParameter("@RemindID", SqlDbType.UniqueIdentifier, 36) { Value = remindID });
                cmd.Parameters.Add(new SqlParameter("@Opinion", SqlDbType.NVarChar, 512) { Value = opinion });
                cmd.Parameters.Add(new SqlParameter("@CurUserID", SqlDbType.UniqueIdentifier, 36) { Value = reviewerUserID });
                cmd.Parameters.Add(new SqlParameter("@CurEnglishName", SqlDbType.NVarChar, 32) { Value = englishName });
            });
        }

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
        public byte[] SelectRemindUser(Guid typeID, int module, string mattersName, string siteDeptNo, string remindNo, int status, Guid userID)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "[SRLS_USP_WorkFlow_SelectTracking]";
                cmd.Parameters.Add(new SqlParameter("@TypeID", SqlDbType.UniqueIdentifier, 36) { Value = typeID });
                cmd.Parameters.Add(new SqlParameter("@Module", SqlDbType.Int, 4) { Value = module });
                cmd.Parameters.Add(new SqlParameter("@MattersName", SqlDbType.NVarChar, 32) { Value = mattersName });
                cmd.Parameters.Add(new SqlParameter("@SiteDeptNo", SqlDbType.NVarChar, 32) { Value = siteDeptNo });
                cmd.Parameters.Add(new SqlParameter("@RemindNo", SqlDbType.NVarChar, 16) { Value = remindNo });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4) { Value = status });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            });
            return base.Serilize(ds);
        }

        /// <summary>
        /// 只查找属于用户自己的待处理任务，不包括代理别人的
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public byte[] SelectUserTask(Guid userID)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "[SRLS_USP_WorkFlow_SelectUserTask]";
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            });
            return base.Serilize(ds);
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
        public byte[] SelecHistoryTask(Guid typeID, int module, string mattersName, string siteDeptNo, Guid userID, DateTime startDate, DateTime endDate)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "[SRLS_USP_WorkFlow_SelectHistoryTracking]";
                cmd.Parameters.Add(new SqlParameter("@TypeID", SqlDbType.UniqueIdentifier, 36) { Value = typeID });
                cmd.Parameters.Add(new SqlParameter("@Module", SqlDbType.Int, 4) { Value = module });
                cmd.Parameters.Add(new SqlParameter("@MattersName", SqlDbType.NVarChar, 32) { Value = mattersName });
                cmd.Parameters.Add(new SqlParameter("@SiteDeptNo", SqlDbType.NVarChar, 32) { Value = siteDeptNo });
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = startDate });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = endDate });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            });
            return base.Serilize(ds);
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
        /// <param name="iTimeZoneFlag">0:当天, 1:当月, 2:当年, 3: 往年</param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] SelectUnFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
             string strTaskType, string strErrorType, string strIsRead, int iTimeZoneFlag, Guid UserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");

            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Task_SelectUnFinishedTasks");

            if (!String.IsNullOrEmpty(strArea))
                db.AddInParameter(cmd, "@AreaName", DbType.String, strArea);
            if (!String.IsNullOrEmpty(strCompanyCode))
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, strCompanyCode);
            if (!String.IsNullOrEmpty(strStoreOrKioskNo))
                db.AddInParameter(cmd, "@StoreOrKioskNo", DbType.String, strStoreOrKioskNo);
            if (!String.IsNullOrEmpty(strTaskType))
                db.AddInParameter(cmd, "@TaskType", DbType.String, strTaskType);
            if (!String.IsNullOrEmpty(strErrorType))
                db.AddInParameter(cmd, "@ErrorType", DbType.String, strErrorType);
            if (!String.IsNullOrEmpty(strIsRead))
                db.AddInParameter(cmd, "@IsRead", DbType.String, strIsRead);

            db.AddInParameter(cmd, "@TimeZoneFlag", DbType.Int32, iTimeZoneFlag);
            db.AddInParameter(cmd, "@UserID", DbType.Guid, UserID);

            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 更新任务备注信息
        /// </summary>
        /// <param name="taskID">CheckID</param>
        /// <param name="strRemark"></param>
        public void UpdateTaskRemark(Guid taskID, string strRemark)
        {
            lock (s_mutexobj)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Task_UpdateRemark");
                db.AddInParameter(cmd, "@TaskID", DbType.Guid, taskID);
                db.AddInParameter(cmd, "@Remark", DbType.String, strRemark);
                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 更新任务已读标志
        /// </summary>
        /// <param name="taskID"></param>
        public void UpdateTaskIsRead(Guid taskID)
        {
            lock (s_mutexobj)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Task_UpdateIsRead");
                db.AddInParameter(cmd, "@TaskID", DbType.Guid, taskID);
                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 更新任务修正人ID与完成时间
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="UserID"></param>
        /// <param name="FinishTime"></param>
        public void UpdateTaskFinishUserIDAndTime(Guid taskID, Guid UserID, DateTime FinishTime)
        {
            lock (s_mutexobj)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Task_UpdateFixUserIDAndTime");
                db.AddInParameter(cmd, "@TaskID", DbType.Guid, taskID);
                db.AddInParameter(cmd, "@FixUserID", DbType.Guid, UserID);
                db.AddInParameter(cmd, "@FixTime", DbType.DateTime, FinishTime);
                db.ExecuteNonQuery(cmd);
            }
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
        public byte[] SelectFinishedTask(string strArea, string strCompanyCode, string strStoreOrKioskNo,
            string strTaskType, string strErrorType, int iTimeZoneFlag, Guid UserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");

            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Task_SelectFinishedTasks");

            if (!String.IsNullOrEmpty(strArea))
                db.AddInParameter(cmd, "@AreaName", DbType.String, strArea);
            if (!String.IsNullOrEmpty(strCompanyCode))
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, strCompanyCode);
            if (!String.IsNullOrEmpty(strStoreOrKioskNo))
                db.AddInParameter(cmd, "@StoreOrKioskNo", DbType.String, strStoreOrKioskNo);
            if (!String.IsNullOrEmpty(strTaskType))
                db.AddInParameter(cmd, "@TaskType", DbType.String, strTaskType);
            if (!String.IsNullOrEmpty(strErrorType))
                db.AddInParameter(cmd, "@ErrorType", DbType.String, strErrorType);

            db.AddInParameter(cmd, "@TimeZoneFlag", DbType.Int32, iTimeZoneFlag);
            db.AddInParameter(cmd, "@UserID", DbType.Guid, UserID);

            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
    }
}