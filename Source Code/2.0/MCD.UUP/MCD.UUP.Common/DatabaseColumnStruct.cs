using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.UUP.Common
{
    /// <summary>
    /// 数据库一个字段的结构信息
    /// </summary>
    public struct DatabaseColumnStruct
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public Type DbType { get; set; }
        /// <summary>
        /// 字段在数据库中的类型名称
        /// </summary>
        public string DbTypeName { get; set; }

        /// <summary>
        /// 字段大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowDBNull { get; set; }
        /// <summary>
        /// 是否为标识
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey { get; set; }
        /// <summary>
        /// 字段的序号
        /// </summary>
        public int ColumnIndex { get; set; }
    }
}