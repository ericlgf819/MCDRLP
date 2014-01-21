using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.RLPlanning.IServices;
using MCD.RLPlanning.Entity;
using MCD.Framework.SqlDAL;

namespace MCD.RLPlanning.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LogService : BaseDAL<LogEntity>, ILogService
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertLog(LogEntity entity)
        {
            return base.InsertSingleEntity(entity);
        }

        /// <summary>
        /// 查询备注信息
        /// </summary>
        /// <param name="sourceID"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public byte[] SelectRemarks(Guid sourceID, int sourceType)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_System_SelectRemark";
                cmd.Parameters.Add(new SqlParameter("@SourceID", SqlDbType.UniqueIdentifier, 36) { Value = sourceID });
                cmd.Parameters.Add(new SqlParameter("@SourceType", SqlDbType.Int, 4) { Value = sourceType });
            });
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <returns></returns>
        public byte[] SelectLogs(DateTime? startTime, DateTime? endTime, string logType, string logTitle)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_System_SelectLog";
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = startTime.Value });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = endTime.Value });
                cmd.Parameters.Add(new SqlParameter("@LogType", SqlDbType.NVarChar, 32) { Value = logType });
                cmd.Parameters.Add(new SqlParameter("@LogTitle", SqlDbType.NVarChar, 32) { Value = logTitle });
            });
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public byte[] SelectDictionary(string keyValue, int type)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_System_SelectDictionary";
                cmd.Parameters.Add(new SqlParameter("@KeyValue", SqlDbType.NVarChar, 32) { Value = keyValue });
                cmd.Parameters.Add(new SqlParameter("@LanguageType", SqlDbType.Int, 4) { Value = type });
            });
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询意见
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="type">0:审核意见;1:复核意见;2:全部意见</param>
        /// <returns></returns>
        public byte[] SelectOpinion(Guid remindID, int type)
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "[SRLS_USP_System_SelectOpinion]";
                cmd.Parameters.Add(new SqlParameter("@RemindID", SqlDbType.UniqueIdentifier, 36) { Value = remindID });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type });
            });
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取当前服务器的时间
        /// </summary>
        /// <returns></returns>
        public DateTime CurrentServerTime()
        {
            return DateTime.Now;
        }
    }
}