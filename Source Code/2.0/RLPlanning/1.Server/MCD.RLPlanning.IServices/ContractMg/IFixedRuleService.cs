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
    public interface IFixedRuleService : IBaseService
    {
        #region 固定规则设置

         /// <summary>
        /// 根据实体信息ID查找固定规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectFixedRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID);

        /// <summary>
        /// 查询所有固定规则
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllFixedRuleSetting();

        /// <summary>
        /// 根据规则ID查询固定规则
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        [OperationContract]
        FixedRuleSettingEntity SelectSingleFixedRuleSetting(System.String ruleSnapshotID);

        ///// <summary>
        ///// 新增固定规则
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleFixedRuleSetting(FixedRuleSettingEntity entity);

        ///// <summary>
        ///// 修改固定规则
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleFixedRuleSetting(FixedRuleSettingEntity entity);

        /// <summary>
        /// 新增或更新单个固定规则
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateFixedRuleSetting(FixedRuleSettingEntity entity);

        /// <summary>
        /// 删除固定规则
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleFixedRuleSetting(FixedRuleSettingEntity entity);

        #endregion

        #region 固定租金时间段设置

        /// <summary>
        /// 根据固定规则ID查找固定租金时间段
        /// </summary>
        /// <param name="ruleSnapshotID">固定规则ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectFixedTimeIntervalSettingsByRuleSnapshotID(System.String ruleSnapshotID);

        /// <summary>
        /// 获取所有固定租金时间段设置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllFixedTimeIntervalSetting();

        /// <summary>
        /// 获取单个固定租金时间段设置
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        [OperationContract]
        FixedTimeIntervalSettingEntity SelectSingleFixedTimeIntervalSetting(System.String timeIntervalID);

        ///// <summary>
        ///// 新增单个固定租金时间段设置
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void InsertSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity);

        ///// <summary>
        ///// 更新单个固定租金时间段设置
        ///// </summary>
        ///// <param name="entity"></param>
        //[OperationContract]
        //void UpdateSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity);

        /// <summary>
        /// 新增或更新单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity);

        /// <summary>
        /// 删除单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity);
        #endregion
    }
}