using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IAreaService : IBaseService
    {
        /// <summary>
        /// 获取所有区域信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAreas();
    }
}