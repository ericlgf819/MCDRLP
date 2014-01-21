using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LiveUpdate.App;

namespace LiveUpdate
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmWait : Form
    {
        //Properties
        private MethodInvoker Method { get; set; }
        private string WaitMessage { get; set; }
        #region ctor

        public FrmWait(MethodInvoker method, string message)
        {
            InitializeComponent();
            //
            this.Method = method;
            this.WaitMessage = message;
            FrmWait.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        //Events
        private void FrmWait_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.WaitMessage))
            {
                this.lblMessage.Text = this.WaitMessage;
            }
            this.Run(() => {
                try
                {
                    this.Method();
                }
                catch (Exception ex)
                {
                    SystemLog.WriteLog("检测更新失败：{0}", ex.Message);
                    //
                    if (this.lblMessage.InvokeRequired)
                    {
                        this.lblMessage.BeginInvoke(new MethodInvoker(() => { this.lblMessage.Text = ex.Message; }));
                    }
                    else
                    {
                        this.lblMessage.Text = ex.Message;
                    }
                    //
                    Thread.Sleep(500);
                }
                this.Close();
            });
        }

        //Methods
        public void Run(MethodInvoker method)
        {
            ThreadStart start = new ThreadStart(method);
            Thread thread = new Thread(start);
            thread.Start();
        }
    }
}