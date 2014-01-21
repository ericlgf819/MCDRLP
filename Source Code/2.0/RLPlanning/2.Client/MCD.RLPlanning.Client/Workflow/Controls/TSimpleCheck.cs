using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.RLPlanning.BLL.Workflow;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.Master;
using MCD.Common;
using MCD.Common.SRLS;
using MCD.Controls;
using MCD.RLPlanning.Client.Workflow.Controls;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Task;


namespace MCD.RLPlanning.Client.Workflow.Task.Controls
{
    /// <summary>
    /// 简单审批任务待办列表。
    /// </summary>
    public partial class TSimpleCheck : TaskListBase
    {
        protected const string s_MissingContractErr = "合同缺失";
        protected const string s_ContractPeriodErr = "合同期限不全";
        protected const string s_SalesErr = "Sales数据不全";
        protected const string s_SalesString = "Sales";
        protected const string s_ContractString = "合同";


        public TSimpleCheck()
        {
            InitializeComponent();

            taskBLL = new TaskBLL();
            UserCompanyBLL = new UserCompanyBLL();
        }

        /// <summary>
        /// 任务搜索BLL
        /// </summary>
        protected TaskBLL taskBLL = null;

        /// <summary>
        /// 搜索当前用户有权限的公司、区域BLL
        /// </summary>
        protected UserCompanyBLL UserCompanyBLL = null;

        /// <summary>
        /// 时间纬度, 0 当天, 1 当月, 2今年, 3往年
        /// </summary>
        public int iTimeZoneFlag = 0;

        /// <summary>
        /// 第一次加载数据。
        /// </summary>
        public override void LoadData()
        {
            //Area控件
            InitAreaComboBox();
            //初始化任务类型
            InitTaskTypeComboBox();
            //初始化问题类型控件
            InitQuestTypeComboBox();
            //阅读状态控件
            InitReadStatusComboBox();
            //初始化GridView表头
            InitGridView();
            //查询
            BindGridView();
        }

        /// <summary>
        /// 初始化GridView
        /// </summary>
        protected virtual void InitGridView()
        {
            this.dataGridView1.Columns.Clear();

            //自动行列距离调整
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.MultiSelect = false;
            GridViewHelper.PaintRowIndexToHeaderCell(this.dataGridView1);
        }

        /// <summary>
        /// 初始化任务类型控件
        /// </summary>
        protected virtual void InitTaskTypeComboBox()
        {
            ComboBoxItem item = new ComboBoxItem(GetMessage("TaskType1"), "E");
            this.cmbTaskType.Items.Add(item);
            item = new ComboBoxItem(GetMessage("TaskType0"), "I");
            this.cmbTaskType.Items.Add(item);
            item = new ComboBoxItem("", "");
            this.cmbTaskType.Items.Insert(0, item);
        }

        /// <summary>
        /// 初始化问题类型控件
        /// </summary>
        protected virtual void InitQuestTypeComboBox()
        {
            ComboBoxItem item = new ComboBoxItem(GetMessage("QuestType0"), s_MissingContractErr);
            //合同期限不全的问题不做为基本的错误类型
            //this.cmbquestType.Items.Add(item);
            //item = new ComboBoxItem(GetMessage("QuestType1"), s_ContractPeriodErr);
            this.cmbquestType.Items.Add(item);
            item = new ComboBoxItem(GetMessage("QuestType2"), s_SalesErr);
            this.cmbquestType.Items.Add(item);
            item = new ComboBoxItem("", "");
            this.cmbquestType.Items.Insert(0, item);
        }

        /// <summary>
        /// 初始化阅读状态控件
        /// </summary>
        protected virtual void InitReadStatusComboBox()
        {
            ComboBoxItem item = new ComboBoxItem(GetMessage("ReadInfo1"), "已读");
            this.cmbReadStatus.Items.Add(item);
            item = new ComboBoxItem(GetMessage("ReadInfo0"), "未读");
            this.cmbReadStatus.Items.Add(item);
            item = new ComboBoxItem("", "");
            this.cmbReadStatus.Items.Insert(0, item);
        }

        /// <summary>
        /// 初始化Area控件
        /// </summary>
        protected virtual void InitAreaComboBox()
        {
            // 绑定区域

            DataSet dsUserArea = UserCompanyBLL.SelectUserArea(new UserCompanyEntity()
            {
                UserId = AppCode.SysEnvironment.CurrentUser.ID
            });
            if (dsUserArea != null && dsUserArea.Tables.Count == 1)
            {
                DataTable dtUserArea = dsUserArea.Tables[0];
                DataRow row = dtUserArea.NewRow();
                row["AreaName"] = string.Empty;
                row["AreaID"] = DBNull.Value;
                dtUserArea.Rows.InsertAt(row, 0);
                //
                ControlHelper.BindComboBox(cmbArea, dtUserArea, "AreaName", "AreaID");
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public override void BindGridView()
        {

        }

        protected virtual void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (this.dataGridView1.Columns[e.ColumnIndex].Name == "IsRead")
            //{
            //    if (e.Value.ToString() == "1")
            //    {
            //        e.Value = base.GetMessage("ReadInfo1");//已读
            //        e.CellStyle.Font = new Font(this.Font, FontStyle.Regular);
            //    }
            //    else
            //    {
            //        e.Value = base.GetMessage("ReadInfo0");//未读
            //        e.CellStyle.Font = new Font(this.Font, FontStyle.Bold);
            //    }
            //    //this.dataGridView1.Rows[e.RowIndex].Cells["ProcID"].Style.Font = e.CellStyle.Font;
            //}
        }
        /// <summary>
        /// 单击单元格中的CheckBox的时候选中或者取消选择。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected virtual void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            cmbArea.SelectedIndex = 0;
            cmbCompany.SelectedIndex = 0;
            txtStoreNo.Text = string.Empty;
            cmbTaskType.SelectedIndex = 0;
            cmbquestType.SelectedIndex = 0;
            cmbReadStatus.SelectedIndex = 0;

            //
            this.BindGridView();
        }
        /// <summary>
        /// 刷新列表。
        /// </summary>
        public override void Refresh()
        {
            this.BindGridView();
        }


        protected virtual void dataGridView1_Sorted(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 更新修正任务的用户ID与时间
        /// </summary>
        protected void UpdateFixUserIDAndTime(DataGridViewCell cell)
        {
            Guid taskID = new Guid(cell.Value.ToString());

            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != taskBLL)
                    {
                        taskBLL.UpdateTaskFinishUserIDAndTime(taskID, SysEnvironment.CurrentUser.ID,
                                    taskBLL.GetServerTime());
                    }
                });
            }, base.GetMessage("Wait"), () =>
            {
                this.taskBLL.CloseService();
            });
            frm.ShowDialog();
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid? AreaId = null;
            if (this.cmbArea.SelectedValue + string.Empty != string.Empty)
            {
                AreaId = new Guid(this.cmbArea.SelectedValue + string.Empty);
            }
            string company = string.Empty;
            if (this.cmbCompany.SelectedValue + string.Empty != string.Empty)
            {
                company = this.cmbCompany.SelectedValue + string.Empty;
            }
            //
            DataSet dsCompany = this.UserCompanyBLL.SelectUserCompany(new UserCompanyEntity()
            {
                AreaId = AreaId,
                Status = 'A',
                UserId = SysEnvironment.CurrentUser.ID
            });
            if (dsCompany != null && dsCompany.Tables.Count > 0)
            {
                DataTable dt = dsCompany.Tables[0];
                dt.Rows.InsertAt(dt.NewRow(), 0);
                //
                this.cmbCompany.DataSource = null;
                ControlHelper.BindComboBox(this.cmbCompany, dt, "CompanyName", "CompanyCode");
            }
        }
    }
}