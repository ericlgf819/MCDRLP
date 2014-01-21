using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.UUP.CommonUI
{
    public partial class PopedomSetting : BaseFrm
    {
        private UserService.UserServiceClient userService = new MCD.UUP.CommonUI.UserService.UserServiceClient();
        private GroupService.GroupServiceClient groupService = new MCD.UUP.CommonUI.GroupService.GroupServiceClient();
        private ModuleService.ModuleServiceClient moduleService = new MCD.UUP.CommonUI.ModuleService.ModuleServiceClient();

        /// <summary>
        /// 应用系统访问时，调用该接口
        /// </summary>
        /// <param name="systemCode"></param>
        public PopedomSetting(string systemCode)
        {
            this.SystemCode = systemCode;
            InitializeComponent();
        }

        private bool showUser = true;
        /// <summary>
        /// 是否显示用户信息
        /// </summary>
        public bool ShowUser
        {
            get { return this.showUser; }
            set { this.showUser = value; }
        }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string SystemCode = string.Empty;

        /// <summary>
        /// 系统用户
        /// </summary>
        private DataTable tableSystemUser = null;
        /// <summary>
        /// 系统用户组
        /// </summary>
        private DataTable tableSystemGroup = null;
        /// <summary>
        /// 系统权限模块
        /// </summary>
        private DataTable tableModule = null;
        /// <summary>
        /// 系统功能
        /// </summary>
        private DataTable tableFunction = null;
        /// <summary>
        /// 用户或用户组所有的功能信息
        /// </summary>
        private DataTable tableUserFunction = null;

        /// <summary>
        /// 显示用户和用户组
        /// </summary>
        private void BindTreeView()
        {
            // 没有选定任何系统时，返回
            if (SystemCode == string.Empty) return;

            if (!ExecuteAction(() =>
                {
                    // 获取选定系统所有用户
                    tableSystemUser = userService.SelectUsersBySystemCode(SystemCode, string.Empty);
                    // 获取选定系统所有用户组
                    tableSystemGroup = groupService.SelectGroupsBySystemCode(SystemCode, string.Empty);
                    // 获取选定系统所有模块信息
                    tableModule = moduleService.SelectModulesBySystemCode(SystemCode, string.Empty);
                    // 获取系统所有的功能信息
                    tableFunction = moduleService.GetFunctionBySystemCode(SystemCode);
                }, "生成权限菜单出错"))
            {
                return;
            }

            // 清空树菜单所有节点
            this.tvSystemUser.Nodes.Clear();

            // 构建用户根节点
            if (ShowUser)
            {
                TreeNode userRoot = new TreeNode() { Text = "用户", Name = "tvUserRoot" };
                this.tvSystemUser.Nodes.Add(userRoot);
                foreach (DataRow row in tableSystemUser.Rows)
                {
                    userRoot.Nodes.Add(new TreeNode() { Text = row["DisplayName"] + string.Empty, Tag = row["ID"] + string.Empty });
                }
            }

            // 构建用户组根节点
            TreeNode groupRoot = new TreeNode() { Text = "用户组", Name = "tvGroupRoot" };
            this.tvSystemUser.Nodes.Add(groupRoot);
            foreach (DataRow row in tableSystemGroup.Rows)
            {
                groupRoot.Nodes.Add(new TreeNode() { Text = row["GroupName"] + string.Empty, Tag = row["ID"] + string.Empty });
            }
            // 展开所有节点
            this.tvSystemUser.ExpandAll();

            // 绑定模块
            this.tvModules.Nodes.Clear();
            this.tvModules.CheckBoxes = true;
            TreeNode moduleRoot = new TreeNode() { Text = "权限模块", Name = "tvGroupRoot" };
            this.tvModules.Nodes.Add(moduleRoot);

            TreeNode node;
            DataRow[] functionRows;
            foreach (DataRow row in tableModule.Rows)
            {
                node = new TreeNode() { Text = row["ModuleName"] + string.Empty, Tag = row["ID"] + string.Empty };
                functionRows = tableFunction.Select("ModuleID='" + row["ID"].ToString() + "'");
                for (int i = 0, j = functionRows.Length; i < j; i++)
                {
                    node.Nodes.Add(new TreeNode() { Text = functionRows[i]["FunctionName"].ToString(), Tag = functionRows[i]["ID"] });
                }
                moduleRoot.Nodes.Add(node);
            }
            // 展开所有节点
            this.tvModules.ExpandAll();
            this.tvModules.AfterCheck += new TreeViewEventHandler(tvModules_AfterCheck);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopedomSetting_Load(object sender, EventArgs e)
        {
            BindTreeView();
        }

        #region 设置权限菜单节点选中状态 ---------
        /// <summary>
        /// 设置子节点选中状态
        /// </summary>
        /// <param name="parent"></param>
        private void SetChildNodeCheck(TreeNode parent)
        {
            this.tvModules.AfterCheck -= new TreeViewEventHandler(tvModules_AfterCheck);
            foreach (TreeNode node in parent.Nodes)
            {
                node.Checked = parent.Checked;
                if (node.Nodes.Count != 0)
                {
                    SetChildNodeCheck(node);
                }
            }
        }

        /// <summary>
        /// 设置父节点选中状态
        /// </summary>
        /// <param name="node"></param>
        private void SetParentNodeCheck(TreeNode node)
        {
            if (node.Parent == null) return;

            bool? check = null;
            foreach (TreeNode n in node.Parent.Nodes)
            {
                if (!check.HasValue)
                {
                    check = n.Checked;
                }
                else
                {
                    // 父节点有2个子节点选择状态不一致
                    if (n.Checked != check.Value)
                    {
                        check = null;
                        break;
                    }
                }
            }
            this.tvModules.AfterCheck -= new TreeViewEventHandler(tvModules_AfterCheck);
            if (check.HasValue)
            { // 有值时，表示父节点下所有子节点都选择一样
                node.Parent.Checked = check.Value;
            }
            else
            {
                node.Parent.Checked = false;
            }

            // 继续检查父节点的父节点
            SetParentNodeCheck(node.Parent);
        }

        /// <summary>
        /// 选定树节点后，判断是否需要选定子节点和父节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModules_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.tvModules.AfterCheck -= new TreeViewEventHandler(tvModules_AfterCheck);

            // 如果当前节点有子节点
            if (e.Node.Nodes.Count != 0)
            {
                SetChildNodeCheck(e.Node);
            }

            SetParentNodeCheck(e.Node);
            this.tvModules.AfterCheck += new TreeViewEventHandler(tvModules_AfterCheck);
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 当选择的是根节点或者没有选中节点时，不做保存动作
            if (this.tvSystemUser.SelectedNode == null ||
                this.tvSystemUser.SelectedNode.Nodes.Count != 0
                || this.tvModules.Nodes.Count == 0)
            {
                return;
            }

            DataTable dtUserFunction = tableUserFunction.Clone();
            DataRow row;
            // 遍历权限模块树，检查用户或用户组是否拥有该权限
            foreach (TreeNode nodeModule in tvModules.Nodes[0].Nodes)
            {
                foreach (TreeNode nodeFunction in nodeModule.Nodes)
                {
                    if (nodeFunction.Checked)
                    {
                        row = dtUserFunction.NewRow();
                        row["ID"] = Guid.NewGuid();
                        row["UserOrGroupID"] = this.tvSystemUser.SelectedNode.Tag;
                        row["FunctionID"] = nodeFunction.Tag;
                        dtUserFunction.Rows.Add(row);
                    }
                }
            }

            if (moduleService.UpdateUserOrGroupFunction(new Guid(this.tvSystemUser.SelectedNode.Tag.ToString()), dtUserFunction))
            {
                MessageBox.Show("保存成功!");
            }
            else
            {
                MessageBox.Show("保存失败!");
            }

        }

        /// <summary>
        /// 选定用户或用户组后，设置该用户/用户组的权限值为选定状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSystemUser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null || this.tvModules.Nodes.Count == 0)
            {
                this.lblSelectedInfo.Text = string.Empty;
                return;
            }

            if (!ExecuteAction(() =>
                {
                    tableUserFunction = moduleService.GetFunctionByUserOrGroupID(new Guid(e.Node.Tag.ToString()));
                }, "获取用户/用户组权限出错"))
            {
                return;
            }

            // 遍历权限模块树，检查用户或用户组是否拥有该权限
            foreach (TreeNode nodeModule in tvModules.Nodes[0].Nodes)
            {
                foreach (TreeNode nodeFunction in nodeModule.Nodes)
                {
                    if (tableUserFunction.Select("UserOrGroupID='" + e.Node.Tag + "' and FunctionID='" + nodeFunction.Tag + "'").Length > 0)
                    {
                        nodeFunction.Checked = true;
                    }
                    else
                    {
                        nodeFunction.Checked = false;
                    }
                }
            }

            this.lblSelectedInfo.Text = "当前已选择" + this.tvSystemUser.SelectedNode.Parent.Text + "：" + this.tvSystemUser.SelectedNode.Text;
        }

    }
}