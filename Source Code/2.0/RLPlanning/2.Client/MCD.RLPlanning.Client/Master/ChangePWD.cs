using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChangePWD : BaseEdit
    {
        //Fields
        private UserBLL UserBLL = new UserBLL();
        private bool IsLogin = false;
        #region ctor

        public ChangePWD()
        {
            InitializeComponent();
        }

        public ChangePWD(bool login)
        {
            this.IsLogin = login;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //
            InitializeComponent();
        }
        #endregion

        private void ChangePWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            string oldPwd = this.txtOldPassword.Text.Trim();
            string newPwd = this.txtNewPassword.Text.Trim();
            string confirmPwd = this.txtPasswordConfirm.Text.Trim();
            if (!newPwd.Equals(confirmPwd))
            {
                base.MessageError(base.GetMessage("PasswordError"));
                //
                this.txtPasswordConfirm.Focus();
                return;
            }
            else if (newPwd.Equals(oldPwd))
            {
                base.MessageError(base.GetMessage("NewPasswordUsed"));
                //
                this.txtNewPassword.Focus();
                return;
            }
            else if (!UIChecker.VerifyPassword(confirmPwd))
            {
                base.MessageError(base.GetMessage("PasswordRegex"));
                //
                this.txtNewPassword.Focus();
                return;
            }
            // 密码验证
            int res = 0;
            base.ExecuteAction(() => {
                res = this.UserBLL.ChangePassword(AppCode.SysEnvironment.CurrentUser.ID, oldPwd, newPwd);
            }, "更新密码出错", "修改密码");
            if (res == 0)
            {
                base.MessageError(base.GetMessage("OldPasswordError"));
                //
                this.txtOldPassword.Focus();
                return;
            }
            else if (res == 1)
            {
                base.MessageError(base.GetMessage("NewPasswordUsed"));
                return;
            }
            else if (res == 2)
            {
                base.MessageInformation(base.GetMessage("SaveSuccess"));
                //
                if (this.IsLogin) // 打开主窗体
                {   
                    Form frm = new FrmMain();
                    this.Visible = false;
                    frm.ShowDialog();
                    this.Close();
                }
            }
            //
            base.btnSave_Click(sender, e);
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UserBLL.Dispose();
            base.BaseFrm_FormClosing(sender, e);
        }

        /// <summary>
        /// 重写权限控制方法，该窗体不需要控制权限
        /// </summary>
        /// <param name="frm"></param>
        protected override void SetFormRight(Form frm)
        {
            // base.SetFormRight(frm);
        }
        /// <summary>
        /// 绑定界面控件
        /// </summary>
        public override void BindFormControl()
        {
            this.lblUserNameValue.DataBindings.Add("Text", AppCode.SysEnvironment.CurrentUser, "UserName");
            //
            base.BindFormControl();
        }
    }
}