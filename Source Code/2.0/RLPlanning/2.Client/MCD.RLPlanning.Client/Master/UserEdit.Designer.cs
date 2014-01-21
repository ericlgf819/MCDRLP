using MCD.Controls;
namespace MCD.RLPlanning.Client.Master
{
    partial class UserEdit
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
            this.lblX2 = new System.Windows.Forms.Label();
            this.cbbGroup = new MCD.Controls.UCComboBox();
            this.cbbDepartment = new MCD.Controls.UCComboBox();
            this.lblX1 = new System.Windows.Forms.Label();
            this.lblX3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtChineseName = new System.Windows.Forms.TextBox();
            this.txtEnglishName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblDisable = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnDisableN = new System.Windows.Forms.RadioButton();
            this.rbtnDisableY = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnLockedN = new System.Windows.Forms.RadioButton();
            this.rbtnLockedY = new System.Windows.Forms.RadioButton();
            this.lblUserNameValue = new MCD.Controls.UCLabel();
            this.lblLocked = new System.Windows.Forms.Label();
            this.lblUserCompany = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblX5 = new System.Windows.Forms.Label();
            this.lblEnglishName = new System.Windows.Forms.Label();
            this.lblChineseName = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.tvCompany = new System.Windows.Forms.TreeView();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(173, 450);
            this.btnSave.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(274, 450);
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.ForeColor = System.Drawing.Color.Red;
            this.lblX2.Location = new System.Drawing.Point(251, 55);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(11, 13);
            this.lblX2.TabIndex = 50;
            this.lblX2.Text = "*";
            this.lblX2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbGroup
            // 
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(347, 48);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(143, 21);
            this.cbbGroup.TabIndex = 1;
            // 
            // cbbDepartment
            // 
            this.cbbDepartment.FormattingEnabled = true;
            this.cbbDepartment.Location = new System.Drawing.Point(102, 47);
            this.cbbDepartment.Name = "cbbDepartment";
            this.cbbDepartment.Size = new System.Drawing.Size(143, 21);
            this.cbbDepartment.TabIndex = 29;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.ForeColor = System.Drawing.Color.Red;
            this.lblX1.Location = new System.Drawing.Point(495, 30);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(11, 13);
            this.lblX1.TabIndex = 46;
            this.lblX1.Text = "*";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX3
            // 
            this.lblX3.AutoSize = true;
            this.lblX3.ForeColor = System.Drawing.Color.Red;
            this.lblX3.Location = new System.Drawing.Point(498, 54);
            this.lblX3.Name = "lblX3";
            this.lblX3.Size = new System.Drawing.Size(11, 13);
            this.lblX3.TabIndex = 45;
            this.lblX3.Text = "*";
            this.lblX3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(346, 82);
            this.txtEmail.MaxLength = 30;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(143, 20);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(308, 84);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(34, 13);
            this.lblEmail.TabIndex = 43;
            this.lblEmail.Tag = "346";
            this.lblEmail.Text = "邮箱:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChineseName
            // 
            this.txtChineseName.Location = new System.Drawing.Point(102, 81);
            this.txtChineseName.MaxLength = 30;
            this.txtChineseName.Name = "txtChineseName";
            this.txtChineseName.Size = new System.Drawing.Size(143, 20);
            this.txtChineseName.TabIndex = 4;
            // 
            // txtEnglishName
            // 
            this.txtEnglishName.Location = new System.Drawing.Point(346, 20);
            this.txtEnglishName.MaxLength = 30;
            this.txtEnglishName.Name = "txtEnglishName";
            this.txtEnglishName.Size = new System.Drawing.Size(143, 20);
            this.txtEnglishName.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(50, 23);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(46, 13);
            this.lblUserName.TabIndex = 32;
            this.lblUserName.Tag = "98";
            this.lblUserName.Text = "用户名:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDisable
            // 
            this.lblDisable.AutoSize = true;
            this.lblDisable.Location = new System.Drawing.Point(40, 120);
            this.lblDisable.Name = "lblDisable";
            this.lblDisable.Size = new System.Drawing.Size(58, 13);
            this.lblDisable.TabIndex = 53;
            this.lblDisable.Tag = "98";
            this.lblDisable.Text = "是否禁用:";
            this.lblDisable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtnDisableN);
            this.panel1.Controls.Add(this.rbtnDisableY);
            this.panel1.Location = new System.Drawing.Point(102, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 27);
            this.panel1.TabIndex = 55;
            // 
            // rbtnDisableN
            // 
            this.rbtnDisableN.AutoSize = true;
            this.rbtnDisableN.Location = new System.Drawing.Point(65, 7);
            this.rbtnDisableN.Name = "rbtnDisableN";
            this.rbtnDisableN.Size = new System.Drawing.Size(37, 17);
            this.rbtnDisableN.TabIndex = 1;
            this.rbtnDisableN.TabStop = true;
            this.rbtnDisableN.Text = "否";
            this.rbtnDisableN.UseVisualStyleBackColor = true;
            // 
            // rbtnDisableY
            // 
            this.rbtnDisableY.AutoSize = true;
            this.rbtnDisableY.Location = new System.Drawing.Point(3, 5);
            this.rbtnDisableY.Name = "rbtnDisableY";
            this.rbtnDisableY.Size = new System.Drawing.Size(37, 17);
            this.rbtnDisableY.TabIndex = 0;
            this.rbtnDisableY.TabStop = true;
            this.rbtnDisableY.Text = "是";
            this.rbtnDisableY.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnLockedN);
            this.panel2.Controls.Add(this.rbtnLockedY);
            this.panel2.Location = new System.Drawing.Point(347, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(143, 27);
            this.panel2.TabIndex = 56;
            // 
            // rbtnLockedN
            // 
            this.rbtnLockedN.AutoSize = true;
            this.rbtnLockedN.Location = new System.Drawing.Point(65, 7);
            this.rbtnLockedN.Name = "rbtnLockedN";
            this.rbtnLockedN.Size = new System.Drawing.Size(37, 17);
            this.rbtnLockedN.TabIndex = 1;
            this.rbtnLockedN.TabStop = true;
            this.rbtnLockedN.Text = "否";
            this.rbtnLockedN.UseVisualStyleBackColor = true;
            // 
            // rbtnLockedY
            // 
            this.rbtnLockedY.AutoSize = true;
            this.rbtnLockedY.Location = new System.Drawing.Point(3, 5);
            this.rbtnLockedY.Name = "rbtnLockedY";
            this.rbtnLockedY.Size = new System.Drawing.Size(37, 17);
            this.rbtnLockedY.TabIndex = 0;
            this.rbtnLockedY.TabStop = true;
            this.rbtnLockedY.Text = "是";
            this.rbtnLockedY.UseVisualStyleBackColor = true;
            // 
            // lblUserNameValue
            // 
            this.lblUserNameValue.AutoSize = true;
            this.lblUserNameValue.Location = new System.Drawing.Point(99, 23);
            this.lblUserNameValue.Name = "lblUserNameValue";
            this.lblUserNameValue.NeedLanguage = false;
            this.lblUserNameValue.Size = new System.Drawing.Size(0, 13);
            this.lblUserNameValue.TabIndex = 57;
            // 
            // lblLocked
            // 
            this.lblLocked.AutoSize = true;
            this.lblLocked.Location = new System.Drawing.Point(285, 119);
            this.lblLocked.Name = "lblLocked";
            this.lblLocked.Size = new System.Drawing.Size(58, 13);
            this.lblLocked.TabIndex = 54;
            this.lblLocked.Tag = "346";
            this.lblLocked.Text = "是否锁定:";
            this.lblLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserCompany
            // 
            this.lblUserCompany.AutoSize = true;
            this.lblUserCompany.Location = new System.Drawing.Point(41, 158);
            this.lblUserCompany.Name = "lblUserCompany";
            this.lblUserCompany.Size = new System.Drawing.Size(58, 13);
            this.lblUserCompany.TabIndex = 71;
            this.lblUserCompany.Tag = "98";
            this.lblUserCompany.Text = "公司列表:";
            this.lblUserCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(65, 340);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(34, 13);
            this.lblRemark.TabIndex = 69;
            this.lblRemark.Tag = "98";
            this.lblRemark.Text = "备注:";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX5
            // 
            this.lblX5.AutoSize = true;
            this.lblX5.ForeColor = System.Drawing.Color.Red;
            this.lblX5.Location = new System.Drawing.Point(495, 232);
            this.lblX5.Name = "lblX5";
            this.lblX5.Size = new System.Drawing.Size(11, 13);
            this.lblX5.TabIndex = 72;
            this.lblX5.Text = "*";
            this.lblX5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEnglishName
            // 
            this.lblEnglishName.AutoSize = true;
            this.lblEnglishName.Location = new System.Drawing.Point(296, 23);
            this.lblEnglishName.Name = "lblEnglishName";
            this.lblEnglishName.Size = new System.Drawing.Size(46, 13);
            this.lblEnglishName.TabIndex = 73;
            this.lblEnglishName.Text = "英文名:";
            // 
            // lblChineseName
            // 
            this.lblChineseName.AutoSize = true;
            this.lblChineseName.Location = new System.Drawing.Point(51, 84);
            this.lblChineseName.Name = "lblChineseName";
            this.lblChineseName.Size = new System.Drawing.Size(46, 13);
            this.lblChineseName.TabIndex = 74;
            this.lblChineseName.Text = "中文名:";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(103, 338);
            this.txtRemark.MaxLength = 500;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(390, 91);
            this.txtRemark.TabIndex = 78;
            // 
            // tvCompany
            // 
            this.tvCompany.CheckBoxes = true;
            this.tvCompany.Location = new System.Drawing.Point(104, 158);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(387, 163);
            this.tvCompany.TabIndex = 82;
            this.tvCompany.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterCheck);
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(38, 50);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(58, 13);
            this.lblDepartment.TabIndex = 83;
            this.lblDepartment.Text = "所属部门:";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(273, 51);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(70, 13);
            this.lblGroup.TabIndex = 84;
            this.lblGroup.Text = "所属用户组:";
            // 
            // UserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 490);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.tvCompany);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblChineseName);
            this.Controls.Add(this.lblEnglishName);
            this.Controls.Add(this.lblX5);
            this.Controls.Add(this.lblUserCompany);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblUserNameValue);
            this.Controls.Add(this.lblLocked);
            this.Controls.Add(this.lblDisable);
            this.Controls.Add(this.lblX2);
            this.Controls.Add(this.cbbGroup);
            this.Controls.Add(this.cbbDepartment);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.lblX3);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtChineseName);
            this.Controls.Add(this.txtEnglishName);
            this.Controls.Add(this.lblUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UserEdit";
            this.Text = "编辑用户信息";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserEdit_KeyDown);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.txtEnglishName, 0);
            this.Controls.SetChildIndex(this.txtChineseName, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            this.Controls.SetChildIndex(this.lblX3, 0);
            this.Controls.SetChildIndex(this.lblX1, 0);
            this.Controls.SetChildIndex(this.cbbDepartment, 0);
            this.Controls.SetChildIndex(this.cbbGroup, 0);
            this.Controls.SetChildIndex(this.lblX2, 0);
            this.Controls.SetChildIndex(this.lblDisable, 0);
            this.Controls.SetChildIndex(this.lblLocked, 0);
            this.Controls.SetChildIndex(this.lblUserNameValue, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.lblUserCompany, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lblX5, 0);
            this.Controls.SetChildIndex(this.lblEnglishName, 0);
            this.Controls.SetChildIndex(this.lblChineseName, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.tvCompany, 0);
            this.Controls.SetChildIndex(this.lblDepartment, 0);
            this.Controls.SetChildIndex(this.lblGroup, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblX2;
        private UCComboBox cbbGroup;
        private UCComboBox cbbDepartment;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.Label lblX3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtChineseName;
        private System.Windows.Forms.TextBox txtEnglishName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblDisable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnDisableY;
        private System.Windows.Forms.RadioButton rbtnDisableN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnLockedN;
        private System.Windows.Forms.RadioButton rbtnLockedY;
        private MCD.Controls.UCLabel lblUserNameValue;
        private System.Windows.Forms.Label lblLocked;
        private System.Windows.Forms.Label lblUserCompany;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblX5;
        private System.Windows.Forms.Label lblEnglishName;
        private System.Windows.Forms.Label lblChineseName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TreeView tvCompany;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblGroup;
    }
}