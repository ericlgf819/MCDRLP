namespace MCD.RLPlanning.Client.ContractMg
{
    partial class FixedRulePanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlDateSegment = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddDateSegment = new MCD.Controls.UCButton();
            this.lblStartDate = new MCD.Controls.UCLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bdsFixedRentRule = new System.Windows.Forms.BindingSource(this.components);
            this.dtpFirstDueDate = new System.Windows.Forms.DateTimePicker();
            this.dtpNextDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new MCD.Controls.UCLabel();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.lblFirstDueDate = new MCD.Controls.UCLabel();
            this.lblRemark = new MCD.Controls.UCLabel();
            this.lblNextDueDate = new MCD.Controls.UCLabel();
            this.lblLineRentCalcStartDate = new MCD.Controls.UCLabel();
            this.cmbPaymentType = new MCD.Controls.UCComboBox();
            this.lblSummary = new MCD.Controls.UCLabel();
            this.lblPaymentType = new MCD.Controls.UCLabel();
            this.cmbLineRentStartDate = new System.Windows.Forms.ComboBox();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            this.ucLabel3 = new MCD.Controls.UCLabel();
            this.btnAddGLAdjustment = new System.Windows.Forms.Button();
            this.radSolarTime = new System.Windows.Forms.RadioButton();
            this.radRentTime = new System.Windows.Forms.RadioButton();
            this.cmbCycle = new MCD.Controls.UCComboBox();
            this.lblCycle = new MCD.Controls.UCLabel();
            this.ucLabel4 = new MCD.Controls.UCLabel();
            this.lblZXConstant = new MCD.Controls.UCLabel();
            this.numZXConstant = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.bdsFixedRentRule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZXConstant)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDateSegment
            // 
            this.pnlDateSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDateSegment.AutoScroll = true;
            this.pnlDateSegment.Location = new System.Drawing.Point(0, 80);
            this.pnlDateSegment.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDateSegment.Name = "pnlDateSegment";
            this.pnlDateSegment.Size = new System.Drawing.Size(885, 20);
            this.pnlDateSegment.TabIndex = 7;
            // 
            // btnAddDateSegment
            // 
            this.btnAddDateSegment.Location = new System.Drawing.Point(5, 54);
            this.btnAddDateSegment.Name = "btnAddDateSegment";
            this.btnAddDateSegment.Size = new System.Drawing.Size(75, 25);
            this.btnAddDateSegment.TabIndex = 9;
            this.btnAddDateSegment.Text = "添加日期段";
            this.btnAddDateSegment.UseVisualStyleBackColor = true;
            this.btnAddDateSegment.Click += new System.EventHandler(this.btnAddInterval_Click);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(84, 67);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 29;
            this.lblStartDate.Text = "生效日期";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsFixedRentRule, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Location = new System.Drawing.Point(64, 27);
            this.textBox2.MaxLength = 2000;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(145, 20);
            this.textBox2.TabIndex = 6;
            // 
            // bdsFixedRentRule
            // 
            this.bdsFixedRentRule.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.FixedRuleSettingEntity);
            // 
            // dtpFirstDueDate
            // 
            this.dtpFirstDueDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedRentRule, "FirstDueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpFirstDueDate.Location = new System.Drawing.Point(496, 2);
            this.dtpFirstDueDate.Name = "dtpFirstDueDate";
            this.dtpFirstDueDate.Size = new System.Drawing.Size(126, 20);
            this.dtpFirstDueDate.TabIndex = 4;
            this.dtpFirstDueDate.Visible = false;
            // 
            // dtpNextDueDate
            // 
            this.dtpNextDueDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedRentRule, "NextDueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpNextDueDate.Enabled = false;
            this.dtpNextDueDate.Location = new System.Drawing.Point(704, 3);
            this.dtpNextDueDate.Name = "dtpNextDueDate";
            this.dtpNextDueDate.Size = new System.Drawing.Size(123, 20);
            this.dtpNextDueDate.TabIndex = 5;
            this.dtpNextDueDate.Visible = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(211, 67);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 30;
            this.lblEndDate.Text = "结束日期";
            // 
            // txtSummary
            // 
            this.txtSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsFixedRentRule, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSummary.Location = new System.Drawing.Point(263, 28);
            this.txtSummary.MaxLength = 2000;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(148, 20);
            this.txtSummary.TabIndex = 8;
            // 
            // lblFirstDueDate
            // 
            this.lblFirstDueDate.AutoSize = true;
            this.lblFirstDueDate.LabelLocation = 496;
            this.lblFirstDueDate.Location = new System.Drawing.Point(419, 5);
            this.lblFirstDueDate.Name = "lblFirstDueDate";
            this.lblFirstDueDate.Size = new System.Drawing.Size(77, 13);
            this.lblFirstDueDate.TabIndex = 25;
            this.lblFirstDueDate.Text = "首次DueDate:";
            this.lblFirstDueDate.Visible = false;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.LabelLocation = 61;
            this.lblRemark.Location = new System.Drawing.Point(27, 30);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(34, 13);
            this.lblRemark.TabIndex = 22;
            this.lblRemark.Text = "备注:";
            // 
            // lblNextDueDate
            // 
            this.lblNextDueDate.AutoSize = true;
            this.lblNextDueDate.LabelLocation = 703;
            this.lblNextDueDate.Location = new System.Drawing.Point(626, 7);
            this.lblNextDueDate.Name = "lblNextDueDate";
            this.lblNextDueDate.Size = new System.Drawing.Size(77, 13);
            this.lblNextDueDate.TabIndex = 24;
            this.lblNextDueDate.Text = "本次DueDate:";
            this.lblNextDueDate.Visible = false;
            // 
            // lblLineRentCalcStartDate
            // 
            this.lblLineRentCalcStartDate.AutoSize = true;
            this.lblLineRentCalcStartDate.LabelLocation = 551;
            this.lblLineRentCalcStartDate.Location = new System.Drawing.Point(433, 30);
            this.lblLineRentCalcStartDate.Name = "lblLineRentCalcStartDate";
            this.lblLineRentCalcStartDate.Size = new System.Drawing.Size(118, 13);
            this.lblLineRentCalcStartDate.TabIndex = 18;
            this.lblLineRentCalcStartDate.Text = "直线租金计算起始日:";
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsFixedRentRule, "PayType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(64, 0);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(87, 21);
            this.cmbPaymentType.TabIndex = 0;
            this.cmbPaymentType.SelectionChangeCommitted += new System.EventHandler(this.cmbPaymentType_SelectionChangeCommitted);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.LabelLocation = 262;
            this.lblSummary.Location = new System.Drawing.Point(228, 31);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(34, 13);
            this.lblSummary.TabIndex = 20;
            this.lblSummary.Text = "摘要:";
            // 
            // lblPaymentType
            // 
            this.lblPaymentType.AutoSize = true;
            this.lblPaymentType.LabelLocation = 61;
            this.lblPaymentType.Location = new System.Drawing.Point(3, 3);
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Size = new System.Drawing.Size(58, 13);
            this.lblPaymentType.TabIndex = 16;
            this.lblPaymentType.Text = "支付类型:";
            // 
            // cmbLineRentStartDate
            // 
            this.cmbLineRentStartDate.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsFixedRentRule, "ZXStartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbLineRentStartDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineRentStartDate.FormattingEnabled = true;
            this.cmbLineRentStartDate.Location = new System.Drawing.Point(551, 27);
            this.cmbLineRentStartDate.Name = "cmbLineRentStartDate";
            this.cmbLineRentStartDate.Size = new System.Drawing.Size(126, 21);
            this.cmbLineRentStartDate.TabIndex = 7;
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(151, 5);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 13);
            this.ucLabel1.TabIndex = 33;
            this.ucLabel1.Text = "*";
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(683, 34);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 13);
            this.ucLabel2.TabIndex = 33;
            this.ucLabel2.Text = "*";
            // 
            // ucLabel3
            // 
            this.ucLabel3.AutoSize = true;
            this.ucLabel3.ForeColor = System.Drawing.Color.Red;
            this.ucLabel3.Location = new System.Drawing.Point(417, 35);
            this.ucLabel3.Name = "ucLabel3";
            this.ucLabel3.NeedLanguage = false;
            this.ucLabel3.Size = new System.Drawing.Size(11, 13);
            this.ucLabel3.TabIndex = 33;
            this.ucLabel3.Text = "*";
            // 
            // btnAddGLAdjustment
            // 
            this.btnAddGLAdjustment.Location = new System.Drawing.Point(762, 53);
            this.btnAddGLAdjustment.Name = "btnAddGLAdjustment";
            this.btnAddGLAdjustment.Size = new System.Drawing.Size(120, 24);
            this.btnAddGLAdjustment.TabIndex = 10;
            this.btnAddGLAdjustment.Text = "录入GL调整凭证";
            this.btnAddGLAdjustment.UseVisualStyleBackColor = true;
            this.btnAddGLAdjustment.Visible = false;
            this.btnAddGLAdjustment.Click += new System.EventHandler(this.btnAddGLAdjustment_Click);
            // 
            // radSolarTime
            // 
            this.radSolarTime.AutoSize = true;
            this.radSolarTime.Location = new System.Drawing.Point(371, 3);
            this.radSolarTime.Name = "radSolarTime";
            this.radSolarTime.Size = new System.Drawing.Size(49, 17);
            this.radSolarTime.TabIndex = 3;
            this.radSolarTime.Text = "公历";
            this.radSolarTime.UseVisualStyleBackColor = true;
            this.radSolarTime.CheckedChanged += new System.EventHandler(this.radRentTime_CheckedChanged);
            // 
            // radRentTime
            // 
            this.radRentTime.AutoSize = true;
            this.radRentTime.Checked = true;
            this.radRentTime.Location = new System.Drawing.Point(318, 3);
            this.radRentTime.Name = "radRentTime";
            this.radRentTime.Size = new System.Drawing.Size(49, 17);
            this.radRentTime.TabIndex = 2;
            this.radRentTime.TabStop = true;
            this.radRentTime.Text = "租赁";
            this.radRentTime.UseVisualStyleBackColor = true;
            this.radRentTime.CheckedChanged += new System.EventHandler(this.radRentTime_CheckedChanged);
            // 
            // cmbCycle
            // 
            this.cmbCycle.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsFixedRentRule, "Cycle", true));
            this.cmbCycle.FormattingEnabled = true;
            this.cmbCycle.Location = new System.Drawing.Point(220, 1);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Size = new System.Drawing.Size(82, 21);
            this.cmbCycle.TabIndex = 1;
            this.cmbCycle.SelectionChangeCommitted += new System.EventHandler(this.cmbCycle_SelectionChangeCommitted);
            // 
            // lblCycle
            // 
            this.lblCycle.AutoSize = true;
            this.lblCycle.LabelLocation = 219;
            this.lblCycle.Location = new System.Drawing.Point(161, 3);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(58, 13);
            this.lblCycle.TabIndex = 37;
            this.lblCycle.Text = "结算周期:";
            // 
            // ucLabel4
            // 
            this.ucLabel4.AutoSize = true;
            this.ucLabel4.ForeColor = System.Drawing.Color.Red;
            this.ucLabel4.Location = new System.Drawing.Point(301, 7);
            this.ucLabel4.Name = "ucLabel4";
            this.ucLabel4.NeedLanguage = false;
            this.ucLabel4.Size = new System.Drawing.Size(11, 13);
            this.ucLabel4.TabIndex = 39;
            this.ucLabel4.Text = "*";
            // 
            // lblZXConstant
            // 
            this.lblZXConstant.AutoSize = true;
            this.lblZXConstant.LabelLocation = 761;
            this.lblZXConstant.Location = new System.Drawing.Point(703, 30);
            this.lblZXConstant.Name = "lblZXConstant";
            this.lblZXConstant.Size = new System.Drawing.Size(58, 13);
            this.lblZXConstant.TabIndex = 20;
            this.lblZXConstant.Text = "直线常数:";
            // 
            // numZXConstant
            // 
            this.numZXConstant.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedRentRule, "ZXConstant", true));
            this.numZXConstant.DecimalPlaces = 2;
            this.numZXConstant.Location = new System.Drawing.Point(759, 27);
            this.numZXConstant.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numZXConstant.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numZXConstant.Name = "numZXConstant";
            this.numZXConstant.Size = new System.Drawing.Size(120, 20);
            this.numZXConstant.TabIndex = 40;
            this.numZXConstant.ThousandsSeparator = true;
            this.numZXConstant.ValueChanged += new System.EventHandler(this.numZXConstant_ValueChanged);
            // 
            // FixedRulePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numZXConstant);
            this.Controls.Add(this.radSolarTime);
            this.Controls.Add(this.radRentTime);
            this.Controls.Add(this.cmbCycle);
            this.Controls.Add(this.btnAddGLAdjustment);
            this.Controls.Add(this.ucLabel3);
            this.Controls.Add(this.ucLabel2);
            this.Controls.Add(this.cmbLineRentStartDate);
            this.Controls.Add(this.pnlDateSegment);
            this.Controls.Add(this.btnAddDateSegment);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dtpFirstDueDate);
            this.Controls.Add(this.dtpNextDueDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.lblFirstDueDate);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.lblNextDueDate);
            this.Controls.Add(this.lblLineRentCalcStartDate);
            this.Controls.Add(this.cmbPaymentType);
            this.Controls.Add(this.lblZXConstant);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblPaymentType);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.ucLabel4);
            this.Controls.Add(this.ucLabel1);
            this.Name = "FixedRulePanel";
            this.Size = new System.Drawing.Size(885, 100);
            ((System.ComponentModel.ISupportInitialize)(this.bdsFixedRentRule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZXConstant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlDateSegment;
        private MCD.Controls.UCButton btnAddDateSegment;
        private MCD.Controls.UCLabel lblStartDate;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dtpFirstDueDate;
        private System.Windows.Forms.DateTimePicker dtpNextDueDate;
        private MCD.Controls.UCLabel lblEndDate;
        private System.Windows.Forms.TextBox txtSummary;
        private MCD.Controls.UCLabel lblFirstDueDate;
        private MCD.Controls.UCLabel lblRemark;
        private MCD.Controls.UCLabel lblNextDueDate;
        private MCD.Controls.UCLabel lblLineRentCalcStartDate;
        private MCD.Controls.UCComboBox cmbPaymentType;
        private MCD.Controls.UCLabel lblSummary;
        private MCD.Controls.UCLabel lblPaymentType;
        private System.Windows.Forms.BindingSource bdsFixedRentRule;
        private System.Windows.Forms.ComboBox cmbLineRentStartDate;
        private MCD.Controls.UCLabel ucLabel1;
        private MCD.Controls.UCLabel ucLabel2;
        private MCD.Controls.UCLabel ucLabel3;
        private System.Windows.Forms.Button btnAddGLAdjustment;
        private System.Windows.Forms.RadioButton radSolarTime;
        private System.Windows.Forms.RadioButton radRentTime;
        private MCD.Controls.UCComboBox cmbCycle;
        private MCD.Controls.UCLabel lblCycle;
        private MCD.Controls.UCLabel ucLabel4;
        private MCD.Controls.UCLabel lblZXConstant;
        private System.Windows.Forms.NumericUpDown numZXConstant;
    }
}
