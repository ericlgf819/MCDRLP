using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using MCD.RLPlanning.IServices.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MCD.RLPlanning.Services.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonService : ICommonService
    {
        #region ICommonService

        /// <summary>
        /// 管理员强制结束流程
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="currentUserID"></param>
        public void ForceToEndByAdministrator(string procID, string currentUserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("usp_Common_ForceToEndByAdministrator", procID, currentUserID);
        }
        #endregion

        #region ICommonService

        /// <summary>
        /// 撤消业务数据
        /// </summary>
        /// <param name="bizType"></param>
        /// <param name="keyID"></param>
        public void CancelBizData(MCD.Common.SRLS.BizType bizType, string keyID,string currentUserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("usp_Common_CancelBizData", bizType.ToString(), keyID, currentUserID);
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

public static class ExtInfo
{
    public static DataSet sqlDataSet(this string sqlthis)
    {
        Database db = DatabaseFactory.CreateDatabase("DBConnection");
        DbCommand cmd = db.GetSqlStringCommand(sqlthis);
        DataSet ds = db.ExecuteDataSet(cmd);
        return ds;
    }

    public static object sqlObject(this string sqlthis)
    {
        Object _object;
        Database db = DatabaseFactory.CreateDatabase("DBConnection");

        using (DbConnection conn = db.CreateConnection())
        {
            DbCommand cmd = db.GetSqlStringCommand(sqlthis);
            cmd.Connection = conn;
            conn.Open();
            _object = cmd.ExecuteScalar();

            conn.Close();
        }
        return _object;
    }

    public static int TypeToInt(this object src)
    {
        int nResult = 0;
        int.TryParse(src.ToString(), out nResult);
        return nResult;
    }
}