using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCompanyBLL : BaseBLL<IUserCompanyService>
    {
        /// <summary>
        /// 获取用户公司关系列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectUserCompany(UserCompanyEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectUserCompany(entity));
        }
        /// <summary>
        /// 获取用户公司区域列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectUserArea(UserCompanyEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectUserArea(entity));
        }
        
        /// <summary>
        /// 新增用户公司关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateUserCompany(UserCompanyEntity entity)
        {
            base.WCFService.UpdateUserCompany(entity);
        }

        /// <summary>
        /// 删除用户公司关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void DeleteUserCompany(UserCompanyEntity entity)
        {
            base.WCFService.DeleteUserCompany(entity);
        }
    }
}