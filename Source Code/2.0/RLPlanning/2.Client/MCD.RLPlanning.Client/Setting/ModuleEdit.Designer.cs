namespace MCD.RLPlanning.Client.Setting
{
    partial class ModuleEdit
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
            this.txtModuleCode = new System.Windows.Forms.TextBox();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.lblName = new MCD.Controls.UCLabel();
            this.lblCode = new MCD.Controls.UCLabel();
            this.lblMsg = new MCD.Controls.UCLabel();
            this.txtSortIndex = new System.Windows.Forms.TextBox();
            this.lblSortIndex = new MCD.Controls.UCLabel();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.dgvFunction = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteFunc = new System.Windows.Forms.Button();
            this.btnAddFunc = new System.Windows.Forms.Button();
            this.lblX1 = new System.Windows.Forms.Label();
            this.lblX3 = new System.Windows.Forms.Label();
            this.lblX2 = new System.Windows.Forms.Label();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunction)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(216, 133);
            this.btnSave.Size = new System.Drawing.Size(96, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保 存";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(319, 133);
            // 
            // txtModuleCode
            // 
            this.txtModuleCode.Location = new System.Drawing.Point(140, 61);
            this.txtModuleCode.MaxLength = 64;
            this.txtModuleCode.Name = "txtModuleCode";
            this.txtModuleCode.Size = new System.Drawing.Size(306, 20);
            this.txtModuleCode.TabIndex = 10;
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(140, 24);
            this.txtModuleName.MaxLength = 32;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(306, 20);
            this.txtModuleName.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.LabelLocation = 134;
            this.lblName.Location = new System.Drawing.Point(39, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(96, 13);
            this.lblName.TabIndex = 14;
            this.lblName.Text = "窗体/菜单名称：";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.LabelLocation = 134;
            this.lblCode.Location = new System.Drawing.Point(39, 66);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(96, 13);
            this.lblCode.TabIndex = 15;
            this.lblCode.Text = "窗体/菜单代码：";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(463, 66);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(70, 13);
            this.lblMsg.TabIndex = 16;
            this.lblMsg.Text = "程序集.类名";
            // 
            // txtSortIndex
            // 
            this.txtSortIndex.Location = new System.Drawing.Point(140, 99);
            this.txtSortIndex.MaxLength = 4;
            this.txtSortIndex.Name = "txtSortIndex";
            this.txtSortIndex.Size = new System.Drawing.Size(306, 20);
            this.txtSortIndex.TabIndex = 17;
            // 
            // lblSortIndex
            // 
            this.lblSortIndex.AutoSize = true;
            this.lblSortIndex.LabelLocation = 134;
            this.lblSortIndex.Location = new System.Drawing.Point(69, 104);
            this.lblSortIndex.Name = "lblSortIndex";
            this.lblSortIndex.Size = new System.Drawing.Size(67, 13);
            this.lblSortIndex.TabIndex = 18;
            this.lblSortIndex.Text = "位置排序：";
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.dgvFunction);
            this.pnlDetail.Controls.Add(this.panel1);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetail.Location = new System.Drawing.Point(0, 170);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(619, 245);
            this.pnlDetail.TabIndex = 6;
            // 
            // dgvFunction
            // 
            this.dgvFunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFunction.Location = new System.Drawing.Point(0, 39);
            this.dgvFunction.Name = "dgvFunction";
            this.dgvFunction.RowTemplate.Height = 23;
            this.dgvFunction.Size = new System.Drawing.Size(619, 206);
            this.dgvFunction.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteFunc);
            this.panel1.Controls.Add(this.btnAddFunc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 39);
            this.panel1.TabIndex = 0;
            // 
            // btnDeleteFunc
            // 
            this.btnDeleteFunc.Location = new System.Drawing.Point(426, 8);
            this.btnDeleteFunc.Name = "btnDeleteFunc";
            this.btnDeleteFunc.Size = new System.Drawing.Size(75, 25);
            this.btnDeleteFunc.TabIndex = 1;
            this.btnDeleteFunc.Text = "删除功能项";
            this.btnDeleteFunc.UseVisualStyleBackColor = true;
            this.btnDeleteFunc.Click += new System.EventHandler(this.btnDeleteFunc_Click);
            // 
            // btnAddFunc
            // 
            this.btnAddFunc.Location = new System.Drawing.Point(345, 8);
            this.btnAddFunc.Name = "btnAddFunc";
            this.btnAddFunc.Size = new System.Drawing.Size(75, 25);
            this.btnAddFunc.TabIndex = 0;
            this.btnAddFunc.Text = "添加功能项";
            this.btnAddFunc.UseVisualStyleBackColor = true;
            this.btnAddFunc.Click += new System.EventHandler(this.btnAddFunc_Click);
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.ForeColor = System.Drawing.Color.Red;
            this.lblX1.Location = new System.Drawing.Point(452, 29);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(11, 13);
            this.lblX1.TabIndex = 78;
            this.lblX1.Text = "*";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX3
            // 
            this.lblX3.AutoSize = true;
            this.lblX3.ForeColor = System.Drawing.Color.Red;
            this.lblX3.Location = new System.Drawing.Point(452, 106);
            this.lblX3.Name = "lblX3";
            this.lblX3.Size = new System.Drawing.Size(11, 13);
            this.lblX3.TabIndex = 79;
            this.lblX3.Text = "*";
            this.lblX3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.ForeColor = System.Drawing.Color.Red;
            this.lblX2.Location = new System.Drawing.Point(452, 68);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(11, 13);
            this.lblX2.TabIndex = 80;
            this.lblX2.Text = "*";
            this.lblX2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ModuleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 415);
            this.Controls.Add(this.lblX2);
            this.Controls.Add(this.lblX3);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.lblSortIndex);
            this.Controls.Add(this.txtSortIndex);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtModuleCode);
            this.Controls.Add(this.txtModuleName);
            this.Controls.Add(this.pnlDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ModuleEdit";
            this.Text = "ModuleEdit";
            this.Controls.SetChildIndex(this.pnlDetail, 0);
            this.Controls.SetChildIndex(this.txtModuleName, 0);
            this.Controls.SetChildIndex(this.txtModuleCode, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblCode, 0);
            this.Controls.SetChildIndex(this.lblMsg, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.txtSortIndex, 0);
            this.Controls.SetChildIndex(this.lblSortIndex, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lblX1, 0);
            this.Controls.SetChildIndex(this.lblX3, 0);
            this.Controls.SetChildIndex(this.lblX2, 0);
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunction)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtModuleCode;
        private System.Windows.Forms.TextBox txtModuleName;
        private MCD.Controls.UCLabel lblName;
        private MCD.Controls.UCLabel lblCode;
        private MCD.Controls.UCLabel lblMsg;
        private System.Windows.Forms.TextBox txtSortIndex;
        private MCD.Controls.UCLabel lblSortIndex;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.DataGridView dgvFunction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteFunc;
        private System.Windows.Forms.Button btnAddFunc;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.Label lblX3;
        private System.Windows.Forms.Label lblX2;

    }
}