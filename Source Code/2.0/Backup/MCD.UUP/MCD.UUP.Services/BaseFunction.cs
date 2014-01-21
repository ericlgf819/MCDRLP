using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace MCD.UUP.Services
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class BaseFunction
    {
        #region 数据库链接字符串

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected virtual string DB_CONNECTION
        {
            get { return "DBConnection"; }
        }

        private string connectionString = string.Empty;
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        protected virtual string ConnectionString
        {
            get
            {
                if (this.connectionString.Equals(string.Empty))
                {
                    this.connectionString = ConfigurationManager.ConnectionStrings[this.DB_CONNECTION].ConnectionString;
                }
                return this.connectionString;
            }
        }
        #endregion

        #region 数据库访问对象

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        protected SqlConnection Conn = null;
        /// <summary>
        /// Command 对象
        /// </summary>
        protected SqlCommand Cmd = null;
        /// <summary>
        /// Adapter 对象
        /// </summary>
        protected SqlDataAdapter Adp = null;
        #endregion

        #region 执行 Sql 语句操作

        /// <summary>
        /// 执行Sql语句，返回 DataTable 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected DataTable GetExecuteTable(Action<SqlCommand> action, string tableName)
        {
            DataTable dt = null;
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                }) {
                    // 给 Cmd 对象赋值
                    action(Cmd);
                    //
                    DataSet ds = new DataSet();
                    //
                    this.Adp = new SqlDataAdapter(this.Cmd);
                    this.Conn.Open();
                    this.Adp.Fill(ds, tableName);
                    this.Conn.Close();
                    //
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        /// <summary>
        /// 执行Sql语句，返回 DataTable 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected DataTable GetExecuteTable(Action<SqlCommand> action)
        {
            return this.GetExecuteTable(action, "Table0");
        }

        /// <summary>
        /// 执行 Sql 语句，返回 DataSet
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected DataSet GetExecuteDataSet(Action<SqlCommand> action)
        {
            DataSet ds = new DataSet();
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                }) {
                    // 给 Cmd 对象赋值
                    action(this.Cmd);
                    //
                    this.Adp = new SqlDataAdapter(this.Cmd);
                    this.Conn.Open();
                    this.Adp.Fill(ds);
                    this.Conn.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行 Sql 语句，返回单值
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected object GetExecuteScalar(Action<SqlCommand> action)
        {
            object res = null;
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                }) {
                    // 给 Cmd 对象赋值
                    action(this.Cmd);
                    //
                    this.Conn.Open();
                    res = Cmd.ExecuteScalar();
                    this.Conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行Sql语句，返回执行结果 True/False
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected bool GetExecuteBoolean(Action<SqlCommand> action)
        {
            bool res = false;
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // 给 Cmd 对象赋值
                    action(this.Cmd);
                    //
                    this.Conn.Open();
                    res = this.Cmd.ExecuteNonQuery() > 0;
                    this.Conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行Sql语句，返回影响的行数
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected int GetExecuteInt(Action<SqlCommand> action)
        {
            int res = 0;
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // 给 Cmd 对象赋值
                    action(this.Cmd);
                    //
                    this.Conn.Open();
                    res = this.Cmd.ExecuteNonQuery();
                    this.Conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行存储过程，返回输出参数的值
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Dictionary<string, object> GetExecuteOutputParamValue(Action<SqlCommand> action)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand()
                {
                    Connection = this.Conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // 给 Cmd 对象赋值
                    action(this.Cmd);
                    //
                    this.Conn.Open();
                    this.Cmd.ExecuteNonQuery();
                    this.Conn.Close();
                    // 获取参数的值
                    for (int i = 0, j = this.Cmd.Parameters.Count; i < j; i++)
                    {
                        if (this.Cmd.Parameters[i].Direction == ParameterDirection.Output)
                        {
                            res.Add(this.Cmd.Parameters[i].ParameterName, this.Cmd.Parameters[i].Value);
                        }
                    }
                }
            }
            return res;
        }
        #endregion

        #region 获取DataReader对象

        /// <summary>
        /// 获取 DataReader 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T GetExecuteDataReader<T>(string sql, Func<SqlDataReader, T> func)
            where T : new()
        {
            T t = default(T);
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand(sql, this.Conn))
                {
                    this.Conn.Open();
                    //
                    SqlDataReader reader = this.Cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    if (func == null) return default(T);
                    t = func(reader);
                    reader.Close();
                    //
                    this.Conn.Close();
                }
            }
            return t;
        }

        /// <summary>
        /// 带事务的更新方法
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected bool GetExecuteTran(Func<SqlCommand, bool> func)
        {
            bool res = false;
            SqlTransaction tran = null;
            try
            {
                using (this.Conn = new SqlConnection(this.ConnectionString))
                {

                    using (this.Cmd = new SqlCommand())
                    {
                        this.Cmd.Connection = this.Conn;
                        this.Conn.Open();
                        using (tran = this.Conn.BeginTransaction())
                        {
                            this.Cmd.Transaction = tran;
                            res = func(Cmd);
                            if (res)
                            {
                                tran.Commit();
                            }
                            else
                            {
                                tran.Rollback();
                            }
                        }
                        this.Conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                this.InsertLog("执行带事务的方法出错", ex.Message, ex.ToString());
            }
            return res;
        }
        #endregion

        #region 写入日志

        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <param name="logMessage"></param>
        /// <returns></returns>
        public virtual bool InsertLog(string logType, string logTitle, string logMessage)
        {
            using (this.Conn = new SqlConnection(this.ConnectionString))
            {
                using (this.Cmd = new SqlCommand())
                {
                    this.Cmd.Connection = this.Conn;
                    this.Cmd.CommandType = CommandType.StoredProcedure;
                    this.Cmd.CommandText = "UUP_FUN_InsertLog";
                    this.Cmd.Parameters.Add(new SqlParameter("@LogType", SqlDbType.NVarChar, 32) { Value = logType });
                    this.Cmd.Parameters.Add(new SqlParameter("@LogTitle", SqlDbType.NVarChar, 256) { Value = logTitle });
                    this.Cmd.Parameters.Add(new SqlParameter("@LogMessage", SqlDbType.NVarChar, 2048) { Value = logMessage });
                    //
                    this.Conn.Open();
                    this.Cmd.ExecuteNonQuery();
                    this.Conn.Close();
                }
            }
            return true;
        }
        #endregion
    }
}