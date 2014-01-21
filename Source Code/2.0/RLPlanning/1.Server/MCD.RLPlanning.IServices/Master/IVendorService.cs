using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IVendorService"，也必须更新 Web.config 中对 "IVendorService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IVendorService : IBaseService
    {
        /// <summary>
        /// 按页返回业主信息。
        /// </summary>
        /// <param name="vendorName"></param>
        /// <param name="vendorNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectVendorPagedResult(string vendorName, string vendorNo, int pageIndex, int pageSize, out int recordCount);

        /// <summary>
        /// 按业主编号或者业主名称模糊查询业主信息。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectVendorByNoOrName(string keywords);
    }
}