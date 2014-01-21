namespace MCD.RLPlanning.Client.Master
{
    partial class KioskAdd
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
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.groupHistory = new System.Windows.Forms.GroupBox();
            this.groupKiosInfo = new System.Windows.Forms.GroupBox();
            this.lblActiveDateRequired = new System.Windows.Forms.Label();
            this.txtNewKioskNo = new System.Windows.Forms.TextBox();
            this.dtpActiveDate = new System.Windows.Forms.DateTimePicker();
            this.lblActiveDate = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblIsNeedSubtractSalseRequired = new System.Windows.Forms.Label();
            this.ddlIsNeedSubtractSalse = new System.Windows.Forms.ComboBox();
            this.lblStoreNoRequired = new System.Windows.Forms.Label();
            this.lblKioskTypeRequired = new System.Windows.Forms.Label();
            this.lblAddressRequired = new System.Windows.Forms.Label();
            this.lblKioskNameRequired = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.ddlStoreNo = new System.Windows.Forms.ComboBox();
            this.ddlKioskType = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblIsNeedSubtractSalse = new System.Windows.Forms.Label();
            this.lblStoreNo = new System.Windows.Forms.Label();
            this.lblKioskType = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtKioskName = new System.Windows.Forms.TextBox();
            this.lblKioskName = new System.Windows.Forms.Label();
            this.lblKioskNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.groupHistory.SuspendLayout();
            this.groupKiosInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 598);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(377, 598);
            // 
            // dgvHistory
            // 
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(7, 21);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowTemplate.Height = 23;
            this.dgvHistory.Size = new System.Drawing.Size(711, 161);
            this.dgvHistory.TabIndex = 0;
            this.dgvHistory.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHistory_CellFormatting);
            // 
            // groupHistory
            // 
            this.groupHistory.Controls.Add(this.dgvHistory);
            this.groupHistory.Location = new System.Drawing.Point(7, 384);
            this.groupHistory.Name = "groupHistory";
            this.groupHistory.Padding = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.groupHistory.Size = new System.Drawing.Size(725, 190);
            this.groupHistory.TabIndex = 24;
            this.groupHistory.TabStop = false;
            this.groupHistory.Text = "挂靠记录";
            // 
            // groupKiosInfo
            // 
            this.groupKiosInfo.Controls.Add(this.lblActiveDateRequired);
            this.groupKiosInfo.Controls.Add(this.txtNewKioskNo);
            this.groupKiosInfo.Controls.Add(this.dtpActiveDate);
            this.groupKiosInfo.Controls.Add(this.lblActiveDate);
            this.groupKiosInfo.Controls.Add(this.lblNote);
            this.groupKiosInfo.Controls.Add(this.lblIsNeedSubtractSalseRequired);
            this.groupKiosInfo.Controls.Add(this.ddlIsNeedSubtractSalse);
            this.groupKiosInfo.Controls.Add(this.lblStoreNoRequired);
            this.groupKiosInfo.Controls.Add(this.lblKioskTypeRequired);
            this.groupKiosInfo.Controls.Add(this.lblAddressRequired);
            this.groupKiosInfo.Controls.Add(this.lblKioskNameRequired);
            this.groupKiosInfo.Controls.Add(this.txtDescription);
            this.groupKiosInfo.Controls.Add(this.ddlStoreNo);
            this.groupKiosInfo.Controls.Add(this.ddlKioskType);
            this.groupKiosInfo.Controls.Add(this.lblDescription);
            this.groupKiosInfo.Controls.Add(this.lblIsNeedSubtractSalse);
            this.groupKiosInfo.Controls.Add(this.lblStoreNo);
            this.groupKiosInfo.Controls.Add(this.lblKioskType);
            this.groupKiosInfo.Controls.Add(this.txtAddress);
            this.groupKiosInfo.Controls.Add(this.lblAddress);
            this.groupKiosInfo.Controls.Add(this.txtKioskName);
            this.groupKiosInfo.Controls.Add(this.lblKioskName);
            this.groupKiosInfo.Controls.Add(this.lblKioskNo);
            this.groupKiosInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupKiosInfo.Location = new System.Drawing.Point(0, 0);
            this.groupKiosInfo.Name = "groupKiosInfo";
            this.groupKiosInfo.Size = new System.Drawing.Size(736, 378);
            this.groupKiosInfo.TabIndex = 78;
            this.groupKiosInfo.TabStop = false;
            this.groupKiosInfo.Text = "甜品店信息";
            // 
            // lblActiveDateRequired
            // 
            this.lblActiveDateRequired.AutoSize = true;
            this.lblActiveDateRequired.ForeColor = System.Drawing.Color.Red;
            this.lblActiveDateRequired.Location = new System.Drawing.Point(325, 240);
            this.lblActiveDateRequired.Name = "lblActiveDateRequired";
            this.lblActiveDateRequired.Size = new System.Drawing.Size(11, 13);
            this.lblActiveDateRequired.TabIndex = 47;
            this.lblActiveDateRequired.Text = "*";
            // 
            // txtNewKioskNo
            // 
            this.txtNewKioskNo.Enabled = false;
            this.txtNewKioskNo.Location = new System.Drawing.Point(218, 30);
            this.txtNewKioskNo.MaxLength = 200;
            this.txtNewKioskNo.Name = "txtNewKioskNo";
            this.txtNewKioskNo.Size = new System.Drawing.Size(235, 20);
            this.txtNewKioskNo.TabIndex = 46;
            // 
            // dtpActiveDate
            // 
            this.dtpActiveDate.Location = new System.Drawing.Point(216, 236);
            this.dtpActiveDate.Name = "dtpActiveDate";
            this.dtpActiveDate.Size = new System.Drawing.Size(104, 20);
            this.dtpActiveDate.TabIndex = 45;
            // 
            // lblActiveDate
            // 
            this.lblActiveDate.AutoSize = true;
            this.lblActiveDate.Location = new System.Drawing.Point(153, 239);
            this.lblActiveDate.Name = "lblActiveDate";
            this.lblActiveDate.Size = new System.Drawing.Size(58, 13);
            this.lblActiveDate.TabIndex = 44;
            this.lblActiveDate.Tag = "210";
            this.lblActiveDate.Text = "生效日期:";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = System.Drawing.Color.Blue;
            this.lblNote.Location = new System.Drawing.Point(476, 176);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(236, 13);
            this.lblNote.TabIndex = 43;
            this.lblNote.Text = "注：输入餐厅编号后按Enter键进行模糊查询";
            // 
            // lblIsNeedSubtractSalseRequired
            // 
            this.lblIsNeedSubtractSalseRequired.AutoSize = true;
            this.lblIsNeedSubtractSalseRequired.ForeColor = System.Drawing.Color.Red;
            this.lblIsNeedSubtractSalseRequired.Location = new System.Drawing.Point(279, 210);
            this.lblIsNeedSubtractSalseRequired.Name = "lblIsNeedSubtractSalseRequired";
            this.lblIsNeedSubtractSalseRequired.Size = new System.Drawing.Size(11, 13);
            this.lblIsNeedSubtractSalseRequired.TabIndex = 42;
            this.lblIsNeedSubtractSalseRequired.Text = "*";
            // 
            // ddlIsNeedSubtractSalse
            // 
            this.ddlIsNeedSubtractSalse.FormattingEnabled = true;
            this.ddlIsNeedSubtractSalse.Location = new System.Drawing.Point(216, 204);
            this.ddlIsNeedSubtractSalse.Name = "ddlIsNeedSubtractSalse";
            this.ddlIsNeedSubtractSalse.Size = new System.Drawing.Size(57, 21);
            this.ddlIsNeedSubtractSalse.TabIndex = 41;
            // 
            // lblStoreNoRequired
            // 
            this.lblStoreNoRequired.AutoSize = true;
            this.lblStoreNoRequired.ForeColor = System.Drawing.Color.Red;
            this.lblStoreNoRequired.Location = new System.Drawing.Point(459, 178);
            this.lblStoreNoRequired.Name = "lblStoreNoRequired";
            this.lblStoreNoRequired.Size = new System.Drawing.Size(11, 13);
            this.lblStoreNoRequired.TabIndex = 40;
            this.lblStoreNoRequired.Text = "*";
            // 
            // lblKioskTypeRequired
            // 
            this.lblKioskTypeRequired.AutoSize = true;
            this.lblKioskTypeRequired.ForeColor = System.Drawing.Color.Red;
            this.lblKioskTypeRequired.Location = new System.Drawing.Point(459, 142);
            this.lblKioskTypeRequired.Name = "lblKioskTypeRequired";
            this.lblKioskTypeRequired.Size = new System.Drawing.Size(11, 13);
            this.lblKioskTypeRequired.TabIndex = 39;
            this.lblKioskTypeRequired.Text = "*";
            // 
            // lblAddressRequired
            // 
            this.lblAddressRequired.AutoSize = true;
            this.lblAddressRequired.ForeColor = System.Drawing.Color.Red;
            this.lblAddressRequired.Location = new System.Drawing.Point(459, 106);
            this.lblAddressRequired.Name = "lblAddressRequired";
            this.lblAddressRequired.Size = new System.Drawing.Size(11, 13);
            this.lblAddressRequired.TabIndex = 38;
            this.lblAddressRequired.Text = "*";
            // 
            // lblKioskNameRequired
            // 
            this.lblKioskNameRequired.AutoSize = true;
            this.lblKioskNameRequired.ForeColor = System.Drawing.Color.Red;
            this.lblKioskNameRequired.Location = new System.Drawing.Point(459, 70);
            this.lblKioskNameRequired.Name = "lblKioskNameRequired";
            this.lblKioskNameRequired.Size = new System.Drawing.Size(11, 13);
            this.lblKioskNameRequired.TabIndex = 37;
            this.lblKioskNameRequired.Text = "*";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(216, 271);
            this.txtDescription.MaxLength = 512;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(413, 101);
            this.txtDescription.TabIndex = 35;
            this.txtDescription.Text = "";
            // 
            // ddlStoreNo
            // 
            this.ddlStoreNo.FormattingEnabled = true;
            this.ddlStoreNo.Location = new System.Drawing.Point(216, 170);
            this.ddlStoreNo.MaxLength = 50;
            this.ddlStoreNo.Name = "ddlStoreNo";
            this.ddlStoreNo.Size = new System.Drawing.Size(236, 21);
            this.ddlStoreNo.TabIndex = 34;
            this.ddlStoreNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dllStoreNo_KeyUp);
            // 
            // ddlKioskType
            // 
            this.ddlKioskType.FormattingEnabled = true;
            this.ddlKioskType.Location = new System.Drawing.Point(216, 134);
            this.ddlKioskType.MaxLength = 50;
            this.ddlKioskType.Name = "ddlKioskType";
            this.ddlKioskType.Size = new System.Drawing.Size(236, 21);
            this.ddlKioskType.TabIndex = 33;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(177, 274);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(34, 13);
            this.lblDescription.TabIndex = 32;
            this.lblDescription.Tag = "210";
            this.lblDescription.Text = "备注:";
            // 
            // lblIsNeedSubtractSalse
            // 
            this.lblIsNeedSubtractSalse.AutoSize = true;
            this.lblIsNeedSubtractSalse.Location = new System.Drawing.Point(81, 207);
            this.lblIsNeedSubtractSalse.Name = "lblIsNeedSubtractSalse";
            this.lblIsNeedSubtractSalse.Size = new System.Drawing.Size(130, 13);
            this.lblIsNeedSubtractSalse.TabIndex = 31;
            this.lblIsNeedSubtractSalse.Tag = "210";
            this.lblIsNeedSubtractSalse.Text = "是否从母店信息中减除:";
            // 
            // lblStoreNo
            // 
            this.lblStoreNo.AutoSize = true;
            this.lblStoreNo.Location = new System.Drawing.Point(154, 174);
            this.lblStoreNo.Name = "lblStoreNo";
            this.lblStoreNo.Size = new System.Drawing.Size(58, 13);
            this.lblStoreNo.TabIndex = 30;
            this.lblStoreNo.Tag = "210";
            this.lblStoreNo.Text = "归属餐厅:";
            // 
            // lblKioskType
            // 
            this.lblKioskType.AutoSize = true;
            this.lblKioskType.Location = new System.Drawing.Point(178, 137);
            this.lblKioskType.Name = "lblKioskType";
            this.lblKioskType.Size = new System.Drawing.Size(34, 13);
            this.lblKioskType.TabIndex = 29;
            this.lblKioskType.Tag = "210";
            this.lblKioskType.Text = "类型:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(216, 100);
            this.txtAddress.MaxLength = 500;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(236, 20);
            this.txtAddress.TabIndex = 28;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(178, 103);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(34, 13);
            this.lblAddress.TabIndex = 27;
            this.lblAddress.Tag = "210";
            this.lblAddress.Text = "地址:";
            // 
            // txtKioskName
            // 
            this.txtKioskName.Location = new System.Drawing.Point(216, 65);
            this.txtKioskName.MaxLength = 50;
            this.txtKioskName.Name = "txtKioskName";
            this.txtKioskName.Size = new System.Drawing.Size(236, 20);
            this.txtKioskName.TabIndex = 26;
            // 
            // lblKioskName
            // 
            this.lblKioskName.AutoSize = true;
            this.lblKioskName.Location = new System.Drawing.Point(178, 68);
            this.lblKioskName.Name = "lblKioskName";
            this.lblKioskName.Size = new System.Drawing.Size(34, 13);
            this.lblKioskName.TabIndex = 25;
            this.lblKioskName.Tag = "210";
            this.lblKioskName.Text = "名称:";
            // 
            // lblKioskNo
            // 
            this.lblKioskNo.AutoSize = true;
            this.lblKioskNo.Location = new System.Drawing.Point(153, 33);
            this.lblKioskNo.Name = "lblKioskNo";
            this.lblKioskNo.Size = new System.Drawing.Size(60, 13);
            this.lblKioskNo.TabIndex = 23;
            this.lblKioskNo.Tag = "210";
            this.lblKioskNo.Text = "Kiosk编号:";
            // 
            // KioskAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 651);
            this.Controls.Add(this.groupKiosInfo);
            this.Controls.Add(this.groupHistory);
            this.Name = "KioskAdd";
            this.Text = "甜品店信息";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.groupHistory, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.groupKiosInfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.groupHistory.ResumeLayout(false);
            this.groupKiosInfo.ResumeLayout(false);
            this.groupKiosInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.GroupBox groupHistory;
        private System.Windows.Forms.GroupBox groupKiosInfo;
        private System.Windows.Forms.TextBox txtNewKioskNo;
        private System.Windows.Forms.DateTimePicker dtpActiveDate;
        private System.Windows.Forms.Label lblActiveDate;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblIsNeedSubtractSalseRequired;
        private System.Windows.Forms.ComboBox ddlIsNeedSubtractSalse;
        private System.Windows.Forms.Label lblStoreNoRequired;
        private System.Windows.Forms.Label lblKioskTypeRequired;
        private System.Windows.Forms.Label lblAddressRequired;
        private System.Windows.Forms.Label lblKioskNameRequired;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.ComboBox ddlStoreNo;
        private System.Windows.Forms.ComboBox ddlKioskType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblIsNeedSubtractSalse;
        private System.Windows.Forms.Label lblStoreNo;
        private System.Windows.Forms.Label lblKioskType;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtKioskName;
        private System.Windows.Forms.Label lblKioskName;
        private System.Windows.Forms.Label lblKioskNo;
        private System.Windows.Forms.Label lblActiveDateRequired;

    }
}