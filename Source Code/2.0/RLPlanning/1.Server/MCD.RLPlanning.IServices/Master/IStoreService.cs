using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IStoreService"，也必须更新 Web.config 中对 "IStoreService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IStoreService : IBaseService
    {
        /// <summary>
        /// 查找所有的餐厅信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllStore(StoreEntity entity);
        /// <summary>
        /// 分页返回指定条件的餐厅信息。
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="companyCode"></param>
        /// <param name="storeNo"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] USDX(Guid? areaID, string companyCode, string storeNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount);
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        StoreEntity SelectSingleStore(StoreEntity entity);


        /// <summary>
        /// 插入單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertSingleStore(string storeNo, string CompanyCode, string StoreName, string SimpleName,
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID);
        /// <summary>
        /// 更新單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateSingleStore(string storeNo, string CompanyCode, string StoreName, string SimpleName,
            DateTime? OpenDate, DateTime? CloseDate, string Status, string EmailAddress, Guid userID);
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateMultiStore(string StorNO,int Count);
        /// <summary>
        /// 删除單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int DeletedSingleStore(StoreEntity _StoreEntity);
      
        
        /// <summary>
        /// 更新Sequence.NextId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        string Update_Sequence (string TableName, string FieldName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoreNo"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime? GetStoreOpenDate(string StoreNo);
    }
}