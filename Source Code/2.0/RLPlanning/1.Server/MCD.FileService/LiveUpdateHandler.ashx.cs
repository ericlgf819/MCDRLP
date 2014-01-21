using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace MCD.FileService
{
    /// <summary>
    /// 在线更新服务器应用程序文件请求处理。
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LiveUpdateHandler : IHttpHandler
    {
        //Properties
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
                    System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(path);//Cach
                    //
                    cache.Insert(key, result, dependency);
                }
            }
            //
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
            {
                return null;
            }
            //
            List<FileInfo> files = this.GetFiles(new DirectoryInfo(path));
            if (files == null)
            {
                return null;
            }
            //
            string webRoot = HttpContext.Current.Server.MapPath(this.AppUpdatePath);
            if (!webRoot.EndsWith(@"\"))
            {
                webRoot += @"\";
            }
            //
            List<UpdateFile> list = new List<UpdateFile>(files.Count);
            foreach (FileInfo file in files)
            {
                list.Add(new UpdateFile() { 
                    FileName = file.Name, 
                    FilePath = file.FullName.Replace(webRoot, "/").Replace(@"\", "/"), 
                    Size = file.Length, 
                    LastUpdateTime = file.LastWriteTime });
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
        /// 将指定对象序列化为xml。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string Serialize(Object o)
        {
            string xml = string.Empty;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                using (MemoryStream mem = new MemoryStream())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        //
                        XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                        n.Add(string.Empty, string.Empty);
                        serializer.Serialize(writer, o, n);
                        //
                        mem.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(mem))
                        {
                            xml = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                xml = string.Empty;
            }
            return xml;
        }
    } 

    #region 文件实体

    /// <summary>
    /// 待更新的文件信息。
    /// </summary>
    [Serializable]
    public class UpdateFile
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
    #endregion
}