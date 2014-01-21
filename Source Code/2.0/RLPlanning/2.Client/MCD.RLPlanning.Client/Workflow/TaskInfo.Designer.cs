namespace MCD.RLPlanning.Client.Workflow
{
    partial class TaskInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("当天");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("当月");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("今年");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("全部");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("待处理任务", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTaskTime = new System.Windows.Forms.TreeView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTaskTime);
            this.splitContainer1.Size = new System.Drawing.Size(709, 415);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvTaskTime
            // 
            this.tvTaskTime.BackColor = System.Drawing.SystemColors.Control;
            this.tvTaskTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTaskTime.Location = new System.Drawing.Point(0, 0);
            this.tvTaskTime.Name = "tvTaskTime";
            treeNode1.Name = "Day";
            treeNode1.Text = "当天";
            treeNode2.Name = "Month";
            treeNode2.Text = "当月";
            treeNode3.Name = "Year";
            treeNode3.Text = "今年";
            treeNode4.Name = "All";
            treeNode4.Text = "全部";
            treeNode5.Name = "RootNode";
            treeNode5.Text = "待处理任务";
            this.tvTaskTime.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.tvTaskTime.Size = new System.Drawing.Size(169, 415);
            this.tvTaskTime.TabIndex = 0;
            this.tvTaskTime.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTaskTime_NodeMouseClick);
            // 
            // TaskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 415);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TaskInfo";
            this.Text = "TaskInfo";
            this.Load += new System.EventHandler(this.TaskInfo_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TreeView tvTaskTime;
        protected System.Windows.Forms.SplitContainer splitContainer1;
    }
}