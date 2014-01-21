using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

using MCD.DockContainer.Docking;
using MCD.Controls;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client;

namespace MCD.Framework.AppCode
{
    /// <summary>
    /// 
    /// </summary>
    public class WinFormLanguage
    {
        //Fields
        /// <summary>
        /// 当前多国语言化的是否为用户控件，为否则为窗体。
        /// </summary>
        private bool isUserControl = false;

        //Properties
        /// <summary>
        /// 当前待多国语言化的窗体。
        /// </summary>
        private Form languageForm { get; set; }
        /// <summary>
        /// 当前待多国语言化的用户控件。
        /// </summary>
        private UserControl languageControl { get; set; }
        /// <summary>
        /// 当前窗体或用户控件的多国语言资源。
        /// </summary>
        private ResourceManager RM { get; set; }
        #region ctor

        /// <summary>
        /// 初始化WinFormLanguage类的新实例，并指定需要多国语言化的窗体。
        /// </summary>
        /// <param name="frm"></param>
        public WinFormLanguage(Form frm)
        {
            this.isUserControl = false;
            this.languageForm = frm;
            this.RM = new ResourceManager(this.languageForm.GetType().FullName, Assembly.GetAssembly(this.languageForm.GetType()));
        }
        /// <summary>
        /// 初始化WinFormLanguage类的新实例，并指定需要多国语言化的窗体以及资源文件的类型。
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="type"></param>
        public WinFormLanguage(Form frm, Type type)
        {
            this.isUserControl = false;
            this.languageForm = frm;
            this.RM = new ResourceManager(type.FullName, Assembly.GetAssembly(type));
        }

        /// <summary>
        /// 初始化WinFormLanguage类的新实例，并指定需要多国语言化的用户控件。
        /// </summary>
        /// <param name="control"></param>
        public WinFormLanguage(UserControl control)
        {
            this.isUserControl = true;
            this.languageControl = control;
            this.RM = new ResourceManager(this.languageControl.GetType().FullName, Assembly.GetAssembly(this.languageControl.GetType()));
        }
        /// <summary>
        /// 初始化WinFormLanguage类的新实例，并指定需要多国语言化的用户控件以及资源文件的类型。
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        public WinFormLanguage(UserControl control, Type type)
        {
            this.isUserControl = true;
            this.languageControl = control;
            this.RM = new ResourceManager(type.FullName, Assembly.GetAssembly(type));
        }
        #endregion

        //Methods
        /// <summary>
        /// 更换语言版本
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public void ChangeLanguage(LANGUAGES language)
        {
            switch (language)
            {
                case LANGUAGES.SimpleChinese:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
                    break;
                case LANGUAGES.TraditionalChinese:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-TW");
                    break;
                case LANGUAGES.English:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                    break;
                default:
                    break;
            }
        }

        #region 设置语言版本

        /// <summary>
        /// 设置窗体语言。
        /// </summary>
        public void SetLanguage()
        {
            this.SetBaseLanguage();
            this.SetThisLanguage();
        }
        /// <summary>
        /// 设置控件语言版本
        /// </summary>
        /// <param name="c"></param>
        private void SetLanguage(Control c)
        {
            string msg = string.Empty;
            if (c is DockContent)
            {
                WinFormLanguage lan = new WinFormLanguage((Form)c);
                if (((DockContent)c).TabPageContextMenuStrip != null)
                {
                    this.SetLanguage(((DockContent)c).TabPageContextMenuStrip);
                }
                msg = lan.GetMsg(((Form)c).Name);
                if (!string.IsNullOrEmpty(msg))
                {
                    ((DockContent)c).TabText = msg;
                }
                lan.SetLanguage();
            }
            else
            {
                if (c.ContextMenuStrip != null)
                {
                    this.SetLanguage(c.ContextMenuStrip);
                }

                switch (c.GetType().Name)
                {
                    // 如果是 Label 控件，防止因设置语言而导致的格式不整齐
                    case "Label":
                        msg = this.GetMsg(c.Name);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            if (msg.IndexOf("(&") > -1)
                            {  // Label 去除快捷键
                                msg = c.Text.Substring(0, c.Text.IndexOf("(&"));
                            }
                            c.Text = msg;
                            if (c.Tag != null)
                            {
                                int left = 0;
                                if (int.TryParse(c.Tag.ToString(), out left))
                                {
                                    c.Location = new System.Drawing.Point(left - c.Width, c.Location.Y);
                                }
                            }
                        }
                        break;
                    case "UCLabel":
                        UCLabel uclbl = c as UCLabel;
                        if (uclbl.NeedLanguage == true)
                        {
                            msg = this.GetMsg(c.Name);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                uclbl.Text = msg;
                            }
                            if (uclbl.LabelLocation != 0)
                            {
                                c.Location = new System.Drawing.Point(uclbl.LabelLocation - uclbl.Width, uclbl.Location.Y);
                            }
                        }
                        break;
                    case "UCButton":
                        msg = this.GetMsg(c.Name);
                        if (((UCButton)c).NeedLanguage)
                        {
                            if (!string.IsNullOrEmpty(msg))
                            {
                                c.Text = msg;
                            }
                        }
                        break;
                    case "UCCheckBox":
                        msg = this.GetMsg(c.Name);
                        if (((UCCheckBox)c).NeedLanguage)
                        {
                            if (!string.IsNullOrEmpty(msg))
                            {
                                c.Text = msg;
                            }
                        }
                        break;
                    case "Button":
                    case "CheckBox":
                    case "LinkLabel":
                    case "RadioButton":
                    case "GroupBox":
                    case "UCLeftMenuItem":
                    case "ToolStripMenuItem":
                        msg = this.GetMsg(c.Name);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            c.Text = msg;
                        }
                        break;
                    case "TabControl":
                        TabControl tbc = c as TabControl;
                        for (int j = 0; j < tbc.TabCount; j++)
                        {
                            msg = this.GetMsg(tbc.TabPages[j].Name);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                tbc.TabPages[j].Text = msg;
                            }
                        }
                        break;
                    case "DataGridView":
                        foreach (DataGridViewColumn col in ((DataGridView)c).Columns)
                        {
                            if (col.GetType() == typeof(DataGridViewCheckBoxColumn))
                            {
                                continue;
                            }
                            //
                            msg = this.GetMsg(col.Name);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                col.HeaderText = msg;
                            }
                        }
                        break;
                    case "ListView":
                        ListView lv = c as ListView;
                        if (lv.Items.Count > 0)
                        {
                            foreach (ListViewItem.ListViewSubItem item in lv.Items[0].SubItems)
                            {
                                msg = this.GetMsg(item.Name);
                                if (!string.IsNullOrEmpty(msg))
                                {
                                    item.Text = msg;
                                }
                            }
                        }
                        break;
                    case "ToolStrip":
                    case "StatusStrip":
                        foreach (ToolStripItem item in ((ToolStrip)c).Items)
                        {
                            if ((item.Tag + string.Empty).ToLower() != "false")
                            {
                                msg = this.GetMsg(item.Name);
                                if (!string.IsNullOrEmpty(msg))
                                {
                                    item.Text = msg;
                                }
                            }
                        }
                        break;
                    case "MenuStrip":
                    case "UCMenuStrip":
                    case "ContextMenuStrip":
                        foreach (ToolStripMenuItem item in ((ToolStrip)c).Items)
                        {
                            this.SetLanguage(item);
                        }
                        break;
                    case "TreeView":
                        foreach (TreeNode node in (c as TreeView).Nodes)
                        {
                            this.SetLanguage(node);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (c.Controls.Count > 0)
            {
                WinFormLanguage lan = null;
                foreach (Control ctrl in c.Controls)
                {
                    this.SetLanguage(ctrl);
                    if (ctrl is UserControl)
                    {
                        lan = new WinFormLanguage(ctrl as UserControl);
                        lan.SetLanguage();
                    }
                }
            }
        }
        /// <summary>
        /// 设置菜单语言
        /// </summary>
        /// <param name="parent"></param>
        private void SetLanguage(ToolStripMenuItem parent)
        {
            string msg = this.GetMsg(parent.Name);
            if (!string.IsNullOrEmpty(msg))
            {
                parent.Text = this.GetMsg(parent.Name);
            }
            if (parent.DropDownItems.Count > 0)
            {
                foreach (ToolStripItem item in parent.DropDownItems)
                {
                    if (item is ToolStripMenuItem)
                    {
                        this.SetLanguage((ToolStripMenuItem)item);
                    }
                }
            }
        }
        /// <summary>
        /// 设置树节点语言
        /// </summary>
        /// <param name="parent"></param>
        private void SetLanguage(TreeNode node)
        {
            string msg = this.GetMsg(node.Name);
            if (!string.IsNullOrEmpty(msg))
            {
                Match match = Regex.Match(node.Text, @"^[^\(]+?(?<num>\(\d+?\))$", RegexOptions.IgnoreCase);
                if (match != null && match.Success)
                {
                    node.Text = string.Format("{0} {1}", msg, match.Groups["num"].Value);
                }
                else
                {
                    node.Text = msg;
                }
            }
            foreach (TreeNode child in node.Nodes)
            {
                this.SetLanguage(child);
            }
        }

        /// <summary>
        /// 从当前窗体的基类窗体资源文件中设置窗体语言。
        /// </summary>
        public void SetBaseLanguage()
        {
            if (!this.isUserControl)
            {
                if (this.languageForm is BaseFrm)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageForm, typeof(BaseFrm));
                    lan.SetThisLanguage();
                }
                if (this.languageForm is BaseList)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageForm, typeof(BaseList));
                    lan.SetThisLanguage();
                }
                if (this.languageForm is BaseEdit)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageForm, typeof(BaseEdit));
                    lan.SetThisLanguage();
                }
                if (this.languageForm is BaseWorkflow)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageForm, typeof(BaseWorkflow));
                    lan.SetThisLanguage();
                }
                if (this.languageForm is BasePhase)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageForm, typeof(BasePhase));
                    lan.SetThisLanguage();
                }
            }
            else
            {
                if (this.languageControl is BaseUserControl)
                {
                    WinFormLanguage lan = new WinFormLanguage(this.languageControl, typeof(BaseUserControl));
                    lan.SetThisLanguage();
                }
            }
        }
        /// <summary>
        /// 从当前窗体的资源文件中设置窗体语言。
        /// </summary>
        public void SetThisLanguage()
        {
            if (!this.isUserControl)
            {
                this.languageForm.Text = this.GetMsg(this.languageForm.Name);
                if (this.languageForm is DockContent)
                {
                    if (((DockContent)this.languageForm).TabPageContextMenuStrip != null)
                    {
                        this.SetLanguage(((DockContent)this.languageForm).TabPageContextMenuStrip);
                    }
                }
                if (this.languageForm.ContextMenuStrip != null)
                {
                    this.SetLanguage(this.languageForm.ContextMenuStrip);
                }
                // 设置子控件
                for (int i = 0; i < this.languageForm.Controls.Count; i++)
                {
                    this.SetLanguage(this.languageForm.Controls[i]);
                }
            }
            else
            {
                this.languageControl.Text = this.GetMsg(this.languageControl.Name);
                if (this.languageControl.ContextMenuStrip != null)
                {
                    this.SetLanguage(this.languageControl.ContextMenuStrip);
                }
                // 设置子控件
                for (int i = 0; i < this.languageControl.Controls.Count; i++)
                {
                    this.SetLanguage(this.languageControl.Controls[i]);
                }
            }
        }

        #endregion
        #region 获取资源字符串内容

        /// <summary>
        /// 获取资源字符串内容
        /// </summary>
        /// <param name="receouseName"></param>
        /// <returns></returns>
        public string GetMsg(string receouseName)
        {
            return this.GetMsg(this.RM, receouseName);
        }

        /// <summary>
        /// 获取资源字符串内容
        /// </summary>
        /// <param name="rm"></param>
        /// <param name="receouseName"></param>
        /// <returns></returns>
        private string GetMsg(ResourceManager rm, string receouseName)
        {
            string msg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(receouseName))
                {
                    //排除启动异常
                    msg = rm.GetString(receouseName, Thread.CurrentThread.CurrentCulture);

                }
                else
                {
                    msg = "";
                }
            }
            catch
            {
                //Modified by Eric
                //修正因为缺失资源而产生错误信息的问题
                msg = null;
                //msg = "Cannot Found:" + receouseName + " , Please Add it to Resource File.";
            }
            return msg;
        }
        #endregion
        #region 获取本地设置的语言

        /// <summary>
        /// 获取本地设置的语言
        /// </summary>
        /// <returns></returns>
        public LANGUAGES LocalLanguage()
        {
            LANGUAGES language = LANGUAGES.NONE;
            //
            string filePath = Path.Combine(Application.StartupPath, "Language.xml");
            if (!File.Exists(filePath))
            {
                language = LANGUAGES.SimpleChinese; //默认中文简体
            }
            else
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(filePath);
                string value = xml.DocumentElement.InnerText.Trim();
                switch (value)
                {
                    case "SimpleChinese":
                        language = LANGUAGES.SimpleChinese;
                        break;
                    case "English":
                        language = LANGUAGES.English;
                        break;
                    case "TraditionalChinese":
                        language = LANGUAGES.TraditionalChinese;
                        break;
                    default:
                        language = LANGUAGES.SimpleChinese;
                        break;
                }
            }
            return language;
        }
        #endregion
    }
}