using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

namespace MCD.RLPlanning.IServices.Setting
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IEntityTypeService: IBaseService
    {
        /// <summary>
        /// 查询实体类型
        /// </summary>
        [OperationContract]
        DataTable SelectEntityType();
    }
}