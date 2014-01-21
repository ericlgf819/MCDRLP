using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.ForcastSales
{
    public partial class ImportErrBox : BaseEdit
    {
        public ImportErrBox(string strErrMsg)
        {
            InitializeComponent();

            string strMsg = string.Empty;
            foreach (var item in strErrMsg.Split('\n'))
            {
                strMsg = item.Trim();

                if (String.IsNullOrEmpty(strMsg))
                    continue;

                m_listErrMsg.Add(strMsg);
            }
        }

        public ImportErrBox(List<string> strErrMsgList)
        {
            InitializeComponent();

            m_listErrMsg = strErrMsgList;
        }

        private List<string> m_listErrMsg = new List<string>();

        private void ImportErrBox_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCol.HeaderText = "";
            dgvCol.Name = "Msg";
            dgvErr.Columns.Add(dgvCol);

            foreach (var item in m_listErrMsg)
            {
                dgvErr.Rows.Add(item);
            }
        }
    }
}
