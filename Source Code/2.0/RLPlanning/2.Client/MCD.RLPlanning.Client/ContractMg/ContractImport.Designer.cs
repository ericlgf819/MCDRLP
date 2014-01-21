namespace MCD.RLPlanning.Client.ContractMg
{
    partial class ContractImport
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
            this.lblPath = new MCD.Controls.UCLabel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnChooseFile = new MCD.Controls.UCButton();
            this.btnImport = new MCD.Controls.UCButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnImportPreview = new MCD.Controls.UCButton();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(950, 397);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.btnTemplate);
            this.pnlTitle.Controls.Add(this.btnImportPreview);
            this.pnlTitle.Controls.Add(this.btnImport);
            this.pnlTitle.Controls.Add(this.btnChooseFile);
            this.pnlTitle.Controls.Add(this.txtPath);
            this.pnlTitle.Controls.Add(this.lblPath);
            this.pnlTitle.Size = new System.Drawing.Size(950, 60);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(24, 20);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(58, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "文件路径:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(87, 17);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(400, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(493, 17);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(70, 23);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "选择文件";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(645, 17);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(70, 23);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            // 
            // btnImportPreview
            // 
            this.btnImportPreview.Location = new System.Drawing.Point(569, 17);
            this.btnImportPreview.Name = "btnImportPreview";
            this.btnImportPreview.Size = new System.Drawing.Size(70, 23);
            this.btnImportPreview.TabIndex = 3;
            this.btnImportPreview.Text = "导入预览";
            this.btnImportPreview.UseVisualStyleBackColor = true;
            this.btnImportPreview.Click += new System.EventHandler(this.btnImportPreview_Click);
            // 
            // btnTemplate
            // 
            this.btnTemplate.Location = new System.Drawing.Point(721, 17);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(70, 23);
            this.btnTemplate.TabIndex = 4;
            this.btnTemplate.Text = "模版下载";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // ContractImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 505);
            this.Name = "ContractImport";
            this.ShowPager = true;
            this.Text = "合同导入";
            this.Load += new System.EventHandler(this.ContractImport_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblPath;
        private MCD.Controls.UCButton btnImport;
        private MCD.Controls.UCButton btnChooseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MCD.Controls.UCButton btnImportPreview;
        protected System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnTemplate;
    }
}