using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MCD.UUP.Common
{
    /// <summary>
    /// 对集合的扩展方法
    /// </summary>
    public class CollectionHandler
    {
        /// <summary>
        /// 查看一个集合是否有数据
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns></returns>
        public static bool HasItem(IList list)
        {
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// 把字符串集合，用符号连接起来
        /// </summary>
        /// <param name="separate">分隔符号</param>
        /// <param name="stringList">字符串集合</param>
        /// <returns>字符串</returns>
        public static string Join(string separate, List<string> stringList)
        {
            if (!CollectionHandler.HasItem(stringList))
            {
                return string.Empty;
            }
            return string.Join(separate, stringList.ToArray());
        }
    }
}