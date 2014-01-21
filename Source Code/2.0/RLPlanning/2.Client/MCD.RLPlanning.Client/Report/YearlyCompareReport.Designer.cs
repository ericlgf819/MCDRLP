namespace MCD.RLPlanning.Client.Report
{
    partial class YearlyCompareReport
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
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbFixManagement = new System.Windows.Forms.CheckBox();
            this.chbFixRent = new System.Windows.Forms.CheckBox();
            this.chbRadioService = new System.Windows.Forms.CheckBox();
            this.chbRadioManagement = new System.Windows.Forms.CheckBox();
            this.chbRadioRent = new System.Windows.Forms.CheckBox();
            this.chbStraightRent = new System.Windows.Forms.CheckBox();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.ddlEntityScope = new MCD.Controls.UCComboBox();
            this.ddlOnlyContainOpenBefore = new MCD.Controls.UCComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblEntityScope = new System.Windows.Forms.Label();
            this.lblOnlyContainOpenBefore = new System.Windows.Forms.Label();
            this.lblContractType = new System.Windows.Forms.Label();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radNew = new System.Windows.Forms.RadioButton();
            this.radChange = new System.Windows.Forms.RadioButton();
            this.radExtend = new System.Windows.Forms.RadioButton();
            this.lblYear = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 173);
            this.pnlMain.Size = new System.Drawing.Size(929, 279);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Controls.Add(this.lblYear);
            this.pnlTitle.Controls.Add(this.radExtend);
            this.pnlTitle.Controls.Add(this.radChange);
            this.pnlTitle.Controls.Add(this.radNew);
            this.pnlTitle.Controls.Add(this.radAll);
            this.pnlTitle.Controls.Add(this.lblContractType);
            this.pnlTitle.Controls.Add(this.lblOnlyContainOpenBefore);
            this.pnlTitle.Controls.Add(this.lblEntityScope);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.label3);
            this.pnlTitle.Controls.Add(this.chbStraightRent);
            this.pnlTitle.Controls.Add(this.chbRadioRent);
            this.pnlTitle.Controls.Add(this.chbRadioManagement);
            this.pnlTitle.Controls.Add(this.chbRadioService);
            this.pnlTitle.Controls.Add(this.chbFixRent);
            this.pnlTitle.Controls.Add(this.chbFixManagement);
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.ddlOnlyContainOpenBefore);
            this.pnlTitle.Controls.Add(this.ddlEntityScope);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.txtYear);
            this.pnlTitle.Size = new System.Drawing.Size(929, 148);
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(275, 14);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(145, 21);
            this.ddlArea.TabIndex = 29;
            this.ddlArea.SelectedIndexChanged += new System.EventHandler(this.ddlArea_SelectedIndexChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(275, 50);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(145, 20);
            this.txtYear.TabIndex = 25;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 40;
            this.label2.Tag = "112";
            this.label2.Text = "租金类型:";
            // 
            // chbFixManagement
            // 
            this.chbFixManagement.AutoSize = true;
            this.chbFixManagement.Checked = true;
            this.chbFixManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFixManagement.Location = new System.Drawing.Point(276, 121);
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
            this.chbFixRent.Location = new System.Drawing.Point(374, 121);
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
            this.chbRadioService.Location = new System.Drawing.Point(460, 121);
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
            this.chbRadioManagement.Location = new System.Drawing.Point(570, 121);
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
            this.chbRadioRent.Location = new System.Drawing.Point(680, 121);
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
            this.chbStraightRent.Location = new System.Drawing.Point(778, 121);
            this.chbStraightRent.Name = "chbStraightRent";
            this.chbStraightRent.Size = new System.Drawing.Size(74, 17);
            this.chbStraightRent.TabIndex = 46;
            this.chbStraightRent.Text = "直线租金";
            this.chbStraightRent.UseVisualStyleBackColor = true;
            // 
            // ddlCompany
            // 
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(546, 17);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(145, 21);
            this.ddlCompany.TabIndex = 31;
            // 
            // ddlEntityScope
            // 
            this.ddlEntityScope.FormattingEnabled = true;
            this.ddlEntityScope.Location = new System.Drawing.Point(546, 49);
            this.ddlEntityScope.Name = "ddlEntityScope";
            this.ddlEntityScope.Size = new System.Drawing.Size(145, 21);
            this.ddlEntityScope.TabIndex = 33;
            // 
            // ddlOnlyContainOpenBefore
            // 
            this.ddlOnlyContainOpenBefore.FormattingEnabled = true;
            this.ddlOnlyContainOpenBefore.Items.AddRange(new object[] {
            "是",
            "否"});
            this.ddlOnlyContainOpenBefore.Location = new System.Drawing.Point(276, 83);
            this.ddlOnlyContainOpenBefore.Name = "ddlOnlyContainOpenBefore";
            this.ddlOnlyContainOpenBefore.Size = new System.Drawing.Size(145, 21);
            this.ddlOnlyContainOpenBefore.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(426, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "*";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(236, 17);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 76;
            this.lblArea.Text = "区域:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(507, 21);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(34, 13);
            this.lblCompany.TabIndex = 77;
            this.lblCompany.Text = "区域:";
            // 
            // lblEntityScope
            // 
            this.lblEntityScope.AutoSize = true;
            this.lblEntityScope.Location = new System.Drawing.Point(507, 53);
            this.lblEntityScope.Name = "lblEntityScope";
            this.lblEntityScope.Size = new System.Drawing.Size(34, 13);
            this.lblEntityScope.TabIndex = 78;
            this.lblEntityScope.Text = "范围:";
            // 
            // lblOnlyContainOpenBefore
            // 
            this.lblOnlyContainOpenBefore.AutoSize = true;
            this.lblOnlyContainOpenBefore.Location = new System.Drawing.Point(69, 87);
            this.lblOnlyContainOpenBefore.Name = "lblOnlyContainOpenBefore";
            this.lblOnlyContainOpenBefore.Size = new System.Drawing.Size(202, 13);
            this.lblOnlyContainOpenBefore.TabIndex = 80;
            this.lblOnlyContainOpenBefore.Text = "是否仅包含在开始年限之前开的餐厅:";
            // 
            // lblContractType
            // 
            this.lblContractType.AutoSize = true;
            this.lblContractType.Location = new System.Drawing.Point(483, 88);
            this.lblContractType.Name = "lblContractType";
            this.lblContractType.Size = new System.Drawing.Size(58, 13);
            this.lblContractType.TabIndex = 81;
            this.lblContractType.Text = "合同类型:";
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Checked = true;
            this.radAll.Location = new System.Drawing.Point(547, 86);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(49, 17);
            this.radAll.TabIndex = 82;
            this.radAll.TabStop = true;
            this.radAll.Text = "全部";
            this.radAll.UseVisualStyleBackColor = true;
            // 
            // radNew
            // 
            this.radNew.AutoSize = true;
            this.radNew.Location = new System.Drawing.Point(602, 87);
            this.radNew.Name = "radNew";
            this.radNew.Size = new System.Drawing.Size(49, 17);
            this.radNew.TabIndex = 83;
            this.radNew.TabStop = true;
            this.radNew.Text = "新建";
            this.radNew.UseVisualStyleBackColor = true;
            // 
            // radChange
            // 
            this.radChange.AutoSize = true;
            this.radChange.Location = new System.Drawing.Point(657, 87);
            this.radChange.Name = "radChange";
            this.radChange.Size = new System.Drawing.Size(49, 17);
            this.radChange.TabIndex = 84;
            this.radChange.TabStop = true;
            this.radChange.Text = "变更";
            this.radChange.UseVisualStyleBackColor = true;
            // 
            // radExtend
            // 
            this.radExtend.AutoSize = true;
            this.radExtend.Location = new System.Drawing.Point(712, 87);
            this.radExtend.Name = "radExtend";
            this.radExtend.Size = new System.Drawing.Size(49, 17);
            this.radExtend.TabIndex = 85;
            this.radExtend.TabStop = true;
            this.radExtend.Text = "续租";
            this.radExtend.UseVisualStyleBackColor = true;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(188, 53);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(82, 13);
            this.lblYear.TabIndex = 86;
            this.lblYear.Text = "合同变动年度:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(425, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "*";
            // 
            // YearlyCompareReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Name = "YearlyCompareReport";
            this.ShowPager = true;
            this.Text = "年度对比报表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.UCComboBox ddlArea;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbFixManagement;
        private System.Windows.Forms.CheckBox chbStraightRent;
        private System.Windows.Forms.CheckBox chbRadioRent;
        private System.Windows.Forms.CheckBox chbRadioManagement;
        private System.Windows.Forms.CheckBox chbRadioService;
        private System.Windows.Forms.CheckBox chbFixRent;
        private Controls.UCComboBox ddlCompany;
        private Controls.UCComboBox ddlEntityScope;
        private Controls.UCComboBox ddlOnlyContainOpenBefore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblEntityScope;
        private System.Windows.Forms.Label lblOnlyContainOpenBefore;
        private System.Windows.Forms.Label lblContractType;
        private System.Windows.Forms.RadioButton radExtend;
        private System.Windows.Forms.RadioButton radChange;
        private System.Windows.Forms.RadioButton radNew;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label label1;
    }
}