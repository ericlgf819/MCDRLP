using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.IServices.PlanningSnapshot;

namespace MCD.RLPlanning.BLL.PlanningSnapshot
{
    /// <summary>
    /// 
    /// </summary>
    public class CloseAccountBLL : BaseBLL<ICloseAccountService>
    {
        /// <summary>
        /// 获取未关帐信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectClosePlanning()
        {
            return base.DeSerilize(base.WCFService.SelectClosePlanning());
        }
        /// <summary>
        /// 检测关账
        /// </summary>
        /// <returns></returns>
        public DataSet CheckClosePlanning(int ID, Guid? ClosedBy)
        {
            return base.DeSerilize(base.WCFService.CheckClosePlanning(ID, ClosedBy));
        }
    }
}