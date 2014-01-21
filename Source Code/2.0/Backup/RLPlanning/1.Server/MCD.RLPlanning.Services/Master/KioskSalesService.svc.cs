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
	///表示对表KioskSales的所有操作的实现。
	/// </summary>
    public class KioskSalesService : BaseDAL<KioskSalesEntity>, IKioskSalesService
    {
        ///<summary>
        ///返回表KioskSales所有行实例的集合。
        ///</summary>
        ///<returns>返回所有行实例KioskSalesList对象。</returns>
        public List<KioskSalesEntity> All()
        {
            return this.Where(string.Empty, null);
        }
        ///<summary>
        /// 根据SQL查询条件返回DataSet。
        ///</summary>
        ///<param name="where">带where的查询条件</param>
        ///<returns>返回DataSet</returns>
        public byte[] GetDataSet(string where, params WcfSqlParameter[] whereParameters)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"SELECT * 
                    FROM [KioskSales] ks INNER JOIN dbo.SRLS_TB_Master_Kiosk kk ON ks.KioskID=kk.KioskID {0}", where);
                if (whereParameters != null)
                {
                    foreach (WcfSqlParameter p in whereParameters)
                    {
                        cmd.Parameters.Add(p.ToSqlParameter());
                    }
                }
            }));
        }
        ///<summary>
        ///根据SQL查询条件返回行实例的集合。
        ///</summary>
        ///<param name="where">带"WHERE"的SQL查询条件</param>
        ///<param name="whereParameters">条件中的参数信息数组</param>
        ///<returns>返回的实例KioskSalesList对象。</returns>
        public List<KioskSalesEntity> Where(string where, params WcfSqlParameter[] whereParameters)
        {
            List<KioskSalesEntity> kioskSalesList = new List<KioskSalesEntity>();
            //
            using (SqlDataReader dr = (SqlDataReader)base.ExecuteProcedureDataReader((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM [KioskSales] {0}", where);
                if (whereParameters != null)
                {
                    foreach (WcfSqlParameter p in whereParameters)
                    {
                        cmd.Parameters.Add(p.ToSqlParameter());
                    }
                }
            }))
            {
                if (dr != null && dr.HasRows)
                {
                    kioskSalesList = base.SetAllEntity(dr);
                }
            }
            return kioskSalesList;
        }
        ///<summary>
        ///获取表KioskSales中指定主码的某条记录的实例。
        ///</summary>
        ///<param name="kioskSalesID"></param>
        ///<returns>返回记录的实例KioskSales</returns>
        public KioskSalesEntity Single(string kioskSalesID)
        {
            string cmdText = "usp_Master_SelectSingleKioskSales";
            return base.GetSingleEntity((cmd) =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
                cmd.Parameters.Add(new SqlParameter("@KioskSalesID", SqlDbType.VarChar, 36) { Value = kioskSalesID });
            });
        }

		///<summary>
		///向表KioskSales中插入一条记录并返回状态。
		///</summary>
		///<param name="kioskSales">要插入记录的KioskSales实例</param>
		///<returns>返回true或false</returns>
        public int Insert(KioskSalesEntity kioskSales)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_InsertKioskSales");
            db.AddInParameter(cmd, "KioskID", DbType.String, kioskSales.KioskID);
            db.AddInParameter(cmd, "Sales", DbType.Decimal, kioskSales.Sales);
            db.AddInParameter(cmd, "SalesDate", DbType.DateTime, kioskSales.SalesDate);
            db.AddInParameter(cmd, "Remark", DbType.String, kioskSales.Remark);
            db.AddInParameter(cmd, "InputSalseUserEnglishName", DbType.String, kioskSales.InputSalseUserEnglishName);
            db.AddOutParameter(cmd, "Result", DbType.Int32, 0);
            db.ExecuteNonQuery(cmd);
            object obj = cmd.Parameters["@Result"].Value;
            return obj == DBNull.Value ? 3 : Convert.ToInt32(obj);
        }
		///<summary>
		///更新表KioskSales中指定主码的某条记录。
		///</summary>
		///<param name="kioskSales">要更新记录的KioskSales实例</param>
		///<returns>更新成功则返回true，否则返回false。</returns>
		public bool Update(KioskSalesEntity kioskSales)
		{
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_UpdateSingleKioskSales");
			db.AddInParameter(cmd, "KioskSalesID", DbType.AnsiString, kioskSales.KioskSalesID);
			db.AddInParameter(cmd, "KioskID", DbType.String, kioskSales.KioskID);
			db.AddInParameter(cmd, "CollectionID", DbType.String, kioskSales.CollectionID);
            db.AddInParameter(cmd, "Sales", DbType.Decimal, kioskSales.Sales);
			db.AddInParameter(cmd, "SalesDate", DbType.DateTime, kioskSales.SalesDate);
			db.AddInParameter(cmd, "ZoneStartDate", DbType.DateTime, kioskSales.ZoneStartDate);
			db.AddInParameter(cmd, "ZoneEndDate", DbType.DateTime, kioskSales.ZoneEndDate);
			db.AddInParameter(cmd, "IsAjustment", DbType.Boolean, kioskSales.IsAjustment);
			db.AddInParameter(cmd, "Remark", DbType.String, kioskSales.Remark);
			db.AddInParameter(cmd, "CreateTime", DbType.DateTime, kioskSales.CreateTime);
			db.AddInParameter(cmd, "InputSalseUserEnglishName", DbType.String, kioskSales.InputSalseUserEnglishName);
			return db.ExecuteNonQuery(cmd) > 0;
		}
        ///<summary>
        ///删除表KioskSales中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskSalesID"></param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        public bool Delete(string kioskSalesID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("usp_Master_DeleteSingleKioskSales");
            db.AddInParameter(cmd, "KioskSalesID", DbType.AnsiString, kioskSalesID);
            return db.ExecuteNonQuery(cmd) > 0;
        }
    }
}