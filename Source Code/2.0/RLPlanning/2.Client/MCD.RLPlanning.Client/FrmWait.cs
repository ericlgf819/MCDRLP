using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmWait : BaseFrm
    {
        //Properties
        private Thread waitThread = null;
        private Action actionMethod { get; set; }
        private Action closeMethod { get; set; }
        private Action<FrmWait> method { get; set; }

        /// <summary>
        /// 获取或设置进度条下方显示的文本。
        /// </summary>
        public string WaitMessage
        {
            get
            {
                string text = string.Empty;
                if (this.lblMessage.InvokeRequired)
                {
                    this.lblMessage.BeginInvoke(new MethodInvoker(() => { text = this.lblMessage.Text; }));
                }
                else
                {
                    text = this.lblMessage.Text;
                }
                return text;
            }
            set
            {
                if (this.lblMessage.InvokeRequired)
                {
                    this.lblMessage.BeginInvoke(new MethodInvoker(() => { this.lblMessage.Text = value; }));
                }
                else
                {
                    this.lblMessage.Text = value;
                }
            }
        }
        #region ctor

        public FrmWait(Action action, string message)
        {
            InitializeComponent();
            //
            this.actionMethod = action;
            this.WaitMessage = message;
            FrmWait.CheckForIllegalCrossThreadCalls = false;
        }
        public FrmWait(Action action, string message, Action close)
        {
            InitializeComponent();
            //
            this.actionMethod = action;
            this.WaitMessage = message;
            this.closeMethod = close;
            FrmWait.CheckForIllegalCrossThreadCalls = false;
        }
        public FrmWait(Action action, string message, bool showStopPicture)
        {
            InitializeComponent();
            //
            this.actionMethod = action;
            this.WaitMessage = message;
            this.picStop.Visible = showStopPicture;
            //
            FrmWait.CheckForIllegalCrossThreadCalls = false;
        }
        public FrmWait(Action action, string message, bool showStopPicture, Action close)
        {
            InitializeComponent();
            //
            this.actionMethod = action;
            this.WaitMessage = message;
            this.closeMethod = close;
            this.picStop.Visible = showStopPicture;
            //
            FrmWait.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        //Events
        private void FrmWait_Load(object sender, EventArgs e)
        {
            this.StartNewThread(() => {
                try
                {
                    if (this.actionMethod != null)
                    {
                        this.actionMethod();
                    }
                    else if (this.method != null)
                    {
                        this.method(this);
                    }
                }
                catch (ThreadAbortException)
                {
                    //
                }
                catch (Exception ex)
                {
                    base.MessageError(ex.Message);
                }
                base.Close();
            });
        }
        private void picStop_MouseHover(object sender, EventArgs e)
        {
            this.picStop.Image = global::MCD.RLPlanning.Client.Properties.Resources.Stop_Hover;
        }
        private void picStop_MouseLeave(object sender, EventArgs e)
        {
            this.picStop.Image = global::MCD.RLPlanning.Client.Properties.Resources.Stop;
        }
        private void picStop_Click(object sender, EventArgs e)
        {
            if (this.closeMethod != null)
            {
                this.closeMethod();
            }
            this.waitThread.Abort();
            //
            base.Close();
        }

        //Methods
        public void StartNewThread(Action method)
        {
            // 启动新的线程完成动作
            ThreadStart start = new ThreadStart(method);
            //
            this.waitThread = new Thread(start);
            this.waitThread.Start();
        }
    }
}