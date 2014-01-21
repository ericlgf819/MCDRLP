using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL.ContractMg;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class RatioTimeIntervalPanel : BaseUserControl, IEntityEnabled, IRentRule
    {
        public RatioTimeIntervalPanel()
        {
            InitializeComponent();
        }

        #region 字段和属性声明

        private const int c_SegmentHeight = 28;
        private const int c_FormulaHeight = 28;

        private ConditionAmountPanel m_ActiveConditionPanel;

        public ConditionAmountPanel ActiveConditionPanel
        {
            get { return m_ActiveConditionPanel; }
            set { m_ActiveConditionPanel = value; }
        }

        private List<ConditionAmountPanel> myConditionPanelList = new List<ConditionAmountPanel>();

        public event EventHandler ActiveConditionPanelChanged;
        public event EventHandler IntervalDeleting;
        public event EventHandler HeightChanged;

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public RatioTimeIntervalSettingEntity CurrentIntervalSetting { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public RatioRuleBLL RatioRuleBLL { get; set; }

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


        public bool ShowDeleteButton
        {
            get { return this.picDeleteTimeSegment.Visible; }
            set { this.picDeleteTimeSegment.Visible = value; }
        }

        #endregion 字段和属性声明

        #region 控件事件处理

        private void PercentRentTimeSegment_Load(object sender, EventArgs e)
        {
            //this.LoadData();
        }

        public void LoadData()
        {
            //this.dtpStartDate.MinDate = this.CurrentEntity.RentStartDate;
            ////this.dtpStartDate.MaxDate = this.CurrentEntity.RentEndDate.AddDays(-1D);
            ////this.dtpEndDate.MinDate = this.CurrentEntity.RentStartDate.AddDays(1D);
            //this.dtpStartDate.MaxDate = this.CurrentEntity.RentEndDate;
            //this.dtpEndDate.MinDate = this.CurrentEntity.RentStartDate;
            //this.dtpEndDate.MaxDate = this.CurrentEntity.RentEndDate;

            this.pnlFormula.SuspendLayout();

            //填充下级
            List<ConditionAmountEntity> conditionList = this.RentRuleAllInfo.GetConditionAmount(this.CurrentIntervalSetting.TimeIntervalID);
            foreach (var item in conditionList)
            {
                this.AddNewFormulaPanel(item, false);
            }
            this.AdjustHeight();

            this.bdsRatioInterval.DataSource = this.CurrentIntervalSetting;

            this.pnlFormula.ResumeLayout();
        }

        private void innerControl_Enter(object sender, EventArgs e)
        {
            //if (this.FocusedFormulaPanelChanged != null)
            //{
            //    this.FocusedFormulaPanelChanged(this, e);
            //}
        }

        void conditionPanel_ConditionDeleting(object sender, EventArgs e)
        {
            ConditionAmountPanel conditionPanel = sender as ConditionAmountPanel;
            if (conditionPanel != null)
            {
                if (this.myConditionPanelList.Count > 1)
                {
                    if (conditionPanel == this.m_ActiveConditionPanel)
                    {
                        int index = this.myConditionPanelList.IndexOf(conditionPanel);

                        if (conditionPanel == this.myConditionPanelList.Last())
                        {
                            this.m_ActiveConditionPanel = this.myConditionPanelList[index - 1];
                            this.myConditionPanelList[index - 1].Focus();
                        }
                        else
                        {
                            this.m_ActiveConditionPanel = this.myConditionPanelList[index + 1];
                            this.myConditionPanelList[index + 1].Focus();
                        }
                    }

                    conditionPanel.CurrentConditionAmount.Enabled = false;
                    //this.RentRuleAllInfo.ConditionAmountList.Remove(conditionPanel.CurrentConditionAmount);
                    //this.RatioRuleBLL.DeleteSingleConditionAmount(conditionPanel.CurrentConditionAmount);
                    this.pnlFormula.Controls.Remove(conditionPanel);
                    this.myConditionPanelList.Remove(conditionPanel);
                    this.CheckAddJudgeButtonVisible();
                    this.AdjustHeight();
                    this.conditionPanel_GotFocus(this.m_ActiveConditionPanel, EventArgs.Empty);
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeMustContainOneJudge"));
                }
            }
        }

        private void conditionPanel_ConditionAdding(object sender, EventArgs e)
        {
            ConditionAmountEntity condition = new ConditionAmountEntity()
                {
                    ConditionID = Guid.NewGuid().ToString(),
                    TimeIntervalID = this.CurrentIntervalSetting.TimeIntervalID,
                    Enabled = true,
                };
            this.RentRuleAllInfo.ConditionAmountList.Add(condition);
            //this.RatioRuleBLL.InsertOrUpdateSingleConditionAmount(condition);
            this.AddNewFormulaPanel(condition, true);
        }

        private void conditionPanel_GotFocus(object sender, EventArgs e)
        {
            ConditionAmountPanel panel = sender as ConditionAmountPanel;
            if (panel != null)
            {
                this.m_ActiveConditionPanel = panel;
                if (this.ActiveConditionPanelChanged != null)
                {
                    this.ActiveConditionPanelChanged(this, e);
                }
                this.innerControl_Enter(panel, e);
            }
        }

        private void btnDeleteTimeSegment_Click(object sender, EventArgs e)
        {
            if (this.MessageConfirm(base.GetMessage("NoticeDeleteConfirm")) == DialogResult.OK)
            {
                if (this.IntervalDeleting != null)
                {
                    this.IntervalDeleting(this, e);
                }
            }
        }

        #endregion 控件事件处理

        #region 公开方法

        public void RefreshStartEndDate()
        {
            this.dtpStartDate.Value = this.CurrentIntervalSetting.StartDate;
            this.dtpEndDate.Value = this.CurrentIntervalSetting.EndDate;
        }

        #endregion


        #region 私有方法

        private void AddNewFormulaPanel(ConditionAmountEntity condition, bool shouldResize)
        {
            ConditionAmountPanel conditionPanel = new ConditionAmountPanel()
                {
                    RentRuleAllInfo = this.RentRuleAllInfo,
                    CurrentConditionAmount = condition,
                };
            conditionPanel.Margin = new Padding(0);
            this.pnlFormula.Controls.Add(conditionPanel);
            this.myConditionPanelList.Add(conditionPanel);
            this.m_ActiveConditionPanel = conditionPanel;
            this.m_ActiveConditionPanel.Focus();
            this.conditionPanel_GotFocus(this.m_ActiveConditionPanel, EventArgs.Empty);

            conditionPanel.ConditionAdding += new EventHandler(conditionPanel_ConditionAdding);
            conditionPanel.ConditionDeleting += new EventHandler(conditionPanel_ConditionDeleting);
            conditionPanel.GotFocus += new EventHandler(conditionPanel_GotFocus);

            this.CheckAddJudgeButtonVisible();

            if (shouldResize)
            {
                this.AdjustHeight();
            }
        }

        private void CheckAddJudgeButtonVisible()
        {
            if (myConditionPanelList.Count > 0)
            {
                myConditionPanelList.ForEach(item => item.ShowAddButton = false);
                if (myConditionPanelList.Count == 1)
                {
                    myConditionPanelList.First().ShowAddButton = true;
                }
                else
                {
                    myConditionPanelList.Last().ShowAddButton = true;
                }
            }
        }


        private void AdjustHeight()
        {
            this.Height = c_SegmentHeight + (this.myConditionPanelList.Count - 1) * c_FormulaHeight + 5;//调整高度
            this.myConditionPanelList.ForEach(item => item.ShowDeleteButton = (this.myConditionPanelList.Count > 1));
            if (this.HeightChanged != null)
            {
                this.HeightChanged(this, EventArgs.Empty);
            }
        }

        #endregion 私有方法

        #region IEntityEnabled 成员

        public bool EntityEnabled
        {
            get
            {
                return this.CurrentIntervalSetting.Enabled;
            }
            set
            {
                this.CurrentIntervalSetting.Enabled = value;
            }
        }

        #endregion

        #region IRentRule 成员

        public bool Disable()
        {
            this.CurrentIntervalSetting.Enabled = false;
            this.RatioRuleBLL.DeleteSingleRatioTimeIntervalSetting(this.CurrentIntervalSetting);
            return true;
        }

        public bool Enable()
        {
            this.CurrentIntervalSetting.Enabled = true;
            this.RatioRuleBLL.InsertOrUpdateSingleRatioTimeIntervalSetting(this.CurrentIntervalSetting);
            return true;
        }

        public bool IsEnable
        {
            get
            {
                return this.CurrentIntervalSetting.Enabled;
            }
            set
            {
                this.CurrentIntervalSetting.Enabled = value;
            }
        }

        #endregion

    }
}
