using MCD.Controls;
namespace MCD.RLPlanning.Client.Master
{
    partial class UserAdd
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtChineseName = new System.Windows.Forms.TextBox();
            this.lblX3 = new System.Windows.Forms.Label();
            this.lblX2 = new System.Windows.Forms.Label();
            this.lblX1 = new System.Windows.Forms.Label();
            this.cbbDepartment = new MCD.Controls.UCComboBox();
            this.cbbGroup = new MCD.Controls.UCComboBox();
            this.lblX5 = new System.Windows.Forms.Label();
            this.lblUserCompany = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblX6 = new System.Windows.Forms.Label();
            this.lblEnglishName = new System.Windows.Forms.Label();
            this.lblChineseName = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.tvCompany = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(178, 449);
            this.btnSave.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(273, 449);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(55, 21);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(46, 13);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Tag = "98";
            this.lblUserName.Text = "用户名:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Location = new System.Drawing.Point(105, 18);
            this.txtUserName.MaxLength = 30;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(143, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // txtEnglishName
            // 
            this.txtEnglishName.Location = new System.Drawing.Point(350, 19);
            this.txtEnglishName.MaxLength = 30;
            this.txtEnglishName.Name = "txtEnglishName";
            this.txtEnglishName.Size = new System.Drawing.Size(143, 20);
            this.txtEnglishName.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(350, 83);
            this.txtEmail.MaxLength = 30;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(143, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(312, 86);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(34, 13);
            this.lblEmail.TabIndex = 17;
            this.lblEmail.Tag = "346";
            this.lblEmail.Text = "邮箱:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChineseName
            // 
            this.txtChineseName.Location = new System.Drawing.Point(106, 81);
            this.txtChineseName.MaxLength = 30;
            this.txtChineseName.Name = "txtChineseName";
            this.txtChineseName.Size = new System.Drawing.Size(143, 20);
            this.txtChineseName.TabIndex = 3;
            // 
            // lblX3
            // 
            this.lblX3.AutoSize = true;
            this.lblX3.ForeColor = System.Drawing.Color.Red;
            this.lblX3.Location = new System.Drawing.Point(498, 54);
            this.lblX3.Name = "lblX3";
            this.lblX3.Size = new System.Drawing.Size(11, 13);
            this.lblX3.TabIndex = 20;
            this.lblX3.Text = "*";
            this.lblX3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.ForeColor = System.Drawing.Color.Red;
            this.lblX2.Location = new System.Drawing.Point(498, 23);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(11, 13);
            this.lblX2.TabIndex = 21;
            this.lblX2.Text = "*";
            this.lblX2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.ForeColor = System.Drawing.Color.Red;
            this.lblX1.Location = new System.Drawing.Point(253, 22);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(11, 13);
            this.lblX1.TabIndex = 22;
            this.lblX1.Text = "*";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbDepartment
            // 
            this.cbbDepartment.FormattingEnabled = true;
            this.cbbDepartment.Location = new System.Drawing.Point(105, 49);
            this.cbbDepartment.Name = "cbbDepartment";
            this.cbbDepartment.Size = new System.Drawing.Size(143, 21);
            this.cbbDepartment.TabIndex = 1;
            this.cbbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbDepartment_KeyDown);
            // 
            // cbbGroup
            // 
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(350, 50);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(143, 21);
            this.cbbGroup.TabIndex = 4;
            // 
            // lblX5
            // 
            this.lblX5.AutoSize = true;
            this.lblX5.ForeColor = System.Drawing.Color.Red;
            this.lblX5.Location = new System.Drawing.Point(252, 53);
            this.lblX5.Name = "lblX5";
            this.lblX5.Size = new System.Drawing.Size(11, 13);
            this.lblX5.TabIndex = 26;
            this.lblX5.Text = "*";
            this.lblX5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserCompany
            // 
            this.lblUserCompany.AutoSize = true;
            this.lblUserCompany.Location = new System.Drawing.Point(42, 114);
            this.lblUserCompany.Name = "lblUserCompany";
            this.lblUserCompany.Size = new System.Drawing.Size(58, 13);
            this.lblUserCompany.TabIndex = 75;
            this.lblUserCompany.Tag = "98";
            this.lblUserCompany.Text = "公司列表:";
            this.lblUserCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(105, 346);
            this.txtRemark.MaxLength = 500;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(388, 91);
            this.txtRemark.TabIndex = 72;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(65, 350);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(34, 13);
            this.lblRemark.TabIndex = 73;
            this.lblRemark.Tag = "98";
            this.lblRemark.Text = "备注:";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX6
            // 
            this.lblX6.AutoSize = true;
            this.lblX6.ForeColor = System.Drawing.Color.Red;
            this.lblX6.Location = new System.Drawing.Point(499, 218);
            this.lblX6.Name = "lblX6";
            this.lblX6.Size = new System.Drawing.Size(11, 13);
            this.lblX6.TabIndex = 76;
            this.lblX6.Text = "*";
            this.lblX6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEnglishName
            // 
            this.lblEnglishName.AutoSize = true;
            this.lblEnglishName.Location = new System.Drawing.Point(300, 21);
            this.lblEnglishName.Name = "lblEnglishName";
            this.lblEnglishName.Size = new System.Drawing.Size(46, 13);
            this.lblEnglishName.TabIndex = 77;
            this.lblEnglishName.Text = "英文名:";
            // 
            // lblChineseName
            // 
            this.lblChineseName.AutoSize = true;
            this.lblChineseName.Location = new System.Drawing.Point(53, 84);
            this.lblChineseName.Name = "lblChineseName";
            this.lblChineseName.Size = new System.Drawing.Size(46, 13);
            this.lblChineseName.TabIndex = 78;
            this.lblChineseName.Text = "中文名:";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(42, 52);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(58, 13);
            this.lblDepartment.TabIndex = 79;
            this.lblDepartment.Text = "所属部门:";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(276, 54);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(70, 13);
            this.lblGroup.TabIndex = 80;
            this.lblGroup.Text = "所属用户组:";
            // 
            // tvCompany
            // 
            this.tvCompany.CheckBoxes = true;
            this.tvCompany.Location = new System.Drawing.Point(106, 114);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(387, 216);
            this.tvCompany.TabIndex = 81;
            this.tvCompany.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterCheck);
            // 
            // UserAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 490);
            this.Controls.Add(this.tvCompany);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.lblChineseName);
            this.Controls.Add(this.lblEnglishName);
            this.Controls.Add(this.lblX6);
            this.Controls.Add(this.lblUserCompany);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.lblX5);
            this.Controls.Add(this.cbbGroup);
            this.Controls.Add(this.cbbDepartment);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.lblX2);
            this.Controls.Add(this.lblX3);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtChineseName);
            this.Controls.Add(this.txtEnglishName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UserAdd";
            this.Text = "新增用户";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtEnglishName, 0);
            this.Controls.SetChildIndex(this.txtChineseName, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.lblX3, 0);
            this.Controls.SetChildIndex(this.lblX2, 0);
            this.Controls.SetChildIndex(this.lblX1, 0);
            this.Controls.SetChildIndex(this.cbbDepartment, 0);
            this.Controls.SetChildIndex(this.cbbGroup, 0);
            this.Controls.SetChildIndex(this.lblX5, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.lblUserCompany, 0);
            this.Controls.SetChildIndex(this.lblX6, 0);
            this.Controls.SetChildIndex(this.lblEnglishName, 0);
            this.Controls.SetChildIndex(this.lblChineseName, 0);
            this.Controls.SetChildIndex(this.lblDepartment, 0);
            this.Controls.SetChildIndex(this.lblGroup, 0);
            this.Controls.SetChildIndex(this.tvCompany, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtEnglishName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtChineseName;
        private System.Windows.Forms.Label lblX3;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.Label lblX1;
        private UCComboBox cbbDepartment;
        private UCComboBox cbbGroup;
        private System.Windows.Forms.Label lblX5;
        private System.Windows.Forms.Label lblUserCompany;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblX6;
        private System.Windows.Forms.Label lblEnglishName;
        private System.Windows.Forms.Label lblChineseName;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.TreeView tvCompany;

    }
}