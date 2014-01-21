using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MCD.RLPlanning.IServices.Report
{
    /// <summary>
    /// 注意: 如果更改此处的接口名称 "IUserOperationService"，也必须更新 Web.config 中对 "IUserOperationService" 的引用。
    /// </summary>
    [ServiceContract]
    public interface IUserOperationService : IBaseService
    {
        /// <summary>
        /// 查询用户操作日志信息
        /// </summary>
        /// <param name="companyStartNo"></param>
        /// <param name="companyEndNo"></param>
        /// <param name="storeStartNo"></param>
        /// <param name="storeEndNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectUserOperations(int? companyStartNo, int? companyEndNo, int? storeStartNo, int? storeEndNo,
            DateTime startDate, DateTime endDate, string operationType);

        /// <summary>
        /// 查询用户表所包含的字段信息(需要显示的)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectTableColumns(string tableName);
    }
}