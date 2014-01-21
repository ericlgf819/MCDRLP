using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class LogBLL : BaseBLL<ILogService>
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertLog(LogEntity entity)
        {
            return base.WCFService.InsertLog(entity);
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <returns></returns>
        public DataSet SelectLogs(DateTime? startTime, DateTime? endTime, string logType, string logTitle)
        {
            return base.DeSerilize(base.WCFService.SelectLogs(startTime, endTime, logType, logTitle));
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="keyValue">字典名称</param>
        /// <param name="type">0:表示简体中文;1:表示繁体中文;2:表示英文</param>
        /// <returns></returns>
        public DataSet SelectDictionary(string keyValue, int type)
        {
            return base.DeSerilize(base.WCFService.SelectDictionary(keyValue, type));
        }

        /// <summary>
        /// 查询备注信息
        /// </summary>
        /// <param name="sourceID"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public DataSet SelectRemarks(Guid sourceID, int sourceType)
        {
            return base.DeSerilize(base.WCFService.SelectRemarks(sourceID, sourceType));
        }

        /// <summary>
        /// 查询意见
        /// </summary>
        /// <param name="remindID"></param>
        /// <returns></returns>
        public DataSet SelectOpinion(Guid remindID)
        {
            return base.DeSerilize(base.WCFService.SelectOpinion(remindID, 2));
        }

        /// <summary>
        /// 查询意见
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="type">0:审核意见;1:复核意见;2:全部意见</param>
        /// <returns></returns>
        public DataSet SelectOpinion(Guid remindID, int type)
        {
            return base.DeSerilize(base.WCFService.SelectOpinion(remindID, type));
        }

        /// <summary>
        /// 获取服务器当前的时间
        /// </summary>
        /// <returns></returns>
        public DateTime CurrentServerTime()
        {
            return base.WCFService.CurrentServerTime();
        }
    }
}