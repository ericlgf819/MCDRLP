namespace MCD.RLPlanning.Client.Setting
{
    partial class SystemParameterInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnTestSMTP = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.txtParamCode = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblValue = new MCD.Controls.UCLabel();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.txtParamName = new System.Windows.Forms.TextBox();
            this.lblRemark = new MCD.Controls.UCLabel();
            this.lblSettingItem = new MCD.Controls.UCLabel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnTestSMTP,
            this.btnPreview,
            this.btnExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(768, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::MCD.RLPlanning.Client.Properties.Resources.保存;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTestSMTP
            // 
            this.btnTestSMTP.Image = global::MCD.RLPlanning.Client.Properties.Resources.编辑;
            this.btnTestSMTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTestSMTP.Name = "btnTestSMTP";
            this.btnTestSMTP.Size = new System.Drawing.Size(111, 22);
            this.btnTestSMTP.Text = "测试邮件服务器";
            this.btnTestSMTP.Click += new System.EventHandler(this.btnTestSMTP_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::MCD.RLPlanning.Client.Properties.Resources.打印预览;
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 22);
            this.btnPreview.Text = "打印预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::MCD.RLPlanning.Client.Properties.Resources.导出数据;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(51, 22);
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.txtParamCode);
            this.pnlTitle.Controls.Add(this.txtRemark);
            this.pnlTitle.Controls.Add(this.lblValue);
            this.pnlTitle.Controls.Add(this.txtParamValue);
            this.pnlTitle.Controls.Add(this.txtParamName);
            this.pnlTitle.Controls.Add(this.lblRemark);
            this.pnlTitle.Controls.Add(this.lblSettingItem);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 25);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(768, 183);
            this.pnlTitle.TabIndex = 10;
            // 
            // txtParamCode
            // 
            this.txtParamCode.Location = new System.Drawing.Point(493, 20);
            this.txtParamCode.MaxLength = 31;
            this.txtParamCode.Name = "txtParamCode";
            this.txtParamCode.Size = new System.Drawing.Size(149, 20);
            this.txtParamCode.TabIndex = 10;
            this.txtParamCode.Visible = false;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(111, 121);
            this.txtRemark.MaxLength = 2000;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(614, 55);
            this.txtRemark.TabIndex = 9;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(77, 47);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 8;
            this.lblValue.Text = "值： ";
            // 
            // txtParamValue
            // 
            this.txtParamValue.Location = new System.Drawing.Point(111, 43);
            this.txtParamValue.MaxLength = 2000;
            this.txtParamValue.Multiline = true;
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParamValue.Size = new System.Drawing.Size(614, 71);
            this.txtParamValue.TabIndex = 7;
            // 
            // txtParamName
            // 
            this.txtParamName.Location = new System.Drawing.Point(111, 14);
            this.txtParamName.MaxLength = 31;
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.ReadOnly = true;
            this.txtParamName.Size = new System.Drawing.Size(299, 20);
            this.txtParamName.TabIndex = 6;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(64, 125);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(46, 13);
            this.lblRemark.TabIndex = 5;
            this.lblRemark.Text = "备注： ";
            // 
            // lblSettingItem
            // 
            this.lblSettingItem.AutoSize = true;
            this.lblSettingItem.Location = new System.Drawing.Point(54, 20);
            this.lblSettingItem.Name = "lblSettingItem";
            this.lblSettingItem.Size = new System.Drawing.Size(55, 13);
            this.lblSettingItem.TabIndex = 4;
            this.lblSettingItem.Text = "配置项：";
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 208);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(768, 512);
            this.dgvList.TabIndex = 11;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // SystemParameterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 720);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SystemParameterInfo";
            this.Text = "系统参数设置";
            this.Load += new System.EventHandler(this.SystemParameterInfo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton btnSave;
        public System.Windows.Forms.ToolStripButton btnTestSMTP;
        public System.Windows.Forms.ToolStripButton btnPreview;
        public System.Windows.Forms.ToolStripButton btnExport;
        public System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TextBox txtParamValue;
        private System.Windows.Forms.TextBox txtParamName;
        private MCD.Controls.UCLabel lblRemark;
        private MCD.Controls.UCLabel lblSettingItem;
        public System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TextBox txtParamCode;
        private System.Windows.Forms.TextBox txtRemark;
        private MCD.Controls.UCLabel lblValue;

    }
}