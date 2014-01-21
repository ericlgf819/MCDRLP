using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity;
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IUserService"，也必须更新 Web.config 中对 "IUserService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 查找所有用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllUsers(UserEntity entity);

        /// <summary>
        /// 插入单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        List<object> InsertSingleUser(UserEntity entity);

        /// <summary>
        /// 查找具有审核和复核权限的人
        /// </summary>
        /// <param name="type">0:表示审核权限，1:表示复核权限</param>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectCheckUsers(int type, string systemCode);

        /// <summary>
        /// 查找具有审核和复核权限的人
        /// 0.审核组；1.复核组
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectPendingUsers(string systemCode);

        /// <summary>
        /// 查找单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        UserEntity SelectSingleUser(UserEntity entity);

        /// <summary>
        /// 更新单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        List<object> UpdateSingleUser(UserEntity entity);

        /// <summary>
        /// 查找已删除用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectDeletedUsers(UserEntity entity);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="hostName">当前登录的主机名称</param>
        /// <param name="res">0,表示用户名不存在，1,表示已经被禁用,2,表示被锁定,3,表示密码错误,4,表示登录成功</param>
        /// <param name="leftTimes">返回密码输入剩余机会</param>
        /// <param name="loginhost">返回已登录的主机名</param>
        /// <returns></returns>
        [OperationContract]
        UserEntity UserLogin(string userName, string password,string hostName, out int res, out int leftTimes,out string loginhost);

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        bool UserLoginOut(string userName);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [OperationContract]
        int ChangePassword(Guid id, string oldPassword, string newPassword);

        /// <summary>
        /// 查找用户的待处理任务
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserTask(Guid userID);

        /// <summary>
        /// 用户任务转发
        /// </summary>
        /// <param name="originalUserID">原用户ID</param>
        /// <param name="transmitUserID">新用户ID</param>
        /// <param name="type">0表示删除原用户;1表示禁用原用户</param>
        /// <returns></returns>
        [OperationContract]
        bool TransmitUserTask(Guid originalUserID, Guid transmitUserID, int type);

        /// <summary>
        /// 查找提交给该用户审核的所有用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserPending(Guid userID);

        /// <summary>
        /// 查找用户所有的代理信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserRepresent(Guid userID);

        /// <summary>
        /// 删除用戶
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteSingleUser(Guid userID, int type, Guid operationID);
    }
}