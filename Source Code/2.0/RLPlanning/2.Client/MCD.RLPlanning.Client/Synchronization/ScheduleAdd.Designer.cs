namespace MCD.RLPlanning.Client.Synchronization
{
    partial class ScheduleAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSycDate = new System.Windows.Forms.Label();
            this.dtSycDate = new System.Windows.Forms.DateTimePicker();
            this.lblIsCalculate = new System.Windows.Forms.Label();
            this.chkIsCalculate = new System.Windows.Forms.CheckBox();
            this.lblCalculateDate = new System.Windows.Forms.Label();
            this.dtCalculateEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblAddedBy = new System.Windows.Forms.Label();
            this.lblAddedDate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSycDetail = new System.Windows.Forms.Label();
            this.lblAddedByValue = new System.Windows.Forms.Label();
            this.lblAddedDateValue = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.txtSycDetail = new System.Windows.Forms.TextBox();
            this.lblX1 = new System.Windows.Forms.Label();
            this.lblSyncTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(93, 401);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(188, 401);
            // 
            // lblSycDate
            // 
            this.lblSycDate.AutoSize = true;
            this.lblSycDate.Location = new System.Drawing.Point(72, 13);
            this.lblSycDate.Name = "lblSycDate";
            this.lblSycDate.Size = new System.Drawing.Size(58, 13);
            this.lblSycDate.TabIndex = 78;
            this.lblSycDate.Text = "同步日期:";
            // 
            // dtSycDate
            // 
            this.dtSycDate.Location = new System.Drawing.Point(138, 14);
            this.dtSycDate.Name = "dtSycDate";
            this.dtSycDate.Size = new System.Drawing.Size(119, 20);
            this.dtSycDate.TabIndex = 79;
            // 
            // lblIsCalculate
            // 
            this.lblIsCalculate.AutoSize = true;
            this.lblIsCalculate.Location = new System.Drawing.Point(48, 49);
            this.lblIsCalculate.Name = "lblIsCalculate";
            this.lblIsCalculate.Size = new System.Drawing.Size(82, 13);
            this.lblIsCalculate.TabIndex = 80;
            this.lblIsCalculate.Text = "是否计算租金:";
            // 
            // chkIsCalculate
            // 
            this.chkIsCalculate.AutoSize = true;
            this.chkIsCalculate.Location = new System.Drawing.Point(138, 50);
            this.chkIsCalculate.Name = "chkIsCalculate";
            this.chkIsCalculate.Size = new System.Drawing.Size(218, 17);
            this.chkIsCalculate.TabIndex = 81;
            this.chkIsCalculate.Text = "在进行数据同步的同时进行租金计算";
            this.chkIsCalculate.UseVisualStyleBackColor = true;
            this.chkIsCalculate.CheckedChanged += new System.EventHandler(this.chkIsCalculate_CheckedChanged);
            // 
            // lblCalculateDate
            // 
            this.lblCalculateDate.AutoSize = true;
            this.lblCalculateDate.Location = new System.Drawing.Point(24, 85);
            this.lblCalculateDate.Name = "lblCalculateDate";
            this.lblCalculateDate.Size = new System.Drawing.Size(106, 13);
            this.lblCalculateDate.TabIndex = 83;
            this.lblCalculateDate.Text = "租金计算截止日期:";
            // 
            // dtCalculateEndDate
            // 
            this.dtCalculateEndDate.CustomFormat = "yyyy-MM";
            this.dtCalculateEndDate.Enabled = false;
            this.dtCalculateEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCalculateEndDate.Location = new System.Drawing.Point(138, 82);
            this.dtCalculateEndDate.Name = "dtCalculateEndDate";
            this.dtCalculateEndDate.Size = new System.Drawing.Size(119, 20);
            this.dtCalculateEndDate.TabIndex = 84;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(95, 114);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(34, 13);
            this.lblRemark.TabIndex = 85;
            this.lblRemark.Text = "备注:";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(138, 112);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(224, 82);
            this.txtRemark.TabIndex = 86;
            // 
            // lblAddedBy
            // 
            this.lblAddedBy.AutoSize = true;
            this.lblAddedBy.Location = new System.Drawing.Point(84, 209);
            this.lblAddedBy.Name = "lblAddedBy";
            this.lblAddedBy.Size = new System.Drawing.Size(46, 13);
            this.lblAddedBy.TabIndex = 87;
            this.lblAddedBy.Text = "设置人:";
            // 
            // lblAddedDate
            // 
            this.lblAddedDate.AutoSize = true;
            this.lblAddedDate.Location = new System.Drawing.Point(72, 238);
            this.lblAddedDate.Name = "lblAddedDate";
            this.lblAddedDate.Size = new System.Drawing.Size(58, 13);
            this.lblAddedDate.TabIndex = 88;
            this.lblAddedDate.Text = "设置日期:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(96, 271);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 89;
            this.lblStatus.Text = "状态:";
            // 
            // lblSycDetail
            // 
            this.lblSycDetail.AutoSize = true;
            this.lblSycDetail.Location = new System.Drawing.Point(73, 305);
            this.lblSycDetail.Name = "lblSycDetail";
            this.lblSycDetail.Size = new System.Drawing.Size(58, 13);
            this.lblSycDetail.TabIndex = 90;
            this.lblSycDetail.Text = "同步日志:";
            // 
            // lblAddedByValue
            // 
            this.lblAddedByValue.AutoSize = true;
            this.lblAddedByValue.Location = new System.Drawing.Point(138, 209);
            this.lblAddedByValue.Name = "lblAddedByValue";
            this.lblAddedByValue.Size = new System.Drawing.Size(53, 13);
            this.lblAddedByValue.TabIndex = 91;
            this.lblAddedByValue.Text = "Added By";
            // 
            // lblAddedDateValue
            // 
            this.lblAddedDateValue.AutoSize = true;
            this.lblAddedDateValue.Location = new System.Drawing.Point(138, 238);
            this.lblAddedDateValue.Name = "lblAddedDateValue";
            this.lblAddedDateValue.Size = new System.Drawing.Size(64, 13);
            this.lblAddedDateValue.TabIndex = 92;
            this.lblAddedDateValue.Text = "Added Date";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Location = new System.Drawing.Point(138, 271);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(37, 13);
            this.lblStatusValue.TabIndex = 93;
            this.lblStatusValue.Text = "Status";
            // 
            // txtSycDetail
            // 
            this.txtSycDetail.Enabled = false;
            this.txtSycDetail.Location = new System.Drawing.Point(138, 303);
            this.txtSycDetail.Multiline = true;
            this.txtSycDetail.Name = "txtSycDetail";
            this.txtSycDetail.Size = new System.Drawing.Size(224, 82);
            this.txtSycDetail.TabIndex = 94;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.ForeColor = System.Drawing.Color.Red;
            this.lblX1.Location = new System.Drawing.Point(297, 18);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(11, 13);
            this.lblX1.TabIndex = 95;
            this.lblX1.Text = "*";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSyncTime
            // 
            this.lblSyncTime.AutoSize = true;
            this.lblSyncTime.Location = new System.Drawing.Point(262, 17);
            this.lblSyncTime.Name = "lblSyncTime";
            this.lblSyncTime.Size = new System.Drawing.Size(34, 13);
            this.lblSyncTime.TabIndex = 96;
            this.lblSyncTime.Text = "00:00";
            // 
            // ScheduleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 447);
            this.Controls.Add(this.lblSyncTime);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.txtSycDetail);
            this.Controls.Add(this.lblStatusValue);
            this.Controls.Add(this.lblAddedDateValue);
            this.Controls.Add(this.lblAddedByValue);
            this.Controls.Add(this.lblSycDetail);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.lblCalculateDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblAddedDate);
            this.Controls.Add(this.lblAddedBy);
            this.Controls.Add(this.lblIsCalculate);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.dtCalculateEndDate);
            this.Controls.Add(this.lblSycDate);
            this.Controls.Add(this.chkIsCalculate);
            this.Controls.Add(this.dtSycDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ScheduleAdd";
            this.Text = "同步计划";
            this.Load += new System.EventHandler(this.ScheduleAdd_Load);
            this.Controls.SetChildIndex(this.dtSycDate, 0);
            this.Controls.SetChildIndex(this.chkIsCalculate, 0);
            this.Controls.SetChildIndex(this.lblSycDate, 0);
            this.Controls.SetChildIndex(this.dtCalculateEndDate, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.lblIsCalculate, 0);
            this.Controls.SetChildIndex(this.lblAddedBy, 0);
            this.Controls.SetChildIndex(this.lblAddedDate, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.lblCalculateDate, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.lblSycDetail, 0);
            this.Controls.SetChildIndex(this.lblAddedByValue, 0);
            this.Controls.SetChildIndex(this.lblAddedDateValue, 0);
            this.Controls.SetChildIndex(this.lblStatusValue, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.txtSycDetail, 0);
            this.Controls.SetChildIndex(this.lblX1, 0);
            this.Controls.SetChildIndex(this.lblSyncTime, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSycDate;
        private System.Windows.Forms.DateTimePicker dtSycDate;
        private System.Windows.Forms.Label lblIsCalculate;
        private System.Windows.Forms.CheckBox chkIsCalculate;
        private System.Windows.Forms.Label lblCalculateDate;
        private System.Windows.Forms.DateTimePicker dtCalculateEndDate;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblAddedBy;
        private System.Windows.Forms.Label lblAddedDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSycDetail;
        private System.Windows.Forms.Label lblAddedByValue;
        private System.Windows.Forms.Label lblAddedDateValue;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.TextBox txtSycDetail;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.Label lblSyncTime;
    }
}