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

namespace MCD.RLPlanning.Services.APGL
{
    /// <summary>
    /// 
    /// </summary>
    public class CALService : ICALService
    {
        #region ICALService

        public bool CheckFormulaStringSmallCycle(string formula)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            object obj = db.ExecuteScalar("dbo.usp_Cal_CheckFormulaStringSmallCycle", formula);
            return Convert.ToBoolean(obj);
        }

        public bool CheckFormulaStringBigCycle(string formula)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            object obj = db.ExecuteScalar("dbo.usp_Cal_CheckFormulaStringBigCycle", formula);
            return Convert.ToBoolean(obj);
        }

        public decimal CalFormula(string formula)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            object obj = db.ExecuteScalar("dbo.usp_Cal_FormulaWithMax", formula);
            return Convert.ToDecimal(obj);
        }

        public bool CheckSQLCondition(string condition)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            object obj = db.ExecuteScalar("dbo.usp_Cal_CheckSQLCondition", condition);
            return Convert.ToBoolean(obj);
        }

        public string GetCertificateFlowNumber()
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_APGL_FileFlowNumber");
            db.AddOutParameter(cmd, "FlowNumber", System.Data.DbType.String, 50);
            db.ExecuteNonQuery(cmd);
            object obj = cmd.Parameters["@FlowNumber"].Value;
            return obj == null ? string.Empty : Convert.ToString(obj);
        }

        public bool ReCaculateSingleAPGL(string procID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Cal_ReCalOneAPGL");
            db.AddInParameter(cmd, "ProcID", System.Data.DbType.String, procID);
            db.AddOutParameter(cmd, "IsSuccess", DbType.Boolean, 0);
            db.ExecuteNonQuery(cmd);
            object result = cmd.Parameters["@IsSuccess"].Value;
            if (result != DBNull.Value)
            {
                return Convert.ToBoolean(result);
            }
            return false;
        }

        public bool ReCaculateSingleZXGLByStage(string glRecordID, int stage)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Cal_CalZXGLByStage");
            db.AddInParameter(cmd, "GLRecordID", System.Data.DbType.String, glRecordID);
            db.AddInParameter(cmd, "Stage", DbType.Int32, stage);
            db.ExecuteNonQuery(cmd);
            return true;
        }

        public void ReCaculateAPGL(string procIDList, out int totalCount, out int successCount, out int failCount, out string successProcIDList)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Cal_ReCalAPGL");
            cmd.CommandTimeout = 0;
            db.AddInParameter(cmd, "ProcIDList", System.Data.DbType.String, procIDList);
            db.AddOutParameter(cmd, "TotalCount", DbType.Int32, 0);
            db.AddOutParameter(cmd, "SuccessCount", DbType.Int32, 0);
            db.AddOutParameter(cmd, "FailCount", DbType.Int32, 0);
            db.AddOutParameter(cmd, "SuccessProcIDList", DbType.String, 4000);
            db.ExecuteNonQuery(cmd);
            //
            totalCount = cmd.Parameters["@TotalCount"].Value == DBNull.Value ? 0 : (int)cmd.Parameters["@TotalCount"].Value;
            successCount = cmd.Parameters["@SuccessCount"].Value == DBNull.Value ? 0 : (int)cmd.Parameters["@SuccessCount"].Value;
            failCount = cmd.Parameters["@FailCount"].Value == DBNull.Value ? 0 : (int)cmd.Parameters["@FailCount"].Value;
            successProcIDList = cmd.Parameters["@SuccessProcIDList"].Value == DBNull.Value ? string.Empty : cmd.Parameters["@SuccessProcIDList"].Value.ToString();
        }

        /// <summary>
        /// 获取指定的一个AP、GL流程生成凭时证时所用到的数据（表0：凭证表；表1：计算结果；表2：计算过程公式及结果；表3：审核人、审批人、条码；表4：待办任务列表）
        /// </summary>
        /// <param name="procIDList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetCertificateAllData(string procIDList, string type)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_APGL_SelectCertificateAllData");
            cmd.CommandTimeout = 0;
            db.AddInParameter(cmd, "ProcIDList", DbType.String, procIDList);
            db.AddInParameter(cmd, "Type", DbType.String, type);
            return db.ExecuteDataSet(cmd);
        }

        public bool RecaculateAPGLAndRunworkflow(string procID, string userChoice, string partComment, string rejectTypeName, string currentUserID, string nextUserIDList)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_APGL_SumitOneRecordID");
            db.AddInParameter(cmd, "ProcID", System.Data.DbType.String, procID);
            db.AddInParameter(cmd, "UserChoice", System.Data.DbType.String, userChoice);
            db.AddInParameter(cmd, "PartComment", System.Data.DbType.String, partComment);
            db.AddInParameter(cmd, "RejectTypeName", System.Data.DbType.String, rejectTypeName);
            db.AddInParameter(cmd, "CurrentUserID", System.Data.DbType.String, currentUserID);
            db.AddInParameter(cmd, "NextUserIDList", System.Data.DbType.String, nextUserIDList);
            db.AddOutParameter(cmd, "IsSuccess", DbType.Boolean, 0);
            db.ExecuteNonQuery(cmd);
            object result = cmd.Parameters["@IsSuccess"].Value;
            if (result != DBNull.Value)
            {
                return Convert.ToBoolean(result);
            }
            return false;
        }
        #endregion

        #region IBaseService

        public DateTime TestMethod()
        {
            return DateTime.Now;
        }
        #endregion
    }
}