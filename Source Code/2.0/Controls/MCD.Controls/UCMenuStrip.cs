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

using MCD.DockContainer.Docking;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCMenuStrip : MenuStrip
    {
        /// <summary>
        /// 主窗体显示菜单控件
        /// </summary>
        public DockPanel MainFormDockPanel { get; set; }

        /// <summary>
        /// 打开窗体方法
        /// </summary>
        public IFormHandler FormHandler { get; set; }
        #region ctor

        public UCMenuStrip()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void InitLayout()
        {
            this.InitialMenu();
            //
            base.InitLayout();
        }
        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            UCToolStripMenuItem item = sender as UCToolStripMenuItem;
            if (item.Tag.ToString() != string.Empty)
            { // form 不为空时，打开窗体
                this.FormHandler.OpenForm(item.Tag + string.Empty, item.DockTitle, this.MainFormDockPanel, item.OpenMethod);
            }
            else
            { // 为空时，则执行自定义方法
                this.FormHandler.MenuClick(item.Name, GetForm(this.Parent));
            }
        }

        //Methods
        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void InitialMenu()
        {
            string shutKey = string.Empty;// 快捷键字母大写
            Keys firstKey  = Keys.None, secondKey = Keys.None; // 功能键、快捷键

            // 加载一级菜单
            List<MenuItem> baseMenus = SysMenuHandler.Instance.GetRootMenu();
            foreach (MenuItem baseMenu in baseMenus)
            {
                if (!baseMenu.Visible) continue;
                //
                UCToolStripMenuItem item = new UCToolStripMenuItem() // 菜单
                {
                    Name = baseMenu.Name,
                    Tag = baseMenu.Form,
                    DockTitle = baseMenu.Text,
                    Text = baseMenu.Text,
                    Image = this.GetMemuImage("menu_rl_" + baseMenu.Text, "menu_br_" + baseMenu.Text)
                };
                // 设置快捷键
                if (baseMenu.ShortKey != string.Empty)
                {
                    this.GetKeyByValue(baseMenu.ShortKey, out shutKey, out firstKey, out secondKey);
                    if (firstKey != Keys.None && secondKey != Keys.None)
                    {
                        item.ShortcutKeys = ((System.Windows.Forms.Keys)((firstKey | secondKey)));
                        item.Text += "(&" + shutKey + ")";
                    }
                }
                // 递归方式初始化二级以下菜单
                if (baseMenu.Menus != null && baseMenu.Menus.Count != 0)
                {
                    this.InitialMenu(item, baseMenu);
                }
                this.Items.Add(item);
            }
        }
        /// <summary>
        /// 递归方式初始化菜单
        /// </summary>
        /// <param name="paretItem"></param>
        /// <param name="parentNode"></param>
        private void InitialMenu(ToolStripMenuItem paretItem, MenuItem parentMenu)
        {
            string shutKey = string.Empty;
            Keys firstKey = Keys.None, secondKey = Keys.None;
            // 加载下一级菜单
            foreach (MenuItem menu in parentMenu.Menus)
            {
                if (!menu.Visible) continue;
                // 表示分隔线
                if (menu.Text == "-")
                {
                    paretItem.DropDownItems.Add(new ToolStripSeparator()
                    {
                        Name = "split" + menu.Name,
                        Size = this.Size
                    });
                    continue;
                }
                // 加载普通菜单
                UCToolStripMenuItem item = new UCToolStripMenuItem()
                {
                    Text = menu.Text,
                    Name = menu.Name,
                    Tag = menu.Form,
                    DockTitle = menu.Text,
                    OpenMethod = menu.OpenMethod,
                    Image = this.GetMemuImage("menu_rl_" + menu.Text, "menu_br_" + menu.Text)
                };
                // 设置快捷键
                if (menu.ShortKey != string.Empty)
                {
                    this.GetKeyByValue(menu.ShortKey, out shutKey, out firstKey, out secondKey);
                    if (firstKey != Keys.None && secondKey != Keys.None)
                    {
                        item.ShortcutKeys = ((System.Windows.Forms.Keys)((firstKey | secondKey)));
                        item.Text += "(&" + shutKey + ")"; // 更改菜单显示，增加快捷键样式
                    }
                }

                // 注册菜单事件
                item.Click += new EventHandler(this.item_Click);

                // 递归添加子节点菜单
                if (menu.Menus != null && menu.Menus.Count != 0)
                {
                    this.InitialMenu(item, menu);
                }
                paretItem.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        private Image GetMemuImage(string name1, string name2)
        {
            object o1 = (Properties.Resources.ResourceManager.GetObject(name1));
            object o2 = (Properties.Resources.ResourceManager.GetObject(name2));
            if (o1 != null)
            {
                return (Image)o1;
            }
            else if (o2 != null)
            {
                return (Image)o2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取主窗体
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Form GetForm(Control c)
        {
            if (base.Parent is Form)
            {
                return (Form)base.Parent;
            }
            else
            {
                return this.GetForm(base.Parent);
            }
        }

        /// <summary>
        /// 将快捷键转换为系统所识别的键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="shutKey"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        private void GetKeyByValue(string key, out string shutKey, out Keys firstKey, out Keys secondKey)
        {
            firstKey = Keys.None;
            secondKey = Keys.None;
            shutKey = string.Empty;
            //
            string[] keys = key.Split(new string[] { "+", "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (keys.Length != 2)
            {
                return;
            }
            // 获取快捷键字母大写
            shutKey = keys[1].ToUpper();
            firstKey = this.GetKeyByCode(keys[0]);
            secondKey = this.GetKeyByCode(keys[1]);
        }
        /// <summary>
        /// 获取键盘按键值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Keys GetKeyByCode(string code)
        {
            Keys key = Keys.None;
            switch (code.Trim().ToLower())
            {
                case "ctrl":
                    key = Keys.Control;
                    break;
                case "alt":
                    key = Keys.Alt;
                    break;
                case "a":
                    key = Keys.A;
                    break;
                case "b":
                    key = Keys.B;
                    break;
                case "c":
                    key = Keys.C;
                    break;
                case "d":
                    key = Keys.D;
                    break;
                case "e":
                    key = Keys.E;
                    break;
                case "f":
                    key = Keys.F;
                    break;
                case "g":
                    key = Keys.G;
                    break;
                case "h":
                    key = Keys.H;
                    break;
                case "i":
                    key = Keys.I;
                    break;
                case "j":
                    key = Keys.J;
                    break;
                case "k":
                    key = Keys.K;
                    break;
                case "l":
                    key = Keys.L;
                    break;
                case "m":
                    key = Keys.M;
                    break;
                case "n":
                    key = Keys.N;
                    break;
                case "o":
                    key = Keys.O;
                    break;
                case "p":
                    key = Keys.P;
                    break;
                case "q":
                    key = Keys.Q;
                    break;
                case "r":
                    key = Keys.R;
                    break;
                case "s":
                    key = Keys.S;
                    break;
                case "t":
                    key = Keys.T;
                    break;
                case "u":
                    key = Keys.U;
                    break;
                case "v":
                    key = Keys.V;
                    break;
                case "w":
                    key = Keys.W;
                    break;
                case "x":
                    key = Keys.X;
                    break;
                case "y":
                    key = Keys.Y;
                    break;
                case "z":
                    key = Keys.Z;
                    break;
                case "f1":
                    key = Keys.F1;
                    break;
                case "f2":
                    key = Keys.F2;
                    break;
                case "f3":
                    key = Keys.F3;
                    break;
                case "f4":
                    key = Keys.F4;
                    break;
                case "f5":
                    key = Keys.F5;
                    break;
                case "f6":
                    key = Keys.F6;
                    break;
                case "f7":
                    key = Keys.F7;
                    break;
                case "f8":
                    key = Keys.F8;
                    break;
                case "f9":
                    key = Keys.F9;
                    break;
                case "f10":
                    key = Keys.F10;
                    break;
                case "f11":
                    key = Keys.F11;
                    break;
                case "f12":
                    key = Keys.F12;
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}