using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

using MCD.Framework.Entity;
using MCD.Framework.DALCommon;

namespace MCD.Framework.SqlDAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDAL<T> where T : BaseEntity, new()
    {
        //Fields
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlDataAdapter adp = null;
        private SqlParameter par = null;

        /// <summary>
        /// Xml 配置文件存放路径
        /// </summary>
        private const string XML_CONIFG_PATH = "XmlConfigPath";
        /// <summary>
        /// 查询数据节点 ID
        /// </summary>
        private const string SELECT_TABLE = "SelectTable";
        /// <summary>
        /// 查询单个实体节点 ID
        /// </summary>
        private const string SELECT_SINGLE = "SelectSingle";
        /// <summary>
        /// 新增实体的节点 ID
        /// </summary>
        private const string INSERT_ENTITY = "InsertEntity";
        /// <summary>
        /// 更新实体的节点 ID
        /// </summary>
        private const string UPDATE_ENTITY = "UpdateEntity";
        /// <summary>
        /// 更新实体的节点 ID
        /// </summary>
        private const string DELETE_ENTITY = "DeleteEntity";
        /// <summary>
        /// 执行存储过程出错
        /// </summary>
        private const string ExecuteProcedure_EXCEPTION = "调用方法 {0},未给 Command 对象指定参数。";
        /// <summary>
        /// 缓存配置文件信息
        /// </summary>
        private static Dictionary<string, XmlConfig> xmlConfigs = new Dictionary<string, XmlConfig>();

        //Properties
        /// <summary>
        /// 获取配置文件存放路径
        /// </summary>
        private string ConfigPath
        {
            get
            {
                if (ConfigurationManager.AppSettings[BaseDAL<T>.XML_CONIFG_PATH] == null)
                {
                    throw new Exception("没有在配置文件中设置 Xml 配置文件存放路径.");
                }
                //
                string configpath = ConfigurationManager.AppSettings[BaseDAL<T>.XML_CONIFG_PATH].ToString();
                return Path.Combine(System.AppDomain.CurrentDomain.RelativeSearchPath, configpath);
            }
        }
        /// <summary>
        /// 从 Xml 配置文件中获取连接字符串
        /// </summary>
        protected virtual string ConnectionString
        {
            get
            {
                T t = new T();
                XmlConfig config = GetXmlConfig(t);
                return config.ConnectionString;
            }
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        public virtual DateTime TestMethod()
        {
            return DateTime.Now;
        }

        /// 过滤SQL字符。
        /// </summary>
        /// <param name="str">要过滤SQL字符的字符串。</param>
        /// <returns>已过滤掉SQL字符的字符串。</returns>
        public string ReplaceSQLChar(string str)
        {
            if (str == String.Empty)
                return String.Empty;
            str = str.Replace("'", "''");
            str = str.Replace("%", "[%]");
            str = str.Replace("_", "[_]");
            str = str.Replace("[", "[[]");
            str = str.Replace("^", "[^]");
            str = str.Replace(";", "；");

            return str;
        }

        /// <summary>
        /// 解压并反序列化为DataSet
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public DataSet DeSerilize(byte[] binaryData)
        {
            // 初始化流，设置读取位置
            MemoryStream mStream = new MemoryStream(binaryData);
            mStream.Seek(0, SeekOrigin.Begin);
            // 解压缩
            DeflateStream unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true);
            // 反序列化得到数据集
            DataSet dsResult = new DataSet();
            dsResult.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter bFormatter = new BinaryFormatter();
            dsResult = (DataSet)bFormatter.Deserialize(unZipStream);

            return dsResult;
        }

        /// <summary>
        /// 把 DataSet 序列化为二进制数组并压缩
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns></returns>
        public byte[] Serilize(DataSet ds)
        {
            // 序列化为二进制
            ds.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            bFormatter.Serialize(mStream, ds);
            byte[] bytes = mStream.ToArray();
            // 压缩 
            MemoryStream oStream = new MemoryStream();
            DeflateStream zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            //返回
            return oStream.ToArray();
        }

        /// <summary>
        /// 获取配置文件属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private XmlConfig GetXmlConfig(T t)
        {
            if (xmlConfigs.Keys.Contains(t.ConfigFileName)) return xmlConfigs[t.ConfigFileName];
            XmlConfig config = new XmlConfig(Path.Combine(ConfigPath, t.ConfigFileName));
            xmlConfigs.Add(t.ConfigFileName, config);
            return config;
        }

        

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="dr">DataReader对象</param>
        /// <returns></returns>
        protected T SetSingleEntity(IDataReader dr)
        {
            T t = new T();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    PropertyHandler.SetPropertyValue<T>(ref t, dr.GetName(i), dr[dr.GetName(i)]);
                }
                break;
            }
            return t;
        }

        protected List<T> SetAllEntity(IDataReader dr)
        {
            List<T> list = new List<T>();
            T t = default(T);
            while (dr.Read())
            {
                t = new T();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    PropertyHandler.SetPropertyValue<T>(ref t, dr.GetName(i), dr[dr.GetName(i)]);
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 设置 Command 对象
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cmd">Command 对象</param>
        /// <param name="procedureID"></param>
        private void SetCommand(T t, DbCommand cmd, ProcedureInfo proc)
        {
            cmd.CommandText = proc.ProcedureName;
            foreach (ParameterInfo parm in proc.Parameters)
            {
                par = new SqlParameter(parm.ParameterName, parm.ParameterType)
                   {
                       Direction = parm.Direction,
                       Value = PropertyHandler.GetPropertyValue<T>(t, parm.PropertyName)
                   };
                if (parm.Size != 0)
                    par.Size = parm.Size;
                cmd.Parameters.Add(par);
            }
        }

        #region IBaseDAL 成员

        /// <summary>
        /// 执行存储过程，返回数据集
        /// 注:使用此方法必须在 Xml 文件中定义 SelectTable 节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataSet GetDataSet(T t)
        {
            return ExecuteProcedureDataSet(t, SELECT_TABLE);
        }

        /// <summary>
        /// 执行存储过程，返回单个实体数据
        /// 注:使用此方法必须在 Xml 文件中定义 SelectSingle 节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetSingleEntity(T t)
        {
            return GetSingleEntity(t, SELECT_SINGLE);
        }

        /// <summary>
        /// 执行存储过程，返回单个实体数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public T GetSingleEntity(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataReader((cmd) =>
            {
                SetCommand(t, cmd, proc);
            }, SetSingleEntity);
        }

        /// <summary>
        /// 执行存储过程，返回单个实体数据
        /// 只需给 Command 对象定义 CommandText 和 Parameters
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public T GetSingleEntity(Action<DbCommand> action)
        {
            return ExecuteProcedureDataReader(action, SetSingleEntity);
        }

        /// <summary>
        /// 执行存储过程，更新单个实体数据
        /// 注:使用此方法必须在 Xml 文件中定义 InsertEntity 节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int InsertSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, INSERT_ENTITY);
        }

        /// <summary>
        /// 使用存储过程更新数据
        /// 注:使用此方法必须在 Xml 文件中定义 UpdateEntity 节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, UPDATE_ENTITY);
        }

        /// <summary>
        /// 使用存储过程删除数据
        /// 注:使用此方法必须在 Xml 文件中定义 DeleteEntity 节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int DeleteSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, DELETE_ENTITY);
        }

        /// <summary>
        /// 执行存储过程，返回是否执行成功
        /// 只需给 Command 对象定义 CommandText 和 Parameters
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ExecuteProcedureBoolean(Action<System.Data.Common.DbCommand> action)
        {
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureInt"));
                    action(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
        }

        /// <summary>
        /// 执行存储过程，返回是否执行成功
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public bool ExecuteProcedureBoolean(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureBoolean((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// 执行存储过程，返回结果影响行数
        /// 只需给 Command 对象定义 CommandText 和 Parameters
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public int ExecuteProcedureInt(Action<System.Data.Common.DbCommand> action)
        {
            int res = 0;
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureInt"));
                    action(cmd);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行存储过程，返回第一行第一列的值
        /// 只需给 Command 对象定义 CommandText 和 Parameters
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public object ExecuteProcedureScalar(Action<System.Data.Common.DbCommand> action)
        {
            object obj;
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureInt"));
                    action(cmd);
                    conn.Open();
                    obj = cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            return obj;
        }

        /// <summary>
        /// 执行存储过程，返回结果影响行数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public int ExecuteProcedureInt(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureInt((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// 执行存储过程，返回数据集
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <param name="tableName">返回的表名称</param>
        /// <returns></returns>
        public DataSet ExecuteProcedureDataSet(Action<System.Data.Common.DbCommand> action, string tableName)
        {
            DataSet ds = new DataSet();
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureDataSet"));
                    action(cmd);
                    
                    adp = new SqlDataAdapter(cmd);
                    conn.Open();
                    adp.Fill(ds, tableName);
                    conn.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行存储过程，返回数据集
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <returns></returns>
        public DataSet ExecuteProcedureDataSet(Action<System.Data.Common.DbCommand> action)
        {
            return ExecuteProcedureDataSet(action, "Table0");
        }

        /// <summary>
        /// 执行存储过程，返回数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public DataSet ExecuteProcedureDataSet(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataSet((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// 执行存储过程，使用 DataReader 的方式读取数据
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <param name="setEntity"></param>
        /// <returns></returns>
        private T ExecuteProcedureDataReader(Action<System.Data.Common.DbCommand> action, Func<IDataReader, T> setEntity)
        {
            IDataReader dr = null;
            T t = default(T);
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (action == null || setEntity == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureDataReader"));
                    action(cmd);
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    t = setEntity(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return t;
        }

        /// <summary>
        /// 执行存储过程，返回 DataReader 对象
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <returns></returns>
        public IDataReader ExecuteProcedureDataReader(Action<System.Data.Common.DbCommand> action)
        {
            IDataReader dr = null;
            conn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand()
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure
            };
            if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureDataReader"));
            action(cmd);
            conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }

        /// <summary>
        /// 执行存储过程，返回 DataReader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="procedureID">Xml中节点的ID</param>
        /// <returns></returns>
        public IDataReader ExecuteProcedureDataReader(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataReader((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// 执行存储过程，返回输出参数的值
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public List<object> ExecuteProcedureParams(T t, string procedureID)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            // 存储过程信息
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureParams((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// 执行存储过程，无返回值
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <returns></returns>
        public void ExecuteProcedure(Action<System.Data.Common.DbCommand> action)
        {
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    action(cmd);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程，无返回值
        /// </summary>
        /// <param name="action">封装一个方法，需给 Command 对象定义 CommandText 和 Parameters，并且在该匿名函数中执行命令</param>
        /// <returns></returns>
        public void ExecuteProcedureNoExecute(Action<System.Data.Common.DbCommand> action)
        {
            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    action(cmd);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程，返回输出参数的值
        /// </summary>
        /// <param name="action">封装一个方法，只需给 Command 对象定义 CommandText 和 Parameters</param>
        /// <returns></returns>
        public List<object> ExecuteProcedureParams(Action<System.Data.Common.DbCommand> action)
        {
            List<object> objs = new List<object>();

            using (conn = new SqlConnection(ConnectionString))
            {
                using (cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // 执行存储过程
                    action(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // 获取返回参数的值
                    foreach (SqlParameter parm in cmd.Parameters)
                    {
                        if ((parm.Direction == ParameterDirection.Output) ||
                                (parm.Direction == ParameterDirection.InputOutput) ||
                                (parm.Direction == ParameterDirection.ReturnValue))
                        {
                            objs.Add(parm.Value);
                        }
                    }// End foreach
                }// End Cmd
            }// End Conn
            return objs;
        }

        /// <summary>
        /// 带事务的方法，执行多个存储过程
        /// </summary>
        /// <param name="action">封装一个方法，需要给 Command 对象定义 CommandText 和 Parameters，并且执行 Command 命令</param>
        /// <returns></returns>
        public bool ExecuteProcedureTran(Action<System.Data.Common.DbCommand> action)
        {
            SqlTransaction tran = null;
            try
            {
                using (conn = new SqlConnection(ConnectionString))
                {
                    using (cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        if (action == null) throw new Exception(string.Format(ExecuteProcedure_EXCEPTION, "ExecuteProcedureDataReader"));
                        conn.Open();
                        tran = conn.BeginTransaction();
                        // 开始事务
                        cmd.Transaction = tran;
                        action(cmd);
                        // 完成事务
                        tran.Commit();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                { // 出错时回滚事务
                    tran.Rollback();
                }
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 执行数据表更新数据库操作
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="insertProcedureID">新增数据存储过程ID</param>
        /// <param name="updateProcedureID">更新数据存储过程ID</param>
        /// <param name="deleteProcedureID">删除数据存储过程ID</param>
        /// <returns></returns>
        public bool ExecuteProcedureInt(DataTable table, string insertProcedureID, string updateProcedureID, string deleteProcedureID)
        {
            T t = default(T);
            XmlConfig config;
            ProcedureInfo proc;

            SqlTransaction tran = null;
            try
            {
                // 配置文件
                config = GetXmlConfig(t);

                using (conn = new SqlConnection(config.ConnectionString))
                {
                    using (cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        tran = conn.BeginTransaction();
                        // 开始事务
                        cmd.Transaction = tran;

                        foreach (DataRow row in table.Rows)
                        {
                            if (row.RowState == DataRowState.Unchanged) continue;
                            if (row.RowState == DataRowState.Deleted)
                            {// 删除
                                // 存储过程信息
                                proc = config.GetProcedure(deleteProcedureID);
                                // 在实体中设置参数的值
                                foreach (ParameterInfo parm in proc.Parameters)
                                {
                                    PropertyHandler.SetPropertyValue<T>(ref t, parm.ParameterName, row[parm.ParameterName, DataRowVersion.Original]);
                                }
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                                // 获取参数的值
                                continue;
                            }
                            // 获取实体
                            t = LoadEntity.GetEntityFromRow<T>(row);

                            if (row.RowState == DataRowState.Added)
                            {// 新增
                                // 存储过程信息
                                proc = config.GetProcedure(insertProcedureID);
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                            }
                            else if (row.RowState == DataRowState.Modified)
                            {// 修改
                                // 存储过程信息
                                proc = config.GetProcedure(updateProcedureID);
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 完成事务
                        tran.Commit();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                { // 出错时回滚事务
                    tran.Rollback();
                }
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 带事务的方法，执行多个存储过程
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedures"></param>
        /// <returns></returns>
        public bool ExecuteProcedureTran(T t, List<string> procedures)
        {
            // 配置文件
            XmlConfig config = GetXmlConfig(t);
            ProcedureInfo proc;

            return ExecuteProcedureTran((cmd) =>
            {
                foreach (string procedureID in procedures)
                {
                    // 存储过程信息
                    proc = config.GetProcedure(procedureID);

                    cmd.Parameters.Clear();
                    SetCommand(t, cmd, proc);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        #endregion

        #region 用户操作轨迹记录

        /// <summary>
        /// 记录用户操作轨迹
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="connectionString">数据库链接字符串</param>
        /// <returns></returns>
        protected virtual bool InsertDataLog(Guid keyValue, string tableName)
        {
            return ExecuteProcedureBoolean((cmd) =>
            {
                cmd.CommandText = "Framework_USP_InsertDataLog";
                cmd.Parameters.Add(new SqlParameter("@KeyValue", SqlDbType.UniqueIdentifier) { Value = keyValue });
                cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 32) { Value = tableName });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) { Value = EnvironmentVariables.UserID });
                cmd.Parameters.Add(new SqlParameter("@EnglishName", SqlDbType.NVarChar, 32) { Value = EnvironmentVariables.EnglishName });
            });
        }
        #endregion
    }
}