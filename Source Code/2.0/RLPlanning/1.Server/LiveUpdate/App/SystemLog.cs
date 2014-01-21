using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LiveUpdate.App
{
    /// <summary>
    /// 提供记录更新写日志的方法。
    /// </summary>
    public class SystemLog
    {
        /// <summary>
        /// 写日志。
        /// </summary>
        /// <param name="log">内容</param>
        /// <param name="args">参数</param>
        public static void WriteLog(string log, params object[] args)
        {
            string dirName = string.Format("{0}/UpdateLog/", Environment.CurrentDirectory);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            //
            string filePath = string.Format("{0}{1:yyyyMMdd}.log", dirName, DateTime.Now);
            string str = string.Format("[{0:yyyy-MM-dd HH:mm:ss}]：{1}", DateTime.Now, log);
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(str, args);
                }
            }
            catch {
                //日志错误不外抛
            }
        }
    }
}