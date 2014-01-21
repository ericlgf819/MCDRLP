using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Client.UUPModuleService;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ModuleList : BaseList
    {
        #region ctor

        public ModuleList()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void btnAddnew_Click(object sender, EventArgs e)
        {
            ModuleEdit frm = new ModuleEdit();
            frm.ShowDialog();
            //
            base.btnAddnew_Click(sender, e);
            this.GetDataSource();
        }
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell == null)
            {
                return;
            }
            //
            string moduleName = base.dgvList.Rows[base.dgvList.CurrentCell.RowIndex].Cells["ModuleName"].Value.ToString();
            if (MessageBox.Show(string.Format(base.GetMessage("ConfirmDelete"), moduleName), base.GetMessage("Caption"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Guid moduleID = new Guid(base.dgvList.Rows[base.dgvList.CurrentCell.RowIndex].Cells["ID"].Value.ToString());
                if (!base.ExecuteAction(() => {
                    base.ModuleService.DeleteModule(new ModuleEntity() { ID = moduleID });
                }, "删除模块信息错误", "模块设置"))
                {
                    return;
                }
                //
                base.btnDelete_Click(sender, e);
                this.GetDataSource();
            }
        }
        /// <summary>
        /// 双击进行编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.dgvList.CurrentCell == null)
            {
                return;
            }
            //
            Guid moduleID = new Guid(base.dgvList.Rows[base.dgvList.CurrentCell.RowIndex].Cells["ID"].Value.ToString());
            ModuleEdit frm = new ModuleEdit(moduleID) {
                ParentFrm = this
            };
            base.RefreshList = false;
            frm.ShowDialog();
            //
            if (base.RefreshList)
            {
                this.GetDataSource();
            }
        }

        //Methods
        protected override void BindFormControl()
        {
            base.BindFormControl();
            //
            base.btnAddnew.Visible = true;
            base.btnDelete.Visible = true;
        }
        /// <summary>
        /// 绑定 GridViewList
        /// </summary>
        protected override void BindGridList()
        {
            base.BindGridList();
            //
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(base.dgvList, "SystemName", base.GetMessage("SystemName"), 166);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(base.dgvList, "ModuleName", base.GetMessage("ModuleName"), 156);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(base.dgvList, "ModuleCode", base.GetMessage("ModuleCode"), 460);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(base.dgvList, "ID", "ID", (col) => {
                col.Visible = false;
            });
            if (base.dgvList.Rows.Count > 0)
            {
                base.dgvList.Rows[0].Selected = true;
                base.dgvList.CurrentCell = base.dgvList.Rows[0].Cells[1];
            }
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            string moduleName = this.txtModuleName.Text.Trim();
            if (!base.ExecuteAction(() =>
            {
                base.DTSource = base.ModuleService.SelectModules(AppCode.SysEnvironment.SystemID, moduleName);
            }, "查询模块信息错误", "模块设置"))
            {
                return;
            }
            //设置主键
            base.DTSource.PrimaryKey = new DataColumn[] { base.DTSource.Columns["ID"] };
            base.GetDataSource();
        }
    }
}