using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.RLPlanning.Client;

namespace MCD.RLPlanning.Client.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TaskInfo : BaseFrm
    {
        public TaskInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 情况右侧控件
        /// </summary>
        /// <param name="panel"></param>
        protected void ClearControls(Panel panel)
        {
            foreach (Control control in this.splitContainer1.Panel2.Controls)
            {
                control.Dispose();
            }
            panel.Controls.Clear();
        }

        #region treeview相关
        /// <summary>
        /// 设置选中节点
        /// </summary>
        /// <param name="treeNode"></param>
        protected void SetSelectedNode(TreeNode treeNode)
        {
            TreeNode selectedNode = null;
            if (treeNode != null)
            {
                TreeNode[] nodes = this.tvTaskTime.Nodes.Find(treeNode.Name, true);
                if (nodes != null && nodes.Length > 0)
                {
                    selectedNode = nodes[0];
                }
            }
            if (selectedNode != null)
            {
                foreach (TreeNode node in this.tvTaskTime.Nodes)
                {
                    this.RecoveryNodeColor(node);
                }
                selectedNode.BackColor = Color.FromArgb(51, 153, 255);
                selectedNode.ForeColor = Color.White;
                //
                this.tvTaskTime.SelectedNode = selectedNode;
            }
        }

        /// <summary>
        /// 恢复结点颜色
        /// </summary>
        /// <param name="treeNode"></param>
        protected void RecoveryNodeColor(TreeNode treeNode)
        {
            treeNode.ForeColor = Color.Black;
            treeNode.BackColor = this.tvTaskTime.BackColor;
            foreach (TreeNode child in treeNode.Nodes)
            {
                this.RecoveryNodeColor(child);
            }
        }
        #endregion

        #region 控件事件
        protected virtual void tvTaskTime_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        protected virtual void TaskInfo_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
