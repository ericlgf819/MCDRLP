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
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class RatioCyclePanel : BaseUserControl, IEntityEnabled, IRentRule
    {
        public RatioCyclePanel()
        {
            InitializeComponent();
        }

        #region 字段和属性声明

        private const int c_PanelHeight = 110;
        private List<RatioTimeIntervalPanel> myIntervalPanelList = new List<RatioTimeIntervalPanel>();
        private ConditionAmountPanel m_ActiveConditionPanel;

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public RatioCycleSettingEntity CurrentCycleSetting { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public RatioRuleBLL RatioRuleBLL { get; set; }

        public event EventHandler HeightChanged;

        private bool m_IsLongCycle;
        /// <summary>
        /// 是否长周期，用于控制“返回已付百分比”按钮的可见性
        /// </summary>
        public bool IsLongCycle
        {
            get { return this.m_IsLongCycle; }
            set
            {
                this.m_IsLongCycle = value;
            }
        }

        private string m_RentType = "";
        /// <summary>
        /// 租金类型
        /// </summary>
        public string RentType
        {
            get { return this.m_RentType; }
            set
            {
                this.m_RentType = value;
            }
        }

        /// <summary>
        /// 设置标题信息
        /// </summary>
        private void SetTitle()
        {
            //这里暂时不实现多语言
            this.btnReturnPaidRatio.Visible = this.IsLongCycle;
            this.lblRatioPanelTitle.Text = string.Format("结算周期较{0}的{1}", this.IsLongCycle ? "长" : "短", this.RentType);
            //不再判断是租金、管理费还是服务费，统一使用租金
            //if (!string.IsNullOrEmpty(this.RentType) && this.RentType.Length > 3)
            //{
            //    string rentType = this.RentType.StartsWith("百分比") ? this.RentType.Substring(3) : this.RentType.Substring(2);
            //    this.btnReturnPaidRatio.Text = "返回已付百分比" + rentType;
            //    this.btnReturnPaidFixed.Text = "返回已付固定" + rentType;
            //}
        }

        private bool m_IsLoading = true;

        #endregion 字段和属性声明

        #region 控件事件处理

        private void PercentRentPanel_Load(object sender, EventArgs e)
        {
        }

        protected override void BindFormControl()
        {
            base.BindFormControl();
            this.SetTitle();
        }

        public void LoadData()
        {
            this.m_IsLoading = true;

            //绑定支付类型
            ControlHelper.BindComboBox(this.cmbPaymentType, this.GetDictionaryTable("RentPaymentType"), "ItemName", "ItemValue");
            //绑定结算周期
            using (ContractBLL contractBLL = new ContractBLL())
            {
                this.cmbCycle.DisplayMember = this.cmbCycle.ValueMember = "CycleItemName";
                this.cmbCycle.DataSource = contractBLL.GetCycleItems(CycleType.百分比).Where(item => true).ToList();
            }

            this.SetFirstDueDateRange();
            this.bdsRatioRentRule.DataSource = this.CurrentCycleSetting;
            //modified by Eric--Begin
            //this.dtpFirstDueDate.Value = this.CurrentCycleSetting.FirstDueDate.HasValue ? this.CurrentCycleSetting.FirstDueDate.Value : DateTime.Now;
            this.dtpFirstDueDate.Value = this.CurrentEntity.RentStartDate.AddMonths(1);
            this.CurrentCycleSetting.FirstDueDate = this.dtpFirstDueDate.Value;
            //modified by Eric--End

            if (this.cmbCycle.SelectedItem == null)
            {
                this.cmbCycle.SelectedIndex = 0;
            }

            this.CheckRadioButtonStatus();//检查租赁/公历状态

            this.pnlDateSegment.SuspendLayout();

            List<RatioTimeIntervalSettingEntity> intervalList =
                this.RentRuleAllInfo.GetRatioTimeIntervalSetting(this.CurrentCycleSetting.RuleSnapshotID, this.CurrentEntity);
            foreach (var interval in intervalList)
            {
                this.AddInterval(interval, false);
            }
            this.AdjustHeight();

            this.SetTitle();

            if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更)
            {
                if (this.CurrentEntity.IsEntityHasAP)
                {
                    this.chkIsPure.Enabled = false;
                    this.cmbPaymentType.Enabled = this.cmbCycle.Enabled = false;
                    this.radRentTime.Enabled = this.radSolarTime.Enabled = false;
                    this.dtpFirstDueDate.Enabled = this.dtpNextDueDate.Enabled = false;
                }
            }

            this.pnlDateSegment.ResumeLayout();

            this.m_IsLoading = false;
        }

        //取消界面上的首次DueDate限制
        private void SetFirstDueDateRange()
        {
            ////该字段有如下约束：大于“起租日”且大于当前日期且小于“租赁到期日”
            //DateTime now = DateTime.Now;
            //DateTime current = new DateTime(now.Year, now.Month, now.Day);
            //DateTime rentStartDate = this.CurrentEntity.RentStartDate;
            //DateTime rentEndDate = this.CurrentEntity.RentEndDate;
            //this.dtpFirstDueDate.MinDate = rentStartDate > current ? rentStartDate.AddDays(1) : current.AddDays(1);

            //this.dtpFirstDueDate.MaxDate = rentEndDate.AddDays(-1);
        }

        #region 填写公式的按钮操作

        private void btnCycleSales_Click(object sender, EventArgs e)
        {
            if (this.m_ActiveConditionPanel != null)
            {
                this.m_ActiveConditionPanel.AddJudgeCondition("CycleSales");
            }
        }

        private void btnGreatThan_Click(object sender, EventArgs e)
        {
            if (this.m_ActiveConditionPanel != null)
            {
                this.m_ActiveConditionPanel.AddJudgeCondition(">");
            }
        }

        private void btnLessThan_Click(object sender, EventArgs e)
        {
            if (this.m_ActiveConditionPanel != null)
            {
                this.m_ActiveConditionPanel.AddJudgeCondition("<");
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (this.m_ActiveConditionPanel != null)
            {
                this.m_ActiveConditionPanel.AddJudgeCondition("=");
            }
        }

        private void btnReturnPaidRatio_Click(object sender, EventArgs e)
        {
            this.m_ActiveConditionPanel.AddPayableCondition("PaidRatio");
        }

        private void btnReturnPaidFixed_Click(object sender, EventArgs e)
        {
            this.m_ActiveConditionPanel.AddPayableCondition("PaidFixed");
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.m_ActiveConditionPanel.AddPayableCondition("MAX{;}");
        }

        private void btnCycleSales2_Click(object sender, EventArgs e)
        {
            this.m_ActiveConditionPanel.AddPayableCondition("CycleSales");
        }

        #endregion

        private void btnAddDateSegment_Click(object sender, EventArgs e)
        {
            RatioTimeIntervalSettingEntity interval = new RatioTimeIntervalSettingEntity()
                {
                    RuleSnapshotID = this.CurrentCycleSetting.RuleSnapshotID,
                    TimeIntervalID = Guid.NewGuid().ToString(),
                    StartDate = this.CurrentEntity.RentStartDate,
                    EndDate = this.CurrentEntity.RentEndDate,
                };
            this.RentRuleAllInfo.RatioTimeIntervalSettingList.Add(interval);
            this.AddInterval(interval, true);
        }

        private void radRentTime_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad.Checked)
            {
                if (rad == this.radSolarTime)
                {
                    this.CurrentCycleSetting.Calendar = CalendarType.公历.ToString();
                }
                else
                {
                    this.CurrentCycleSetting.Calendar = CalendarType.租赁.ToString();
                }
                this.CheckRadioButtonStatus();
            }
        }

        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.m_IsLoading && this.cmbCycle.SelectedItem != null && this.CurrentCycleSetting != null)
            {
                CycleItemEntity cycleItem = this.cmbCycle.Items[this.cmbCycle.SelectedIndex] as CycleItemEntity;
                if (cycleItem != null)
                {
                    this.CurrentCycleSetting.Cycle = cycleItem.CycleItemName;
                    this.CurrentCycleSetting.CycleMonthCount = cycleItem.CycleMonthCount;
                    this.SetFirstDueDateRange();
                }
            }
        }

        private void intervalPanel_ActiveConditionPanelChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is RatioTimeIntervalPanel)
            {
                this.m_ActiveConditionPanel = (sender as RatioTimeIntervalPanel).ActiveConditionPanel;
            }
        }

        private void intervalPanel_HeightChanged(object sender, EventArgs e)
        {
            this.AdjustHeight(false);
        }

        private void intervalPanel_IntervalDeleting(object sender, EventArgs e)
        {
            RatioTimeIntervalPanel intervalPanel = sender as RatioTimeIntervalPanel;
            if (intervalPanel != null)
            {
                if (this.myIntervalPanelList.Count > 1)
                {
                    if (intervalPanel.ActiveConditionPanel == this.m_ActiveConditionPanel)
                    {
                        int index = this.myIntervalPanelList.IndexOf(intervalPanel);
                        if (intervalPanel == this.myIntervalPanelList.Last())
                        {
                            this.myIntervalPanelList[index - 1].ActiveConditionPanel.Focus();
                        }
                        else
                        {
                            this.myIntervalPanelList[index + 1].ActiveConditionPanel.Focus();
                        }
                    }

                    RatioTimeIntervalSettingEntity interval = intervalPanel.CurrentIntervalSetting;
                    interval.Enabled = false;
                    //删除下级conditions
                    List<ConditionAmountEntity> conditionList = this.RentRuleAllInfo.GetConditionAmount(interval.TimeIntervalID);
                    foreach (ConditionAmountEntity condition in conditionList)
                    {
                        condition.Enabled = false;
                    }

                    this.myIntervalPanelList.Remove(intervalPanel);
                    this.pnlDateSegment.Controls.Remove(intervalPanel);
                    this.AdjustHeight();
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeMustContainOneInterval"));
                }
            }
        }

        /// <summary>
        /// GL调账。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGLAdjustment_Click(object sender, EventArgs e)
        {
            GLAdjustmentEntity entity = new GLAdjustmentEntity();
            RatioRuleSettingEntity rule = this.RentRuleAllInfo.RatioRuleSettingList.FirstOrDefault(item => item.RatioID.Equals(this.CurrentCycleSetting.RatioID));
            if (rule != null)
            {
                entity.EntityInfoSettingID = rule.EntityInfoSettingID;
                entity.RentType = rule.RentType;
                entity.RuleSnapshotID = this.CurrentCycleSetting.RuleSnapshotID;
                entity.RuleID = this.CurrentCycleSetting.RuleID;

                GLAdjustment form = new GLAdjustment(entity);
                form.ShowDialog();
            }
        }

        #endregion 控件事件处理

        #region 私有方法

        private void CheckRadioButtonStatus()
        {
            this.radSolarTime.Checked = this.CurrentCycleSetting.Calendar.Equals(CalendarType.公历.ToString());
            this.radRentTime.Checked = !this.radSolarTime.Checked;
        }

        private void AddInterval(RatioTimeIntervalSettingEntity interval, bool shouldResize)
        {
            RatioTimeIntervalPanel intervalPanel = new RatioTimeIntervalPanel()
                {
                    RentRuleAllInfo = this.RentRuleAllInfo,
                    CurrentIntervalSetting = interval,
                    CurrentEntity = this.CurrentEntity,
                    RatioRuleBLL = this.RatioRuleBLL,
                };
            intervalPanel.IntervalDeleting += new EventHandler(intervalPanel_IntervalDeleting);
            intervalPanel.HeightChanged += new EventHandler(intervalPanel_HeightChanged);
            intervalPanel.ActiveConditionPanelChanged += new EventHandler(intervalPanel_ActiveConditionPanelChanged);
            this.m_ActiveConditionPanel = intervalPanel.ActiveConditionPanel;
            intervalPanel.LoadData();

            myIntervalPanelList.Add(intervalPanel);
            this.pnlDateSegment.Controls.Add(intervalPanel);
            if (shouldResize)
            {
                this.AdjustHeight();
            }
        }

        private void AdjustHeight(bool setFirstLast)
        {
            if (this.myIntervalPanelList.Count > 0)
            {
                int height = c_PanelHeight;
                foreach (var item in this.myIntervalPanelList)
                {
                    height += item.Height + item.Margin.Bottom;
                }
                this.Height = height + 6;

                this.myIntervalPanelList.ForEach(item =>
                    {
                        item.ShowDeleteButton = (this.myIntervalPanelList.Count > 1);
                        if (setFirstLast)
                        {
                            if (item == this.myIntervalPanelList.First())
                            {
                                item.StartDateEnable = false;
                                item.CurrentIntervalSetting.StartDate = this.CurrentEntity.RentStartDate;
                            }
                            else
                            {
                                item.StartDateEnable = true;
                            }
                            //item.StartDateEnable = (item == this.myIntervalPanelList.First() ? false : true);

                            if (item == this.myIntervalPanelList.Last())
                            {
                                item.EndDateEnable = false;
                                item.CurrentIntervalSetting.EndDate = this.CurrentEntity.RentEndDate;
                            }
                            else
                            {
                                item.EndDateEnable = true;
                            }
                        }
                        item.RefreshStartEndDate();
                        //item.EndDateEnable = (item == this.myIntervalPanelList.Last() ? false : true);
                    });

                //if (setFirstLast)
                //{
                //    //this.myIntervalPanelList.First().CurrentIntervalSetting.StartDate = this.CurrentEntity.RentStartDate;
                //    //this.myIntervalPanelList.Last().CurrentIntervalSetting.EndDate = this.CurrentEntity.RentEndDate;
                //    //this.myIntervalPanelList.First().StartDateEnable = false;
                //    //this.myIntervalPanelList.Last().EndDateEnable = false;
                //}

                if (this.HeightChanged != null)
                {
                    this.HeightChanged(this, EventArgs.Empty);
                }
            }
        }

        private void AdjustHeight()
        {
            this.AdjustHeight(true);
        }

        /// <summary>
        /// 存放数据字典
        /// </summary>
        private static Dictionary<string, DataTable> DictionaryTables = new Dictionary<string, DataTable>();
        /// <summary>
        /// 获取数据字典内容
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        protected DataTable GetDictionaryTable(string keyValue)
        {
            int type = 0;
            if (AppCode.SysEnvironment.CurrentLanguage == LANGUAGES.SimpleChinese)
            {
                type = 0;
            }
            else if (AppCode.SysEnvironment.CurrentLanguage == LANGUAGES.TraditionalChinese)
            {
                type = 1;
            }
            else if (AppCode.SysEnvironment.CurrentLanguage == LANGUAGES.English)
            {
                type = 2;
            }
            if (!DictionaryTables.Keys.Contains(keyValue))
            {
                using (LogBLL logBLL = new LogBLL())
                {
                    DataTable dt = logBLL.SelectDictionary(keyValue, type).Tables[0];
                    DictionaryTables.Add(keyValue, dt);
                }
            }
            return DictionaryTables[keyValue];
        }

        #endregion 私有方法

        #region IEntityEnabled 成员

        public bool EntityEnabled
        {
            get
            {
                return this.CurrentCycleSetting.Enabled;
            }
            set
            {
                this.CurrentCycleSetting.Enabled = value;
            }
        }

        #endregion

        #region IRentRule 成员

        public bool Disable()
        {
            this.CurrentCycleSetting.Enabled = false;
            return true;
        }

        public bool Enable()
        {
            this.CurrentCycleSetting.Enabled = true;
            return true;
        }

        public bool IsEnable
        {
            get
            {
                return this.CurrentCycleSetting.Enabled;
            }
            set
            {
                this.CurrentCycleSetting.Enabled = value;
            }
        }

        #endregion

    }
}
