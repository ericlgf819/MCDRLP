using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.UUP.Entity;
using MCD.UUP.IServices;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "LogService"，也必须更新 Web.config 中对 "LogService" 的引用。 
    /// </summary>
    public class LogService : BaseService, ILogService
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertLog(LogEntity entity)
        {
            return base.SetEntityInsert<LogEntity>(entity);
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <returns></returns>
        public DataTable SelectLogs(DateTime? startTime, DateTime? endTime, string logType, string logTitle)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectLog";
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = startTime.Value });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = endTime.Value });
                cmd.Parameters.Add(new SqlParameter("@LogType", SqlDbType.NVarChar, 32) { Value = logType });
                cmd.Parameters.Add(new SqlParameter("@LogTitle", SqlDbType.NVarChar, 32) { Value = logTitle });
            });
        }
    }
}