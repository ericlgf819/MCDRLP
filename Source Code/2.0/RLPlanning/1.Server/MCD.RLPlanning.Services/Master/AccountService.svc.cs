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
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountService : BaseDAL<AccountEntity>, IAccountService
    {
        #region IAccountService 成员

        /// <summary>
        /// 查找所有科目信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllAccount(AccountEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }

        #endregion
    }
}