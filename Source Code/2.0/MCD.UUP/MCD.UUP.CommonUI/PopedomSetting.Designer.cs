namespace MCD.UUP.CommonUI
{
    partial class PopedomSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopedomSetting));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.tvSystemUser = new System.Windows.Forms.TreeView();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tvModules = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(609, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(33, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tvSystemUser
            // 
            this.tvSystemUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvSystemUser.Location = new System.Drawing.Point(0, 25);
            this.tvSystemUser.Name = "tvSystemUser";
            this.tvSystemUser.Size = new System.Drawing.Size(151, 396);
            this.tvSystemUser.TabIndex = 1;
            this.tvSystemUser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSystemUser_AfterSelect);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblSelectedInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(151, 25);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(458, 34);
            this.pnlInfo.TabIndex = 3;
            // 
            // lblSelectedInfo
            // 
            this.lblSelectedInfo.AutoSize = true;
            this.lblSelectedInfo.Location = new System.Drawing.Point(19, 13);
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(0, 12);
            this.lblSelectedInfo.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tvModules);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(151, 59);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(458, 362);
            this.pnlMain.TabIndex = 4;
            // 
            // tvModules
            // 
            this.tvModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvModules.Location = new System.Drawing.Point(0, 0);
            this.tvModules.Name = "tvModules";
            this.tvModules.Size = new System.Drawing.Size(458, 362);
            this.tvModules.TabIndex = 2;
            // 
            // PopedomSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 421);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.tvSystemUser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PopedomSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限模块设置";
            this.Load += new System.EventHandler(this.PopedomSetting_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TreeView tvSystemUser;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblSelectedInfo;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TreeView tvModules;
    }
}