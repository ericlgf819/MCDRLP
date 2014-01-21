using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.Framework.AppCode;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client.UUPPopedomService;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 用户控件基类，支持国语言资源。
    /// </summary>
    public partial class BaseUserControl : UserControl
    {
        #region ctor

        public BaseUserControl()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            if (!base.DesignMode)
            {
                this.Language.SetLanguage();
                this.BindFormControl();
            }
        }
        private void BaseUserControl_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 窗体关闭时，消亡对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// 窗体加载后初始化控件。
        /// </summary>
        protected virtual void BindFormControl()
        {
            
        }
        /// <summary>
        /// 在派生类中重写刷新窗体列表。
        /// </summary>
        public new virtual void Refresh()
        {

        }

        /// <summary>
        /// 执行指定的委托返回是否有异常，并指定发生异常时写错误日志的类型与来源、是否提示异常信息。
        /// </summary>
        /// <param name="action">指定的委托</param>
        /// <param name="logType">写日志的类型</param>
        /// <param name="logSource">写日志的错误来源</param>
        /// <param name="showErrorMsg">是否显示出错误提示框</param>
        /// <returns>是否运行成功</returns>
        public bool ExecuteAction(Action action, string logType, string logSource, bool showErrorMsg)
        {
            Form frm = this.FindForm();
            if (frm is BaseFrm)
            {
                return (frm as BaseFrm).ExecuteAction(action, logType, logSource, showErrorMsg);
            }
            return true;
        }
        /// <summary>
        /// 执行指定的委托并返回是否有异常，并指定异常时是否提示异常信息。
        /// </summary>
        /// <param name="action">指定的委托</param>
        /// <param name="showErrorMsg">是否显示出错误提示框</param>
        /// <returns>是否运行成功</returns>
        public bool ExecuteAction(Action action, bool showErrorMsg)
        {
            return this.ExecuteAction(action, null, null, showErrorMsg);
        }
        /// <summary>
        /// 执行指定的委托并返回是否有异常不提示异常信息。
        /// </summary>
        /// <param name="action">指定的委托</param>
        /// <returns>是否运行成功</returns>
        public bool ExecuteAction(Action action)
        {
            return this.ExecuteAction(action, null, null, false);
        }

        #region 多語言版本控制

        private WinFormLanguage lan = null;
        /// <summary>
        /// 多语言版本
        /// </summary>
        protected virtual WinFormLanguage Language
        {
            get
            {
                if (this.lan == null)
                {
                    this.lan = new WinFormLanguage(this);
                }
                return this.lan;
            }
        }

        /// <summary>
        /// 依据关键字获取相应的语言版本信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetMessage(string key)
        {
            return this.Language.GetMsg(key);
        }
        #endregion

        #region 弹出消息框

        /// <summary>
        /// 弹出错误消息框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void MessageError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 弹出错误消息框
        /// </summary>
        /// <param name="message">错误消息</param>
        public void MessageError(string message)
        {
            this.MessageError(message, GetMessage("Error"));
        }

        /// <summary>
        /// 弹出信息消息框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void MessageInformation(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 弹出信息消息框
        /// </summary>
        /// <param name="message"></param>
        public void MessageInformation(string message)
        {
            this.MessageInformation(message, GetMessage("Information"));
        }

        /// <summary>
        /// 弹出指定消息的警告消息框，并指定标题。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void MessageWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 弹出指定消息的警告消息框。
        /// </summary>
        /// <param name="message"></param>
        public void MessageWarning(string message)
        {
            this.MessageWarning(message, "Warning");
        }

        /// <summary>
        /// 弹出确认消息框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public DialogResult MessageConfirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 弹出确认消息框
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public DialogResult MessageConfirm(string message)
        {
            return this.MessageConfirm(message, "Confirmation");
        }
        #endregion
    }
}