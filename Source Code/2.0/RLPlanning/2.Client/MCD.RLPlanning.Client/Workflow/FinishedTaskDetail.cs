using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FinishedTaskDetail : TaskDetail
    {
        /// <summary>
        /// 完成时间与处理人
        /// </summary>
        public string m_strFinishTime = null;
        public string m_strFixUserName = null;

        public FinishedTaskDetail()
        {
            InitializeComponent();

            //Remark只读
            tbRemark.ReadOnly = true;
            //保存按钮隐藏
            btnSave.Visible = false;
        }

                /// <summary>
        /// 载入动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TaskDetail_Load(object sender, EventArgs e)
        {
            base.TaskDetail_Load(sender, e);

            lbFinishTime.Text = m_strFinishTime;
            lbFixUserName.Text = m_strFixUserName;
        }
    }
}
