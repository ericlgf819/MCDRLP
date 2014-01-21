namespace MCD.RLPlanning.Client.ContractMg
{
    partial class ContractList
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
            this.txtStoreDeptNo = new System.Windows.Forms.TextBox();
            this.txtVendorNo = new System.Windows.Forms.TextBox();
            this.txtCompanyNo = new System.Windows.Forms.TextBox();
            this.txtContractNo = new System.Windows.Forms.TextBox();
            this.cmbArea = new MCD.Controls.UCComboBox();
            this.cmbStatus = new MCD.Controls.UCComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnDetail = new System.Windows.Forms.ToolStripButton();
            this.btnChange = new System.Windows.Forms.ToolStripButton();
            this.btnRelet = new System.Windows.Forms.ToolStripButton();
            this.btnCreateAPGLByHand = new System.Windows.Forms.ToolStripButton();
            this.btnUnDoDelete = new System.Windows.Forms.ToolStripButton();
            this.lblFromSRLS = new System.Windows.Forms.Label();
            this.cbFromSRLS = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblCompanyNo = new System.Windows.Forms.Label();
            this.lblStoreDeptNo = new System.Windows.Forms.Label();
            this.lblContractNo = new System.Windows.Forms.Label();
            this.lblVendorNo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(0, 119);
            this.pnlMain.Size = new System.Drawing.Size(837, 234);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblStatus);
            this.pnlTitle.Controls.Add(this.lblVendorNo);
            this.pnlTitle.Controls.Add(this.lblContractNo);
            this.pnlTitle.Controls.Add(this.lblStoreDeptNo);
            this.pnlTitle.Controls.Add(this.lblCompanyNo);
            this.pnlTitle.Controls.Add(this.lblArea);
            this.pnlTitle.Controls.Add(this.cbFromSRLS);
            this.pnlTitle.Controls.Add(this.lblFromSRLS);
            this.pnlTitle.Controls.Add(this.toolStrip2);
            this.pnlTitle.Controls.Add(this.cmbStatus);
            this.pnlTitle.Controls.Add(this.txtContractNo);
            this.pnlTitle.Controls.Add(this.txtStoreDeptNo);
            this.pnlTitle.Controls.Add(this.cmbArea);
            this.pnlTitle.Controls.Add(this.txtVendorNo);
            this.pnlTitle.Controls.Add(this.txtCompanyNo);
            this.pnlTitle.Size = new System.Drawing.Size(837, 94);
            this.pnlTitle.TabIndex = 2;
            // 
            // txtStoreDeptNo
            // 
            this.txtStoreDeptNo.Location = new System.Drawing.Point(529, 9);
            this.txtStoreDeptNo.MaxLength = 50;
            this.txtStoreDeptNo.Name = "txtStoreDeptNo";
            this.txtStoreDeptNo.Size = new System.Drawing.Size(120, 21);
            this.txtStoreDeptNo.TabIndex = 0;
            // 
            // txtVendorNo
            // 
            this.txtVendorNo.Location = new System.Drawing.Point(728, 13);
            this.txtVendorNo.MaxLength = 50;
            this.txtVendorNo.Name = "txtVendorNo";
            this.txtVendorNo.Size = new System.Drawing.Size(120, 21);
            this.txtVendorNo.TabIndex = 1;
            this.txtVendorNo.Visible = false;
            // 
            // txtCompanyNo
            // 
            this.txtCompanyNo.Location = new System.Drawing.Point(290, 10);
            this.txtCompanyNo.MaxLength = 50;
            this.txtCompanyNo.Name = "txtCompanyNo";
            this.txtCompanyNo.Size = new System.Drawing.Size(120, 21);
            this.txtCompanyNo.TabIndex = 2;
            // 
            // txtContractNo
            // 
            this.txtContractNo.Location = new System.Drawing.Point(78, 38);
            this.txtContractNo.MaxLength = 50;
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(120, 21);
            this.txtContractNo.TabIndex = 3;
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(78, 9);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(120, 20);
            this.cmbArea.TabIndex = 4;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(290, 37);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 20);
            this.cmbStatus.TabIndex = 5;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDetail,
            this.btnChange,
            this.btnRelet,
            this.btnCreateAPGLByHand,
            this.btnUnDoDelete});
            this.toolStrip2.Location = new System.Drawing.Point(0, 69);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(837, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnDetail
            // 
            this.btnDetail.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_view;
            this.btnDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(52, 22);
            this.btnDetail.Text = "详情";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnChange
            // 
            this.btnChange.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_change;
            this.btnChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(52, 22);
            this.btnChange.Text = "变更";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnRelet
            // 
            this.btnRelet.Image = global::MCD.RLPlanning.Client.Properties.Resources.con_copy;
            this.btnRelet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRelet.Name = "btnRelet";
            this.btnRelet.Size = new System.Drawing.Size(52, 22);
            this.btnRelet.Text = "续租";
            this.btnRelet.Click += new System.EventHandler(this.btnRelet_Click);
            // 
            // btnCreateAPGLByHand
            // 
            this.btnCreateAPGLByHand.Image = global::MCD.RLPlanning.Client.Properties.Resources.读取;
            this.btnCreateAPGLByHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateAPGLByHand.Name = "btnCreateAPGLByHand";
            this.btnCreateAPGLByHand.Size = new System.Drawing.Size(106, 22);
            this.btnCreateAPGLByHand.Text = "手工发起APGL";
            this.btnCreateAPGLByHand.Visible = false;
            this.btnCreateAPGLByHand.Click += new System.EventHandler(this.btnCreateAPGLByHand_Click);
            // 
            // btnUnDoDelete
            // 
            this.btnUnDoDelete.Image = global::MCD.RLPlanning.Client.Properties.Resources.menu_事项导入;
            this.btnUnDoDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnDoDelete.Name = "btnUnDoDelete";
            this.btnUnDoDelete.Size = new System.Drawing.Size(76, 22);
            this.btnUnDoDelete.Text = "撤销删除";
            this.btnUnDoDelete.Visible = false;
            this.btnUnDoDelete.Click += new System.EventHandler(this.btnUnDoDelete_Click);
            // 
            // lblFromSRLS
            // 
            this.lblFromSRLS.AutoSize = true;
            this.lblFromSRLS.Location = new System.Drawing.Point(480, 38);
            this.lblFromSRLS.Name = "lblFromSRLS";
            this.lblFromSRLS.Size = new System.Drawing.Size(41, 12);
            this.lblFromSRLS.TabIndex = 6;
            this.lblFromSRLS.Text = "来源：";
            // 
            // cbFromSRLS
            // 
            this.cbFromSRLS.FormattingEnabled = true;
            this.cbFromSRLS.Location = new System.Drawing.Point(529, 35);
            this.cbFromSRLS.Name = "cbFromSRLS";
            this.cbFromSRLS.Size = new System.Drawing.Size(121, 20);
            this.cbFromSRLS.TabIndex = 7;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(29, 13);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(41, 12);
            this.lblArea.TabIndex = 8;
            this.lblArea.Text = "区域：";
            // 
            // lblCompanyNo
            // 
            this.lblCompanyNo.AutoSize = true;
            this.lblCompanyNo.Location = new System.Drawing.Point(217, 13);
            this.lblCompanyNo.Name = "lblCompanyNo";
            this.lblCompanyNo.Size = new System.Drawing.Size(65, 12);
            this.lblCompanyNo.TabIndex = 9;
            this.lblCompanyNo.Text = "公司编号：";
            // 
            // lblStoreDeptNo
            // 
            this.lblStoreDeptNo.AutoSize = true;
            this.lblStoreDeptNo.Location = new System.Drawing.Point(427, 13);
            this.lblStoreDeptNo.Name = "lblStoreDeptNo";
            this.lblStoreDeptNo.Size = new System.Drawing.Size(95, 12);
            this.lblStoreDeptNo.TabIndex = 10;
            this.lblStoreDeptNo.Text = "餐厅/部门编号：";
            // 
            // lblContractNo
            // 
            this.lblContractNo.AutoSize = true;
            this.lblContractNo.Location = new System.Drawing.Point(5, 41);
            this.lblContractNo.Name = "lblContractNo";
            this.lblContractNo.Size = new System.Drawing.Size(65, 12);
            this.lblContractNo.TabIndex = 11;
            this.lblContractNo.Text = "合同编号：";
            // 
            // lblVendorNo
            // 
            this.lblVendorNo.AutoSize = true;
            this.lblVendorNo.Location = new System.Drawing.Point(655, 17);
            this.lblVendorNo.Name = "lblVendorNo";
            this.lblVendorNo.Size = new System.Drawing.Size(65, 12);
            this.lblVendorNo.TabIndex = 12;
            this.lblVendorNo.Text = "业主编号：";
            this.lblVendorNo.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(241, 41);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 12);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "状态：";
            // 
            // ContractList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 374);
            this.Name = "ContractList";
            this.ShowPager = true;
            this.Text = "ContractList";
            this.Load += new System.EventHandler(this.ContractList_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStoreDeptNo;
        private System.Windows.Forms.TextBox txtVendorNo;
        private System.Windows.Forms.TextBox txtCompanyNo;
        private System.Windows.Forms.TextBox txtContractNo;
        private MCD.Controls.UCComboBox cmbArea;
        private MCD.Controls.UCComboBox cmbStatus;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnDetail;
        private System.Windows.Forms.ToolStripButton btnChange;
        private System.Windows.Forms.ToolStripButton btnRelet;
        private System.Windows.Forms.ToolStripButton btnCreateAPGLByHand;
        private System.Windows.Forms.ToolStripButton btnUnDoDelete;
        private System.Windows.Forms.Label lblFromSRLS;
        private System.Windows.Forms.ComboBox cbFromSRLS;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblCompanyNo;
        private System.Windows.Forms.Label lblStoreDeptNo;
        private System.Windows.Forms.Label lblContractNo;
        private System.Windows.Forms.Label lblVendorNo;
        private System.Windows.Forms.Label lblStatus;
    }
}