using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class PopupTextForm : Form
    {
        public PopupTextForm()
        {
            InitializeComponent();
        }


        private bool m_ReadOnly;
        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this.m_ReadOnly;
            }
            set
            {
                if (this.m_ReadOnly != value)
                {
                    this.m_ReadOnly = value;
                    this.txtContent.ReadOnly = value;
                    this.btnConfirm.Enabled = !value;
                }
            }
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content 
        {
            get { return this.txtContent.Text; }
            set { this.txtContent.Text = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //private void txtContent_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.Handled = true;
        //    }
        //}
    }
}
