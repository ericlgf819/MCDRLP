using System;
using System.Data;
using System.Collections.Generic;
using System.ServiceModel;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.IServices.Master
{
	/// <summary>
	///定义对KioskSalesCollection表操作的契约。
	/// </summary>
	[ServiceContract]
    public interface IKioskSalesCollectionService : IBaseService
    {
		#region 基本操作
		///<summary>
		///删除表KioskSalesCollection中的指定记录并返回状态。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Delete(string collectionID);
		
		///<summary>
		///向表KioskSalesCollection中插入一条记录并返回状态。
		///</summary>
		///<param name="kioskSalesCollection">要插入记录的KioskSalesCollection实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Insert(KioskSalesCollectionEntity kioskSalesCollection);
		
		///<summary>
		///更新表KioskSalesCollection中指定记录。
		///</summary>
		///<param name="kioskSalesCollection">要更新记录的KioskSalesCollection实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Update(KioskSalesCollectionEntity kioskSalesCollection);
		#endregion
		
		#region 获取对象
		///<summary>
		///获取表KioskSalesCollection中指定主码的某条记录的实例。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>返回记录的实例KioskSalesCollection</returns>
		[OperationContract]
		KioskSalesCollectionEntity Single(string collectionID);
		
		///<summary>
		///根据SQL查询条件返回行实例的集合。
		///</summary>
		///<param name="where">带"WHERE"的SQL查询条件</param>
		///<param name="whereParameters">条件中的参数信息数组</param>
		///<returns>返回的实例KioskSalesCollectionList对象。</returns>
		[OperationContract]
		List<KioskSalesCollectionEntity> Where(string where, params WcfSqlParameter[] whereParameters);
		
		///<summary>
		///返回表KioskSalesCollection所有行实例的集合。
		///</summary>
		///<returns>返回所有行实例KioskSalesCollection的集合</returns>
		[OperationContract]
		List<KioskSalesCollectionEntity> All();
		#endregion
		
		#region 接口扩展
		#endregion
    }
}