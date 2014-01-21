namespace MCD.RLPlanning.Client.ContractMg
{
    partial class GLAdjustment
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
            this.groupList = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpChangeEnd = new System.Windows.Forms.DateTimePicker();
            this.lblChangeDateEnd = new System.Windows.Forms.Label();
            this.dtpChangeStart = new System.Windows.Forms.DateTimePicker();
            this.lblChangeDateStart = new System.Windows.Forms.Label();
            this.lvChanges = new System.Windows.Forms.ListView();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.tbAmount = new System.Windows.Forms.NumericUpDown();
            this.cmbRentType = new System.Windows.Forms.ComboBox();
            this.lblRentType = new System.Windows.Forms.Label();
            this.tbNote = new System.Windows.Forms.RichTextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.dtpChangeDate = new System.Windows.Forms.DateTimePicker();
            this.lblChangeDate = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.groupList.SuspendLayout();
            this.groupAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(591, 67);
            this.btnSave.Size = new System.Drawing.Size(78, 26);
            this.btnSave.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.Visible = false;
            // 
            // groupList
            // 
            this.groupList.Controls.Add(this.btnReset);
            this.groupList.Controls.Add(this.btnSearch);
            this.groupList.Controls.Add(this.dtpChangeEnd);
            this.groupList.Controls.Add(this.lblChangeDateEnd);
            this.groupList.Controls.Add(this.dtpChangeStart);
            this.groupList.Controls.Add(this.lblChangeDateStart);
            this.groupList.Controls.Add(this.lvChanges);
            this.groupList.Location = new System.Drawing.Point(9, 201);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(683, 277);
            this.groupList.TabIndex = 8;
            this.groupList.TabStop = false;
            this.groupList.Text = "调整金额列表";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(594, 25);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(502, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpChangeEnd
            // 
            this.dtpChangeEnd.Location = new System.Drawing.Point(327, 28);
            this.dtpChangeEnd.Name = "dtpChangeEnd";
            this.dtpChangeEnd.Size = new System.Drawing.Size(125, 21);
            this.dtpChangeEnd.TabIndex = 4;
            // 
            // lblChangeDateEnd
            // 
            this.lblChangeDateEnd.AutoSize = true;
            this.lblChangeDateEnd.Location = new System.Drawing.Point(304, 34);
            this.lblChangeDateEnd.Name = "lblChangeDateEnd";
            this.lblChangeDateEnd.Size = new System.Drawing.Size(17, 12);
            this.lblChangeDateEnd.TabIndex = 3;
            this.lblChangeDateEnd.Tag = "323";
            this.lblChangeDateEnd.Text = "到";
            // 
            // dtpChangeStart
            // 
            this.dtpChangeStart.Location = new System.Drawing.Point(162, 28);
            this.dtpChangeStart.Name = "dtpChangeStart";
            this.dtpChangeStart.Size = new System.Drawing.Size(127, 21);
            this.dtpChangeStart.TabIndex = 2;
            // 
            // lblChangeDateStart
            // 
            this.lblChangeDateStart.AutoSize = true;
            this.lblChangeDateStart.Location = new System.Drawing.Point(83, 31);
            this.lblChangeDateStart.Name = "lblChangeDateStart";
            this.lblChangeDateStart.Size = new System.Drawing.Size(65, 12);
            this.lblChangeDateStart.TabIndex = 1;
            this.lblChangeDateStart.Tag = "153";
            this.lblChangeDateStart.Text = "调整日期从";
            // 
            // lvChanges
            // 
            this.lvChanges.Location = new System.Drawing.Point(13, 63);
            this.lvChanges.Name = "lvChanges";
            this.lvChanges.Size = new System.Drawing.Size(656, 204);
            this.lvChanges.TabIndex = 0;
            this.lvChanges.UseCompatibleStateImageBehavior = false;
            // 
            // groupAdd
            // 
            this.groupAdd.Controls.Add(this.tbAmount);
            this.groupAdd.Controls.Add(this.cmbRentType);
            this.groupAdd.Controls.Add(this.lblRentType);
            this.groupAdd.Controls.Add(this.tbNote);
            this.groupAdd.Controls.Add(this.btnSave);
            this.groupAdd.Controls.Add(this.lblNote);
            this.groupAdd.Controls.Add(this.dtpChangeDate);
            this.groupAdd.Controls.Add(this.lblChangeDate);
            this.groupAdd.Controls.Add(this.lblAmount);
            this.groupAdd.Location = new System.Drawing.Point(9, 12);
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.Size = new System.Drawing.Size(683, 183);
            this.groupAdd.TabIndex = 9;
            this.groupAdd.TabStop = false;
            this.groupAdd.Text = "新增调整金额";
            this.groupAdd.Controls.SetChildIndex(this.lblAmount, 0);
            this.groupAdd.Controls.SetChildIndex(this.lblChangeDate, 0);
            this.groupAdd.Controls.SetChildIndex(this.dtpChangeDate, 0);
            this.groupAdd.Controls.SetChildIndex(this.lblNote, 0);
            this.groupAdd.Controls.SetChildIndex(this.btnSave, 0);
            this.groupAdd.Controls.SetChildIndex(this.tbNote, 0);
            this.groupAdd.Controls.SetChildIndex(this.lblRentType, 0);
            this.groupAdd.Controls.SetChildIndex(this.cmbRentType, 0);
            this.groupAdd.Controls.SetChildIndex(this.tbAmount, 0);
            // 
            // tbAmount
            // 
            this.tbAmount.DecimalPlaces = 2;
            this.tbAmount.Location = new System.Drawing.Point(138, 69);
            this.tbAmount.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.tbAmount.Minimum = new decimal(new int[] {
            1215752191,
            23,
            0,
            -2147483648});
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(120, 21);
            this.tbAmount.TabIndex = 16;
            // 
            // cmbRentType
            // 
            this.cmbRentType.FormattingEnabled = true;
            this.cmbRentType.Location = new System.Drawing.Point(138, 36);
            this.cmbRentType.Name = "cmbRentType";
            this.cmbRentType.Size = new System.Drawing.Size(269, 20);
            this.cmbRentType.TabIndex = 15;
            this.cmbRentType.SelectionChangeCommitted += new System.EventHandler(this.cmbRentType_SelectionChangeCommitted);
            // 
            // lblRentType
            // 
            this.lblRentType.AutoSize = true;
            this.lblRentType.Location = new System.Drawing.Point(78, 39);
            this.lblRentType.Name = "lblRentType";
            this.lblRentType.Size = new System.Drawing.Size(53, 12);
            this.lblRentType.TabIndex = 14;
            this.lblRentType.Tag = "132";
            this.lblRentType.Text = "租金类型";
            // 
            // tbNote
            // 
            this.tbNote.Location = new System.Drawing.Point(138, 106);
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(414, 44);
            this.tbNote.TabIndex = 13;
            this.tbNote.Text = "";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(95, 106);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(29, 12);
            this.lblNote.TabIndex = 12;
            this.lblNote.Tag = "132";
            this.lblNote.Text = "备注";
            // 
            // dtpChangeDate
            // 
            this.dtpChangeDate.Location = new System.Drawing.Point(425, 70);
            this.dtpChangeDate.Name = "dtpChangeDate";
            this.dtpChangeDate.Size = new System.Drawing.Size(127, 21);
            this.dtpChangeDate.TabIndex = 11;
            // 
            // lblChangeDate
            // 
            this.lblChangeDate.AutoSize = true;
            this.lblChangeDate.Location = new System.Drawing.Point(354, 76);
            this.lblChangeDate.Name = "lblChangeDate";
            this.lblChangeDate.Size = new System.Drawing.Size(53, 12);
            this.lblChangeDate.TabIndex = 9;
            this.lblChangeDate.Tag = "420";
            this.lblChangeDate.Text = "调整日期";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(78, 73);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(53, 12);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Tag = "132";
            this.lblAmount.Text = "调整金额";
            // 
            // GLAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 494);
            this.Controls.Add(this.groupAdd);
            this.Controls.Add(this.groupList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GLAdjustment";
            this.ShowInTaskbar = false;
            this.Text = "GLAdjustment";
            this.Controls.SetChildIndex(this.groupList, 0);
            this.Controls.SetChildIndex(this.groupAdd, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.groupList.ResumeLayout(false);
            this.groupList.PerformLayout();
            this.groupAdd.ResumeLayout(false);
            this.groupAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupList;
        private System.Windows.Forms.DateTimePicker dtpChangeEnd;
        private System.Windows.Forms.Label lblChangeDateEnd;
        private System.Windows.Forms.DateTimePicker dtpChangeStart;
        private System.Windows.Forms.Label lblChangeDateStart;
        private System.Windows.Forms.ListView lvChanges;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.RichTextBox tbNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.DateTimePicker dtpChangeDate;
        private System.Windows.Forms.Label lblChangeDate;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.ComboBox cmbRentType;
        private System.Windows.Forms.Label lblRentType;
        private System.Windows.Forms.NumericUpDown tbAmount;
    }
}