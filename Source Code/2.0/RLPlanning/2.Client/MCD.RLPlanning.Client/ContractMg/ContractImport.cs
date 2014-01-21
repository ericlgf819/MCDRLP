using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.Common;
using System.Runtime.InteropServices;
using MCD.RLPlanning.Client.AppCode;


namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class ContractImport : BaseList
    {
        public ContractImport()
        {
            InitializeComponent();
            if (!base.DesignMode)
            {
                this.dgvList.AutoGenerateColumns = false;
                this.dgvList.AllowUserToAddRows = false;
                this.dgvList.AllowUserToDeleteRows = false;
                this.dgvList.AllowUserToOrderColumns = false;
                this.dgvList.ReadOnly = true;

                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ExcelIndex", base.GetMessage("ExcelIndex"),100);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ContractIndex", base.GetMessage("ContractIndex"),100);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "RelationData", base.GetMessage("RelationData"),200);
                GridViewHelper.AppendColumnToDataGridView(this.dgvList, "CheckMessage", base.GetMessage("CheckMessage"));
                this.dgvList.Columns["CheckMessage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;                
            }
        }

        #region 字段和属性

        private ContractBLL contractBLL;

        #endregion

        #region 重写基类方法

        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.contractBLL.Dispose();
            base.BaseFrm_FormClosing(sender, e);
        }

        #endregion

        #region 控件事件处理

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = this.openFileDialog1.FileName;
            }
        }

        protected virtual void btnImport_Click(object sender, EventArgs e)
        {
            this.dgvList.DataSource = null;
            Application.DoEvents();
            this.ImportContract(ContractImportType.导入);
        }

        private void ContractImport_Load(object sender, EventArgs e)
        {
            this.contractBLL = new ContractBLL();

            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvList);

            this.toolStrip1.Visible = false;
        }

        protected virtual void btnImportPreview_Click(object sender, EventArgs e)
        {
            this.dgvList.DataSource = null;
            Application.DoEvents();
            this.ImportContract(ContractImportType.预览);
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //合并单元格
            GridViewHelper.MerageCell(dgv, e, new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 合同导入类型
        /// </summary>
        private enum ContractImportType
        {
            导入,
            预览,
        }

        /// <summary>
        /// 执行合同导入
        /// </summary>
        /// <param name="importType"></param>
        private void ImportContract(ContractImportType importType)
        {
            string path = this.txtPath.Text.Trim();
            if (File.Exists(path))
            {
                // 检测文件扩展名
                if (this.CheckFileExtensionIsProhibit(path))
                {
                    base.MessageInformation(base.GetMessage("FileExtensionIsProhibit"));
                    return;
                }
                // 检测文件是否当前打开
                if (WinAPI.CheckFileIsOpen(path))
                {
                    MessageInformation(base.GetMessage("FileIsOpen"));
                    return;
                }
                //校验列数是否正确
                string checkResult = this.contractBLL.CheckExcelFormat(path);
                if (checkResult != null && checkResult != string.Empty && checkResult.Length > 0)
                {
                    MessageInformation(string.Format(base.GetMessage("SheetFormatError"), checkResult));
                    return;
                }

                DataTable dt = null;
                int totalCount = 0, successCount = 0, failCount = 0;

                //执行查询
                FrmWait frm = new FrmWait(() =>
                {
                    base.ExecuteAction(() =>
                    {
                        dt = this.contractBLL.ImportContracts(path, importType.ToString(), AppCode.SysEnvironment.CurrentUser.ID.ToString(),
                            out totalCount, out successCount, out failCount);
                    }, base.GetMessage("ImportContractError"), base.GetMessage("ImportContracts"));
                }, base.GetMessage("Wait"), () =>
                {
                    this.contractBLL.CloseService();
                });
                frm.ShowDialog();

                if (dt != null)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = "ContractIndex ASC,ExcelIndex ASC";
                    this.dgvList.DataSource = dv;
                }
                else
                {
                    this.dgvList.DataSource = null;
                }

                //弹出执行结果
                string importResultInfo = string.Format(base.GetMessage("ImportResult"), totalCount, successCount, failCount);
                if (totalCount == 0)//可能格式错误或没有填入数据
                {
                    importResultInfo += System.Environment.NewLine + base.GetMessage("FormatError");
                }
                MessageInformation(importResultInfo);
            }
            else
            {
                //弹出错误信息
                MessageInformation(base.GetMessage("FileNotExist"));
            }
        }

        protected bool CheckFileExtensionIsProhibit(string path)
        {
            if (AppCode.SysEnvironment.SystemSettings["AttachFileTypeList"] != null)
            {
                string[] extensions = SysEnvironment.SystemSettings["AttachFileTypeList"].ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (!extensions.Contains(path.Substring(path.LastIndexOf('.')).ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            //获取Excel文件名
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.Filter = "Excel文件|*.xls";
            DialogResult ret = dlg.ShowDialog();

            if (ret == DialogResult.OK || ret == DialogResult.Yes)
            {
                string strFilePath = dlg.FileName;
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\Template\Contract Template.xls", strFilePath);
            }
        }
    }
}
