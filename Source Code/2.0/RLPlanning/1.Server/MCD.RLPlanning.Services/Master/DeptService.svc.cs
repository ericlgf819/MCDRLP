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
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class DeptService : BaseDAL<DeptEntity>, IDeptService
    {
        /// <summary>
        /// 查找所有的部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllDepartments(DeptEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }

        /// <summary>
        /// 查詢所有正常使用的部門信息
        /// 其中返回的部門名稱前面已增加公司編號
        /// </summary>
        /// <returns></returns>
        public byte[] SelectActiveDept()
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "dbo.[SRLS_USP_Master_SelectAllDepartments]";
            }));
        }
        /// <summary>
        /// 查询所属指定公司的状态为Active的餐厅/部门信息，若不指定公司编号则返回所有。
        /// </summary>
        /// <returns></returns>
        public byte[] SelectActiveStoreDept(string companyCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[usp_Mastters_SelectStoreDeptByCompanyCode]");
            db.AddInParameter(cmd, "CompanyCode", DbType.AnsiString, companyCode);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
    }
}