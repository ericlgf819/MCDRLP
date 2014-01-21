namespace MCD.RLPlanning.Client.Report
{
    partial class KioskStoreRelationChangReport
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
            this.lblCompany = new System.Windows.Forms.Label();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.lblStartMonth = new System.Windows.Forms.Label();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dtpEndMonth = new System.Windows.Forms.DateTimePicker();
            this.dtpStartMonth = new System.Windows.Forms.DateTimePicker();
            this.lblKioskNo = new System.Windows.Forms.Label();
            this.lblEndMonth = new System.Windows.Forms.Label();
            this.txtKioskNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 153);
            this.pnlMain.Size = new System.Drawing.Size(929, 299);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label3);
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.txtKioskNo);
            this.pnlTitle.Controls.Add(this.lblEndMonth);
            this.pnlTitle.Controls.Add(this.lblKioskNo);
            this.pnlTitle.Controls.Add(this.dtpEndMonth);
            this.pnlTitle.Controls.Add(this.dtpStartMonth);
            this.pnlTitle.Controls.Add(this.txtStoreNo);
            this.pnlTitle.Controls.Add(this.lblStartMonth);
            this.pnlTitle.Controls.Add(this.lblStoreNo);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Size = new System.Drawing.Size(929, 128);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(347, 21);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(34, 13);
            this.lblCompany.TabIndex = 30;
            this.lblCompany.Tag = "383";
            this.lblCompany.Text = "公司:";
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(114, 17);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(145, 21);
            this.ddlArea.TabIndex = 29;
            this.ddlArea.SelectedIndexChanged += new System.EventHandler(this.ddlArea_SelectedIndexChanged);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(76, 20);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 28;
            this.lblArea.Tag = "112";
            this.lblArea.Text = "区域:";
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(52, 60);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(58, 13);
            this.lblStoreNo.TabIndex = 32;
            this.lblStoreNo.Tag = "112";
            this.lblStoreNo.Text = "餐厅编号:";
            // 
            // lblStartMonth
            // 
            this.lblStartMonth.AutoSize = true;
            this.lblStartMonth.Location = new System.Drawing.Point(52, 100);
            this.lblStartMonth.Name = "lblStartMonth";
            this.lblStartMonth.Size = new System.Drawing.Size(58, 13);
            this.lblStartMonth.TabIndex = 36;
            this.lblStartMonth.Tag = "112";
            this.lblStartMonth.Text = "开始月份:";
            // 
            // ddlCompany
            // 
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(385, 17);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(145, 21);
            this.ddlCompany.TabIndex = 31;
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(114, 57);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(144, 20);
            this.txtStoreNo.TabIndex = 47;
            // 
            // dtpEndMonth
            // 
            this.dtpEndMonth.CustomFormat = "yyyy年M月";
            this.dtpEndMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndMonth.Location = new System.Drawing.Point(385, 97);
            this.dtpEndMonth.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpEndMonth.MinDate = new System.DateTime(1912, 1, 1, 0, 0, 0, 0);
            this.dtpEndMonth.Name = "dtpEndMonth";
            this.dtpEndMonth.Size = new System.Drawing.Size(145, 20);
            this.dtpEndMonth.TabIndex = 49;
            // 
            // dtpStartMonth
            // 
            this.dtpStartMonth.CustomFormat = "yyyy年M月";
            this.dtpStartMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartMonth.Location = new System.Drawing.Point(114, 97);
            this.dtpStartMonth.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpStartMonth.MinDate = new System.DateTime(1912, 1, 1, 0, 0, 0, 0);
            this.dtpStartMonth.Name = "dtpStartMonth";
            this.dtpStartMonth.Size = new System.Drawing.Size(145, 20);
            this.dtpStartMonth.TabIndex = 48;
            // 
            // lblKioskNo
            // 
            this.lblKioskNo.AutoSize = true;
            this.lblKioskNo.Location = new System.Drawing.Point(311, 60);
            this.lblKioskNo.Name = "lblKioskNo";
            this.lblKioskNo.Size = new System.Drawing.Size(70, 13);
            this.lblKioskNo.TabIndex = 51;
            this.lblKioskNo.Text = "甜品店编号:";
            // 
            // lblEndMonth
            // 
            this.lblEndMonth.AutoSize = true;
            this.lblEndMonth.Location = new System.Drawing.Point(323, 100);
            this.lblEndMonth.Name = "lblEndMonth";
            this.lblEndMonth.Size = new System.Drawing.Size(58, 13);
            this.lblEndMonth.TabIndex = 52;
            this.lblEndMonth.Text = "结束月份:";
            // 
            // txtKioskNo
            // 
            this.txtKioskNo.Location = new System.Drawing.Point(385, 57);
            this.txtKioskNo.Name = "txtKioskNo";
            this.txtKioskNo.Size = new System.Drawing.Size(144, 20);
            this.txtKioskNo.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(264, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(535, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(264, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "*";
            // 
            // KioskStoreRelationChangReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "KioskStoreRelationChangReport";
            this.ShowPager = true;
            this.Text = "甜品店挂靠关系变更报表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompany;
        private Controls.UCComboBox ddlArea;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblStoreNo;
        private System.Windows.Forms.Label lblStartMonth;
        private Controls.UCComboBox ddlCompany;
        private System.Windows.Forms.TextBox txtStoreNo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DateTimePicker dtpEndMonth;
        private System.Windows.Forms.DateTimePicker dtpStartMonth;
        private System.Windows.Forms.Label lblKioskNo;
        private System.Windows.Forms.Label lblEndMonth;
        private System.Windows.Forms.TextBox txtKioskNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}