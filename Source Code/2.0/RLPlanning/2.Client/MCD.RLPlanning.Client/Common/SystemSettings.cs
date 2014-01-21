using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.BLL.Setting;

namespace MCD.RLPlanning.Client.Common
{
    /// <summary>
    /// 表示系统配置信息。
    /// </summary>
    public class SystemSettings
    {
        //Fields
        private DataTable dtConfig = null;

        //Properties
        /// <summary>
        /// 获取系统指定名称的键的配置项的值。
        /// </summary>
        /// <param name="key">配置项名称或键</param>
        /// <returns>返回配置项的值</returns>
        public string this[string keyOrName]
        {
            get
            {
                if (this.dtConfig != null)
                {
                    DataRow[] rows = this.dtConfig.Select(string.Format("ParamCode='{0}' OR ParamName='{0}'", keyOrName));
                    if (rows != null && rows.Length > 0)
                    {
                        DataRow dr = rows[0];
                        return (dr["ParamValue"] == DBNull.Value ? string.Empty : dr["ParamValue"].ToString());
                    }
                }
                return string.Empty;
            }
        }

        //Methods
        /// <summary>
        /// 初始化配置。
        /// </summary>
        public void LoadSettings()
        {
            using (SystemParameterBLL bll = new SystemParameterBLL())
            {
                this.dtConfig = bll.SelectSystemParameter();
            }
        }
    }
}