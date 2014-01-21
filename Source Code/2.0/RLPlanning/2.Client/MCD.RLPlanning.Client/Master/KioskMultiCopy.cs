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
    public partial class KioskMultiCopy : BaseEdit
    {
        //Fields
        private string KioskID = string.Empty;
        #region ctor

        public KioskMultiCopy(string KioskID)
        {
            InitializeComponent();
            //
            this.KioskID = KioskID;
        }
        #endregion

        //Events
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            int nCount = 0;
            int.TryParse(this.nudCount.Value.ToString(),out nCount);
            if (nCount < 1 || nCount > 100)
            {
                base.MessageInformation(base.GetMessage("CopyCount"));
                //
                this.nudCount.Focus();
                return;
            }
            //
            KioskBLL.Instance.UpdateMultiKiosk(KioskID, nCount);
            //
            if (this.ParentFrm != null)
            {
                this.ParentFrm.RefreshList = true;
            }
            this.Close();
        }
    }
}