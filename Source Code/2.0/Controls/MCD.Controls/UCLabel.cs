using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCLabel : Label
    {
        #region ctor

        public UCLabel()
        {
            InitializeComponent();
        }
        #endregion

        private int labelLocation = 0;
        /// <summary>
        /// 获取或设置控件右边离左边界的距离
        /// </summary>
        [DefaultValue(0),
        Description("获取或设置控件右边区域离界面左边边界的距离，为0时表示系统不坐控制。"),
        Category("Behavior")]
        public int LabelLocation
        {
            get
            {
                return this.labelLocation;
            }
            set
            {
                this.labelLocation = value;
            }
        }

        private bool needLanguage = true;
        /// <summary>
        /// 获取或设置是否需要多语言控制
        /// </summary>
        [DefaultValue(true),
        Description("获取或设置该控件是否需要进行多语言版本控制。"),
        Category("Behavior")]
        public bool NeedLanguage
        {
            get { return this.needLanguage; }
            set { this.needLanguage = value; }
        }
    }
}