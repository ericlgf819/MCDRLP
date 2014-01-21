using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.DALCommon;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.Services.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityService : BaseDAL<ContractEntity>, IEntityService
    {
        #region IEntityService

        /// <summary>
        /// 根据合同快照ID获取关联的实体
        /// /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        public byte[] SelectEntitiesByContractSnapshotID(string contractSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_SelectEntitiesByContractSnapshotID");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public EntityEntity SelectSingleEntity(string entityID)
        {
            EntityEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_SelectSingleEntity");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new EntityEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<EntityEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleEntity(EntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_InsertEntity");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@EntityTypeName", DbType.String, entity.EntityTypeName);
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@EntityNo", DbType.String, entity.EntityNo);
            db.AddInParameter(cmd, "@EntityName", DbType.String, entity.EntityName);
            db.AddInParameter(cmd, "@StoreOrDept", DbType.String, entity.StoreOrDept);
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, entity.StoreOrDeptNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, entity.KioskNo);
            db.AddInParameter(cmd, "@OpeningDate", DbType.DateTime, entity.OpeningDate);
            db.AddInParameter(cmd, "@RentStartDate", DbType.DateTime, entity.RentStartDate);
            db.AddInParameter(cmd, "@RentEndDate", DbType.DateTime, entity.RentEndDate);
            db.AddInParameter(cmd, "@IsCalculateAP", DbType.Boolean, entity.IsCalculateAP);
            db.AddInParameter(cmd, "@APStartDate", DbType.DateTime, entity.APStartDate);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSingleEntity(EntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_UpdateEntity");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@EntityTypeName", DbType.String, entity.EntityTypeName);
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@EntityNo", DbType.String, entity.EntityNo);
            db.AddInParameter(cmd, "@EntityName", DbType.String, entity.EntityName);
            db.AddInParameter(cmd, "@StoreOrDept", DbType.String, entity.StoreOrDept);
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, entity.StoreOrDeptNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, entity.KioskNo);
            db.AddInParameter(cmd, "@OpeningDate", DbType.DateTime, entity.OpeningDate);
            db.AddInParameter(cmd, "@RentStartDate", DbType.DateTime, entity.RentStartDate);
            db.AddInParameter(cmd, "@RentEndDate", DbType.DateTime, entity.RentEndDate);
            db.AddInParameter(cmd, "@IsCalculateAP", DbType.Boolean, entity.IsCalculateAP);
            db.AddInParameter(cmd, "@APStartDate", DbType.DateTime, entity.APStartDate);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleEntity(EntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_DeleteEntity");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        ///  根据租金类型名称查找租金类型
        /// </summary>
        /// <param name="entityTypeName">租金类型名称</param>
        /// <returns></returns>
        public byte[] SelectRentTypeByEntityTypeName(string entityTypeName)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT * FROM dbo.fn_TypeCode_SelectRentTypeByEntityTypeName(@EntityTypeName)");
            db.AddInParameter(cmd, "@EntityTypeName", DbType.String, entityTypeName);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
        #endregion

        #region 业主实体关系相关代码

        /// <summary>
        /// 根据实体ID查询业主实体关系
        /// </summary>
        /// <returns></returns>
        public byte[] SelectVendorEntitiesByEntityID(string entityID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectVendorEntitiesByEntityID]");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询单个业主实体关系
        /// </summary>
        /// <param name="vendorEntityID"></param>
        /// <returns></returns>
        public VendorEntityEntity SelectSingleVendorEntity(System.String vendorEntityID)
        {
            VendorEntityEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleVendorEntity]");
            db.AddInParameter(cmd, "@VendorEntityID", DbType.String, vendorEntityID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new VendorEntityEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<VendorEntityEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleVendorEntity(VendorEntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertVendorEntity]");
            db.AddInParameter(cmd, "@VendorEntityID", DbType.String, entity.VendorEntityID);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSingleVendorEntity(VendorEntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateVendorEntity]");
            db.AddInParameter(cmd, "@VendorEntityID", DbType.String, entity.VendorEntityID);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个业主实体关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleVendorEntity(VendorEntityEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteVendorEntity]");
            db.AddInParameter(cmd, "@VendorEntityID", DbType.String, entity.VendorEntityID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 通过实体ID删除业主实体关系
        /// </summary>
        /// <param name="entityID"></param>
        public void DeleteVendorEntityByEntityID(string entityID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            string sql = "DELETE FROM dbo.VendorEntity WHERE EntityID = @EntityID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 通过合同和业主查询实体信息
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        public byte[] SelectEntitiesByContractAndVendor(string contractSnapshotID, string vendorNo)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectEntitiesByContractAndVendor]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, vendorNo);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);            
        }

        /// <summary>
        /// 检查业主实体关系
        /// </summary>
        /// <param name="enityID"></param>
        /// <param name="vendorNoArray">当前选中的业主的ID集合，用英文逗号分隔</param>
        public void CheckVendorEntity(string enityID, string vendorNoArray,string partComment)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_CheckVendorEntity]");
            db.AddInParameter(cmd, "@EntityID", DbType.String, enityID);
            db.AddInParameter(cmd, "@VendorNoArray", DbType.String, vendorNoArray);
            db.AddInParameter(cmd, "@PartComment", DbType.String, partComment);
            db.ExecuteNonQuery(cmd);            
        }
        #endregion

        #region 实体信息设置区

        /// <summary>
        /// 查询所有实体信息
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllEntityInfoSetting()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllEntityInfoSettings]");
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 根据实体信息ID查询所有实体信息
        /// </summary>
        /// <param name="entityInfoSettingID"></param>
        /// <returns></returns>
        public EntityInfoSettingEntity SelectSingleEntityInfoSetting(System.String entityInfoSettingID)
        {
            EntityInfoSettingEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleEntityInfoSetting]");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entityInfoSettingID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new EntityInfoSettingEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<EntityInfoSettingEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增加实体信息
        /// </summary>
        /// <param name="entity"></param>
        public void InsertSingleEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertEntityInfoSetting]");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.AddInParameter(cmd, "@RealestateSales", DbType.Decimal, entity.RealestateSales);
            db.AddInParameter(cmd, "@MarginAmount", DbType.Decimal, entity.MarginAmount);
            db.AddInParameter(cmd, "@MarginRemark", DbType.String, entity.MarginRemark);
            db.AddInParameter(cmd, "@TaxRate", DbType.Decimal, entity.TaxRate);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 修改实体信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateSingleEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateEntityInfoSetting]");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.AddInParameter(cmd, "@RealestateSales", DbType.Decimal, entity.RealestateSales);
            db.AddInParameter(cmd, "@MarginAmount", DbType.Decimal, entity.MarginAmount);
            db.AddInParameter(cmd, "@MarginRemark", DbType.String, entity.MarginRemark);
            db.AddInParameter(cmd, "@TaxRate", DbType.Decimal, entity.TaxRate);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 新增或更新实体信息设置
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrUpdateEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertOrUpdateEntityInfoSetting]");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.AddInParameter(cmd, "@RealestateSales", DbType.Decimal, entity.RealestateSales);
            db.AddInParameter(cmd, "@MarginAmount", DbType.Decimal, entity.MarginAmount);
            db.AddInParameter(cmd, "@MarginRemark", DbType.String, entity.MarginRemark);
            db.AddInParameter(cmd, "@TaxRate", DbType.Decimal, entity.TaxRate);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        ///  删除实体信息
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteSingleEntityInfoSetting(EntityInfoSettingEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteEntityInfoSetting]");
            db.AddInParameter(cmd, "@EntityInfoSettingID", DbType.String, entity.EntityInfoSettingID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion

        /// <summary>
        /// AP/GL的周期结束时间值
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public DateTime GetEntityAPGLMaxCycleEndDate(string entityID)
        {
            DateTime date = new DateTime(1900, 1, 1);
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT  dbo.fn_Contract_GetEntityAPGLMaxCycleEndDate(@EntityID)");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            object value = db.ExecuteScalar(cmd);
            if (value != null)
            {
                date = DateTime.Parse(value.ToString());
            }
            return date;
        }

        /// <summary>
        /// 获取是否存在和指定Entity关联的AP/GL
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool GetEntityAPGLIsRunning(string entityID)
        {
            bool result = true;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT  dbo.[fn_Contract_GetEntityAPGLIsRunning](@EntityID)");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            object value = db.ExecuteScalar(cmd);
            if (value != null)
            {
                result = bool.Parse(value.ToString());
            }
            return result;
        }

        /// <summary>
        /// 验证当前Entity的租赁起始与结束时间是否与该Entity其它时间段有交集
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ValidateRentBeginEndDate(EntityEntity entity)
        {
            bool bRet = true;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[RLPlanning_Contract_ValidateRentDate]");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entity.EntityID);
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, entity.StoreOrDeptNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, entity.KioskNo);
            db.AddInParameter(cmd, "@RentStartDate", DbType.DateTime, entity.RentStartDate);
            db.AddInParameter(cmd, "@RentEndDate", DbType.DateTime, entity.RentEndDate);
            
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            bRet = (bool)(dt.Rows[0][0]);
            return bRet;
        }
    }
}