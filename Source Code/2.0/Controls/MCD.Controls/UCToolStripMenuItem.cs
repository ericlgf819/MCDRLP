using System;
using System.Windows.Forms;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCToolStripMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// 菜单显示的标题
        /// </summary>
        public string DockTitle { get; set; }

        /// <summary>
        /// 打开方式
        /// </summary>
        public FORM_OPEN OpenMethod { get; set; }
        #region ctor

        public UCToolStripMenuItem()
        {
            InitializeComponent();
        }
        #endregion
    }
}