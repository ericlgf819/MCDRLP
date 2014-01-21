using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using MCD.Controls;
using MCD.Common;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.ContractMg;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RentRulePanel : BaseUserControl
    {
        #region ctor

        public RentRulePanel()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段和属性声明

        private bool m_ReadOnly = false;
        public bool ReadOnly
        {
            get
            {
                return m_ReadOnly;
            }
            set
            {
                m_ReadOnly = value;
                this.grpEntityInfo.Enabled = !m_ReadOnly;
            }
        }

        public string VendorNo { get; set; }

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public ContractBLL ContractBLL { get; set; }

        public FixedRuleBLL FixedRuleBLL { get; set; }

        public RatioRuleBLL RatioRuleBLL { get; set; }

        public EntityBLL EntityBLL { get; set; }

        /// <summary>
        /// 是否合同变更，如果合同变更，则首次DueDate不能修改
        /// </summary>
        public bool IsContractChange { get; set; }

        #endregion 字段和属性声明

        #region 控件事件处理

        private bool m_IsLoading = true;
        private void RentRulePanel_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                //this.SuspendLayout();
                this.FillEntityComboBox();

                this.FindRulePanel(this);
                this.SetToolTip(this);
                this.EntityNameChange();
                //权限设置
                //(this.FindForm() as ContractEdit).CheckRight();
                this.m_IsLoading = false;

                //无法实现将TextBox设置为ReadOnly和将Label设置为Enable
                //this.SetReadOnlyThreadEnter();

                //this.ResumeLayout();
            }

            if (AppCode.SysEnvironment.CurrentContractStatus == "已生效" && AppCode.SysEnvironment.CurrentContractSnapshotCreateTime == null)
            {
                this.btnAddGLAdjustment.Enabled = true;
            }
            else
            {
                this.btnAddGLAdjustment.Enabled = false;
            }
        }

        private void RulePanel_SizeChanged(object sender, EventArgs e)
        {
            this.SetToolTip(this);
        }

        private void ratioRentRulePanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            this.SetToolTip(this);
        }

        /// <summary>
        /// 启用、禁用规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colpanel_LinkClick(object sender, EventArgs e)
        {
            CollapsiblePanel colpanel = sender as CollapsiblePanel;
            if (colpanel.LinkButtonText == "已启用")
            {
                //禁用规则
                if (colpanel.Tag != null && colpanel.Tag is IRentRule)
                {
                    (colpanel.Tag as IRentRule).Disable();
                    colpanel.LinkButtonImage = Properties.Resources.已禁用;
                    colpanel.LinkButtonText = "已禁用";
                }
            }
            else
            {
                //启用规则
                if (colpanel.Tag != null && colpanel.Tag is IRentRule)
                {
                    (colpanel.Tag as IRentRule).Enable();
                    colpanel.LinkButtonImage = Properties.Resources.已启用;
                    colpanel.LinkButtonText = "已启用";
                }
            }
        }

        private void cmbEntityName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (m_EntityIndex != this.cmbEntityName.SelectedIndex)
            {
                this.m_EntityIndex = this.cmbEntityName.SelectedIndex;
                if (!this.m_IsLoading)
                {
                    EntityNameChange();
                }
            }

            //权限设置
            //(this.FindForm() as ContractEdit).CheckRight();
        }

        private int m_EntityIndex = -1;
        private void cmbEntityName_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        #endregion 控件事件处理

        #region 私有方法

        private void EntityNameChange()
        {
            // 启动新的线程完成动作
            ThreadStart start = new ThreadStart(this.ProcessWithThread);
            Thread thread = new Thread(start);
            thread.Start();
        }

        private void ProcessWithThread()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NormalDelegate(LoadSync), new object[] { });
            }
            else
            {
                this.LoadSync();
            }
        }

        private delegate void NormalDelegate();
        private void LoadSync()
        {
            this.CurrentEntity = this.cmbEntityName.SelectedItem as EntityEntity;
            //当前实体是否已计算AP
            this.CurrentEntity.IsEntityHasAP = ContractBLL.IsEntityHasAP(this.CurrentEntity.EntityID);

            this.bdsEntity.DataSource = this.CurrentEntity;
            EntityInfoSettingEntity entityInfoSetting = this.RentRuleAllInfo.GetEntityInfoSetting(this.VendorNo, this.CurrentEntity.EntityID);
            this.bdsEntityInfoSetting.DataSource = entityInfoSetting;
            //加载当前项
            this.pnlRentPanelContainer.Controls.Clear();
            //根据租金类型添加租金控件
            List<string> rentTypeList = new List<string>();

            rentTypeList = this.EntityBLL.SelectRentTypeByEntityTypeName(this.CurrentEntity.EntityTypeName);

            List<Control> rulePanelList = new List<Control>();
            rulePanelList.AddRange(this.LoadFixedRentRulePanel(entityInfoSetting.EntityInfoSettingID, this.CurrentEntity.EntityTypeName, rentTypeList));
            rulePanelList.AddRange(this.LoadRatioRentRulePanel(entityInfoSetting.EntityInfoSettingID, this.CurrentEntity.EntityTypeName, rentTypeList));

            //控制内部规则控件是否可以编辑，要求滚动条可用，折叠可用，其他控件禁用
            if (this.ReadOnly)
            {
                this.pnlRentPanelContainer.Enabled = true;
                rulePanelList.ForEach(panel =>
                    {
                        CollapsiblePanel p = panel as CollapsiblePanel;
                        if (p != null)
                        {
                            ControlHelper.EnabledControl(p, false);

                            Control[] headers = p.Controls.Find("pnlHeader", false);
                            foreach (Control header in headers)
                            {
                                header.Enabled = true;
                            }
                            //启用/禁用规则按钮也不可用
                            p.Controls.Find("toollbl", true).ToList().ForEach(item => { item.Enabled = false; });
                        }
                    });
            }


            //默认折叠规则
            rulePanelList.ForEach(panel =>
                {
                    CollapsiblePanel p = panel as CollapsiblePanel;
                    if (p != null)
                    {
                        p.Collapse = true;
                        if (p.Tag != null && p.Tag is IRentRule)
                        {
                            IRentRule rule = p.Tag as IRentRule;
                            if (!rule.IsEnable)
                            {
                                p.LinkButtonImage = Properties.Resources.已禁用;
                                p.LinkButtonText = "已禁用";
                                rule.Disable();
                            }
                        }

                        //SetTextBoxAndLabelStatus(p);
                        p.LinkClick += new EventHandler(colpanel_LinkClick);
                    }
                });

            this.pnlRentPanelContainer.SuspendLayout();

            this.pnlRentPanelContainer.Controls.AddRange(rulePanelList.ToArray());

            if (this.IsContractChange)
            {
                this.Controls.Find("dtpFirstDueDate", true).ToList().ForEach(item => item.Enabled = false);
            }

            //变更时，可以禁用变成启用, 不可从启用变成禁用
            if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更)
            {
                //当实体已出AP时，将启用禁用按钮设置为不可用
                if (this.CurrentEntity.IsEntityHasAP)
                {
                    this.pnlRentPanelContainer.Controls.Find("toollbl", true).ToList().ForEach(item => item.Enabled = false);
                }
                else
                {
                    this.Controls.Find("toollbl", true).ToList().ForEach(item =>
                        {
                            if (item.Text == "已启用")
                            {
                                item.Enabled = false;
                            }
                        });
                }
            }

            //Added by Eric -- Begin
            BugFixForResumeContract(rulePanelList);
            //Added by Eric -- End

            this.pnlRentPanelContainer.ResumeLayout();
        }

        /// <summary>
        /// 修正续租合同时，如果起租时间大于原来的租赁到期日，导致无法提交合同的bug
        /// </summary>
        private void BugFixForResumeContract(List<Control> rulePanelList)
        {
            //续租并且因为新的租赁起始时间与原来的时间无交集才会有这个bug, PS.变更也有一样
            //无交集的时间段，会导致原来Rule对应的TimeInterval全部被删光，导致无法提交合同
            if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.续租 ||
                AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更)
            {
                if (AppCode.SysEnvironment.s_mapIsLastEntityRentDateOverlap.ContainsKey(CurrentEntity.EntityID) &&
                    AppCode.SysEnvironment.s_mapIsLastEntityRentDateOverlap[CurrentEntity.EntityID])
                {
                    AppCode.SysEnvironment.s_bugFixed = true;
                    //将启用的规则先禁用，再启用
                    rulePanelList.ForEach(panel =>
                    {
                        CollapsiblePanel p = panel as CollapsiblePanel;
                        if (p != null)
                        {
                            if (p.Tag != null && p.Tag is IRentRule)
                            {
                                IRentRule rule = p.Tag as IRentRule;

                                //如果是合同变更，并且不是固定系列租金，则不需要进行下面的操作来修正bug
                                if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更 &&
                                    !p.Title.Contains("固定"))
                                {
                                    return;
                                }

                                if (rule.IsEnable)
                                {
                                    rule.Disable();
                                    rule.Enable();
                                }
                            }
                        }
                    });
                }
            }
        }

        private void SetReadOnlyThreadEnter()
        {
            ThreadStart start = new ThreadStart(this.SetReadOnlyStatusThread);
            Thread thread = new Thread(start);
            thread.Start();
        }

        private void SetReadOnlyStatusThread()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetReadOnlyDelegate(SetTextBoxAndLabelStatus), new object[] { this });
            }
            else
            {
                this.SetTextBoxAndLabelStatus(this);
            }
        }

        private delegate void SetReadOnlyDelegate(Control parent);
        /// <summary>
        /// 设置文本框为只读，设置Label为可用
        /// </summary>
        /// <param name="parent"></param>
        public void SetTextBoxAndLabelStatus(Control parent)
        {
            if (parent != null)
            {
                foreach (Control child in parent.Controls)
                {
                    if (child is Label)
                    {
                        (child as Label).Enabled = true;
                    }
                    else if (child is TextBox)
                    {
                        (child as TextBox).Enabled = (child as TextBox).ReadOnly = true;
                    }
                    else if (child is RichTextBox)
                    {
                        (child as RichTextBox).Enabled = (child as RichTextBox).ReadOnly = true;
                    }
                    else if (child.Controls.Count > 0)
                    {
                        SetTextBoxAndLabelStatus(child);
                    }
                }
            }
        }

        /// <summary>
        /// 填充实体下拉框
        /// </summary>
        private void FillEntityComboBox()
        {
            this.cmbEntityName.DisplayMember = "EntityName";
            this.cmbEntityName.ValueMember = "EntityID";
            this.cmbEntityName.DataSource = this.RentRuleAllInfo.GetEntitiesByVendorNo(this.VendorNo);
        }

        private List<Control> LoadFixedRentRulePanel(string entityInfoSettingID, string entityTypeName, List<string> rentTypeList)
        {
            List<Control> rulePanelList = new List<Control>();

            var fixedRuleList = this.RentRuleAllInfo.GetFixedRuleSetting(entityInfoSettingID, entityTypeName, rentTypeList);
            fixedRuleList.Sort((a, b) => { return string.Compare(a.RentType, b.RentType); });
            foreach (var rule in fixedRuleList)
            {
                CollapsiblePanel panel = new CollapsiblePanel()
                    {
                        Width = 890,//this.pnlRentPanelContainer.Width,
                        //Dock = DockStyle.Top,
                        BodyColor = Color.White,
                        Title = rule.RentType,
                        ShowBodyBorder = true,
                        BorderColor = Color.Black,
                        LinkButtonImage = Properties.Resources.已启用
                    };
                FixedRulePanel rulePanel = new FixedRulePanel()
                    {
                        RentRuleAllInfo = this.RentRuleAllInfo,
                        CurrentRule = rule,
                        CurrentEntity = this.CurrentEntity,
                        FixedRuleBLL = this.FixedRuleBLL,
                        ContractBLL = this.ContractBLL,
                    };
                panel.Tag = rulePanel;//fxh新增
                rulePanel.Location = new Point(2, 32);
                panel.Controls.Add(rulePanel);
                rulePanel.LoadData();
                //this.pnlRentPanelContainer.Controls.Add(rulePanel);
                rulePanelList.Add(panel);
            }

            return rulePanelList;
        }

        private List<Control> LoadRatioRentRulePanel(string entityInfoSettingID, string entityTypeName, List<string> rentTypeList)
        {
            List<Control> rulePanelList = new List<Control>();

            var ratioRuleList = this.RentRuleAllInfo.GetRatioRuleSetting(entityInfoSettingID, entityTypeName, rentTypeList);
            ratioRuleList.Sort((a, b) => { return string.Compare(a.RentType, b.RentType); });
            foreach (var rule in ratioRuleList)
            {
                CollapsiblePanel panel = new CollapsiblePanel()
                    {
                        Width = 890,//this.pnlRentPanelContainer.Width,
                        Title = rule.RentType,
                        //Dock = DockStyle.Top,
                        BodyColor = Color.White,
                        BorderColor = Color.Black,
                        ShowBodyBorder = true,
                        LinkButtonImage = Properties.Resources.已启用
                    };
                RatioRulePanel rulePanel = new RatioRulePanel()
                    {
                        RentRuleAllInfo = this.RentRuleAllInfo,
                        CurrentRule = rule,
                        CurrentEntity = this.CurrentEntity,
                        RatioRuleBLL = this.RatioRuleBLL,
                        Location = new Point(2, 32),
                    };
                panel.Tag = rulePanel;//fxh新增
                panel.Controls.Add(rulePanel);
                rulePanel.LoadData();
                rulePanelList.Add(panel);

                //this.pnlRentPanelContainer.Controls.Add(panel);
            }
            return rulePanelList;
        }

        private void SetToolTip(Control control)
        {
            if (control is PictureBox)
            {
                if (control.Name == "picDeleteTimeSegment")
                {
                    this.toolTip1.SetToolTip(control, base.GetMessage("ToolTipDeleteTimeInterval"));
                }
                else if (control.Name == "picDeleteJudge")
                {
                    this.toolTip1.SetToolTip(control, base.GetMessage("ToolTipDeleteJudgeCondition"));
                }
            }
            else
            {
                if (control.Controls != null && control.Controls.Count > 0)
                {
                    foreach (Control child in control.Controls)
                    {
                        this.SetToolTip(child);
                    }
                }
            }
        }

        /// <summary>
        /// 查找规则容器，必须在规则容器添加完成之后执行
        /// </summary>
        private void FindRulePanel(Control control)
        {
            if (control is FixedRulePanel || control is RatioRulePanel)
            {
                control.SizeChanged += new EventHandler(this.RulePanel_SizeChanged);
            }
            else
            {
                if (control.Controls != null && control.Controls.Count > 0)
                {
                    foreach (Control child in control.Controls)
                    {
                        this.FindRulePanel(child);
                    }
                }
                else
                {
                    // 如果合同变更，则首次DueDate不能修改
                    if (control.Name == "dtpFirstDueDate")
                    {
                        control.Enabled = !this.IsContractChange;
                    }
                }
            }
        }

        #endregion 私有方法

        /// <summary>
        /// 录入GL调整凭证。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGLAdjustment_Click(object sender, EventArgs e)
        {
            EntityInfoSettingEntity entityInfoSetting = this.RentRuleAllInfo.GetEntityInfoSetting(this.VendorNo, this.CurrentEntity.EntityID);
            if (entityInfoSetting != null)
            {
                List<EntityRentType> rentTypeList = this.RentRuleAllInfo.GetRentTypeListOfEntity(entityInfoSetting.EntityInfoSettingID);
                EntityRentType rentType = new EntityRentType() { EntityInfoSettingID = entityInfoSetting.EntityInfoSettingID };
                GLAdjustment form = new GLAdjustment(rentType, rentTypeList);
                form.ShowDialog();
            }
        }

    }
}