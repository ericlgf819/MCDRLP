using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL.Master;
using MCD.Common;
using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class VendorEdit : BaseEdit
    {
        public VendorEdit()
        {
            InitializeComponent();
            this.ReadOnly = false;
        }

        #region 字段和属性声明

        private ContractBLL contractBLL = new ContractBLL();
        private VendorBLL vendorBLL = new VendorBLL();

        /// <summary>
        /// 业主合同关系ID
        /// </summary>
        public string VendorContractID { get; set; }
        /// <summary>
        /// 合同快照ID
        /// </summary>
        public string ContractSnapshotID { get; set; }

        public VendorContractEntity CurrentVendorContract { get; set; }

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        public bool ReadOnly { get; set; }
        public string TaskName { get; set; }

        #endregion 字段和属性声明


        #region 重写基类方法

        public override void BindFormControl()
        {
            //加载支付方式下拉框
            this.BindComboBoxFromDictionary(this.cmbPaymentType, "PaymentType");

            base.BindFormControl();

            //填充控件
            if (this.CurrentState != EDIT_STATUS.AddNew)
            {
                this.btnCreateVirtualVendor.Enabled = false;
                this.CurrentVendorContract = contractBLL.SelectSingleVendorContract(this.VendorContractID);
                this.cmbVendorName.Text = this.CurrentVendorContract.VendorName;
            }
            else
            {
                this.btnCreateVirtualVendor.Enabled = true;
                this.CurrentVendorContract = new VendorContractEntity()
                    {
                        ContractSnapshotID = this.ContractSnapshotID,
                        IsVirtual = false,
                        PayMentType = "M",
                        Status = "",
                        VendorContractID = Guid.NewGuid().ToString(),
                    };
            }
            this.bdsVendorContract.DataSource = this.CurrentVendorContract;
            this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo;

            if (!this.IsContractChange())
            {
                this.ReadOnly = true;
            }

            this.CheckControlStatus();

            if (ReadOnly)
            {
                base.EnabledControl(this.pnlEdit, false);
                this.btnSave.Enabled = false;
            }
            else
            {
                this.btnSave.Enabled = true;
            }

            if (this.IsContractChange()&&this.TaskName!="审核")
            {
                if (this.CurrentVendorContract.IsVirtual)//变更时虚拟业主允许修改
                {
                    this.CheckControlStatus();
                }
                this.cmbPaymentType.Enabled = true;//变更时允许修改PaymentType
                this.btnSave.Enabled = true;
            }

            this.toolTip1.SetToolTip(this.txtVendorNo, base.GetMessage("TipPressEnter"));

            //added by Eric --Begin
            if (EDIT_STATUS.View == CurrentState)
            {
                this.txtVendorNo.ReadOnly = true;
                this.cmbVendorName.Enabled = false;
                this.cmbPaymentType.Enabled = false;
                this.txtStatus.ReadOnly = true;
                this.txtBlockPayment.ReadOnly = true;
            }
            //added by Eric --End
        }

        /// <summary>
        /// 合同变更且合同快照ID是最新ID
        /// </summary>
        /// <returns></returns>
        private bool IsContractChange()
        {
            return (AppCode.SysEnvironment.EditingContractSnapshotID == AppCode.SysEnvironment.CurrentContractSnapshotID);
        }

        private void CheckControlStatus()
        {
            if (!this.ReadOnly && this.CurrentVendorContract.IsVirtual)
            {
                //虚拟业主只能改名称和编号
                this.txtVendorNo.ReadOnly = false;
                this.cmbVendorName.Enabled = true;
                this.cmbPaymentType.Enabled = false;
                //commented by Eric--Begin
                //this.cmbPaymentType.SelectedIndex = 0;
                //commented by Eric--End
                this.txtStatus.ReadOnly = true;
                this.txtBlockPayment.ReadOnly = true;
            }
            else
            {
                //正式的只能改编号和PaymentType
                //当该业主有关联实体时，且不为虚拟业主，则不允许修改VendorNo
                if (this.ReadOnly && this.CurrentVendorContract.Status == "A")//this.RentRuleAllInfo.GetEntitiesByVendorNo(this.CurrentVendorContract.VendorNo).Count > 0)
                {
                    this.txtVendorNo.ReadOnly = true;
                    this.cmbVendorName.Enabled = false;
                    this.cmbPaymentType.Enabled = false;
                    this.txtStatus.ReadOnly = true;
                    this.txtBlockPayment.ReadOnly = true;
                }
                else
                {
                    this.txtVendorNo.ReadOnly = false;
                    this.cmbVendorName.Enabled = false;
                    this.cmbPaymentType.Enabled = true;
                    this.txtStatus.ReadOnly = true;
                    this.txtBlockPayment.ReadOnly = true;
                }

                if (this.IsContractChange())
                {
                    this.cmbPaymentType.Enabled = true;
                    if (this.CurrentVendorContract.IsVirtual)
                    {
                        this.cmbVendorName.Enabled = true;
                    }
                }
            }

            //added by Eric --Begin
            switch (CurrentState)
            {
                case EDIT_STATUS.View:
                    {
                        this.txtVendorNo.ReadOnly = true;
                        this.cmbVendorName.Enabled = false;
                        this.cmbPaymentType.Enabled = false;
                        this.txtStatus.ReadOnly = true;
                        this.txtBlockPayment.ReadOnly = true;
                        break;
                    }
                case EDIT_STATUS.AddNew:
                    {
                        this.cmbVendorName.Enabled = true;
                        break;
                    }
                default:
                    break;
            }
            //added by Eric --End
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //释放资源
            this.contractBLL.Dispose();
            this.vendorBLL.Dispose();
            base.OnFormClosing(e);
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.GetVendorContractEntity(this.CurrentVendorContract))
            {
                return;
            }

            if (this.CurrentVendorContract.IsVirtual)
            {
                this.CurrentVendorContract.VendorName = this.cmbVendorName.Text;
                this.CurrentVendorContract.PayMentType = this.cmbPaymentType.SelectedValue.ToString();
                this.CurrentVendorContract.Status = this.txtStatus.Text;
                this.CurrentVendorContract.BlockPayMent = this.txtBlockPayment.Text;

                if (string.IsNullOrEmpty(this.CurrentVendorContract.VendorName)
                    || this.CurrentVendorContract.VendorName.Trim().Length == 0)
                {
                    this.MessageInformation(base.GetMessage("VendorNameIsEmpty"));
                    return;
                }
            }

            if (this.CurrentState == EDIT_STATUS.AddNew)
            {
                var vendorContract = this.RentRuleAllInfo.VendorContractList.FirstOrDefault
                    (item => item.VendorNo == this.CurrentVendorContract.VendorNo);

                if (vendorContract == null)
                {
                    this.contractBLL.InsertSingleVendorContract(this.CurrentVendorContract);
                    this.MessageInformation(base.GetMessage("NoticeAddVendorSuccess"));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeVendorRepeat"));
                    this.txtVendorNo.Focus();
                }
            }
            else
            {
                //业主合同编号不同但业主相同，则不允许修改
                var vendorContract = this.RentRuleAllInfo.VendorContractList.FirstOrDefault
                    (item => item.VendorNo == this.CurrentVendorContract.VendorNo
                        && item.VendorContractID != this.CurrentVendorContract.VendorContractID);

                if (vendorContract == null)
                {
                    this.contractBLL.UpdateSinglVendorContract(this.CurrentVendorContract);
                    this.MessageInformation(base.GetMessage("NoticeUpdateVendorSuccess"));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeVendorRepeat"));
                    this.txtVendorNo.Focus();
                }
            }

            base.btnSave_Click(sender, e);
        }

        #endregion 重写基类方法


        #region 控件事件处理

        protected override void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //
            this.Close();
        }

        private void txtVendorNo_KeyUp(object sender, KeyEventArgs e)
        {
            //把业主信息带过来
            if (e.KeyCode == Keys.Enter && this.txtVendorNo.Text != this.CurrentVendorContract.VendorNo)
            {
                //e.Handled = true;
                GetVendorInfoByInputNo();
            }
        }

        private void btnCreateVirtualVendor_Click(object sender, EventArgs e)
        {
            //this.btnCreateVirtualVendor.Enabled = false;
            //this.CurrentVendorContract.IsVirtual = true;
            //this.CurrentVendorContract.Status = "A";
            //this.CheckVirtualStatus();

            //this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo = Guid.NewGuid().ToString();
            //this.CurrentVendorContract.VendorName = "";
            //this.CurrentVendorContract.PayMentType = this.cmbPaymentType.SelectedValue.ToString();
            //this.cmbPaymentType.Enabled = false;
            //(this.ParentFrm as ContractEdit).ShouldRefreshRule = true;

            this.btnCreateVirtualVendor.Enabled = false;
            this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo = Guid.NewGuid().ToString();
            //this.CurrentVendorContract.Status = "A";
            this.CurrentVendorContract.IsVirtual = true;
            this.CurrentVendorContract.PayMentType = this.cmbPaymentType.SelectedValue.ToString();
            this.CurrentVendorContract.VendorName = string.Empty;
            (this.ParentFrm as ContractEdit).ShouldRefreshRule = true;
            this.CheckControlStatus();
        }

        private void txtVendorNo_Leave(object sender, EventArgs e)
        {
            //this.GetVendorInfoByInputNo();
        }

        /// <summary>
        /// 从模糊筛选的结果中选择指定业主。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendorName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.SelectedItem == null)
                return;

            DataRowView dr = cmb.SelectedItem as DataRowView;
            VendorEntity vendor = new VendorEntity();
            ReflectHelper.SetPropertiesByDataRow<VendorEntity>(ref vendor, dr.Row);

            if (vendor == null || string.IsNullOrEmpty(vendor.VendorNo.Trim()))
                return;

            this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo = vendor.VendorNo;
            this.CurrentVendorContract.VendorName = vendor.VendorName.Trim();
            if (string.IsNullOrEmpty(vendor.PayMentType) || (vendor.PayMentType != "M" && vendor.PayMentType != "T"))
            {
                vendor.PayMentType = "M";
            }

            this.cmbPaymentType.SelectedValue = this.CurrentVendorContract.PayMentType = vendor.PayMentType;
            this.txtStatus.Text = this.CurrentVendorContract.Status = vendor.Status;
            this.txtBlockPayment.Text = this.CurrentVendorContract.BlockPayMent = vendor.BlockPayMent;
            this.CurrentVendorContract.IsVirtual = false;
            (this.ParentFrm as ContractEdit).ShouldRefreshRule = true;
            this.CheckControlStatus();
        }

        private void txtVendorNo_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtVendorNo.Text != this.CurrentVendorContract.VendorNo)
            {
                this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo;
                e.Cancel = true;
                //if (!this.GetVendorInfoByInputNo())
                //{
                //}
            }
        }

        #endregion 控件事件处理


        #region 私有方法

        private bool GetVendorContractEntity(VendorContractEntity entity)
        {
            if (entity == null)
            {
                entity = new VendorContractEntity();
            }

            if (null != entity.VendorNo && entity.VendorNo.Trim().Length == 0)
            {
                this.MessageInformation(base.GetMessage("NoticeInputVendorNo"));
                this.txtVendorNo.Focus();
                return false;
            }

            if (!entity.IsVirtual
                && !UIChecker.VerifyComboxNull(this.cmbPaymentType, base.GetMessage("NoticeChoosePaymentType"), base.GetMessage("NoticeTitle")))
            {
                return false;
            }

            if (string.IsNullOrEmpty(entity.VendorContractID))
            {
                entity.VendorContractID = Guid.NewGuid().ToString();
            }
            entity.ContractSnapshotID = this.ContractSnapshotID;

            if (!entity.IsVirtual && !entity.Status.ToLower().Equals("a"))
            {
                this.MessageInformation(base.GetMessage("NoticeVendorIsNotActive"));
                return false;
            }

            if (!entity.IsVirtual && (string.IsNullOrEmpty(entity.BlockPayMent) || !entity.BlockPayMent.ToLower().Equals("n")))
            {
                if (this.MessageConfirm(base.GetMessage("NoticeConfirmVendorIsNotNo")) != DialogResult.OK)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 通过输入的业主编号获取业主信息
        /// </summary>
        private bool GetVendorInfoByInputNo()
        {

            if (this.txtVendorNo.Text.Trim().Length == 0)
            {
                this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo = "";
                this.cmbVendorName.Text = this.CurrentVendorContract.VendorName = "";
                return true;
            }
            else
            {
                string inputString = this.txtVendorNo.Text.Trim();
                DataSet ds = null;
                FrmWait frm = new FrmWait(() =>
                {
                    base.ExecuteAction(() =>
                    {
                        ds = this.vendorBLL.SelectVendorByNoOrName(inputString);
                    }, "读取业主信息出错", "读取业主信息出错");

                }, base.GetMessage("Wait"), () =>
                {
                    this.vendorBLL.CloseService();
                });
                frm.ShowDialog();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //将模糊查询的业主信息绑定到业主下拉框
                    DataTable dt = ds.Tables[0];
                    dt.Columns.Add("VendorNoName", typeof(string));
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["VendorNoName"] = dr["VendorNo"] + ":" + dr["VendorName"];
                    }
                    this.cmbVendorName.DataSource = dt;
                    this.cmbVendorName.ValueMember = "VendorNo";
                    this.cmbVendorName.DisplayMember = "VendorNoName";
                    //this.cmbVendorName.SelectedIndex = -1;

                    if (this.cmbVendorName.Items.Count == 1)
                    {
                        this.cmbVendorName_SelectionChangeCommitted(this.cmbVendorName, EventArgs.Empty);
                    }
                    else
                    {
                        this.cmbVendorName.DroppedDown = true;
                        this.cmbVendorName.Focus();
                        //fixed by Eric -- Begin
                        this.cmbVendorName.Enabled = true;
                        //fixed by Eric -- End
                    }

                    return true;
                }
                else
                {
                    this.MessageInformation(base.GetMessage("NoticeVendorNoNotExist"));
                    this.txtVendorNo.Text = this.CurrentVendorContract.VendorNo;
                    this.txtVendorNo.Focus();
                    return false;
                }
            }
        }
        #endregion 私有方法

        #region 公用方法
        static public void CreateVirtualVendorAndSave(string ContractSnapshotID)
        {
            VendorContractEntity CurrentVendorContract = new VendorContractEntity()
            {
                ContractSnapshotID = ContractSnapshotID,
                IsVirtual = false,
                PayMentType = "M",
                Status = "",
                BlockPayMent = "",
                VendorContractID = Guid.NewGuid().ToString(),
                VendorNo = "00000",
                VendorName = "00000:虚拟Vendor"
            };

            ContractBLL cbl = new ContractBLL();
            cbl.InsertSingleVendorContract(CurrentVendorContract);
            cbl.Dispose();
        }
        #endregion

    }
}
