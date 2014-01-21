namespace MCD.RLPlanning.Client.Master
{
    partial class StoreMultiCopy
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
        /// the contents of this method with the code Addor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.lblCopyCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(92, 124);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(187, 124);
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(160, 37);
            this.nudCount.Maximum = new decimal(new int[] {
            268435455,
            1042612833,
            542101086,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(120, 20);
            this.nudCount.TabIndex = 0;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCopyCount
            // 
            this.lblCopyCount.AutoSize = true;
            this.lblCopyCount.Location = new System.Drawing.Point(68, 39);
            this.lblCopyCount.Name = "lblCopyCount";
            this.lblCopyCount.Size = new System.Drawing.Size(82, 13);
            this.lblCopyCount.TabIndex = 1;
            this.lblCopyCount.Text = "批量复制个数:";
            // 
            // StoreMultiCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 182);
            this.Controls.Add(this.lblCopyCount);
            this.Controls.Add(this.nudCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StoreMultiCopy";
            this.Text = "批量复制餐厅";
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.nudCount, 0);
            this.Controls.SetChildIndex(this.lblCopyCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label lblCopyCount;

     
    }
}