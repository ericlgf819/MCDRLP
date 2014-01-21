using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using MCD.UUP.IServices;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "PopedomService"，也必须更新 Web.config 中对 "PopedomService" 的引用。 
    /// </summary>
    public class PopedomService : BaseService, IPopedomService
    {
        /// <summary>
        /// 根据用户名称获取用户有权限访问的所有菜单信息
        /// 返回表结构：
        /// 1.ModuleCode,应用程序集.类名
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetUserPopedomByUserName(string systemCode, string userName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectUserModule]";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
            });
        }

        /// <summary>
        /// 根据用户组获取用户组有权限访问的所有菜单信息
        /// 返回表结构：
        /// 1.ModuleCode,应用程序集.类名
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetGroupPopedom(string systemCode, Guid groupID)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectGroupModule]";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, 36) { Value = groupID });
            });
        }

        /// <summary>
        /// 判断某个用户是否具有访问某个模块权限
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleCode">模块名，默认应用程序集.类名</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool HasRightModule(string systemCode, string moduleCode, string userName)
        {
            object obj = base.GetExecuteScalar((cmd) => {
                cmd.CommandText = "[UUP_FUN_IsUserHasModuleRight]";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar, 64) { Value = moduleCode });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
            });
            return obj != null;
        }

        /// <summary>
        /// 获取某个模块中用户所有的功能权限
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetFormFunctionRight(string systemCode, string moduleCode, string userName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectUserFunctionByUserAccount]";
                cmd.Parameters.Add(new SqlParameter("@UserAccount", SqlDbType.NVarChar, 32) { Value = userName });
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar, 64) { Value = moduleCode });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
            });
        }

        /// <summary>
        /// 获取某个模块中用户组的功能权限
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetGroupFormFunctionRight(string systemCode, string moduleCode, Guid groupID)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectGroupFunction]";
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, 32) { Value = groupID });
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar, 64) { Value = moduleCode });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
            });
        }
    }
}