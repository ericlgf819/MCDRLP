using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MCD.Common.SRLS;

namespace MCD.RLPlanning.IServices.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICommonService : IBaseService
    {
        /// <summary>
        /// 强制结束流程
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="currentUserID"></param>
        [OperationContract]
        void ForceToEndByAdministrator(string procID,string currentUserID);

        /// <summary>
        /// 撤消业务数据
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="currentUserID"></param>
        [OperationContract]
        void CancelBizData(BizType bizType, string keyID, string currentUserID);
    }
}