using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 提供生成凭证文档并打包导出的方法。
    /// </summary>
    public class CertificateDocument
    {
        //Fields
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"\temp\";//根目录
        private string tPath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\T\";//PaymentType为T的文件路径
        private string mPath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\M\";//PaymentType为M的文件路径

        private string apCerTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"\Template\AP凭证模板.xls";
        private string glCerTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"\Template\GL凭证模板.xls";
        private string taskListTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"\Template\待办任务模板.xls";

        //Properties
        /// <summary>
        /// 获取或设置当前登录用户ID。
        /// </summary>
        public string CurrentUserEnglishName { get; set; }
        /// <summary>
        /// 获取或设置生成的凭证类型。
        /// </summary>
        public CaculateType CaculateType { get; set; }
        /// <summary>
        /// 获取或设置返回文件名流水号的方法。
        /// </summary>
        public Func<string> FuncFlowNumber { get; set; }
        /// <summary>
        /// 获取或设置生成凭证用到的所有数据(包含5个表：
        /// 表0：凭证表；
        /// 表1：计算结果；
        /// 表2：计算过程公式及结果；
        /// 表3：审核人、审批人、条码；
        /// 表4：待办任务列表)。
        /// </summary>
        public DataSet DataSource { get; set; }
        
        #region 生成凭证

        /// <summary>
        /// 表示一个GL凭证文件的类型。
        /// </summary>
        internal class GLCertificateFile
        {
            public string CompanyCode { get; set; }
            public string RentType { get; set; }
            public string GLType { get; set; }
        }

        /// <summary>
        /// 生成凭证Excel文件。
        /// </summary>
        private void GenerateCertificateExcel()
        {
            if (this.DataSource == null)
            {
                throw new Exception("凭证数据源DataSource不能为空");
            }
            //
            if (this.CaculateType == CaculateType.AP)
            {
                //生成PaymentType为"T"的Excel凭证文件
                this.GenerateAPCertificateExcel("T");
                //生成PaymentType为"M"的Excel凭证文件
                this.GenerateAPCertificateExcel("M");
            }
            else
            {
                //同一种租金类型生成一个GL文件
                List<GLCertificateFile> list = new List<GLCertificateFile>();
                foreach (DataRow dr in this.DataSource.Tables[0].Rows)
                {
                    string glType = dr["GLType"] == DBNull.Value ? string.Empty : dr["GLType"].ToString();
                    if (!list.Exists(item => item.GLType.Equals(glType)))
                    {
                        string rentType = dr["RentType"] == DBNull.Value ? string.Empty : dr["RentType"].ToString();
                        //
                        list.Add(new GLCertificateFile() { RentType = rentType, GLType = glType });
                    }
                }
                //
                foreach (GLCertificateFile entity in list)
                {
                    this.GenerateGLCertificateExcel(entity.RentType, entity.GLType);
                }
            }
        }
        /// <summary>
        /// 按指定的PaymentType生成AP凭证Excel文件。
        /// </summary>
        /// <param name="paymentType">"T" or "M"</param>
        private void GenerateAPCertificateExcel(string paymentType)
        {
            DataTable dt = this.DataSource.Tables[0].Clone();
            if (dt == null)
            {
                return;
            }
            //
            DataRow[] drs = this.DataSource.Tables[0].Select(
                "PaymentType='" + paymentType + "' AND CoNumber IS NOT NULL AND ISNULL(InvoiceAmount,0)<>0", 
                "CoNumber DESC,Site ASC,Dep ASC,InvoiceAmount ASC");
            if (drs == null || drs.Length <= 0)
            {
                return;
            }
            //添加该PaymentType下的所有凭证数据的列头
            foreach (DataRow dr in drs)
            {
                dt.Rows.Add(dr.ItemArray);
                //
                DataRow[] drsTmp = this.DataSource.Tables[0].Select(
                    "APRecordID='" + dr["APRecordID"] + "' AND CoNumber IS NULL", 
                    "OpenItem ASC");
                if (drsTmp != null && drsTmp.Length > 0)
                {
                    foreach (DataRow drTmp in drsTmp)
                    {
                        dt.Rows.Add(drTmp.ItemArray);
                    }
                }
            }
            //统计InvoiceAmount、InputAmount之和
            decimal invoiceAmount = 0M;
            decimal inputAmount = 0M;
            foreach (DataRow r in drs)
            {
                decimal temp = 0M;
                if (r["InvoiceAmount"] != DBNull.Value && decimal.TryParse(r["InvoiceAmount"].ToString(), out temp))
                {
                    invoiceAmount += temp;
                }
                //
                temp = 0M;
                if (r["InputAmount"] != DBNull.Value && decimal.TryParse(r["InputAmount"].ToString(), out temp))
                {
                    inputAmount += temp;
                }
            }
            //填充数据源到模板后导出
            TemplateToExcel xls = new TemplateToExcel(this.apCerTemplatePath, dt);
            xls.SheetName = "AP Entry";
            xls.SetColumnMapping("MSIS_G_Voucher", "B");//MSIS-G Voucher #
            xls.SetColumnMapping("CoNumber", "C");//Co.
            xls.SetColumnMapping("VendorNumber", "D");//Vendor Number
            xls.SetColumnMapping("VendorName", "E");//Vendor Name
            xls.SetColumnMapping("InvoiceDigits", "F");//Invoice # (20 Digits)
            xls.SetColumnMapping("FaPiaoFlag", "G");//Fa Piao flag(Y or N)
            xls.SetColumnMapping("DescriptionInfo", "H");//Description(30 Digits)
            xls.SetColumnMapping("InvoiceDate", "I");//Invoice Date
            //xls.SetColumnMapping("DueDate", "J");//Invoice Date
            xls.SetColumnMapping("InvoiceAmount", "K");//Invoice Amount
            xls.SetColumnMapping("Quantity", "L");//Quantity
            xls.SetColumnMapping("Unit", "M");//Unit
            xls.SetColumnMapping("UnitPrice", "N");//Unit Price
            xls.SetColumnMapping("TotalGrossAmount", "O");//Total Gross Amount
            xls.SetColumnMapping("InventoryDescription", "P");//Inventory Description(26 Digits)
            xls.SetColumnMapping("AccountNumber", "Q");//Account Number
            xls.SetColumnMapping("Site", "R");//Site
            xls.SetColumnMapping("Dep", "S");//Dep
            xls.SetColumnMapping("OpenItem", "T");//OpenItem#
            xls.SetColumnMapping("DescInfo", "U");//Desc(30 Digits)
            xls.SetColumnMapping("Ref1", "V");//Ref1
            xls.SetColumnMapping("Ref2", "W");//Ref2
            xls.SetColumnMapping("InputAmount", "X");//Input Amount
            xls.SetColumnMapping("UserID", "Y");//User ID
            xls.SetColumnMapping("ApprCode", "Z");//Appr.Code
            xls.SetColumnMapping("UserName", "AA");//User Name
            xls.SetColumnMapping("Pre_Approve", "AB");//Pre-approve
            xls.SetColumnMapping("Imagelink", "AC");//Image link
            xls.SetColumnMapping("ImageSend", "AD");//Image Send
            xls.SetColumnMapping("BarCode", "AE");//Bar Code
            xls.Fill(4);

            //B3若“payment type”为T，则显示“Vendor Master”。若“payment type”为M，则显示“Manual Cheque”
            xls.SetCellValue("B3", (paymentType.Equals("T") ? "Vendor Master" : "Manual Cheque"));

            //D3生成AP凭证日期所属月份
            xls.SetCellValue("D3", DateTime.Now.Month.ToString());

            //H3不需要
            //K3该列的总和
            xls.SetCellValue("K3", invoiceAmount.ToString());

            //I3默认等于K3
            xls.SetCellValue("I3", invoiceAmount.ToString());

            //P3默认为0
            xls.SetCellValue("P3", 0);

            //Q3等于K3-X3
            xls.SetCellValue("Q3", invoiceAmount - inputAmount);

            //X3该列的总和
            xls.SetCellValue("X3", inputAmount);

            //保存文件
            string fileName = string.Format(@"{0}{1}{2}{3}{4}.xls", (paymentType == "T" ? this.tPath : this.mPath), 
                this.CaculateType, paymentType, DateTime.Now.ToString("yyyyMMdd"), this.FuncFlowNumber());
            xls.SaveAs(fileName);
        }
        /// <summary>
        /// 按公司和租金类型生成一个GL凭证Excel文件。
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="rentType"></param>
        private void GenerateGLCertificateExcel(string rentType, string glType)
        {
            DataTable dt = this.DataSource.Tables[0].Clone();
            if (dt == null)
            {
                return;
            }
            //Debit凭证
            DataRow[] drsDebit = this.DataSource.Tables[0].Select(
                "GLType='" + glType + "' AND Co IS NOT NULL AND ISNULL(Debit,0)<>0", 
                "Co,AC_Mo,ScCd,Site,Dept,Debit");
            if (drsDebit == null || drsDebit.Length <= 0)
            {
                return;
            }
            //
            string corp = string.Empty, tmpCorp = string.Empty;
            string acMo = string.Empty, tmpAcMo = string.Empty;
            string scCd = string.Empty, tmpScCd = string.Empty;
            //获取凭证
            foreach (DataRow drDebit in drsDebit)
            {
                //添加该公司下的所有凭证数据到dt并设置Ref1的值为当前登录用户名的最后三位
                if (!string.IsNullOrEmpty(this.CurrentUserEnglishName))
                {
                    drDebit["Ref1"] = this.CurrentUserEnglishName.Length > 2 ? this.CurrentUserEnglishName.Substring(this.CurrentUserEnglishName.Length - 3) : this.CurrentUserEnglishName;
                }
                //Co, AC_Mo, ScCd相同的行合并
                DataRow tmpRow = dt.NewRow();
                tmpRow.ItemArray = drDebit.ItemArray;
                tmpCorp = tmpRow["CompanyCode"] == DBNull.Value ? string.Empty : tmpRow["CompanyCode"].ToString();
                tmpAcMo = tmpRow["AC_Mo"] == DBNull.Value ? string.Empty : tmpRow["AC_Mo"].ToString();
                tmpScCd = tmpRow["ScCd"] == DBNull.Value ? string.Empty : tmpRow["ScCd"].ToString();
                if (tmpCorp == corp && tmpAcMo == acMo && tmpScCd == scCd)
                {
                    tmpRow["Co"] = DBNull.Value;
                    tmpRow["AC_Mo"] = DBNull.Value;
                    tmpRow["ScCd"] = DBNull.Value;
                }
                corp = tmpCorp;
                acMo = tmpAcMo;
                scCd = tmpScCd;
                //添加Debit凭证
                dt.Rows.Add(tmpRow.ItemArray);
                //获取Credit凭证
                DataRow[] drsCredit = this.DataSource.Tables[0].Select("GLRecordID='" + drDebit["GLRecordID"] + "' AND Co IS NULL");
                if (drsCredit != null && drsCredit.Length > 0)
                {
                    foreach (DataRow drCredit in drsCredit)
                    {
                        dt.Rows.Add(drCredit.ItemArray);
                    }
                }
            }
            //统计Debit、Credit之和
            decimal sumDebit = 0M;
            decimal sumCredit = 0M;
            foreach (DataRow r in drsDebit)
            {
                decimal temp = 0M;
                if (r["Debit"] != DBNull.Value && decimal.TryParse(r["Debit"].ToString(), out temp))
                {
                    sumDebit += temp;
                }
                //
                temp = 0M;
                if (r["Credit"] != DBNull.Value && decimal.TryParse(r["Credit"].ToString(), out temp))
                {
                    sumCredit += temp;
                }
            }
            //导出凭证数据到Excel
            TemplateToExcel xls = new TemplateToExcel(this.glCerTemplatePath, dt);
            xls.SheetName = "General Journal";
            xls.SetColumnMapping("Voucher", "B");//Voucher #
            xls.SetColumnMapping("Co", "C");//Co.
            xls.SetColumnMapping("AC_Mo", "D");//A/C Mo.
            xls.SetColumnMapping("ScCd", "E");//Sc.Cd.
            xls.SetColumnMapping("TransDate", "F");//Trans.Date
            //xls.SetColumnMapping("InterCompany", "G");//Inter Company 无数据列映射
            xls.SetColumnMapping("AccountNumber", "H");//Account Number
            xls.SetColumnMapping("Site", "I");//Site
            xls.SetColumnMapping("Dept", "J");//Dept
            xls.SetColumnMapping("Debit", "K");//Debit
            xls.SetColumnMapping("Credit", "L");//Credit
            xls.SetColumnMapping("DescriptionInfo", "M");//Description
            xls.SetColumnMapping("OpenItem", "N");//Open Item #
            xls.SetColumnMapping("Ref1", "O");//Ref #1
            xls.SetColumnMapping("Ref2", "P");//Ref #2

            //命名规则：GLYYYYMMDDxxx（若为直线GL则为：SLYYYYMMDDxxx）
            string fileName = string.Format(@"{0}{1}{2}{3}.xls", this.path, (glType.IndexOf("直线租金") > -1 ? "SL" : "GL"), DateTime.Now.ToString("yyyyMMdd"), this.FuncFlowNumber());
            xls.Fill(4);

            //K3该列总和
            xls.SetCellValue("K3", sumDebit);

            //L3该列总和
            xls.SetCellValue("L3", sumCredit);

            //M3若k3不等于L3则显示Unbalanced Entry
            if (sumDebit != sumCredit)
            {
                xls.SetCellValue("M3", "Unbalanced Entry");
            }
            xls.SaveAs(fileName);
        }

        /// <summary>
        /// 生成影像文件。
        /// </summary>
        private void GenerateCertificateImage()
        {
            if (this.DataSource == null)
            {
                return;
            }
            //生成pdf
            //CertificatePdf pdf = new CertificatePdf();
            //pdf.CaculateType = this.CaculateType;
            //pdf.FuncFlowNumber = this.FuncFlowNumber;
            //pdf.DataSource = this.DataSource;
            //pdf.CreatePdf();
            //生成tif
            CertificateTif tif = new CertificateTif();
            tif.CaculateType = this.CaculateType;
            tif.FuncFlowNumber = this.FuncFlowNumber;
            tif.DataSource = this.DataSource;
            tif.CreateTif();
        }

        /// <summary>
        /// 生成任务列表Excel。
        /// </summary>
        private void GenerateTaskExcel()
        {
            TemplateToExcel xls = new TemplateToExcel(this.taskListTemplatePath, this.DataSource.Tables[4]);
            xls.SheetName = "任务列表";
            xls.SetColumnMapping("ProcID", "A");//任务编号
            xls.SetColumnMapping("ContractNo", "B");//合同编号
            xls.SetColumnMapping("CompanyCode", "C");//公司编号
            xls.SetColumnMapping("StoreOrDeptNo", "D");//餐厅编号
            xls.SetColumnMapping("VendorNo", "E");//Vendor编号
            xls.SetColumnMapping("VendorName", "F");//Vendor名称
            xls.SetColumnMapping("PayMentType", "G");//PaymentType
            xls.SetColumnMapping("RentType", "H");//租金类型
            xls.SetColumnMapping("RentDateDiff", "I");//期间
            if (this.CaculateType == CaculateType.AP)
            {
                xls.SetColumnMapping("APAmount", "J");//金额
            }
            else
            {
                xls.SetColumnMapping("GLAmount", "J");//金额
            }
            xls.Fill(1);
            //
            xls.SaveAs(string.Format("{0}{1}.xls", this.path, "任务列表"));
        }

        /// <summary>
        /// 生成文件。
        /// </summary>
        public void Create()
        {
            //删除凭证临时目录
            if (System.IO.Directory.Exists(this.path))
            {
                System.IO.Directory.Delete(this.path, true);
            }
            //新建临时目录
            System.IO.Directory.CreateDirectory(this.path);
            //AP凭证目录
            if (this.CaculateType == CaculateType.AP)
            {
                System.IO.Directory.CreateDirectory(this.tPath);
                System.IO.Directory.CreateDirectory(this.mPath);
            }
            //生成文件
            this.GenerateCertificateExcel();
            this.GenerateCertificateImage();
            this.GenerateTaskExcel();
        }

        /// <summary>
        /// 将生成的凭证打包到指定的路径下。
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAs(string fileName)
        {
            //AP凭证需要按PaymentType分开打包
            if (this.CaculateType == CaculateType.AP)
            {
                //生成PaymentType为"T"的zip
                string zipFileName = string.Format("{0}{1}{2}{3:yyyyMMdd}{4}.zip", this.path, this.CaculateType, "T", DateTime.Now, this.FuncFlowNumber());
                ZipHelper.CreateZipFile(this.tPath, zipFileName);
                //生成PaymentType为"M"的zip
                zipFileName = string.Format("{0}{1}{2}{3:yyyyMMdd}{4}.zip", this.path, this.CaculateType, "M", DateTime.Now, this.FuncFlowNumber());
                ZipHelper.CreateZipFile(this.mPath, zipFileName);
            }
            //将两种类型的zip再打包到指定路径
            ZipHelper.CreateZipFile(this.path, fileName);
        }
        #endregion
    }

    /// <summary>
    /// 凭证类型。
    /// </summary>
    public enum CaculateType
    {
        AP,
        GL
    }
}