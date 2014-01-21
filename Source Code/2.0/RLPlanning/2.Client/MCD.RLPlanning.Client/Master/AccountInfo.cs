using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountInfo : BasePhase
    {
        //Fields
        AccountBLL AccountBLL = new AccountBLL();
        #region ctor

        public AccountInfo()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AccountBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        /// <summary>
        /// 绑定界面控件
        /// </summary>
        protected override void BindFormControl()
        {
            // 绑定控件 状态
            ControlHelper.BindComboBox(this.ddlStatus, base.DTStatus, "StatusName", "StatusValue");
        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            string accountNo = this.txtAccountNo.Text.Trim();
            string status = null;
            if (this.ddlStatus.SelectedIndex != 0)
            {
                status = this.ddlStatus.SelectedValue.ToString();
            }
            //
            DataTable dt = null;
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet dsAccount = this.AccountBLL.SelectAllAccount(accountNo, status);
                    if (dsAccount != null && dsAccount.Tables.Count > 0)
                    {
                        dt =dsAccount .Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.AccountBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取科目信息数据错误", "科目信息");
            if (dt == null) return;
            base.dgvList.DataSource = dt;
            //
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AccountNo", base.GetMessage("AccountNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AC_Desc", base.GetMessage("AC_Desc"), 260);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Open_Item_AC", base.GetMessage("Open_Item_AC"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CreatorName", base.GetMessage("CreatorName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CreateTime", base.GetMessage("CreateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            base.BindGridList();
        }
    }
}