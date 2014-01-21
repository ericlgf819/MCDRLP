namespace MCD.RLPlanning.Client.ForcastSales
{
    partial class KeyIn
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
            this.tbStoreNoOrName = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblStoreNoOrName = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lbCompany = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lbArea = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(929, 337);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.cbArea);
            this.pnlTitle.Controls.Add(this.lbArea);
            this.pnlTitle.Controls.Add(this.cbStatus);
            this.pnlTitle.Controls.Add(this.lbStatus);
            this.pnlTitle.Controls.Add(this.lbCompany);
            this.pnlTitle.Controls.Add(this.cbCompany);
            this.pnlTitle.Controls.Add(this.cbType);
            this.pnlTitle.Controls.Add(this.lblStoreNoOrName);
            this.pnlTitle.Controls.Add(this.lblType);
            this.pnlTitle.Controls.Add(this.tbStoreNoOrName);
            // 
            // tbStoreNoOrName
            // 
            this.tbStoreNoOrName.Location = new System.Drawing.Point(532, 2);
            this.tbStoreNoOrName.Name = "tbStoreNoOrName";
            this.tbStoreNoOrName.Size = new System.Drawing.Size(121, 21);
            this.tbStoreNoOrName.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(238, 32);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 12);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "类型:";
            // 
            // lblStoreNoOrName
            // 
            this.lblStoreNoOrName.AutoSize = true;
            this.lblStoreNoOrName.Location = new System.Drawing.Point(430, 5);
            this.lblStoreNoOrName.Name = "lblStoreNoOrName";
            this.lblStoreNoOrName.Size = new System.Drawing.Size(89, 12);
            this.lblStoreNoOrName.TabIndex = 3;
            this.lblStoreNoOrName.Text = "餐厅编号/名称:";
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "",
            "甜品店",
            "餐厅"});
            this.cbType.Location = new System.Drawing.Point(285, 29);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 4;
            // 
            // cbCompany
            // 
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(287, 2);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(119, 20);
            this.cbCompany.TabIndex = 5;
            // 
            // lbCompany
            // 
            this.lbCompany.AutoSize = true;
            this.lbCompany.Location = new System.Drawing.Point(238, 5);
            this.lbCompany.Name = "lbCompany";
            this.lbCompany.Size = new System.Drawing.Size(35, 12);
            this.lbCompany.TabIndex = 6;
            this.lbCompany.Text = "公司:";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(30, 32);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(35, 12);
            this.lbStatus.TabIndex = 7;
            this.lbStatus.Text = "状态:";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "",
            "A",
            "I"});
            this.cbStatus.Location = new System.Drawing.Point(77, 30);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 20);
            this.cbStatus.TabIndex = 8;
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Location = new System.Drawing.Point(30, 5);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(35, 12);
            this.lbArea.TabIndex = 9;
            this.lbArea.Text = "区域:";
            // 
            // cbArea
            // 
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(77, 2);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(121, 20);
            this.cbArea.TabIndex = 10;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // KeyIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 438);
            this.Name = "KeyIn";
            this.ShowPager = true;
            this.Text = "KeyIn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeyIn_FormClosed);
            this.Load += new System.EventHandler(this.KeyIn_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStoreNoOrName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox tbStoreNoOrName;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lbCompany;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label lbArea;
    }
}