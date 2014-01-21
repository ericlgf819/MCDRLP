using System;
using System.ServiceModel;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IBaseService
    {
        /// <summary>
        /// 该方法用于检测 WCF 服务是否正常，并且用于返回服务器端时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime TestMethod();
    }
}