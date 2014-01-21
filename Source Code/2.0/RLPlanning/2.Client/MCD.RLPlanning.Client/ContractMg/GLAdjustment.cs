using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.Common;
using System.Text.RegularExpressions;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.ContractMg;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// GL调整金额录入。
    /// </summary>
    public partial class GLAdjustment : BaseEdit
    {
        //当前调整记录对应的GL
        private GLAdjustmentEntity adjustmentEntity = null;
        private List<EntityRentType> RentTypeList = null;
        private EntityRentType CurrentRentType = new EntityRentType();

        public GLAdjustment(GLAdjustmentEntity entity)
        {
            InitializeComponent();
            this.adjustmentEntity = entity;
        }

        /// <summary>
        /// 初始化KioskChangeInfo类的新实例。
        /// </summary>
        /// <param name="entity"></param>
        public GLAdjustment(EntityRentType currentRentType, List<EntityRentType> rentTypeList)
        {
            InitializeComponent();
            this.adjustmentEntity = new GLAdjustmentEntity();
            this.CurrentRentType = currentRentType;
            this.RentTypeList = rentTypeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        protected override void SetFormRight(Form frm)
        { }

        /// <summary>
        /// 加载变更记录列表。
        /// </summary>
        private void LoadData()
        {
            this.lvChanges.Items.Clear();
            List<GLAdjustmentEntity> list = MCD.RLPlanning.BLL.Master.GLAdjustmentBLL.Instance.Where(this.CurrentRentType.EntityInfoSettingID,
                this.CurrentRentType.RuleSnapshotID,
                this.CurrentRentType.RuleID,
                this.CurrentRentType.ToString(),
                this.dtpChangeStart.Value, 
                this.dtpChangeEnd.Value).OrderByDescending(item => item.CreateTime).ToList();
            int index = 1;
            foreach (GLAdjustmentEntity e in list)
            {
                ListViewItem item = new ListViewItem(index.ToString());
                item.SubItems.Add(e.Amount.HasValue ? e.Amount.Value.ToString() : "");
                item.SubItems.Add(e.AdjustmentDate.HasValue ? e.AdjustmentDate.Value.ToShortDateString() : "");
                item.SubItems.Add(e.GLDateZone);
                item.SubItems.Add(e.Remark);
                item.SubItems.Add(e.CreatorName);
                item.SubItems.Add(e.CreateTime.Value.ToShortDateString());
                this.lvChanges.Items.Add(item);
                index++;
            }
        }

        /// <summary>
        /// 初始化列表控件。
        /// </summary>
        public override void BindFormControl()
        {
            this.cmbRentType.Items.Add(new EntityRentType());//添加一个空项
            if (this.RentTypeList != null)
            {
                foreach (EntityRentType t in this.RentTypeList)
                {
                    this.cmbRentType.Items.Add(t);
                }
            }

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 20);
            this.lvChanges.SmallImageList = imgList;
            this.lvChanges.Columns.Add("", 30, HorizontalAlignment.Center);
            this.lvChanges.Columns.Add(base.GetMessage("KioskSales"), 100, HorizontalAlignment.Left);
            this.lvChanges.Columns.Add(base.GetMessage("SalesDate"), 110, HorizontalAlignment.Center);
            this.lvChanges.Columns.Add(base.GetMessage("GLDateZone"), 200, HorizontalAlignment.Center);
            this.lvChanges.Columns.Add(base.GetMessage("Remark"), 180, HorizontalAlignment.Center);
            this.lvChanges.Columns.Add(base.GetMessage("InputSalseUserEnglishName"), 100, HorizontalAlignment.Center);
            this.lvChanges.Columns.Add(base.GetMessage("CreateTime"), 110, HorizontalAlignment.Center);
            this.lvChanges.View = View.Details;
            this.lvChanges.GridLines = true;
            this.lvChanges.FullRowSelect = true;

            //默认显示当前月的记录
            this.dtpChangeStart.Value = DateTime.Parse("1900-01-01");
            this.dtpChangeEnd.Value = DateTime.Now;
            this.dtpChangeDate.MaxDate = DateTime.Now;
            this.LoadData();
        }

        /// <summary>
        /// 窗体输入验证。
        /// </summary>
        /// <returns></returns>
        public override bool ValidInput()
        {
            //检查是否选择租金类型
            if (this.cmbRentType.SelectedIndex < 0 || this.cmbRentType.SelectedItem == null)
            {
                this.cmbRentType.Focus();
                base.MessageError(base.GetMessage("PleaseSelectRentType"));
                return false;
            }
            else if (this.cmbRentType.SelectedItem is EntityRentType)
            {
                if (string.IsNullOrEmpty((this.cmbRentType.SelectedItem as EntityRentType).ToString()))
                {
                    base.MessageError(base.GetMessage("PleaseSelectRentType"));
                    return false;
                }
            }
            if (this.tbAmount.Text.Trim().Empty() || !Regex.IsMatch(this.tbAmount.Text.Trim(), @"^-?\d{1,9}(\.\d{1,2})?$"))
            {
                this.tbAmount.Focus();
                base.MessageError(base.GetMessage("AmountErrMsg"));
                return false;
            }
            if (this.dtpChangeDate.Value == null)
            {
                this.dtpChangeDate.Focus();
                base.MessageError(base.GetMessage("ChangeDateEmptyMsg"));
                return false;
            }
            if (double.Parse(this.tbAmount.Text.Trim()) <= 0 && this.tbNote.Text.Trim().Empty())
            {
                this.tbNote.Focus();
                base.MessageError(base.GetMessage("NoteEmptyMsg"));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存数据。
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            adjustmentEntity.AdjustmentID = Guid.NewGuid().ToString();
            adjustmentEntity.Amount = decimal.Parse(this.tbAmount.Text.Trim());
            adjustmentEntity.AdjustmentDate = this.dtpChangeDate.Value;
            adjustmentEntity.Remark = this.tbNote.Text.Trim();
            adjustmentEntity.CreateTime = DateTime.Now;
            adjustmentEntity.CreatorName = AppCode.SysEnvironment.CurrentUser.EnglishName;
            adjustmentEntity.EntityInfoSettingID = this.CurrentRentType.EntityInfoSettingID;
            adjustmentEntity.RentType = this.CurrentRentType.ToString();
            adjustmentEntity.RuleSnapshotID = this.CurrentRentType.RuleSnapshotID;
            adjustmentEntity.RuleID = this.CurrentRentType.RuleID;
            MCD.RLPlanning.BLL.Master.GLAdjustmentBLL.Instance.Insert(adjustmentEntity);
            base.MessageInformation(base.GetMessage("SaveSucc"));
            this.LoadData();
            return false;
        }

        /// <summary>
        /// 点击查询。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        /// <summary>
        /// 点击重置。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.dtpChangeStart.Value = DateTime.Parse("1900-01-01");
            this.dtpChangeEnd.Value = DateTime.Now;
            this.LoadData();
        }

        /// <summary>
        /// 切换租金类型。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.adjustmentEntity != null && this.cmbRentType.SelectedItem != null)
            {
                EntityRentType selectItem = this.cmbRentType.SelectedItem as EntityRentType;
                this.CurrentRentType = selectItem;
            }
            this.LoadData();
        }
    }
}
