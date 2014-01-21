namespace MCD.RLPlanning.Client.ContractMg
{
    partial class EntityEdit
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
            this.lblEntityType = new MCD.Controls.UCLabel();
            this.cmbEntityType = new MCD.Controls.UCComboBox();
            this.bdsEntity = new System.Windows.Forms.BindingSource(this.components);
            this.lblStoreDeptNo = new MCD.Controls.UCLabel();
            this.lblKioskNo = new MCD.Controls.UCLabel();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.lblEntityName = new MCD.Controls.UCLabel();
            this.cmbStoreDeptNo = new MCD.Controls.UCComboBox();
            this.cmbKiosk = new MCD.Controls.UCComboBox();
            this.lblOpeningDate = new MCD.Controls.UCLabel();
            this.lblRentStartDate = new MCD.Controls.UCLabel();
            this.lblOwnerVendor = new MCD.Controls.UCLabel();
            this.cklVendor = new System.Windows.Forms.CheckedListBox();
            this.lblIsCalculateAP = new MCD.Controls.UCLabel();
            this.cmbIsCalculateAP = new MCD.Controls.UCComboBox();
            this.lblAPStartDate = new MCD.Controls.UCLabel();
            this.lblRemark = new MCD.Controls.UCLabel();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRentEndDate = new MCD.Controls.UCLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStoreDept = new MCD.Controls.UCLabel();
            this.txtStoreDept = new System.Windows.Forms.TextBox();
            this.lblKioskIsNull = new System.Windows.Forms.Label();
            this.dtpRentStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRentEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpOpeningDate = new System.Windows.Forms.DateTimePicker();
            this.dtpAPStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.lblNote = new MCD.Controls.UCLabel();
            this.lblVendorInfo = new MCD.Controls.UCLabel();
            this.lblOpeningDateNotice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntity)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(132, 506);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(258, 506);
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.LabelLocation = 122;
            this.lblEntityType.Location = new System.Drawing.Point(67, 11);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(55, 13);
            this.lblEntityType.TabIndex = 1;
            this.lblEntityType.Text = "实体类型";
            // 
            // cmbEntityType
            // 
            this.cmbEntityType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsEntity, "EntityTypeName", true));
            this.cmbEntityType.FormattingEnabled = true;
            this.cmbEntityType.Location = new System.Drawing.Point(128, 8);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(121, 21);
            this.cmbEntityType.TabIndex = 0;
            this.cmbEntityType.SelectedIndexChanged += new System.EventHandler(this.cmbEntityType_SelectedIndexChanged);
            // 
            // bdsEntity
            // 
            this.bdsEntity.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.EntityEntity);
            // 
            // lblStoreDeptNo
            // 
            this.lblStoreDeptNo.AutoSize = true;
            this.lblStoreDeptNo.LabelLocation = 96;
            this.lblStoreDeptNo.Location = new System.Drawing.Point(12, 35);
            this.lblStoreDeptNo.Name = "lblStoreDeptNo";
            this.lblStoreDeptNo.Size = new System.Drawing.Size(84, 13);
            this.lblStoreDeptNo.TabIndex = 1;
            this.lblStoreDeptNo.Text = "餐厅/部门编号";
            // 
            // lblKioskNo
            // 
            this.lblKioskNo.AutoSize = true;
            this.lblKioskNo.LabelLocation = 96;
            this.lblKioskNo.Location = new System.Drawing.Point(39, 87);
            this.lblKioskNo.Name = "lblKioskNo";
            this.lblKioskNo.Size = new System.Drawing.Size(57, 13);
            this.lblKioskNo.TabIndex = 4;
            this.lblKioskNo.Text = "Kiosk编号";
            // 
            // txtEntityName
            // 
            this.txtEntityName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsEntity, "EntityName", true));
            this.txtEntityName.Location = new System.Drawing.Point(102, 112);
            this.txtEntityName.MaxLength = 100;
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(279, 20);
            this.txtEntityName.TabIndex = 3;
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.LabelLocation = 96;
            this.lblEntityName.Location = new System.Drawing.Point(41, 114);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(55, 13);
            this.lblEntityName.TabIndex = 4;
            this.lblEntityName.Text = "实体名称";
            // 
            // cmbStoreDeptNo
            // 
            this.cmbStoreDeptNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsEntity, "StoreOrDeptNo", true));
            this.cmbStoreDeptNo.FormattingEnabled = true;
            this.cmbStoreDeptNo.Location = new System.Drawing.Point(102, 31);
            this.cmbStoreDeptNo.Name = "cmbStoreDeptNo";
            this.cmbStoreDeptNo.Size = new System.Drawing.Size(121, 21);
            this.cmbStoreDeptNo.TabIndex = 0;
            this.cmbStoreDeptNo.SelectedIndexChanged += new System.EventHandler(this.cmbStoreDeptNo_SelectedIndexChanged);
            this.cmbStoreDeptNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbStoreDeptNo_KeyUp);
            // 
            // cmbKiosk
            // 
            this.cmbKiosk.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsEntity, "KioskNo", true));
            this.cmbKiosk.FormattingEnabled = true;
            this.cmbKiosk.Location = new System.Drawing.Point(102, 85);
            this.cmbKiosk.Name = "cmbKiosk";
            this.cmbKiosk.Size = new System.Drawing.Size(279, 21);
            this.cmbKiosk.TabIndex = 2;
            this.cmbKiosk.SelectedIndexChanged += new System.EventHandler(this.cmbKiosk_SelectedIndexChanged);
            this.cmbKiosk.SelectionChangeCommitted += new System.EventHandler(this.cmbKiosk_SelectionChangeCommitted);
            // 
            // lblOpeningDate
            // 
            this.lblOpeningDate.AutoSize = true;
            this.lblOpeningDate.LabelLocation = 96;
            this.lblOpeningDate.Location = new System.Drawing.Point(53, 140);
            this.lblOpeningDate.Name = "lblOpeningDate";
            this.lblOpeningDate.Size = new System.Drawing.Size(43, 13);
            this.lblOpeningDate.TabIndex = 4;
            this.lblOpeningDate.Text = "开业日";
            // 
            // lblRentStartDate
            // 
            this.lblRentStartDate.AutoSize = true;
            this.lblRentStartDate.LabelLocation = 96;
            this.lblRentStartDate.Location = new System.Drawing.Point(53, 166);
            this.lblRentStartDate.Name = "lblRentStartDate";
            this.lblRentStartDate.Size = new System.Drawing.Size(43, 13);
            this.lblRentStartDate.TabIndex = 4;
            this.lblRentStartDate.Text = "起租日";
            // 
            // lblOwnerVendor
            // 
            this.lblOwnerVendor.AutoSize = true;
            this.lblOwnerVendor.LabelLocation = 96;
            this.lblOwnerVendor.Location = new System.Drawing.Point(41, 220);
            this.lblOwnerVendor.Name = "lblOwnerVendor";
            this.lblOwnerVendor.Size = new System.Drawing.Size(55, 13);
            this.lblOwnerVendor.TabIndex = 4;
            this.lblOwnerVendor.Text = "所属业主";
            // 
            // cklVendor
            // 
            this.cklVendor.FormattingEnabled = true;
            this.cklVendor.Location = new System.Drawing.Point(103, 216);
            this.cklVendor.Name = "cklVendor";
            this.cklVendor.Size = new System.Drawing.Size(278, 64);
            this.cklVendor.TabIndex = 7;
            // 
            // lblIsCalculateAP
            // 
            this.lblIsCalculateAP.AutoSize = true;
            this.lblIsCalculateAP.LabelLocation = 96;
            this.lblIsCalculateAP.Location = new System.Drawing.Point(39, 319);
            this.lblIsCalculateAP.Name = "lblIsCalculateAP";
            this.lblIsCalculateAP.Size = new System.Drawing.Size(57, 13);
            this.lblIsCalculateAP.TabIndex = 4;
            this.lblIsCalculateAP.Text = "是否出AP";
            // 
            // cmbIsCalculateAP
            // 
            this.cmbIsCalculateAP.FormattingEnabled = true;
            this.cmbIsCalculateAP.Location = new System.Drawing.Point(102, 315);
            this.cmbIsCalculateAP.Name = "cmbIsCalculateAP";
            this.cmbIsCalculateAP.Size = new System.Drawing.Size(121, 21);
            this.cmbIsCalculateAP.TabIndex = 8;
            this.cmbIsCalculateAP.SelectedIndexChanged += new System.EventHandler(this.cmbIsCalculateAP_SelectedIndexChanged);
            // 
            // lblAPStartDate
            // 
            this.lblAPStartDate.AutoSize = true;
            this.lblAPStartDate.LabelLocation = 96;
            this.lblAPStartDate.Location = new System.Drawing.Point(15, 346);
            this.lblAPStartDate.Name = "lblAPStartDate";
            this.lblAPStartDate.Size = new System.Drawing.Size(81, 13);
            this.lblAPStartDate.TabIndex = 4;
            this.lblAPStartDate.Text = "开始出AP日期";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.LabelLocation = 96;
            this.lblRemark.Location = new System.Drawing.Point(65, 372);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(31, 13);
            this.lblRemark.TabIndex = 4;
            this.lblRemark.Text = "备注";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsEntity, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(102, 368);
            this.txtRemark.MaxLength = 2000;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(279, 76);
            this.txtRemark.TabIndex = 10;
            // 
            // lblRentEndDate
            // 
            this.lblRentEndDate.AutoSize = true;
            this.lblRentEndDate.LabelLocation = 96;
            this.lblRentEndDate.Location = new System.Drawing.Point(29, 192);
            this.lblRentEndDate.Name = "lblRentEndDate";
            this.lblRentEndDate.Size = new System.Drawing.Size(67, 13);
            this.lblRentEndDate.TabIndex = 4;
            this.lblRentEndDate.Text = "租赁到期日";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(255, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(229, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(387, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(229, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(230, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(387, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(233, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(233, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "*";
            // 
            // lblStoreDept
            // 
            this.lblStoreDept.AutoSize = true;
            this.lblStoreDept.LabelLocation = 96;
            this.lblStoreDept.Location = new System.Drawing.Point(36, 61);
            this.lblStoreDept.Name = "lblStoreDept";
            this.lblStoreDept.Size = new System.Drawing.Size(60, 13);
            this.lblStoreDept.TabIndex = 1;
            this.lblStoreDept.Text = "餐厅/部门";
            // 
            // txtStoreDept
            // 
            this.txtStoreDept.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsEntity, "StoreOrDept", true));
            this.txtStoreDept.Location = new System.Drawing.Point(102, 59);
            this.txtStoreDept.Name = "txtStoreDept";
            this.txtStoreDept.ReadOnly = true;
            this.txtStoreDept.Size = new System.Drawing.Size(279, 20);
            this.txtStoreDept.TabIndex = 1;
            // 
            // lblKioskIsNull
            // 
            this.lblKioskIsNull.AutoSize = true;
            this.lblKioskIsNull.ForeColor = System.Drawing.Color.Red;
            this.lblKioskIsNull.Location = new System.Drawing.Point(387, 93);
            this.lblKioskIsNull.Name = "lblKioskIsNull";
            this.lblKioskIsNull.Size = new System.Drawing.Size(11, 13);
            this.lblKioskIsNull.TabIndex = 7;
            this.lblKioskIsNull.Text = "*";
            // 
            // dtpRentStartDate
            // 
            this.dtpRentStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "RentStartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpRentStartDate.Location = new System.Drawing.Point(103, 165);
            this.dtpRentStartDate.Name = "dtpRentStartDate";
            this.dtpRentStartDate.Size = new System.Drawing.Size(120, 20);
            this.dtpRentStartDate.TabIndex = 5;
            // 
            // dtpRentEndDate
            // 
            this.dtpRentEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "RentEndDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpRentEndDate.Location = new System.Drawing.Point(103, 191);
            this.dtpRentEndDate.Name = "dtpRentEndDate";
            this.dtpRentEndDate.Size = new System.Drawing.Size(120, 20);
            this.dtpRentEndDate.TabIndex = 6;
            // 
            // dtpOpeningDate
            // 
            this.dtpOpeningDate.Checked = false;
            this.dtpOpeningDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "OpeningDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpOpeningDate.Location = new System.Drawing.Point(103, 138);
            this.dtpOpeningDate.Name = "dtpOpeningDate";
            this.dtpOpeningDate.Size = new System.Drawing.Size(119, 20);
            this.dtpOpeningDate.TabIndex = 4;
            // 
            // dtpAPStartDate
            // 
            this.dtpAPStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bdsEntity, "APStartDate", true));
            this.dtpAPStartDate.Location = new System.Drawing.Point(102, 342);
            this.dtpAPStartDate.Name = "dtpAPStartDate";
            this.dtpAPStartDate.Size = new System.Drawing.Size(119, 20);
            this.dtpAPStartDate.TabIndex = 9;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.lblNote);
            this.pnlEdit.Controls.Add(this.lblVendorInfo);
            this.pnlEdit.Controls.Add(this.lblStoreDeptNo);
            this.pnlEdit.Controls.Add(this.dtpAPStartDate);
            this.pnlEdit.Controls.Add(this.lblStoreDept);
            this.pnlEdit.Controls.Add(this.dtpOpeningDate);
            this.pnlEdit.Controls.Add(this.cmbStoreDeptNo);
            this.pnlEdit.Controls.Add(this.dtpRentEndDate);
            this.pnlEdit.Controls.Add(this.cmbKiosk);
            this.pnlEdit.Controls.Add(this.dtpRentStartDate);
            this.pnlEdit.Controls.Add(this.cmbIsCalculateAP);
            this.pnlEdit.Controls.Add(this.txtStoreDept);
            this.pnlEdit.Controls.Add(this.txtEntityName);
            this.pnlEdit.Controls.Add(this.label8);
            this.pnlEdit.Controls.Add(this.txtRemark);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.lblKioskNo);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.lblEntityName);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.lblOpeningDate);
            this.pnlEdit.Controls.Add(this.lblOpeningDateNotice);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.lblRentStartDate);
            this.pnlEdit.Controls.Add(this.lblKioskIsNull);
            this.pnlEdit.Controls.Add(this.lblRentEndDate);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.lblOwnerVendor);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Controls.Add(this.lblIsCalculateAP);
            this.pnlEdit.Controls.Add(this.lblAPStartDate);
            this.pnlEdit.Controls.Add(this.cklVendor);
            this.pnlEdit.Controls.Add(this.lblRemark);
            this.pnlEdit.Enabled = false;
            this.pnlEdit.Location = new System.Drawing.Point(26, 30);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(408, 469);
            this.pnlEdit.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = System.Drawing.Color.Blue;
            this.lblNote.LabelLocation = 360;
            this.lblNote.Location = new System.Drawing.Point(78, 9);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(272, 13);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "注：输入餐厅编号后按Enter键以校验餐厅是否存在";
            // 
            // lblVendorInfo
            // 
            this.lblVendorInfo.AutoEllipsis = true;
            this.lblVendorInfo.LabelLocation = 96;
            this.lblVendorInfo.Location = new System.Drawing.Point(18, 244);
            this.lblVendorInfo.Name = "lblVendorInfo";
            this.lblVendorInfo.Size = new System.Drawing.Size(79, 61);
            this.lblVendorInfo.TabIndex = 13;
            this.lblVendorInfo.Text = "选择或取消业主，会添加或删除相关租金规则";
            // 
            // lblOpeningDateNotice
            // 
            this.lblOpeningDateNotice.AutoSize = true;
            this.lblOpeningDateNotice.ForeColor = System.Drawing.Color.Red;
            this.lblOpeningDateNotice.Location = new System.Drawing.Point(229, 143);
            this.lblOpeningDateNotice.Name = "lblOpeningDateNotice";
            this.lblOpeningDateNotice.Size = new System.Drawing.Size(11, 13);
            this.lblOpeningDateNotice.TabIndex = 7;
            this.lblOpeningDateNotice.Text = "*";
            // 
            // EntityEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 545);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbEntityType);
            this.Controls.Add(this.lblEntityType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntityEdit";
            this.ShowInTaskbar = false;
            this.Text = "编辑实体信息";
            this.Load += new System.EventHandler(this.EntityEdit_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblEntityType, 0);
            this.Controls.SetChildIndex(this.cmbEntityType, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pnlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bdsEntity)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCD.Controls.UCLabel lblEntityType;
        private MCD.Controls.UCComboBox cmbEntityType;
        private MCD.Controls.UCLabel lblStoreDeptNo;
        private MCD.Controls.UCLabel lblKioskNo;
        private System.Windows.Forms.TextBox txtEntityName;
        private MCD.Controls.UCLabel lblEntityName;
        private MCD.Controls.UCComboBox cmbStoreDeptNo;
        private MCD.Controls.UCComboBox cmbKiosk;
        private MCD.Controls.UCLabel lblOpeningDate;
        private MCD.Controls.UCLabel lblRentStartDate;
        private MCD.Controls.UCLabel lblOwnerVendor;
        private System.Windows.Forms.CheckedListBox cklVendor;
        private MCD.Controls.UCLabel lblIsCalculateAP;
        private MCD.Controls.UCComboBox cmbIsCalculateAP;
        private MCD.Controls.UCLabel lblAPStartDate;
        private MCD.Controls.UCLabel lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private MCD.Controls.UCLabel lblRentEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private MCD.Controls.UCLabel lblStoreDept;
        private System.Windows.Forms.TextBox txtStoreDept;
        private System.Windows.Forms.Label lblKioskIsNull;
        private System.Windows.Forms.DateTimePicker dtpRentStartDate;
        private System.Windows.Forms.DateTimePicker dtpRentEndDate;
        private System.Windows.Forms.BindingSource bdsEntity;
        private System.Windows.Forms.DateTimePicker dtpOpeningDate;
        private System.Windows.Forms.DateTimePicker dtpAPStartDate;
        private System.Windows.Forms.Panel pnlEdit;
        private MCD.Controls.UCLabel lblVendorInfo;
        private System.Windows.Forms.Label lblOpeningDateNotice;
        private MCD.Controls.UCLabel lblNote;
    }
}