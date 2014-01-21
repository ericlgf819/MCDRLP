using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MCD.Controls
{
    /// <summary>
    /// Xml 菜单信息读取
    /// </summary>
    public class SysMenuHandler : XmlDocument
    {
        //Fields
        private static SysMenuHandler m_instance = null;
        private string m_systemMenuPath = string.Empty;
        private XmlNode m_document = null;
        /// <summary>
        /// 默认菜单高度
        /// </summary>
        private const int MENU_ITEM_HEIGHT = 22;

        //Properties
        public static SysMenuHandler Instance
        {
            get
            {
                if (SysMenuHandler.m_instance == null)
                {
                    SysMenuHandler.m_instance = new SysMenuHandler();
                }
                return SysMenuHandler.m_instance;
            }
        }
        /// <summary>
        /// 菜单配置性文件路径
        /// </summary>
        private string SystemMenuPath
        {
            get
            {
                if (this.m_systemMenuPath == string.Empty)
                {
                    this.m_systemMenuPath = Path.Combine(Application.StartupPath, "SystemMenu.xml");
                }
                return this.m_systemMenuPath;
            }
        }
        /// <summary>
        /// 根节点
        /// </summary>
        private XmlNode Document
        {
            get
            {
                if (this.m_document == null)
                {
                    string xml = string.Empty;
                    if (!File.Exists(this.SystemMenuPath))
                    {
                        xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                        xml += "<Menu>";
                        xml += "<Menu name=\"MasterMenu\" text=\"基本信息\" shortKey=\"Alt+M\" sort=\"0\">";
                        xml += "<Menu name=\"DeptInfo\" text=\"部门信息\" shortKey=\"Ctrl+D\" height=\"20\" form=\"\"/>";
                        xml += "</Menu>";
                        xml += "</Menu>";
                        //
                        base.LoadXml(xml);
                    }
                    else
                    {
                        base.Load(this.SystemMenuPath);
                    }
                    //
                    this.m_document = (XmlNode)base.DocumentElement;
                }
                return this.m_document;
            }
        }

        //Methods
        /// <summary>
        /// 跟进 Name 获取菜单节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MenuItem GetMenuItemByName(string name)
        {
            XmlNode itemNode = this.Document.SelectSingleNode("//Menu/*[@name ='" + name + "']");
            if (itemNode == null)
            {
                return null;
            }
            return this.LoadMenu(itemNode);
        }
        /// <summary>
        /// 获取一级菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetRootMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            XmlNodeList nodes = this.Document.SelectNodes("Menu");
            foreach (XmlNode node in nodes)
            {
                items.Add(this.LoadMenu(node));
            }
            return items;
        }
        /// <summary>
        /// 加载菜单节点信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private MenuItem LoadMenu(XmlNode node)
        {
            MenuItem item = new MenuItem()
            {
                Name = node.Attributes["name"].Value + string.Empty,
                Text = node.Attributes["text"].Value + string.Empty,
                Form = node.Attributes["form"] == null ? string.Empty : node.Attributes["form"].Value,
                ShortKey = node.Attributes["shortKey"] == null ? string.Empty : node.Attributes["shortKey"].Value,
                Visible = this.GetAttributesBooleanValue(node, "visible", true),    // 是否显示
                AutoOpen = this.GetAttributesBooleanValue(node, "autoOpen"),        // 默认打开
                ShowLeft = this.GetAttributesBooleanValue(node, "showLeft", true),  // 左边菜单
                Expand = this.GetAttributesBooleanValue(node, "expand"),            // 默认展开
                OpenMethod = this.GetAttributesOpenValue(node, "openMethod"),       // 打开方式
                Height = this.GetAttributesIntValue(node, "height")                 // 菜单高度
            };
            // 有子节点，加载子节点菜单
            if (node.HasChildNodes)
            {
                List<MenuItem> items = new List<MenuItem>();
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.NodeType == XmlNodeType.Comment)
                        continue;

                    // 递归加载子菜单的节点
                    items.Add(this.LoadMenu(n));
                }
                item.Menus = items;
            }
            //
            return item;
        }

        /// <summary>
        /// 将节点属性转换为布尔值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        private bool GetAttributesBooleanValue(XmlNode node, string attrName)
        {
            return this.GetAttributesBooleanValue(node, attrName, false);
        }
        /// <summary>
        /// 将节点属性转换为布尔值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool GetAttributesBooleanValue(XmlNode node, string attrName, bool defaultValue)
        {
            bool boolValue = defaultValue;
            if (node.Attributes[attrName] != null && !bool.TryParse(node.Attributes[attrName].Value, out boolValue))
            {
                boolValue = defaultValue;
            }
            return boolValue;
        }
        /// <summary>
        /// 将节点值转换为整型字符
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        private int GetAttributesIntValue(XmlNode node, string attrName)
        {
            int intValue = SysMenuHandler.MENU_ITEM_HEIGHT;
            if (node.Attributes[attrName] != null && !int.TryParse(node.Attributes[attrName].Value, out intValue))
            {
                intValue = SysMenuHandler.MENU_ITEM_HEIGHT;
            }
            return intValue;
        }
        /// <summary>
        /// 将节点值转换为 FORM_OPEN
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        private FORM_OPEN GetAttributesOpenValue(XmlNode node, string attrName)
        {
            FORM_OPEN openMethod = FORM_OPEN.Default;
            if (node.Attributes[attrName] != null)
            {
                if (node.Attributes[attrName].Value.ToLower().ToString() == "show")
                {
                    openMethod = FORM_OPEN.Show;
                }
                else if (node.Attributes[attrName].Value.ToLower().ToString() == "dialog")
                {
                    openMethod = FORM_OPEN.ShowDialog;
                }
                else if (node.Attributes[attrName].Value.ToLower().ToString() == "process")
                {
                    openMethod = FORM_OPEN.Process;
                }
            }
            return openMethod;
        }
    }

    /// <summary>
    /// 菜单实体
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 生成控件的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示在菜单中的标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 菜单打开的窗体或应用程序
        /// </summary>
        public string Form { get; set; }
        /// <summary>
        /// 菜单是否默认打开
        /// </summary>
        public bool AutoOpen { get; set; }
        /// <summary>
        /// 菜单打开的方式
        /// </summary>
        public FORM_OPEN OpenMethod { get; set; }
        /// <summary>
        /// 左边菜单是否默认展开
        /// </summary>
        public bool Expand { get; set; }
        /// <summary>
        /// 快捷键
        /// </summary>
        public string ShortKey { get; set; }
        /// <summary>
        /// 左边菜单显示高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 是否显示在左边树形菜单中
        /// </summary>
        public bool ShowLeft { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuItem> Menus { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; }
    }

    /// <summary>
    /// 窗体打开方式
    /// </summary>
    public enum FORM_OPEN
    {
        /// <summary>
        /// 模态窗体
        /// </summary>
        ShowDialog,
        /// <summary>
        /// 非模态窗体
        /// </summary>
        Show,
        /// <summary>
        /// 新的应用程序
        /// </summary>
        Process,
        /// <summary>
        /// 默认方式
        /// </summary>
        Default
    }
}