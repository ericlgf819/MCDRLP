using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyService : BaseDAL<CompanyEntity>, ICompanyService
    {
        /// <summary>
        /// 查找所有的公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllCompany(CompanyEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }
        /// <summary>
        /// 查找所有的公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectAllCompanyWithArea(CompanyEntity entity)
        {
            DataSet ds = base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "RLP_Master_SelectUserCompanyWithArea";
                //
                if (!string.IsNullOrEmpty(entity.UserID))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = new Guid(entity.UserID) });
                }
                if (!string.IsNullOrEmpty(entity.Status))
                {
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1) { Value = entity.Status });
                }
            });

            return base.Serilize(ds);
        }
        /// <summary>
        /// 查找單個公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CompanyEntity SelectSingleCompany(CompanyEntity entity)
        {
            return base.GetSingleEntity(entity);
        }

        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddSingleCompany(CompanyEntity entity)
        {
            int nReturn =0;
            //
            List<object> Info = base.ExecuteProcedureParams(entity, "InsertEntity");        
            int.TryParse(Info[0].ToString(), out nReturn);
            return nReturn;
        }
        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleCompany(CompanyEntity entity)
        {
            int nReturn = 0;
            //
            IDataReader reader = base.ExecuteProcedureDataReader(entity, "UpdateEntity");
            while (reader.Read())
            {
                nReturn = reader.GetInt32(0);
            }
            return nReturn;
        }
        /// <summary>
        /// 删除單個公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSingleCompany(CompanyEntity entity)
        {
            return base.DeleteSingleEntity(entity);
        }
    }
}