using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class UCButton : System.Windows.Forms.Button
    {
        private bool popedom = true;
        /// <summary>
        /// 是否需要权限控制，默认为是
        /// </summary>
        [DefaultValue(true),
        Description("获取或设置按钮控件是否需要进行权限控制."),
        Category("Behavior")]
        public bool Popedom
        {
            get { return this.popedom; }
            set { this.popedom = value; }
        }

        private bool language = true;
        /// <summary>
        /// 是否需要语言控制，默认为是
        /// </summary>
        [DefaultValue(true),
        Description("获取或设置该控件是否需要进行语言控制."),
        Category("Behavior")]
        public bool NeedLanguage
        {
            get { return this.language; }
            set { this.language = value; }
        }
    }
}