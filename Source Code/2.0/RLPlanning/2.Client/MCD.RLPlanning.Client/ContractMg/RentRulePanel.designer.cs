namespace MCD.RLPlanning.Client.ContractMg
{
    partial class RentRulePanel
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
            this.grpEntityInfo = new System.Windows.Forms.GroupBox();
            this.dtpOpeningDate = new System.Windows.Forms.DateTimePicker();
            this.bdsEntity = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtpRentStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblTaxRate = new MCD.Controls.UCLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bdsEntityInfoSetting = new System.Windows.Forms.BindingSource(this.components);
            this.lblMarginRemark = new MCD.Controls.UCLabel();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.lblMarginAmount = new MCD.Controls.UCLabel();
            this.ucLabel5 = new MCD.Controls.UCLabel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblYearSales = new MCD.Controls.UCLabel();
            this.lblRentEndDate = new MCD.Controls.UCLabel();
            this.lblRentStartDate = new MCD.Controls.UCLabel();
            this.lblOpeningDate = new MCD.Controls.UCLabel();
            this.pnlRentPanelContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbEntityName = new MCD.Controls.UCComboBox();
            this.lblEntityName = new MCD.Controls.UCLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddGLAdjustment = new System.Windows.Forms.Button();
            this.grpEntityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntityInfoSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEntityInfo
            // 
            this.grpEntityInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEntityInfo.Controls.Add(this.dtpOpeningDate);
            this.grpEntityInfo.Controls.Add(this.dateTimePicker1);
            this.grpEntityInfo.Controls.Add(this.dtpRentStartDate);
            this.grpEntityInfo.Controls.Add(this.lblTaxRate);
            this.grpEntityInfo.Controls.Add(this.textBox1);
            this.grpEntityInfo.Controls.Add(this.lblMarginRemark);
            this.grpEntityInfo.Controls.Add(this.numericUpDown3);
            this.grpEntityInfo.Controls.Add(this.numericUpDown2);
            this.grpEntityInfo.Controls.Add(this.lblMarginAmount);
            this.grpEntityInfo.Controls.Add(this.ucLabel5);
            this.grpEntityInfo.Controls.Add(this.numericUpDown1);
            this.grpEntityInfo.Controls.Add(this.lblYearSales);
            this.grpEntityInfo.Controls.Add(this.lblRentEndDate);
            this.grpEntityInfo.Controls.Add(this.lblRentStartDate);
            this.grpEntityInfo.Controls.Add(this.lblOpeningDate);
            this.grpEntityInfo.Location = new System.Drawing.Point(3, 30);
            this.grpEntityInfo.Name = "grpEntityInfo";
            this.grpEntityInfo.Size = new System.Drawing.Size(928, 113);
            this.grpEntityInfo.TabIndex = 1;
            this.grpEntityInfo.TabStop = false;
            this.grpEntityInfo.Text = "实体信息";
            // 
            // dtpOpeningDate
            // 
            this.dtpOpeningDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "OpeningDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpOpeningDate.Enabled = false;
            this.dtpOpeningDate.Location = new System.Drawing.Point(89, 17);
            this.dtpOpeningDate.Name = "dtpOpeningDate";
            this.dtpOpeningDate.Size = new System.Drawing.Size(120, 21);
            this.dtpOpeningDate.TabIndex = 0;
            // 
            // bdsEntity
            // 
            this.bdsEntity.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.EntityEntity);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "RentEndDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(590, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // dtpRentStartDate
            // 
            this.dtpRentStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "RentStartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpRentStartDate.Enabled = false;
            this.dtpRentStartDate.Location = new System.Drawing.Point(308, 17);
            this.dtpRentStartDate.Name = "dtpRentStartDate";
            this.dtpRentStartDate.Size = new System.Drawing.Size(120, 21);
            this.dtpRentStartDate.TabIndex = 1;
            // 
            // lblTaxRate
            // 
            this.lblTaxRate.AutoSize = true;
            this.lblTaxRate.LabelLocation = 307;
            this.lblTaxRate.Location = new System.Drawing.Point(273, 44);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new System.Drawing.Size(35, 12);
            this.lblTaxRate.TabIndex = 11;
            this.lblTaxRate.Text = "税率:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsEntityInfoSetting, "MarginRemark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(89, 66);
            this.textBox1.MaxLength = 2000;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(621, 37);
            this.textBox1.TabIndex = 6;
            // 
            // bdsEntityInfoSetting
            // 
            this.bdsEntityInfoSetting.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.EntityInfoSettingEntity);
            // 
            // lblMarginRemark
            // 
            this.lblMarginRemark.AutoSize = true;
            this.lblMarginRemark.LabelLocation = 88;
            this.lblMarginRemark.Location = new System.Drawing.Point(18, 72);
            this.lblMarginRemark.Name = "lblMarginRemark";
            this.lblMarginRemark.Size = new System.Drawing.Size(71, 12);
            this.lblMarginRemark.TabIndex = 9;
            this.lblMarginRemark.Text = "保证金备注:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntityInfoSetting, "TaxRate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown3.DecimalPlaces = 4;
            this.numericUpDown3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDown3.Location = new System.Drawing.Point(308, 42);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown3.TabIndex = 4;
            this.numericUpDown3.Value = new decimal(new int[] {
            550,
            0,
            0,
            262144});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntityInfoSetting, "MarginAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Location = new System.Drawing.Point(89, 42);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.ThousandsSeparator = true;
            // 
            // lblMarginAmount
            // 
            this.lblMarginAmount.AutoSize = true;
            this.lblMarginAmount.LabelLocation = 88;
            this.lblMarginAmount.Location = new System.Drawing.Point(30, 44);
            this.lblMarginAmount.Name = "lblMarginAmount";
            this.lblMarginAmount.Size = new System.Drawing.Size(59, 12);
            this.lblMarginAmount.TabIndex = 7;
            this.lblMarginAmount.Text = "保证金额:";
            // 
            // ucLabel5
            // 
            this.ucLabel5.AutoSize = true;
            this.ucLabel5.ForeColor = System.Drawing.Color.Red;
            this.ucLabel5.Location = new System.Drawing.Point(432, 49);
            this.ucLabel5.Name = "ucLabel5";
            this.ucLabel5.NeedLanguage = false;
            this.ucLabel5.Size = new System.Drawing.Size(11, 12);
            this.ucLabel5.TabIndex = 6;
            this.ucLabel5.Text = "*";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntityInfoSetting, "RealestateSales", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(590, 42);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.ThousandsSeparator = true;
            // 
            // lblYearSales
            // 
            this.lblYearSales.AutoSize = true;
            this.lblYearSales.LabelLocation = 585;
            this.lblYearSales.Location = new System.Drawing.Point(455, 44);
            this.lblYearSales.Name = "lblYearSales";
            this.lblYearSales.Size = new System.Drawing.Size(137, 12);
            this.lblYearSales.TabIndex = 4;
            this.lblYearSales.Text = "地产部提供预估年sales:";
            // 
            // lblRentEndDate
            // 
            this.lblRentEndDate.AutoSize = true;
            this.lblRentEndDate.LabelLocation = 585;
            this.lblRentEndDate.Location = new System.Drawing.Point(515, 20);
            this.lblRentEndDate.Name = "lblRentEndDate";
            this.lblRentEndDate.Size = new System.Drawing.Size(71, 12);
            this.lblRentEndDate.TabIndex = 2;
            this.lblRentEndDate.Text = "租赁到期日:";
            // 
            // lblRentStartDate
            // 
            this.lblRentStartDate.AutoSize = true;
            this.lblRentStartDate.LabelLocation = 307;
            this.lblRentStartDate.Location = new System.Drawing.Point(261, 18);
            this.lblRentStartDate.Name = "lblRentStartDate";
            this.lblRentStartDate.Size = new System.Drawing.Size(47, 12);
            this.lblRentStartDate.TabIndex = 2;
            this.lblRentStartDate.Text = "起租日:";
            // 
            // lblOpeningDate
            // 
            this.lblOpeningDate.AutoSize = true;
            this.lblOpeningDate.LabelLocation = 88;
            this.lblOpeningDate.Location = new System.Drawing.Point(42, 18);
            this.lblOpeningDate.Name = "lblOpeningDate";
            this.lblOpeningDate.Size = new System.Drawing.Size(47, 12);
            this.lblOpeningDate.TabIndex = 0;
            this.lblOpeningDate.Text = "开业日:";
            // 
            // pnlRentPanelContainer
            // 
            this.pnlRentPanelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRentPanelContainer.AutoScroll = true;
            this.pnlRentPanelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRentPanelContainer.Location = new System.Drawing.Point(0, 149);
            this.pnlRentPanelContainer.Name = "pnlRentPanelContainer";
            this.pnlRentPanelContainer.Size = new System.Drawing.Size(931, 488);
            this.pnlRentPanelContainer.TabIndex = 2;
            // 
            // cmbEntityName
            // 
            this.cmbEntityName.DataSource = this.bdsEntity;
            this.cmbEntityName.DisplayMember = "EntityName";
            this.cmbEntityName.FormattingEnabled = true;
            this.cmbEntityName.Location = new System.Drawing.Point(92, 3);
            this.cmbEntityName.Name = "cmbEntityName";
            this.cmbEntityName.Size = new System.Drawing.Size(339, 20);
            this.cmbEntityName.TabIndex = 0;
            this.cmbEntityName.ValueMember = "EntityID";
            this.cmbEntityName.SelectedIndexChanged += new System.EventHandler(this.cmbEntityName_SelectedIndexChanged);
            this.cmbEntityName.SelectionChangeCommitted += new System.EventHandler(this.cmbEntityName_SelectionChangeCommitted);
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.LabelLocation = 91;
            this.lblEntityName.Location = new System.Drawing.Point(33, 6);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(59, 12);
            this.lblEntityName.TabIndex = 0;
            this.lblEntityName.Text = "实体名称:";
            // 
            // btnAddGLAdjustment
            // 
            this.btnAddGLAdjustment.Image = global::MCD.RLPlanning.Client.Properties.Resources.编辑;
            this.btnAddGLAdjustment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddGLAdjustment.Location = new System.Drawing.Point(438, 2);
            this.btnAddGLAdjustment.Name = "btnAddGLAdjustment";
            this.btnAddGLAdjustment.Size = new System.Drawing.Size(138, 23);
            this.btnAddGLAdjustment.TabIndex = 3;
            this.btnAddGLAdjustment.Text = "录入GL调整凭证";
            this.btnAddGLAdjustment.UseVisualStyleBackColor = true;
            this.btnAddGLAdjustment.Click += new System.EventHandler(this.btnAddGLAdjustment_Click);
            // 
            // RentRulePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddGLAdjustment);
            this.Controls.Add(this.pnlRentPanelContainer);
            this.Controls.Add(this.grpEntityInfo);
            this.Controls.Add(this.cmbEntityName);
            this.Controls.Add(this.lblEntityName);
            this.Name = "RentRulePanel";
            this.Size = new System.Drawing.Size(934, 639);
            this.Load += new System.EventHandler(this.RentRulePanel_Load);
            this.grpEntityInfo.ResumeLayout(false);
            this.grpEntityInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntityInfoSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblEntityName;
        private MCD.Controls.UCComboBox cmbEntityName;
        private System.Windows.Forms.GroupBox grpEntityInfo;
        private MCD.Controls.UCLabel lblOpeningDate;
        private MCD.Controls.UCLabel lblTaxRate;
        private System.Windows.Forms.TextBox textBox1;
        private MCD.Controls.UCLabel lblMarginRemark;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private MCD.Controls.UCLabel lblMarginAmount;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private MCD.Controls.UCLabel lblYearSales;
        private MCD.Controls.UCLabel lblRentStartDate;
        private MCD.Controls.UCLabel ucLabel5;
        private System.Windows.Forms.FlowLayoutPanel pnlRentPanelContainer;
        private System.Windows.Forms.DateTimePicker dtpOpeningDate;
        private System.Windows.Forms.DateTimePicker dtpRentStartDate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.BindingSource bdsEntityInfoSetting;
        private System.Windows.Forms.BindingSource bdsEntity;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private MCD.Controls.UCLabel lblRentEndDate;
        private System.Windows.Forms.Button btnAddGLAdjustment;
    }
}
