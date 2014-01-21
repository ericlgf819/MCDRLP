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
    public partial class UCMenuButton : UserControl
    {
        //Fields
        private bool m_IsExpand = false;

        //Properties
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand
        {
            get
            {
                return this.m_IsExpand;
            }
            set
            {
                if (value)
                {
                    this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.Down;
                }
                else
                {
                    this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.Up;
                }
                this.m_IsExpand = value;
                //
                Thread.Sleep(30);
            }
        }
        /// <summary>
        /// 父容器
        /// </summary>
        public UCLeftMenu ParentControl { get; set; }
        /// <summary>
        /// 获取或设置显示在菜单上的文字
        /// </summary>
        public string MenuText
        {
            get { return this.lblMenuButtonMain.Text; }
            set { this.lblMenuButtonMain.Text = value; }
        }
        #region ctor

        public UCMenuButton()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        /// <summary>
        /// 鼠标移到控件上时，改变图标样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblMenuButtonMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsExpand)
            {
                this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.DownMouse;
            }
            else
            {
                this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.UpMouse;
            }
            Thread.Sleep(30);
        }
        /// <summary>
        /// 鼠标移出控件上时，改变图标样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblMenuButtonMain_MouseLeave(object sender, EventArgs e)
        {
            if (this.IsExpand)
            {
                this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.Down;
            }
            else
            {
                this.pBoxMenuButton.Image = global::MCD.Controls.Properties.Resources.Up;
            }
        }
        /// <summary>
        /// 鼠标点击标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblMenuButtonMain_Click(object sender, EventArgs e)
        {
            this.ParentControl.MenuClick(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pBoxMenuButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblMenuButtonMain_MouseMove(sender, e);
        }
        /// <summary>
        /// 鼠标点击图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pBoxMenuButton_Click(object sender, EventArgs e)
        {
            this.ParentControl.MenuClick(this);
        }

        //Methods
        /// <summary>
        /// 设置显示的控件名
        /// </summary>
        /// <param name="name"></param>
        public void SetLabelName(string name)
        {
            this.lblMenuButtonMain.Name = name;
        }
    }
}