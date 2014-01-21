using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.Controls
{
    /// <summary>
    /// 表示ComboBox中的项。
    /// </summary>
    public class ComboBoxItem
    {
        //Properties
        /// <summary>
        /// 获取或设置项上显示的文本。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 获取或设置项表示的值。
        /// </summary>
        public string Value { get; set; }

        #region ctor

        /// <summary>
        /// 初始化ComboBoxItem类的新实例，并指定项上显示的文本与项所表示的值。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public ComboBoxItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }
        #endregion

        /// <summary>
        /// 返回当前项的文本。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}