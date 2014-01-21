using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

using MCD.UUP.Common;
using MCD.UUP.Entity;
using MCD.UUP.IServices;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "ModuleService"，也必须更新 Web.config 中对 "ModuleService" 的引用。 
    /// </summary>
    public class ModuleService : BaseService, IModuleService
    {
        #region IModuleService 成员

        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtFunction"></param>
        /// <returns></returns>
        public bool InsertModule(ModuleEntity entity, DataTable dtFunction)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UUP_FUN_InsertOrUpdateModule]";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar) { Value = entity.ModuleName });
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar) { Value = entity.ModuleCode });
                cmd.Parameters.Add(new SqlParameter("@SystemID", SqlDbType.UniqueIdentifier) { Value = entity.SystemID });
                cmd.Parameters.Add(new SqlParameter("@SortIndex", SqlDbType.Int) { Value = entity.SortIndex });
                cmd.ExecuteNonQuery();
                //
                foreach (DataRow row in dtFunction.Rows)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[UUP_FUN_InsertFunctionInfo]";
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                    cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier) { Value = row["ModuleID"] });
                    cmd.Parameters.Add(new SqlParameter("@FunctionName", SqlDbType.NVarChar) { Value = row["FunctionName"] });
                    cmd.Parameters.Add(new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = row["FunctionCode"] });
                    cmd.Parameters.Add(new SqlParameter("@FunctionType", SqlDbType.NVarChar) { Value = row["FunctionType"] });
                    cmd.ExecuteNonQuery();
                }
                return true;
            });
        }
        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtFunction"></param>
        /// <returns></returns>
        public bool UpdateModule(ModuleEntity entity, DataTable dtFunction)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UUP_FUN_InsertOrUpdateModule]";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar) { Value = entity.ModuleName });
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar) { Value = entity.ModuleCode });
                cmd.Parameters.Add(new SqlParameter("@SystemID", SqlDbType.UniqueIdentifier) { Value = entity.SystemID });
                cmd.Parameters.Add(new SqlParameter("@SortIndex", SqlDbType.Int) { Value = entity.SortIndex });
                cmd.ExecuteNonQuery();
                //
                foreach (DataRow row in dtFunction.Rows)
                {
                    cmd.Parameters.Clear();
                    switch (row.RowState)
                    {
                        case DataRowState.Added:
                            cmd.CommandText = "[UUP_FUN_InsertFunctionInfo]";
                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                            cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier) { Value = row["ModuleID"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionName", SqlDbType.NVarChar) { Value = row["FunctionName"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = row["FunctionCode"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionType", SqlDbType.NVarChar) { Value = row["FunctionType"] });
                            break;
                        case DataRowState.Deleted:
                            cmd.CommandText = "[UUP_FUN_DeleteFunction]";
                            object id = row[dtFunction.PrimaryKey[0].ColumnName, DataRowVersion.Original];
                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = id });
                            break;
                        case DataRowState.Modified:
                            cmd.CommandText = "[UUP_FUN_UpdateFunction]";
                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                            cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier) { Value = row["ModuleID"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionName", SqlDbType.NVarChar) { Value = row["FunctionName"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = row["FunctionCode"] });
                            cmd.Parameters.Add(new SqlParameter("@FunctionType", SqlDbType.NVarChar) { Value = row["FunctionType"] });
                            break;
                        case DataRowState.Detached:
                        case DataRowState.Unchanged:
                        default:
                            continue;
                    }
                    cmd.ExecuteNonQuery();
                }
                return true;
            });
        }
        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteModule(ModuleEntity entity)
        {
            return base.GetExecuteBoolean((cmd) => {
                cmd.CommandText = "UUP_FUN_DeleteModule";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
            });
        }

        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ModuleEntity GetSingleModule(ModuleEntity entity)
        {
            return base.SetEntitySingle<ModuleEntity>(entity);
        }
        /// <summary>
        /// 查询模块信息
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectModules(Guid systemID, string moduleName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectModule";
                if (systemID != Guid.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@SystemID", SqlDbType.UniqueIdentifier) { Value = systemID });
                }
                cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar, 32) { Value = moduleName });
            });
        }
        /// <summary>
        /// 查询模块信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public DataTable SelectModulesBySystemCode(string systemCode, string moduleName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectModuleBySystemCode]";
                cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar, 32) { Value = moduleName });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
            });
        }
        /// <summary>
        /// 查询模块信息，包含功能分组
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="moduleName"></param>
        /// <param name="userOrGroupID"></param>
        /// <returns></returns>
        public DataTable SelectModuleFunctionBySystemCodeAndGroup(string systemCode, string moduleName, string userOrGroupID)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectModuleWithGroupBySystemCode]";
                cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar, 50) { Value = moduleName });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 50) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.NVarChar, 50) { Value = userOrGroupID });
            });
        }

        /// <summary>
        /// 获取功能项表
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public DataTable GetFunctionByModuleID(Guid moduleID)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectFunction";
                if (moduleID != Guid.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier) { Value = moduleID });
                }
            });
        }
        /// <summary>
        /// 依据系统编码获取功能项表
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public DataTable GetFunctionBySystemCode(string systemCode)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectFunctionBySystemCode]";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
            });
        }
        /// <summary>
        /// 根据用户ID或用户组ID获取该用户或用户组所拥有的权限功能
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <returns></returns>
        public DataTable GetFunctionByUserOrGroupID(Guid userOrGroupID)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "[UUP_FUN_SelectUserFunction]";
                cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.UniqueIdentifier) { Value = userOrGroupID });
            });
        }

        /// <summary>
        /// 检测模块代码是否已经存在
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleID"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public bool IsExistsModuleCode(Guid systemID, Guid moduleID, string moduleCode)
        {
            object obj = base.GetExecuteScalar((cmd) => {
                cmd.CommandText = "UUP_FUN_IsExistsModule";
                cmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar, 64) { Value = moduleCode });
                cmd.Parameters.Add(new SqlParameter("@SystemID", SqlDbType.UniqueIdentifier) { Value = systemID });
                if (moduleID != Guid.Empty)
                    cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier) { Value = moduleID });
            });
            // 不等于 NULL ，表示该模块代码已经存在
            return obj != null;
        }

        /// <summary>
        /// 更新用户或用户组的模块信息
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <param name="dtUserFunction"></param>
        /// <returns></returns>
        public bool UpdateUserOrGroupFunction(Guid userOrGroupID, DataTable dtUserFunction)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UUP_FUN_DeleteFunctionByUserOrGroupID]";
                cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.UniqueIdentifier) { Value = userOrGroupID });
                cmd.ExecuteNonQuery();
                //
                foreach (DataRow row in dtUserFunction.Rows)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[UUP_FUN_InsertFunction]";
                    cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.UniqueIdentifier) { Value = userOrGroupID });
                    cmd.Parameters.Add(new SqlParameter("@FunctionID", SqlDbType.UniqueIdentifier) { Value = row["FunctionID"] });
                    cmd.ExecuteNonQuery();
                }
                return true;
            });
        }
        /// <summary>
        /// 按组更新权限信息
        /// </summary>
        /// <param name="userOrGroupID"></param>
        /// <param name="dtModuleFunction"></param>
        /// <returns></returns>
        public bool UpdateUserOrGroupFunctionWithGroup(Guid userOrGroupID, DataTable dtModuleFunction)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UUP_FUN_DeleteFunctionByUserOrGroupID]";
                cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.UniqueIdentifier) { Value = userOrGroupID });
                cmd.ExecuteNonQuery();
                //
                foreach (DataRow row in dtModuleFunction.Rows)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[UUP_FUN_UpdateUserFunctionWithGroup]";
                    cmd.Parameters.Add(new SqlParameter("@UserOrGroupID", SqlDbType.UniqueIdentifier) { Value = userOrGroupID });
                    cmd.Parameters.Add(new SqlParameter("@ModuleID", SqlDbType.NVarChar) { Value = row["ModuleID"] });
                    cmd.Parameters.Add(new SqlParameter("@ViewPopedom", SqlDbType.Bit) { Value = row["ViewPopedom"] });
                    cmd.Parameters.Add(new SqlParameter("@OperatePopedom", SqlDbType.Bit) { Value = row["OperatePopedom"] });
                    cmd.ExecuteNonQuery();
                }
                return true;
            });
        }
        #endregion
    }
}