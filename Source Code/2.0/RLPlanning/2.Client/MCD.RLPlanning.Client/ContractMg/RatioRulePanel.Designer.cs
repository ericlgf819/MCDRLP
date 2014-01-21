namespace MCD.RLPlanning.Client.ContractMg
{
    partial class RatioRulePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSummary = new MCD.Controls.UCLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bdsRatioRule = new System.Windows.Forms.BindingSource(this.components);
            this.lblRemark = new MCD.Controls.UCLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pnlContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioRule)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.LabelLocation = 60;
            this.lblSummary.Location = new System.Drawing.Point(26, 4);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(35, 12);
            this.lblSummary.TabIndex = 0;
            this.lblSummary.Text = "摘要:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsRatioRule, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(66, 1);
            this.textBox1.MaxLength = 2000;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 0;
            // 
            // bdsRatioRule
            // 
            this.bdsRatioRule.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.RatioRuleSettingEntity);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.LabelLocation = 335;
            this.lblRemark.Location = new System.Drawing.Point(301, 4);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(35, 12);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "备注:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsRatioRule, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Location = new System.Drawing.Point(341, 1);
            this.textBox2.MaxLength = 2000;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 21);
            this.textBox2.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.Location = new System.Drawing.Point(0, 23);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(885, 43);
            this.pnlContainer.TabIndex = 2;
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(269, 6);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 12);
            this.ucLabel1.TabIndex = 4;
            this.ucLabel1.Text = "*";
            // 
            // RatioRulePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucLabel1);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.lblSummary);
            this.Name = "RatioRulePanel";
            this.Size = new System.Drawing.Size(885, 66);
            this.Load += new System.EventHandler(this.RatioRentRulePanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsRatioRule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblSummary;
        private System.Windows.Forms.TextBox textBox1;
        private MCD.Controls.UCLabel lblRemark;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.FlowLayoutPanel pnlContainer;
        private System.Windows.Forms.BindingSource bdsRatioRule;
        private MCD.Controls.UCLabel ucLabel1;
    }
}
