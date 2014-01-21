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
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 依据系统编码查询用户信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable SelectUsersBySystemCode(string systemCode, string userName);

        /// <summary>
        /// 依据系统编码开始同步用户
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <returns></returns>
        [OperationContract]
        bool PhysicsUsers(string systemCode);
    }
}