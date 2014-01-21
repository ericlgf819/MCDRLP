namespace MCD.RLPlanning.Client.Common
{
    partial class UpDownloadFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpDownloadFrm));
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbFileInfo = new System.Windows.Forms.Label();
            this.lbRemainTime = new System.Windows.Forms.Label();
            this.lbProgress = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(22, 86);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(322, 13);
            this.progBar.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(367, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbFileInfo
            // 
            this.lbFileInfo.AutoSize = true;
            this.lbFileInfo.Location = new System.Drawing.Point(19, 46);
            this.lbFileInfo.Name = "lbFileInfo";
            this.lbFileInfo.Size = new System.Drawing.Size(49, 13);
            this.lbFileInfo.TabIndex = 2;
            this.lbFileInfo.Text = "lbFileInfo";
            // 
            // lbRemainTime
            // 
            this.lbRemainTime.AutoSize = true;
            this.lbRemainTime.Location = new System.Drawing.Point(19, 104);
            this.lbRemainTime.Name = "lbRemainTime";
            this.lbRemainTime.Size = new System.Drawing.Size(74, 13);
            this.lbRemainTime.TabIndex = 3;
            this.lbRemainTime.Text = "lbRemainTime";
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(288, 104);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(56, 13);
            this.lbProgress.TabIndex = 4;
            this.lbProgress.Text = "lbProgress";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(177, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = global::MCD.RLPlanning.Client.Properties.Resources.UploadTo;
            this.pictureBox2.Image = global::MCD.RLPlanning.Client.Properties.Resources.UploadTo;
            this.pictureBox2.InitialImage = global::MCD.RLPlanning.Client.Properties.Resources.UploadTo;
            this.pictureBox2.Location = new System.Drawing.Point(302, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 32);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::MCD.RLPlanning.Client.Properties.Resources.UploadSource;
            this.pictureBox1.Image = global::MCD.RLPlanning.Client.Properties.Resources.UploadSource;
            this.pictureBox1.InitialImage = global::MCD.RLPlanning.Client.Properties.Resources.UploadSource;
            this.pictureBox1.Location = new System.Drawing.Point(36, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 32);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(19, 66);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(37, 13);
            this.lbPath.TabIndex = 8;
            this.lbPath.Text = "lbPath";
            // 
            // UpDownloadFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 127);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.lbRemainTime);
            this.Controls.Add(this.lbFileInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpDownloadFrm";
            this.Text = "ProgressFrm";
            this.Load += new System.EventHandler(this.ProgressFrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpDownloadFrm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbFileInfo;
        private System.Windows.Forms.Label lbRemainTime;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbPath;
    }
}