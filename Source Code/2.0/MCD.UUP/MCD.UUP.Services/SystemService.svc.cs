using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.UUP.IServices;
using MCD.UUP.Entity;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 注意: 如果更改此处的类名 "SystemService"，也必须更新 Web.config 中对 "SystemService" 的引用。
    /// </summary>
    public class SystemService : BaseService, ISystemService
    {
        #region ISystemService 成员

        /// <summary>
        /// 新增系统信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertSystem(SystemEntity entity)
        {
            return base.SetEntityInsert<SystemEntity>(entity);
        }

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSystem(SystemEntity entity)
        {
            return base.SetEntityUpdate<SystemEntity>(entity);
        }

        /// <summary>
        /// 删除系统信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSystem(SystemEntity entity)
        {
            return base.SetEntityDelete<SystemEntity>(entity);
        }

        /// <summary>
        /// 获取单个系统信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemEntity GetSingleSystem(SystemEntity entity)
        {
            return base.SetEntitySingle<SystemEntity>(entity);
        }

        /// <summary>
        /// 查询系统信息
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public DataTable SelectSystems(string systemName)
        {
            return base.GetExecuteTable((cmd) => {
                cmd.CommandText = "UUP_FUN_SelectSystem";
                cmd.Parameters.Add(new SqlParameter("@SystemName", SqlDbType.NVarChar, 32) { Value = systemName });
            });
        }

        /// <summary>
        /// 检测数据库链接是否正确
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ConnectionTest(SystemEntity entity)
        {
            string sql = "Select Top 1 {0},{1} from {2} {3}";
            string connstr = "Data Source={0};Initial Catalog={1};User id={2};Password={3}";
            try
            {
                // 构造检查链接的 Sql 语句
                sql = string.Format(sql, entity.AccountField,
                    entity.DisplayField,
                    entity.DBTable,
                    string.IsNullOrEmpty(entity.Filter) ? string.Empty : " where " + entity.Filter);
                using (base.Conn = new SqlConnection(string.Format(connstr, entity.DBSource, entity.DBName, entity.DBAccount, entity.DBPassword)))
                {
                    // 链接成功
                    base.Conn.Open();
                    using (base.Cmd = new SqlCommand(sql, base.Conn))
                    {
                        base.Cmd.ExecuteScalar();
                    }
                    base.Conn.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}