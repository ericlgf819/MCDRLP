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
    public class RatioRuleBLL : BaseBLL<IRatioRuleService>
    {
        #region 百分比规则

        /// 根据实体信息ID查找百分比规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        public DataSet SelectRatioRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID)
        {
            return base.DeSerilize(base.WCFService.SelectRatioRuleSettingsByEntityInfoSettingID(entityInfoSettingID));
        }

        /// <summary>
        /// 获取所有百分比规则
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllRatioRuleSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllRatioRuleSetting());
        }

        /// <summary>
        /// 获取单个百分比规则
        /// </summary>
        /// <param name="ratioID"></param>
        /// <returns></returns>
        public RatioRuleSettingEntity SelectSingleRatioRuleSetting(System.String ratioID)
        {
            return base.WCFService.SelectSingleRatioRuleSetting(ratioID);
        }

        /// <summary>
        /// 新增或修改单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertOrUpdateSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        {
             base.WCFService.InsertOrUpdateSingleRatioRuleSetting(entity);
        }

        /// <summary>
        /// 删除单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        {
             base.WCFService.DeleteSingleRatioRuleSetting(entity);
        }
        #endregion

        #region 百分比时间段设置

        /// 根据规则快照ID查找百分比时间段
        /// </summary>
        /// <param name="ruleSnapshotID">规则快照ID</param>
        /// <returns></returns>
        public DataSet SelectleRatioTimeIntervalSettingByRuleSnapshotID(System.String ruleSnapshotID)
        {
            return base.DeSerilize(base.WCFService.SelectleRatioTimeIntervalSettingByRuleSnapshotID(ruleSnapshotID));
        }

        /// <summary>
        /// 获取所有百分比时间段
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllRatioTimeIntervalSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllRatioTimeIntervalSetting());
        }

        /// <summary>
        /// 获取单个百分比时间段
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        public RatioTimeIntervalSettingEntity SelectSingleRatioTimeIntervalSetting(System.String timeIntervalID)
        {
            return base.WCFService.SelectSingleRatioTimeIntervalSetting(timeIntervalID);
        }

        /// <summary>
        /// 新增或修改单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertOrUpdateSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        {
             base.WCFService.InsertOrUpdateSingleRatioTimeIntervalSetting(entity);
        }

        /// <summary>
        /// 删除单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        {
             base.WCFService.DeleteSingleRatioTimeIntervalSetting(entity);
        }
        #endregion
       
        #region 时间条件金额
        /// <summary>
        /// 时间段ID查找时间条件金额
        /// </summary>
        /// <param name="timeIntervalID">时间段ID</param>
        /// <returns></returns>
        public DataSet SelectConditionAmountsByTimeIntervalID(System.String timeIntervalID)
        {
            return base.DeSerilize(base.WCFService.SelectConditionAmountsByTimeIntervalID(timeIntervalID));
        }

        /// <summary>
        /// 获取所有时间段条件金额
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllConditionAmount()
        {
            return base.DeSerilize(base.WCFService.SelectAllConditionAmount());
        }

        /// <summary>
        /// 获取单个时间段条件金额
        /// </summary>
        /// <param name="conditionID"></param>
        /// <returns></returns>
        public ConditionAmountEntity SelectSingleConditionAmount(System.String conditionID)
        {
            return base.WCFService.SelectSingleConditionAmount(conditionID);
        }

        /// <summary>
        /// 新增或修改单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertOrUpdateSingleConditionAmount(ConditionAmountEntity entity)
        {
             base.WCFService.InsertOrUpdateSingleConditionAmount(entity);
        }

        /// <summary>
        /// 删除单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleConditionAmount(ConditionAmountEntity entity)
        {
             base.WCFService.DeleteSingleConditionAmount(entity);
        }
        #endregion
 
        #region 百分比周期

        /// <summary>
        /// 根据百分比ID查找百分比周期
        /// </summary>
        /// <param name="ratioID">百分比ID</param>
        /// <returns></returns>
        public DataSet SelectRatioCycleSettingsByRatioID(System.String ratioID)
        {
            return base.DeSerilize(base.WCFService.SelectRatioCycleSettingsByRatioID(ratioID));
        }

        /// <summary>
        /// 获取所有百分比周期
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllRatioCycleSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllRatioCycleSetting());
        }

        /// <summary>
        /// 获取单个百分比周期
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        public RatioCycleSettingEntity SelectSingleRatioCycleSetting(System.String ruleSnapshotID)
        {
            return base.WCFService.SelectSingleRatioCycleSetting(ruleSnapshotID);
        }

        /// <summary>
        /// 新增或修改单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertOrUpdateSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        {
             base.WCFService.InsertOrUpdateSingleRatioCycleSetting(entity);
        }

        /// <summary>
        /// 删除单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        {
             base.WCFService.DeleteSingleRatioCycleSetting(entity);
        }
        #endregion
    }
}