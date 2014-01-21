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

using MCD.Common;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeCodeService : BaseDAL<TypeCodeEntity>, ITypeCodeService
    {
        #region ITypeCodeService 成员

        /// <summary>
        /// 查找所有typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllTypeCode(TypeCodeEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }
        /// <summary>
        /// 查找單個typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TypeCodeEntity SelectSingleTypeCode(TypeCodeEntity entity)
        {
            return base.GetSingleEntity(entity);
        }
        /// <summary>
        /// 通过租金类型名称获取TypeCode
        /// </summary>
        /// <param name="rentTypeName"></param>
        /// <returns></returns>
        public TypeCodeEntity SelectTypeCodeByRentTypeName(string rentTypeName)
        {
            TypeCodeEntity entity = null;
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("[dbo].[usp_Master_SelectTypeCodeByRentTypeName]");
            db.AddInParameter(cmd, "@RentTypeName", DbType.String, rentTypeName);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new TypeCodeEntity();
                //
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ReflectHelper.SetPropertiesByDataRow<TypeCodeEntity>(ref entity, row);
            }
            return entity;
        }

        /// <summary>
        /// 获取有效的TypeCode，状态为已生效，且未创建快照
        /// </summary>
        /// <returns></returns>
        public byte[] SelectActiveTypeCode()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetSqlStringCommand(@"SELECT * FROM dbo.TypeCode 
                WHERE Status='已生效' AND SnapshotCreateTime IS NULL ORDER BY TypeCodeName");
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds != null)
            {
                return base.Serilize(ds);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 检查typecode录入规则
        /// </summary>
        /// <param name="typeCode">typecode</param>
        /// <returns>1:合法；0:非法</returns>
        public int CheckInput(string typeCodeName)
        {
            return base.ExecuteProcedureInt((cmd) => {
                cmd.CommandText = "usp_Master_CheckTypeCode";
                cmd.Parameters.Add(new SqlParameter("@TypeCodeName", SqlDbType.NVarChar, 50) { Value = typeCodeName });
            });
        }
        /// <summary>
        /// 通过快照ID，找到TYPECODE名称
        /// </summary>
        /// <param name="id">快照ID</param>
        /// <returns>TYPECODE名称</returns>
        public string GetTypeCodeByID(string id)
        {
            string result = String.Empty;
            base.ExecuteProcedure((cmd) => {
                cmd.CommandText = "usp_Master_SelectTypeCodeByID";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 50) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                result = Convert.ToString(cmd.Parameters["@Res"].Value);
            });
            return result;
        }
        /// <summary>
        /// 判断typecode是否处于修改状态
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public int IsWorkFlowMod(string typeCodeName)
        {
            int result = 0;
            base.ExecuteProcedure((cmd) => {
                cmd.CommandText = "usp_Master_SelectTypeCodeByName";
                cmd.Parameters.Add(new SqlParameter("@TypeCodeName", SqlDbType.NVarChar, 50) { Value = typeCodeName });
                cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                result = Convert.ToInt32(cmd.Parameters["@Res"].Value);
            });
            return result;
        }

        /// <summary>
        /// 判断是否存在租金类型和实体类型的组合
        /// </summary>
        /// <param name="rentType">租金类型</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public bool IsExistsRentTypeAndEntityType(string typeCodeSnapshotID, string rentType, string entityType)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            string sql = string.Format("SELECT dbo.fn_TypeCode_IsEntityRentTypeExist('{0}','{1}','{2}')", typeCodeSnapshotID, entityType, rentType);
            return Convert.ToBoolean(db.ExecuteScalar(CommandType.Text,sql));
        }
        /// <summary>
        /// 是否TYPECODE已经存在
        /// </summary>
        /// <param name="typeCodeSnapshotID"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public bool IsExistsTypeCode(string typeCodeSnapshotID, string typeCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            string sql = string.Format("SELECT dbo.fn_TypeCode_IsTypeCodeExist('{0}','{1}')", typeCodeSnapshotID, typeCode);
            return Convert.ToBoolean(db.ExecuteScalar(CommandType.Text,sql));
        }
        #endregion
    }
}