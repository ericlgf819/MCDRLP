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
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client.UUPGroupService;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GroupList : BaseList
    {
        //Fields
        private UserBLL UserBLL = new UserBLL();
        protected string SystemCode = string.Empty;
        #region ctor

        public GroupList()
        {
            this.SystemCode = SysEnvironment.SystemCode;
            //
            InitializeComponent();
        }
        #endregion

        //Events
        #region 按钮事件

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnAddnew_Click(object sender, EventArgs e)
        {
            GroupEdit frm = new GroupEdit(this.SystemCode);
            frm.ShowDialog();
            //
            base.btnAddnew_Click(sender, e);
            this.GetDataSource();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell == null) return;
            //
            object id = base.dgvList.CurrentRow.Cells["ID"].Value;
            string groupName = base.dgvList.CurrentRow.Cells["GroupName"].Value + string.Empty;
            if (base.MessageConfirm(string.Format(base.GetMessage("ConfirmDelete"), groupName)) == DialogResult.OK)
            {
                if (this.UserBLL.SelectGroupUsers(new Guid(id.ToString())).Tables[0].Rows.Count > 0)
                {
                    base.MessageError(base.GetMessage("ExistsUsers"));
                    return;
                }
                //
                base.GroupService.DeleteGroups(new GroupEntity() { ID = new Guid(id.ToString()) });
                //
                base.btnDelete_Click(sender, e);
                this.GetDataSource();
            }
        }
        #endregion
        /// <summary>
        /// 双击进行编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.dgvList.CurrentCell == null) return;
            //
            object id = base.dgvList.CurrentRow.Cells["ID"].Value;
            DataRow row = base.DTSource.Rows.Find(id);
            GroupEntity entity = new GroupEntity() {
                ID = new Guid(row["ID"].ToString()),
                GroupName = row["GroupName"] + string.Empty,
                Remark = row["Remark"] + string.Empty,
                SystemCode = this.SystemCode
            };
            GroupEdit frm = new GroupEdit(entity);
            frm.ShowDialog();
            //
            base.dgvList_CellDoubleClick(sender, e);
            this.GetDataSource();
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UserBLL.Dispose();
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
        }
        /// <summary>
        /// 绑定 DataGridView
        /// </summary>
        protected override void BindGridList()
        {
            base.BindGridList();
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", string.Empty, 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "GroupName", base.GetMessage("GroupName"), 136);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "SystemName", base.GetMessage("SystemName"), 196);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Remark", base.GetMessage("Remark"), 466);
            base.dgvList.Columns[0].Visible = false;
            //
            if (base.dgvList.Rows.Count > 0)
            {
                base.dgvList.Rows[0].Selected = true;
            }
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            string groupName = this.txtGroupName.Text.Trim();
            if (!base.ExecuteAction(() =>
            {
                base.DTSource = base.GroupService.SelectGroupsBySystemCode(this.SystemCode, groupName);
            }, "查询模块信息错误", "用户组"))
            {
                return;
            }
            // 设置主键
            base.DTSource.PrimaryKey = new DataColumn[] { base.DTSource.Columns["ID"] };
            base.GetDataSource();
        }
    }
}