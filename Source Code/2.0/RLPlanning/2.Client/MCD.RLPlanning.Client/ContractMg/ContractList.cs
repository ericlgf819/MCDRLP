using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.Common;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.BLL.Workflow;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.AppCode;


namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ContractList : BaseList
    {
        /// <summary>
        /// 外部传入的ContractNo
        /// </summary>
        public static string s_strContractNoFromOutSide = null;

        public ContractList()
        {
            InitializeComponent();
            this.dgvList.CellPainting += new DataGridViewCellPaintingEventHandler(dgvList_CellPainting);
        }

        #region 字段和属性声明

        private ContractBLL contractBLL = new ContractBLL();
        private CommonBLL comBLL = new CommonBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();

        #endregion

        #region 重写基类方法

        /// <summary>
        /// 绑定界面控件
        /// </summary>
        protected override void BindFormControl()
        {
            base.BindFormControl();
            //填充下拉框，区域和状态
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

            this.BindContractStatus();
            this.btnAddnew.Visible = true;
            this.btnDelete.Visible = true;

            //来源下拉框初始化
            InitFromSRLSComboBox();
        }

        #region 工具栏按钮事件

        protected override void btnAddnew_Click(object sender, EventArgs e)
        {
            RefreshList = false;
            ContractEdit frm = new ContractEdit()
                {
                    ParentFrm = this,
                    CurrentAction = ActionType.New,
                    CopyType = ContractCopyType.新建,
                    //IsAddNew = true,
                    IsNewWorkflow = true,
                    WorkflowBizStatus = WorkflowBizStatus.草稿,
                };
            frm.ShowDialog();
            if (RefreshList)
            {
                GetDataSource();
            }
        }

        protected override void btnEdit_Click(object sender, EventArgs e)
        {
            base.btnEdit_Click(sender, e);
            if (this.dgvList.CurrentCell == null)
            {
                return;
            }

            DataRowView row = this.dgvList.CurrentRow.DataBoundItem as DataRowView;
            string snapshotID = row["ContractSnapshotID"].ToString();//获取合同快照ID
            string status = row["Status"].ToString();
            string creatorName = row["CreatorName"].ToString();
            string lastModifyUserName = row["LastModifyUserName"].ToString();

            //将合同信息作为参数传递给编辑窗体
            ContractEdit frm = new ContractEdit()
            {
                ParentFrm = this,
                ContractSnapshotID = snapshotID,
                //IsAddNew = false,
                IsNewWorkflow = false,
                DataKey = snapshotID,
                WorkflowBizStatus = Enum.IsDefined(typeof(WorkflowBizStatus),status) 
                    ? (WorkflowBizStatus)Enum.Parse(typeof(WorkflowBizStatus), status) : WorkflowBizStatus.已失效,
            };

            //commented by Eric--Begin
            //if ((status == ContractStatus.草稿.ToString()
            //    || status == ContractStatus.审核退回.ToString())
            //    && lastModifyUserName == AppCode.SysEnvironment.CurrentUser.UserName)//&& creatorName == AppCode.SysEnvironment.CurrentUser.UserName)
            //{
            //    frm.CurrentAction = ActionType.Edit;
            //}
            //else
            //{
            //    frm.CurrentAction = ActionType.View;
            //}
            //commented by Eric--End

            //Added by Eric--Begin
            frm.CurrentAction = ActionType.View;
            //Added by Eric--End

            frm.ShowDialog();
            //if (RefreshList)
            //{
            //    GetDataSource();
            //}
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null)
            {
                return;
            }

            DataRowView row = this.dgvList.CurrentRow.DataBoundItem as DataRowView;
            string snapshotID = row["ContractSnapshotID"].ToString();//获取合同快照ID
            string contractID = row["ContractID"].ToString();
            string status = row["Status"].ToString();
            string creatorName = Convert.ToString(row["CreatorName"]);
            string lastModifyUserName = Convert.ToString(row["LastModifyUserName"]);
            bool isFromSRLS = (bool)row["FromSRLS"];
            //modified by Eric
            //只能删除虚拟合同--Begin
            if (!isFromSRLS)
            {

                if (MessageConfirm(base.GetMessage("ConfirmDeleteContract")) == DialogResult.OK)
                {
                    this.contractBLL.DeleteContract(snapshotID, AppCode.SysEnvironment.CurrentUser.ID.ToString());
                    this.MessageInformation(base.GetMessage("DeleteContractSuccess"));
                    this.GetDataSource();
                }
            }
            //只能删除虚拟合同--End
            else
            {
                this.MessageInformation(base.GetMessage("CanNotDeleteContract"));
            }
        }
        #endregion 工具栏按钮事件

        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            //显示分页
            base.ShowPager = true;

            //定义变量，存储查询参数
            string StoreOrDeptNo = this.txtStoreDeptNo.Text.Trim();
            string VendorNo = this.txtVendorNo.Text.Trim();
            string CompanyNo = this.txtCompanyNo.Text.Trim();
            string ContractNo = this.txtContractNo.Text.Trim();
            string val = string.Empty;
            Guid AreaNo = Guid.Empty;
            bool? bFromSRLS = new bool?();

            if (cbFromSRLS.SelectedValue != null)
            {
                try
                {
                    int iTmp = int.Parse(cbFromSRLS.SelectedValue.ToString());
                    if (0 == iTmp)
                        bFromSRLS = false;
                    else
                        bFromSRLS = true;
                }
                catch
                {
                    ;
                }
            }

            if (ControlHelper.HasComboBoxSelected(new ComboBox[] { this.cmbArea }, out val)
                && this.cmbArea.SelectedValue != null)
            {
                try
                {
                    AreaNo = (Guid)this.cmbArea.SelectedValue;
                }
                catch
                {
                    AreaNo = Guid.Empty;
                }
            }

            string Status = string.Empty;
            if (ControlHelper.HasComboBoxSelected(new ComboBox[] { this.cmbStatus }, out val)
                && this.cmbStatus.SelectedValue != null)
            {
                Status = this.cmbStatus.SelectedValue.ToString();
            }

            //总记录数
            int recordCount = 0;

            //执行查询
            FrmWait frm = new FrmWait(() =>
            {
                base.ExecuteAction(() =>
                {
                    base.DTSource = this.contractBLL.SelectContracts(StoreOrDeptNo, VendorNo, CompanyNo, ContractNo, AreaNo, Status, bFromSRLS, SysEnvironment.CurrentUser.ID, base.CurrentPageIndex, base.PageSize, out recordCount).Tables[0];
                }, base.GetMessage("SearchContractError"), base.GetMessage("SearchContractList"));
            }, base.GetMessage("Wait"), () =>
            {
                this.contractBLL.CloseService();
            });
            frm.ShowDialog();
            base.RecordCount = recordCount;
            base.GetDataSource();
        }

        /// <summary>
        /// 绑定DataGridView控件的列
        /// </summary>
        protected override void BindGridList()
        {
            base.BindGridList();
            //填充列
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ContractSnapshotID", string.Empty, 0);//合同快照ID
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "CompanyCode", base.GetMessage("CompanyCode"), 80);//公司编号
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "ContractNO", base.GetMessage("ContractNO"), 80);//合同编号
            //GridViewHelper.AppendColumnToDataGridView(this.dgvList, "CreateTime", base.GetMessage("CreateTime"), 80);//新增时间
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "LastModifyTime", base.GetMessage("LastModifyTime"), 120);//最后修改时间
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "VendorNo", base.GetMessage("VendorNo"), 80);//业主编号
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "VendorName", base.GetMessage("VendorName"), 150);//业主名称,
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "EntityTypeName", base.GetMessage("EntityTypeName"), 80);//实体类型
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "StoreOrDeptNo", base.GetMessage("StoreOrDeptNo"), 110);//餐厅(部门)编号
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "EntityName", base.GetMessage("EntityName"), 250);//实体名称
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "RentStartDate", base.GetMessage("RentStartDate"), 70, (column) =>
            {
                column.DefaultCellStyle.Format = "yyyy-MM-dd";
            });//起租日
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "RentEndDate", base.GetMessage("RentEndDate"), 100, (column) =>
            {
                column.DefaultCellStyle.Format = "yyyy-MM-dd";
            });//到期日
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "Status", base.GetMessage("Status"), 60);//状态
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "LastModifyUserName", base.GetMessage("LastModifyUserName"),100);//状态
            GridViewHelper.AppendColumnToDataGridView(this.dgvList, "FromSRLS", string.Empty, 0);//是否是虚拟合同
            this.dgvList.Columns["FromSRLS"].Visible = false;                                    //虚拟合同列不可见

            this.dgvList.Columns[0].Visible = false;
            if (this.dgvList.Rows.Count > 0)
            {
                this.dgvList.Rows[0].Selected = true;
                this.dgvList.CurrentCell = this.dgvList.Rows[0].Cells[1];
            }
        }

        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnEdit_Click(sender, e);
        }

        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            contractBLL.Dispose();
            base.BaseFrm_FormClosing(sender, e);
            s_strContractNoFromOutSide = null;
        }

        private int m_PreRowIndex = -1;
        protected override void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentRow != null)
            {
                int currentIndex = this.dgvList.CurrentRow.Index;
                m_PreRowIndex = this.dgvList.CurrentRow.Index;

            }

            base.dgvList_SelectionChanged(sender, e);
        }
        #endregion

        #region 控件事件处理

        private void ContractList_Load(object sender, EventArgs e)
        {
            //如果有外部传入的ContractNo，则需要给控件填入相应的值，再触发搜索
            if (null != s_strContractNoFromOutSide)
            {
                txtContractNo.Text = s_strContractNoFromOutSide;
                GetDataSource();
            }
        }

        //明细，相当于编辑
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentRow != null)
            {
                this.btnEdit_Click(sender, e);
            }
        }

        //变更
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null)
            {
                return;
            }

            DataRowView row = this.dgvList.CurrentRow.DataBoundItem as DataRowView;
            bool isLocked = (bool)row["IsLocked"];

            if (isLocked)
            {
                this.MessageInformation(base.GetMessage("ContractIsLocked"));
                return;
            }
            
            string snapshotID = row["ContractSnapshotID"].ToString();//获取合同快照ID
            string status = row["Status"].ToString();

            //根据状态判断，如果处在审核中则不允许变更，
            //TODO:后续需要添加是否有计算流程的判断
            if (status == ContractStatus.已生效.ToString())
            {
                ContractEdit frm = new ContractEdit()
                    {
                        ParentFrm = this,
                        CopyType = ContractCopyType.变更,
                        CurrentAction = ActionType.New,
                        //IsAddNew = true,
                        IsNewWorkflow = true,
                        WorkflowBizStatus = WorkflowBizStatus.草稿,
                        ContractSnapshotID = snapshotID,
                    };
                //frm.CurrentAction = ActionType.New;
                frm.ShowDialog();
                if (RefreshList)
                {
                    GetDataSource();
                }
            }
            else
            {
                this.MessageInformation(base.GetMessage("NoticeCanNotChangeContract"));
            }
        }

        //续租
        private void btnRelet_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null)
            {
                return;
            }

            DataRowView row = this.dgvList.CurrentRow.DataBoundItem as DataRowView;
            bool isLocked = (bool)row["IsLocked"];

            if (isLocked)
            {
                this.MessageInformation(base.GetMessage("ContractIsLocked"));
                return;
            }

            string snapshotID = row["ContractSnapshotID"].ToString();//获取合同快照ID
            string status = row["Status"].ToString();

            if (status == ContractStatus.已生效.ToString())
            {
                ContractEdit frm = new ContractEdit()
                    {
                        ParentFrm = this,
                        CopyType = ContractCopyType.续租,
                        CurrentAction = ActionType.New,
                        //IsAddNew = true,
                        IsNewWorkflow = true,
                        WorkflowBizStatus = WorkflowBizStatus.草稿,
                        ContractSnapshotID = snapshotID,
                    };
                //frm.CurrentAction = ActionType.New;
                frm.ShowDialog();
                if (RefreshList)
                {
                    GetDataSource();
                }
            }
            else
            {
                this.MessageInformation(base.GetMessage("NoticeCanNotChangeContract"));
            }
        }

        void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //合并单元格
            GridViewHelper.MerageCell(dgv, e, new List<int>() { 1,2,3 });
        } 

        #endregion

        #region 私有方法

        private void BindContractStatus()
        {
            DataTable dtContractStatus = this.GetDictionaryTable("ContractStatus");
            DataTable dt = dtContractStatus.Copy();
            DataRow row = dt.NewRow();
            row["ItemValue"] = "";
            row["ItemName"] = "";
            dt.Rows.InsertAt(row, 0);

            this.cmbStatus.DisplayMember = "ItemName";
            this.cmbStatus.ValueMember = "ItemValue";
            this.cmbStatus.DataSource = dt;
        }

        #endregion

        //手工发起APGL
        private void btnCreateAPGLByHand_Click(object sender, EventArgs e)
        {
            if (MessageConfirm(base.GetMessage("btnCreateAPGLByHandMessage")) == DialogResult.OK)
            {
                //执行查询
                FrmWait frm = new FrmWait(() =>
                {
                   contractBLL.CreateAPGLByHand();
                }, base.GetMessage("Wait"), false);
                frm.ShowDialog();
                MessageBox.Show(base.GetMessage("btnCreateAPGLByHandDone"));
            }
        }

        //撤销删除
        private void btnUnDoDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvList.CurrentCell == null)
            {
                return;
            }

            DataRowView row = this.dgvList.CurrentRow.DataBoundItem as DataRowView;
            string snapshotID = row["ContractSnapshotID"].ToString();//获取合同快照ID
            string status = row["Status"].ToString();

            if (status == "删除中")
            {
                WorkflowBLL bll = new WorkflowBLL();
                bll.Init(WorkflowType.合同删除, snapshotID);

                if (bll.CurrentTask != null)
                {
                    if (!bll.CurrentInstance.CreatorID.Equals(AppCode.SysEnvironment.CurrentUser.ID.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        base.MessageInformation("只有任务的发起人才能执行该操作！");
                        return;
                    }

                    //任务的发起人可以撤销误删除的流程，被退回时可以撤销删除
                    if (bll.CurrentTask.TaskName == "发起" && bll.CurrentTask.IsReject == 1)
                    {
                        if (base.MessageConfirm("确认撤销删除吗？") == DialogResult.OK)
                        {
                            comBLL.CancelBizData(BizType.撤销删除合同, snapshotID, AppCode.SysEnvironment.CurrentUser.ID.ToString());
                            base.MessageInformation("已撤销合同删除流程！");
                            this.GetDataSource();
                        }
                    }
                    else
                    {
                        base.MessageInformation("只有审批被拒绝时的合同删除流程才能撤销！");
                    }
                }
            }
            else
            {
                base.MessageInformation("只有对删除中的合同才允许执行该操作！");
            }
        }

        private void InitFromSRLSComboBox()
        {
            // 绑定来源
            ControlHelper.BindComboBox(cbFromSRLS, base.DTDataFrom, "SourceName", "SourceValue");
            cbFromSRLS.SelectedIndex = 1;
        }
    }
}
