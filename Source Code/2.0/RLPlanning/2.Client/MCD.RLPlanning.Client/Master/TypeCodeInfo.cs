using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Common;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TypeCodeInfo : BasePhase
    {
        //Fields
        SelectBLL SelectBLL = new SelectBLL();
        private TypeCodeBLL TypeCodeBLL = new TypeCodeBLL();
        #region ctor

        public TypeCodeInfo()
        {
            InitializeComponent();
        }
        #endregion

        private void TypeCodeInfo_Load(object sender, EventArgs e)
        {
            // 绑定租金类型 不变表
            DataTable dtValue = base.GetDataTable("RentType", () => {
                DataSet dsRentType = this.SelectBLL.SelectRentType();
                if (dsRentType != null && dsRentType.Tables.Count > 0)
                {
                    return dsRentType.Tables[0];
                }
                return null;
            });
            DataTable dt = dtValue.Copy();
            DataRow row = dt.NewRow();//默认行
            row["txt"] = string.Empty;
            row["val"] = string.Empty;
            dt.Rows.InsertAt(row, 0);
            ControlHelper.BindComboBox(this.ddlRentType, dt, "txt", "val");
            // 绑定实体类型 不变表
            dtValue = base.GetDataTable("EntityType", () =>
            {
                DataSet dsEntityType = this.SelectBLL.SelectEntityType();
                if (dsEntityType != null && dsEntityType.Tables.Count > 0)
                {
                    return dsEntityType.Tables[0];
                }
                return null;
            }); 
            dt = dtValue.Copy();
            row = dt.NewRow();//默认行
            row["txt"] = string.Empty;
            row["val"] = string.Empty;
            dt.Rows.InsertAt(row, 0);
            ControlHelper.BindComboBox(this.ddlEntityType, dt, "txt", "val");
            // 绑定现有的TypeCode Status
            DataSet dsTypeCodeStatus = this.SelectBLL.SelectTypeCodeStatus();
            ControlHelper.BindComboBox(this.ddlStatus, dsTypeCodeStatus);
        }
        /// <summary>
        /// 打開編輯窗體
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && base.dgvList.CurrentCell != null)
            {
                string typeCodeSnapshotID = base.dgvList.CurrentRow.Cells["TypeCodeSnapshotID"].Value + string.Empty;
                //
                TypeCodeEdit frm = new TypeCodeEdit(typeCodeSnapshotID) { 
                    ParentFrm = this, 
                    ReadOnly = true
                };
                frm.ShowDialog();
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SelectBLL.Dispose();
            this.TypeCodeBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            string typecode = this.txtTypeCode.Text.Trim();
            string rentType = this.ddlRentType.Text;
            string entityType = this.ddlEntityType.Text;
            string status = this.ddlStatus.Text;
            //
            DataTable dt = null;
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    dt = this.TypeCodeBLL.SelectAllTypeCode(typecode, rentType, entityType,status).Tables[0];
                }, base.GetMessage("Wait"), () =>
                {
                    this.TypeCodeBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取typecode信息数据错误", "typecode信息");
            if (dt == null) return;
            //
            this.dgvList.DataSource = dt;
            this.dgvList.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(dgvList, "TypeCodeSnapshotID", base.GetMessage("TypeCodeSnapshotID"), 1);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "TypeCodeName", base.GetMessage("TypeCode"), 150);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "RentTypeName", base.GetMessage("RentTypeName"), 85);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "EntityTypeName", base.GetMessage("EntityTypeName"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "Description", base.GetMessage("Description"), 150);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YTGLDebit", base.GetMessage("YTGLDebit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YTGLCredit", base.GetMessage("YTGLCredit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YTAPNormal", base.GetMessage("YTAPNormal"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YTAPDifferences", base.GetMessage("YTAPDifferences"), 60);
            //GridViewHelper.AppendColumnToDataGridView(dgvList, "YTRemark", base.GetMessage("YTRemark"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YFGLDebit", base.GetMessage("YFGLDebit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YFGLCredit", base.GetMessage("YFGLCredit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YFAPNormal", base.GetMessage("YFAPNormal"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "YFAPDifferences", base.GetMessage("YFAPDifferences"), 60);
            //GridViewHelper.AppendColumnToDataGridView(dgvList, "YFRemak", base.GetMessage("YFRemak"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "BFGLDebit", base.GetMessage("BFGLDebit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "BFGLCredit", base.GetMessage("BFGLCredit"), 60);
            //GridViewHelper.AppendColumnToDataGridView(dgvList, "BFRemak", base.GetMessage("BFRemak"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "ZXGLDebit", base.GetMessage("ZXGLDebit"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "ZXGLCredit", base.GetMessage("ZXGLCredit"), 60);
            //GridViewHelper.AppendColumnToDataGridView(dgvList, "ZXRemark", base.GetMessage("ZXRemark"), 60);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "Status", base.GetMessage("Status"), 60);
            //GridViewHelper.AppendColumnToDataGridView(dgvList, "LastModifyUserName", "LastModifyUserName", 0);
            this.dgvList.Columns[0].Visible = false;
            //
            base.BindGridList();
        }
    }
}