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
	///表示对表SRLS_TB_Master_Kiosk的所有操作的实现。
	/// </summary>
    public class KioskService : BaseDAL<KioskEntity>, IKioskService
    {
        ///<summary>
        ///向表SRLS_TB_Master_Kiosk中插入一条记录并返回状态。
        ///</summary>
        ///<param name="entity">要插入记录的SRLS_TB_Master_Kiosk实例</param>
        ///<returns>返回true或false</returns>
        public int Insert(KioskEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_InsertSingleKiosk]");
            db.AddInParameter(cmd, "KioskID", DbType.String, entity.KioskID);
            db.AddInParameter(cmd, "StoreNo", DbType.String, entity.StoreNo);
            db.AddInParameter(cmd, "KioskNo", DbType.String, entity.KioskNo);
            db.AddInParameter(cmd, "TemStoreNo", DbType.String, entity.TemStoreNo);
            db.AddInParameter(cmd, "ActiveDate", DbType.DateTime, entity.ActiveDate);
            db.AddInParameter(cmd, "KioskName", DbType.String, entity.KioskName);
            db.AddInParameter(cmd, "SimpleName", DbType.String, entity.SimpleName);
            db.AddInParameter(cmd, "Address", DbType.String, entity.Address);
            db.AddInParameter(cmd, "KioskType", DbType.String, entity.KioskType);
            db.AddInParameter(cmd, "Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "OpenDate", DbType.DateTime, entity.OpenDate);
            db.AddInParameter(cmd, "CloseDate", DbType.DateTime, entity.CloseDate);
            db.AddInParameter(cmd, "IsEnable", DbType.Boolean, entity.IsEnable);
            db.AddInParameter(cmd, "IsLocked", DbType.Boolean, entity.IsLocked);
            db.AddInParameter(cmd, "Status", DbType.String, entity.Status);
            db.AddInParameter(cmd, "IsNeedSubtractSalse", DbType.Boolean, entity.IsNeedSubtractSalse);
            db.AddInParameter(cmd, "PartComment", DbType.String, entity.PartComment);
            db.AddInParameter(cmd, "CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "CreatorName", DbType.String, entity.CreatorName);
            db.AddInParameter(cmd, "LastModifyTime", DbType.DateTime, entity.LastModifyTime);
            db.AddInParameter(cmd, "LastModifyUserName", DbType.String, entity.LastModifyUserName);
            db.AddOutParameter(cmd, "Result", DbType.Int32, 0);
            db.ExecuteNonQuery(cmd);
            //
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        ///<summary>
        ///更新表SRLS_TB_Master_Kiosk中指定主码的某条记录。
        ///</summary>
        ///<param name="entity">要更新记录的SRLS_TB_Master_Kiosk实例</param>
        ///<returns>更新成功则返回true，否则返回false。</returns>
        public int Update(KioskEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_UpdateSingleKiosk]");
            db.AddInParameter(cmd, "KioskID", DbType.String, entity.KioskID);
            db.AddInParameter(cmd, "StoreNo", DbType.String, entity.StoreNo);
            db.AddInParameter(cmd, "KioskNo", DbType.String, entity.KioskNo);
            db.AddInParameter(cmd, "TemStoreNo", DbType.String, entity.TemStoreNo);
            db.AddInParameter(cmd, "ActiveDate", DbType.String, entity.ActiveDate);
            db.AddInParameter(cmd, "KioskName", DbType.String, entity.KioskName);
            db.AddInParameter(cmd, "SimpleName", DbType.String, entity.SimpleName);
            db.AddInParameter(cmd, "Address", DbType.String, entity.Address);
            db.AddInParameter(cmd, "KioskType", DbType.String, entity.KioskType);
            db.AddInParameter(cmd, "Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "OpenDate", DbType.DateTime, entity.OpenDate);
            db.AddInParameter(cmd, "CloseDate", DbType.DateTime, entity.CloseDate);
            //
            db.AddInParameter(cmd, "IsEnable", DbType.Boolean, entity.IsEnable);
            db.AddInParameter(cmd, "IsLocked", DbType.Boolean, entity.IsLocked);
            db.AddInParameter(cmd, "Status", DbType.String, entity.Status);
            db.AddInParameter(cmd, "IsNeedSubtractSalse", DbType.Boolean, entity.IsNeedSubtractSalse);
            db.AddInParameter(cmd, "PartComment", DbType.String, entity.PartComment);
            db.AddInParameter(cmd, "CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "CreatorName", DbType.String, entity.CreatorName);
            db.AddInParameter(cmd, "LastModifyTime", DbType.DateTime, entity.LastModifyTime);
            db.AddInParameter(cmd, "LastModifyUserName", DbType.String, entity.LastModifyUserName);
            db.AddOutParameter(cmd, "Result", DbType.Int32, 0);//
            db.ExecuteNonQuery(cmd);
            //
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        ///<summary>
        ///删除表SRLS_TB_Master_Kiosk中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskID"></param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        public bool Delete(string kioskID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_DeleteSingleKiosk]");
            db.AddInParameter(cmd, "KioskID", DbType.String, kioskID);
            return db.ExecuteNonQuery(cmd) > 0;
        }

        public string Update_Sequence()
        {
            return base.ExecuteProcedureScalar((cmd) => {
                cmd.CommandText = "dbo.[Update_Sequence]";
                cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 50) { Value = "SRLS_TB_Master_Kiosk" });
                cmd.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.NVarChar, 50) { Value = "KioskNo" });
            }).ToString();
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateMultiKiosk(string KioskID, int Count)
        {
            return base.ExecuteProcedureInt((cmd) => {
                cmd.CommandText = "dbo.[UpdateMultiKiosk]";
                cmd.Parameters.Add(new SqlParameter("@KioskID", SqlDbType.NVarChar, 50) { Value = KioskID });
                cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 50) { Value = Count });
            });
        }
		
		///<summary>
		///获取表SRLS_TB_Master_Kiosk中指定主码的某条记录的实例。
		///</summary>
		///<param name="kioskNo">KioskNo</param>
		///<returns>返回记录的实例SRLS_TB_Master_Kiosk</returns>
		public KioskEntity Single(string kioskID)
		{
            string cmdText = "dbo.[usp_Master_SelectSingleKiosk]";
			return base.GetSingleEntity((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
                cmd.Parameters.Add(new SqlParameter("@KioskID", SqlDbType.NVarChar, 50) { Value = kioskID });
            });
		}
		///<summary>
		///根据SQL查询条件返回行实例的集合。
		///</summary>
		///<param name="where">带"WHERE"的SQL查询条件</param>
		///<param name="whereParameters">条件中的参数信息数组</param>
		///<returns>返回的实例SRLS_TB_Master_KioskList对象。</returns>
        public List<KioskEntity> Where(Guid? areaID, string CompanyCode, string storeNo, string kioskNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            List<KioskEntity> sRLS_TB_Master_KioskList = new List<KioskEntity>();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_SelectAllKiosk]");
            db.AddInParameter(cmd, "@AreaID", DbType.Guid, areaID);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            db.AddInParameter(cmd, "@StoreNo", DbType.String, storeNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, kioskNo);
            db.AddInParameter(cmd, "@Status", DbType.String, status);
            db.AddInParameter(cmd, "@FromSRLS", DbType.Boolean, FromSRLS);
            db.AddInParameter(cmd, "@UserId", DbType.Guid, UserId);
            //
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 0);
            using (SqlDataReader dr = db.ExecuteReader(cmd) as SqlDataReader)
            {
                if (dr != null && dr.HasRows)
                {
                    sRLS_TB_Master_KioskList = base.SetAllEntity(dr);
                }
            }
            object result = db.GetParameterValue(cmd, "RecordCount");
            recordCount = result == null ? 0 : (int)result;
            return sRLS_TB_Master_KioskList;
        }

        /// <summary>
        /// 获取指定甜品店最近的挂靠记录。
        /// </summary>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public byte[] GetRecentChangeHistory(string kioskID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT TOP 1 * FROM KioskStoreRelationChangHistory WHERE KioskID=@KioskID");
            db.AddInParameter(cmd, "@KioskID", DbType.String, kioskID);
            return base.Serilize(db.ExecuteDataSet(cmd));
        }
        /// <summary>
        /// 获取指定Kiosk的挂靠记录。
        /// </summary>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public byte[] GetChangeRelationHistory(string kioskID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetSqlStringCommand(@"SELECT KioskID, KioskNo, IsNeedSubtractSalse, StoreNo, StartDate,
                (CASE WHEN DATEDIFF(DD, EndDate, '21001231')=0 THEN NULL ELSE EndDate END) AS EndDate, CreateUserEnglishName, CreateTime 
                FROM dbo.v_KioskStoreZoneInfo WHERE KioskID=@KioskID ORDER BY StartDate");
            db.AddInParameter(cmd, "@KioskID", DbType.String, kioskID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return Serilize(ds);
        }
        /// <summary>
        /// 删除指定的挂靠记录。
        /// </summary>
        /// <param name="changeID"></param>
        public void DeleteKioskChangeRelationHistory(string changeID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetSqlStringCommand("DELETE FROM KioskStoreRelationChangHistory WHERE ChangeID=@ChangeID");
            db.AddInParameter(cmd, "@ChangeID", DbType.String, changeID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取指定名称指定长度的流水号。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetFlowNumber(string name, int length)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_CreateKioskFlowNumber]");
            db.AddInParameter(cmd, "Name", DbType.AnsiString, name);
            db.AddInParameter(cmd, "Length", DbType.Int32, length);
            db.AddOutParameter(cmd, "FlowNumber", DbType.String, 10);
            db.ExecuteNonQuery(cmd);
            //
            return cmd.Parameters["@FlowNumber"].Value.ToString();
        }
        /// <summary>
        /// 根据公司编号获取流水号。
        /// </summary>
        /// <param name="compamyCode"></param>
        /// <returns></returns>
        public string GetKioskNumber(string compamyCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT dbo.fn_SerialNumber_Kiosk(@CompanyCode) AS KioskNumber");
            db.AddInParameter(cmd, "CompanyCode", DbType.AnsiString, compamyCode);
            object obj = db.ExecuteScalar(cmd);
            return (obj == null ? string.Empty : obj.ToString());
        }
        /// <summary>
        /// 获取指定甜品店编号的甜品店是否存在与之关联的合同。
        /// </summary>
        /// <param name="kioskNo">kioskNo</param>
        /// <returns></returns>
        public bool ExistsRelatedContract(string kioskNo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT TOP 1 ContractSnapshotID FROM Entity WHERE EntityTypeName='甜品店' AND KioskNo=@KioskNo");
            db.AddInParameter(cmd, "KioskNo", DbType.AnsiString, kioskNo);
            object obj = db.ExecuteScalar(cmd);
            return obj != null;
        }
    }
}