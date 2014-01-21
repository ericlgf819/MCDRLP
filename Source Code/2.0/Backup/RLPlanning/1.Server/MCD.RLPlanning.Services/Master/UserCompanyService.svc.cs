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
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.Services.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCompanyService : BaseDAL<UserCompanyEntity>, IUserCompanyService
    {
        /// <summary>
        /// 查询所有用户公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectUserCompany(UserCompanyEntity entity)
        {
            return base.Serilize(base.GetDataSet(entity));
        }
        /// <summary>
        /// 查询所有用户区域信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] SelectUserArea(UserCompanyEntity entity)
        {
            return base.Serilize(base.ExecuteProcedureDataSet((cmd) =>
            {
                cmd.CommandText = "RLP_Master_SelectUserArea";
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = entity.UserId });
                if (entity.Status != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1) { Value = entity.Status });
                }
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateUserCompany(UserCompanyEntity entity)
        {
            base.ExecuteProcedure((cmd) => {
                cmd.CommandText = "RLP_Master_UpdateUserCompany";
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = entity.UserId });
                if (entity.CompanyCode != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CompanyCodes", SqlDbType.NVarChar) { Value = entity.CompanyCode });
                }
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>
        /// 更新单个用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteUserCompany(UserCompanyEntity entity)
        {
            base.ExecuteProcedure((cmd) => {
                cmd.CommandText = "RLP_Master_DeleteUserCompany";
                if (entity.UserId != Guid.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = entity.UserId });
                }
                if (entity.CompanyCode != null){
                    cmd.Parameters.Add(new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = entity.CompanyCode });
                }
                cmd.ExecuteNonQuery();
            });
        }
    }
}