using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices.Master
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IAccountService:IBaseService
    {
        /// <summary>
        /// 查找所有科目信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllAccount(AccountEntity entity);
    }
}