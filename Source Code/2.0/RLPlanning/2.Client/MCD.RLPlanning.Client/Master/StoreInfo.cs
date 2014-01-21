using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.ContractMg;
using MCD.RLPlanning.Client.ForcastSales;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StoreInfo : BasePhase
    {
        //Fields
        private StoreBLL StoreBLL = new StoreBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        #region ctor

        public StoreInfo()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnNew_Click(object sender, EventArgs e)
        {
            StoreAdd form = new StoreAdd() { 
                ParentFrm = this 
            };
            base.RefreshList = false;
            form.Show();
            //
            if (base.RefreshList)
            {
                this.BindGridList();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell == null)
            {
                return;
            }
            //
            if (base.dgvList.Rows.Count > 0 && base.dgvList.SelectedRows != null && base.dgvList.SelectedRows.Count > 0)
            {
                bool FromSRLS = (bool)(base.dgvList.SelectedRows[0].Cells["FromSRLS"].Value);
                if (FromSRLS)
                {
                    this.MessageInformation(base.GetMessage("FromSRLS"), base.GetMessage("Caption"));
                    return;
                }
                //
                if (MessageBox.Show(this, base.GetMessage("ConfirmDelete"), base.GetMessage("Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (base.dgvList.Rows.Count > 0)
                    {
                        StoreEntity entity = new StoreEntity();
                        entity.StoreNo = base.dgvList.SelectedRows[0].Cells["StoreNO"].Value.ToString();
                        //
                        int res = this.StoreBLL.DeletedSingleStore(entity);
                        string message = base.GetMessage("deleteOk");
                        //if (res == 1)
                        //{
                        //    message = base.GetMessage("ExistSalesData");
                        //}
                        //else 
                        if (res == 2)
                        {
                            message = base.GetMessage("ExistKioskData");
                        }
                        else 
                            if (res == 3)
                        {
                            message = base.GetMessage("ExistContractData");
                        }
                        base.MessageInformation(message, base.GetMessage("Caption"));
                        //
                        this.BindGridList();
                    }
                }
            }
            else
            {
                base.MessageError(base.GetMessage("PleaseSelectItem"), base.GetMessage("Caption"));
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnCopy_Click(object sender, EventArgs e)
        {
            if (null == base.dgvList.CurrentRow)
            {
                return;
            }
            //
            string StoreNO = base.dgvList.CurrentRow.Cells["StoreNO"].Value.ToString();
            StoreAdd frm = new StoreAdd(StoreNO) {
                ParentFrm = this 
            };
            base.RefreshList = false;
            frm.Show();
            //
            if (base.RefreshList)
            {
                this.BindGridList();
            }
        }
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnMultiCopy_Click(object sender, EventArgs e)
        {
            if (null == base.dgvList.CurrentRow)
            {
                return;
            }
            //
            string storeno = base.dgvList.CurrentRow.Cells["StoreNO"].Value.ToString();
            StoreMultiCopy frm = new StoreMultiCopy(storeno) {
                ParentFrm = this 
            };
            base.RefreshList = false;
            frm.ShowDialog();
            //
            if (base.RefreshList)
            {
                this.BindGridList();
            }
        }
        /// <summary>
        /// 创建新合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnCreateContract_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell != null)
            {
                string companyCode = base.dgvList.CurrentRow.Cells["CompanyCode"].Value + string.Empty;
                string storeNo = base.dgvList.CurrentRow.Cells["StoreNo"].Value + string.Empty;
                string strStatus = base.dgvList.CurrentRow.Cells["Status"].Value + string.Empty;

                if ("A" != strStatus)
                {
                    MessageError(GetMessage("InactiveStore"));
                    return;
                }

                ContractEdit frm = new ContractEdit()
                {
                    CompanyCodeFromOutSide = companyCode,
                    StoreNoFromOutSide = storeNo,
                    ParentFrm = this,
                    CurrentAction = ActionType.New,
                    CopyType = ContractCopyType.新建,
                    //IsAddNew = true,
                    IsNewWorkflow = true,
                    WorkflowBizStatus = WorkflowBizStatus.草稿,
                };
                frm.ShowDialog();
            }
        }
        /// <summary>
        /// 导入预测Sales 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnKeyInSales_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell != null)
            {
                //string strStatus = dgvList.Rows[dgvList.CurrentCell.RowIndex].Cells["Status"].Value.ToString();
                //if ("I" == strStatus)
                //{
                //    MessageError(GetMessage("InvalidStore"));
                //    return;
                //}

                string storeNo = base.dgvList.CurrentRow.Cells["StoreNo"].Value + string.Empty;
                KeyInBox frm = new KeyInBox(storeNo, null);
                frm.ShowDialog();
            }
        }
        /// <summary>
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && base.dgvList.CurrentCell != null)
            {
                string storeNo = base.dgvList.CurrentRow.Cells["StoreNo"].Value + string.Empty;
                //
                StoreEdit frm = new StoreEdit(storeNo) { 
                    ParentFrm = this
                };
                string FromSRLS = base.dgvList.CurrentRow.Cells["FromSRLS"].Value.ToString();
                if (FromSRLS == "True")
                {
                    foreach (Control ctrl in frm.Controls)
                    {
                        if (ctrl.Name == "lblX3")
                        {
                            ctrl.Visible = false;
                        }
                        else if (ctrl.Name != "btnCancel")
                        {
                            ctrl.Enabled = false;
                        }
                    }
                }
                base.RefreshList = false;
                frm.Show();
                //
                if (base.RefreshList)
                {
                    this.BindGridList();
                    base.SetGridSelectedRow("StoreNo", storeNo);
                }
            }
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StoreBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }


        /// <summary>
        /// 绑定控件数据
        /// </summary>
        protected override void BindFormControl()
        {
            // 绑定区域
            DataSet dsUserArea = this.UserCompanyBLL.SelectUserArea(new UserCompanyEntity()
            {
                UserId = AppCode.SysEnvironment.CurrentUser.ID
            });
            if (dsUserArea != null && dsUserArea.Tables.Count == 1)
            {
                DataTable dtUserArea = dsUserArea.Tables[0];
                DataRow row = dtUserArea.NewRow();
                row["AreaName"] = string.Empty;
                row["AreaID"] = DBNull.Value;
                dtUserArea.Rows.InsertAt(row, 0);
                //
                ControlHelper.BindComboBox(this.ddlArea, dtUserArea, "AreaName", "AreaID");
            }
            // 绑定状态
            ControlHelper.BindComboBox(this.ddlStatus, base.DTStatus, "StatusName", "StatusValue");
            // 绑定来源
            ControlHelper.BindComboBox(this.ddlFromSRLS, base.DTDataFrom, "SourceName", "SourceValue");
            this.ddlFromSRLS.SelectedIndex = 1;
            //
            base.btnNew.Visible = true;
            base.btnDelete.Visible = true;
            this.btnCopy.Visible = true;
            this.btnMultiCopy.Visible = true;
            base.btnCreateContract.Visible = true;
            base.btnKeyInSales.Visible = true;
        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            base.ShowPager = true;
            //
            Guid? areaID = null;
            if (this.ddlArea.SelectedValue != null && this.ddlArea.SelectedValue.ToString() != string.Empty)
            {
                areaID = new Guid(this.ddlArea.SelectedValue.ToString());
            }
            string companyNo = this.txtCompanyNo.Text.Trim();
            string storeNo = this.txtStoreNo.Text.Trim();
            string status = null;
            if (this.ddlStatus.SelectedValue != null && this.ddlStatus.SelectedValue.ToString() != string.Empty)
            {
                status = this.ddlStatus.SelectedValue.ToString();
            }
            bool? fromSRLS = null;
            if (this.ddlFromSRLS.SelectedValue != null && this.ddlFromSRLS.SelectedValue.ToString() != string.Empty)
            {
                fromSRLS = int.Parse(this.ddlFromSRLS.SelectedValue.ToString()) == 1;
            }
            //
            DataTable dt = null;
            int recordCount = 0;
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    dt = this.StoreBLL.USDX(areaID, companyNo, storeNo, status, fromSRLS, AppCode.SysEnvironment.CurrentUser.ID, 
                        base.CurrentPageIndex, base.PageSize, out recordCount).Tables[0];
                }, base.GetMessage("Wait"), () =>
                {
                    this.StoreBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取餐厅数据错误", "餐厅信息");
            if (dt == null) return;
            base.dgvList.DataSource = dt;
            base.RecordCount = recordCount;
            //
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyCode", base.GetMessage("CompanyCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanySimpleName", base.GetMessage("CompanySimpleName"), 110);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreNo", base.GetMessage("StoreNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreName", base.GetMessage("StoreName"), 230);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "SimpleName", base.GetMessage("SimpleName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EmailAddress", base.GetMessage("EmailAddress"), 130);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "OpenDate", base.GetMessage("OpenDate"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CloseDate", base.GetMessage("CloseDate"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FromSRLS", base.GetMessage("FromSRLS"), 0);
            base.dgvList.Columns["FromSRLS"].Visible = false;
            //
            base.BindGridList();
        }
    }
}