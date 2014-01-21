namespace MCD.RLPlanning.Client.ForcastSales
{
    partial class KeyInBox
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
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.lblStoreNameTile = new System.Windows.Forms.Label();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.btnAddSales = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(933, 465);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1028, 465);
            // 
            // dgvSales
            // 
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgvSales.Location = new System.Drawing.Point(12, 57);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.RowTemplate.Height = 23;
            this.dgvSales.Size = new System.Drawing.Size(1105, 402);
            this.dgvSales.TabIndex = 5;
            this.dgvSales.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSales_CellBeginEdit);
            this.dgvSales.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_CellEndEdit);
            this.dgvSales.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSales_RowsAdded);
            // 
            // lblStoreNameTile
            // 
            this.lblStoreNameTile.AutoSize = true;
            this.lblStoreNameTile.Location = new System.Drawing.Point(12, 26);
            this.lblStoreNameTile.Name = "lblStoreNameTile";
            this.lblStoreNameTile.Size = new System.Drawing.Size(65, 12);
            this.lblStoreNameTile.TabIndex = 6;
            this.lblStoreNameTile.Text = "餐厅名称：";
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Location = new System.Drawing.Point(85, 26);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(59, 12);
            this.lblStoreName.TabIndex = 7;
            this.lblStoreName.Text = "StoreName";
            // 
            // btnAddSales
            // 
            this.btnAddSales.Location = new System.Drawing.Point(1028, 18);
            this.btnAddSales.Name = "btnAddSales";
            this.btnAddSales.Size = new System.Drawing.Size(89, 28);
            this.btnAddSales.TabIndex = 8;
            this.btnAddSales.Text = "添加Sales";
            this.btnAddSales.UseVisualStyleBackColor = true;
            this.btnAddSales.Click += new System.EventHandler(this.btnAddSales_Click);
            // 
            // KeyInBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 504);
            this.Controls.Add(this.btnAddSales);
            this.Controls.Add(this.lblStoreName);
            this.Controls.Add(this.lblStoreNameTile);
            this.Controls.Add(this.dgvSales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyInBox";
            this.Text = "KeyInBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyInBox_FormClosing);
            this.Load += new System.EventHandler(this.KeyInBox_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.dgvSales, 0);
            this.Controls.SetChildIndex(this.lblStoreNameTile, 0);
            this.Controls.SetChildIndex(this.lblStoreName, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnAddSales, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.Label lblStoreNameTile;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.Button btnAddSales;

    }
}
