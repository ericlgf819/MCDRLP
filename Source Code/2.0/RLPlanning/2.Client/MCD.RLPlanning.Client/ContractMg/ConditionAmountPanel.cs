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

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class ConditionAmountPanel : BaseUserControl, IEntityEnabled
    {
        public ConditionAmountPanel()
        {
            InitializeComponent();
        }

        #region 字段和属性

        public bool ShowAddButton
        {
            get
            {
                return this.btnAddJudge.Visible;
            }
            set
            {
                this.btnAddJudge.Visible = value;
            }
        }

        public bool ShowDeleteButton
        {
            get { return this.picDeleteJudge.Visible; }
            set { this.picDeleteJudge.Visible = value; }
        }

        /// <summary>
        /// 点击删除时触发的事件
        /// </summary>
        public event EventHandler ConditionDeleting;
        /// <summary>
        /// 点击添加时触发的事件
        /// </summary>
        public event EventHandler ConditionAdding;

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public ConditionAmountEntity CurrentConditionAmount { get; set; }

        #endregion

        #region 公共方法

        public void AddJudgeCondition(string condition)
        {
            int index = this.txtFormula.SelectionStart;
            this.txtFormula.Text = this.txtFormula.Text.Insert(index, condition);
            this.txtFormula.SelectionStart = index + condition.Length;
        }

        public void AddPayableCondition(string condition)
        {
            int index = this.txtCyclePayable.SelectionStart;
            this.txtCyclePayable.Text = this.txtCyclePayable.Text.Insert(index, condition);
            this.txtCyclePayable.SelectionStart = index + condition.Length;
        }

        #endregion

        #region 事件处理方法

        private void PercentRentFormulaPanel_Load(object sender, EventArgs e)
        {
            this.bdsConditionAmount.DataSource = this.CurrentConditionAmount;
        }

        private void btnDeleteJudge_Click(object sender, EventArgs e)
        {
            if (this.MessageConfirm(base.GetMessage("NoticeDeleteConfirm")) == DialogResult.OK)
            {
                if (this.ConditionDeleting != null)
                {
                    this.ConditionDeleting(this, EventArgs.Empty);
                }
            }
        }

        private void btnAddJudge_Click(object sender, EventArgs e)
        {
            if (this.ConditionAdding != null)
            {
                this.ConditionAdding(this, EventArgs.Empty);
            }
        }

        private void innerControl_Enter(object sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }

        #endregion

        #region IEntityEnabled 成员

        public bool EntityEnabled
        {
            get
            {
                return this.CurrentConditionAmount.Enabled;
            }
            set
            {
                this.CurrentConditionAmount.Enabled = value;
            }
        }

        #endregion
    }
}
