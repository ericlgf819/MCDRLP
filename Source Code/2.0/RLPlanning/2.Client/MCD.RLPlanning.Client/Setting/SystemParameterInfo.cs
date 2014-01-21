using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Setting;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SystemParameterInfo : BaseFrm
    {
        //Fields
        private SystemParameterBLL SystemParameterBLL = new SystemParameterBLL();
        private DataTable DTSource = null;
        #region ctor

        public SystemParameterInfo()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void SystemParameterInfo_Load(object sender, EventArgs e)
        {
            this.BindGridList();
        }
        /// <summary>
        /// 列表单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtParamCode.Text = this.dgvList.CurrentRow.Cells["ParamCode"].Value.ToString();
            this.txtParamName.Text = this.dgvList.CurrentRow.Cells["ParamName"].Value.ToString();//系统参数名称
            this.txtParamValue.Text = this.dgvList.CurrentRow.Cells["ParamValue"].Value.ToString();//值
            this.txtRemark.Text = this.dgvList.CurrentRow.Cells["Remark"].Value.ToString();//备注
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtParamCode.Text))
            {
                MessageBox.Show(base.GetMessage("PleaseSelect"), base.MessageTitle);
            }
            else
            {
                this.SystemParameterBLL.UpdateSystemParameter(this.txtParamCode.Text, this.txtParamName.Text, this.txtParamValue.Text, this.txtRemark.Text);
                MessageBox.Show(base.GetMessage("UpdateSuccess"), base.MessageTitle);
                //
                this.BindGridList();
            }
        }
        /// <summary>
        /// 测试邮件服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestSMTP_Click(object sender, EventArgs e)
        {
            bool success = false;
            FrmWait frmwait = new FrmWait(() => {
                AppCode.SysEnvironment.SystemSettings.LoadSettings();
                //
                string mailHost = AppCode.SysEnvironment.SystemSettings["MailHost"];
                string mailFrom = AppCode.SysEnvironment.SystemSettings["MailFrom"];
                string testMailPassword = AppCode.SysEnvironment.SystemSettings["CurrentMailPassword"];
                success = EmailHelper.SendEmail(mailHost, mailFrom.Substring(0, mailFrom.IndexOf("@")), testMailPassword, 
                    new System.Net.Mail.MailAddress(mailFrom), new System.Net.Mail.MailAddress(mailFrom), "租金系统测试邮件", "租金系统测试邮件");
            }, base.GetMessage("Wait"), false);
            frmwait.ShowDialog();
            //
            if (success)
            {
                MessageBox.Show(base.GetMessage("TestEmailResultSuccess"));
            }
            else
            {
                MessageBox.Show(base.GetMessage("TestEmailResultFailure"));
            }
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            GridViewHelper.Print_DataGridView(this.dgvList);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            GridViewHelper.DataGridViewToExcel(this.Text, this.Text, this.dgvList);
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SystemParameterBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        protected void BindGridList()
        {
            base.ExecuteAction(() =>
            {
                this.DTSource = this.SystemParameterBLL.SelectSystemParameter();
                this.DTSource.DefaultView.RowFilter = "IsVisible=1";//--
            }, "获取系统参数出错", "系统参数");
            this.dgvList.DataSource = this.DTSource.DefaultView;
            //
            this.dgvList.Columns.Clear();
            this.dgvList.AutoGenerateColumns = false;
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToOrderColumns = false;
            this.dgvList.ReadOnly = true;
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvList);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ParamCode", "ParamCode", 400);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ParamName", base.GetMessage("ParamName"), 400);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ParamValue", base.GetMessage("ParamValue"), 200);
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Remark", base.GetMessage("Remark"), 250);
            this.dgvList.Columns[0].Visible = false;
        }
    }
}