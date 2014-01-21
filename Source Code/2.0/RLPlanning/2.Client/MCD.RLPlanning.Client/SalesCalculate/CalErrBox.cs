using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.SalesCalculate
{
    public partial class CalErrBox : BaseEdit
    {
        public CalErrBox(DataSet ds)
        {
            InitializeComponent();

            if (0 == ds.Tables.Count)
            {
                return;
            }

            string errMsg = string.Empty;

            //item[0]餐厅编号，item[1]甜品店编号
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                //餐厅
                if (string.IsNullOrEmpty(item[1].ToString()))
                {
                    errMsg = string.Format(GetMessage("StoreInCal"), item[0].ToString());
                }
                //甜品店
                else
                {
                    errMsg = string.Format(GetMessage("KioskInCal"), item[1].ToString());
                }
                m_listErrMsg.Add(errMsg);
            }
        }

        private List<string> m_listErrMsg = new List<string>();

        private void CalErrBox_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCol.HeaderText = "";
            dgvCol.Name = "Msg";
            dvErrInfo.Columns.Add(dgvCol);

            foreach (var item in m_listErrMsg)
            {
                dvErrInfo.Rows.Add(item);
            }
        }
    }
}
