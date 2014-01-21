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
    /// 
    /// </summary>
    public partial class YearlyCompareReport : BaseList
    {
        //Fields
        private AreaBLL AreaBLL;
        private EntityTypeBLL EntityTypeBLL;
        private UserCompanyBLL UserCompanyBLL;
        private ReportBLL ReportBLL;
        #region ctor

        public YearlyCompareReport()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.AreaBLL = new AreaBLL();
                this.EntityTypeBLL = new EntityTypeBLL();
                this.UserCompanyBLL = new UserCompanyBLL();
                this.ReportBLL = new ReportBLL();
            }
        }
        #endregion

        //Events
        protected void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            //
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
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
            this.EntityTypeBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            this.ReportBLL.Dispose();
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
                //DataTable dtUserArea = dtValue.Copy();
                //DataRow row = dtUserArea.NewRow();
                //row["AreaName"] = string.Empty;
                //row["ID"] = DBNull.Value;
                //dtUserArea.Rows.InsertAt(row, 0);
                //
                DataView dvValue = new DataView(dtValue);
                dvValue.Sort = "ShowOrder ASC";
                ControlHelper.BindComboBox(this.ddlArea, dvValue, "AreaName", "ID");
            }
            // 绑定实体类型
            DataTable dtEntityType = base.GetDataTable("EntityType", () =>
            {
                return this.EntityTypeBLL.SelectEntityType();
            });
            dtEntityType.Rows.Add(string.Empty, string.Empty);
            dtEntityType.DefaultView.Sort = "txt ASC";
            ControlHelper.BindComboBox(this.ddlEntityScope, dtEntityType.DefaultView, "txt", "val");
            //
            this.ddlOnlyContainOpenBefore.SelectedIndex = 0;
        }
        protected override void GetDataSource()
        {
            // 1.检测
            if (this.txtYear.Text == string.Empty)
            {
                base.MessageInformation(base.GetMessage("MustYear"), base.GetMessage("Caption"));
                //
                this.txtYear.Focus();
                return;
            }
            else if (this.txtYear.Text.Length != 4)
            {
                base.MessageInformation(base.GetMessage("YearInvalid"), base.GetMessage("Caption"));
                //
                this.txtYear.Focus();
                return;
            }
            else if (!this.chbFixManagement.Checked && !this.chbFixRent.Checked && !this.chbRadioService.Checked &&
                !this.chbRadioManagement.Checked && !this.chbRadioRent.Checked && !this.chbStraightRent.Checked)
            {
                base.MessageInformation(base.GetMessage("MustSelectRentType"), base.GetMessage("Caption"));
                return;
            }
            // 2.获取条件
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
            string CompanyCode = string.Empty;
            if (this.ddlCompany.SelectedValue + string.Empty != string.Empty)
            {
                CompanyCode = this.ddlCompany.SelectedValue.ToString();
            }
            int Year = int.Parse(this.txtYear.Text);
            string EntityScope = this.ddlEntityScope.SelectedValue + string.Empty;
            bool OnlyContainOpenBefore = this.ddlOnlyContainOpenBefore.SelectedIndex == 0;
            string ContractType = null;
            if (this.radNew.Checked) 
            {
                ContractType = "1";
            }
            else if (this.radChange.Checked)
            {
                ContractType = "2";
            }
            else if (this.radExtend.Checked) 
            {
                ContractType = "3";
            }
            bool FixManagement = this.chbFixManagement.Checked, FixRent = this.chbFixRent.Checked,
                RadioService = this.chbRadioService.Checked, RadioManagement = this.chbRadioManagement.Checked == true,
                RadioRent = this.chbRadioRent.Checked, StraightRent = this.chbStraightRent.Checked;
            // 3.读取数据源
            if (!base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.ReportBLL.SelectYearlyCompareSales(AreaID, CompanyCode, Year, EntityScope,
                        OnlyContainOpenBefore, ContractType,
                       FixManagement, FixRent, RadioService, RadioManagement, RadioRent, StraightRent);
                    if (ds != null && ds.Tables.Count == 1)
                    {
                        base.DTSource = ds.Tables[0];
                    }
                    else
                    {
                        base.DTSource = null;
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.ReportBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, string.Format("查找{0}出错", base.Text), base.Text))
            {
                return;
            }
            //
            base.GetDataSource();
        }
        protected override void BindGridList()
        {
            base.BindGridList();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AreaName", base.GetMessage("AreaName"), 40);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyName", base.GetMessage("CompanyName"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StoreNo", base.GetMessage("StoreNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "KioskNo", base.GetMessage("KioskNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "RentType", base.GetMessage("RentType"), 80);
            //
            int iyear = Convert.ToInt32(this.txtYear.Text) - 1;
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, iyear.ToString(), iyear.ToString() + base.GetMessage("YearMonthRentColumn"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, (++iyear).ToString(), iyear.ToString() + base.GetMessage("YearMonthRentColumn"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, (++iyear).ToString(), iyear.ToString() + base.GetMessage("YearMonthRentColumn"), 150);
            // 用于导出
            base.DecimalColumns = new List<int>();
            for (int i = 5; i < base.dgvList.Columns.Count; i++)
            {
                base.DecimalColumns.Add(i);
            }
        }
    }
}
