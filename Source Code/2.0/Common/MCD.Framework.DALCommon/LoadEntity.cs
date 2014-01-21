using System;
using System.Data;

namespace MCD.Framework.DALCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadEntity
    {
        /// <summary>
        /// 从 DataTable 中获取实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static T GetEntityFromTable<T>(DataTable dt, object keyValue)
            where T : new()
        {
            try
            {
                DataRow row = dt.Rows.Find(keyValue);
                return LoadEntity.GetEntityFromRow<T>(row);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 从 DataRow 中获取实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T GetEntityFromRow<T>(DataRow row)
            where T : new()
        {
            // 遍历行
            T t = new T();
            try
            {
                foreach (DataColumn col in row.Table.Columns)
                {
                    PropertyHandler.SetPropertyValue<T>(ref t, col.ColumnName, row[col.ColumnName]);
                }
            }
            catch { }
            //
            return t;
        }
    }
}