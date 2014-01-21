using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using MCD.RLPlanning.Entity.Setting;

namespace MCD.RLPlanning.IServices.Setting
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IRentTypeService: IBaseService
    {
        /// <summary>
        /// 获取所有GL计算日期
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         DataTable SelectRentType();

        /// <summary>
        /// GL计算日期设置
        /// </summary>
        [OperationContract]
        int UpdateGLStartDate(RentTypeEntity entity);
    }
}