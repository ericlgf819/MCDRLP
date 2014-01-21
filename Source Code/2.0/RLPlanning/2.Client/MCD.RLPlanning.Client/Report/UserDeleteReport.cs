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
using MCD.RLPlanning.Client.UUPGroupService;

namespace MCD.RLPlanning.Client.Report
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserDeleteReport : BaseList
    {
        //Fields
        private DeptBLL DeptBLL;
        private UserBLL UserBLL;
        #region ctor

        public UserDeleteReport()
        {
            InitializeComponent();
            if (!base.DesignMode)
            {
                this.DeptBLL = new DeptBLL();
                this.UserBLL = new UserBLL();
            }
        }
        #endregion

        //Events
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.ColumnIndex == 5 && e.Value.ToString() != string.Empty)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseFrm.DATETIME_LONG_FORMAT);
                }
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DeptBLL.Dispose();
            this.UserBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            // 绑定部门 -- 同步表, 可缓存
            DataTable dtValue = base.GetDataTable("Department", () =>
            {
                DataSet ds = this.DeptBLL.SelectActiveDept();
                if (ds != null && ds.Tables.Count>0)
                {
                    return ds.Tables[0];
                }
                return null;
            });
            DataRow row = dtValue.NewRow();
            row["DeptName"] = string.Empty;
            row["DeptCode"] = string.Empty;
            dtValue.Rows.InsertAt(row, 0);
            ControlHelper.BindComboBox(this.cbbDept, dtValue, "DeptName", "DeptCode");
            // 绑定用户组
            dtValue = base.GroupService.SelectGroupsBySystemCode(AppCode.SysEnvironment.SystemCode, string.Empty);
            row = dtValue.NewRow();
            row["GroupName"] = string.Empty;
            row["ID"] = Guid.Empty;
            dtValue.Rows.InsertAt(row, 0);
            ControlHelper.BindComboBox(this.cbbGroup, dtValue, "GroupName", "ID");
        }
        protected override void GetDataSource()
        {
            UserEntity entity = new UserEntity();
            entity.UserName = this.txtUserName.Text.ToRowFilterString();
            entity.EnglishName = this.txtEnglishName.Text.ToRowFilterString();
            entity.DeptCode = this.cbbDept.SelectedValue + string.Empty;
            if (this.cbbGroup.SelectedValue != null)
            {
                entity.GroupID = new Guid(this.cbbGroup.SelectedValue + string.Empty);
            }
            else
            {
                entity.GroupID = Guid.Empty;
            }
            //
            if (!base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.UserBLL.SelectDeletedUsers(entity);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        base.DTSource = ds.Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.UserBLL.CloseService();
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
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", string.Empty, 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UserName", base.GetMessage("UserName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EnglishName", base.GetMessage("EnglishName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "DeptName", base.GetMessage("DeptName"), 250);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "GroupName", base.GetMessage("GroupName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "DeleteDate", base.GetMessage("DeleteDate"), 120);
            base.dgvList.Columns[0].Visible = false;
        }
    }
}