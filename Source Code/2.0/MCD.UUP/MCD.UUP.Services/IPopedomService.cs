using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace MCD.UUP.Services
{
    // 注意: 如果更改此处的接口名称 "IPopedomService"，也必须更新 Web.config 中对 "IPopedomService" 的引用。
    [ServiceContract]
    public interface IPopedomService
    {
        /// <summary>
        /// 根据用户名称获取用户有权限访问的所有菜单信息
        /// 返回表结构：
        /// 1.ModuleCode,应用程序集.类名
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetUserPopedomByUserName(string systemCode, string userName);

        /// <summary>
        /// 根据用户组获取用户组有权限访问的所有菜单信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetGroupPopedom(string systemCode, Guid groupID);

        /// <summary>
        /// 判断某个用户是否具有访问某个模块权限
        /// </summary>
        /// <param name="moduleCode">模块名，默认应用程序集.类名</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [OperationContract]
        bool HasRightModule(string systemCode, string moduleCode, string userName);

        /// <summary>
        /// 获取某个模块中用户所有的功能权限
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFormFunctionRight(string systemCode, string moduleCode, string userName);

        /// <summary>
        /// 获取某个模块中用户组的功能权限
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetGroupFormFunctionRight(string systemCode, string moduleCode, Guid groupID);

    }
}

