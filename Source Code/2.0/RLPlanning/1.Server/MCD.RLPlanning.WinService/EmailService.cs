using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Diagnostics;

using System.Data;
using System.Data.SqlClient;

using System.ServiceProcess;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MCD.Common;

namespace MCD.RLPlanning.WinService
{
    /// <summary>
    /// 催办邮件生成以及发送服务。
    /// </summary>
    public partial class EmailService : ServiceBase
    {
        //Fields
        private Thread MainThread;
        private static string M_SynchronizateTime = string.Empty;

        //Properties
        /// <summary>
        /// 系统同步时间
        /// </summary>
        public static string SynchronizateTime
        {
            get
            {
                if (EmailService.M_SynchronizateTime.Equals(string.Empty))
                {
                    EmailService.M_SynchronizateTime = ConfigurationManager.AppSettings["SynchronizateTime"] + string.Empty;
                }
                return EmailService.M_SynchronizateTime;
            }
        }
        public static int Hour
        {
            get
            {
                int ihour = 1;
                if (!string.IsNullOrEmpty(EmailService.SynchronizateTime))
                {
                    try
                    {
                        int.TryParse(EmailService.SynchronizateTime.Split(':')[0], out ihour);
                    }
                    catch { }
                }
                return ihour;
            }
        }
        public static int Minute
        {
            get
            {
                int iminute = 1;
                if (!string.IsNullOrEmpty(EmailService.SynchronizateTime))
                {
                    try
                    {
                        int.TryParse(EmailService.SynchronizateTime.Split(':')[1], out iminute);
                    }
                    catch { }
                }
                return iminute;
            }
        }
        #region ctor

        /// <summary>
        /// 
        /// </summary>
        public EmailService()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        protected override void OnStart(string[] args)
        {
            this.MainThread = new Thread(new ThreadStart(this.ThreadProcess));
            //
            this.MainThread.Priority = ThreadPriority.Lowest;
            this.MainThread.IsBackground = true;
            this.MainThread.Name = "McD Rent & Lease Planning System Synchronization Service";
            this.MainThread.Start();
            //
            Executer.AddLog("系统服务", "已启动", string.Empty);
        }
        protected override void OnStop()
        {
            if (this.MainThread.IsAlive) this.MainThread.Abort();
            //
            Executer.AddLog("系统服务", "已停止", string.Empty);
        }

        //Methods
        private void ThreadProcess()
        {
            while (true)
            {
                Thread.Sleep(60000);//每间隔 1 分钟执行程序一次
                if (DateTime.Now.Hour != EmailService.Hour || DateTime.Now.Minute != EmailService.Minute) continue;
                //
                try
                {
                    Executer.doSynchonization();
                }
                catch (Exception ex)
                {
                    Executer.AddErrorLog("系统服务", "服务异常", ex.ToString(), ex.StackTrace);
                }
            }
        }
    }
}