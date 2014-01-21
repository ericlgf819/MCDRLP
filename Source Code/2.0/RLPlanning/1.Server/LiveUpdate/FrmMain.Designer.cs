namespace LiveUpdate
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.prgBarFile = new System.Windows.Forms.ProgressBar();
            this.prgBarDown = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFileProg = new System.Windows.Forms.Label();
            this.lblDownProg = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgBarFile
            // 
            this.prgBarFile.Location = new System.Drawing.Point(39, 37);
            this.prgBarFile.Name = "prgBarFile";
            this.prgBarFile.Size = new System.Drawing.Size(346, 23);
            this.prgBarFile.TabIndex = 0;
            // 
            // prgBarDown
            // 
            this.prgBarDown.Location = new System.Drawing.Point(39, 79);
            this.prgBarDown.Name = "prgBarDown";
            this.prgBarDown.Size = new System.Drawing.Size(346, 23);
            this.prgBarDown.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(394, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFileProg
            // 
            this.lblFileProg.AutoSize = true;
            this.lblFileProg.Location = new System.Drawing.Point(398, 44);
            this.lblFileProg.Name = "lblFileProg";
            this.lblFileProg.Size = new System.Drawing.Size(71, 12);
            this.lblFileProg.TabIndex = 3;
            this.lblFileProg.Text = "lblFileProg";
            // 
            // lblDownProg
            // 
            this.lblDownProg.AutoSize = true;
            this.lblDownProg.Location = new System.Drawing.Point(398, 86);
            this.lblDownProg.Name = "lblDownProg";
            this.lblDownProg.Size = new System.Drawing.Size(71, 12);
            this.lblDownProg.TabIndex = 4;
            this.lblDownProg.Text = "lblDownProg";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(37, 13);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(137, 12);
            this.lblMsg.TabIndex = 5;
            this.lblMsg.Text = "请稍后，正在检测更新…";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(39, 116);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(65, 12);
            this.lblCurrent.TabIndex = 6;
            this.lblCurrent.Text = "lblCurrent";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 155);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblDownProg);
            this.Controls.Add(this.lblFileProg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.prgBarDown);
            this.Controls.Add(this.prgBarFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBarFile;
        private System.Windows.Forms.ProgressBar prgBarDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFileProg;
        private System.Windows.Forms.Label lblDownProg;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblCurrent;
    }
}

