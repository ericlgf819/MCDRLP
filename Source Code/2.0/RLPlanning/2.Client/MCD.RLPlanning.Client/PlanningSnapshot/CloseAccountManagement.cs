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
    public partial class CloseAccountManagement : BaseList
    {
        //Fields
        private CloseAccountBLL CloseAccountBLL = new CloseAccountBLL();
        #region ctor

        public CloseAccountManagement()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        /// <summary>
        /// 格式化是否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 2 || e.ColumnIndex == 3) && e.Value != DBNull.Value && e.Value != null)
            {
                if (e.Value.ToString() == "1" || e.Value.ToString().ToLower() == "true")
                {
                    e.Value = "是 ";
                    //
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.SelectionForeColor = Color.Black;
                }
                else if (e.Value.ToString() == "0" || e.Value.ToString().ToLower() == "false")
                {
                    e.Value = "否 ";
                    //
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                }
            }
            //
            base.dgvList_CellFormatting(sender, e);
        }
        protected override void btnCheckCloseAccount_Click(object sender, EventArgs e)
        {
            if (base.dgvList.DataSource != null)
            {
                //FrmWait frmwait = new FrmWait(() =>
                //{
                    this.CheckCloseAccount(null);
                //}, null);
                //frmwait.ShowDialog();
                //
                this.GetDataSource();
            }
        }
        protected override void btnCloseAccount_Click(object sender, EventArgs e)
        {
            if (base.dgvList.DataSource != null)
            {
                string IsDetected = base.dgvList.Rows[0].Cells["IsDetected"].Value + string.Empty;
                if (IsDetected == "否" || IsDetected.ToLower() == "false")
                {
                    base.MessageInformation(base.GetMessage("MustDetected"), base.GetMessage("Caption"));
                    return;
                }
                //
                //FrmWait frmwait = new FrmWait(() =>
                //{
                    this.CheckCloseAccount(AppCode.SysEnvironment.CurrentUser.ID);
                //}, null);
                //frmwait.ShowDialog();
                //
                this.GetDataSource();
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.CloseAccountBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            base.btnReset.Visible = false;
            base.btnCheckCloseAccount.Visible = true;
            base.btnCloseAccount.Visible = true;
        }
        protected override void BindGridList()
        {
            base.BindGridList();
            //
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", "ID", 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CloseYear", base.GetMessage("CloseYear"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsDetected", base.GetMessage("IsDetected"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsColsed", base.GetMessage("IsColsed"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ClosedBy", base.GetMessage("ClosedBy"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ClosedDate", base.GetMessage("ClosedDate"), 150);
            base.dgvList.Columns[0].Visible = false;
        }
        protected override void GetDataSource()
        {
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.CloseAccountBLL.SelectClosePlanning();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        base.DTSource = ds.Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.CloseAccountBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "查找关帐信息出错", "关帐管理");
            base.RecordCount = base.DTSource.Rows.Count;
            //
            base.GetDataSource();
        }
        private void CheckCloseAccount(Guid? ClosedBy)
        {
            if (base.dgvList.SelectedRows[0].Index == 0)
            {
                int ID = (int)(base.dgvList.Rows[0].Cells["ID"].Value);
                //
                DataSet ds = this.CloseAccountBLL.CheckClosePlanning(ID, ClosedBy);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                {
                    // 检测不通过
                    CloseAccountErrors frm = new CloseAccountErrors(ds);
                    frm.ShowDialog();
                }
                else
                {
                    if (ClosedBy.HasValue)
                    {
                        base.MessageInformation(base.GetMessage("ClosedOK"), base.GetMessage("Caption"));
                    }
                    else
                    {
                        base.MessageInformation(base.GetMessage("DetectedOK"), base.GetMessage("Caption"));
                    }
                }
            }
        }
    }
}