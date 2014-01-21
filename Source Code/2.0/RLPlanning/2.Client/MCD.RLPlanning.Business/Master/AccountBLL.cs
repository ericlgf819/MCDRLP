using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBLL : BaseBLL<IAccountService>
    {
        /// <summary>
        /// 查找所有科目信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllAccount(string accountNo,string status)
        {
            AccountEntity entity = new AccountEntity() {
                AccountNo = accountNo,
                Status = status
            };
            return base.DeSerilize(base.WCFService.SelectAllAccount(entity));
        }
    }
}