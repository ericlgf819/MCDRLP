using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICompanyService : IBaseService
    {
        /// <summary>
        /// 查找所有公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllCompany(CompanyEntity entity);
        /// <summary>
        /// 查找所有公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllCompanyWithArea(CompanyEntity entity);
        /// <summary>
        /// 查找公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        CompanyEntity SelectSingleCompany(CompanyEntity entity);

        /// <summary>
        /// 插入公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int AddSingleCompany(CompanyEntity entity);
        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateSingleCompany(CompanyEntity entity);
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteSingleCompany(CompanyEntity entity);
    }
}