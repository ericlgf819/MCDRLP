using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;

using MCD.UUP.Common;
using MCD.UUP.Entity;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class BaseService : BaseFunction
    {
        /// <summary>
        /// 缓存数据表架构信息
        /// </summary>
        private static Dictionary<string, Dictionary<string, DatabaseColumnStruct>> TableSchemasCache =
            new Dictionary<string, Dictionary<string, DatabaseColumnStruct>>();

        /// <summary>
        /// 获取实体的属性值
        /// </summary>
        private object value = null;

        #region 查询单条数据

        /// <summary>
        /// 构造插入 SQL 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected virtual string CreateSelectSql(string tableName)
        {
            string sql = "SELECT * FROM {0} WHERE {1}";
            //
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(tableName);
            string keyName = string.Empty;
            // 查找主键
            foreach (var item in schema)
            {
                if (item.Value.IsKey)
                {
                    keyName = item.Key;
                    break;
                }
            }
            //
            return string.Format(sql, tableName, keyName + "=@" + keyName);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private T SetEntitySingle<T>(DataTable dtSource) where T : new()
        {
            if (dtSource == null || dtSource.Rows.Count == 0) return default(T);
            //
            T t = new T();
            DataRow row = dtSource.Rows[0];
            foreach (DataColumn col in dtSource.Columns)
            {
                PropertyHandler.SetPropertyValue<T>(ref t, col.ColumnName, row[col.ColumnName]);
            }
            return t;
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual T SetEntitySingle<T>(T t) where T : BaseEntity, new()
        {
            string sql = this.CreateSelectSql(t.TableName);
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(t.TableName);
            //
            DataSet ds = new DataSet();
            using (base.Conn = new SqlConnection(base.ConnectionString))
            {
                using (base.Cmd = new SqlCommand(sql, base.Conn))
                {
                    // 查找主键
                    foreach (var item in schema)
                    {
                        if (item.Value.IsKey)
                        {
                            value = PropertyHandler.GetPropertyValue<T>(t, item.Key);
                            base.Cmd.Parameters.Add(new SqlParameter("@" + item.Key, TypeHandler.GetTypeFromSqlType(item.Value.DbType.Name)) { Value = value });
                            break;
                        }
                    }
                    //
                    base.Adp = new SqlDataAdapter(base.Cmd);
                    base.Conn.Open();
                    base.Adp.Fill(ds, t.TableName);
                    base.Conn.Close();
                }
            }
            return ds.Tables[0].Rows.Count > 0 ? this.SetEntitySingle<T>(ds.Tables[0]) : default(T);
        }
        #endregion

        #region 插入操作

        /// <summary>
        /// 构造插入 SQL 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected virtual string CreateInsertSql(string tableName)
        {
            string sql = "INSERT INTO {0}({1}) VALUES({2})";
            //
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(tableName);
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (var item in schema)
            {
                if (!item.Value.IsIdentity) //Identity的不需要插入
                {
                    columns.Add(item.Key);
                    parameters.Add("@" + item.Key);
                }
            }
            //
            return string.Format(sql, tableName
                , CollectionHandler.Join(",", columns)
                , CollectionHandler.Join(",", parameters));
        }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual int SetEntityInsert<T>(T t) where T : BaseEntity, new()
        {
            string sql = this.CreateInsertSql(t.TableName);
            //
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(t.TableName);
            return base.GetExecuteInt((cmd) =>
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                // 参数
                foreach (var item in schema)
                {
                    if (!item.Value.IsIdentity) //Identity的不需要插入
                    {
                        value = PropertyHandler.GetPropertyValue<T>(t, item.Key);
                        cmd.Parameters.Add(new SqlParameter("@" + item.Key, TypeHandler.GetTypeFromSqlType(item.Value.DbType.Name))
                            {
                                Value = (value == null && item.Value.DbType.Name.ToLower() == "string") ? string.Empty : value,
                                Size = item.Value.Size
                            }
                        );
                    }
                }
            });
        }
        #endregion

        #region 更新操作

        /// <summary>
        /// 构造更新 SQL 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected virtual string CreateUpdateSql(string tableName)
        {
            string sql = "UPDATE {0} SET {1} WHERE {2}={3}";
            //
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(tableName);
            List<string> columns = new List<string>();
            string keyName = string.Empty;
            foreach (var item in schema)
            {
                if (item.Value.IsKey)
                {
                    keyName = item.Key;
                }
                if (!item.Value.IsIdentity) //Identity的不需要插入
                {
                    columns.Add(item.Key + "=@" + item.Key);
                }
            }
            //
            return string.Format(sql, tableName
                , CollectionHandler.Join(",", columns), keyName, "@" + keyName);
        }

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual int SetEntityUpdate<T>(T t) where T : BaseEntity, new()
        {
            string sql = this.CreateUpdateSql(t.TableName);
            // 获取表架构
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(t.TableName);
            return base.GetExecuteInt((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                //
                foreach (var item in schema)
                {
                    if (!item.Value.IsIdentity) //Identity的不需要插入
                    {
                        value = PropertyHandler.GetPropertyValue<T>(t, item.Key);
                        Cmd.Parameters.Add(new SqlParameter("@" + item.Key, TypeHandler.GetTypeFromSqlType(item.Value.DbType.Name))
                            {
                                Value = (value == null && item.Value.DbType.Name.ToLower() == "string") ? string.Empty : value,
                                Size = item.Value.Size
                            }
                        );
                    }
                }
            });
        }
        #endregion

        #region 删除操作

        /// <summary>
        /// 删除单个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual int SetEntityDelete<T>(T t) where T : BaseEntity, new()
        {
            string sql = "DELETE FROM {0} WHERE {1}";
            //
            object value = null;
            string keyName, keyType, tableName;
            keyName = keyType = string.Empty;
            tableName = t.TableName;
            Dictionary<string, DatabaseColumnStruct> schema = this.GetTableSchema(tableName);
            // 查找主键
            foreach (var item in schema)
            {
                if (item.Value.IsKey)
                {
                    keyName = item.Key;
                    keyType = item.Value.DbType.Name;
                    break;
                }
            }
            // 没有主键，不执行操作
            if (keyName == string.Empty) return 0;
            // 构造 Sql 语句
            sql = string.Format(sql, tableName, keyName + "=@" + keyName);
            value = PropertyHandler.GetPropertyValue<T>(t, keyName);
            // 执行 Sql 语句
            return base.GetExecuteInt((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("@" + keyName, TypeHandler.GetTypeFromSqlType(keyType)) { Value = value });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <param name="dbType"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public virtual int SetEntityDelete<T>(string keyName, DbType dbType, object keyValue)
            where T : BaseEntity, new()
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", new T().TableName, keyName + "=@" + keyName);
            //
            return base.GetExecuteInt((cmd) => {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@" + keyName, dbType) { Value = keyValue });
            });
        }
        #endregion

        #region 获取数据表架构

        /// <summary>
        /// 获取数据表架构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataTable SelectTableSchema<T>()
            where T : BaseEntity, new()
        {
            T t = new T();
            string sql = string.Format("SELECT * FROM {0} where 1=0", t.TableName);
            //
            return base.GetExecuteTable((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
            }, t.TableName);
        }

        /// <summary>
        /// 根据表明获取数据表架构
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private Dictionary<string, DatabaseColumnStruct> GetTableSchema(string tableName)
        {
            string sql = string.Format("select * from {0} where 1=0", tableName);
            if (!BaseService.TableSchemasCache.ContainsKey(tableName))
            {
                Dictionary<string, DatabaseColumnStruct> cols = 
                    base.GetExecuteDataReader<Dictionary<string, DatabaseColumnStruct>>(sql, this.GetTableSchema);
                BaseService.TableSchemasCache.Add(tableName, cols);
            }
            //
            return BaseService.TableSchemasCache[tableName];
        }

        /// <summary>
        /// 根据 DbDataReader 对象获取数据表架构
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Dictionary<string, DatabaseColumnStruct> GetTableSchema(SqlDataReader dr)
        {
            Dictionary<string, DatabaseColumnStruct> cols = new Dictionary<string, DatabaseColumnStruct>();
            //
            DataTable dtSchema = dr.GetSchemaTable();
            foreach (DataRow schemaInfo in dtSchema.Rows)
            {
                cols.Add(schemaInfo["ColumnName"].ToString().ToUpper() , new DatabaseColumnStruct() {
                    ColumnIndex = Convert.ToInt32(schemaInfo["ColumnOrdinal"]),
                    DbType = (Type)schemaInfo["DataType"],
                    DbTypeName = schemaInfo["DataTypeName"].ToString(),
                    AllowDBNull = (bool)schemaInfo["AllowDBNull"],
                    IsIdentity = (bool)schemaInfo["IsIdentity"],
                    Name = schemaInfo["ColumnName"].ToString(),
                    Size = (int)schemaInfo["ColumnSize"],
                    IsKey = schemaInfo["IsKey"].Equals(DBNull.Value) ? false : (bool)schemaInfo["IsKey"]
                });
            }
            return cols;
        }
        #endregion

        #region 主从表操作

        /// <summary>
        /// 级联新增
        /// </summary>
        /// <typeparam name="TParent">主表实体</typeparam>
        /// <typeparam name="TChild">子表实体</typeparam>
        /// <param name="t">主表实体对象</param>
        /// <param name="dtChild">子表数据表</param>
        /// <returns></returns>
        protected bool CascadeInsert<TParent, TChild>(TParent t, DataTable dtChild)
            where TParent : BaseEntity, new()
            where TChild : BaseEntity, new()
        {
            using (TransactionScope tran = new TransactionScope())
            {
                // 新增主表数据
                if (this.SetEntityInsert<TParent>(t) == 0)
                {
                    return false;
                }
                // 新增从表数据
                foreach (DataRow row in dtChild.Rows)
                {
                    // 获取实体
                    TChild tChild = LoadEntity.Instance.GetEntityFromRow<TChild>(row);
                    this.SetEntityInsert<TChild>(tChild);
                }
                tran.Complete();
            }
            return true;
        }

        /// <summary>
        /// 级联更新
        /// </summary>
        /// <typeparam name="TParent">主表实体</typeparam>
        /// <typeparam name="TChild">子表实体</typeparam>
        /// <param name="t">主表实体对象</param>
        /// <param name="dtChild">子表数据表</param>
        /// <returns></returns>
        protected bool CascadeUpdate<TParent, TChild>(TParent t, DataTable dtChild)
            where TParent : BaseEntity, new()
            where TChild : BaseEntity, new()
        {
            using (TransactionScope tran = new TransactionScope())
            {
                // 更新主表数据
                if (this.SetEntityUpdate<TParent>(t) == 0)
                {
                    return false;
                }
                // 更新从表数据
                foreach (DataRow row in dtChild.Rows)
                {
                    if (row.RowState == DataRowState.Unchanged) continue;
                    // 获取实体
                    TChild tChild = LoadEntity.Instance.GetEntityFromRow<TChild>(row);
                    if (row.RowState == DataRowState.Added)
                    {// 新增
                        this.SetEntityInsert<TChild>(tChild);
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {// 修改
                        this.SetEntityUpdate<TChild>(tChild);
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {// 删除
                        this.SetEntityDelete<TChild>(dtChild.PrimaryKey[0].ColumnName,
                            TypeHandler.GetTypeFromSqlType(dtChild.PrimaryKey[0].DataType.Name),
                            row[dtChild.PrimaryKey[0].ColumnName, DataRowVersion.Original]);
                    }
                }
                tran.Complete();
            }
            return true;
        }
        #endregion
    }
}