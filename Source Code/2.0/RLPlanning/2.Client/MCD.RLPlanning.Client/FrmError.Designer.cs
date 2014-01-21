namespace MCD.RLPlanning.Client
{
    partial class FrmError
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
            this.lblErrorTitle = new MCD.Controls.UCLabel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.lblMessage = new MCD.Controls.UCLabel();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pnlDetail.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblErrorTitle
            // 
            this.lblErrorTitle.AutoSize = true;
            this.lblErrorTitle.Location = new System.Drawing.Point(29, 34);
            this.lblErrorTitle.Name = "lblErrorTitle";
            this.lblErrorTitle.NeedLanguage = false;
            this.lblErrorTitle.Size = new System.Drawing.Size(27, 13);
            this.lblErrorTitle.TabIndex = 2;
            this.lblErrorTitle.Text = "xxxx";
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(372, 102);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(97, 25);
            this.btnDetail.TabIndex = 1;
            this.btnDetail.Text = "查看详细信息";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(29, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(199, 13);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "非常抱歉，系统运行出现以下错误。";
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.txtDetails);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetail.Location = new System.Drawing.Point(0, 133);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(502, 282);
            this.pnlDetail.TabIndex = 1;
            this.pnlDetail.Visible = false;
            // 
            // txtDetails
            // 
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Location = new System.Drawing.Point(0, 0);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(502, 282);
            this.txtDetails.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblErrorTitle);
            this.pnlTitle.Controls.Add(this.lblMessage);
            this.pnlTitle.Controls.Add(this.btnDetail);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(502, 133);
            this.pnlTitle.TabIndex = 2;
            // 
            // FrmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 415);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlDetail);
            this.Name = "FrmError";
            this.Text = "系统运行出错";
            this.Load += new System.EventHandler(this.FrmError_Load);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetail;
        private MCD.Controls.UCLabel lblMessage;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Button btnDetail;
        private MCD.Controls.UCLabel lblErrorTitle;
        private System.Windows.Forms.Panel pnlTitle;
    }
}