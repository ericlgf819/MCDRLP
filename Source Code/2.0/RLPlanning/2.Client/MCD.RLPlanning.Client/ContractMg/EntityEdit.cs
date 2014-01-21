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
using MCD.Controls;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Entity.ContractMg;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.BLL.Setting;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EntityEdit : BaseEdit
    {
        public EntityEdit()
        {
            InitializeComponent();
            this.ReadOnly = false;
        }

        #region 字段和属性声明

        private EntityBLL entityBLL = new EntityBLL();
        private EntityTypeBLL entityTypeBLL = new EntityTypeBLL();
        private DeptBLL deptBLL = new DeptBLL();
        private ContractBLL contractBLL = new ContractBLL();
        private StoreBLL storeBLL = new StoreBLL();

        /// <summary>
        /// 是否加载中
        /// </summary>
        private bool m_IsLoading = true;

        public RentRuleAllInfo RentRuleAllInfo { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        public string EntityID { get; set; }

        /// <summary>
        /// 合同快照ID
        /// </summary>
        public string ContractSnapshotID { get; set; }

        /// <summary>
        /// 从外部传入的StoreNo
        /// </summary>
        public string StoreNoFromOutSide = string.Empty;

        /// <summary>
        /// 从外部传入的KioskNo
        /// </summary>
        public string KioskNoFromOutSide = string.Empty;

        /// <summary>
        /// 实体对象
        /// </summary>
        public EntityEntity Entity { get; set; }

        public bool ReadOnly { get; set; }
        public string TaskName { get; set; }

        /// <summary>
        /// 是否合同变更，如果是变更，则不能修改开业日
        /// </summary>
        public bool IsContractChange { get; set; }

        /// <summary>
        /// 获取或设置实体所属餐厅的归属公司编号。
        /// </summary>
        public string CompanyCode { get; set; }

        #endregion 字段和属性声明

        #region 重写基类方法

        private const string c_KioskName = "甜品店";
        private const string c_StoreName = "餐厅";

        public override void BindFormControl()
        {
            FrmWait frmwait = new FrmWait(() => {
               Init();
            }, base.GetMessage("Wait"), false);
            frmwait.ShowDialog();
        }

        private void Init()
        {
            //modified by Eric
            //将Entity初始化上提--Begin
            if (this.CurrentState != EDIT_STATUS.AddNew && this.EntityID.Trim().Length > 0)
            {
                this.Entity = entityBLL.SelectSingleEntity(this.EntityID);
                this.Entity.ContractSnapshotID = this.ContractSnapshotID;

                //当编辑kiosk时加载出kiosk编号
                if (this.Entity.EntityTypeName == c_KioskName)
                {
                    this.cmbKiosk.DataSource = null;
                    List<KioskEntity> kioskList = KioskBLL.Instance.Where(null, this.Entity.StoreOrDeptNo, "A", 0, 60).ToList();
                    this.cmbKiosk.DataSource = kioskList;
                    this.cmbKiosk.DisplayMember = "KioskName";
                    this.cmbKiosk.ValueMember = "KioskNo";
                }
            }
            else
            {
                this.Entity = new EntityEntity()
                {
                    ContractSnapshotID = this.ContractSnapshotID,
                    EntityID = Guid.NewGuid().ToString(),
                    RentStartDate = DateTime.Now,
                    RentEndDate = DateTime.Now.AddYears(10),
                    APStartDate = DateTime.Now,
                    OpeningDate = DateTime.Now,
                    IsCalculateAP = false,
                };
            }
            //将Entity初始化上提--End

            //Modified by Eric --Begin
            //上提代码
            //绑定下拉框
            //实体类型
            DataTable dtEntityType = GetDataTable("EntityType", () => { return this.entityTypeBLL.SelectEntityType(); });
            ControlHelper.BindComboBox(this.cmbEntityType, dtEntityType, "txt", "val");
            //Modified by Eric --End

            //如果外部有传入StoreNo时
            //则需在这里需要赋值，为了让界面能够显示
            if (string.Empty != StoreNoFromOutSide && string.Empty == KioskNoFromOutSide)
            {
                Entity.EntityTypeName = c_StoreName;
                Entity.StoreOrDeptNo = StoreNoFromOutSide;
                Entity.KioskNo = KioskNoFromOutSide;
            }
            else if (string.Empty != StoreNoFromOutSide && string.Empty != KioskNoFromOutSide)
            {
                Entity.EntityTypeName = c_KioskName;
                Entity.StoreOrDeptNo = StoreNoFromOutSide;
                Entity.KioskNo = KioskNoFromOutSide;
            }

            //是否出AP
            //是否出APbug修正
            bool origIsCalculateAP = Entity.IsCalculateAP;
            this.BindComboBoxFromDictionary(this.cmbIsCalculateAP, "IsCalculateAP");
            //因为BindComboBox会触发cmbIsCalculateAP_SelectedIndexChanged，导致初始化时永远都是true
            //所以要设置回来
            Entity.IsCalculateAP = origIsCalculateAP;

            base.BindFormControl();

            FillVendorList();//填充所属业主列表

            //默认为否，改为“2.是”后，不能再更改该字段。
            if (this.Entity.IsCalculateAP)
            {
                this.cmbIsCalculateAP.Enabled = false;
                this.cmbIsCalculateAP.SelectedValue = "1";
            }
            else
            {
                this.cmbIsCalculateAP.SelectedValue = "0";
            }

            if (this.ReadOnly)
            {
                this.cmbEntityType.Enabled = this.btnSave.Enabled = false;
                base.EnabledControl(this.pnlEdit, false);
            }
            else
            {
                this.cmbEntityType.Enabled = this.btnSave.Enabled = true;
                base.EnabledControl(this.pnlEdit, true);
            }

            this.dtpOpeningDate.Enabled = !this.ReadOnly && !this.IsContractChange;

            this.bdsEntity.DataSource = this.Entity;

            this.m_IsLoading = false;

            if (this.Entity.OpeningDate.HasValue)
            {
                this.dtpOpeningDate.Value = this.Entity.OpeningDate.Value;
            }

            this.dtpRentStartDate.Value = this.Entity.RentStartDate;
            this.dtpRentEndDate.Value = this.Entity.RentEndDate;



            //fxh新增，编辑时候：当实体类型为kiosk时，Kiosk编号字段必填
            string value = Convert.ToString(this.cmbEntityType.SelectedValue).ToLower();
            if (this.CurrentState != EDIT_STATUS.AddNew && !string.IsNullOrEmpty(value) && !this.ReadOnly)
            {
                if (value == c_KioskName)
                {
                    this.lblKioskIsNull.Visible = true;
                    this.cmbKiosk.Enabled = true;
                    this.cmbStoreDeptNo.Enabled = false;
                    this.cmbStoreDeptNo.Enabled = true;
                }
                else
                {
                    this.lblKioskIsNull.Visible = false;
                    this.cmbKiosk.Enabled = false;
                    this.cmbKiosk.SelectedItem = null;
                    this.cmbStoreDeptNo.Enabled = true;
                }

                //开业日
                //当“实体类型”字段的值为“餐厅”或Kiosk，“开业日”字段可从MSIS同步至本系统。
                //当“实体类型”字段为1.餐厅或2.kiosk（mccafe）时，该字段必填
                if (value == c_StoreName || value == c_KioskName)
                {
                    this.dtpOpeningDate.Enabled = true;
                    this.lblOpeningDateNotice.Visible = true;
                    this.txtEntityName.ReadOnly = true;
                }
                else
                {
                    this.dtpOpeningDate.Enabled = false;
                    this.lblOpeningDateNotice.Visible = false;
                    this.txtEntityName.ReadOnly = false;
                }
            }

            if (this.ReadOnly)
            {
                this.cmbEntityType.Enabled = false;
            }
            else
            {
                if (this.IsContractChange && this.CurrentState != EDIT_STATUS.AddNew)
                {
                    //变更编辑时，只能修改时间、所属业主，其他都不能修改
                    this.cmbEntityType.Enabled = false;
                    this.cmbStoreDeptNo.Enabled = false;
                    this.txtStoreDept.ReadOnly = true;
                    this.cmbKiosk.Enabled = false;
                    this.txtEntityName.ReadOnly = true;

                    //起租日: 在没有AP/GL时,可随意改变,有AP/GL时起租日不可再改变
                    if (this.entityBLL.GetEntityAPGLIsRunning(this.Entity.EntityID))
                    {
                        this.dtpRentStartDate.Enabled = false;
                    }

                    //租赁到期日: 最小值,不能小于已发起AP/GL的周期结束时间值
                    this.dtpRentEndDate.MinDate = this.entityBLL.GetEntityAPGLMaxCycleEndDate(this.Entity.EntityID);

                }

                if (this.CurrentState == EDIT_STATUS.AddNew)
                {
                    this.dtpOpeningDate.Enabled = false;
                }

                //实体类型不为甜品店时，起租日也不能更改
                if (this.CurrentState == EDIT_STATUS.Edit && this.Entity.EntityTypeName != c_KioskName)
                {
                    this.dtpOpeningDate.Enabled = false;
                }
            }

            this.txtStoreDept.ReadOnly = true;

            //如果外部有传入StoreNo时
            //则需在这里强强制设置相关控件的可见性和可用性
            if (string.Empty != StoreNoFromOutSide && string.Empty == KioskNoFromOutSide)
            {
                //餐厅不能有甜品店的相关选项
                this.lblKioskIsNull.Visible = false;
                this.cmbKiosk.Enabled = false;
                this.cmbKiosk.SelectedIndex = -1;
                this.cmbStoreDeptNo.Enabled = true;
                //餐厅/甜品店的实体名称不能修改
                this.txtEntityName.ReadOnly = true;
            }
            else if (string.Empty != StoreNoFromOutSide && string.Empty != KioskNoFromOutSide)
            {
                //甜品店不能有餐厅的相关选项
                this.lblKioskIsNull.Visible = true;
                this.cmbKiosk.Enabled = true;
                this.cmbStoreDeptNo.Enabled = false;
                this.cmbStoreDeptNo.Enabled = true;
                //餐厅/甜品店的实体名称不能修改
                this.txtEntityName.ReadOnly = true;
            }

            //手动触发一次回车回调事件，来触发外部传入的StoreNo
            if (string.Empty != StoreNoFromOutSide)
            {
                cmbStoreDeptNo_KeyUp(this, new KeyEventArgs(Keys.Enter));
            }
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                base.btnSave_Click(sender, e);
                string info = (this.CurrentState == EDIT_STATUS.AddNew ?
                        base.GetMessage("AddEntitySuccess") : base.GetMessage("EditEntitySuccess"));
                this.MessageInformation(info);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //释放资源
            this.entityBLL.Dispose();
            base.OnFormClosing(e);
        }


        #endregion 重写基类方法

        #region 控件事件处理
        private void EntityEdit_Load(object sender, EventArgs e)
        {

        }

        private void cmbStoreDeptNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(this.cmbStoreDeptNo.Text))
            {
                string value = this.cmbEntityType.SelectedValue.ToString().ToLower();
                string filter = string.Format("StoreDeptNo='{0}'", this.cmbStoreDeptNo.Text);

                if (value == c_StoreName || value == c_KioskName)
                {
                    filter = string.Format("{0} AND IsStore=1", filter);
                }

                // 获取餐厅和部门编号
                DataTable dtStoreDept = null;
                FrmWait frm = new FrmWait(() =>
                {
                    base.ExecuteAction(() =>
                    {
                        dtStoreDept = deptBLL.SelectActiveStoreDept(this.CompanyCode);
                    }, "读取餐厅部门信息出错", "读取餐厅部门信息出错");

                }, base.GetMessage("Wait"), () =>
                {
                    this.deptBLL.CloseService();
                });
                frm.ShowDialog();

                if (dtStoreDept == null)
                    return;

                DataRow[] drs = dtStoreDept.Select(filter);
                if (drs == null || drs.Length != 1)
                {
                    base.MessageError(base.GetMessage("StoreDeptNoError"));
                    return;
                }

                DataRow dr = drs[0];
                string storeDeptName = dr["StoreDeptName"] == DBNull.Value ? "" : dr["StoreDeptName"].ToString();
                string storeDeptNo = dr["StoreDeptNo"].ToString();
                this.txtStoreDept.Text = this.Entity.StoreOrDept = storeDeptName;
                this.Entity.StoreOrDeptNo = storeDeptNo;
                //Commented by Eric -- Begin
                //this.txtEntityName.Text = this.Entity.EntityName = storeDeptName;
                //Commented by Eric -- End

                //Added by Eric -- Begin
                //实体类型是餐厅或者甜品店，实体名称为餐厅名称
                if (this.Entity.EntityTypeName == c_KioskName || this.Entity.EntityTypeName == c_StoreName)
                {
                    this.txtEntityName.Text = this.Entity.EntityName = storeDeptName;
                }
                //其它类型实体，如果实体有名称则不需要将餐厅名称代替实体名称
                else
                {
                    if (String.IsNullOrEmpty(this.Entity.EntityName))
                    {
                        this.txtEntityName.Text = this.Entity.EntityName = storeDeptName;
                    }
                }
                //Added by Eric -- End

                //为餐厅时带出开业日
                //if (value == c_StoreName || value == c_KioskName)// && this.m_Store != null)
                //{
                this.m_Store = this.storeBLL.SelectSingleStore(storeDeptNo);
                //只要输入的餐厅部门编号可以查找到餐厅，则将餐厅开业日带出
                if (this.m_Store != null)
                {
                    this.Entity.OpeningDate = (this.m_Store.OpenDate.HasValue ? this.m_Store.OpenDate : DateTime.Now);
                    this.dtpOpeningDate.Value = this.Entity.OpeningDate.Value;
                    this.Entity.RentEndDate = (this.m_Store.RentEndDate.HasValue ? this.m_Store.RentEndDate.Value : DateTime.Now.AddYears(10));

                    //commented by Eric
                    //this.dtpRentStartDate.Value = this.Entity.OpeningDate.Value;
                    //this.dtpRentEndDate.Value = this.Entity.RentEndDate;
                }
                //}

                this.cmbKiosk.DataSource = null;
                //若实体类型为kiosk则加载出所输入餐厅的甜品店信息
                if (value == c_KioskName)
                {
                    this.cmbKiosk.DataSource = null;
                    List<KioskEntity> kioskList = KioskBLL.Instance.Where(null, Convert.ToString(dr["StoreDeptNo"]), "A", 0, 60);
                    this.cmbKiosk.DisplayMember = "KioskName";
                    this.cmbKiosk.ValueMember = "KioskNo";
                    this.cmbKiosk.DataSource = kioskList;

                    //将外部传入的KioskNo设置到控件中去
                    if (string.Empty != KioskNoFromOutSide)
                    {
                        int iSelectedIndex = 0;
                        foreach (var item in kioskList)
                        {
                            if (KioskNoFromOutSide == item.KioskNo)
                                break;

                            ++iSelectedIndex;
                        }

                        if (kioskList.Count != 0)
                            cmbKiosk.SelectedIndex = iSelectedIndex;

                        cmbKiosk.Enabled = true;

                        //将KioskNoFromOutSide置为string.Empty,否则用户无法更改KisokNo
                        KioskNoFromOutSide = string.Empty;
                    }
                }

                this.txtStoreDept.ReadOnly = true;
            }
        }

        private void cmbKiosk_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        //private string m_PreSelectEntityTypeName = "";
        private void cmbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //变更时，允许修改租赁结束日，其他都不允许修改
            //added by Eric--Begin
            if (CurrentState != EDIT_STATUS.View)
            //added by Eric--End
            {
                if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更
                    && (AppCode.SysEnvironment.CurrentContractSnapshotID == 
                        AppCode.SysEnvironment.EditingContractSnapshotID) && this.TaskName != "审核")
                {
                    this.pnlEdit.Enabled = true;
                    foreach (Control control in this.pnlEdit.Controls)
                    {
                        if (!(control is Label))
                        {
                            control.Enabled = false;
                        }
                    }
                    this.dtpRentStartDate.Enabled = !this.contractBLL.IsEntityHasAP(this.Entity.EntityID);
                    this.dtpRentEndDate.Enabled = true;
                    this.dtpAPStartDate.Enabled = true;
                    this.cmbIsCalculateAP.Enabled = true;
                    this.btnSave.Enabled = true;
                }
            }


            if (this.ReadOnly || AppCode.SysEnvironment.CurrentContractSnapshotID != AppCode.SysEnvironment.EditingContractSnapshotID)
            {
                return;
            }

            //this.pnlEdit.Enabled = (this.cmbEntityType.SelectedItem != null);
            base.EnabledControl(this.pnlEdit, this.cmbEntityType.SelectedItem != null);
            if (!this.m_IsLoading && this.cmbEntityType.SelectedItem != null)
            {
                string value = this.cmbEntityType.SelectedValue.ToString().ToLower();

                //当实体类型为kiosk时，Kiosk编号字段必填
                if (value == c_KioskName)
                {
                    this.lblKioskIsNull.Visible = true;
                    this.cmbKiosk.Enabled = true;
                    this.cmbStoreDeptNo.Enabled = true;
                }
                else
                {
                    this.lblKioskIsNull.Visible = false;
                    this.cmbKiosk.Enabled = false;
                    this.cmbKiosk.SelectedIndex = -1;
                    this.cmbStoreDeptNo.Enabled = true;
                }

                //开业日
                //当“实体类型”字段的值为“餐厅”或Kiosk，“开业日”字段可从MSIS同步至本系统。
                //当“实体类型”字段为1.餐厅或2.kiosk（mccafe）时，该字段必填
                if (value == c_StoreName || value == c_KioskName)
                {
                    this.dtpOpeningDate.Enabled = true;
                    this.lblOpeningDateNotice.Visible = true;
                    this.txtEntityName.ReadOnly = true;
                }
                else
                {
                    this.dtpOpeningDate.Enabled = false;
                    this.lblOpeningDateNotice.Visible = false;
                    this.txtEntityName.ReadOnly = false;
                }

                //只有当类型为甜品店时，才允许修改开业日
                this.dtpOpeningDate.Enabled = (value == c_KioskName);


                // 获取餐厅和部门编号
                //DataTable dtStoreDept = GetDataTable("ActiveStoreDept", () =>
                //    {
                //        return remindBLL.SelectActiveStoreDept();
                //    });

                ////如果是餐厅或者是甜品店，把餐厅部门下拉框中的值填充为餐厅，否则填充为全部
                //if (value == c_StoreName || value == c_KioskName)
                //{
                //    this.cmbStoreDeptNo.DataSource = null;
                //    //刷新成餐厅
                //    var stores = from r in dtStoreDept.AsEnumerable()
                //                 where r.Field<bool>("IsStore") == true
                //                 select r;
                //    DataTable dt = dtStoreDept.Clone();
                //    foreach (var item in stores)
                //    {
                //        dt.ImportRow(item);
                //    }

                //    ControlHelper.BindComboBox(this.cmbStoreDeptNo, dt, "StoreDeptNo", "StoreDeptNo");

                //    this.cmbStoreDeptNo.SelectedIndex = -1;//默认未选择任何项

                //    if (this.cmbStoreDeptNo.SelectedItem != null)
                //    {
                //        DataRowView row = this.cmbStoreDeptNo.SelectedItem as DataRowView;
                //        this.txtEntityName.Text = this.Entity.EntityName = row["StoreDeptName"] == DBNull.Value ? "" : row["StoreDeptName"].ToString();
                //    }
                //}
                //else
                //{
                //    if (this.m_PreSelectEntityTypeName == c_StoreName)
                //    {
                //        this.cmbStoreDeptNo.DataSource = null;
                //        //刷新成全部
                //        ControlHelper.BindComboBox(this.cmbStoreDeptNo, dtStoreDept, "StoreDeptNo", "StoreDeptNo");
                //    }
                //}

                //this.m_PreSelectEntityTypeName = value;
                this.cmbStoreDeptNo.Text = this.Entity.StoreOrDeptNo = string.Empty;
                this.txtEntityName.Text = this.Entity.EntityName = string.Empty;
                this.txtStoreDept.Text = this.Entity.StoreOrDept = string.Empty;
                this.cmbKiosk.DataSource = null;
            }
            //当是否出AP为否时，禁用出AP日期
            if (this.cmbIsCalculateAP.SelectedItem != null)
            {
                this.dtpAPStartDate.Enabled = (this.cmbIsCalculateAP.SelectedValue.ToString() == "1");
            }

            this.txtStoreDept.ReadOnly = true;
        }

        private StoreEntity m_Store;
        private void cmbStoreDeptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ReadOnly)
            //{
            //    return;
            //}

            //if (!this.m_IsLoading && this.cmbStoreDeptNo.SelectedItem != null)
            //{
            //    DataRowView row = this.cmbStoreDeptNo.SelectedItem as DataRowView;

            //    string storeDeptName = row["StoreDeptName"] == DBNull.Value ? "" : row["StoreDeptName"].ToString();
            //    string storeDeptNo = row["StoreDeptNo"] == DBNull.Value ? "" : row["StoreDeptNo"].ToString();
            //    this.m_Store = this.storeBLL.SelectSingleStore(storeDeptNo);

            //    if (this.Entity != null)
            //    {
            //        this.Entity.StoreOrDept = storeDeptName;
            //        this.Entity.StoreOrDeptNo = storeDeptNo;

            //        //为餐厅时带出开业日
            //        if (this.Entity.EntityTypeName == c_StoreName)
            //        {
            //            this.txtEntityName.Text = this.Entity.EntityName = storeDeptName;

            //            if (this.m_Store != null)
            //            {
            //                if (this.m_Store.OpenDate.HasValue)
            //                {
            //                    this.Entity.OpeningDate = this.m_Store.OpenDate;
            //                }
            //                else
            //                {
            //                    this.Entity.OpeningDate = DateTime.Now;
            //                }
            //                this.dtpOpeningDate.Value = this.Entity.OpeningDate.Value;

            //                if (this.m_Store.RentEndDate.HasValue)
            //                {
            //                    this.Entity.RentEndDate = this.m_Store.RentEndDate.Value;
            //                }
            //                else
            //                {
            //                    this.Entity.RentEndDate = DateTime.Now;
            //                }
            //                this.dtpRentStartDate.Value = this.Entity.OpeningDate.Value;
            //                this.dtpRentEndDate.Value = this.Entity.RentEndDate;
            //            }
            //        }
            //    }

            //    this.txtStoreDept.Text = storeDeptName;

            //    //若实体类型为甜品店，则按餐厅加载旗下的所有甜品店
            //    if (this.Entity.EntityTypeName == c_KioskName)
            //    {
            //        this.cmbKiosk.Text = string.Empty;
            //        List<SRLS_TB_Master_KioskEntity> kioskList = SRLS_TB_Master_KioskBLL.Instance.Where(null, storeDeptNo, WorkflowBizStatus.已生效.ToString());
            //        this.cmbKiosk.DataSource = kioskList;
            //        this.cmbKiosk.DisplayMember = "KioskNo";
            //        this.cmbKiosk.ValueMember = "KioskNo";
            //    }
            //}
            //else
            //{
            //    this.txtStoreDept.Text = "";
            //}
        }

        private void cmbKiosk_SelectedIndexChanged(object sender, EventArgs e)
        {
            //commented by Eric --Begin
            //if (this.ReadOnly)
            //{
            //    return;
            //}
            //commented by Eric --End

            ////若该记录为kiosk，选择了kiosk编号后，“餐厅/部门编号”及“实体名称”自动带出。
            ////若该记录为非kiosk，选择了 “餐厅/部门编号” 自动带出“实体名称”，“kiosk编号”字段无效。
            if (!this.m_IsLoading && this.cmbEntityType.SelectedItem != null
                && this.cmbEntityType.SelectedValue.ToString().ToLower() == c_KioskName)
            {
                KioskEntity entity = this.cmbKiosk.SelectedItem as KioskEntity;
                //带出餐厅/部门编号，实体名称，开业日期
                //this.cmbStoreDeptNo.SelectedText = entity.StoreNo;
                //this.cmbStoreDeptNo.SelectedValue = entity.StoreNo;
                if (entity != null)
                {
                    this.cmbStoreDeptNo.SelectedValue = entity.StoreNo;
                    this.txtEntityName.Text = entity.KioskName;
                    if (this.Entity != null)
                    {
                        this.txtEntityName.Text = this.Entity.EntityName = entity.KioskName;
                        this.Entity.KioskNo = entity.KioskNo;
                    }

                    if (entity.OpenDate.HasValue)
                    {
                        this.dtpOpeningDate.Value = entity.OpenDate.Value;
                    }
                }
                //if (this.Entity != null)
                //{
                //    this.Entity.EntityName = entity.KioskName;
                //}

                //if (entity.OpenDate.HasValue)
                //{
                //    this.dtpOpeningDate.Value = entity.OpenDate.Value;
                //    //this.dtpOpeningDate.Checked = true;
                //}
                //else
                //{
                //    //this.dtpOpeningDate.Checked = false;
                //}
            }
        }

        //是否出AP
        private void cmbIsCalculateAP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbIsCalculateAP.SelectedItem != null && this.Entity != null)
            {
                string value = this.cmbIsCalculateAP.SelectedValue.ToString();
                if (value == "1")
                {
                    this.Entity.IsCalculateAP = true;
                    //this.dtpAPStartDate.Checked = true;
                    this.dtpAPStartDate.Enabled = true;
                }
                else
                {
                    this.Entity.IsCalculateAP = false;
                    //this.dtpAPStartDate.Checked = false;
                    this.dtpAPStartDate.Enabled = false;
                }
            }
        }

        #endregion 控件事件处理

        #region 私有方法

        private bool Save()
        {
            if (this.GetCurrentEntity())
            {
                if (this.CurrentState == EDIT_STATUS.AddNew)
                {
                    EntityEntity entity = this.RentRuleAllInfo.EntityList.FirstOrDefault(item => item.EntityName == this.Entity.EntityName);
                    if (entity == null)
                    {
                        this.entityBLL.InsertSingleEntity(this.Entity);
                    }
                    else
                    {
                        this.MessageInformation(base.GetMessage("NoticeEntityRepeat"));
                        return false;
                    }
                }
                else
                {
                    EntityEntity entity = this.RentRuleAllInfo.EntityList
                        .FirstOrDefault(item => item.EntityID != this.Entity.EntityID && item.EntityName == this.Entity.EntityName);
                    if (entity == null)
                    {
                        //Added by Eric -- Begin
                        DetermineRentDateOverlap(this.Entity);
                        //Added by Eric -- End
                        this.entityBLL.UpdateSingleEntity(this.Entity);
                    }
                    else
                    {
                        this.MessageInformation(base.GetMessage("NoticeEntityRepeat"));
                        return false;
                    }
                }

                //保存业主实体关系
                List<string> vendorNos = new List<string>();
                foreach (VendorEntityEntity item in this.m_NewVendorList)
                {
                    vendorNos.Add(item.VendorNo);
                }

                string vendorNoArray = string.Join(";", vendorNos.ToArray());
                this.entityBLL.CheckVendorEntity(this.Entity.EntityID, vendorNoArray, AppCode.SysEnvironment.ContractCopyType.ToString());

                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断当前修改的租赁起止日期去之前的是否有交集
        /// </summary>
        private void DetermineRentDateOverlap(EntityEntity curEntity)
        {
            EntityEntity lastEntity = RentRuleAllInfo.EntityList.FirstOrDefault(item => item.EntityID == curEntity.EntityID);
            if (lastEntity.RentEndDate < curEntity.RentStartDate)
            {
                AppCode.SysEnvironment.s_mapIsLastEntityRentDateOverlap[curEntity.EntityID] = true;
            }
        }

        private void ShowCurrentEntity()
        {
            this.bdsEntity.DataSource = this.Entity;
            if (this.Entity.OpeningDate.HasValue)
            {
                //this.dtpOpeningDate.Checked = true;
                this.dtpOpeningDate.Value = this.Entity.OpeningDate.Value;
            }
            else
            {
                //this.dtpOpeningDate.Checked = false;
            }

            string isCalcAP = this.Entity.IsCalculateAP ? "1" : "0";
            this.cmbIsCalculateAP.SelectedValue = isCalcAP;
            if (this.Entity.APStartDate.HasValue)
            {
                //this.dtpAPStartDate.Checked = true;
                this.dtpAPStartDate.Value = this.Entity.APStartDate.Value;
            }
            else
            {
                //this.dtpAPStartDate.Checked = false;
            }

        }

        private bool GetCurrentEntity()
        {
            // added by Eric -- Begin
            DateTime tmpTime = dtpRentEndDate.Value;
            // added by Eric -- End

            this.Validate();
            this.ValidateChildren();

            // added by Eric -- Begin
            if (tmpTime != dtpRentEndDate.Value)
                dtpRentEndDate.Value = tmpTime;
            // added by Eric -- End

            //有效性检查
            if (!UIChecker.VerifyComboxNull(this.cmbEntityType, base.GetMessage("NoticeChooseEntityType"), base.GetMessage("NoticeTitle")))
            {
                this.cmbEntityType.Focus();
                return false;
            }

            //if (!UIChecker.VerifyComboxNull(this.cmbStoreDeptNo, base.GetMessage("NoticeChooseStoreNo"), base.GetMessage("NoticeTitle")))
            //{
            //    this.cmbStoreDeptNo.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(this.cmbStoreDeptNo.Text))
            {
                base.MessageError(base.GetMessage("NoticeChooseStoreNo"));
                this.cmbStoreDeptNo.Focus();
                return false;
            }

            if (this.Entity.EntityTypeName.ToLower() == c_KioskName)
            {
                if (!UIChecker.VerifyComboxNull(this.cmbKiosk, base.GetMessage("NoticeChooseKioskNo"), base.GetMessage("NoticeTitle")))
                {
                    this.cmbKiosk.Focus();
                    return false;
                }
            }
            else
            {
                this.Entity.KioskNo = string.Empty;
            }

            //commented by Eric--Begin
            //if (this.Entity.EntityTypeName.ToLower() == c_StoreName
            //    && this.m_Store != null && this.m_Store.RentEndDate.HasValue)
            //{
            //    //租赁到期日
            //    //当“实体类型”字段为1.餐厅时，MSIS中有该字段的值。
            //    //用户录入时，将用户录入的值与MSIS同步过来的值比较。
            //    //若不一致，则提醒用户“MSIS中值为yyyymmdd，与录入的值不符，请确认！”，但系统允许录入。
            //    if (!this.Entity.RentEndDate.ToString("yyyyMMdd").Equals(this.m_Store.RentEndDate.Value.ToString("yyyyMMdd")))
            //    {
            //        if (this.MessageConfirm(string.Format(base.GetMessage("NoticeConfirmRentEndDate"),
            //            this.m_Store.RentEndDate.Value.ToString("yyyy-MM-dd"))) != DialogResult.OK)
            //        {
            //            this.dtpOpeningDate.Focus();
            //            return false;
            //        }
            //    }
            //}
            //commented by Eric--End

            string dateFormat = "yyyyMMdd";
            string openDate = this.dtpOpeningDate.Value.ToString(dateFormat);
            string startDate = this.dtpRentStartDate.Value.ToString(dateFormat);
            string endDate = this.dtpRentEndDate.Value.ToString(dateFormat);
            //租赁到期日必须大于起租日
            if (endDate.CompareTo(startDate) < 0)
            {
                this.MessageInformation(base.GetMessage("EndDateLessThenStartDate"));
                this.dtpRentEndDate.Focus();
                return false;
            }
            ////开业日必须大于等于起租日
            //if (openDate.CompareTo(startDate) < 0)
            //{
            //    this.MessageInformation(base.GetMessage("OpenDateLessThenStartDate"));
            //    this.dtpRentStartDate.Focus();
            //    return false;
            //}

            if (this.Entity.EntityTypeName.ToLower() == c_KioskName)
            {
                //TEMP:AL:暂不处理：若该实体为kiosk，当kiosk的租期不在母店租期内时，需提醒，但允许录入。
            }

            if (!UIChecker.VerifyTextBoxNull(this.txtEntityName, base.GetMessage("NoticeInputEntityName"), base.GetMessage("NoticeTitle")))
            {
                this.txtEntityName.Focus();
                return false;
            }

            if (this.Entity.EntityTypeName == c_KioskName || this.Entity.EntityTypeName == c_StoreName)
            {
                this.Entity.OpeningDate = this.dtpOpeningDate.Value;
            }
            else
            {
                this.Entity.OpeningDate = null;
            }

            //检查是否选中了业主
            if (this.cklVendor.CheckedItems.Count == 0)
            {
                this.MessageInformation(base.GetMessage("NoticeChooseVendor"));
                this.cklVendor.Focus();
                return false;
            }
            else
            {
                this.GetVendorList();//获取选中的业主列表
            }

            if (UIChecker.VerifyComboxNull(this.cmbIsCalculateAP, base.GetMessage("NoticeChooseIsCalcAP"), base.GetMessage("NoticeTitle")))
            {
                this.Entity.IsCalculateAP = (this.cmbIsCalculateAP.SelectedValue.ToString() == "1");
            }
            else
            {
                this.cmbIsCalculateAP.Focus();
                return false;
            }

            if (this.Entity.IsCalculateAP)
            {
                this.Entity.APStartDate = this.dtpAPStartDate.Value;
            }
            else
            {
                this.Entity.APStartDate = null;
            }

            // 检查当前租赁起止日期是否与其它合同的日期冲突
            //entityBLL.ValidateRentBeginEndDate(Entity);

            return true;
        }

        //当前实体已关联的Vendor列表
        private List<VendorEntityEntity> m_OldVendorList = new List<VendorEntityEntity>();
        //当前实体编辑后关联的Vendor列表
        private List<VendorEntityEntity> m_NewVendorList = new List<VendorEntityEntity>();

        private void GetVendorList()
        {
            m_NewVendorList.Clear();
            foreach (VendorContractEntity item in this.cklVendor.CheckedItems)
            {
                VendorEntityEntity entity = new VendorEntityEntity()
                    {
                        EntityID = this.Entity.EntityID,
                        EntityName = this.Entity.EntityName,
                        VendorEntityID = Guid.NewGuid().ToString(),
                        VendorName = item.VendorName,
                        VendorNo = item.VendorNo,
                    };
                this.m_NewVendorList.Add(entity);
            }
        }

        private void FillVendorList()
        {
            //合同选择的Vendor
            List<VendorContractEntity> vendorContractList = new List<VendorContractEntity>();

            //获取当前合同的的Vendor
            DataSet ds = contractBLL.SelectVendorContractByContractSnapshotID(this.ContractSnapshotID);
            if (ds != null && ds.Tables.Count > 0)
            {
                vendorContractList = ReflectHelper.ConvertDataTableToEntityList<VendorContractEntity>(ds.Tables[0]);
            }

            //获取当前已关联的业主
            ds = entityBLL.SelectVendorEntitiesByEntityID(this.EntityID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                m_OldVendorList = ReflectHelper.ConvertDataTableToEntityList<VendorEntityEntity>(ds.Tables[0]);
            }

            foreach (VendorContractEntity entity in vendorContractList)
            {
                if (m_OldVendorList.FirstOrDefault(item => item.VendorNo == entity.VendorNo) != null)
                {
                    this.cklVendor.Items.Add(entity, true);
                }
                else
                {
                    this.cklVendor.Items.Add(entity, false);
                }
            }
        }
        #endregion 私有方法
    }
}