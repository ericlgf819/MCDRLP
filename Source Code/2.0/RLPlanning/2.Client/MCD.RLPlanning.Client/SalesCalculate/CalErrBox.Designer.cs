namespace MCD.RLPlanning.Client.SalesCalculate
{
    partial class CalErrBox
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
            this.dvErrInfo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dvErrInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(319, 297);
            this.btnSave.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(414, 297);
            this.btnCancel.Visible = false;
            // 
            // dvErrInfo
            // 
            this.dvErrInfo.AllowUserToAddRows = false;
            this.dvErrInfo.AllowUserToDeleteRows = false;
            this.dvErrInfo.AllowUserToResizeColumns = false;
            this.dvErrInfo.AllowUserToResizeRows = false;
            this.dvErrInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvErrInfo.Location = new System.Drawing.Point(12, 12);
            this.dvErrInfo.Name = "dvErrInfo";
            this.dvErrInfo.Size = new System.Drawing.Size(491, 315);
            this.dvErrInfo.TabIndex = 78;
            // 
            // CalErrBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 338);
            this.Controls.Add(this.dvErrInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalErrBox";
            this.Text = "CalErrBox";
            this.Load += new System.EventHandler(this.CalErrBox_Load);
            this.Controls.SetChildIndex(this.dvErrInfo, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dvErrInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dvErrInfo;
    }
}