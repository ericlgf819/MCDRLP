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
    public interface IGroupService
    {
        /// <summary>
        /// 依据系统编码查询用户组信息
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <param name="groupName">用户组名称</param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectGroupsBySystemCode(string systemCode, string groupName);

        /// <summary>
        /// 检测用户组名称是否已经存在
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <param name="groupName">用户组名称</param>
        /// <param name="groupID">系统ID</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsUserGroup(string systemCode, string groupName, Guid groupID);

        /// <summary>
        /// 新增用户组信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableGroupUser"></param>
        /// <returns></returns>
        [OperationContract]
        bool InsertGroups(GroupEntity entity, DataTable tableGroupUser);

        /// <summary>
        /// 删除用户组信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteGroups(GroupEntity entity);

        /// <summary>
        /// 更新用户组信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableGroupUser"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateGroups(GroupEntity entity, DataTable tableGroupUser);

        /// <summary>
        /// 根据用户组ID获取用户信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetUsersByGroupID(Guid groupID, string systemCode);
    }
}