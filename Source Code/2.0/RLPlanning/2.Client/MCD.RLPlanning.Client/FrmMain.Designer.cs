namespace MCD.RLPlanning.Client
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.topMenu = new MCD.Controls.UCMenuStrip();
            this.dpnlMainForm = new MCD.DockContainer.Docking.DockPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblSystemInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPendingTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.FormHandler = null;
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.MainFormDockPanel = this.dpnlMainForm;
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(809, 24);
            this.topMenu.TabIndex = 0;
            this.topMenu.Text = "ucMenuStrip1";
            // 
            // dpnlMainForm
            // 
            this.dpnlMainForm.ActiveAutoHideContent = null;
            this.dpnlMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpnlMainForm.Location = new System.Drawing.Point(0, 49);
            this.dpnlMainForm.Name = "dpnlMainForm";
            this.dpnlMainForm.Size = new System.Drawing.Size(809, 418);
            this.dpnlMainForm.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSystemInfo,
            this.lblCurrentUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 467);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(809, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblSystemInfo
            // 
            this.lblSystemInfo.Name = "lblSystemInfo";
            this.lblSystemInfo.Size = new System.Drawing.Size(91, 17);
            this.lblSystemInfo.Text = "当前系统用户：";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(67, 17);
            this.lblCurrentUser.Tag = "false";
            this.lblCurrentUser.Text = "当前用户名";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPendingTask,
            this.toolStripSeparator4,
            this.btnCancel,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(809, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPendingTask
            // 
            this.btnPendingTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPendingTask.Image = ((System.Drawing.Image)(resources.GetObject("btnPendingTask.Image")));
            this.btnPendingTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPendingTask.Name = "btnPendingTask";
            this.btnPendingTask.Size = new System.Drawing.Size(71, 22);
            this.btnPendingTask.Tag = "MCD.RLPlanning.Client.Task.PendingTask";
            this.btnPendingTask.Text = "待处理任务";
            this.btnPendingTask.Click += new System.EventHandler(this.btnPendingTask_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::MCD.RLPlanning.Client.Properties.Resources.关闭;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(51, 22);
            this.btnCancel.Text = "注销";
            this.btnCancel.ToolTipText = "注销用户";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 22);
            this.btnExit.Text = "退出";
            this.btnExit.ToolTipText = "退出系统";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 489);
            this.Controls.Add(this.dpnlMainForm);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.topMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.topMenu;
            this.Name = "FrmMain";
            this.Text = "租金预测系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCMenuStrip topMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPendingTask;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel lblSystemInfo;
        protected MCD.DockContainer.Docking.DockPanel dpnlMainForm;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentUser;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton btnExit;
    }
}

