using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Web;

namespace LiveUpdate.App
{
    /// <summary>
    /// 提供文件版本检测以及自动更新的方法的客户端调用。
    /// </summary>
    public class UpdateClient
    {
        //Fields
        /// <summary>
        /// 服务端返回更新文件地址。
        /// </summary>
        private static readonly string updateHandlerName = "LiveUpdateHandler.ashx";
        private static readonly string dowloadHandlerName = "LiveUpdateDownloadHandler.ashx?FileName={0}&FilePath={1}";

        //Properties
        /// <summary>
        /// 获取或设置在线更新服务器地址。
        /// </summary>
        public string LiveUpdateServer { get; set; }
        /// <summary>
        /// 获取或设置客户端程序安装目录名称。
        /// </summary>
        public string ClientSetupPath { get; set; }
        /// <summary>
        /// 获取待更新的服务器文件集合。
        /// </summary>
        public List<UpdateFile> UpdateFiles
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取或设置是否有文件需要更新。
        /// </summary>
        public bool NeedUpdate
        {
            get { return (this.UpdateFiles != null && this.UpdateFiles.Count > 0); }
        }

        //Methods
        /// <summary>
        /// 检查更新并加载更新文件。
        /// </summary>
        public void CheckUpdate()
        {
            this.UpdateFiles = new List<UpdateFile>();
            //
            List<UpdateFile> localFiles = this.GetLocalFiles();
            List<UpdateFile> serverFiles = this.GetServerFiles();
            if (serverFiles != null && serverFiles.Count > 0)
            {
                if (localFiles == null || localFiles.Count <= 0)
                {
                    serverFiles.ForEach(item => this.UpdateFiles.Add(item));
                }
                else
                {
                    foreach (UpdateFile serverFile in serverFiles)
                    {
                        if (!localFiles.Exists(item => item.FilePath.Equals(serverFile.FilePath)
                            && item.LastUpdateTime.Equals(serverFile.LastUpdateTime)))
                        {
                            this.UpdateFiles.Add(serverFile);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 异步下载文件指定文件。
        /// </summary>
        /// <param name="file">待下载的文件实体</param>
        /// <param name="progressChanged">下载进度变化时候执行的委托</param>
        /// <param name="completed">下载完成后执行的委托</param>
        public void DownloadFile(UpdateFile file, Action<DownloadProgressChangedEventArgs> progressChanged, 
            Action<AsyncCompletedEventArgs> completed)
        {
            if (string.Compare(file.FileName, "MCD.RLPlanning.LiveUpdate.exe") == 0) return;
            // 文件保存路径
            string savePath = Environment.CurrentDirectory.TrimEnd('\\') + @"\" + file.FilePath.Replace("/", @"\").TrimStart('\\');
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            file.SavePath = savePath;
            // 文件URL
            string server = this.LiveUpdateServer;
            if (!server.EndsWith("/"))
            {
                server += "/";
            }
            string url = string.Format(server + UpdateClient.dowloadHandlerName, HttpUtility.UrlEncode(file.FileName), HttpUtility.UrlEncode(file.FilePath));
            using (WebClient client = new WebClient())
            {
                // 报告下载进度
                client.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) => {
                    progressChanged(e);
                };
                // 下载完成
                client.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) => {
                    // 文件下载完后更新下载文件的最后修改时间
                    UpdateFile update = e.UserState as UpdateFile;
                    if (update != null)
                    {
                        if (File.Exists(update.SavePath))
                        {
                            try
                            {
                                File.SetLastWriteTime(update.SavePath, update.LastUpdateTime);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                //
                            }
                        }
                    }
                    //
                    completed(e);
                };
                // 开始异步下载----------------------------------------
                client.DownloadFileAsync(new Uri(url), savePath, file);
            }
        }

        /// <summary>
        /// 获取服务器文件信息。
        /// </summary>
        /// <returns></returns>
        public List<UpdateFile> GetServerFiles()
        {
            List<UpdateFile> list = null;
            try
            {
                string server = this.LiveUpdateServer;
                if (!server.EndsWith("/"))
                {
                    server += "/";
                }
                string url = server + UpdateClient.updateHandlerName;
                //
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.CookieContainer = new CookieContainer();
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                req.KeepAlive = true;
                req.Timeout = 10000000;
                //
                string xml = null;
                using (HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        xml = reader.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(xml))
                {
                    list = new List<UpdateFile>();
                    object o = this.Deserialize(list.GetType(), xml);
                    if (o != null)
                    {
                        list = o as List<UpdateFile>;
                    }
                }
            }
            catch 
            {
                list = null;
            }
            return list;
        }
        /// <summary>
        /// 返回指定目录下的文件表。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<UpdateFile> GetLocalFiles()
        {
            string clientPath = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(this.ClientSetupPath))
            {
                clientPath = this.ClientSetupPath;
            }
            if (!Directory.Exists(clientPath))
            {
                Directory.CreateDirectory(clientPath);
            }
            //
            DirectoryInfo dir = new DirectoryInfo(clientPath);
            List<FileInfo> files = this.GetFiles(dir);
            if (files == null)
            {
                return null;
            }
            //
            List<UpdateFile> list = new List<UpdateFile>(files.Count);
            foreach (FileInfo file in files)
            {
                list.Add(new UpdateFile()
                {
                    FileName = file.Name,
                    FilePath = file.FullName.Replace(dir.FullName, "").Replace(@"\", "/"),
                    Size = file.Length,
                    LastUpdateTime = file.LastWriteTime
                });
            }
            return list;
        }
        /// <summary>
        /// 递归获取指定目录下的所有文件（包括子目录）。
        /// </summary>
        /// <param name="dir">待获取的目录</param>
        /// <returns>返回文件集合</returns>
        private List<FileInfo> GetFiles(DirectoryInfo dir)
        {
            List<FileInfo> list = new List<FileInfo>();
            //
            FileInfo[] files = dir.GetFiles();
            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    list.Add(file);
                }
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (dirs.Length > 0)
            {
                foreach (DirectoryInfo child in dirs)
                {
                    list.AddRange(this.GetFiles(child));
                }
            }
            return list;
        }

        /// <summary>
        /// 将指定的xml格式的字符反序列化为对应的对象并返回。
        /// </summary>
        /// <param name="t">对象的类型</param>
        /// <param name="xml">待反序列化的xml格式的字符的内容</param>
        /// <returns>返回对应的对象</returns>
        private Object Deserialize(Type t, string xml)
        {
            Object o = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(t);
                using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    o = serializer.Deserialize(mem);
                }
            }
            catch
            {
                o = null;
            }
            return o;
        }
    }
}