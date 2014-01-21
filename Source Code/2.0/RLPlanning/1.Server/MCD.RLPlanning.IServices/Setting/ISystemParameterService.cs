using System;
using System.Data;
using System.ServiceModel;

using MCD.RLPlanning.Entity.Setting;

namespace MCD.RLPlanning.IServices.Setting
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "ISystemParameterService"，也必须更新 Web.config 中对 "ISystemParameterService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface ISystemParameterService : IBaseService
    {
        /// <summary>
        /// 获取所有系统参数
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectSystemParameter();

        /// <summary>
        /// 修改系统参数的值和备注
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void UpdateSystemParameter(SystemParameterEntity entity);

        /// <summary>
        /// 获取系统参数表配置的值
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetSystemParameterByCode(string paramCode);
    }
}