namespace MCD.RLPlanning.Client.Workflow.Task.Controls
{
    partial class TSimpleCheck
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblReadStatus = new System.Windows.Forms.Label();
            this.cmbquestType = new System.Windows.Forms.ComboBox();
            this.cmbReadStatus = new System.Windows.Forms.ComboBox();
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.lblTaskType = new System.Windows.Forms.Label();
            this.lblQuestType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(786, 266);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(590, 37);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(121, 21);
            this.txtStoreNo.TabIndex = 9;
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Location = new System.Drawing.Point(89, 72);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(121, 20);
            this.cmbTaskType.TabIndex = 13;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(42, 40);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(41, 12);
            this.lblArea.TabIndex = 14;
            this.lblArea.Tag = "84";
            this.lblArea.Text = "区域：";
            // 
            // cmbCompany
            // 
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(290, 37);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(206, 20);
            this.cmbCompany.TabIndex = 16;
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(89, 37);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(121, 20);
            this.cmbArea.TabIndex = 17;
            this.cmbArea.SelectedIndexChanged += new System.EventHandler(this.cmbArea_SelectedIndexChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(241, 40);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(41, 12);
            this.lblCompany.TabIndex = 18;
            this.lblCompany.Text = "公司：";
            // 
            // lblReadStatus
            // 
            this.lblReadStatus.AutoSize = true;
            this.lblReadStatus.Location = new System.Drawing.Point(517, 75);
            this.lblReadStatus.Name = "lblReadStatus";
            this.lblReadStatus.Size = new System.Drawing.Size(65, 12);
            this.lblReadStatus.TabIndex = 22;
            this.lblReadStatus.Text = "阅读状态：";
            // 
            // cmbquestType
            // 
            this.cmbquestType.FormattingEnabled = true;
            this.cmbquestType.Location = new System.Drawing.Point(290, 72);
            this.cmbquestType.Name = "cmbquestType";
            this.cmbquestType.Size = new System.Drawing.Size(206, 20);
            this.cmbquestType.TabIndex = 21;
            // 
            // cmbReadStatus
            // 
            this.cmbReadStatus.FormattingEnabled = true;
            this.cmbReadStatus.Location = new System.Drawing.Point(590, 72);
            this.cmbReadStatus.Name = "cmbReadStatus";
            this.cmbReadStatus.Size = new System.Drawing.Size(121, 20);
            this.cmbReadStatus.TabIndex = 20;
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(517, 40);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(65, 12);
            this.lblStoreNo.TabIndex = 23;
            this.lblStoreNo.Text = "餐厅编号：";
            // 
            // lblTaskType
            // 
            this.lblTaskType.AutoSize = true;
            this.lblTaskType.Location = new System.Drawing.Point(18, 75);
            this.lblTaskType.Name = "lblTaskType";
            this.lblTaskType.Size = new System.Drawing.Size(65, 12);
            this.lblTaskType.TabIndex = 24;
            this.lblTaskType.Text = "任务类型：";
            // 
            // lblQuestType
            // 
            this.lblQuestType.AutoSize = true;
            this.lblQuestType.Location = new System.Drawing.Point(217, 75);
            this.lblQuestType.Name = "lblQuestType";
            this.lblQuestType.Size = new System.Drawing.Size(65, 12);
            this.lblQuestType.TabIndex = 25;
            this.lblQuestType.Text = "问题类型：";
            // 
            // TSimpleCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblQuestType);
            this.Controls.Add(this.lblTaskType);
            this.Controls.Add(this.lblStoreNo);
            this.Controls.Add(this.lblReadStatus);
            this.Controls.Add(this.cmbquestType);
            this.Controls.Add(this.cmbReadStatus);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.cmbArea);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.cmbTaskType);
            this.Controls.Add(this.txtStoreNo);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TSimpleCheck";
            this.Size = new System.Drawing.Size(795, 412);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.txtStoreNo, 0);
            this.Controls.SetChildIndex(this.cmbTaskType, 0);
            this.Controls.SetChildIndex(this.lblArea, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.cmbArea, 0);
            this.Controls.SetChildIndex(this.lblCompany, 0);
            this.Controls.SetChildIndex(this.cmbReadStatus, 0);
            this.Controls.SetChildIndex(this.cmbquestType, 0);
            this.Controls.SetChildIndex(this.lblReadStatus, 0);
            this.Controls.SetChildIndex(this.lblStoreNo, 0);
            this.Controls.SetChildIndex(this.lblTaskType, 0);
            this.Controls.SetChildIndex(this.lblQuestType, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblArea;
        protected System.Windows.Forms.ComboBox cmbArea;
        protected System.Windows.Forms.Label lblCompany;
        protected System.Windows.Forms.ComboBox cmbCompany;
        protected System.Windows.Forms.TextBox txtStoreNo;
        protected System.Windows.Forms.ComboBox cmbTaskType;
        protected System.Windows.Forms.Label lblReadStatus;
        protected System.Windows.Forms.ComboBox cmbquestType;
        protected System.Windows.Forms.ComboBox cmbReadStatus;
        protected System.Windows.Forms.Label lblStoreNo;
        protected System.Windows.Forms.Label lblTaskType;
        protected System.Windows.Forms.Label lblQuestType;
        protected System.Windows.Forms.DataGridView dataGridView1;

    }
}
