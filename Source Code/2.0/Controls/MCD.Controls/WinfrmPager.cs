using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.Common
{
    /// <summary>
    /// Winform分页用户控件。
    /// </summary>
    public partial class WinfrmPager : UserControl
    {
        //Fields
        /// <summary>
        /// 当页索引改变后执行的事件。
        /// </summary>
        public event EventHandler PageIndexChanged;

        //Properties
        int recordCount = 0;
        /// <summary>
        /// 获取或设置分页数据的总记录数。
        /// </summary>
        public int RecordCount
        {
            get { return this.recordCount; }
            set
            {
                if (value != this.recordCount)
                {
                    this.recordCount = value;
                    //
                    this.PageIndex = 0;
                    this.ChangePage(EventArgs.Empty);
                }
            }
        }

        int pageSize = 50;
        /// <summary>
        /// 获取或设置每页显示的条数。
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }

        int pageIndex = 0;
        /// <summary>
        /// 获取或设置当前显示页的索引，首页为0。
        /// </summary>
        public int PageIndex
        {
            get { return this.pageIndex; }
            set { this.pageIndex = value; }
        }

        /// <summary>
        /// 获取总页数。
        /// </summary>
        public int PageCount
        {
            get
            {
                if (this.RecordCount % this.PageSize == 0)
                {
                    return this.RecordCount / this.PageSize;
                }
                return this.RecordCount / this.PageSize + 1;
            }
        }
        #region ctor

        /// <summary>
        /// 初始化WinfrmPager类的新实例。
        /// </summary>
        public WinfrmPager()
        {
            InitializeComponent();
            //默认每页50条
            this.PageSize = 50;
            this.PageIndex = 0;
        }
        #endregion

        //Events
        private void WinfrmPager_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.btnFirst.Enabled = this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = this.btnLast.Enabled = false;
                this.lblRecordCount.Text = this.lblPageCount.Text = string.Empty;
            }
        }
        /// <summary>
        /// 触发PageIndexChanged事件。
        /// </summary>
        /// <param name="e"></param>
        protected void OnPageIndexChanged(EventArgs e)
        {
            if (this.PageIndexChanged != null)
            {
                this.PageIndexChanged(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 输入页码后回车可切换到当前页。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            //
            int index = 0;
            if (int.TryParse(this.tbCurrentPage.Text, out index))
            {
                this.PageIndex = index - 1;
                this.ChangePage(EventArgs.Empty);
            }
        }
        /// <summary>
        /// 翻页按钮事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            switch (button.Name)
            {
                case "btnFirst":
                    this.PageIndex = 0;
                    break;
                case "btnPrevious":
                    this.PageIndex--;
                    break;
                case "btnNext":
                    this.PageIndex++;
                    break;
                case "btnLast":
                    this.PageIndex = this.PageCount - 1;
                    break;
                default: break;
            }
            this.ChangePage(EventArgs.Empty);
        }

        //Methods
        /// <summary>
        /// 翻页。
        /// </summary>
        public void ChangePage(EventArgs e)
        {
            //检查页面是否超出范围
            if (this.PageIndex < 0)
            {
                this.PageIndex = 0;
            }
            if (this.PageIndex > this.PageCount - 1)
            {
                this.PageIndex = this.PageCount - 1;
            }
            //改变翻页按钮的状态
            if (this.PageCount > 1)
            {
                if (this.PageIndex == 0)
                {
                    this.btnFirst.Enabled = this.btnPrevious.Enabled = false;
                    this.btnNext.Enabled = this.btnLast.Enabled = true;
                }
                else if (this.PageIndex < this.PageCount - 1)
                {
                    this.btnFirst.Enabled = this.btnPrevious.Enabled = true;
                    this.btnNext.Enabled = this.btnLast.Enabled = true;
                }
                else
                {
                    this.btnFirst.Enabled = this.btnPrevious.Enabled = true;
                    this.btnNext.Enabled = this.btnLast.Enabled = false;
                }
            }
            else
            {
                this.btnFirst.Enabled = this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = this.btnLast.Enabled = false;
            }
            if (this.PageCount >= 0)
            {
                this.lblPageCount.Text = string.Format("of {0}", this.PageCount);
            }
            this.tbCurrentPage.Text = (this.PageIndex + 1).ToString();
            this.lblRecordCount.Text = string.Format("Total:{0},  Per page:{1}", this.RecordCount, this.PageSize);
            //执行翻页事件
            this.OnPageIndexChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 手动设置当前页码
        /// </summary>
        /// <param name="iCurIndex"></param>
        public void SetCurrentIndexText(int iCurIndex)
        {
            this.tbCurrentPage.Text = (iCurIndex + 1).ToString();
        }
    }
}