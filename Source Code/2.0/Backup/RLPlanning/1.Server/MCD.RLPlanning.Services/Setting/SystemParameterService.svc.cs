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
using MCD.RLPlanning.IServices.Setting;
using MCD.RLPlanning.Entity.Setting;

namespace MCD.RLPlanning.Services.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemParameterService : BaseDAL<SystemParameterEntity>, ISystemParameterService
    {
        /// <summary>
        /// 查询系统参数
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSystemParameter()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataTable dt = db.ExecuteDataSet(CommandType.StoredProcedure, "usp_Sys_SelectSystemParameter").Tables[0];
            return dt;
        }

        /// <summary>
        /// 修改系统参数的值和备注
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSystemParameter(SystemParameterEntity entity)
        {
           Database db =DatabaseFactory.CreateDatabase("DBConnection");
            //
           DbCommand comm = db.GetStoredProcCommand("usp_Sys_UpdateSystemParameter");
           db.AddInParameter(comm, "@ParamCode", DbType.String, entity.ParamCode);
           db.AddInParameter(comm, "@ParamName",DbType.String,entity.ParamName);
           db.AddInParameter(comm, "@ParamValue", DbType.String, entity.ParamValue);
           db.AddInParameter(comm, "@Remark", DbType.String, entity.Remark);
           db.ExecuteNonQuery(comm);
        }

        public string GetSystemParameterByCode(string paramCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            string sql = string.Format("SELECT dbo.fn_GetSysParamValueByCode('{0}')", paramCode);
            object result = db.ExecuteScalar(CommandType.Text, sql);
            return result == DBNull.Value ? null : result.ToString();
        }
    }
}