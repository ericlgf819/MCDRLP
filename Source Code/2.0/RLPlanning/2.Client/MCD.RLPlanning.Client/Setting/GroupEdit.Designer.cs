namespace MCD.RLPlanning.Client.Setting
{
    partial class GroupEdit
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
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.lblRemark = new MCD.Controls.UCLabel();
            this.lblGroupName = new MCD.Controls.UCLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lBoxSelected = new System.Windows.Forms.ListBox();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.lboxUsers = new System.Windows.Forms.ListBox();
            this.btnRemoveOne = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnAddOne = new System.Windows.Forms.Button();
            this.lblX1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(165, 173);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 173);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(137, 53);
            this.txtRemark.MaxLength = 512;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(314, 104);
            this.txtRemark.TabIndex = 3;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(137, 16);
            this.txtGroupName.MaxLength = 32;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(314, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.LabelLocation = 131;
            this.lblRemark.Location = new System.Drawing.Point(65, 102);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(70, 13);
            this.lblRemark.TabIndex = 6;
            this.lblRemark.Text = "用户组描述:";
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.LabelLocation = 131;
            this.lblGroupName.Location = new System.Drawing.Point(65, 20);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(70, 13);
            this.lblGroupName.TabIndex = 7;
            this.lblGroupName.Text = "用户组名称:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lBoxSelected);
            this.panel2.Controls.Add(this.btnRemoveAll);
            this.panel2.Controls.Add(this.lboxUsers);
            this.panel2.Controls.Add(this.btnRemoveOne);
            this.panel2.Controls.Add(this.btnAddAll);
            this.panel2.Controls.Add(this.btnAddOne);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 211);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 259);
            this.panel2.TabIndex = 78;
            this.panel2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "已选用户";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "用户列表";
            // 
            // lBoxSelected
            // 
            this.lBoxSelected.FormattingEnabled = true;
            this.lBoxSelected.Location = new System.Drawing.Point(352, 42);
            this.lBoxSelected.Name = "lBoxSelected";
            this.lBoxSelected.Size = new System.Drawing.Size(153, 290);
            this.lBoxSelected.TabIndex = 9;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(238, 212);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(75, 25);
            this.btnRemoveAll.TabIndex = 8;
            this.btnRemoveAll.Text = "<<-";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            // 
            // lboxUsers
            // 
            this.lboxUsers.FormattingEnabled = true;
            this.lboxUsers.Location = new System.Drawing.Point(56, 42);
            this.lboxUsers.Name = "lboxUsers";
            this.lboxUsers.Size = new System.Drawing.Size(153, 290);
            this.lboxUsers.TabIndex = 4;
            // 
            // btnRemoveOne
            // 
            this.btnRemoveOne.Location = new System.Drawing.Point(238, 179);
            this.btnRemoveOne.Name = "btnRemoveOne";
            this.btnRemoveOne.Size = new System.Drawing.Size(75, 25);
            this.btnRemoveOne.TabIndex = 7;
            this.btnRemoveOne.Text = "<-";
            this.btnRemoveOne.UseVisualStyleBackColor = true;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(238, 116);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(75, 25);
            this.btnAddAll.TabIndex = 5;
            this.btnAddAll.Text = "->>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            // 
            // btnAddOne
            // 
            this.btnAddOne.Location = new System.Drawing.Point(238, 147);
            this.btnAddOne.Name = "btnAddOne";
            this.btnAddOne.Size = new System.Drawing.Size(75, 25);
            this.btnAddOne.TabIndex = 6;
            this.btnAddOne.Text = "->";
            this.btnAddOne.UseVisualStyleBackColor = true;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.ForeColor = System.Drawing.Color.Red;
            this.lblX1.Location = new System.Drawing.Point(456, 24);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(11, 13);
            this.lblX1.TabIndex = 79;
            this.lblX1.Text = "*";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GroupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 470);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblGroupName);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtGroupName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GroupEdit";
            this.Text = "用户组设置";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.txtGroupName, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.lblGroupName, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lblX1, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtGroupName;
        private MCD.Controls.UCLabel lblRemark;
        private MCD.Controls.UCLabel lblGroupName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lBoxSelected;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.ListBox lboxUsers;
        private System.Windows.Forms.Button btnRemoveOne;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAddOne;
        private System.Windows.Forms.Label lblX1;
    }
}