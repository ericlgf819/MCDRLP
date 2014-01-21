using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GroupPermission : BaseFrm
    {
        //Fields
        /// <summary>
        /// 系统编码
        /// </summary>
        public string SystemCode = string.Empty;
        /// <summary>
        /// 系统用户组
        /// </summary>
        private DataTable tableSystemGroup = null;
        /// <summary>
        /// 模块功能表
        /// </summary>
        private DataTable dtModuleFunction = null;
        /// <summary>
        /// 选中节点的Index 
        /// </summary>
        private int selectIndex = 0;
        #region ctor

        public GroupPermission()
        {
            this.SystemCode = AppCode.SysEnvironment.SystemCode;
            //
            InitializeComponent();
        }
        #endregion

        //Events
        private void GroupPermission_Load(object sender, EventArgs e)
        {
            this.dgvList.AutoGenerateColumns = false;
            //
            this.BindGridList();
            this.GetDataSource();
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvList);
        }
        private void tvSystemUser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 将所有的节点颜色清空
            foreach (TreeNode node in this.tvSystemUser.Nodes[0].Nodes)
            {
                if (node.Checked == false)
                {
                    node.BackColor = Color.White;
                    node.ForeColor = Color.Black;
                }
            }
            if (e.Node.Tag == null) //选中根节点
            {
                this.dgvList.DataSource = null;
                this.lblSelectedInfo.Text = string.Empty;
                return;
            }
            //
            if (!base.ExecuteAction(() => {
                this.dtModuleFunction = base.ModuleService.SelectModuleFunctionBySystemCodeAndGroup(this.SystemCode, string.Empty,
                    this.tvSystemUser.SelectedNode.Tag.ToString());
            }, "获取用户/用户组权限出错", "权限设置"))
            {
                return;
            }
            this.lblSelectedInfo.Text = string.Format(base.GetMessage("SelectedNode"), this.tvSystemUser.SelectedNode.Parent.Text,
                this.tvSystemUser.SelectedNode.Text);
            this.dtModuleFunction.Columns.Add("GroupName");
            foreach (DataRow dr in this.dtModuleFunction.Rows)
            {
                dr["GroupName"] = this.tvSystemUser.SelectedNode.Text;
            }
            //DataView dv = this.dtModuleFunction.DefaultView;
            this.dgvList.DataSource = this.dtModuleFunction;

            // 没有对应的权限则不显示CheckBox
            foreach (DataGridViewRow dr in this.dgvList.Rows)
            {
                object desc = dr.Cells["ViewPopedomDetail"].Value;
                if (desc == DBNull.Value || desc == null || string.IsNullOrEmpty(desc.ToString()))
                {
                    (dr.Cells["ViewPopedom"] as DataGridViewEnabledCheckBoxCell).Enabled = false;
                }
                //
                desc = dr.Cells["OperatePopedomDetail"].Value;
                if (desc == DBNull.Value || desc == null || string.IsNullOrEmpty(desc.ToString()))
                {
                    (dr.Cells["OperatePopedom"] as DataGridViewEnabledCheckBoxCell).Enabled = false;
                }
            }
            this.dgvList.Refresh();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tvSystemUser.SelectedNode == null || this.tvSystemUser.SelectedNode.Level == 0)
            {
                return;
            }
            //
            if (base.ModuleService.UpdateUserOrGroupFunctionWithGroup(new Guid(this.tvSystemUser.SelectedNode.Tag.ToString()), 
                this.dtModuleFunction))
            {
                MessageBox.Show(base.GetMessage("SaveSuccess"));
            }
            else
            {
                MessageBox.Show(base.GetMessage("SaveFailure"));
            }
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            GridViewHelper.Print_DataGridView(this.dgvList);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lblSelectedInfo.Text))
            {
                return;
            }
            //
            this.BindGridExportList();
            GridViewHelper.DataGridViewToExcel(this.tvSystemUser.SelectedNode.Text + this.Text, 
                this.tvSystemUser.SelectedNode.Text + this.Text, this.dgvExport);
        }

        //Methods
        /// <summary>
        /// 向DataGridView添加列的定义
        /// </summary>
        private void BindGridList()
        {
            //列信息：模块、查看权限、操作权限
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ModuleName", base.GetMessage("ModuleName"), 200);
            //
            GridViewHelper.AppendColumnToDataGridView<DataGridViewEnabledCheckBoxColumn>(this.dgvList, "ViewPopedom", string.Empty, 70);
            SetGridAllCheckBox set = new SetGridAllCheckBox(this.dgvList, 1);
            set.SetSelectAllCheckBox();
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ViewPopedomDetail", base.GetMessage("ViewPopedomDetail"), 300);
            //
            GridViewHelper.AppendColumnToDataGridView<DataGridViewEnabledCheckBoxColumn>(this.dgvList, "OperatePopedom", string.Empty, 70);
            SetGridAllCheckBox set2 = new SetGridAllCheckBox(this.dgvList, 3);
            set2.SetSelectAllCheckBox();
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "OperatePopedomDetail", base.GetMessage("OperatePopedomDetail"), 300);
        }
        private void GetDataSource()
        {
            if (this.SystemCode == string.Empty) return;
            // 获取选定系统所有用户组
            if (!base.ExecuteAction(() => {
                this.tableSystemGroup = base.GroupService.SelectGroupsBySystemCode(this.SystemCode, string.Empty);
            }, "生成权限菜单出错", "权限设置"))
            {
                return;
            }
            //
            // 清空树菜单所有节点
            this.tvSystemUser.Nodes.Clear();
            // 构建用户组根节点
            TreeNode groupRoot = new TreeNode() { Text = base.GetMessage("Group"), Name = "tvGroupRoot" };
            this.tvSystemUser.Nodes.Add(groupRoot);
            foreach (DataRow row in tableSystemGroup.Rows)
            {
                groupRoot.Nodes.Add(new TreeNode() { Text = row["GroupName"] + string.Empty, Tag = row["ID"] + string.Empty });
            }
            this.tvSystemUser.ExpandAll();
        }
        /// <summary>
        /// 向DataGridView添加列的定义
        /// </summary>
        private void BindGridExportList()
        {
            this.dgvExport.Columns.Clear();
            this.dgvExport.AutoGenerateColumns = false;
            this.dgvExport.AllowUserToAddRows = false;
            this.dgvExport.ReadOnly = true;
            this.dgvExport.AllowUserToOrderColumns = true;
            this.dgvExport.MultiSelect = false;
            this.dgvExport.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            this.dgvExport.DefaultCellStyle.SelectionForeColor = Color.Black;
            //
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "GroupName", base.GetMessage("Group"), 200);
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "ModuleName", base.GetMessage("ModuleName"), 200);
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "ViewPopedom", base.GetMessage("ViewPopedom"), 70);
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "ViewPopedomDetail", base.GetMessage("ViewPopedomDetail"), 300);
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "OperatePopedom", base.GetMessage("OperatePopedom"), 70);
            GridViewHelper.AppendColumnToDataGridView(this.dgvExport, "OperatePopedomDetail", base.GetMessage("OperatePopedomDetail"), 300);
            //
            DataTable tbl = (DataTable)this.dgvList.DataSource;
            foreach (DataRow dr in tbl.Rows)
            {
                if (Convert.ToString(dr["ViewPopedomDetail"]) == "")
                {
                    dr["ViewPopedom"] = DBNull.Value;
                }
                if (Convert.ToString(dr["OperatePopedomDetail"]) == "")
                {
                    dr["OperatePopedom"] = DBNull.Value;
                }
            }
            this.dgvExport.DataSource = tbl;
        }

        /// <summary>
        /// 查找树节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        private void SelectTreeNode(TreeView tv, string text, ref int time)
        {
            foreach (TreeNode node in tv.Nodes)
            {
                this.SelectTreeNode(tv, node, text, ref time);
            }
        }
        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        private void SelectTreeNode(TreeView tv, TreeNode parent, string text, ref int time)
        {
            int index = 0;
            bool selected = false;
            // 将所有的节点颜色清空
            foreach (TreeNode node in this.tvSystemUser.Nodes[0].Nodes)
            {
                node.BackColor = Color.White;
                node.ForeColor = Color.Black;
            }
            // 检测当前节点下的所有子节点
            foreach (TreeNode node in this.tvSystemUser.Nodes[0].Nodes)
            {
                if (node.Text.IndexOf(text) > -1)
                {
                    if (index == time)
                    {
                        tvSystemUser.SelectedNode = node;
                        node.BackColor = SystemColors.Highlight;
                        node.ForeColor = Color.White;
                        time++;
                        selected = true;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            if (selected == false && selectIndex > 0)
            { // 将 selectIndex 置为 0 重新查找一次
                selectIndex = 0;
                foreach (TreeNode node in this.tvSystemUser.Nodes[0].Nodes)
                {
                    if (node.Text.IndexOf(text) > -1)
                    {
                        tvSystemUser.SelectedNode = node;
                        node.BackColor = SystemColors.Highlight;
                        node.ForeColor = Color.White;
                        time++;
                        break;
                    }
                }
            }
        }
    }
}