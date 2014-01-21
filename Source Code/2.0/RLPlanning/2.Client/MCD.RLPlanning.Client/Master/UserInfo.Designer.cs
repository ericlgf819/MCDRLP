using MCD.Controls;
namespace MCD.RLPlanning.Client.Master
{
    partial class UserInfo
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtEnglishName = new System.Windows.Forms.TextBox();
            this.lblEnglishName = new System.Windows.Forms.Label();
            this.ddlStatus = new MCD.Controls.UCComboBox();
            this.ddlGroup = new MCD.Controls.UCComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Size = new System.Drawing.Size(635, 255);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblUserGroup);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.ddlGroup);
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.txtEnglishName);
            this.pnlTitle.Controls.Add(this.lblEnglishName);
            this.pnlTitle.Controls.Add(this.txtUserName);
            this.pnlTitle.Controls.Add(this.lblUserName);
            this.pnlTitle.Size = new System.Drawing.Size(635, 91);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(53, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(46, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Tag = "98";
            this.lblUserName.Text = "用户名:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(104, 17);
            this.txtUserName.MaxLength = 32;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(145, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // txtEnglishName
            // 
            this.txtEnglishName.Location = new System.Drawing.Point(360, 17);
            this.txtEnglishName.MaxLength = 32;
            this.txtEnglishName.Name = "txtEnglishName";
            this.txtEnglishName.Size = new System.Drawing.Size(145, 20);
            this.txtEnglishName.TabIndex = 3;
            // 
            // lblEnglishName
            // 
            this.lblEnglishName.AutoSize = true;
            this.lblEnglishName.Location = new System.Drawing.Point(310, 20);
            this.lblEnglishName.Name = "lblEnglishName";
            this.lblEnglishName.Size = new System.Drawing.Size(46, 13);
            this.lblEnglishName.TabIndex = 2;
            this.lblEnglishName.Tag = "354";
            this.lblEnglishName.Text = "英文名:";
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(360, 51);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(145, 21);
            this.ddlStatus.TabIndex = 8;
            // 
            // ddlGroup
            // 
            this.ddlGroup.FormattingEnabled = true;
            this.ddlGroup.Location = new System.Drawing.Point(104, 51);
            this.ddlGroup.Name = "ddlGroup";
            this.ddlGroup.Size = new System.Drawing.Size(145, 21);
            this.ddlGroup.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(322, 54);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "状态:";
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Location = new System.Drawing.Point(29, 54);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(70, 13);
            this.lblUserGroup.TabIndex = 18;
            this.lblUserGroup.Text = "所属用户组:";
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 394);
            this.Name = "UserInfo";
            this.ShowPager = true;
            this.Text = "用户信息";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtEnglishName;
        private System.Windows.Forms.Label lblEnglishName;
        private UCComboBox ddlStatus;
        private UCComboBox ddlGroup;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblUserGroup;
    }
}