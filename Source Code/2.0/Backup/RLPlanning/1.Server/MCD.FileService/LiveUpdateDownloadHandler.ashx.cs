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
    /// 更新文件下载请求处理。
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LiveUpdateDownloadHandler : IHttpHandler
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
        /// 更新文件目录的虚拟路径。
        /// </summary>
        private string AppUpdatePath
        {
            get { return ConfigurationManager.AppSettings["AppUpdatePath"].ToString(); }
        }

        /// <summary>
        /// 更新文件下载请求处理。
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;
            //
            System.IO.Stream iStream = null;
            try
            {
                string filename = context.Request["FileName"];
                string filepath = context.Request["FilePath"];
                filepath = context.Server.MapPath(this.AppUpdatePath) + @"\" + filepath.Replace("/", @"\");
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                Response.Clear();
                //
                long dataToRead = iStream.Length;
                long p = 0;
                if (Request.Headers["Range"] != null)
                {
                    Response.StatusCode = 206;
                    p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                }
                if (p != 0)
                {
                    Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }
                Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding(65001).GetBytes(Path.GetFileName(filename))));
                //
                iStream.Position = p;
                dataToRead = dataToRead - p;
                int length;
                byte[] buffer = new Byte[10240];
                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10240);
                        //
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();
                        //
                        buffer = new Byte[10240];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }
                Response.End();
            }
        }
    }
}
