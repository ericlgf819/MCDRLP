using System.Data;
using System.Runtime.Serialization;
using System.Data.SqlClient;

namespace MCD.RLPlanning.Entity.Common
{
    /// <summary>
    /// WCF支持的SqlParameter。
    /// </summary>
    [DataContract]
    public class WcfSqlParameter
    {
        //Fields
        private WcfSqlDbType sqlDbType = WcfSqlDbType.Uknown;
        private WcfParamDirection paramDirection = WcfParamDirection.InputOutput;

        //Properties
        [DataMember]
        public WcfSqlDbType SqlDbType
        {
            get { return this.sqlDbType; }
            set { this.sqlDbType = value; }
        }

        [DataMember]
        public WcfParamDirection ParamDirection
        {
            get { return this.paramDirection; }
            set { this.paramDirection = value; }
        }

        [DataMember]
        public bool IsNullable { get; set; }

        [DataMember]
        public string ParameterName { get; set; }

        [DataMember]
        public int Size { get; set; }

        [DataMember]
        public object Value { get; set; }
        #region ctor

        public WcfSqlParameter(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }

        public WcfSqlParameter(string parameterName, WcfSqlDbType dbType)
        {
            this.ParameterName = parameterName;
            this.SqlDbType = dbType;
        }

        public WcfSqlParameter(string parameterName, WcfSqlDbType dbType, int size)
        {
            this.ParameterName = parameterName;
            this.SqlDbType = dbType;
            this.Size = size;
        }

        public WcfSqlParameter(string parameterName, WcfSqlDbType dbType, int size, object value)
        {
            this.ParameterName = parameterName;
            this.SqlDbType = dbType;
            this.Size = size;
            this.Value = value;
        }
        #endregion

        /// <summary>
        /// 将WcfSqlParameter转换为SqlParameter。
        /// </summary>
        /// <returns></returns>
        public SqlParameter ToSqlParameter()
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = this.ParameterName,
                Size = this.Size,
                Direction = (ParameterDirection)this.ParamDirection,
                IsNullable = this.IsNullable,
                Value = this.Value
            };
            if (this.SqlDbType != WcfSqlDbType.Uknown)
            {
                param.SqlDbType = (SqlDbType)this.SqlDbType;
            }
            return param;
        }
    }

    /// <summary>
    /// WCF支持的ParamDirection。
    /// </summary>
    [DataContract]
    public enum WcfParamDirection
    {
        [EnumMember]
        Input = 1,
        [EnumMember]
        Output = 2,
        [EnumMember]
        InputOutput = 3,
        [EnumMember]
        ReturnValue = 6,
    }

    /// <summary>
    /// WCF支持的SqlDbType。
    /// </summary>
    [DataContract]
    public enum WcfSqlDbType
    {
        [EnumMember]
        BigInt = 0,
        [EnumMember]
        Binary = 1,
        [EnumMember]
        Bit = 2,
        [EnumMember]
        Char = 3,
        [EnumMember]
        DateTime = 4,
        [EnumMember]
        Decimal = 5,
        [EnumMember]
        Float = 6,
        [EnumMember]
        Image = 7,
        [EnumMember]
        Int = 8,
        [EnumMember]
        Money = 9,
        [EnumMember]
        NChar = 10,
        [EnumMember]
        NText = 11,
        [EnumMember]
        NVarChar = 12,
        [EnumMember]
        Real = 13,
        [EnumMember]
        UniqueIdentifier = 14,
        [EnumMember]
        SmallDateTime = 15,
        [EnumMember]
        SmallInt = 16,
        [EnumMember]
        SmallMoney = 17,
        [EnumMember]
        Text = 18,
        [EnumMember]
        Timestamp = 19,
        [EnumMember]
        TinyInt = 20,
        [EnumMember]
        VarBinary = 21,
        [EnumMember]
        VarChar = 22,
        [EnumMember]
        Variant = 23,
        [EnumMember]
        Xml = 25,
        [EnumMember]
        Udt = 29,
        [EnumMember]
        Structured = 30,
        [EnumMember]
        Date = 31,
        [EnumMember]
        Time = 32,
        [EnumMember]
        DateTime2 = 33,
        [EnumMember]
        DateTimeOffset = 34,
        [EnumMember]
        Uknown = 35
    }
}
