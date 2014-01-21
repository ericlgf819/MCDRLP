using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common;
using MCD.Framework.SqlDAL;
using MCD.Framework.DALCommon;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.Services.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class RatioRuleService : BaseDAL<RatioRuleSettingEntity>, IRatioRuleService
    {
        #region 百分比规则

        /// 根据实体信息ID查找百分比规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        public byte[] SelectRatioRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectRatioRuleSettingsByEntityInfoSettingID");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entityInfoSettingID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取所有百分比规则
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllRatioRuleSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllRatioRuleSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个百分比规则
        /// </summary>
        /// <param name="ratioID"></param>
        /// <returns></returns>
        public RatioRuleSettingEntity SelectSingleRatioRuleSetting(System.String ratioID)
        {
            RatioRuleSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleRatioRuleSetting]");
            db.AddInParameter(cmd, "@RatioID", DbType.String, ratioID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new RatioRuleSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<RatioRuleSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        ///// <summary>
        ///// 新增单个百分比规则
        ///// </summary>
        ///// <param name="entity"></param>
        //public void InsertSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertRatioRuleSetting]");

        //    db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
        //    db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
        //    db.AddInParameter(cmd, "@RentType", DbType.String, entity.RentType);
        //    db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
        //    db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);

        //    db.ExecuteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 更新单个百分比规则
        ///// </summary>
        ///// <param name="entity"></param>
        //public void UpdateSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateRatioRuleSetting]");

        //    db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
        //    db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
        //    db.AddInParameter(cmd, "@RentType", DbType.String, entity.RentType);
        //    db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
        //    db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);

        //    db.ExecuteNonQuery(cmd);
        //}

        /// <summary>
        /// 新增或更新单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateRatioRuleSetting]");
            db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.AddInParameter(cmd, "@RentType", DbType.String, entity.RentType);
            db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个百分比规则
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleRatioRuleSetting(RatioRuleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteRatioRuleSetting]");
            db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
            db.ExecuteNonQuery(cmd);
        }

        #endregion

        #region 百分比周期

        /// <summary>
        /// 根据百分比ID查找百分比周期
        /// </summary>
        /// <param name="ratioID">百分比ID</param>
        /// <returns></returns>
        public byte[] SelectRatioCycleSettingsByRatioID(System.String ratioID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectRatioCycleSettingsByRatioID");
            db.AddInParameter(cmd, "@RatioID", DbType.String, ratioID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取所有百分比周期
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllRatioCycleSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllRatioCycleSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个百分比周期
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        public RatioCycleSettingEntity SelectSingleRatioCycleSetting(System.String ruleSnapshotID)
        {
            RatioCycleSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleRatioCycleSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, ruleSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new RatioCycleSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<RatioCycleSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        ///// <summary>
        ///// 新增单个百分比周期
        ///// </summary>
        ///// <param name="entity"></param>
        //public void InsertSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertRatioCycleSetting]");

        //    db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@RuleID", DbType.String, entity.RuleID);
        //    db.AddInParameter(cmd, "@IsPure", DbType.Boolean, entity.IsPure);
        //    db.AddInParameter(cmd, "@FirstDueDate", DbType.DateTime, entity.FirstDueDate);
        //    db.AddInParameter(cmd, "@NextDueDate", DbType.DateTime, entity.NextDueDate);
        //    db.AddInParameter(cmd, "@NextAPStartDate", DbType.DateTime, entity.NextAPStartDate);
        //    db.AddInParameter(cmd, "@NextAPEndDate", DbType.DateTime, entity.NextAPEndDate);
        //    db.AddInParameter(cmd, "@NextGLStartDate", DbType.DateTime, entity.NextGLStartDate);
        //    db.AddInParameter(cmd, "@NextGLEndDate", DbType.DateTime, entity.NextGLEndDate);
        //    db.AddInParameter(cmd, "@PayType", DbType.String, entity.PayType);
        //    db.AddInParameter(cmd, "@ZXStartDate", DbType.DateTime, entity.ZXStartDate);
        //    db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
        //    db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
        //    db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);
        //    db.AddInParameter(cmd, "@CycleType", DbType.String, entity.CycleType);
        //    db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);

        //    db.ExecuteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 更新单个百分比周期
        ///// </summary>
        ///// <param name="entity"></param>
        //public void UpdateSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateRatioCycleSetting]");

        //    db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@RuleID", DbType.String, entity.RuleID);
        //    db.AddInParameter(cmd, "@IsPure", DbType.Boolean, entity.IsPure);
        //    db.AddInParameter(cmd, "@FirstDueDate", DbType.DateTime, entity.FirstDueDate);
        //    db.AddInParameter(cmd, "@NextDueDate", DbType.DateTime, entity.NextDueDate);
        //    db.AddInParameter(cmd, "@NextAPStartDate", DbType.DateTime, entity.NextAPStartDate);
        //    db.AddInParameter(cmd, "@NextAPEndDate", DbType.DateTime, entity.NextAPEndDate);
        //    db.AddInParameter(cmd, "@NextGLStartDate", DbType.DateTime, entity.NextGLStartDate);
        //    db.AddInParameter(cmd, "@NextGLEndDate", DbType.DateTime, entity.NextGLEndDate);
        //    db.AddInParameter(cmd, "@PayType", DbType.String, entity.PayType);
        //    db.AddInParameter(cmd, "@ZXStartDate", DbType.DateTime, entity.ZXStartDate);
        //    db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
        //    db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
        //    db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);
        //    db.AddInParameter(cmd, "@CycleType", DbType.String, entity.CycleType);
        //    db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);

        //    db.ExecuteNonQuery(cmd);
        //}

        /// <summary>
        /// 新增或修改单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateRatioCycleSetting]");
            db.AddInParameter(cmd, "@RatioID", DbType.String, entity.RatioID);
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.AddInParameter(cmd, "@RuleID", DbType.String, entity.RuleID);
            db.AddInParameter(cmd, "@IsPure", DbType.Boolean, entity.IsPure);
            db.AddInParameter(cmd, "@FirstDueDate", DbType.DateTime, entity.FirstDueDate);
            db.AddInParameter(cmd, "@NextDueDate", DbType.DateTime, entity.NextDueDate);
            db.AddInParameter(cmd, "@NextAPStartDate", DbType.DateTime, entity.NextAPStartDate);
            db.AddInParameter(cmd, "@NextAPEndDate", DbType.DateTime, entity.NextAPEndDate);
            db.AddInParameter(cmd, "@NextGLStartDate", DbType.DateTime, entity.NextGLStartDate);
            db.AddInParameter(cmd, "@NextGLEndDate", DbType.DateTime, entity.NextGLEndDate);
            db.AddInParameter(cmd, "@PayType", DbType.String, entity.PayType);
            db.AddInParameter(cmd, "@ZXStartDate", DbType.DateTime, entity.ZXStartDate);
            db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
            db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
            db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);
            db.AddInParameter(cmd, "@CycleType", DbType.String, entity.CycleType);
            db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个百分比周期
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleRatioCycleSetting(RatioCycleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteRatioCycleSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region 百分比时间段设置

        /// <summary>
        /// 根据规则快照ID查找百分比时间段
        /// </summary>
        /// <param name="ruleSnapshotID">规则快照ID</param>
        /// <returns></returns>
        public byte[] SelectleRatioTimeIntervalSettingByRuleSnapshotID(System.String ruleSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectleRatioTimeIntervalSettingByRuleSnapshotID");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, ruleSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取所有百分比时间段
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllRatioTimeIntervalSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllRatioTimeIntervalSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个百分比时间段
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        public RatioTimeIntervalSettingEntity SelectSingleRatioTimeIntervalSetting(System.String timeIntervalID)
        {
            RatioTimeIntervalSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleRatioTimeIntervalSetting]");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, timeIntervalID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new RatioTimeIntervalSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<RatioTimeIntervalSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        ///// <summary>
        ///// 新增单个百分比时间段
        ///// </summary>
        ///// <param name="entity"></param>
        //public void InsertSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertRatioTimeIntervalSetting]");
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
        //    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);

        //    db.ExecuteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 更新单个百分比时间段
        ///// </summary>
        ///// <param name="entity"></param>
        //public void UpdateSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateRatioTimeIntervalSetting]");
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
        //    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);

        //    db.ExecuteNonQuery(cmd);
        //}

        /// <summary>
        /// 新增或更新单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateRatioTimeIntervalSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个百分比时间段
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleRatioTimeIntervalSetting(RatioTimeIntervalSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteRatioTimeIntervalSetting]");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region 时间条件金额

        /// <summary>
        /// 时间段ID查找时间条件金额
        /// </summary>
        /// <param name="timeIntervalID">时间段ID</param>
        /// <returns></returns>
        public byte[] SelectConditionAmountsByTimeIntervalID(System.String timeIntervalID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectConditionAmountsByAndTimeIntervalID");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, timeIntervalID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取所有时间段条件金额
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllConditionAmount()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllConditionAmounts]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个时间段条件金额
        /// </summary>
        /// <param name="conditionID"></param>
        /// <returns></returns>
        public ConditionAmountEntity SelectSingleConditionAmount(System.String conditionID)
        {
            ConditionAmountEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleConditionAmount]");
            db.AddInParameter(cmd, "@ConditionID", DbType.String, conditionID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new ConditionAmountEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<ConditionAmountEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
                //
                if (entity.ConditionDesc != string.Empty && entity.ConditionDesc != null)
                {
                    entity.ConditionDesc = entity.ConditionDesc.ToNumberString();
                }
                if (entity.AmountFormulaDesc != string.Empty && entity.AmountFormulaDesc != null)
                {
                    entity.AmountFormulaDesc = entity.AmountFormulaDesc.ToNumberString();
                }
            }
            return entity;
        }

        ///// <summary>
        ///// 新增单个时间段条件金额
        ///// </summary>
        ///// <param name="entity"></param>
        //public void InsertSingleConditionAmount(ConditionAmountEntity entity)
        //{
        //    this.ConditionToSQL(entity);
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertConditionAmount]");

        //    db.AddInParameter(cmd, "@ConditionID", DbType.String, entity.ConditionID);
        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@ConditionDesc", DbType.String, entity.ConditionDesc);
        //    db.AddInParameter(cmd, "@AmountFormulaDesc", DbType.String, entity.AmountFormulaDesc);
        //    db.AddInParameter(cmd, "@SQLCondition", DbType.String, entity.SQLCondition);
        //    db.AddInParameter(cmd, "@SQLAmountFormula", DbType.String, entity.SQLAmountFormula);

        //    db.ExecuteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 更新单个时间段条件金额
        ///// </summary>
        ///// <param name="entity"></param>
        //public void UpdateSingleConditionAmount(ConditionAmountEntity entity)
        //{
        //    this.ConditionToSQL(entity);

        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateConditionAmount]");

        //    db.AddInParameter(cmd, "@ConditionID", DbType.String, entity.ConditionID);
        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@ConditionDesc", DbType.String, entity.ConditionDesc);
        //    db.AddInParameter(cmd, "@AmountFormulaDesc", DbType.String, entity.AmountFormulaDesc);
        //    db.AddInParameter(cmd, "@SQLCondition", DbType.String, entity.SQLCondition);
        //    db.AddInParameter(cmd, "@SQLAmountFormula", DbType.String, entity.SQLAmountFormula);

        //    db.ExecuteNonQuery(cmd);
        //}

        private void ConditionToSQL(ConditionAmountEntity entity)
        {
            string  cycleSales= "CycleSales";
            if (!string.IsNullOrEmpty(entity.ConditionDesc) && entity.ConditionDesc.Trim().Length > 0)
            {
                entity.SQLCondition = entity.ConditionDesc.ToNormalString().ToFloatString(2);
                if (!(entity.SQLCondition.StartsWith(cycleSales) || entity.SQLCondition.EndsWith(cycleSales)))
                {
                    entity.SQLCondition = entity.ConditionDesc.Replace(cycleSales, cycleSales + " AND " + cycleSales).ToFloatString(2);
                }
            }
            else
            {
                entity.SQLCondition = string.Empty;
            }
            if (!string.IsNullOrEmpty(entity.AmountFormulaDesc) && entity.AmountFormulaDesc.Trim().Length > 0)
            {
                entity.SQLAmountFormula = entity.AmountFormulaDesc.ToNormalString().ToFloatString(2);
            }
        }

        /// <summary>
        /// 新增或修改单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleConditionAmount(ConditionAmountEntity entity)
        {
            this.ConditionToSQL(entity);
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateConditionAmount]");
            string conditionDesc = null;
            if (entity.ConditionDesc != string.Empty && entity.ConditionDesc != null)
            {
                conditionDesc = entity.ConditionDesc.ToNormalString();
            }
            string formulaDesc = null;
            if (entity.AmountFormulaDesc != string.Empty && entity.AmountFormulaDesc != null)
            {
                formulaDesc = entity.AmountFormulaDesc.ToNormalString();
            }
            //
            db.AddInParameter(cmd, "@ConditionID", DbType.String, entity.ConditionID);
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
            db.AddInParameter(cmd, "@ConditionDesc", DbType.String, conditionDesc);
            db.AddInParameter(cmd, "@AmountFormulaDesc", DbType.String, formulaDesc);
            db.AddInParameter(cmd, "@SQLCondition", DbType.String, entity.SQLCondition);
            db.AddInParameter(cmd, "@SQLAmountFormula", DbType.String, entity.SQLAmountFormula);
            db.AddInParameter(cmd, "@ConditionNumberValue", DbType.String, entity.ConditionNumberValue);
            db.AddInParameter(cmd, "@FormulaNumberValue", DbType.String, entity.FormulaNumberValue);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个时间段条件金额
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleConditionAmount(ConditionAmountEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteConditionAmount]");
            db.AddInParameter(cmd, "@ConditionID", DbType.String, entity.ConditionID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion
    }
}