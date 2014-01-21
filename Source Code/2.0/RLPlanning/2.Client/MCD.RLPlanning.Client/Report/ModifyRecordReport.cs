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


namespace MCD.RLPlanning.Client.Report
{
    public partial class ModifyRecordReport : BaseList
    {
        private ReportBLL ReportBLL = new ReportBLL();

        private AreaBLL AreaBLL = new AreaBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();

        public ModifyRecordReport()
        {
            InitializeComponent();
        }

        private void ModifyRecordReport_Load(object sender, EventArgs e)
        {
            //初始化Datagridview
            BindGridList();
            //初始化区域控件
            InitAreaComboBox();
        }
        protected override void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.ColumnIndex == 8 && e.Value.ToString() != string.Empty)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString(BaseFrm.DATETIME_LONG_FORMAT);
                }
            }
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        protected override void GetDataSource()
        {
            //验证开始时间是否小于结束时间
            if (!ValidateBeginEndDate())
                return;

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
            string strCompanyCode = cbCompany.SelectedValue == null ? string.Empty : cbCompany.SelectedValue.ToString();
            string strStoreOrKioskNo = tbStoreOrKioskNo.Text;
            DateTime StartDate = dtStartDate.Value;
            DateTime EndDate = dtEndDate.Value;
            string strChangeType = cbChangeType.SelectedItem == null ? string.Empty : cbChangeType.SelectedItem.ToString();

            //总记录数
            int recordCount = 0;

            //执行查询
            FrmWait frm = new FrmWait(() =>
            {
                ExecuteAction(() =>
                {
                    if (null != ReportBLL)
                    {
                        DTSource = ReportBLL.SelectChangeReport(strAreaID, strCompanyCode, strStoreOrKioskNo, StartDate, EndDate,
                                    strChangeType, base.CurrentPageIndex, base.PageSize, out recordCount).Tables[0];
                    }
                },
                    base.GetMessage("ModifyRecordReportError"), base.GetMessage("ModifyRecordReport"));
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
            InitDataGridView();
        }

        /// <summary>
        /// 初始化DataGridView头
        /// </summary>
        private void InitDataGridView()
        {
            GridViewHelper.AppendColumnToDataGridView(dgvList, "ContractNo", GetMessage("ContractNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "AreaName", GetMessage("AreaName"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "CompanyName", GetMessage("CompanyName"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "StoreNo", GetMessage("StoreNo"), 200);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "KioskNo", GetMessage("KioskNo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "RentEndDate", GetMessage("RentEndDate"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "ChangeInfo", GetMessage("ChangeInfo"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "LastModifyUserName", GetMessage("LastModifyUserName"), 100);
            GridViewHelper.AppendColumnToDataGridView(dgvList, "LastModifyTime", GetMessage("LastModifyTime"), 120);
        }

        private const string c_dateerror = "结束时间必须大于开始时间";

        /// <summary>
        /// 验证结束时间是否大于开始时间
        /// </summary>
        /// <returns></returns>
        private bool ValidateBeginEndDate()
        {
            //同一天是允许的
            if (dtStartDate.Value.Year == dtEndDate.Value.Year &&
                dtStartDate.Value.Month == dtEndDate.Value.Month &&
                dtStartDate.Value.Day == dtEndDate.Value.Day)
            {
                return true;
            }

            if (dtStartDate.Value > dtEndDate.Value)
            {
                MessageError(c_dateerror);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 初始化区域ComboBox控件
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
                if (company != string.Empty)
                {
                    this.cbCompany.SelectedValue = company;
                }
            }
        }

        private void cbArea_KeyUp(object sender, KeyEventArgs e)
        {
            //区域下拉框里的值，非空，则不需要处理，如果为空，则相当于将区域选择成空，所以要刷新公司列表
            if (!String.IsNullOrEmpty(cbArea.Text))
                return;

            Guid? AreaId = null;
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
                if (company != string.Empty)
                {
                    this.cbCompany.SelectedValue = company;
                }
            }
        }
    }
}
