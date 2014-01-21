using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.Workflow.Controls
{
    /// <summary>
    /// 待办任务基类。
    /// </summary>
    public partial class TaskListBase : BaseUserControl
    {
        /// <summary>
        /// 获取或设置重新计算按钮的可见性。
        /// </summary>
        public bool ShowRecaculateButton
        {
            get
            {
                return this.btnRecaculate.Visible;
            }
            set
            {
                this.btnRecaculate.Visible = value;
            }
        }
        /// <summary>
        /// 获取或设置重新计算按钮的可见性。
        /// </summary>
        public bool ShowBatchSubmitButton
        {
            get
            { 
                return this.btnBatchSubmit.Visible;
            }
            set
            { 
                this.btnBatchSubmit.Visible = value;
            }
        }
        /// <summary>
        /// 获取或设置生成凭证按钮的可见性。
        /// </summary>
        public bool ShowGenCertButton
        {
            get
            {
                return this.btnGenCertificate.Visible;
            }
            set
            {
                this.btnGenCertificate.Visible = value;
            }
        }
        #region ctor

        public TaskListBase()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 工具栏按钮事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            switch (button.Name)
            {
                case "btnSearch":
                    this.BindGridView();
                    break;
                case "btnReset":
                    this.Reset();
                    break;
                case "btnPrintReview":
                    this.PrintPreview();
                    break;
                case "btnExport":
                    this.ExportToExcel();
                    break;
                case "btnRecaculate":
                    this.Recaculate();
                    break;
                case "btnBatchSubmit":
                    this.BatchSubmit();
                    break;
                case "btnGenCertificate":
                    this.GenarateCertificate();
                    break;
                case "btnCalculate":
                    this.CalCulate();
                    break;
                default:
                    break;
            }
        }

        #region 按钮事件

        /// <summary>
        /// 在派生类中重写以加载数据。
        /// </summary>
        public virtual void LoadData()
        {

        }

        /// <summary>
        /// 查询。
        /// </summary>
        public virtual void BindGridView()
        {

        }

        /// <summary>
        /// 重置。
        /// </summary>
        public virtual void Reset()
        {

        }

        /// <summary>
        /// 重新计算。
        /// </summary>
        public virtual void Recaculate()
        {

        }

        /// <summary>
        /// 批量提交。
        /// </summary>
        public virtual void BatchSubmit()
        {

        }

        /// <summary>
        /// 生成凭证。
        /// </summary>
        public virtual void GenarateCertificate()
        {

        }

        /// <summary>
        /// 计算租金
        /// </summary>
        public virtual void CalCulate()
        {
 
        }

        /// <summary>
        /// 打印预览。
        /// </summary>
        public virtual void PrintPreview()
        {
            List<Control> ctrls = MCD.Common.ControlHelper.FindControl(this, ctrl => { 
                return ctrl is DataGridView && ctrl.Name != "dgvCompanySum"; });
            if (ctrls != null && ctrls.Count > 0)
            {
                MCD.Common.GridViewHelper.Print_DataGridView(ctrls[0] as DataGridView);
            }
        }

        /// <summary>
        /// 文本框回车事件，触动查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGridView();
            }
        }

        /// <summary>
        /// 导出到Excel。
        /// </summary>
        public virtual void ExportToExcel()
        {
            List<Control> ctrls = MCD.Common.ControlHelper.FindControl(this, ctrl => { 
                return ctrl is DataGridView && ctrl.Name != "dgvCompanySum"; });
            if (ctrls != null && ctrls.Count > 0)
            {
                MCD.Common.GridViewHelper.DataGridViewToExcel(base.ParentForm.Text, "Sheet1", ctrls[0] as DataGridView);
            }
        }
        #endregion

        private void TaskListBase_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox)
                {
                    ctrl.KeyDown += new KeyEventHandler(this.ctrl_KeyDown);
                }
            }
        }
    }
}