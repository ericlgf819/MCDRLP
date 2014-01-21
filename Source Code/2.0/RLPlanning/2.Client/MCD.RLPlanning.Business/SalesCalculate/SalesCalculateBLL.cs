using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.IServices.SalesCalculate;
using MCD.Common;


namespace MCD.RLPlanning.Business.SalesCalculate
{
    public class SalesCalculateBLL : BaseBLL<ISalesCalculate>
    {
        /// <summary>
        /// 筛选Store与Kiosk
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strStoreName"></param>
        /// <param name="strKioskNo"></param>
        /// <param name="strKioskName"></param>
        /// <param name="companyCodesDT"></param>
        /// <returns></returns>
        public DataSet SelectStoreOrKiosk(string strStoreNo, string strStoreName, string strKioskNo,
                    string strKioskName, DataTable companyCodesDT)
        {
            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.SelectStoreOrKiosk(strStoreNo, strStoreName,
                        strKioskNo, strKioskName, companyCodesDT));
        }

        /// <summary>
        /// 租金计算
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calCollectionTb"></param>
        public int Calculate(Guid userID, DataTable calCollectionTb, out byte[] entitysInCal, out string exceptionMsg)
        {
            entitysInCal = null;
            exceptionMsg = null;

            if (null == WCFService)
                return (int)SalesCalRetCode.EN_CAL_UNKNOWNERR;

            //压缩
            DataSet ds = new DataSet();
            ds.Tables.Add(calCollectionTb);
            byte[] collectionStream = Serilize(ds);

            if (null == calCollectionTb)
                return (int)SalesCalRetCode.EN_CAL_UNKNOWNERR;

            return WCFService.Calculate(userID, collectionStream, out entitysInCal, out exceptionMsg);
        }

        /// <summary>
        /// 租金计算错误结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calStartTime"></param>
        /// <returns></returns>
        public DataSet SelectCalculateResult(Guid userID, DateTime calStartTime)
        {
            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.SelectCalculateResult(userID,calStartTime));
        }

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns>返回的是string，如果是null表面获取失败</returns>
        public override DateTime GetServerTime()
        {
            if (null == this.WCFService)
            {
                throw new Exception("获取服务器时间错误");
            }
            return this.WCFService.GetServerTime();
        }
    }
}
