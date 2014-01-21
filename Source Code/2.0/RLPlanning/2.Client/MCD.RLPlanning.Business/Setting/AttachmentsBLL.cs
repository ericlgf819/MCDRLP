using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;
using System.Collections.Specialized;

using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Setting;
using MCD.RLPlanning.IServices.Setting;

namespace MCD.RLPlanning.BLL.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class AttachmentsBLL : BaseBLL<IAttachmentsService>
    {
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="category">类别</param>
        /// <param name="objectID">业务主键</param>
        /// <returns></returns>
        public DataSet SelectAttachment(CategoryType category, string objectID)
        {
            return base.DeSerilize(base.WCFService.SelectAttachment(category, objectID));
        }

        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="entity"></param>
        public void InsertAttachment(AttachmentsEntity entity)
        {
            base.WCFService.InsertAttachment(entity);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAttachment(string id)
        {
            base.WCFService.DeleteAttachment(id);
        }

        /// <summary>
        /// 更新关联主键ID
        /// </summary>
        public void UpdateAttachmentObjectID(CategoryType category, string objectID,string tempObjectID)
        {
            base.WCFService.UpdateAttachmentObjectID(category, objectID, tempObjectID);
        }

        /// <summary>
        /// 删除文件服务器上指定位置的文件。
        /// </summary>
        /// <param name="path">附件路径，格式"\\Remind\\49f23c66-ce0a-443b-a790-3dab11971b90.jpg"</param>
        /// <returns></returns>
        public static bool DeleteFileByPath(string path)
        {
            string url = ConfigurationManager.AppSettings["AttachURL"];
            if (url.EndsWith("/"))
            {
                url.TrimEnd('/');
            }
            url = string.Format("{0}/UploadHandler.ashx", url);
            NameValueCollection val = new NameValueCollection();
            val.Add("action", "delete");
            val.Add("path", path);
            //
            byte[] data = null;
            string result = string.Empty;
            using (WebClient client = new WebClient())
            {
                data = client.UploadValues(url, "POST", val);
                result = System.Text.Encoding.Default.GetString(data);
            }
            //
            if (!string.IsNullOrEmpty(result) && result.Equals("DELETE_FILE_SUCC"))
            {
                return true;
            }
            return false;
        }
    }
}