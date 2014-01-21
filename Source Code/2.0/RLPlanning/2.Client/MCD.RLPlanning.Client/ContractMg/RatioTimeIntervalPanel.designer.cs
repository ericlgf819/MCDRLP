namespace MCD.RLPlanning.Client.ContractMg
{
    partial class RatioTimeIntervalPanel
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
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.bdsRatioInterval = new System.Windows.Forms.BindingSource(this.components);
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlFormula = new System.Windows.Forms.FlowLayoutPanel();
            this.picDeleteTimeSegment = new System.Windows.Forms.PictureBox();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteTimeSegment)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsRatioInterval, "EndDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpEndDate.Location = new System.Drawing.Point(182, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(124, 20);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // bdsRatioInterval
            // 
            this.bdsRatioInterval.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.RatioTimeIntervalSettingEntity);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsRatioInterval, "StartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpStartDate.Location = new System.Drawing.Point(40, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(124, 20);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // pnlFormula
            // 
            this.pnlFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFormula.Location = new System.Drawing.Point(325, 0);
            this.pnlFormula.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFormula.Name = "pnlFormula";
            this.pnlFormula.Size = new System.Drawing.Size(555, 26);
            this.pnlFormula.TabIndex = 8;
            // 
            // picDeleteTimeSegment
            // 
            this.picDeleteTimeSegment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDeleteTimeSegment.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.picDeleteTimeSegment.Location = new System.Drawing.Point(3, 1);
            this.picDeleteTimeSegment.Name = "picDeleteTimeSegment";
            this.picDeleteTimeSegment.Size = new System.Drawing.Size(24, 24);
            this.picDeleteTimeSegment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDeleteTimeSegment.TabIndex = 9;
            this.picDeleteTimeSegment.TabStop = false;
            this.picDeleteTimeSegment.Click += new System.EventHandler(this.btnDeleteTimeSegment_Click);
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(308, 9);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 13);
            this.ucLabel1.TabIndex = 10;
            this.ucLabel1.Text = "*";
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(166, 9);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 13);
            this.ucLabel2.TabIndex = 10;
            this.ucLabel2.Text = "*";
            // 
            // RatioTimeIntervalPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.picDeleteTimeSegment);
            this.Controls.Add(this.pnlFormula);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.ucLabel2);
            this.Controls.Add(this.ucLabel1);
            this.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.Name = "RatioTimeIntervalPanel";
            this.Size = new System.Drawing.Size(880, 26);
            this.Load += new System.EventHandler(this.PercentRentTimeSegment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteTimeSegment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.FlowLayoutPanel pnlFormula;
        private System.Windows.Forms.PictureBox picDeleteTimeSegment;
        private System.Windows.Forms.BindingSource bdsRatioInterval;
        private MCD.Controls.UCLabel ucLabel1;
        private MCD.Controls.UCLabel ucLabel2;
    }
}
