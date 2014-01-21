using System;
using System.Data;
using System.Collections.Generic;
using System.ServiceModel;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.IServices.Master
{
	/// <summary>
	///定义对GLAdjustment表操作的契约。
	/// </summary>
	[ServiceContract]
    public interface IGLAdjustmentService : IBaseService
    {
		///<summary>
		///删除表GLAdjustment中的指定记录并返回状态。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Delete(string adjustmentID);
		
		///<summary>
		///向表GLAdjustment中插入一条记录并返回状态。
		///</summary>
		///<param name="gLAdjustment">要插入记录的GLAdjustment实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Insert(GLAdjustmentEntity gLAdjustment);
		
		///<summary>
		///更新表GLAdjustment中指定记录。
		///</summary>
		///<param name="gLAdjustment">要更新记录的GLAdjustment实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		[OperationContract]
		bool Update(GLAdjustmentEntity gLAdjustment);

		///<summary>
		///获取表GLAdjustment中指定主码的某条记录的实例。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>返回记录的实例GLAdjustment</returns>
		[OperationContract]
		GLAdjustmentEntity Single(string adjustmentID);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityInfoSettingID"></param>
		/// <param name="ruleSnapshotID"></param>
		/// <param name="ruleID"></param>
		/// <param name="rentType"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		[OperationContract]
        List<GLAdjustmentEntity> Where(string entityInfoSettingID, string ruleSnapshotID, string ruleID, string rentType, DateTime? startDate, DateTime? endDate);
    }
}