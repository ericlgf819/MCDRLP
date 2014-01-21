using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StoreEdit : BaseEdit
    {
        //Fields
        private StoreBLL StoreBLL = new StoreBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        private StoreEntity entity = null;
        #region ctor

        public StoreEdit(string storeNo)
        {
            InitializeComponent();
            //
            this.entity = this.StoreBLL.SelectSingleStore(storeNo);
        }
        #endregion

        private void StoreEdit_Load(object sender, EventArgs e)
        {
            // 绑定 公司列表
            DataTable dt = null;
            base.ExecuteAction(() =>
            {
                FrmWait frmwait = new FrmWait(() =>
                {
                    DataSet dsUserCompany = this.UserCompanyBLL.SelectUserCompany(new UserCompanyEntity()
                    {
                        UserId = AppCode.SysEnvironment.CurrentUser.ID,
                        Status = 'A'
                    });
                    if (dsUserCompany != null && dsUserCompany.Tables.Count > 0)
                    {
                        dt = dsUserCompany.Tables[0];
                    }
                }, base.GetMessage("Wait"), false);
                frmwait.ShowDialog();
            }, "获取公司信息数据错误", "公司信息");
            if (dt == null) return;
            //
            ControlHelper.BindComboBox(this.cmbCompanyCodeValue, dt, "CompanyName", "CompanyCode");
            this.cmbCompanyCodeValue.SelectedValue = this.entity.CompanyCode;
            this.txtStoreNo.DataBindings.Add("Text", this.entity, "StoreNo");
            this.txtStoreName.DataBindings.Add("Text", this.entity, "StoreName");
            this.txtSimpleName.DataBindings.Add("Text", this.entity, "SimpleName");
            if (this.entity.OpenDate != null)
            {
                this.dtpOpenDateValue.Value = this.entity.OpenDate.Value;
            }
            else if (!this.dtpOpenDateValue.Enabled)
            {
                this.dtpOpenDateValue.Visible = false;
            }
            if (this.entity.CloseDate != null)
            {
                this.dtpCloseDateValue.Value = this.entity.CloseDate.Value;
                this.lblX3.Visible = false;
            }
            else if (!this.dtpCloseDateValue.Enabled)
            {
                this.dtpCloseDateValue.Visible = false;
            }
            this.txtEmailAddress.DataBindings.Add("Text", this.entity, "EmailAddress");
            //
            ControlHelper.BindComboBox(this.ddlStatus, base.DTStatus, "StatusName", "StatusValue");
            this.ddlStatus.SelectedValue = this.entity.Status;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            // 检测公司
            if (this.cmbCompanyCodeValue.SelectedIndex == -1)
            {
                base.MessageError(base.GetMessage("MustCompanyCode"), base.GetMessage("Caption"));
                //
                this.cmbCompanyCodeValue.Focus();
                return;
            }
            else if (this.txtStoreName.Text.Trim() == string.Empty)
            {
                base.MessageError(base.GetMessage("MustStoreName"), base.GetMessage("Caption"));
                //
                this.txtStoreName.Focus();
                return;
            } //检测关店日期
            else if (this.dtpCloseDateValue.Value.Date < this.dtpOpenDateValue.Value.Date)
            {
                base.MessageError(base.GetMessage("CloseDateError"), base.GetMessage("Caption"));
                //
                this.cmbCompanyCodeValue.Focus();
                return;
            } //检测Email
            else if (!this.txtEmailAddress.Check(CheckType.Email))
            {
                base.MessageError(String.Format(base.GetMessage("MustEmail"), base.GetMessage("lblEmailAddress").TrimEnd(':')));
                this.txtEmailAddress.Focus();
                //
                return;
            } //检测状态
            else if (this.ddlStatus.SelectedIndex == 0)
            {
                base.MessageError(base.GetMessage("MustStatus"), base.GetMessage("Caption"));
                //
                this.ddlStatus.Focus();
                return;
            }
            // 保存
            Guid userID = AppCode.SysEnvironment.CurrentUser.ID;
            string CompanyCode = this.cmbCompanyCodeValue.SelectedValue.ToString();
            string StoreNo = this.entity.StoreNo;
            string StoreName = this.txtStoreName.Text.Trim();
            string SimpleName = this.txtSimpleName.Text.Trim();
            DateTime? OpenDate = null;
            if (this.dtpOpenDateValue.Enabled)
            {
                OpenDate = this.dtpOpenDateValue.Value;
            }
            else {
                OpenDate = this.entity.OpenDate;
            }
            DateTime? CloseDate = this.dtpCloseDateValue.Value;
            if (CloseDate.HasValue)
            {
                //如果存在合同, 如果合同大于 则不允许,  如果小于, 则提醒
                //return;
            }
            string EmailAddress = this.txtEmailAddress.Text.Trim();
            string Status = this.ddlStatus.SelectedValue.ToString();
            //
            int res = this.StoreBLL.UpdateSingleStore(StoreNo, CompanyCode, StoreName, SimpleName, 
                OpenDate, CloseDate, Status, EmailAddress, userID);
            switch (res)
            {
                case 0:
                    base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                    //
                    if (base.ParentFrm != null)
                    {
                        base.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                    break;
                case -1:
                    base.MessageInformation(base.GetMessage("ExistStoreName"), base.GetMessage("Caption"));
                    //
                    this.txtStoreName.Focus();
                    break;
                default:
                    MessageError(base.GetMessage("SaveFailure"), base.GetMessage("Caption"));
                    break;
            }
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StoreBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }
    }
}