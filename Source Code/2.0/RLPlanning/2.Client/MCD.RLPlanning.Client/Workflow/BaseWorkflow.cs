using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common.SRLS;
using MCD.RLPlanning.BLL.Workflow;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.Workflow;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 简单审批流程处理窗体基类，审批流程窗体可从此窗体继承。
    /// </summary>
    public partial class BaseWorkflow : BaseFrm
    {
        //Fields
        private ActionType currentState = ActionType.View;
        private WorkflowBizStatus workflowBizStatus = WorkflowBizStatus.已生效;
        private WorkflowType currentWorkflow = WorkflowType.NULL;
        private bool isNewWorkflow = false;
        private WorkflowBLL workflow = new WorkflowBLL();

        //Properties
        /// <summary>
        /// 获取或设置当前的流程实例ID。
        /// </summary>
        public string ProcID { get; set; }
        /// <summary>
        /// 获取或设置当前处理的待办任务名称。
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 获取或设置当前处理的待办任务ID。
        /// </summary>
        public string TaskID { get; set; }
        /// <summary>
        /// 获取或设置当前编辑的数据主键。
        /// </summary>
        public string DataKey { get; set; }
        /// <summary>
        /// 获取或设置当前待办任务的处理人信息。
        /// </summary>
        public string PartID { get; set; }
        /// <summary>
        /// 在业务列表中打开时，以获取或设置当前窗体的操作状态，默认为View。
        /// </summary>
        public ActionType CurrentAction
        {
            get { return this.currentState; }
            set { this.currentState = value; }
        }
        /// <summary>
        /// 在业务列表中打开时，以获取或设置当前业务数据的状态，默认为已生效。
        /// </summary>
        public WorkflowBizStatus WorkflowBizStatus
        {
            get { return this.workflowBizStatus; }
            set { this.workflowBizStatus = value; }
        }
        /// <summary>
        /// 获取或设置当前窗体运行的流程类型。
        /// </summary>
        public WorkflowType CurrentWorkflowType
        {
            get { return this.currentWorkflow; }
            set { this.currentWorkflow = value; }
        }
        /// <summary>
        /// 获取或设置当前操作是否要创建新的流程，默认为false。
        /// </summary>
        public bool IsNewWorkflow
        {
            get { return this.isNewWorkflow;}
            set { this.isNewWorkflow = value; }
        }
        /// <summary>
        /// 获取或设置父窗体。
        /// </summary>
        public Object ParentFrm { get; set; }
        #region ctor

        public BaseWorkflow()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 初始化窗体。
        /// </summary>
        private void InitForm()
        {
            //是否只读模式
            bool isView = false;
            if (!string.IsNullOrEmpty(this.ProcID))
            {
                //=====================================================当前窗体是在流程中打开==============================================

                //初始化工作流
                this.workflow.Init(this.ProcID, this.TaskID);
                if (this.workflow.CurrentTask == null || this.workflow.CurrentInstance == null)
                {
                    base.MessageError("流程或业务数据已被删除，无法打开！");
                    this.Close();
                    return;
                }
                this.PartID = this.workflow.CurrentTaskUserID;
                this.TaskName = this.workflow.CurrentTask.TaskName;
                this.DataKey = this.workflow.DataLocator;
                this.TaskID = Convert.ToString(this.workflow.CurrentTaskID);
                this.CurrentWorkflowType = (WorkflowType)Enum.Parse(typeof(WorkflowType), this.workflow.CurrentInstance.AppName);
                this.Text = string.Format("{0}流程-{1}", this.workflow.CurrentInstance.AppName, this.workflow.CurrentTask.TaskName);

                //若不是自己的待办或者是已办任务则只能查看
                if ((!string.IsNullOrEmpty(this.PartID) && this.PartID.ToUpper() != AppCode.SysEnvironment.CurrentUser.ID.ToString().ToUpper())
                    || this.workflow.CurrentInstance.CurrentActi.Contains("结束") || this.CurrentAction == ActionType.View)
                {
                    isView = true;

                    //若是在待办且登录用户为当前设置的管理员则显示转发任务按钮
                    //if (this.CurrentAction != ActionType.View)
                        this.btnForwardTo.Visible = AppCode.SysEnvironment.CurrentUser.IsAdminGroupUser;
                }
                else
                {
                    //处理待办
                    if (this.TaskName.Equals("审批") || this.TaskName.Equals("审核"))
                    {
                        //审批时不能修改业务数据
                        foreach (Control c in this.Controls)
                        {
                            this.EnabledControl(c, false);
                        }
                        //若当前步骤为审批则显示审批按钮
                        this.plApprove.Visible = this.btnPass.Visible = this.btnReject.Visible = this.btnCancel.Visible = true;
                        this.btnSaveData.Visible = this.btnSend.Visible = false;
                    }
                    else
                    {
                        //非审批步骤只显示发送暂存按钮
                        this.btnSend.Visible = this.btnSaveData.Visible = this.btnCancel.Visible = true;
                        this.plApprove.Visible = this.btnPass.Visible = this.btnReject.Visible = false;
                    }
                }
                //当前窗体只是运行流程
                this.IsNewWorkflow = false;
            }
            else
            {
                //============================================当前窗体是在业务列表数据中打开的==========================================

                //业务数据中不能转发任务
                this.btnForwardTo.Visible = false;

                //审核中、已失效或View状态的时候，只能查看
                if (this.CurrentAction == ActionType.View
                    || this.WorkflowBizStatus == WorkflowBizStatus.审核中 
                    || this.WorkflowBizStatus == WorkflowBizStatus.已失效)
                {
                    isView = true;
                }
                
                //View、Edit时读取流程信息
                //commented by Eric--Begin
                //if (this.CurrentAction != ActionType.New)
                //{
                //    this.workflow.Init(this.GetWorkflowType(this.CurrentAction), this.DataKey);
                //    if (!string.IsNullOrEmpty(this.workflow.ProcID))
                //    {
                //        this.ProcID = this.workflow.ProcID;
                //        this.PartID = this.workflow.CurrentTaskUserID;
                //        this.TaskName = this.workflow.CurrentTask.TaskName;
                //        this.DataKey = this.workflow.DataLocator;
                //        this.TaskID = Convert.ToString(this.workflow.CurrentTaskID);
                //        this.CurrentWorkflowType = (WorkflowType)Enum.Parse(typeof(WorkflowType), this.workflow.CurrentInstance.AppName);
                //        this.Text = string.Format("{0}流程-{1}", this.workflow.CurrentInstance.AppName, 
                //            this.workflow.CurrentTask.TaskName.Equals("结束") ? "已生效" : this.workflow.CurrentTask.TaskName);

                //        //流程未结束时，非流程的当前处理人无法修改
                //        if (!this.TaskName.Equals("结束") && !this.TaskName.Equals("强制结束") && !string.IsNullOrEmpty(this.PartID) && 
                //            this.PartID.ToUpper() != AppCode.SysEnvironment.CurrentUser.ID.ToString().ToUpper())
                //        {
                //            isView = true;
                //        }
                //    }
                //}
                //commented by Eric--End

                //可编辑时，只显示暂存与发送按钮
                if (!isView)
                {
                    this.btnSaveData.Visible = this.btnSend.Visible = true;
                    this.btnPass.Visible = this.btnReject.Visible = false;
                    this.plApprove.Visible = false;
                }

                //若新增数据或者修改已经生效的数据，则需要发起新的流程
                if (this.CurrentAction == ActionType.New || this.WorkflowBizStatus == WorkflowBizStatus.已生效)
                {
                    this.IsNewWorkflow = true;
                }
                else
                {
                    this.IsNewWorkflow = false;
                }
            }

            //只读模式，禁用所有控件
            if (isView)
            {
                foreach (Control c in this.Controls)
                {
                    this.EnabledControl(c, false);
                }

                //隐藏所有操作按钮，只显示取消按钮
                this.plApprove.Visible = this.btnSaveData.Visible = this.btnSend.Visible = this.btnPass.Visible = this.btnReject.Visible = false;
                this.btnCancel.Visible = true;
            }

            //流程处理按钮及审批意见面板总是非禁用
            this.EnabledControl(this.pnlBottom, true);
            this.lbViewHistory.Visible = !string.IsNullOrEmpty(this.ProcID);

            //若审批面板不可见则将窗体高度自动减小
            if (!this.plApprove.Visible)
            {
                this.Height -= this.plApprove.Height;
                Point p = this.Location;
                p.Y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                this.Location = p;
            }

            //为Plan系统修正按钮的隐藏与显示
            AlterUIForPlanSys(isView);
        }

        /// <summary>
        /// 窗体加载时初始化窗体控件数据。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseWorkflow_Load(object sender, EventArgs e)
        {
            //初始化窗体数据
            if (!base.DesignMode)
            {
                this.InitForm();
                this.BindFormControl(this.DataKey);
            }
        }

        /// <summary>
        /// 按钮事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            string dataKey = string.Empty;
            bool isRefresh = false;
            bool isClose = true;
            bool runWorkflow = false;

            WorkflowType flowType = this.GetWorkflowType(this.CurrentAction);

            switch ((sender as Button).Name)
            {
                //暂存
                case "btnSaveData":
                    {
                        if (!this.SaveData(SaveAction.TempSave, out dataKey, out runWorkflow))
                            return;

                        if (!runWorkflow)
                        {
                            MessageInformation(base.GetMessage("SaveDataSucc"));
                            return;
                        }

                        if (!this.IsNewWorkflow)
                        {
                            MessageInformation(base.GetMessage("SaveDataSucc"));
                            return;
                        }

                        this.ProcID = workflow.Create(flowType, null, dataKey, AppCode.SysEnvironment.CurrentUser.ID.ToString());
                        this.AfterRunWorkflow(WorkflowBizStatus.草稿, this.tbOpinion.Text, dataKey);

                        if (this.IsNewWorkflow)
                            this.IsNewWorkflow = false;
                        if (this.CurrentAction == ActionType.New)
                            this.CurrentAction = ActionType.Edit;

                        this.CurrentWorkflowType = flowType;
                        this.MessageInformation(base.GetMessage("SaveDataSucc"));
                        isRefresh = true;
                        isClose = false;
                    }
                    break;
                //创建或发送流程
                case "btnSend":
                    {
                        if (!this.SaveData(SaveAction.Send, out dataKey, out runWorkflow))
                            return;

                        if (!runWorkflow)
                        {
                            MessageInformation(base.GetMessage("SaveDataSucc"));
                            return;
                        }

                        if (this.IsNewWorkflow)
                        {
                            //modified by Eric
                            //移除工作流，原新建的任何工作流直接生效--Begin
                            //workflow.CreateAndRun(flowType, null, dataKey, AppCode.SysEnvironment.CurrentUser.ID.ToString(), null);
                            this.AfterRunWorkflow(WorkflowBizStatus.已生效, this.tbOpinion.Text, dataKey);
                            //移除工作流，原新建的任何工作流直接生效--End
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(this.ProcID))
                            {
                                MessageError("流程不存在或创建不成功，发送失败！");
                                return;
                            }
                            workflow.Run(this.ProcID, WorkflowUserChoice.NULL, null, null, AppCode.SysEnvironment.CurrentUser.ID.ToString(), null);
                            this.AfterRunWorkflow(WorkflowBizStatus.审核中, this.tbOpinion.Text, dataKey);
                        }
                        this.MessageInformation(base.GetMessage("SendSucc"));
                        isRefresh = true;
                    }
                    break;
                //通过
                case "btnPass":
                    {
                        if (MessageConfirm(base.GetMessage("ConfirmPass")) != DialogResult.OK)
                            return;

                        if (!this.SaveData(SaveAction.Pass, out dataKey, out runWorkflow))
                            return;

                        if (!runWorkflow)
                            return;

                        if (string.IsNullOrEmpty(this.ProcID))
                        {
                            MessageError("流程不存在或创建不成功，审批失败！");
                            return;
                        }
                        workflow.Run(this.ProcID, WorkflowUserChoice.通过, this.tbOpinion.Text, null, AppCode.SysEnvironment.CurrentUser.ID.ToString(), null);
                        this.AfterRunWorkflow(WorkflowBizStatus.已生效, this.tbOpinion.Text, dataKey);
                        this.MessageInformation(base.GetMessage("SendSucc"));
                        isRefresh = true;
                    }
                    break;
                //拒绝
                case "btnReject":
                    {
                        //检验是否填写审批意见
                        if (string.IsNullOrEmpty(this.tbOpinion.Text.Trim()))
                        {
                            this.ShowError(this.tbOpinion, base.GetMessage("EmptyOpinionMsg"));
                            return;
                        }
                        if (MessageConfirm(base.GetMessage("ConfirmReject")) != DialogResult.OK)
                            return;

                        if (!this.SaveData(SaveAction.Reject, out dataKey, out runWorkflow))
                            return;

                        if (!runWorkflow)
                            return;

                        if (string.IsNullOrEmpty(this.ProcID))
                        {
                            MessageError("流程不存在或创建不成功，审批失败！");
                            return;
                        }
                        workflow.Run(this.ProcID, WorkflowUserChoice.拒绝, this.tbOpinion.Text, string.Empty, AppCode.SysEnvironment.CurrentUser.ID.ToString(), null);
                        this.AfterRunWorkflow(WorkflowBizStatus.审核退回, this.tbOpinion.Text, dataKey);
                        this.MessageInformation(base.GetMessage("SendSucc"));
                        isRefresh = true;
                    }
                    break;
                //取消
                case "btnCancel":
                    {
                        isRefresh = false;
                    }
                    break;
            }
            //刷新父窗体
            if (isRefresh)
            {
                this.RefreshFrm();
            }
            //关闭窗体
            if (isClose)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 该窗体不受权限控制。
        /// </summary>
        /// <param name="frm"></param>
        protected override void SetFormRight(Form frm)
        { }

        /// <summary>
        /// 刷新父窗体。
        /// </summary>
        public override void RefreshFrm()
        {
            if (this.ParentFrm != null)
            {
                if (this.ParentFrm is BaseList)
                {
                    (this.ParentFrm as BaseList).RefreshFrm();
                }
                else if (this.ParentFrm is MCD.RLPlanning.Client.Workflow.Task.Controls.TSimpleCheck)
                {
                    (this.ParentFrm as MCD.RLPlanning.Client.Workflow.Task.Controls.TSimpleCheck).Refresh();
                }
            }
        }

        /// <summary>
        /// 查看流转过程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbViewHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.ProcID))
            //{
            //    MCD.RLPlanning.Client.Workflow.Task.TaskHistoryInfoFrm form = new MCD.RLPlanning.Client.Workflow.Task.TaskHistoryInfoFrm();
            //    form.ProcID = this.ProcID;
            //    form.ShowDialog();
            //}
        }

        /// <summary>
        /// 转发任务。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForwardTo_Click(object sender, EventArgs e)
        {
            //ForwardTask forward = new ForwardTask();
            //forward.ProcID = this.ProcID;
            //forward.TaskID = Convert.ToInt32(this.TaskID);
            //forward.ParentFrm = this;
            //forward.ShowDialog();
        }
        #endregion

        #region 公开方法

        /// <summary>
        /// 在派生类中重写以获取创建新流程时发起流程的类型。
        /// </summary>
        /// <param name="action">当前窗体动作</param>
        /// <returns></returns>
        protected virtual WorkflowType GetWorkflowType(ActionType action)
        {
            return WorkflowType.跟进事项;
        }

        /// <summary>
        /// 在派生类中重写以初始化窗体控件数据。
        /// </summary>
        /// <param name="dataKey">当前窗体显示数据的数据主键值</param>
        protected virtual void BindFormControl(string dataKey)
        {

        }

        /// <summary>
        /// 在派生类中重写以保存流程窗体的数据，并返回保存状态，当返回True的时候将会继续往下运行，返回False时直接终止执行。
        /// </summary>
        /// <param name="dataKey">返回当前数据主键</param>
        /// <param name="runWorkflow">指示保存数据完成后是否运行流程</param>
        /// <returns></returns>
        protected virtual bool SaveData(SaveAction action, out string dataKey, out bool runWorkflow)
        {
            runWorkflow = true;
            dataKey = string.Empty;
            //
            return true;
        }

        /// <summary>
        /// 在派生类中重写以定义流程运行成功之后执行的方法。
        /// </summary>
        /// <param name="status">当前业务数据的最新状态</param>
        /// <param name="opinion">审批意见</param>
        /// <param name="dataKey">当前业务数据主键</param>
        protected virtual void AfterRunWorkflow(WorkflowBizStatus status, string opinion, string dataKey)
        {

        }

        /// <summary>
        /// 在指定的控件旁边显示错误图标提示,并设置提示信息，若提示信息为空则不显示错误图标。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="message"></param>
        protected void ShowError(Control ctrl, string message)
        {
            this.MessageError(message);
        }
        #endregion

        #region 预测系统界面修正
        /// <summary>
        /// 为预测系统修正按钮的显示与隐藏
        /// </summary>
        /// <param name="bIsView"> true为只读模式 </param>
        void AlterUIForPlanSys(bool bIsView)
        {
            // 非只读模式只有提交和取消按钮可用
            if (!bIsView)
            {
                btnCancel.Visible = btnSend.Visible = true;
                btnPass.Visible = btnReject.Visible = btnForwardTo.Visible = btnSaveData.Visible = false;
            }
        }
        #endregion
    }

    /// <summary>
    /// 保存数据的操作枚举。
    /// </summary>
    public enum SaveAction
    {
        /// <summary>
        /// 暂存。
        /// </summary>
        TempSave,
        /// <summary>
        /// 发送。
        /// </summary>
        Send,
        /// <summary>
        /// 通过。
        /// </summary>
        Pass,
        /// <summary>
        /// 拒绝。
        /// </summary>
        Reject
    }
}