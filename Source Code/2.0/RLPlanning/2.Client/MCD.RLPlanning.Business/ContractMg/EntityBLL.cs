using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.BLL.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBLL : BaseBLL<IEntityService>
    {
        #region 实体相关代码

        private static Dictionary<string, List<string>> s_RentTypeDic = new Dictionary<string, List<string>>();

        /// <summary>
        /// 根据合同快照ID获取关联的实体
        /// /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        public DataSet SelectEntitiesByContractSnapshotID(string contractSnapshotID)
        {
            return base.DeSerilize(base.WCFService.SelectEntitiesByContractSnapshotID(contractSnapshotID));
        }

        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public EntityEntity SelectSingleEntity(string entityID)
        {
            return base.WCFService.SelectSingleEntity(entityID);
        }

        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleEntity(EntityEntity entity)
        {
            base.WCFService.InsertSingleEntity(entity);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSingleEntity(EntityEntity entity)
        {
            base.WCFService.UpdateSingleEntity(entity);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleEntity(EntityEntity entity)
        {
            base.WCFService.DeleteSingleEntity(entity);
        }

        /// <summary>
        ///  根据租金类型名称查找租金类型
        /// </summary>
        /// <param name="entityTypeName">租金类型名称</param>
        public List<string> SelectRentTypeByEntityTypeName(string entityTypeName)
        {
            List<string> rentTypeList = new List<string>();
            //
            if (!EntityBLL.s_RentTypeDic.ContainsKey(entityTypeName))
            {
                DataSet ds = DeSerilize(base.WCFService.SelectRentTypeByEntityTypeName(entityTypeName));
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        rentTypeList.Add(row["RentTypeName"].ToString());
                    }
                }
            }
            else
            {
                rentTypeList = EntityBLL.s_RentTypeDic[entityTypeName];
            }
            return rentTypeList;
        }
        #endregion

        #region 业主实体相关代码

        /// <summary>
        /// 查询所有业主实体关系
        /// </summary>
        /// <returns></returns>
        public DataSet SelectVendorEntitiesByEntityID(string entityID)
        {
            return base.DeSerilize(base.WCFService.SelectVendorEntitiesByEntityID(entityID));
        }

        /// <summary>
        /// 查询单个业主实体关系
        /// </summary>
        /// <param name="vendorEntityID"></param>
        /// <returns></returns>
        public VendorEntityEntity SelectSingleVendorEntity(System.String vendorEntityID)
        {
            return base.WCFService.SelectSingleVendorEntity(vendorEntityID);
        }

        /// <summary>
        /// 新增单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleVendorEntity(VendorEntityEntity entity)
        {
            base.WCFService.InsertSingleVendorEntity(entity);
        }

        /// <summary>
        /// 更新单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSinglVendorEntity(VendorEntityEntity entity)
        {
            base.WCFService.UpdateSingleVendorEntity(entity);
        }

        /// <summary>
        /// 删除单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleVendorEntity(VendorEntityEntity entity)
        {
            base.WCFService.DeleteSingleVendorEntity(entity);
        }

        /// <summary>
        /// 通过实体ID删除业主实体关系
        /// </summary>
        /// <param name="entityID"></param>
        public void DeleteVendorEntityByEntityID(string entityID)
        {
            base.WCFService.DeleteVendorEntityByEntityID(entityID);
        }

        /// <summary>
        /// 通过合同和业主查询实体信息
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        public DataTable SelectEntitiesByContractAndVendor(string contractSnapshotID, string vendorNo)
        {
            DataTable dt = null;
            //
            DataSet ds = DeSerilize(base.WCFService.SelectEntitiesByContractAndVendor(contractSnapshotID, vendorNo));
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 检查业主实体关系
        /// </summary>
        /// <param name="enityID"></param>
        /// <param name="vendorNoArray">当前选中的业主的ID集合，用英文逗号分隔</param>
        public void CheckVendorEntity(string enityID, string vendorNoArray, string partComment)
        {
            base.WCFService.CheckVendorEntity(enityID, vendorNoArray, partComment);
        }

        #endregion

        #region 实体信息设置区
        /// <summary>
        /// 查询所有实体信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllEntityInfoSetting()
        {
            return base.DeSerilize(base.WCFService.SelectAllEntityInfoSetting());
        }

        /// <summary>
        /// 根据实体信息ID查询所有实体信息
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <returns></returns>
        public EntityInfoSettingEntity SelectSingleEntityInfoSetting(System.String entityInfoSettingID)
        {
            return base.WCFService.SelectSingleEntityInfoSetting(entityInfoSettingID);
        }

        /// <summary>
        /// 新增加实体信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            base.WCFService.InsertSingleEntityInfoSetting(entity);
        }

        /// <summary>
        /// 修改实体信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSinglEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            base.WCFService.UpdateSingleEntityInfoSetting(entity);
        }

        /// <summary>
        /// 新增或更新实体信息设置
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            base.WCFService.InsertOrUpdateEntityInfoSetting(entity);
        }

        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            base.WCFService.DeleteSingleEntityInfoSetting(entity);
        }
        #endregion

        /// <summary>
        /// AP/GL的周期结束时间值
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public DateTime GetEntityAPGLMaxCycleEndDate(string entityID)
        {
            return base.WCFService.GetEntityAPGLMaxCycleEndDate(entityID);
        }

        /// <summary>
        /// 获取是否存在和指定Entity关联的AP/GL
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool GetEntityAPGLIsRunning(string entityID)
        {
            return base.WCFService.GetEntityAPGLIsRunning(entityID);
        }

        /// <summary>
        /// 验证当前Entity的租赁起始与结束时间是否与该Entity其它时间段有交集
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ValidateRentBeginEndDate(EntityEntity entity)
        {
            return base.WCFService.ValidateRentBeginEndDate(entity);
        }
    }
}