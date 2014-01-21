using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.Common;
using MCD.Controls;
using MCD.RLPlanning.Client;
using MCD.RLPlanning.Client.AppCode;
using MCD.RLPlanning.Client.Workflow.Task.Controls;
using MCD.RLPlanning.Client.ContractMg;
using MCD.RLPlanning.Client.ForcastSales;
using MCD.RLPlanning.Client.SalesCalculate;
using MCD.RLPlanning.Business.SalesCalculate;

namespace MCD.RLPlanning.Client.Workflow.Controls
{
    public partial class TFinishedTask : TSimpleCheck
    {
        public TFinishedTask()
        {
            InitializeComponent();

            //将阅读状态的控件给隐藏掉
            lblReadStatus.Visible = false;
            cmbReadStatus.Visible = false;
            //将重新计算按钮隐藏起来
            btnRecaculate.Visible = false;
        }

        /// <summary>
        /// 初始化GridView
        /// </summary>
        protected override void InitGridView()
        {
            base.InitGridView();

            //绑定列头
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "TaskType", GetMessage("TaskType"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "StoreNo", GetMessage("StoreNo"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "KioskNo", GetMessage("KioskNo"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ErrorType", GetMessage("ErrorType"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CalEndDate", GetMessage("CalEndDate"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "FixUserName", GetMessage("FixUserName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CreateTime", GetMessage("CreateTime"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "TaskFinishTime", GetMessage("FinishTime"), 200);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewLinkColumn>(dataGridView1, "Operation", GetMessage("Operation"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CheckID", "CheckID", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "TaskNo", "TaskNo", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "Remark", "Remark", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ContractNo", "ContractNo", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CompanyCode", "CompanyCode", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ContractSnapShotID", "ContractSnapShotID", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "EntityID", "EntityID", 0);

            //将TaskNo,Remark列给隐藏起来
            dataGridView1.Columns["CheckID"].Visible = false;
            dataGridView1.Columns["TaskNo"].Visible = false;
            dataGridView1.Columns["Remark"].Visible = false;
            dataGridView1.Columns["ContractNo"].Visible = false;
            dataGridView1.Columns["CompanyCode"].Visible = false;
            dataGridView1.Columns["ContractSnapShotID"].Visible = false;
            dataGridView1.Columns["EntityID"].Visible = false;

            //将CalEndDate列隐藏起来
            dataGridView1.Columns["CalEndDate"].Visible = false;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public override void BindGridView()
        {
            //执行查询
            DataSet ds = null;

            string strArea, strCompanyCode, strQuestType;
            string strStoreNo, strTaskType;

            strArea = cmbArea.SelectedText;

            if (null == cmbCompany.SelectedValue)
                strCompanyCode = string.Empty;
            else
                strCompanyCode = cmbCompany.SelectedValue.ToString();

            if (null == cmbquestType.SelectedItem)
                strQuestType = string.Empty;
            else
                strQuestType = ((ComboBoxItem)cmbquestType.SelectedItem).Value;

            strStoreNo = txtStoreNo.Text;

            if (null == cmbTaskType.SelectedItem)
                strTaskType = string.Empty;
            else
                strTaskType = ((ComboBoxItem)cmbTaskType.SelectedItem).Value;

            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    ds = taskBLL.SelectFinishedTask(strArea, strCompanyCode,
                     strStoreNo, strTaskType, strQuestType,
                     iTimeZoneFlag, SysEnvironment.CurrentUser.ID);
                });
            }, base.GetMessage("Wait"), () =>
            {
                this.taskBLL.CloseService();
            });
            frm.ShowDialog();

            if (null != ds && ds.Tables.Count > 0)
            {
                //gridview绑定
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        /// <summary>
        /// 双击弹出明细框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FinishedTaskDetail frm = new FinishedTaskDetail();
                frm.m_strCreateTime = dataGridView1.Rows[e.RowIndex].Cells["CreateTime"].Value.ToString();
                frm.m_strKioskNo = dataGridView1.Rows[e.RowIndex].Cells["KioskNo"].Value.ToString();
                frm.m_strStoreNo = dataGridView1.Rows[e.RowIndex].Cells["StoreNo"].Value.ToString();
                frm.m_strTaskType = dataGridView1.Rows[e.RowIndex].Cells["TaskType"].Value.ToString();
                frm.m_strQuestType = dataGridView1.Rows[e.RowIndex].Cells["ErrorType"].Value.ToString();
                frm.m_strRemark = dataGridView1.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
                frm.m_strTaskNo = dataGridView1.Rows[e.RowIndex].Cells["TaskNo"].Value.ToString();
                frm.m_strFixUserName = dataGridView1.Rows[e.RowIndex].Cells["FixUserName"].Value.ToString();
                frm.m_strFinishTime = dataGridView1.Rows[e.RowIndex].Cells["TaskFinishTime"].Value.ToString();
                frm.ShowDialog();
            }
        }

        protected override void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
                {
                    string strType = dataGridView1.Rows[e.RowIndex].Cells["ErrorType"].Value.ToString();
                    string strStoreNo = dataGridView1.Rows[e.RowIndex].Cells["StoreNo"].Value.ToString();
                    string strKioskNo = dataGridView1.Rows[e.RowIndex].Cells["KioskNo"].Value.ToString();
                    //string strContractNo = dataGridView1.Rows[e.RowIndex].Cells["ContractNo"].Value.ToString();
                    string strCompanyCode = dataGridView1.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();

                    string strContractSnapshotID =
                                dataGridView1.Rows[e.RowIndex].Cells["ContractSnapShotID"].Value == null ?
                                string.Empty : dataGridView1.Rows[e.RowIndex].Cells["ContractSnapShotID"].Value.ToString();

                    string strEntityID = dataGridView1.Rows[e.RowIndex].Cells["EntityID"].Value == null ?
                        string.Empty : dataGridView1.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();

                    //更新修正人ID与任务完成时间
                    UpdateFixUserIDAndTime(dataGridView1.Rows[e.RowIndex].Cells["CheckID"]);

                    switch (strType)
                    {
                        //合同期限不全
                        case s_ContractPeriodErr:
                            {
                                if (ParentForm.ParentForm is FrmMain)
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
                                if (ParentForm.ParentForm is FrmMain)
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

                    //处理I事件
                    try
                    {
                        string strTaskType = dataGridView1.Rows[e.RowIndex].Cells["TaskType"].Value.ToString();
                        if ("I" == strTaskType)
                        {
                            //有Sales相关的Information，直接打开sales录入界面
                            if (strType.Contains(s_SalesString))
                            {
                                KeyInBox frm = new KeyInBox(strStoreNo, strKioskNo);
                                frm.ShowDialog();
                            }
                            //有合同相关的Information, 直接打开合同变更界面
                            else if (strType.Contains(s_ContractString))
                            {
                                //变更合同
                                ContractEdit.ChangeContract(strContractSnapshotID, strEntityID, ParentForm);
                            }
                        }
                    }
                    catch { } //不做处理
                }
            }
        }
    }
}
