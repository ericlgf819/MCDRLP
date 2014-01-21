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
using MCD.Common;
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class RatioRulePanel : BaseUserControl, IRentRule
    {
        public RatioRulePanel()
        {
            InitializeComponent();
        }

        #region 字段和属性声明

        private RatioCyclePanel m_ShortCyclePanel;
        private ToolPanel t_ShortTool;
        private RatioCyclePanel m_LongCyclePanel;
        private ToolPanel t_LongTool;

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public RatioRuleSettingEntity CurrentRule { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public RatioRuleBLL RatioRuleBLL { get; set; }

        #endregion 字段和属性声明

        #region 重写基类方法

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.Parent != null)
            {
                this.Parent.Height = this.Top + this.Height;
            }
        }

        #endregion 重写基类方法

        #region 控件事件处理

        private void RatioRentRulePanel_Load(object sender, EventArgs e)
        {
        }

        public void LoadData()
        {
            this.bdsRatioRule.DataSource = this.CurrentRule;
            this.AddPercentRentPanel();
        }

        void PercentRentPanel_HeightChanged(object sender, EventArgs e)
        {
            this.AdjustHeight();
        }

        #endregion 控件事件处理

        #region 私有方法

        private void AdjustHeight()
        {
            int height = this.pnlContainer.Top;
            if (this.m_ShortCyclePanel != null)
            {
                height += this.m_ShortCyclePanel.Height + this.m_ShortCyclePanel.Margin.Bottom;
            }
            if (this.m_LongCyclePanel != null)
            {
                height += this.m_LongCyclePanel.Height + this.m_LongCyclePanel.Margin.Bottom;
            }
            if (this.t_ShortTool != null)
            {
                height += this.t_ShortTool.Height + this.t_ShortTool.Margin.Bottom;
            }
            if (this.t_LongTool != null)
            {
                height += this.t_LongTool.Height + this.t_LongTool.Margin.Bottom;
            }
            this.Height = height;
        }

        private void AddPercentRentPanel()
        {
            this.pnlContainer.SuspendLayout();

            List<RatioCycleSettingEntity> cycleList = this.RentRuleAllInfo.GetRatioCycleSetting(this.CurrentRule.RatioID);

            RatioCycleSettingEntity longSetting = cycleList[0].CycleMonthCount > cycleList[1].CycleMonthCount ? cycleList[0] : cycleList[1];
            RatioCycleSettingEntity shortSetting = cycleList[0].CycleMonthCount > cycleList[1].CycleMonthCount ? cycleList[1] : cycleList[0];

            this.m_ShortCyclePanel = new RatioCyclePanel()
                {
                    RentRuleAllInfo = this.RentRuleAllInfo,
                    CurrentCycleSetting = shortSetting,
                    CurrentEntity = this.CurrentEntity,
                    RatioRuleBLL = this.RatioRuleBLL,
                    Width = this.pnlContainer.Width,
                    IsLongCycle = false,
                    RentType = this.CurrentRule.RentType,
                };
            this.m_ShortCyclePanel.LoadData();
            this.m_ShortCyclePanel.HeightChanged += new EventHandler(PercentRentPanel_HeightChanged);

            this.m_LongCyclePanel = new RatioCyclePanel()
                {
                    RentRuleAllInfo = this.RentRuleAllInfo,
                    CurrentCycleSetting = longSetting,
                    CurrentEntity = this.CurrentEntity,
                    RatioRuleBLL = this.RatioRuleBLL,
                    Width = this.pnlContainer.Width,
                    IsLongCycle = true,
                    RentType = this.CurrentRule.RentType,
                };
            this.m_LongCyclePanel.LoadData();
            this.m_LongCyclePanel.HeightChanged += new EventHandler(PercentRentPanel_HeightChanged);

            this.t_ShortTool = new ToolPanel()
            {
                RatioCycleControl = this.m_ShortCyclePanel
            };
            this.t_LongTool = new ToolPanel()
            {
                RatioCycleControl = this.m_LongCyclePanel
            };

            this.pnlContainer.Controls.Add(this.t_ShortTool);
            this.pnlContainer.Controls.Add(this.m_ShortCyclePanel);

            this.pnlContainer.Controls.Add(this.t_LongTool);
            this.pnlContainer.Controls.Add(this.m_LongCyclePanel);
            this.AdjustHeight();

            //fxh新增
            this.t_LongTool.InitStatus(longSetting.Enabled);
            this.t_ShortTool.InitStatus(shortSetting.Enabled);

            this.pnlContainer.ResumeLayout();
        }

        #endregion 私有方法

        #region IRentRule 成员

        /// <summary>
        /// 删除当前百分比规则。
        /// </summary>
        /// <returns></returns>
        public bool Disable()
        {
            this.Visible = false;
            this.CurrentRule.Enabled = false;
            ControlHelper.FindControl(this.pnlContainer, ctrl =>
            {
                return ctrl is IEntityEnabled;
            }).ForEach(item =>
            {
                (item as IEntityEnabled).EntityEnabled = false;
            });

            //this.RatioRuleBLL.DeleteSingleRatioRuleSetting(this.CurrentRule);

            return true;

        }

        /// <summary>
        /// 添加规则。
        /// </summary>
        /// <returns></returns>
        public bool Enable()
        {
            this.Visible = true;
            this.CurrentRule.Enabled = true;
            //this.RatioRuleBLL.InsertOrUpdateSingleRatioRuleSetting(this.CurrentRule);
            return true;
        }

        /// <summary>
        /// 获取或设置当前百分比规则的启用状态。
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.CurrentRule.Enabled;
            }
            set
            {
                this.CurrentRule.Enabled = value;
            }
        }

        #endregion}
    }
}
