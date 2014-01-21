namespace MCD.RLPlanning.Client.Workflow
{
    partial class FinishedTaskDetail
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
            this.lbFixUserNameTitle = new System.Windows.Forms.Label();
            this.lbFixUserName = new System.Windows.Forms.Label();
            this.lbFinishTimeTitle = new System.Windows.Forms.Label();
            this.lbFinishTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTaskNo
            // 
            this.lbTaskNo.Size = new System.Drawing.Size(0, 13);
            this.lbTaskNo.Text = "";
            // 
            // lbTaskType
            // 
            this.lbTaskType.Size = new System.Drawing.Size(0, 13);
            this.lbTaskType.Text = "";
            // 
            // lbStoreNo
            // 
            this.lbStoreNo.Size = new System.Drawing.Size(0, 13);
            this.lbStoreNo.Text = "";
            // 
            // lbKioskNo
            // 
            this.lbKioskNo.Size = new System.Drawing.Size(0, 13);
            this.lbKioskNo.Text = "";
            // 
            // lbCreateTime
            // 
            this.lbCreateTime.Size = new System.Drawing.Size(0, 13);
            this.lbCreateTime.Text = "";
            // 
            // lbQuestType
            // 
            this.lbQuestType.Size = new System.Drawing.Size(0, 13);
            this.lbQuestType.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(-2, 358);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(168, 318);
            // 
            // lbFixUserNameTitle
            // 
            this.lbFixUserNameTitle.AutoSize = true;
            this.lbFixUserNameTitle.Location = new System.Drawing.Point(32, 276);
            this.lbFixUserNameTitle.Name = "lbFixUserNameTitle";
            this.lbFixUserNameTitle.Size = new System.Drawing.Size(55, 13);
            this.lbFixUserNameTitle.TabIndex = 93;
            this.lbFixUserNameTitle.Text = "处理人：";
            // 
            // lbFixUserName
            // 
            this.lbFixUserName.AutoSize = true;
            this.lbFixUserName.Location = new System.Drawing.Point(94, 276);
            this.lbFixUserName.Name = "lbFixUserName";
            this.lbFixUserName.Size = new System.Drawing.Size(35, 13);
            this.lbFixUserName.TabIndex = 94;
            this.lbFixUserName.Text = "admin";
            // 
            // lbFinishTimeTitle
            // 
            this.lbFinishTimeTitle.AutoSize = true;
            this.lbFinishTimeTitle.Location = new System.Drawing.Point(233, 276);
            this.lbFinishTimeTitle.Name = "lbFinishTimeTitle";
            this.lbFinishTimeTitle.Size = new System.Drawing.Size(67, 13);
            this.lbFinishTimeTitle.TabIndex = 95;
            this.lbFinishTimeTitle.Text = "完成时间：";
            // 
            // lbFinishTime
            // 
            this.lbFinishTime.AutoSize = true;
            this.lbFinishTime.Location = new System.Drawing.Point(307, 276);
            this.lbFinishTime.Name = "lbFinishTime";
            this.lbFinishTime.Size = new System.Drawing.Size(65, 13);
            this.lbFinishTime.TabIndex = 96;
            this.lbFinishTime.Text = "2012/12/21";
            // 
            // FinishedTaskDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 389);
            this.Controls.Add(this.lbFixUserNameTitle);
            this.Controls.Add(this.lbFixUserName);
            this.Controls.Add(this.lbFinishTime);
            this.Controls.Add(this.lbFinishTimeTitle);
            this.Name = "FinishedTaskDetail";
            this.Text = "FinishedTaskDetail";
            this.Controls.SetChildIndex(this.lbFinishTimeTitle, 0);
            this.Controls.SetChildIndex(this.lbFinishTime, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.lbFixUserName, 0);
            this.Controls.SetChildIndex(this.lbFixUserNameTitle, 0);
            this.Controls.SetChildIndex(this.lbStoreNo, 0);
            this.Controls.SetChildIndex(this.lbStoreNoTitle, 0);
            this.Controls.SetChildIndex(this.lbCreateTimeTitle, 0);
            this.Controls.SetChildIndex(this.lbCreateTime, 0);
            this.Controls.SetChildIndex(this.lbTaskNoTitle, 0);
            this.Controls.SetChildIndex(this.lbKioskNoTitle, 0);
            this.Controls.SetChildIndex(this.lbKioskNo, 0);
            this.Controls.SetChildIndex(this.lbTaskNo, 0);
            this.Controls.SetChildIndex(this.lbTaskType, 0);
            this.Controls.SetChildIndex(this.lbTypeTitle, 0);
            this.Controls.SetChildIndex(this.lbQuestTypeTitle, 0);
            this.Controls.SetChildIndex(this.lbQuestType, 0);
            this.Controls.SetChildIndex(this.tbRemark, 0);
            this.Controls.SetChildIndex(this.lbRemark, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFixUserNameTitle;
        private System.Windows.Forms.Label lbFixUserName;
        private System.Windows.Forms.Label lbFinishTimeTitle;
        private System.Windows.Forms.Label lbFinishTime;
    }
}