using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class AreaService : BaseDAL<AreaEntity>, IAreaService
    {
        /// <summary>
        /// 获取所有区域信息
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAreas()
        {
            return base.Serilize(base.GetDataSet(new AreaEntity()));
        }
    }
}