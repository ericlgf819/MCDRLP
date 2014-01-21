using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.Report;
using MCD.RLPlanning.BLL.Setting;

namespace MCD.RLPlanning.Client.Report
{
    /// <summary>
    /// 合同到期预警报表
    /// </summary>
    public partial class ContractExpiresEarlyWarningReport : BaseList
    {
        //Fields
        private AreaBLL AreaBLL;
        private ReportBLL ReportBLL;
        private EntityTypeBLL EntityTypeBLL;
        private UserCompanyBLL UserCompanyBLL;
        #region ctor

        public ContractExpiresEarlyWarningReport()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.AreaBLL = new AreaBLL();
                this.ReportBLL = new ReportBLL();
                this.EntityTypeBLL = new EntityTypeBLL();
                this.UserCompanyBLL = new UserCompanyBLL();
            }
        }
        #endregion

        //Events
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value + string.Empty != string.Empty)
            {
                if (e.ColumnIndex == 5) 
                {
                    if (e.Value.ToString() == "True")
                    {
                        e.Value = base.GetMessage("Yes");
                    }
                    else
                    {
                        e.Value = base.GetMessage("No");
                    }
                }
                else if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");
                }
            }
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid? AreaId = null;
            if (this.ddlArea.SelectedValue + string.Empty != string.Empty)
            {
                AreaId = new Guid(this.ddlArea.SelectedValue + string.Empty);
            }
            string company = string.Empty;
            if (this.ddlCompany.SelectedValue + string.Empty != string.Empty)
            {
                company = this.ddlCompany.SelectedValue + string.Empty;
            }
            //
            DataSet dsCompany = this.UserCompanyBLL.SelectUserCompany(new UserCompanyEntity()
            {
                AreaId = AreaId,
                Status = 'A'
            });
            if (dsCompany != null && dsCompany.Tables.Count > 0)
            {
                DataTable dt = dsCompany.Tables[0];
                dt.Rows.InsertAt(dt.NewRow(), 0);
                //
                this.ddlCompany.DataSource = null;
                ControlHelper.BindComboBox(this.ddlCompany, dt, "CompanyName", "CompanyCode");
                if (company != string.Empty)
                {
                    this.ddlCompany.SelectedValue = company;
                }
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AreaBLL.Dispose();
            this.ReportBLL.Dispose();
            this.EntityTypeBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            // 绑定区域 -- 同步表,可缓存
            DataTable dtValue = base.GetDataTable("Area", () =>
            {
                DataSet dsArea = this.AreaBLL.SelectAreas();
                if (dsArea != null && dsArea.Tables.Count == 1)
                {
                    return dsArea.Tables[0];
                }
                else
                {
                    return null;
                }
            });
            if (dtValue != null)
            {
                DataView dvValue = new DataView(dtValue);
                dvValue.Sort = "ShowOrder ASC";
                ControlHelper.BindComboBox(this.ddlArea, dvValue, "AreaName", "ID");
            }
            // 绑定实体类型
            DataTable dtEntityType = base.GetDataTable("EntityType", () => { 
                return this.EntityTypeBLL.SelectEntityType(); 
            });
            dtEntityType.Rows.Add(string.Empty, string.Empty);
            dtEntityType.DefaultView.Sort = "txt ASC";
            ControlHelper.BindComboBox(this.ddlEntityType, dtEntityType.DefaultView, "txt", "val");
            // 绑定到期年份
            DataTable dtMonth = base.PrepareKeyValueTable();
            base.AddNewKeyValueRow(dtMonth, 0, "");
            for (int i = 0; i < 5; i++)
            {
                int year = DateTime.Now.Year + i;
                //
                base.AddNewKeyValueRow(dtMonth, year, year.ToString());
            }
            ControlHelper.BindComboBox(this.ddlExpireYear, dtMonth, "Name", "Value");
        }
        protected override void GetDataSource()
        {
            int recordCount = 0;
            this.ShowPager = true;
            //
            if (this.ddlArea.SelectedValue == null)
            {
                base.MessageInformation(base.GetMessage("MustArea"), base.GetMessage("Caption"));
                //
                this.ddlArea.Focus();
                return;
            }
            else if (this.ddlArea.FindString(this.ddlArea.Text) == -1)
            {
                base.MessageInformation(string.Format(base.GetMessage("InvalidAreaInput"), this.ddlArea.Text), base.GetMessage("Caption"));
                //
                this.ddlArea.Focus();
                return;
            }
            Guid? AreaID = new Guid(this.ddlArea.SelectedValue.ToString());
            string companyCode = null, entityType = null, storeNo = this.txtStoreNo.Text.Trim(), kioskName = this.txtKioskName.Text.Trim();
            if (this.ddlCompany.SelectedValue + string.Empty != string.Empty)
            {
                companyCode = this.ddlCompany.SelectedValue.ToString();
            }
            if (this.ddlEntityType.SelectedValue + string.Empty != string.Empty)
            {
                entityType = this.ddlEntityType.SelectedValue.ToString();
            }
            int expireYear = int.Parse(this.ddlExpireYear.SelectedValue.ToString());
            //
            if (!base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.ReportBLL.SelectContractExpireWarningPagedResult(AreaID, companyCode, entityType, storeNo, kioskName,
                        expireYear, base.CurrentPageIndex, base.PageSize, out recordCount);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        base.DTSource = ds.Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.ReportBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, string.Format("查找{0}出错", base.Text), base.Text)) {
                return;
            };
            //
            base.GetDataSource();
            this.RecordCount = recordCount;
        }
        protected override void BindGridList()
        {
            base.BindGridList();
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ContractNO", base.GetMessage("ContractNO"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyCode", base.GetMessage("CompanyCode"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EntityTypeName", base.GetMessage("EntityTypeName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreOrDeptNo", base.GetMessage("StoreOrDeptNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EntityName", base.GetMessage("EntityName"), 250);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsRelatedKiosk", base.GetMessage("IsRelatedKiosk"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "VendorNo", base.GetMessage("VendorNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "VendorName", base.GetMessage("VendorName"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RentStartDate", base.GetMessage("RentStartDate"), 100); 
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RentEndDate", base.GetMessage("RentEndDate"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AvailableDays", base.GetMessage("AvailableDays"), 150);
        }
    }
}