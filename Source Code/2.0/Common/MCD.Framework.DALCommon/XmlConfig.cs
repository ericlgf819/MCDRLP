using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Configuration;
using System.IO;

namespace MCD.Framework.DALCommon
{
    /// <summary>
    /// Xml 配置文件信息内容
    /// </summary>
    public class XmlConfig : XmlDocument
    {
        //Fields
        /// <summary>
        /// 缓存存储过程信息
        /// </summary>
        private static Dictionary<string, ProcedureInfo> Procedures = new Dictionary<string, ProcedureInfo>();
        private string connectionString = string.Empty;

        //Properties
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string XmlPath { get; set; }
        /// <summary>
        /// 获取数据库链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (this.connectionString == string.Empty)
                {
                    string value = "DBConnection";
                    XmlNode node = null;
                    if (this.DocumentElement != null)
                    {
                        node = this.DocumentElement.SelectSingleNode("Provider");
                    }
                    if (node != null && node.Attributes["name"] != null)
                    {
                        value = node.Attributes["name"].Value.Trim();
                    }
                    if (ConfigurationManager.ConnectionStrings[value] == null)
                    {
                        throw new Exception(string.Format("config 配置文件中没有设置名为 {0} 的链接字符串。", value));
                    }
                    this.connectionString = ConfigurationManager.ConnectionStrings[value].ConnectionString;
                }
                //
                return this.connectionString;
            }
        }
        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlPath">配置文件路径</param>
        public XmlConfig(string xmlPath)
        {
            this.XmlPath = xmlPath;
            if (System.IO.File.Exists(xmlPath))
            {
                this.Load(xmlPath);
            }
        }
        #endregion

        //Methods
        /// <summary>
        /// 存储过程节点信息
        /// </summary>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        public ProcedureInfo GetProcedure(string procedureID)
        {
            if (XmlConfig.Procedures.Keys.Contains(Path.GetFileName(this.XmlPath) + procedureID))
            {
                return XmlConfig.Procedures[Path.GetFileName(this.XmlPath) + procedureID];
            }
            //
            ProcedureInfo proc = new ProcedureInfo(this, procedureID);
            XmlConfig.Procedures.Add(Path.GetFileName(this.XmlPath) + procedureID, proc);
            return proc;
        }
    }

    /// <summary>
    /// 存储过程节点信息
    /// </summary>
    public class ProcedureInfo
    {
        //Fields
        private XmlNode procedureNode = null;
        private string procedureName = null;
        private List<ParameterInfo> parameters = null;

        //Properties
        private XmlConfig xmlConfig { get; set; }
        private string ProcedureID { get; set; }
        /// <summary>
        /// 存储过程节点
        /// </summary>
        public XmlNode ProcedureNode
        {
            get
            {
                if (this.procedureNode == null)
                {
                    this.procedureNode = this.xmlConfig.DocumentElement.SelectSingleNode("Statements/Procedure[@id='" + this.ProcedureID + "']");
                    if (this.procedureNode == null)
                    {
                        throw new Exception(string.Format("没有配置 {0} 的存储过程节点", this.ProcedureID));
                    }
                }
                //
                return this.procedureNode;
            }
        }
        /// <summary>
        /// 获取存储过程名称
        /// </summary>
        public string ProcedureName
        {
            get
            {
                if (string.IsNullOrEmpty(this.procedureName))
                {
                    if (this.ProcedureNode.Attributes["value"] == null)
                    {
                        throw new Exception(string.Format("没有为存储过程节点 {0} 设置 value 属性", this.ProcedureID));
                    }
                    this.procedureName = this.ProcedureNode.Attributes["value"].Value.Trim();
                }
                //
                return this.procedureName;
            }
        }
        /// <summary>
        /// 获取参数信息
        /// </summary>
        public List<ParameterInfo> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = new List<ParameterInfo>();
                    //
                    XmlNodeList paramNodes = this.ProcedureNode.SelectNodes("Parameters/Parameter");
                    foreach (XmlNode node in paramNodes)
                    {
                        int length = 0;
                        SqlDbType type = this.GetDBType(node.Attributes["type"].Value, out length);
                        var param = new ParameterInfo()
                        {
                            ParameterName = node.Attributes["name"].Value,
                            ParameterType = type,
                            Size = length,
                            Direction = 
                                (node.Attributes["direction"] != null && node.Attributes["direction"].Value.Trim().StartsWith("out")) ? 
                                ParameterDirection.Output : ParameterDirection.Input
                        };
                        this.parameters.Add(param);
                    }
                }
                //
                return this.parameters;
            }
        }
        #region ctor

        public ProcedureInfo(XmlConfig config, string procedureID)
        {
            this.xmlConfig = config;
            this.ProcedureID = procedureID;
        }
        #endregion

        //Methods
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private SqlDbType GetDBType(string typeName, out int length)
        {
            length = 0;
            SqlDbType dbType = SqlDbType.NVarChar;
            //
            string type = typeName;
            if (typeName.IndexOf("(") > 0)
            {
                type = typeName.Substring(0, typeName.IndexOf("("));
                int.TryParse(typeName.Substring(typeName.IndexOf("(") + 1, typeName.IndexOf(")") - typeName.IndexOf("(") - 1)
                    , out length);
            }
            switch (type.ToLower())
            {
                case "uniqueidentifier":
                    dbType = SqlDbType.UniqueIdentifier;
                    length = 36;
                    break;
                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;
                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "int":
                    dbType = SqlDbType.Int;
                    length = 8;
                    break;
                default:
                    break;
            }
            return dbType;
        }
    }

    /// <summary>
    /// 参数信息
    /// </summary>
    public struct ParameterInfo
    {
        //Properties
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数名对应的属性名，默认为去除 @
        /// </summary>
        public string PropertyName
        {
            get
            {
                if (string.IsNullOrEmpty(this.ParameterName))
                {
                    return string.Empty;
                }
                else if (this.ParameterName.StartsWith("@"))
                {
                    return this.ParameterName.Substring(1);
                }
                else
                {
                    return this.ParameterName;
                }
            }
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public SqlDbType ParameterType { get; set; }
        /// <summary>
        /// 参数大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 参数方式
        /// </summary>
        public ParameterDirection Direction { get; set; }
    }
}