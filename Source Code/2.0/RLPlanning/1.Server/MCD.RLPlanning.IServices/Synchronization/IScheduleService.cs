using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Synchronization;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.IServices.Synchronization
{
    /// <summary>
    /// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IScheduleService" in both code and config file together. 
    /// </summary>
    [ServiceContract]
    public interface IScheduleService : IBaseService
    {
        /// <summary>
        /// 获取所有同步计划
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllSchedule();
        /// <summary>
        /// 获取同步计划
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ScheduleEntity SelectSingleStore(ScheduleEntity entity);
        /// <summary>
        /// 新增同步计划
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int InsertSchedule(DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId);
        /// <summary>
        /// 更新同步计划
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int UpdateSchedule(int ID, DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId);
        /// <summary>
        /// 同步时调用的  更新方法
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SycSchedule(int ID, string Status, string SynDetail);
        /// <summary>
        /// 删除同步计划
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void DeleteSchedule(int ID);
    }
}