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
using MCD.RLPlanning.Entity.Setting;
using MCD.RLPlanning.IServices.Setting;

namespace MCD.RLPlanning.Services.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class RentTypeService : BaseDAL<RentTypeEntity>, IRentTypeService
    {
        /// <summary>
        /// GL计算日期设置
        /// </summary>
        public int UpdateGLStartDate(RentTypeEntity entity)
        {
            int returnValue = 0;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand comm = db.GetStoredProcCommand("usp_Master_UpdateGLStartDate");
            db.AddInParameter(comm, "@RentTypeName", DbType.String, entity.RentTypeName);
            db.AddInParameter(comm, "@WhichMonth", DbType.String, entity.WhichMonth);
            db.AddInParameter(comm, "@GLStartDate", DbType.Int32, entity.GLStartDate);
            db.AddInParameter(comm, "@LastModifyUserName", DbType.String, entity.LastModifyUserName);
            db.AddOutParameter(comm,"@Res",DbType.Int32,4);
            db.ExecuteNonQuery(comm);
            //
            returnValue = (int)db.GetParameterValue(comm, "@Res");
            return returnValue;
        }

        /// <summary>
        /// 获取所有GL计算日期
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRentType()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataTable  dt = db.ExecuteDataSet(CommandType.StoredProcedure, "usp_Master_SelectGLStartDate").Tables[0];
            return dt;
        }
    }
}