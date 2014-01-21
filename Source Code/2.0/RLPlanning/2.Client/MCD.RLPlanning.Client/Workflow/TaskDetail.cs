using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Client;
using MCD.RLPlanning.BLL.Task;

namespace MCD.RLPlanning.Client.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TaskDetail : BaseEdit
    {
        public TaskDetail(Guid TaskID)
        {
            InitializeComponent();
            m_TaskID = TaskID;
            m_taskBLL = new TaskBLL();
        }

        public TaskDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 任务的GUID
        /// </summary>
        private Guid m_TaskID;

        /// <summary>
        /// 任务BLL
        /// </summary>
        private TaskBLL m_taskBLL;

        public string m_strStoreNo;
        public string m_strKioskNo;
        public string m_strTaskNo;
        public string m_strTaskType;
        public string m_strQuestType;
        public string m_strCreateTime;
        public string m_strRemark;
        public string m_strIsRead;

        public bool m_isRemarkChanged = false;

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != m_taskBLL)
                        m_taskBLL.UpdateTaskRemark(m_TaskID, tbRemark.Text);
                }, GetMessage("SaveTaskRemarkError"), GetMessage("SaveTaskRemark"));
            }, base.GetMessage("Wait"), () =>
            {
                this.m_taskBLL.CloseService();
            });
            frm.ShowDialog();

            Close();
            m_isRemarkChanged = true;
        }

        /// <summary>
        /// 载入动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void TaskDetail_Load(object sender, EventArgs e)
        {
            lbCreateTime.Text = m_strCreateTime;
            lbStoreNo.Text = m_strStoreNo;
            lbKioskNo.Text = m_strKioskNo;
            lbTaskNo.Text = m_strTaskNo;
            lbTaskType.Text = m_strTaskType;
            lbQuestType.Text = m_strQuestType;
            tbRemark.Text = m_strRemark;

            //如果未读，则需要更新是否读的标志
            if ("未读" == m_strIsRead)
            {
                FrmWait frm = new FrmWait(() =>
                {
                    ExecuteAction(() =>
                    {
                        if (null != m_taskBLL)
                            m_taskBLL.UpdateTaskIsRead(m_TaskID);
                    }, GetMessage("SaveTaskRemarkError"), GetMessage("SaveTaskRemark"));
                }, base.GetMessage("Wait"), () =>
                {
                    this.m_taskBLL.CloseService();
                });

                frm.ShowDialog();

                m_isRemarkChanged = true;
            }
        }

        private void TaskDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != m_taskBLL)
                m_taskBLL.Dispose();
        }
    }
}
