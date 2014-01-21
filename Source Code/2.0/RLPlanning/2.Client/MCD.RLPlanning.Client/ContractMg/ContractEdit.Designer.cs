namespace MCD.RLPlanning.Client.ContractMg
{
    partial class ContractEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractEdit));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpContractHistory = new System.Windows.Forms.GroupBox();
            this.lstContractHistory = new System.Windows.Forms.ListBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pagContractBasicInfo = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnAddVendor = new System.Windows.Forms.Button();
            this.grpVendor = new System.Windows.Forms.GroupBox();
            this.btnDelVendor = new System.Windows.Forms.Button();
            this.dgvVendor = new System.Windows.Forms.DataGridView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.grpEntityInfo = new System.Windows.Forms.GroupBox();
            this.btnDelEntity = new System.Windows.Forms.Button();
            this.dgvEntity = new System.Windows.Forms.DataGridView();
            this.sysAttach1 = new MCD.RLPlanning.Client.Common.SysAttach();
            this.txtContractUpdateRemark = new System.Windows.Forms.TextBox();
            this.bdsContract = new System.Windows.Forms.BindingSource(this.components);
            this.txtContractRemark = new System.Windows.Forms.TextBox();
            this.grpRenter = new System.Windows.Forms.GroupBox();
            this.ucLabel1 = new MCD.Controls.UCLabel();
            this.txtCompanyRemark = new System.Windows.Forms.TextBox();
            this.txtSimpleName = new System.Windows.Forms.TextBox();
            this.cmbCompany = new MCD.Controls.UCComboBox();
            this.lblCompanyRemark = new MCD.Controls.UCLabel();
            this.lblSimpleName = new MCD.Controls.UCLabel();
            this.lblChooseCompany = new MCD.Controls.UCLabel();
            this.txtContractNo = new System.Windows.Forms.TextBox();
            this.lblContractUpdateRemark = new MCD.Controls.UCLabel();
            this.lblContractRemark = new MCD.Controls.UCLabel();
            this.lblContractNo = new MCD.Controls.UCLabel();
            this.pagRentCalcRule = new System.Windows.Forms.TabPage();
            this.tabVendors = new System.Windows.Forms.TabControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpContractHistory.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.pagContractBasicInfo.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grpVendor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendor)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grpEntityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsContract)).BeginInit();
            this.grpRenter.SuspendLayout();
            this.pagRentCalcRule.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(7, 522);
            this.pnlBottom.Size = new System.Drawing.Size(887, 147);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(7, 9);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpContractHistory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabMain);
            this.splitContainer1.Size = new System.Drawing.Size(887, 513);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.TabIndex = 3;
            // 
            // grpContractHistory
            // 
            this.grpContractHistory.Controls.Add(this.lstContractHistory);
            this.grpContractHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContractHistory.Location = new System.Drawing.Point(0, 0);
            this.grpContractHistory.Name = "grpContractHistory";
            this.grpContractHistory.Size = new System.Drawing.Size(115, 513);
            this.grpContractHistory.TabIndex = 0;
            this.grpContractHistory.TabStop = false;
            this.grpContractHistory.Text = "合同历史版本";
            // 
            // lstContractHistory
            // 
            this.lstContractHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstContractHistory.FormattingEnabled = true;
            this.lstContractHistory.Location = new System.Drawing.Point(3, 16);
            this.lstContractHistory.Name = "lstContractHistory";
            this.lstContractHistory.Size = new System.Drawing.Size(109, 494);
            this.lstContractHistory.TabIndex = 0;
            this.lstContractHistory.SelectedIndexChanged += new System.EventHandler(this.lstContractHistory_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pagContractBasicInfo);
            this.tabMain.Controls.Add(this.pagRentCalcRule);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(768, 513);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // pagContractBasicInfo
            // 
            this.pagContractBasicInfo.Controls.Add(this.splitContainer2);
            this.pagContractBasicInfo.Controls.Add(this.txtContractUpdateRemark);
            this.pagContractBasicInfo.Controls.Add(this.txtContractRemark);
            this.pagContractBasicInfo.Controls.Add(this.grpRenter);
            this.pagContractBasicInfo.Controls.Add(this.txtContractNo);
            this.pagContractBasicInfo.Controls.Add(this.lblContractUpdateRemark);
            this.pagContractBasicInfo.Controls.Add(this.lblContractRemark);
            this.pagContractBasicInfo.Controls.Add(this.lblContractNo);
            this.pagContractBasicInfo.Location = new System.Drawing.Point(4, 22);
            this.pagContractBasicInfo.Name = "pagContractBasicInfo";
            this.pagContractBasicInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pagContractBasicInfo.Size = new System.Drawing.Size(760, 487);
            this.pagContractBasicInfo.TabIndex = 0;
            this.pagContractBasicInfo.Text = "合同基础信息";
            this.pagContractBasicInfo.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(7, 218);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnAddVendor);
            this.splitContainer2.Panel1.Controls.Add(this.grpVendor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(754, 261);
            this.splitContainer2.SplitterDistance = 87;
            this.splitContainer2.TabIndex = 17;
            // 
            // btnAddVendor
            // 
            this.btnAddVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddVendor.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVendor.Image")));
            this.btnAddVendor.Location = new System.Drawing.Point(711, 3);
            this.btnAddVendor.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddVendor.Name = "btnAddVendor";
            this.btnAddVendor.Size = new System.Drawing.Size(24, 24);
            this.btnAddVendor.TabIndex = 0;
            this.btnAddVendor.UseVisualStyleBackColor = true;
            this.btnAddVendor.Click += new System.EventHandler(this.btnAddVendor_Click);
            // 
            // grpVendor
            // 
            this.grpVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVendor.Controls.Add(this.btnDelVendor);
            this.grpVendor.Controls.Add(this.dgvVendor);
            this.grpVendor.Location = new System.Drawing.Point(3, 3);
            this.grpVendor.Name = "grpVendor";
            this.grpVendor.Size = new System.Drawing.Size(741, 85);
            this.grpVendor.TabIndex = 3;
            this.grpVendor.TabStop = false;
            this.grpVendor.Text = "出租方";
            // 
            // btnDelVendor
            // 
            this.btnDelVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelVendor.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.btnDelVendor.Location = new System.Drawing.Point(674, 0);
            this.btnDelVendor.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelVendor.Name = "btnDelVendor";
            this.btnDelVendor.Size = new System.Drawing.Size(24, 24);
            this.btnDelVendor.TabIndex = 1;
            this.btnDelVendor.UseVisualStyleBackColor = true;
            this.btnDelVendor.Click += new System.EventHandler(this.btnDelVendor_Click);
            // 
            // dgvVendor
            // 
            this.dgvVendor.AllowUserToAddRows = false;
            this.dgvVendor.AllowUserToDeleteRows = false;
            this.dgvVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVendor.Location = new System.Drawing.Point(3, 16);
            this.dgvVendor.MultiSelect = false;
            this.dgvVendor.Name = "dgvVendor";
            this.dgvVendor.ReadOnly = true;
            this.dgvVendor.RowTemplate.Height = 23;
            this.dgvVendor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendor.Size = new System.Drawing.Size(735, 66);
            this.dgvVendor.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btnAddEntity);
            this.splitContainer3.Panel1.Controls.Add(this.grpEntityInfo);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.sysAttach1);
            this.splitContainer3.Size = new System.Drawing.Size(754, 170);
            this.splitContainer3.SplitterDistance = 87;
            this.splitContainer3.TabIndex = 0;
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddEntity.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEntity.Image")));
            this.btnAddEntity.Location = new System.Drawing.Point(711, 7);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(24, 24);
            this.btnAddEntity.TabIndex = 0;
            this.btnAddEntity.UseVisualStyleBackColor = true;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // grpEntityInfo
            // 
            this.grpEntityInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEntityInfo.Controls.Add(this.btnDelEntity);
            this.grpEntityInfo.Controls.Add(this.dgvEntity);
            this.grpEntityInfo.Location = new System.Drawing.Point(3, 8);
            this.grpEntityInfo.Name = "grpEntityInfo";
            this.grpEntityInfo.Size = new System.Drawing.Size(741, 78);
            this.grpEntityInfo.TabIndex = 3;
            this.grpEntityInfo.TabStop = false;
            this.grpEntityInfo.Text = "实体信息";
            // 
            // btnDelEntity
            // 
            this.btnDelEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelEntity.Image = global::MCD.RLPlanning.Client.Properties.Resources.删除;
            this.btnDelEntity.Location = new System.Drawing.Point(674, -1);
            this.btnDelEntity.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelEntity.Name = "btnDelEntity";
            this.btnDelEntity.Size = new System.Drawing.Size(24, 24);
            this.btnDelEntity.TabIndex = 2;
            this.btnDelEntity.UseVisualStyleBackColor = true;
            this.btnDelEntity.Click += new System.EventHandler(this.btnDelEntity_Click);
            // 
            // dgvEntity
            // 
            this.dgvEntity.AllowUserToAddRows = false;
            this.dgvEntity.AllowUserToDeleteRows = false;
            this.dgvEntity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntity.Location = new System.Drawing.Point(3, 16);
            this.dgvEntity.MultiSelect = false;
            this.dgvEntity.Name = "dgvEntity";
            this.dgvEntity.ReadOnly = true;
            this.dgvEntity.RowTemplate.Height = 23;
            this.dgvEntity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEntity.Size = new System.Drawing.Size(735, 59);
            this.dgvEntity.TabIndex = 0;
            // 
            // sysAttach1
            // 
            this.sysAttach1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sysAttach1.Category = MCD.Common.SRLS.CategoryType.Contract;
            this.sysAttach1.CmdType = MCD.RLPlanning.Client.Common.ActionType.New;
            this.sysAttach1.GroupTitle = "合同文件";
            this.sysAttach1.Location = new System.Drawing.Point(3, 8);
            this.sysAttach1.Name = "sysAttach1";
            this.sysAttach1.ObjectID = null;
            this.sysAttach1.Size = new System.Drawing.Size(741, 73);
            this.sysAttach1.TabIndex = 0;
            this.sysAttach1.TempObjectID = null;
            // 
            // txtContractUpdateRemark
            // 
            this.txtContractUpdateRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsContract, "UpdateInfo", true));
            this.txtContractUpdateRemark.Location = new System.Drawing.Point(106, 69);
            this.txtContractUpdateRemark.MaxLength = 2000;
            this.txtContractUpdateRemark.Multiline = true;
            this.txtContractUpdateRemark.Name = "txtContractUpdateRemark";
            this.txtContractUpdateRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContractUpdateRemark.Size = new System.Drawing.Size(635, 35);
            this.txtContractUpdateRemark.TabIndex = 2;
            // 
            // bdsContract
            // 
            this.bdsContract.DataSource = typeof(MCD.RLPlanning.Entity.ContractMg.ContractEntity);
            // 
            // txtContractRemark
            // 
            this.txtContractRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsContract, "Remark", true));
            this.txtContractRemark.Location = new System.Drawing.Point(106, 29);
            this.txtContractRemark.MaxLength = 2000;
            this.txtContractRemark.Multiline = true;
            this.txtContractRemark.Name = "txtContractRemark";
            this.txtContractRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContractRemark.Size = new System.Drawing.Size(635, 35);
            this.txtContractRemark.TabIndex = 1;
            // 
            // grpRenter
            // 
            this.grpRenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRenter.Controls.Add(this.ucLabel1);
            this.grpRenter.Controls.Add(this.txtCompanyRemark);
            this.grpRenter.Controls.Add(this.txtSimpleName);
            this.grpRenter.Controls.Add(this.cmbCompany);
            this.grpRenter.Controls.Add(this.lblCompanyRemark);
            this.grpRenter.Controls.Add(this.lblSimpleName);
            this.grpRenter.Controls.Add(this.lblChooseCompany);
            this.grpRenter.Location = new System.Drawing.Point(7, 104);
            this.grpRenter.Name = "grpRenter";
            this.grpRenter.Size = new System.Drawing.Size(743, 111);
            this.grpRenter.TabIndex = 3;
            this.grpRenter.TabStop = false;
            this.grpRenter.Text = "承租方";
            // 
            // ucLabel1
            // 
            this.ucLabel1.AutoSize = true;
            this.ucLabel1.ForeColor = System.Drawing.Color.Red;
            this.ucLabel1.Location = new System.Drawing.Point(421, 20);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.NeedLanguage = false;
            this.ucLabel1.Size = new System.Drawing.Size(11, 13);
            this.ucLabel1.TabIndex = 14;
            this.ucLabel1.Text = "*";
            // 
            // txtCompanyRemark
            // 
            this.txtCompanyRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsContract, "CompanyRemark", true));
            this.txtCompanyRemark.Location = new System.Drawing.Point(99, 67);
            this.txtCompanyRemark.MaxLength = 2000;
            this.txtCompanyRemark.Multiline = true;
            this.txtCompanyRemark.Name = "txtCompanyRemark";
            this.txtCompanyRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCompanyRemark.Size = new System.Drawing.Size(635, 39);
            this.txtCompanyRemark.TabIndex = 2;
            // 
            // txtSimpleName
            // 
            this.txtSimpleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsContract, "CompanySimpleName", true));
            this.txtSimpleName.Location = new System.Drawing.Point(99, 41);
            this.txtSimpleName.MaxLength = 50;
            this.txtSimpleName.Name = "txtSimpleName";
            this.txtSimpleName.ReadOnly = true;
            this.txtSimpleName.Size = new System.Drawing.Size(635, 20);
            this.txtSimpleName.TabIndex = 1;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsContract, "CompanyCode", true));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(99, 13);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(316, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // lblCompanyRemark
            // 
            this.lblCompanyRemark.AutoSize = true;
            this.lblCompanyRemark.LabelLocation = 93;
            this.lblCompanyRemark.Location = new System.Drawing.Point(35, 70);
            this.lblCompanyRemark.Name = "lblCompanyRemark";
            this.lblCompanyRemark.Size = new System.Drawing.Size(58, 13);
            this.lblCompanyRemark.TabIndex = 1;
            this.lblCompanyRemark.Text = "公司备注:";
            // 
            // lblSimpleName
            // 
            this.lblSimpleName.AutoSize = true;
            this.lblSimpleName.LabelLocation = 93;
            this.lblSimpleName.Location = new System.Drawing.Point(35, 44);
            this.lblSimpleName.Name = "lblSimpleName";
            this.lblSimpleName.Size = new System.Drawing.Size(58, 13);
            this.lblSimpleName.TabIndex = 1;
            this.lblSimpleName.Text = "公司简称:";
            // 
            // lblChooseCompany
            // 
            this.lblChooseCompany.AutoSize = true;
            this.lblChooseCompany.LabelLocation = 93;
            this.lblChooseCompany.Location = new System.Drawing.Point(23, 16);
            this.lblChooseCompany.Name = "lblChooseCompany";
            this.lblChooseCompany.Size = new System.Drawing.Size(70, 13);
            this.lblChooseCompany.TabIndex = 1;
            this.lblChooseCompany.Text = "请选择公司:";
            // 
            // txtContractNo
            // 
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsContract, "ContractNO", true));
            this.txtContractNo.Location = new System.Drawing.Point(106, 5);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.ReadOnly = true;
            this.txtContractNo.Size = new System.Drawing.Size(120, 20);
            this.txtContractNo.TabIndex = 0;
            // 
            // lblContractUpdateRemark
            // 
            this.lblContractUpdateRemark.AutoSize = true;
            this.lblContractUpdateRemark.LabelLocation = 100;
            this.lblContractUpdateRemark.Location = new System.Drawing.Point(20, 72);
            this.lblContractUpdateRemark.Name = "lblContractUpdateRemark";
            this.lblContractUpdateRemark.Size = new System.Drawing.Size(82, 13);
            this.lblContractUpdateRemark.TabIndex = 1;
            this.lblContractUpdateRemark.Text = "合同更新说明:";
            // 
            // lblContractRemark
            // 
            this.lblContractRemark.AutoSize = true;
            this.lblContractRemark.LabelLocation = 100;
            this.lblContractRemark.Location = new System.Drawing.Point(42, 29);
            this.lblContractRemark.Name = "lblContractRemark";
            this.lblContractRemark.Size = new System.Drawing.Size(58, 13);
            this.lblContractRemark.TabIndex = 1;
            this.lblContractRemark.Text = "合同备注:";
            // 
            // lblContractNo
            // 
            this.lblContractNo.AutoSize = true;
            this.lblContractNo.LabelLocation = 100;
            this.lblContractNo.Location = new System.Drawing.Point(42, 8);
            this.lblContractNo.Name = "lblContractNo";
            this.lblContractNo.Size = new System.Drawing.Size(58, 13);
            this.lblContractNo.TabIndex = 1;
            this.lblContractNo.Text = "合同编号:";
            // 
            // pagRentCalcRule
            // 
            this.pagRentCalcRule.Controls.Add(this.tabVendors);
            this.pagRentCalcRule.Location = new System.Drawing.Point(4, 22);
            this.pagRentCalcRule.Name = "pagRentCalcRule";
            this.pagRentCalcRule.Padding = new System.Windows.Forms.Padding(3);
            this.pagRentCalcRule.Size = new System.Drawing.Size(760, 487);
            this.pagRentCalcRule.TabIndex = 1;
            this.pagRentCalcRule.Text = "租金计算规则";
            this.pagRentCalcRule.UseVisualStyleBackColor = true;
            // 
            // tabVendors
            // 
            this.tabVendors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabVendors.Location = new System.Drawing.Point(3, 3);
            this.tabVendors.Name = "tabVendors";
            this.tabVendors.SelectedIndex = 0;
            this.tabVendors.Size = new System.Drawing.Size(754, 481);
            this.tabVendors.TabIndex = 0;
            // 
            // ContractEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 678);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ContractEdit";
            this.Padding = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.Text = "合同编辑";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.pnlBottom, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpContractHistory.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.pagContractBasicInfo.ResumeLayout(false);
            this.pagContractBasicInfo.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.grpVendor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendor)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.grpEntityInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsContract)).EndInit();
            this.grpRenter.ResumeLayout(false);
            this.grpRenter.PerformLayout();
            this.pagRentCalcRule.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpContractHistory;
        private System.Windows.Forms.ListBox lstContractHistory;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pagContractBasicInfo;
        private System.Windows.Forms.TabPage pagRentCalcRule;
        private System.Windows.Forms.GroupBox grpEntityInfo;
        private System.Windows.Forms.GroupBox grpVendor;
        private System.Windows.Forms.GroupBox grpRenter;
        private System.Windows.Forms.TextBox txtContractNo;
        private MCD.Controls.UCLabel lblContractUpdateRemark;
        private MCD.Controls.UCLabel lblContractRemark;
        private MCD.Controls.UCLabel lblContractNo;
        private MCD.Controls.UCLabel lblChooseCompany;
        private System.Windows.Forms.TextBox txtContractUpdateRemark;
        private System.Windows.Forms.TextBox txtContractRemark;
        private System.Windows.Forms.TextBox txtCompanyRemark;
        private System.Windows.Forms.TextBox txtSimpleName;
        private MCD.Controls.UCComboBox cmbCompany;
        private MCD.Controls.UCLabel lblCompanyRemark;
        private MCD.Controls.UCLabel lblSimpleName;
        private System.Windows.Forms.Button btnAddVendor;
        private System.Windows.Forms.DataGridView dgvVendor;
        private System.Windows.Forms.DataGridView dgvEntity;
        private System.Windows.Forms.TabControl tabVendors;
        private System.Windows.Forms.Button btnAddEntity;
        private System.Windows.Forms.BindingSource bdsContract;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private MCD.RLPlanning.Client.Common.SysAttach sysAttach1;
        private MCD.Controls.UCLabel ucLabel1;
        private System.Windows.Forms.Button btnDelVendor;
        private System.Windows.Forms.Button btnDelEntity;
    }
}