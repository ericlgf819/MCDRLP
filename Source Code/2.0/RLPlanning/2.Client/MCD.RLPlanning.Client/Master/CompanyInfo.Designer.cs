namespace MCD.RLPlanning.Client.Master
{
    partial class CompanyInfo
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
            this.txtCompanyNoOrName = new System.Windows.Forms.TextBox();
            this.ddlFromSRLS = new MCD.Controls.UCComboBox();
            this.lblFromSRLS = new System.Windows.Forms.Label();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.ddlStatus = new MCD.Controls.UCComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblCompanyNoOrName = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblCompanyNoOrName);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.ddlFromSRLS);
            this.pnlTitle.Controls.Add(this.lblFromSRLS);
            this.pnlTitle.Controls.Add(this.txtCompanyNoOrName);
            this.pnlTitle.Size = new System.Drawing.Size(943, 96);
            this.pnlTitle.TabIndex = 2;
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 121);
            this.pnlBody.Size = new System.Drawing.Size(943, 207);
            this.pnlBody.TabIndex = 3;
            // 
            // txtCompanyNoOrName
            // 
            this.txtCompanyNoOrName.Location = new System.Drawing.Point(377, 17);
            this.txtCompanyNoOrName.MaxLength = 32;
            this.txtCompanyNoOrName.Name = "txtCompanyNoOrName";
            this.txtCompanyNoOrName.Size = new System.Drawing.Size(145, 20);
            this.txtCompanyNoOrName.TabIndex = 3;
            // 
            // ddlFromSRLS
            // 
            this.ddlFromSRLS.FormattingEnabled = true;
            this.ddlFromSRLS.Location = new System.Drawing.Point(377, 55);
            this.ddlFromSRLS.Name = "ddlFromSRLS";
            this.ddlFromSRLS.Size = new System.Drawing.Size(145, 21);
            this.ddlFromSRLS.TabIndex = 19;
            // 
            // lblFromSRLS
            // 
            this.lblFromSRLS.AutoSize = true;
            this.lblFromSRLS.Location = new System.Drawing.Point(337, 58);
            this.lblFromSRLS.Name = "lblFromSRLS";
            this.lblFromSRLS.Size = new System.Drawing.Size(34, 13);
            this.lblFromSRLS.TabIndex = 18;
            this.lblFromSRLS.Tag = "";
            this.lblFromSRLS.Text = "来源:";
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(104, 17);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(145, 21);
            this.ddlArea.TabIndex = 20;
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(104, 55);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(145, 21);
            this.ddlStatus.TabIndex = 21;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(64, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "状态:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(62, 20);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 23;
            this.lblArea.Text = "区域:";
            // 
            // lblCompanyNoOrName
            // 
            this.lblCompanyNoOrName.AutoSize = true;
            this.lblCompanyNoOrName.Location = new System.Drawing.Point(284, 20);
            this.lblCompanyNoOrName.Name = "lblCompanyNoOrName";
            this.lblCompanyNoOrName.Size = new System.Drawing.Size(87, 13);
            this.lblCompanyNoOrName.TabIndex = 24;
            this.lblCompanyNoOrName.Text = "公司编号/名称:";
            // 
            // CompanyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 351);
            this.Name = "CompanyInfo";
            this.ShowPager = true;
            this.Text = "公司信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompanyNoOrName;
        private Controls.UCComboBox ddlFromSRLS;
        private System.Windows.Forms.Label lblFromSRLS;
        private Controls.UCComboBox ddlStatus;
        private Controls.UCComboBox ddlArea;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblCompanyNoOrName;
    }
}