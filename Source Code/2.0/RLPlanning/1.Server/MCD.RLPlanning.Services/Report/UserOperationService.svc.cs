using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.Entity;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.IServices.Report;
using MCD.RLPlanning.Entity.Report;

namespace MCD.RLPlanning.Services.Report
{
    /// <summary>
    /// 用户操作日志
    /// </summary>
    public class UserOperationService : BaseDAL<UserOperationEntity>, IUserOperationService
    {
        /// <summary>
        /// 查询用户操作日志信息
        /// </summary>
        /// <param name="companyStartNo"></param>
        /// <param name="companyEndNo"></param>
        /// <param name="storeStartNo"></param>
        /// <param name="storeEndNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public byte[] SelectUserOperations(int? companyStartNo, int? companyEndNo, int? storeStartNo, int? storeEndNo,
            DateTime startDate, DateTime endDate, string operationType)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[SRLS_USP_System_SelectUserOperations]");
            if (companyStartNo.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@CompanyStartNo", SqlDbType.Int, 4) { Value = companyStartNo.Value });
            }
            if (companyEndNo.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@CompanyEndNo", SqlDbType.Int, 4) { Value = companyEndNo.Value });
            }
            if (storeStartNo.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@StoreStartNo", SqlDbType.Int, 4) { Value = storeStartNo.Value });
            }
            if (storeEndNo.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@StoreEndNo", SqlDbType.Int, 4) { Value = storeEndNo.Value });
            }
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = startDate });
            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = endDate });
            cmd.Parameters.Add(new SqlParameter("@OperationType", SqlDbType.NVarChar, 8) { Value = operationType });
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询用户表所包含的字段信息(需要显示的)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public byte[] SelectTableColumns(string tableName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[SRLS_USP_System_SelectTableColumns]");
            cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 64) { Value = tableName });
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }
    }
}