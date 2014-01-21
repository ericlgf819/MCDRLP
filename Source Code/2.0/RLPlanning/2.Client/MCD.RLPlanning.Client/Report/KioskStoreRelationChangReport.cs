using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.Report;

namespace MCD.RLPlanning.Client.Report
{
    /// <summary>
    /// 
    /// </summary>
    public partial class KioskStoreRelationChangReport : BaseList
    {
        //Fields
        private AreaBLL AreaBLL;
        private UserCompanyBLL UserCompanyBLL;
        private ReportBLL ReportBLL;
        #region ctor

        public KioskStoreRelationChangReport()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.AreaBLL = new AreaBLL();
                this.UserCompanyBLL = new UserCompanyBLL();
                this.ReportBLL = new ReportBLL();
            }
        }
        #endregion

        //Events
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
                DataView dvValue = new DataView(dtValue);
                dvValue.Sort = "ShowOrder ASC";
                ControlHelper.BindComboBox(this.ddlArea, dvValue, "AreaName", "ID");
            }
        }
        protected override void GetDataSource()
        {
            // 1.检测
            if (((TimeSpan)(this.dtpStartMonth.Value - this.dtpEndMonth.Value)).Days > 0)
            {
                base.MessageInformation(base.GetMessage("StartMonthLaterThanEndMonth"), base.GetMessage("Caption"));
                //
                this.dtpStartMonth.Focus();
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
            else if (this.ddlArea.SelectedValue == null)
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
            string StoreNo = this.txtStoreNo.Text.Trim(), KioskNo = this.txtKioskNo.Text.Trim();
            DateTime StartMonth = new DateTime(this.dtpStartMonth.Value.Year, this.dtpStartMonth.Value.Month, 1), 
            EndMonth = (new DateTime(this.dtpEndMonth.Value.Year, this.dtpEndMonth.Value.Month, 1)).AddMonths(1);
            // 3.读取数据源
            if (!base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.ReportBLL.SelectKioskStoreRelationChangReport(AreaID, CompanyCode, StoreNo, KioskNo,
                        StartMonth, EndMonth);
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
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsNeedSubtractSalse", base.GetMessage("IsNeedSubtractSalse"), 180);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "StartDate", base.GetMessage("StartDate"), 80,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_SHORT_FORMAT; });
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EndDate", base.GetMessage("EndDate"), 80,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_SHORT_FORMAT; });
            GridViewHelper.PaintRowIndexToHeaderCell(base.dgvList);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CreateUserEnglishName", base.GetMessage("CreateUserEnglishName"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CreateTime", base.GetMessage("CreateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
        }
    }
}