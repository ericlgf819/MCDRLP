using System;
using System.Data;
using System.ServiceModel;

using MCD.UUP.Entity;

namespace MCD.UUP.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ISystemService : IBaseService
    {
        /// <summary>
        /// 新增系统信息数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertSystem(SystemEntity entity);

        /// <summary>
        /// 修改系统信息数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateSystem(SystemEntity entity);

        /// <summary>
        /// 删除系统信息数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteSystem(SystemEntity entity);

        /// <summary>
        /// 获取单个系统信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        SystemEntity GetSingleSystem(SystemEntity entity);

        /// <summary>
        /// 检测数据库链接是否正确
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        bool ConnectionTest(SystemEntity entity);

        /// <summary>
        /// 获取所有系统信息
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectSystems(string systemName);
    }
}