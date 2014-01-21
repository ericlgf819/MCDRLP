using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class VendorBLL : BaseBLL<IVendorService>
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
        public DataSet SelectVendorPagedResult(string vendorName, string vendorNo, int pageIndex, int pageSize, out int recordCount)
        {
            return base.DeSerilize(base.WCFService.SelectVendorPagedResult(vendorName, vendorNo, pageIndex, pageSize, out recordCount));
        }

        /// <summary>
        /// 按业主编号或者业主名称模糊查询业主信息。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataSet SelectVendorByNoOrName(string keywords)
        {
            return base.DeSerilize(base.WCFService.SelectVendorByNoOrName(keywords));
        }
    }
}