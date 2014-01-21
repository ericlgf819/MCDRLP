using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.IServices;
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreBLL : BaseBLL<IStoreService>
    {
        /// <summary>
        /// 查找所有的餐厅信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllStore(Guid areaID, string companyCode, string storeNo, string status)
        {
            StoreEntity entity = new StoreEntity() {
                AreaID = areaID,
                CompanyCode = companyCode,
                StoreNo = storeNo,
                Status = status
            };
            return base.DeSerilize(base.WCFService.SelectAllStore(entity));
        }
        /// <summary>
        /// 分页返回查询结果。
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="companyCode"></param>
        /// <param name="storeNo"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet USDX(Guid? areaID, string companyCode, string storeNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount)
        {
            return base.DeSerilize(base.WCFService.USDX(areaID, companyCode, storeNo, status, FromSRLS, UserId,
                pageIndex, pageSize, out recordCount));
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public StoreEntity SelectSingleStore(string storeNo)
        {
            StoreEntity entity = new StoreEntity() { StoreNo = storeNo };
            //
            return base.WCFService.SelectSingleStore(entity);
        }


        /// <summary>
        /// 插入單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertSingleStore(string storeNo, string CompanyCode, string StoreName, string SimpleName,
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID)
        {
            return base.WCFService.InsertSingleStore(storeNo, CompanyCode, StoreName, SimpleName,
                OpenDate, CloseDate, Status, EmailAddress, userID);
        }
        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleStore (string storeNo, string CompanyCode, string StoreName, string SimpleName, 
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID)
        {
            return base.WCFService.UpdateSingleStore(storeNo,CompanyCode, StoreName, SimpleName, 
                OpenDate, CloseDate, Status, EmailAddress,  userID);
        }
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateMultiStore(string storeNo, int Count)
        {
            return base.WCFService.UpdateMultiStore(storeNo, Count);
        }
        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeletedSingleStore(StoreEntity _StoreEntity)
        {
            return base.WCFService.DeletedSingleStore(_StoreEntity);
        }

        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Update_Sequence(string TableName, string FieldName)
        {
            return base.WCFService.Update_Sequence(TableName, FieldName);
        }

        public DateTime? GetStoreOpenDate(string StoreNo)
        {
            return base.WCFService.GetStoreOpenDate(StoreNo);
        }
    }
}