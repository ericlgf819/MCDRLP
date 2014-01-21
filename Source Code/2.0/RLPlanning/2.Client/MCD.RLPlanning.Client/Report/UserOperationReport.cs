using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Report;

namespace MCD.RLPlanning.Client.Report
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserOperationReport : BaseFrm
    {
        //Fields
        private UserOperationBLL UserOperationBLL;
        #region ctor

        public UserOperationReport()
        {
            InitializeComponent();
            //
            if (!base.DesignMode)
            {
                this.UserOperationBLL = new UserOperationBLL();
            }
        }
        #endregion

        //Events
        private void btnExport_Click(object sender, EventArgs e)
        {
            int? companyStartNo = null, companyEndNo = null, storeStartNo = null, storeEndNo = null;
            //
            //if (!ControlHelper.GetTextValue(this.txtCompanyStartNo, ref companyStartNo))
            //{
            //    base.MessageError(base.GetMessage("CompanyNoStartIsNotFormat"));
            //    return;
            //}
            //else if (!ControlHelper.GetTextValue(this.txtCompanyEndNo, ref companyEndNo))
            //{
            //    base.MessageError(base.GetMessage("CompanyNoEndIsNotFormat"));
            //    return;
            //}
            //else if (!ControlHelper.GetTextValue(this.txtStoreStartNo, ref storeStartNo))
            //{
            //    base.MessageError(base.GetMessage("StoreNoStartIsNotFormat"));
            //    return;
            //}
            //else if (!ControlHelper.GetTextValue(this.txtStoreEndNo, ref storeEndNo))
            //{
            //    base.MessageError(base.GetMessage("StoreNoEndIsNotFormat"));
            //    return;
            //}
            //else if (companyEndNo.HasValue && companyStartNo.HasValue && companyEndNo.Value < companyStartNo.Value)
            //{
            //    base.MessageError(base.GetMessage("CompanyNoSmall"));
            //    //
            //    this.txtCompanyEndNo.Focus();
            //    return;
            //}
            //else if (storeEndNo < storeStartNo)
            //{
            //    base.MessageError(base.GetMessage("StoreEndNoSmall"));
            //    //
            //    this.txtStoreEndNo.Focus();
            //    return;
            //}
            //
            DateTime startDate = Convert.ToDateTime(this.dtStartDate.Value.ToShortDateString()), 
                endDate = Convert.ToDateTime(this.dtEndDate.Value.ToShortDateString());
            string operationType = string.Empty;
            if (this.chkAdd.Checked)
            {
                operationType += "0";
            }
            if (this.chkEdit.Checked)
            {
                operationType += "1";
            }
            if (this.chkDelete.Checked)
            {
                operationType += "2";
            }
            if (startDate.Date > DateTime.Now.Date)
            {
                base.MessageError(base.GetMessage("StartDateMaxToday"));
                //
                this.dtStartDate.Focus();
                return;
            }
            else if (startDate.Date > endDate.Date)
            {
                base.MessageError(base.GetMessage("EndDateSmall"));
                //
                this.dtEndDate.Focus();
                return;
            }
            else if (operationType == string.Empty)
            {
                base.MessageError(base.GetMessage("OperationTypeNULL"));
                return;
            }
            // 保存
            List<string[]> sheetValues = null;
            FrmWait frm = new FrmWait(() => {
                base.ExecuteAction(() =>
                {
                    sheetValues = this.UserOperationBLL.GetSheetValues(companyStartNo, companyEndNo, storeStartNo, storeEndNo,
                            startDate, endDate, operationType);
                }, "查询用户操作日志失败", "用户信息操作日志");
            }, base.GetMessage("Wait"), () =>
            {
                this.UserOperationBLL.CloseService();
            });
            frm.ShowDialog();
            //
            if (sheetValues == null || sheetValues.Count <= 0)
            {
                base.MessageInformation(base.GetMessage("NoRecords"));
                return;
            }
            // 生成文件
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.FileName = string.Format("用户信息操作日志({0:yyyyMMdd}-{1:yyyyMMdd}).xls", startDate, endDate);
            saveDlg.Filter = string.Format("Excel 2003工作表|*.xls");
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                MCD.Common.TemplateToExcel.SaveToXls(sheetValues, "用户信息操作日志", saveDlg.FileName);
            }
        }
        protected override void BaseFrm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.UserOperationBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }
    }
}