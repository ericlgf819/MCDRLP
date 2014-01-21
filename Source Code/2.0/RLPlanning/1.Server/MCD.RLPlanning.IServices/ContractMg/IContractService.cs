using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.Common;

namespace MCD.RLPlanning.IServices.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IContractService : IBaseService
    {
        #region 合同相关方法


        /// <summary>
        /// 手工发起APGL
        /// </summary>
        [OperationContract]
        void CreateAPGLByHand();

        /// <summary>
        /// 根据条件查找合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectContracts(string storeOrDeptNo, string vendorNo, string companyNo, string contractNo, Guid area, string status, bool? bFromSRLS, Guid UserID, int pageIndex, int pageSize, out int recordCount);

        /// <summary>
        /// 获取合同历史版本
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectContractHistory(string contractID);

        /// <summary>
        /// 查找单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        ContractEntity SelectSingleContract(ContractEntity entity);

        /// <summary>
        /// 更新单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        List<object> UpdateSingleContract(ContractEntity entity);

        /// <summary>
        /// 写入单个实体数据B
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        List<object> InsertSingleContract(ContractEntity entity);

        /// <summary>
        /// 删除代理信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteSingleContract(ContractEntity entity);

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="currentUserID"></param>
        [OperationContract]
        void DeleteContract(string contractSnapshotID, string currentUserID);

        /// <summary>
        /// 根据合同快照ID找出所关联的所有表
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllRentRuleInfosByContractSnapshotID(string contractSnapshotID);

        /// <summary>
        /// 新增合同但未保存时执行的操作
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="copyType"></param>
        [OperationContract]
        void UndoContract(string contractSnapshotID, ContractCopyType copyType);

        /// <summary>
        /// 复制合同，用于变更和续租合同
        /// </summary>
        /// <param name="contractSnapshotID">要复制的合同</param>
        /// <param name="operatorID">操作人ID</param>
        /// <param name="copyType">复制类型，变更/续租</param>
        /// <returns>新合同快照ID</returns>
        [OperationContract]
        string CopyContract(string contractSnapshotID, string operatorID, string copyType);

        /// <summary>
        /// 校验合同信息
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns>IndexId int,Code NVARCHAR(50), RelationData NVARCHAR(50),CheckMessage NVARCHAR(500)</returns>
        [OperationContract]
        byte[] CheckContract(string contractSnapshotID);

        /// <summary>
        /// 导入合同
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ImportContracts(DataTable dt, string importType, string currentUserID,
            out int totalCount, out int successCount, out int failCount);

        /// <summary>
        /// 实体是否已经出了AP
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsEntityHasAP(string entityID);

        #endregion

        #region VendorContract相关方法

        /// <summary>
        /// 通过合同快照ID查询业主合同关系
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectVendorContractByContractSnapshotID(string contractSnapshotID);

        /// <summary>
        /// 查询单个业主合同关系
        /// </summary>
        /// <param name="vendorContractID"></param>
        /// <returns></returns>
        [OperationContract]
        VendorContractEntity SelectSingleVendorContract(System.String vendorContractID);

        /// <summary>
        /// 新增单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void InsertSingleVendorContract(VendorContractEntity entity);

        /// <summary>
        /// 更新单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void UpdateSingleVendorContract(VendorContractEntity entity);

        /// <summary>
        /// 删除单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteSingleVendorContract(VendorContractEntity entity);

        #endregion VendorContract相关方法

        #region 获取结算周期

        /// <summary>
        /// 获取结算周期
        /// </summary>
        /// <param name="cycleType">固定/百分比</param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetCycleItems(string cycleType);

        /// <summary>
        /// 获取给定EntityID相同EntityTypeName与EntityName的最新的EntityID
        /// </summary>
        /// <param name="strEntityID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetLatestEntityID(string strEntityID);

        #endregion
    }
}