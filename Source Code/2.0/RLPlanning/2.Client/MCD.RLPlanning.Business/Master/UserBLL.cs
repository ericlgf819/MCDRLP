using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class UserBLL : BaseBLL<IUserService>
    {
        /// <summary>
        /// 查找单个用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserEntity SelectSingleUser(Guid userID)
        {
            UserEntity entity = new UserEntity() { ID = userID };
            //
            return base.WCFService.SelectSingleUser(entity);
        }
        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllUsers(UserEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectAllUsers(entity));
        }
        /// <summary>
        /// 根据用户组查找该组所有用户
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public DataSet SelectGroupUsers(Guid groupID)
        {
            UserEntity entity = new UserEntity() {
                UserName = string.Empty,
                EnglishName = string.Empty,
                DeptCode = string.Empty,
                Status = 1,
                GroupID = groupID,
                CheckUserID = Guid.Empty
            };
            return this.SelectAllUsers(entity);
        }

        /// <summary>
        /// 插入单个用户
        /// 0表示用户名已经存在，1表示插入成功，2表示插入失败
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string InsertSingleUser(UserEntity entity)
        {
            entity.Password = base.GetMd5Value(entity.UserName);
            //
            List<object> res = base.WCFService.InsertSingleUser(entity);
            if (res.Count > 0)
            {
                return res[0].ToString();
            }
            else
            {
                return "2";
            }
        }
        /// <summary>
        /// 更新单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleUser(UserEntity entity)
        {
            entity.Password = base.GetMd5Value(entity.UserName);
            //
            List<object> res = base.WCFService.UpdateSingleUser(entity);
            if (res.Count > 0)
            {
                return Convert.ToInt16(res[0]);
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// 查找已删除用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectDeletedUsers(UserEntity entity)
        {
            return base.DeSerilize(base.WCFService.SelectDeletedUsers(entity));
        }

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
        public UserEntity UserLogin(string userName, string password, string hostName, out int res, out int leftTimes, out string loginhost)
        {
            try
            {
                password = base.GetMd5Value(password);
                //
                return base.WCFService.UserLogin(userName, password, hostName, out res, out leftTimes, out loginhost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改密码
        /// 0 表示旧密码错误;1 表示新密码最近被使用过;
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public int ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            oldPassword = base.GetMd5Value(oldPassword);
            newPassword = base.GetMd5Value(newPassword);
            //
            return base.WCFService.ChangePassword(id, oldPassword, newPassword);
        }

        /// <summary>
        /// 查找用户的待处理任务
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet SelectUserTask(Guid userID)
        {
            return base.DeSerilize(base.WCFService.SelectUserTask(userID));
        }

        /// <summary>
        /// 查找提交给该用户审核的所有用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet SelectUserPending(Guid userID)
        {
            return base.DeSerilize(base.WCFService.SelectUserPending(userID));
        }

        /// <summary>
        /// 查找用户所有的代理信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet SelectUserRepresent(Guid userID)
        {
            return base.DeSerilize(base.WCFService.SelectUserRepresent(userID));
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserLoginOut(string userName)
        {
            return base.WCFService.UserLoginOut(userName);
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
            return base.WCFService.TransmitUserTask(originalUserID, transmitUserID, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <param name="operationID"></param>
        /// <returns></returns>
        public bool DeleteSingleUser(Guid userID, int type, Guid operationID)
        {
            return base.WCFService.DeleteSingleUser(userID, type, operationID);
        }
    }
}