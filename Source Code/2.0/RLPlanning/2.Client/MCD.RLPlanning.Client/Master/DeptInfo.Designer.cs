namespace MCD.RLPlanning.Client.Master
{
    partial class DeptInfo
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
            this.txtDeptCodeOrName = new System.Windows.Forms.TextBox();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.ddlCompany = new MCD.Controls.UCComboBox();
            this.ddlArea = new MCD.Controls.UCComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDeptCodeOrName = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblDeptCodeOrName);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.ddlCompany);
            this.pnlTitle.Controls.Add(this.ddlArea);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.txtDeptCodeOrName);
            this.pnlTitle.Size = new System.Drawing.Size(852, 100);
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 125);
            this.pnlBody.Size = new System.Drawing.Size(852, 258);
            // 
            // txtDeptCodeOrName
            // 
            this.txtDeptCodeOrName.Location = new System.Drawing.Point(116, 62);
            this.txtDeptCodeOrName.MaxLength = 32;
            this.txtDeptCodeOrName.Name = "txtDeptCodeOrName";
            this.txtDeptCodeOrName.Size = new System.Drawing.Size(145, 20);
            this.txtDeptCodeOrName.TabIndex = 1;
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(372, 62);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(145, 21);
            this.ddlStatus.TabIndex = 5;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(333, 24);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(34, 13);
            this.lblCompany.TabIndex = 81;
            this.lblCompany.Text = "公司:";
            // 
            // ddlCompany
            // 
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(372, 21);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(145, 21);
            this.ddlCompany.TabIndex = 80;
            // 
            // ddlArea
            // 
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(116, 21);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(145, 21);
            this.ddlArea.TabIndex = 79;
            this.ddlArea.SelectedIndexChanged += new System.EventHandler(this.ddlArea_SelectedIndexChanged);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(77, 24);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(34, 13);
            this.lblArea.TabIndex = 78;
            this.lblArea.Tag = "112";
            this.lblArea.Text = "区域:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(333, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 82;
            this.lblStatus.Text = "状态:";
            // 
            // lblDeptCodeOrName
            // 
            this.lblDeptCodeOrName.AutoSize = true;
            this.lblDeptCodeOrName.Location = new System.Drawing.Point(24, 65);
            this.lblDeptCodeOrName.Name = "lblDeptCodeOrName";
            this.lblDeptCodeOrName.Size = new System.Drawing.Size(87, 13);
            this.lblDeptCodeOrName.TabIndex = 83;
            this.lblDeptCodeOrName.Text = "部门编号/名称:";
            // 
            // DeptInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 406);
            this.Name = "DeptInfo";
            this.ShowPager = true;
            this.Text = "部门信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDeptCodeOrName;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label lblCompany;
        private Controls.UCComboBox ddlCompany;
        private Controls.UCComboBox ddlArea;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDeptCodeOrName;
    }
}