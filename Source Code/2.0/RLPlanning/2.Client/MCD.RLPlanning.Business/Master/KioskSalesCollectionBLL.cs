using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using MCD.RLPlanning.BLL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;
using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.BLL.Master
{
	/// <summary>
	/// 包含了对表KioskSalesCollection的所有操作的客户端的调用方法。
	/// </summary>
    public sealed class KioskSalesCollectionBLL : BaseBLL<IKioskSalesCollectionService>
    {
        //Fields
		/// <summary>
		/// 获取对表KioskSalesCollection的数据库操作类的实例。
		/// </summary>
		private static KioskSalesCollectionBLL _instance = new KioskSalesCollectionBLL();

        //Properties
		/// <summary>
		/// 获取对表KioskSalesCollection的数据库操作类的单例。
		/// </summary>
		public static KioskSalesCollectionBLL Instance
		{
			get
			{
                return KioskSalesCollectionBLL._instance;
			}	
		}
		
		#region 基本操作

		///<summary>
		///删除表KioskSalesCollection中的指定记录并返回状态。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		public bool Delete(string collectionID)
		{
			return base.WCFService.Delete(collectionID);
		}
		
		///<summary>
		///向表KioskSalesCollection中插入一条记录并返回状态。
		///</summary>
		///<param name="kioskSalesCollection">要插入记录的KioskSalesCollection实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		public bool Insert(KioskSalesCollectionEntity kioskSalesCollection)
		{
			return base.WCFService.Insert(kioskSalesCollection);
		}
		
		///<summary>
		///更新表KioskSalesCollection中指定记录。
		///</summary>
		///<param name="kioskSalesCollection">要更新记录的KioskSalesCollection实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		public bool Update(KioskSalesCollectionEntity kioskSalesCollection)
		{
			return base.WCFService.Update(kioskSalesCollection);
		}
		#endregion
		
		#region 获取对象

		///<summary>
		///获取表KioskSalesCollection中指定主码的某条记录的实例。
		///</summary>
		///<param name="collectionID"></param>
		///<returns>返回记录的实例KioskSalesCollection</returns>
		public KioskSalesCollectionEntity Single(string collectionID)
		{
			return base.WCFService.Single(collectionID);
		}
		
		///<summary>
		///根据SQL查询条件返回行实例的集合。
		///</summary>
		///<param name="where">带"WHERE"的SQL查询条件</param>
		///<param name="whereParameters">条件中的参数信息数组</param>
		///<returns>返回的实例KioskSalesCollectionList对象。</returns>
		public List<KioskSalesCollectionEntity> Where(string where, params WcfSqlParameter[] whereParameters)
		{
			return base.WCFService.Where(where, whereParameters);
		}
		
		///<summary>
		///返回表KioskSalesCollection所有行实例的集合。
		///</summary>
		///<returns>返回所有行实例KioskSalesCollection的集合</returns>
		public List<KioskSalesCollectionEntity> All()
		{
			return base.WCFService.All();
		}
		#endregion
    }
}