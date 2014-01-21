using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCD.UUP.CommonUI.LogService;
using System.Windows.Forms;

namespace MCD.UUP.CommonUI
{
    public class BaseFrm : Form
    {
        #region 执行方法，出错时，记录错误日志 -----------
        private LogServiceClient _logClient = null;
        /// <summary>
        /// 日志业务层对象
        /// </summary>
        private LogServiceClient logClient
        {
            get
            {
                if (_logClient == null)
                {
                    _logClient = new LogServiceClient();
                }
                return _logClient;
            }
        }

        /// <summary>
        /// 执行匿名委托方法，弹出信息框
        /// </summary>
        /// <param name="func">匿名委托方法</param>
        /// <param name="logType">错误类型</param>
        /// <param name="errMessage">出错时弹出信息</param>
        /// <param name="successMessage">成功时弹出信息</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType, string errMessage, string successMessage)
        {
            try
            {
                // 执行匿名委托方法
                if (func())
                {
                    // 弹出执行成功时的消息
                    if (successMessage != string.Empty)
                    {
                        MessageBox.Show(successMessage);
                    }
                    return true;
                }
                else
                {
                    // 弹出执行错误时的消息
                    if (errMessage != string.Empty)
                    {
                        MessageBox.Show(errMessage);
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                // 写入异常日志
                AddLog(logType, ex.Message, ex.ToString());
                // 弹出执行错误时的消息
                if (errMessage != string.Empty)
                {
                    MessageBox.Show(errMessage);
                }
                return false;
            }
        }

        /// <summary>
        /// 执行方法，返回是否执行成功
        /// </summary>
        /// <param name="func">执行方法</param>
        /// <param name="logType">错误类型</param>
        /// <param name="errMessage">出错时显示信息</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType, string errMessage)
        {
            return ExecuteBoolean(func, logType, errMessage, string.Empty);
        }

        /// <summary>
        /// 执行方法，返回是否执行成功
        /// </summary>
        /// <param name="func">执行方法</param>
        /// <param name="logType">错误类型</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string logType)
        {
            return ExecuteBoolean(func, logType, string.Empty, string.Empty);
        }

        /// <summary>
        /// 执行无返回值的方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="logType"></param>
        /// <returns></returns>
        public bool ExecuteAction(Action action, string logType)
        {
            try
            {
                // 执行匿名委托方法
                action();
                return true;
            }
            catch (Exception ex)
            {
                // 写入异常日志
                AddLog(logType, ex.Message, ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <param name="logMessage"></param>
        /// <returns></returns>
        public int AddLog(string logType, string logTitle, string logMessage)
        {
            try
            {
                return logClient.InsertLog(new LogEntity()
                {
                    ID = Guid.NewGuid(),
                    LogTime = DateTime.Now,
                    LogTitle = logTitle,
                    LogMessage = logMessage,
                    LogType = logType
                });
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
