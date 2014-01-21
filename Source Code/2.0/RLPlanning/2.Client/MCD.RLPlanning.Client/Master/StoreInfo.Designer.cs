using MCD.Controls;
namespace MCD.RLPlanning.Client.Master
{
    partial class StoreInfo
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
            this.txtCompanyNo = new System.Windows.Forms.TextBox();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.ddlFromSRLS = new MCD.Controls.UCComboBox();
            this.lblFromSRLS = new System.Windows.Forms.Label();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.ddlStatus = new MCD.Controls.UCComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.lblStore);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.ddlFromSRLS);
            this.pnlTitle.Controls.Add(this.lblFromSRLS);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.txtStoreNo);
            this.pnlTitle.Controls.Add(this.txtCompanyNo);
            this.pnlTitle.Size = new System.Drawing.Size(1044, 89);
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 114);
            this.pnlBody.Size = new System.Drawing.Size(1044, 214);
            // 
            // txtCompanyNo
            // 
            this.txtCompanyNo.Location = new System.Drawing.Point(397, 16);
            this.txtCompanyNo.MaxLength = 32;
            this.txtCompanyNo.Name = "txtCompanyNo";
            this.txtCompanyNo.Size = new System.Drawing.Size(160, 20);
            this.txtCompanyNo.TabIndex = 3;
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(674, 16);
            this.txtStoreNo.MaxLength = 32;
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(160, 20);
            this.txtStoreNo.TabIndex = 6;
            // 
            // ddlFromSRLS
            // 
            this.ddlFromSRLS.FormattingEnabled = true;
            this.ddlFromSRLS.Location = new System.Drawing.Point(397, 54);
            this.ddlFromSRLS.Name = "ddlFromSRLS";
            this.ddlFromSRLS.Size = new System.Drawing.Size(160, 21);
            this.ddlFromSRLS.TabIndex = 17;
            // 
            // lblFromSRLS
            // 
            this.lblFromSRLS.AutoSize = true;
            this.lblFromSRLS.Location = new System.Drawing.Point(359, 57);
            this.lblFromSRLS.Name = "lblFromSRLS";
            this.lblFromSRLS.Size = new System.Drawing.Size(34, 13);
            this.lblFromSRLS.TabIndex = 16;
            this.lblFromSRLS.Tag = "";
            this.lblFromSRLS.Text = "来源:";
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(105, 18);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(160, 21);
            this.ddlArea.TabIndex = 11;
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(105, 55);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(160, 21);
            this.ddlStatus.TabIndex = 18;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(67, 21);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 19;
            this.lblArea.Text = "区域:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(306, 19);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(87, 13);
            this.lblCompany.TabIndex = 20;
            this.lblCompany.Text = "公司编号/简称:";
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(583, 19);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(87, 13);
            this.lblStore.TabIndex = 21;
            this.lblStore.Text = "餐厅编号/名称:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(67, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "状态:";
            // 
            // StoreInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 351);
            this.Name = "StoreInfo";
            this.ShowPager = true;
            this.Text = "餐厅信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompanyNo;
        private System.Windows.Forms.TextBox txtStoreNo;
        private UCComboBox ddlFromSRLS;
        private System.Windows.Forms.Label lblFromSRLS;
        private UCComboBox ddlArea;
        private UCComboBox ddlStatus;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.Label lblStatus;
    }
}