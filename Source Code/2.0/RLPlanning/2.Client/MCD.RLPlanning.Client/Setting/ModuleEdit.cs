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
    /// 功能类型
    /// </summary>
    public enum FunctionType
    {
        查看,
        操作,
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ModuleEdit : BaseEdit
    {
        //Fields
        private Guid moduleID = Guid.NewGuid();
        private ModuleEntity currentModule = null;
        private DataTable tableFunction = null;

        //Properties
        /// <summary>
        /// 功能ID
        /// </summary>
        public Guid ModuleID
        {
            get
            {
                return this.moduleID;
            }
        }
        /// <summary>
        /// 当前编辑的主表实体
        /// </summary>
        public ModuleEntity CurrentModule
        {
            get
            {
                if (this.currentModule == null)
                {
                    if (base.IsAddNew)
                    {
                        this.currentModule = new ModuleEntity() { ID = this.ModuleID };
                    }
                    else
                    {
                        this.currentModule = base.ModuleService.GetSingleModule(new ModuleEntity() { ID = this.ModuleID });
                    }
                }
                return this.currentModule;
            }
        }
        /// <summary>
        /// 功能项
        /// </summary>
        public DataTable TableFunction
        {
            get
            {
                if (this.tableFunction == null)
                {
                    this.tableFunction = base.ModuleService.GetFunctionByModuleID(this.ModuleID);
                    this.tableFunction.PrimaryKey = new DataColumn[] { this.tableFunction.Columns["ID"] };
                }
                return this.tableFunction;
            }
        }
        #region ctor

        public ModuleEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="moduleID"></param>
        public ModuleEdit(Guid moduleID)
        {
            InitializeComponent();
            //
            this.moduleID = moduleID;
            this.IsAddNew = false;
        }
        #endregion

        #region 按钮操作

        /// <summary>
        /// 添加功能项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFunc_Click(object sender, EventArgs e)
        {
            DataRow row = this.TableFunction.NewRow();
            row["ID"] = Guid.NewGuid();
            row["ModuleID"] = this.ModuleID;
            //
            this.TableFunction.Rows.Add(row);
        }
        /// <summary>
        /// 删除功能信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteFunc_Click(object sender, EventArgs e)
        {
            if (this.dgvFunction.CurrentCell == null) return;
            //
            if (MessageBox.Show(base.GetMessage("ConfirmDelete"), base.GetMessage("Caption"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                object id = this.dgvFunction.Rows[this.dgvFunction.CurrentCell.RowIndex].Cells["ID"].Value;
                this.TableFunction.Rows.Find(id).Delete();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            // 先结束 DataGridView 的编辑状态
            this.dgvFunction.Update();
            this.dgvFunction.EndEdit();
            // 再结束数据源的编辑状态
            base.BindingContext[this.CurrentModule].EndCurrentEdit();
            base.BindingContext[this.TableFunction].EndCurrentEdit();

            // 界面数据校验
            if (this.txtModuleName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(base.GetMessage("ModuleNameNULL"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtModuleName.Focus();
                return;
            }
            if (this.txtModuleCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show(base.GetMessage("ModuleCodeNULL"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtModuleCode.Focus();
                return;
            }
            int val = 0;
            if (!int.TryParse(this.txtSortIndex.Text, out val))
            {
                MessageBox.Show(base.GetMessage("SortIndexNotNumber"), base.GetMessage("Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtSortIndex.Focus();
                return;
            }
            // 校验功能项是否输入完整
            object functionTypeValue;
            foreach (DataGridViewRow row in this.dgvFunction.Rows)
            {
                functionTypeValue = row.Cells["FunctionType"].Value;
                if (row.Cells[0].Value.ToString().Trim() == string.Empty || row.Cells[1].Value.ToString().Trim() == string.Empty ||
                    functionTypeValue == null || functionTypeValue == DBNull.Value || functionTypeValue.ToString().Trim() == string.Empty)
                {
                    base.MessageError(base.GetMessage("FuncNULL"));
                    return;
                }
            }
            // 检验模块代码是否已经存在
            this.CurrentModule.SystemID = AppCode.SysEnvironment.SystemID;
            if (base.ModuleService.IsExistsModuleCode(this.CurrentModule.SystemID,
              base.IsAddNew ? Guid.Empty : this.CurrentModule.ID, // 新增时，不需要增加ModuleID判断
             this.CurrentModule.ModuleCode.Trim()))
            {
                base.MessageError(base.GetMessage("ModuleCodeExists"));
                return;
            }
            if (base.IsAddNew)
            {// 新增
                if (base.ExecuteBoolean(() =>
                {
                    if (base.ModuleService.InsertModule(this.CurrentModule, this.TableFunction))
                    {
                        this.TableFunction.AcceptChanges();
                        base.IsAddNew = false;
                        base.MessageInformation(base.GetMessage("SaveSuccess"));
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }, "新增模块信息", "模块信息保存失败!"))
                {
                    if (base.ParentFrm != null)
                    {
                        base.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                };
            }
            else
            {// 编辑
                if (base.ExecuteBoolean(() =>
                {
                    if (base.ModuleService.UpdateModule(this.CurrentModule, this.TableFunction))
                    {
                        this.TableFunction.AcceptChanges();
                        base.MessageInformation(base.GetMessage("SaveSuccess"));
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }, "新增模块信息", "模块信息保存失败!"))
                {
                    if (base.ParentFrm != null)
                    {
                        base.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                };
            }
        }
        #endregion
        
        //Methods
        /// <summary>
        /// 绑定界面控件
        /// </summary>
        public override void BindFormControl()
        {
            this.txtModuleCode.DataBindings.Clear();
            this.txtModuleName.DataBindings.Clear();
            // 绑定主表数据
            this.txtModuleCode.DataBindings.Add("Text", this.CurrentModule, "ModuleCode");
            this.txtModuleName.DataBindings.Add("Text", this.CurrentModule, "ModuleName");
            this.txtSortIndex.DataBindings.Add("Text", this.CurrentModule, "SortIndex");
            // 绑定从表数据
            this.dgvFunction.Columns.Clear();
            this.dgvFunction.AutoGenerateColumns = false;
            this.dgvFunction.AllowUserToAddRows = false;
            this.dgvFunction.DataSource = TableFunction;
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(this.dgvFunction, "FunctionName", base.GetMessage("FunctionName"), 188);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(this.dgvFunction, "FunctionCode", base.GetMessage("FunctionCode"), 288);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewComboBoxColumn>(this.dgvFunction, "FunctionType", base.GetMessage("FunctionType"), 100);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewTextBoxColumn>(this.dgvFunction, "ID", "ID", (col) => { col.Visible = false; });
            this.PrepareFunctionTypeColumn();//
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvFunction);
        }
        private void PrepareFunctionTypeColumn()
        {
            DataTable dtFunctionType = new DataTable();
            dtFunctionType.Columns.Add("Name", typeof(string));
            dtFunctionType.Columns.Add("Value", typeof(string));
            DataRow row = dtFunctionType.NewRow();
            row["Name"] = row["Value"] = FunctionType.查看.ToString();
            dtFunctionType.Rows.Add(row);//
            row = dtFunctionType.NewRow();
            row["Name"] = row["Value"] = FunctionType.操作.ToString();
            dtFunctionType.Rows.Add(row);//
            //
            DataGridViewComboBoxColumn clmFunctionType = this.dgvFunction.Columns["FunctionType"] as DataGridViewComboBoxColumn;
            clmFunctionType.DataSource = dtFunctionType;
            clmFunctionType.DisplayMember = "Name";
            clmFunctionType.ValueMember = "Value";
        }
    }
}