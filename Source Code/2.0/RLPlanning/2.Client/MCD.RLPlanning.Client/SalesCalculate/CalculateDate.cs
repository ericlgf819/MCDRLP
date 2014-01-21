using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.RLPlanning.Business.SalesCalculate;

namespace MCD.RLPlanning.Client.SalesCalculate
{
    public partial class CalculateDate : BaseFrm
    {
        //返回值
        public string strDateTime;

        //从外部传入BLL用来获取服务器时间用
        private SalesCalculateBLL m_scBLL = null; 

        public CalculateDate(SalesCalculateBLL bll)
        {
            InitializeComponent();
            m_scBLL = bll;
        }

        private const string c_timeerr = "租金计算时间不能小于当前时间";

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            strDateTime = dtpOpenDateValue.Value.ToString();

            //获取服务器时间，来判断所选的租金计算时间是否小于当前时间
            DateTime curTime = DateTime.Now;
            if (null != m_scBLL)
            {
                curTime = m_scBLL.GetServerTime();
            }

            if (dtpOpenDateValue.Value.Year < curTime.Year ||
                (dtpOpenDateValue.Value.Year == curTime.Year && dtpOpenDateValue.Value.Month < curTime.Month))
            {
                MessageError(c_timeerr);
                return;
            }

            Close();
        }

        private void CalculateDate_Load(object sender, EventArgs e)
        {
            dtpOpenDateValue.CustomFormat = "yyyy-MM";
            //计算年份自动+1
            dtpOpenDateValue.Value.AddYears(1);
        }
    }
}
