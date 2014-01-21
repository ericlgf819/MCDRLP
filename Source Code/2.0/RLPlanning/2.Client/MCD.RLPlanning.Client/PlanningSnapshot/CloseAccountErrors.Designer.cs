namespace MCD.RLPlanning.Client.PlanningSnapshot
{
    partial class CloseAccountErrors
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
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 35);
            this.pnlMain.Size = new System.Drawing.Size(1084, 554);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Size = new System.Drawing.Size(1084, 10);
            // 
            // CloseAccountErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 612);
            this.Name = "CloseAccountErrors";
            this.ShowPager = true;
            this.Text = "关帐检测错误列表";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}