using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.UUP.Entity;
using MCD.UUP.IServices;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "GroupService"，也必须更新 Web.config 中对 "GroupService" 的引用。 
    /// </summary>
    public class GroupService : BaseService, IGroupService
    {
        /// <summary>
        /// 依据系统编码查询用户组信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectGroupsBySystemCode(string systemCode, string groupName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectGroup";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, 32) { Value = groupName });
            });
        }

        /// <summary>
        /// 检测用户组名称是否已经存在
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <param name="groupName">用户组名称</param>
        /// <param name="groupID">系统ID</param>
        /// <returns></returns>
        public bool IsExistsUserGroup(string systemCode, string groupName, Guid groupID)
        {
            object obj = base.GetExecuteScalar((cmd) => {
                cmd.CommandText = "[UUP_FUN_IsExistsGroup]";
                cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, 32) { Value = groupName });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = groupID });
            });
            return obj != null;
        }

        /// <summary>
        /// 新增用户组信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableGroupUser"></param>
        /// <returns></returns>
        public bool InsertGroups(GroupEntity entity, DataTable tableGroupUser)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandText = "UUP_FUN_InsertGroup";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = entity.SystemCode });
                cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, 32) { Value = entity.GroupName });
                cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 512) { Value = entity.Remark });
                cmd.Parameters.Add(new SqlParameter("@IsEnable", SqlDbType.Bit) { Value = entity.IsEnable });
                if (cmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
                //
                foreach (DataRow row in tableGroupUser.Rows)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[UUP_FUN_InsertGroupUser]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                    cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        return false;
                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 删除用户组信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteGroups(GroupEntity entity)
        {
            return base.GetExecuteBoolean((cmd) => {
                cmd.CommandText = "[UUP_FUN_DeleteGroup]";
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
            });
        }

        /// <summary>
        /// 更新用户组信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableGroupUser"></param>
        /// <returns></returns>
        public bool UpdateGroups(GroupEntity entity, System.Data.DataTable tableGroupUser)
        {
            return base.GetExecuteTran((cmd) => {
                cmd.CommandText = "UUP_FUN_UpdateGroup";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = entity.SystemCode });
                cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, 32) { Value = entity.GroupName });
                cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 512) { Value = entity.Remark });
                cmd.Parameters.Add(new SqlParameter("@IsEnable", SqlDbType.Bit) { Value = entity.IsEnable });
                if (cmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
                // 删除所有行
                cmd.CommandText = "[UUP_FUN_DeleteAllGroupUser]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                cmd.ExecuteNonQuery();
                // 添加
                foreach (DataRow row in tableGroupUser.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {// 新增
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[UUP_FUN_InsertGroupUser]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = entity.ID });
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public DataSet GetUsersByGroupID(Guid groupID, string systemCode)
        {
            return base.GetExecuteDataSet((cmd) =>
            {
                cmd.CommandText = "UUP_FUN_SelectGroupUsers";
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) { Value = groupID });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
            });
        }
    }
}