using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Xml;
using System.DirectoryServices;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MCD.RLPlanning.ServiceControl
{
    /// <summary>
    /// 
    /// </summary>
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        #region ctor

        public ServiceInstaller()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        /// <summary>
        /// 安装之后
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnAfterInstall(IDictionary savedState)
        {
            try
            {
                string targetdir = this.Context.Parameters["targetdir"];
                string dataSource = this.Context.Parameters["dataSource"];
                string initial = this.Context.Parameters["initial"];
                string userID = this.Context.Parameters["userID"];
                string password = this.Context.Parameters["password"];
                //
                string config = Path.Combine(targetdir, "Web.config");
                if (File.Exists(config))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(config);
                    //
                    XmlNode node = xml.DocumentElement.SelectSingleNode("appSettings/add[@key='XmlConfigPath']");
                    if (node != null && node.Attributes["value"] != null)
                    {
                        node.Attributes["value"].Value = targetdir + "XmlConfig";
                    }
                    node = xml.DocumentElement.SelectSingleNode("appSettings/add[@key='ServerSavePath']");
                    if (node != null && node.Attributes["value"] != null)
                    {
                        node.Attributes["value"].Value = targetdir + "UploadFiles";
                    }
                    node = xml.DocumentElement.SelectSingleNode("connectionStrings/add[@name='DBConnection']");
                    if (node != null && node.Attributes["connectionString"] != null)
                    {
                        node.Attributes["connectionString"].Value = 
                            string.Format("Data Source={0};Initial Catalog={1};User id={2};Password={3};", 
                            dataSource, initial, userID, password);
                    }
                    xml.Save(config);
                }
            }
            catch { }
            //
            base.OnAfterInstall(savedState);
        }
        /// <summary>
        /// 卸载之后，删除虚拟目录
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnAfterUninstall(IDictionary savedState)
        {
            try
            {
                string targetdir = this.Context.Parameters["targetdir"];
                if (targetdir.EndsWith("\\") || targetdir.EndsWith("/"))
                {
                    targetdir = targetdir.Substring(0, targetdir.Length - 1);
                }
                //
                string virtualPath = Path.GetFileName(targetdir);
                this.DeleteVirtualPath(virtualPath);
            }
            catch { }
            //
            base.OnAfterUninstall(savedState);
        }

        //Methods
        /// <summary>
        /// 删除虚拟目录
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public bool DeleteVirtualPath(string virtualPath)
        {
            DirectoryEntry iisServer = null, folderRoot = null;
            try
            {
                iisServer = new DirectoryEntry("IIS://LocalHost/W3SVC/1");
                folderRoot = iisServer.Children.Find("Root", "IIsWebVirtualDir");
                //
                object[] paras = new object[2];
                paras[0] = "IIsWebVirtualDir"; //表示操作的是虚拟目录
                paras[1] = virtualPath;
                folderRoot.Invoke("Delete", paras);
                //
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (folderRoot != null)
                {
                    folderRoot.CommitChanges();
                }
                if (iisServer != null)
                {
                    iisServer.CommitChanges();
                }
            }
        }
        ///// <summary>
        ///// 删除文件夹
        ///// </summary>
        ///// <param name="parentPath"></param>
        //protected void DeleteDirectory(string parentPath)
        //{
        //    // 删除文件
        //    string[] files = Directory.GetFiles(parentPath);
        //    foreach (string file in files)
        //    {
        //        this.DeleteFile(file);
        //    }
        //    // 递归删除子文件夹
        //    string[] directorys = Directory.GetDirectories(parentPath);
        //    foreach (string directory in directorys)
        //    {
        //        this.DeleteDirectory(directory);
        //    }
        //    // 删除当前文件夹
        //    Directory.Delete(parentPath);
        //}
        ///// <summary>
        ///// 删除文件
        ///// </summary>
        ///// <param name="file"></param>
        //protected void DeleteFile(string file)
        //{
        //    try
        //    {
        //        File.Delete(file);
        //    }
        //    catch { }
        //}
    }
}