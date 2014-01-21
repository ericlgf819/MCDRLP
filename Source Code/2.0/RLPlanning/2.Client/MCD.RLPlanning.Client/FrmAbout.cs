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
    public partial class FrmAbout : BaseFrm
    {
        #region ctor

        public FrmAbout()
        {
            InitializeComponent();
            //
            this.lblMessage.Text = "MCD Store Rent && Lease System";
            this.lblVersion.Text = AppCode.SysEnvironment.SystemSettings["CurrentVersion"];
        }
        #endregion

        /// <summary>
        /// 窗体改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAbout_Resize(object sender, EventArgs e)
        {
            ControlHelper.AlignCenterControl((Control)this, this.pboxMain);
            //
            this.pboxMain.Top -= 50;
        }
    }
}