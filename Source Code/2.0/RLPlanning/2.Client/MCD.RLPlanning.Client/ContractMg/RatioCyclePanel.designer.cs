namespace MCD.RLPlanning.Client.ContractMg
{
    partial class RatioCyclePanel
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
            this.radSolarTime = new System.Windows.Forms.RadioButton();
            this.radRentTime = new System.Windows.Forms.RadioButton();
            this.dtpFirstDueDate = new System.Windows.Forms.DateTimePicker();
            this.bdsRatioRentRule = new System.Windows.Forms.BindingSource(this.components);
            this.dtpNextDueDate = new System.Windows.Forms.DateTimePicker();
            this.pnlDateSegment = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCondition = new MCD.Controls.UCLabel();
            this.lblStartDate = new MCD.Controls.UCLabel();
            this.lblEndDate = new MCD.Controls.UCLabel();
            this.btnEqual = new MCD.Controls.UCButton();
            this.btnLessThan = new MCD.Controls.UCButton();
            this.btnGreatThan = new MCD.Controls.UCButton();
            this.btnCycleSales2 = new MCD.Controls.UCButton();
            this.btnMax = new MCD.Controls.UCButton();
            this.btnReturnPaidFixed = new MCD.Controls.UCButton();
            this.btnCycleSales = new MCD.Controls.UCButton();
            this.btnAddDateSegment = new MCD.Controls.UCButton();
            this.lblFirstDueDate = new MCD.Controls.UCLabel();
            this.lblNextDueDate = new MCD.Controls.UCLabel();
            this.cmbCycle = new MCD.Controls.UCComboBox();
            this.cmbPaymentType = new MCD.Controls.UCComboBox();
            this.lblCycle = new MCD.Controls.UCLabel();
            this.lblPaymentType = new MCD.Controls.UCLabel();
            this.lblRatioPanelTitle = new MCD.Controls.UCLabel();
            this.btnReturnPaidRatio = new MCD.Controls.UCButton();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            this.chkIsPure = new System.Windows.Forms.CheckBox();
            this.btnAddGLAdjustment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioRentRule)).BeginInit();
            this.SuspendLayout();
            // 
            // radSolarTime
            // 
            this.radSolarTime.AutoSize = true;
            this.radSolarTime.Location = new System.Drawing.Point(377, 27);
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
            this.radRentTime.Location = new System.Drawing.Point(324, 27);
            this.radRentTime.Name = "radRentTime";
            this.radRentTime.Size = new System.Drawing.Size(49, 17);
            this.radRentTime.TabIndex = 2;
            this.radRentTime.TabStop = true;
            this.radRentTime.Text = "租赁";
            this.radRentTime.UseVisualStyleBackColor = true;
            this.radRentTime.CheckedChanged += new System.EventHandler(this.radRentTime_CheckedChanged);
            // 
            // dtpFirstDueDate
            // 
            this.dtpFirstDueDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsRatioRentRule, "FirstDueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpFirstDueDate.Location = new System.Drawing.Point(503, 25);
            this.dtpFirstDueDate.Name = "dtpFirstDueDate";
            this.dtpFirstDueDate.Size = new System.Drawing.Size(126, 20);
            this.dtpFirstDueDate.TabIndex = 4;
            this.dtpFirstDueDate.Visible = false;
            // 
            // bdsRatioRentRule
            // 
            this.bdsRatioRentRule.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.RatioCycleSettingEntity);
            // 
            // dtpNextDueDate
            // 
            this.dtpNextDueDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsRatioRentRule, "NextDueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpNextDueDate.Enabled = false;
            this.dtpNextDueDate.Location = new System.Drawing.Point(714, 25);
            this.dtpNextDueDate.Name = "dtpNextDueDate";
            this.dtpNextDueDate.Size = new System.Drawing.Size(124, 20);
            this.dtpNextDueDate.TabIndex = 5;
            this.dtpNextDueDate.Visible = false;
            // 
            // pnlDateSegment
            // 
            this.pnlDateSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDateSegment.AutoScroll = true;
            this.pnlDateSegment.Location = new System.Drawing.Point(0, 114);
            this.pnlDateSegment.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDateSegment.Name = "pnlDateSegment";
            this.pnlDateSegment.Size = new System.Drawing.Size(885, 30);
            this.pnlDateSegment.TabIndex = 15;
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(347, 95);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(117, 13);
            this.lblCondition.TabIndex = 21;
            this.lblCondition.Text = "基于Sales的判断条件";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(73, 95);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 20;
            this.lblStartDate.Text = "生效日期";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(219, 95);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 19;
            this.lblEndDate.Text = "结束日期";
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(502, 53);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(29, 25);
            this.btnEqual.TabIndex = 10;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnLessThan
            // 
            this.btnLessThan.Location = new System.Drawing.Point(467, 53);
            this.btnLessThan.Name = "btnLessThan";
            this.btnLessThan.Size = new System.Drawing.Size(29, 25);
            this.btnLessThan.TabIndex = 9;
            this.btnLessThan.Text = "<";
            this.btnLessThan.UseVisualStyleBackColor = true;
            this.btnLessThan.Click += new System.EventHandler(this.btnLessThan_Click);
            // 
            // btnGreatThan
            // 
            this.btnGreatThan.Location = new System.Drawing.Point(432, 53);
            this.btnGreatThan.Name = "btnGreatThan";
            this.btnGreatThan.Size = new System.Drawing.Size(29, 25);
            this.btnGreatThan.TabIndex = 8;
            this.btnGreatThan.Text = ">";
            this.btnGreatThan.UseVisualStyleBackColor = true;
            this.btnGreatThan.Click += new System.EventHandler(this.btnGreatThan_Click);
            // 
            // btnCycleSales2
            // 
            this.btnCycleSales2.Location = new System.Drawing.Point(749, 86);
            this.btnCycleSales2.Name = "btnCycleSales2";
            this.btnCycleSales2.Size = new System.Drawing.Size(124, 25);
            this.btnCycleSales2.TabIndex = 14;
            this.btnCycleSales2.Text = "结算周期sales";
            this.btnCycleSales2.UseVisualStyleBackColor = true;
            this.btnCycleSales2.Click += new System.EventHandler(this.btnCycleSales2_Click);
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(622, 86);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(124, 25);
            this.btnMax.TabIndex = 13;
            this.btnMax.Text = "MAX{;}";
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnReturnPaidFixed
            // 
            this.btnReturnPaidFixed.Location = new System.Drawing.Point(749, 57);
            this.btnReturnPaidFixed.Name = "btnReturnPaidFixed";
            this.btnReturnPaidFixed.Size = new System.Drawing.Size(124, 25);
            this.btnReturnPaidFixed.TabIndex = 12;
            this.btnReturnPaidFixed.Text = "返回已付固定租金";
            this.btnReturnPaidFixed.Click += new System.EventHandler(this.btnReturnPaidFixed_Click);
            // 
            // btnCycleSales
            // 
            this.btnCycleSales.Location = new System.Drawing.Point(324, 53);
            this.btnCycleSales.Name = "btnCycleSales";
            this.btnCycleSales.Size = new System.Drawing.Size(101, 25);
            this.btnCycleSales.TabIndex = 7;
            this.btnCycleSales.Text = "结算周期sales";
            this.btnCycleSales.UseVisualStyleBackColor = true;
            this.btnCycleSales.Click += new System.EventHandler(this.btnCycleSales_Click);
            // 
            // btnAddDateSegment
            // 
            this.btnAddDateSegment.Location = new System.Drawing.Point(3, 53);
            this.btnAddDateSegment.Name = "btnAddDateSegment";
            this.btnAddDateSegment.Size = new System.Drawing.Size(75, 25);
            this.btnAddDateSegment.TabIndex = 6;
            this.btnAddDateSegment.Text = "添加日期段";
            this.btnAddDateSegment.UseVisualStyleBackColor = true;
            this.btnAddDateSegment.Click += new System.EventHandler(this.btnAddDateSegment_Click);
            // 
            // lblFirstDueDate
            // 
            this.lblFirstDueDate.AutoSize = true;
            this.lblFirstDueDate.LabelLocation = 502;
            this.lblFirstDueDate.Location = new System.Drawing.Point(425, 28);
            this.lblFirstDueDate.Name = "lblFirstDueDate";
            this.lblFirstDueDate.Size = new System.Drawing.Size(77, 13);
            this.lblFirstDueDate.TabIndex = 12;
            this.lblFirstDueDate.Text = "首次DuaDate:";
            this.lblFirstDueDate.Visible = false;
            // 
            // lblNextDueDate
            // 
            this.lblNextDueDate.AutoSize = true;
            this.lblNextDueDate.LabelLocation = 714;
            this.lblNextDueDate.Location = new System.Drawing.Point(637, 28);
            this.lblNextDueDate.Name = "lblNextDueDate";
            this.lblNextDueDate.Size = new System.Drawing.Size(77, 13);
            this.lblNextDueDate.TabIndex = 13;
            this.lblNextDueDate.Text = "本次DuaDate:";
            this.lblNextDueDate.Visible = false;
            // 
            // cmbCycle
            // 
            this.cmbCycle.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsRatioRentRule, "Cycle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCycle.FormattingEnabled = true;
            this.cmbCycle.Location = new System.Drawing.Point(220, 25);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Size = new System.Drawing.Size(87, 21);
            this.cmbCycle.TabIndex = 1;
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsRatioRentRule, "PayType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbPaymentType.Enabled = false;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(66, 25);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(87, 21);
            this.cmbPaymentType.TabIndex = 0;
            // 
            // lblCycle
            // 
            this.lblCycle.AutoSize = true;
            this.lblCycle.LabelLocation = 219;
            this.lblCycle.Location = new System.Drawing.Point(161, 28);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(58, 13);
            this.lblCycle.TabIndex = 1;
            this.lblCycle.Text = "结算周期:";
            // 
            // lblPaymentType
            // 
            this.lblPaymentType.AutoSize = true;
            this.lblPaymentType.LabelLocation = 64;
            this.lblPaymentType.Location = new System.Drawing.Point(6, 28);
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Size = new System.Drawing.Size(58, 13);
            this.lblPaymentType.TabIndex = 1;
            this.lblPaymentType.Text = "支付类型:";
            // 
            // lblRatioPanelTitle
            // 
            this.lblRatioPanelTitle.AutoSize = true;
            this.lblRatioPanelTitle.Location = new System.Drawing.Point(4, 8);
            this.lblRatioPanelTitle.Name = "lblRatioPanelTitle";
            this.lblRatioPanelTitle.NeedLanguage = false;
            this.lblRatioPanelTitle.Size = new System.Drawing.Size(151, 13);
            this.lblRatioPanelTitle.TabIndex = 0;
            this.lblRatioPanelTitle.Text = "结算周期较短的百分比租金";
            // 
            // btnReturnPaidRatio
            // 
            this.btnReturnPaidRatio.Location = new System.Drawing.Point(622, 57);
            this.btnReturnPaidRatio.Name = "btnReturnPaidRatio";
            this.btnReturnPaidRatio.Size = new System.Drawing.Size(124, 25);
            this.btnReturnPaidRatio.TabIndex = 11;
            this.btnReturnPaidRatio.Text = "返回已付百分比租金";
            this.btnReturnPaidRatio.UseVisualStyleBackColor = true;
            this.btnReturnPaidRatio.Click += new System.EventHandler(this.btnReturnPaidRatio_Click);
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(153, 31);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 13);
            this.ucLabel1.TabIndex = 25;
            this.ucLabel1.Text = "*";
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(310, 31);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 13);
            this.ucLabel2.TabIndex = 25;
            this.ucLabel2.Text = "*";
            // 
            // chkIsPure
            // 
            this.chkIsPure.AutoSize = true;
            this.chkIsPure.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bdsRatioRentRule, "IsPure", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsPure.Location = new System.Drawing.Point(324, 4);
            this.chkIsPure.Name = "chkIsPure";
            this.chkIsPure.Size = new System.Drawing.Size(74, 17);
            this.chkIsPure.TabIndex = 26;
            this.chkIsPure.Text = "纯百分比";
            this.chkIsPure.UseVisualStyleBackColor = true;
            // 
            // btnAddGLAdjustment
            // 
            this.btnAddGLAdjustment.Location = new System.Drawing.Point(759, 1);
            this.btnAddGLAdjustment.Name = "btnAddGLAdjustment";
            this.btnAddGLAdjustment.Size = new System.Drawing.Size(120, 24);
            this.btnAddGLAdjustment.TabIndex = 36;
            this.btnAddGLAdjustment.Text = "录入GL调整凭证";
            this.btnAddGLAdjustment.UseVisualStyleBackColor = true;
            this.btnAddGLAdjustment.Visible = false;
            this.btnAddGLAdjustment.Click += new System.EventHandler(this.btnAddGLAdjustment_Click);
            // 
            // RatioCyclePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnAddGLAdjustment);
            this.Controls.Add(this.cmbPaymentType);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.chkIsPure);
            this.Controls.Add(this.ucLabel1);
            this.Controls.Add(this.pnlDateSegment);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.btnLessThan);
            this.Controls.Add(this.btnGreatThan);
            this.Controls.Add(this.btnCycleSales2);
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnReturnPaidRatio);
            this.Controls.Add(this.btnReturnPaidFixed);
            this.Controls.Add(this.btnCycleSales);
            this.Controls.Add(this.btnAddDateSegment);
            this.Controls.Add(this.dtpFirstDueDate);
            this.Controls.Add(this.dtpNextDueDate);
            this.Controls.Add(this.lblFirstDueDate);
            this.Controls.Add(this.lblNextDueDate);
            this.Controls.Add(this.cmbCycle);
            this.Controls.Add(this.radSolarTime);
            this.Controls.Add(this.radRentTime);
            this.Controls.Add(this.lblPaymentType);
            this.Controls.Add(this.lblRatioPanelTitle);
            this.Controls.Add(this.ucLabel2);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Name = "RatioCyclePanel";
            this.Size = new System.Drawing.Size(885, 144);
            this.Load += new System.EventHandler(this.PercentRentPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioRentRule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblRatioPanelTitle;
        private MCD.Controls.UCLabel lblPaymentType;
        private MCD.Controls.UCLabel lblCycle;
        private System.Windows.Forms.RadioButton radSolarTime;
        private System.Windows.Forms.RadioButton radRentTime;
        private MCD.Controls.UCComboBox cmbPaymentType;
        private MCD.Controls.UCComboBox cmbCycle;
        private System.Windows.Forms.DateTimePicker dtpFirstDueDate;
        private System.Windows.Forms.DateTimePicker dtpNextDueDate;
        private MCD.Controls.UCLabel lblFirstDueDate;
        private MCD.Controls.UCLabel lblNextDueDate;
        private MCD.Controls.UCButton btnAddDateSegment;
        private MCD.Controls.UCButton btnCycleSales;
        private MCD.Controls.UCButton btnGreatThan;
        private MCD.Controls.UCButton btnLessThan;
        private MCD.Controls.UCButton btnEqual;
        private MCD.Controls.UCButton btnReturnPaidFixed;
        private MCD.Controls.UCLabel lblStartDate;
        private MCD.Controls.UCLabel lblEndDate;
        private MCD.Controls.UCLabel lblCondition;
        private MCD.Controls.UCButton btnMax;
        private MCD.Controls.UCButton btnCycleSales2;
        private System.Windows.Forms.FlowLayoutPanel pnlDateSegment;
        private System.Windows.Forms.BindingSource bdsRatioRentRule;
        private MCD.Controls.UCButton btnReturnPaidRatio;
        private MCD.Controls.UCLabel ucLabel1;
        private MCD.Controls.UCLabel ucLabel2;
        private System.Windows.Forms.CheckBox chkIsPure;
        private System.Windows.Forms.Button btnAddGLAdjustment;
    }
}
