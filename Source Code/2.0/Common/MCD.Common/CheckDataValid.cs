using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckDataValid
    {
        /// <summary>
        /// 是否符合给定规则，符合返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularStr"></param>
        /// <returns></returns>
        public static bool CheckValid(string value, string regularStr)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regularStr);
            if (reg.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否符合常规用户名合法性：字母，数组，下划线组合，字符大头。
        /// 符合返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool CheckName(string value, int minLength, int maxLength)
        {
            return CheckDataValid.CheckValid(value, @"^[a-zA-Z][a-zA-Z0-9|_]{" + minLength.ToString() + "," + maxLength.ToString() + "}$");
        }
        /// <summary>
        /// 是否只含有汉字、数字、字母、下划线，下划线位置不限
        /// 符合返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckCommon(string value)
        {
            return CheckDataValid.CheckValid(value, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
        }
        /// <summary>
        /// 是否只含有汉字、数字、字母、下划线，连接符-，英文句号.   位置不限
        /// 符合返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckFileName(string value)
        {
            return CheckDataValid.CheckValid(value, @"^[a-zA-Z0-9_.-\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 验证是否为指定位数的数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str, int lenth)
        {
            if (str.Length > lenth)
            {
                return false;
            }
            else
            {
                return CheckDataValid.IsNumeric(str);
            }
        }
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
            {
                return false;
            }
            //
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}