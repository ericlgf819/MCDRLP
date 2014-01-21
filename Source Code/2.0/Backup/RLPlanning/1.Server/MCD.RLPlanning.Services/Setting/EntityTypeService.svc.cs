using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Setting;
using MCD.RLPlanning.IServices.Setting;

namespace MCD.RLPlanning.Services.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityTypeService :BaseDAL<EntityTypeEntity>, IEntityTypeService
    {
        /// <summary>
        /// 查询实体类型
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEntityType()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataTable dt = db.ExecuteDataSet(CommandType.StoredProcedure, "usp_Master_SelectEntityType").Tables[0];
            return dt;
        }
    }
}