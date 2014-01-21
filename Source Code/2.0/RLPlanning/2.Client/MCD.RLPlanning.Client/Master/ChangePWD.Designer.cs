namespace MCD.RLPlanning.Client.Master
{
    partial class ChangePWD
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
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtPasswordConfirm = new System.Windows.Forms.TextBox();
            this.lblUserName = new MCD.Controls.UCLabel();
            this.lblOldPassword = new MCD.Controls.UCLabel();
            this.lblNewPassword = new MCD.Controls.UCLabel();
            this.ucLabel5 = new MCD.Controls.UCLabel();
            this.ucLabel6 = new MCD.Controls.UCLabel();
            this.ucLabel7 = new MCD.Controls.UCLabel();
            this.lblUserNameValue = new MCD.Controls.UCLabel();
            this.lblDescription = new MCD.Controls.UCLabel();
            this.lblPasswordConfirm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(206, 184);
            this.btnSave.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(307, 184);
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(120, 86);
            this.txtOldPassword.MaxLength = 32;
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(120, 20);
            this.txtOldPassword.TabIndex = 6;
            this.txtOldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangePWD_KeyDown);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(120, 117);
            this.txtNewPassword.MaxLength = 32;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(120, 20);
            this.txtNewPassword.TabIndex = 7;
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangePWD_KeyDown);
            // 
            // txtPasswordConfirm
            // 
            this.txtPasswordConfirm.Location = new System.Drawing.Point(381, 120);
            this.txtPasswordConfirm.MaxLength = 32;
            this.txtPasswordConfirm.Name = "txtPasswordConfirm";
            this.txtPasswordConfirm.PasswordChar = '*';
            this.txtPasswordConfirm.Size = new System.Drawing.Size(120, 20);
            this.txtPasswordConfirm.TabIndex = 9;
            this.txtPasswordConfirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangePWD_KeyDown);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.LabelLocation = 115;
            this.lblUserName.Location = new System.Drawing.Point(56, 52);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(58, 13);
            this.lblUserName.TabIndex = 13;
            this.lblUserName.Text = "用户帐号:";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.LabelLocation = 115;
            this.lblOldPassword.Location = new System.Drawing.Point(70, 89);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(46, 13);
            this.lblOldPassword.TabIndex = 14;
            this.lblOldPassword.Text = "旧密码:";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.LabelLocation = 115;
            this.lblNewPassword.Location = new System.Drawing.Point(70, 120);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(46, 13);
            this.lblNewPassword.TabIndex = 15;
            this.lblNewPassword.Text = "新密码:";
            // 
            // ucLabel5
            // 
            this.ucLabel5.AutoSize = true;
            this.ucLabel5.ForeColor = System.Drawing.Color.Red;
            this.ucLabel5.Location = new System.Drawing.Point(246, 92);
            this.ucLabel5.Name = "ucLabel5";
            this.ucLabel5.NeedLanguage = false;
            this.ucLabel5.Size = new System.Drawing.Size(11, 13);
            this.ucLabel5.TabIndex = 17;
            this.ucLabel5.Text = "*";
            // 
            // ucLabel6
            // 
            this.ucLabel6.AutoSize = true;
            this.ucLabel6.ForeColor = System.Drawing.Color.Red;
            this.ucLabel6.Location = new System.Drawing.Point(246, 124);
            this.ucLabel6.Name = "ucLabel6";
            this.ucLabel6.NeedLanguage = false;
            this.ucLabel6.Size = new System.Drawing.Size(11, 13);
            this.ucLabel6.TabIndex = 18;
            this.ucLabel6.Text = "*";
            // 
            // ucLabel7
            // 
            this.ucLabel7.AutoSize = true;
            this.ucLabel7.ForeColor = System.Drawing.Color.Red;
            this.ucLabel7.Location = new System.Drawing.Point(505, 126);
            this.ucLabel7.Name = "ucLabel7";
            this.ucLabel7.NeedLanguage = false;
            this.ucLabel7.Size = new System.Drawing.Size(11, 13);
            this.ucLabel7.TabIndex = 19;
            this.ucLabel7.Text = "*";
            // 
            // lblUserNameValue
            // 
            this.lblUserNameValue.AutoSize = true;
            this.lblUserNameValue.Location = new System.Drawing.Point(118, 52);
            this.lblUserNameValue.Name = "lblUserNameValue";
            this.lblUserNameValue.NeedLanguage = false;
            this.lblUserNameValue.Size = new System.Drawing.Size(0, 13);
            this.lblUserNameValue.TabIndex = 20;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(56, 242);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(220, 65);
            this.lblDescription.TabIndex = 21;
            this.lblDescription.Text = "密码规则说明：\r\n1.密码长度必须大于等于6位。\r\n2.密码必须包含字母和数字。\r\n3.密码不能是最近十三次使用过的密码。\r\n4.密码区分大小写。";
            // 
            // lblPasswordConfirm
            // 
            this.lblPasswordConfirm.AutoSize = true;
            this.lblPasswordConfirm.Location = new System.Drawing.Point(307, 122);
            this.lblPasswordConfirm.Name = "lblPasswordConfirm";
            this.lblPasswordConfirm.Size = new System.Drawing.Size(70, 13);
            this.lblPasswordConfirm.TabIndex = 78;
            this.lblPasswordConfirm.Text = "新密码确认:";
            // 
            // ChangePWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 352);
            this.Controls.Add(this.lblPasswordConfirm);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblUserNameValue);
            this.Controls.Add(this.ucLabel7);
            this.Controls.Add(this.ucLabel6);
            this.Controls.Add(this.ucLabel5);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtPasswordConfirm);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Name = "ChangePWD";
            this.Text = "密码修改";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangePWD_KeyDown);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.txtOldPassword, 0);
            this.Controls.SetChildIndex(this.txtNewPassword, 0);
            this.Controls.SetChildIndex(this.txtPasswordConfirm, 0);
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.lblOldPassword, 0);
            this.Controls.SetChildIndex(this.lblNewPassword, 0);
            this.Controls.SetChildIndex(this.ucLabel5, 0);
            this.Controls.SetChildIndex(this.ucLabel6, 0);
            this.Controls.SetChildIndex(this.ucLabel7, 0);
            this.Controls.SetChildIndex(this.lblUserNameValue, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.lblPasswordConfirm, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtPasswordConfirm;
        private MCD.Controls.UCLabel lblUserName;
        private MCD.Controls.UCLabel lblOldPassword;
        private MCD.Controls.UCLabel lblNewPassword;
        private MCD.Controls.UCLabel ucLabel5;
        private MCD.Controls.UCLabel ucLabel6;
        private MCD.Controls.UCLabel ucLabel7;
        private MCD.Controls.UCLabel lblUserNameValue;
        private MCD.Controls.UCLabel lblDescription;
        private System.Windows.Forms.Label lblPasswordConfirm;
    }
}