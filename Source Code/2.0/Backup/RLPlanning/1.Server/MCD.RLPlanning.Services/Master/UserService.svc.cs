using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : BaseDAL<UserEntity>, IUserService
    {
        #region IUserService 成员

        /// <summary>
        /// 查找单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UserEntity SelectSingleUser(UserEntity entity)
        {
            return base.GetSingleEntity(entity);
        }
        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllUsers(UserEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }

        /// <summary>
        /// 插入单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<object> InsertSingleUser(UserEntity entity)
        {
            return base.ExecuteProcedureParams(entity, "InsertEntity");
        }
        /// <summary>
        /// 更新单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<object> UpdateSingleUser(UserEntity entity)
        {
            return base.ExecuteProcedureParams(entity, "UpdateEntity");
        }
        /// <summary>
        /// 删除用戶
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <param name="operationID"></param>
        /// <returns></returns>
        public bool DeleteSingleUser(Guid userID, int type, Guid operationID)
        {
            return base.ExecuteProcedureBoolean((cmd) =>
            {
                cmd.CommandText = "SRLS_USP_Master_DeleteUser";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type });
                cmd.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.UniqueIdentifier, 36) { Value = operationID });
            });
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="hostName">当前登录的主机名称</param>
        /// <param name="res">0表示用户名不存在；1表示已经被禁用；2表示被锁定；3表示密码错误；4表示登录成功；
        ///                     5表示该用户已经在其他机器登录，并且只能在该机器正常登录；6RL1.0系统用户</param>
        /// <param name="leftTimes">返回密码输入剩余机会</param>
        /// <param name="loginhost">返回已登录的主机名</param>
        /// <returns></returns>
        public UserEntity UserLogin(string userName, string password, string hostName, out int res, out int leftTimes, out string loginhost)
        {
            UserEntity user = new UserEntity();
            //
            int parmValue1 = 0, parmValue2 = 0;
            string parmValue3 = string.Empty;
            List<Object> parms = base.ExecuteProcedureParams((cmd) => {
                cmd.CommandText = "SRLS_USP_System_UserLoginIn";
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 32) { Value = password });
                cmd.Parameters.Add(new SqlParameter("@HostName", SqlDbType.NVarChar, 32) { Value = hostName });
                cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@LeftTimes", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@LoginHost", SqlDbType.NVarChar, 32) { Direction = ParameterDirection.Output });
            });
            if (parms.Count == 3) //返回值
            {
                parmValue1 = Convert.ToInt16(parms[0]);
                parmValue2 = Convert.ToInt16(parms[1]);
                parmValue3 = parms[2] + string.Empty;
                if (parmValue1 == 4)
                {
                    user = base.GetSingleEntity((cmd) => {
                        cmd.CommandText = "[SRLS_USP_Master_SelectSingleUser]";
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
                        cmd.Parameters.Add(new SqlParameter("@FromSRLS", SqlDbType.Bit) { Value = false });
                    });
                }
            }
            res = parmValue1;
            leftTimes = parmValue2;
            loginhost = parmValue3;
            //
            return user;
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserLoginOut(string userName)
        {
            return base.ExecuteProcedureBoolean((cmd) => {
                cmd.CommandText = "SRLS_USP_System_UserLoginOut";
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
            });
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public int ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            int parmValue1 = 0;
            base.ExecuteProcedure((cmd) => {
                cmd.CommandText = "SRLS_USP_System_ChangePassword";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 36) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@OldPassword", SqlDbType.NVarChar, 32) { Value = oldPassword });
                cmd.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.NVarChar, 32) { Value = newPassword });
                cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                parmValue1 = Convert.ToInt16(cmd.Parameters["@Res"].Value);
            });
            return parmValue1;
        }

        /// <summary>
        /// 查找用户的待处理任务
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public byte[] SelectUserTask(Guid userID)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "SRLS_USP_Master_SelectUserTask";
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            }));
        }
        /// <summary>
        /// 查找已删除用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectDeletedUsers(UserEntity entity)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_Master_SelectDeletedUsers";
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 16) { Value = entity.UserName });
                cmd.Parameters.Add(new SqlParameter("@EnglishName", SqlDbType.VarChar, 16) { Value = entity.EnglishName });
                cmd.Parameters.Add(new SqlParameter("@DeptCode", SqlDbType.VarChar, 16) { Value = entity.DeptCode });
                cmd.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, 36) { Value = entity.GroupID });
            }));
        }


        /// <summary>
        /// 查找具有审核和复核权限的人
        /// </summary>
        /// <param name="type">0:表示审核权限，1:表示复核权限</param>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public byte[] SelectCheckUsers(int type, string systemCode)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "SRLS_USP_Master_SelectCheckUser";
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = type });
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.VarChar, 16) { Value = systemCode });
            }));
        }
        /// <summary>
        /// 查找具有审核和复核权限的人
        /// 0.审核组；1.复核组
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public byte[] SelectPendingUsers(string systemCode)
        {
            DataSet ds = base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "SRLS_USP_Master_SelectPendingUsers";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.VarChar, 16) { Value = systemCode });
            });
            ds.Tables[0].TableName = "CheckerUsers";
            ds.Tables[1].TableName = "ReviewUsers";
            //
            return base.Serilize(ds);
        }
        /// <summary>
        /// 用户任务转发
        /// </summary>
        /// <param name="originalUserID">原用户ID</param>
        /// <param name="transmitUserID">新用户ID</param>
        /// <param name="type">0表示删除原用户;1表示禁用原用户</param>
        /// <returns></returns>
        public bool TransmitUserTask(Guid originalUserID, Guid transmitUserID, int type)
        {
            return base.ExecuteProcedureBoolean((cmd) => {
                cmd.CommandText = "SRLS_USP_Master_TransmitUserTask";
                cmd.Parameters.Add(new SqlParameter("@OriginalUserID", SqlDbType.UniqueIdentifier, 36) { Value = originalUserID });
                cmd.Parameters.Add(new SqlParameter("@TransmitUserID", SqlDbType.UniqueIdentifier, 36) { Value = transmitUserID });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type });
            });
        }
        /// <summary>
        /// 查找提交给该用户审核的所有用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public byte[] SelectUserPending(Guid userID)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_Master_SelectUserPending";
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            }));
        }
        /// <summary>
        /// 查找用户所有的代理信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public byte[] SelectUserRepresent(Guid userID)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "SRLS_USP_Master_SelectUserRepresent";
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 36) { Value = userID });
            }));
        }
        #endregion
    }
}