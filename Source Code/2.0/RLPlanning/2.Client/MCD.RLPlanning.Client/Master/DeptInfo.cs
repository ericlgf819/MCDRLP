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

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DeptInfo : BasePhase
    {
        //Fields
        private AreaBLL AreaBLL;
        private DeptBLL DeptBLL;
        private UserCompanyBLL UserCompanyBLL;
        #region ctor

        public DeptInfo()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.AreaBLL = new AreaBLL();
                this.DeptBLL = new DeptBLL();
                this.UserCompanyBLL = new UserCompanyBLL();
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
            this.DeptBLL.Dispose();
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
                DataTable dtUserArea = dtValue.Copy();
                DataRow row = dtUserArea.NewRow();
                row["AreaName"] = string.Empty;
                row["ID"] = DBNull.Value;
                dtUserArea.Rows.InsertAt(row, 0);
                //
                DataView dvValue = new DataView(dtUserArea);
                dvValue.Sort = "ShowOrder ASC";
                ControlHelper.BindComboBox(this.ddlArea, dvValue, "AreaName", "ID");
            }
            // 绑定控件 状态
            ControlHelper.BindComboBox(this.ddlStatus, base.DTStatus, "StatusName", "StatusValue");
        }
        protected override void BindGridList()
        {
            DataTable dt = null;
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    Guid? AreaID = null;
                    if (this.ddlArea.SelectedValue + string.Empty != string.Empty)
                    {
                        AreaID = new Guid(this.ddlArea.SelectedValue.ToString());
                    }
                    string CompanyCode = string.Empty;
                    if (this.ddlCompany.SelectedValue + string.Empty != string.Empty)
                    {
                        CompanyCode = this.ddlCompany.SelectedValue.ToString();
                    }
                    //
                    DataSet dsDept = this.DeptBLL.SelectAllDepartments(new DeptEntity(){
                        AreaId = AreaID,
                        CompanyCode = CompanyCode,
                        DeptName = this.txtDeptCodeOrName.Text.Trim(),
                        Status = this.ddlStatus.SelectedValue + string.Empty
                    });
                    if (dsDept !=null && dsDept.Tables.Count >0)
                    {
                        dt = dsDept.Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.DeptBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, string.Format("查找{0}出错", base.Text), base.Text);
            if (dt == null) return;
            base.dgvList.DataSource = dt;
            //
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "CompanyCode", base.GetMessage("CompanyCode"), 120);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "DeptCode", base.GetMessage("DeptCode"), 120);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "DeptName", base.GetMessage("DeptName"), 242);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            base.BindGridList();
        }
    }
}