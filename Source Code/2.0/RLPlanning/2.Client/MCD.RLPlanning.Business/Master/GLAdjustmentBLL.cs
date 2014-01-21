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
	/// 包含了对表GLAdjustment的所有操作的客户端的调用方法。
	/// </summary>
    public sealed class GLAdjustmentBLL : BaseBLL<IGLAdjustmentService>
    {
        //Fields
		/// <summary>
		/// 获取对表GLAdjustment的数据库操作类的实例。
		/// </summary>
		private static GLAdjustmentBLL _instance = new GLAdjustmentBLL();

        //Properties
		/// <summary>
		/// 获取对表GLAdjustment的数据库操作类的单例。
		/// </summary>
		public static GLAdjustmentBLL Instance
		{
			get
			{
                return GLAdjustmentBLL._instance;
			}	
		}
		
		///<summary>
		///删除表GLAdjustment中的指定记录并返回状态。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>删除成功则返回true，否则返回false</returns>
		public bool Delete(string adjustmentID)
		{
			return base.WCFService.Delete(adjustmentID);
		}
		
		///<summary>
		///向表GLAdjustment中插入一条记录并返回状态。
		///</summary>
		///<param name="gLAdjustment">要插入记录的GLAdjustment实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		public bool Insert(GLAdjustmentEntity gLAdjustment)
		{
			return base.WCFService.Insert(gLAdjustment);
		}
		
		///<summary>
		///更新表GLAdjustment中指定记录。
		///</summary>
		///<param name="gLAdjustment">要更新记录的GLAdjustment实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		public bool Update(GLAdjustmentEntity gLAdjustment)
		{
			return base.WCFService.Update(gLAdjustment);
		}
		
		///<summary>
		///获取表GLAdjustment中指定主码的某条记录的实例。
		///</summary>
		///<param name="adjustmentID"></param>
		///<returns>返回记录的实例GLAdjustment</returns>
		public GLAdjustmentEntity Single(string adjustmentID)
		{
			return base.WCFService.Single(adjustmentID);
		}
		
        /// <summary>
        /// 查询指定条件的GL调整金额纪录。
        /// </summary>
        /// <param name="entityInfoSettingID">实体信息设置ID</param>
        /// <param name="ruleSnapshotID">规则快照ID</param>
        /// <param name="ruleID">规则设置ID</param>
        /// <param name="rentType">租金类型</param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>返回调整金额纪录集合</returns>
        public List<GLAdjustmentEntity> Where(string entityInfoSettingID, string ruleSnapshotID, string ruleID, string rentType, DateTime? startDate, DateTime? endDate)
        {
            return base.WCFService.Where(entityInfoSettingID, ruleSnapshotID, ruleID, rentType, startDate, endDate);
        }
    }
}