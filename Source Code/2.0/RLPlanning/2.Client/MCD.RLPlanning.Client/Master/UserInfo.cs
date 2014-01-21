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

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserInfo : BaseList
    {
        //Fields
        private UserBLL UserBLL;
        private DeptBLL DeptBLL;
        private bool FirstLoad = true;
        #region ctor

        public UserInfo()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.UserBLL = new UserBLL();
                this.DeptBLL = new DeptBLL();
            }
        }
        #endregion

        //Events
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().Replace(@"\r\n", Environment.NewLine).Trim();
                //
                if (e.ColumnIndex == 6 && e.Value.ToString() != string.Empty)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseFrm.DATETIME_LONG_FORMAT);
                }
            }
        }
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && base.dgvList.CurrentCell != null)
            {
                Guid userID = new Guid(base.dgvList.CurrentRow.Cells["ID"].Value.ToString());
                //
                UserEdit frm = new UserEdit(userID) { ParentFrm = this };
                #region 根据来源判断是否禁用窗体

                string FromSRLS = base.dgvList.CurrentRow.Cells["FromSRLS"].Value.ToString();
                if (FromSRLS == "True")
                {
                    foreach (Control ctr in frm.Controls)
                    {
                        ctr.Enabled = false;
                    }
                }
                else
                {
                    foreach (Control ctr in frm.Controls)
                    {
                        ctr.Enabled = true;
                    }
                }
                #endregion
                base.RefreshList = false;
                frm.ShowDialog();
                //
                if (base.RefreshList)
                {
                    this.GetDataSource();
                    base.SetGridSelectedRow("ID", userID.ToString());
                }
            }
        }
        protected override void btnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                base.RefreshList = false;
                UserAdd frm = new UserAdd() { ParentFrm = this };
                frm.ShowDialog();
                //
                if (base.RefreshList)
                {
                    this.GetDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell == null)
            {
                base.MessageError(base.GetMessage("SelectUser"), base.GetMessage("Caption"));
                return;
            }
            //
            bool FromSRLS = Boolean.Parse(base.dgvList.CurrentRow.Cells["FromSRLS"].Value + string.Empty);
            if (FromSRLS)
            {
                base.MessageInformation(base.GetMessage("FromSRLS"), base.GetMessage("Caption"));
                return;
            }
            //
            string userName = base.dgvList.CurrentRow.Cells["UserName"].Value + string.Empty;
            if (userName.ToLower().Equals("administrator"))
            {
                base.MessageError("该管理员为系统预留账号，无法删除！", base.GetMessage("Caption"));
                return;
            }
            //
            if (base.MessageConfirm(string.Format(base.GetMessage("ConfirmDelete"), userName)) == DialogResult.OK)
            {
                Guid id = new Guid(base.dgvList.CurrentRow.Cells["ID"].Value.ToString());
                //
                this.UserBLL.DeleteSingleUser(id, 0, AppCode.SysEnvironment.CurrentUser.ID);
                this.GetDataSource();
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UserBLL.Dispose();
            this.DeptBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            base.BindFormControl();
            //
            base.btnAddnew.Visible = true;
            base.btnDelete.Visible = true;
            // 绑定部门
            //DataTable dtValue = base.GetDataTable("Department", () => {
            //    return this.DeptBLL.SelectActiveDept().Tables[0];
            //});
            //DataTable dt = dtValue.Copy();
            //DataRow row = dt.NewRow();//默认行
            //row["DeptName"] = string.Empty;
            //row["DeptCode"] = string.Empty;
            //dt.Rows.InsertAt(row, 0);
            ////
            //ControlHelper.BindComboBox(this.ddlDept, dt, "DeptName", "DeptCode");
            // 绑定所属用户组
            DataTable dtValue = base.GetDataTable("UserGroup", () =>
            {
                return base.GroupService.SelectGroupsBySystemCode(AppCode.SysEnvironment.SystemCode, string.Empty);
            });
            DataTable dt = dtValue.Copy();
            DataRow row = dt.NewRow();
            row["GroupName"] = string.Empty;
            row["ID"] = Guid.Empty;
            dt.Rows.InsertAt(row, 0);
            //
            ControlHelper.BindComboBox(this.ddlGroup, dt, "GroupName", "ID");
            // 绑定状态
            dtValue = new DataTable();
            dtValue.Columns.Add("Value", typeof(int));
            dtValue.Columns.Add("Name", typeof(string));
            row = dtValue.NewRow();
            row["Name"] = string.Empty;
            row["Value"] = 10;
            dtValue.Rows.Add(row);//
            row = dtValue.NewRow();
            row["Name"] = "禁用";
            row["Value"] = 0;
            dtValue.Rows.Add(row);//
            row = dtValue.NewRow();
            row["Name"] = "启用";
            row["Value"] = 1;
            dtValue.Rows.Add(row);//
            row = dtValue.NewRow();
            row["Name"] = "锁定";
            row["Value"] = 2;
            dtValue.Rows.Add(row);
            //
            ControlHelper.BindComboBox(this.ddlStatus, dtValue, "Name", "Value");
        }
        protected override void GetDataSource()
        {
            UserEntity user = new UserEntity();
            user.UserName = this.txtUserName.Text.Trim();
            user.EnglishName = this.txtEnglishName.Text.Trim();
            //user.DeptCode = this.ddlDept.SelectedValue + string.Empty;
            // 状态
            byte byteValue = 0;
            if (byte.TryParse(this.ddlStatus.SelectedValue + string.Empty, out byteValue))
            {
                user.Status = byteValue;
            }
            else
            {
                user.Status = 10;
            }
            // 用户组
            if (this.ddlGroup.SelectedValue != null)
            {
                user.GroupID = new Guid(this.ddlGroup.SelectedValue + string.Empty);
            }
            else
            {
                user.GroupID = Guid.Empty;
            }
            // 来源
            user.FromSRLS = null;
            //
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    base.DTSource = this.UserBLL.SelectAllUsers(user).Tables[0];
                }, base.GetMessage("Wait"), () =>
                {
                    this.UserBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "查找所有用户信息出错", "用户信息");
            base.GetDataSource();
        }
        protected override void BindGridList()
        {
            base.BindGridList();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", string.Empty, 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UserName", base.GetMessage("UserName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "EnglishName", base.GetMessage("EnglishName"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "DeptName", base.GetMessage("DeptName"), 151);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "GroupName", base.GetMessage("GroupName"), 90);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 60);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 118);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "FromSRLS", base.GetMessage("FromSRLS"), 0);
            base.dgvList.Columns[0].Visible = false;
            base.dgvList.Columns["FromSRLS"].Visible = false;
            //初次导入选中第一行
            if (this.FirstLoad && base.dgvList.Rows.Count > 0)
            {
                this.FirstLoad = false;
                //
                base.dgvList.Rows[0].Selected = true;
                base.dgvList.CurrentCell = base.dgvList.Rows[0].Cells[1];
            }
        }
    }
}