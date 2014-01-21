using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client;
using MCD.Common;
using MCD.RLPlanning.Client.ContractMg;
using MCD.RLPlanning.Business.ForecastSales;
using Missing = System.Reflection.Missing;
using Excel = Microsoft.Office.Interop.Excel;


namespace MCD.RLPlanning.Client.ForcastSales
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Import : ContractImport
    {
        private SalesBLL salesBLL;

        public Import()
        {
            InitializeComponent();

            if (!base.DesignMode)
            {
                this.dgvList.AutoGenerateColumns = false;
                this.dgvList.AllowUserToAddRows = false;
                this.dgvList.AllowUserToDeleteRows = false;
                this.dgvList.AllowUserToOrderColumns = false;
                this.dgvList.ReadOnly = true;

                dgvList.Columns.Clear();
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Company", base.GetMessage("Company"), 60);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "餐厅编号", base.GetMessage("StoreNo"), 100);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Store", base.GetMessage("StoreName"), 200);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Kiosk", base.GetMessage("KioskName"), 200);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "年度", base.GetMessage("Year"), 50);
                for (int index = 1; index <= 12; ++index)
                {
                    GridViewHelper.AppendColumnToDataGridView(this.dgvList, string.Format("{0}月", index), string.Format("{0}", index), 80);
                }
            }
        }

        private void Import_Load(object sender, EventArgs e)
        {
            salesBLL = new SalesBLL();
            this.toolStrip1.Visible = false;
        }

        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != salesBLL)
                salesBLL.Dispose();

            base.BaseFrm_FormClosing(sender, e);
        }

        #region 事件重载
        /// <summary>
        /// 导入预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnImportPreview_Click(object sender, EventArgs e)
        {
            this.dgvList.DataSource = null;
            Application.DoEvents();
            ImportSales(true);
        }

        protected override void btnImport_Click(object sender, EventArgs e)
        {
            this.dgvList.DataSource = null;
            Application.DoEvents();

            //保存时间可能较长，需要提醒用户
            if (File.Exists(txtPath.Text.Trim()))
            {
                DialogResult res =
                    MessageBox.Show(GetMessage("TipContent"), GetMessage("TipContentTitle"), MessageBoxButtons.YesNo);
                if (DialogResult.No == res)
                {
                    return;
                }
            }

            ImportSales(false);
        }
        #endregion

        #region 导入方法
        /// <summary>
        /// 导入Sales数据
        /// </summary>
        /// <param name="bPreview">true表明是预览，fales为导入</param>
        private void ImportSales(bool bPreview)
        {
            string path = txtPath.Text.Trim();
            if (File.Exists(path))
            {
                // 检测文件扩展名
                if (base.CheckFileExtensionIsProhibit(path))
                {
                    base.MessageInformation(base.GetMessage("FileExtensionIsProhibit"));
                    return;
                }
                // 检测文件是否当前打开
                if (WinAPI.CheckFileIsOpen(path))
                {
                    base.MessageInformation(base.GetMessage("FileIsOpen"));
                    return;
                }

                //校验列数是否正确
                string checkResult = this.salesBLL.CheckExcelFormat(path);
                if (checkResult != null && checkResult != string.Empty && checkResult.Length > 0)
                {
                    MessageInformation(string.Format(base.GetMessage("SheetFormatError"), checkResult));
                    return;
                }

                DataTable dt = null;
                //执行导入Sales
                StringBuilder errMsg = null;
                string warnmsg = null;
                List<string> busyStores = null, busyKiosks = null;
                FrmWait frm = new FrmWait(() =>
                {
                    ExecuteAction(() =>
                    {
                        dt = salesBLL.ImportSales(path, bPreview, out errMsg, SysEnvironment.CurrentUser.ID, out warnmsg, out busyStores, out busyKiosks);
                    },
                        base.GetMessage("ImportSalesError"), base.GetMessage("ImportSales"));
                }, base.GetMessage("Wait"), false);

                frm.ShowDialog();

                

                //显示提示信息
                if (!string.IsNullOrEmpty(warnmsg))
                {
                    ImportErrBox msgBox = new ImportErrBox(warnmsg);
                    msgBox.ShowDialog();
                }

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
                //没有错误信息才显示正确的信息
                else if (null != dt)
                {
                    dgvList.DataSource = dt;
                }
            }
            else
            {
                MessageInformation(base.GetMessage("FileNotExists"));
            }
        }
        #endregion

        private void btnTemplateDownload_Click(object sender, EventArgs e)
        {
            //获取Excel文件名
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.Filter = "Excel文件|*.xls";
            DialogResult ret = dlg.ShowDialog();

            if (ret == DialogResult.OK || ret == DialogResult.Yes)
            {
                
                FrmWait frm = new FrmWait(() =>
                {
                    ExecuteAction(() =>
                    {
                        //获取template的dataset
                        DataSet ds = salesBLL.SelectImportSalesTemplate(SysEnvironment.CurrentUser.ID.ToString());

                        if (null == ds)
                        {
                            MessageError(GetMessage("SelectTemplateError"));
                            return;
                        }

                        //导出到Excel中
                        ExportExcel(ds.Tables[0], dlg.FileName);
                    },
                    base.GetMessage("ExportTemplateError"), base.GetMessage("ExportTemplate"), true);
                }, base.GetMessage("Wait"), () =>
                {
                    this.salesBLL.CloseService();
                });

                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 导出Excel模板
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strFileName"></param>
        private void ExportExcel(DataTable dt, string strFileName)
        {
            if (null == dt)
                return;

            Excel.Application xlsApp = new Excel.Application();

            //该机器没有安装excel
            if (null == xlsApp)
            {
                MessageError(GetMessage("NoExcel"));
                return;
            }

            Excel.Workbooks workbooks = xlsApp.Workbooks;
            Excel.Workbook workbook = workbooks.Add(Missing.Value);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            //将多余的sheet删除
            for (int i = workbook.Worksheets.Count; i > 1; --i)
            {
                ((Excel.Worksheet)(workbook.Worksheets[i])).Delete();
            }

            //列名填充
            for (int iCol = 0; iCol < dt.Columns.Count; ++iCol)
            {
                worksheet.Cells[1, iCol + 1] = dt.Columns[iCol].ToString();
            }

            //行数据
            for (int iRow = 0; iRow < dt.Rows.Count; ++iRow)
            {
                for (int iCol = 0; iCol < dt.Columns.Count; ++iCol)
                {
                    worksheet.Cells[iRow + 2, iCol + 1] = dt.Rows[iRow][iCol];
                }
            }

            //冻结首行
            worksheet.get_Range("A2").Select();
            xlsApp.ActiveWindow.FreezePanes = true;

            //保存
            workbook.Saved = true;

            //导出成97-2003格式
            workbook.SaveAs(strFileName, Excel.XlFileFormat.xlExcel8);
            xlsApp.Quit();
        }
    }
}