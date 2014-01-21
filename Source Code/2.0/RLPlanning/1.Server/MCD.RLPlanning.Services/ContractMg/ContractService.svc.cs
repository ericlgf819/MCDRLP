using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using MCD.RLPlanning.IServices;
using System.Data;
using MCD.RLPlanning.Entity.Master;
using MCD.Framework.SqlDAL;
using System.Data.SqlClient;
using MCD.RLPlanning.IServices.ContractMg;
using MCD.RLPlanning.Entity.ContractMg;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using MCD.Framework.DALCommon;
using MCD.Common;
using System.Configuration;

namespace MCD.RLPlanning.Services.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractService : BaseDAL<ContractEntity>, IContractService
    {
        #region IContractService

        public void CreateAPGLByHand()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Service_AllDailyServiceByHand");
            cmd.CommandTimeout = 60 * 12;
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据条件查找合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectContracts(string storeOrDeptNo, string vendorNo, string companyNo, string contractNo, Guid area, string status, bool? bFromSRLS, Guid UserID, int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_SelectContract");
            db.AddInParameter(cmd, "@StoreOrDeptNo", DbType.String, storeOrDeptNo);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, vendorNo);
            db.AddInParameter(cmd, "@CompanyNo", DbType.String, companyNo);
            db.AddInParameter(cmd, "@ContractNo", DbType.String, contractNo);
            db.AddInParameter(cmd, "@Area", DbType.Guid, area);
            db.AddInParameter(cmd, "@Status", DbType.String, status);
            db.AddInParameter(cmd, "@UserID", DbType.Guid, UserID);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);

            if (bFromSRLS.HasValue)
                db.AddInParameter(cmd, "@FromSRLS", DbType.Boolean, bFromSRLS.Value);

            DataSet ds = db.ExecuteDataSet(cmd);
            //
            object obj = cmd.Parameters["@RecordCount"].Value;
            recordCount = (obj == null ? 0 : (int)obj);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取合同历史版本
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public byte[] SelectContractHistory(string contractID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.usp_Contract_SelectContractHistory");
            db.AddInParameter(cmd, "@ContractID", DbType.String, contractID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ContractEntity SelectSingleContract(ContractEntity entity)
        {
            ContractEntity result = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = new ContractEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<ContractEntity>(ref result, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return result;
        }

        /// <summary>
        /// 更新单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<object> UpdateSingleContract(ContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateSingleContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, entity.CompanyCode);
            db.AddInParameter(cmd, "@ContractID", DbType.String, entity.ContractID);
            db.AddInParameter(cmd, "@ContractNO", DbType.String, entity.ContractNO);
            db.AddInParameter(cmd, "@Version", DbType.String, entity.Version);
            db.AddInParameter(cmd, "@ContractName", DbType.String, entity.ContractName);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanySimpleName", DbType.String, entity.CompanySimpleName);
            db.AddInParameter(cmd, "@CompanyRemark", DbType.String, entity.CompanyRemark);
            db.AddInParameter(cmd, "@Status", DbType.String, entity.Status);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@UpdateInfo", DbType.String, entity.UpdateInfo);
            db.AddInParameter(cmd, "@IsLocked", DbType.Boolean, entity.IsLocked);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@CreatorName", DbType.String, entity.CreatorName);
            db.AddInParameter(cmd, "@LastModifyTime", DbType.DateTime, entity.LastModifyTime);
            db.AddInParameter(cmd, "@LastModifyUserName", DbType.String, entity.LastModifyUserName);
            db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);
            db.AddInParameter(cmd, "@IsSave", DbType.Boolean, entity.IsSave);
            db.AddOutParameter(cmd, "@Res", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            //
            return new List<object>() { cmd.Parameters["@Res"].Value };
        }

        /// <summary>
        /// 新增单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<object> InsertSingleContract(ContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, entity.CompanyCode);
            db.AddInParameter(cmd, "@ContractID", DbType.String, entity.ContractID);
            db.AddInParameter(cmd, "@ContractNO", DbType.String, entity.ContractNO);
            db.AddInParameter(cmd, "@Version", DbType.String, entity.Version);
            db.AddInParameter(cmd, "@ContractName", DbType.String, entity.ContractName);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanySimpleName", DbType.String, entity.CompanySimpleName);
            db.AddInParameter(cmd, "@CompanyRemark", DbType.String, entity.CompanyRemark);
            db.AddInParameter(cmd, "@Status", DbType.String, entity.Status);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@UpdateInfo", DbType.String, entity.UpdateInfo);
            db.AddInParameter(cmd, "@IsLocked", DbType.Boolean, entity.IsLocked);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@CreatorName", DbType.String, entity.CreatorName);
            db.AddInParameter(cmd, "@LastModifyTime", DbType.DateTime, entity.LastModifyTime);
            db.AddInParameter(cmd, "@LastModifyUserName", DbType.String, entity.LastModifyUserName);
            db.AddInParameter(cmd, "@SnapshotCreateTime", DbType.DateTime, entity.SnapshotCreateTime);
            db.AddInParameter(cmd, "@IsSave", DbType.Boolean, entity.IsSave);
            db.AddOutParameter(cmd, "@Res", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            //
            return new List<object>() { cmd.Parameters["@Res"].Value };
        }

        /// <summary>
        /// 删除单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSingleContract(ContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteSingleContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddOutParameter(cmd, "@Res", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            //
            return Convert.ToInt32(cmd.Parameters["@Res"].Value);
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="currentUserID"></param>
        public void DeleteContract(string contractSnapshotID, string currentUserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据合同快照ID找出所关联的所有表
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns></returns>
        public byte[] SelectAllRentRuleInfosByContractSnapshotID(string contractSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectAllRentRuleInfosByContractSnapshotID]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 新增合同但未保存时执行的操作
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="copyType"></param>
        public void UndoContract(string contractSnapshotID, ContractCopyType copyType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UndoContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            db.AddInParameter(cmd, "@CopyType", DbType.String, copyType.ToString());
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 复制合同，用于变更和续租合同
        /// </summary>
        /// <param name="contractSnapshotID">要复制的合同</param>
        /// <param name="operatorID">操作人ID</param>
        /// <param name="copyType">复制类型，变更/续租</param>
        /// <returns>新合同快照ID</returns>
        public string CopyContract(string contractSnapshotID, string operatorID, string copyType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_CopyContract]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            db.AddInParameter(cmd, "@OperationID", DbType.String, operatorID);
            db.AddInParameter(cmd, "@CopyType", DbType.String, copyType.ToString());
            db.AddOutParameter(cmd, "@NewContractSnapshotID", DbType.String, 50);
            db.ExecuteNonQuery(cmd);
            //
            return db.GetParameterValue(cmd, "@NewContractSnapshotID").ToString();
        }

        /// <summary>
        /// 校验合同信息
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns>IndexId int,Code NVARCHAR(50), RelationData NVARCHAR(50),CheckMessage NVARCHAR(500)</returns>
        public byte[] CheckContract(string contractSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_CheckContract]");
            cmd.CommandTimeout = 0;
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        public byte[] ImportContracts(DataTable dt,string importType, string currentUserID, 
            out int totalCount, out int successCount,out int failCount)
        {
            totalCount = successCount = failCount = 0;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                copy.BulkCopyTimeout = 500;
                try
                {
                    // 清空Import_Contract
                    db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_Contract");
                    //用Column的SetOrdinal就可以调整列的位置了
                    //for (int i = 0; i < 36; i++)//错位一格对应起来
                    //{
                    //    copy.ColumnMappings.Add(i, i + 1);
                    //}
                    //copy.ColumnMappings.Add(36, 0);//Excel行号对应关系

                    copy.DestinationTableName = "Import_Contract";
                    copy.WriteToServer(dt);

                    //执行导入，将临时表的数据导入到正式表中
                    DbCommand cmd = db.GetStoredProcCommand("[usp_Import_Contract]");
                    db.AddInParameter(cmd, "@ImportType", DbType.String, importType);
                    db.AddInParameter(cmd, "@CurrentUserID", DbType.String, currentUserID);
                    db.AddOutParameter(cmd, "@TotalCount", DbType.Int32, 4);
                    db.AddOutParameter(cmd, "@SuccessCount", DbType.Int32, 4);
                    db.AddOutParameter(cmd, "@FailCount", DbType.Int32, 4);

                    DataSet ds = db.ExecuteDataSet(cmd);
                    totalCount = int.Parse(cmd.Parameters["@TotalCount"].Value.ToString());
                    successCount = int.Parse(cmd.Parameters["@SuccessCount"].Value.ToString());
                    failCount = int.Parse(cmd.Parameters["@FailCount"].Value.ToString());

                    return base.Serilize(ds);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // 写入数据库 或者 更新时出错
                }
            }
            return null;
        }

        /// <summary>
        /// 实体是否已经出了AP
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool IsEntityHasAP(string entityID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand("SELECT dbo.fn_Contract_IsEntityHasAP(@EntityID)");
            db.AddInParameter(cmd, "@EntityID", DbType.String, entityID);
            return (bool)db.ExecuteScalar(cmd);
        }
        #endregion

        #region VendorContract

        /// <summary>
        /// 查询所有业主合同关系
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        public byte[] SelectVendorContractByContractSnapshotID(string contractSnapshotID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectVendorContractsByContractSnapshotID]");
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, contractSnapshotID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 查询单个业主合同关系
        /// </summary>
        /// <param name="vendorContractID"></param>
        /// <returns></returns>
        public VendorContractEntity SelectSingleVendorContract(System.String vendorContractID)
        {
            VendorContractEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_SelectSingleVendorContract]");
            db.AddInParameter(cmd, "@VendorContractID", DbType.String, vendorContractID);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new VendorContractEntity();
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyHandler.SetPropertyValue<VendorContractEntity>(ref entity, dt.Columns[i].ColumnName, row[i]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleVendorContract(VendorContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_InsertVendorContract]");
            db.AddInParameter(cmd, "@VendorContractID", DbType.String, entity.VendorContractID);
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.AddInParameter(cmd, "@VendorName", DbType.String, entity.VendorName);
            db.AddInParameter(cmd, "@PayMentType", DbType.String, entity.PayMentType);
            db.AddInParameter(cmd, "@IsVirtual", DbType.Boolean, entity.IsVirtual);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSingleVendorContract(VendorContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_UpdateVendorContract]");
            db.AddInParameter(cmd, "@VendorContractID", DbType.String, entity.VendorContractID);
            db.AddInParameter(cmd, "@ContractSnapshotID", DbType.String, entity.ContractSnapshotID);
            db.AddInParameter(cmd, "@VendorNo", DbType.String, entity.VendorNo);
            db.AddInParameter(cmd, "@VendorName", DbType.String, entity.VendorName);
            db.AddInParameter(cmd, "@PayMentType", DbType.String, entity.PayMentType);
            db.AddInParameter(cmd, "@IsVirtual", DbType.Boolean, entity.IsVirtual);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleVendorContract(VendorContractEntity entity)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_DeleteVendorContract]");
            db.AddInParameter(cmd, "@VendorContractID", DbType.String, entity.VendorContractID);
            db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region 获取结算周期

        /// <summary>
        /// 获取结算周期
        /// </summary>
        /// <param name="cycleType"></param>
        /// <returns></returns>
        public byte[] GetCycleItems(string cycleType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Contract_GetCycleItem]");
            db.AddInParameter(cmd, "@CycleType", DbType.String, cycleType);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
        #endregion

        /// <summary>
        /// 获取给定EntityID相同EntityTypeName与EntityName的最新的EntityID
        /// </summary>
        /// <param name="strEntityID"></param>
        /// <returns></returns>
        public byte[] GetLatestEntityID(string strEntityID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[RLPlanning_UTIL_GetLatestEntityID]");
            db.AddInParameter(cmd, "@EntityID", DbType.String, strEntityID);
            DataSet ds = db.ExecuteDataSet(cmd);
            return base.Serilize(ds);
        }
    }
}