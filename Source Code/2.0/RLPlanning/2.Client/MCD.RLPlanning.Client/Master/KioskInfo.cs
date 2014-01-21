using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Controls;
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
    public partial class KioskInfo : BasePhase
    {
        //Fields
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        /// <summary>
        /// 
        /// </summary>
        public DataSet dsStores = null;
        #region ctor

        /// <summary>
        /// 
        /// </summary>
        public KioskInfo()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 点击新增按钮新增。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnNew_Click(object sender, EventArgs e)
        {
            KioskAdd frm = new KioskAdd()
            {
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
        /// 删除选定的项。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.SelectedRows == null || base.dgvList.SelectedRows.Count <= 0)
            {
                return;
            }
            //
            KioskEntity entity = base.dgvList.SelectedRows[0].DataBoundItem as KioskEntity;
            if (entity == null)
            {
                return;
            }
            else if (entity.FromSRLS != null && entity.FromSRLS.Value)
            {
                this.MessageInformation(base.GetMessage("FromSRLS"), base.GetMessage("Caption"));
                return;
            }
            //
            if (MessageBox.Show(this, base.GetMessage("DelKioskConfirmMsg"), base.GetMessage("Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            // 检查是否有关联的合同，若有则不允许删除
            else if (KioskBLL.Instance.ExistsRelatedContract(entity.KioskNo))
            {
                base.MessageError(base.GetMessage("ExistsRelatContract"));
                return;
            }
            //
            if (KioskBLL.Instance.Delete(entity.KioskID))
            {
                this.BindGridList();
            }
        }
        /// <summary>
        /// 
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
            KioskEntity entity = base.dgvList.CurrentRow.DataBoundItem as KioskEntity;
            KioskAdd frm = new KioskAdd("copy", entity){
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
        /// 
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
            string KioskID = base.dgvList.CurrentRow.Cells["KioskID"].Value.ToString();
            KioskMultiCopy frm = new KioskMultiCopy(KioskID)
            {
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
        /// 执行新建合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnCreateContract_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell != null)
            {
                KioskEntity entity = base.dgvList.CurrentRow.DataBoundItem as KioskEntity;
                // 甜品店不单独结算sales，则不能添加合同
                if (entity.IsNeedSubtractSalse.HasValue && !entity.IsNeedSubtractSalse.Value)
                {
                    base.MessageError(base.GetMessage("CreateContractErrorWithSales"));
                    return;
                }
                //为了获取对应的CompanyCode
                StoreBLL storeBLL = new StoreBLL();
                StoreEntity storeEntity = storeBLL.SelectSingleStore(entity.StoreNo);
                storeBLL.Dispose();
                if (null == storeEntity)
                {
                    base.MessageError(GetMessage("CreateContractErrorWithCompanyCode"));
                    return;
                }
                //
                ContractEdit frm = new ContractEdit()
                {
                    CompanyCodeFromOutSide = storeEntity.CompanyCode,
                    StoreNoFromOutSide = entity.StoreNo,
                    KioskNoFromOutSide = entity.KioskNo,
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
                //如果选中的Kiosk不需要独立计算sales，则不能录入sales IsNeedSubtractSalse
                int iRow = dgvList.CurrentCell.RowIndex;
                string strIsNeedSubtractSales = dgvList.Rows[iRow].Cells["IsNeedSubtractSalse"].Value.ToString();

                if ("False" == strIsNeedSubtractSales)
                {
                    MessageError(GetMessage("SalesInputErr"));
                    return;
                }

                //无效甜品店不能导入sales
                //string strStatus = dgvList.Rows[iRow].Cells["Status"].Value.ToString();
                //if ("I" == strStatus)
                //{
                //    MessageError(GetMessage("InvalidKiosk"));
                //    return;
                //}

                KioskEntity entity = base.dgvList.CurrentRow.DataBoundItem as KioskEntity;
                //
                KeyInBox frm = new KeyInBox(entity.StoreNo, entity.KioskNo);
                frm.ShowDialog();
            }
        }
        /// <summary>
        /// 双单元格时弹出编辑窗体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && base.dgvList.CurrentCell != null)
            {
                KioskEntity entity = base.dgvList.Rows[e.RowIndex].DataBoundItem as KioskEntity;
                KioskAdd frm = new KioskAdd("edit", entity)
                {
                    ParentFrm = this
                };
                //
                string FromSRLS = base.dgvList.CurrentRow.Cells["FromSRLS"].Value.ToString();
                if (FromSRLS == "True")
                {
                    foreach (Control ctrl in frm.Controls)
                    {
                        if (ctrl.Name == "groupKiosInfo")
                        {
                            foreach (Control child in ctrl.Controls)
                            {
                                if (!(child.Name == "ddlStoreNo" || child.Name == "ddlIsNeedSubtractSalse" || child.Name == "dtpActiveDate" || child.Name == "txtDescription"))
                                {
                                    child.Enabled = false;
                                }
                            }
                        }
                        else if (!(ctrl.Name == "btnSave" || ctrl.Name == "btnCancel"))
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
                    base.SetGridSelectedRow("KioskNo", entity.KioskNo);
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
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
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
        /// 初始化列后获取数据源并绑定到DataGridView。
        /// </summary>
        protected override void BindGridList()
        {
            //显示分页
            base.ShowPager = true;
            //
            Guid? areaID = null;
            if (this.ddlArea.SelectedValue != null && this.ddlArea.SelectedValue.ToString() != string.Empty)
            {
                areaID = new Guid(this.ddlArea.SelectedValue.ToString());
            }
            string companyNo = this.txtCompanyNo.Text.Trim();
            string storeNo = this.txtStoreNo.Text.Trim();
            string kioskNo = this.txtKioskNo.Text.Trim();
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
            List<KioskEntity> list = null;
            int recordCount = 0;
            FrmWait frmwait = new FrmWait(() =>
            {
                list = KioskBLL.Instance.Where(areaID, companyNo, storeNo, kioskNo, status, fromSRLS, AppCode.SysEnvironment.CurrentUser.ID, 
                    this.CurrentPageIndex, base.PageSize, out recordCount);
            }, base.GetMessage("Wait"), () =>
            {
                KioskBLL.Instance.CloseService();
            });
            frmwait.ShowDialog();
            //
            this.RecordCount = recordCount;
            base.dgvList.DataSource = list;
            base.dgvList.Columns.Clear();
            //添加数据列
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskID", base.GetMessage("KioskID"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskNo", base.GetMessage("KioskNo"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskName", base.GetMessage("KioskName"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Address", base.GetMessage("Address"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskType", base.GetMessage("KioskType"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreNo", base.GetMessage("StoreNo"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsNeedSubtractSalse", base.GetMessage("IsNeedSubtractSalse"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"));
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Description", base.GetMessage("Description"),300);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ActiveDate", base.GetMessage("ActiveDate"), 150, 
                col => { col.DefaultCellStyle.Format = "yyyy-MM-dd"; });
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FromSRLS", base.GetMessage("FromSRLS"), 0);
            base.dgvList.Columns["FromSRLS"].Visible = false;
            base.dgvList.Columns["KioskID"].Visible = false;
            //
            base.BindGridList();
            // 默认选中第一行
            if (list.Count > 0) 
            {
                base.SetGridSelectedRow("KioskID", list[0].KioskID.ToString());
            }
        }
    }
}