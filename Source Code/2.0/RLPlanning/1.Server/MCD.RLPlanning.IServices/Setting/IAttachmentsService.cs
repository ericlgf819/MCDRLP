using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.Entity.Setting;
using MCD.Common.SRLS;

namespace MCD.RLPlanning.IServices.Setting
{
    /// <summary>
    /// NOTE: If you change the class name "AttachmentsService" here, you must also update the reference to "AttachmentsService" in Web.config.
    /// </summary>
    [ServiceContract]
    public interface IAttachmentsService : IBaseService
    {
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="category">类别</param>
        /// <param name="objectID">业务主键</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAttachment(CategoryType category, string objectID); 

        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertAttachment(AttachmentsEntity entity);

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="id"></param>
        [OperationContract]
        void DeleteAttachment(string id);

        /// <summary>
        /// 更新关联主键ID
        /// </summary>
        [OperationContract]
        void UpdateAttachmentObjectID(CategoryType category, string objectID, string tempObjectID);
    }
}