using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using MCD.RLPlanning.IServices.ForecastSales;
using MCD.RLPlanning.BLL;


namespace MCD.RLPlanning.Business.ForecastSales
{
    public class SalesBLL : BaseBLL<ISalesService>
    {
        /// <summary>
        /// 连接字符串的格式化字符串
        /// </summary>
        private const string c_ConnectionStringFormat = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;';" +
                            "Data Source={0}";

        /// <summary>
        /// 该excel表有效的最大列数
        /// </summary>
        private const int c_MaxColumnCount = 17;

        #region ColumnName
        private const string c_CompanyCodeColumn = "Company";
        private const string c_StoreNoColumn = "餐厅编号";
        private const string c_StoreNameColumn = "Store";
        private const string c_KioskNameColumn = "Kiosk";
        private const string c_YearColumn = "年度";
        private const string c_MonthColumn = "{0}月";
        #endregion

        /// <summary>
        /// 导入Sales数据
        /// </summary>
        /// <param name="dt"></param>
        public DataTable ImportSales(string xlsPath, bool bPreview, out StringBuilder errMsg, Guid userID, out string warnmsg, out List<string> busyStores, out List<string> busyKiosks)
        {
            busyKiosks = busyStores = null;
            errMsg = new StringBuilder();
            try
            {
                StringBuilder strWarningBuilder = new StringBuilder();
                warnmsg = null;

                //先检查xls的有效性
                string strFormatErr = ValideXlsFileFormat(xlsPath);

                //如果文件格式不是97-03的excel，就直接返回
                if (!String.IsNullOrEmpty(strFormatErr))
                {
                    errMsg.Append(strFormatErr);
                    return null;
                }

                DataTable dt = ConvertXlsToDataTable(xlsPath);

                //validate，如果关键验证不通过，则直接返回，不继续向下进行更进一步的验证

                //验证当前用户是否有对某家店的导入权限，如果没有则移除该行数据
                dt = ValidatePriviledgeAndRemoveInvalidRow(dt, userID, out warnmsg);
                if (null == dt)
                {
                    errMsg.Append("验证实体权限出错");
                    return dt;
                }
                strWarningBuilder.Append(warnmsg);

                //验证实体是否存在，或者有效，如果否则移除该行数据
                dt = ValidateEntityActiveAndRemoveInactiveRows(dt, out warnmsg);
                if (null == dt)
                {
                    errMsg.Append("验证实体有效性出错");
                    return dt;
                }
                strWarningBuilder.Append(warnmsg);

                //验证实体编号与公司编号是否有关联，没关联移除该行数据并提示用户
                dt = ValidateEntityNoAndCompanyCodeAndRemoveInvalidRows(dt, out warnmsg);
                if (null == dt)
                {
                    errMsg.Append("验证实体编号和公司编号是否匹配出错");
                    return dt;
                }
                strWarningBuilder.Append(warnmsg);

                //验证导入的甜品店是否有非独立计算sales的，有的话需要移除，并且提示用户
                dt = ValidateKioskIsSubstractAndRemoveInvalidRow(dt, out warnmsg);
                if (null == dt)
                {
                    errMsg.Append("验证甜品店是否独立结算Sales出错");
                    return dt;
                }
                strWarningBuilder.Append(warnmsg);

                //验证导入的sales数据所在年份是否在开店年份之前，是的话移除，并且提示用户
                dt = ValidateWhetherYearBeforeOpenDateYear(dt, out warnmsg);
                if (null == dt)
                {
                    errMsg.Append("验证sales时间是否在开店时间前出错");
                    return dt;
                }
                strWarningBuilder.Append(warnmsg);
                warnmsg = strWarningBuilder.ToString();

                string retMsg;

                //验证导入是否有重复年份的店
                retMsg = ValidateDuplication(dt);
                if (!String.IsNullOrEmpty(retMsg))
                {
                    errMsg.Append(retMsg);
                    return null;
                }

                //将开店年中，开店月前的sales给剔除掉
                dt = RemoveSalesBeforeOpenMonth(dt);
                if (null == dt)
                {
                    errMsg.Append("剔除开店时间前的无效sales数据时出错");
                    return dt;
                }

                //不验证空输入
                ////验证是否在开店后的导入月中，有空sales存在
                //retMsg = ValidateSalesNullInput(dt);
                //if (null == retMsg)
                //{
                //    errMsg.Append("连接服务器超时，请在Excel表中分批导入Sales数据");
                //    return dt;
                //}
                //if (!String.IsNullOrEmpty(retMsg))
                //{
                //    errMsg.Append(retMsg);
                //    return null;
                //}

                //验证输入是否正确
                foreach (DataRow row in dt.Rows)
                {
                    retMsg = ValidateEachRow(row);
                    if (!String.IsNullOrEmpty(retMsg))
                    {
                        errMsg.Append(retMsg);
                    }
                }

                //输入验证如果有错误，则直接返回
                if (0 != errMsg.Length)
                {
                    return null;
                }

                //非预览则需要导入到数据库中
                if (false == bPreview)
                {
                    byte[] byteRet = null;
                    DataSet errDs = DeSerilize(byteRet = ImportSalesDT(dt));

                    //Table count:1. 普通错误
                    if (errDs.Tables.Count == 1)
                    {
                        foreach (DataRow errItem in errDs.Tables[0].Rows)
                        {
                            errMsg.Append(errItem[0].ToString().Trim() + "\n");
                        } 
                    }
                    //Table count2. 导入的实体中有正在进行导入的实体
                    else if (errDs.Tables.Count == 2 && errDs.Tables[1].Columns.Count == 0)
                    {
                        //获取正在被他人进行导入的StoreNo与KioskNo
                        busyStores = new List<string>();
                        busyKiosks = new List<string>();

                        string strStoreNo, strKioskNo;
                        foreach (DataRow errItem in errDs.Tables[0].Rows)
                        {
                            strStoreNo = errItem[0].ToString();
                            strKioskNo = errItem[1].ToString();

                            //餐厅
                            if (String.IsNullOrEmpty(strKioskNo))
                            {
                                busyStores.Add(strStoreNo);
                            }

                            //甜品店
                            else
                            {
                                busyKiosks.Add(strKioskNo);
                            }
                        } 
                    }


                    if (null == byteRet)
                    {
                        errMsg.Append("导入Sales存储过程返回值为NULL");
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                warnmsg = null;
                //The remote server returned an error: (400) Bad Request
                //提示用户上传文件数据量过大
                //if ((e.Message.Contains("400") && e.Message.Contains("server")) ||
                //    (e.Message.Contains("deserialize") && e.Message.Contains("exceeded")))
                //{
                //    errMsg.Append("Excel内数据量过大，请分批上传");
                //}
                //else
                //{
                    errMsg.Append(e.Message);
                //}

                return null;
            }
        }

        private byte[] ImportSalesDT(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return WCFService.ImportSales(byteTable);
        }

        /// <summary>
        /// 验证导入的公司是否有权限
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="UserID"></param>
        /// <returns>返回2张表，第一张是没权限的项，第二张是有权限的合法项</returns>
        public DataSet ValidateImportCompany(DataTable dt, Guid UserID)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.ValidateImportCompany(byteTable, UserID));
        }

        /// <summary>
        /// 验证Kiosk是否独立结算租金，将非独立结算sales的kiosk给剔除，并且返回名称
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet ValidateKiosk(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.ValidateKiosk(byteTable));
        }

        /// 验证导入的Sales所在年是否在开店年前，如果是剔除
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet ValidateRentStartDate(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.ValidateRentStartDate(byteTable));
        }

        /// <summary>
        /// 验证实体是否存在或者有效
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet ValidateEntityExistence(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.ValidateEntityExistence(byteTable));
        }

        /// <summary>
        /// 验证公司编号与实体编号是否有关联
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet ValidateCompanyCodeAndStoreNo(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.ValidateCompanyCodeAndStoreNo(byteTable));
        }

        /// <summary>
        /// 更新Sales数据
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateSales(DataTable dt, out StringBuilder errMsg, out List<string> busyStores, out List<string> busyKiosks)
        {
            errMsg = new StringBuilder();
            busyStores = busyKiosks = null;
            //validate
            foreach (DataRow row in dt.Rows)
            {
                string retMsg = ValidateEachRow(row);
                if (null != retMsg)
                {
                    errMsg.Append(retMsg);
                }
            }

            //如果之前的客户端验证有错误，则不调用服务器的存储过程
            if (0 != errMsg.Length)
            {
                return;
            }

            //导入到数据库中
            byte[] byteRet = null;
            DataSet errDs = DeSerilize(byteRet = ImportSalesDT(dt));

            //Table count:1. 普通错误
            if (errDs.Tables.Count == 1)
            {
                foreach (DataRow errItem in errDs.Tables[0].Rows)
                {
                    errMsg.Append(errItem[0].ToString().Trim() + "\n");
                }
            }
            //Table count2. 导入的实体中有正在进行导入的实体
            else if (errDs.Tables.Count == 2 && errDs.Tables[1].Columns.Count == 0)
            {
                //获取正在被他人进行导入的StoreNo与KioskNo
                busyStores = new List<string>();
                busyKiosks = new List<string>();

                string strStoreNo, strKioskNo;
                foreach (DataRow errItem in errDs.Tables[0].Rows)
                {
                    strStoreNo = errItem[0].ToString();
                    strKioskNo = errItem[1].ToString();

                    //餐厅
                    if (String.IsNullOrEmpty(strKioskNo))
                    {
                        busyStores.Add(strStoreNo);
                    }

                    //甜品店
                    else
                    {
                        busyKiosks.Add(strKioskNo);
                    }
                }
            }


            if (null == byteRet)
            {
                errMsg.Append("导入Sales存储过程返回值为NULL");
            }
        }

        /// <summary>
        /// 根据storeNo和kioskNo来查询Sales数据
        /// </summary>
        /// <param name="storeNo"></param>
        /// <param name="kioskNo"></param>
        /// <returns></returns>
        public DataSet SelectSales(string storeNo, string kioskNo)
        {
            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.SelectSales(storeNo, kioskNo));
        }

        /// <summary>
        /// 根据类型、餐厅名称或编号返回餐厅、甜品店信息
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strStoreNoOrName"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageCount"></param>
        /// <returns></returns>
        public DataSet SelectStoreOrKiosk(string strType, string strStoreNoOrName, string strCompanyCode, string strStatus, 
                string strUserID, string strAreaID, int iPageIndex, int iPageSize, out int iRecordCount)
        {
            iRecordCount = 0;

            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.SelectStoreOrKiosk(strType, strStoreNoOrName, strCompanyCode, strStatus,
                    strUserID, strAreaID, iPageIndex, iPageSize, out iRecordCount));
        }


        /// <summary>
        /// 返回Excel模板的Dataset
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public DataSet SelectImportSalesTemplate(string strUserID)
        {
            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.SelectImportSalesTemplate(strUserID));
        }

                /// <summary>
        /// 返回当前导入的数据的最小合同时间或者开店时间
        /// </summary>
        /// <returns></returns>
        public DataSet GetMinRentStartDateOrOpenningDate(DataTable dt)
        {
            if (null == WCFService)
                return null;

            byte[] byteTable = DataTable2Byte(dt);
            if (null == byteTable)
                return null;

            return DeSerilize(WCFService.GetMinRentStartDateOrOpenningDate(byteTable));
        }

        /// <summary>
        /// 校验Excel格式
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <returns></returns>
        public string CheckExcelFormat(string xlsPath)
        {
            List<string> errorSheets = new List<string>();
            string connStr = string.Format(c_ConnectionStringFormat, xlsPath);
            //
            List<string> sheetNames = GetExcelSheetNames(xlsPath);
            foreach (string sheetName in sheetNames)
            {
                string sql = "SELECT * FROM [" + sheetName + "]";
                DataSet ds = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
                {
                    da.FillSchema(ds, SchemaType.Mapped);// 填充DataSet
                }
                if (ds.Tables.Count > 0)
                {
                    int columnCount = ds.Tables[0].Columns.Count;
                    if (columnCount != c_MaxColumnCount)
                    {
                        errorSheets.Add(sheetName);
                    }
                }
            }
            return string.Join(",", errorSheets.ToArray());
        }

        private DataTable ConvertXlsToDataTable(string szXlsPath)
        {
            string connStr = string.Format(c_ConnectionStringFormat, szXlsPath);
            DataTable dtExcel = null;
            List<DataTable> dtSheets = new List<DataTable>();
            //获取Sheet name
            List<string> sheetNames = GetExcelSheetNames(szXlsPath);

            foreach (var sheetName in sheetNames)
            {
                string sql = "select * from [" + sheetName + "A1:AQ]";
                DataSet ds = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
                {
                    da.Fill(ds);    // 填充DataSet
                }

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    //移除空白行
                    RemoveEmptyRows(dt);
                    //移除12个月都没sales的行
                    RemoveNoSalesRows(dt);
                    //将数字转为字符串
                    dt = ChangeToTextTable(dt);
                    dtSheets.Add(dt);
                }
            }

            dtExcel = MarginTable(dtSheets);
            if (null != dtExcel)
            {
                dtExcel.TableName = "Import_Sales";
            }
            return dtExcel;
        }

        /// <summary>
        /// 获取Excel中的SheetName
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <returns></returns>
        private List<string> GetExcelSheetNames(string xlsPath)
        {
            List<string> sheetNames = new List<string>();
            //
            try
            {
                if (System.IO.File.Exists(xlsPath))
                {
                    string connStr = string.Format(c_ConnectionStringFormat, xlsPath);
                    OleDbConnection con = new OleDbConnection(connStr);
                    con.Open();
                    System.Data.DataTable table = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);//new Object[] { null, null, null, "TABLE" });
                    con.Close();
                    //
                    foreach (DataRow row in table.Rows)
                    {
                        string sheetName = row["TABLE_NAME"].ToString();
                        //真正Sheet名称应该是以$结尾
                        if (sheetName.EndsWith("$"))
                        {
                            sheetNames.Add(sheetName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sheetNames;
        }

        /// <summary>
        /// 将单元格全部转换为字符型
        /// </summary>
        /// <param name="dtExcel"></param>
        /// <returns></returns>
        private DataTable ChangeToTextTable(System.Data.DataTable dtExcel)
        {
            System.Data.DataTable resultTable = new System.Data.DataTable();
            resultTable.TableName = dtExcel.TableName;
            foreach (DataColumn item in dtExcel.Columns)
            {
                resultTable.Columns.Add(item.ColumnName, typeof(string));
            }
            //
            foreach (DataRow row in dtExcel.Rows)
            {
                List<string> values = new List<string>();
                foreach (var item in row.ItemArray)
                {
                    if (item == null || item == DBNull.Value || item.ToString() == string.Empty)
                    {
                        values.Add(null);
                    }
                    else
                    {
                        values.Add(item.ToString());
                    }
                }
                resultTable.Rows.Add(values.ToArray());
            }
            return resultTable;
        }

        /// <summary>
        /// 移除空白行
        /// </summary>
        /// <param name="dt"></param>
        private void RemoveEmptyRows(DataTable dt)
        {
            int columnCount = dt.Columns.Count;
            //删除空白行
            for (int index = dt.Rows.Count - 1; index >= 0; index--)
            {
                if (IsEmptyRow(dt.Rows[index], columnCount))
                {
                    dt.Rows.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// 移除12个月没有sales的行数据
        /// </summary>
        /// <param name="dt"></param>
        private void RemoveNoSalesRows(DataTable dt)
        {
            List<DataRow> needtodellst = new List<DataRow>();
            bool bNosales = true;
            string strData = string.Empty;
            foreach (DataRow item in dt.Rows)
            {
                bNosales = true;

                //12个月里 只要有一年有Sales数据，就不能把这行删除
                for (int i = 1; i < 13; ++i)
                {
                    strData = item[string.Format(c_MonthColumn, i).ToString()] + string.Empty;
                    if (!String.IsNullOrEmpty(strData))
                    {
                        bNosales = false;
                        break;
                    }
                }

                if (bNosales)
                {
                    needtodellst.Add(item);
                }
            }

            //开始将记录下来的空行数据删除
            foreach (var item in needtodellst)
            {
                dt.Rows.Remove(item);
            }
        }

        /// <summary>
        /// 判断是否是空白行
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        private bool IsEmptyRow(DataRow row, int columnCount)
        {
            bool result = true;
            for (int i = 1; i < columnCount; i++)
            {
                if ((row[i] + string.Empty) != string.Empty)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 合并DataTable
        /// </summary>
        /// <param name="tableList"></param>
        /// <returns></returns>
        private DataTable MarginTable(List<System.Data.DataTable> tableList)
        {
            System.Data.DataTable dtCheckResult = tableList.Count > 0 ? tableList[0].Clone() : null;
            //
            if (tableList.Count > 0)
            {
                foreach (System.Data.DataTable dtTemp in tableList)
                {
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        dtCheckResult.ImportRow(row);
                    }
                }
            }
            return dtCheckResult;
        }

        #region 验证错误信息
        private const string c_CompanyCodeErr = "餐厅名为：{0} 的餐厅编号不能为空\n";
        private const string c_YearErr = "餐厅编号为：{0} 的年度数据不能非正整数\n";
        private const string c_MonthSalesErr = "餐厅编号为：{0} {1} 年的 {2} 月份Sales数据必须为数字\n";
        private const string c_MonthSalesNullErr = "餐厅编号为：{0} {1} 年的 {2} 月份的Sales数据不能为空\n";
        private const string c_MonthSalesNegativeErr = "餐厅编号为：{0} {1} 年的 {2} 月份的Sales数据不能为负数\n";
        private const string c_MonthSalesLengthErr = "餐厅编号为：{0} {1} 年的 {2} 月份的Sales数据长度超8位\n";
        private const string c_FileFormatErr = "导入文件必须是*.xls文件\n";
        private const string c_DuplicationErr = "{0}年 StoreNo:{1} Store:{2} Kiosk:{3} 的导入数据重复！\n";
        private const string c_DuplicationNoKioskErr = "{0}年 StoreNo:{1} Store:{2} 的导入数据重复！\n";
        #endregion

        /// <summary>
        /// 验证文件的有效性
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private string ValideXlsFileFormat(string strPath)
        {
            string strRet = null;
            try
            {
                if (System.IO.File.Exists(strPath))
                {
                    string connStr = string.Format(c_ConnectionStringFormat, strPath);
                    OleDbConnection con = new OleDbConnection(connStr);
                    con.Open();
                    con.Close();
                }
            }
            catch
            {
                strRet = c_FileFormatErr;

                //for debug
                //strRet = e.Message + "\n";
                //strRet += e.Source + "\n";
                //strRet += e.StackTrace;
            }
            return strRet;
        }


        /// <summary>
        /// 验证从Excel转换过来的DataTable里的内容是否正确
        /// </summary>
        /// <param name="row"></param>
        /// <param name="bValidateNullSales">true表面需要验证0sales</param>
        /// <returns></returns>
        public string ValidateEachRow(DataRow row, bool bValidateNullSales = false)
        {
            //餐厅编号不能为空
            if (String.IsNullOrEmpty(row[c_StoreNoColumn].ToString()))
            {
                return string.Format(c_CompanyCodeErr, row[c_StoreNameColumn]);
            }

            //年不能为空或者为非正整数
            if (String.IsNullOrEmpty(row[c_YearColumn].ToString()))
            {
                return string.Format(c_YearErr, row[c_StoreNoColumn]);
            }

            int iYear;
            try
            {
                iYear = Int32.Parse(row[c_YearColumn].ToString());
                if (iYear <= 0)
                {
                    return string.Format(c_YearErr, row[c_StoreNoColumn]);
                }
            }
            catch
            {
                return string.Format(c_YearErr, row[c_StoreNoColumn]);
            }

            //月Sales不能为非正数
            for (int i = 1; i < 13; ++i)
            {
                //无数值的跳过
                if (String.IsNullOrEmpty(row[string.Format(c_MonthColumn, i)].ToString()))
                {
                    //如果需要提示空sales错误，则返回错误信息
                    if (bValidateNullSales)
                        return string.Format(c_MonthSalesNullErr, row[c_StoreNoColumn], iYear, i);

                    continue;
                }

                try
                {
                    decimal fSales = Decimal.Parse(row[string.Format(c_MonthColumn, i)].ToString());
                    //负数错误
                    if (fSales < 0)
                    {
                        return string.Format(c_MonthSalesNegativeErr, row[c_StoreNoColumn], iYear, i);
                    }
                    //长度错误 
                    if (fSales / (decimal)10000000.0f >= (decimal)10.0f)
                    {
                        return string.Format(c_MonthSalesLengthErr, row[c_StoreNoColumn], iYear, i);
                    }
                }
                catch
                {
                    return string.Format(c_MonthSalesErr, row[c_StoreNoColumn], iYear, i);
                }
            }

            return null;
        }

        /// <summary>
        /// 验证当前导入年份是否有空sales存在，开店前允许有空sales存在
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ValidateSalesNullInput(DataTable dt)
        {
            DataSet ds = GetMinRentStartDateOrOpenningDate(dt);

            //返回null表明连接超时
            if (0 == ds.Tables.Count)
                return null;

            StringBuilder strErrBuilder = new StringBuilder();

            DateTime? minTime = null;
            string strMinTime = null, strStoreNo = null, strKioskName = null;
            string strSales = null;
            foreach (DataRow item in dt.Rows)
            {
                strStoreNo = item[c_StoreNoColumn].ToString();
                strKioskName = item[c_KioskNameColumn].ToString();

                //餐厅
                if (String.IsNullOrEmpty(strKioskName))
                {
                    strMinTime = ds.Tables[0].Select(
                                                string.Format("StoreNo='{0}' AND KioskName IS NULL", strStoreNo)
                                            )[0]["MinDate"].ToString();
                }
                //甜品店
                else
                {
                    strMinTime = ds.Tables[0].Select(
                                               string.Format("KioskName='{0}'", strKioskName)
                                           )[0]["MinDate"].ToString();
                }

                //转换时间类型
                try
                {
                    minTime = DateTime.Parse(strMinTime);
                }
                catch
                {
                    minTime = null;
                }
                
                //获取年份
                int iYear = -1;
                try
                {
                    iYear = int.Parse(item[c_YearColumn].ToString());
                }
                catch { ; }
                

                for (int i = 1; i < 13; ++i)
                {
                    strSales = item[string.Format(c_MonthColumn, i)].ToString();

                    if (String.IsNullOrEmpty(strSales))
                    {
                        //如果时间是处于合理最小时间前，则不填写sales数据不算错
                        if (minTime.HasValue && -1 != iYear)
                        {
                            //处在合理最小时间年的，并且小于该年的最小月前的sales数据可以不用填
                            if (minTime.Value.Year == iYear && i < minTime.Value.Month)
                                continue;

                            //小于合理最小年的情况属于导入了开店年前的数据，是非法的，前面的步骤就会剔除

                            //其它时间段需要报错
                            strErrBuilder.Append(string.Format(c_MonthSalesNullErr, item[c_StoreNoColumn], iYear, i).ToString());
                        }
                    }
                }

                minTime = null;
                strMinTime = null;
                strStoreNo = null;
                strKioskName = null;
            }

            //如果有错误信息则直接返回
            if (strErrBuilder.Length != 0)
                return strErrBuilder.ToString();

            //StringEmpty表面没有错误，也没有连接超时
            return string.Empty;
        }

        /// <summary>
        /// 在开店年份中，将开店月前的已填sales数据置空
        /// </summary>
        private DataTable RemoveSalesBeforeOpenMonth(DataTable dt)
        {
            DataSet ds = GetMinRentStartDateOrOpenningDate(dt);

            //返回null表明连接超时
            if (0 == ds.Tables.Count)
                return null;

            DateTime? minTime = null;
            string strMinTime = null, strStoreNo = null, strKioskName = null;
            string strSales = null;
            foreach (DataRow item in dt.Rows)
            {
                strStoreNo = item[c_StoreNoColumn].ToString();
                strKioskName = item[c_KioskNameColumn].ToString();

                //餐厅
                if (String.IsNullOrEmpty(strKioskName))
                {
                    try
                    {
                        strMinTime = ds.Tables[0].Select(
                                                string.Format("StoreNo='{0}' AND KioskName IS NULL", strStoreNo)
                                            )[0]["MinDate"].ToString();
                    }
                    catch
                    {
                        strMinTime = string.Empty;
                    }
                }
                //甜品店
                else
                {
                    try
                    {
                        strMinTime = ds.Tables[0].Select(
                                               string.Format("KioskName='{0}'", strKioskName)
                                           )[0]["MinDate"].ToString();
                    }
                    catch
                    {
                        strMinTime = string.Empty;
                    }
                }

                //转换时间类型
                try
                {
                    minTime = DateTime.Parse(strMinTime);
                }
                catch
                {
                    minTime = null;
                }

                //获取年份
                int iYear = -1;
                try
                {
                    iYear = int.Parse(item[c_YearColumn].ToString());
                }
                catch { ; }


                for (int i = 1; i < 13; ++i)
                {
                    strSales = item[string.Format(c_MonthColumn, i)].ToString();

                    if (!String.IsNullOrEmpty(strSales))
                    {
                        //如果时间是处于合理最小时间前，则将已经填写的sales数据置空
                        if (minTime.HasValue && -1 != iYear)
                        {
                            //处在合理最小时间年的，并且小于该年的最小月前的sales数据可以不用填
                            if (minTime.Value.Year == iYear && i < minTime.Value.Month)
                                item[string.Format(c_MonthColumn, i)] = null;
                        }
                    }
                }

                minTime = null;
                strMinTime = null;
                strStoreNo = null;
                strKioskName = null;
            }

            return dt;
        }

        private const string c_NoPriviledgeWarning = "您没有导入公司编号：{0} 下的餐厅sales的权限\n";

        /// <summary>
        /// 验证用户对该店是否有权限，如果没有则移除该行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable ValidatePriviledgeAndRemoveInvalidRow(DataTable dt, Guid userID, out string warningmsg)
        {
            warningmsg = null; //warning信息提示用户没有权限导入的sales数据

            DataSet ds = ValidateImportCompany(dt, userID);

            //没有返回值说明连接服务器超时
            if (null == ds || ds.Tables.Count < 2)
                return null;

            DataTable invalidCompanyTbl = ds.Tables[0]; 
            DataTable validCompanyTbl = ds.Tables[1];

            //如果有无效公司，则产生warning信息
            if (invalidCompanyTbl.Rows.Count > 0)
            {
                StringBuilder strBuilder = new StringBuilder();
                foreach(DataRow item in invalidCompanyTbl.Rows)
                {
                    strBuilder.Append(string.Format(c_NoPriviledgeWarning, item[c_CompanyCodeColumn].ToString()));
                }

                warningmsg = strBuilder.ToString();
            }

            return validCompanyTbl;
        }

        private const string c_KioskNotSubstract = "甜品店: {0} 不是独立结算Sales，无法导入Sales数据\n";
        /// <summary>
        /// 如果甜品店不是独立计算sales的，则需要
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="userID"></param>
        /// <param name="warningmsg"></param>
        /// <returns></returns>
        private DataTable ValidateKioskIsSubstractAndRemoveInvalidRow(DataTable dt, out string warningmsg)
        {
            warningmsg = null;
            StringBuilder warnBuilder = new StringBuilder();
            DataTable retTb = dt;
            DataSet ds = ValidateKiosk(dt);

            //0表示超时
            if (0 == ds.Tables.Count)
                return null;

            //将剔除后的数据返回
            if (ds.Tables.Count == 2)
            {
                retTb = ds.Tables[0];
            }

            //将有没有独立结算Sales的KioskName返回出来
            if (ds.Tables.Count == 2)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    warnBuilder.Append(string.Format(c_KioskNotSubstract, item[0].ToString()));
                }
            }

            if (warnBuilder.Length > 0)
                warningmsg = warnBuilder.ToString();

            return retTb;
        }

        private const string c_SalesBeforeOpenDate = "餐厅: {0} {1}年 在开业之前无需导入该年的Sales数据\n";
        private const string c_SalesBeforeOpenDateWithKiosk = "餐厅: {0} 甜品店: {1} {2}年 在开业之前无需导入该年的Sales数据\n";
        /// <summary>
        /// 验证导入sales数据 是否在开店年前
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="warnmsg"></param>
        /// <returns></returns>
        private DataTable ValidateWhetherYearBeforeOpenDateYear(DataTable dt, out string warnmsg)
        {
            DataTable rettb = dt;
            warnmsg = null;
            StringBuilder warnBuilder = new StringBuilder();

            DataSet ds = ValidateRentStartDate(dt);

            //0表示超时
            if (0 == ds.Tables.Count)
                return null;

            //将剔除后的数据返回
            if (ds.Tables.Count == 2)
            {
                rettb = ds.Tables[0];
            }

            //将有没有独立结算Sales的KioskName返回出来
            if (ds.Tables.Count == 2)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    string strStoreName, strKioskName, strYear;
                    strStoreName = item["StoreName"].ToString();
                    strKioskName = item["KioskName"].ToString();
                    strYear = item["Year"].ToString();
                    
                    //餐厅
                    if (String.IsNullOrEmpty(strKioskName))
                    {
                        warnBuilder.Append(string.Format(c_SalesBeforeOpenDate, strStoreName, strYear));
                    }
                    //甜品店
                    else
                    {
                        warnBuilder.Append(string.Format(c_SalesBeforeOpenDateWithKiosk, strStoreName, strKioskName, strYear));
                    }
                }
            }

            if (warnBuilder.Length > 0)
                warnmsg = warnBuilder.ToString();

            return rettb;
        }

        private const string c_EntityNotExist = "餐厅编号:{0} 餐厅名称:{1} 甜品店名称:{2} 的实体不存在或者状态为\"I\"\n";
        /// <summary>
        /// 验证实体是否存在或者有效
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable ValidateEntityActiveAndRemoveInactiveRows(DataTable dt, out string warnmsg)
        {
            DataTable rettb = dt;
            warnmsg = null;
            StringBuilder warnBuilder = new StringBuilder();

            DataSet ds = ValidateEntityExistence(dt);

            //0表示超时
            if (0 == ds.Tables.Count)
                return null;

            //将剔除后的数据返回
            if (ds.Tables.Count == 2)
            {
                rettb = ds.Tables[0];
            }

            //将有没有独立结算Sales的KioskName返回出来
            if (ds.Tables.Count == 2)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    string strStoreName, strKioskName, strStoreNo;
                    strStoreNo = item["StoreNo"].ToString();
                    strStoreName = item["StoreName"].ToString();
                    strKioskName = item["KioskName"].ToString();

                    warnBuilder.Append(string.Format(c_EntityNotExist, strStoreNo, strStoreName, strKioskName));
                }
            }

            if (warnBuilder.Length > 0)
                warnmsg = warnBuilder.ToString();
            return rettb;
        }

        private const string c_CompanyCodeAndStoreNoInvalid = "餐厅编号:{0} 的实体与公司: {1} 没有关联\n";
        /// <summary>
        /// 验证实体编号与公司编号是否有关联
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="warnmsg"></param>
        /// <returns></returns>
        private DataTable ValidateEntityNoAndCompanyCodeAndRemoveInvalidRows(DataTable dt, out string warnmsg)
        {
            DataTable rettb = dt;
            warnmsg = null;
            StringBuilder warnBuilder = new StringBuilder();

            DataSet ds = ValidateCompanyCodeAndStoreNo(dt);

            //0表示超时
            if (0 == ds.Tables.Count)
                return null;

            //将剔除后的数据返回
            if (ds.Tables.Count == 2)
            {
                rettb = ds.Tables[0];
            }

            //将有没有关联的公司编号与实体编号返回出来
            if (ds.Tables.Count == 2)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    string strCompanyCode, strStoreNo;
                    strCompanyCode = item["CompanyCode"].ToString();
                    strStoreNo = item["StoreNo"].ToString();

                    warnBuilder.Append(string.Format(c_CompanyCodeAndStoreNoInvalid, strStoreNo, strCompanyCode));
                }
            }

            if (warnBuilder.Length > 0)
                warnmsg = warnBuilder.ToString();
            return rettb;
        }
        /// <summary>
        /// 验证是否有重复店和年份的sales导入数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ValidateDuplication(DataTable dt)
        {
            HashSet<SDuplicatedItem> dupItemsSet = new HashSet<SDuplicatedItem>(); //用来存放重复项的信息
            SDuplicatedItem dpItem;
            for (int index = 0; index < dt.Rows.Count; ++index)
            {
                dpItem = new SDuplicatedItem() 
                {
                    StoreNo = dt.Rows[index][c_StoreNoColumn].ToString(),
                    StoreName = dt.Rows[index][c_StoreNameColumn].ToString(),
                    KioskName = dt.Rows[index][c_KioskNameColumn].ToString(),
                    Year = dt.Rows[index][c_YearColumn].ToString()
                };

                //如果当前记录已经被记录了，则不需要再做下面的操作--Begin
                bool bExists = false;
                foreach (var it in dupItemsSet)
                {
                    if (it.KioskName == dpItem.KioskName &&
                        it.StoreName == dpItem.StoreName &&
                        it.Year == dpItem.Year &&
                        it.StoreNo == dpItem.StoreNo)
                    {
                        bExists = true;
                        break;
                    }
                }
                if (bExists)
                    continue;
                //如果当前记录已经被记录了，则不需要再做下面的操作--End

                //检查是否重复
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    //将当前比较的index跳过
                    if (i == index)
                        continue;

                    if (
                        dpItem.StoreNo == dt.Rows[i][c_StoreNoColumn].ToString() &&
                        dpItem.StoreName == dt.Rows[i][c_StoreNameColumn].ToString() &&
                        dpItem.KioskName == dt.Rows[i][c_KioskNameColumn].ToString() &&
                        dpItem.Year == dt.Rows[i][c_YearColumn].ToString()
                    )
                    {
                        dupItemsSet.Add(dpItem);
                        break;
                    }
                }
            }

            //如果有重复记录，则需要将错误信息返回给用户
            if (dupItemsSet.Count > 0)
            {
                StringBuilder sbErr = new StringBuilder();
                foreach (var item in dupItemsSet)
                {
                    if (String.IsNullOrEmpty(item.KioskName))
                    {
                        sbErr.Append(
                            string.Format(c_DuplicationNoKioskErr, item.Year, item.StoreNo,
                                            item.StoreName));
                    }
                    else
                    {
                        sbErr.Append(
                            string.Format(c_DuplicationErr, item.Year, item.StoreNo,
                                            item.StoreName, item.KioskName));
                    }
                }

                return sbErr.ToString();
            }

            return null;
        }

        /// <summary>
        /// 将DataTable压缩成byte
        /// </summary>
        /// <returns></returns>
        private byte[] DataTable2Byte(DataTable dt)
        {
            if (null == dt)
                return null;

            //数据复制
            DataTable newTable = dt.Clone();
            foreach (DataRow item in dt.Rows)
            {
                newTable.BeginInit();
                newTable.ImportRow(item);
                newTable.EndInit();
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(newTable);
            return Serilize(ds);
        }

        #region 供数据重复错误检查用
        private struct SDuplicatedItem
        {
            public string StoreNo;
            public string StoreName;
            public string KioskName;
            public string Year;
        }
        #endregion
    }
}