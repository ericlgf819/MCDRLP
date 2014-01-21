using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCLeftMenuItem : Label
    {
        //Fields
        private FORM_OPEN openMethod = FORM_OPEN.Default;
        
        //Properties
        /// <summary>
        /// 获取或设置菜单打开方式
        /// </summary>
        public FORM_OPEN OpenMethod
        {
            get { return this.openMethod; }
            set { this.openMethod = value; }
        }
        /// <summary>
        /// 获取或设置控件的显示值
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text.Trim();
            }
            set
            {
                base.Text = value;
            }
        }
        #region ctor

        public UCLeftMenuItem()
        {
            InitializeComponent();
            //
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleLeft;
            base.Dock = DockStyle.Top;
            base.BackColor = Color.WhiteSmoke;

            // 鼠标移出控件上时，显示颜色
            base.MouseMove += new MouseEventHandler(this.UCLeftMenuItem_MouseMove);
            // 鼠标移到控件上时，显示颜色
            base.MouseLeave += new EventHandler(this.UCLeftMenuItem_MouseLeave);
        }
        #endregion

        //Events
        /// <summary>
        /// 鼠标移出控件时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCLeftMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.WhiteSmoke;
            Thread.Sleep(30);
        }
        /// <summary>
        /// 鼠标移到控件时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCLeftMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.PapayaWhip;
        }
    }
}