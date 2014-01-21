namespace MCD.RLPlanning.Client.SalesCalculate
{
    partial class CalculateDate
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
            this.lblCalculateDate = new System.Windows.Forms.Label();
            this.dtpOpenDateValue = new System.Windows.Forms.DateTimePicker();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCalculateDate
            // 
            this.lblCalculateDate.AutoSize = true;
            this.lblCalculateDate.Location = new System.Drawing.Point(34, 38);
            this.lblCalculateDate.Name = "lblCalculateDate";
            this.lblCalculateDate.Size = new System.Drawing.Size(58, 13);
            this.lblCalculateDate.TabIndex = 1;
            this.lblCalculateDate.Text = "截止日期:";
            // 
            // dtpOpenDateValue
            // 
            this.dtpOpenDateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOpenDateValue.Location = new System.Drawing.Point(98, 37);
            this.dtpOpenDateValue.Name = "dtpOpenDateValue";
            this.dtpOpenDateValue.Size = new System.Drawing.Size(125, 20);
            this.dtpOpenDateValue.TabIndex = 11;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(108, 101);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 12;
            this.btnCalculate.Text = "开始计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // CalculateDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 164);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.dtpOpenDateValue);
            this.Controls.Add(this.lblCalculateDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculateDate";
            this.Text = "租金计算截止日期";
            this.Load += new System.EventHandler(this.CalculateDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCalculateDate;
        private System.Windows.Forms.DateTimePicker dtpOpenDateValue;
        private System.Windows.Forms.Button btnCalculate;
    }
}