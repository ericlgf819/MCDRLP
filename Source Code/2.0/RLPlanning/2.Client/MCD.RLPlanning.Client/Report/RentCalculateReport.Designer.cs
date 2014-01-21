namespace MCD.RLPlanning.Client.Report
{
    partial class RentCalculateReport
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
            this.lblStartMonth = new System.Windows.Forms.Label();
            this.lblRentType = new System.Windows.Forms.Label();
            this.chbFixManagement = new System.Windows.Forms.CheckBox();
            this.chbFixRent = new System.Windows.Forms.CheckBox();
            this.chbRadioService = new System.Windows.Forms.CheckBox();
            this.chbRadioManagement = new System.Windows.Forms.CheckBox();
            this.chbRadioRent = new System.Windows.Forms.CheckBox();
            this.chbStraightRent = new System.Windows.Forms.CheckBox();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dtpEndMonth = new System.Windows.Forms.DateTimePicker();
            this.ddlCloseAcountYear = new MCD.Controls.UCComboBox();
            this.dtpStartMonth = new System.Windows.Forms.DateTimePicker();
            this.lblCloseAccountYear = new System.Windows.Forms.Label();
            this.lblEndMonth = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlEntityScope = new MCD.Controls.UCComboBox();
            this.lblEntityScope = new System.Windows.Forms.Label();
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 260);
            this.pnlMain.Size = new System.Drawing.Size(929, 192);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblStoreNo);
            this.pnlTitle.Controls.Add(this.ddlEntityScope);
            this.pnlTitle.Controls.Add(this.lblEntityScope);
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.lblEndMonth);
            this.pnlTitle.Controls.Add(this.lblCloseAccountYear);
            this.pnlTitle.Controls.Add(this.ddlCloseAcountYear);
            this.pnlTitle.Controls.Add(this.dtpEndMonth);
            this.pnlTitle.Controls.Add(this.dtpStartMonth);
            this.pnlTitle.Controls.Add(this.txtStoreNo);
            this.pnlTitle.Controls.Add(this.chbStraightRent);
            this.pnlTitle.Controls.Add(this.chbRadioRent);
            this.pnlTitle.Controls.Add(this.chbRadioManagement);
            this.pnlTitle.Controls.Add(this.chbRadioService);
            this.pnlTitle.Controls.Add(this.chbFixRent);
            this.pnlTitle.Controls.Add(this.chbFixManagement);
            this.pnlTitle.Controls.Add(this.lblRentType);
            this.pnlTitle.Controls.Add(this.lblStartMonth);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Size = new System.Drawing.Size(929, 235);
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
            // lblStartMonth
            // 
            this.lblStartMonth.AutoSize = true;
            this.lblStartMonth.Location = new System.Drawing.Point(52, 133);
            this.lblStartMonth.Name = "lblStartMonth";
            this.lblStartMonth.Size = new System.Drawing.Size(58, 13);
            this.lblStartMonth.TabIndex = 36;
            this.lblStartMonth.Tag = "112";
            this.lblStartMonth.Text = "开始月份:";
            // 
            // lblRentType
            // 
            this.lblRentType.AutoSize = true;
            this.lblRentType.Location = new System.Drawing.Point(51, 174);
            this.lblRentType.Name = "lblRentType";
            this.lblRentType.Size = new System.Drawing.Size(58, 13);
            this.lblRentType.TabIndex = 40;
            this.lblRentType.Tag = "112";
            this.lblRentType.Text = "租金类型:";
            // 
            // chbFixManagement
            // 
            this.chbFixManagement.AutoSize = true;
            this.chbFixManagement.Checked = true;
            this.chbFixManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFixManagement.Location = new System.Drawing.Point(114, 173);
            this.chbFixManagement.Name = "chbFixManagement";
            this.chbFixManagement.Size = new System.Drawing.Size(86, 17);
            this.chbFixManagement.TabIndex = 41;
            this.chbFixManagement.Text = "固定管理费";
            this.chbFixManagement.UseVisualStyleBackColor = true;
            // 
            // chbFixRent
            // 
            this.chbFixRent.AutoSize = true;
            this.chbFixRent.Checked = true;
            this.chbFixRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFixRent.Location = new System.Drawing.Point(210, 173);
            this.chbFixRent.Name = "chbFixRent";
            this.chbFixRent.Size = new System.Drawing.Size(74, 17);
            this.chbFixRent.TabIndex = 42;
            this.chbFixRent.Text = "固定租金";
            this.chbFixRent.UseVisualStyleBackColor = true;
            // 
            // chbRadioService
            // 
            this.chbRadioService.AutoSize = true;
            this.chbRadioService.Checked = true;
            this.chbRadioService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioService.Location = new System.Drawing.Point(294, 173);
            this.chbRadioService.Name = "chbRadioService";
            this.chbRadioService.Size = new System.Drawing.Size(98, 17);
            this.chbRadioService.TabIndex = 43;
            this.chbRadioService.Text = "百分比服务费";
            this.chbRadioService.UseVisualStyleBackColor = true;
            // 
            // chbRadioManagement
            // 
            this.chbRadioManagement.AutoSize = true;
            this.chbRadioManagement.Checked = true;
            this.chbRadioManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioManagement.Location = new System.Drawing.Point(402, 173);
            this.chbRadioManagement.Name = "chbRadioManagement";
            this.chbRadioManagement.Size = new System.Drawing.Size(98, 17);
            this.chbRadioManagement.TabIndex = 44;
            this.chbRadioManagement.Text = "百分比管理费";
            this.chbRadioManagement.UseVisualStyleBackColor = true;
            // 
            // chbRadioRent
            // 
            this.chbRadioRent.AutoSize = true;
            this.chbRadioRent.Checked = true;
            this.chbRadioRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioRent.Location = new System.Drawing.Point(510, 173);
            this.chbRadioRent.Name = "chbRadioRent";
            this.chbRadioRent.Size = new System.Drawing.Size(86, 17);
            this.chbRadioRent.TabIndex = 45;
            this.chbRadioRent.Text = "百分比租金";
            this.chbRadioRent.UseVisualStyleBackColor = true;
            // 
            // chbStraightRent
            // 
            this.chbStraightRent.AutoSize = true;
            this.chbStraightRent.Checked = true;
            this.chbStraightRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbStraightRent.Location = new System.Drawing.Point(606, 173);
            this.chbStraightRent.Name = "chbStraightRent";
            this.chbStraightRent.Size = new System.Drawing.Size(74, 17);
            this.chbStraightRent.TabIndex = 46;
            this.chbStraightRent.Text = "直线租金";
            this.chbStraightRent.UseVisualStyleBackColor = true;
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
            this.txtStoreNo.Location = new System.Drawing.Point(385, 54);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(144, 20);
            this.txtStoreNo.TabIndex = 47;
            // 
            // dtpEndMonth
            // 
            this.dtpEndMonth.CustomFormat = "yyyy年M月";
            this.dtpEndMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndMonth.Location = new System.Drawing.Point(385, 130);
            this.dtpEndMonth.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpEndMonth.MinDate = new System.DateTime(1912, 1, 1, 0, 0, 0, 0);
            this.dtpEndMonth.Name = "dtpEndMonth";
            this.dtpEndMonth.Size = new System.Drawing.Size(145, 20);
            this.dtpEndMonth.TabIndex = 49;
            // 
            // ddlCloseAcountYear
            // 
            this.ddlCloseAcountYear.FormattingEnabled = true;
            this.ddlCloseAcountYear.Location = new System.Drawing.Point(114, 94);
            this.ddlCloseAcountYear.Name = "ddlCloseAcountYear";
            this.ddlCloseAcountYear.Size = new System.Drawing.Size(145, 21);
            this.ddlCloseAcountYear.TabIndex = 50;
            this.ddlCloseAcountYear.SelectedIndexChanged += new System.EventHandler(this.ddlCloseAcountYear_SelectedIndexChanged);
            // 
            // dtpStartMonth
            // 
            this.dtpStartMonth.CustomFormat = "yyyy年M月";
            this.dtpStartMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartMonth.Location = new System.Drawing.Point(114, 130);
            this.dtpStartMonth.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpStartMonth.MinDate = new System.DateTime(1912, 1, 1, 0, 0, 0, 0);
            this.dtpStartMonth.Name = "dtpStartMonth";
            this.dtpStartMonth.Size = new System.Drawing.Size(145, 20);
            this.dtpStartMonth.TabIndex = 48;
            // 
            // lblCloseAccountYear
            // 
            this.lblCloseAccountYear.AutoSize = true;
            this.lblCloseAccountYear.Location = new System.Drawing.Point(52, 97);
            this.lblCloseAccountYear.Name = "lblCloseAccountYear";
            this.lblCloseAccountYear.Size = new System.Drawing.Size(58, 13);
            this.lblCloseAccountYear.TabIndex = 51;
            this.lblCloseAccountYear.Text = "关账年度:";
            // 
            // lblEndMonth
            // 
            this.lblEndMonth.AutoSize = true;
            this.lblEndMonth.Location = new System.Drawing.Point(323, 133);
            this.lblEndMonth.Name = "lblEndMonth";
            this.lblEndMonth.Size = new System.Drawing.Size(58, 13);
            this.lblEndMonth.TabIndex = 52;
            this.lblEndMonth.Text = "结束月份:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(264, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "*";
            // 
            // ddlEntityScope
            // 
            this.ddlEntityScope.FormattingEnabled = true;
            this.ddlEntityScope.Location = new System.Drawing.Point(114, 57);
            this.ddlEntityScope.Name = "ddlEntityScope";
            this.ddlEntityScope.Size = new System.Drawing.Size(145, 21);
            this.ddlEntityScope.TabIndex = 58;
            // 
            // lblEntityScope
            // 
            this.lblEntityScope.AutoSize = true;
            this.lblEntityScope.Location = new System.Drawing.Point(76, 60);
            this.lblEntityScope.Name = "lblEntityScope";
            this.lblEntityScope.Size = new System.Drawing.Size(34, 13);
            this.lblEntityScope.TabIndex = 57;
            this.lblEntityScope.Tag = "112";
            this.lblEntityScope.Text = "范围:";
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(323, 57);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(58, 13);
            this.lblStoreNo.TabIndex = 79;
            this.lblStoreNo.Text = "餐厅编号:";
            // 
            // RentCalculateReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "RentCalculateReport";
            this.ShowPager = true;
            this.Text = "租金计算报表";
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
        private System.Windows.Forms.Label lblRentType;
        private System.Windows.Forms.Label lblStartMonth;
        private System.Windows.Forms.CheckBox chbFixManagement;
        private System.Windows.Forms.CheckBox chbStraightRent;
        private System.Windows.Forms.CheckBox chbRadioRent;
        private System.Windows.Forms.CheckBox chbRadioManagement;
        private System.Windows.Forms.CheckBox chbRadioService;
        private System.Windows.Forms.CheckBox chbFixRent;
        private Controls.UCComboBox ddlCompany;
        private System.Windows.Forms.TextBox txtStoreNo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DateTimePicker dtpEndMonth;
        private Controls.UCComboBox ddlCloseAcountYear;
        private System.Windows.Forms.DateTimePicker dtpStartMonth;
        private System.Windows.Forms.Label lblCloseAccountYear;
        private System.Windows.Forms.Label lblEndMonth;
        private System.Windows.Forms.Label label2;
        private Controls.UCComboBox ddlEntityScope;
        private System.Windows.Forms.Label lblEntityScope;
        private System.Windows.Forms.Label lblStoreNo;
    }
}