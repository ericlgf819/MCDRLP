<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class UploadHandler : IHttpHandler 
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
    /// 附件上传删除Http请求处理程序。
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files != null && context.Request.Files.Count > 0)
        {
            //接收上传的文件
            foreach (string fileKey in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[fileKey];
                //
                string path = context.Request["Path"];
                string fileName = Path.Combine(context.Server.MapPath(".") + @"\File", path);
                if (Directory.Exists(Path.GetDirectoryName(fileName)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                file.SaveAs(fileName);
            }
            context.Response.Write("UPLOAD_SUCC");//上传成功
        }
        else
        {
            //处理普通请求
            if (context.Request.HttpMethod.ToUpper() == "POST")
            {
                //处理普通POST请求
                if (context.Request.Form["action"] == null)
                {
                    context.Response.Write("Missing parameter:'action'");
                    context.Response.End();
                    return;
                }
                //
                string action = context.Request.Form["action"].ToUpper();
                switch (action)
                {
                    //删除指定位置的文件
                    case "DELETE":
                        if (context.Request.Form["path"] == null)
                        {
                            context.Response.Write("Missing parameter:'path'");
                            context.Response.End();
                            return;
                        }
                        //
                        string path = context.Request.Form["Path"];
                        string fileName = Path.Combine(context.Server.MapPath(".") + @"\File", path);
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                            //
                            context.Response.Write("DELETE_FILE_SUCC");
                        }
                        else
                        {
                            context.Response.Write("FILE_NOT_EXISTS");
                        }
                        break;
                    default: break;
                }
            }
        }
        context.Response.End();
    }
}