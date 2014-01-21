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
	///表示对表KioskSalesCollection的所有操作的实现。
	/// </summary>
    public class KioskSalesCollectionService : BaseDAL<KioskSalesCollectionEntity>, IKioskSalesCollectionService
    {
		///<summary>
		///删除表KioskSalesCollection中的指定记录并返回状态。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		public bool Delete(string collectionID)
		{
			Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_DeleteSingleKioskSalesCollection");
			db.AddInParameter(cmd, "CollectionID", DbType.String, collectionID);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///向表KioskSalesCollection中插入一条记录并返回状态。
		///</summary>
		///<param name="kioskSalesCollection">要插入记录的KioskSalesCollection实例</param>
		///<returns>返回true或false</returns>
		public bool Insert(KioskSalesCollectionEntity kioskSalesCollection)
		{
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_InsertSingleKioskSalesCollection");
			db.AddInParameter(cmd, "CollectionID", DbType.String, kioskSalesCollection.CollectionID);
			db.AddInParameter(cmd, "KioskID", DbType.String, kioskSalesCollection.KioskID);
			db.AddInParameter(cmd, "WorkflowRelationID", DbType.String, kioskSalesCollection.WorkflowRelationID);
            db.AddInParameter(cmd, "Sales", DbType.Decimal, kioskSalesCollection.Sales);
			db.AddInParameter(cmd, "ZoneStartDate", DbType.DateTime, kioskSalesCollection.ZoneStartDate);
			db.AddInParameter(cmd, "ZoneEndDate", DbType.DateTime, kioskSalesCollection.ZoneEndDate);
			db.AddInParameter(cmd, "Remark", DbType.String, kioskSalesCollection.Remark);
			db.AddInParameter(cmd, "CreateTime", DbType.DateTime, kioskSalesCollection.CreateTime);
			db.AddInParameter(cmd, "InputSalseUserEnglishName", DbType.String, kioskSalesCollection.InputSalseUserEnglishName);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///更新表KioskSalesCollection中指定主码的某条记录。
		///</summary>
		///<param name="kioskSalesCollection">要更新记录的KioskSalesCollection实例</param>
		///<returns>更新成功则返回true，否则返回false。</returns>
		public bool Update(KioskSalesCollectionEntity kioskSalesCollection)
		{
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_UpdateSingleKioskSalesCollection");
			db.AddInParameter(cmd, "CollectionID", DbType.String, kioskSalesCollection.CollectionID);
			db.AddInParameter(cmd, "KioskID", DbType.String, kioskSalesCollection.KioskID);
			db.AddInParameter(cmd, "WorkflowRelationID", DbType.String, kioskSalesCollection.WorkflowRelationID);
            db.AddInParameter(cmd, "Sales", DbType.Decimal, kioskSalesCollection.Sales);
			db.AddInParameter(cmd, "ZoneStartDate", DbType.DateTime, kioskSalesCollection.ZoneStartDate);
			db.AddInParameter(cmd, "ZoneEndDate", DbType.DateTime, kioskSalesCollection.ZoneEndDate);
			db.AddInParameter(cmd, "Remark", DbType.String, kioskSalesCollection.Remark);
			db.AddInParameter(cmd, "CreateTime", DbType.DateTime, kioskSalesCollection.CreateTime);
			db.AddInParameter(cmd, "InputSalseUserEnglishName", DbType.String, kioskSalesCollection.InputSalseUserEnglishName);
			return db.ExecuteNonQuery(cmd) > 0;
		}
		
		///<summary>
		///获取表KioskSalesCollection中指定主码的某条记录的实例。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>返回记录的实例KioskSalesCollection</returns>
		public KioskSalesCollectionEntity Single(string collectionID)
		{
            string cmdText = "usp_Master_SelectSingleKioskSalesCollection";
			return base.GetSingleEntity((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
				cmd.Parameters.Add(new SqlParameter("@CollectionID", SqlDbType.NVarChar, 50){ Value = collectionID });
            });
		}
		
		///<summary>
		///根据SQL查询条件返回行实例的集合。
		///</summary>
		///<param name="where">带"WHERE"的SQL查询条件</param>
		///<param name="whereParameters">条件中的参数信息数组</param>
		///<returns>返回的实例KioskSalesCollectionList对象。</returns>
		public List<KioskSalesCollectionEntity> Where(string where, params WcfSqlParameter[] whereParameters)
		{
			List<KioskSalesCollectionEntity> kioskSalesCollectionList = new List<KioskSalesCollectionEntity>();
            //
			using(SqlDataReader dr =  (SqlDataReader)base.ExecuteProcedureDataReader((cmd) =>  {
				cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM [KioskSalesCollection] {0}", where);
				if (whereParameters != null)
                {
                    foreach (WcfSqlParameter p in whereParameters)
                    {
                        cmd.Parameters.Add(p.ToSqlParameter());
                    }
                }
			}))
			{
				if(dr != null && dr.HasRows)
				{
					kioskSalesCollectionList = base.SetAllEntity(dr);
				}
			}
			return kioskSalesCollectionList;
		}
		
		///<summary>
		///返回表KioskSalesCollection所有行实例的集合。
		///</summary>
		///<returns>返回所有行实例KioskSalesCollectionList对象。</returns>
		public List<KioskSalesCollectionEntity> All()
		{
			return this.Where(string.Empty, null);
		}
    }
}