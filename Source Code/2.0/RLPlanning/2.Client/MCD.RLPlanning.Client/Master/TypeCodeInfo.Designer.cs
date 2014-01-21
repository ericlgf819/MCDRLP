namespace MCD.RLPlanning.Client.Master
{
    partial class TypeCodeInfo
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
            this.lblTypeCode = new System.Windows.Forms.Label();
            this.txtTypeCode = new System.Windows.Forms.TextBox();
            this.lblRentTypeName = new System.Windows.Forms.Label();
            this.ddlRentType = new System.Windows.Forms.ComboBox();
            this.lblEntityTypeName = new System.Windows.Forms.Label();
            this.ddlEntityType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.ddlStatus);
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.ddlEntityType);
            this.pnlTitle.Controls.Add(this.lblEntityTypeName);
            this.pnlTitle.Controls.Add(this.ddlRentType);
            this.pnlTitle.Controls.Add(this.lblRentTypeName);
            this.pnlTitle.Controls.Add(this.txtTypeCode);
            this.pnlTitle.Controls.Add(this.lblTypeCode);
            this.pnlTitle.Size = new System.Drawing.Size(862, 46);
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 71);
            this.pnlBody.Size = new System.Drawing.Size(862, 251);
            // 
            // lblTypeCode
            // 
            this.lblTypeCode.AutoSize = true;
            this.lblTypeCode.Location = new System.Drawing.Point(13, 14);
            this.lblTypeCode.Name = "lblTypeCode";
            this.lblTypeCode.Size = new System.Drawing.Size(59, 13);
            this.lblTypeCode.TabIndex = 0;
            this.lblTypeCode.Text = "TypeCode:";
            // 
            // txtTypeCode
            // 
            this.txtTypeCode.Location = new System.Drawing.Point(76, 11);
            this.txtTypeCode.MaxLength = 50;
            this.txtTypeCode.Name = "txtTypeCode";
            this.txtTypeCode.Size = new System.Drawing.Size(100, 20);
            this.txtTypeCode.TabIndex = 1;
            // 
            // lblRentTypeName
            // 
            this.lblRentTypeName.AutoSize = true;
            this.lblRentTypeName.Location = new System.Drawing.Point(200, 14);
            this.lblRentTypeName.Name = "lblRentTypeName";
            this.lblRentTypeName.Size = new System.Drawing.Size(58, 13);
            this.lblRentTypeName.TabIndex = 2;
            this.lblRentTypeName.Text = "租金类型:";
            // 
            // ddlRentType
            // 
            this.ddlRentType.FormattingEnabled = true;
            this.ddlRentType.Location = new System.Drawing.Point(263, 10);
            this.ddlRentType.Name = "ddlRentType";
            this.ddlRentType.Size = new System.Drawing.Size(121, 21);
            this.ddlRentType.TabIndex = 3;
            // 
            // lblEntityTypeName
            // 
            this.lblEntityTypeName.AutoSize = true;
            this.lblEntityTypeName.Location = new System.Drawing.Point(410, 14);
            this.lblEntityTypeName.Name = "lblEntityTypeName";
            this.lblEntityTypeName.Size = new System.Drawing.Size(58, 13);
            this.lblEntityTypeName.TabIndex = 4;
            this.lblEntityTypeName.Text = "实体类型:";
            // 
            // ddlEntityType
            // 
            this.ddlEntityType.FormattingEnabled = true;
            this.ddlEntityType.Location = new System.Drawing.Point(473, 10);
            this.ddlEntityType.Name = "ddlEntityType";
            this.ddlEntityType.Size = new System.Drawing.Size(121, 21);
            this.ddlEntityType.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(610, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "状态:";
            // 
            // ddlStatus
            // 
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(649, 10);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(121, 21);
            this.ddlStatus.TabIndex = 7;
            // 
            // TypeCodeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 345);
            this.Name = "TypeCodeInfo";
            this.ShowPager = true;
            this.Text = "TypeCode信息";
            this.Load += new System.EventHandler(this.TypeCodeInfo_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTypeCode;
        private System.Windows.Forms.Label lblEntityTypeName;
        private System.Windows.Forms.ComboBox ddlRentType;
        private System.Windows.Forms.Label lblRentTypeName;
        private System.Windows.Forms.TextBox txtTypeCode;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox ddlEntityType;
    }
}