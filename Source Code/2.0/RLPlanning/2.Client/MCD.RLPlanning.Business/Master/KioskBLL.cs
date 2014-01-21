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
	/// 包含了对表SRLS_TB_Master_Kiosk的所有操作的客户端的调用方法。
	/// </summary>
    public sealed class KioskBLL : BaseBLL<IKioskService>
    {
        //Fields
		/// <summary>
		/// 获取对表SRLS_TB_Master_Kiosk的数据库操作类的实例。
		/// </summary>
		private static KioskBLL _instance = new KioskBLL();

        //Properties
		/// <summary>
		/// 获取对表SRLS_TB_Master_Kiosk的数据库操作类的单例。
		/// </summary>
		public static KioskBLL Instance
		{
			get
			{
				return KioskBLL._instance;
			}	
		}
		
		
		///<summary>
		///向表SRLS_TB_Master_Kiosk中插入一条记录并返回状态。
		///</summary>
		///<param name="sRLS_TB_Master_Kiosk">要插入记录的SRLS_TB_Master_Kiosk实例</param>
		///<returns>成功则返回true，否则返回false</returns>
		public int Insert(KioskEntity sRLS_TB_Master_Kiosk)
		{
			return base.WCFService.Insert(sRLS_TB_Master_Kiosk);
		}
		///<summary>
		///更新表SRLS_TB_Master_Kiosk中指定记录。
		///</summary>
		///<param name="sRLS_TB_Master_Kiosk">要更新记录的SRLS_TB_Master_Kiosk实例</param>
		///<returns>更新成功则返回true，否则返回false</returns>
		public int Update(KioskEntity sRLS_TB_Master_Kiosk)
		{
			return base.WCFService.Update(sRLS_TB_Master_Kiosk);
		}
        ///<summary>
        ///删除表SRLS_TB_Master_Kiosk中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskNo">KioskNo</param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        public bool Delete(string kioskID)
        {
            return base.WCFService.Delete(kioskID);
        }

        ///<summary>
        ///更新表Sequence中指定记录。
        ///</summary>
        ///<returns>最新ID</returns>
        public string Update_Sequence()
        {
            return base.WCFService.Update_Sequence();
        }
        /// <summary>
        /// 批量复制Kiosk
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateMultiKiosk(string KioskID, int Count)
        {
            return base.WCFService.UpdateMultiKiosk(KioskID, Count);
        }

		///<summary>
		///获取表SRLS_TB_Master_Kiosk中指定主码的某条记录的实例。
		///</summary>
        ///<param name="kioskID">kioskID</param>
		///<returns>返回记录的实例SRLS_TB_Master_Kiosk</returns>
		public KioskEntity Single(string kioskID)
		{
            return base.WCFService.Single(kioskID);
		}
        /// <summary>
        /// 根据甜品店编号、餐厅编号、状态检索甜品店信息。
        /// </summary>
        /// <param name="kioskNo"></param>
        /// <param name="storeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<KioskEntity> Where(Guid? areaID, string CompanyCode, string storeNo, string kioskNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount)
        {
            return base.WCFService.Where(areaID, CompanyCode, storeNo, kioskNo, status, FromSRLS, UserId,
                pageIndex, pageSize, out recordCount);
        }
        public List<KioskEntity> Where(string kioskNo, string storeNo, string status, int iPageIndex = 1, int iPageSize = 0)
        {
            int recordCount = 0;
            //
            return base.WCFService.Where(null, null, storeNo, kioskNo, status, null, null, 
                                            iPageIndex, iPageSize, out recordCount);
        }
		///<summary>
		///返回表SRLS_TB_Master_Kiosk所有行实例的集合。
		///</summary>
		///<returns>返回所有行实例SRLS_TB_Master_Kiosk的集合</returns>
		public List<KioskEntity> All()
		{
            int recordCount = 0;
            //
            return base.WCFService.Where(null, null, null, null, null, true, null, 1, 0, out recordCount);
        }

        /// <summary>
        /// 获取指定甜品店最近的挂靠记录。
        /// </summary>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public DataRow GetRecentChangeHistory(string kioskID)
        {
            DataSet ds = base.DeSerilize(base.WCFService.GetRecentChangeHistory(kioskID));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0];
            }
            //
            return null;
        }
        /// <summary>
        /// 获取指定Kiosk的挂靠记录。
        /// </summary>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public DataTable GetChangeRelationHistory(string kioskID)
        {
            return base.DeSerilize(base.WCFService.GetChangeRelationHistory(kioskID)).Tables[0];
        }
        /// <summary>
        /// 删除指定的挂靠记录。
        /// </summary>
        /// <param name="changeID"></param>
        public void DeleteKioskChangeRelationHistory(string changeID)
        {
            base.WCFService.DeleteKioskChangeRelationHistory(changeID);
        }

        /// <summary>
        /// 根据归属餐厅编号生成KIOSK编号。
        /// </summary>
        /// <param name="storeNo">归属餐厅编号</param>
        /// <returns>返回KIOSK编号</returns>
        public string GetKioskNumber(string storeNo)
        {
            StoreEntity store = null;
            using (StoreBLL stBll = new StoreBLL())
            {
                store = stBll.SelectSingleStore(storeNo);
            }
            return base.WCFService.GetKioskNumber(store.CompanyCode);
        }
        /// <summary>
        /// 获取指定甜品店编号的甜品店是否存在与之关联的合同。
        /// </summary>
        /// <param name="kioskNo">kioskNo</param>
        /// <returns></returns>
        public bool ExistsRelatedContract(string kioskNo)
        {
            return base.WCFService.ExistsRelatedContract(kioskNo);
        }
    }
}