using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Synchronization;

namespace MCD.RLPlanning.Client.Synchronization
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ScheduleList : BaseList
    {
        //Fields
        private ScheduleBLL ScheduleBLL = new ScheduleBLL();
        #region ctor

        public ScheduleList()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {
                if (e.ColumnIndex == 2)
                {
                    if (e.Value.ToString() == "True")
                    {
                        e.Value = "是 ";
                    }
                    else if (e.Value.ToString() == "False")
                    {
                        e.Value = "否 ";
                    }
                }
                else if (e.ColumnIndex == 4 && e.Value.ToString() == "同步异常")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                }
                else if (e.ColumnIndex == 6)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseFrm.DATETIME_LONG_FORMAT);
                }
            }
        }
        /// <summary>
        /// 双击打开编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.dgvList.CurrentCell != null)
            {
                int ID = Convert.ToInt32(base.dgvList.CurrentRow.Cells[0].Value.ToString());
                //
                ScheduleAdd frm = new ScheduleAdd(ID)
                {
                    ParentFrm = this
                };
                base.RefreshList = false;
                frm.ShowDialog();
                //
                if (base.RefreshList)
                {
                    this.GetDataSource();
                    //
                    base.SetGridSelectedRow("ID", ID.ToString());
                }
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnAddnew_Click(object sender, EventArgs e)
        {
            ScheduleAdd frm = new ScheduleAdd()
            {
                ParentFrm = this
            };
            base.RefreshList = false;
            frm.ShowDialog(this);
            //
            if (base.RefreshList)
            {
                this.GetDataSource();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (base.dgvList.CurrentCell != null)
            {
                if (base.dgvList.CurrentCell.OwningRow.Cells[4].Value + string.Empty != "未启动")
                {
                    base.MessageInformation(base.GetMessage("OnlyDeleteNotStart"));
                    return;
                }
                else if (MessageBox.Show(this, base.GetMessage("ConfirmDelete"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(base.dgvList.CurrentRow.Cells[0].Value.ToString());
                    //
                    this.ScheduleBLL.DeleteSchedule(ID);
                    base.MessageInformation(base.GetMessage("deleteOk"));
                    //
                    this.GetDataSource();
                }
            }
            else
            {
                base.MessageError(base.GetMessage("PleaseSelectItem"));
            }
        }
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ScheduleBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected override void BindFormControl()
        {
            base.btnReset.Visible = false;
            base.btnAddnew.Visible = true;
            base.btnDelete.Visible = true;
        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        protected override void BindGridList()
        {
            base.BindGridList();
            //
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "ID", "ID", 0);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "SycDate", base.GetMessage("SycDate"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "IsCalculate", base.GetMessage("IsCalculate"), 120);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Remark", base.GetMessage("Remark"), 250);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Status", base.GetMessage("Status"), 80);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "AddedBy", base.GetMessage("AddedBy"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "LastModifiedDate", base.GetMessage("LastModifiedDate"), 120);
            base.dgvList.Columns[0].Visible = false;
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            if (!base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet ds = this.ScheduleBLL.SelectAllSchedule();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        base.DTSource = ds.Tables[0];
                    }
                    else
                    {
                        base.DTSource = null;
                    }
                }, base.GetMessage("Wait"), () =>
                {
                    this.ScheduleBLL.CloseService();
                });
                frmwait.ShowDialog();
            }, "获取同步计划数据错误", "同步计划信息"))
            {
                return;
            }
            // 设置主键
            base.DTSource.PrimaryKey = new DataColumn[] { base.DTSource.Columns["ID"] };
            base.GetDataSource();
            //
            if (base.DTSource != null && base.DTSource.Rows.Count > 0) 
            {
                base.SetGridSelectedRow("ID", base.DTSource.Rows[0]["ID"].ToString());
            }
        }
    }
}