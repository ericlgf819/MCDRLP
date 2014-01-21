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
    public partial class BasePhase : BaseFrm
    {
        #region ctor

        public BasePhase()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void BasePhase_Load(object sender, EventArgs e)
        {
            if (base.DesignMode)
            {
                return;
            }
            //默认不显示分页控件
            this.ShowPager = false;
            this.PageSize = AppCode.SysEnvironment.DefaultPageSize;
            //
            this.BindFormControl();
            foreach (Control ctrl in pnlTitle.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.KeyDown += new KeyEventHandler(this.ctrl_KeyDown);
                }
            }
        }
        /// <summary>
        /// 文本框回车事件，触动查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSelect_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 选中行时，设置整个行被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null) return;
            // 设置整个行为选中状态
            this.dgvList.Rows[this.dgvList.CurrentCell.RowIndex].Selected = true;
        }
        /// <summary>
        /// gv双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #region 按钮事件

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSelect_Click(object sender, EventArgs e)
        {
            this.BindGridList();
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
            this.BindGridList();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnNew_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 创建新合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCreateContract_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 录入预测销售数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnKeyInSales_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCopy_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnMultiCopy_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnPreview_Click(object sender, EventArgs e)
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
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnExport_Click(object sender, EventArgs e)
        {
            this.CurrentPageIndex = 0;
            this.PageSize = int.MaxValue;
            this.winfrmPager.ChangePage(EventArgs.Empty);
            //
            GridViewHelper.DataGridViewToExcel(this.Text, this.Text, this.dgvList);
            //
            this.PageSize = AppCode.SysEnvironment.DefaultPageSize;
            this.winfrmPager.ChangePage(EventArgs.Empty);
        }
        #endregion

        //Methods
        /// <summary>
        /// 绑定界面控件
        /// </summary>
        protected virtual void BindFormControl()
        {

        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected virtual void BindGridList()
        {
            //自动行列距离调整
            this.dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
        /// <summary>
        /// 設置DataGridView選中行
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        protected virtual void SetGridSelectedRow(string keyName, string keyValue)
        {
            foreach (DataGridViewRow row in this.dgvList.Rows)
            {
                if (row.Cells[keyName].Value != null && row.Cells[keyName].Value.ToString() == keyValue)
                {
                    row.Selected = true;
                    //
                    this.dgvList.CurrentCell = row.Cells[1];
                    //break;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        /// <summary>
        /// 编辑后刷新父窗体方法
        /// </summary>
        public override void RefreshFrm()
        {
            this.BindGridList();
            //
            base.RefreshFrm();
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
                    if (ctrl is ComboBox)
                    {
                        (ctrl as ComboBox).SelectedIndex = 0;
                    }
                    if (ctrl is CheckBox)
                    {
                        (ctrl as CheckBox).Checked = false;
                    }
                }
            }
        }

        #region 分页相关

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
        /// 获取或设置是否显示分页控件。
        /// </summary>
        public bool ShowPager
        {
            get { return this.plPager.Visible; }
            set { this.plPager.Visible = value; }
        }

        /// <summary>
        /// 设置分页控件所显示的总记录数。
        /// </summary>
        public int RecordCount
        {
            set { this.winfrmPager.RecordCount = value; }
            get { return this.winfrmPager.RecordCount; }
        }

        /// <summary>
        /// 设置分页控件每页显示的条数。
        /// </summary>
        public int PageSize
        {
            set { this.winfrmPager.PageSize = value; }
            get { return this.winfrmPager.PageSize; }
        }

        /// <summary>
        /// 翻页时执行的事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winfrmPager_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindGridList();
        }
        #endregion
    }
}