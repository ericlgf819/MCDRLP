using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace MCD.RLPlanning.IServices.Report
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IReportService : IBaseService
    {
        /// <summary>
        /// 按页返回合同到期预警报表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectContractExpireWarningPagedResult(Guid? AreaId, string CompanyCode, string EntityType, string StoreNo, string KioskName,
            int ExpireYear, int PageIndex, int PageSize, out int RecordCount);

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
        [OperationContract]
        byte[] SelectKioskStoreRelationChangReport(Guid? AreaID, string CompanyCode, string StoreNo, string KioskNo, 
            DateTime StartMonth, DateTime EndMonth);

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
        [OperationContract]
        byte[] SelectRentCalculateReport(Guid? AreaID, string CompanyCode, string EntityScope, string StoreNo, int CloseAcountYear, DateTime? StartMonth, DateTime? EndMonth,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent);

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
        [OperationContract]
        byte[] SelectYearlyCompareSales(Guid? AreaID, string CompanyCode, int Year, string EntityScope,
            bool OnlyContainOpenBefore, string ContractType,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent);

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
        [OperationContract]
        byte[] SelectAverageCompare(Guid? AreaID, string CompanyCode, string EntityScope, int EarliestOpenYear, int RentYear,
            bool FixManagement, bool FixRent, bool RadioService, bool RadioManagement, bool RadioRent, bool StraightRent);

        /// <summary>
        /// 返回Sales报表数据
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strCompanyCode"></param>
        /// <param name="strYear"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectStoreKioskSalesReport(string strAreaID, string strStoreNo, string strCompanyCode, string strYear,
            string strEntityType, bool IsCashYear, int pageIndex, int pageSize, out int recordCount);

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
        [OperationContract]
        byte[] SelectChangeReport(string strAreaID, string strCompanyCode, string strStoreOrKioskNo,
            DateTime startDate, DateTime endDate, string strChangeType, int pageIndex, int pageSize, out int recordCount);
    }
}