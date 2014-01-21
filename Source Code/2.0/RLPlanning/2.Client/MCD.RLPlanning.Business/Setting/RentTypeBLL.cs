using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Setting;
using MCD.RLPlanning.IServices.Setting;

namespace MCD.RLPlanning.BLL.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class RentTypeBLL : BaseBLL<IRentTypeService>
    {
        /// <summary>
        /// 获取所有GL计算日期
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRentType()
        {
            return base.WCFService.SelectRentType();
        }

        /// <summary>
        /// GL计算日期设置
        /// </summary>
        /// <param name="rentTypeName">租金类型名称</param>
        /// <param name="gLStartDate">GL计算日期</param>
        /// <param name="lastModifyTime">最后修改时间</param>
        /// <param name="lastModifyUserName">最后修改人</param>
        public int UpdateGLStartDate(string rentTypeName,string whichMonth, int gLStartDate, string lastModifyUserName)
        {
            RentTypeEntity entity = new RentTypeEntity() {
                RentTypeName = rentTypeName,
                WhichMonth=whichMonth,
                GLStartDate = gLStartDate,
                LastModifyUserName= lastModifyUserName
            };
            return base.WCFService.UpdateGLStartDate(entity);
        }
    }
}