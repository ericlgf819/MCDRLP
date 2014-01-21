namespace MCD.RLPlanning.Client.Report
{
    partial class SalesDataReport
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
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.tbStoreNo = new System.Windows.Forms.TextBox();
            this.lblCompanyCode = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.cbEntityType = new System.Windows.Forms.ComboBox();
            this.lblCashCloseYear = new System.Windows.Forms.Label();
            this.cbCashCloseYear = new System.Windows.Forms.ComboBox();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 137);
            this.pnlMain.Size = new System.Drawing.Size(929, 315);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.tbYear);
            this.pnlTitle.Controls.Add(this.cbCashCloseYear);
            this.pnlTitle.Controls.Add(this.lblCashCloseYear);
            this.pnlTitle.Controls.Add(this.cbEntityType);
            this.pnlTitle.Controls.Add(this.lblEntityType);
            this.pnlTitle.Controls.Add(this.cbCompany);
            this.pnlTitle.Controls.Add(this.cbArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.lblYear);
            this.pnlTitle.Controls.Add(this.lblCompanyCode);
            this.pnlTitle.Controls.Add(this.tbStoreNo);
            this.pnlTitle.Controls.Add(this.lblStoreNo);
            this.pnlTitle.Size = new System.Drawing.Size(929, 112);
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(153, 50);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(67, 13);
            this.lblStoreNo.TabIndex = 0;
            this.lblStoreNo.Text = "餐厅编号：";
            // 
            // tbStoreNo
            // 
            this.tbStoreNo.Location = new System.Drawing.Point(226, 47);
            this.tbStoreNo.Name = "tbStoreNo";
            this.tbStoreNo.Size = new System.Drawing.Size(121, 20);
            this.tbStoreNo.TabIndex = 1;
            // 
            // lblCompanyCode
            // 
            this.lblCompanyCode.AutoSize = true;
            this.lblCompanyCode.Location = new System.Drawing.Point(472, 20);
            this.lblCompanyCode.Name = "lblCompanyCode";
            this.lblCompanyCode.Size = new System.Drawing.Size(43, 13);
            this.lblCompanyCode.TabIndex = 2;
            this.lblCompanyCode.Text = "公司：";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(472, 50);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(43, 13);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "年度：";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(177, 20);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(43, 13);
            this.lblArea.TabIndex = 6;
            this.lblArea.Text = "区域：";
            // 
            // cbArea
            // 
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(226, 16);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(121, 21);
            this.cbArea.TabIndex = 7;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // cbCompany
            // 
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(521, 16);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(121, 21);
            this.cbCompany.TabIndex = 8;
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.Location = new System.Drawing.Point(153, 83);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(67, 13);
            this.lblEntityType.TabIndex = 9;
            this.lblEntityType.Text = "实体类型：";
            // 
            // cbEntityType
            // 
            this.cbEntityType.FormattingEnabled = true;
            this.cbEntityType.Items.AddRange(new object[] {
            "",
            "餐厅",
            "甜品店"});
            this.cbEntityType.Location = new System.Drawing.Point(226, 80);
            this.cbEntityType.Name = "cbEntityType";
            this.cbEntityType.Size = new System.Drawing.Size(121, 21);
            this.cbEntityType.TabIndex = 10;
            // 
            // lblCashCloseYear
            // 
            this.lblCashCloseYear.AutoSize = true;
            this.lblCashCloseYear.Location = new System.Drawing.Point(448, 83);
            this.lblCashCloseYear.Name = "lblCashCloseYear";
            this.lblCashCloseYear.Size = new System.Drawing.Size(67, 13);
            this.lblCashCloseYear.TabIndex = 12;
            this.lblCashCloseYear.Text = "关帐年度：";
            // 
            // cbCashCloseYear
            // 
            this.cbCashCloseYear.FormattingEnabled = true;
            this.cbCashCloseYear.Location = new System.Drawing.Point(521, 80);
            this.cbCashCloseYear.Name = "cbCashCloseYear";
            this.cbCashCloseYear.Size = new System.Drawing.Size(121, 21);
            this.cbCashCloseYear.TabIndex = 13;
            this.cbCashCloseYear.SelectedIndexChanged += new System.EventHandler(this.cbCashCloseYear_SelectedIndexChanged);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(521, 47);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(120, 20);
            this.tbYear.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(353, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "*";
            // 
            // SalesDataReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "SalesDataReport";
            this.ShowPager = true;
            this.Text = "SalesDataReport";
            this.Load += new System.EventHandler(this.SalesDataReport_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStoreNo;
        private System.Windows.Forms.TextBox tbStoreNo;
        private System.Windows.Forms.Label lblCompanyCode;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.ComboBox cbEntityType;
        private System.Windows.Forms.Label lblCashCloseYear;
        private System.Windows.Forms.ComboBox cbCashCloseYear;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Label label2;
    }
}