using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using MCD.RLPlanning.Entity;

namespace MCD.RLPlanning.IServices
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
        byte[] SelectLogs(DateTime? startTime, DateTime? endTime, string logType, string logTitle);

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectDictionary(string keyValue, int type);

        /// <summary>
        /// 当前服务器的时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime CurrentServerTime();

        /// <summary>
        /// 查询备注信息
        /// </summary>
        /// <param name="sourceID"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectRemarks(Guid sourceID, int sourceType);

        /// <summary>
        /// 查询意见
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="type">0:审核意见;1:复核意见;2:全部意见</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectOpinion(Guid remindID, int type);
    }
}