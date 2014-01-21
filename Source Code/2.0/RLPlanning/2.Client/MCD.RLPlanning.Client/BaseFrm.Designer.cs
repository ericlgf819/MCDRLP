namespace MCD.RLPlanning.Client
{
    partial class BaseFrm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuDockPanel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCloseThis = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDockPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuDockPanel
            // 
            this.contextMenuDockPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCloseThis,
            this.menuCloseOther,
            this.menuCloseAll});
            this.contextMenuDockPanel.Name = "contextMenuDockPanel";
            this.contextMenuDockPanel.Size = new System.Drawing.Size(167, 70);
            // 
            // menuCloseThis
            // 
            this.menuCloseThis.Name = "menuCloseThis";
            this.menuCloseThis.Size = new System.Drawing.Size(166, 22);
            this.menuCloseThis.Text = "关闭";
            this.menuCloseThis.Click += new System.EventHandler(this.menuCloseThis_Click);
            // 
            // menuCloseOther
            // 
            this.menuCloseOther.Name = "menuCloseOther";
            this.menuCloseOther.Size = new System.Drawing.Size(166, 22);
            this.menuCloseOther.Text = "除此之外全部关闭";
            this.menuCloseOther.Click += new System.EventHandler(this.menuCloseOther_Click);
            // 
            // menuCloseAll
            // 
            this.menuCloseAll.Name = "menuCloseAll";
            this.menuCloseAll.Size = new System.Drawing.Size(166, 22);
            this.menuCloseAll.Text = "关闭全部窗口";
            this.menuCloseAll.Click += new System.EventHandler(this.menuCloseAll_Click);
            // 
            // BaseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "BaseFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabPageContextMenuStrip = this.contextMenuDockPanel;
            this.TabText = "BaseDock";
            this.Text = "BaseDock";
            this.Load += new System.EventHandler(this.BaseFrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseFrm_FormClosing);
            this.contextMenuDockPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ContextMenuStrip contextMenuDockPanel;
        private System.Windows.Forms.ToolStripMenuItem menuCloseThis;
        private System.Windows.Forms.ToolStripMenuItem menuCloseOther;
        private System.Windows.Forms.ToolStripMenuItem menuCloseAll;
    }
}