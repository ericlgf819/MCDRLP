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
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CompanyInfo : BasePhase
    {
        //Fields
        private CompanyBLL CompanyBLL = new CompanyBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        #region ctor

        public CompanyInfo()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        /// <summary>
        /// 雙擊公司信息，打開編輯窗體
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && base.dgvList.CurrentCell != null)
            {
                bool FromSRLS = Boolean.Parse(base.dgvList.SelectedRows[0].Cells["FromSRLS"].Value.ToString());
                if (FromSRLS)
                {
                    //base.MessageInformation(base.GetMessage("FromSRLS"));
                    return;
                }
                //
                string companycode = base.dgvList.CurrentRow.Cells["CompanyCode"].Value.ToString();
                CompanyEdit frm = new CompanyEdit(companycode) { 
                    ParentFrm = this 
                };
                base.RefreshList = false;
                frm.ShowDialog();
                //
                if (base.RefreshList)
                {
                    this.BindGridList();
                    //
                    base.SetGridSelectedRow("CompanyCode", companycode);
                }
            }
        }
        /// <summary>
        /// 新增Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnNew_Click(object sender, EventArgs e)
        {
            CompanyAdd frm = new CompanyAdd() { 
                ParentFrm = this 
            };
            frm.ShowDialog(this);
            //
            this.BindGridList();
        }
        /// <summary>
        /// 删除typecode
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
                    base.MessageInformation(base.GetMessage("FromSRLS"), base.GetMessage("Caption"));
                    return;
                }
                //
                if (MessageBox.Show(this, base.GetMessage("ConfirmDelete"), base.GetMessage("Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int res = this.CompanyBLL.DeleteSingleCompany(new CompanyEntity(){ 
                        CompanyCode = base.dgvList.SelectedRows[0].Cells["CompanyCode"].Value.ToString() 
                    });
                    //
                    if (res == -1)
                    {
                        base.MessageInformation(base.GetMessage("ExistStore"), base.GetMessage("Caption"));
                    }
                    else if (res == 0)
                    {
                        base.MessageInformation(base.GetMessage("deleteError"), base.GetMessage("Caption"));
                    }
                    else
                    {
                        base.MessageInformation(base.GetMessage("deleteOk"), base.GetMessage("Caption"));
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
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.CompanyBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            this.btnNew.Visible = true;
            this.btnDelete.Visible = true;
            // 绑定区域
            DataSet dsUserArea = this.UserCompanyBLL.SelectUserArea(new UserCompanyEntity() {
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
        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            CompanyEntity entity = new CompanyEntity();
            if (this.ddlArea.SelectedValue != null && this.ddlArea.SelectedValue.ToString() != string.Empty)
            {
                entity.AreaID = new Guid(this.ddlArea.SelectedValue.ToString());
            }
            if (this.txtCompanyNoOrName.Text.Trim() != string.Empty)
            {
                entity.CompanyCode = this.txtCompanyNoOrName.Text.Trim();
            }
            if (this.ddlStatus.SelectedValue != null && this.ddlStatus.SelectedValue.ToString() != string.Empty)
            {
                entity.Status = this.ddlStatus.SelectedValue.ToString();
            }
            if (this.ddlFromSRLS.SelectedValue != null && this.ddlFromSRLS.SelectedValue.ToString() != string.Empty)
            {
                entity.FromSRLS = int.Parse(this.ddlFromSRLS.SelectedValue.ToString()) == 1;
            }
            entity.UserID = AppCode.SysEnvironment.CurrentUser.ID.ToString();
            //
            DataTable dt = null;
            base.ExecuteAction(() => {
                FrmWait frmwait = new FrmWait(() => {
                    DataSet dsCompany = this.CompanyBLL.SelectAllComapny(entity);
                    if (dsCompany != null && dsCompany.Tables.Count > 0)
                    {
                        dt = dsCompany.Tables[0];
                    }
                    else
                    {
                        dt = null;
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.CompanyBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取公司信息数据错误", "公司信息");
            if (dt == null) return;
            //
            base.dgvList.DataSource = dt;
            base.BindGridList();
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AreaName", base.GetMessage("AreaName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyCode", base.GetMessage("CompanyCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyName", base.GetMessage("CompanyName"), 260);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "SimpleName", base.GetMessage("SimpleName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 60);
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ResponsibleEnglishName", base.GetMessage("ResponsibleEnglishName"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FixedSourceCode", base.GetMessage("FixedSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FixedManageSourceCode", base.GetMessage("FixedManageSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StraightLineSourceCode", base.GetMessage("StraightLineSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RatioSourceCode", base.GetMessage("RatioSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RatioManageSourceCode", base.GetMessage("RatioManageSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RatioServiceSourceCode", base.GetMessage("RatioServiceSourceCode"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FromSRLS", base.GetMessage("FromSRLS"), 0);
            base.dgvList.Columns["FromSRLS"].Visible = false;
        }
    }
}