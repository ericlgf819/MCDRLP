using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Synchronization;
using MCD.RLPlanning.BLL.Synchronization;

namespace MCD.RLPlanning.Client.Synchronization
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ScheduleAdd : BaseEdit
    {
        //Fields
        private ScheduleEntity entity = null;
        private ScheduleBLL ScheduleBLL = new ScheduleBLL();
        #region ctor

        public ScheduleAdd()
        {
            InitializeComponent();
        }
        public ScheduleAdd(int ID)
        {
            this.entity = new ScheduleEntity() { 
                ID = ID
            };
            //
            InitializeComponent();
        }
        #endregion

        //Events
        protected void ScheduleAdd_Load(object sender, EventArgs e)
        {
            this.lblSyncTime.Text = AppCode.SysEnvironment.SynchronizateTime;
            //
            if (this.entity != null)
            {
                base.ExecuteAction(() =>
                {
                    FrmWait frmwait = new FrmWait(() =>
                    {
                        this.entity = this.ScheduleBLL.SelectSingleSchedule(this.entity);
                    }, base.GetMessage("Wait"), false);
                    frmwait.ShowDialog();
                }, "获取同步计划数据错误", "同步计划");
                //
                this.dtSycDate.Value = this.entity.SycDate;
                if (this.entity.CalculateEndDate != null) 
                {
                    this.chkIsCalculate.Checked = true;
                    this.dtCalculateEndDate.Value = this.entity.CalculateEndDate.Value;
                }
                this.txtRemark.Text = this.entity.Remark;
                //
                if (this.entity.Status == "未启动")
                {
                    this.dtSycDate.MinDate = DateTime.Now.AddDays(1);
                    this.dtCalculateEndDate.MinDate = this.dtSycDate.MinDate;
                    //
                    if (this.chkIsCalculate.Checked)
                    {
                        this.dtCalculateEndDate.Enabled = true;
                    }
                    //
                    this.HideSycContent();
                }
                else
                {
                    if (this.entity.Status == "同步异常")
                    {
                        this.lblStatusValue.ForeColor = Color.Red;
                    }
                    //
                    this.dtSycDate.Enabled = false;
                    this.chkIsCalculate.Enabled = false;
                    this.dtCalculateEndDate.Enabled = false;
                    this.txtRemark.Enabled = false;
                    //
                    this.lblAddedByValue.Text = this.entity.EnglishName;
                    this.lblAddedDateValue.Text = this.entity.LastModifiedDate.ToString();
                    this.lblStatusValue.Text = this.entity.Status;
                    this.txtSycDetail.Text = this.entity.SynDetail;
                    //
                    base.btnSave.Visible = false;
                    base.btnCancel.Location = new Point(base.btnCancel.Location.X - 40, base.btnCancel.Location.Y);
                }
            }
            else 
            {
                this.dtSycDate.MinDate = DateTime.Now.AddDays(1);
                this.dtCalculateEndDate.MinDate = this.dtSycDate.MinDate;
                //
                this.HideSycContent();
            }
        }
        protected void chkIsCalculate_CheckedChanged(object sender, EventArgs e)
        {
            this.dtCalculateEndDate.Enabled = (sender as CheckBox).Checked; 
        }
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            //检测
            if (this.dtCalculateEndDate.Enabled)
            {
                if ( this.dtSycDate.Value > this.dtCalculateEndDate.Value)
                {
                    base.MessageInformation(base.GetMessage("SycDateMoreThanCalculateEndDate"));
                    //
                    this.dtSycDate.Focus();
                    return;
                }
            }
            // 保存
            DateTime SycDate = this.dtSycDate.Value;
            DateTime? CalculateEndDate = null;
            if (this.chkIsCalculate.Checked)
            {
                CalculateEndDate = this.dtCalculateEndDate.Value;
            }
            string Remark = this.txtRemark.Text.Trim();
            //
            int res;
            if (this.entity == null)
            {
                res = this.ScheduleBLL.InsertSchedule(SycDate, CalculateEndDate, Remark, AppCode.SysEnvironment.CurrentUser.ID);
            }
            else
            {
                res = this.ScheduleBLL.UpdateSchedule(this.entity.ID, SycDate, CalculateEndDate, Remark, AppCode.SysEnvironment.CurrentUser.ID);
            }
            switch (res)
            {
                case 0:
                    base.MessageInformation(base.GetMessage("SaveSuccess"));
                    //
                    if (base.ParentFrm != null)
                    {
                        base.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                    break;
                case -1:
                    base.MessageInformation(string.Format(base.GetMessage("ExistSycDate"), this.dtSycDate.Value.ToString("yyyy-MM-dd")));
                    //
                    this.dtSycDate.Focus();
                    break;
                default:
                    base.MessageError(base.GetMessage("SaveFailure"));
                    break;
            }
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ScheduleBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        private void HideSycContent()
        {
            this.lblAddedBy.Visible = this.lblAddedByValue.Visible = false;
            this.lblAddedDate.Visible = this.lblAddedDateValue.Visible = false;
            this.lblStatus.Visible = this.lblStatusValue.Visible = false;
            this.lblSycDetail.Visible = this.txtSycDetail.Visible = false;
            //
            base.btnSave.Location = new Point(base.btnSave.Location.X, base.btnSave.Location.Y - 150);
            base.btnCancel.Location = new Point(base.btnCancel.Location.X, base.btnCancel.Location.Y - 150);
            this.Height -= 150;
        }
    }
}