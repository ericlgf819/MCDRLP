using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.Framework.Entity;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.Entity.Common;

namespace MCD.RLPlanning.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectService : BaseDAL<DictionaryItem>, ISelectService
    {
        #region ISelectService
        
        /// <summary>
        /// 选择租金类型
        /// </summary>
        /// <returns></returns>
        public byte[] SelectRentType()
        {
            DataSet ds = base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "usp_Master_SelectRentType";
            });
            return base.Serilize(ds);
        }
        /// <summary>
        /// 选择实体类型
        /// </summary>
        /// <returns></returns>
        public byte[] SelectEntityType()
        {
            DataSet ds = base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "usp_Master_SelectEntityType";
            });
            return base.Serilize(ds);
        }
        /// <summary>
        /// 选择Typecode状态
        /// </summary>
        /// <returns></returns>
        public byte[] SelectTypeCodeStatus()
        {
            DataSet ds = base.ExecuteProcedureDataSet((cmd) => {
                cmd.CommandText = "usp_Master_SelectTypeCodeStatus";
            });
            return Serilize(ds);
        }

        /// <summary>
        /// 选择激活状态的ACCOUNT
        /// </summary>
        /// <returns></returns>
        public byte[] SelectActiveAccount()
        {
            DataSet ds = ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "usp_Master_SelectActiveAccount";
            });
            return Serilize(ds);
        }

        /// <summary>
        /// 获取指定键值的数据字典项集合。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetItemsByKeyName(string keyName)
        {
            List<DictionaryItem> items = new List<DictionaryItem>();
            //
            using (SqlDataReader dr = (SqlDataReader)base.ExecuteProcedureDataReader((cmd) =>
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [SRLS_TB_System_DictionaryItem] 
                    WHERE KeyID=(SELECT TOP 1 ID FROM SRLS_TB_System_DictionaryKey WHERE KeyName=@KeyName) 
                    ORDER BY OrderIndex DESC";
                cmd.Parameters.Add(new SqlParameter("@KeyName", SqlDbType.NVarChar, 32) { Value = keyName });
            }))
            {
                if (dr != null && dr.HasRows)
                {
                    items = base.SetAllEntity(dr);
                }
            }
            return items;
        }
        /// <summary>
        /// 获取指定键名称的数据字典项集合。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetItemsByKeyValue(string keyValue)
        {
            List<DictionaryItem> items = new List<DictionaryItem>();
            //
            using (SqlDataReader dr = (SqlDataReader)base.ExecuteProcedureDataReader((cmd) => {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [SRLS_TB_System_DictionaryItem] 
                    WHERE KeyID=(SELECT TOP 1 ID FROM SRLS_TB_System_DictionaryKey WHERE KeyValue=@KeyValue) 
                    ORDER BY OrderIndex DESC";
                cmd.Parameters.Add(new SqlParameter("@KeyValue", SqlDbType.NVarChar, 32) { Value = keyValue });
            }))
            {
                if (dr != null && dr.HasRows)
                {
                    items = base.SetAllEntity(dr);
                }
            }
            return items;
        }
        #endregion
    }
}