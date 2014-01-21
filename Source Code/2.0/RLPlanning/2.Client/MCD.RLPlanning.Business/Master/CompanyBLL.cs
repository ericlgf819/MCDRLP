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
    /// 
    /// </summary>
    public class CompanyBLL : BaseBLL<ICompanyService>
    {
        /// <summary>
        /// 获取所有公司信息 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllComapny(CompanyEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectAllCompany(entity));
        }
        /// <summary>
        /// 获取所有公司信息 以Area ShowOrder 排序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllComapnyWithArea(CompanyEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectAllCompanyWithArea(entity));
        }
        /// <summary>
        /// 由CompanyCode获取公司信息
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public CompanyEntity SelectSingleCompany(string companyCode)
        {
            CompanyEntity entity = new CompanyEntity() {
                CompanyCode = companyCode
            };
            return base.WCFService.SelectSingleCompany(entity);
        }
        
        /// <summary>
        /// 插入公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddSingleCompany(CompanyEntity entity)
        {
            return base.WCFService.AddSingleCompany(entity);
        }
        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleCompany(CompanyEntity entity)
        {
            return base.WCFService.UpdateSingleCompany(entity);
        }
        /// <summary>
        /// 插入公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSingleCompany(CompanyEntity entity)
        {
            return base.WCFService.DeleteSingleCompany(entity);
        }
    }
}