namespace MCD.RLPlanning.Client.ContractMg
{
    partial class ConditionAmountPanel
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
            this.components = new System.ComponentModel.Container();
            this.lblCyclePayable = new MCD.Controls.UCLabel();
            this.btnAddJudge = new MCD.Controls.UCButton();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.bdsConditionAmount = new System.Windows.Forms.BindingSource(this.components);
            this.txtCyclePayable = new System.Windows.Forms.TextBox();
            this.picDeleteJudge = new System.Windows.Forms.PictureBox();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            this.popupTextComponent1 = new MCD.RLPlanning.Client.ContractMg.PopupTextComponent(this.components);
            this.popupTextComponent2 = new MCD.RLPlanning.Client.ContractMg.PopupTextComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bdsConditionAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteJudge)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCyclePayable
            // 
            this.lblCyclePayable.AutoSize = true;
            this.lblCyclePayable.LabelLocation = 357;
            this.lblCyclePayable.Location = new System.Drawing.Point(275, 7);
            this.lblCyclePayable.Name = "lblCyclePayable";
            this.lblCyclePayable.Size = new System.Drawing.Size(82, 13);
            this.lblCyclePayable.TabIndex = 15;
            this.lblCyclePayable.Text = "结算周期应付:";
            // 
            // btnAddJudge
            // 
            this.btnAddJudge.Location = new System.Drawing.Point(196, 1);
            this.btnAddJudge.Name = "btnAddJudge";
            this.btnAddJudge.Size = new System.Drawing.Size(75, 25);
            this.btnAddJudge.TabIndex = 1;
            this.btnAddJudge.Text = "添加判断";
            this.btnAddJudge.UseVisualStyleBackColor = true;
            this.btnAddJudge.Click += new System.EventHandler(this.btnAddJudge_Click);
            // 
            // txtFormula
            // 
            this.txtFormula.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsConditionAmount, "ConditionDesc", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFormula.Location = new System.Drawing.Point(3, 4);
            this.txtFormula.MaxLength = 2000;
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(155, 20);
            this.txtFormula.TabIndex = 0;
            this.txtFormula.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // bdsConditionAmount
            // 
            this.bdsConditionAmount.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.ConditionAmountEntity);
            // 
            // txtCyclePayable
            // 
            this.txtCyclePayable.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsConditionAmount, "AmountFormulaDesc", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCyclePayable.Location = new System.Drawing.Point(359, 4);
            this.txtCyclePayable.MaxLength = 2000;
            this.txtCyclePayable.Name = "txtCyclePayable";
            this.txtCyclePayable.Size = new System.Drawing.Size(180, 20);
            this.txtCyclePayable.TabIndex = 2;
            this.txtCyclePayable.Enter += new System.EventHandler(this.innerControl_Enter);
            // 
            // picDeleteJudge
            // 
            this.picDeleteJudge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDeleteJudge.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.picDeleteJudge.Location = new System.Drawing.Point(167, 1);
            this.picDeleteJudge.Name = "picDeleteJudge";
            this.picDeleteJudge.Size = new System.Drawing.Size(24, 24);
            this.picDeleteJudge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDeleteJudge.TabIndex = 17;
            this.picDeleteJudge.TabStop = false;
            this.picDeleteJudge.Click += new System.EventHandler(this.btnDeleteJudge_Click);
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(539, 11);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 13);
            this.ucLabel2.TabIndex = 18;
            this.ucLabel2.Text = "*";
            // 
            // popupTextComponent1
            // 
            this.popupTextComponent1.TargetTextBox = this.txtFormula;
            // 
            // popupTextComponent2
            // 
            this.popupTextComponent2.TargetTextBox = this.txtCyclePayable;
            // 
            // ConditionAmountPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picDeleteJudge);
            this.Controls.Add(this.txtCyclePayable);
            this.Controls.Add(this.lblCyclePayable);
            this.Controls.Add(this.btnAddJudge);
            this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.ucLabel2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ConditionAmountPanel";
            this.Size = new System.Drawing.Size(550, 28);
            this.Load += new System.EventHandler(this.PercentRentFormulaPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsConditionAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteJudge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblCyclePayable;
        private MCD.Controls.UCButton btnAddJudge;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.TextBox txtCyclePayable;
        private System.Windows.Forms.PictureBox picDeleteJudge;
        private System.Windows.Forms.BindingSource bdsConditionAmount;
        private MCD.Controls.UCLabel ucLabel2;
        private PopupTextComponent popupTextComponent1;
        private PopupTextComponent popupTextComponent2;
    }
}
