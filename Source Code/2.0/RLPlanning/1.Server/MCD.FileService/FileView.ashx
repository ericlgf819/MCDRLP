<%@ WebHandler Language="C#" Class="FileView.FileViewHandler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace FileView
{
    public class FileViewHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 更新文件所在的虚拟目录。
        /// </summary>
        private string AppUpdatePath
        {
            get { return ConfigurationManager.AppSettings["AppUpdatePath"].ToString(); }
        }

        /// <summary>
        /// 处理自动更新文件请求。
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            string key = "MCDRL_UPDATE_FILES";

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            object data = cache.Get(key);
            if (data != null)
            {
                result = data.ToString();
            }
            else
            {
                string path = this.AppUpdatePath;
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
                if (path.IndexOf(":") < 0)
                {
                    path = context.Server.MapPath(path);
                }
                List<UpdateFile> files = this.GetServerFiles(path);
                result = this.Serialize(files);
                if (!string.IsNullOrEmpty(result))
                {
                    System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(path);
                    cache.Insert(key, result, dependency);
                }
            }
            
            context.Response.Clear();
            if (!string.IsNullOrEmpty(result))
            {
                context.Response.Write(result);
            }
            else
            {
                context.Response.Write("<span style=\"color:red;font-size:20px;\">服务端更新目录不存在或无任何文件！</span>");
            }
            context.Response.End();
        }

        /// <summary>
        /// 返回指定目录下的文件表。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<UpdateFile> GetServerFiles(string path)
        {
            if (!Directory.Exists(path))
                return null;

            List<FileInfo> files = this.GetFiles(new DirectoryInfo(path));
            if (files == null)
                return null;

            List<UpdateFile> list = new List<UpdateFile>(files.Count);
            string filePath = string.Empty;
            string webRoot = HttpContext.Current.Server.MapPath(AppUpdatePath);
            if (!webRoot.EndsWith(@"\"))
                webRoot += @"\";

            foreach (FileInfo file in files)
            {
                filePath = file.FullName.Replace(webRoot, "/").Replace(@"\", "/");

                UpdateFile f = new UpdateFile();
                f.FileName = file.FullName;
                f.FilePath = filePath;
                f.Size = file.Length;
                list.Add(f);
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
        /// 将指定对象序列化为xml。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string Serialize(Object o)
        {
            string xml = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                using (MemoryStream mem = new MemoryStream())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                        n.Add("", "");
                        serializer.Serialize(writer, o, n);

                        mem.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(mem))
                        {
                            xml = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch { xml = ""; }
            return xml;
        }
    }

    /// <summary>
    /// 待更新的文件信息。
    /// </summary>
    [Serializable]
    public class UpdateFile
    {
        private string fileName = string.Empty;
        private string filePath = string.Empty;
        private long size = 0;

        /// <summary>
        /// 文件名。
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// 路径。
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        /// <summary>
        /// 文件大小。
        /// </summary>
        public long Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
