using System.Collections.Generic;
using System.ServiceModel;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.Common;
using System;

namespace MCD.RLPlanning.IServices.Master
{
	/// <summary>
	///定义对SRLS_TB_Master_Kiosk表操作的契约。
	/// </summary>
    [ServiceContract]
    public interface IKioskService : IBaseService
    {
        ///<summary>
        ///向表SRLS_TB_Master_Kiosk中插入一条记录并返回状态。
        ///</summary>
        ///<param name="sRLS_TB_Master_Kiosk">要插入记录的SRLS_TB_Master_Kiosk实例</param>
        ///<returns>成功则返回true，否则返回false</returns>
        [OperationContract]
        int Insert(KioskEntity sRLS_TB_Master_Kiosk);
        ///<summary>
        ///更新表SRLS_TB_Master_Kiosk中指定记录。
        ///</summary>
        ///<param name="sRLS_TB_Master_Kiosk">要更新记录的SRLS_TB_Master_Kiosk实例</param>
        ///<returns>更新成功则返回true，否则返回false</returns>
        [OperationContract]
        int Update(KioskEntity sRLS_TB_Master_Kiosk);
        ///<summary>
        ///删除表SRLS_TB_Master_Kiosk中的指定记录并返回状态。
        ///</summary>
        ///<param name="kioskID">kioskID</param>
        ///<returns>删除成功则返回true，否则返回false</returns>
        [OperationContract]
        bool Delete(string kioskID);


        [OperationContract]
        string Update_Sequence();
        /// <summary>
        /// 查找單個餐廳信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateMultiKiosk(string KioskID, int Count);

        ///<summary>
        ///获取表SRLS_TB_Master_Kiosk中指定主码的某条记录的实例。
        ///</summary>
        ///<param name="kioskID">kioskID</param>
        ///<returns>返回记录的实例SRLS_TB_Master_Kiosk</returns>
        [OperationContract]
        KioskEntity Single(string kioskID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kioskNo"></param>
        /// <param name="storeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        List<KioskEntity> Where(Guid? areaID, string CompanyCode, string storeNo, string kioskNo, string status, bool? FromSRLS, Guid? UserId,
            int pageIndex, int pageSize, out int recordCount);

        [OperationContract]
        byte[] GetRecentChangeHistory(string kioskID);
        [OperationContract]
        byte[] GetChangeRelationHistory(string kioskID);
        [OperationContract]
        void DeleteKioskChangeRelationHistory(string changeID);

        /// <summary>
        /// 获取指定名称指定长度的流水号。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [OperationContract]
        string GetFlowNumber(string name, int length);
        /// <summary>
        /// 根据公司编号获取流水号。
        /// </summary>
        /// <param name="compamyCode"></param>
        /// <returns></returns>
        [OperationContract]
        string GetKioskNumber(string compamyCode);
        /// <summary>
        /// 获取指定甜品店编号的甜品店是否存在与之关联的合同。
        /// </summary>
        /// <param name="kioskNo">kioskNo</param>
        /// <returns></returns>
        [OperationContract]
        bool ExistsRelatedContract(string kioskNo);
    }
}