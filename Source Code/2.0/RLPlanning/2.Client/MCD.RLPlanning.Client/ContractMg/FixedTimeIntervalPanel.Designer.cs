namespace MCD.RLPlanning.Client.ContractMg
{
    partial class FixedTimeIntervalPanel
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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.bdsFixedInterval = new System.Windows.Forms.BindingSource(this.components);
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblCyclePayable = new MCD.Controls.UCLabel();
            this.numPayable = new System.Windows.Forms.NumericUpDown();
            this.lblCycle = new MCD.Controls.UCLabel();
            this.cmbCycle = new MCD.Controls.UCComboBox();
            this.radRentTime = new System.Windows.Forms.RadioButton();
            this.radSolarTime = new System.Windows.Forms.RadioButton();
            this.picDeleteTimeSegment = new System.Windows.Forms.PictureBox();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            this.ucLabel3 = new MCD.Controls.UCLabel();
            this.ucLabel4 = new MCD.Controls.UCLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bdsFixedInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPayable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteTimeSegment)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedInterval, "StartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpStartDate.Location = new System.Drawing.Point(44, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(124, 21);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // bdsFixedInterval
            // 
            this.bdsFixedInterval.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.FixedTimeIntervalSettingEntity);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedInterval, "EndDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpEndDate.Location = new System.Drawing.Point(187, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(124, 21);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // lblCyclePayable
            // 
            this.lblCyclePayable.AutoSize = true;
            this.lblCyclePayable.LabelLocation = 411;
            this.lblCyclePayable.Location = new System.Drawing.Point(329, 7);
            this.lblCyclePayable.Name = "lblCyclePayable";
            this.lblCyclePayable.Size = new System.Drawing.Size(83, 12);
            this.lblCyclePayable.TabIndex = 3;
            this.lblCyclePayable.Text = "结算周期应付:";
            // 
            // numPayable
            // 
            this.numPayable.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsFixedInterval, "Amount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numPayable.DecimalPlaces = 2;
            this.numPayable.Location = new System.Drawing.Point(413, 4);
            this.numPayable.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numPayable.Name = "numPayable";
            this.numPayable.Size = new System.Drawing.Size(102, 21);
            this.numPayable.TabIndex = 2;
            this.numPayable.ThousandsSeparator = true;
            this.numPayable.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // lblCycle
            // 
            this.lblCycle.AutoSize = true;
            this.lblCycle.LabelLocation = 707;
            this.lblCycle.Location = new System.Drawing.Point(649, 7);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(59, 12);
            this.lblCycle.TabIndex = 5;
            this.lblCycle.Text = "结算周期:";
            this.lblCycle.Visible = false;
            // 
            // cmbCycle
            // 
            this.cmbCycle.FormattingEnabled = true;
            this.cmbCycle.Location = new System.Drawing.Point(713, 4);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Size = new System.Drawing.Size(82, 20);
            this.cmbCycle.TabIndex = 3;
            this.cmbCycle.Visible = false;
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            this.cmbCycle.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // radRentTime
            // 
            this.radRentTime.AutoSize = true;
            this.radRentTime.Checked = true;
            this.radRentTime.Location = new System.Drawing.Point(534, 6);
            this.radRentTime.Name = "radRentTime";
            this.radRentTime.Size = new System.Drawing.Size(47, 16);
            this.radRentTime.TabIndex = 4;
            this.radRentTime.TabStop = true;
            this.radRentTime.Text = "租赁";
            this.radRentTime.UseVisualStyleBackColor = true;
            this.radRentTime.Visible = false;
            this.radRentTime.CheckedChanged += new System.EventHandler(this.radRentTime_CheckedChanged);
            this.radRentTime.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // radSolarTime
            // 
            this.radSolarTime.AutoSize = true;
            this.radSolarTime.Location = new System.Drawing.Point(587, 6);
            this.radSolarTime.Name = "radSolarTime";
            this.radSolarTime.Size = new System.Drawing.Size(47, 16);
            this.radSolarTime.TabIndex = 5;
            this.radSolarTime.Text = "公历";
            this.radSolarTime.UseVisualStyleBackColor = true;
            this.radSolarTime.Visible = false;
            this.radSolarTime.CheckedChanged += new System.EventHandler(this.radRentTime_CheckedChanged);
            this.radSolarTime.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // picDeleteTimeSegment
            // 
            this.picDeleteTimeSegment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDeleteTimeSegment.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.picDeleteTimeSegment.Location = new System.Drawing.Point(4, 2);
            this.picDeleteTimeSegment.Name = "picDeleteTimeSegment";
            this.picDeleteTimeSegment.Size = new System.Drawing.Size(24, 22);
            this.picDeleteTimeSegment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDeleteTimeSegment.TabIndex = 8;
            this.picDeleteTimeSegment.TabStop = false;
            this.picDeleteTimeSegment.Click += new System.EventHandler(this.btnDeleteInterval_Click);
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(313, 9);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 12);
            this.ucLabel1.TabIndex = 9;
            this.ucLabel1.Text = "*";
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(169, 9);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 12);
            this.ucLabel2.TabIndex = 9;
            this.ucLabel2.Text = "*";
            // 
            // ucLabel3
            // 
            this.ucLabel3.AutoSize = true;
            this.ucLabel3.ForeColor = System.Drawing.Color.Red;
            this.ucLabel3.Location = new System.Drawing.Point(513, 9);
            this.ucLabel3.Name = "ucLabel3";
            this.ucLabel3.NeedLanguage = false;
            this.ucLabel3.Size = new System.Drawing.Size(11, 12);
            this.ucLabel3.TabIndex = 9;
            this.ucLabel3.Text = "*";
            // 
            // ucLabel4
            // 
            this.ucLabel4.AutoSize = true;
            this.ucLabel4.ForeColor = System.Drawing.Color.Red;
            this.ucLabel4.Location = new System.Drawing.Point(797, 9);
            this.ucLabel4.Name = "ucLabel4";
            this.ucLabel4.NeedLanguage = false;
            this.ucLabel4.Size = new System.Drawing.Size(11, 12);
            this.ucLabel4.TabIndex = 9;
            this.ucLabel4.Text = "*";
            this.ucLabel4.Visible = false;
            // 
            // FixedTimeIntervalPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picDeleteTimeSegment);
            this.Controls.Add(this.numPayable);
            this.Controls.Add(this.lblCyclePayable);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.ucLabel3);
            this.Controls.Add(this.ucLabel2);
            this.Controls.Add(this.ucLabel1);
            this.Controls.Add(this.radSolarTime);
            this.Controls.Add(this.radRentTime);
            this.Controls.Add(this.cmbCycle);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.ucLabel4);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FixedTimeIntervalPanel";
            this.Size = new System.Drawing.Size(810, 28);
            this.Load += new System.EventHandler(this.FixedRentTimeSegment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsFixedInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPayable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteTimeSegment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private MCD.Controls.UCLabel lblCyclePayable;
        private System.Windows.Forms.NumericUpDown numPayable;
        private MCD.Controls.UCLabel lblCycle;
        private MCD.Controls.UCComboBox cmbCycle;
        private System.Windows.Forms.RadioButton radRentTime;
        private System.Windows.Forms.RadioButton radSolarTime;
        private System.Windows.Forms.PictureBox picDeleteTimeSegment;
        private System.Windows.Forms.BindingSource bdsFixedInterval;
        private MCD.Controls.UCLabel ucLabel1;
        private MCD.Controls.UCLabel ucLabel2;
        private MCD.Controls.UCLabel ucLabel3;
        private MCD.Controls.UCLabel ucLabel4;
    }
}
