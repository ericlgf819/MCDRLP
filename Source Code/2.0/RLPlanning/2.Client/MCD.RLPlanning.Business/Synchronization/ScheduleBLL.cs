using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Synchronization;
using MCD.RLPlanning.IServices.Synchronization;

namespace MCD.RLPlanning.BLL.Synchronization
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleBLL : BaseBLL<IScheduleService>
    {
        /// <summary>
        /// 获取所有同步计划
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllSchedule()
        {
            return base.DeSerilize(base.WCFService.SelectAllSchedule());
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ScheduleEntity SelectSingleSchedule(ScheduleEntity entity)
        {
            return base.WCFService.SelectSingleStore(entity);
        }
        /// <summary>
        /// 新增同步计划
        /// </summary>
        /// <returns></returns>
        public int InsertSchedule(DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId)
        {
            return base.WCFService.InsertSchedule(SycDate, CalculateEndDate, Remark, UserId);
        }
        /// <summary>
        /// 更新同步计划
        /// </summary>
        /// <returns></returns>
        public int UpdateSchedule(int ID, DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId)
        {
            return base.WCFService.UpdateSchedule(ID, SycDate, CalculateEndDate, Remark, UserId);
        }
        /// <summary>
        /// 同步时调用的  更新方法
        /// </summary>
        /// <returns></returns>
        public void SycSchedule(int ID, string Status, string SynDetail)
        {
            base.WCFService.SycSchedule(ID, Status, SynDetail);
        }
        /// <summary>
        /// 删除同步计划
        /// </summary>
        /// <returns></returns>
        public void DeleteSchedule(int ID)
        {
            base.WCFService.DeleteSchedule(ID);
        }
    }
}