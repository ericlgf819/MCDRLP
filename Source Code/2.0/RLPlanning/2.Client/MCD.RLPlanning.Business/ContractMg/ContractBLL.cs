using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using Microsoft.Office.Interop.Excel;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.IServices;
using MCD.RLPlanning.IServices.ContractMg;

namespace MCD.RLPlanning.BLL.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractBLL : BaseBLL<IContractService>
    {
        //Fields
        private static List<CycleItemEntity> s_FixedCycleItems = null;
        private static List<CycleItemEntity> s_RatioCycleItems = null;

        /// <summary>
        /// 获取结算周期
        /// </summary>
        /// <param name="cycleType">固定/百分比</param>
        /// <returns></returns>
        public List<CycleItemEntity> GetCycleItems(CycleType cycleType)
        {
            List<CycleItemEntity> result = null;
            //
            switch (cycleType)
            {
                case CycleType.固定:
                    if (ContractBLL.s_FixedCycleItems == null)
                    {
                        DataSet dsFixed = base.DeSerilize(base.WCFService.GetCycleItems(CycleType.固定.ToString()));
                        if (dsFixed != null && dsFixed.Tables.Count > 0)
                        {
                            ContractBLL.s_FixedCycleItems = ReflectHelper.ConvertDataTableToEntityList<CycleItemEntity>(dsFixed.Tables[0]);
                        }
                    }
                    result = ContractBLL.s_FixedCycleItems;
                    break;
                case CycleType.百分比:
                    if (ContractBLL.s_RatioCycleItems == null)
                    {
                        DataSet dsRatio = base.DeSerilize(base.WCFService.GetCycleItems(CycleType.百分比.ToString()));
                        if (dsRatio != null && dsRatio.Tables.Count > 0)
                        {
                            ContractBLL.s_RatioCycleItems = ReflectHelper.ConvertDataTableToEntityList<CycleItemEntity>(dsRatio.Tables[0]);
                        }
                    }
                    result = ContractBLL.s_RatioCycleItems;
                    break;
                default:
                    break;
            }
            return result;
        }

        #region 合同相关方法

        /// <summary>
        /// 手工发起APGL
        /// </summary>
        public void CreateAPGLByHand()
        {
            base.WCFService.CreateAPGLByHand();
        }

        /// <summary>
        /// 根据条件查找合同信息
        /// </summary>
        /// <param name="storeOrDeptNo"></param>
        /// <param name="vendorNo"></param>
        /// <param name="companyNo"></param>
        /// <param name="contractNo"></param>
        /// <param name="area"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataSet SelectContracts(string storeOrDeptNo, string vendorNo, string companyNo, string contractNo, Guid area, string status, bool? bFromSRLS, Guid UserID, int pageIndex, int pageSize, out int recordCount)
        {
            return base.DeSerilize(base.WCFService.SelectContracts(storeOrDeptNo, vendorNo, companyNo, contractNo, area, status, bFromSRLS, UserID, pageIndex, pageSize, out recordCount));
        }

        /// <summary>
        /// 获取合同历史版本
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public DataSet SelectContractHistory(string contractID)
        {
            return base.DeSerilize(base.WCFService.SelectContractHistory(contractID));
        }

        /// <summary>
        /// 获取单个合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ContractEntity GetSingleContract(string id)
        {
            ContractEntity entity = new ContractEntity() { ContractSnapshotID = id };
            //
            return base.WCFService.SelectSingleContract(entity);
        }

        /// <summary>
        /// 更新单个实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateSingleContract(ContractEntity entity)
        {
            List<object> res = base.WCFService.UpdateSingleContract(entity);
            if (res == null || res.Count == 0) return 0;
            //
            return Convert.ToInt32(res[0] + string.Empty);
        }

        /// <summary>
        /// 写入单个合同信息
        /// 0表示失败，1表示成功
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertSingleContract(ContractEntity entity)
        {
            List<object> res = base.WCFService.InsertSingleContract(entity);
            if (res == null || res.Count == 0) return 2;
            //
            return Convert.ToInt16(res[0] + string.Empty);
        }

        /// <summary>
        /// 删除合同信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSingleContract(ContractEntity entity)
        {
            return base.WCFService.DeleteSingleContract(entity);
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="currentUserID"></param>
        public void DeleteContract(string contractSnapshotID, string currentUserID)
        {
            base.WCFService.DeleteContract(contractSnapshotID, currentUserID);
        }

        /// <summary>
        /// 根据合同快照ID找出所关联的所有租金规则信息
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns>0:Contract,1:VendorContract,2:Entity,3:VendorEntity,4:EntityInfoSetting,
        /// 5:FixedRuleSetting,6:FixedTimeIntervalSetting
        /// 7:RatioRuleSetting,8:RatioCycleSetting,9:RatioTimeIntervalSetting,10:ConditionAmount</returns>
        public DataSet SelectAllRentRuleInfosByContractSnapshotID(string contractSnapshotID)
        {
            return base.DeSerilize(base.WCFService.SelectAllRentRuleInfosByContractSnapshotID(contractSnapshotID));
        }

        /// <summary>
        /// 获取租金规则相关信息类
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        public RentRuleAllInfo GetRentRuleInfoEntity(string contractSnapshotID)
        {
            return new RentRuleAllInfo(this.SelectAllRentRuleInfosByContractSnapshotID(contractSnapshotID), contractSnapshotID, this);
        }

        /// <summary>
        /// 新增合同但未保存时执行的操作
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <param name="copyType"></param>
        public void UndoContract(string contractSnapshotID, ContractCopyType copyType)
        {
            base.WCFService.UndoContract(contractSnapshotID, copyType);
        }

        /// <summary>
        /// 复制合同，用于变更和续租合同
        /// </summary>
        /// <param name="contractSnapshotID">要复制的合同</param>
        /// <param name="operatorID">操作人ID</param>
        /// <param name="copyType">复制类型，变更/续租</param>
        /// <returns>新合同快照ID</returns>
        public string CopyContract(string contractSnapshotID, string operatorID, ContractCopyType copyType)
        {
            return base.WCFService.CopyContract(contractSnapshotID, operatorID, copyType.ToString());
        }

        /// <summary>
        /// 校验合同信息
        /// </summary>
        /// <param name="contractSnapshotID">合同快照ID</param>
        /// <returns>IndexId int,Code NVARCHAR(50), RelationData NVARCHAR(50),CheckMessage NVARCHAR(500)</returns>
        public System.Data.DataTable CheckContract(string contractSnapshotID)
        {
            DataSet ds = base.DeSerilize(base.WCFService.CheckContract(contractSnapshotID));
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 实体是否已经出了AP
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool IsEntityHasAP(string entityID)
        {
            return base.WCFService.IsEntityHasAP(entityID);
        }
        #endregion

        #region 导入合同

        private const string c_Column_LineNumber = "LineNumber";//新增的行号列的列名
        private const string c_Column_ContractIndex = "合同序号";//合同序号列

        /// <summary>
        /// 连接字符串的格式化字符串
        /// </summary>
        private const string c_ConnectionStringFormat = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;';" +
                            "Data Source={0}";
        /// <summary>
        /// Excel模板的列数，34是实际的数据列数，39为包含列表字段的列数
        /// </summary>
        private const int c_ColumnCount1 = 34, c_ColumnCount2 = 39;

        /// <summary>
        /// 校验Excel格式
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <returns></returns>
        public string CheckExcelFormat(string xlsPath)
        {
            List<string> errorSheets = new List<string>();
            string connStr = string.Format(ContractBLL.c_ConnectionStringFormat, xlsPath);
            //
            List<string> sheetNames = this.GetExcelSheetNames(xlsPath);
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
                    if (columnCount != c_ColumnCount1 && columnCount != c_ColumnCount2)
                    {
                        errorSheets.Add(sheetName);
                    }
                }
            }
            return string.Join(",", errorSheets.ToArray());
        }

        /// <summary>
        /// 导入合同
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <param name="currentUserID"></param>
        /// <param name="recordCount"></param>
        /// <param name="successCount"></param>
        /// <returns></returns>
        public System.Data.DataTable ImportContracts(string xlsPath, string importType, string currentUserID,
            out int totalCount, out int successCount, out int failCount)
        {
            totalCount = successCount = failCount = 0;
            System.Data.DataTable dtResult = null;
            //
            try
            {
                System.Data.DataTable dtExcel = this.ConvertExcelToDataTable(xlsPath);
                if (dtExcel != null && dtExcel.Rows.Count > 0)
                {
                    DataSet dsImportResult = base.DeSerilize(base.WCFService.ImportContracts(dtExcel, importType, currentUserID,
                        out totalCount, out successCount, out failCount));
                    if (dsImportResult != null && dsImportResult.Tables.Count > 0)
                    {
                        dtResult = dsImportResult.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtResult;
        }

        /// <summary>
        /// 将Excel转换为DataTable
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <returns></returns>
        public System.Data.DataTable ConvertExcelToDataTable(string xlsPath)
        {
            System.Data.DataTable dtExcel = new System.Data.DataTable();
            //
            string connStr = string.Format(ContractBLL.c_ConnectionStringFormat, xlsPath);
            List<System.Data.DataTable> dtSheets = new List<System.Data.DataTable>();
            List<string> sheetNames = this.GetExcelSheetNames(xlsPath);
            foreach (string sheetName in sheetNames)
            {
                // 查询语句
                string sql = "SELECT * FROM [" + sheetName + "A2:AH]";
                DataSet ds = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
                {
                    da.Fill(ds);    // 填充DataSet
                }
                if (ds.Tables.Count > 0)
                {
                    System.Data.DataTable dt = ds.Tables[0];
                    dt = this.ChangeToTextTable(dt);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.RemoveAt(0);
                        dt.Columns.Add(c_Column_LineNumber, typeof(string));
                        dt.Columns[c_Column_LineNumber].SetOrdinal(0);//设置行号在第一列，和数据库中位置对应
                        dt.Columns.Add("SQLCondition", typeof(string));
                        dt.Columns.Add("SQLAmountFormula", typeof(string));
                        dt.Columns.Add("条件数字串", typeof(string));
                        dt.Columns.Add("公式数字串", typeof(string));
                        //
                        this.AddLineNumber(dt, sheetName);
                        this.RemoveEmptyRows(dt);
                        this.FillEmptyCell(dt);
                        this.FormatContractIndex(dt, sheetName);
                        this.FormatNumeric(dt);
                        this.ProcessSQLFormat(dt);
                        dtSheets.Add(dt);
                    }
                }
            }
            dtExcel = this.MarginTable(dtSheets);
            dtExcel.TableName = "Import_Contract";
            return dtExcel;
        }

        /// <summary>
        /// 将单元格全部转换为字符型
        /// </summary>
        /// <param name="dtExcel"></param>
        /// <returns></returns>
        private System.Data.DataTable ChangeToTextTable(System.Data.DataTable dtExcel)
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
        /// 将部分字段内容处理为数字类型
        /// </summary>
        /// <param name="dt"></param>
        private void FormatNumeric(System.Data.DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                this.ReplaceSymbol(row, "保证金*");
                this.ReplaceSymbol(row, "税率");
                this.ReplaceSymbol(row, "条件");
                this.ReplaceSymbol(row, "公式");
            }
        }

        /// <summary>
        /// 将数字中的符号去掉
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cellName"></param>
        private void ReplaceSymbol(DataRow row, string cellName)
        {
            object value = row[cellName];
            if (value != null && value != DBNull.Value)
            {
                row[cellName] = value.ToString().Replace(",", "").Replace("$", "").Replace("￥", "");
            }
        }

        /// <summary>
        /// 合并DataTable
        /// </summary>
        /// <param name="tableList"></param>
        /// <returns></returns>
        private System.Data.DataTable MarginTable(List<System.Data.DataTable> tableList)
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
                    string connStr = string.Format(ContractBLL.c_ConnectionStringFormat, xlsPath);
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

        ///// <summary>
        ///// 获取Sheet名称
        ///// </summary>
        ///// <param name="xlsPath"></param>
        ///// <returns></returns>
        //private static string GetSheetName(string xlsPath)
        //{
        //    Microsoft.Office.Interop.Excel.Application xlApp = null;
        //    Microsoft.Office.Interop.Excel.Workbook xlBook = null;
        //    string sheetName = string.Empty;
        //    Object Nothing = System.Reflection.Missing.Value;

        //    try
        //    {
        //        // 创建Excel对象
        //        xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //        if (xlApp == null)
        //        {
        //            throw new Exception("事项导入中，终端机器没有安装 Office!");
        //        }
        //        xlBook = xlApp.Workbooks.Open(xlsPath, Nothing,
        //            Nothing, Nothing, Nothing,
        //            Nothing, Nothing, Nothing, Nothing, Nothing,
        //            Nothing, Nothing, Nothing, Nothing, Nothing);

        //        sheetName = ((Worksheet)xlBook.Worksheets[1]).Name;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //        if (xlBook != null)
        //        {
        //            xlBook.Close(Nothing, Nothing, Nothing);
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
        //            xlBook = null;

        //            xlApp.Application.Quit();
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
        //            xlApp = null;

        //            GC.Collect();
        //            GC.WaitForPendingFinalizers();
        //        }
        //    }
        //    return sheetName;
        //}

        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        private void AddLineNumber(System.Data.DataTable dt, string sheetName)
        {
            int lineNumber = 4;
            foreach (DataRow row in dt.Rows)
            {
                row[ContractBLL.c_Column_LineNumber] = string.Format("{0}_{1}", sheetName, lineNumber);
                lineNumber++;
            }
        }

        /// <summary>
        /// 移除空白行
        /// </summary>
        /// <param name="dt"></param>
        private void RemoveEmptyRows(System.Data.DataTable dt)
        {
            int columnCount = dt.Columns.Count;
            //删除空白行
            for (int index = dt.Rows.Count - 1; index >= 0; index--)
            {
                if (this.IsEmptyRow(dt.Rows[index], columnCount))
                {
                    dt.Rows.RemoveAt(index);
                }
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
        /// 填充空白单元格
        /// </summary>
        /// <param name="dt"></param>
        /// <remarks>按照上一行的数据填充</remarks>
        private void FillEmptyCell(System.Data.DataTable dt)
        {
            int columnCount = dt.Columns.Count;
            DataRow row = null;
            bool isNewContract = true;
            string contractIndex = "";

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                row = dt.Rows[rowIndex];

                if (row[c_Column_ContractIndex] != DBNull.Value && contractIndex != row[c_Column_ContractIndex].ToString())
                {
                    isNewContract = true;
                }

                if (isNewContract)
                {
                    contractIndex = row[c_Column_ContractIndex].ToString();
                    isNewContract = false;
                }
                else
                {
                    if (rowIndex > 0)//排除第一行
                    {
                        for (int columnIndex = 1; columnIndex < 33; columnIndex++)//33到首次DueDate列
                        {
                            if ((row[columnIndex] + string.Empty) == string.Empty)
                            {
                                row[columnIndex] = dt.Rows[rowIndex - 1][columnIndex];
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 格式化合同序号，给合同序号添加Sheet编号
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        private void FormatContractIndex(System.Data.DataTable dt, string sheetName)
        {
            dt.Columns.Add("ContractIndex", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["ContractIndex"] = string.Format("{0}_{1}", sheetName, row[ContractBLL.c_Column_ContractIndex].ToString());
            }
            dt.Columns.Remove(ContractBLL.c_Column_ContractIndex);
            dt.Columns["ContractIndex"].SetOrdinal(1);
            dt.Columns["ContractIndex"].ColumnName = ContractBLL.c_Column_ContractIndex;
        }

        /// <summary>
        /// 将条件和公式处理成SQL格式
        /// </summary>
        /// <param name="dt"></param>
        private void ProcessSQLFormat(System.Data.DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                object formula = row["公式"];
                object condition = row["条件"];
                string cycleSales = "CycleSales";
                string cycleSalesToLower = cycleSales.ToLower();
                //
                if (condition != DBNull.Value && condition != null)
                {
                    string sqlCondition = condition.ToString().ToNormalString();
                    if (sqlCondition.ToLower().Contains(cycleSalesToLower))
                    {
                        if (!(sqlCondition.StartsWith(cycleSales, true, CultureInfo.CurrentCulture)
                            || sqlCondition.EndsWith(cycleSales, true, CultureInfo.CurrentCulture)))
                        {
                            string replace = sqlCondition.Substring(sqlCondition.IndexOf(cycleSales, StringComparison.OrdinalIgnoreCase), 10);
                            sqlCondition = sqlCondition.Replace(replace, cycleSales + " AND " + cycleSales);
                        }
                    }
                    row["条件"] = condition.ToString().ToNormalString();
                    row["SQLCondition"] = sqlCondition;
                    row["条件数字串"] = condition.ToString().ExtractDigital();
                }
                //
                if (formula != DBNull.Value && formula != null)
                {
                    row["公式"] = formula.ToString().ToNormalString();
                    row["SQLAmountFormula"] = formula.ToString().ToNormalString();
                    row["公式数字串"] = formula.ToString().ExtractDigital();
                }
            }
        }
        #endregion 导入合同

        #region VendorContract

        /// <summary>
        /// 通过合同快照ID查询业主合同关系
        /// </summary>
        /// <param name="contractSnapshotID"></param>
        /// <returns></returns>
        public DataSet SelectVendorContractByContractSnapshotID(string contractSnapshotID)
        {
            return base.DeSerilize(base.WCFService.SelectVendorContractByContractSnapshotID(contractSnapshotID));
        }

        /// <summary>
        /// 查询单个业主合同关系
        /// </summary>
        /// <param name="vendorContractID"></param>
        /// <returns></returns>
        public VendorContractEntity SelectSingleVendorContract(System.String vendorContractID)
        {
            return base.WCFService.SelectSingleVendorContract(vendorContractID);
        }

        /// <summary>
        /// 新增单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void InsertSingleVendorContract(VendorContractEntity entity)
        {
            base.WCFService.InsertSingleVendorContract(entity);
        }

        /// <summary>
        /// 更新单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateSinglVendorContract(VendorContractEntity entity)
        {
            base.WCFService.UpdateSingleVendorContract(entity);
        }

        /// <summary>
        /// 删除单个业主合同关系
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteSingleVendorContract(VendorContractEntity entity)
        {
            base.WCFService.DeleteSingleVendorContract(entity);
        }
        #endregion

        /// <summary>
        /// 获取给定EntityID相同EntityTypeName与EntityName的最新的EntityID
        /// </summary>
        /// <param name="strEntityID"></param>
        /// <returns></returns>
        public DataSet GetLatestEntityID(string strEntityID)
        {
            if (null == WCFService)
                return null;

            return DeSerilize(WCFService.GetLatestEntityID(strEntityID));
        }
    }
}