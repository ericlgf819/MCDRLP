namespace MCD.RLPlanning.Client
{
    partial class BaseWorkflow
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.plApprove = new System.Windows.Forms.Panel();
            this.groupApprove = new System.Windows.Forms.GroupBox();
            this.tbOpinion = new System.Windows.Forms.RichTextBox();
            this.lblRejectType = new System.Windows.Forms.Label();
            this.lblOpinion = new System.Windows.Forms.Label();
            this.plBtns = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnForwardTo = new System.Windows.Forms.Button();
            this.lbViewHistory = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.plApprove.SuspendLayout();
            this.groupApprove.SuspendLayout();
            this.plBtns.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pnlBottom
            // 
            this.pnlBottom.AutoSize = true;
            this.pnlBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBottom.Controls.Add(this.plApprove);
            this.pnlBottom.Controls.Add(this.plBtns);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(7, 263);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(719, 135);
            this.pnlBottom.TabIndex = 1;
            // 
            // plApprove
            // 
            this.plApprove.Controls.Add(this.groupApprove);
            this.plApprove.Dock = System.Windows.Forms.DockStyle.Top;
            this.plApprove.Location = new System.Drawing.Point(0, 0);
            this.plApprove.Name = "plApprove";
            this.plApprove.Size = new System.Drawing.Size(719, 102);
            this.plApprove.TabIndex = 46;
            // 
            // groupApprove
            // 
            this.groupApprove.Controls.Add(this.tbOpinion);
            this.groupApprove.Controls.Add(this.lblRejectType);
            this.groupApprove.Controls.Add(this.lblOpinion);
            this.groupApprove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupApprove.Location = new System.Drawing.Point(0, 0);
            this.groupApprove.Name = "groupApprove";
            this.groupApprove.Size = new System.Drawing.Size(719, 102);
            this.groupApprove.TabIndex = 0;
            this.groupApprove.TabStop = false;
            this.groupApprove.Text = "审批意见";
            // 
            // tbOpinion
            // 
            this.tbOpinion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpinion.Location = new System.Drawing.Point(127, 49);
            this.tbOpinion.MaxLength = 1000;
            this.tbOpinion.Name = "tbOpinion";
            this.tbOpinion.Size = new System.Drawing.Size(434, 43);
            this.tbOpinion.TabIndex = 51;
            this.tbOpinion.Text = "";
            // 
            // lblRejectType
            // 
            this.lblRejectType.AutoSize = true;
            this.lblRejectType.Location = new System.Drawing.Point(68, 19);
            this.lblRejectType.Name = "lblRejectType";
            this.lblRejectType.Size = new System.Drawing.Size(53, 12);
            this.lblRejectType.TabIndex = 52;
            this.lblRejectType.Text = "拒绝类型";
            // 
            // lblOpinion
            // 
            this.lblOpinion.AutoSize = true;
            this.lblOpinion.Location = new System.Drawing.Point(68, 48);
            this.lblOpinion.Name = "lblOpinion";
            this.lblOpinion.Size = new System.Drawing.Size(53, 12);
            this.lblOpinion.TabIndex = 50;
            this.lblOpinion.Text = "审批意见";
            // 
            // plBtns
            // 
            this.plBtns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plBtns.Controls.Add(this.flowLayoutPanel1);
            this.plBtns.Controls.Add(this.lbViewHistory);
            this.plBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBtns.Location = new System.Drawing.Point(0, 102);
            this.plBtns.Name = "plBtns";
            this.plBtns.Size = new System.Drawing.Size(719, 33);
            this.plBtns.TabIndex = 45;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnReject);
            this.flowLayoutPanel1.Controls.Add(this.btnPass);
            this.flowLayoutPanel1.Controls.Add(this.btnSend);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveData);
            this.flowLayoutPanel1.Controls.Add(this.btnForwardTo);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(217, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(502, 33);
            this.flowLayoutPanel1.TabIndex = 35;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(434, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnClick);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(363, 6);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(65, 23);
            this.btnReject.TabIndex = 37;
            this.btnReject.Text = "拒绝";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.OnClick);
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(292, 6);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(65, 23);
            this.btnPass.TabIndex = 36;
            this.btnPass.Text = "通过";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.OnClick);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(221, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 23);
            this.btnSend.TabIndex = 35;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnClick);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(150, 6);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(65, 23);
            this.btnSaveData.TabIndex = 38;
            this.btnSaveData.Text = "暂存";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.OnClick);
            // 
            // btnForwardTo
            // 
            this.btnForwardTo.Location = new System.Drawing.Point(79, 6);
            this.btnForwardTo.Name = "btnForwardTo";
            this.btnForwardTo.Size = new System.Drawing.Size(65, 23);
            this.btnForwardTo.TabIndex = 39;
            this.btnForwardTo.Text = "转发任务";
            this.btnForwardTo.Visible = false;
            this.btnForwardTo.Click += new System.EventHandler(this.btnForwardTo_Click);
            // 
            // lbViewHistory
            // 
            this.lbViewHistory.AutoSize = true;
            this.lbViewHistory.Location = new System.Drawing.Point(13, 11);
            this.lbViewHistory.Name = "lbViewHistory";
            this.lbViewHistory.Size = new System.Drawing.Size(77, 12);
            this.lbViewHistory.TabIndex = 34;
            this.lbViewHistory.TabStop = true;
            this.lbViewHistory.Text = "查看流转过程";
            this.lbViewHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbViewHistory_LinkClicked);
            // 
            // BaseWorkflow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 405);
            this.Controls.Add(this.pnlBottom);
            this.Name = "BaseWorkflow";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.ShowInTaskbar = false;
            this.Text = "BaseWorkflow";
            this.Load += new System.EventHandler(this.BaseWorkflow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.plApprove.ResumeLayout(false);
            this.groupApprove.ResumeLayout(false);
            this.groupApprove.PerformLayout();
            this.plBtns.ResumeLayout(false);
            this.plBtns.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupApprove;
        private System.Windows.Forms.RichTextBox tbOpinion;
        private System.Windows.Forms.Label lblRejectType;
        private System.Windows.Forms.Label lblOpinion;
        private System.Windows.Forms.LinkLabel lbViewHistory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnForwardTo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel plApprove;
        private System.Windows.Forms.Panel plBtns;
        public System.Windows.Forms.Panel pnlBottom;
    }
}