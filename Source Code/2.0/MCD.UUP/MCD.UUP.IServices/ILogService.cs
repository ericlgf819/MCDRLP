using System;
using System.Data;
using System.ServiceModel;

using MCD.UUP.Entity;

namespace MCD.UUP.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ILogService : IBaseService
    {   
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertLog(LogEntity entity);

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectLogs(DateTime? startTime, DateTime? endTime, string logType, string logTitle);
    }
}