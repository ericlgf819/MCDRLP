using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmError : BaseFrm
    {
        //Properties
        private Exception ex { get; set; }
        #region ctor

        public FrmError(Exception e)
        {
            this.ex = e;
            //
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmError_Load(object sender, EventArgs e)
        {
            this.lblErrorTitle.Text = ex.Message;
            this.txtDetails.Text = ex.ToString();
            //
            this.SetFormHeight();
        }
        /// <summary>
        /// 显示详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetail_Click(object sender, EventArgs e)
        {
            this.pnlDetail.Visible = !this.pnlDetail.Visible;
            //
            this.SetFormHeight();
        }

        /// <summary>
        /// 设置窗体居中
        /// </summary>
        private void SetFormHeight()
        {
            if (this.pnlDetail.Visible == false)
            {
                base.Height = 157;
            }
            else
            {
                base.Height = 417;
            }
            ControlHelper.AlignCenterForm(this);
        }
    }
}