using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Controls;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmMain : BaseFrm
    {
        //Fields
        private bool showDialogConfirm = true;
        private IFormHandler formHandler = null;

        /// <summary>
        /// 获取或设置系统退出时，是否需要弹出窗体提示
        /// </summary>
        public bool ShowDialogConfirm
        {
            get
            {
                return this.showDialogConfirm;
            }
            set
            {
                this.showDialogConfirm = value;
            }
        }
        /// <summary>
        /// 控制界面打开方法
        /// </summary>
        private IFormHandler FormHandler
        {
            get
            {
                if (this.formHandler == null)
                {
                    this.formHandler = new FormHandler(base.Language);
                }
                return this.formHandler;
            }
        }
        #region ctor

        public FrmMain()
        {
            InitializeComponent();
        }
        #endregion
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmWait frmwait = new FrmWait(() => {
                this.InitFrm();
            }, base.GetMessage("Wait"), false);
            //
            frmwait.ShowDialog();
            this.FormHandler.OpenForm("PendingTaskInfo", this.dpnlMainForm);//默认打开
            //
            this.btnPendingTask.Enabled = true;
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ShowDialogConfirm)
            {
                string confirmMsg = Program.IsLogOut ? base.GetMessage("ConfirmLogout") : base.GetMessage("ConfirmQuit");
                if (base.MessageConfirm(confirmMsg) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    Program.IsLogOut = false;
                    return;
                }
            }
            //
            // 逐窗口关闭
            while (this.dpnlMainForm.Contents.Count > 0)
            {
                this.dpnlMainForm.Contents[0].DockHandler.Close();
            }
            //退出用户状态
            try
            {
                UserBLL userBLL = new UserBLL();
                base.ExecuteAction(() =>
                {
                    userBLL.UserLoginOut(AppCode.SysEnvironment.CurrentUser.UserName);
                }, "用户退出系统", "系统退出");
                userBLL.Dispose();
            }
            catch (Exception)
            {
                base.CheckOnLine(true);
            }
        }

        /// <summary>
        /// 待处理任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPendingTask_Click(object sender, EventArgs e)
        {
            this.FormHandler.OpenForm("PendingTaskInfo", this.dpnlMainForm);
        }
        /// <summary>
        /// 合同信息锁定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContractList_Click(object sender, EventArgs e)
        {
            this.FormHandler.OpenForm("ContractList", this.dpnlMainForm);
        }
        /// <summary>
        /// 注销系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Program.IsLogOut = true;
            base.Close();
        }
        /// <summary>
        /// 退出系统。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Program.IsLogOut = false;
            base.Close();
        }

        //Methods
        private void InitFrm()
        {
            // 显示当前系统用户
            this.lblCurrentUser.Text = AppCode.SysEnvironment.CurrentUser.UserName;
            // 设置顶部菜单权限
            base.SetMenuRight((MenuStrip)this.topMenu);
            // 设置工具栏按钮权限
            base.SetToolStripRight(this.toolStrip1);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void CreateHandle()
        {
            base.CreateHandle();
            // 导航栏菜单
            this.topMenu.FormHandler = this.FormHandler;
        }

        
        /// <summary>
        /// 打开窗体
        /// </summary>
        /// <param name="classType">程序集.类名</param>
        /// <param name="displayName">显示名称</param>
        internal void OpenForm(string classType, string displayName)
        {
            this.FormHandler.OpenForm(classType, displayName, this.dpnlMainForm, FORM_OPEN.Default);
        }
        /// <summary>
        /// 外部调用的打开窗体接口
        /// </summary>
        /// <param name="classTypeLastName"></param>
        public void OpenForm(string classTypeLastName)
        {
            this.FormHandler.OpenForm(classTypeLastName, this.dpnlMainForm);
        }        
        /// <summary>
        /// 外部调用的关闭窗口接口
        /// </summary>
        /// <param name="classTypeLastName"></param>
        public void CloseForm(string classTypeLastName)
        {
            this.FormHandler.CloseForm(classTypeLastName, this.dpnlMainForm);
        }
    }
}