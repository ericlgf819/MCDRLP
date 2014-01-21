using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.Common;
using MCD.RLPlanning.Client.ForcastSales;
using MCD.RLPlanning.Business.ForecastSales;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.Client.AppCode;



namespace MCD.RLPlanning.Client.ForcastSales
{
    /// <summary>
    /// 
    /// </summary>
    public partial class KeyIn : BaseList
    {
        private SalesBLL salesBLL = null;
        private UserCompanyBLL UserCompanyBLL = null;
        private AreaBLL AreaBLL = null;

        public KeyIn()
        {
            InitializeComponent();

            salesBLL = new SalesBLL();
            UserCompanyBLL = new UserCompanyBLL();
            AreaBLL = new AreaBLL();
        }

        private void KeyIn_Load(object sender, EventArgs e)
        {
            //导入Sales按钮可见
            btnKeyInSales.Visible = true;
            //重置按钮可见
            btnReset.Visible = true;
            //其它多余按钮不可见
            btnAddnew.Visible = false;
            btnDelete.Visible = false;
            btnPreview.Visible = false;
            btnExport.Visible = false;
            //初始化grid view
            base.BindGridList();
            InitGridView();
            //初始化所以Combo box控件
            InitComboBox();
        }

        private void InitComboBox()
        {
            //初始化公司下拉列表
            InitCompanyComboBox();

            //初始化区域下拉列表
            InitAreaComboBox();
        }

        private void InitAreaComboBox()
        {
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
                //dtUserArea.Rows.InsertAt(row, 0);
                //
                ControlHelper.BindComboBox(cbArea, dtUserArea, "AreaName", "AreaID");
            }
        }

        private void InitCompanyComboBox()
        {
            //绑定公司
            DataSet ds = UserCompanyBLL.SelectUserCompany(new UserCompanyEntity() 
                                            { UserId = SysEnvironment.CurrentUser.ID, Status = 'A' });

            if (ds != null && ds.Tables.Count == 1)
            {
                DataTable dtUserCompany = ds.Tables[0];
                DataRow row = dtUserCompany.NewRow();
                row["CompanyName"] = string.Empty;
                row["CompanyCode"] = DBNull.Value;
                dtUserCompany.Rows.InsertAt(row, 0);
                ControlHelper.BindComboBox(cbCompany, ds.Tables[0], "CompanyName", "CompanyCode");
            }
        }

        private void InitGridView()
        {
            GridViewHelper.AppendColumnToDataGridView(dgvList, "AreaName", GetMessage("AreaName"), 100);   
            GridViewHelper.AppendColumnToDataGridView(dgvList, "CompanyCode", GetMessage("CompanyCode"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "CompanyName", GetMessage("CompanyName"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "Type", GetMessage("Type"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "StoreNo", GetMessage("StoreNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "StoreName", GetMessage("StoreName"), 250);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "KioskNo", GetMessage("KioskNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "KioskName", GetMessage("KioskName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "Status", GetMessage("Status"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "UpdateTime", GetMessage("UpdateTime"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "SalesBeginDate", GetMessage("SalesBeginDate"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "SalesEndDate", GetMessage("SalesEndDate"), 100);
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            //显示分页
            base.ShowPager = true;

            //定义变量，存储查询参数
            string strType = cbType.Text;
            string strStoreNoName = tbStoreNoOrName.Text;
            string strCompany = null == cbCompany.SelectedValue ? string.Empty : cbCompany.SelectedValue.ToString();
            string strStatus = cbStatus.Text;
            string strArea = null == cbArea.SelectedValue ? string.Empty : cbArea.SelectedValue.ToString();

            //总记录数
            int recordCount = 0;

            //执行查询
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    base.DTSource = 
                        salesBLL.SelectStoreOrKiosk(
                                strType, strStoreNoName, strCompany, strStatus,
                                SysEnvironment.CurrentUser.ID.ToString(), strArea,
                                base.CurrentPageIndex, base.PageSize, out recordCount
                            ).Tables[0];
                },
                    base.GetMessage("SearchStoreOrKioskError"), base.GetMessage("SearchStoreOrKioskList"));
            }, base.GetMessage("Wait"), () =>
            {
                this.salesBLL.CloseService();
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
            InitGridView();
        }

        private void KeyIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null != salesBLL)
                salesBLL.Dispose();

            if (null != UserCompanyBLL)
                UserCompanyBLL.Dispose();

            if (null != AreaBLL)
                AreaBLL.Dispose();
        }

        /// <summary>
        /// 单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            KeyInSales();
            //Sales信息框关闭后，刷新列表
            GetDataSource();
        }

        //导入预测Sales
        protected override void btnKeyInSales_Click(object sender, EventArgs e)
        {
            KeyInSales();
        }

        private void KeyInSales()
        {
            string storeNo, kioskNo;
            if (null != dgvList.CurrentRow)
            {
                //string strStatus = dgvList.CurrentRow.Cells["Status"].Value.ToString();
                //if ("I" == strStatus)
                //{
                //    MessageError(GetMessage("InvalidEntity"));
                //    return;
                //}

                storeNo = dgvList.CurrentRow.Cells["StoreNo"].Value.ToString();
                kioskNo = dgvList.CurrentRow.Cells["KioskNo"].Value.ToString();

                if (String.IsNullOrEmpty(kioskNo))
                {
                    kioskNo = null;
                }

                KeyInBox frm = new KeyInBox(storeNo, kioskNo);
                frm.ShowDialog();
            }
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid? AreaId = null;
            if (this.cbArea.SelectedValue + string.Empty != string.Empty)
            {
                AreaId = new Guid(this.cbArea.SelectedValue + string.Empty);
            }
            string company = string.Empty;
            if (this.cbCompany.SelectedValue + string.Empty != string.Empty)
            {
                company = this.cbCompany.SelectedValue + string.Empty;
            }
            //
            DataSet dsCompany = this.UserCompanyBLL.SelectUserCompany(new UserCompanyEntity()
            {
                AreaId = AreaId,
                Status = 'A',
                UserId = SysEnvironment.CurrentUser.ID
            });
            if (dsCompany != null && dsCompany.Tables.Count > 0)
            {
                DataTable dt = dsCompany.Tables[0];
                //dt.Rows.InsertAt(dt.NewRow(), 0);
                //
                this.cbCompany.DataSource = null;
                ControlHelper.BindComboBox(this.cbCompany, dt, "CompanyName", "CompanyCode");
            }
        }
    }
}