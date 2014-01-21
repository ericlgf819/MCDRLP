using System;
using System.Windows.Forms;

namespace MCD.RLPlanning.ServiceUnInstall
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (MessageBox.Show("您确认需要卸载 Store Rent & Lease System 服务程序吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            // 获取系统文件夹路径
            string sysroot = System.Environment.SystemDirectory;
            // 执行卸载方法
            System.Diagnostics.Process.Start(sysroot + "\\msiexec.exe", "/x {87FB5D4C-F02A-4C63-A15C-58EE8DC62A72} /qr");
        }
    }
}