using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.RLPlanning.Client.Workflow.Task.Controls;

namespace MCD.RLPlanning.Client.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PendingTaskInfo : TaskInfo
    {
        public PendingTaskInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 未完成任务的Panel
        /// </summary>
        private TUnFinishedTask list = null;

        #region 控件事件
        protected override void tvTaskTime_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //设置当前点击的节点为选中节点
            SetSelectedNode(e.Node);


            switch (e.Node.Name)
            {
                case "Day":
                    {
                        if (null != list)
                        {
                            list.iTimeZoneFlag = 0;
                            list.BindGridView();
                        }
                        break;
                    }
                case "Month":
                    {
                        if (null != list)
                        {
                            list.iTimeZoneFlag = 1;
                            list.BindGridView();
                        }
                        break;
                    }
                case "Year":
                    {
                        if (null != list)
                        {
                            list.iTimeZoneFlag = 2;
                            list.BindGridView();
                        }
                        break;
                    }
                case "All":
                    {
                        if (null != list)
                        {
                            list.iTimeZoneFlag = 3;
                            list.BindGridView();
                        }
                        break;
                    }
                    
                default: break;
            }
        }

        protected override void TaskInfo_Load(object sender, EventArgs e)
        {
            //展开所有结点
            tvTaskTime.ExpandAll();

            //设置“全部”节点为默认选中节点
            TreeNode[] nodes = this.tvTaskTime.Nodes.Find("All", true);
            if (nodes != null && nodes.Length > 0)
            {
                this.SetSelectedNode(nodes[0]);
            }

            //将其他“当天”，“当月”，“当年”给删除
            tvTaskTime.Nodes[0].Nodes.RemoveAt(0);
            tvTaskTime.Nodes[0].Nodes.RemoveAt(0);
            tvTaskTime.Nodes[0].Nodes.RemoveAt(0);

            //加载"待处理"节点对应的待办列表
            list = new TUnFinishedTask();
            list.iTimeZoneFlag = 3; //待处理任务时间维度只有“全部”
            list.Dock = DockStyle.Fill;
            list.ShowRecaculateButton = false;
            this.ClearControls(this.splitContainer1.Panel2);
            this.splitContainer1.Panel2.Controls.Add(list);
            //
            list.LoadData();
        }
        #endregion
    }
}
