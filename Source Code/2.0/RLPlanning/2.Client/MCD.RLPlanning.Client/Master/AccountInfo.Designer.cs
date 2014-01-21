namespace MCD.RLPlanning.Client.Master
{
    partial class AccountInfo
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
            this.lbAccountNo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.txtAccountNo);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.lbAccountNo);
            this.pnlTitle.Size = new System.Drawing.Size(782, 39);
            this.pnlTitle.TabIndex = 2;
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 64);
            this.pnlBody.Size = new System.Drawing.Size(782, 300);
            this.pnlBody.TabIndex = 3;
            // 
            // lbAccountNo
            // 
            this.lbAccountNo.AutoSize = true;
            this.lbAccountNo.Location = new System.Drawing.Point(24, 13);
            this.lbAccountNo.Name = "lbAccountNo";
            this.lbAccountNo.Size = new System.Drawing.Size(67, 13);
            this.lbAccountNo.TabIndex = 0;
            this.lbAccountNo.Tag = "80";
            this.lbAccountNo.Text = "科目编号：";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(246, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Tag = "280";
            this.lblStatus.Text = "状态：";
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.Location = new System.Drawing.Point(83, 9);
            this.txtAccountNo.MaxLength = 100;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(117, 20);
            this.txtAccountNo.TabIndex = 2;
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Items.AddRange(new object[] {
            "",
            "A",
            "I"});
            this.ddlStatus.Location = new System.Drawing.Point(284, 10);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(121, 21);
            this.ddlStatus.TabIndex = 3;
            // 
            // AccountInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 387);
            this.Name = "AccountInfo";
            this.ShowPager = true;
            this.Text = "科目信息列表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAccountNo;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblStatus;
    }
}