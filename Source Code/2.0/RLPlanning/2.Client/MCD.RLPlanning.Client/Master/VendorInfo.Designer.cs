namespace MCD.RLPlanning.Client.Master
{
    partial class VendorInfo
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
            this.lblCompanyCode = new System.Windows.Forms.Label();
            this.txtVendorNo = new System.Windows.Forms.TextBox();
            this.txtVendorMame = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.txtVendorMame);
            this.pnlTitle.Controls.Add(this.lblCompanyName);
            this.pnlTitle.Controls.Add(this.txtVendorNo);
            this.pnlTitle.Controls.Add(this.lblCompanyCode);
            this.pnlTitle.Size = new System.Drawing.Size(670, 67);
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 92);
            this.pnlBody.Size = new System.Drawing.Size(670, 236);
            // 
            // lblCompanyCode
            // 
            this.lblCompanyCode.AutoSize = true;
            this.lblCompanyCode.Location = new System.Drawing.Point(44, 25);
            this.lblCompanyCode.Name = "lblCompanyCode";
            this.lblCompanyCode.Size = new System.Drawing.Size(79, 13);
            this.lblCompanyCode.TabIndex = 0;
            this.lblCompanyCode.Tag = "120";
            this.lblCompanyCode.Text = "供应商编号：";
            // 
            // txtVendorNo
            // 
            this.txtVendorNo.Location = new System.Drawing.Point(126, 22);
            this.txtVendorNo.MaxLength = 32;
            this.txtVendorNo.Name = "txtVendorNo";
            this.txtVendorNo.Size = new System.Drawing.Size(160, 20);
            this.txtVendorNo.TabIndex = 1;
            // 
            // txtVendorMame
            // 
            this.txtVendorMame.Location = new System.Drawing.Point(433, 22);
            this.txtVendorMame.MaxLength = 32;
            this.txtVendorMame.Name = "txtVendorMame";
            this.txtVendorMame.Size = new System.Drawing.Size(160, 20);
            this.txtVendorMame.TabIndex = 3;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(351, 26);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(79, 13);
            this.lblCompanyName.TabIndex = 2;
            this.lblCompanyName.Tag = "427";
            this.lblCompanyName.Text = "供应商名称：";
            // 
            // VendorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 351);
            this.Name = "VendorInfo";
            this.ShowPager = true;
            this.Text = "供应商信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompanyCode;
        private System.Windows.Forms.TextBox txtVendorMame;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtVendorNo;
    }
}