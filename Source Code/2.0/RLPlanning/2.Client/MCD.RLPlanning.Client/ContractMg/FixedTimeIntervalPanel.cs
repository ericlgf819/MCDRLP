using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.Client.Common;
using MCD.Common;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class FixedTimeIntervalPanel : BaseUserControl, IEntityEnabled
    {
        public FixedTimeIntervalPanel()
        {
            InitializeComponent();
        }

        #region 字段和属性声明

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public FixedTimeIntervalSettingEntity CurrentInterval { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public ContractBLL ContractBLL { get; set; }

        public event EventHandler IntervalDeleting;

        public event EventHandler GotFocused;

        public event EventHandler CycleChanged;

        public bool ShowDeleteButton 
        {
            get { return this.picDeleteTimeSegment.Visible; }
            set { this.picDeleteTimeSegment.Visible = value; }
        }

        public bool StartDateEnable
        {
            get { return this.dtpStartDate.Enabled; }
            set { this.dtpStartDate.Enabled = value; }
        }

        public bool EndDateEnable
        {
            get { return this.dtpEndDate.Enabled; }
            set { this.dtpEndDate.Enabled = value; }
        }

        #endregion 字段和属性声明

        #region 控件事件处理

        private void FixedRentTimeSegment_Load(object sender, EventArgs e)
        {
            if (!this.CurrentInterval.StartDate.HasValue)
            {
                this.CurrentInterval.StartDate = this.CurrentEntity.RentStartDate;
            }

            if (!this.CurrentInterval.EndDate.HasValue)
            {
                this.CurrentInterval.EndDate = this.CurrentEntity.RentEndDate;
            }

            this.bdsFixedInterval.DataSource = this.CurrentInterval;
            CheckRadioButtonStatus();
        }

        private void btnDeleteInterval_Click(object sender, EventArgs e)
        {
            if (this.MessageConfirm(base.GetMessage("NoticeDeleteConfirm")) == DialogResult.OK)
            {
                if (this.IntervalDeleting != null)
                {
                    this.IntervalDeleting(this, e);
                }
            }
        }

        private void innerControl_Enter(object sender, EventArgs e)
        {
            if (this.GotFocused != null)
            {
                this.GotFocused(this, e);
            }
        }

        private void radRentTime_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad.Checked)
            {
                if (rad == this.radSolarTime)
                {
                    this.CurrentInterval.Calendar = CalendarType.公历.ToString();
                }
                else
                {
                    this.CurrentInterval.Calendar = CalendarType.租赁.ToString();
                }
                this.CheckRadioButtonStatus();
            }
        }

        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbCycle.SelectedItem != null && this.CurrentInterval != null)
            {
                CycleItemEntity cycleItem = this.cmbCycle.SelectedItem as CycleItemEntity;
                if (cycleItem != null)
                {
                    this.CurrentInterval.CycleMonthCount = cycleItem.CycleMonthCount;
                }

                if (this.CycleChanged != null)
                {
                    this.CycleChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion 控件事件处理

        #region 公开方法

        public void RefreshStartEndDate()
        {
            if (this.CurrentInterval.StartDate.HasValue)
            {
                this.dtpStartDate.Value = this.CurrentInterval.StartDate.Value;
            }

            if (this.CurrentInterval.EndDate.HasValue)
            {
                this.dtpEndDate.Value = this.CurrentInterval.EndDate.Value;
            }
        }

        #endregion

        #region 私有方法

        private void CheckRadioButtonStatus()
        {
            this.radSolarTime.Checked = this.CurrentInterval.Calendar.Equals(CalendarType.公历.ToString());
            this.radRentTime.Checked = !this.radSolarTime.Checked;
        }

        #endregion 私有方法

        #region IEntityEnabled 成员

        public bool EntityEnabled
        {
            get
            {
                return this.CurrentInterval.Enabled;
            }
            set
            {
                this.CurrentInterval.Enabled = value;
            }
        }

        #endregion
    }
}
