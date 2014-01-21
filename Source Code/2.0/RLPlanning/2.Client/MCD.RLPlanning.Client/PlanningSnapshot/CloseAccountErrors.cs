using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.PlanningSnapshot;

namespace MCD.RLPlanning.Client.PlanningSnapshot
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CloseAccountErrors : BaseList
    {
        private DataTable tblErrors;
        #region ctor

        public CloseAccountErrors(DataSet ds)
        {
            InitializeComponent();
            //
            this.tblErrors = ds.Tables[0];
        }
        #endregion

        //Methods
        protected override void BindFormControl()
        {
            base.btnSelect.Visible = false;
            base.btnReset.Visible = false;
            //
            base.GetDataSource();
        }
        protected override void BindGridList()
        {
            base.DTSource = this.tblErrors;
            //
            base.BindGridList();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AreaName", base.GetMessage("AreaName"), 40);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyName", base.GetMessage("CompanyName"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreNo", base.GetMessage("StoreNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "StoreName", base.GetMessage("StoreName"), 250);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskNo", base.GetMessage("KioskNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "KioskName", base.GetMessage("KioskName"), 150);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Reason", base.GetMessage("Reason"), 200);
        }
    }
}