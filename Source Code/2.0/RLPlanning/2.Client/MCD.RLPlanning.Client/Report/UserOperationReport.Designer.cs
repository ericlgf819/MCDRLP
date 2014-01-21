namespace MCD.RLPlanning.Client.Report
{
    partial class UserOperationReport
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
            this.lblOperationTo = new MCD.Controls.UCLabel();
            this.lblOperationFrom = new MCD.Controls.UCLabel();
            this.lblOperationDate = new MCD.Controls.UCLabel();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblOperatingfunctions = new MCD.Controls.UCLabel();
            this.chkAdd = new System.Windows.Forms.CheckBox();
            this.chkEdit = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOperationTo
            // 
            this.lblOperationTo.AutoSize = true;
            this.lblOperationTo.Location = new System.Drawing.Point(296, 45);
            this.lblOperationTo.Name = "lblOperationTo";
            this.lblOperationTo.Size = new System.Drawing.Size(19, 13);
            this.lblOperationTo.TabIndex = 12;
            this.lblOperationTo.Text = "到";
            // 
            // lblOperationFrom
            // 
            this.lblOperationFrom.AutoSize = true;
            this.lblOperationFrom.Location = new System.Drawing.Point(155, 44);
            this.lblOperationFrom.Name = "lblOperationFrom";
            this.lblOperationFrom.Size = new System.Drawing.Size(19, 13);
            this.lblOperationFrom.TabIndex = 11;
            this.lblOperationFrom.Text = "从";
            // 
            // lblOperationDate
            // 
            this.lblOperationDate.AutoSize = true;
            this.lblOperationDate.LabelLocation = 152;
            this.lblOperationDate.Location = new System.Drawing.Point(75, 45);
            this.lblOperationDate.Name = "lblOperationDate";
            this.lblOperationDate.Size = new System.Drawing.Size(58, 13);
            this.lblOperationDate.TabIndex = 10;
            this.lblOperationDate.Text = "操作日期:";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(179, 41);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(111, 20);
            this.dtStartDate.TabIndex = 15;
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(321, 41);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(111, 20);
            this.dtEndDate.TabIndex = 16;
            // 
            // lblOperatingfunctions
            // 
            this.lblOperatingfunctions.AutoSize = true;
            this.lblOperatingfunctions.LabelLocation = 152;
            this.lblOperatingfunctions.Location = new System.Drawing.Point(75, 84);
            this.lblOperatingfunctions.Name = "lblOperatingfunctions";
            this.lblOperatingfunctions.Size = new System.Drawing.Size(58, 13);
            this.lblOperatingfunctions.TabIndex = 17;
            this.lblOperatingfunctions.Text = "操作行为:";
            // 
            // chkAdd
            // 
            this.chkAdd.AutoSize = true;
            this.chkAdd.Checked = true;
            this.chkAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdd.Location = new System.Drawing.Point(160, 84);
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.Size = new System.Drawing.Size(50, 17);
            this.chkAdd.TabIndex = 18;
            this.chkAdd.Text = "新增";
            this.chkAdd.UseVisualStyleBackColor = true;
            // 
            // chkEdit
            // 
            this.chkEdit.AutoSize = true;
            this.chkEdit.Checked = true;
            this.chkEdit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEdit.Location = new System.Drawing.Point(242, 85);
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(50, 17);
            this.chkEdit.TabIndex = 19;
            this.chkEdit.Text = "编辑";
            this.chkEdit.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Checked = true;
            this.chkDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelete.Location = new System.Drawing.Point(324, 85);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(50, 17);
            this.chkDelete.TabIndex = 20;
            this.chkDelete.Text = "删除";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(199, 161);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 39);
            this.btnExport.TabIndex = 21;
            this.btnExport.Text = "导出日志";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // UserOperationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 346);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.chkEdit);
            this.Controls.Add(this.chkAdd);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.lblOperatingfunctions);
            this.Controls.Add(this.lblOperationTo);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.lblOperationDate);
            this.Controls.Add(this.lblOperationFrom);
            this.Name = "UserOperationReport";
            this.Text = "用户操作日志报表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblOperationTo;
        private MCD.Controls.UCLabel lblOperationFrom;
        private MCD.Controls.UCLabel lblOperationDate;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private MCD.Controls.UCLabel lblOperatingfunctions;
        private System.Windows.Forms.CheckBox chkAdd;
        private System.Windows.Forms.CheckBox chkEdit;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.Button btnExport;
    }
}