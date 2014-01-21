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
    public class LoadEntity
    {
        //Fields
        private static LoadEntity instance = null;

        //Properties
        public static LoadEntity Instance
        {
            get
            {
                if (LoadEntity.instance == null)
                {
                    LoadEntity.instance = new LoadEntity();
                }
                return LoadEntity.instance;
            }
        }

        //Methods
        /// <summary>
        /// 从 DataRow 中获取实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public T GetEntityFromRow<T>(DataRow row)
            where T : new()
        {
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
        /// <summary>
        /// 从 DataTable 中获取实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public T GetEntityFromTable<T>(DataTable dt, object keyValue)
            where T : new()
        {
            try
            {
                DataRow row = dt.Rows.Find(keyValue);
                //
                return this.GetEntityFromRow<T>(row);
            }
            catch
            {
                return default(T);
            }
        }
    }
}