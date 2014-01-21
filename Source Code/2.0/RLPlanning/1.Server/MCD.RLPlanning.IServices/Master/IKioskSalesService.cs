using System;
using System.Data;
using System.Collections.Generic;
using System.ServiceModel;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.IServices.Master
{
	/// <summary>
	///定义对KioskSales表操作的契约。
	/// </summary>
	[ServiceContract]
    public interface IKioskSalesService : IBaseService
    {
        ///<summary>
        ///返回表KioskSales所有行实例的集合。
        ///</summary>
        ///<returns>返回所有行实例KioskSales的集合</returns>
        [OperationContract]
        List<KioskSalesEntity> All();
        ///<summary>
        /// 根据SQL查询条件返回DataSet。
        ///</summary>
        ///<param name="where">带where的查询条件</param>
        ///<returns>返回DataSet</returns>
        [OperationContract]
        byte[] GetDataSet(string where, params WcfSqlParameter[] whereParameters);
        ///<summary>
        ///根据SQL查询条件返回行实例的集合。
        ///</summary>
        ///<param name="where">带"WHERE"的SQL查询条件</param>
        ///<param name="whereParameters">条件中的参数信息数组</param>
        ///<returns>返回的实例KioskSalesList对象。</returns>
        [OperationContract]
        List<KioskSalesEntity> Where(string where, params WcfSqlParameter[] whereParameters);
        ///<summary>
        ///获取表KioskSales中指定主码的某条记录的实例。
        ///</summary>
        ///<param name="kioskSalesID"></param>
        ///<returns>返回记录的实例KioskSales</returns>
        [OperationContract]
        KioskSalesEntity Single(string kioskSalesID);

		///<summary>
		///向表KioskSales中插入一条记录并返回状态。
		///</summary>
		///<param name="kioskSales">要插入记录的KioskSales实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		[OperationContract]
		int Insert(KioskSalesEntity kioskSales);
		///<summary>
		///更新表KioskSales中指定记录。
		///</summary>
		///<param name="kioskSales">要更新记录的KioskSales实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		[OperationContract]
        bool Update(KioskSalesEntity kioskSales);
        ///<summary>
        ///删除表KioskSales中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskSalesID"></param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        [OperationContract]
        bool Delete(string kioskSalesID);
    }
}