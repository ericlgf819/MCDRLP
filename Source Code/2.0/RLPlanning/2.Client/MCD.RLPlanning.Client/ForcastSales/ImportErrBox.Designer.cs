namespace MCD.RLPlanning.Client.ForcastSales
{
    partial class ImportErrBox
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
            this.dgvErr = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(498, 339);
            this.btnSave.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(602, 339);
            this.btnCancel.Visible = false;
            // 
            // dgvErr
            // 
            this.dgvErr.AllowUserToAddRows = false;
            this.dgvErr.AllowUserToDeleteRows = false;
            this.dgvErr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErr.Location = new System.Drawing.Point(13, 13);
            this.dgvErr.Name = "dgvErr";
            this.dgvErr.ReadOnly = true;
            this.dgvErr.Size = new System.Drawing.Size(676, 356);
            this.dgvErr.TabIndex = 78;
            // 
            // ImportErrBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 381);
            this.Controls.Add(this.dgvErr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportErrBox";
            this.Text = "错误提示";
            this.Load += new System.EventHandler(this.ImportErrBox_Load);
            this.Controls.SetChildIndex(this.dgvErr, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvErr;
    }
}