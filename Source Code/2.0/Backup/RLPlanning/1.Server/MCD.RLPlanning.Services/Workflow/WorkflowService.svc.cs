using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common.SRLS;
using MCD.RLPlanning.IServices.Workflow;
using MCD.RLPlanning.Entity.Workflow;

namespace MCD.RLPlanning.Services.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
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
        public void BatchRunWorkflow(string procIDList, WorkflowUserChoice userChoice, string partComment, string rejectTypeName, 
            string currentUserID, string nextUserIDList, out int successCount, out int failCount)
        {
            successCount = failCount = 0;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Workflow_VolumeRun");
            cmd.CommandTimeout = 0;
            db.AddInParameter(cmd, "@ProcIDList", DbType.String, procIDList);
            db.AddInParameter(cmd, "@UserChoice", DbType.String, userChoice == WorkflowUserChoice.NULL ? null : userChoice.ToString());
            db.AddInParameter(cmd, "@PartComment", DbType.String, partComment);
            db.AddInParameter(cmd, "@RejectTypeName", DbType.String, rejectTypeName);
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "@NextUserIDList", DbType.String, nextUserIDList);
            db.AddOutParameter(cmd, "@TotalCount", DbType.Int32, 0);
            db.AddOutParameter(cmd, "@SuccessCount", DbType.Int32, 0);
            db.AddOutParameter(cmd, "@FailCount", DbType.Int32, 0);
            db.ExecuteNonQuery(cmd);
            //
            if (cmd.Parameters["@SuccessCount"].Value != null)
            {
                successCount = Convert.ToInt32(cmd.Parameters["@SuccessCount"].Value);
            }
            if (cmd.Parameters["@FailCount"].Value != null)
            {
                failCount = Convert.ToInt32(cmd.Parameters["@FailCount"].Value);
            }
        }

        #region IBaseService Members

        public DateTime TestMethod()
        {
            return DateTime.Now;
        }
        #endregion

        #region IWorkflowService Members

        public string Create(WorkflowType workflowType, string procName, string dataLocator, string currentUserID)
        {
            string procID;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            if (string.IsNullOrEmpty(procName))
            {
                procName = "";
            }
            int appCode = Convert.ToInt16(workflowType);
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Workflow_Create");
            db.AddInParameter(cmd, "@AppCode", DbType.Int32, appCode);
            db.AddInParameter(cmd, "@ProcName", DbType.String, procName);
            db.AddInParameter(cmd, "@DataLocator", DbType.String, dataLocator);
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.AddOutParameter(cmd, "@ProcID", DbType.String, 50);
            db.ExecuteNonQuery(cmd);
            //
            procID = cmd.Parameters["@ProcID"].Value.ToString();
            return procID;
        }

        public void Run(string procID, WorkflowUserChoice userChoice, string partComment, string rejectTypeName, 
            string currentUserID, string nextUserIDList)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_Run", procID, this.GetStrValue(userChoice), partComment, rejectTypeName, 
                currentUserID, nextUserIDList);
        }

        public string CreateAndRun(WorkflowType workflowType, string procName, string dataLocator, string currentUserID, 
            string nextUserID)
        {
            string procID;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            if (string.IsNullOrEmpty(procName))
            {
                procName = "";
            }
            int appCode = Convert.ToInt16(workflowType);
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Workflow_CreateAndRun");
            db.AddInParameter(cmd, "@AppCode", DbType.Int32, appCode);
            db.AddInParameter(cmd, "@ProcName", DbType.String, procName);
            db.AddInParameter(cmd, "@DataLocator", DbType.String, dataLocator);
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.AddOutParameter(cmd, "@ProcID", DbType.String, 50);
            db.AddInParameter(cmd, "@NextUserID", DbType.String, nextUserID);
            db.ExecuteNonQuery(cmd);
            //
            procID = cmd.Parameters["@ProcID"].Value.ToString();
            return procID;
        }

        public void SavePartComment(string procID, int taskID, string partComment, string rejectTypeName)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_SavePartComment", procID, taskID, partComment, rejectTypeName);
        }

        public void UpdateReadTime(string procID, int taskID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_UpdateReadTime", procID, taskID);
        }

        public void ForwardTo(string toUserID, string procID, int taskID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_ForwardTo", toUserID, procID, taskID);
        }

        public void DeleteInst(string procID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_DeleteInst", procID);
        }

        public DataTable SelectTaskList(string procID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            return db.ExecuteDataSet("dbo.usp_Workflow_SelectTaskList", procID).Tables[0];
        }

        public DataTable SelectTaskInfoList(string currentUserID, WorkflowTaskType workflowTaskType, WorkflowDoingType workflowDoingType, DateTime insCreateDate)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            return db.ExecuteDataSet("dbo.usp_Workflow_SelectTaskInfoList", currentUserID, workflowTaskType.ToString(), workflowDoingType.ToString(), insCreateDate).Tables[0];
        }

        public string SelectProcID(string dataLocator)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            string procID = Convert.ToString(db.ExecuteScalar("dbo.usp_Workflow_SelectProcID", dataLocator));
            return procID;
        }

        public DataSet SelectTaskCount(string currentUserID, WorkflowDoingType doingType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            return db.ExecuteDataSet("dbo.usp_Workflow_SelectLeftMenuCount", currentUserID, doingType.ToString());
        }

        public DataTable GetInst(string procID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataSet ds = db.ExecuteDataSet("dbo.usp_Workflow_SelectInst", procID);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetTask(string procID, string taskID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataSet ds = db.ExecuteDataSet("dbo.usp_Workflow_SelectTask", procID, taskID);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public void ForceToEnd(string procID, string currentUserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Workflow_ForceToEnd", procID, currentUserID);
        }

        public DataTable SelectDoneTaskInfoList(string currentUserID, string procID, string storeOrDeptNo, string appCode, string appName, bool isForceToEnd, string contractNo, string rentType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Workflow_SelectDoneTaskInfoList");
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "@ProcID", DbType.String, procID);
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, storeOrDeptNo);
            db.AddInParameter(cmd, "@AppCode", DbType.String, appCode);
            db.AddInParameter(cmd, "@AppName", DbType.String, appName);
            db.AddInParameter(cmd, "@IsForceToEnd", DbType.Boolean, isForceToEnd);
            db.AddInParameter(cmd, "@ContractNo", DbType.String, contractNo);
            db.AddInParameter(cmd, "@RentType", DbType.String, rentType);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public DataTable SelectCreatedCertTaskList(WorkflowTaskType workflowTaskType, string currentUserID, string procID, string contractNo, string storeOrDeptNo, string rentType, string companyCode, int isRead)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Workflow_SelectCreatedCertTaskList");
            db.AddInParameter(cmd, "@TaskType", DbType.String, workflowTaskType.ToString());
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.AddInParameter(cmd, "@ProcID", DbType.String, procID);
            db.AddInParameter(cmd, "@ContractNo", DbType.String, contractNo);
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, storeOrDeptNo);
            db.AddInParameter(cmd, "@RentType", DbType.String, rentType);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, companyCode);
            db.AddInParameter(cmd, "@IsRead", DbType.Int32, isRead);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        /// <summary>
        /// 获取指定流程指定步骤的参与者英文名信息。
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public string GetPartUserEnglishName(string procID, string taskName)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT TOP 1 PartName FROM dbo.WF_Work_Items WHERE ProcID=@ProcID AND TaskName=@TaskName ORDER BY TaskID DESC");
            db.AddInParameter(cmd, "@ProcID", DbType.String, procID);
            db.AddInParameter(cmd, "@TaskName", DbType.String, taskName);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// 通过AppCode获取步骤信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public DataTable SelectStepInfoByAppCode(string appCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            return db.ExecuteDataSet(CommandType.Text,
                string.Format("SELECT * FROM dbo.WorkflowStepInfo WHERE AppCode = '{0}'", appCode)).Tables[0];
        }
        #endregion

        private string GetStrValue(WorkflowUserChoice userChoice)
        {
            string result = null;
            if (userChoice != WorkflowUserChoice.NULL)
            {
                result = userChoice.ToString();
            }
            return result;
        }
    }
}