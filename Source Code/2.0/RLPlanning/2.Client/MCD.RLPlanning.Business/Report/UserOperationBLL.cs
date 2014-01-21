using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using Microsoft.Office.Interop.Excel;

using MCD.RLPlanning.IServices.Report;

namespace MCD.RLPlanning.BLL.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class UserOperationBLL : BaseBLL<IUserOperationService>
    {
        //Fields
        private int sheetRowsCount = 0;
        private Object Nothing = System.Reflection.Missing.Value;
        private Dictionary<string, System.Data.DataTable> TableColumns = new Dictionary<string, System.Data.DataTable>();

        /// <summary>
        /// 每个 Worksheet 显示最大行数
        /// </summary>
        protected int SheetRowsCount
        {
            get
            {
                if (this.sheetRowsCount == 0)
                {
                    if (!int.TryParse(ConfigurationManager.AppSettings["SheetRowsCount"] + string.Empty, out this.sheetRowsCount))
                    {
                        this.sheetRowsCount = 50000;
                    }
                    else if (this.sheetRowsCount < 1000)
                    {
                        this.sheetRowsCount = 1000;
                    }
                    else if (this.sheetRowsCount > 65000)
                    {
                        this.sheetRowsCount = 65000;
                    }
                }
                return this.sheetRowsCount;
            }
        }

        /// <summary>
        /// 查询用户操作日志信息
        /// </summary>
        /// <param name="companyStartNo"></param>
        /// <param name="companyEndNo"></param>
        /// <param name="storeStartNo"></param>
        /// <param name="storeEndNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private DataSet SelectUserOperations(int? companyStartNo, int? companyEndNo, int? storeStartNo, int? storeEndNo,
            DateTime startDate, DateTime endDate, string operationType)
        {
            return base.DeSerilize(base.WCFService.SelectUserOperations(companyStartNo, companyEndNo, storeStartNo, storeEndNo, 
                startDate, endDate, operationType));
        }
        /// <summary>
        /// 获取该表需要显示的字段名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private System.Data.DataTable GetColumnsTable(string tableName)
        {
            if (!this.TableColumns.Keys.Contains(tableName))
            {
                this.TableColumns.Add(tableName, base.DeSerilize(base.WCFService.SelectTableColumns(tableName)).Tables[0]);
            }
            return this.TableColumns[tableName];
        }

        /// <summary>
        /// 导出为 Excel
        /// </summary>
        /// <param name="companyStartNo"></param>
        /// <param name="companyEndNo"></param>
        /// <param name="storeStartNo"></param>
        /// <param name="storeEndNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operationType"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<string[]> GetSheetValues(int? companyStartNo, int? companyEndNo, int? storeStartNo, int? storeEndNo,
            DateTime startDate,  DateTime endDate, string operationType)
        {
            List<string[]> sheetValues = new List<string[]>();
            //
            bool hasChange = false;
            XmlDocument xmlCurrent = new XmlDocument();
            XmlDocument xmlLast = new XmlDocument();
            //
            System.Data.DataSet ds = this.SelectUserOperations(companyStartNo, companyEndNo, storeStartNo, storeEndNo,
                startDate, endDate, operationType);
            System.Data.DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataTable dtColumns = this.GetColumnsTable(dt.Rows[i]["TableName"] + string.Empty);
                //
                sheetValues.Add(new string[] { "操作用户:", dt.Rows[i]["EnglishName"] + string.Empty, string.Empty, "操作时间:", dt.Rows[i]["UpdateTime"] + string.Empty });
                sheetValues.Add(new string[] { "操作对象:", dt.Rows[i]["DisplayTableName"] + string.Empty, string.Empty, "操作功能:", dt.Rows[i]["OperationName"] + string.Empty });
                if (dt.Rows[i]["LogType"] + string.Empty == "0" || dt.Rows[i]["LogType"] + string.Empty == "2")
                {
                    sheetValues.Add(new string[] { dt.Rows[i]["OperationName"] + "内容如下:", string.Empty, string.Empty, string.Empty, string.Empty });
                    if (dt.Rows[i]["DataInfo"] == DBNull.Value)
                    {
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        continue;
                    }
                    xmlCurrent.LoadXml(dt.Rows[i]["DataInfo"] + string.Empty);
                    foreach (DataRow row in dtColumns.Rows)
                    {
                        if (xmlCurrent.DocumentElement.Attributes[row["ColumnName"].ToString()] == null) continue;
                        //
                        hasChange = true;
                        sheetValues.Add(new string[] { row["SimpleChinese"] + ":", 
                            xmlCurrent.DocumentElement.Attributes[row["ColumnName"].ToString()].Value,
                            string.Empty, 
                            string.Empty, 
                            string.Empty });
                    }
                }
                else
                {
                    // 修改时，显示前后内容对比
                    sheetValues.Add(new string[] { dt.Rows[i]["OperationName"] + "前内容如下:", string.Empty, string.Empty, dt.Rows[i]["OperationName"] + "后内容如下:", string.Empty });
                    if (dt.Rows[i]["DataInfo"] == DBNull.Value || dt.Rows[i]["LastDataInfo"] == DBNull.Value)
                    {
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                        continue;
                    }
                    xmlCurrent.LoadXml(dt.Rows[i]["DataInfo"] + string.Empty);
                    xmlLast.LoadXml(dt.Rows[i]["LastDataInfo"] + string.Empty);
                    hasChange = false;
                    foreach (DataRow row in dtColumns.Rows)
                    {
                        //if (row["AlwayDisplay"] + string.Empty == "0"
                        //    && this.GetAttributeValue(xmlCurrent.DocumentElement, row["ColumnName"].ToString()) ==
                        //        this.GetAttributeValue(xmlLast.DocumentElement, row["ColumnName"].ToString())
                        //    )
                        //{// 修改前后没有变化，不显示
                        //    continue;
                        //}

                        hasChange = true;
                        sheetValues.Add(new string[] { row["SimpleChinese"] + ":", 
                            this.GetAttributeValue(xmlLast.DocumentElement, row["ColumnName"].ToString()),
                            string.Empty, 
                            row["SimpleChinese"] + ":",
                            this.GetAttributeValue(xmlCurrent.DocumentElement, row["ColumnName"].ToString())
                        });
                    }
                }
                if (hasChange)
                {
                    // 每条记录之间增加 2 个空白行
                    sheetValues.Add(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    sheetValues.Add(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                }
                else
                {
                    sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                    sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                    sheetValues.Remove(sheetValues[sheetValues.Count - 1]);
                }
            }
            return sheetValues;
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetAttributeValue(XmlElement ele, string name)
        {
            if (ele.Attributes[name] == null)
            {
                return string.Empty;
            }
            else
            {
                return ele.Attributes[name].Value.Trim();
            }
        }

        #region 已过时

        //private ApplicationClass excelApp;
        //private Workbook excelBook;
        //private Worksheet excelSheet;

        //[Obsolete("该方法为原导出方法，依赖于Office环境，固废除，请采用ExportToXls(List<string[]> sheetValues, string sheetName, string filePath)方法替代")]
        //private bool ExportToExcel(List<string[]> sheetValues, string sheetName)
        //{
        //    if (sheetValues.Count == 0) return false;

        //    try
        //    {
        //        int colCount = sheetValues[0].Length;
        //        int rowCount = sheetValues.Count;
        //        int workSheetIndex = 2;
        //        int startIndex, endIndex;
        //        startIndex = endIndex = 0;
        //        string[,] values;

        //        excelApp = new ApplicationClass();
        //        excelApp.Visible = false;
        //        excelApp.DisplayAlerts = false;
        //        excelApp.AlertBeforeOverwriting = false;
        //        excelApp.AskToUpdateLinks = false;
        //        excelBook = excelApp.Workbooks.Add(true);

        //        while (endIndex < rowCount)
        //        {
        //            startIndex = (workSheetIndex - 2) * SheetRowsCount;
        //            endIndex = Math.Min((workSheetIndex - 1) * SheetRowsCount, rowCount);
        //            if (excelBook.Worksheets.Count < workSheetIndex)
        //            {
        //                ((Worksheet)excelBook.Worksheets[1]).Copy(Nothing, excelBook.Worksheets[workSheetIndex - 1]);
        //            }
        //            excelSheet = (Worksheet)excelBook.Worksheets[workSheetIndex];
        //            excelSheet.Name = sheetName + (workSheetIndex - 1).ToString();

        //            values = new string[endIndex - startIndex, colCount];

        //            for (int i = startIndex, j = endIndex; i < j; i++)
        //            {
        //                for (int k = 0; k < colCount; k++)
        //                {
        //                    values[i - startIndex, k] = sheetValues[i][k];
        //                }
        //            }
        //            excelSheet.get_Range(excelSheet.Cells[2, 1], excelSheet.Cells[endIndex - startIndex + 1, colCount]).Value2 = values;

        //            workSheetIndex++;
        //        }
        //        // 删除第一个 Worksheet;
        //        ((Worksheet)excelBook.Worksheets[1]).Delete();

        //        // 成功时直接打开 Excel
        //        excelApp.Visible = true;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // 出错时才结束 Excel 进程
        //        if (excelBook != null)
        //        {
        //            excelBook.Close(Nothing, Nothing, Nothing);
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
        //            excelBook = null;

        //            excelApp.Application.Quit();
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        //            excelApp = null;

        //            GC.Collect();
        //            GC.WaitForPendingFinalizers();
        //        }

        //        throw ex;
        //    }
        //}
        #endregion
    }
}