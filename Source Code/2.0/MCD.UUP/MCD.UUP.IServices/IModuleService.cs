using System;
using System.Data;
using System.ServiceModel;

using MCD.UUP.Entity;

namespace MCD.UUP.IServices
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IModuleService"，也必须更新 Web.config 中对 "IModuleService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IModuleService 
    {
        /// <summary>
        /// 新增模块信息数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtFunction"></param>
        /// <returns></returns>
        [OperationContract]
        bool InsertModule(ModuleEntity entity, DataTable dtFunction);

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtFunction"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateModule(ModuleEntity entity, DataTable dtFunction);

        /// <summary>
        /// 删除模块信息数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteModule(ModuleEntity entity);

        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        ModuleEntity GetSingleModule(ModuleEntity entity);

        /// <summary>
        /// 获取所有模块信息
        /// </summary>
        /// <param name="systemID">系统ID</param>
        /// <param name="moduleName">模块/编码名称</param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectModules(Guid systemID,string moduleName);

        /// <summary>
        /// 获取所有模块信息
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectModulesBySystemCode(string systemCode, string moduleName);

        /// <summary>
        /// 获取所有模块信息，其中包含按功能分组的信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleName"></param>
        /// <param name="userOrGroupID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectModuleFunctionBySystemCodeAndGroup(string systemCode, string moduleName, string userOrGroupID);

        /// <summary>
        /// 获取功能项表
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFunctionByModuleID(Guid moduleID);

        /// <summary>
        /// 依据系统编码获取功能项表
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFunctionBySystemCode(string systemCode);

        /// <summary>
        /// 检测模块代码是否已经存在
        /// </summary>
        /// <param name="systemID">系统 ID</param>
        /// <param name="moduleID"></param>
        /// <param name="moduleCode">模块编码</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsModuleCode(Guid systemID, Guid moduleID, string moduleCode);

        /// <summary>
        /// 根据用户ID或用户组ID获取该用户或用户组所拥有的权限功能
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFunctionByUserOrGroupID(Guid userOrGroupID);

        /// <summary>
        /// 更新用户或用户组的模块信息
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <param name="dtUserFunction"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUserOrGroupFunction(Guid userOrGroupID,DataTable dtUserFunction);

        /// <summary>
        /// 按组更新权限信息
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <param name="dtModuleFunction"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUserOrGroupFunctionWithGroup(Guid userOrGroupID, DataTable dtModuleFunction);
    }
}