using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common.SRLS;
using MCD.Framework.SqlDAL;
using MCD.RLPlanning.Entity.PlanningSnapshot;
using MCD.RLPlanning.IServices.PlanningSnapshot;

namespace MCD.RLPlanning.Services.PlanningSnapshot
{
    /// <summary>
    /// 
    /// </summary>
    public class CloseAccountService : BaseDAL<CloseAccountEntity>, ICloseAccountService
    {
        #region ICloseAccount

        public byte[] SelectClosePlanning()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DataSet ds = db.ExecuteDataSet("dbo.[RLP_PlanningSnapshot_SelectClosePlanning]");
            return base.Serilize(ds);
        }
        public byte[] CheckClosePlanning(int ID, Guid? ClosedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.[RLP_PlanningSnapshot_CheckClosePlanning]");
            cmd.CommandTimeout = int.MaxValue;
            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            if (ClosedBy.HasValue)
            {
                db.AddInParameter(cmd, "@ClosedBy", DbType.Guid, ClosedBy.Value);
            }
            DataSet ds = db.ExecuteDataSet(cmd);
            //
            return base.Serilize(ds);
        }

        #endregion
    }
}