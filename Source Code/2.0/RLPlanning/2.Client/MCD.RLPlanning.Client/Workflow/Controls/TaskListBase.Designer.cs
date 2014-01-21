namespace MCD.RLPlanning.Client.Workflow.Controls
{
    partial class TaskListBase
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnPrintReview = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnCalculate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecaculate = new System.Windows.Forms.ToolStripButton();
            this.btnBatchSubmit = new System.Windows.Forms.ToolStripButton();
            this.btnGenCertificate = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSearch,
            this.btnReset,
            this.btnPrintReview,
            this.btnExport,
            this.btnCalculate,
            this.toolStripSeparator1,
            this.btnRecaculate,
            this.btnBatchSubmit,
            this.btnGenCertificate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(843, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MCD.RLPlanning.Client.Properties.Resources.查询;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(52, 22);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::MCD.RLPlanning.Client.Properties.Resources.读取;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(52, 22);
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnPrintReview
            // 
            this.btnPrintReview.Image = global::MCD.RLPlanning.Client.Properties.Resources.打印预览;
            this.btnPrintReview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrintReview.Name = "btnPrintReview";
            this.btnPrintReview.Size = new System.Drawing.Size(76, 22);
            this.btnPrintReview.Text = "打印预览";
            this.btnPrintReview.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::MCD.RLPlanning.Client.Properties.Resources.导出数据;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(52, 22);
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Image = global::MCD.RLPlanning.Client.Properties.Resources.编辑;
            this.btnCalculate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(76, 22);
            this.btnCalculate.Text = "租金计算";
            this.btnCalculate.Visible = false;
            this.btnCalculate.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRecaculate
            // 
            this.btnRecaculate.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_change;
            this.btnRecaculate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecaculate.Name = "btnRecaculate";
            this.btnRecaculate.Size = new System.Drawing.Size(76, 22);
            this.btnRecaculate.Text = "重新计算";
            this.btnRecaculate.Visible = false;
            this.btnRecaculate.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnBatchSubmit
            // 
            this.btnBatchSubmit.Image = global::MCD.RLPlanning.Client.Properties.Resources.menu_新增事项;
            this.btnBatchSubmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatchSubmit.Name = "btnBatchSubmit";
            this.btnBatchSubmit.Size = new System.Drawing.Size(76, 22);
            this.btnBatchSubmit.Text = "批量提交";
            this.btnBatchSubmit.Visible = false;
            this.btnBatchSubmit.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // btnGenCertificate
            // 
            this.btnGenCertificate.Image = global::MCD.RLPlanning.Client.Properties.Resources.menu_科目信息;
            this.btnGenCertificate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenCertificate.Name = "btnGenCertificate";
            this.btnGenCertificate.Size = new System.Drawing.Size(76, 22);
            this.btnGenCertificate.Text = "生成凭证";
            this.btnGenCertificate.Visible = false;
            this.btnGenCertificate.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // TaskListBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "TaskListBase";
            this.Size = new System.Drawing.Size(843, 419);
            this.Load += new System.EventHandler(this.TaskListBase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripButton btnBatchSubmit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGenCertificate;
        private System.Windows.Forms.ToolStripButton btnPrintReview;
        private System.Windows.Forms.ToolStripButton btnExport;
        protected System.Windows.Forms.ToolStripButton btnRecaculate;
        protected System.Windows.Forms.ToolStripButton btnCalculate;
    }
}
