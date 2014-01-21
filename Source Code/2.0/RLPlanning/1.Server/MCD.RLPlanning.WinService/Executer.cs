using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Mail;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common;

namespace MCD.RLPlanning.WinService
{
    /// <summary>
    /// 
    /// </summary>
    public class Executer
    {
        //Methods
        public static void doSynchonization()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                DbCommand cmd = db.GetStoredProcCommand("[dbo].[RLP_Synchronization_Service_Job]");
                cmd.CommandTimeout = int.MaxValue;
                //
                db.ExecuteNonQuery(cmd);
            }
            catch { }
        }

        #region Log

        public static void AddLog(string modules, string operate, string operateDesc)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                db.ExecuteNonQuery("dbo.[usp_Common_AddOperateLog]", modules, operate, operateDesc, null, null);
            }
            catch { }
        }
        public static void AddErrorLog(string logType, string logSource, string logTitle, string logMessage)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                db.ExecuteNonQuery("dbo.[usp_Common_AddErrorLog]", logType, logSource, logTitle, logMessage, null, null);
            }
            catch { }
        }
        #endregion
    }
}