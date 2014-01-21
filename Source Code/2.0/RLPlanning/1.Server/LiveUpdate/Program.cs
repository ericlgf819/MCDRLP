using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using LiveUpdate.App;

namespace LiveUpdate
{
    static class Program
    {
        /// <summary>
        /// 更新客户端。
        /// </summary>
        static UpdateClient client = null;
        #region ctor

        static Program()
        {
            if (Program.client == null)
            {
                Program.client = new UpdateClient();
                Program.client.LiveUpdateServer = ConfigurationManager.AppSettings["LiveUpdateServer"].ToString();
            }
        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //检查更新
            FrmWait wait = new FrmWait(() => { client.CheckUpdate(); }, null);
            wait.ShowDialog();

            //开始更新
            if (client.NeedUpdate)
            {
                FrmMain update = new FrmMain();
                update.Client = client;
                update.ShowDialog();
            }

            //运行客户端程序
            string excutePath = Environment.CurrentDirectory + @"\" + ConfigurationManager.AppSettings["AppExeName"];
            if (File.Exists(excutePath))
            {
                Process.Start(excutePath);
            }
            else
            {
                string msg = "启动应用程序客户端失败，请检查客户端以及服务端config文件配置是否正确！";
                SystemLog.WriteLog(msg);
                MessageBox.Show(msg, "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// 更新异常时直接启动主程序。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SystemLog.WriteLog("在线更新程序出现异常，异常信息：{0}", e.Exception.Message);
            Process.Start(Environment.CurrentDirectory + @"\" + ConfigurationManager.AppSettings["AppExeName"]);
        }
    }
}