﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CompanyAdd : BaseEdit
    {
        //Fields
        private AreaBLL AreaBLL = new AreaBLL();
        private CompanyBLL CompanyBLL = new CompanyBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        private CompanyEntity entity = new CompanyEntity();
        #region ctor

        public CompanyAdd()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyAdd_Load(object sender, EventArgs e)
        {
            // 实体默认值
            this.entity.FixedSourceCode = "60";
            this.entity.FixedManageSourceCode = "60";
            this.entity.StraightLineSourceCode = "60";
            this.entity.RatioSourceCode = "60";
            this.entity.RatioManageSourceCode = "60";
            this.entity.RatioServiceSourceCode = "60";
            // 绑定
            // 绑定区域 -- 同步表,可缓存
            DataTable dtValue = base.GetDataTable("Area", () =>
            {
                DataSet dsArea = this.AreaBLL.SelectAreas();
                if (dsArea != null && dsArea.Tables.Count == 1)
                {
                    return dsArea.Tables[0];
                }
                else
                {
                    return null;
                }
            });
            DataView dvValue = new DataView(dtValue);
            dvValue.Sort = "ShowOrder ASC";
            ControlHelper.BindComboBox(this.ddlArea, dvValue, "AreaName", "ID");
            // 绑定状态
            ControlHelper.BindComboBox(this.ddlStatus, base.DTStatus, "StatusName", "StatusValue");
            this.ddlStatus.SelectedValue = 'A';
            // 绑定实体字段
            this.txtCompanyCodeValue.DataBindings.Add("Text", this.entity, "CompanyCode");
            this.txtCompanyNameValue.DataBindings.Add("Text", this.entity, "CompanyName");
            this.txtSimpleName.DataBindings.Add("Text", this.entity, "SimpleName");
            this.txtFixed.DataBindings.Add("Text", this.entity, "FixedSourceCode");
            this.txtFixedManage.DataBindings.Add("Text", this.entity, "FixedManageSourceCode");
            this.txtStraightLine.DataBindings.Add("Text", this.entity, "StraightLineSourceCode");
            this.txtRatio.DataBindings.Add("Text", this.entity, "RatioSourceCode");
            this.txtRatioManage.DataBindings.Add("Text", this.entity, "RatioManageSourceCode");
            this.txtRatioService.DataBindings.Add("Text", this.entity, "RatioServiceSourceCode");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.entity].EndCurrentEdit();
            // 校验
            if (string.IsNullOrEmpty(this.entity.CompanyCode))
            {
                base.MessageError(base.GetMessage("CompanyCodeNULL"), base.GetMessage("Caption"));
                //
                this.txtCompanyCodeValue.Focus();
                return;
            }
            else if (!Regex.IsMatch(this.entity.CompanyCode, @"\d+") || this.entity.CompanyCode.Length >= 5)
            {
                base.MessageError(base.GetMessage("CompanyCodeMustData"), base.GetMessage("Caption"));
                //
                this.txtCompanyCodeValue.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(this.entity.CompanyName))
            {
                base.MessageError(base.GetMessage("CompanyNameNULL"), base.GetMessage("Caption"));
                //
                this.txtCompanyNameValue.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(this.entity.SimpleName))
            {
                base.MessageError(base.GetMessage("SimpleNameNULL"), base.GetMessage("Caption"));
                //
                this.txtSimpleName.Focus();
                return;
            }
            else if (!this.txtFixed.Check(CheckType.MoneyType))
            {
                this.ShowErrorMessage("lblFixed");
                //
                this.txtFixed.Focus();
                return;
            }
            else if (!this.txtFixedManage.Check(CheckType.MoneyType))
            {
                this.ShowErrorMessage("lblFixedManage");
                //
                this.txtFixedManage.Focus();
                return;
            }
            else if (!this.txtRatioManage.Check(CheckType.MoneyType))
            {
                this.ShowErrorMessage("lblRatioManage");
                //
                this.txtRatioManage.Focus();
                return;
            }
            else if (!this.txtRatioService.Check(CheckType.MoneyType))
            {
                this.ShowErrorMessage("lblRatioService");
                //
                this.txtRatioService.Focus();
                return;
            }
            else if (!this.txtStraightLine.Check(CheckType.MoneyType))
            {
                this.ShowErrorMessage("lblStraightLine");
                //
                this.txtStraightLine.Focus();
                return;
            }
            else if (this.ddlStatus.SelectedIndex == 0)
            {
                base.MessageError(base.GetMessage("StatusNULL"), base.GetMessage("Caption"));
                //
                this.ddlStatus.Focus();
                return;
            }
            this.entity.AreaID = new Guid(this.ddlArea.SelectedValue.ToString());
            this.entity.Status = this.ddlStatus.SelectedValue.ToString();
            this.entity.FromSRLS = false;
            this.entity.OperationID = AppCode.SysEnvironment.CurrentUser.ID;
            this.entity.UpdateTime = DateTime.Now;
            // 保存
            int res = this.CompanyBLL.AddSingleCompany(this.entity);
            switch (res)
            {
                case 0:
                    base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                    //
                    if (this.ParentFrm != null)
                    {
                        this.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                    break;
                case -1:
                    base.MessageError(base.GetMessage("CompanyCodeExist"), base.GetMessage("Caption"));
                    break;
                case -2:
                    base.MessageError(base.GetMessage("CompanyNameExist"), base.GetMessage("Caption"));
                    break;
                case -3:
                    base.MessageError(base.GetMessage("SimpleNameExist"), base.GetMessage("Caption"));
                    break;
                default:
                    base.MessageError(base.GetMessage("SaveFailure"), base.GetMessage("Caption"));
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
            this.AreaBLL.Dispose();
            this.CompanyBLL.Dispose();
            this.UserCompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        /// <summary>
        /// 弹出错误信息
        /// </summary>
        /// <param name="lable">标签</param>
        private void ShowErrorMessage(string lable)
        {
            base.MessageError(String.Format(base.GetMessage("MustNum"), base.GetMessage(lable).TrimEnd(':')), base.GetMessage("Caption"));
        }
    }
}