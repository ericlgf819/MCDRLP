using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using MCD.UUP.Common;
using MCD.UUP.IServices;
using MCD.UUP.Entity;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "UserService"，也必须更新 Web.config 中对 "UserService" 的引用。
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        #region IUserService 成员

        /// <summary>
        /// 依据系统编码查询用户信息
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectUsersBySystemCode(string systemCode, string userName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectUsers";
                cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 32) { Value = userName });
            });
        }

        /// <summary>
        /// 依据系统编码开始同步用户
        /// 1.获取数据库访问链接。
        /// 2.获取当前系统用户信息，检测每个用户在应用系统中是否存在，不存在则删除
        /// 3.获取应用系统用户信息，检测每个用户在权限系统是否存在，不存在则添加，存在则更新
        /// </summary>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public bool PhysicsUsers(string systemCode)
        {
            try
            {
                // 获取当前的系统信息
                DataTable dtSystem = base.GetExecuteTable((cmd) => {
                    cmd.CommandText = "UUP_FUN_SelectSystemByCode";
                    cmd.Parameters.Add(new SqlParameter("@SystemCode", SqlDbType.NVarChar, 32) { Value = systemCode });
                });
                if (dtSystem == null || dtSystem.Rows.Count == 0) return false; // 获取失败返回 FALSE
                //
                SystemEntity system = LoadEntity.Instance.GetEntityFromRow<SystemEntity>(dtSystem.Rows[0]);
                // 获取统一权限系统中的用户信息
                DataTable dtUsersFromUUP = this.SelectUsersBySystemCode(systemCode, string.Empty);
                object o = new object();
                lock (o)
                {
                    // 获取应用系统中的用户信息
                    using (base.Conn = new SqlConnection(
                        string.Format("Data Source={0};Initial Catalog={1};User id={2};Password={3}",
                            system.DBSource,
                            system.DBName,
                            system.DBAccount,
                            system.DBPassword)))
                    {
                        using (base.Cmd = new SqlCommand(string.Format("select {0},{1} from {2} where {3}",
                            system.AccountField, system.DisplayField, system.DBTable, system.Filter.Trim() == string.Empty ? "1=1" : system.Filter), base.Conn))
                        {
                            base.Adp = new SqlDataAdapter(base.Cmd);
                            DataSet ds = new DataSet();
                            base.Conn.Open();
                            base.Adp.Fill(ds, system.TableName);
                            base.Conn.Close();
                            // 遍历应用系统中的每个用户数据，检测在统一权限系统中是否存在，如果不存在，则新增
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                // 更新用户数据
                                base.GetExecuteInt((cmd) => {
                                    cmd.CommandText = "UUP_FUN_PhysicsUsers";
                                    cmd.Parameters.Add(new SqlParameter("@SystemID", SqlDbType.UniqueIdentifier) { Value = system.ID });
                                    cmd.Parameters.Add(new SqlParameter("@UserAccount", SqlDbType.NVarChar, 32) { Value = row[0] });
                                    cmd.Parameters.Add(new SqlParameter("@DisplayName", SqlDbType.NVarChar, 32) { Value = row[1] });
                                });
                            }
                            // 删除多余用户
                            foreach (DataRow row in dtUsersFromUUP.Rows)
                            {
                                if (ds.Tables[0].Select(system.AccountField + "='" + row["UserAccount"].ToString() + "'").Count() == 0)
                                {
                                    // 更新用户数据
                                    base.GetExecuteInt((cmd) => {
                                        cmd.CommandText = "UUP_FUN_DeleteUsers";
                                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = row["ID"] });
                                    });
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                base.InsertLog("同步用户数据失败", ex.Message, ex.ToString());
                return false;
            }
        }
        #endregion
    }
}