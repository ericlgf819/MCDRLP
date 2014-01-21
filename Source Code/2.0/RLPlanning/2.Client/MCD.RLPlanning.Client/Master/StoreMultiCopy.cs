using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StoreMultiCopy : BaseEdit
    {
        //Fields
        private string StoreNO = string.Empty;
        private StoreBLL StoreBLL = new StoreBLL();
        #region ctor

        public StoreMultiCopy(string StoreNO)
        {
            InitializeComponent();
            //
            this.StoreNO = StoreNO;
        }
        #endregion

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            int nCount = 0;
            int.TryParse(this.nudCount.Value.ToString(), out nCount);
            if (nCount < 1 || nCount > 100)
            {
                base.MessageInformation(base.GetMessage("CopyCount"));
                //
                this.nudCount.Focus();
                return;
            }
            //
            this.StoreBLL.UpdateMultiStore(StoreNO, nCount);
            //
            if (this.ParentFrm != null)
            {
                this.ParentFrm.RefreshList = true;
            }
            this.Close();
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StoreBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }
    }
}