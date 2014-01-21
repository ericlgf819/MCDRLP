namespace MCD.RLPlanning.Client.ContractMg
{
    partial class ToolPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toollbl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // toollbl
            // 
            this.toollbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toollbl.Image = global::MCD.RLPlanning.Client.Properties.Resources.已启用;
            this.toollbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toollbl.Location = new System.Drawing.Point(796, 0);
            this.toollbl.Margin = new System.Windows.Forms.Padding(0);
            this.toollbl.Name = "toollbl";
            this.toollbl.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toollbl.Size = new System.Drawing.Size(65, 23);
            this.toollbl.TabIndex = 0;
            this.toollbl.TabStop = true;
            this.toollbl.Text = "已启用";
            this.toollbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toollbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.toollbl_LinkClicked);
            // 
            // ToolPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toollbl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ToolPanel";
            this.Size = new System.Drawing.Size(885, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel toollbl;
    }
}
