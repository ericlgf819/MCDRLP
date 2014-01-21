namespace MCD.RLPlanning.Client.ForcastSales
{
    partial class Import
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
            this.btnTemplateDownload = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(868, 365);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.btnTemplateDownload);
            this.pnlTitle.Size = new System.Drawing.Size(868, 55);
            this.pnlTitle.Controls.SetChildIndex(this.btnTemplateDownload, 0);
            this.pnlTitle.Controls.SetChildIndex(this.txtPath, 0);
            // 
            // btnTemplateDownload
            // 
            this.btnTemplateDownload.Location = new System.Drawing.Point(721, 16);
            this.btnTemplateDownload.Name = "btnTemplateDownload";
            this.btnTemplateDownload.Size = new System.Drawing.Size(75, 21);
            this.btnTemplateDownload.TabIndex = 4;
            this.btnTemplateDownload.Text = "模板下载";
            this.btnTemplateDownload.UseVisualStyleBackColor = true;
            this.btnTemplateDownload.Click += new System.EventHandler(this.btnTemplateDownload_Click);
            // 
            // Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 466);
            this.Name = "Import";
            this.ShowPager = true;
            this.Text = "Import";
            this.Load += new System.EventHandler(this.Import_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTemplateDownload;
    }
}