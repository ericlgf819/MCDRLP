using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Data;

namespace MCD.RLPlanning.IServices.Common
{
    /// <summary>
    /// 执行Sql语句的契约。
    /// </summary>
    public interface ISqlHelper : IBaseService
    {
        /// <summary>
        /// 执行指定的select语句，返回DataSet对象。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ExcuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] parms);

        /// <summary>
        /// 执行指定的sql，返回受影响的行数。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract]
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] parms);

        /// <summary>
        /// 执行指定的sql，返回IDataReader对象。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract]
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] parms);

        /// <summary>
        /// 执行指定的sql，返回首行首列的值。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract]
        object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] parms);
    }
}