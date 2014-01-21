using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.Client;
using MCD.RLPlanning.Business.SalesCalculate;
using MCD.Common;
using MCD.RLPlanning.Client.ContractMg;
using MCD.RLPlanning.Client.ForcastSales;
using MCD.RLPlanning.BLL.Task;
using MCD.RLPlanning.Client.AppCode;


namespace MCD.RLPlanning.Client.SalesCalculate
{
    public partial class CalculateResult : BaseFrm
    {
        private const string s_cstrModifyContract = "修改合同";
        private const string s_cstrSalesInput = "录入销售数据";

        protected const string s_MissingContractErr = "合同缺失";
        protected const string s_ContractPeriodErr = "合同期限不全";
        protected const string s_SalesErr = "Sales数据不全";

        //计算开始时间，用来筛选错误的计算结果
        public static DateTime s_calStartDate;
        //发起计算的操作员ID
        public static Guid s_operatorID;

        public struct SStoreKioskPair
        {
            public string m_strStoreNo;
            public string m_strKioskNo;
            public string m_strCompanyCode;
            public string m_strCompanyName;
            public string m_strEntityType;
            public string m_strEntityName;
        }
        public static HashSet<SStoreKioskPair> s_hsAllStoreKioskNo = new HashSet<SStoreKioskPair>();

        private SalesCalculateBLL salesBLL = null;

        protected TaskBLL taskBLL = new TaskBLL();

        public CalculateResult()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击修正错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCalculateResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvCalculateResult.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
                {
                    string strType = dgvCalculateResult.Rows[e.RowIndex].Cells["ErrorType"].Value.ToString();
                    string strStoreNo = dgvCalculateResult.Rows[e.RowIndex].Cells["StoreNo"].Value.ToString();
                    string strKioskNo = dgvCalculateResult.Rows[e.RowIndex].Cells["KioskNo"].Value.ToString();
                    //string strContractNo = dataGridView1.Rows[e.RowIndex].Cells["ContractNo"].Value.ToString();
                    string strCompanyCode = dgvCalculateResult.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();

                    string strContractSnapshotID =
                            dgvCalculateResult.Rows[e.RowIndex].Cells["ContractSnapShotID"].Value == null ?
                            string.Empty : dgvCalculateResult.Rows[e.RowIndex].Cells["ContractSnapShotID"].Value.ToString();
                    string strEntityID = dgvCalculateResult.Rows[e.RowIndex].Cells["EntityID"].Value == null ?
                        string.Empty : dgvCalculateResult.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();

                    //更新修正人ID与任务完成时间
                    UpdateFixUserIDAndTime(dgvCalculateResult.Rows[e.RowIndex].Cells["CheckID"]);

                    switch (strType)
                    {
                        //合同期限不全
                        case s_ContractPeriodErr:
                            {
                                if (null != ParentForm)
                                {
                                    ////如果合同界面已经打开，则先关闭
                                    //((FrmMain)ParentForm.ParentForm).CloseForm("ContractList");
                                    ////将合同编号传入
                                    //ContractList.s_strContractNoFromOutSide = strContractNo;
                                    ////调用合同界面
                                    //((FrmMain)ParentForm.ParentForm).OpenForm("ContractList");

                                    //变更合同
                                    ContractEdit.ChangeContract(strContractSnapshotID, strEntityID, ParentForm);
                                }

                                break;
                            }
                        //合同缺失
                        case s_MissingContractErr:
                            {
                                if (null != ParentForm)
                                {
                                    ////如果合同界面已经打开，则先关闭
                                    //((FrmMain)ParentForm.ParentForm).CloseForm("ContractList");
                                    ////将合同编号清空
                                    //ContractList.s_strContractNoFromOutSide = null;
                                    ////调用合同界面
                                    //((FrmMain)ParentForm.ParentForm).OpenForm("ContractList");

                                    //创建新合同
                                    ContractEdit.CreateNewContract(strStoreNo, strKioskNo, strCompanyCode, ParentForm);
                                }

                                break;
                            }
                        //sales缺失
                        case s_SalesErr:
                            {
                                KeyInBox frm = new KeyInBox(strStoreNo, strKioskNo);
                                frm.ShowDialog();
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 更新修正任务的用户ID与时间
        /// </summary>
        private void UpdateFixUserIDAndTime(DataGridViewCell cell)
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
                }, "CalculateResult", "CalculateResultErr");
            }, base.GetMessage("Wait"));
            frm.ShowDialog();
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitDataGridView()
        {
            dgvCalculateResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCalculateResult.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCalculateResult.AutoGenerateColumns = false;
            dgvCalculateResult.AllowUserToAddRows = false;
            dgvCalculateResult.ReadOnly = true;
            dgvCalculateResult.AllowUserToOrderColumns = true;
            dgvCalculateResult.MultiSelect = false;

            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "CompanyCode", GetMessage("CompanyCode"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "CompanyName", GetMessage("CompanyName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "StoreNo", GetMessage("StoreNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "EntityType", GetMessage("EntityType"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "EntityName", GetMessage("EntityName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "ErrorType", GetMessage("ErrorType"), 100);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewLinkColumn>(dgvCalculateResult, "Operation", GetMessage("Operation"), 100);


            //不显示的列
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "KioskNo", "KioskNo");
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "ContractNo", "ContractNo");
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "ContractSnapShotID", "ContractSnapShotID", 0);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "EntityID", "EntityID", 0);
            GridViewHelper.AppendColumnToDataGridView(dgvCalculateResult, "CheckID", "CheckID", 0);
            dgvCalculateResult.Columns["KioskNo"].Visible = false;
            dgvCalculateResult.Columns["ContractNo"].Visible = false;
            dgvCalculateResult.Columns["ContractSnapShotID"].Visible = false;
            dgvCalculateResult.Columns["EntityID"].Visible = false;
            dgvCalculateResult.Columns["CheckID"].Visible = false;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = null;
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != salesBLL)
                    {
                        ds = salesBLL.SelectCalculateResult(s_operatorID, s_calStartDate);
                    }
                }, "CalculateResult", "CalculateResultErr");
            }, base.GetMessage("Wait"));
            frm.ShowDialog();

            if (null != ds && ds.Tables.Count > 0)
            {
                InsertCorrectResult(ds.Tables[0]);
                dgvCalculateResult.DataSource = ds.Tables[0];
            }
        }

        /// <summary>
        /// 将计算正确的信息，插入datatable中去
        /// </summary>
        private void InsertCorrectResult(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                string strStoreNo = item["StoreNo"].ToString();
                string strKioskNo = string.Empty;

                if (null != item["KioskNo"])
                    strKioskNo = item["KioskNo"].ToString();

                SStoreKioskPair needtodel = new SStoreKioskPair();
                foreach (var i in s_hsAllStoreKioskNo)
                {
                    if (i.m_strStoreNo == strStoreNo && i.m_strKioskNo == strKioskNo)
                    {
                        needtodel = i;
                        break;
                    }
                }

                s_hsAllStoreKioskNo.Remove(needtodel);
            }

            //s_hsAllStoreKioskNo中剩下的就是正确计算的结果
            foreach (var pair in s_hsAllStoreKioskNo)
            {
                dt.Rows.Add(pair.m_strCompanyCode, pair.m_strCompanyName, pair.m_strStoreNo,
                            pair.m_strEntityType, pair.m_strEntityName, "计算成功", "");
            }
        }

        /// <summary>
        /// 界面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateResult_Load(object sender, EventArgs e)
        {
            salesBLL = new SalesCalculateBLL();
            InitDataGridView();
            BindGridView();
        }

        /// <summary>
        /// 界面销毁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != salesBLL)
                salesBLL.Dispose();
        }
    }
}
