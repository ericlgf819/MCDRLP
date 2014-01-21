using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.Framework.DALCommon;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.Services.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class FixedRuleService : BaseDAL<FixedRuleSettingEntity>,IFixedRuleService
    {
        #region 固定规则设置

        /// <summary>
        /// 根据实体信息ID查找固定规则
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息ID</param>
        /// <returns></returns>
        public byte[] SelectFixedRuleSettingsByEntityInfoSettingID(System.String entityInfoSettingID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectFixedRuleSettingsByEntityInfoSettingID");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entityInfoSettingID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询所有固定规则
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllFixedRuleSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllFixedRuleSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 根据规则ID查询固定规则
        /// </summary>
        /// <param name="ruleSnapshotID"></param>
        /// <returns></returns>
        public FixedRuleSettingEntity SelectSingleFixedRuleSetting(System.String ruleSnapshotID)
        {
            FixedRuleSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleFixedRuleSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, ruleSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new FixedRuleSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<FixedRuleSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增或更新单个固定规则
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateFixedRuleSetting(FixedRuleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateFixedRuleSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.AddInParameter(cmd, "@RuleID", DbType.String, entity.RuleID);
            db.AddInParameter(cmd, "@RentType", DbType.String, entity.RentType);
            db.AddInParameter(cmd, "@FirstDueDate", DbType.DateTime, entity.FirstDueDate);
            db.AddInParameter(cmd, "@NextDueDate", DbType.DateTime, entity.NextDueDate);
            db.AddInParameter(cmd, "@NextAPStartDate", DbType.DateTime, entity.NextAPStartDate);
            db.AddInParameter(cmd, "@NextAPEndDate", DbType.DateTime, entity.NextAPEndDate);
            db.AddInParameter(cmd, "@NextGLStartDate", DbType.DateTime, entity.NextGLStartDate);
            db.AddInParameter(cmd, "@NextGLEndDate", DbType.DateTime, entity.NextGLEndDate);
            db.AddInParameter(cmd, "@PayType", DbType.String, entity.PayType);
            db.AddInParameter(cmd, "@ZXStartDate", DbType.DateTime, entity.ZXStartDate);
            db.AddInParameter(cmd, "@ZXConstant", DbType.Decimal, entity.ZXConstant);
            db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
            db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
            db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);
            db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除固定规则
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleFixedRuleSetting(FixedRuleSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteFixedRuleSetting]");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region 固定租金时间段设置

        /// <summary>
        /// 根据固定规则ID查找固定租金时间段
        /// </summary>
        /// <param name="ruleSnapshotID">固定规则ID</param>
        /// <returns></returns>
        public byte[] SelectFixedTimeIntervalSettingsByRuleSnapshotID(System.String ruleSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Contract_SelectFixedTimeIntervalSettingsByRuleSnapshotID");
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, ruleSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取所有固定租金时间段设置
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllFixedTimeIntervalSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllFixedTimeIntervalSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个固定租金时间段设置
        /// </summary>
        /// <param name="timeIntervalID"></param>
        /// <returns></returns>
        public FixedTimeIntervalSettingEntity SelectSingleFixedTimeIntervalSetting(System.String timeIntervalID)
        {
            FixedTimeIntervalSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleFixedTimeIntervalSetting]");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, timeIntervalID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new FixedTimeIntervalSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<FixedTimeIntervalSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        ///// <summary>
        ///// 新增单个固定租金时间段设置
        ///// </summary>
        ///// <param name="entity"></param>
        //public void InsertSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)                                                   
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertFixedTimeIntervalSetting]");

        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
        //    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);
        //    db.AddInParameter(cmd, "@Amount", DbType.Decimal, entity.Amount);
        //    db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
        //    db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
        //    db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);

        //    db.ExecuteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 更新单个固定租金时间段设置
        ///// </summary>
        ///// <param name="entity"></param>
        //public void UpdateSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DBConnection");
        //    DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateFixedTimeIntervalSetting]");

        //    db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
        //    db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
        //    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
        //    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);
        //    db.AddInParameter(cmd, "@Amount", DbType.Decimal, entity.Amount);
        //    db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
        //    db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
        //    db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);

        //    db.ExecuteNonQuery(cmd);
        //}

        /// <summary>
        /// 新增或更新单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateFixedTimeIntervalSetting]");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
            db.AddInParameter(cmd, "@RuleSnapshotID", DbType.String, entity.RuleSnapshotID);
            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);
            db.AddInParameter(cmd, "@Amount", DbType.Decimal, entity.Amount);
            db.AddInParameter(cmd, "@Cycle", DbType.String, entity.Cycle);
            db.AddInParameter(cmd, "@CycleMonthCount", DbType.Int32, entity.CycleMonthCount);
            db.AddInParameter(cmd, "@Calendar", DbType.String, entity.Calendar);
            db.ExecuteNonQuery(cmd); 
        }

        /// <summary>
        /// 删除单个固定租金时间段设置
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleFixedTimeIntervalSetting(FixedTimeIntervalSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteFixedTimeIntervalSetting]");
            db.AddInParameter(cmd, "@TimeIntervalID", DbType.String, entity.TimeIntervalID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion
    }
}