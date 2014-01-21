using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectHelper
    {
        //Fields
        private const BindingFlags PublicInstancePropertyBindingFlags = BindingFlags.CreateInstance
            | BindingFlags.Instance         //实例
            | BindingFlags.Public           //公有
            | BindingFlags.SetProperty      //可写属性
            | BindingFlags.IgnoreCase;      //忽略大小写

        //Methods
        /// <summary>
        /// 通过DataRow给对象设置属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="row"></param>
        public static void SetPropertiesByDataRow<T>(ref T entity, DataRow row)
        {
            if (entity != null)
            {
                Type type = typeof(T);
                DataTable dt = row.Table;
                //
                PropertyInfo[] properties = type.GetProperties(ReflectHelper.PublicInstancePropertyBindingFlags);
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        if (dt.Columns.Contains(property.Name) && property.CanWrite)
                        {
                            object value = row[property.Name];
                            if (value != DBNull.Value && value != null)
                            {
                                property.SetValue(entity, value, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 将DataTable转换为对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToEntityList<T>(DataTable dt)
        {
            List<T> myList = new List<T>();
            //
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        T entity = Activator.CreateInstance<T>();
                        ReflectHelper.SetPropertiesByDataRow<T>(ref entity, row);
                        //
                        myList.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //
            return myList;
        }
    }
}