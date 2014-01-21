using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Windows.Forms;

using MCD.Controls;
using MCD.DockContainer.Docking;
using MCD.Framework.AppCode;
using MCD.RLPlanning.Client.Common;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 窗口处理类
    /// </summary>
    public class FormHandler : IFormHandler
    {
        //Fields
        private int windowsCount = 0;

        //Properties
        /// <summary>
        /// 控制 DockPanel 打开的窗口数量
        /// </summary>
        protected int WindowsCount
        {
            get
            {
                if (this.windowsCount == 0)
                {
                    if (!int.TryParse(ConfigurationManager.AppSettings["WindowsCount"] + string.Empty, out this.windowsCount))
                    {
                        this.windowsCount = 5;
                    }
                }
                return this.windowsCount;
            }
        }
        /// <summary>
        /// 设置语言
        /// </summary>
        private WinFormLanguage Language { get; set; }
        #region ctor

        public FormHandler(WinFormLanguage language)
        {
            this.Language = language;
        }
        #endregion

        /// <summary>
        /// 打开窗体
        /// </summary>
        /// <param name="name"></param>
        /// <param name="panel"></param>
        public void OpenForm(string name, DockPanel panel)
        {
            MCD.Controls.MenuItem item = SysMenuHandler.Instance.GetMenuItemByName(name);
            //
            this.OpenForm(item.Form, item.Text, panel, FORM_OPEN.Default);
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="displayName"></param>
        /// <param name="panel"></param>
        /// <param name="method"></param>
        public void OpenForm(string classType, string displayName, DockPanel panel, FORM_OPEN method)
        {
            Type type;
            Form frm;
            switch (method)
            {
                case FORM_OPEN.Default:
                    displayName = this.Language.GetMsg(classType.Substring(classType.LastIndexOf(".") + 1));
                    //
                    this.OpenForm(classType, displayName, panel);
                    break;
                case FORM_OPEN.ShowDialog:
                    type = Type.GetType(classType);
                    frm = Activator.CreateInstance(type) as Form;
                    frm.ShowDialog();
                    break;
                case FORM_OPEN.Show:
                    type = Type.GetType(classType);
                    frm = Activator.CreateInstance(type) as Form;
                    frm.Show();
                    break;
                case FORM_OPEN.Process:
                    Process.Start(classType);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="classType">命名空间.类名</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="panel">DockPanel</param>
        public void OpenForm(string classType, string displayName, DockPanel panel)
        {
            Type type = Type.GetType(classType);
            DockContent dockContent = Activator.CreateInstance(type) as DockContent; //实例化
            dockContent.TabText = displayName;
            dockContent.Tag = classType;
            DockContent exists = this.ExistDockContent(dockContent, panel);
            if (exists != null)
            {
                exists.Show(panel);
            }
            else
            {
                dockContent.Show(panel);
            }
            // 控制大于 5 个窗体时，关闭之前的
            if (panel.Contents.Count > this.WindowsCount)
            {
                panel.Contents[0].DockHandler.Close();
            }
        }

        /// <summary>
        /// 关闭指定类型的Panel
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="panel"></param>
        public void CloseForm(string classType, DockPanel panel)
        {
            IDockContent needtoclose = null;
            foreach (var item in panel.Contents)
            {
                if (classType == item.DockHandler.Form.Name)
                    needtoclose = item;
            }

            if (null != needtoclose)
                needtoclose.DockHandler.Close();
        }

        /// <summary>
        /// 检查窗体是否已经打开，如果打开，返回已经打开的窗体
        /// </summary>
        /// <param name="checkContent"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        public DockContent ExistDockContent(DockContent checkContent, DockPanel panel)
        {
            DockContent exist = null;
            foreach (DockContent content in panel.Contents)
            {
                if (string.Compare(content.Tag + String.Empty, checkContent.Tag + String.Empty, false) == 0)
                {
                    exist = content;
                    break;
                }
            }
            return exist;
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="name">菜单名</param>
        /// <param name="sender"></param>
        public void MenuClick(string name, object sender)
        {
            switch (name)
            {
                case "SimpleChinese":
                    this.SetLocal(name);
                    this.SetLanguage(sender as Form, LANGUAGES.SimpleChinese);
                    break;
                case "English":
                    this.SetLocal(name);
                    this.SetLanguage(sender as Form, LANGUAGES.English);
                    break;
                case "TraditionalChinese":
                    this.SetLocal(name);
                    this.SetLanguage(sender as Form, LANGUAGES.TraditionalChinese);
                    break;
                case "Help":
                    string path = string.Empty;
                    if ((sender as Form).Text.IndexOf("MCD", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\Template\User Manual en.pdf";
                    }
                    else
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\Template\User Manual.pdf";
                    }
                    if (File.Exists(path))
                    {
                        Process.Start(path);
                    }
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 设置窗体语言
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="language"></param>
        public void SetLanguage(Form frm, LANGUAGES language)
        {
            WinFormLanguage lan = new WinFormLanguage(frm);
            lan.ChangeLanguage(language);
            lan.SetLanguage();
        }
        /// <summary>
        /// 设置本地化语言
        /// </summary>
        /// <param name="languageValue"></param>
        public void SetLocal(string languageValue)
        {
            XmlDocument xml = null;
            //
            string filePath = Path.Combine(Application.StartupPath, "Language.xml");
            if (File.Exists(filePath))
            {
                try
                {
                    // 防止文件出错导致永远无法访问该文件
                    xml = new XmlDocument();
                    xml.Load(filePath);
                }
                catch
                {
                    File.Delete(filePath);
                }
            }
            if (xml == null)
            {
                xml = new XmlDocument();
                xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Language/>");
            }
            xml.DocumentElement.InnerText = languageValue;
            xml.Save(filePath);
        }
    }
}