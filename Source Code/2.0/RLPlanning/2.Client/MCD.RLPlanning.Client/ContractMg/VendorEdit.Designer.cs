namespace MCD.RLPlanning.Client.ContractMg
{
    partial class VendorEdit
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
            this.components = new System.ComponentModel.Container();
            this.lblVendorNo = new MCD.Controls.UCLabel();
            this.lblVendorName = new MCD.Controls.UCLabel();
            this.txtVendorNo = new System.Windows.Forms.TextBox();
            this.bdsVendorContract = new System.Windows.Forms.BindingSource(this.components);
            this.lblPaymentType = new MCD.Controls.UCLabel();
            this.cmbPaymentType = new MCD.Controls.UCComboBox();
            this.txtBlockPayment = new System.Windows.Forms.TextBox();
            this.lblBlockPayment = new MCD.Controls.UCLabel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new MCD.Controls.UCLabel();
            this.btnCreateVirtualVendor = new System.Windows.Forms.Button();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.ucLabel2 = new MCD.Controls.UCLabel();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.cmbVendorName = new System.Windows.Forms.ComboBox();
            this.ucLabel3 = new MCD.Controls.UCLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblinfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bdsVendorContract)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(142, 207);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 207);
            // 
            // lblVendorNo
            // 
            this.lblVendorNo.AutoSize = true;
            this.lblVendorNo.LabelLocation = 90;
            this.lblVendorNo.Location = new System.Drawing.Point(36, 10);
            this.lblVendorNo.Name = "lblVendorNo";
            this.lblVendorNo.Size = new System.Drawing.Size(58, 13);
            this.lblVendorNo.TabIndex = 4;
            this.lblVendorNo.Text = "业主编号:";
            // 
            // lblVendorName
            // 
            this.lblVendorName.AutoSize = true;
            this.lblVendorName.LabelLocation = 90;
            this.lblVendorName.Location = new System.Drawing.Point(36, 36);
            this.lblVendorName.Name = "lblVendorName";
            this.lblVendorName.Size = new System.Drawing.Size(58, 13);
            this.lblVendorName.TabIndex = 5;
            this.lblVendorName.Text = "业主名称:";
            // 
            // txtVendorNo
            // 
            this.txtVendorNo.Location = new System.Drawing.Point(97, 7);
            this.txtVendorNo.MaxLength = 50;
            this.txtVendorNo.Name = "txtVendorNo";
            this.txtVendorNo.Size = new System.Drawing.Size(121, 20);
            this.txtVendorNo.TabIndex = 0;
            this.txtVendorNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVendorNo_KeyUp);
            this.txtVendorNo.Leave += new System.EventHandler(this.txtVendorNo_Leave);
            this.txtVendorNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorNo_Validating);
            // 
            // bdsVendorContract
            // 
            this.bdsVendorContract.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.VendorContractEntity);
            // 
            // lblPaymentType
            // 
            this.lblPaymentType.AutoSize = true;
            this.lblPaymentType.LabelLocation = 90;
            this.lblPaymentType.Location = new System.Drawing.Point(18, 62);
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Size = new System.Drawing.Size(75, 13);
            this.lblPaymentType.TabIndex = 5;
            this.lblPaymentType.Text = "PaymentType:";
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsVendorContract, "PayMentType", true));
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(97, 59);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(121, 21);
            this.cmbPaymentType.TabIndex = 3;
            // 
            // txtBlockPayment
            // 
            this.txtBlockPayment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsVendorContract, "BlockPayMent", true));
            this.txtBlockPayment.Location = new System.Drawing.Point(97, 112);
            this.txtBlockPayment.Name = "txtBlockPayment";
            this.txtBlockPayment.ReadOnly = true;
            this.txtBlockPayment.Size = new System.Drawing.Size(247, 20);
            this.txtBlockPayment.TabIndex = 5;
            // 
            // lblBlockPayment
            // 
            this.lblBlockPayment.AutoSize = true;
            this.lblBlockPayment.LabelLocation = 90;
            this.lblBlockPayment.Location = new System.Drawing.Point(12, 115);
            this.lblBlockPayment.Name = "lblBlockPayment";
            this.lblBlockPayment.Size = new System.Drawing.Size(78, 13);
            this.lblBlockPayment.TabIndex = 5;
            this.lblBlockPayment.Text = "BlockPayment:";
            // 
            // txtStatus
            // 
            this.txtStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsVendorContract, "Status", true));
            this.txtStatus.Location = new System.Drawing.Point(97, 86);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(121, 20);
            this.txtStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.LabelLocation = 90;
            this.lblStatus.Location = new System.Drawing.Point(60, 89);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "状态:";
            // 
            // btnCreateVirtualVendor
            // 
            this.btnCreateVirtualVendor.Location = new System.Drawing.Point(238, 4);
            this.btnCreateVirtualVendor.Name = "btnCreateVirtualVendor";
            this.btnCreateVirtualVendor.Size = new System.Drawing.Size(106, 23);
            this.btnCreateVirtualVendor.TabIndex = 1;
            this.btnCreateVirtualVendor.Text = "创建虚拟Vendor";
            this.btnCreateVirtualVendor.UseVisualStyleBackColor = true;
            this.btnCreateVirtualVendor.Visible = false;
            this.btnCreateVirtualVendor.Click += new System.EventHandler(this.btnCreateVirtualVendor_Click);
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(221, 64);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 13);
            this.ucLabel1.TabIndex = 10;
            this.ucLabel1.Text = "*";
            // 
            // ucLabel2
            // 
            this.ucLabel2.AutoSize = true;
            this.ucLabel2.ForeColor = System.Drawing.Color.Red;
            this.ucLabel2.Location = new System.Drawing.Point(221, 13);
            this.ucLabel2.Name = "ucLabel2";
            this.ucLabel2.NeedLanguage = false;
            this.ucLabel2.Size = new System.Drawing.Size(11, 13);
            this.ucLabel2.TabIndex = 10;
            this.ucLabel2.Text = "*";
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.cmbVendorName);
            this.pnlEdit.Controls.Add(this.lblVendorNo);
            this.pnlEdit.Controls.Add(this.lblBlockPayment);
            this.pnlEdit.Controls.Add(this.lblStatus);
            this.pnlEdit.Controls.Add(this.txtBlockPayment);
            this.pnlEdit.Controls.Add(this.lblPaymentType);
            this.pnlEdit.Controls.Add(this.txtVendorNo);
            this.pnlEdit.Controls.Add(this.lblVendorName);
            this.pnlEdit.Controls.Add(this.cmbPaymentType);
            this.pnlEdit.Controls.Add(this.txtStatus);
            this.pnlEdit.Controls.Add(this.btnCreateVirtualVendor);
            this.pnlEdit.Controls.Add(this.ucLabel3);
            this.pnlEdit.Controls.Add(this.ucLabel1);
            this.pnlEdit.Controls.Add(this.ucLabel2);
            this.pnlEdit.Location = new System.Drawing.Point(12, 49);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(447, 139);
            this.pnlEdit.TabIndex = 11;
            // 
            // cmbVendorName
            // 
            this.cmbVendorName.FormattingEnabled = true;
            this.cmbVendorName.Location = new System.Drawing.Point(97, 30);
            this.cmbVendorName.Name = "cmbVendorName";
            this.cmbVendorName.Size = new System.Drawing.Size(289, 21);
            this.cmbVendorName.TabIndex = 11;
            this.cmbVendorName.SelectionChangeCommitted += new System.EventHandler(this.cmbVendorName_SelectionChangeCommitted);
            // 
            // ucLabel3
            // 
            this.ucLabel3.AutoSize = true;
            this.ucLabel3.ForeColor = System.Drawing.Color.Red;
            this.ucLabel3.Location = new System.Drawing.Point(390, 36);
            this.ucLabel3.Name = "ucLabel3";
            this.ucLabel3.NeedLanguage = false;
            this.ucLabel3.Size = new System.Drawing.Size(11, 13);
            this.ucLabel3.TabIndex = 10;
            this.ucLabel3.Text = "*";
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.ForeColor = System.Drawing.Color.Blue;
            this.lblinfo.Location = new System.Drawing.Point(119, 22);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(198, 13);
            this.lblinfo.TabIndex = 13;
            this.lblinfo.Tag = "300";
            this.lblinfo.Text = "输入编号按Enter进行获取Vendor信息";
            // 
            // VendorEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 255);
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.pnlEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VendorEdit";
            this.ShowInTaskbar = false;
            this.Text = "编辑出租方信息";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.pnlEdit, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lblinfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bdsVendorContract)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblVendorNo;
        private MCD.Controls.UCLabel lblVendorName;
        private System.Windows.Forms.TextBox txtVendorNo;
        private MCD.Controls.UCLabel lblPaymentType;
        private MCD.Controls.UCComboBox cmbPaymentType;
        private System.Windows.Forms.TextBox txtBlockPayment;
        private MCD.Controls.UCLabel lblBlockPayment;
        private System.Windows.Forms.TextBox txtStatus;
        private MCD.Controls.UCLabel lblStatus;
        private System.Windows.Forms.Button btnCreateVirtualVendor;
        private System.Windows.Forms.BindingSource bdsVendorContract;
        private MCD.Controls.UCLabel ucLabel1;
        private MCD.Controls.UCLabel ucLabel2;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblinfo;
        private System.Windows.Forms.ComboBox cmbVendorName;
        private MCD.Controls.UCLabel ucLabel3;
    }
}