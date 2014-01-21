using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public enum CheckType
    {
        NotEmpty = 1,  //非空
        Email = 2,   //邮箱
        IntType = 4,   //整形
        MoneyType = 8,  //货币
        DateType = 16,  //时间
        Chinese = 32,   //中文
        Mobile = 64,  //手机号码
        ZipCode = 128,  //邮编
        Identity = 256,  //身份证
        URL = 512,  //网址
        ExcelColumn = 1024 //excel列头
    }

    /// <summary>
    /// 
    /// </summary>
    public static class UIChecker
    {
        public static bool Check(this TextBox txt, CheckType type)
        {
            bool flag = true;
            //
            string value = txt.Text.Trim();
            foreach (CheckType day in Enum.GetValues(typeof(CheckType)))
            {
                string temp = (type & day).ToString();
                if (temp != "0")
                {
                    Type t = Type.GetType("MCD.Common.UIChecker");
                    flag &= Convert.ToBoolean(t.InvokeMember(temp, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, new string[] { value }));
                }
            }
            return flag;
        }


        /// <summary>
        /// 检测文本是否为空
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorTitle"></param>
        /// <returns></returns>
        public static bool VerifyTextBoxNull(TextBox txt, string errorMessage, string errorTitle)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt.Focus();
                //
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检测文本是否为空
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorTitle"></param>
        /// <returns></returns>
        public static bool VerifyTextBoxNull(TextBox txt, string errorMessage)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                MessageBox.Show(errorMessage, "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt.Focus();
                //
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检测文本是否为空
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorTitle"></param>
        /// <returns></returns>
        public static bool VerifyTextBoxNull(TextBox txt, string errorMessage, CheckType type)
        {
            if (!txt.Check(type))
            {
                MessageBox.Show(errorMessage, "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt.Focus();
                //
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测日期段文本框内的时间段是否合法
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="IsTheSameMonth">是否同一个月</param>
        /// <returns></returns>
        public static bool VerifyDatePakerTimer(DateTimePicker begin, DateTimePicker end)
        {
            if (!begin.Text.NotEmpty())
            {
                MessageBox.Show("开始时间不能空！", "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                begin.Focus();
                //
                return false;
            }
            if (!end.Text.NotEmpty())
            {
                MessageBox.Show("结束时间不能空！", "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                end.Focus();
                //
                return false;
            }
            if (DateTime.Parse(begin.Text) > DateTime.Parse(end.Text))
            {
                MessageBox.Show("结束时间不能小于开始时间！", "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                end.Focus();
                //
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测 Combox 是否为空
        /// </summary>
        /// <param name="cbb"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorTitle"></param>
        /// <returns></returns>
        public static bool VerifyComboxNull(ComboBox cbb, string errorMessage, string errorTitle)
        {
            if (cbb.SelectedValue == null)
            {
                MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbb.Focus();
                //
                return false;
            }
            return true;
        }

        /// <summary>
        /// 密码规则验证
        /// 1.长度大于等于 6 位
        /// 2.必须包含数字和字母
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool VerifyPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            //
            bool hascHaracters = false, hasNumber = false;
            Regex regHar = new Regex(@"^[A-Za-z]+$");
            Regex regNum = new Regex(@"^[0-9]+$");
            for (int i = 0; i < password.Length; i++)
            {
                if (hascHaracters == false && regHar.IsMatch(password[i].ToString()))
                {
                    hascHaracters = true;
                }
                if (hasNumber == false && regNum.IsMatch(password[i].ToString()))
                {
                    hasNumber = true;
                }
            }
            if (hasNumber && hascHaracters)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region  textbox扩展调用

        /// <summary>
        /// 是否为正数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool MoneyType(this string value)
        {
            double temp;
            return !value.NotEmpty() ? true : (Double.TryParse(value, out temp) ? (temp >= 0) : false);
        }

        /// <summary>
        /// 验证是否非
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotEmpty(this string value)
        {
            return !String.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 验证是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Empty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 验证是否是时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Email(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ExcelColumn(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"[A-Z]{1}|A[A-Z]{1}", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是时间类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DateType(this string value)
        {
            DateTime time = new DateTime();
            return !value.NotEmpty() ? true : DateTime.TryParse(value, out time);
        }

        /// <summary>
        /// 验证是否整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IntType(this string value)
        {
            int temp;
            return !value.NotEmpty() ? true : Int32.TryParse(value, out temp);
        }

        /// <summary>
        /// 验证是否全中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Chinese(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为RUL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool URL(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNormalChar(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Mobile(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"^1[35]\d{9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为身份证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIdentityCard(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"\d{17}[\d|X]|\d{15}", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为邮编
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ZipCode(this string value)
        {
            return !value.NotEmpty() ? true : Regex.IsMatch(value, @"\d{6}", RegexOptions.IgnoreCase);
        }
        #endregion

        #region   String实例扩展调用

        /// <summary>
        /// 验证字符串长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="begin">最小长度</param>
        /// <param name="end">最大长度</param>
        /// <param name="NotEmpty">是否允许空</param>
        /// <returns></returns>
        public static bool IsLengthStr(this string value, int begin, int end, bool NotEmpty)
        {
            return value.NotEmpty() ? NotEmpty : (value.Length >= begin || value.Length <= end);
        }

        /// <summary>
        /// 验证数字大小
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="NotEmpty">是否允许空</param>
        /// <returns></returns>
        public static bool IsRangNum(this string value, int min, int max, bool NotEmpty)
        {
            int temp = 0;
            return value.NotEmpty() ? NotEmpty : Int32.TryParse(value, out temp) || temp >= min || temp <= max;
        }

        /// <summary>
        /// 将字符串中所有数字转换为指定小数点位数的小数。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length">转换后小数点位数</param>
        /// <returns></returns>
        public static string ToFloatString(this string value, int length)
        {
            return Regex.Replace(value, @"\d+(,\d\d\d)*(\.\d+)?", (match) =>
            {
                if (match == null)
                {
                    return string.Empty;
                }
                //
                double d = 0D;
                if (double.TryParse(match.Value, out d))
                {
                    return d.ToString(string.Format("F{0}", length));
                }
                return match.Value;
            }, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 将字符串中所有数字转换为三位逗号分隔且小数点位数为两位的数字。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNumberString(this string value)
        {
            return Regex.Replace(value, @"\d+(,\d\d\d)*(\.\d+)?", (match) =>
            {
                if (match == null)
                {
                    return string.Empty;
                }
                //
                double d = 0D;
                if (double.TryParse(match.Value, out d))
                {
                    return d.ToString("N");
                }
                return match.Value;
            }, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 将字符串中所三位逗号分隔的数值类型转换为普通数字（去掉逗号）且保留两位小数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNormalString(this string value)
        {
            return value.ToFloatString(2);
        }

        /// <summary>
        /// 提取字符串中所有数字（除百分数）将其转换为指定位数的小数后用指定的分隔符分隔后返回。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ExtractDigital(this string value, string split, int length)
        {
            string result = string.Empty;
            MatchCollection matches = Regex.Matches(value, @"\d+(,\d\d\d)*(\.\d+)?%?", RegexOptions.IgnoreCase);
            if (matches != null && matches.Count > 0)
            {
                double d = 0D;
                foreach (Match match in matches)
                {
                    if (!match.Value.EndsWith("%") && double.TryParse(match.Value, out d))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = d.ToString(string.Format("F{0}", length));
                        }
                        else
                        {
                            result = string.Format("{0}{1}{2}", result, split, d.ToString(string.Format("F{0}", length)));
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 提取字符串中所有数字（除百分数）将其转换为小数点位数为2位的小数后用分号";"分隔后返回。
        /// </summary>
        /// <returns></returns>
        public static string ExtractDigital(this string value)
        {
            return value.ExtractDigital(";", 2);
        }

        /// <summary>  
        /// 处理DataRow筛选条件的特殊字符  
        /// </summary>  
        /// <param name="rowFilter">行筛选条件表达式</param>  
        /// <returns></returns>  
        public static string ToRowFilterString(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //在DataView的RowFilter里面的特殊字符要用"[]"括起来，单引号要换成"''",他的表达式里面没有通配符的说法  
                return value.Replace("[", "[[ ")
                    .Replace("]", " ]]")
                    .Replace("*", "[*]")
                    .Replace("%", "[%]")
                    .Replace("[[ ", "[[]")
                    .Replace(" ]]", "[]]")
                    .Replace("\'", "''");
            }
            return value;
        }
        #endregion
    }
}