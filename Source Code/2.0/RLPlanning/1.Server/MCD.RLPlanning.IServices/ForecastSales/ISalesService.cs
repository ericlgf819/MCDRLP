using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.IServices.ForecastSales
{
    [ServiceContract]
    public interface ISalesService : IBaseService
    {
        #region Sales数据导入相关方法
        /// <summary>
        /// 导入Sales数据
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        byte[] ImportSales(byte[] byteTbl);

        /// <summary>
        /// 验证导入的公司是否有权限
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ValidateImportCompany(byte[] byteTbl, Guid UserID);

        /// <summary>
        /// 更新Sales数据
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        void UpdateSales(DataTable dt);

        /// <summary>
        /// 根据storeNo和kioskNo来查询Sales数据
        /// </summary>
        /// <param name="storeNo"></param>
        /// <param name="kioskNo"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectSales(string storeNo, string kioskNo);

        /// <summary>
        /// 根据类型、餐厅名称或编号返回餐厅、甜品店信息
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strStoreNoOrName"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageCount"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectStoreOrKiosk(string strType, string strStoreNoOrName, string strCompanyCode, string strStatus, 
                string strUserID, string strAreaID, int iPageIndex, int iPageSize, out int iRecordCount);

        /// <summary>
        /// 返回Excel模板的Dataset
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectImportSalesTemplate(string strUserID);

        /// <summary>
        /// 返回当前导入的数据的最小合同时间或者开店时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetMinRentStartDateOrOpenningDate(byte[] bytetbl);

        /// <summary>
        /// 验证Kiosk是否独立结算租金，将非独立结算sales的kiosk给剔除，并且返回名称
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ValidateKiosk(byte[] byteTbl);

        /// <summary>
        /// 验证导入的Sales所在年是否在开店年前，如果是剔除
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ValidateRentStartDate(byte[] byteTbl);

        /// <summary>
        /// 验证导入的实体是否存在，或者是否有效，如果不存在/无效 则剔除
        /// </summary>
        /// <param name="byteTbl"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ValidateEntityExistence(byte[] byteTbl);

        /// <summary>
        /// 验证公司编号与餐厅编号是否有关联
        /// </summary>
        /// <param name="byteTbl"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ValidateCompanyCodeAndStoreNo(byte[] byteTbl);
        #endregion
    }
}
