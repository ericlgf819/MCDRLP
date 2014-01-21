using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.Master;
using MCD.Common;
using MCD.RLPlanning.BLL.Setting;
using MCD.Common.SRLS;
using MCD.RLPlanning.Client.Common;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Login : BaseFrm
    {
        //Fields
        private UserBLL userBLL = new UserBLL();
        #region ctor

        public Login()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void Login_Load(object sender, EventArgs e)
        {
            //
        }
        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmWait frmwait = new FrmWait(() => {
                this.DoLogin();
            }, base.GetMessage("Wait"), false);
            frmwait.ShowDialog();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Methods
        private void DoLogin()
        {
            this.DialogResult = DialogResult.None;
            //
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            if (userName == string.Empty)
            {
                MessageBox.Show(base.GetMessage("UserNameNULL"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password == string.Empty)
            {
                MessageBox.Show(base.GetMessage("PasswordNULL"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (password != userName && !UIChecker.VerifyPassword(password))
            //{
            //    MessageBox.Show(base.GetMessage("PasswordRegex"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            // TODO: 验证用户和密码
            string hostName = System.Environment.UserName;
            string loginhost = string.Empty;
            int res = 0;
            int leftTimes = 0;
            AppCode.SysEnvironment.CurrentUser = userBLL.UserLogin(userName, password, hostName, out res, out leftTimes, out loginhost);
            if (res == 0)
            {
                MessageBox.Show(base.GetMessage("UserNotExists"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (res == 1)
            {
                MessageBox.Show(base.GetMessage("UserDisalbed"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (res == 2)
            {
                MessageBox.Show(base.GetMessage("UserLocked"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (res == 3)
            {
                if (leftTimes == 0)
                {
                    MessageBox.Show(base.GetMessage("UserLocked"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(string.Format(base.GetMessage("PasswordError"), leftTimes), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            else if (res == 5)
            {
                MessageBox.Show(string.Format(base.GetMessage("UserLogined"), loginhost), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (res == 6)
            {
                MessageBox.Show(string.Format(base.GetMessage("UserFromSRLS"), loginhost), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //登录成功后，初始化系统配置信息
                SystemSettings setting = new SystemSettings();
                setting.LoadSettings();//--
                AppCode.SysEnvironment.SystemSettings = setting;//
                //
                this.DialogResult = DialogResult.Yes;
            }
        }
        /// <summary>
        /// 登录界面部做权限控制
        /// </summary>
        /// <param name="frm"></param>
        protected override void SetFormRight(Form frm)
        {
            // base.SetFormRight(frm);
        }
    }
}