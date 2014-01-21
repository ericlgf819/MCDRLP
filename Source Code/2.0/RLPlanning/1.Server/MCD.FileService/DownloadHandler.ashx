<%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Web;
using System.IO;

public class DownloadHandler : IHttpHandler
{
    //Properties
    public bool IsReusable
    {
        get { return true; }
    }
    
    /// <summary>
    ///功能说明：文件下载类--不管是什么格式的文件,都能够弹出打开/保存窗口,
    ///包括使用下载工具下载
    ///继承于IHttpHandler接口，可以用来自定义HTTP 处理程序同步处理HTTP的请求
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
            filepath = Path.Combine(context.Server.MapPath(".") + @"\File", filepath);
            iStream = new System.IO.FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
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
            byte[] buffer = new Byte[10240];
            int length;
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
        context.Response.Write("执行完毕");
    }
}