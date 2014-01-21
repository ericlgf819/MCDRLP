namespace MCD.RLPlanning.Client
{
    partial class BasePhase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasePhase));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnCreateContract = new System.Windows.Forms.ToolStripButton();
            this.btnKeyInSales = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnMultiCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.plPager = new System.Windows.Forms.Panel();
            this.winfrmPager = new MCD.RLPlanning.Client.Common.WinfrmPager();
            this.toolStrip1.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.plPager.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelect,
            this.btnReset,
            this.btnNew,
            this.btnDelete,
            this.btnCopy,
            this.btnMultiCopy,
            this.btnCreateContract,
            this.btnKeyInSales,
            this.btnPreview,
            this.btnExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(738, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSelect
            // 
            this.btnSelect.Image = global::MCD.RLPlanning.Client.Properties.Resources.查询;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(54, 22);
            this.btnSelect.Text = "查找 ";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(51, 22);
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnNew
            // 
            this.btnNew.AccessibleDescription = "";
            this.btnNew.Image = global::MCD.RLPlanning.Client.Properties.Resources.新建;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(51, 22);
            this.btnNew.Text = "新增";
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(51, 22);
            this.btnDelete.Text = "删除";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateContract
            // 
            this.btnCreateContract.Image = global::MCD.RLPlanning.Client.Properties.Resources.新建;
            this.btnCreateContract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateContract.Name = "btnCreateContract";
            this.btnCreateContract.Size = new System.Drawing.Size(75, 22);
            this.btnCreateContract.Text = "新建合同";
            this.btnCreateContract.Visible = false;
            this.btnCreateContract.Click += new System.EventHandler(this.btnCreateContract_Click);
            // 
            // btnKeyInSales
            // 
            this.btnKeyInSales.Image = global::MCD.RLPlanning.Client.Properties.Resources.menu_合同管理;
            this.btnKeyInSales.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKeyInSales.Name = "btnKeyInSales";
            this.btnKeyInSales.Size = new System.Drawing.Size(123, 22);
            this.btnKeyInSales.Text = "录入预测销售数据";
            this.btnKeyInSales.Visible = false;
            this.btnKeyInSales.Click += new System.EventHandler(this.btnKeyInSales_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::MCD.RLPlanning.Client.Properties.Resources.复制;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(51, 22);
            this.btnCopy.Text = "复制";
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnMultiCopy
            // 
            this.btnMultiCopy.Image = global::MCD.RLPlanning.Client.Properties.Resources.批量复制;
            this.btnMultiCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMultiCopy.Name = "btnMultiCopy";
            this.btnMultiCopy.Size = new System.Drawing.Size(75, 22);
            this.btnMultiCopy.Text = "批量复制";
            this.btnMultiCopy.Visible = false;
            this.btnMultiCopy.Click += new System.EventHandler(this.btnMultiCopy_Click);
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
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 25);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(738, 70);
            this.pnlTitle.TabIndex = 1;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.dgvList);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 95);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(738, 355);
            this.pnlBody.TabIndex = 2;
            // 
            // dgvList
            // 
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(738, 355);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.SelectionChanged += new System.EventHandler(this.dgvList_SelectionChanged);
            // 
            // plPager
            // 
            this.plPager.Controls.Add(this.winfrmPager);
            this.plPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plPager.Location = new System.Drawing.Point(0, 427);
            this.plPager.Name = "plPager";
            this.plPager.Size = new System.Drawing.Size(738, 23);
            this.plPager.TabIndex = 3;
            this.plPager.Visible = false;
            // 
            // winfrmPager
            // 
            this.winfrmPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winfrmPager.Location = new System.Drawing.Point(0, 0);
            this.winfrmPager.Name = "winfrmPager";
            this.winfrmPager.PageIndex = -1;
            this.winfrmPager.PageSize = 50;
            this.winfrmPager.RecordCount = 0;
            this.winfrmPager.Size = new System.Drawing.Size(738, 23);
            this.winfrmPager.TabIndex = 0;
            this.winfrmPager.PageIndexChanged += new System.EventHandler(this.winfrmPager_PageIndexChanged);
            // 
            // BasePhase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 450);
            this.Controls.Add(this.plPager);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BasePhase";
            this.TabText = "BasePhase";
            this.Text = "BasePhase";
            this.Load += new System.EventHandler(this.BasePhase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.plPager.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton btnSelect;
        protected System.Windows.Forms.ToolStripButton btnPreview;
        protected System.Windows.Forms.ToolStripButton btnExport;
        protected System.Windows.Forms.Panel pnlTitle;
        protected System.Windows.Forms.DataGridView dgvList;
        protected System.Windows.Forms.Panel pnlBody;
        protected System.Windows.Forms.ToolStripButton btnNew;
        protected System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.Panel plPager;
        private MCD.RLPlanning.Client.Common.WinfrmPager winfrmPager;
        private System.Windows.Forms.ToolStripButton btnReset;
        public System.Windows.Forms.ToolStripButton btnCreateContract;
        public System.Windows.Forms.ToolStripButton btnKeyInSales;
        protected System.Windows.Forms.ToolStripButton btnCopy;
        protected System.Windows.Forms.ToolStripButton btnMultiCopy;
    }
}