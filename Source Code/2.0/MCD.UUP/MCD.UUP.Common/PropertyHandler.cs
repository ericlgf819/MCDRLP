using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MCD.UUP.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyHandler
    {
        //Fields
        private const BindingFlags PublicInstancePropertyBindingFlags = BindingFlags.CreateInstance
            | BindingFlags.Instance         //实例
            | BindingFlags.Public           //公有
            | BindingFlags.SetProperty      //可写属性
            | BindingFlags.IgnoreCase;      //忽略大小写

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <typeparam name="T">待获取属性值的实体类</typeparam>
        /// <param name="t">待获取属性值的实体类实例</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T t, string propertyName)
        {
            return PropertyHandler.GetPropertyValue<T>(t, propertyName, null);
        }
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T t, string propertyName,object[] index)
        {
            PropertyInfo pInfo = t.GetType().GetProperty(propertyName,  PropertyHandler.PublicInstancePropertyBindingFlags);
            //
            return pInfo != null ? pInfo.GetValue(t, index) : null;
        }

        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(ref T t, string propertyName, object value)
        {
            PropertyHandler.SetPropertyValue<T>(ref t, propertyName, value, null);   
        }
        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public static void SetPropertyValue<T>(ref T t, string propertyName, object value, object[] index)
        {
            PropertyInfo pInfo = t.GetType().GetProperty(propertyName, PropertyHandler.PublicInstancePropertyBindingFlags);
            if (pInfo != null)
            {
                pInfo.SetValue(t, value, index);
            }
        }
    }
}