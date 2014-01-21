namespace MCD.RLPlanning.Client.Report
{
    partial class ModifyRecordReport
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
            this.lblArea = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblStoreOrKioskNo = new System.Windows.Forms.Label();
            this.lblChangeType = new System.Windows.Forms.Label();
            this.cbChangeType = new System.Windows.Forms.ComboBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.tbStoreOrKioskNo = new System.Windows.Forms.TextBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 136);
            this.pnlMain.Size = new System.Drawing.Size(929, 316);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.dtEndDate);
            this.pnlTitle.Controls.Add(this.lblEndDate);
            this.pnlTitle.Controls.Add(this.tbStoreOrKioskNo);
            this.pnlTitle.Controls.Add(this.dtStartDate);
            this.pnlTitle.Controls.Add(this.lblStartDate);
            this.pnlTitle.Controls.Add(this.cbChangeType);
            this.pnlTitle.Controls.Add(this.lblChangeType);
            this.pnlTitle.Controls.Add(this.lblStoreOrKioskNo);
            this.pnlTitle.Controls.Add(this.cbCompany);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.cbArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Size = new System.Drawing.Size(929, 111);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(187, 11);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(43, 13);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "区域：";
            // 
            // cbArea
            // 
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(236, 8);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(121, 21);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            this.cbArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbArea_KeyUp);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(486, 11);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(43, 13);
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text = "公司：";
            // 
            // cbCompany
            // 
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(535, 8);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(121, 21);
            this.cbCompany.TabIndex = 3;
            // 
            // lblStoreOrKioskNo
            // 
            this.lblStoreOrKioskNo.AutoSize = true;
            this.lblStoreOrKioskNo.Location = new System.Drawing.Point(122, 46);
            this.lblStoreOrKioskNo.Name = "lblStoreOrKioskNo";
            this.lblStoreOrKioskNo.Size = new System.Drawing.Size(108, 13);
            this.lblStoreOrKioskNo.TabIndex = 4;
            this.lblStoreOrKioskNo.Text = "餐厅/甜品店编号：";
            // 
            // lblChangeType
            // 
            this.lblChangeType.AutoSize = true;
            this.lblChangeType.Location = new System.Drawing.Point(462, 42);
            this.lblChangeType.Name = "lblChangeType";
            this.lblChangeType.Size = new System.Drawing.Size(67, 13);
            this.lblChangeType.TabIndex = 6;
            this.lblChangeType.Text = "变更内容：";
            // 
            // cbChangeType
            // 
            this.cbChangeType.FormattingEnabled = true;
            this.cbChangeType.Items.AddRange(new object[] {
            "",
            "合同新增",
            "实体",
            "出租方",
            "固定租金",
            "固定管理费",
            "百分比服务费",
            "百分比管理费",
            "百分比租金"});
            this.cbChangeType.Location = new System.Drawing.Point(535, 42);
            this.cbChangeType.Name = "cbChangeType";
            this.cbChangeType.Size = new System.Drawing.Size(121, 21);
            this.cbChangeType.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(163, 77);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(67, 13);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "开始日期：";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(236, 77);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(121, 20);
            this.dtStartDate.TabIndex = 9;
            // 
            // tbStoreOrKioskNo
            // 
            this.tbStoreOrKioskNo.Location = new System.Drawing.Point(236, 43);
            this.tbStoreOrKioskNo.Name = "tbStoreOrKioskNo";
            this.tbStoreOrKioskNo.Size = new System.Drawing.Size(121, 20);
            this.tbStoreOrKioskNo.TabIndex = 10;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(462, 77);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(67, 13);
            this.lblEndDate.TabIndex = 11;
            this.lblEndDate.Text = "结束日期：";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(535, 77);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(121, 20);
            this.dtEndDate.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(363, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "*";
            // 
            // ModifyRecordReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "ModifyRecordReport";
            this.ShowPager = true;
            this.Text = "ModifyRecordReport";
            this.Load += new System.EventHandler(this.ModifyRecordReport_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblStoreOrKioskNo;
        private System.Windows.Forms.Label lblChangeType;
        private System.Windows.Forms.ComboBox cbChangeType;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.TextBox tbStoreOrKioskNo;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Label label2;
    }
}