using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MCD.RLPlanning.Entity.ContractMg;

namespace MCD.RLPlanning.IServices.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IRatioRuleService : IBaseService
    {
        #region 百分比规则

        /// 根据实体信息ID查找百分比规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectRatioRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID);

        /// <summary>
        /// 获取所有百分比规则
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllRatioRuleSetting(); 

        /// <summary>
        /// 获取单个百分比规则
        /// </summary>
		/// <param name="ratioID"></param>
        /// <returns></returns>
        [OperationContract]
        RatioRuleSettingEntity SelectSingleRatioRuleSetting(System.String ratioID);

        ///// <summary>
        ///// 新增单个百分比规则
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleRatioRuleSetting(RatioRuleSettingEntity entity);

        ///// <summary>
        ///// 更新单个百分比规则
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleRatioRuleSetting(RatioRuleSettingEntity entity);

        /// <summary>
        /// 新增或更新单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateSingleRatioRuleSetting(RatioRuleSettingEntity entity);

        /// <summary>
        /// 删除单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleRatioRuleSetting(RatioRuleSettingEntity entity);
        #endregion

        #region 百分比时间段设置

        /// 根据规则快照ID查找百分比时间段
        /// </summary>
        /// <param name="ruleSnapshotID">规则快照ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectleRatioTimeIntervalSettingByRuleSnapshotID(System.String ruleSnapshotID);

        /// <summary>
        /// 获取所有百分比时间段
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllRatioTimeIntervalSetting();

        /// <summary>
        /// 获取单个百分比时间段
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        [OperationContract]
        RatioTimeIntervalSettingEntity SelectSingleRatioTimeIntervalSetting(System.String timeIntervalID);

        ///// <summary>
        ///// 新增单个百分比时间段
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity);

        ///// <summary>
        ///// 更新单个百分比时间段
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity);

        /// <summary>
        /// 新增或更新单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity);

        /// <summary>
        /// 删除单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity);
     
        #endregion

        #region 时间条件金额
        
        /// <summary>
        /// 时间段ID查找时间条件金额
        /// </summary>
        /// <param name="timeIntervalID">时间段ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectConditionAmountsByTimeIntervalID(System.String timeIntervalID); 

        /// <summary>
        /// 获取所有时间段条件金额
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllConditionAmount();

        /// <summary>
        /// 获取单个时间段条件金额
        /// </summary>
        /// <param name="conditionID"></param>
        /// <returns></returns>
        [OperationContract]
        ConditionAmountEntity SelectSingleConditionAmount(System.String conditionID);

        ///// <summary>
        ///// 新增单个时间段条件金额
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleConditionAmount(ConditionAmountEntity entity);

        ///// <summary>
        ///// 更新单个时间段条件金额
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleConditionAmount(ConditionAmountEntity entity);

        /// <summary>
        /// 新增或更新单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateSingleConditionAmount(ConditionAmountEntity entity);

        /// <summary>
        /// 删除单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleConditionAmount(ConditionAmountEntity entity);
        #endregion

        #region 百分比周期
        /// <summary>
        /// 根据百分比ID查找百分比周期
        /// </summary>
        /// <param name="ratioID">百分比ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectRatioCycleSettingsByRatioID(System.String ratioID); 

        /// <summary>
        /// 获取所有百分比周期
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllRatioCycleSetting();

        /// <summary>
        /// 获取单个百分比周期
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        [OperationContract]
        RatioCycleSettingEntity SelectSingleRatioCycleSetting(System.String ruleSnapshotID);

        ///// <summary>
        ///// 新增单个百分比周期
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleRatioCycleSetting(RatioCycleSettingEntity entity);

        ///// <summary>
        ///// 更新单个百分比周期
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleRatioCycleSetting(RatioCycleSettingEntity entity);

        /// <summary>
        /// 新增或更新单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateSingleRatioCycleSetting(RatioCycleSettingEntity entity);

        /// <summary>
        /// 删除单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleRatioCycleSetting(RatioCycleSettingEntity entity);

        #endregion
    }
}