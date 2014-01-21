using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Controls;
using MCD.Common;
using MCD.RLPlanning.BLL;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseList : BaseFrm
    {
        /// <summary>
        /// 数据源表格
        /// </summary>
        protected DataTable DTSource = null;
        protected List<int> DecimalColumns = null;

        #region 分页相关

        /// <summary>
        /// 获取或设置是否显示分页控件。
        /// </summary>
        public bool ShowPager
        {
            get
            {
                return this.plPager.Visible;
            }
            set
            {
                this.plPager.Visible = value;
            }
        }

        /// <summary>
        /// 获取分页控件的当前页索引。
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (this.winfrmPager.PageIndex < 0)
                {
                    return 0;
                }
                return this.winfrmPager.PageIndex;
            }
            set
            {
                this.winfrmPager.PageIndex = value;
            }
        }

        /// <summary>
        /// 设置分页控件所显示的总记录数。
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.winfrmPager.RecordCount;
            }
            set
            {
                this.winfrmPager.RecordCount = value;
            }
        }

        /// <summary>
        /// 设置分页控件每页显示的条数。
        /// </summary>
        public int PageSize
        {
            get
            { 
                return this.winfrmPager.PageSize;
            }
            set
            { 
                this.winfrmPager.PageSize = value;
            }
        }

        /// <summary>
        /// 翻页时执行的事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winfrmPager_PageIndexChanged(object sender, EventArgs e)
        {
            this.GetDataSource();
        }

        /// <summary>
        /// 重新获取列表数据源
        /// </summary>
        /// <returns></returns>
        protected virtual void GetDataSource()
        {
            this.BindGridList();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected virtual void BindGridList()
        {
            this.dgvList.DataSource = this.DTSource;
            this.dgvList.Columns.Clear();

            //自动行列距离调整
            this.dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//
            this.dgvList.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvList.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            this.dgvList.DefaultCellStyle.SelectionForeColor = Color.Black;
            //
            this.dgvList.AutoGenerateColumns = false;
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.ReadOnly = true;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.MultiSelect = false;
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvList);
        }

        #endregion
        #region ctor

        public BaseList()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void BaseList_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.ShowPager = false; //默认不显示分页控件
                this.PageSize = AppCode.SysEnvironment.DefaultPageSize;
                //
                this.BindFormControl();
                this.dgvList.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                this.dgvList.DefaultCellStyle.SelectionForeColor = Color.Black;
                foreach (Control ctrl in this.pnlTitle.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        ctrl.KeyDown += new KeyEventHandler(this.ctrl_KeyDown);
                    }
                }
            }
        }
        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSelect_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 文本框回车事件，触动查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSelect_Click(sender, new EventArgs());
            }
        }

        /// <summary>
        /// 刷新窗体
        /// </summary>
        public override void RefreshFrm()
        {
            this.GetDataSource();
            //
            base.RefreshFrm();
        }
        /// <summary>
        /// 绑定界面控件
        /// </summary>
        protected virtual void BindFormControl()
        {

        }

        /// <summary>
        /// 重置指定容器上的控件值
        /// </summary>
        /// <param name="ctrl">容器</param>
        private void ResetCtrls(Control ctrls)
        {
            foreach (Control ctrl in ctrls.Controls)
            {
                if (ctrl.HasChildren)
                {
                    this.ResetCtrls(ctrl);
                }
                else
                {
                    if (ctrl is TextBox)
                    {
                        (ctrl as TextBox).Text = String.Empty;
                    }
                    else if (ctrl is ComboBox)
                    {
                        (ctrl as ComboBox).SelectedIndex = 0;
                    }
                    else if (ctrl is CheckBox)
                    {
                        (ctrl as CheckBox).Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// 設置DataGridView選中行
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        protected virtual void SetGridSelectedRow(string keyName, string keyValue)
        {
            foreach (DataGridViewRow row in this.dgvList.Rows)
            {
                if (row.Cells[keyName].Value.ToString() == keyValue)
                {
                    row.Selected = true;
                    //
                    foreach (DataGridViewColumn col in this.dgvList.Columns)
                    {
                        if (col.Visible == true)
                        {
                            this.dgvList.CurrentCell = row.Cells[col.DataPropertyName];
                            break;
                        }
                    }
                }
                else
                {
                    row.Selected = false;
                }
            }
        }
        
        #region 工具栏按钮事件

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnAddnew_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnEdit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnPreview_Click(object sender, EventArgs e)
        {
            if (this.dgvList.DataSource != null)
            {
                this.CurrentPageIndex = 0;
                this.PageSize = int.MaxValue;
                this.winfrmPager.ChangePage(EventArgs.Empty);
                //
                GridViewHelper.Print_DataGridView(this.dgvList);
                //
                this.PageSize = AppCode.SysEnvironment.DefaultPageSize;
                this.winfrmPager.ChangePage(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dgvList.DataSource != null)
            {
                this.CurrentPageIndex = 0;
                this.PageSize = int.MaxValue;
                this.winfrmPager.ChangePage(EventArgs.Empty);
                //
                GridViewHelper.DataGridViewToExcel(this.Text, this.Text, this.dgvList, this.DecimalColumns);
                //
                this.PageSize = AppCode.SysEnvironment.DefaultPageSize;//
                this.winfrmPager.ChangePage(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 关闭方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSelect_Click(object sender, EventArgs e)
        {
            this.GetDataSource();
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSaveAs_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnReset_Click(object sender, EventArgs e)
        {
            this.ResetCtrls(this.pnlTitle);
            //
            this.GetDataSource();
        }

        /// <summary>
        /// 检查关帐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCheckCloseAccount_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 执行关帐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCloseAccount_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 执行新建合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCreateContract_Click(object sender, EventArgs e)
        {

        }

        //导入预测Sales
        protected virtual void btnKeyInSales_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region DataGridView事件

        /// <summary>
        /// 数据行绘制之前发生事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        /// <summary>
        /// GridView行选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null) return;

            // 设置整个行为选中状态
            this.dgvList.Rows[this.dgvList.CurrentCell.RowIndex].Selected = true;
        }

        /// <summary>
        /// 单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 单击单元格按钮的时候触发。
        /// </summary>
        /// <param name="e">按钮所在的行列信息</param>
        /// <param name="columnName">按钮所在的列的名称</param>
        protected virtual void OnRowButtonClick(DataGridViewCellEventArgs e, string columnName)
        {

        }

        /// <summary>
        /// 单元格内容单击事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn column = this.dgvList.Columns[e.ColumnIndex];
            if ((column.CellType == typeof(DataGridViewButtonCell) || column.CellType == typeof(DataGridViewImageCell))
                && !string.IsNullOrEmpty(column.Name))
            {
                this.OnRowButtonClick(e, column.Name);
            }
        }

        /// <summary>
        /// 格式化显示的类容。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;
            //
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().Replace(@"\r\n", Environment.NewLine).Trim();
            }
            //
            if (this.dgvList.Columns[e.ColumnIndex].ValueType == typeof(DateTime))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseList.DATETIME_SHORT_FORMAT);
                }
            }
            else if(e.Value.ToString() == "True")
            {
                e.Value = "是 ";
            }
            else if (e.Value.ToString() == "False")
            {
                e.Value = "否 ";
            }
        }
        #endregion
        #region 用代码构造键值对结构的表

        /// <summary>
        /// 建立键值对结构的表
        /// </summary>
        /// <returns></returns>
        protected DataTable PrepareKeyValueTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            //
            return dt;
        }
        /// <summary>
        /// 向用代码构造的表中插入一行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected void AddNewKeyValueRow(DataTable dt, int value, string name)
        {
            DataRow row = dt.NewRow();
            row["Value"] = value;
            row["Name"] = name;
            //
            dt.Rows.Add(row);
        }

        #endregion
    }
}