using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Report;
using MCD.RLPlanning.IServices.Report;

namespace MCD.RLPlanning.Services.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportService : BaseDAL<ReportEntity>, IReportService
    {
        /// <summary>
        /// 按页返回合同到期预警报表
        /// </summary>
        /// <returns></returns>
        public byte[] SelectContractExpireWarningPagedResult(Guid? AreaId, string CompanyCode, string EntityType, string StoreNo, string KioskName,
            int ExpireYear, int PageIndex, int PageSize, out int RecordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Report_SelectContractExpireWarning]");
            cmd.CommandTimeout = 600;
            if (AreaId.HasValue) 
            {
                db.AddInParameter(cmd, "@AreaId", DbType.Guid, AreaId.Value);
            }
            if (CompanyCode != null)
            {
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            }
            if (StoreNo != null)
            {
                db.AddInParameter(cmd, "@StoreNo", DbType.String, StoreNo);
            }
            if (KioskName != null)
            {
                db.AddInParameter(cmd, "@KioskName", DbType.String, KioskName);
            }
            if (EntityType != null)
            {
                db.AddInParameter(cmd, "@EntityType", DbType.String, EntityType);
            }
            db.AddInParameter(cmd, "@ExpireYear", DbType.Int32, ExpireYear);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, PageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);
            DataSet ds = db.ExecuteDataSet(cmd);
            object obj = cmd.Parameters["@RecordCount"].Value;
            //
            RecordCount = (obj == null || obj == DBNull.Value ? 0 : (int)obj);
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取租金
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="CloseAcountYear"></param>
        /// <param name="StartMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="FixManagement"></param>
        /// <param name="FixRent"></param>
        /// <param name="RadioService"></param>
        /// <param name="RadioManagement"></param>
        /// <param name="RadioRent"></param>
        /// <param name="StraightRent"></param>
        /// <returns></returns>
        public byte[] SelectKioskStoreRelationChangReport(Guid? AreaID, string CompanyCode, string StoreNo, string KioskNo,
            DateTime StartMonth, DateTime EndMonth)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[RLP_Report_KioskStoreRelationChangHistory]");
            cmd.CommandTimeout = 600;
            if (AreaID.HasValue)
            {
                db.AddInParameter(cmd, "@AreaID", DbType.Guid, AreaID);
            }
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            }
            db.AddInParameter(cmd, "@StoreNo", DbType.String, StoreNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, KioskNo);
            db.AddInParameter(cmd, "@StartMonth", DbType.Date, StartMonth);
            db.AddInParameter(cmd, "@EndMonth", DbType.Date, EndMonth);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取租金
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="CloseAcountYear"></param>
        /// <param name="StartMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="FixManagement"></param>
        /// <param name="FixRent"></param>
        /// <param name="RadioService"></param>
        /// <param name="RadioManagement"></param>
        /// <param name="RadioRent"></param>
        /// <param name="StraightRent"></param>
        /// <returns></returns>
        public byte[] SelectRentCalculateReport(Guid? AreaID, string CompanyCode, string EntityScope, string StoreNo, int CloseAcountYear, DateTime? StartMonth, DateTime? EndMonth,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[RLP_Report_RentCalculate]");
            cmd.CommandTimeout = 600;
            if (AreaID.HasValue)
            {
                db.AddInParameter(cmd, "@AreaID", DbType.Guid, AreaID);
            }
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            }
            db.AddInParameter(cmd, "@EntityScope", DbType.String, EntityScope);
            db.AddInParameter(cmd, "@StoreNo", DbType.String, StoreNo);
            db.AddInParameter(cmd, "@CloseAcountYear", DbType.Int32, CloseAcountYear);
            db.AddInParameter(cmd, "@StartMonth", DbType.Date, StartMonth);
            db.AddInParameter(cmd, "@EndMonth", DbType.Date, EndMonth);
            db.AddInParameter(cmd, "@FixManagement", DbType.Boolean, FixManagement);
            db.AddInParameter(cmd, "@FixRent", DbType.Boolean, FixRent);
            db.AddInParameter(cmd, "@RadioService", DbType.Boolean, RadioService);
            db.AddInParameter(cmd, "@RadioManagement", DbType.Boolean, RadioManagement);
            db.AddInParameter(cmd, "@RadioRent", DbType.Boolean, RadioRent);
            db.AddInParameter(cmd, "@StraightRent", DbType.Boolean, StraightRent);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取租金年度对比
        /// </summary>
        /// <param name="YearA"></param>
        /// <param name="YearB"></param>
        /// <param name="AreaID"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="EntityScope"></param>
        /// <param name="ContainContractChange"></param>
        /// <param name="OnlyContainOpenBefore"></param>
        /// <param name="ContainContractExtend"></param>
        /// <param name="FixManagement"></param>
        /// <param name="FixRent"></param>
        /// <param name="RadioService"></param>
        /// <param name="RadioManagement"></param>
        /// <param name="RadioRent"></param>
        /// <param name="StraightRent"></param>
        /// <returns></returns>
        public byte[] SelectYearlyCompareSales(Guid? AreaID, string CompanyCode, int Year, string EntityScope,
            bool OnlyContainOpenBefore, string ContractType,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[RLP_Report_YearlyCompare]");
            cmd.CommandTimeout = 600;
            if (AreaID.HasValue)
            {
                db.AddInParameter(cmd, "@AreaID", DbType.Guid, AreaID);
            }
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            }
            db.AddInParameter(cmd, "@Year", DbType.Int32, Year);
            db.AddInParameter(cmd, "@EntityScope", DbType.String, EntityScope);
            db.AddInParameter(cmd, "@OnlyContainOpenBefore", DbType.Boolean, OnlyContainOpenBefore);
            db.AddInParameter(cmd, "@ContractType", DbType.String, ContractType);
            //
            db.AddInParameter(cmd, "@FixManagement", DbType.Boolean, FixManagement);
            db.AddInParameter(cmd, "@FixRent", DbType.Boolean, FixRent);
            db.AddInParameter(cmd, "@RadioService", DbType.Boolean, RadioService);
            db.AddInParameter(cmd, "@RadioManagement", DbType.Boolean, RadioManagement);
            db.AddInParameter(cmd, "@RadioRent", DbType.Boolean, RadioRent);
            db.AddInParameter(cmd, "@StraightRent", DbType.Boolean, StraightRent);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 获取平均销售额和租金
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="EntityScope"></param>
        /// <param name="RentYear"></param>
        /// <param name="EarliestOpenYear"></param>
        /// <param name="FixManagement"></param>
        /// <param name="FixRent"></param>
        /// <param name="RadioService"></param>
        /// <param name="RadioManagement"></param>
        /// <param name="RadioRent"></param>
        /// <param name="StraightRent"></param>
        /// <returns></returns>
        public byte[] SelectAverageCompare(Guid? AreaID, string CompanyCode, string EntityScope, int EarliestOpenYear, int RentYear,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[RLP_Report_AverageCompare]");
            cmd.CommandTimeout = 600;
            if (AreaID.HasValue)
            {
                db.AddInParameter(cmd, "@AreaID", DbType.Guid, AreaID);
            }
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                db.AddInParameter(cmd, "@CompanyCode", DbType.String, CompanyCode);
            }
            db.AddInParameter(cmd, "@EntityScope", DbType.String, EntityScope);
            db.AddInParameter(cmd, "@EarliestOpenYear", DbType.Int32, EarliestOpenYear);
            db.AddInParameter(cmd, "@RentYear", DbType.Int32, RentYear);
            //
            db.AddInParameter(cmd, "@FixManagement", DbType.Boolean, FixManagement);
            db.AddInParameter(cmd, "@FixRent", DbType.Boolean, FixRent);
            db.AddInParameter(cmd, "@RadioService", DbType.Boolean, RadioService);
            db.AddInParameter(cmd, "@RadioManagement", DbType.Boolean, RadioManagement);
            db.AddInParameter(cmd, "@RadioRent", DbType.Boolean, RadioRent);
            db.AddInParameter(cmd, "@StraightRent", DbType.Boolean, StraightRent);
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        /// <summary>
        /// 返回Sales报表数据
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strCompanyCode"></param>
        /// <param name="strYear"></param>
        /// <returns></returns>
        public byte[] SelectStoreKioskSalesReport(string strAreaID, string strStoreNo, string strCompanyCode, string strYear,
            string strEntityType, bool IsCashYear, int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[RLPlanning_Report_SelectSales]");
            db.AddInParameter(cmd, "@AreaID", DbType.String, strAreaID);
            db.AddInParameter(cmd, "@StoreNo", DbType.String, strStoreNo);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, strCompanyCode);
            db.AddInParameter(cmd, "@Year", DbType.String, strYear);
            db.AddInParameter(cmd, "@EntityType", DbType.String, strEntityType);
            db.AddInParameter(cmd, "@IsCashYear", DbType.Boolean, IsCashYear);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);

            //将timeout时间放长
            cmd.CommandTimeout = 600;

            DataSet ds = db.ExecuteDataSet(cmd);
            object obj = cmd.Parameters["@RecordCount"].Value;
            recordCount = (obj == null || obj == DBNull.Value ? 0 : (int)obj);

            return Serilize(ds);
        }

        /// <summary>
        /// 返回合同变更报表
        /// </summary>
        /// <param name="strCompanyCode"></param>
        /// <param name="strStoreOrKioskNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="strChangeType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public byte[] SelectChangeReport(string strAreaID, string strCompanyCode, string strStoreOrKioskNo,
            DateTime startDate, DateTime endDate, string strChangeType, int pageIndex, int pageSize, out int recordCount)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[RLPlanning_Report_SelectChange]");
            db.AddInParameter(cmd, "@AreaID", DbType.String, strAreaID);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, strCompanyCode);
            db.AddInParameter(cmd, "@StoreOrKioskNo", DbType.String, strStoreOrKioskNo);
            db.AddInParameter(cmd, "@StartDate", DbType.Date, startDate);
            db.AddInParameter(cmd, "@EndDate", DbType.Date, endDate);
            db.AddInParameter(cmd, "@ChangeType", DbType.String, strChangeType);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);

            //将timeout时间放长
            cmd.CommandTimeout = 600;

            DataSet ds = db.ExecuteDataSet(cmd);
            object obj = cmd.Parameters["@RecordCount"].Value;
            recordCount = (obj == null || obj == DBNull.Value ? 0 : (int)obj);

            return Serilize(ds);
        }
    }
}