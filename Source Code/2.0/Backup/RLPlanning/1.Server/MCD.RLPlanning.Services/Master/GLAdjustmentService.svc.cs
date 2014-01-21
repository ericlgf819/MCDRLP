using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.Services.Master
{
	/// <summary>
	///表示对表GLAdjustment的所有操作的实现。
	/// </summary>
    public class GLAdjustmentService : BaseDAL<GLAdjustmentEntity>, IGLAdjustmentService
    {
		///<summary>
		///删除表GLAdjustment中的指定记录并返回状态。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		public bool Delete(string adjustmentID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = "usp_Master_DeleteSingleGLAdjustment";
            DbCommand cmd = db.GetStoredProcCommand(sql);
			db.AddInParameter(cmd, "AdjustmentID", DbType.String, adjustmentID);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///向表GLAdjustment中插入一条记录并返回状态。
		///</summary>
		///<param name="gLAdjustment">要插入记录的GLAdjustment实例</param>
		///<returns>返回true或false</returns>
		public bool Insert(GLAdjustmentEntity gLAdjustment)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = "usp_Master_InsertSingleGLAdjustment";
            DbCommand cmd = db.GetStoredProcCommand(sql);
			db.AddInParameter(cmd, "AdjustmentID", DbType.String, gLAdjustment.AdjustmentID);
			db.AddInParameter(cmd, "EntityInfoSettingID", DbType.String, gLAdjustment.EntityInfoSettingID);
			db.AddInParameter(cmd, "RuleSnapshotID", DbType.String, gLAdjustment.RuleSnapshotID);
			db.AddInParameter(cmd, "RuleID", DbType.String, gLAdjustment.RuleID);
			db.AddInParameter(cmd, "RentType", DbType.String, gLAdjustment.RentType);
			db.AddInParameter(cmd, "AccountingCycle", DbType.String, gLAdjustment.AccountingCycle);
            db.AddInParameter(cmd, "AdjustmentDate", DbType.DateTime, gLAdjustment.AdjustmentDate);
			db.AddInParameter(cmd, "Amount", DbType.Decimal, gLAdjustment.Amount);
			db.AddInParameter(cmd, "Remark", DbType.String, gLAdjustment.Remark);
			db.AddInParameter(cmd, "CreateTime", DbType.DateTime, gLAdjustment.CreateTime);
			db.AddInParameter(cmd, "CreatorName", DbType.String, gLAdjustment.CreatorName);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///更新表GLAdjustment中指定主码的某条记录。
		///</summary>
		///<param name="gLAdjustment">要更新记录的GLAdjustment实例</param>
		///<returns>更新成功则返回true，否则返回false。</returns>
		public bool Update(GLAdjustmentEntity gLAdjustment)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = "usp_Master_UpdateSingleGLAdjustment";
            DbCommand cmd = db.GetStoredProcCommand(sql);
			db.AddInParameter(cmd, "AdjustmentID", DbType.String, gLAdjustment.AdjustmentID);
			db.AddInParameter(cmd, "EntityInfoSettingID", DbType.String, gLAdjustment.EntityInfoSettingID);
			db.AddInParameter(cmd, "RuleSnapshotID", DbType.String, gLAdjustment.RuleSnapshotID);
			db.AddInParameter(cmd, "RuleID", DbType.String, gLAdjustment.RuleID);
			db.AddInParameter(cmd, "RentType", DbType.String, gLAdjustment.RentType);
			db.AddInParameter(cmd, "AccountingCycle", DbType.String, gLAdjustment.AccountingCycle);
            db.AddInParameter(cmd, "AdjustmentDate", DbType.DateTime, gLAdjustment.AdjustmentDate);
			db.AddInParameter(cmd, "Amount", DbType.Decimal, gLAdjustment.Amount);
			db.AddInParameter(cmd, "Remark", DbType.String, gLAdjustment.Remark);
			db.AddInParameter(cmd, "CreateTime", DbType.DateTime, gLAdjustment.CreateTime);
			db.AddInParameter(cmd, "CreatorName", DbType.String, gLAdjustment.CreatorName);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///获取表GLAdjustment中指定主码的某条记录的实例。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>返回记录的实例GLAdjustment</returns>
		public GLAdjustmentEntity Single(string adjustmentID)
		{
            string cmdText = "usp_Master_SelectSingleGLAdjustment";
			return base.GetSingleEntity((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
				cmd.Parameters.Add(new SqlParameter("@AdjustmentID", SqlDbType.NVarChar, 50){ Value = adjustmentID });
            });
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityInfoSettingID"></param>
		/// <param name="ruleSnapshotID"></param>
		/// <param name="ruleID"></param>
		/// <param name="rentType"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
        public List<GLAdjustmentEntity> Where(string entityInfoSettingID, string ruleSnapshotID, string ruleID, string rentType, DateTime? startDate, DateTime? endDate)
		{
            List<GLAdjustmentEntity> gLAdjustmentList = new List<GLAdjustmentEntity>();
            Database db = DatabaseFactory.CreateDatabase();
            //
            string sql = "usp_Master_SelectAllGLAdjustment";
            DbCommand cmd = db.GetStoredProcCommand(sql);
            db.AddInParameter(cmd, "EntityInfoSettingID", DbType.String, entityInfoSettingID);
            db.AddInParameter(cmd, "RuleSnapshotID", DbType.String, ruleSnapshotID);
            db.AddInParameter(cmd, "RuleID", DbType.String, ruleID);
            db.AddInParameter(cmd, "RentType", DbType.String, rentType);
            db.AddInParameter(cmd, "StartDate", DbType.DateTime, startDate);
            db.AddInParameter(cmd, "EndDate", DbType.DateTime, endDate);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
               gLAdjustmentList = base.SetAllEntity(dr); 
            }
			return gLAdjustmentList;
		}
    }
}