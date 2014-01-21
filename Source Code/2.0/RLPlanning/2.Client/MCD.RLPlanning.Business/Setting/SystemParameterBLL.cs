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
    public class SystemParameterBLL : BaseBLL<ISystemParameterService>
    {
        /// <summary>
        /// 查询系统参数
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSystemParameter()
        {
            return base.WCFService.SelectSystemParameter();
        }

        /// <summary>
        ///  修改系统参数的值和备注
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSystemParameter(string paramCode,string paramName, string paramValue, string remark)
        {
            SystemParameterEntity entity = new SystemParameterEntity() {
                ParamCode = paramCode,
                ParamName = paramName,
                ParamValue = paramValue,
                Remark = remark
            };
            //
            base.WCFService.UpdateSystemParameter(entity);
        }

        /// <summary>
        /// 获取系统参数配置值
        /// </summary>
        /// <param name="paramCode"></param>
        /// <returns></returns>
        public string GetSystemParameterByCode(string paramCode)
        {
            return base.WCFService.GetSystemParameterByCode(paramCode);
        }
    }
}