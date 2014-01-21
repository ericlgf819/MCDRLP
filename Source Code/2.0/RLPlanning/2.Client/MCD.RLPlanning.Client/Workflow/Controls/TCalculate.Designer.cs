namespace MCD.RLPlanning.Client.Workflow.Controls
{
    partial class TCalculate
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvCalculate = new System.Windows.Forms.DataGridView();
            this.lbStoreNo = new System.Windows.Forms.Label();
            this.tbStoreNo = new System.Windows.Forms.TextBox();
            this.lbStoreName = new System.Windows.Forms.Label();
            this.tbStoreName = new System.Windows.Forms.TextBox();
            this.lbKioskNo = new System.Windows.Forms.Label();
            this.tbKioskNo = new System.Windows.Forms.TextBox();
            this.lbKioskName = new System.Windows.Forms.Label();
            this.tbKioskName = new System.Windows.Forms.TextBox();
            this.lblSelectedCountLabel = new System.Windows.Forms.Label();
            this.lblSelectedCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dvCalculate)).BeginInit();
            this.SuspendLayout();
            // 
            // dvCalculate
            // 
            this.dvCalculate.AllowUserToAddRows = false;
            this.dvCalculate.AllowUserToDeleteRows = false;
            this.dvCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dvCalculate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvCalculate.Location = new System.Drawing.Point(13, 75);
            this.dvCalculate.MultiSelect = false;
            this.dvCalculate.Name = "dvCalculate";
            this.dvCalculate.ReadOnly = true;
            this.dvCalculate.RowTemplate.Height = 23;
            this.dvCalculate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvCalculate.Size = new System.Drawing.Size(814, 315);
            this.dvCalculate.TabIndex = 1;
            this.dvCalculate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvCalculate_CellContentClick);
            this.dvCalculate.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dvCalculate_CellPainting);
            // 
            // lbStoreNo
            // 
            this.lbStoreNo.AutoSize = true;
            this.lbStoreNo.Location = new System.Drawing.Point(24, 33);
            this.lbStoreNo.Name = "lbStoreNo";
            this.lbStoreNo.Size = new System.Drawing.Size(65, 12);
            this.lbStoreNo.TabIndex = 2;
            this.lbStoreNo.Text = "餐厅编号：";
            // 
            // tbStoreNo
            // 
            this.tbStoreNo.Location = new System.Drawing.Point(98, 33);
            this.tbStoreNo.Name = "tbStoreNo";
            this.tbStoreNo.Size = new System.Drawing.Size(100, 21);
            this.tbStoreNo.TabIndex = 3;
            // 
            // lbStoreName
            // 
            this.lbStoreName.AutoSize = true;
            this.lbStoreName.Location = new System.Drawing.Point(214, 33);
            this.lbStoreName.Name = "lbStoreName";
            this.lbStoreName.Size = new System.Drawing.Size(65, 12);
            this.lbStoreName.TabIndex = 4;
            this.lbStoreName.Text = "餐厅名称：";
            // 
            // tbStoreName
            // 
            this.tbStoreName.Location = new System.Drawing.Point(288, 33);
            this.tbStoreName.Name = "tbStoreName";
            this.tbStoreName.Size = new System.Drawing.Size(100, 21);
            this.tbStoreName.TabIndex = 5;
            // 
            // lbKioskNo
            // 
            this.lbKioskNo.AutoSize = true;
            this.lbKioskNo.Location = new System.Drawing.Point(437, 0);
            this.lbKioskNo.Name = "lbKioskNo";
            this.lbKioskNo.Size = new System.Drawing.Size(77, 12);
            this.lbKioskNo.TabIndex = 6;
            this.lbKioskNo.Text = "甜品店编号：";
            this.lbKioskNo.Visible = false;
            // 
            // tbKioskNo
            // 
            this.tbKioskNo.Location = new System.Drawing.Point(522, -3);
            this.tbKioskNo.Name = "tbKioskNo";
            this.tbKioskNo.Size = new System.Drawing.Size(100, 21);
            this.tbKioskNo.TabIndex = 7;
            this.tbKioskNo.Visible = false;
            // 
            // lbKioskName
            // 
            this.lbKioskName.AutoSize = true;
            this.lbKioskName.Location = new System.Drawing.Point(629, 0);
            this.lbKioskName.Name = "lbKioskName";
            this.lbKioskName.Size = new System.Drawing.Size(77, 12);
            this.lbKioskName.TabIndex = 8;
            this.lbKioskName.Text = "甜品店名称：";
            this.lbKioskName.Visible = false;
            // 
            // tbKioskName
            // 
            this.tbKioskName.Location = new System.Drawing.Point(714, -3);
            this.tbKioskName.Name = "tbKioskName";
            this.tbKioskName.Size = new System.Drawing.Size(100, 21);
            this.tbKioskName.TabIndex = 9;
            this.tbKioskName.Visible = false;
            // 
            // lblSelectedCountLabel
            // 
            this.lblSelectedCountLabel.AutoSize = true;
            this.lblSelectedCountLabel.Location = new System.Drawing.Point(11, 60);
            this.lblSelectedCountLabel.Name = "lblSelectedCountLabel";
            this.lblSelectedCountLabel.Size = new System.Drawing.Size(35, 12);
            this.lblSelectedCountLabel.TabIndex = 10;
            this.lblSelectedCountLabel.Text = "已选:";
            // 
            // lblSelectedCount
            // 
            this.lblSelectedCount.AutoSize = true;
            this.lblSelectedCount.Location = new System.Drawing.Point(53, 60);
            this.lblSelectedCount.Name = "lblSelectedCount";
            this.lblSelectedCount.Size = new System.Drawing.Size(29, 12);
            this.lblSelectedCount.TabIndex = 11;
            this.lblSelectedCount.Text = "0/50";
            // 
            // TCalculate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSelectedCount);
            this.Controls.Add(this.lblSelectedCountLabel);
            this.Controls.Add(this.tbKioskName);
            this.Controls.Add(this.lbKioskName);
            this.Controls.Add(this.tbKioskNo);
            this.Controls.Add(this.lbKioskNo);
            this.Controls.Add(this.tbStoreName);
            this.Controls.Add(this.lbStoreName);
            this.Controls.Add(this.tbStoreNo);
            this.Controls.Add(this.lbStoreNo);
            this.Controls.Add(this.dvCalculate);
            this.Name = "TCalculate";
            this.Load += new System.EventHandler(this.TCalculate_Load);
            this.Controls.SetChildIndex(this.dvCalculate, 0);
            this.Controls.SetChildIndex(this.lbStoreNo, 0);
            this.Controls.SetChildIndex(this.tbStoreNo, 0);
            this.Controls.SetChildIndex(this.lbStoreName, 0);
            this.Controls.SetChildIndex(this.tbStoreName, 0);
            this.Controls.SetChildIndex(this.lbKioskNo, 0);
            this.Controls.SetChildIndex(this.tbKioskNo, 0);
            this.Controls.SetChildIndex(this.lbKioskName, 0);
            this.Controls.SetChildIndex(this.tbKioskName, 0);
            this.Controls.SetChildIndex(this.lblSelectedCountLabel, 0);
            this.Controls.SetChildIndex(this.lblSelectedCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dvCalculate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dvCalculate;
        private System.Windows.Forms.Label lbStoreNo;
        private System.Windows.Forms.TextBox tbStoreNo;
        private System.Windows.Forms.Label lbStoreName;
        private System.Windows.Forms.TextBox tbStoreName;
        private System.Windows.Forms.Label lbKioskNo;
        private System.Windows.Forms.TextBox tbKioskNo;
        private System.Windows.Forms.Label lbKioskName;
        private System.Windows.Forms.TextBox tbKioskName;
        private System.Windows.Forms.Label lblSelectedCountLabel;
        private System.Windows.Forms.Label lblSelectedCount;
    }
}
