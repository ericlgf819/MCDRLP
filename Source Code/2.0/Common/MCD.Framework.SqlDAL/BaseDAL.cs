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
        /// Xml �����ļ����·��
        /// </summary>
        private const string XML_CONIFG_PATH = "XmlConfigPath";
        /// <summary>
        /// ��ѯ���ݽڵ� ID
        /// </summary>
        private const string SELECT_TABLE = "SelectTable";
        /// <summary>
        /// ��ѯ����ʵ��ڵ� ID
        /// </summary>
        private const string SELECT_SINGLE = "SelectSingle";
        /// <summary>
        /// ����ʵ��Ľڵ� ID
        /// </summary>
        private const string INSERT_ENTITY = "InsertEntity";
        /// <summary>
        /// ����ʵ��Ľڵ� ID
        /// </summary>
        private const string UPDATE_ENTITY = "UpdateEntity";
        /// <summary>
        /// ����ʵ��Ľڵ� ID
        /// </summary>
        private const string DELETE_ENTITY = "DeleteEntity";
        /// <summary>
        /// ִ�д洢���̳���
        /// </summary>
        private const string ExecuteProcedure_EXCEPTION = "���÷��� {0},δ�� Command ����ָ��������";
        /// <summary>
        /// ���������ļ���Ϣ
        /// </summary>
        private static Dictionary<string, XmlConfig> xmlConfigs = new Dictionary<string, XmlConfig>();

        //Properties
        /// <summary>
        /// ��ȡ�����ļ����·��
        /// </summary>
        private string ConfigPath
        {
            get
            {
                if (ConfigurationManager.AppSettings[BaseDAL<T>.XML_CONIFG_PATH] == null)
                {
                    throw new Exception("û���������ļ������� Xml �����ļ����·��.");
                }
                //
                string configpath = ConfigurationManager.AppSettings[BaseDAL<T>.XML_CONIFG_PATH].ToString();
                return Path.Combine(System.AppDomain.CurrentDomain.RelativeSearchPath, configpath);
            }
        }
        /// <summary>
        /// �� Xml �����ļ��л�ȡ�����ַ���
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
        /// ���Է���
        /// </summary>
        public virtual DateTime TestMethod()
        {
            return DateTime.Now;
        }

        /// ����SQL�ַ���
        /// </summary>
        /// <param name="str">Ҫ����SQL�ַ����ַ�����</param>
        /// <returns>�ѹ��˵�SQL�ַ����ַ�����</returns>
        public string ReplaceSQLChar(string str)
        {
            if (str == String.Empty)
                return String.Empty;
            str = str.Replace("'", "''");
            str = str.Replace("%", "[%]");
            str = str.Replace("_", "[_]");
            str = str.Replace("[", "[[]");
            str = str.Replace("^", "[^]");
            str = str.Replace(";", "��");

            return str;
        }

        /// <summary>
        /// ��ѹ�������л�ΪDataSet
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public DataSet DeSerilize(byte[] binaryData)
        {
            // ��ʼ���������ö�ȡλ��
            MemoryStream mStream = new MemoryStream(binaryData);
            mStream.Seek(0, SeekOrigin.Begin);
            // ��ѹ��
            DeflateStream unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true);
            // �����л��õ����ݼ�
            DataSet dsResult = new DataSet();
            dsResult.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter bFormatter = new BinaryFormatter();
            dsResult = (DataSet)bFormatter.Deserialize(unZipStream);

            return dsResult;
        }

        /// <summary>
        /// �� DataSet ���л�Ϊ���������鲢ѹ��
        /// </summary>
        /// <param name="ds">DataSet����</param>
        /// <returns></returns>
        public byte[] Serilize(DataSet ds)
        {
            // ���л�Ϊ������
            ds.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            bFormatter.Serialize(mStream, ds);
            byte[] bytes = mStream.ToArray();
            // ѹ�� 
            MemoryStream oStream = new MemoryStream();
            DeflateStream zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            //����
            return oStream.ToArray();
        }

        /// <summary>
        /// ��ȡ�����ļ�����
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
        /// ����ʵ��
        /// </summary>
        /// <param name="dr">DataReader����</param>
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
        /// ���� Command ����
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cmd">Command ����</param>
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

        #region IBaseDAL ��Ա

        /// <summary>
        /// ִ�д洢���̣��������ݼ�
        /// ע:ʹ�ô˷��������� Xml �ļ��ж��� SelectTable �ڵ�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataSet GetDataSet(T t)
        {
            return ExecuteProcedureDataSet(t, SELECT_TABLE);
        }

        /// <summary>
        /// ִ�д洢���̣����ص���ʵ������
        /// ע:ʹ�ô˷��������� Xml �ļ��ж��� SelectSingle �ڵ�
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetSingleEntity(T t)
        {
            return GetSingleEntity(t, SELECT_SINGLE);
        }

        /// <summary>
        /// ִ�д洢���̣����ص���ʵ������
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public T GetSingleEntity(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataReader((cmd) =>
            {
                SetCommand(t, cmd, proc);
            }, SetSingleEntity);
        }

        /// <summary>
        /// ִ�д洢���̣����ص���ʵ������
        /// ֻ��� Command ������ CommandText �� Parameters
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public T GetSingleEntity(Action<DbCommand> action)
        {
            return ExecuteProcedureDataReader(action, SetSingleEntity);
        }

        /// <summary>
        /// ִ�д洢���̣����µ���ʵ������
        /// ע:ʹ�ô˷��������� Xml �ļ��ж��� InsertEntity �ڵ�
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int InsertSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, INSERT_ENTITY);
        }

        /// <summary>
        /// ʹ�ô洢���̸�������
        /// ע:ʹ�ô˷��������� Xml �ļ��ж��� UpdateEntity �ڵ�
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, UPDATE_ENTITY);
        }

        /// <summary>
        /// ʹ�ô洢����ɾ������
        /// ע:ʹ�ô˷��������� Xml �ļ��ж��� DeleteEntity �ڵ�
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int DeleteSingleEntity(T t)
        {
            return ExecuteProcedureInt(t, DELETE_ENTITY);
        }

        /// <summary>
        /// ִ�д洢���̣������Ƿ�ִ�гɹ�
        /// ֻ��� Command ������ CommandText �� Parameters
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
        /// ִ�д洢���̣������Ƿ�ִ�гɹ�
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public bool ExecuteProcedureBoolean(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureBoolean((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// ִ�д洢���̣����ؽ��Ӱ������
        /// ֻ��� Command ������ CommandText �� Parameters
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
        /// ִ�д洢���̣����ص�һ�е�һ�е�ֵ
        /// ֻ��� Command ������ CommandText �� Parameters
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
        /// ִ�д洢���̣����ؽ��Ӱ������
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public int ExecuteProcedureInt(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureInt((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// ִ�д洢���̣��������ݼ�
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
        /// <param name="tableName">���صı�����</param>
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
        /// ִ�д洢���̣��������ݼ�
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
        /// <returns></returns>
        public DataSet ExecuteProcedureDataSet(Action<System.Data.Common.DbCommand> action)
        {
            return ExecuteProcedureDataSet(action, "Table0");
        }

        /// <summary>
        /// ִ�д洢���̣��������ݼ�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public DataSet ExecuteProcedureDataSet(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataSet((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// ִ�д洢���̣�ʹ�� DataReader �ķ�ʽ��ȡ����
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
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
        /// ִ�д洢���̣����� DataReader ����
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
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
        /// ִ�д洢���̣����� DataReader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="procedureID">Xml�нڵ��ID</param>
        /// <returns></returns>
        public IDataReader ExecuteProcedureDataReader(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureDataReader((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// ִ�д洢���̣��������������ֵ
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public List<object> ExecuteProcedureParams(T t, string procedureID)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            // �洢������Ϣ
            ProcedureInfo proc = config.GetProcedure(procedureID);

            return ExecuteProcedureParams((cmd) =>
            {
                SetCommand(t, cmd, proc);
            });
        }

        /// <summary>
        /// ִ�д洢���̣��޷���ֵ
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
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
        /// ִ�д洢���̣��޷���ֵ
        /// </summary>
        /// <param name="action">��װһ����������� Command ������ CommandText �� Parameters�������ڸ�����������ִ������</param>
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
        /// ִ�д洢���̣��������������ֵ
        /// </summary>
        /// <param name="action">��װһ��������ֻ��� Command ������ CommandText �� Parameters</param>
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
                    // ִ�д洢����
                    action(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // ��ȡ���ز�����ֵ
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
        /// ������ķ�����ִ�ж���洢����
        /// </summary>
        /// <param name="action">��װһ����������Ҫ�� Command ������ CommandText �� Parameters������ִ�� Command ����</param>
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
                        // ��ʼ����
                        cmd.Transaction = tran;
                        action(cmd);
                        // �������
                        tran.Commit();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                { // ����ʱ�ع�����
                    tran.Rollback();
                }
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// ִ�����ݱ�������ݿ����
        /// </summary>
        /// <param name="table">���ݱ�</param>
        /// <param name="insertProcedureID">�������ݴ洢����ID</param>
        /// <param name="updateProcedureID">�������ݴ洢����ID</param>
        /// <param name="deleteProcedureID">ɾ�����ݴ洢����ID</param>
        /// <returns></returns>
        public bool ExecuteProcedureInt(DataTable table, string insertProcedureID, string updateProcedureID, string deleteProcedureID)
        {
            T t = default(T);
            XmlConfig config;
            ProcedureInfo proc;

            SqlTransaction tran = null;
            try
            {
                // �����ļ�
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
                        // ��ʼ����
                        cmd.Transaction = tran;

                        foreach (DataRow row in table.Rows)
                        {
                            if (row.RowState == DataRowState.Unchanged) continue;
                            if (row.RowState == DataRowState.Deleted)
                            {// ɾ��
                                // �洢������Ϣ
                                proc = config.GetProcedure(deleteProcedureID);
                                // ��ʵ�������ò�����ֵ
                                foreach (ParameterInfo parm in proc.Parameters)
                                {
                                    PropertyHandler.SetPropertyValue<T>(ref t, parm.ParameterName, row[parm.ParameterName, DataRowVersion.Original]);
                                }
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                                // ��ȡ������ֵ
                                continue;
                            }
                            // ��ȡʵ��
                            t = LoadEntity.GetEntityFromRow<T>(row);

                            if (row.RowState == DataRowState.Added)
                            {// ����
                                // �洢������Ϣ
                                proc = config.GetProcedure(insertProcedureID);
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                            }
                            else if (row.RowState == DataRowState.Modified)
                            {// �޸�
                                // �洢������Ϣ
                                proc = config.GetProcedure(updateProcedureID);
                                cmd.Parameters.Clear();
                                SetCommand(t, cmd, proc);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // �������
                        tran.Commit();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                { // ����ʱ�ع�����
                    tran.Rollback();
                }
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// ������ķ�����ִ�ж���洢����
        /// </summary>
        /// <param name="t"></param>
        /// <param name="procedures"></param>
        /// <returns></returns>
        public bool ExecuteProcedureTran(T t, List<string> procedures)
        {
            // �����ļ�
            XmlConfig config = GetXmlConfig(t);
            ProcedureInfo proc;

            return ExecuteProcedureTran((cmd) =>
            {
                foreach (string procedureID in procedures)
                {
                    // �洢������Ϣ
                    proc = config.GetProcedure(procedureID);

                    cmd.Parameters.Clear();
                    SetCommand(t, cmd, proc);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        #endregion

        #region �û������켣��¼

        /// <summary>
        /// ��¼�û������켣
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="tableName">���ݱ���</param>
        /// <param name="connectionString">���ݿ������ַ���</param>
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