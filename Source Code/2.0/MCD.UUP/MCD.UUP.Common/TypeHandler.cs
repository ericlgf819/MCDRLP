using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MCD.UUP.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static DbType GetTypeFromSqlType(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "guid":
                    {
                        return DbType.Guid;
                    }
                case "string":
                    {
                        return DbType.String;
                    }
                case "datetime":
                    {
                        return DbType.DateTime;
                    }
                case "int":
                    {
                        return DbType.Int32;
                    }
                case "byte":
                    {
                        return DbType.Boolean;
                    }
                case "float":
                    {
                        return DbType.Decimal;
                    }
                case "money":
                    {
                        return DbType.Decimal;
                    }
                case "decimal":
                    {
                        return DbType.Decimal;
                    }
                case "image":
                    {
                        return DbType.Binary;
                    }
                default: return DbType.String;
            }
        }
    }
}