using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.Controls;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.Client.AppCode;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ContractEdit : BaseWorkflow
    {
        #region ctor

        public ContractEdit()
        {
            InitializeComponent();
            //this.IsContractSaved = false;
            this.CopyType = ContractCopyType.新建;
        }
        #endregion

        #region 字段和属性声明

        private CompanyBLL companyBLL;
        private ContractBLL contractBLL;
        private EntityBLL entityBLL;
        private FixedRuleBLL fixedRuleBLL;
        private RatioRuleBLL ratioRuleBLL;
        private TypeCodeBLL typeCodeBLL;
        private UserCompanyBLL UserCompanyBLL;

        /// <summary>
        /// 当前合同的快照ID
        /// </summary>
        public string ContractSnapshotID { get; set; }        

        /// <summary>
        /// 当前合同复制状态
        /// </summary>
        public ContractCopyType CopyType
        {
            get
            {
                return AppCode.SysEnvironment.ContractCopyType;
            }
            set
            {
                AppCode.SysEnvironment.ContractCopyType = value;
            }
        }

        //Added by Eric
        //--Begin
        /// <summary>
        /// 从外部传入的company code
        /// </summary>
        public string CompanyCodeFromOutSide = string.Empty;

        /// <summary>
        /// 从外部传入的store number
        /// </summary>
        public string StoreNoFromOutSide = string.Empty;

        /// <summary>
        /// 从外部传入的kiosk number
        /// </summary>
        public string KioskNoFromOutSide = string.Empty;

        /// <summary>
        /// 从外部传入的EntityID
        /// </summary>
        public string EntityIDFromOutSide = string.Empty;
        //--End

        /// <summary>
        /// 租金规则信息
        /// </summary>
        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        private ContractEntity m_CurrentContract;
        /// <summary>
        /// 当前编辑的合同对象
        /// </summary>
        public ContractEntity CurrentContract
        {
            get
            {
                return this.m_CurrentContract;
            }
            set
            {
                if (this.m_CurrentContract != value)
                {
                    this.m_CurrentContract = value;
                    if (this.m_CurrentContract != null && this.m_CurrentContract.PartComment != null)
                    {
                        try
                        {
                            this.CopyType = (ContractCopyType)Enum.Parse(typeof(ContractCopyType), this.m_CurrentContract.PartComment);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否需要刷新规则
        /// </summary>
        public bool ShouldRefreshRule { get; set; }

        #endregion 字段和属性声明

        #region 重写基类方法

        /// <summary>
        /// 正在綁定控件
        /// </summary>
        private bool m_IsBinding = true;
        protected override void BindFormControl(string datakey)
        {
            this.m_IsBinding = true;
            this.InitialBLL();

            if (!string.IsNullOrEmpty(datakey) && datakey.Trim().Length > 0)
            {
                this.ContractSnapshotID = datakey;
            }

            this.dgvEntity.AutoGenerateColumns = false;
            this.dgvVendor.AutoGenerateColumns = false;

            this.FillCompanyComboBox();

            this.InitialGridColumns();

            if (string.IsNullOrEmpty(this.ContractSnapshotID) || this.ContractSnapshotID.Trim().Length == 0)
            {
                //this.m_IsAddNew = true;
                this.CurrentAction = ActionType.New;
            }

            //填充列表数据
            if (this.CurrentAction == ActionType.New)
            {
                this.CreateNewContract();
            }
            else
            {
                this.CurrentContract = this.contractBLL.GetSingleContract(this.ContractSnapshotID);
            }

            //added by Eric -- Begin
            AppCode.SysEnvironment.s_mapIsLastEntityRentDateOverlap.Clear();
            AppCode.SysEnvironment.s_bugFixed = false;
            //added by Eric -- End
            AppCode.SysEnvironment.EditingContractSnapshotID = this.CurrentContract.ContractSnapshotID;
            AppCode.SysEnvironment.CurrentContractSnapshotID = this.CurrentContract.ContractSnapshotID;
            AppCode.SysEnvironment.CurrentContractStatus = this.CurrentContract.Status;
            AppCode.SysEnvironment.CurrentContractSnapshotCreateTime = this.CurrentContract.SnapshotCreateTime;
            this.m_ContractCache[this.ContractSnapshotID] = this.CurrentContract;
            this.FillContractHistoryList();

            //added by Eric--新建合同自动填写虚拟公司和虚拟业主
            if (ActionType.New == CurrentAction && ContractCopyType.新建 == CopyType
                && String.IsNullOrEmpty(CompanyCodeFromOutSide))
            {
                // 自动产生虚拟vendor
                FillVirtualVendorInfo();
            }

            //Added by Eric
            //根据外部传入的companycode，来设置company控件信息--Begin
            if (string.Empty != CompanyCodeFromOutSide)
            {
                if (null != CurrentContract)
                {
                    CurrentContract.CompanyCode = CompanyCodeFromOutSide;

                    // 自动产生虚拟vendor
                    FillVirtualVendorInfo();
                }
            }
            //根据外部传入的companycode，来设置company控件信息--End

            this.ShowCurrentContractInfo();
            //this.RefreshRule();


            this.m_IsBinding = false;

            //如果外部有传入companycode，则需要手动添加Entity
            if (string.Empty != CompanyCodeFromOutSide)
            {
                AddEntity();
            }

            //如果外部有传入EntityID,则手动弹出EntityID编辑框
            if (string.Empty != EntityIDFromOutSide)
            {
                //获取最新的EntityID, 因为合同变更会重新复制一份与之前合同一模一样的Entity,但是EntityID不同
                DataSet ds = contractBLL.GetLatestEntityID(EntityIDFromOutSide);

                if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    EditEntity(ds.Tables[0].Rows[0]["EntityID"].ToString());
            }

            //added by Eric -- Begin
            //如果是续租，则需要将租赁起止时间改为被续租的租赁终止时间下一天和后5年
            if (ContractCopyType.续租 == CopyType)
            {
                ExtProcessResume();
            }
            //added by Eric -- End
        }

        /// <summary>
        /// 续租处理，租赁起始日期为
        /// </summary>
        private void ExtProcessResume()
        {
            string entityID = string.Empty;
            DataRowView row = null;

            if (null == entityBLL)
                return;

            foreach (DataGridViewRow item in this.dgvEntity.Rows)
            {
                row = item.DataBoundItem as DataRowView;
                entityID = row["EntityID"].ToString();
                EntityEntity entity = entityBLL.SelectSingleEntity(entityID);
                entity.ContractSnapshotID = this.ContractSnapshotID;
                entity.RentStartDate = entity.RentEndDate.AddDays(1);
                entity.RentEndDate = entity.RentStartDate.AddYears(5);
                entityBLL.UpdateSingleEntity(entity);
                item.Cells["RentStartDate"].Value = entity.RentStartDate.ToString();
                item.Cells["RentEndDate"].Value = entity.RentEndDate.ToString();

                AppCode.SysEnvironment.s_mapIsLastEntityRentDateOverlap[entityID] = true;
            }
        }

        ///// <summary>
        ///// 检查权限。
        ///// </summary>
        //public void CheckRight()
        //{
        //    base.SetFormRight(this);
        //}

        protected override bool SaveData(SaveAction action, out string dataKey, out bool runWorkflow)
        {
            string msg = string.Empty;
            runWorkflow = true;
            dataKey = this.ContractSnapshotID;
            switch (action)
            {
                case SaveAction.TempSave:
                    if (!this.CheckPayment(out msg))
                    {
                        base.MessageError(msg);
                        return false;
                    }
                    this.CurrentContract.Status = ContractStatus.草稿.ToString();
                    if (this.SaveContractInfo(false) && this.SaveRentRuleAllInfo())
                        return true;
                    else
                        return false;
                case SaveAction.Send:
                    //this.CurrentContract.Status = ContractStatus.审核中.ToString();
                    if (!this.CheckPayment(out msg))
                    {
                        base.MessageError(msg);
                        return false;
                    }
                    return this.Submit();
                case SaveAction.Pass:
                    break;
                case SaveAction.Reject:
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void AfterRunWorkflow(WorkflowBizStatus status, string opinion, string dataKey)
        {
            //base.AfterRunWorkflow(status, opinion, dataKey);
            this.CurrentContract.Status = status.ToString();
            this.SaveContractInfo(false);
        }

        protected override MCD.Common.SRLS.WorkflowType GetWorkflowType(ActionType action)
        {
            WorkflowType workflowType = WorkflowType.合同新增;
            switch (this.CopyType)
            {
                case ContractCopyType.变更:
                    workflowType = WorkflowType.合同变更;
                    break;
                case ContractCopyType.续租:
                    workflowType = WorkflowType.合同续租;
                    break;
                case ContractCopyType.新建:
                    workflowType = WorkflowType.合同新增;
                    break;
                default:
                    break;
            }
            return workflowType;
        }

        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗体关闭时检查是否已保存，如果未保存，则将合同撤销
            //if (this.CurrentContract != null && !this.CurrentContract.IsSave)
            //{
            //    this.contractBLL.UndoContract(this.ContractSnapshotID, this.CopyType);
            //}

            if (this.m_ContractCache != null && this.contractBLL != null)
            {
                foreach (var item in this.m_ContractCache)
                {
                    ContractEntity contract = item.Value;
                    if (contract != null && contract.IsSave == false)
                    {
                        this.contractBLL.UndoContract(item.Key, this.CopyType);
                    }
                }
            }

            if (this.ParentFrm != null)
            {
                ContractList parent = (this.ParentFrm as ContractList);
                if (parent != null)
                {
                    parent.RefreshList = true;
                }
            }

            this.DisposeBLL();
            base.BaseFrm_FormClosing(sender, e);
        }

        #endregion 重写基类方法

        #region 控件事件处理

        private void btnAddVendor_Click(object sender, EventArgs e)
        {
            this.AddVendor();
        }

        private void btnAddEntity_Click(object sender, EventArgs e)
        {
            this.AddEntity();
        }

        /// <summary>
        /// 改变公司选中项后自动带出公司简称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbCompany.SelectedItem != null)
            {
                DataRowView row = this.cmbCompany.SelectedItem as DataRowView;
                object value = row["SimpleName"];
                string simpleName = (value != null && value != DBNull.Value) ? value.ToString() : "";
                string companyName = row["CompanyName"].ToString();
                if (this.CurrentContract != null)
                {
                    this.txtSimpleName.Text = simpleName;
                    this.CurrentContract.CompanySimpleName = simpleName;
                    this.CurrentContract.CompanyName = companyName;
                }
            }
        }

        private int m_HistoryIndex = -1;
        private void lstContractHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.m_IsBinding)
            {
                int index = this.lstContractHistory.SelectedIndex;
                if (index != this.m_HistoryIndex)
                {
                    this.m_HistoryIndex = index;
                    if (!this.m_IsBinding)
                    {
                        
                            ContractHistoryChange();
                    
                    }
                }
            }
        }

        private Dictionary<string, ContractEntity> m_ContractCache = new Dictionary<string, ContractEntity>();
        private void ContractHistoryChange()
        {
            DataRowView row = this.lstContractHistory.SelectedItem as DataRowView;
            if (row != null)
            {
                this.ContractSnapshotID = row["ContractSnapshotID"].ToString();
                AppCode.SysEnvironment.CurrentContractSnapshotID = this.ContractSnapshotID;
                this.CurrentContract = this.contractBLL.GetSingleContract(this.ContractSnapshotID);
                AppCode.SysEnvironment.CurrentContractStatus = this.CurrentContract.Status;
                AppCode.SysEnvironment.CurrentContractSnapshotCreateTime = this.CurrentContract.SnapshotCreateTime;
                this.m_ContractCache[this.ContractSnapshotID] = this.CurrentContract;
                this.ShowCurrentContractInfo();
                this.RefreshRule();
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmWait frmwait = new FrmWait(() => {
                this.ChangeTab();
            }, base.GetMessage("Wait"), false);
            frmwait.ShowDialog();
        }

        private void ChangeTab()
        {
            //切换到基本信息页时，保存规则信息
            if (this.tabMain.SelectedTab == this.pagContractBasicInfo && this.CurrentAction != ActionType.View)
            {
                if (this.RentRuleAllInfo != null)
                {
                    ThreadStart threadStart = new ThreadStart(() => { this.SaveRentRuleAllInfo(); });
                    Thread thread = new Thread(threadStart);
                    thread.Start();
                    //this.SaveRentRuleAllInfo();
                }
            }
        }

        private void dgvVendor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex != this.dgvVendor.Columns["clmDelete"].Index)
            //{
            //    DataRowView row = this.dgvVendor.Rows[e.RowIndex].DataBoundItem as DataRowView;
            //    string vendorContractID = row["VendorContractID"].ToString();
            //    this.EditVendor(vendorContractID);
            //}
            if (e.RowIndex >= 0)
            {
                DataRowView row = this.dgvVendor.Rows[e.RowIndex].DataBoundItem as DataRowView;
                string vendorContractID = row["VendorContractID"].ToString();
                this.EditVendor(vendorContractID);
            }
        }
        private void dgvEntity_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex != this.dgvEntity.Columns["clmDelete"].Index)
            //{
            //    DataRowView row = this.dgvEntity.Rows[e.RowIndex].DataBoundItem as DataRowView;
            //    string entityID = row["EntityID"].ToString();
            //    this.EditEntity(entityID);
            //}
            if (e.RowIndex >= 0)
            {
                DataRowView row = this.dgvEntity.Rows[e.RowIndex].DataBoundItem as DataRowView;
                string entityID = row["EntityID"].ToString();
                this.EditEntity(entityID);
            }
        }

        //合同中删除Vendor
        private void btnDelVendor_Click(object sender, EventArgs e)
        {
            if (this.dgvVendor.SelectedRows == null || this.dgvVendor.SelectedRows.Count <= 0)
                return;

            if (this.CurrentAction != ActionType.View)
            {
                DataRowView row = this.dgvVendor.SelectedRows[0].DataBoundItem as DataRowView;
                VendorContractEntity vc = new VendorContractEntity();
                ReflectHelper.SetPropertiesByDataRow(ref vc, row.Row);
                if (this.RentRuleAllInfo.GetEntitiesByVendorNo(vc.VendorNo).Count > 0)
                {
                    this.MessageInformation(base.GetMessage("CanNotDeleteVendor"));
                }
                else
                {
                    if (this.MessageConfirm(base.GetMessage("DeleteVendorConfirmInfo")) == DialogResult.OK)
                    {
                        this.contractBLL.DeleteSingleVendorContract(vc);
                        this.FillVendorGrid();
                        this.MessageInformation(base.GetMessage("DeleteVendorSuccess"));
                        this.RefreshRule();
                    }
                }
            }

        }

        //合同中删除实体
        private void btnDelEntity_Click(object sender, EventArgs e)
        {
            if (this.dgvEntity.SelectedRows == null || this.dgvEntity.SelectedRows.Count <= 0)
                return;

            if (this.CurrentAction != ActionType.View)
            {
                DataRowView row = this.dgvEntity.SelectedRows[0].DataBoundItem as DataRowView;
                EntityEntity entity = new EntityEntity();
                ReflectHelper.SetPropertiesByDataRow(ref entity, row.Row);

                if (this.MessageConfirm(base.GetMessage("DeleteEntityConfirmInfo")) == DialogResult.OK)
                {
                    this.entityBLL.DeleteSingleEntity(entity);
                    this.FillEntityGrid();
                    this.MessageInformation(base.GetMessage("DeleteEntitySuccess"));
                    this.RefreshRule();
                }
            }
        }

        //[Obsolete]
        //void dgvVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (e.RowIndex >= 0)
        //    //{
        //    //    if (this.CurrentAction != ActionType.View && e.ColumnIndex == this.dgvVendor.Columns["clmDelete"].Index)
        //    //    {
        //    //        DataRowView row = this.dgvVendor.Rows[e.RowIndex].DataBoundItem as DataRowView;
        //    //        VendorContractEntity vc = new VendorContractEntity();
        //    //        ReflectHelper.SetPropertiesByDataRow(ref vc, row.Row);
        //    //        if (this.RentRuleAllInfo.GetEntitiesByVendorNo(vc.VendorNo).Count > 0)
        //    //        {
        //    //            this.MessageInformation(base.GetMessage("CanNotDeleteVendor"));
        //    //        }
        //    //        else
        //    //        {
        //    //            if (this.MessageConfirm(base.GetMessage("DeleteVendorConfirmInfo")) == DialogResult.OK)
        //    //            {
        //    //                this.contractBLL.DeleteSingleVendorContract(vc);
        //    //                this.FillVendorGrid();
        //    //                this.MessageInformation(base.GetMessage("DeleteVendorSuccess"));
        //    //                this.RefreshRule();
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}

        //[Obsolete]
        //void dgvEntity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (e.RowIndex >= 0)
        //    //{
        //    //    if (this.CurrentAction != ActionType.View && e.ColumnIndex == this.dgvEntity.Columns["clmDelete"].Index)
        //    //    {
        //    //        DataRowView row = this.dgvEntity.Rows[e.RowIndex].DataBoundItem as DataRowView;
        //    //        EntityEntity entity = new EntityEntity();
        //    //        ReflectHelper.SetPropertiesByDataRow(ref entity, row.Row);

        //    //        if (this.MessageConfirm(base.GetMessage("DeleteEntityConfirmInfo")) == DialogResult.OK)
        //    //        {
        //    //            this.entityBLL.DeleteSingleEntity(entity);
        //    //            this.FillEntityGrid();
        //    //            this.MessageInformation(base.GetMessage("DeleteEntitySuccess"));
        //    //            this.RefreshRule();
        //    //        }
        //    //    }
        //    //}
        //}

        #endregion 控件事件处理

        #region 私有方法

        /// <summary>
        /// 初始化BLL
        /// </summary>
        private void InitialBLL()
        {
            this.companyBLL = new CompanyBLL();
            this.contractBLL = new ContractBLL();
            this.entityBLL = new EntityBLL();
            this.fixedRuleBLL = new FixedRuleBLL();
            this.ratioRuleBLL = new RatioRuleBLL();
            this.typeCodeBLL = new TypeCodeBLL();
            this.UserCompanyBLL = new UserCompanyBLL();
        }

        /// <summary>
        /// 释放BLL资源
        /// </summary>
        private void DisposeBLL()
        {
            if (this.companyBLL != null)
            {
                this.companyBLL.Dispose();
            }
            if (this.contractBLL != null)
            {
                this.contractBLL.Dispose();
            }
            if (this.entityBLL != null)
            {
                this.entityBLL.Dispose();
            }
            if (this.fixedRuleBLL != null)
            {
                this.fixedRuleBLL.Dispose();
            }
            if (this.ratioRuleBLL != null)
            {
                this.ratioRuleBLL.Dispose();
            }
            if (this.typeCodeBLL != null)
            {
                this.typeCodeBLL.Dispose();
            }
            if (this.UserCompanyBLL != null)
            {
                this.UserCompanyBLL.Dispose();
            }
        }

        /// <summary>
        /// 创建新的合同
        /// </summary>
        private void CreateNewContract()
        {
            switch (this.CopyType)
            {
                case ContractCopyType.变更:
                    this.ContractSnapshotID = this.contractBLL.CopyContract(this.ContractSnapshotID,
                        AppCode.SysEnvironment.CurrentUser.ID.ToString(), ContractCopyType.变更);
                    this.CurrentContract = this.contractBLL.GetSingleContract(this.ContractSnapshotID);
                    this.CurrentContract.LastModifyUserName = AppCode.SysEnvironment.CurrentUser.UserName;
                    this.DataKey = this.ContractSnapshotID;
                    break;
                case ContractCopyType.续租:
                    this.ContractSnapshotID = this.contractBLL.CopyContract(this.ContractSnapshotID,
                        AppCode.SysEnvironment.CurrentUser.ID.ToString(), ContractCopyType.续租);
                    this.CurrentContract = this.contractBLL.GetSingleContract(this.ContractSnapshotID);
                    this.CurrentContract.LastModifyUserName = AppCode.SysEnvironment.CurrentUser.UserName;
                    this.DataKey = this.ContractSnapshotID;
                    break;
                case ContractCopyType.新建:
                    this.CurrentContract = new ContractEntity()
                    {
                        ContractSnapshotID = Guid.NewGuid().ToString(),
                        ContractID = Guid.NewGuid().ToString(),
                        IsSave = false,
                        OperationID = AppCode.SysEnvironment.CurrentUser.ID,
                        CreateTime = DateTime.Now,
                        CreatorName = AppCode.SysEnvironment.CurrentUser.UserName,
                        IsLocked = false,
                        LastModifyUserName = AppCode.SysEnvironment.CurrentUser.UserName,
                        Status = ContractStatus.草稿.ToString(),//设置状态为草稿（1.草稿；2.审核中；3.已生效）
                        Version = "1",
                    };
                    this.ContractSnapshotID = this.CurrentContract.ContractSnapshotID;
                    this.DataKey = this.ContractSnapshotID;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 获取租金相关信息
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public RentRuleAllInfo GetRentRuleAllInfo(bool refresh)
        {
            if (refresh || this.RentRuleAllInfo == null)
            {
                this.RentRuleAllInfo = this.contractBLL.GetRentRuleInfoEntity(this.ContractSnapshotID);
                this.RentRuleAllInfo.FixedRuleBLL = this.fixedRuleBLL;
                this.RentRuleAllInfo.RatioRuleBLL = this.ratioRuleBLL;
                this.RentRuleAllInfo.EntityBLL = this.entityBLL;
                this.RentRuleAllInfo.TypeCodeBLL = this.typeCodeBLL;
            }
            return this.RentRuleAllInfo;
        }

        /// <summary>
        /// 显示当前合同信息
        /// </summary>
        private void ShowCurrentContractInfo()
        {
            if (this.CurrentContract != null)
            {
                try
                {
                    //加载附件信息
                    this.sysAttach1.ObjectID = this.CurrentContract.ContractSnapshotID;
                    //设置附件控件的状态
                    this.sysAttach1.CmdType = this.CurrentAction;
                    this.sysAttach1.BindGridView();
                    this.sysAttach1.Enabled = true;
                    this.sysAttach1.Controls.Find("groupBoxTitle", true)[0].Enabled = true;
                    this.sysAttach1.Controls.Find("dataGridView1", true)[0].Enabled = true;
                }
                catch
                { }

                //刷新规则
                if (this.CurrentAction != ActionType.New || this.CopyType != ContractCopyType.新建)
                {
                    this.RefreshRule();
                }

                //业主和实体列表始终可用
                this.grpVendor.Enabled = true;
                this.dgvVendor.Enabled = true;
                this.grpEntityInfo.Enabled = true;
                this.dgvEntity.Enabled = true;
                this.lstContractHistory.Enabled = true;
                base.SetFormRight(this);

                //变更时不允许删除业主和实体
                if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更)
                {
                    this.btnAddEntity.Enabled = this.btnDelEntity.Enabled = false;
                    this.btnAddVendor.Enabled = this.btnDelVendor.Enabled = false;
                    this.cmbCompany.Enabled = false;
                }
                
                this.bdsContract.DataSource = this.CurrentContract;
                FillVendorGrid();
                FillEntityGrid();
            }
        }

        #region 填充下拉框和表格
        
        private void FillContractHistoryList()
        {
            //填充历史版本
            DataSet ds = this.contractBLL.SelectContractHistory(this.CurrentContract.ContractID);
            string clmContractNoName = "ContractNO";
            string clmContractSnapshotIDName = "ContractSnapshotID";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.lstContractHistory.DisplayMember = clmContractNoName;
                this.lstContractHistory.ValueMember = clmContractSnapshotIDName;
                this.lstContractHistory.DataSource = ds.Tables[0];

                if (this.lstContractHistory.Items.Count > 0)
                {
                    //DataRowView row = this.lstContractHistory.Items[0] as DataRowView;
                    //if (row[clmContractNoName] == DBNull.Value || row[clmContractNoName] == null)//存在未生效的则选中该合同
                    //{
                    //    this.lstContractHistory.SelectedIndex = 0;
                    //}
                    //else//否则选中最近的合同（按合同编号排序在最后）
                    //{
                    //    this.lstContractHistory.SelectedIndex = this.lstContractHistory.Items.Count - 1;
                    //}
                    //取消判断，按照合同创建时间排序后，最后创建的肯定排在最后一个
                    this.lstContractHistory.SelectedIndex = this.lstContractHistory.Items.Count - 1;
                }
            }
        }

        private static DataSet s_CompanyCache = null;
        private void FillCompanyComboBox()
        {
            //Commented by Eric -- Begin
            //if (s_CompanyCache == null)
            //{
            //    s_CompanyCache = UserCompanyBLL.SelectUserCompany(new UserCompanyEntity() { UserId = SysEnvironment.CurrentUser.ID, Status = 'A' });
            //}
            //Commented by Eric -- End

            //Added by Eric -- Begin
            //Description: Do not cache the company information
            s_CompanyCache = UserCompanyBLL.SelectUserCompany(new UserCompanyEntity() { UserId = SysEnvironment.CurrentUser.ID, Status = 'A' });
            //Added by Eric -- End

            if (!s_CompanyCache.Tables[0].Columns.Contains("CompanyDisplayName"))
            {
                s_CompanyCache.Tables[0].Columns.Add("CompanyDisplayName", typeof(string), "CompanyCode + ':' + CompanyName");
            }
            //填充公司下拉框
            ControlHelper.BindComboBox(this.cmbCompany, s_CompanyCache.Tables[0], "CompanyDisplayName", "CompanyCode");
        }

        private void InitialGridColumns()
        {
            #region 出租方列表

            GridViewHelper.AppendColumnToDataGridView(this.dgvVendor, "VendorNo", base.GetMessage("dgvVendor_VendorNo"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvVendor, "VendorName", base.GetMessage("dgvVendor_VendorName"), 300);
            GridViewHelper.AppendColumnToDataGridView(this.dgvVendor, "PayMentType", base.GetMessage("dgvVendor_PayMentType"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvVendor, "Status", base.GetMessage("dgvVendor_Status"), 60);
            GridViewHelper.AppendColumnToDataGridView(this.dgvVendor, "BlockPayMent", base.GetMessage("dgvVendor_BlockPayMent"), 100);

            //this.dgvVendor.CellContentClick += new DataGridViewCellEventHandler(dgvVendor_CellContentClick);
            this.dgvVendor.CellDoubleClick += new DataGridViewCellEventHandler(dgvVendor_CellDoubleClick);
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvVendor);

            #endregion

            #region 实体列表

            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "EntityTypeName", base.GetMessage("dgvEntity_EntityTypeName"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "StoreOrDeptNo", base.GetMessage("dgvEntity_StoreOrDeptNo"), 120);
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "EntityName", base.GetMessage("dgvEntity_EntityName"), 300);
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "OpeningDate", base.GetMessage("dgvEntity_OpeningDate"), 100, (column) =>
            {
                column.DefaultCellStyle.Format = "yyyy-MM-dd";
            });
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "RentStartDate", base.GetMessage("dgvEntity_RentStartDate"), 100, (column) =>
            {
                column.DefaultCellStyle.Format = "yyyy-MM-dd";
            });
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "RentEndDate", base.GetMessage("dgvEntity_RentEndDate"), 100, (column) =>
            {
                column.DefaultCellStyle.Format = "yyyy-MM-dd";
            });
            GridViewHelper.AppendColumnToDataGridView(this.dgvEntity, "OwnerID", base.GetMessage("dgvEntity_OwnerID"), 120);

            //this.dgvEntity.CellContentClick += new DataGridViewCellEventHandler(dgvEntity_CellContentClick);
            this.dgvEntity.CellDoubleClick += new DataGridViewCellEventHandler(dgvEntity_CellDoubleClick);

            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvEntity);

            #endregion
        }

        private void FillVendorGrid()
        {
            DataSet ds = this.contractBLL.SelectVendorContractByContractSnapshotID(this.ContractSnapshotID);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dgvVendor.DataSource = ds.Tables[0];
            }
        }

        private void FillEntityGrid()
        {
            DataSet ds = this.entityBLL.SelectEntitiesByContractSnapshotID(this.ContractSnapshotID);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dgvEntity.DataSource = ds.Tables[0];
            }
        }

        #endregion

        #region 添加编辑实体和业主 

        private void AddVendor()
        {
            if (this.CheckContractSave())
            {
                VendorEdit frm = new VendorEdit()
                {
                    ParentFrm = this,
                    RentRuleAllInfo = this.GetRentRuleAllInfo(false),
                    CurrentState = EDIT_STATUS.AddNew,
                    ContractSnapshotID = this.ContractSnapshotID,
                };

                this.ShouldRefreshRule = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.FillVendorGrid();
                    this.GetRentRuleAllInfo(true);
                    if (this.ShouldRefreshRule)
                    {
                        this.RefreshRule();
                    }
                }
            }
        }

        private void EditVendor(string vendorContractID)
        {
            //added by Eric--Begin
            EDIT_STATUS status = EDIT_STATUS.None;

            if (ActionType.View == CurrentAction)
                status = EDIT_STATUS.View;
            else
                status = EDIT_STATUS.Edit;
            //added by Eric--End


            VendorEdit frm = new VendorEdit()
            {
                ParentFrm = this,
                CurrentState = status, //modified by Eric
                ContractSnapshotID = this.ContractSnapshotID,
                VendorContractID = vendorContractID,
                RentRuleAllInfo = this.GetRentRuleAllInfo(false),
                TaskName=base.TaskName,
                ReadOnly = (base.TaskName == "审核") || (this.CurrentAction == ActionType.View) || (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更),
            };

            this.ShouldRefreshRule = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.FillVendorGrid();
                this.GetRentRuleAllInfo(true);
                if (this.ShouldRefreshRule)
                {
                    this.RefreshRule();
                }
            }
        }

        private void AddEntity()
        {
            if (String.IsNullOrEmpty(this.CurrentContract.CompanyCode))
            {
                this.MessageInformation(base.GetMessage("NoticeChooseCompany"));
                return;
            }

            if (this.CheckContractSave())
            {
                EntityEdit frm = new EntityEdit()
                    {
                        ParentFrm = this,
                        CurrentState = EDIT_STATUS.AddNew,
                        ContractSnapshotID = this.ContractSnapshotID,
                        RentRuleAllInfo = this.GetRentRuleAllInfo(false),
                        IsContractChange = (this.CopyType == ContractCopyType.变更),
                        CompanyCode = this.CurrentContract.CompanyCode,
                        StoreNoFromOutSide = this.StoreNoFromOutSide,
                        KioskNoFromOutSide = this.KioskNoFromOutSide
                    };

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.FillEntityGrid();
                    this.tabVendors.TabPages.Clear();
                    this.GetRentRuleAllInfo(true);
                    this.RefreshRule();
                }
            }
        }

        private void EditEntity(string entityID)
        {
            //added by Eric--Begin
            EDIT_STATUS status = EDIT_STATUS.None;

            if (ActionType.View == CurrentAction)
                status = EDIT_STATUS.View;
            else
                status = EDIT_STATUS.Edit;
            //added by Eric--End

            EntityEdit frm = new EntityEdit()
            {
                ParentFrm = this,
                CurrentState = status, //modified by Eric
                ContractSnapshotID = this.ContractSnapshotID,
                EntityID = entityID,
                RentRuleAllInfo = this.GetRentRuleAllInfo(false),
                TaskName=base.TaskName,
                ReadOnly = (base.TaskName == "审核") || (this.CurrentAction == ActionType.View) || (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更),
                IsContractChange = (this.CopyType == ContractCopyType.变更),
                CompanyCode = this.CurrentContract.CompanyCode
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.FillEntityGrid();
                this.tabVendors.TabPages.Clear();
                this.GetRentRuleAllInfo(true);
                this.RefreshRule();
            }
        }

        #endregion

        private bool CheckContractSave()
        {
            //新增且未保存
            if (this.CurrentAction == ActionType.New && !this.CurrentContract.IsSave)
            {
                if (this.SaveContractInfo(true))
                {
                    this.CurrentContract = this.contractBLL.GetSingleContract(this.ContractSnapshotID);
                    this.bdsContract.DataSource = this.CurrentContract;
                    this.FillContractHistoryList();
                    this.ShowCurrentContractInfo();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool SaveContractInfo(bool autoSave)
        {
            if (this.cmbCompany.SelectedItem == null)
            {
                this.MessageInformation(base.GetMessage("NoticeChooseCompany"));
                this.cmbCompany.Focus();
                return false;
            }
            else
            {
                this.CurrentContract.LastModifyTime = DateTime.Now;

                if (!autoSave)
                {
                    this.CurrentContract.IsSave = true;
                }

                this.CurrentContract.LastModifyUserName = AppCode.SysEnvironment.CurrentUser.UserName;
                this.CurrentContract.LastModifyTime = DateTime.Now;
                if (this.CurrentAction == ActionType.Edit || this.CopyType != ContractCopyType.新建)
                {
                    this.contractBLL.UpdateSingleContract(this.CurrentContract);
                }
                else
                {
                    this.contractBLL.InsertSingleContract(this.CurrentContract);
                }
                this.CurrentAction = ActionType.Edit;
                return true;
            }
        }

        private bool Submit()
        {
            bool result = false;

            this.GetRentRuleAllInfo(false);

            if (this.SaveContractInfo(false) && this.SaveRentRuleAllInfo())
            {
                string errorInfo = this.CheckContract();// +this.CheckRentRuleAvailable();

                if (errorInfo.Length > 0)
                {
                    //added by Eric -- Begin
                    if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更 ||
                        AppCode.SysEnvironment.ContractCopyType == ContractCopyType.续租)
                    {
                        if (!AppCode.SysEnvironment.s_bugFixed)
                        {
                            if (errorInfo.Contains("不闭合") || errorInfo.Contains("缺失"))
                            {
                                this.MessageInformation("新的租赁开始时间大于之前的租赁结束时间！请点击规则页面调整相应的计算规则");
                            }
                            else
                            {
                                this.MessageInformation(errorInfo);
                            }
                        }
                        else
                        {
                            this.MessageInformation(errorInfo);
                        }
                    }
                    //added by Eric -- End
                    else
                    {
                        this.MessageInformation(errorInfo);
                    }
                    result = false;

                    //added by Eric -- Begin
                    this.CurrentContract.IsSave = false;
                    //added by Eric -- End
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        private string CheckContract()
        {
            DataTable dtError = this.contractBLL.CheckContract(this.ContractSnapshotID);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dtError.Rows)
            {
                string index = row["IndexId"].ToString();
                string relationDate = (row["RelationData"] != DBNull.Value && row["RelationData"] != null ? row["RelationData"].ToString() : "").Trim();
                //展示错误信息
                string format = base.GetMessage(row["Code"].ToString()) + "\n";
                sb.AppendFormat(format, index, string.IsNullOrEmpty(relationDate) ? "" : "[" + relationDate + "] ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 保存规则信息
        /// </summary>
        /// <param name="check">是否校验</param>
        public bool SaveRentRuleAllInfo()
        {
            if (this.RentRuleAllInfo != null)
            {
                foreach (var item in this.RentRuleAllInfo.EntityInfoSettingList)
                {
                    this.entityBLL.InsertOrUpdateEntityInfoSetting(item);
                }

                foreach (var item in this.RentRuleAllInfo.FixedRuleSettingList)
                {
                    if (item.Enabled)
                        this.fixedRuleBLL.InsertOrUpdateFixedRuleSetting(item);
                    else
                    {
                        this.fixedRuleBLL.DeleteSingleFixedRuleSetting(item);
                    }
                }

                foreach (var item in this.RentRuleAllInfo.FixedTimeIntervalSettingList)
                {
                    if (item.Enabled)
                        this.fixedRuleBLL.InsertOrUpdateSingleFixedTimeIntervalSetting(item);
                    else
                        this.fixedRuleBLL.DeleteSingleFixedTimeIntervalSetting(item);
                }

                foreach (var item in this.RentRuleAllInfo.RatioRuleSettingList)
                {
                    if (item.Enabled)
                        this.ratioRuleBLL.InsertOrUpdateSingleRatioRuleSetting(item);
                    else
                        this.ratioRuleBLL.DeleteSingleRatioRuleSetting(item);
                }

                foreach (var item in this.RentRuleAllInfo.RatioCycleSettingList)
                {
                    if (item.Enabled)
                        this.ratioRuleBLL.InsertOrUpdateSingleRatioCycleSetting(item);
                    else
                        this.ratioRuleBLL.DeleteSingleRatioCycleSetting(item);
                }

                foreach (var item in this.RentRuleAllInfo.RatioTimeIntervalSettingList)
                {
                    if (item.Enabled)
                        this.ratioRuleBLL.InsertOrUpdateSingleRatioTimeIntervalSetting(item);
                    else
                        this.ratioRuleBLL.DeleteSingleRatioTimeIntervalSetting(item);
                }

                foreach (var item in this.RentRuleAllInfo.ConditionAmountList)
                {
                    if (item.Enabled)
                        this.ratioRuleBLL.InsertOrUpdateSingleConditionAmount(item);
                    else
                        this.ratioRuleBLL.DeleteSingleConditionAmount(item);
                }
            }
            return true;
        }

        protected void RefreshRule()
        {
            // 启动新的线程完成动作
            ThreadStart start = new ThreadStart(this.RefreshRuleInThread);
            Thread thread = new Thread(start);
            thread.Start();
        }

        private void RefreshRuleInThread()
        {
            if (this.tabVendors.InvokeRequired)
            {
                this.tabVendors.Invoke(new RefreshRuleSyncDelegate(RefreshRuleSync), new object[] { });
            }
            else
            {
                this.RefreshRuleSync();
            }
        }

        private delegate void RefreshRuleSyncDelegate();
        private void RefreshRuleSync()
        {
            this.tabVendors.SuspendLayout();
            this.tabVendors.TabPages.Clear();

            this.GetRentRuleAllInfo(true);

            if (this.RentRuleAllInfo.VendorContractList != null)
            {
                foreach (var item in this.RentRuleAllInfo.VendorContractList)
                {
                    //若业主无关联实体，则不加载该业主的租金规则Tab
                    if (!this.RentRuleAllInfo.VendorEntityList.Any(ve => ve.VendorNo.ToLower().Equals(item.VendorNo.ToLower())))
                        continue;
                    this.AddTabPage(item);
                }
            }
            this.tabVendors.ResumeLayout();
        }

        private void AddTabPage(VendorContractEntity item)
        {
            TabPage page = new TabPage(item.VendorName);
            this.tabVendors.TabPages.Add(page);
            //
            RentRulePanel panel = new RentRulePanel() {
                ContractBLL = this.contractBLL,
                FixedRuleBLL = this.fixedRuleBLL,
                RatioRuleBLL = this.ratioRuleBLL,
                EntityBLL = this.entityBLL,
                VendorNo = item.VendorNo,
                RentRuleAllInfo = this.RentRuleAllInfo,
                Dock = DockStyle.Fill,
                ReadOnly = (base.TaskName == "审核") || 
                    (this.CurrentAction == ActionType.View) || 
                    (AppCode.SysEnvironment.CurrentContractSnapshotID != AppCode.SysEnvironment.EditingContractSnapshotID),
            };
            page.Controls.Add(panel);
        }

        /// <summary>
        /// 检查支付类型是否可选。
        /// </summary>
        /// <returns></returns>
        private bool CheckPayment(out string msg)
        {
            msg = string.Empty;
            List<Control> controls = ControlHelper.FindControl(this.tabVendors, item => item is FixedRulePanel);
            foreach (Control ctrl in controls)
            {
                if (ctrl is FixedRulePanel)
                {
                    if (!(ctrl as FixedRulePanel).CheckPaymentType(out msg))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 当外部传入CompanyCode的时候，需要为用户产生一个虚拟vendor，为之后自动产生entity信息做准备
        /// </summary>
        private void FillVirtualVendorInfo()
        {
            if (this.CheckContractSave())
            {
                VendorEdit.CreateVirtualVendorAndSave(ContractSnapshotID);
            }
        }
        #endregion 私有方法

        #region 静态公共方法
        /// <summary>
        /// 创建新合同
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strKioskNo"></param>
        /// <param name="strCompanyCode"></param>
        /// <param name="pFrm"></param>
        static public void CreateNewContract(string strStoreNo, string strKioskNo, string strCompanyCode, object pFrm)
        {
            ContractEdit frm = new ContractEdit()
            {
                CompanyCodeFromOutSide = strCompanyCode,
                StoreNoFromOutSide = strStoreNo,
                KioskNoFromOutSide = strKioskNo,
                ParentFrm = pFrm,
                CurrentAction = ActionType.New,
                CopyType = ContractCopyType.新建,
                //IsAddNew = true,
                IsNewWorkflow = true,
                WorkflowBizStatus = WorkflowBizStatus.草稿,
            };
            frm.ShowDialog();
        }

        /// <summary>
        /// 变更合同
        /// </summary>
        /// <param name="strSnapshotID"></param>
        /// <param name="EntityID"></param>
        static public void ChangeContract(string strSnapshotID, string EntityID, object pFrm)
        {
            ContractEdit frm = new ContractEdit()
            {
                ParentFrm = pFrm,
                EntityIDFromOutSide = EntityID,
                CopyType = ContractCopyType.变更,
                CurrentAction = ActionType.New,
                //IsAddNew = true,
                IsNewWorkflow = true,
                WorkflowBizStatus = WorkflowBizStatus.草稿,
                ContractSnapshotID = strSnapshotID,
            };
            
            frm.ShowDialog();
        }
        #endregion
    }
}