using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.BLL.Report;
using MCD.Common;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.PlanningSnapshot;


namespace MCD.RLPlanning.Client.Report
{
    public partial class SalesDataReport : BaseList
    {
        public SalesDataReport()
        {
            InitializeComponent();
        }

        private ReportBLL ReportBLL = new ReportBLL();

        private AreaBLL AreaBLL = new AreaBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();
        private CloseAccountBLL clsAccountBLL = new CloseAccountBLL();

        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            //验证年份格式是否正确
            if (tbYear.Enabled)
            {
                if (!ValideYearInput())
                    return;
            }

            //显示分页
            base.ShowPager = true;

            //定义变量，存储查询参数
            if (this.cbArea.SelectedValue == null)
            {
                base.MessageInformation(base.GetMessage("MustArea"), base.GetMessage("Caption"));
                //
                this.cbArea.Focus();
                return;
            }
            else if (this.cbArea.FindString(this.cbArea.Text) == -1)
            {
                base.MessageInformation(string.Format(base.GetMessage("InvalidAreaInput"), this.cbArea.Text), base.GetMessage("Caption"));
                //
                this.cbArea.Focus();
                return;
            }
            string strAreaID = cbArea.SelectedValue == null ? string.Empty : cbArea.SelectedValue.ToString();
            //
            string strStore = tbStoreNo.Text;
            string strCompanyCode = cbCompany.SelectedValue == null ? string.Empty : cbCompany.SelectedValue.ToString();
            bool bIsCashClosedYear = !tbYear.Enabled;
            string strYear = bIsCashClosedYear ? (cbCashCloseYear.SelectedValue ==
                                                    null ? string.Empty : cbCashCloseYear.SelectedValue.ToString()) 
                                                    : tbYear.Text;
            string strEntityType = cbEntityType.SelectedItem == null ? string.Empty : cbEntityType.SelectedItem.ToString();

            //如果有个区下面没有公司，导致公司没有选择，则必须提示用户选择公司
            //if (String.IsNullOrEmpty(strCompanyCode))
            //{
            //    MessageError(GetMessage("MustSelectCompany"));
            //    return;
            //}

            //总记录数
            int recordCount = 0;

            //执行查询
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != ReportBLL)
                    {
                        DTSource = ReportBLL.SelectStoreKioskSalesReport(strAreaID, strStore, strCompanyCode,
                                    strYear, strEntityType, bIsCashClosedYear, base.CurrentPageIndex, base.PageSize, out recordCount).Tables[0];
                    }
                },
                    base.GetMessage("SalesDataReportError"), base.GetMessage("SalesDataReport"));
            }, base.GetMessage("Wait"), () =>
            {
                this.ReportBLL.CloseService();
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
            //初始化DataGridView头
            InitDataGridView();
        }

        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesDataReport_Load(object sender, EventArgs e)
        {
            //初始化Datagridview
            BindGridList();
            //初始化区域控件
            InitAreaComboBox();
            //初始化关帐年度控件
            InitCashCloseYearComboBox();
        }

        /// <summary>
        /// 初始化DataGridView头
        /// </summary>
        private void InitDataGridView()
        {
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Company", GetMessage("Company"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "餐厅编号", GetMessage("StoreNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Store", GetMessage("StoreName"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "Kiosk", GetMessage("KioskName"), 200);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "年度", GetMessage("Year"), 100);
            //
            for (int index = 1; index <= 12; ++index)
            {
                GridViewHelper.AppendColumnToDataGridView(base.dgvList, string.Format("{0}月", index), string.Format("{0}", index), 80);
            }
            // 用于导出
            base.DecimalColumns = new List<int>();
            for (int i = 5; i < base.dgvList.Columns.Count; i++)
            {
                base.DecimalColumns.Add(i);
            }
        }

        private const string c_yearerr = "年度数据格式错误";
        /// <summary>
        /// 验证年份数据是否输入正确
        /// </summary>
        /// <returns></returns>
        private bool ValideYearInput()
        {
            //不填则直接填当年
            if (String.IsNullOrEmpty(tbYear.Text))
            {
                tbYear.Text = DateTime.Now.Year.ToString();
                return true;
            }

            //一定要是xxxx格式
            if (tbYear.Text.Length > 0 && tbYear.Text.Length != 4)
            {
                MessageError(c_yearerr);
                return false;
            }

            try
            {
                int iYear = int.Parse(tbYear.Text);

                //年度小于等于0
                if (iYear <= 0)
                {
                    MessageError(c_yearerr);
                    return false;
                }
            }
            catch
            {
                //非数字
                MessageError(c_yearerr);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 初始化区域控件
        /// </summary>
        private void InitAreaComboBox()
        {
            // 绑定区域
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

            if (dtValue != null)
            {
                DataTable dtUserArea = dtValue.Copy();
                DataRow row = dtUserArea.NewRow();
                row["AreaName"] = string.Empty;
                row["ID"] = DBNull.Value;
                //dtUserArea.Rows.InsertAt(row, 0);
                //
                DataView dvValue = new DataView(dtUserArea);
                dvValue.Sort = "ShowOrder ASC";
                ControlHelper.BindComboBox(cbArea, dvValue, "AreaName", "ID");
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
                Status = 'A'
            });
            if (dsCompany != null && dsCompany.Tables.Count > 0)
            {
                DataTable dt = dsCompany.Tables[0];
                dt.Rows.InsertAt(dt.NewRow(), 0);
                //
                this.cbCompany.DataSource = null;
                ControlHelper.BindComboBox(this.cbCompany, dt, "CompanyName", "CompanyCode");
            }
        }

        /// <summary>
        /// 初始化关帐年度ComboBox
        /// </summary>
        private void InitCashCloseYearComboBox()
        {
            DataTable dtValue = new DataTable();
            dtValue.Columns.Add("ID", typeof(string));
            dtValue.Columns.Add("CloseYear", typeof(string));

            DataSet ds = new CloseAccountBLL().SelectClosePlanning();
            DataRow[] RowsYear = ds.Tables[0].Select("[IsColsed] =True AND [IsDetected] =True");

            dtValue.Rows.Add("", "");
            for (int i = 0; i < RowsYear.Count(); i++)
            {
                object nYear = RowsYear[i]["CloseYear"];
                dtValue.Rows.Add(nYear, nYear);
            }

            ControlHelper.BindComboBox(cbCashCloseYear, dtValue, "CloseYear", "ID");
        }

        private void cbCashCloseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择关帐年度后，需要将年度控件禁用
            if (String.IsNullOrEmpty(cbCashCloseYear.SelectedValue.ToString()))
                tbYear.Enabled = true;
            else
                tbYear.Enabled = false;
        }
    }
}
