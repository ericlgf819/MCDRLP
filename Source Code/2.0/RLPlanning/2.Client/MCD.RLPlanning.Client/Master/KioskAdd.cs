using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.Common;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// Kiosk信息保存，新增。
    /// </summary>
    public partial class KioskAdd : BaseEdit
    {
        //实体
        private KioskBLL KioskBLL = new KioskBLL();
        private StoreBLL StoreBLL = new StoreBLL();
        private KioskEntity entity = new KioskEntity();
        private string opration = "add";
        #region ctor

        /// <summary>
        /// 初始化KioskInfo类的新实例。
        /// </summary>
        public KioskAdd()
        {
            this.InitializeComponent();
            //
            this.entity.FromSRLS = false;
            this.entity.KioskID = Guid.NewGuid().ToString();
            this.entity.KioskNo = KioskBLL.Instance.Update_Sequence();
            this.entity.KioskName = string.Format("虚拟甜品店{0}", this.entity.KioskNo);
            this.entity.Address = "虚拟甜品店地址";
            this.entity.IsNeedSubtractSalse = true;
        }
        /// <summary>
        /// 初始化KioskInfo类的新实例。
        /// </summary>
        public KioskAdd(string opration, KioskEntity kioskEntity)
        {
            this.InitializeComponent();
            //
            this.opration = opration;
            this.entity = kioskEntity;
            if (opration == "copy")
            {
                this.entity.KioskID = Guid.NewGuid().ToString();
                this.entity.KioskNo = KioskBLL.Instance.Update_Sequence();
                this.entity.KioskName = string.Format("虚拟甜品店{0}", this.entity.KioskNo);
            }
        }
        #endregion

        //Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dllStoreNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = this.ddlStoreNo.Text.Trim();
                //
                int recordCount = 0;
                DataTable dt = this.StoreBLL.USDX(null, null, text, null, null, AppCode.SysEnvironment.CurrentUser.ID,
                    -1, 50, out recordCount).Tables[0];
                //
                this.ddlStoreNo.DisplayMember = "StoreName";
                this.ddlStoreNo.ValueMember = "StoreNo";
                if (dt.Rows.Count == 1 && dt.Rows[0]["Status"] + string.Empty == "I" &&
                    (dt.Rows[0]["StoreNo"] + string.Empty == text || dt.Rows[0]["StoreName"] + string.Empty == text))
                {
                    base.MessageError(base.GetMessage("MustSelectActiveStore"), base.GetMessage("Caption"));
                    //
                    this.ddlStoreNo.DataSource = null;
                }
                else
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "Status='A'";
                    //
                    this.ddlStoreNo.DataSource = dv;
                    //this.ddlStoreNo.DroppedDown = true;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvHistory.Columns[e.ColumnIndex].Name == "IsNeedSubtractSalse")
            {
                if (e.Value.ToString() == "True")
                {
                    e.Value = base.GetMessage("Yes");
                }
                else
                {
                    e.Value = base.GetMessage("No");
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtKioskName.Text.Trim()))
            {
                base.MessageError(base.GetMessage("MustKioskName"), base.GetMessage("Caption"));
                //
                this.txtKioskName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(this.txtAddress.Text.Trim()))
            {
                base.MessageError(base.GetMessage("MustAddress"), base.GetMessage("Caption"));
                //
                this.txtAddress.Focus();
                return;
            }
            else if (this.ddlKioskType.SelectedIndex == -1)
            {
                base.MessageError(base.GetMessage("MustKiosType"), base.GetMessage("Caption"));
                //
                this.ddlKioskType.Focus();
                return;
            }
            else if (this.ddlStoreNo.SelectedIndex == -1 || this.ddlStoreNo.SelectedValue == null)
            {
                base.MessageError(base.GetMessage("MustStoreNo"), base.GetMessage("Caption"));
                //
                this.ddlStoreNo.Focus();
                return;
            }
            else if (this.ddlIsNeedSubtractSalse.SelectedIndex == -1)
            {
                base.MessageError(base.GetMessage("MustIsNeedSubtractSalse"), base.GetMessage("Caption"));
                //
                this.ddlIsNeedSubtractSalse.Focus();
                return;
            }
            else if (this.opration == "edit")
            {
                if (this.entity.FromSRLS.HasValue && this.entity.FromSRLS.Value)
                {
                    if (this.dtpActiveDate.Value < DateTime.Now)
                    {
                        base.MessageError(base.GetMessage("ActiveDateLessThanToday"), base.GetMessage("Caption"));
                        //
                        this.dtpActiveDate.Focus();
                        return;
                    }
                }
            }
            // 生效日期不能早于挂靠店的开店日期
            DateTime? OpenDate = this.StoreBLL.GetStoreOpenDate(this.ddlStoreNo.SelectedValue + string.Empty);
            if (OpenDate.HasValue && OpenDate.Value > this.dtpActiveDate.Value)
            {
                base.MessageError(base.GetMessage("ActiveDateLessThanStoreOpenDate"), base.GetMessage("Caption"));
                //
                this.dtpActiveDate.Focus();
                return;
            }
            // 保存
            this.entity.KioskNo = this.txtNewKioskNo.Text.Trim();
            this.entity.KioskName = this.txtKioskName.Text.Trim();
            this.entity.SimpleName = this.txtKioskName.Text.Trim();
            this.entity.KioskType = this.ddlKioskType.SelectedItem.ToString();
            this.entity.Address = this.txtAddress.Text.Trim();
            this.entity.StoreNo = this.ddlStoreNo.SelectedValue.ToString();
            this.entity.TemStoreNo = this.entity.StoreNo;
            this.entity.StoreName = (this.ddlStoreNo.SelectedItem as System.Data.DataRowView)["StoreName"] + string.Empty;
            this.entity.IsNeedSubtractSalse = this.ddlIsNeedSubtractSalse.SelectedIndex == 0;
            this.entity.ActiveDate = this.dtpActiveDate.Value;
            this.entity.Description = this.txtDescription.Text.Trim();
            //
            this.entity.OpenDate = DateTime.Now;
            this.entity.Status = "A";
            this.entity.IsEnable = true;
            this.entity.IsLocked = false;
            this.entity.CreateTime = DateTime.Now;
            this.entity.CreatorName = AppCode.SysEnvironment.CurrentUser.UserName;
            this.entity.LastModifyTime = DateTime.Now;
            this.entity.LastModifyUserName = AppCode.SysEnvironment.CurrentUser.UserName;
            //
            if (this.opration == "add" || this.opration == "copy")
            {
                this.InsertIntoDB();
            }
            else if (this.opration == "edit")
            {
                this.UpdateDB();
            }
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.KioskBLL.Dispose();
            this.StoreBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        //Methods
        public override void BindFormControl()
        {
            this.txtNewKioskNo.DataBindings.Add("Text", this.entity, "KioskNo");
            this.txtKioskName.DataBindings.Add("Text", this.entity, "KioskName");
            this.txtAddress.DataBindings.Add("Text", this.entity, "Address");
            // 绑定Kiosk类别下拉框
            this.ddlKioskType.Items.Clear();
            this.ddlKioskType.Items.Add("mccafe");
            this.ddlKioskType.Items.Add("kiosk");
            this.ddlKioskType.DropDownStyle = ComboBoxStyle.DropDownList;//
            if (null == this.entity.KioskType)
            {
                this.ddlKioskType.SelectedIndex = 1;
            }
            else if (this.entity.KioskType == "mccafe")
            {
                this.ddlKioskType.SelectedIndex = 0;
            }
            else if (this.entity.KioskType == "kiosk")
            {
                this.ddlKioskType.SelectedIndex = 1;
            }
            //  归属餐厅
            if (!string.IsNullOrEmpty(this.entity.TemStoreNo))
            {
                int recordCount = 0;
                DataTable dt = this.StoreBLL.USDX(null, null, this.entity.TemStoreNo, null, null, AppCode.SysEnvironment.CurrentUser.ID,
                    -1, 50, out recordCount).Tables[0];
                //
                this.ddlStoreNo.DisplayMember = "StoreName";
                this.ddlStoreNo.ValueMember = "StoreNo";
                this.ddlStoreNo.DataSource = dt;
                this.ddlStoreNo.DroppedDown = false;
            }
            // 初始化是否从母店信息中减除下拉框
            this.ddlIsNeedSubtractSalse.Items.Clear();
            this.ddlIsNeedSubtractSalse.Items.Add("是");
            this.ddlIsNeedSubtractSalse.Items.Add("否");
            this.ddlIsNeedSubtractSalse.DropDownStyle = ComboBoxStyle.DropDownList;//
            if (!this.entity.IsNeedSubtractSalse.HasValue || this.entity.IsNeedSubtractSalse.Value)
            {
                this.ddlIsNeedSubtractSalse.SelectedIndex = 0;
            }
            else
            {
                this.ddlIsNeedSubtractSalse.SelectedIndex = 1;
            }
            // 生效日期
            if (this.entity.ActiveDate.HasValue)
            {
                this.dtpActiveDate.Value = this.entity.ActiveDate.Value;
            }
            this.txtDescription.DataBindings.Add("Text", this.entity, "Description");
            // 编辑或者查看的时候加载挂靠记录
            this.BindGridView();
        }
        private void BindGridView()
        {
            this.dgvHistory.AutoGenerateColumns = false;
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToOrderColumns = true;
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            this.dgvHistory.DefaultCellStyle.SelectionForeColor = Color.Black;
            //
            this.dgvHistory.Columns.Clear();
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "StoreNo", base.GetMessage("StoreNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "IsNeedSubtractSalse", base.GetMessage("IsNeedSubtractSalse"), 180);
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "StartDate", base.GetMessage("StartDate"), 80,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_SHORT_FORMAT; });
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "EndDate", base.GetMessage("EndDate"), 80,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_SHORT_FORMAT; });
            GridViewHelper.PaintRowIndexToHeaderCell(this.dgvHistory);
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "CreateUserEnglishName", base.GetMessage("CreateUserEnglishName"), 80);
            GridViewHelper.AppendColumnToDataGridView(this.dgvHistory, "CreateTime", base.GetMessage("CreateTime"), 120,
                col => { col.DefaultCellStyle.Format = BaseFrm.DATETIME_LONG_FORMAT; });
            //
            this.dgvHistory.DataSource = this.KioskBLL.GetChangeRelationHistory(this.entity.KioskID);
        }
        private void InsertIntoDB()
        {
            int res = this.KioskBLL.Insert(this.entity);
            if (res == -1)
            {
                base.MessageInformation(base.GetMessage("ExistsKioskName"), base.GetMessage("Caption"));
                //
                this.txtKioskName.Focus();
            }
            else
            {
                //if (res == 0)
                //{
                //    base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                //}
                //else
                //{
                    base.MessageInformation(base.GetMessage("HasGLRecordClear"), base.GetMessage("Caption"));
                //}
                //
                if (this.ParentFrm != null)
                {
                    this.ParentFrm.RefreshList = true;
                }
                this.Close();
            }
        }
        private void UpdateDB()
        {
            int res = this.KioskBLL.Update(this.entity);
            if (res == -1)
            {
                base.MessageInformation(base.GetMessage("ExistsKioskName"), base.GetMessage("Caption"));
                //
                this.txtKioskName.Focus();
            }
            else
            {
                if (res == 0)
                {
                    base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                }
                else
                {
                    base.MessageInformation(base.GetMessage("HasGLRecordClear"), base.GetMessage("Caption"));
                }
                //
                if (this.ParentFrm != null)
                {
                    this.ParentFrm.RefreshList = true;
                }
                this.Close();
            }
        }
    }
}