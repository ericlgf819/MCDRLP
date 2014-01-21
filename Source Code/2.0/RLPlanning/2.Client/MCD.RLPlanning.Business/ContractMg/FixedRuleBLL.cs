using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.BLL.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class FixedRuleBLL : BaseBLL<IFixedRuleService>
    {
        #region 固定规则设置

         /// <summary>
        /// 根据实体信息ID查找固定规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        public DataSet SelectFixedRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID)
        {
            return base.DeSerilize(base.WCFService.SelectFixedRuleSettingsByEntityInfoSettingID(entityInfoSettingID));
        }

        /// <summary>
        /// 查询所有固定规则
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllFixedRuleSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllFixedRuleSetting());
        }

        /// <summary>
        ///根据规则ID查询固定规则
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        public FixedRuleSettingEntity SelectSingleFixedRuleSetting(System.String ruleSnapshotID)
        {
            return base.WCFService.SelectSingleFixedRuleSetting(ruleSnapshotID);
        }

        /// <summary>
        /// 新增或更新单个固定规则
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateFixedRuleSetting(FixedRuleSettingEntity entity)
        {
            base.WCFService.InsertOrUpdateFixedRuleSetting(entity);
        }

        /// <summary>
        ///  删除固定规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleFixedRuleSetting(FixedRuleSettingEntity entity)
        {
            base.WCFService.DeleteSingleFixedRuleSetting(entity);
        }
        #endregion

        #region 固定租金时间段设置

         /// <summary>
        /// 根据固定规则ID查找固定租金时间段
        /// </summary>
        /// <param name="ruleSnapshotID">固定规则ID</param>
        /// <returns></returns>
        public DataSet SelectFixedTimeIntervalSettingsByRuleSnapshotID(System.String ruleSnapshotID)
        {
            return base.DeSerilize(base.WCFService.SelectFixedTimeIntervalSettingsByRuleSnapshotID(ruleSnapshotID));
        }

        /// <summary>
        /// 获取所有固定租金时间段设置
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllFixedTimeIntervalSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllFixedTimeIntervalSetting());
        }

        /// <summary>
        /// 获取单个固定租金时间段设置
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        public FixedTimeIntervalSettingEntity SelectSingleFixedTimeIntervalSetting(System.String timeIntervalID)
        {
            return base.WCFService.SelectSingleFixedTimeIntervalSetting(timeIntervalID);
        }

        /// <summary>
        /// 新增或更新单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)
        { 
             base.WCFService.InsertOrUpdateSingleFixedTimeIntervalSetting(entity);
        }

        /// <summary>
        /// 删除单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)
        {
             base.WCFService.DeleteSingleFixedTimeIntervalSetting(entity);
        }
        #endregion
    }
}