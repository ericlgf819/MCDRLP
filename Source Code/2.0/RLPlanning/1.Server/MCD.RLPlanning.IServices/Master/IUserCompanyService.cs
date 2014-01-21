using System;
using System.Collections.Generic;
using System.ServiceModel;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices.Master
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IUserCompanyService"，也必须更新 Web.config 中对 "IUserCompanyService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IUserCompanyService : IBaseService
    {
        /// <summary>
        /// 获取用户公司关系列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserCompany(UserCompanyEntity entity);
        /// <summary>
        /// 获取用户公司关系列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserArea(UserCompanyEntity entity);
        
        /// <summary>
        /// 新增用户公司关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void UpdateUserCompany(UserCompanyEntity entity);

        /// <summary>
        /// 删除用户公司关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteUserCompany(UserCompanyEntity entity);
    }
}