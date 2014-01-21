using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices;

namespace MCD.RLPlanning.Client.AppCode
{
    /// <summary>
    /// 调用Windows API
    /// </summary>
    public class WinAPI
    {
        #region 设置窗体位置

        public static readonly IntPtr c_TopMost = new IntPtr(-1);
        public static readonly IntPtr c_NoTopMost = new IntPtr(-2);
        public const uint c_NoSize = 0x1;
        public const uint c_NoMove = 0x2;
        public const uint c_NoActivate = 0x10;
        public const uint c_ShowWindow = 0x40;

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nCmdShow">命令参数</param>
        /// <returns>Return Type: BOOL->int</returns>
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "ShowWindow")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool ShowWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, ShowWindowCommandEnum m_CmdShow);

        /// <summary>
        /// 为窗口指定一个新位置和状态
        /// </summary>
        /// <param name="hWnd">欲定位的窗口句柄</param>
        /// <param name="hWndInsertAfter">在窗口列表中，窗口hwnd会置于这个窗口句柄的后面，也可以选择</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetWindowPos")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetWindowPos([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd,
            [System.Runtime.InteropServices.InAttribute()] System.IntPtr hWndInsertAfter,
            int X, int Y, int width, int height, uint uFlags);

        /// <summary>
        /// 显示窗口的命令
        /// </summary>
        public enum ShowWindowCommandEnum
        {
            /// <summary>
            /// 隐藏窗口并激活其他窗口。
            /// </summary>
            Hide = 0,
            /// <summary>
            /// 激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
            /// </summary>
            ShowNormal = 1,
            /// <summary>
            /// 激活窗口并将其最小化。
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// 激活窗口并将其最大化。
            /// </summary>
            ShowMaximized = 3,
            /// <summary>
            /// 最大化指定的窗口。
            /// </summary>
            Maximize = 3,
            /// <summary>
            /// 以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// 在窗口原来的位置以原来的尺寸激活和显示窗口。
            /// </summary>
            Show = 5,
            /// <summary>
            /// 最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// 窗口最小化，激活窗口仍然维持激活状态。
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// 以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
            /// </summary>
            Restore = 9,
            /// <summary>
            /// 依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
            /// </summary>
            ShowDefault = 10
        }

        #endregion

        #region 判断文件是否被占用

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public static readonly int OF_READWRITE = 2;
        public static readonly int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 检查文件是否被占用
        /// </summary>
        /// <param name="xlsPath"></param>
        /// <returns></returns>
        public static bool CheckFileIsOpen(string xlsPath)
        {
            bool result = false;
            //
            IntPtr vHandle = WinAPI._lopen(xlsPath, WinAPI.OF_READWRITE | WinAPI.OF_SHARE_DENY_NONE);
            if (vHandle == WinAPI.HFILE_ERROR)
            {
                result = true;
            }
            WinAPI.CloseHandle(vHandle);
            //
            return result;
        }
        #endregion
    }
}