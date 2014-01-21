namespace MCD.Controls
{
    partial class UCMenuButton
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMenuButton));
            this.pnlMenuButtonMain = new System.Windows.Forms.Panel();
            this.lblMenuButtonMain = new System.Windows.Forms.Label();
            this.pBoxMenuButton = new System.Windows.Forms.PictureBox();
            this.ttpMenuTitle = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMenuButtonMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxMenuButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenuButtonMain
            // 
            this.pnlMenuButtonMain.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlMenuButtonMain.Controls.Add(this.lblMenuButtonMain);
            this.pnlMenuButtonMain.Controls.Add(this.pBoxMenuButton);
            this.pnlMenuButtonMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMenuButtonMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenuButtonMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuButtonMain.Name = "pnlMenuButtonMain";
            this.pnlMenuButtonMain.Size = new System.Drawing.Size(167, 25);
            this.pnlMenuButtonMain.TabIndex = 3;
            // 
            // lblMenuButtonMain
            // 
            this.lblMenuButtonMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMenuButtonMain.Location = new System.Drawing.Point(0, 0);
            this.lblMenuButtonMain.Name = "lblMenuButtonMain";
            this.lblMenuButtonMain.Size = new System.Drawing.Size(138, 25);
            this.lblMenuButtonMain.TabIndex = 3;
            this.lblMenuButtonMain.Text = "基本信息";
            this.lblMenuButtonMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenuButtonMain.MouseLeave += new System.EventHandler(this.lblMenuButtonMain_MouseLeave);
            this.lblMenuButtonMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblMenuButtonMain_MouseMove);
            this.lblMenuButtonMain.Click += new System.EventHandler(this.lblMenuButtonMain_Click);
            // 
            // pBoxMenuButton
            // 
            this.pBoxMenuButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pBoxMenuButton.Image = ((System.Drawing.Image)(resources.GetObject("pBoxMenuButton.Image")));
            this.pBoxMenuButton.Location = new System.Drawing.Point(138, 0);
            this.pBoxMenuButton.Name = "pBoxMenuButton";
            this.pBoxMenuButton.Size = new System.Drawing.Size(29, 25);
            this.pBoxMenuButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxMenuButton.TabIndex = 2;
            this.pBoxMenuButton.TabStop = false;
            this.pBoxMenuButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBoxMenuButton_MouseMove);
            this.pBoxMenuButton.Click += new System.EventHandler(this.pBoxMenuButton_Click);
            // 
            // UCMenuButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMenuButtonMain);
            this.Name = "UCMenuButton";
            this.Size = new System.Drawing.Size(167, 25);
            this.pnlMenuButtonMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBoxMenuButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenuButtonMain;
        private System.Windows.Forms.Label lblMenuButtonMain;
        private System.Windows.Forms.PictureBox pBoxMenuButton;
        private System.Windows.Forms.ToolTip ttpMenuTitle;
    }
}
