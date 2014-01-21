using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.Client.Common;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FixedRulePanel : BaseUserControl, IRentRule
    {
        public FixedRulePanel()
        {
            InitializeComponent();
        }

        #region 字段和属性
        //固定高度
        private const int c_FixedHeight = 80;

        //时间段控件的高度
        private const int c_SegmentHeight = 30;

        //当前活动时间段控件
        private FixedTimeIntervalPanel m_ActiveIntervalPanel = null;

        /// <summary>
        /// 是否正在加载
        /// </summary>
        //private bool m_IsLoading = true;

        //时间段控件列表，用来计算高度和判断是否能删除最后一个
        private List<FixedTimeIntervalPanel> myIntervalPanelList = new List<FixedTimeIntervalPanel>();

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public FixedRuleSettingEntity CurrentRule { get; set; }

        public EntityEntity CurrentEntity { get; set; }

        public ContractBLL ContractBLL { get; set; }

        public FixedRuleBLL FixedRuleBLL { get; set; }

        List<FixedTimeIntervalSettingEntity> m_IntervalList = null;
        #endregion

        #region 事件处理方法

        public void LoadData()
        {
            //this.m_IsLoading = true;
            this.cmbCycle.DisplayMember = "CycleItemName";
            this.cmbCycle.ValueMember = "CycleItemName";
            this.cmbCycle.DataSource = this.ContractBLL.GetCycleItems(CycleType.固定).Where(item => true).ToList();

            ControlHelper.BindComboBox(this.cmbPaymentType, GetDictionaryTable("RentPaymentType"), "ItemName", "ItemValue");

            this.FillLineRentStartDateComboBox();//填充直线租金计算起始日

            CheckRadioButtonStatus();

            if (!this.CurrentRule.ZXStartDate.HasValue)
            {
                this.CurrentRule.ZXStartDate = (this.cmbLineRentStartDate.Items[0] as ZXStartDateItem).Date;
            }

            if (string.IsNullOrEmpty(this.CurrentRule.PayType))
            {
                DataRowView row = this.cmbPaymentType.Items[0] as DataRowView;
                if (row != null)
                {
                    string payType = row["ItemValue"].ToString();
                    this.CurrentRule.PayType = payType;
                }
            }

            this.pnlDateSegment.SuspendLayout();

            this.m_IntervalList = this.RentRuleAllInfo.GetFixedTimeIntervalSetting(this.CurrentRule, this.CurrentEntity);
            foreach (var item in m_IntervalList)
            {
                this.AddTimeSegment(item, false);
            }
            this.AdjustPanelHeight();

            this.SetFirstDueDateRange();

            //固定管理费 不显示直线租金计算起始日
            //if (this.CurrentRule.RentType.IndexOf("管理费") > -1)
            //{
            //    this.lblLineRentCalcStartDate.Visible = this.cmbLineRentStartDate.Visible = this.ucLabel2.Visible = false;
            //}

            if (!(this.CurrentRule.RentType == "固定租金" 
                    && (this.CurrentEntity.EntityTypeName == "餐厅" 
                        || this.CurrentEntity.EntityTypeName == "甜品店")))
            {
                this.lblLineRentCalcStartDate.Visible = this.cmbLineRentStartDate.Visible = this.ucLabel2.Visible = false;
                this.lblZXConstant.Visible = this.numZXConstant.Visible = false;
            }

            if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更)
            {
                if (this.CurrentEntity.IsEntityHasAP)//当实体已经出了AP时，禁用相关控件
                {
                    this.cmbPaymentType.Enabled = false;
                    this.cmbCycle.Enabled = false;
                    this.radRentTime.Enabled = this.radSolarTime.Enabled = false;
                    this.dtpFirstDueDate.Enabled = this.dtpNextDueDate.Enabled = false;
                    this.cmbLineRentStartDate.Enabled = false;
                }
                else
                {
                    this.cmbPaymentType.Enabled = true;
                    this.cmbCycle.Enabled = true;
                    this.radRentTime.Enabled = this.radSolarTime.Enabled = true;
                    this.dtpFirstDueDate.Enabled = true;
                    this.dtpNextDueDate.Enabled = false;
                    this.cmbLineRentStartDate.Enabled = true;
                }
            }

            this.bdsFixedRentRule.DataSource = this.CurrentRule;
            this.pnlDateSegment.ResumeLayout();
            //this.m_IsLoading = false;
        }

        private void cmbPaymentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.CurrentRule.PayType = Convert.ToString(this.cmbPaymentType.SelectedValue);
            this.SetFirstDueDateRange();

            //if (this.CurrentEntity != null)
            //    this.CheckPaymentType();
        }

        private void radRentTime_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad.Checked)
            {
                if (rad == this.radSolarTime)
                {
                    this.CurrentRule.Calendar = CalendarType.公历.ToString();
                }
                else
                {
                    this.CurrentRule.Calendar = CalendarType.租赁.ToString();
                }
                this.CheckRadioButtonStatus();
                this.SetFirstDueDateRange();
            }
        }

        /// <summary>
        /// 检查当前选择的支付类型是否可用。
        /// </summary>
        /// <returns></returns>
        public bool CheckPaymentType(out string errMsg)
        {
            /*
            * 若对应的Type Code记录中，无完整地定义业务类型为预提的科目（即其中一个科目为空）则此处不能选择3.延付；
            * 若对应的Type Code记录中，无完整地定义业务类型为预付的科目（即其中一个科目为空）则此处不能选择1.预付；2.实付
            */
            errMsg = string.Empty;

            DataSet ds = null;
            using (MCD.RLPlanning.BLL.Master.TypeCodeBLL bll = new MCD.RLPlanning.BLL.Master.TypeCodeBLL())
            {
                ds = bll.SelectAllTypeCode("", this.CurrentRule.RentType, this.CurrentEntity.EntityTypeName, "已生效");
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                DataRow[] drs = null;
                string payType = Convert.ToString(this.cmbPaymentType.SelectedValue);

                //当前选择 延付
                if (payType.Equals("延付"))
                {
                    drs = ds.Tables[0].Select("YTAPDifferences = '' OR YTAPDifferences IS NULL OR " +
                        "YTAPNormal = '' OR YTAPNormal IS NULL OR " +
                        "YTGLCredit = '' OR YTGLCredit IS NULL OR " +
                        "YTGLDebit = '' OR YTGLDebit IS NULL OR " +
                        "YTRemark = '' OR YTRemark IS NULL");
                    if (drs != null && drs.Length > 0)
                    {
                        errMsg = "无法选择延付，因为对应的Type Code记录中未完整地定义业务类型为预提的科目！";
                        this.cmbPaymentType.Focus();
                        return false;
                    }
                }
                //当前选择预付或者实付
                else if (payType.Equals("预付") || payType.Equals("实付"))
                {
                    drs = ds.Tables[0].Select("YFAPDifferences = '' OR YFAPDifferences IS NULL OR " +
                        "YFAPNormal = '' OR YFAPNormal IS NULL OR " +
                        "YFGLCredit = '' OR YFGLCredit IS NULL OR " +
                        "YFGLDebit = '' OR YFGLDebit IS NULL OR " +
                        "YFRemak = '' OR YFRemak IS NULL");

                    if (drs != null && drs.Length > 0)
                    {
                        errMsg = "无法选择" + payType + "，因为对应的Type Code记录中未完整地定义业务类型为预付的科目！";
                        this.cmbPaymentType.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 设置FirstDueDate的范围
        /// </summary>
        /// <param name="intervalList"></param>
        private void SetFirstDueDateRange()
        {
            //Added by Eric -- Begin
            switch (CurrentRule.PayType)
            {
                case "实付":
                    {
                        //实付什么都不用改
                        break;
                    }
                case "预付":
                    {
                        //提前一个月
                        dtpFirstDueDate.Value = CurrentEntity.RentStartDate.AddMonths(-1);
                        break;
                    }
                case "延付":
                    {
                        //延后一个月
                        dtpFirstDueDate.Value = CurrentEntity.RentStartDate.AddMonths(1);
                        break;
                    }
                default:
                    break;
            }
            //Added by Eric -- End
        }

        /// <summary>
        /// 获取首次DueDate日期
        /// </summary>
        /// <param name="rentStartDate">起租日</param>
        /// <param name="calType">公历/租赁</param>
        /// <param name="cycleType">固定/百分比</param>
        /// <returns></returns>
        private DateTime GetFirstCycleEndDate(DateTime rentStartDate, CalendarType calendarType, int cycleMonthCount)
        {
            DateTime cycleEndDate;
            DateTime startDate = new DateTime(1950, 1, 1);
            DateTime tempDate;
            if (calendarType == CalendarType.租赁)
            {
                cycleEndDate = rentStartDate.AddMonths(cycleMonthCount).AddDays(-1);
            }
            else//公历
            {
                tempDate = startDate;
                while (tempDate < rentStartDate)
                {
                    tempDate = tempDate.AddMonths(cycleMonthCount);
                }
                cycleEndDate = tempDate.AddDays(-1);
            }
            return cycleEndDate;
        }


        //填充直线租金计算起始日
        private void FillLineRentStartDateComboBox()
        {
            List<ZXStartDateItem> items = new List<ZXStartDateItem>();
            DataTable dt = this.GetDictionaryTable("ZXStartDateType");
            foreach (DataRow row in dt.Rows)
            {
                ZXStartDateItem item = new ZXStartDateItem()
                    {
                        Name = row["ItemName"].ToString(),
                        Value = row["ItemValue"].ToString(),
                    };
                items.Add(item);
            }

            ZXStartDateItem itemOpening = items.FirstOrDefault(item => item.Value == "1");
            if (this.CurrentEntity.OpeningDate.HasValue)
            {
                itemOpening.Date = this.CurrentEntity.OpeningDate.Value;
            }
            ZXStartDateItem itemRentStart = items.FirstOrDefault(item => item.Value == "2");
            itemRentStart.Date = this.CurrentEntity.RentStartDate;

            items.Clear();
            if (this.CurrentEntity.OpeningDate.HasValue)
            {
                items.Add(itemOpening);
            }
            items.Add(itemRentStart);

            this.cmbLineRentStartDate.DisplayMember = "Display";
            this.cmbLineRentStartDate.ValueMember = "Date";
            this.cmbLineRentStartDate.DataSource = items;
        }

        private void btnAddInterval_Click(object sender, EventArgs e)
        {
            FixedTimeIntervalSettingEntity interval = new FixedTimeIntervalSettingEntity()
                {
                    TimeIntervalID = Guid.NewGuid().ToString(),
                    RuleSnapshotID = this.CurrentRule.RuleSnapshotID,
                    StartDate = this.CurrentEntity.RentStartDate,
                    EndDate = this.CurrentEntity.RentEndDate,
                    Calendar = CalendarType.公历.ToString(),
                    Enabled = true,
                };

            this.RentRuleAllInfo.FixedTimeIntervalSettingList.Add(interval);
            this.AddTimeSegment(interval, true);
        }

        /// <summary>
        /// GL调账。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGLAdjustment_Click(object sender, EventArgs e)
        {
            GLAdjustmentEntity entity = new GLAdjustmentEntity();
            entity.EntityInfoSettingID = this.CurrentRule.EntityInfoSettingID;
            entity.RentType = this.CurrentRule.RentType;
            entity.RuleSnapshotID = this.CurrentRule.RuleSnapshotID;
            entity.RuleID = this.CurrentRule.RuleID;

            GLAdjustment form = new GLAdjustment(entity);
            form.ShowDialog();
        }

        private void intervalPanel_GotFocused(object sender, EventArgs e)
        {
            FixedTimeIntervalPanel segment = sender as FixedTimeIntervalPanel;
            if (segment != null)
            {
                this.m_ActiveIntervalPanel = segment;
            }
        }

        private void intervalPanel_IntervalDeleting(object sender, EventArgs e)
        {
            FixedTimeIntervalPanel intervalPanel = sender as FixedTimeIntervalPanel;
            if (intervalPanel != null)
            {
                if (this.myIntervalPanelList.Count > 1)
                {
                    intervalPanel.CurrentInterval.Enabled = false;
                    this.pnlDateSegment.Controls.Remove(intervalPanel);
                    this.myIntervalPanelList.Remove(intervalPanel);
                    this.AdjustPanelHeight();
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeMustContainOneInterval"));
                }
            }
        }

        private void intervalPanel_CycleChanged(object sender, EventArgs e)
        {
            FixedTimeIntervalPanel intervalPanel = sender as FixedTimeIntervalPanel;
            if (intervalPanel != null && intervalPanel == this.myIntervalPanelList[0])
            {
                this.SetFirstDueDateRange();
            }
        }

        private void cmbCycle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CycleItemEntity cycle = this.cmbCycle.SelectedItem as CycleItemEntity;
            if (cycle != null)
            {
                //this.CurrentRule.Cycle = cycle.CycleItemName;
                this.CurrentRule.CycleMonthCount = cycle.CycleMonthCount;
            }
        }

        #endregion

        #region 私有方法

        private void AddTimeSegment(FixedTimeIntervalSettingEntity interval, bool shouldResize)
        {
            FixedTimeIntervalPanel segment = new FixedTimeIntervalPanel()
                {
                    RentRuleAllInfo = this.RentRuleAllInfo,
                    CurrentInterval = interval,
                    CurrentEntity = this.CurrentEntity,
                    ContractBLL = this.ContractBLL,
                };
            segment.Margin = new Padding(0);
            segment.IntervalDeleting += new EventHandler(intervalPanel_IntervalDeleting);
            segment.GotFocused += new EventHandler(intervalPanel_GotFocused);
            segment.CycleChanged += new EventHandler(intervalPanel_CycleChanged);
            segment.Margin = new Padding(0);
            this.myIntervalPanelList.Add(segment);
            this.pnlDateSegment.Controls.Add(segment);
            if (shouldResize)
            {
                this.AdjustPanelHeight();
            }
            this.m_ActiveIntervalPanel = segment;
        }

        private void AdjustPanelHeight()
        {
            if (this.myIntervalPanelList.Count > 0)
            {
                this.Height = c_FixedHeight + this.myIntervalPanelList.Count * c_SegmentHeight + 10;
                this.myIntervalPanelList.ForEach(item =>
                    {
                        item.ShowDeleteButton = (this.myIntervalPanelList.Count > 1);
                        if (item == this.myIntervalPanelList.First())
                        {
                            item.StartDateEnable = false;
                            item.CurrentInterval.StartDate = this.CurrentEntity.RentStartDate;
                        }
                        else
                        {
                            item.StartDateEnable = true;
                        }
                        //item.StartDateEnable = (item == this.myIntervalPanelList.First() ? false : true);
                        if (item == this.myIntervalPanelList.Last())
                        {
                            item.EndDateEnable = false;
                            item.CurrentInterval.EndDate = this.CurrentEntity.RentEndDate;
                        }
                        else
                        {
                            item.EndDateEnable = true;
                        }

                        item.RefreshStartEndDate();
                        //item.EndDateEnable = (item == this.myIntervalPanelList.Last() ? false : true);
                    });
                //this.myIntervalPanelList.First().StartDateEnable = false;
                //this.myIntervalPanelList.First().CurrentInterval.StartDate = this.CurrentEntity.RentStartDate;
                //this.myIntervalPanelList.Last().EndDateEnable = false;
                //this.myIntervalPanelList.Last().CurrentInterval.EndDate = this.CurrentEntity.RentEndDate;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.Parent != null)
            {
                this.Parent.Height = this.Top + this.Height;
            }
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
                    DictionaryTables[keyValue] = dt;
                }
            }
            return DictionaryTables[keyValue];
        }

        private void CheckRadioButtonStatus()
        {
            this.radSolarTime.Checked = CalendarType.公历.ToString().Equals(this.CurrentRule.Calendar);
            this.radRentTime.Checked = !this.radSolarTime.Checked;
        }

        #endregion

        #region IRentRule 成员
        /// <summary>
        /// 禁用规则。
        /// </summary>
        /// <returns></returns>
        public bool Disable()
        {
            this.Visible = false;
            this.CurrentRule.Enabled = false;

            ControlHelper.FindControl(this.pnlDateSegment, ctrl =>
            {
                return ctrl is IEntityEnabled;
            }).ForEach(item =>
            {
                (item as IEntityEnabled).EntityEnabled = false;
            });

            //if (this.CurrentRule.Enabled)
            //{
            //    this.FixedRuleBLL.DeleteSingleFixedRuleSetting(this.CurrentRule);
            //}

            return true;
        }

        /// <summary>
        /// 启用规则。
        /// </summary>
        /// <returns></returns>
        public bool Enable()
        {
            this.Visible = true;
            this.CurrentRule.Enabled = true;
            //this.FixedRuleBLL.InsertOrUpdateFixedRuleSetting(this.CurrentRule);

            ControlHelper.FindControl(this.pnlDateSegment, ctrl =>
            {
                return ctrl is IEntityEnabled;
            }).ForEach(item =>
            {
                (item as IEntityEnabled).EntityEnabled = true;
            });

            return true;
        }

        /// <summary>
        /// 获取或设置当前规则的启用状态。
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
        #endregion

        //内部类，用于展示直线租金计算起租日
        internal class ZXStartDateItem
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Value { get; set; }
            public string Display
            {
                get
                {
                    return string.Format("{0}{1}", this.Name, this.Date.ToString("yyyy-MM-dd"));
                }
            }
        }

        private void numZXConstant_ValueChanged(object sender, EventArgs e)
        {
            this.CurrentRule.ZXConstant = this.numZXConstant.Value;
        }


    }
}
