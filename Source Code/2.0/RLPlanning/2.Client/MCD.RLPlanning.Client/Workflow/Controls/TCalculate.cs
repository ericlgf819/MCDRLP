using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Business.SalesCalculate;
using MCD.RLPlanning.Client;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client.SalesCalculate;
using MCD.RLPlanning.Client.Workflow.Task.Controls;

namespace MCD.RLPlanning.Client.Workflow.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TCalculate : TaskListBase
    {
        //存放选中的CompanyCode
        public HashSet<string> m_setCompanyCodes = new HashSet<string>();

        /// <summary>
        /// SalesCalculateBLL
        /// </summary>
        private SalesCalculateBLL m_scBLL = new SalesCalculateBLL();

        /// <summary>
        /// 已经选择的实体数量
        /// </summary>
        private int m_iSelectedCount = 0;

        /// <summary>
        /// 是否全选
        /// </summary>
        private bool m_bAllSelected = true;

        /// <summary>
        /// 最多能选中的实体数量
        /// </summary>
        static readonly int s_iMaxCount = 50;

        public TCalculate()
        {
            InitializeComponent();
        }

        private void TCalculate_Load(object sender, EventArgs e)
        {
            InitGridView();

            //租金计算按钮可见
            btnCalculate.Visible = true;

            //初始化已经选择的条目
            RefreshSelectedCount();
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitGridView()
        {
            this.dvCalculate.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dvCalculate.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dvCalculate.AutoGenerateColumns = false;
            this.dvCalculate.AllowUserToAddRows = false;
            this.dvCalculate.ReadOnly = true;
            this.dvCalculate.AllowUserToOrderColumns = true;
            this.dvCalculate.MultiSelect = false;
            GridViewHelper.PaintRowIndexToHeaderCell(this.dvCalculate);

            //checkbox列头
            GridViewHelper.AppendColumnToDataGridView<DataGridViewCheckBoxColumn>(dvCalculate, "check", "", 30);
            SetGridAllCheckBox set = new SetGridAllCheckBox(dvCalculate, 0);
            set.cb = new CustomCallBack(ColumnCheckBoxCallBack);    //自定义回调
            set.SetSelectAllCheckBox();

            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "CompanyCode", GetMessage("CompanyCode"));
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "CompanyName", GetMessage("CompanyName"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "StoreNo", GetMessage("StoreNo"));
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "EntityType", GetMessage("EntityType"));
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "EntityName", GetMessage("EntityName"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "RentStartDate", GetMessage("RentStartDate"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "RentEndDate", GetMessage("RentEndDate"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "SalesStartDate", GetMessage("SalesStartDate"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "SalesEndDate", GetMessage("SalesEndDate"), 150);
            GridViewHelper.AppendColumnToDataGridView(dvCalculate, "KioskNo", "KioskNo", 0);

            //kioskno不用显示
            dvCalculate.Columns["KioskNo"].Visible = false;
        }

        // datagrid view内容被选中
        private void dvCalculate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //如果是DataGridViewCheckBoxCell类型，则需要设置checkbox的勾选状态
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dvCalculate.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
                {
                    bool bCheckState = false;

                    if (null != dvCalculate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        bCheckState = (bool)dvCalculate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    }

                    dvCalculate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !bCheckState;

                    //更新选中条目的数量
                    if (!bCheckState)
                    {
                        ++m_iSelectedCount;
                    }
                    else
                    {
                        --m_iSelectedCount;
                    }
                    RefreshSelectedCount();
                }
            }
        }

        /// <summary>
        /// 计算租金
        /// </summary>
        public override void CalCulate()
        {
            //判断是否有选中任何店
            if (!IsCheckAnyItem())
            {
                MessageError(GetMessage("MustSelectItem"));
                return;
            }

            //判断是否选择了过多才实体来进行计算
            if (m_iSelectedCount > s_iMaxCount)
            {
                MessageError(string.Format(GetMessage("SelectTooMuch"), s_iMaxCount));
                return;
            }

            //获取计算时间
            CalculateDate calDateFrm = new CalculateDate(m_scBLL);
            calDateFrm.ShowDialog();

            //当用户没有按计算按钮，而是直接关闭了对话框，则后续计算也不用进行下去了
            if (String.IsNullOrEmpty(calDateFrm.strDateTime))
                return;

            //再计算

            //首先将选中的餐厅、甜品店与计算结束日期转换为DataTable
            DataTable dt = ConvertSelectedItem2DT(calDateFrm.strDateTime);
            //没有选中任何餐厅，需要提示，并且返回
            if (0 == dt.Rows.Count)
            {
                MessageError(GetMessage("MustSelectItem"));
                return;
            }   

            //计算开始时间
            DateTime calStartTime = DateTime.Now; 
            if (null != m_scBLL)
            {
                calStartTime = m_scBLL.GetServerTime();
            }

            int iCalRetCode = (int)SalesCalRetCode.EN_CAL_UNKNOWNERR;
            byte[] byteEntitysInCal = null;
            string exceptionErr = null;
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != m_scBLL)
                    {
                        iCalRetCode = m_scBLL.Calculate(SysEnvironment.CurrentUser.ID, dt, out byteEntitysInCal, out exceptionErr);
                    }
                });
            }, base.GetMessage("Wait"), false);
            frm.ShowDialog();

            try
            {
                switch ((SalesCalRetCode)iCalRetCode)
                {
                    case SalesCalRetCode.EN_CAL_SUCCESS:
                        {
                            //显示计算结果
                            if (ParentForm.ParentForm is FrmMain)
                            {
                                //传递必要的参数
                                CalculateResult.s_calStartDate = calStartTime;
                                CalculateResult.s_operatorID = SysEnvironment.CurrentUser.ID;

                                //打开计算结果界面
                                ((FrmMain)ParentForm.ParentForm).CloseForm("CalculateResult");
                                ((FrmMain)ParentForm.ParentForm).OpenForm("CalculateResult");
                            }
                            break;
                        }
                    case SalesCalRetCode.EN_CAL_HASENTITYINCAL:
                        {
                            DataSet ds = m_scBLL.DeSerilize(byteEntitysInCal);
                            CalErrBox errBox = new CalErrBox(ds);
                            errBox.ShowDialog();
                            break;
                        }
                    case SalesCalRetCode.EN_CAL_SERVERBUSY:
                        {
                            MessageInformation(GetMessage("ServerBusy"));
                            break;
                        }
                    case SalesCalRetCode.EN_CAL_TIMEOUT:
                        {
                            MessageError(GetMessage("SqlTimeOut"));
                            break;
                        }
                    default:
                        {
                            MessageError(GetMessage("UnKnownErr"));

                            if (!string.IsNullOrEmpty(exceptionErr))
                            {
                                MessageError(exceptionErr);
                            }
                            
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace);
            }
        }

        /// <summary>
        /// 查询。
        /// </summary>
        public override void BindGridView()
        {
            //如果左侧的公司树没有任何结点选中，则需要提醒用户
            if (0 == m_setCompanyCodes.Count)
            {
                MessageError(GetMessage("CompanyCheckError"));
                return;
            }

            // 重新查询，需要将已选中数量置0
            m_iSelectedCount = 0;
            RefreshSelectedCount();

            //查询
            DataSet ds = null;
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != m_scBLL)
                    {
                        ds = m_scBLL.SelectStoreOrKiosk(tbStoreNo.Text, tbStoreName.Text, tbKioskNo.Text, tbKioskName.Text,
                                            ConvertHastSetToDataTable(m_setCompanyCodes));
                    }
                });
            }, base.GetMessage("Wait"), false);
            frm.ShowDialog();

            //数据绑定
            if (null != ds && ds.Tables.Count != 0)
                dvCalculate.DataSource = ds.Tables[0];
            else
            {
                dvCalculate.DataSource = null;
                MessageInformation(GetMessage("ServerBusy"));
            }
        }

        /// <summary>
        /// 将hashset转成table
        /// </summary>
        /// <param name="hashSet"></param>
        /// <returns></returns>
        private DataTable ConvertHastSetToDataTable(HashSet<string> hashSet)
        {
            DataTable dt = new DataTable();
            dt.TableName = "RLPlanning_Cal_TmpCompanyCodeTbl";
            dt.Columns.Add("CompanyCode");
            foreach (var item in hashSet)
            {
                dt.Rows.Add(item);
            }
            return dt;
        }

        /// <summary>
        /// 将选中的餐厅、甜品店转换为datatable
        /// </summary>
        /// <param name="strCalEndDate">计算结束时间</param>
        /// <returns></returns>
        private DataTable ConvertSelectedItem2DT(string strCalEndDate)
        {
            //清空上一次的残留数据
            CalculateResult.s_hsAllStoreKioskNo.Clear();

            DataTable dt = new DataTable();
            dt.TableName = "RLPlanning_Cal_TmpTbl";
            dt.Columns.Add("StoreNo");
            dt.Columns.Add("KioskNo");
            dt.Columns.Add("CalEndDate");

            string strStoreNo = string.Empty;
            string strKioskNo = string.Empty;
 
            foreach (DataGridViewRow item in dvCalculate.Rows)
            {
                //将选中的记录下来
                if (null != item.Cells["check"].Value && (bool)item.Cells["check"].Value)
                {
                    strStoreNo = item.Cells["StoreNo"].Value.ToString();
                    strKioskNo = item.Cells["KioskNo"].Value.ToString();

                    dt.Rows.Add(strStoreNo, strKioskNo, strCalEndDate);

                    //将选中的要计算的餐厅和甜品店信息暂存下来，供计算结果使用
                    CalculateResult.SStoreKioskPair skpair = new CalculateResult.SStoreKioskPair() 
                    {
                        m_strStoreNo = strStoreNo,
                        m_strKioskNo = strKioskNo,
                        m_strCompanyCode = item.Cells["CompanyCode"].Value.ToString(),
                        m_strCompanyName = item.Cells["CompanyName"].Value.ToString(),
                        m_strEntityName = item.Cells["EntityName"].Value.ToString(),
                        m_strEntityType = item.Cells["EntityType"].Value.ToString()
                    };

                    CalculateResult.s_hsAllStoreKioskNo.Add(skpair);
                }
            }
            return dt;
        }

        /// <summary>
        /// 判断是否有选中的店
        /// </summary>
        /// <returns></returns>
        private bool IsCheckAnyItem()
        {
            foreach (DataGridViewRow item in dvCalculate.Rows)
            {
                //如果有选中的记录，则不用提示
                if (null != item.Cells["check"].Value && (bool)item.Cells["check"].Value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvCalculate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //合并单元格
            GridViewHelper.MerageCell(dgv, e, new List<int>() { 3 });   //storeno
        }

        /// <summary>
        /// 重置。
        /// </summary>
        public override void Reset()
        {
            tbKioskName.Text = string.Empty;
            tbKioskNo.Text = string.Empty;
            tbStoreName.Text = string.Empty;
            tbStoreNo.Text = string.Empty;

            //查询
            BindGridView();
        }

        /// <summary>
        /// 刷新已经选中的条目
        /// </summary>
        private void RefreshSelectedCount()
        {
            lblSelectedCount.Text = string.Format("{0}/{1} (每次最多计算{1}家实体)", m_iSelectedCount, s_iMaxCount);

            //如果选择的条目数量大于最大数量，则需要用红色来提醒用户
            if (m_iSelectedCount > s_iMaxCount)
            {
                lblSelectedCount.ForeColor = Color.Red;
            }
            else
            {
                lblSelectedCount.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// CheckBox列表头点击的回调事件
        /// </summary>
        private void ColumnCheckBoxCallBack()
        {
            if (m_bAllSelected)
            {
                m_iSelectedCount = dvCalculate.Rows.Count;
            }
            else
            {
                m_iSelectedCount = 0;
            }
            RefreshSelectedCount();
            m_bAllSelected = !m_bAllSelected;
        }
    }
}