using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

using MCD.RLPlanning.Entity;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.BLL.Setting;
using MCD.RLPlanning.Client.Master;
using System.Security.Principal;

namespace MCD.RLPlanning.Client
{
    static class Program
    {
        //Fields
        /// <summary>
        /// 获取或设置是否注销登录。
        /// </summary>
        public static bool IsLogOut = false;
        private static LogBLL logBLL = null;

        //Properties
        /// <summary>
        /// 日志业务层对象
        /// </summary>
        private static LogBLL LogBLL
        {
            get
            {
                if (Program.logBLL == null)
                {
                    Program.logBLL = new LogBLL();
                }
                return Program.logBLL;
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Program.Application_ThreadException);
            Application.ApplicationExit += new EventHandler(Program.Application_ApplicationExit);
            //
            string excutePath = Environment.CurrentDirectory + @"\" + ConfigurationManager.AppSettings["AppUpdateName"];
            if (System.IO.File.Exists(excutePath) && Program.CheckAPPUpdate())
            {
                if (!Program.IsAdmin())
                {
                    Program.RunAsAdmin(excutePath);
                }
                else
                {
                    Process.Start(excutePath);
                }
            }
            else
            {
                Program.ShowLogin();
            }
        }

        //Events
        /// <summary>
        /// 系统异常时捕获错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                Program.LogBLL.InsertLog(new LogEntity()
                {
                    ID = Guid.NewGuid(),
                    LogTime = DateTime.Now,
                    LogTitle = e.Exception.Message,
                    LogMessage = e.Exception.ToString(),
                    LogType = "系统运行发生异常",
                    LogSource = e.Exception.Source,
                    EnglishName = AppCode.SysEnvironment.CurrentUser.EnglishName,
                    UserID = AppCode.SysEnvironment.CurrentUser.ID
                });
                //
                if (!string.IsNullOrEmpty(e.Exception.Message.Trim()))
                {
                    FrmError frm = new FrmError(e.Exception);
                    frm.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show(e.Exception.Message);
            }
        }
        /// <summary>
        /// 注销登录后重新启动窗体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Program.IsLogOut)
            {
                Process.Start(Application.ExecutablePath);
            }
            Program.IsLogOut = false;
        }

        //Methods
        private static bool IsAdmin()
        {
            //WindowsIdentity id = WindowsIdentity.GetCurrent();
            //WindowsPrincipal principal = new WindowsPrincipal(id);
            ////
            //return principal.IsInRole(WindowsBuiltInRole.Administrator);
            //
            string OSType = Program.GetOSType();
            //
            return OSType != "Windows Vista" && OSType != "Windows7";
        }
        private static void RunAsAdmin(string excutePath)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo(excutePath);
            procInfo.UseShellExecute = true;
            procInfo.WorkingDirectory = Environment.CurrentDirectory;
            //procInfo.FileName = Application.ExecutablePath;
            procInfo.Verb = "runas";
            try
            {
                Process.Start(procInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process");
            }
        }
        private static bool CheckAPPUpdate()
        {
            bool hasUpdate = false;
            using (SystemParameterBLL paraBLL = new SystemParameterBLL())
            {
                string AppVersion = paraBLL.GetSystemParameterByCode("AppVersion");
                if (!string.IsNullOrEmpty(AppVersion))
                {
                    hasUpdate = ConfigurationManager.AppSettings["AppVersion"] + string.Empty != AppVersion;
                }
            }
            //
            return hasUpdate;
        }
        private static string GetOSType()
        {
            //定义系统版本
            Version ver = System.Environment.OSVersion.Version;
            string OSType = "";
            //Major主版本号
            //Minor副版本号
            if (ver.Major == 5 && ver.Minor == 0)
            {
                OSType = "Windows 2000";
            }
            else if (ver.Major == 5 && ver.Minor == 1)
            {
                OSType = "Windows XP";
            }
            else if (ver.Major == 5 && ver.Minor == 2)
            {
                OSType = "Windows 2003";
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                OSType = "Windows Vista";
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                OSType = "Windows7";
            }
            else
            {
                OSType = "未知";
            }
            return OSType;
        }
        /// <summary>
        /// 显示登录窗体。
        /// </summary>
        private static void ShowLogin()
        {
            Login login = new Login();
            if (login.ShowDialog() == DialogResult.Yes)
            {
                Form frm = null;
                if (AppCode.SysEnvironment.CurrentUser.IsChangePWD == false)
                {
                    frm = new ChangePWD(true);
                }
                else
                {
                    frm = new FrmMain();
                }
                Application.Run(frm);
            }
        }
    }
}