using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using MCD.RLPlanning.IServices;


namespace MCD.RLPlanning.IServices.SalesCalculate
{
    [ServiceContract]
    public interface ISalesCalculate : IBaseService
    {
        #region 租金计算相关方法
        /// <summary>
        /// 筛选Store、Kiosk
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strStoreName"></param>
        /// <param name="strKioskNo"></param>
        /// <param name="strKioskName"></param>
        /// <param name="companyCodesDT"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectStoreOrKiosk(string strStoreNo, string strStoreName, string strKioskNo, 
                    string strKioskName, DataTable companyCodesDT);
        
        /// <summary>
        /// 计算租金
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calCollectionTb"></param>
        /// <returns></returns>
        [OperationContract]
        int Calculate(Guid userID, byte[] calCollectionTb, out byte[] entitysInCal, out string exceptionMsg);

        /// <summary>
        /// 得到计算错误结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calStartTime"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectCalculateResult(Guid userID, DateTime calStartTime);

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetServerTime();
        #endregion
    }
}
