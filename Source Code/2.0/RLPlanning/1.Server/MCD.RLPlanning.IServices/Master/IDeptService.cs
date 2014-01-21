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
    public interface IDeptService : IBaseService
    {
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllDepartments(DeptEntity entity);

        /// <summary>
        /// 查詢所有正常使用的部門信息
        /// 其中返回的部門名稱前面已增加公司編號
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectActiveDept();

        /// <summary>
        /// 查询所属指定公司的状态为Active的餐厅/部门信息，若不指定公司编号则返回所有。
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectActiveStoreDept(string companyCode);
    }
}