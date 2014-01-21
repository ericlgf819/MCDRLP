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
    public partial class AreaInfo : BaseList
    {
        //Fields
        private AreaBLL AreaBLL = new AreaBLL();
        #region ctor

        public AreaInfo()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().Replace(@"\r\n", Environment.NewLine).Trim();
                //
                if (e.ColumnIndex == 3 && e.Value.ToString() != string.Empty)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseFrm.DATETIME_LONG_FORMAT);
                }
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AreaBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            base.btnReset.Visible = false;
        }
        protected override void BindGridList()
        {
            //区域信息表 -- 同步表,可缓存
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    base.DTSource = base.GetDataTable("Area", () =>
                    {
                        DataSet dsValue = this.AreaBLL.SelectAreas();
                        if (dsValue != null && dsValue.Tables.Count > 0)
                        {
                            return dsValue.Tables[0];
                        }
                        else
                        {
                            return null;
                        }
                    });
                }, base.GetMessage("Wait"), () => { 
                    this.AreaBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取区域信息数据错误", "区域信息");
            //
            base.BindGridList();
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", "ID", 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AreaName", base.GetMessage("AreaName"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Remark", base.GetMessage("Remark"), 150);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "UpdateTime", base.GetMessage("UpdateTime"), 120);
            base.dgvList.Columns[0].Visible = false;
        }
    }
}