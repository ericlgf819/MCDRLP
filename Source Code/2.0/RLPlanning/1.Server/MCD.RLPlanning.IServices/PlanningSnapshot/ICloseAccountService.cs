using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace MCD.RLPlanning.IServices.PlanningSnapshot
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICloseAccountService : IBaseService
    {
        /// <summary>
        /// 获取关帐信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectClosePlanning();
        /// <summary>
        /// 检测关账
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] CheckClosePlanning(int ID, Guid? ClosedBy);
    }
}