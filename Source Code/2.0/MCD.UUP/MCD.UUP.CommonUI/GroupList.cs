using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.UUP.CommonUI.GroupService;

namespace MCD.UUP.CommonUI
{
    public partial class GroupList : BaseFrm
    {
        public GroupList(string systemCode)
        {
            SystemCode = systemCode;
            InitializeComponent();
        }

        private GroupServiceClient groupService = new GroupServiceClient();

        public DataTable TableGroup = null;

        protected string SystemCode = string.Empty;

        #region 按钮事件 ---------------
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddnew_Click(object sender, EventArgs e)
        {
            GroupEdit frm = new GroupEdit(SystemCode);
            frm.ShowDialog();

            RefreshForm();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEidt_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null) return;
            object id = this.dgvList.CurrentRow.Cells["ID"].Value;
            DataRow row = TableGroup.Rows.Find(id);
            GroupEntity entity = new GroupEntity()
            {
                ID = new Guid(row["ID"].ToString()),
                GroupName = row["GroupName"] + string.Empty,
                Remark = row["Remark"] + string.Empty,
                SystemCode = this.SystemCode
            };

            GroupEdit frm = new GroupEdit(entity);
            frm.ShowDialog();

            RefreshForm();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认删除模块信息？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (this.dgvList.CurrentCell == null) return;
            object id = this.dgvList.CurrentRow.Cells["ID"].Value;

            groupService.DeleteGroups(new GroupEntity() { ID = new Guid(id.ToString()) });
            RefreshForm();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// 双击编辑用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEidt_Click(sender, e);
        }

        /// <summary>
        /// 获取绑定列表的数据源
        /// </summary>
        /// <returns></returns>
        protected bool GetListSource()
        {
            string groupName;
            groupName = this.txtGroupName.Text.Trim();

            if (!ExecuteAction(() =>
            {
                TableGroup = groupService.SelectGroupsBySystemCode(SystemCode, groupName);
            }, "查询模块信息错误"))
            {
                return false;
            }

            // 设置主键
            TableGroup.PrimaryKey = new DataColumn[] { TableGroup.Columns["ID"] };

            return true;
        }

        /// <summary>
        /// 绑定列表控件
        /// </summary>
        protected void BindGridList()
        {
            //this.dgvList.Columns.Clear();
            this.dgvList.AutoGenerateColumns = false;
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.ReadOnly = true;
            this.dgvList.DataSource = TableGroup;

            dgvList.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dgvList.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        /// <summary>
        /// 刷新显示数据
        /// </summary>
        protected virtual void RefreshForm()
        {
            // 获取列表数据源
            if (GetListSource())
            {
                // 显示列表
                BindGridList();
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupList_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        /// <summary>
        /// 选中行改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvList.CurrentCell == null) return;

            // 设置整个行为选中状态
            dgvList.Rows[dgvList.CurrentCell.RowIndex].Selected = true;
        }
    }
}