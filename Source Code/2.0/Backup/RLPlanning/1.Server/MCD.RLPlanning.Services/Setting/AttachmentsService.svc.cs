using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.Common.SRLS;
using MCD.RLPlanning.IServices.Setting;
using MCD.RLPlanning.Entity.Setting;

namespace MCD.RLPlanning.Services.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class AttachmentsService : BaseDAL<AttachmentsEntity>, IAttachmentsService
    {
        #region IAttachmentsService Members

        public byte[] SelectAttachment(CategoryType category, string objectID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataSet ds = db.ExecuteDataSet("dbo.usp_Sys_SelectAttachment", category.ToString(), objectID);
            return Serilize(ds);
        }

        public void InsertAttachment(MCD.RLPlanning.Entity.Setting.AttachmentsEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Sys_InsertAttachment");
            db.AddInParameter(cmd, "@Category", DbType.String, entity.Category.ToString());
            db.AddInParameter(cmd, "@ObjectID", DbType.String, entity.ObjectID);
            db.AddInParameter(cmd, "@FileName", DbType.String, entity.FileName);
            db.AddInParameter(cmd, "@FileType", DbType.String, entity.FileType);
            db.AddInParameter(cmd, "@FileSize", DbType.Int32, entity.FileSize);
            db.AddInParameter(cmd, "@FilePath", DbType.String, entity.FilePath);
            db.AddInParameter(cmd, "@CreateUserID", DbType.String, entity.CreateUserID);
            db.AddInParameter(cmd, "@CreateUserName", DbType.String, entity.CreateUserName);
            db.ExecuteNonQuery(cmd);
        }

        public void UpdateAttachmentObjectID(CategoryType category, string objectID, string tempObjectID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Sys_UpdateAttachmentObjectID", category.ToString(), objectID, tempObjectID);
        }

        public void DeleteAttachment(string id)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            db.ExecuteNonQuery("dbo.usp_Sys_DeleteAttachment", id);
        }
        #endregion
    }
}