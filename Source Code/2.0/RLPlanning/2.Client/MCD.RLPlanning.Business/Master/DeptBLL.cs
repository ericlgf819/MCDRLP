using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 部门信息业务逻辑层
    /// </summary>
    public class DeptBLL : BaseBLL<IDeptService>
    {
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllDepartments(DeptEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectAllDepartments(entity));
        }

        /// <summary>
        /// 查詢所有正常使用的部門信息
        /// 其中返回的部門名稱前面已增加公司編號
        /// </summary>
        /// <returns></returns>
        public DataSet SelectActiveDept()
        {
            return base.DeSerilize(base.WCFService.SelectActiveDept());
        }
        /// <summary>
        /// 查询所属指定公司的状态为Active的餐厅/部门信息，若不指定公司编号则返回所有。
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectActiveStoreDept(string companyCode)
        {
            System.Data.DataTable dt = null;
            DataSet ds = base.DeSerilize(base.WCFService.SelectActiveStoreDept(companyCode));
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}