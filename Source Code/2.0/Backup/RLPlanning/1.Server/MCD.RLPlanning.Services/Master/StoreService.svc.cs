using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreService : BaseDAL<StoreEntity>, IStoreService
    {
        /// <summary>
        /// 查找所有的餐厅信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllStore(StoreEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }
        /// <summary>
        /// 分页返回查询结果。
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="companyCode"></param>
        /// <param name="storeNo"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public byte[] USDX(Guid? areaID, string companyCode, string storeNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[SRLS_USP_Master_SelectStore]");
            db.AddInParameter(cmd, "@AreaID", DbType.Guid, areaID);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, companyCode);
            db.AddInParameter(cmd, "@StoreNo", DbType.String, storeNo);
            db.AddInParameter(cmd, "@Status", DbType.String, status);
            db.AddInParameter(cmd, "@FromSRLS", DbType.Boolean, FromSRLS);
            db.AddInParameter(cmd, "@UserId", DbType.Guid, UserId);
            //
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            object obj = cmd.Parameters["@RecordCount"].Value;
            recordCount = (obj == null ? 0 : (int)obj);
            return Serilize(ds);
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public StoreEntity SelectSingleStore(StoreEntity entity)
        {
            return base.GetSingleEntity(entity);
        }


        /// <summary>
        /// 插入單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertSingleStore (string storeNo, string CompanyCode, string StoreName, string SimpleName, 
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID)
        {
            object oResult = base.ExecuteProcedureScalar((cmd) =>
            {
                cmd.CommandText = "dbo.[SRLS_USP_Master_InsertSingleStore]";
                cmd.Parameters.Add(new SqlParameter("@storeNo", SqlDbType.NVarChar, 50) { Value = storeNo });
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", SqlDbType.NVarChar, 50) { Value = CompanyCode });
                cmd.Parameters.Add(new SqlParameter("@StoreName", SqlDbType.NVarChar, 200) { Value = StoreName });
                cmd.Parameters.Add(new SqlParameter("@SimpleName", SqlDbType.NVarChar, 50) { Value = SimpleName });
                cmd.Parameters.Add(new SqlParameter("@OpenDate", SqlDbType.DateTime) { Value = OpenDate });
                cmd.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime) { Value = CloseDate });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 1) { Value = Status });
                cmd.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 200) { Value = EmailAddress });
                cmd.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            });
            int nResult = 0;
            int.TryParse(oResult.ToString(), out nResult);
            return nResult;
        }
        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleStore (string storeNo, string CompanyCode, string StoreName, string SimpleName, 
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID)
        {
            object objResult = base.ExecuteProcedureScalar((cmd) =>
            {
                cmd.CommandText = "dbo.[SRLS_USP_Master_UpdateSingleStore]";
                cmd.Parameters.Add(new SqlParameter("@storeNo", SqlDbType.NVarChar, 50) { Value = storeNo });
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", SqlDbType.NVarChar, 50) { Value = CompanyCode });
                cmd.Parameters.Add(new SqlParameter("@StoreName", SqlDbType.NVarChar, 200) { Value = StoreName });
                cmd.Parameters.Add(new SqlParameter("@SimpleName", SqlDbType.NVarChar, 50) { Value = SimpleName });
                if (OpenDate.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@OpenDate", SqlDbType.DateTime) { Value = OpenDate.Value });
                }
                if (CloseDate.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime) { Value = CloseDate.Value });
                }
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 1) { Value = Status });
                cmd.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 200) { Value = EmailAddress });
                cmd.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            });
            int nResult = 0;
            int.TryParse(objResult.ToString(), out nResult);
            return nResult;
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateMultiStore(string StoreNO,int Count)
        {
            return base.ExecuteProcedureInt((cmd) =>
            {
                cmd.CommandText = "dbo.[UpdateMultiStore]";
                cmd.Parameters.Add(new SqlParameter("@StoreNO", SqlDbType.NVarChar, 50) { Value = StoreNO });
                cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 50) { Value = Count });
            });
        }
        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeletedSingleStore(StoreEntity _StoreEntity)
        {
            string strResult = base.ExecuteProcedureScalar((cmd) => {
                cmd.CommandText = "dbo.[SRLS_USP_Master_DeletedSingleStore]";
                cmd.Parameters.Add(new SqlParameter("@storeNo", SqlDbType.NVarChar, 50) { Value = _StoreEntity.StoreNo });
            }).ToString();
            int nResult = int.Parse(strResult);
            return nResult;
        }

        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Update_Sequence (string TableName, string FieldName)
        {
            return base.ExecuteProcedureScalar((cmd) =>
            {
                cmd.CommandText = "dbo.[Update_Sequence]";
                cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 50) { Value = TableName });
                cmd.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.NVarChar, 50) { Value = FieldName });
            }).ToString();
        }
        public DateTime? GetStoreOpenDate(string StoreNo)
        {
            DateTime? OpenDate = null;
            base.ExecuteProcedureNoExecute((cmd) =>
            {
                cmd.CommandText = "dbo.[RLP_Master_GetStoreOpenDate]";
                cmd.Parameters.Add(new SqlParameter("@StoreNo", SqlDbType.NVarChar, 50) { Value = StoreNo });
                cmd.Parameters.Add(new SqlParameter("@OpenDate", SqlDbType.Date) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                if (cmd.Parameters["@OpenDate"].Value != null)
                {
                    OpenDate = Convert.ToDateTime(cmd.Parameters["@OpenDate"].Value);
                }
            });
            //
            return OpenDate;
        }
    }
}