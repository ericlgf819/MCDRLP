namespace MCD.RLPlanning.Client.Master
{
    partial class KioskInfo
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
            this.lblKioskNo = new System.Windows.Forms.Label();
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtKioskNo = new System.Windows.Forms.TextBox();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.lblArea = new MCD.Controls.UCLabel();
            this.lblCompany = new MCD.Controls.UCLabel();
            this.txtCompanyNo = new System.Windows.Forms.TextBox();
            this.ddlFromSRLS = new MCD.Controls.UCComboBox();
            this.lblFromSRLS = new System.Windows.Forms.Label();
            this.ddlStatus = new MCD.Controls.UCComboBox();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.ddlFromSRLS);
            this.pnlTitle.Controls.Add(this.lblFromSRLS);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.txtCompanyNo);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.txtStoreNo);
            this.pnlTitle.Controls.Add(this.txtKioskNo);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.lblStoreNo);
            this.pnlTitle.Controls.Add(this.lblKioskNo);
            this.pnlTitle.Size = new System.Drawing.Size(903, 91);
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 116);
            this.pnlBody.Size = new System.Drawing.Size(903, 330);
            // 
            // lblKioskNo
            // 
            this.lblKioskNo.AutoSize = true;
            this.lblKioskNo.Location = new System.Drawing.Point(54, 59);
            this.lblKioskNo.Name = "lblKioskNo";
            this.lblKioskNo.Size = new System.Drawing.Size(60, 13);
            this.lblKioskNo.TabIndex = 9;
            this.lblKioskNo.Text = "Kiosk编号:";
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(640, 25);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(82, 13);
            this.lblStoreNo.TabIndex = 10;
            this.lblStoreNo.Text = "归属餐厅编号:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(389, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "状态:";
            // 
            // txtKioskNo
            // 
            this.txtKioskNo.Location = new System.Drawing.Point(119, 56);
            this.txtKioskNo.Name = "txtKioskNo";
            this.txtKioskNo.Size = new System.Drawing.Size(160, 20);
            this.txtKioskNo.TabIndex = 12;
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(727, 21);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(160, 20);
            this.txtStoreNo.TabIndex = 13;
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(120, 23);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(160, 21);
            this.ddlArea.TabIndex = 16;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(81, 26);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 15;
            this.lblArea.Text = "区域:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(338, 26);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(87, 13);
            this.lblCompany.TabIndex = 18;
            this.lblCompany.Text = "公司编号/简称:";
            // 
            // txtCompanyNo
            // 
            this.txtCompanyNo.Location = new System.Drawing.Point(433, 23);
            this.txtCompanyNo.MaxLength = 32;
            this.txtCompanyNo.Name = "txtCompanyNo";
            this.txtCompanyNo.Size = new System.Drawing.Size(160, 20);
            this.txtCompanyNo.TabIndex = 17;
            // 
            // ddlFromSRLS
            // 
            this.ddlFromSRLS.FormattingEnabled = true;
            this.ddlFromSRLS.Location = new System.Drawing.Point(727, 54);
            this.ddlFromSRLS.Name = "ddlFromSRLS";
            this.ddlFromSRLS.Size = new System.Drawing.Size(160, 21);
            this.ddlFromSRLS.TabIndex = 20;
            // 
            // lblFromSRLS
            // 
            this.lblFromSRLS.AutoSize = true;
            this.lblFromSRLS.Location = new System.Drawing.Point(686, 58);
            this.lblFromSRLS.Name = "lblFromSRLS";
            this.lblFromSRLS.Size = new System.Drawing.Size(34, 13);
            this.lblFromSRLS.TabIndex = 19;
            this.lblFromSRLS.Text = "来源:";
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(433, 55);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(160, 21);
            this.ddlStatus.TabIndex = 21;
            // 
            // KioskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 469);
            this.Name = "KioskInfo";
            this.ShowPager = true;
            this.Text = "Kiosk信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStoreNo;
        private System.Windows.Forms.Label lblKioskNo;
        private System.Windows.Forms.TextBox txtStoreNo;
        private System.Windows.Forms.TextBox txtKioskNo;
        private Controls.UCComboBox ddlArea;
        private Controls.UCLabel lblArea;
        private Controls.UCLabel lblCompany;
        private System.Windows.Forms.TextBox txtCompanyNo;
        private Controls.UCComboBox ddlFromSRLS;
        private System.Windows.Forms.Label lblFromSRLS;
        private Controls.UCComboBox ddlStatus;

    }
}