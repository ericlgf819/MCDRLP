using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class VendorService : BaseDAL<VendorEntity>, IVendorService
    {
        /// <summary>
        /// 按页返回业主信息。
        /// </summary>
        /// <param name="vendorName"></param>
        /// <param name="vendorNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public byte[] SelectVendorPagedResult(string vendorName, string vendorNo, int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[SRLS_USP_Master_SelectVendor]");
            db.AddInParameter(cmd, "@VendorName", DbType.String, vendorName);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, vendorNo);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);
            //
            DataSet ds = db.ExecuteDataSet(cmd);
            object obj = cmd.Parameters["@RecordCount"].Value;
            recordCount = (obj == null ? 0 : (int)obj);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 按业主编号或者业主名称模糊查询业主信息。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte[] SelectVendorByNoOrName(string keywords)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Master_SelectVendorByNoOrName]");
            db.AddInParameter(cmd, "@Keywords", DbType.String, keywords);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
    }
}