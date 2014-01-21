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
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.Common;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TypeCodeEdit : BaseEdit
    {
        //Fields
        private bool isAdd = false;
        public bool ReadOnly = false;
        private SelectBLL SelectBLL = new SelectBLL();
        private TypeCodeBLL TypeCodeBLL = new TypeCodeBLL();
        private TypeCodeEntity entity = null;
        #region ctor

        public TypeCodeEdit(string typeCodeSnapshotID)
        {
            this.InitializeComponent();
            //
            this.isAdd = String.IsNullOrEmpty(typeCodeSnapshotID);
            //
            this.entity = this.TypeCodeBLL.SelectSingleTypeCode(typeCodeSnapshotID);
            this.entity.OperationID = AppCode.SysEnvironment.CurrentUser.ID.ToString();  //初始化操作者
        }
        #endregion

        private void TypeCodeEdit_Load(object sender, EventArgs e)
        {
            // 绑定租金类型 不变表
            DataTable dtValue = base.GetDataTable("RentType", () => {
                DataSet dsRentType = this.SelectBLL.SelectRentType();
                if (dsRentType != null && dsRentType.Tables.Count > 0)
                {
                    return dsRentType.Tables[0];
                }
                return null;
            });
            ControlHelper.BindComboBox(this.cbRentType, dtValue, "txt", "val");
            // 绑定实体类型 不变表
            dtValue = base.GetDataTable("EntityType", () =>
            {
                DataSet dsEntityType = this.SelectBLL.SelectEntityType();
                if (dsEntityType != null && dsEntityType.Tables.Count > 0)
                {
                    return dsEntityType.Tables[0];
                }
                return null;
            });
            ControlHelper.BindComboBox(this.cbEntityType, dtValue, "txt", "val");
            //
            DataSet ds = this.SelectBLL.SelectActiveAccount();
            ControlHelper.BindComboBox(this.cbBFGLCredit, ds);
            ControlHelper.BindComboBox(this.cbBFGLDebit, ds);
            ControlHelper.BindComboBox(this.cbYFAPDifferences, ds);
            ControlHelper.BindComboBox(this.cbYFAPNormal, ds);
            ControlHelper.BindComboBox(this.cbYFGLCredit, ds);
            ControlHelper.BindComboBox(this.cbYFGLDebit, ds);
            ControlHelper.BindComboBox(this.cbYTAPDifferences, ds);
            ControlHelper.BindComboBox(this.cbYTAPNormal, ds);
            ControlHelper.BindComboBox(this.cbYTGLCredit, ds);
            ControlHelper.BindComboBox(this.cbYTGLDebit, ds);
            ControlHelper.BindComboBox(this.cbZXGLCredit, ds);
            ControlHelper.BindComboBox(this.cbZXGLDebit, ds);
            //
            this.txtBFRemak.DataBindings.Add("Text", entity, "BFRemak");
            this.txtDescription.DataBindings.Add("Text", entity, "Description");
            this.txtInvoicePrefix.DataBindings.Add("Text", entity, "InvoicePrefix");
            this.txtTypeCode.DataBindings.Add("Text", entity, "TypeCodeName");
            this.txtUpdateInfo.DataBindings.Add("Text", entity, "UpdateInfo");
            this.txtYFRemak.DataBindings.Add("Text", entity, "YFRemak");
            this.txtYTRemark.DataBindings.Add("Text", entity, "YTRemark");
            this.txtZXRemark.DataBindings.Add("Text", entity, "ZXRemark");
            //
            this.cbBFGLCredit.DataBindings.Add("Text", entity, "BFGLCredit");
            this.cbBFGLDebit.DataBindings.Add("Text", entity, "BFGLDebit");
            this.cbEntityType.DataBindings.Add("Text", entity, "EntityTypeName");
            this.cbRentType.DataBindings.Add("Text", entity, "RentTypeName");
            this.cbYFAPDifferences.DataBindings.Add("Text", entity, "YFAPDifferences");
            this.cbYFAPNormal.DataBindings.Add("Text", entity, "YFAPNormal");
            this.cbYFGLCredit.DataBindings.Add("Text", entity, "YFGLCredit");
            this.cbYFGLDebit.DataBindings.Add("Text", entity, "YFGLDebit");
            this.cbYTAPDifferences.DataBindings.Add("Text", entity, "YTAPDifferences");
            this.cbYTAPNormal.DataBindings.Add("Text", entity, "YTAPNormal");
            this.cbYTGLCredit.DataBindings.Add("Text", entity, "YTGLCredit");
            this.cbYTGLDebit.DataBindings.Add("Text", entity, "YTGLDebit");
            this.cbZXGLCredit.DataBindings.Add("Text", entity, "ZXGLCredit");
            this.cbZXGLDebit.DataBindings.Add("Text", entity, "ZXGLDebit");
            //
            if (!this.isAdd)
            {
                this.txtTypeCode.Enabled = false;       //禁用typecode编辑框
                this.cbRentType.Enabled = false;        //禁用租金类型下拉框
                this.cbEntityType.Enabled = false;      //禁用实体类型下拉框
            }
            if (this.ReadOnly)
            {
                ControlHelper.DisEnabledControl(this.gbTypeCode);//
                //
                this.txtInvoicePrefix.Enabled = false;   //禁用其他typecode编辑框
                this.txtUpdateInfo.Enabled = false;
                this.txtDescription.Enabled = false;
            }
        }
        #region  输入触发验证

        /// <summary>
        /// 预付的四个科目当“租金类型”为：百分比租金;百分比管理费;百分比服务费三种时，该字段为非必填项, 反之必填写 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbRentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rentType = this.cbRentType.Text;
            if (!(new string[] { "百分比租金", "百分比管理费", "百分比服务费" }).Contains(rentType))
            {
                this.label11.Visible = true;
                this.label12.Visible = true;
                this.label13.Visible = true;
                this.label14.Visible = true;
            }
            else
            {
                this.label11.Visible = false;
                this.label12.Visible = false;
                this.label13.Visible = false;
                this.label14.Visible = false;
            }
        }
        private void cbYTGLDebit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYTGLDebit, this.cbYTGLCredit);
        }
        private void cbYTGLCredit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYTGLCredit, this.cbYTGLDebit);
        }
        private void cbYTAPNormal_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYTAPNormal, this.cbYTAPDifferences);
        }
        private void cbYTAPDifferences_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYTAPDifferences, this.cbYTAPNormal);
        }
        private void cbYFGLDebit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYFGLDebit, this.cbYFGLCredit);
        }
        private void cbYFGLCredit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYFGLCredit, this.cbYFGLDebit);
        }
        private void cbYFAPNormal_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYFAPNormal, this.cbYFAPDifferences);
        }
        private void cbYFAPDifferences_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbYFAPDifferences, this.cbYFAPNormal);
        }
        private void cbBFGLDebit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbBFGLDebit, this.cbBFGLCredit);
        }
        private void cbBFGLCredit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbBFGLCredit, this.cbBFGLDebit);
        }
        private void cbZXGLDebit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbZXGLDebit, this.cbZXGLCredit);
        }
        private void cbZXGLCredit_Leave(object sender, EventArgs e)
        {
            this.CheckCombox(this.cbZXGLCredit, this.cbZXGLDebit);
        }
        #endregion
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SelectBLL.Dispose();
            this.TypeCodeBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        /// <summary>
        /// 对比两个下拉列表选取的是否一样
        /// </summary>
        /// <param name="cb1"></param>
        /// <param name="cb2"></param>
        private void CheckCombox(ComboBox cb1, ComboBox cb2)
        {
            string debit = cb1.Text;
            string credit = cb2.Text;
            if (debit.NotEmpty() && credit.NotEmpty())
            {
                if (debit == credit)
                {
                    base.MessageError(base.GetMessage("NotSelTheSame"));
                    //
                    cb1.Focus();
                    return;
                }
            }
        }
    }
}