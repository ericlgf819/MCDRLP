using MCD.Controls;
namespace MCD.RLPlanning.Client.Report
{
    partial class UserDeleteReport
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
            this.cbbGroup = new MCD.Controls.UCComboBox();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.cbbDept = new MCD.Controls.UCComboBox();
            this.lblDeptName = new System.Windows.Forms.Label();
            this.txtEnglishName = new System.Windows.Forms.TextBox();
            this.lblEnglishName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 113);
            this.pnlMain.Size = new System.Drawing.Size(750, 270);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.cbbGroup);
            this.pnlTitle.Controls.Add(this.lblUserGroup);
            this.pnlTitle.Controls.Add(this.cbbDept);
            this.pnlTitle.Controls.Add(this.lblDeptName);
            this.pnlTitle.Controls.Add(this.txtEnglishName);
            this.pnlTitle.Controls.Add(this.lblEnglishName);
            this.pnlTitle.Controls.Add(this.txtUserName);
            this.pnlTitle.Controls.Add(this.lblUserName);
            this.pnlTitle.Size = new System.Drawing.Size(750, 88);
            // 
            // cbbGroup
            // 
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(389, 45);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(145, 21);
            this.cbbGroup.TabIndex = 23;
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Location = new System.Drawing.Point(315, 48);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(70, 13);
            this.lblUserGroup.TabIndex = 21;
            this.lblUserGroup.Tag = "383";
            this.lblUserGroup.Text = "所属用户组:";
            // 
            // cbbDept
            // 
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Location = new System.Drawing.Point(118, 42);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(145, 21);
            this.cbbDept.TabIndex = 19;
            // 
            // lblDeptName
            // 
            this.lblDeptName.AutoSize = true;
            this.lblDeptName.Location = new System.Drawing.Point(55, 46);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(58, 13);
            this.lblDeptName.TabIndex = 17;
            this.lblDeptName.Tag = "112";
            this.lblDeptName.Text = "所属部门:";
            // 
            // txtEnglishName
            // 
            this.txtEnglishName.Location = new System.Drawing.Point(389, 13);
            this.txtEnglishName.MaxLength = 32;
            this.txtEnglishName.Name = "txtEnglishName";
            this.txtEnglishName.Size = new System.Drawing.Size(145, 20);
            this.txtEnglishName.TabIndex = 16;
            // 
            // lblEnglishName
            // 
            this.lblEnglishName.AutoSize = true;
            this.lblEnglishName.Location = new System.Drawing.Point(338, 16);
            this.lblEnglishName.Name = "lblEnglishName";
            this.lblEnglishName.Size = new System.Drawing.Size(46, 13);
            this.lblEnglishName.TabIndex = 15;
            this.lblEnglishName.Tag = "383";
            this.lblEnglishName.Text = "英文名:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(118, 13);
            this.txtUserName.MaxLength = 32;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(145, 20);
            this.txtUserName.TabIndex = 14;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(67, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(46, 13);
            this.lblUserName.TabIndex = 13;
            this.lblUserName.Tag = "112";
            this.lblUserName.Text = "用户名:";
            // 
            // UserDeleteReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 406);
            this.Name = "UserDeleteReport";
            this.ShowPager = true;
            this.Text = "已删除用户报表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCComboBox cbbGroup;
        private System.Windows.Forms.Label lblUserGroup;
        private UCComboBox cbbDept;
        private System.Windows.Forms.Label lblDeptName;
        private System.Windows.Forms.TextBox txtEnglishName;
        private System.Windows.Forms.Label lblEnglishName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
    }
}