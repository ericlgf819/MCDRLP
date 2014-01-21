using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ISelectService : IBaseService
    {
        /// <summary>
        /// 选择租金类型
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectRentType();
        /// <summary>
        /// 选择实体类型
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectEntityType();
        /// <summary>
        /// 选择Typecode状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectTypeCodeStatus();

        /// <summary>
        /// 选择激活状态的ACCOUNT
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectActiveAccount();

        /// <summary>
        /// 获取指定键名称的数据字典项集合。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [OperationContract]
        List<DictionaryItem> GetItemsByKeyName(string keyName);
        /// <summary>
        /// 获取指定键值的数据字典项集合。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        List<DictionaryItem> GetItemsByKeyValue(string keyValue);
    }
}