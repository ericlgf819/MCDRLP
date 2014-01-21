using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VendorInfo : BasePhase
    {
        //Fields
        VendorBLL VendorBLL = new VendorBLL();
        #region ctor
        
        public VendorInfo()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.VendorBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            base.ShowPager = true;
            string vendorName = this.txtVendorMame.Text.Trim();
            string vendorNo = this.txtVendorNo.Text.Trim();
            //
            int recordCount = 0;
            DataTable dt = null;
            base.ExecuteAction(() => {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet dsVender = this.VendorBLL.SelectVendorPagedResult(vendorName, vendorNo, base.CurrentPageIndex, base.PageSize, out recordCount);
                    if (dsVender != null && dsVender.Tables.Count > 0)
                    {
                        dt = dsVender.Tables[0];
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.VendorBLL.CloseService();
                });
                frmwait.ShowDialog();                  
            }, "获取供应商信息数据错误", "供应商信息");
            if (dt == null) return;
            base.dgvList.DataSource = dt;
            base.RecordCount = recordCount;
            //
            base.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "VendorNo", base.GetMessage("VendorNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "VendorName", base.GetMessage("VendorName"), 350);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 90);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "BlockPayMent", base.GetMessage("BlockPayMent"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "PayMentType", base.GetMessage("PayMentType"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            base.BindGridList();
        }
    }
}