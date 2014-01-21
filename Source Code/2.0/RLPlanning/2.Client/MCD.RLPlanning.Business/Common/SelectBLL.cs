using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectBLL : BaseBLL<ISelectService>
    {
        /// <summary>
        /// 选择租金类型
        /// </summary>
        /// <returns></returns>
        public DataSet SelectRentType()
        {
            return base.DeSerilize(base.WCFService.SelectRentType());
        }
        /// <summary>
        /// 选择实体类型
        /// </summary>
        /// <returns></returns>
        public DataSet SelectEntityType()
        {
            return base.DeSerilize(base.WCFService.SelectEntityType());
        }
        /// <summary>
        /// 选择Typecode状态
        /// </summary>
        /// <returns></returns>
        public DataSet SelectTypeCodeStatus()
        {
            return base.DeSerilize(base.WCFService.SelectTypeCodeStatus());
        }

        /// <summary>
        /// 选择激活状态的ACCOUNT
        /// </summary>
        /// <returns></returns>
        public DataSet SelectActiveAccount()
        {
            return DeSerilize(base.WCFService.SelectActiveAccount());
        }

        /// <summary>
        /// 获取指定键名称的字典项集合。
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetItemsByKeyName(string keyName)
        {
            return base.WCFService.GetItemsByKeyName(keyName);
        }
        /// <summary>
        /// 获取指定键值的字典项集合。
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetItemsByKeyValue(string keyValue)
        {
            return base.WCFService.GetItemsByKeyValue(keyValue);
        }
    }
}