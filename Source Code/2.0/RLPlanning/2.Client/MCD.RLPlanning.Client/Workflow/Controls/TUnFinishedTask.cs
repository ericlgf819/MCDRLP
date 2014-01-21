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
using MCD.RLPlanning.Client.ContractMg;
using MCD.RLPlanning.Client.ForcastSales;
using MCD.RLPlanning.Client.SalesCalculate;
using MCD.RLPlanning.Business.SalesCalculate;


namespace MCD.RLPlanning.Client.Workflow.Task.Controls
{
    public partial class TUnFinishedTask : TSimpleCheck
    {
        public TUnFinishedTask()
        {
            InitializeComponent();
        }

        private SalesCalculateBLL m_scBLL = new SalesCalculateBLL();

        /// <summary>
        /// 将datagridview中，内容为"-"的LinkText，改为普通text
        /// </summary>
        private void ChangeLinkText2Text()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (null == item.Cells["Operation"].Value)
                    continue;

                if ("-" == item.Cells["Operation"].Value.ToString())
                {
                    item.Cells["Operation"] = new DataGridViewTextBoxCell();
                    item.Cells["Operation"].Value = "-";
                }
            }
        }

        /// <summary>
        /// 初始化GridView
        /// </summary>
        protected override void InitGridView()
        {
            base.InitGridView();

            //绑定列头
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "IsRead", GetMessage("IsRead"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "TaskType", GetMessage("TaskType"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "StoreNo", GetMessage("StoreNo"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "KioskNo", GetMessage("KioskNo"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ErrorType", GetMessage("ErrorType"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CalEndDate", GetMessage("CalEndDate"), 200);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CreateTime", GetMessage("CreateTime"), 200);
            GridViewHelper.AppendColumnToDataGridView<DataGridViewLinkColumn>(dataGridView1, "Operation", GetMessage("Operation"));
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CheckID", "CheckID", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "TaskNo", "TaskNo", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "Remark", "Remark", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ContractNo", "ContractNo", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "CompanyCode", "CompanyCode", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "ContractSnapShotID", "ContractSnapShotID", 0);
            GridViewHelper.AppendColumnToDataGridView(dataGridView1, "EntityID", "EntityID", 0);

            //将ID,TaskNo,Remark,ContractNo, CompanyCode, ContractSnapShotID, EntityID列给隐藏起来
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
            string strStoreNo, strTaskType, strReadStatus;
            
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

            if (null == cmbReadStatus.SelectedItem)
                strReadStatus = string.Empty;
            else
                strReadStatus = ((ComboBoxItem)cmbReadStatus.SelectedItem).Value;

            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != taskBLL)
                    {
                        ds = taskBLL.SelectUnFinishedTask(strArea, strCompanyCode,
                             strStoreNo, strTaskType, strQuestType,
                             strReadStatus, iTimeZoneFlag, SysEnvironment.CurrentUser.ID);
                    }
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

                //将所有的"-"的LinkText改为普通Text
                ChangeLinkText2Text();
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

        /// <summary>
        /// 双击弹出明细框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Guid taskID = new Guid(dataGridView1.Rows[e.RowIndex].Cells["CheckID"].Value.ToString());

                TaskDetail frm = new TaskDetail(taskID) 
                {
                    m_strCreateTime = dataGridView1.Rows[e.RowIndex].Cells["CreateTime"].Value.ToString(),
                    m_strKioskNo = dataGridView1.Rows[e.RowIndex].Cells["KioskNo"].Value.ToString(),
                    m_strStoreNo = dataGridView1.Rows[e.RowIndex].Cells["StoreNo"].Value.ToString(),
                    m_strTaskType = dataGridView1.Rows[e.RowIndex].Cells["TaskType"].Value.ToString(),
                    m_strQuestType = dataGridView1.Rows[e.RowIndex].Cells["ErrorType"].Value.ToString(),
                    m_strRemark = dataGridView1.Rows[e.RowIndex].Cells["Remark"].Value.ToString(),
                    m_strTaskNo = dataGridView1.Rows[e.RowIndex].Cells["TaskNo"].Value.ToString(),
                    m_strIsRead = dataGridView1.Rows[e.RowIndex].Cells["IsRead"].Value.ToString()
                };
                frm.ShowDialog();

                //关闭明细界面后如果有remark更新过，则需要再触发搜索来更新remark
                if (frm.m_isRemarkChanged)
                    BindGridView();
            }
        }

        protected override void dataGridView1_Sorted(object sender, EventArgs e)
        {
            //将所有的"-"的LinkText改为普通Text
            ChangeLinkText2Text();
        }

        /// <summary>
        /// 重新计算。
        /// </summary>
        public override void Recaculate()
        {
            //获取全部有权限可见的E型任务
            DataSet ds = null;

            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != taskBLL)
                    {
                        ds = taskBLL.SelectUnFinishedTask(string.Empty, string.Empty,
                             string.Empty, string.Empty, string.Empty,
                             string.Empty, 3, SysEnvironment.CurrentUser.ID);
                    }
                });
            }, base.GetMessage("Wait"), () =>
            {
                this.taskBLL.CloseService();
            });
            frm.ShowDialog();

            //将任务中的StoreNo与KioskNo存储下来，发起租金计算
            DataTable calParmTbl = ConvertTaskItem2Table(ds);
            //计算
            if (null != calParmTbl)
                CalCulate(calParmTbl);
        }

        /// <summary>
        /// 需要计算的餐厅与甜品店和对应的计算时间存入Datatable中
        /// </summary>
        /// <param name="strCalEndDate">计算结束时间</param>
        /// <returns></returns>
        private DataTable ConvertTaskItem2Table(DataSet ds)
        {
            //清空上一次的残留数据
            CalculateResult.s_hsAllStoreKioskNo.Clear();

            //如果没有任务，则直接返回了
            if (null == ds || 0 == ds.Tables.Count || 0 == ds.Tables[0].Rows.Count)
                return null;

            DataTable dt = new DataTable();
            dt.TableName = "RLPlanning_Cal_TmpTbl";
            dt.Columns.Add("StoreNo");
            dt.Columns.Add("KioskNo");
            dt.Columns.Add("CalEndDate");

            string strStoreNo = string.Empty;
            string strKioskNo = string.Empty;
            string strCalEndDate = string.Empty;

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                strStoreNo = item["StoreNo"].ToString();
                strKioskNo = item["KioskNo"].ToString();
                strCalEndDate = item["CalEndDate"].ToString();

                dt.Rows.Add(strStoreNo, strKioskNo, strCalEndDate);

                //将选中的要计算的餐厅和甜品店信息暂存下来，供计算结果使用
                CalculateResult.SStoreKioskPair skpair = new CalculateResult.SStoreKioskPair()
                {
                    m_strStoreNo = strStoreNo,
                    m_strKioskNo = strKioskNo,
                    m_strCompanyCode = item["CompanyCode"].ToString(),
                    m_strCompanyName = item["CompanyName"].ToString(),
                    m_strEntityName = item["EntityName"].ToString(),
                    m_strEntityType = item["EntityType"].ToString()
                };

                CalculateResult.s_hsAllStoreKioskNo.Add(skpair);
            }
            return dt;
        }

        /// <summary>
        /// 计算租金
        /// </summary>
        private void CalCulate(DataTable parmTbl)
        {
            //计算开始时间
            DateTime calStartTime = DateTime.Now;
            if (null != m_scBLL)
            {
                calStartTime = m_scBLL.GetServerTime();
            }

            byte[] byteEntitysInCal = null;
            string exceptionErr = null;

            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != m_scBLL)
                    {
                        m_scBLL.Calculate(SysEnvironment.CurrentUser.ID, parmTbl, out byteEntitysInCal, out exceptionErr);
                    }
                });
            }, base.GetMessage("Wait"), () =>
            {
                this.m_scBLL.CloseService();
            });
            frm.ShowDialog();

            try
            {
                //显示计算结果
                if (ParentForm.ParentForm is FrmMain)
                {
                    //传递必要的参数
                    CalculateResult.s_calStartDate = calStartTime;
                    CalculateResult.s_operatorID = SysEnvironment.CurrentUser.ID;

                    //打开计算结果界面
                    ((FrmMain)ParentForm.ParentForm).CloseForm("CalculateResult");
                    ((FrmMain)ParentForm.ParentForm).OpenForm("CalculateResult");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace);
            }
        }
    }
}
