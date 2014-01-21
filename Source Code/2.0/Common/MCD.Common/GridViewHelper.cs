using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Drawing.Printing;

namespace MCD.Common
{
    /// <summary>
    /// 关于UI控制的辅助工具类
    /// </summary>
    public class GridViewHelper
    {
        #region GridView 相关方法


        #region 合并单元格,按照行进行统计
        /// <summary>
        /// 合并单元格 StoreNo CompanyName
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="e">绘制参数</param>
        /// <param name="columnIndexList">列序号</param>
        public static void MerageCellRow(DataGridView dgv, DataGridViewCellPaintingEventArgs e, List<int> columnIndexList)
        {
            if (e.RowIndex >= 0)
            {
                string StoreNo = ((DataTable)dgv.DataSource).Rows[e.RowIndex]["StoreNo"] + string.Empty;
                if (e.ColumnIndex < 4 && StoreNo == string.Empty)
                {
                    string CompanyName = ((DataTable)dgv.DataSource).Rows[e.RowIndex]["CompanyName"] + string.Empty;
                    //
                    using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                            if (e.ColumnIndex == 0)
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left - 1,
                                e.CellBounds.Top, e.CellBounds.Left - 1,
                                e.CellBounds.Bottom);
                            }

                            if (e.ColumnIndex == 2)
                            {
                                if (CompanyName == string.Empty)
                                {
                                    e.Graphics.DrawString("合计", e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                }
                                else
                                {
                                    e.Graphics.DrawString("公司合计", e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                }
                            }
                            if (e.ColumnIndex == 3)
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom);
                            }
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        #endregion  ------------

        #region 合并单元格
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="e">绘制参数</param>
        /// <param name="columnIndexList">列序号</param>
        public static void MerageCell(DataGridView dgv, DataGridViewCellPaintingEventArgs e, List<int> columnIndexList)
        {
            if (columnIndexList.Contains(e.ColumnIndex) && e.RowIndex != -1)
            {
                using
                (
                    Brush gridBrush = new SolidBrush(dgv.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                )
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.RowIndex < dgv.Rows.Count - 1 &&
                        dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() !=
                        e.Value.ToString())
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                        e.CellBounds.Top, e.CellBounds.Right - 1,
                        e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            if (e.RowIndex > 0 &&
                            dgv.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() ==
                            e.Value.ToString())
                            { }
                            else
                            {
                                e.Graphics.DrawString(Convert.ToString(e.Value), e.CellStyle.Font,
                                Brushes.Black, e.CellBounds.X + 2,
                                e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                        }
                        e.Handled = true;
                    }
                }
            }
        }



        #endregion  ------------

        #region GridView 添加列
        /// <summary>
        /// /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="actionInitial">初始化</param>
        /// <param name="percent">是否百分比填充</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width
            , DataGridViewCellStyle style
            , Action<T> actionInitial
            )
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, width, style, actionInitial, false);
        }
        /// <summary>
        /// /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="actionInitial">初始化</param>
        /// <param name="percent">是否百分比填充</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width
            , DataGridViewCellStyle style
            , Action<T> actionInitial
            , bool percent)
            where T : DataGridViewColumn, new()
        {
            // 初始化列对象
            T t = new T()
            {
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                HeaderText = headerText
            };
            // 设置宽度
            if (width.HasValue)
            {
                if (percent)
                {
                    t.Width = (dgv.Width - 43) * width.Value / 100;
                }
                else
                {
                    t.Width = width.Value;
                }
            }
            // 格式化
            if (actionInitial != null)
            {
                actionInitial(t);
            }
            // 添加样式
            if (style != null)
            {
                t.DefaultCellStyle = style;
            }
            // 将列对象添加到 Grid 中
            dgv.Columns.Add(t);

            return t;
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText)
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, null, null, null, false);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="actionInitial">初始化方法</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , Action<T> actionInitial)
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, null, null, actionInitial, false);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">列宽</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int width)
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, width, null, null, false);
        }

        /// <summary>
        /// /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">列宽</param>
        /// <param name="percent">是否百分比填充</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int width
            , bool percent)
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, width, null, null, percent);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="style">显示样式</param>
        /// <returns></returns>
        public static T AppendColumnToDataGridView<T>(DataGridView dgv
            , string dataPropertyName
            , string headerText, DataGridViewCellStyle style)
            where T : DataGridViewColumn, new()
        {
            return AppendColumnToDataGridView<T>(dgv, dataPropertyName, headerText, null, style, null, false);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AppendColumnToDataGridView(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width)
        {
            return AppendColumnToDataGridView<DataGridViewTextBoxColumn>(dgv, dataPropertyName, headerText, width, null, null, false);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="percent">是否百分比填充</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AppendColumnToDataGridView(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width
            , bool percent)
        {
            return AppendColumnToDataGridView<DataGridViewTextBoxColumn>(dgv, dataPropertyName, headerText, width, null, null, percent);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="actionInitial">初始化</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AppendColumnToDataGridView(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width
            , Action<DataGridViewTextBoxColumn> actionInitial)
        {
            return AppendColumnToDataGridView<DataGridViewTextBoxColumn>(dgv, dataPropertyName, headerText, width, null, actionInitial, false);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="actionInitial">初始化</param>
        /// <param name="percent">是否百分比填充，默认为 false</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AppendColumnToDataGridView(DataGridView dgv
            , string dataPropertyName
            , string headerText
            , int? width
            , Action<DataGridViewTextBoxColumn> actionInitial
            , bool percent)
        {
            return AppendColumnToDataGridView<DataGridViewTextBoxColumn>(dgv, dataPropertyName, headerText, width, null, actionInitial, percent);
        }

        /// <summary>
        /// 为GridView添加列
        /// </summary>
        /// <typeparam name="T">GridView列的类型</typeparam>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="dataPropertyName">对应的栏位名称</param>
        /// <param name="headerText">标题</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AppendColumnToDataGridView(DataGridView dgv
            , string dataPropertyName
            , string headerText)
        {
            return AppendColumnToDataGridView(dgv, dataPropertyName, headerText, null, null);
        }
        #endregion

        #region GridView 选中行
        /// <summary>
        /// 选中DataGridView的某一行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="rowIndex"></param>
        public static void SelectRowIndex(DataGridView dgv, int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count)
            {
                dgv.ClearSelection();
                dgv.Rows[rowIndex].Selected = true;
                dgv.CurrentCell = dgv.Rows[rowIndex].Cells[0];
            }
        }
        #endregion

        #region GridView 设置标题
        /// <summary>
        /// 修改Grid的标题
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="headerTexts"></param>
        public static void ModifyDataGridViewHeaderText(DataGridView dgv, string[] headerTexts)
        {
            for (int i = 0; i < dgv.ColumnCount || i < headerTexts.Length; i++)
            {
                dgv.Columns[i].HeaderText = headerTexts[i];
            }
        }
        #endregion

        #region GridView 禁用排序
        /// <summary>
        /// 禁用DataGridView的排序
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="columnIndexs">如果为null，则禁用全部列的排序</param>
        public static void DisableSort(DataGridView dgv, params int[] columnIndexs)
        {
            if (columnIndexs == null || columnIndexs.Length == 0)
            {
                foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                {
                    dgvColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                for (int i = 0; i < columnIndexs.Length; i++)
                {
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        /// <summary>
        /// 禁用DataGridView全部列的排序
        /// </summary>
        /// <param name="dgv"></param>
        public static void DisableSort(DataGridView dgv)
        {
            DisableSort(dgv, null);
        }
        #endregion

        #region DataGridView 绘画行号
        /// <summary>
        /// 绘画行号处理方式
        /// </summary>
        /// <param name="rowIndex">真正的行索引</param>
        /// <returns>返回行号需要显示的字符串信息</returns>
        public delegate string PaintRowIndexHandler(int rowIndex);

        /// <summary>
        /// 为DataGridView绘画行号
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="paintRowIndexHandler">绘画行号处理方式</param>
        public static void PaintRowIndexToHeaderCell(DataGridView dgv, PaintRowIndexHandler paintRowIndexHandler)
        {
            if (paintRowIndexHandler == null)
            {
                throw new Exception("paintRowIndexHandler不能为空");
            }

            dgv.RowPostPaint += delegate(object _sender, DataGridViewRowPostPaintEventArgs _e)
            {
                if (dgv.RowHeadersVisible)
                {
                    string writeText = paintRowIndexHandler(_e.RowIndex);

                    if (!string.IsNullOrEmpty(writeText))
                    {
                        //决定绘画行号范围
                        Rectangle rect = new Rectangle(
                            _e.RowBounds.Left, _e.RowBounds.Top,
                            dgv.RowHeadersWidth, _e.RowBounds.Height);
                        rect.Inflate(-2, -2);

                        //绘画行号 
                        TextRenderer.DrawText(_e.Graphics,
                            writeText,
                            _e.InheritedRowStyle.Font,      //引用行的字体
                            rect,
                            _e.InheritedRowStyle.ForeColor, //引用行的字颜色
                            TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                    }
                }
            };
        }

        /// <summary>
        /// 为DataGridView绘画行号
        /// </summary>
        /// <param name="dgv"></param>
        public static void PaintRowIndexToHeaderCell(DataGridView dgv)
        {
            PaintRowIndexToHeaderCell(dgv, delegate(int _rowIndex)
            {
                return (_rowIndex + 1).ToString();
            });
        }
        #endregion

        #region DataGridView 列信息格式化
        /// <summary>
        /// 为DataGrid列显示信息格式化
        /// </summary>
        /// <param name="dgvColumn"></param>
        /// <param name="formatString"></param>
        public static DataGridViewColumn FormatDataGridViewColumn(DataGridViewColumn dgvColumn, string formatString)
        {
            dgvColumn.DefaultCellStyle.Format = formatString;
            return dgvColumn;
        }
        #endregion

        #region DataGridView 打印
        private static StringFormat StrFormat;  // Holds content of a TextBox Cell to write by DrawString
        private static StringFormat StrFormatComboBox; // Holds content of a Boolean Cell to write by DrawImage
        private static Button CellButton;       // Holds the Contents of Button Cell
        private static CheckBox CellCheckBox;   // Holds the Contents of CheckBox Cell 
        private static ComboBox CellComboBox;   // Holds the Contents of ComboBox Cell

        private static int TotalWidth;          // Summation of Columns widths
        private static int RowPos;              // Position of currently printing row 
        private static bool NewPage;            // Indicates if a new page reached
        private static int PageNo;              // Number of pages to print
        private static int TotalPages;              // Number of pages to print
        private static bool IsGetTotalPages = false;
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns
        private static ArrayList ColumnWidths = new ArrayList(); // Width of Columns
        private static ArrayList ColumnTypes = new ArrayList();  // DataType of Columns
        private static int CellHeight;          // Height of DataGrid Cell
        private static int RowsPerPage;         // Number of Rows per Page
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static string PrintTitle = "";  // Header of pages
        private static DataGridView dgvPrint;        // Holds DataGridView Object to print its contents
        private static List<string> SelectedColumns = new List<string>();   // The Columns Selected by user to print.
        private static List<string> AvailableColumns = new List<string>();  // All Columns avaiable in DataGrid 
        private static bool PrintAllRows = true;   // True = print all rows,  False = print selected rows    
        private static bool FitToPageWidth = true; // True = Fits selected columns to page width ,  False = Print columns as showed    
        private static int HeaderHeight = 0;

        public static void Print_DataGridView(DataGridView dgv)
        {
            PrintPreviewDialog ppvw;
            try
            {
                dgvPrint = dgv;
                // Getting all Coulmns Names in the DataGridView
                GridViewHelper.AvailableColumns.Clear();
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    if (!c.Visible || c.GetType() == typeof(DataGridViewButtonColumn)) continue;
                    //
                    GridViewHelper.AvailableColumns.Add(c.HeaderText);
                }

                #region Showing the PrintOption Form
                //PrintOptions dlg = new PrintOptions(AvailableColumns);
                //if (dlg.ShowDialog() != DialogResult.OK) return;

                //PrintTitle = dlg.PrintTitle;
                //PrintAllRows = dlg.PrintAllRows;
                //FitToPageWidth = dlg.FitToPageWidth;
                //SelectedColumns = dlg.GetSelectedColumns();
                #endregion

                #region No PrintOption Form
                PrintTitle = string.Empty;
                PrintAllRows = true;
                FitToPageWidth = true;
                SelectedColumns = AvailableColumns;
                #endregion

                GridViewHelper.RowsPerPage = 0;

                GridViewHelper.printDoc.DefaultPageSettings.Margins.Left = 50;
                GridViewHelper.printDoc.DefaultPageSettings.Margins.Right = 50;
                GridViewHelper.printDoc.DefaultPageSettings.Margins.Top = 50;
                GridViewHelper.printDoc.DefaultPageSettings.Margins.Bottom = 70;
                GridViewHelper.printDoc.DefaultPageSettings.Landscape = true;  //(True为横向，False为竖向)
                //PageSettings ps = ShowPageSetupDialog(printDoc);
                //PrinterSettings ps2 = ShowPrintSetupDialog(printDoc);
                
                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                GridViewHelper.printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(GridViewHelper.PrintDoc_BeginPrint);
                GridViewHelper.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(GridViewHelper.PrintDoc_PrintPage);
                ((Form)ppvw).WindowState = FormWindowState.Maximized;
                if (ppvw.ShowDialog() != DialogResult.OK)
                {
                    GridViewHelper.printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(GridViewHelper.PrintDoc_BeginPrint);
                    GridViewHelper.printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(GridViewHelper.PrintDoc_PrintPage);
                    return;
                }

                // Printing the Documnet
                GridViewHelper.printDoc.Print();
                GridViewHelper.printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(GridViewHelper.PrintDoc_BeginPrint);
                GridViewHelper.printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(GridViewHelper.PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Formatting the Content of Text Cell to print
                GridViewHelper.StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;
                StrFormat.LineAlignment = StringAlignment.Center;
                StrFormat.Trimming = StringTrimming.EllipsisCharacter;

                // Formatting the Content of Combo Cells to print
                StrFormatComboBox = new StringFormat();
                StrFormatComboBox.LineAlignment = StringAlignment.Center;
                StrFormatComboBox.FormatFlags = StringFormatFlags.NoWrap;
                StrFormatComboBox.Trimming = StringTrimming.EllipsisCharacter;

                ColumnLefts.Clear();
                ColumnWidths.Clear();
                ColumnTypes.Clear();
                CellHeight = 0;
                RowsPerPage = 0;

                // For various column types
                CellButton = new Button();
                CellCheckBox = new CheckBox();
                CellComboBox = new ComboBox();

                // Calculating Total Widths
                TotalWidth = 0;
                foreach (DataGridViewColumn GridCol in dgvPrint.Columns)
                {
                    if (!GridCol.Visible) continue;
                    if (!SelectedColumns.Contains(GridCol.HeaderText)) continue;
                    TotalWidth += GridCol.Width;
                }
                PageNo = 1;
                TotalPages = 1;
                NewPage = true;
                RowPos = 0;
                IsGetTotalPages = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int  i;
            int tmpTop = e.MarginBounds.Top;
            //int tmpLeft = e.MarginBounds.Left;

            try
            {
                if (!IsGetTotalPages)
                    TotalPages = PrintTotalPage(e);//根据打印的内容得到打印的总页数


                // Printing Current Page, Row by Row
                while (RowPos <= dgvPrint.Rows.Count - 1)
                {
                    #region 遍历行 ---
                    DataGridViewRow GridRow = dgvPrint.Rows[RowPos];
                    if (GridRow.IsNewRow || (!PrintAllRows && !GridRow.Selected))
                    {
                        RowPos++;
                        continue;
                    }

                    #region 获取列中最大的高度 ----
                    int maxHeigth = 0;
                    int width = 0;
                    int height = 0;
                    string text = string.Empty;
                    i = 0;
                    foreach (DataGridViewCell Cel in GridRow.Cells)
                    {
                        #region 遍历列
                        if (!Cel.OwningColumn.Visible) continue;
                        if (Cel.GetType() == typeof(DataGridViewButtonColumn)) continue;
                        if (!SelectedColumns.Contains(Cel.OwningColumn.HeaderText))
                            continue;

                        // For the TextBox Column
                        if (((Type)ColumnTypes[i]).Name == "DataGridViewTextBoxColumn" ||
                            ((Type)ColumnTypes[i]).Name == "DataGridViewLinkColumn"
                            || ((Type)ColumnTypes[i]).Name == "DataGridViewButtonColumn"
                            || ((Type)ColumnTypes[i]).Name == "DataGridViewCheckBoxColumn")
                        {
                            text = Cel.Value + string.Empty;
                            width = (int)ColumnWidths[i];
                        }

                        // For the ComboBox Column
                        else if (((Type)ColumnTypes[i]).Name == "DataGridViewComboBoxColumn")
                        {
                            text = Cel.EditedFormattedValue + string.Empty;
                            width = (int)ColumnWidths[i];
                        }
                        // 每列所需要的实际高度
                        height = (int)(e.Graphics.MeasureString(text,
                                Cel.InheritedStyle.Font, width).Height) + 8;

                        if (height > maxHeigth) maxHeigth = height;
                        #endregion
                        i++;
                    }
                    #endregion

                    CellHeight = maxHeigth;// GridRow.Height;

                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        //DrawFooter(e, RowsPerPage);
                        DrawFooter(e);
                        NewPage = true;
                        PageNo++;
                        e.HasMorePages = true;
                        return;
                    }
                    else
                    {
                        if (NewPage)
                        {
                            // Draw Header
                            e.Graphics.DrawString(PrintTitle, new Font(dgvPrint.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                            e.Graphics.MeasureString(PrintTitle, new Font(dgvPrint.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String s = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(s, new Font(dgvPrint.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(s, new Font(dgvPrint.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString(PrintTitle, new Font(new Font(dgvPrint.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            // Draw Columns
                            tmpTop = e.MarginBounds.Top;
                            i = 0;
                            foreach (DataGridViewColumn GridCol in dgvPrint.Columns)
                            {
                                #region 遍历列
                                if (!GridCol.Visible) continue;
                                //if (GridCol.GetType() == typeof(DataGridViewCheckBoxColumn)) continue;
                                if (!SelectedColumns.Contains(GridCol.HeaderText))
                                    continue;

                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight), StrFormat);
                                i++;
                                #endregion
                            }
                            NewPage = false;
                            tmpTop += HeaderHeight;
                        }

                        // Draw Columns Contents
                        i = 0;
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            #region 遍历列
                            if (!Cel.OwningColumn.Visible) continue;
                            if (!SelectedColumns.Contains(Cel.OwningColumn.HeaderText)) continue;

                            // For the TextBox Column
                            if (((Type)ColumnTypes[i]).Name == "DataGridViewTextBoxColumn" ||
                                ((Type)ColumnTypes[i]).Name == "DataGridViewLinkColumn")
                            {
                                string val = string.Empty;
                                if (Cel.Value != null)
                                {
                                    val = Cel.Value.ToString();
                                }
                                if (Cel.Value is DateTime)
                                {
                                    DateTime dtCell = (DateTime)Cel.Value;
                                    if (dtCell.Hour == 0 && dtCell.Minute == 0 && dtCell.Second == 0)
                                    {
                                        val = dtCell.ToString("yyyy-MM-dd");
                                    }
                                }
                                //
                                Font celFont = new Font("宋体", Cel.InheritedStyle.Font.Size, FontStyle.Regular, GraphicsUnit.Pixel);
                                e.Graphics.DrawString(val, celFont,
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)ColumnLefts[i], (float)tmpTop,
                                        (int)ColumnWidths[i], (float)CellHeight), StrFormat);
                            }
                            // For the Button Column
                            else if (((Type)ColumnTypes[i]).Name == "DataGridViewButtonColumn")
                            {
                                CellButton.Text = Cel.Value.ToString();
                                CellButton.Size = new Size((int)ColumnWidths[i], CellHeight);
                                Bitmap bmp = new Bitmap(CellButton.Width, CellButton.Height);
                                CellButton.DrawToBitmap(bmp, new Rectangle(0, 0,
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            }
                            // For the CheckBox Column
                            else if (((Type)ColumnTypes[i]).Name == "DataGridViewCheckBoxColumn")
                            {
                                //continue;
                                //CellCheckBox.Size = new Size(14, 14);
                                ////CellCheckBox.Checked = (bool)Cel.Value;  这句是选中后才能打印
                                //Bitmap bmp = new Bitmap((int)ColumnWidths[i], CellHeight);
                                //Graphics tmpGraphics = Graphics.FromImage(bmp);
                                //tmpGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0,
                                //        bmp.Width, bmp.Height));
                                //CellCheckBox.DrawToBitmap(bmp,
                                //        new Rectangle((int)((bmp.Width - CellCheckBox.Width) / 2),
                                //        (int)((bmp.Height - CellCheckBox.Height) / 2),
                                //        CellCheckBox.Width, CellCheckBox.Height));
                                //e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            }
                            // For the ComboBox Column
                            else if (((Type)ColumnTypes[i]).Name == "DataGridViewComboBoxColumn")
                            {
                                CellComboBox.Size = new Size((int)ColumnWidths[i], CellHeight);
                                Bitmap bmp = new Bitmap(CellComboBox.Width, CellComboBox.Height);
                                CellComboBox.DrawToBitmap(bmp, new Rectangle(0, 0,
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                                e.Graphics.DrawString(Cel.EditedFormattedValue + string.Empty, Cel.InheritedStyle.Font,
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)ColumnLefts[i] + 1, tmpTop, (int)ColumnWidths[i]
                                        - 16, CellHeight), StrFormatComboBox);
                            }
                            // For the Image Column
                            else if (((Type)ColumnTypes[i]).Name == "DataGridViewImageColumn")
                            {
                                Rectangle CelSize = new Rectangle((int)ColumnLefts[i],
                                        tmpTop, (int)ColumnWidths[i], CellHeight);
                                Size ImgSize = ((Image)(Cel.FormattedValue)).Size;
                                e.Graphics.DrawImage((Image)Cel.FormattedValue,
                                        new Rectangle((int)ColumnLefts[i] + (int)((CelSize.Width - ImgSize.Width) / 2),
                                        tmpTop + (int)((CelSize.Height - ImgSize.Height) / 2),
                                        ((Image)(Cel.FormattedValue)).Width, ((Image)(Cel.FormattedValue)).Height));

                            }



                            // Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)ColumnLefts[i],
                                    tmpTop, (int)ColumnWidths[i], CellHeight));

                            i++;
                            #endregion
                        }
                        tmpTop += CellHeight;
                    }

                    RowPos++;
                    // For the first page it calculates Rows per Page
                    if (PageNo == 1) RowsPerPage++;
                    #endregion
                }

                if (RowsPerPage == 0) return;

                // Write Footer (Page Number)
                //DrawFooter(e, RowsPerPage);
                DrawFooter(e);

               e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int PrintTotalPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int totalPage = 1;
            int row = 0;
            int tmpWidth, i;
            int tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;
            bool newpage = true;


            try
            {
                // Before starting first page, it saves Width & Height of Headers and CoulmnType

                foreach (DataGridViewColumn GridCol in dgvPrint.Columns)
                {
                    if (!GridCol.Visible) continue;
                    // Skip if the current column not selected
                    if (GridCol.GetType() == typeof(DataGridViewButtonColumn)) continue;
                    if (!SelectedColumns.Contains(GridCol.HeaderText)) continue;

                    // Detemining whether the columns are fitted to page or not.
                    if (FitToPageWidth)
                        tmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                   (double)TotalWidth * (double)TotalWidth *
                                   ((double)e.MarginBounds.Width / (double)TotalWidth))));
                    else
                        tmpWidth = GridCol.Width;

                    int headerheight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, tmpWidth).Height) + 10;
                    if (headerheight > HeaderHeight)
                        HeaderHeight = headerheight;

                    // Save width & height of headres and ColumnType
                    ColumnLefts.Add(tmpLeft);
                    ColumnWidths.Add(tmpWidth);
                    ColumnTypes.Add(GridCol.GetType());
                    tmpLeft += tmpWidth;
                }

                // Printing Current Page, Row by Row
                while (row <= dgvPrint.Rows.Count - 1)
                {
                    #region 遍历行 ---
                    DataGridViewRow GridRow = dgvPrint.Rows[row];
                    if (GridRow.IsNewRow || (!PrintAllRows && !GridRow.Selected))
                    {
                        row++;
                        continue;
                    }

                    #region 获取列中最大的高度 ----
                    int maxHeigth = 0;
                    int width = 0;
                    int height = 0;
                    string text = string.Empty;
                    i = 0;
                    foreach (DataGridViewCell Cel in GridRow.Cells)
                    {
                        #region 遍历列
                        if (!Cel.OwningColumn.Visible) continue;
                        if (Cel.GetType() == typeof(DataGridViewButtonColumn)) continue;
                        if (!SelectedColumns.Contains(Cel.OwningColumn.HeaderText))
                            continue;

                        // For the TextBox Column
                        if (((Type)ColumnTypes[i]).Name == "DataGridViewTextBoxColumn" ||
                            ((Type)ColumnTypes[i]).Name == "DataGridViewLinkColumn"
                            || ((Type)ColumnTypes[i]).Name == "DataGridViewButtonColumn"
                            || ((Type)ColumnTypes[i]).Name == "DataGridViewCheckBoxColumn")
                        {
                            text = Cel.Value + string.Empty;
                            width = (int)ColumnWidths[i];
                        }

                        // For the ComboBox Column
                        else if (((Type)ColumnTypes[i]).Name == "DataGridViewComboBoxColumn")
                        {
                            text = Cel.EditedFormattedValue + string.Empty;
                            width = (int)ColumnWidths[i];
                        }
                        // 每列所需要的实际高度
                        height = (int)(e.Graphics.MeasureString(text,
                                Cel.InheritedStyle.Font, width).Height) + 8;

                        if (height > maxHeigth) maxHeigth = height;
                        #endregion
                        i++;
                    }
                    #endregion

                    CellHeight = maxHeigth;// GridRow.Height;

                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        newpage = true;
                        totalPage++;
                        tmpTop = e.MarginBounds.Top;
                    }
                    else
                    {
                        if (newpage)
                        {
                            // Draw Columns
                            tmpTop = e.MarginBounds.Top;
                            newpage = false;
                            tmpTop += HeaderHeight;
                        }

                        tmpTop += CellHeight;
                        row++;
                    }


                    #endregion
                }

                IsGetTotalPages = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return totalPage;
        }

        private static void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Writing the Page Number on the Bottom of Page
            string PageNum = " 第 " + PageNo.ToString()
                + " 页，共 " + TotalPages.ToString() + " 页";

            e.Graphics.DrawString(PageNum, dgvPrint.Font, Brushes.Black,
                e.MarginBounds.Left + (e.MarginBounds.Width -
                e.Graphics.MeasureString(PageNum, dgvPrint.Font,
                e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top +
                e.MarginBounds.Height + 31);
        }
        private static void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e, int RowsPerPage)
        {
            double cnt = 0;

            // Detemining rows number to print
            if (PrintAllRows)
            {
                if (dgvPrint.Rows[dgvPrint.Rows.Count - 1].IsNewRow)
                    cnt = dgvPrint.Rows.Count - 2; // When the DataGridView doesn't allow adding rows
                else
                    cnt = dgvPrint.Rows.Count - 1; // When the DataGridView allows adding rows
            }
            else
                cnt = dgvPrint.SelectedRows.Count;

            double totalPage = Math.Ceiling((double)(cnt / RowsPerPage));
            if (totalPage == 0) totalPage = 1;

            // Writing the Page Number on the Bottom of Page
            string PageNum = " 第 " + PageNo.ToString()
                + " 页，共 " +totalPage.ToString()+ " 页";

            e.Graphics.DrawString(PageNum, dgvPrint.Font, Brushes.Black,
                e.MarginBounds.Left + (e.MarginBounds.Width -
                e.Graphics.MeasureString(PageNum, dgvPrint.Font,
                e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top +
                e.MarginBounds.Height + 31);
        }


        #region 页面设置对话框
        /// <summary>
        /// 页面设置对话框
        /// </summary>
        /// <param name="printDocument"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        private static PageSettings ShowPageSetupDialog(PrintDocument printDocument)
        {
            //声明返回值的PageSettings
            PageSettings ps = new PageSettings();
            if (printDocument == null)
            {
                throw new Exception("关联的打印文件不能为空！");
            }
            try
            {
                //申明并实例化PageSetupDialog
                PageSetupDialog psDlg = new PageSetupDialog();

                //相关文件及文件页面默认设置
                psDlg.Document = printDocument;
                psDlg.PageSettings = printDocument.DefaultPageSettings;
                //显示对话框
                DialogResult result = psDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ps = psDlg.PageSettings;
                    printDocument.DefaultPageSettings = psDlg.PageSettings;
                }
            }
            catch (System.Drawing.Printing.InvalidPrinterException)
            {
                MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ps;
        }
        #endregion

        #region 打印设置对话框
        /// <summary>
        /// 打印设置对话框
        /// </summary>
        /// <param name="printDocument"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        private static PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
        {
            //声明返回值的PrinterSettings
            PrinterSettings ps = new PrinterSettings();
            if (printDocument == null)
            {
                throw new Exception("关联的打印文件不能为空！");
            }
            try
            {
                //申明并实例化PrintDialog
                PrintDialog pDlg = new PrintDialog();

                //能选定页
                pDlg.AllowSomePages = true;
                //指定打印文件
                pDlg.Document = printDocument;
                //显示对话框
                DialogResult result = pDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //保存打印设置
                    ps = pDlg.PrinterSettings;
                    //打印
                    printDocument.Print();
                }
            }
            catch (System.Drawing.Printing.InvalidPrinterException)
            {
                MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ps;
        }
        #endregion

        #endregion

        #region 导出 DataGridView 到 Excel
        /// <summary>
        /// 导出 DataGridView 到 Excel
        /// </summary>
        /// <param name="caption">显示标题</param>
        /// <param name="sheetName">Sheetname</param>
        /// <param name="dgv">导出的DataGridView</param>
        public static void ExportExcel(string caption, string sheetName, DataGridView dgv)
        {
            // 列索引，行索引，总列数，总行数
            int colIndex = 0;
            int rowIndex = 0;
            int startIndex = 1;
            int colCount = 0;
            // 不显示隐藏的列
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Columns[i].Visible == true)
                {
                    colCount++;
                }
            }

            int rowCount = dgv.RowCount;
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlBook;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;
            Microsoft.Office.Interop.Excel.Range range;

            // 创建Excel对象
            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            if (xlApp == null)
            {
                MessageBox.Show("您的机器未安装 Office!");
                return;
            }
            try
            {
                // 创建Excel工作薄
                xlBook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                xlSheet.Name = sheetName == string.Empty ? "Sheet1" : sheetName;
                // 设置标题
                if (caption != string.Empty)
                {
                    range = xlSheet.get_Range(xlApp.Cells[startIndex, 1], xlApp.Cells[startIndex, colCount]); //标题所占的单元格数与DataGridView中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = caption;
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    startIndex++;
                }
                // 设置 GridView 的标题
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible == false) continue; // 不显示隐藏的列
                    //if (col is DataGridViewCheckBoxColumn) continue;
                    range = (Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[startIndex, ++colIndex];
                    range.Value2 = col.HeaderText;
                    range.Font.Bold = true;
                    range.Font.Size = 13;
                    range.ColumnWidth = Convert.ToDecimal(col.Width) / 9;
                    range.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(200, 200, 200));
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                }
                startIndex++;

                // 创建缓存数据
                object[,] objData = new object[rowCount, colCount];

                // 获取数据
                int index;
                for (rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    index = 0;// 不显示隐藏的列
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        //if (dgv[colIndex, rowIndex].ValueType == typeof(string)
                        //    || dgv[colIndex, rowIndex].ValueType == typeof(DateTime))//这里就是验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
                        //{
                        //    objData[rowIndex, colIndex] = dgv[colIndex, rowIndex].Value;
                        //}
                        //else
                        //{
                        //    objData[rowIndex, colIndex] = (dgv[colIndex, rowIndex].Value + string.Empty).Trim();    
                        //}
                        while (dgv.Columns[index].Visible == false)
                        {
                            index++;
                        }
                        DataGridViewCell cell = dgv[index, rowIndex];
                        if (cell is DataGridViewComboBoxCell)
                        {
                            objData[rowIndex, colIndex] = cell.FormattedValue;
                        }
                        else
                        {
                            objData[rowIndex, colIndex] = (cell.Value + string.Empty).Trim();
                        }
                        index++;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                // 写入Excel
                if (rowCount > 0)
                {
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[startIndex, 1], xlApp.Cells[rowCount + startIndex - 1, colCount]);
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                }

                // 显示 Excel
                //xlApp.Visible = true;

                //弹出保存文件对话框保存Excel
                string extension = xlApp.Version == "12.0" ? "xlsx" : "xls";
                SaveFileDialog saveDlg = new SaveFileDialog();

                saveDlg.FileName = string.Format("{0}.{1}", string.IsNullOrEmpty(caption) ? dgv.FindForm().Text : (string.IsNullOrEmpty(sheetName) ? dgv.FindForm().Text : sheetName), extension);
                saveDlg.Filter = string.Format("Microsoft Office Excel 工作表|*.{0}", extension);
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    xlBook.Saved = true;
                    xlBook.SaveCopyAs(saveDlg.FileName);

                    //确保Excel进程关闭    
                    xlApp.Quit();
                    xlApp = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                if (xlApp != null)
                {
                    xlApp.ShowStartupDialog = false;
                    xlApp.Quit();
                    GC.Collect(); //强制回收
                }
                ex.ToString();
            }
        }

        /// <summary>
        /// 导出DataGridView数据到Excel，导出时不依赖是否安装Excel环境。
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="sheetName"></param>
        /// <param name="dgv"></param>
        public static void DataGridViewToExcel(string caption, string sheetName, DataGridView dgv)
        {
            TemplateToExcel xls = new TemplateToExcel();
            xls.TemplatePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\Template\Report Template.xls";

            int rowIndex = 0, columnIndex = 0;

            //设置列标题
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn)
                    && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                {
                    xls.SetCellValue(rowIndex, columnIndex, string.IsNullOrEmpty(col.HeaderText) ? col.Name : col.HeaderText);
                    columnIndex++;
                }
            }
            rowIndex++;

            //填充数据
            string val = string.Empty;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                columnIndex = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn) && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                    {
                        if (col.GetType() == typeof(DataGridViewComboBoxColumn))
                        {
                            xls.SetCellValue(rowIndex, columnIndex, 
                                dr.Cells[col.Index].Value == null || dr.Cells[col.Index].Value == DBNull.Value ? "" : 
                                (dr.Cells[col.Index] as DataGridViewComboBoxCell).FormattedValue);
                        }
                        else
                        {
                            val = string.Empty;
                            if (dr.Cells[col.Index].Value != null && dr.Cells[col.Index].Value != DBNull.Value)
                            {
                                if (dr.Cells[col.Index].Value is DateTime)
                                {
                                    DateTime dtCell = (DateTime)dr.Cells[col.Index].Value;
                                    if (dtCell.Hour == 0 && dtCell.Minute == 0 && dtCell.Second == 0)
                                    {
                                        val = dtCell.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        val = dr.Cells[col.Index].Value + string.Empty;
                                    }
                                }
                                else
                                {
                                    val = dr.Cells[col.Index].Value.ToString().Trim();
                                }
                            }
                            //
                            xls.SetCellValue(rowIndex, columnIndex, val == "True" ? "是" : val == "False" ? "否" : val);
                        }
                        columnIndex++;
                    }
                }
                rowIndex++;
            }

            //列宽自适应
            columnIndex = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn) && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                {
                    xls.SetColumnVisible(columnIndex, true);//模板中该列可见
                    xls.SetColumnWidth(columnIndex, -1);//该列自适应
                    columnIndex++;
                }
            }

            //保存Excel到指定位置
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.FileName = string.Format("{0}.xls", string.IsNullOrEmpty(caption) ? dgv.FindForm().Text : (string.IsNullOrEmpty(sheetName) ? dgv.FindForm().Text : sheetName));
            saveDlg.Filter = string.Format("Excel 2003工作表|*.xls");
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                xls.SaveAs(saveDlg.FileName);
            }
        }
        /// <summary>
        /// 导出DataGridView数据到Excel，导出时不依赖是否安装Excel环境。
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="sheetName"></param>
        /// <param name="dgv"></param>
        public static void DataGridViewToExcel(string caption, string sheetName, DataGridView dgv, List<int> DecimalColumns)
        {
            TemplateToExcel xls = new TemplateToExcel();
            xls.TemplatePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\Template\Report Template.xls";

            int rowIndex = 0, columnIndex = 0;

            //设置列标题
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn)
                    && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                {
                    xls.SetCellValue(rowIndex, columnIndex, string.IsNullOrEmpty(col.HeaderText) ? col.Name : col.HeaderText);
                    columnIndex++;
                }
            }
            rowIndex++;

            //填充数据
            string val = string.Empty;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                columnIndex = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn) && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                    {
                        if (col.GetType() == typeof(DataGridViewComboBoxColumn))
                        {
                            xls.SetCellValue(rowIndex, columnIndex,
                                dr.Cells[col.Index].Value == null || dr.Cells[col.Index].Value == DBNull.Value ? "" :
                                (dr.Cells[col.Index] as DataGridViewComboBoxCell).FormattedValue);
                        }
                        else
                        {
                            val = string.Empty;
                            if (dr.Cells[col.Index].Value != null && dr.Cells[col.Index].Value != DBNull.Value)
                            {
                                if (dr.Cells[col.Index].Value is DateTime)
                                {
                                    DateTime dtCell = (DateTime)dr.Cells[col.Index].Value;
                                    if (dtCell.Hour == 0 && dtCell.Minute == 0 && dtCell.Second == 0)
                                    {
                                        val = dtCell.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        val = dr.Cells[col.Index].Value + string.Empty;
                                    }
                                }
                                else
                                {
                                    val = dr.Cells[col.Index].Value.ToString().Trim();
                                }
                            }
                            //
                            if (!string.IsNullOrEmpty(val) && DecimalColumns != null && DecimalColumns.Count > 0 && DecimalColumns.Contains(columnIndex))
                            {
                                decimal dec = 0;
                                if (Decimal.TryParse(val, out dec))
                                {
                                    xls.SetCellValue(rowIndex, columnIndex, dec);
                                }
                                else
                                {
                                    xls.SetCellValue(rowIndex, columnIndex, val == "True" ? "是" : val == "False" ? "否" : val);
                                }
                            }
                            else
                            {
                                xls.SetCellValue(rowIndex, columnIndex, val == "True" ? "是" : val == "False" ? "否" : val);
                            }
                        }
                        columnIndex++;
                    }
                }
                rowIndex++;
            }

            //列宽自适应
            columnIndex = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.GetType() != typeof(DataGridViewImageColumn) && col.GetType() != typeof(DataGridViewCheckBoxColumn))
                {
                    xls.SetColumnVisible(columnIndex, true);//模板中该列可见
                    xls.SetColumnWidth(columnIndex, -1);//该列自适应
                    columnIndex++;
                }
            }

            //保存Excel到指定位置
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.FileName = string.Format("{0}.xls", string.IsNullOrEmpty(caption) ? dgv.FindForm().Text : (string.IsNullOrEmpty(sheetName) ? dgv.FindForm().Text : sheetName));
            saveDlg.Filter = string.Format("Excel 2003工作表|*.xls");
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                xls.SaveAs(saveDlg.FileName);
            }
        }

        /// <summary>
        /// 导出 DataGridView 到 Excel
        /// </summary>
        /// <param name="caption">显示标题</param>
        /// <param name="sheetName">Sheetname</param>
        /// <param name="dgv">导出的DataGridView</param>
        public static void ExportExcelForCheckBoxList(string caption, string sheetName, DataGridView dgv)
        {
            // 列索引，行索引，总列数，总行数
            int colIndex = 0;
            int rowIndex = 0;
            int startIndex = 1;
            int colCount = 0;
            // 不显示隐藏的列
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Columns[i].Visible == true)
                {
                    colCount++;
                }
            }


            int rowCount = dgv.RowCount;
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlBook;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;
            Microsoft.Office.Interop.Excel.Range range;

            // 创建Excel对象
            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            if (xlApp == null)
            {
                MessageBox.Show("您的机器未安装 Office!");
                return;
            }
            try
            {
                //弹出保存文件对话框保存Excel
                string extension = xlApp.Version == "12.0" ? "xlsx" : "xls";
                SaveFileDialog saveDlg = new SaveFileDialog();

                saveDlg.FileName = string.Format("{0}.{1}", string.IsNullOrEmpty(caption) ? dgv.FindForm().Text : (string.IsNullOrEmpty(sheetName) ? dgv.FindForm().Text : sheetName), extension);
                saveDlg.Filter = string.Format("Microsoft Office Excel 工作表|*.{0}", extension);
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    // 创建Excel工作薄
                    xlBook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                    xlSheet.Name = sheetName == string.Empty ? "Sheet1" : sheetName;
                    // 设置标题
                    if (caption != string.Empty)
                    {
                        range = xlSheet.get_Range(xlApp.Cells[startIndex, 1], xlApp.Cells[startIndex, colCount]); //标题所占的单元格数与DataGridView中的列数相同
                        range.MergeCells = true;
                        xlApp.ActiveCell.FormulaR1C1 = caption;
                        xlApp.ActiveCell.Font.Size = 20;
                        xlApp.ActiveCell.Font.Bold = true;
                        xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        startIndex++;
                    }
                    // 设置 GridView 的标题
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible == false) continue; // 不显示隐藏的列
                        //if (col is DataGridViewCheckBoxColumn) continue;
                        range = (Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[startIndex, ++colIndex];
                        range.Value2 = col.HeaderText;
                        range.Font.Bold = true;
                        range.Font.Size = 13;
                        range.ColumnWidth = Convert.ToDecimal(col.Width) / 9;
                        range.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(200, 200, 200));
                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    }
                    startIndex++;

                    // 创建缓存数据
                    object[,] objData = new object[rowCount, colCount];

                    // 获取数据
                    int index;
                    int actualRow = -1;
                    for (rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        //如果复选框没有选中，则不导出该行
                        if ((dgv.Rows[rowIndex].Cells[0] as DataGridViewCheckBoxCell).EditedFormattedValue.ToString().ToLower() != "true")
                            continue;
                        else
                        {
                            actualRow++;
                        }

                        index = 0;// 不显示隐藏的列
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            //if (dgv[colIndex, rowIndex].ValueType == typeof(string)
                            //    || dgv[colIndex, rowIndex].ValueType == typeof(DateTime))//这里就是验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
                            //{
                            //    objData[rowIndex, colIndex] = dgv[colIndex, rowIndex].Value;
                            //}
                            //else
                            //{
                            //    objData[rowIndex, colIndex] = (dgv[colIndex, rowIndex].Value + string.Empty).Trim();    
                            //}
                            while (dgv.Columns[index].Visible == false)
                            {
                                index++;
                            }
                            DataGridViewCell cell = dgv[index, rowIndex];
                            if (cell is DataGridViewComboBoxCell || cell is DataGridViewCheckBoxCell)
                            {
                                objData[actualRow, colIndex] = String.Empty;
                            }
                            else
                            {
                                objData[actualRow, colIndex] = (cell.Value + string.Empty).Trim();
                            }

                            index++;
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    // 写入Excel
                    if (rowCount > 0)
                    {
                        // 写入Excel
                        range = xlSheet.get_Range(xlApp.Cells[startIndex, 1], xlApp.Cells[rowCount + startIndex - 1, colCount]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }

                    // 显示 Excel
                    //xlApp.Visible = true;


                    xlBook.Saved = true;
                    xlBook.SaveCopyAs(saveDlg.FileName);

                    //确保Excel进程关闭    
                    xlApp.Quit();
                    xlApp = null;
                    GC.Collect();
                }
                else
                {
                    xlApp.Quit();
                    xlApp = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                if (xlApp != null)
                {
                    xlApp.ShowStartupDialog = false;
                    xlApp.Quit();
                    GC.Collect(); //强制回收
                }
                ex.ToString();
            }
        }
        #endregion
        #endregion

        #region MD5 加密
        /// <summary>
        /// 返回 MD5 加密后的数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetMd5Value(string value)
        {
            byte[] result = Encoding.Default.GetBytes(value);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion
    }
}
