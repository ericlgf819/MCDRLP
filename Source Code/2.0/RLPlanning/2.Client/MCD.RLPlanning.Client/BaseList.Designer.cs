namespace MCD.RLPlanning.Client
{
    partial class BaseList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseList));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnAddnew = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnCreateContract = new System.Windows.Forms.ToolStripButton();
            this.btnKeyInSales = new System.Windows.Forms.ToolStripButton();
            this.btnCheckCloseAccount = new System.Windows.Forms.ToolStripButton();
            this.btnCloseAccount = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.plPager = new System.Windows.Forms.Panel();
            this.winfrmPager = new MCD.RLPlanning.Client.Common.WinfrmPager();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.plPager.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvList);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 85);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(929, 390);
            this.pnlMain.TabIndex = 8;
            // 
            // dgvList
            // 
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(929, 390);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvList_RowPrePaint);
            this.dgvList.SelectionChanged += new System.EventHandler(this.dgvList_SelectionChanged);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 25);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(929, 60);
            this.pnlTitle.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelect,
            this.btnReset,
            this.btnAddnew,
            this.btnSaveAs,
            this.btnEdit,
            this.btnSave,
            this.btnDelete,
            this.btnClose,
            this.btnCreateContract,
            this.btnKeyInSales,
            this.btnCheckCloseAccount,
            this.btnCloseAccount,
            this.btnPreview,
            this.btnExport,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(929, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSelect
            // 
            this.btnSelect.Image = global::MCD.RLPlanning.Client.Properties.Resources.查询;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(51, 22);
            this.btnSelect.Text = "查询";
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
            // btnAddnew
            // 
            this.btnAddnew.Image = global::MCD.RLPlanning.Client.Properties.Resources.新建;
            this.btnAddnew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddnew.Name = "btnAddnew";
            this.btnAddnew.Size = new System.Drawing.Size(51, 22);
            this.btnAddnew.Text = "新增";
            this.btnAddnew.Visible = false;
            this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Image = global::MCD.RLPlanning.Client.Properties.Resources.编辑;
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(63, 22);
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.Visible = false;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MCD.RLPlanning.Client.Properties.Resources.编辑;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(51, 22);
            this.btnEdit.Text = "编辑";
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::MCD.RLPlanning.Client.Properties.Resources.保存;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Visible = false;
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
            // btnClose
            // 
            this.btnClose.Image = global::MCD.RLPlanning.Client.Properties.Resources.关闭;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 22);
            this.btnClose.Text = "关闭";
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateContract
            // 
            this.btnCreateContract.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_change;
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
            // btnCheckCloseAccount
            // 
            this.btnCheckCloseAccount.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_change;
            this.btnCheckCloseAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckCloseAccount.Name = "btnCheckCloseAccount";
            this.btnCheckCloseAccount.Size = new System.Drawing.Size(75, 22);
            this.btnCheckCloseAccount.Text = "检查关帐";
            this.btnCheckCloseAccount.Visible = false;
            this.btnCheckCloseAccount.Click += new System.EventHandler(this.btnCheckCloseAccount_Click);
            // 
            // btnCloseAccount
            // 
            this.btnCloseAccount.Image = global::MCD.RLPlanning.Client.Properties.Resources.menu_区域信息;
            this.btnCloseAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseAccount.Name = "btnCloseAccount";
            this.btnCloseAccount.Size = new System.Drawing.Size(51, 22);
            this.btnCloseAccount.Text = "关帐";
            this.btnCloseAccount.Visible = false;
            this.btnCloseAccount.Click += new System.EventHandler(this.btnCloseAccount_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // plPager
            // 
            this.plPager.Controls.Add(this.winfrmPager);
            this.plPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plPager.Location = new System.Drawing.Point(0, 452);
            this.plPager.Name = "plPager";
            this.plPager.Size = new System.Drawing.Size(929, 23);
            this.plPager.TabIndex = 9;
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
            this.winfrmPager.Size = new System.Drawing.Size(929, 23);
            this.winfrmPager.TabIndex = 0;
            this.winfrmPager.PageIndexChanged += new System.EventHandler(this.winfrmPager_PageIndexChanged);
            // 
            // BaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 475);
            this.Controls.Add(this.plPager);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BaseList";
            this.TabText = "BaseList";
            this.Text = "BaseList";
            this.Load += new System.EventHandler(this.BaseList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaseList_KeyDown);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.plPager.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton btnAddnew;
        public System.Windows.Forms.ToolStripButton btnSelect;
        public System.Windows.Forms.ToolStripButton btnDelete;
        public System.Windows.Forms.ToolStripButton btnClose;
        public System.Windows.Forms.DataGridView dgvList;
        public System.Windows.Forms.ToolStripButton btnPreview;
        public System.Windows.Forms.ToolStripButton btnExport;
        public System.Windows.Forms.ToolStripButton btnEdit;
        public System.Windows.Forms.ToolStripButton btnSave;
        public System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.Panel plPager;
        private MCD.RLPlanning.Client.Common.WinfrmPager winfrmPager;
        public System.Windows.Forms.ToolStripButton btnCheckCloseAccount;
        public System.Windows.Forms.ToolStripButton btnCloseAccount;
        public System.Windows.Forms.ToolStripButton btnReset;
        public System.Windows.Forms.ToolStripButton btnCreateContract;
        public System.Windows.Forms.ToolStripButton btnKeyInSales;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}