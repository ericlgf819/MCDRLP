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
    public class AreaBLL : BaseBLL<IAreaService>
    {
        /// <summary>
        /// 获取所有区域信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAreas()
        {
            return base.DeSerilize(base.WCFService.SelectAreas());
        }
    }
}