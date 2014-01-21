using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.Client;
using MCD.Common;
using MCD.RLPlanning.Business.ForecastSales;

namespace MCD.RLPlanning.Client.ForcastSales
{
    public partial class KeyInBox : BaseEdit
    {
        private string StoreNo = null;
        private string KioskNo = null;
        private string StoreName = null;
        private string CompanyCode = null;
        private string KioskName = null;

        private SalesBLL salesBLL = null;

        //真实数据的最大年份和月份
        private int iRealSalesMaxYear = 0;
        private int iRealSalesMaxMonth = 0;

        //开店年月
        private int iRentStartYear = 0;
        private int iRentStartMonth = 0;

        /// <summary>
        /// Sales数据的列信息
        /// </summary>
        private const int c_iSalesBeginColumnIndex = 5;
        private const int c_iSalesColumnSum = 12;

        /// <summary>
        /// 公司编号和KioskName列号
        /// </summary>
        private const int c_iStoreNoColumnIndex = 1;
        private const int c_iKioskName = 3;

        /// <summary>
        /// 编辑单元格之前，该单元格内的值
        /// </summary>
        private string ori_cell_value = null;

        /// <summary>
        /// 存储被编辑过的行、列号
        /// </summary>
        private HashSet<KeyValuePair<int, int>> modifiedRowColIndexSet = new HashSet<KeyValuePair<int, int>>();
        /// <summary>
        /// 存储被修改过的行号
        /// </summary>
        private HashSet<int> modifiedRowIndexSet = new HashSet<int>();

        public KeyInBox(string inputStoreNo, string inputKioskNo)
        {
            StoreNo = inputStoreNo;

            if (!String.IsNullOrEmpty(inputKioskNo))
                KioskNo = inputKioskNo;

            InitializeComponent();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {    
            //如果没有任何数据，则保持等于退出
            if (null == dgvSales.DataSource)
            {
                Close();
                return;
            }

            //在新增之前先判断上一行的sales数据是否完全填写完整
            string err_Msg = string.Empty;
            if (!Validate_LastLine(out err_Msg))
            {
                MessageError(err_Msg);
                return;
            }

            StringBuilder errMsg = null;

            ////保存时间可能较长，需要提醒用户
            //DialogResult res =
            //    MessageBox.Show(GetMessage("TipContent"), GetMessage("TipContentTitle"), MessageBoxButtons.YesNo);
            //if (DialogResult.No == res)
            //{
            //    return;
            //}

            //将被修改过的数据剥离出来--Begin
            DataTable sourceTbl = (DataTable)dgvSales.DataSource;
            DataTable importSalesTbl = sourceTbl.Clone();
            foreach (var index in modifiedRowIndexSet)
            {
                DataRow newRow = importSalesTbl.NewRow();
                //赋值餐厅编号和kiosk名称
                newRow[c_iStoreNoColumnIndex] = StoreNo;
                newRow[c_iKioskName] = KioskName;
                //赋值年份信息
                newRow[c_iSalesBeginColumnIndex - 1] = sourceTbl.Rows[index][c_iSalesBeginColumnIndex - 1];

                //取出将该行修改过的列，并赋值
                foreach (var item in modifiedRowColIndexSet)
                {
                    if (item.Key == index)
                    {
                        //用逗号隔开sales数据，会导致sqlserver格式转换错误，所以需要转回来
                        float fTmp;
                        try
                        {
                            fTmp = float.Parse(sourceTbl.Rows[index][item.Value].ToString());
                            newRow[item.Value] = fTmp;
                        }
                        catch
                        {
                            newRow[item.Value] = sourceTbl.Rows[index][item.Value];
                        }
                    }
                }

                importSalesTbl.Rows.InsertAt(newRow, 0);
            }
            //将被修改过的数据剥离出来--End
            List<string> busyStores = null, busyKiosks = null;
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    salesBLL.UpdateSales(importSalesTbl, out errMsg, out busyStores, out busyKiosks);
                },
                GetMessage("UpdateSalesError"), GetMessage("UpdateSales"));
            }, base.GetMessage("Wait"), false);
            frm.ShowDialog();

            //显示错误信息
            //  1. 计算冲突错误
            if (null != busyStores && null != busyKiosks)
            {
                List<string> errList = new List<string>();
                //餐厅
                foreach (var item in busyStores)
                {
                    errList.Add(string.Format(GetMessage("StoreInImport"), item));
                }
                //甜品店
                foreach (var item in busyKiosks)
                {
                    errList.Add(string.Format(GetMessage("KioskInImport"), item));
                }

                ImportErrBox msgBox = new ImportErrBox(errList);
                msgBox.ShowDialog();
            }
            //  2. 非计算冲突错误
            else if (null != errMsg && errMsg.Length != 0)
            {
                ImportErrBox msgBox = new ImportErrBox(errMsg.ToString());
                msgBox.ShowDialog();
            }
            // 成功
            else
            {
                MessageInformation(GetMessage("SaveSuccess"));
                Close();
            }
        }

        /// <summary>
        /// 关闭回调事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyInBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != salesBLL)
            {
                salesBLL.Dispose();
            }
        }

        /// <summary>
        /// 初始化datagridview控件
        /// </summary>
        private void InitGridView()
        {
            //grid view不可删除，不可改变顺序，不可增加
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.AllowUserToOrderColumns = false;
            dgvSales.AllowUserToAddRows = false;

            GridViewHelper.AppendColumnToDataGridView(dgvSales, "Company", GetMessage("Company"), 70);
            GridViewHelper.AppendColumnToDataGridView(dgvSales, "餐厅编号", GetMessage("StoreNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvSales, "Store", GetMessage("StoreName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvSales, "Kiosk", GetMessage("KioskName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvSales, "年度", GetMessage("Year"), 60);
            for (int index = 1; index <= 12; ++index)
            {
                GridViewHelper.AppendColumnToDataGridView(dgvSales, string.Format("{0}月", index), string.Format("{0}", index), 70);
            }

            //除了年份和sales之外，其它列不可编辑
            dgvSales.Columns["Company"].ReadOnly = true;
            dgvSales.Columns["餐厅编号"].ReadOnly = true;
            dgvSales.Columns["Store"].ReadOnly = true;
            dgvSales.Columns["Kiosk"].ReadOnly = true;

            //列不可排序，否则会让禁止编辑的功能失效
            foreach (DataGridViewColumn item in dgvSales.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //新行增加
        private void dgvSales_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AutoFillColumnContent(e.RowIndex, e.RowCount);
        }

        private void KeyInBox_Load(object sender, EventArgs e)
        {
            salesBLL = new SalesBLL();

            //绑定GridView列名
            InitGridView();

            //载入sales数据
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    LoadSalesData();
                },
                GetMessage("SearchSalesError"), GetMessage("SearchSales"));
            }, GetMessage("Wait"), false);
            frm.ShowDialog();

            //lable信息的绑定
            lblStoreName.Text = StoreName;
        }

        /// <summary>
        /// 载入预测Sales数据
        /// </summary>
        /// <returns></returns>
        private void LoadSalesData()
        {
            DataSet ds = salesBLL.SelectSales(StoreNo, KioskNo);

            //只读数据的读取
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                CompanyCode = ds.Tables[1].Rows[0]["CompanyCode"] + string.Empty;
                StoreName = ds.Tables[1].Rows[0]["StoreName"] + string.Empty;
                KioskName = ds.Tables[1].Rows[0]["KioskName"] + string.Empty;
            }

            //读取真实Sales数据的最大年份和月份
            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                try
                {
                    iRealSalesMaxYear = (int)ds.Tables[2].Rows[0]["Year"];
                    iRealSalesMaxMonth = (int)ds.Tables[2].Rows[0]["Month"];
                }
                catch
                {
                    iRealSalesMaxMonth = iRealSalesMaxMonth = 0;
                }
            }

            //读取开店年月
            if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                try
                {
                    iRentStartYear = (int)ds.Tables[3].Rows[0]["Year"];
                    iRentStartMonth = (int)ds.Tables[3].Rows[0]["Month"];
                }
                catch
                {
                    iRentStartYear = iRentStartMonth = 0;
                }
            }

            //将空值填0
            FillZero2NullCell(ds.Tables[0]);

            //绑定Data grid view 数据源
            dgvSales.DataSource = ds.Tables[0];

            //将真实数据设置为只读
            MakeRealSalesCellReadOnly();

            //将0值格式化
            FormatZeroInDGV();

            //将同一年中开店前的几个月sales值自动填0，并且设置readonly
            for (int i = 0; i < dgvSales.Rows.Count; ++i)
            {
                AutoFillSalesBeforeRentStartDate(i);
            }

            //将sales值用逗号3位隔开
            FormatSalesCell();
        }

        /// <summary>
        /// Sales数值格式用逗号3位隔开
        /// </summary>
        private void FormatSalesCell()
        {
            string strSales = string.Empty;
            float fSales = 0.0f;

            foreach (DataGridViewRow item in dgvSales.Rows)
            {
                for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                {
                    if (null != item.Cells[i].Value)
                    {
                        strSales = item.Cells[i].Value.ToString();
                    }

                    if (!string.IsNullOrEmpty(strSales))
                    {
                        try
                        {
                            fSales = float.Parse(strSales);
                            item.Cells[i].Value = string.Format("{0:N2}", fSales);
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定datagridview
        /// </summary>
        private void BindGridView(DataTable dt)
        {
            if (null != dt)
            {
                dgvSales.DataSource = dt;
                return;
            }

            //没有数据则需要客户端来新建DataSource
            dt = new DataTable();
            dt.Columns.Add("Company");
            dt.Columns.Add("餐厅编号");
            dt.Columns.Add("Store");
            dt.Columns.Add("Kiosk");
            dt.Columns.Add("年度");
            for (int i = 1; i <= 12; ++i)
            {
                dt.Columns.Add(String.Format("{0}月", i));
            }

            dgvSales.DataSource = dt;
        }

        /// <summary>
        /// 将指定行的空sales值填0
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="iRowIndex">-1表面全部行都要将空sales填0值</param>
        /// <returns></returns>
        private bool FillZero2NullCell(DataTable dt, int iRowIndex = -1)
        {
            if (dt.Columns.Count < c_iSalesBeginColumnIndex + c_iSalesColumnSum)
                return false;
            if (iRowIndex < -1)
                return false;

            string strYear;
            if (-1 == iRowIndex)
            {
                for (int iRow = 0; iRow < dt.Rows.Count; ++iRow)
                {
                    for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                    {
                        if (string.Empty == dt.Rows[iRow][i].ToString())
                        {
                            dt.Rows[iRow][i] = 0.0f;

                            //如果添加的值处于最大sales年份，则表明该0.0f值相当于新添加的值，需要记录行列号
                            strYear = dt.Rows[iRow][c_iSalesBeginColumnIndex - 1].ToString();
                            try
                            {
                                if (iRealSalesMaxYear <= int.Parse(strYear))
                                {
                                    modifiedRowIndexSet.Add(iRow);
                                    modifiedRowColIndexSet.Add(new KeyValuePair<int, int>(iRow, i));
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                {
                    if (string.Empty == dt.Rows[iRowIndex][i].ToString())
                    {
                        dt.Rows[iRowIndex][i] = 0.0f;

                        //如果添加的值处于最大sales年份，则表明该0.0f值相当于新添加的值，需要记录行列号
                        strYear = dt.Rows[iRowIndex][c_iSalesBeginColumnIndex - 1].ToString();
                        try
                        {
                            if (iRealSalesMaxYear <= int.Parse(strYear))
                            {
                                modifiedRowIndexSet.Add(iRowIndex);
                                modifiedRowColIndexSet.Add(new KeyValuePair<int, int>(iRowIndex, i));
                            }
                        }
                        catch
                        {
                            //这种情况说明是没有sales数据导致的
                            if (String.IsNullOrEmpty(strYear))
                            {
                                modifiedRowIndexSet.Add(iRowIndex);
                                modifiedRowColIndexSet.Add(new KeyValuePair<int, int>(iRowIndex, i));
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 格式化datagridview中的0值
        /// </summary>
        /// <param name="iRowIndex">-1表面所有行都要format</param>
        /// <returns></returns>
        private bool FormatZeroInDGV(int iRowIndex = -1)
        {
            if (iRowIndex < -1)
                return false;
            if (0 == dgvSales.Rows.Count)
                return false;
            if (dgvSales.Rows.Count < iRowIndex + 1)
                return false;

            string strfval;
            float fval;
            if (-1 == iRowIndex)
            {
                foreach (DataGridViewRow item in dgvSales.Rows)
                {
                    for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                    {

                        try
                        {
                            strfval = null == item.Cells[i].Value ?
                                                string.Empty : item.Cells[i].Value.ToString();
                            fval = float.Parse(strfval);

                            if (0.0f == fval)
                                item.Cells[i].Value = string.Format("{0:N2}", fval);
                        }
                        catch
                        {
                            item.Cells[i].Value = string.Format("{0:N2}", 0.0f);
                        }
                    }
                }
            }
            else
            {
                for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                {

                    try
                    {
                        strfval = null == dgvSales.Rows[iRowIndex].Cells[i].Value ? 
                                            string.Empty : dgvSales.Rows[iRowIndex].Cells[i].Value.ToString();
                        fval = float.Parse(strfval);

                        if (0.0f == fval)
                            dgvSales.Rows[iRowIndex].Cells[i].Value = string.Format("{0:N2}", fval);
                    }
                    catch
                    {
                        dgvSales.Rows[iRowIndex].Cells[i].Value = string.Format("{0:N2}", 0.0f);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 将真实sales数据设置为只读
        /// </summary>
        private void MakeRealSalesCellReadOnly()
        {
            int iYear = 0;
            foreach (DataGridViewRow item in dgvSales.Rows)
            {
                //年
                try
                {
                    iYear = int.Parse(item.Cells[c_iSalesBeginColumnIndex - 1].Value.ToString());
                }
                catch
                {
                    iYear = 0;
                }

                if (iYear <= iRealSalesMaxYear && 0 != iYear)
                    item.Cells[c_iSalesBeginColumnIndex - 1].ReadOnly = true;

                //Sales
                for (int index = c_iSalesBeginColumnIndex; index < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++index)
                {
                    int iMonth = index - 4;

                    //真实数据与预测数据交叉的一个月
                    if (iYear == iRealSalesMaxYear && 0 != iYear &&
                        iMonth <= iRealSalesMaxMonth)
                    {
                        item.Cells[index].ReadOnly = true;
                        item.Cells[index].Style.BackColor = Color.LightGray;
                    }

                    //之前的真实数据
                    if (iYear < iRealSalesMaxYear && 0 != iYear)
                    {
                        item.Cells[index].ReadOnly = true;
                        item.Cells[index].Style.BackColor = Color.LightGray;
                    }
                }
            }
        }

        /// <summary>
        /// 如果某家店不是在年头开的，则需要将开店前的1到N个月的sales数据填0，并且为ReadOnly
        /// </summary>
        /// <param name="iRowIndex">需要检查的dgv的index</param>
        private void AutoFillSalesBeforeRentStartDate(int iRowIndex)
        {
            int iYear = 0;
            try
            {
                iYear = int.Parse(dgvSales.Rows[iRowIndex].Cells[c_iSalesBeginColumnIndex - 1].Value.ToString());
            }
            catch
            {
                iYear = 0;
            }

            if (iYear == iRentStartYear)
            {
                int ipivot = c_iSalesBeginColumnIndex - 1;
                for (int i = 1; i < iRentStartMonth; ++i)
                {
                    dgvSales.Rows[iRowIndex].Cells[ipivot + i].Value = string.Format("{0:N2}", 0.0f);
                    dgvSales.Rows[iRowIndex].Cells[ipivot + i].ReadOnly = true;
                    dgvSales.Rows[iRowIndex].Cells[ipivot + i].Style.BackColor = Color.LightGray;

                    //如果这些Cell的位置信息已经记录在modifiedRowColIndexSet中，
                    //则需要将这些信息删除，因为开店前的0数据是不用存入数据库的
                    modifiedRowColIndexSet.Remove(new KeyValuePair<int, int>(iRowIndex, ipivot + i));
                }
            }
        }

        /// <summary>
        /// 自动填充不可编辑的列数据
        /// </summary>
        private void AutoFillColumnContent(int iFirstNewRowIndex = 0, int iRowCount = 1)
        {
            for (int i = iFirstNewRowIndex; i < iFirstNewRowIndex + iRowCount; ++i)
            {
                dgvSales.Rows[i].Cells["Company"].Value = CompanyCode;
                dgvSales.Rows[i].Cells["餐厅编号"].Value = StoreNo;
                dgvSales.Rows[i].Cells["Store"].Value = StoreName;
                dgvSales.Rows[i].Cells["Kiosk"].Value = KioskName;
            }
        }

        /// <summary>
        /// 新增行年份自动加一
        /// </summary>
        /// <param name="iFirstNewRowIndex"></param>
        /// <param name="iRowCount"></param>
        private void AutoIncreaseYear()
        {
            DataTable dt = (DataTable)dgvSales.DataSource;
            int iLastIndex = dt.Rows.Count - 1;

            //新增的第一行不用做年份加一的操作而是取当开店年或者当前年
            if (0 == iLastIndex)
            {
                if (0 == iRentStartYear)
                    dt.Rows[iLastIndex]["年度"] = DateTime.Now.Year;
                else
                    dt.Rows[iLastIndex]["年度"] = iRentStartYear;
                return;
            }

            int iYear;
            try
            {
                iYear = Int32.Parse(dt.Rows[iLastIndex - 1]["年度"].ToString());
                iYear += 1;
                dt.Rows[iLastIndex]["年度"] = iYear;
            }
            catch
            {
                return;
            }
        }

        private void btnAddSales_Click(object sender, EventArgs e)
        {
            //在新增之前先判断上一行的sales数据是否完全填写完整
            string errMsg = string.Empty;
            if (!Validate_LastLine(out errMsg))
            {
                MessageError(errMsg);
                return;
            }

            DataTable tb = (DataTable)dgvSales.DataSource;
            tb.Rows.Add();
            //记录编辑过的行号
            modifiedRowIndexSet.Add(tb.Rows.Count - 1);
            //年份自增
            AutoIncreaseYear();
            //新增的行要将空值填0，下面两句一定要在上面年份自增完成后调用！！
            FillZero2NullCell(tb, tb.Rows.Count - 1);
            FormatZeroInDGV(tb.Rows.Count - 1);
            //新增的行如果年份是开店日期年，则需要将开店前几个月的数据自动填0并且只读
            AutoFillSalesBeforeRentStartDate(tb.Rows.Count - 1);
        }

        private void dgvSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string errMsg = string.Empty;
            if (Validate_Input(e.RowIndex, e.ColumnIndex, out errMsg))
            {
                //记录编辑过的行号
                modifiedRowIndexSet.Add(e.RowIndex);
                //记录编辑过的行列号
                modifiedRowColIndexSet.Add(new KeyValuePair<int, int>(e.RowIndex, e.ColumnIndex));
            }
            else
            {
                dgvSales.CurrentCell = dgvSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvSales.CurrentCell.Value = ori_cell_value;
                MessageError(errMsg);
                return;
            }

            //如果编辑的单元格是年份，则需要判断该年份是否处于刚开店的年份，还是在开店年份之前
            if (e.ColumnIndex == (c_iSalesBeginColumnIndex - 1))
            {
                YearInput_PostProcess(e.RowIndex);
            }
        }

        /// <summary>
        /// 年份输入后的处理，如果输入的年大于开业年，则需要将该行sales的只读属性去掉。
        /// 如果等于开业年，则需要在开业月份前加上只读属性
        /// </summary>
        /// <param name="iRow"></param>
        private void YearInput_PostProcess(int iRowIndex)
        {
            int iYear;
            try
            {
                iYear = int.Parse(dgvSales.Rows[iRowIndex].Cells[c_iSalesBeginColumnIndex - 1].Value.ToString());

                //如果新增的年大于开店年份，则该行所有sales数据列可编辑
                if (iYear > iRentStartYear)
                {
                    for (int i = c_iSalesBeginColumnIndex; i < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++i)
                    {
                        dgvSales.Rows[iRowIndex].Cells[i].ReadOnly = false;
                        dgvSales.Rows[iRowIndex].Cells[i].Style.BackColor = Color.White;
                    }
                }

                //如果新增的年等于开店年份，需要将该年中小于开店月的sales数据设置为0，并且设置单元格为只读
                if (iYear == iRentStartYear)
                {
                    AutoFillSalesBeforeRentStartDate(iRowIndex);
                }
            }
            catch
            {
                //直接返回就行了
                return;
            }
        }

        private bool Validate_Input(int iRow, int iColumn, out string errMsg)
        {
            errMsg = string.Empty;
            string column_name = dgvSales.Columns[iColumn].Name;
            switch (column_name)
            {
                case "年度":
                    {
                        if (Validate_YearCell(iRow, iColumn, out errMsg))
                        {
                            //自动填充开店前的sales
                            AutoFillSalesBeforeRentStartDate(iRow);
                            return true;
                        }
                        break;
                    }
                //其它都是月sales数据
                default:
                    {
                        if (Validate_SalesCell(iRow, iColumn, out errMsg))
                        {
                            return true;
                        }
                        break;
                    }
            }

            return false;
        }

        #region 错误提示
        private const string c_length_err = "Sales长度不能超过8位";
        private const string c_type_err = "输入的值不能是负数或者是非数字字符";
        private const string c_year_err = "该年份已经存在";
        private const string c_lessyear_err = "年份不能小于开业年";
        private const string c_notfilledall_err = "每个月的Sales信息需要全部填写";
        #endregion

        /// <summary>
        /// 验证最后一行数据是否完全填写完整
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool Validate_LastLine(out string errMsg)
        {
            errMsg = string.Empty;

            int iLastRowIndex = dgvSales.Rows.Count - 1;

            //没任何行，则不用验证
            if (iLastRowIndex < 0)
                return true;

            //c_iSalesBeginColumnIndex是1月sales的column index，现在需要校验是否年有输入，
            //所以从c_iSalesBeginColumnIndex-1开始判断
            string value = string.Empty;
            for (int index = c_iSalesBeginColumnIndex - 1; index < c_iSalesBeginColumnIndex + c_iSalesColumnSum; ++index)
            {
                if (String.IsNullOrEmpty(dgvSales.Rows[iLastRowIndex].Cells[index].Value.ToString()))
                {
                    errMsg = c_notfilledall_err;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证输入的年份是否合法
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool Validate_YearCell(int iRow, int iColumn, out string errMsg)
        {
            errMsg = string.Empty;
            if (!Validate_BasicInput(iRow, iColumn, out errMsg))
            {
                return false;
            }

            //判断是否有重复的年
            string szyear = dgvSales.Rows[iRow].Cells[iColumn].Value.ToString();
            for (int i = 0; i < dgvSales.Rows.Count; ++i)
            {
                //扫描到当前行跳过
                if (i == iRow)
                {
                    continue;
                }

                if (dgvSales.Rows[i].Cells[iColumn].Value.ToString() == szyear)
                {
                    errMsg = c_year_err;
                    return false;
                }
            }

            //判断年是否小于开业年
            try
            {
                int iYear = int.Parse(szyear);
                if (iYear < iRentStartYear)
                {
                    errMsg = c_lessyear_err;
                    return false;
                }
            }
            catch
            {
                //莫名的错误
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证Sales值
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool Validate_SalesCell(int iRow, int iColumn, out string errMsg)
        {
            errMsg = string.Empty;

            if (Validate_BasicInput(iRow, iColumn, out errMsg))
            {
                //format the value
                string szvalue = dgvSales.Rows[iRow].Cells[iColumn].Value.ToString();
                float fvalue = Single.Parse(szvalue);
                dgvSales.Rows[iRow].Cells[iColumn].Value = string.Format("{0:N2}", fvalue);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证基本输入的合法性
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool Validate_BasicInput(int iRow, int iColumn, out string errMsg)
        {
            errMsg = string.Empty;
            string cell_value = dgvSales.Rows[iRow].Cells[iColumn].Value.ToString();
            int ivalue = 0;
            float fvalue = 0.0f;
            bool bisint = false;
            bool bisdecimal = false;

            //验证是否是正数
            try
            {
                ivalue = Int32.Parse(cell_value);
                bisint = true;
            }
            catch
            {
                bisint = false;
            }

            //验证是否是小数
            try
            {
                fvalue = Single.Parse(cell_value);
                bisdecimal = true;
            }
            catch
            {
                bisdecimal = false;
            }

            //验证是否大于0以及是否是非数字
            if ((bisint && ivalue < 0) || (bisdecimal && fvalue < 0.0f) || (!bisint && !bisdecimal))
            {
                errMsg = c_type_err;
                return false;
            }

            //验证非小数部分长度是否大于8位
            if (bisint)
            {
                if (ivalue.ToString().Length > 8)
                {
                    errMsg = c_length_err;
                    return false;
                }
            }

            if (bisdecimal)
            {
                if (fvalue / 10000000.0f >= 10.0f)
                {
                    errMsg = c_length_err;
                    return false;
                }
            }

            return true;
        }

        private void dgvSales_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //记录之前的值
            ori_cell_value = dgvSales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }
    }
}