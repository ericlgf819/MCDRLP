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

using MCD.Framework.SqlDAL;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Synchronization;
using MCD.RLPlanning.IServices.Synchronization;

namespace MCD.RLPlanning.Services.Synchronization
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ScheduleService" in code, svc and config file together.
    public class ScheduleService : BaseDAL<ScheduleEntity>, IScheduleService
    {
        /// <summary>
        /// 获取所有同步计划
        /// </summary>
        /// <returns></returns>
        public byte[] SelectAllSchedule()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataSet ds = db.ExecuteDataSet("dbo.RLP_Synchronization_SelectAllSchedule");
            return base.Serilize(ds);
        }
        /// <summary>
        /// 获取同步计划
        /// </summary>
        /// <returns></returns>
        public ScheduleEntity SelectSingleStore(ScheduleEntity entity)
        {
            return base.GetSingleEntity(entity);
        }
        /// <summary>
        /// 新增同步计划
        /// </summary>
        /// <returns></returns>
        public int InsertSchedule(DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId)
        {
            int res = 0;
            base.ExecuteProcedure((cmd) =>
            {
                cmd.CommandText = "dbo.RLP_Synchronization_InsertSchedule";
                cmd.Parameters.Add(new SqlParameter("@SycDate", SqlDbType.Date) { Value = SycDate });
                if (CalculateEndDate != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CalculateEndDate", SqlDbType.Date) { Value = CalculateEndDate });
                }
                if (Remark != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.Text) { Value = Remark });
                }
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@res", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                res = Convert.ToInt16(cmd.Parameters["@res"].Value);
            });
            //
            return res;
        }
        /// <summary>
        /// 更新同步计划
        /// </summary>
        /// <returns></returns>
        public int UpdateSchedule(int ID, DateTime SycDate, DateTime? CalculateEndDate, string Remark, Guid UserId)
        {
            int res = 0;
            base.ExecuteProcedure((cmd) =>
            {
                cmd.CommandText = "dbo.RLP_Synchronization_UpdateSchedule";
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                cmd.Parameters.Add(new SqlParameter("@SycDate", SqlDbType.Date) { Value = SycDate });
                if (CalculateEndDate != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CalculateEndDate", SqlDbType.Date) { Value = CalculateEndDate });
                }
                if (Remark != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.Text) { Value = Remark });
                }
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@res", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.ExecuteNonQuery();
                //
                res = Convert.ToInt16(cmd.Parameters["@res"].Value);
            });
            //
            return res;
        }
        /// <summary>
        /// 同步时调用的  更新方法
        /// </summary>
        /// <returns></returns>
        public void SycSchedule(int ID, string Status, string SynDetail)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.dbo.RLP_Synchronization_DeleteSchedule");
            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "@Status", DbType.String, Status);
            if (SynDetail != null)
            {
                db.AddInParameter(cmd, "@SynDetail", DbType.String, SynDetail);
            }
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除同步计划
        /// </summary>
        /// <returns></returns>
        public void DeleteSchedule(int ID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.RLP_Synchronization_DeleteSchedule");
            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            db.ExecuteNonQuery(cmd);
        }
    }
}