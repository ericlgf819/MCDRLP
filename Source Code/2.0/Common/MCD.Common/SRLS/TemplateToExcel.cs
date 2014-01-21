using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MCD.Common
{
    /// <summary>
    /// 提供根据Excel模板将DataTable导出到Excel的方法。
    /// </summary>
    public class TemplateToExcel
    {
        #region 字段属性
        private Dictionary<string, int> columnMappings = new Dictionary<string, int>();
        private HSSFWorkbook workbook = null;//工作簿对象
        private Sheet sheet = null;//模板工作表
        private Regex localRegex = new Regex(@"([A-Z]{0,2})(\d{1,5})", RegexOptions.IgnoreCase);

        /// <summary>
        /// 获取或待生成Excel的数据源DataTable对象。
        /// </summary>
        public DataTable DataSource
        {
            get;
            set;
        }

        private string templatePath = string.Empty;
        /// <summary>
        /// 获取或设置模板文件路径。
        /// </summary>
        public string TemplatePath
        {
            get { return templatePath; }
            set
            {
                if (templatePath != value)
                {
                    templatePath = value;
                    this.InitializeWorkbook();
                }
            }
        }

        /// <summary>
        /// 获取或设置第一个工作表的名称。
        /// </summary>
        public string SheetName
        {
            get { return workbook.GetSheetName(0); }
            set { workbook.SetSheetName(0, value); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化TemplateToExcel类的新实例。
        /// </summary>
        public TemplateToExcel()
        {
            
        }

        /// <summary>
        /// 初始化TemplateToExcel类的新实例，并指定Excel模板路径及数据源。
        /// </summary>
        /// <param name="tempalePath"></param>
        /// <param name="dataSource"></param>
        public TemplateToExcel(string tempalePath, DataTable dataSource)
            : this()
        {
            this.TemplatePath = tempalePath;
            this.DataSource = dataSource;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化工作簿对象。
        /// </summary>
        private void InitializeWorkbook()
        {
            if (!File.Exists(this.TemplatePath))
            {
                throw new Exception(string.Format("指定位置的Excel模板文件：{0}不存在。", this.TemplatePath));
            }

            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            FileStream file = new FileStream(this.TemplatePath, FileMode.Open, FileAccess.Read);

            workbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "MCD";
            workbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "";
            workbook.SummaryInformation = si;

            sheet = workbook.GetSheetAt(0);
        }

        /// <summary>
        /// 根据单元格的位置字符串返回行列索引。
        /// </summary>
        /// <param name="cellLocation"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        private void GetCellLocation(string cellLocation, out int rowIndex, out int columnIndex)
        {
            Match match = localRegex.Match(cellLocation);
            if (match == null || !match.Success)
                throw new Exception(string.Format("单元格位置字符串\"{0}\"格式不正确。", cellLocation));

            //列号
            string str = null;
            str = match.Groups[1].Value.ToUpper();
            columnIndex = str.Length == 2 ? (((int)str[0] - 64) * 26 + ((int)str[1] - 64)) : ((int)str[0] - 64);
            if (columnIndex > 256)
                throw new Exception(string.Format("单元格位置的列超过最大范围256。", cellLocation));

            //行号
            rowIndex = int.Parse(match.Groups[2].Value);
            if(rowIndex > 65536)
                throw new Exception(string.Format("单元格位置的行超过最大范围65536。", cellLocation));

            rowIndex = rowIndex - 1;
            columnIndex = columnIndex - 1;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 设置指定位置的单元格的值，该位置是由单元格所在位置的行列索引组成。
        /// </summary>
        /// <param name="rowIndex">单元格所在的行索引</param>
        /// <param name="columnIndex">单元格所在的列索引</param>
        /// <param name="value">单元格显示的值</param>
        public Cell SetCellValue(int rowIndex, int columnIndex, object value)
        {
            Row dr = sheet.GetRow(rowIndex);
            if (dr == null)
                dr = sheet.CreateRow(rowIndex);

            Cell cell = dr.GetCell(columnIndex);
            if (cell == null)
                cell = dr.CreateCell(columnIndex);

            if (value != null && value != DBNull.Value)
            {
                if (value.GetType() == typeof(bool))
                    cell.SetCellValue(Convert.ToBoolean(value));
                else if (value.GetType() == typeof(DateTime))
                    cell.SetCellValue(Convert.ToDateTime(value));
                else if (value.GetType() == typeof(double)
                    || value.GetType() == typeof(float)
                    || value.GetType() == typeof(int)
                    || value.GetType() == typeof(decimal))
                    cell.SetCellValue(Convert.ToDouble(value));
                else
                    cell.SetCellValue(Convert.ToString(value));
            }
            return cell;
        }

        /// <summary>
        /// 设置指定位置的单元格的值，该位置是Excel中表示位置的字符串如"B2"。
        /// </summary>
        /// <param name="cellLocation"></param>
        /// <param name="value"></param>
        public Cell SetCellValue(string cellLocation, object value)
        {
            int rowIndex, columnIndex;
            this.GetCellLocation(cellLocation, out rowIndex, out columnIndex);
            if (rowIndex > -1 && columnIndex > -1)
            {
                return this.SetCellValue(rowIndex, columnIndex, value);
            }
            return null;
        }

        /// <summary>
        /// 设置指定列的宽度，为-1时根据内容自适应，为0时则隐藏该列。
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="width"></param>
        public void SetColumnWidth(int columnIndex, int width)
        {
            if (sheet != null)
            {
                if (width == -1)
                    sheet.AutoSizeColumn(columnIndex);
                else if (width > 0)
                    sheet.SetColumnWidth(columnIndex, width);
            }
        }

        /// <summary>
        /// 设置指定的列是否可见。
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="visible"></param>
        public void SetColumnVisible(int columnIndex, bool visible)
        {
            sheet.SetColumnHidden(columnIndex, !visible);
        }

        public void SetActiveCell(int rowIndex, int columnIndex)
        {
            sheet.SetActiveCell(rowIndex, columnIndex);
        }

        /// <summary>
        /// 获取一个新的单元格样式实例。
        /// </summary>
        public CellStyle NewCellStyle()
        {
            if (workbook != null)
                return workbook.CreateCellStyle();
            return null;
        }

        /// <summary>
        /// 新增数据源中列名称到Excel模板中的列映射。
        /// </summary>
        /// <param name="columnName">数据源中的列名称</param>
        /// <param name="columnIndex">Excel中的列索引</param>
        public void SetColumnMapping(string columnName, int columnIndex)
        {
            if (columnMappings.ContainsKey(columnName))
                columnMappings[columnName] = columnIndex;
            else
                columnMappings.Add(columnName, columnIndex);
        }

        /// <summary>
        /// 新增数据源中列名称到Excel模板中的列映射。
        /// </summary>
        /// <param name="columnName">数据源中的列名称</param>
        /// <param name="str">Excel中的表示列位置的字符串</param>
        public void SetColumnMapping(string columnName, string columnStr)
        {
            int columnNum = columnStr.Length == 2 ? (((int)columnStr[0] - 64) * 26 + ((int)columnStr[1] - 64)) : ((int)columnStr[0] - 64);
            if (columnNum > 0)
                this.SetColumnMapping(columnName, columnNum - 1);
        }

        /// <summary>
        /// 从数据源中指定索引的行开始填充数据到Excel模板中。
        /// </summary>
        /// <param name="startRowIndex"></param>
        public void Fill(int startRowIndex)
        {
            if (this.DataSource == null)
                return;

            int rowIndex = startRowIndex, columnIndex = 0;
            foreach (DataRow dr in this.DataSource.Rows)
            {
                columnIndex = 0;
                foreach (DataColumn column in this.DataSource.Columns)
                {
                    if (columnMappings.ContainsKey(column.ColumnName))
                    {
                        this.SetCellValue(rowIndex, columnMappings[column.ColumnName], dr[column.ColumnName]);
                    }
                    //else
                    //{
                    //    this.SetCellValue(rowIndex, columnIndex, dr[column.ColumnName]);
                    //}
                    columnIndex++;
                }
                rowIndex++;
            }

            //Force excel to recalculate all the formula while open
            sheet.ForceFormulaRecalculation = true;
        }

        /// <summary>
        /// 另存为到指定的路径。
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAs(string fileName)
        {
            //Write the stream data of workbook to the root directory
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(file);
            }
        }

        /// <summary>
        /// 将数据保存到指定Excel文件。
        /// </summary>
        /// <param name="sheetValues"></param>
        /// <param name="sheetName"></param>
        /// <param name="fileName"></param>
        public static void SaveToXls(List<string[]> sheetValues, string sheetName, string fileName)
        {
            int rowIndex = 0, columnIndex = 0;
            MCD.Common.TemplateToExcel excel = new MCD.Common.TemplateToExcel();
            excel.TemplatePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\Template\Empty Template.xls";
            excel.SheetName = sheetName;
            foreach (string[] arr in sheetValues)
            {
                foreach (string str in arr)
                {
                    excel.SetCellValue(rowIndex, columnIndex, str);
                    columnIndex++;
                }
                columnIndex = 0;
                rowIndex++;
            }
            excel.SaveAs(fileName);
        }
        #endregion
    }
}