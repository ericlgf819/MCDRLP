using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.IServices.Setting;

namespace MCD.RLPlanning.BLL.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityTypeBLL : BaseBLL<IEntityTypeService>
    {
        /// <summary>
        /// 查询实体类
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEntityType()
        {
            return base.WCFService.SelectEntityType();
        }
    }
}