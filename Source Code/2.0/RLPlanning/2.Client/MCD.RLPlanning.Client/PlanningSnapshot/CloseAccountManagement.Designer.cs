namespace MCD.RLPlanning.Client.PlanningSnapshot
{
    partial class CloseAccountManagement
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
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Size = new System.Drawing.Size(737, 479);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlTitle.Location = new System.Drawing.Point(0, 27);
            this.pnlTitle.Size = new System.Drawing.Size(737, 11);
            // 
            // CloseAccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 527);
            this.Name = "CloseAccountManagement";
            this.ShowPager = true;
            this.Text = "关帐管理";
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}