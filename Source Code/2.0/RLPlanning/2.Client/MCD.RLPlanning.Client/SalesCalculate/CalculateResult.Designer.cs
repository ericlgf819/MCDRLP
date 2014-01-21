namespace MCD.RLPlanning.Client.SalesCalculate
{
    partial class CalculateResult
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
            this.dgvCalculateResult = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculateResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCalculateResult
            // 
            this.dgvCalculateResult.AllowUserToAddRows = false;
            this.dgvCalculateResult.AllowUserToDeleteRows = false;
            this.dgvCalculateResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculateResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalculateResult.Location = new System.Drawing.Point(0, 0);
            this.dgvCalculateResult.Name = "dgvCalculateResult";
            this.dgvCalculateResult.ReadOnly = true;
            this.dgvCalculateResult.Size = new System.Drawing.Size(709, 415);
            this.dgvCalculateResult.TabIndex = 1;
            this.dgvCalculateResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalculateResult_CellContentClick);
            // 
            // CalculateResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 415);
            this.Controls.Add(this.dgvCalculateResult);
            this.Name = "CalculateResult";
            this.Text = "租金计算结果";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalculateResult_FormClosing);
            this.Load += new System.EventHandler(this.CalculateResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculateResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCalculateResult;
    }
}