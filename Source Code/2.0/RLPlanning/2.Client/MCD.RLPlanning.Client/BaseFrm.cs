using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Collections;
using System.Net.NetworkInformation;

using MCD.Controls;
using MCD.DockContainer.Docking;
using MCD.Common;
using MCD.Framework.AppCode;
using MCD.RLPlanning.Entity;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.UUPPopedomService;
using MCD.RLPlanning.Client.UUPGroupService;
using MCD.RLPlanning.Client.UUPModuleService;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 所有窗体的基类
    /// </summary>
    public partial class BaseFrm : DockContent
    {
        //Feidls
        private FrmMain mainForm = null;
        private string messageTitle = string.Empty;
        protected static string DATETIME_LONG_FORMAT = "yyyy/M/dd HH:mm:ss";
        protected static string DATETIME_SHORT_FORMAT = "yyyy/M/dd";

        //Properties
        /// <summary>
        /// 是否刷新窗体
        /// </summary>
        /// <returns></returns>
        public bool RefreshList { get; set; }
        /// <summary>
        /// 当前服务器的时间
        /// </summary>
        public DateTime CurrentServerTime
        {
            get
            {
                return this.LogBLL.CurrentServerTime();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public FrmMain MainForm
        {
            get
            {
                if (this.mainForm == null)
                {
                    int count = 0;
                    //
                    Control ctl = this.Parent;
                    while (ctl.GetType() != typeof(FrmMain))
                    {
                        ctl = ctl.Parent;
                        if (count > 100)
                        {
                            throw new Exception(string.Format("该窗体不加载在主窗体中，不能使用主窗体方法：{0}；BaseFrm.MainForm 属性。",
                                this.GetType().FullName));
                        }
                    }
                    this.mainForm = ctl as FrmMain;
                }
                return this.mainForm;
            }
        }
        /// <summary>
        /// 弹出对话框的标题
        /// </summary>
        public string MessageTitle
        {
            get
            {
                if (string.IsNullOrEmpty(this.messageTitle))
                {
                    this.messageTitle = this.GetMessage("Caption");
                }
                return this.messageTitle;
            }
        }
        #region 权限服务

        private GroupServiceClient groupService = null;
        private ModuleServiceClient moduleService = null;
        private PopedomServiceClient popedomService = null;

        /// <summary>
        /// 用户组服务对象
        /// </summary>
        protected GroupServiceClient GroupService
        {
            get
            {
                if (this.groupService == null || this.groupService.State == System.ServiceModel.CommunicationState.Closed)
                {
                    this.groupService = new GroupServiceClient();
                }
                return this.groupService;
            }
        }
        /// <summary>
        /// 模块信息服务对象
        /// </summary>
        protected ModuleServiceClient ModuleService
        {
            get
            {
                if (this.moduleService == null || this.moduleService.State == System.ServiceModel.CommunicationState.Closed)
                {
                    this.moduleService = new ModuleServiceClient();
                }
                return this.moduleService;
            }
        }
        /// <summary>
        /// 权限模块访问
        /// </summary>
        private PopedomServiceClient PopedomService
        {
            get
            {
                if (this.popedomService == null)
                {
                    this.popedomService = new PopedomServiceClient();
                }
                return this.popedomService;
            }
        }
        #endregion
        #region ctor

        public BaseFrm()
        {
            InitializeComponent();
            //
            this.Icon = Properties.Resources.LOGO2;
        }
        #endregion

        //Events
        private void BaseFrm_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                // 登录时，初始化语言版本
                if (this.GetType() == typeof(Login))
                {
                    this.InitialLanguage();
                }

                // 设置语言版本
                this.Language.SetLanguage();

                // 设置界面权限 
                this.SetFormRight(this);

                // 控件 TabIndex 排序
                this.SortControl((Control)this);
            }
        }
        /// <summary>
        /// 窗体关闭时，消亡对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ModuleService != null &&
                this.ModuleService.State != CommunicationState.Closing && this.ModuleService.State != CommunicationState.Closed)
            {
                try
                {
                    this.ModuleService.Close();
                }
                catch { this.ModuleService.Abort(); }
            }//
            if (this.GroupService != null &&
                this.GroupService.State != CommunicationState.Closing && this.GroupService.State != CommunicationState.Closed)
            {
                try
                {
                    this.GroupService.Close();
                }
                catch { this.GroupService.Abort(); }
            }//
            if (this.PopedomService != null &&
                this.PopedomService.State != CommunicationState.Closing &&  this.PopedomService.State != CommunicationState.Closed)
            {
                try
                {
                    this.PopedomService.Close();
                }
                catch { this.PopedomService.Abort(); }
            }
            this.LogBLL.Dispose();
        }

        //Methods
        private void SortControl(Control c)
        {
            ArrayList ctrls = new ArrayList(c.Controls);
            ctrls.Sort(ControlCompare.Instance);
            //
            Control ctrl;
            for (int i = 0, j = ctrls.Count; i < j; i++)
            {
                ctrl = ctrls[i] as Control;
                ctrl.TabIndex = i;
                if (ctrl.Controls.Count > 0)
                {
                    this.SortControl(ctrl);
                }
            }
        }
        /// <summary>
        /// 递归设置指定控件以及控件的所有子控件的启用状态。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="enabled"></param>
        protected void EnabledControl(Control ctrl, bool enabled)
        {
            if (ctrl is NumericUpDown)
            {
                if (!enabled)
                {
                    (ctrl as NumericUpDown).ReadOnly = true;
                    (ctrl as NumericUpDown).Increment = 0;
                }
            }
            if (ctrl is GroupBox || ctrl is Label || ctrl is Panel || ctrl is TabControl
                || ctrl is TabPage || ctrl is LinkLabel || ctrl is SplitContainer || ctrl is UserControl || ctrl is DataGridView)
            {
                ctrl.Enabled = true;
            }
            else if (ctrl is TextBox)
            {
                (ctrl as TextBox).ReadOnly = !enabled;
            }
            else if (ctrl is RichTextBox)
            {
                (ctrl as RichTextBox).ReadOnly = !enabled;
            }
            else
            {
                ctrl.Enabled = enabled;
            }
            //
            foreach (Control c in ctrl.Controls)
            {
                this.EnabledControl(c, enabled);
            }
        }
        /// <summary>
        /// 递归设置指定控件以及控件的所有子控件的启用状态。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="enabled"></param>
        protected void EnabledControl(Control ctrl, Func<Control, bool> controlFilter, bool enabled)
        {
            if (controlFilter(ctrl))
            {
                ctrl.Enabled = enabled;
            }
            //
            foreach (Control c in ctrl.Controls)
            {
                this.EnabledControl(c, controlFilter, enabled);
            }
        }
        /// <summary>
        /// 递归对指定的控件的所有子控件调用指定的委托。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="action"></param>
        protected void ForeachContrl(Control ctrl, Action<Control> action)
        {
            action(ctrl);
            //
            foreach (Control c in ctrl.Controls)
            {
                this.ForeachContrl(c, action);
            }
        }
        /// <summary>
        /// 刷新窗体方法
        /// </summary>
        public virtual void RefreshFrm() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blAlertMessage"></param>
        /// <returns></returns>
        protected void CheckOnLine(bool blAlertMessage)
        {
            var p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pr;
            pr = p.Send("152.141.153.223");//需要改成由服务地址转换到的ip
            if (pr.Status != IPStatus.Success)
            {
                if (blAlertMessage)
                {
                    this.MessageWarning("系统已断网", this.GetMessage("Caption"));
                }
            }
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
            return this.Language.GetMsg(key); //不做缓存,因为改变语言时,再点查询重新生成 Grid 时,未改变语言
        }

        /// <summary>
        /// 初始化语言版本
        /// </summary>
        protected virtual void InitialLanguage()
        {
            AppCode.SysEnvironment.CurrentLanguage = this.Language.LocalLanguage();
            //
            this.Language.ChangeLanguage(AppCode.SysEnvironment.CurrentLanguage);
        }

        #endregion

        #region 权限控制

        private DataTable tableUserModule = null;

        /// <summary>
        /// 获取当前用户所具备的模块权限
        /// </summary>
        private DataTable DTUserModule
        {
            get
            {
                if (this.tableUserModule == null)
                {
                    // 查询当前用户所在的用户组所具备的权限,由于统一权限管理没有实时关联本系统用户，所以只能以该方式访问
                    this.tableUserModule = this.PopedomService.GetGroupPopedom(AppCode.SysEnvironment.SystemCode, AppCode.SysEnvironment.CurrentUser.GroupID);
                    this.tableUserModule.PrimaryKey = new DataColumn[] { this.tableUserModule.Columns["ModuleCode"] };
                }
                return this.tableUserModule;
            }
        }

        /// <summary>
        /// 获取当前用户所具备的功能权限
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        private DataTable GetGroupFunction(string moduleCode)
        {
            DataTable tableUserFunction = this.PopedomService.GetGroupFormFunctionRight(AppCode.SysEnvironment.SystemCode, moduleCode, AppCode.SysEnvironment.CurrentUser.GroupID);
            tableUserFunction.PrimaryKey = new DataColumn[] { tableUserFunction.Columns["FunctionCode"] };
            //
            return tableUserFunction;
        }

        /// <summary>
        /// 设置窗体权限
        /// </summary>
        /// <param name="frm"></param>
        protected virtual void SetFormRight(Form frm)
        {
            if (AppCode.SysEnvironment.CurrentUser != null)
            {
                this.SetFormRight(frm, AppCode.SysEnvironment.SystemCode, AppCode.SysEnvironment.CurrentUser.UserName);
            }
        }
        /// <summary>
        /// 设置界面权限
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="systemCode"></param>
        /// <param name="userAccount"></param>
        protected virtual void SetFormRight(Form frm, string systemCode, string userAccount)
        {
            string moduleCode = frm.GetType().FullName;
            DataTable dtFunction = this.GetGroupFunction(moduleCode);
            //
            this.SetControlRight(dtFunction, (Control)frm);
        }

        /// <summary>
        /// 设置控件权限
        /// </summary>
        /// <param name="dtfuns"></param>
        /// <param name="parent"></param>
        private void SetControlRight(DataTable dtfuns, ToolStrip parent)
        {
            foreach (ToolStripItem item in ((ToolStrip)parent).Items)
            {
                if (item is ToolStripButton)
                {
                    if (dtfuns.Rows.Find(item.Name) == null)
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 设置控件权限
        /// </summary>
        /// <param name="dtfuns"></param>
        /// <param name="parent"></param>
        protected virtual void SetControlRight(DataTable dtfuns, Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is UCButton)
                {
                    if (((UCButton)ctrl).Popedom == false) continue;// 自定义控件，获取是否需要进行权限控制的按钮
                    if (dtfuns.Rows.Find(ctrl.Name) == null)
                    {
                        ctrl.Enabled = false;
                    }
                }
                else if (ctrl is Button || ctrl is LinkLabel)
                {
                    if (dtfuns.Rows.Find(ctrl.Name) == null)
                    {
                        ctrl.Enabled = false;
                    }
                }
                else if (ctrl.Controls.Count > 0)
                {
                    this.SetControlRight(dtfuns, ctrl);
                }
                else if (ctrl is ToolStrip)
                {
                    this.SetControlRight(dtfuns, (ToolStrip)ctrl);
                }
            }
        }

        /// <summary>
        /// 设置顶部菜单权限
        /// </summary>
        /// <param name="menu"></param>
        protected virtual void SetMenuRight(MenuStrip menu)
        {
            foreach (ToolStripMenuItem item in menu.Items)
            {
                bool hasRight = false;
                //
                this.SetMenuRight(item, ref hasRight);
                if (!hasRight)
                {
                    item.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 检测是否需要屏蔽父菜单
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hasRight">是否所有菜单都无权限访问，屏蔽父菜单</param>
        private void SetMenuRight(ToolStripMenuItem parent, ref bool hasRight)
        {
            foreach (ToolStripItem dropItem in parent.DropDownItems)
            {
                ToolStripMenuItem item = dropItem as ToolStripMenuItem;
                if (item == null) continue;
                //
                if (item.DropDownItems.Count > 0)
                {
                    // 有子菜单时，考虑子菜单情况
                    this.SetMenuRight(item, ref hasRight);
                }
                else
                {
                    // 根节点时，考虑当前菜单的情况
                    if (item.Tag + string.Empty == string.Empty)
                    {// 没有设置权限功能，默认为所有用户都可以访问  Form 为空
                        hasRight = true;
                    }
                    else if (this.DTUserModule.Rows.Find(item.Tag) != null)
                    {
                        // 用户有权限访问的菜单
                        hasRight = true;
                    }
                    else
                    {
                        // 用户无权限访问的菜单
                        item.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 设置导航栏权限 只适用于主窗口
        /// </summary>
        /// <param name="menu"></param>
        protected virtual void SetToolStripRight(ToolStrip bar)
        {
            foreach (ToolStripItem item in bar.Items)
            {
                if (item.Name == "btnExit" || item.Name == "btnCancel")
                {
                    item.Enabled = true;
                }
                else
                {
                    if (item is ToolStripButton)
                    {
                        if (this.DTUserModule.Rows.Find(item.Tag) != null)
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置左边菜单权限
        /// </summary>
        /// <param name="menu"></param>
        protected virtual void SetMenuRight(UCLeftMenu menu)
        {
            foreach (Control ctrl in menu.Controls)
            {
                if (ctrl is UCMenuButton)
                {
                    bool hasRight = false;
                    //
                    Control[] pnls = menu.Controls.Find(ctrl.Name.Replace("btn", "pnl"), false);// 存放菜单的容器
                    if (pnls != null && pnls.Count((_s) => { if (_s is Panel) return true; return false; }) > 0)
                    {
                        this.SetMenuRight(pnls[0] as Panel, ref hasRight);
                    }
                    if (!hasRight)
                    {
                        ctrl.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 检测是否需要屏蔽父菜单
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hasRight">是否所有菜单都无权限访问，屏蔽父菜单</param>
        private void SetMenuRight(Panel parent, ref bool hasRight)
        {
            foreach (Control ctrl in parent.Controls)
            {
                UCLeftMenuItem item = ctrl as UCLeftMenuItem;
                if (item == null) continue;

                // 根节点时，考虑当前菜单的情况
                if (item.Tag + string.Empty == string.Empty)
                {
                    // 没有设置权限功能，默认为所有用户都可以访问  Form 为空
                    hasRight = true;
                }
                else if (this.DTUserModule.Rows.Find(item.Tag) != null)
                {
                    // 用户有权限访问的菜单
                    hasRight = true;
                }
                else
                {
                    // 用户无权限访问的菜单
                    item.Enabled = false;
                }
            }
        }

        #endregion

        #region 错误日志

        private LogBLL m_LogBLL = null;

        /// <summary>
        /// 日志业务层对象
        /// </summary>
        private LogBLL LogBLL
        {
            get
            {
                if (this.m_LogBLL == null)
                {
                    this.m_LogBLL = new LogBLL();
                }
                return this.m_LogBLL;
            }
        }

        /// <summary>
        /// 执行匿名委托方法，弹出信息框
        /// </summary>
        /// <param name="func">匿名委托方法</param>
        /// <param name="logType">错误类型</param>
        /// <param name="logSource">日志记录源</param>
        /// <param name="errMessage">出错时弹出信息</param>
        /// <param name="successMessage">成功时弹出信息</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType, string logSource, string errMessage, string successMessage)
        {
            try
            {
                // 执行匿名委托方法
                if (func())
                {
                    // 弹出执行成功时的消息
                    if (successMessage != string.Empty)
                    {
                        MessageBox.Show(successMessage);
                    }
                    return true;
                }
                else
                {
                    // 弹出执行错误时的消息
                    if (errMessage != string.Empty)
                    {
                        MessageBox.Show(errMessage);
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.AddLog(logType, logSource, ex.Message, ex.ToString());
                // 弹出执行错误时的消息
                if (errMessage != string.Empty)
                {
                    MessageBox.Show(errMessage);
                }
                return false;
            }
        }

        /// <summary>
        /// 执行方法，返回是否执行成功
        /// </summary>
        /// <param name="func">执行方法</param>
        /// <param name="logType">错误类型</param>
        /// <param name="errMessage">出错时显示信息</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType, string logSource, string errMessage)
        {
            return this.ExecuteBoolean(func, logType, logSource, errMessage, string.Empty);
        }

        /// <summary>
        /// 执行方法，返回是否执行成功
        /// </summary>
        /// <param name="func">执行方法</param>
        /// <param name="logType">错误类型</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType, string logSource)
        {
            return this.ExecuteBoolean(func, logType, logSource, string.Empty, string.Empty);
        }

        /// <summary>
        /// 执行无返回值的方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="logType"></param>
        /// <returns></returns>
        public bool ExecuteAction(Action action, string logType, string logSource)
        {
            return this.ExecuteAction(action, logType, logSource, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="logType"></param>
        /// <param name="logSource"></param>
        /// <param name="showErrorMsg"></param>
        /// <returns></returns>
        public bool ExecuteAction(Action action, string logType, string logSource, bool showErrorMsg)
        {
            try
            {
                action();// 执行匿名委托方法
                return true;//The given URI must be absolute.
            }
            catch (Exception ex)
            {
                // 写入异常日志
                if (string.IsNullOrEmpty(logType))
                {
                    logType = "系统运行发生异常";
                }
                if (string.IsNullOrEmpty(logSource))
                {
                    logSource = ex.Source;
                }
                this.AddLog(logType, logSource, ex.Message, this.GetStackTrace(ex, 2000));
                if (showErrorMsg)
                {
                    this.MessageError(ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// 递归获取异常堆栈
        /// </summary>
        /// <param name="ex">异常</param>
        private string GetStackTrace(Exception ex)
        {
            StringBuilder sbStackTrace = new StringBuilder();
            //
            sbStackTrace.Append(ex.StackTrace);
            if (ex.InnerException != null)
            {
                sbStackTrace.Append(" --> ");
                sbStackTrace.Append(this.GetStackTrace(ex.InnerException));
            }
            return sbStackTrace.ToString();
        }

        /// <summary>
        /// 递归获取指定长度的异常堆栈信息。
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetStackTrace(Exception ex, int length)
        {
            string stackTrace = this.GetStackTrace(ex);
            if (stackTrace.Length > length)
            {
                return stackTrace.Substring(0, length);
            }
            return stackTrace;
        }

        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="logSource"></param>
        /// <param name="logTitle"></param>
        /// <param name="logMessage"></param>
        /// <returns></returns>
        public int AddLog(string logType, string logSource, string logTitle, string logMessage)
        {
            try
            {
                return this.LogBLL.InsertLog(new LogEntity() {
                    ID = Guid.NewGuid(),
                    LogTime = DateTime.Now,
                    LogTitle = logTitle,
                    LogMessage = logMessage,
                    LogType = logType,
                    UserID = SysEnvironment.CurrentUser.ID,
                    EnglishName = SysEnvironment.CurrentUser.EnglishName,
                    LogSource = logSource
                });
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Tab Panel 右键菜单操作

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCloseThis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 关闭其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCloseOther_Click(object sender, EventArgs e)
        {
            foreach (IDockContent content in this.DockPanel.DocumentsToArray())
            {
                if (content.DockHandler.IsActivated) continue;
                {
                    content.DockHandler.Close();
                }
            }
        }
        /// <summary>
        /// 关闭全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCloseAll_Click(object sender, EventArgs e)
        {
            foreach (IDockContent content in this.DockPanel.DocumentsToArray())
            {
                content.DockHandler.Close();
            }
        }

        #endregion

        #region 弹出消息框

        /// <summary>
        /// 弹出错误消息框
        /// </summary>
        /// <param name="message">错误消息</param>
        public void MessageError(string message)
        {
            this.MessageError(message, GetMessage("Error"));
        }
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
        /// 弹出信息消息框
        /// </summary>
        /// <param name="message"></param>
        public void MessageInformation(string message)
        {
            this.MessageInformation(message, GetMessage("Information"));
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
        /// 弹出指定消息的警告消息框。
        /// </summary>
        /// <param name="message"></param>
        public void MessageWarning(string message)
        {
            this.MessageWarning(message, "Warning");
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
        /// 弹出确认消息框
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public DialogResult MessageConfirm(string message)
        {
            return this.MessageConfirm(message, "Confirmation");
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

        #endregion

        #region 虚拟数据源

        /// <summary>
        /// 存放数据字典
        /// </summary>
        private Dictionary<string, DataTable> DictionaryTables = new Dictionary<string, DataTable>();

        /// <summary>
        /// 获取数据字典内容
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        protected DataTable GetDictionaryTable(string keyValue)
        {
            int type = 0;
            if (AppCode.SysEnvironment.CurrentLanguage == LANGUAGES.TraditionalChinese)
            {
                type = 1;
            }
            else if (AppCode.SysEnvironment.CurrentLanguage == LANGUAGES.English)
            {
                type = 2;
            }
            //
            if (!this.DictionaryTables.Keys.Contains(keyValue))
            {
                DataTable dt = this.LogBLL.SelectDictionary(keyValue, type).Tables[0];
                this.DictionaryTables.Add(keyValue, dt);
            }
            return this.DictionaryTables[keyValue];
        }

        /// <summary>
        /// 使用数据字典绑定 ComboBox 控件
        /// </summary>
        /// <param name="cbb"></param>
        /// <param name="keyValue">数据字典名称</param>
        protected void BindComboBoxFromDictionary(ComboBox cbb, string keyValue)
        {
            ControlHelper.BindComboBox(cbb, this.GetDictionaryTable(keyValue), "ItemName", "ItemValue");
        }

        private DataTable dtYesNo = null;
        /// <summary>
        /// 查找時顯示的數據源
        /// </summary>
        protected DataTable DTYesNo
        {
            get
            {
                if (this.dtYesNo == null)
                {
                    this.dtYesNo = new DataTable("YesNo");
                    this.dtYesNo.Columns.Add(new DataColumn("StatusValue", typeof(int)));
                    this.dtYesNo.Columns.Add(new DataColumn("StatusName", typeof(string)));
                    //
                    DataRow row = this.dtYesNo.NewRow();
                    row["StatusValue"] = 2;
                    row["StatusName"] = string.Empty;
                    this.dtYesNo.Rows.Add(row);

                    row = this.dtYesNo.NewRow();
                    row["StatusValue"] = 1;
                    row["StatusName"] = this.GetMessage("Yes");
                    this.dtYesNo.Rows.Add(row);

                    row = this.dtYesNo.NewRow();
                    row["StatusValue"] = 0;
                    row["StatusName"] = this.GetMessage("No");
                    this.dtYesNo.Rows.Add(row);
                }
                return this.dtYesNo;
            }
        }

        private DataTable dtYesNo1 = null;
        /// <summary>
        /// 查找時顯示的數據源
        /// </summary>
        protected DataTable DTYesNo1
        {
            get
            {
                if (this.dtYesNo1 == null)
                {
                    this.dtYesNo1 = this.DTYesNo.Copy();
                }
                return this.dtYesNo1;
            }
        }

        private DataTable dtStatus = null;
        /// <summary>
        /// 状态数据源
        /// </summary>
        protected DataTable DTStatus
        {
            get
            {
                if (this.dtStatus == null)
                {
                    this.dtStatus = new DataTable("Status");
                    this.dtStatus.Columns.Add(new DataColumn("StatusName", typeof(string)));
                    this.dtStatus.Columns.Add(new DataColumn("StatusValue", typeof(string)));
                    //
                    DataRow row = this.dtStatus.NewRow();
                    row["StatusName"] = string.Empty;
                    row["StatusValue"] = string.Empty;
                    this.dtStatus.Rows.Add(row);
                    //
                    row = this.dtStatus.NewRow();
                    row["StatusName"] = "A";
                    row["StatusValue"] = "A";
                    this.dtStatus.Rows.Add(row);
                    //
                    row = this.dtStatus.NewRow();
                    row["StatusName"] = "I";
                    row["StatusValue"] = "I";
                    this.dtStatus.Rows.Add(row);
                }
                return this.dtStatus;
            }
        }

        private DataTable dtDataFrom = null;
        /// <summary>
        /// 来源数据源
        /// </summary>
        protected DataTable DTDataFrom
        {
            get
            {
                if (this.dtDataFrom == null)
                {
                    this.dtDataFrom = new DataTable("Source");
                    this.dtDataFrom.Columns.Add(new DataColumn("SourceName", typeof(string)));
                    this.dtDataFrom.Columns.Add(new DataColumn("SourceValue", typeof(string)));
                    //
                    DataRow row = this.dtDataFrom.NewRow();
                    row["SourceName"] = string.Empty;
                    row["SourceValue"] = string.Empty;
                    this.dtDataFrom.Rows.Add(row);
                    //
                    row = this.dtDataFrom.NewRow();
                    row["SourceName"] = "租金预测系统";
                    row["SourceValue"] = "0";
                    this.dtDataFrom.Rows.Add(row);
                    //
                    row = this.dtDataFrom.NewRow();
                    row["SourceName"] = "租金计算系统";
                    row["SourceValue"] = "1";
                    this.dtDataFrom.Rows.Add(row);
                }
                return this.dtDataFrom;
            }
        }

        private DataTable dtDealyDays = null;
        /// <summary>
        /// 处理天数
        /// </summary>
        public DataTable DTDealDays
        {
            get
            {
                if (this.dtDealyDays == null)
                {
                    this.dtDealyDays = new DataTable("DealDays");
                    this.dtDealyDays.Columns.Add(new DataColumn("DayValue", typeof(int)));
                    this.dtDealyDays.Columns.Add(new DataColumn("DayName", typeof(string)));

                    DataRow row = this.dtDealyDays.NewRow();
                    row["DayValue"] = 0;
                    row["DayName"] = this.GetMessage("DealDays0");
                    this.dtDealyDays.Rows.Add(row);

                    row = this.dtDealyDays.NewRow();
                    row["DayValue"] = 1;
                    row["DayName"] = this.GetMessage("DealDays1");
                    this.dtDealyDays.Rows.Add(row);

                    row = this.dtDealyDays.NewRow();
                    row["DayValue"] = 2;
                    row["DayName"] = this.GetMessage("DealDays2");
                    this.dtDealyDays.Rows.Add(row);
                    
                    row = this.dtDealyDays.NewRow();
                    row["DayValue"] = 3;
                    row["DayName"] = this.GetMessage("DealDays3");
                    this.dtDealyDays.Rows.Add(row);
                }
                return this.dtDealyDays;
            }
        }

        /// <summary>
        /// 获取备注类数据源
        /// </summary>
        /// <param name="sourceID"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public DataTable GetRemarkTable(Guid sourceID, int sourceType)
        {
            return this.LogBLL.SelectRemarks(sourceID, sourceType).Tables[0];
        }

        /// <summary>
        /// 获取意见信息数据源
        /// </summary>
        /// <param name="remarkID"></param>
        /// <returns></returns>
        public DataTable GetOpinionTable(Guid remarkID, int type)
        {
            return this.LogBLL.SelectOpinion(remarkID, type).Tables[0];
        }

        /// <summary>
        /// 获取本地化数据集合
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string tableName, Func<DataTable> func, bool bFromData=false)
        {
            if (bFromData)
            {
                this.ExecuteAction(() =>
                {
                    if (AppCode.SysEnvironment.LocalTables.Keys.Contains(tableName))
                    {
                        AppCode.SysEnvironment.LocalTables[tableName] = func();
                    }
                    else
                    {
                        AppCode.SysEnvironment.LocalTables.Add(tableName, func());
                    }
                }, "获取{" + tableName + "}数据出错", "本地化数据处理");

            }
            else if (!AppCode.SysEnvironment.LocalTables.Keys.Contains(tableName))
            {
                this.ExecuteAction(() =>
                {
                    AppCode.SysEnvironment.LocalTables.Add(tableName, func());
                }, "获取{" + tableName + "}数据出错", "本地化数据处理");
            }
            return AppCode.SysEnvironment.LocalTables[tableName].Copy();
        }

        /// <summary>
        /// 移除本地化数据
        /// </summary>
        /// <param name="tableName"></param>
        public void RemoveDataTable(string tableName)
        {
            if (AppCode.SysEnvironment.LocalTables.Keys.Contains(tableName))
            {
                AppCode.SysEnvironment.LocalTables.Remove(tableName);
            }
        }

        #endregion
    }
}