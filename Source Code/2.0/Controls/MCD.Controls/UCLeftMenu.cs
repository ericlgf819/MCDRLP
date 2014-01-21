using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using MCD.DockContainer;
using MCD.DockContainer.Docking;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCLeftMenu : UserControl
    {
        //Fields
        public PictureBox pboxClose = null;
        /// <summary>
        /// 菜单默认是打开状态
        /// </summary>
        private MENU_STATUS menuStatus = MENU_STATUS.OPEN;
        /// <summary>
        /// 默认菜单高度
        /// </summary>
        private const int MENU_ITEM_HEIGHT = 22;

        //Properties
        /// <summary>
        /// 主窗体显示菜单控件
        /// </summary>
        public DockPanel MainFormDockPanel { get; set; }
        /// <summary>
        /// 打开窗体方法
        /// </summary>
        public IFormHandler FormHandler { get; set; }
        /// <summary>
        /// 一级菜单
        /// </summary>
        public List<MenuItem> menuItems
        {
            get; private set;
        }
        #region ctor

        public UCLeftMenu()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void UCLeftMenu_Load(object sender, EventArgs e)
        {
            // 加载一级菜单
            this.menuItems = SysMenuHandler.Instance.GetRootMenu();
            for (int i = menuItems.Count - 1; i >= 0; i--)
            {
                MenuItem baseMenu = menuItems[i];
                // 不显示在左边树形菜单
                if (!baseMenu.ShowLeft)
                {
                    continue;
                }

                // 生成显示菜单容器的 Panel 
                Panel pnlMenu = new Panel()
                {
                    Name = "pnl" + baseMenu.Name,
                    Dock = DockStyle.Top,
                    BackColor = SystemColors.InactiveCaptionText
                };

                // 生成显示/隐藏菜单的按钮
                UCMenuButton btnMenu = new UCMenuButton()
                {
                    Name = "btn" + baseMenu.Name,
                    MenuText = baseMenu.Text.IndexOf("(") > -1 ? baseMenu.Text.Substring(0, baseMenu.Text.IndexOf("(")) : baseMenu.Text,
                    Tag = "pnl" + baseMenu.Name,
                    Dock = DockStyle.Top,
                    ParentControl = this
                };
                btnMenu.SetLabelName(baseMenu.Name);

                // 给菜单容器加载菜单控件
                if (baseMenu.Menus != null)
                {
                    this.AddMenuItem(baseMenu, pnlMenu);
                }

                // 给主控件添加菜单容器和按钮
                this.Controls.Add(pnlMenu);
                this.Controls.Add(btnMenu);

                // 是否需要展开菜单
                if (baseMenu.Expand)
                {
                    btnMenu.IsExpand = true;
                    pnlMenu.Visible = true;
                }
                else
                {// 隐藏菜单
                    btnMenu.IsExpand = false;
                    pnlMenu.Visible = false;
                }
            }
            //
            this.AddTitleInfo();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pboxClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //
            this.menuStatus = MENU_STATUS.CLOSE;
        }
        /// <summary>
        /// 菜单点击事件，显示窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            UCLeftMenuItem item = sender as UCLeftMenuItem;
            //
            this.FormHandler.OpenForm(item.Tag + string.Empty, item.Text, this.MainFormDockPanel, item.OpenMethod);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCLeftMenu_MouseMove(object sender, MouseEventArgs e)
        {
            //base.Refresh();
        }
        
        //Methods
        /// <summary>
        /// 显示菜单
        /// </summary>
        public void OpenMenu()
        {
            if (this.menuStatus == MENU_STATUS.CLOSE)
            {
                this.Visible = true;
                //
                this.menuStatus = MENU_STATUS.OPEN;
            }
        }

        /// <summary>
        /// 隐藏菜单
        /// </summary>
        public void CloseMenu()
        {
            this.Visible = false;
            //
            this.menuStatus = MENU_STATUS.CLOSE;
        }

        /// <summary>
        /// 给菜单容器添加菜单
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pnlParent"></param>
        private void AddMenuItem(MenuItem parent, Panel pnlParent)
        {
            int top = pnlParent.Location.Y;// 菜单的显示位置
            for (int i = parent.Menus.Count - 1; i >= 0; i--)
            {
                MenuItem menu = parent.Menus[i];
                if (menu.Text == "-") continue;// - 表示分隔线，分隔线不生成菜单
                if (!menu.ShowLeft)// 不显示
                {
                    continue;
                }
                // 生成菜单项
                UCLeftMenuItem item = new UCLeftMenuItem()
                {
                    Name = "lmi" + menu.Name,
                    Text = menu.Text,
                    Tag = menu.Form,
                    Height = menu.Height,
                    OpenMethod = menu.OpenMethod,
                    Location = new Point(pnlParent.Location.X, top + 1) // 菜单在竖直方向显示位置
                };
                // 设置菜单点击事件
                item.Click += new EventHandler(this.item_Click);
                //
                pnlParent.Controls.Add(item);
                //
                top += item.Height;
            }
            // 设置菜单容器的高度
            pnlParent.Height = top - pnlParent.Location.Y;
        }

        /// <summary>
        /// 按钮单击事件，显示/隐藏菜单
        /// </summary>
        /// <param name="sender"></param>
        public void MenuClick(object sender)
        {
            UCMenuButton btnMenu = sender as UCMenuButton;
            //
            Control[] pnls = base.Controls.Find(btnMenu.Tag + string.Empty, true);
            if (pnls.Count() > 0)
            {
                Panel pnl = pnls[0] as Panel;
                if (pnl != null)
                {
                    if (pnl.Visible)
                    {
                        pnl.Visible = false;
                        btnMenu.BackColor = SystemColors.Control;
                        btnMenu.IsExpand = false;
                    }
                    else
                    {
                        pnl.Visible = true;
                        btnMenu.BackColor = SystemColors.ButtonShadow;
                        btnMenu.IsExpand = true;
                    }
                }
            }
        }

        /// <summary>
        /// 加载菜单头部信息
        /// </summary>
        private void AddTitleInfo()
        {
            this.pboxClose = new System.Windows.Forms.PictureBox();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLeftMenu));
            //
            Panel pnlMenuTitle = new System.Windows.Forms.Panel();
            pnlMenuTitle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            pnlMenuTitle.Controls.Add(this.pboxClose);
            pnlMenuTitle.Dock = System.Windows.Forms.DockStyle.Top;
            pnlMenuTitle.Location = new System.Drawing.Point(0, 0);
            pnlMenuTitle.Name = "pnlMenuTitle";
            pnlMenuTitle.Size = new System.Drawing.Size(174, 25);
            pnlMenuTitle.TabIndex = 0;
            //
            this.pboxClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pboxClose.Image = global::MCD.Controls.Properties.Resources.close;
            this.pboxClose.Location = new System.Drawing.Point(146, 0);
            this.pboxClose.Name = "pboxClose";
            this.pboxClose.Size = new System.Drawing.Size(28, 27);
            this.pboxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxClose.TabIndex = 1;
            this.pboxClose.TabStop = false;
            this.pboxClose.Click += new System.EventHandler(this.pboxClose_Click);
            //
            this.Controls.Add(pnlMenuTitle);
        }
    }

    /// <summary>
    /// 菜单显示状态
    /// </summary>
    public enum MENU_STATUS
    {
        /// <summary>
        /// 打开状态
        /// </summary>
        OPEN,
        /// <summary>
        /// 关闭状态
        /// </summary>
        CLOSE,
        /// <summary>
        /// 无状态
        /// </summary>
        NONE
    }
}