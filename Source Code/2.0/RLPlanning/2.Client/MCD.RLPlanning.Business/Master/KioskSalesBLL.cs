using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using MCD.RLPlanning.Entity.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;
using MCD.RLPlanning.BLL;

namespace MCD.RLPlanning.BLL.Master
{
	/// <summary>
	/// 包含了对表KioskSales的所有操作的客户端的调用方法。
	/// </summary>
    public sealed class KioskSalesBLL : BaseBLL<IKioskSalesService>
    {
        //Fields
		/// <summary>
		/// 获取对表KioskSales的数据库操作类的实例。
		/// </summary>
		private static KioskSalesBLL _instance = new KioskSalesBLL();

        //Properties
		/// <summary>
		/// 获取对表KioskSales的数据库操作类的单例。
		/// </summary>
		public static KioskSalesBLL Instance
		{
			get
			{
				return KioskSalesBLL._instance;
			}	
		}


        ///<summary>
        ///返回表KioskSales所有行实例的集合。
        ///</summary>
        ///<returns>返回所有行实例KioskSales的集合</returns>
        public List<KioskSalesEntity> All()
        {
            return base.WCFService.All();
        }
        /// <summary>
        /// 获取指定收集任务的调整金额纪录。
        /// </summary>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public DataTable GetKioskSales(string collectionID)
        {
            string where = string.Format("WHERE ks.CollectionID = '{0}'", collectionID);
            DataSet ds = base.DeSerilize(base.WCFService.GetDataSet(where));
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 检索属于指定调整日期范围的、指定餐厅的调整记录。
        /// </summary>
        /// <param name="kioskID"></param>
        /// <param name="changeStart"></param>
        /// <param name="changeEnd"></param>
        /// <returns></returns>
        public List<KioskSalesEntity> Where(string kioskID, DateTime changeStart, DateTime changeEnd)
        {
            List<WcfSqlParameter> parms = new List<WcfSqlParameter>();
            //
            string where = "WHERE 1=1";
            if (!string.IsNullOrEmpty(kioskID))
            {
                where = string.Format("{0} AND KioskID=@KioskID", where);
                parms.Add(new WcfSqlParameter("@KioskID", WcfSqlDbType.NVarChar, 50, kioskID));
            }
            if (changeStart != null)
            {
                where = string.Format("{0} AND DateDiff(day, @ChangeStart, SalesDate)>=0", where);
                parms.Add(new WcfSqlParameter("@ChangeStart", WcfSqlDbType.DateTime, 0, changeStart));
            }
            if (changeEnd != null)
            {
                where = string.Format("{0} AND DateDiff(day, SalesDate, @ChangeEnd)>=0", where);
                parms.Add(new WcfSqlParameter("@ChangeEnd", WcfSqlDbType.DateTime, 0, changeEnd));
            }
            return base.WCFService.Where(where, parms.ToArray());
        }
        ///<summary>
        ///根据SQL查询条件返回行实例的集合。
        ///</summary>
        ///<param name="where">带"WHERE"的SQL查询条件</param>
        ///<param name="whereParameters">条件中的参数信息数组</param>
        ///<returns>返回的实例KioskSalesList对象。</returns>
        public List<KioskSalesEntity> Where(string where, params WcfSqlParameter[] whereParameters)
        {
            return base.WCFService.Where(where, whereParameters);
        }
        ///<summary>
        ///获取表KioskSales中指定主码的某条记录的实例。
        ///</summary>
        ///<param name="kioskSalesID">KioskSalesID</param>
        ///<returns>返回记录的实例KioskSales</returns>
        public KioskSalesEntity Single(string kioskSalesID)
        {
            return base.WCFService.Single(kioskSalesID);
        }

		///<summary>
        ///向表KioskSales中插入一条记录并返回状态(0:没有对应的正常导入的区间,1:录入成功,2:当前录入的所属区间的所有调整金额小于0)。
		///</summary>
		///<param name="kioskSales">要插入记录的KioskSales实例</param>
		///<returns>成功则返回1</returns>
		public int Insert(KioskSalesEntity kioskSales)
		{
			return base.WCFService.Insert(kioskSales);
		}
		///<summary>
		///更新表KioskSales中指定记录。
		///</summary>
		///<param name="kioskSales">要更新记录的KioskSales实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		public bool Update(KioskSalesEntity kioskSales)
		{
			return base.WCFService.Update(kioskSales);
		}
        ///<summary>
        ///删除表KioskSales中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskSalesID">KioskSalesID</param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        public bool Delete(string kioskSalesID)
        {
            return base.WCFService.Delete(kioskSalesID);
        }
    }
}