namespace MCD.RLPlanning.Client.Report
{
    partial class ContractExpiresEarlyWarningReport
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
            this.lblCompany = new MCD.Controls.UCLabel();
            this.lblArea = new MCD.Controls.UCLabel();
            this.lblEntityType = new MCD.Controls.UCLabel();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.lblStoreNo = new MCD.Controls.UCLabel();
            this.lblKioskName = new MCD.Controls.UCLabel();
            this.txtKioskName = new System.Windows.Forms.TextBox();
            this.lblExpireYear = new System.Windows.Forms.Label();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.ddlExpireYear = new MCD.Controls.UCComboBox();
            this.ddlEntityType = new MCD.Controls.UCComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 143);
            this.pnlMain.Size = new System.Drawing.Size(722, 249);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.ddlEntityType);
            this.pnlTitle.Controls.Add(this.ddlExpireYear);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblExpireYear);
            this.pnlTitle.Controls.Add(this.txtKioskName);
            this.pnlTitle.Controls.Add(this.lblKioskName);
            this.pnlTitle.Controls.Add(this.txtStoreNo);
            this.pnlTitle.Controls.Add(this.lblStoreNo);
            this.pnlTitle.Controls.Add(this.lblEntityType);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Size = new System.Drawing.Size(722, 118);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.LabelLocation = 418;
            this.lblCompany.Location = new System.Drawing.Point(388, 22);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(34, 13);
            this.lblCompany.TabIndex = 59;
            this.lblCompany.Tag = "110";
            this.lblCompany.Text = "公司:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.LabelLocation = 105;
            this.lblArea.Location = new System.Drawing.Point(82, 21);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 61;
            this.lblArea.Tag = "420";
            this.lblArea.Text = "区域:";
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.LabelLocation = 105;
            this.lblEntityType.Location = new System.Drawing.Point(58, 53);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(58, 13);
            this.lblEntityType.TabIndex = 63;
            this.lblEntityType.Tag = "110";
            this.lblEntityType.Text = "实体类型:";
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(427, 51);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(161, 20);
            this.txtStoreNo.TabIndex = 3;
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.LabelLocation = 418;
            this.lblStoreNo.Location = new System.Drawing.Point(364, 54);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(58, 13);
            this.lblStoreNo.TabIndex = 70;
            this.lblStoreNo.Tag = "420";
            this.lblStoreNo.Text = "餐厅编号:";
            // 
            // lblKioskName
            // 
            this.lblKioskName.AutoSize = true;
            this.lblKioskName.LabelLocation = 105;
            this.lblKioskName.Location = new System.Drawing.Point(56, 83);
            this.lblKioskName.Name = "lblKioskName";
            this.lblKioskName.Size = new System.Drawing.Size(60, 13);
            this.lblKioskName.TabIndex = 72;
            this.lblKioskName.Tag = "110";
            this.lblKioskName.Text = "Kiosk名称:";
            // 
            // txtKioskName
            // 
            this.txtKioskName.Location = new System.Drawing.Point(120, 81);
            this.txtKioskName.MaxLength = 32;
            this.txtKioskName.Name = "txtKioskName";
            this.txtKioskName.Size = new System.Drawing.Size(161, 20);
            this.txtKioskName.TabIndex = 74;
            // 
            // lblExpireYear
            // 
            this.lblExpireYear.AutoSize = true;
            this.lblExpireYear.Location = new System.Drawing.Point(363, 83);
            this.lblExpireYear.Name = "lblExpireYear";
            this.lblExpireYear.Size = new System.Drawing.Size(58, 13);
            this.lblExpireYear.TabIndex = 76;
            this.lblExpireYear.Text = "到期年份:";
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(120, 18);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(161, 21);
            this.ddlArea.TabIndex = 77;
            this.ddlArea.SelectedIndexChanged += new System.EventHandler(this.ddlArea_SelectedIndexChanged);
            // 
            // ddlCompany
            // 
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(427, 18);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(161, 21);
            this.ddlCompany.TabIndex = 78;
            // 
            // ddlExpireYear
            // 
            this.ddlExpireYear.FormattingEnabled = true;
            this.ddlExpireYear.Location = new System.Drawing.Point(427, 79);
            this.ddlExpireYear.Name = "ddlExpireYear";
            this.ddlExpireYear.Size = new System.Drawing.Size(161, 21);
            this.ddlExpireYear.TabIndex = 79;
            // 
            // ddlEntityType
            // 
            this.ddlEntityType.FormattingEnabled = true;
            this.ddlEntityType.Location = new System.Drawing.Point(120, 49);
            this.ddlEntityType.Name = "ddlEntityType";
            this.ddlEntityType.Size = new System.Drawing.Size(161, 21);
            this.ddlEntityType.TabIndex = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(287, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "*";
            // 
            // ContractExpiresEarlyWarningReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 415);
            this.Name = "ContractExpiresEarlyWarningReport";
            this.ShowPager = true;
            this.Text = "合同到期预警报表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblCompany;
        private MCD.Controls.UCLabel lblArea;
        private MCD.Controls.UCLabel lblEntityType;
        private System.Windows.Forms.TextBox txtStoreNo;
        private MCD.Controls.UCLabel lblStoreNo;
        private MCD.Controls.UCLabel lblKioskName;
        private System.Windows.Forms.TextBox txtKioskName;
        private System.Windows.Forms.Label lblExpireYear;
        private Controls.UCComboBox ddlArea;
        private Controls.UCComboBox ddlCompany;
        private Controls.UCComboBox ddlExpireYear;
        private Controls.UCComboBox ddlEntityType;
        private System.Windows.Forms.Label label2;

    }
}