using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;

namespace MCD.RLPlanning.IServices.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IEntityService : IBaseService
    {
        #region 实体相关代码

        /// <summary>
        /// 根据合同快照ID获取关联的实体
        /// /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectEntitiesByContractSnapshotID(string contractSnapshotID);

        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityEntity SelectSingleEntity(string entityID);

        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void InsertSingleEntity(EntityEntity entity);

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void UpdateSingleEntity(EntityEntity entity);

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteSingleEntity(EntityEntity entity);


        /// <summary>
        ///  根据租金类型名称查找租金类型
        /// </summary>
        /// <param name="entityTypeName">租金类型名称</param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectRentTypeByEntityTypeName(string EntityTypeName);

        #endregion

        #region 业主实体关系相关代码

        /// <summary>
        /// 根据实体ID查询业主实体关系
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectVendorEntitiesByEntityID(string entityID);

        /// <summary>
        /// 查询单个业主实体关系
        /// </summary>
        /// <param name="vendorEntityID"></param>
        /// <returns></returns>
        [OperationContract]
        VendorEntityEntity SelectSingleVendorEntity(System.String vendorEntityID);

        /// <summary>
        /// 新增单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void InsertSingleVendorEntity(VendorEntityEntity entity);

        /// <summary>
        /// 更新单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void UpdateSingleVendorEntity(VendorEntityEntity entity);

        /// <summary>
        /// 删除单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteSingleVendorEntity(VendorEntityEntity entity);

        /// <summary>
        /// 通过实体ID删除业主实体关系
        /// </summary>
        /// <param name="entityID"></param>
        [OperationContract]
        void DeleteVendorEntityByEntityID(string entityID);


        /// <summary>
        /// 通过合同和业主查询实体信息
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectEntitiesByContractAndVendor(string contractSnapshotID, string vendorNo);

        /// <summary>
        /// 检查业主实体关系
        /// </summary>
        /// <param name="enityID"></param>
        /// <param name="vendorNoArray">当前选中的业主的ID集合，用英文逗号分隔</param>
        [OperationContract]
        void CheckVendorEntity(string enityID, string vendorNoArray,string partComment);

        #endregion

        #region 实体信息设置区
        /// <summary>
        /// 查询所有实体信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllEntityInfoSetting();

        /// <summary>
        /// 根据实体信息ID查询所有实体信息
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityInfoSettingEntity SelectSingleEntityInfoSetting(System.String entityInfoSettingID);

        /// <summary>
        /// 新增加实体信息
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertSingleEntityInfoSetting(EntityInfoSettingEntity entity);

        /// <summary>
        /// 修改实体信息
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void UpdateSingleEntityInfoSetting(EntityInfoSettingEntity entity);

        /// <summary>
        /// 新增或更新实体信息设置
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void InsertOrUpdateEntityInfoSetting(EntityInfoSettingEntity entity);

        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void DeleteSingleEntityInfoSetting(EntityInfoSettingEntity entity);


        #endregion
        /// <summary>
        /// 获取和指定Entity关联的AP/GL的周期结束时间值
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime GetEntityAPGLMaxCycleEndDate(string entityID);

        /// <summary>
        /// 获取是否存在和指定Entity关联的AP/GL
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [OperationContract]
        bool GetEntityAPGLIsRunning(string entityID);

        /// <summary>
        /// 验证当前Entity的租赁起始与结束时间是否与该Entity其它时间段有交集
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        bool ValidateRentBeginEndDate(EntityEntity entity);
    }
}