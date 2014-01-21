using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MCD.Common.SRLS;
using MCD.RLPlanning.IServices.Common;

namespace MCD.RLPlanning.BLL.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonBLL : BaseBLL<ICommonService>
    {
        /// <summary>
        /// 管理员强制结束流程,结束不同的流程对不同的数据进行处理
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="currentUserID"></param>
        public void ForceToEndByAdministrator(string procID,string currentUserID)
        {
            base.WCFService.ForceToEndByAdministrator(procID, currentUserID);
        }

        /// <summary>
        /// 撤消业务数据,对草稿,审核退回的业务数据, 如果不想要了, 统一用此方法
        /// </summary>
        /// <param name="bizType">类型</param>
        /// <param name="keyID">ID</param>
        public void CancelBizData(BizType bizType, string keyID,string currentUserID)
        {
            base.WCFService.CancelBizData(bizType, keyID, currentUserID);
        }
    }
}