namespace MCD.Controls
{
    partial class CollapsiblePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.picExpandCollapse = new System.Windows.Forms.PictureBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.toollbl = new System.Windows.Forms.LinkLabel();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExpandCollapse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.picExpandCollapse);
            this.pnlHeader.Controls.Add(this.picIcon);
            this.pnlHeader.Controls.Add(this.toollbl);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(250, 30);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseLeave += new System.EventHandler(this.pnlHeader_MouseLeave);
            this.pnlHeader.MouseHover += new System.EventHandler(this.pnlHeader_MouseHover);
            // 
            // picExpandCollapse
            // 
            this.picExpandCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picExpandCollapse.BackColor = System.Drawing.Color.Transparent;
            this.picExpandCollapse.Image = global::MCD.Controls.Properties.Resources.collapse;
            this.picExpandCollapse.Location = new System.Drawing.Point(225, 5);
            this.picExpandCollapse.Name = "picExpandCollapse";
            this.picExpandCollapse.Size = new System.Drawing.Size(20, 20);
            this.picExpandCollapse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picExpandCollapse.TabIndex = 2;
            this.picExpandCollapse.TabStop = false;
            this.picExpandCollapse.MouseLeave += new System.EventHandler(this.picExpandCollapse_MouseLeave);
            this.picExpandCollapse.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picExpandCollapse_MouseMove);
            this.picExpandCollapse.Click += new System.EventHandler(this.picExpandCollapse_Click);
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Location = new System.Drawing.Point(5, 5);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(20, 20);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            this.picIcon.Visible = false;
            // 
            // toollbl
            // 
            this.toollbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toollbl.BackColor = System.Drawing.Color.Transparent;
            this.toollbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toollbl.Location = new System.Drawing.Point(160, 6);
            this.toollbl.Name = "toollbl";
            this.toollbl.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toollbl.Size = new System.Drawing.Size(65, 21);
            this.toollbl.TabIndex = 0;
            this.toollbl.TabStop = true;
            this.toollbl.Text = "“—∆Ù”√";
            this.toollbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toollbl.Click += new System.EventHandler(this.toollbl_Click);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Interval = 50;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // CollapsiblePanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlHeader);
            this.Size = new System.Drawing.Size(250, 150);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExpandCollapse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picExpandCollapse;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.LinkLabel toollbl;
    }
}
