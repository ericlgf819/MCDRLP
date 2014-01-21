namespace MCD.RLPlanning.Client.Report
{
    partial class AverageCompareReport
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
            this.chbStraightRent = new System.Windows.Forms.CheckBox();
            this.chbRadioRent = new System.Windows.Forms.CheckBox();
            this.chbRadioManagement = new System.Windows.Forms.CheckBox();
            this.chbRadioService = new System.Windows.Forms.CheckBox();
            this.chbFixRent = new System.Windows.Forms.CheckBox();
            this.chbFixManagement = new System.Windows.Forms.CheckBox();
            this.lblRentType = new System.Windows.Forms.Label();
            this.ddlEntityScope = new MCD.Controls.UCComboBox();
            this.lblEntityScope = new System.Windows.Forms.Label();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblEarliestOpenYear = new System.Windows.Forms.Label();
            this.txtEarliestOpenYear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRentYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblRentYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 199);
            this.pnlMain.Size = new System.Drawing.Size(929, 253);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label3);
            this.pnlTitle.Controls.Add(this.lblRentYear);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.txtRentYear);
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Controls.Add(this.txtEarliestOpenYear);
            this.pnlTitle.Controls.Add(this.chbStraightRent);
            this.pnlTitle.Controls.Add(this.chbRadioRent);
            this.pnlTitle.Controls.Add(this.chbRadioManagement);
            this.pnlTitle.Controls.Add(this.chbRadioService);
            this.pnlTitle.Controls.Add(this.chbFixRent);
            this.pnlTitle.Controls.Add(this.chbFixManagement);
            this.pnlTitle.Controls.Add(this.lblRentType);
            this.pnlTitle.Controls.Add(this.ddlEntityScope);
            this.pnlTitle.Controls.Add(this.lblEntityScope);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.lblEarliestOpenYear);
            this.pnlTitle.Size = new System.Drawing.Size(929, 174);
            // 
            // chbStraightRent
            // 
            this.chbStraightRent.AutoSize = true;
            this.chbStraightRent.Checked = true;
            this.chbStraightRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbStraightRent.Location = new System.Drawing.Point(617, 133);
            this.chbStraightRent.Name = "chbStraightRent";
            this.chbStraightRent.Size = new System.Drawing.Size(74, 17);
            this.chbStraightRent.TabIndex = 69;
            this.chbStraightRent.Text = "直线租金";
            this.chbStraightRent.UseVisualStyleBackColor = true;
            // 
            // chbRadioRent
            // 
            this.chbRadioRent.AutoSize = true;
            this.chbRadioRent.Checked = true;
            this.chbRadioRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioRent.Location = new System.Drawing.Point(519, 133);
            this.chbRadioRent.Name = "chbRadioRent";
            this.chbRadioRent.Size = new System.Drawing.Size(86, 17);
            this.chbRadioRent.TabIndex = 68;
            this.chbRadioRent.Text = "百分比租金";
            this.chbRadioRent.UseVisualStyleBackColor = true;
            // 
            // chbRadioManagement
            // 
            this.chbRadioManagement.AutoSize = true;
            this.chbRadioManagement.Checked = true;
            this.chbRadioManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioManagement.Location = new System.Drawing.Point(409, 133);
            this.chbRadioManagement.Name = "chbRadioManagement";
            this.chbRadioManagement.Size = new System.Drawing.Size(98, 17);
            this.chbRadioManagement.TabIndex = 67;
            this.chbRadioManagement.Text = "百分比管理费";
            this.chbRadioManagement.UseVisualStyleBackColor = true;
            // 
            // chbRadioService
            // 
            this.chbRadioService.AutoSize = true;
            this.chbRadioService.Checked = true;
            this.chbRadioService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadioService.Location = new System.Drawing.Point(299, 133);
            this.chbRadioService.Name = "chbRadioService";
            this.chbRadioService.Size = new System.Drawing.Size(98, 17);
            this.chbRadioService.TabIndex = 66;
            this.chbRadioService.Text = "百分比服务费";
            this.chbRadioService.UseVisualStyleBackColor = true;
            // 
            // chbFixRent
            // 
            this.chbFixRent.AutoSize = true;
            this.chbFixRent.Checked = true;
            this.chbFixRent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFixRent.Location = new System.Drawing.Point(213, 133);
            this.chbFixRent.Name = "chbFixRent";
            this.chbFixRent.Size = new System.Drawing.Size(74, 17);
            this.chbFixRent.TabIndex = 65;
            this.chbFixRent.Text = "固定租金";
            this.chbFixRent.UseVisualStyleBackColor = true;
            // 
            // chbFixManagement
            // 
            this.chbFixManagement.AutoSize = true;
            this.chbFixManagement.Checked = true;
            this.chbFixManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFixManagement.Location = new System.Drawing.Point(115, 133);
            this.chbFixManagement.Name = "chbFixManagement";
            this.chbFixManagement.Size = new System.Drawing.Size(86, 17);
            this.chbFixManagement.TabIndex = 64;
            this.chbFixManagement.Text = "固定管理费";
            this.chbFixManagement.UseVisualStyleBackColor = true;
            // 
            // lblRentType
            // 
            this.lblRentType.AutoSize = true;
            this.lblRentType.Location = new System.Drawing.Point(51, 134);
            this.lblRentType.Name = "lblRentType";
            this.lblRentType.Size = new System.Drawing.Size(58, 13);
            this.lblRentType.TabIndex = 63;
            this.lblRentType.Tag = "112";
            this.lblRentType.Text = "租金类型:";
            // 
            // ddlEntityScope
            // 
            this.ddlEntityScope.FormattingEnabled = true;
            this.ddlEntityScope.Location = new System.Drawing.Point(115, 58);
            this.ddlEntityScope.Name = "ddlEntityScope";
            this.ddlEntityScope.Size = new System.Drawing.Size(145, 21);
            this.ddlEntityScope.TabIndex = 56;
            // 
            // lblEntityScope
            // 
            this.lblEntityScope.AutoSize = true;
            this.lblEntityScope.Location = new System.Drawing.Point(76, 62);
            this.lblEntityScope.Name = "lblEntityScope";
            this.lblEntityScope.Size = new System.Drawing.Size(34, 13);
            this.lblEntityScope.TabIndex = 55;
            this.lblEntityScope.Tag = "112";
            this.lblEntityScope.Text = "范围:";
            // 
            // ddlCompany
            // 
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(421, 22);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(145, 21);
            this.ddlCompany.TabIndex = 54;
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(115, 19);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(145, 21);
            this.ddlArea.TabIndex = 52;
            this.ddlArea.SelectedIndexChanged += new System.EventHandler(this.ddlArea_SelectedIndexChanged);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(76, 23);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 51;
            this.lblArea.Tag = "112";
            this.lblArea.Text = "区域:";
            // 
            // lblEarliestOpenYear
            // 
            this.lblEarliestOpenYear.AutoSize = true;
            this.lblEarliestOpenYear.Location = new System.Drawing.Point(28, 99);
            this.lblEarliestOpenYear.Name = "lblEarliestOpenYear";
            this.lblEarliestOpenYear.Size = new System.Drawing.Size(82, 13);
            this.lblEarliestOpenYear.TabIndex = 47;
            this.lblEarliestOpenYear.Tag = "112";
            this.lblEarliestOpenYear.Text = "最早开业年份:";
            // 
            // txtEarliestOpenYear
            // 
            this.txtEarliestOpenYear.Location = new System.Drawing.Point(115, 96);
            this.txtEarliestOpenYear.MaxLength = 4;
            this.txtEarliestOpenYear.Name = "txtEarliestOpenYear";
            this.txtEarliestOpenYear.Size = new System.Drawing.Size(145, 20);
            this.txtEarliestOpenYear.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(569, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "*";
            // 
            // txtRentYear
            // 
            this.txtRentYear.Location = new System.Drawing.Point(421, 96);
            this.txtRentYear.MaxLength = 4;
            this.txtRentYear.Name = "txtRentYear";
            this.txtRentYear.Size = new System.Drawing.Size(145, 20);
            this.txtRentYear.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(266, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 76;
            this.label2.Text = "*";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(382, 25);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(34, 13);
            this.lblCompany.TabIndex = 77;
            this.lblCompany.Text = "公司:";
            // 
            // lblRentYear
            // 
            this.lblRentYear.AutoSize = true;
            this.lblRentYear.Location = new System.Drawing.Point(298, 99);
            this.lblRentYear.Name = "lblRentYear";
            this.lblRentYear.Size = new System.Drawing.Size(118, 13);
            this.lblRentYear.TabIndex = 78;
            this.lblRentYear.Text = "所需计算租金的年份:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(266, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "*";
            // 
            // AverageCompareReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "AverageCompareReport";
            this.ShowPager = true;
            this.Text = "平均销售额和租金对比报表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbStraightRent;
        private System.Windows.Forms.CheckBox chbRadioRent;
        private System.Windows.Forms.CheckBox chbRadioManagement;
        private System.Windows.Forms.CheckBox chbRadioService;
        private System.Windows.Forms.CheckBox chbFixRent;
        private System.Windows.Forms.CheckBox chbFixManagement;
        private System.Windows.Forms.Label lblRentType;
        private Controls.UCComboBox ddlEntityScope;
        private System.Windows.Forms.Label lblEntityScope;
        private Controls.UCComboBox ddlCompany;
        private Controls.UCComboBox ddlArea;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblEarliestOpenYear;
        private System.Windows.Forms.TextBox txtEarliestOpenYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRentYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblRentYear;
        private System.Windows.Forms.Label label3;
    }
}