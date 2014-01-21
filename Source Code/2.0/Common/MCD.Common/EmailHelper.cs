using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.IO;

namespace MCD.Common
{
    /// <summary>
    /// 提供发送邮件的方法。
    /// </summary>
    public class EmailHelper
    {
        public static void ErrorLog(string message, Exception ex)
        {
            string path = Environment.CurrentDirectory + "\\Log\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
            using (StreamWriter str = new StreamWriter(path + fileName, true))
            {
                str.WriteLine("-------------------------------------");
                str.WriteLine("Date:" + DateTime.Now);
                str.WriteLine("Message:" + message + ":" + ex.Message);
                str.WriteLine("Description:" + ex.InnerException);
                str.WriteLine("-------------------------------------");
                str.WriteLine(" ");
            }
        }

        /// <summary>
        /// 将指定的邮件通过指定邮件服务器发送至指定的多个收件人以及抄送人。
        /// </summary>
        /// <param name="host">邮件服务器地址，如“smtp.126.com”</param>
        /// <param name="userName">发件人邮箱用户名</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="to">收件人邮箱地址集合</param>
        /// <param name="cc">抄送人邮箱地址集合</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns>返回是否发送成功</returns>
        public static bool SendEmail(string host, string userName, string password, MailAddress from, List<MailAddress> to, 
            List<MailAddress> cc, string subject, string body)
        {
            bool succ = true;
            using (MailMessage message = new MailMessage())
            {
                try
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.From = from;
                    if (to != null && to.Count > 0)
                    {
                        to.ForEach(addr =>
                        {
                            message.To.Add(addr);
                        });
                    }
                    if (cc != null && cc.Count > 0)
                    {
                        cc.ForEach(addr =>
                        {
                            message.CC.Add(addr);
                        });
                    }
                    //
                    SmtpClient client = new SmtpClient(host);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(userName, password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);
                }
                catch(Exception ex)
                {
                    message.Dispose();
                    //记日志
                    EmailHelper.ErrorLog("邮件发送失败\r\nFrom:" + from.Address
                        + "\r\nTo:" + (to.Count > 0 ? to[0].Address : "")
                        + "\r\nCC:" + (to.Count > 0 ? cc[0].Address : "")
                        + "\r\nSubject:" + subject
                        + "\r\nBody:" + body, ex);
                    succ = false;
                }
            }
            return succ;
        }
        /// <summary>
        /// 将指定的邮件通过指定邮件服务器发送至指定的收件人以及抄送人。
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="userName">发件人邮箱用户名</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="cc">抄送人邮箱地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns>返回是否发送成功</returns>
        public static bool SendEmail(string host, string userName, string password, MailAddress from, MailAddress to, 
            MailAddress cc, string subject, string body)
        {
            if (to == null)
            {
                return false;
            }
            //
            List<MailAddress> addrTo = new List<MailAddress>() { to };
            List<MailAddress> addrCc = new List<MailAddress>();
            if (cc != null)
            {
                addrCc.Add(cc);
            }
            //
            return EmailHelper.SendEmail(host, userName, password, from, addrTo, addrCc, subject, body);
        }
        /// <summary>
        /// 将指定的邮件通过指定邮件服务器发送至指定的收件人。
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="userName">发件人邮箱用户名</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns>返回是否发送成功</returns>
        public static bool SendEmail(string host, string userName, string password, MailAddress from, MailAddress to, 
            string subject, string body)
        {
            if (to == null)
            {
                return false;
            }
            //
            List<MailAddress> addrTo = new List<MailAddress>() { to };
            return EmailHelper.SendEmail(host, userName, password, from, addrTo, null, subject, body);
        }
        /// <summary>
        /// 将指定的邮件通过指定邮件服务器发送至指定的收件人。
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="userName">发件人邮箱用户名</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="fromDisplayName">发件人显示名称</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="toDisplayName">收件人显示名称</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns>返回是否发送成功</returns>
        public static bool SendEmail(string host, string userName, string password, string from, string fromDisplayName, string to, 
            string toDisplayName, string subject, string body)
        {
            MailAddress addrFrom = new MailAddress(from, fromDisplayName);
            List<MailAddress> addrTo = new List<MailAddress>() { new MailAddress(to, toDisplayName) };
            return EmailHelper.SendEmail(host, userName, password, addrFrom, addrTo, null, subject, body);
        }
        /// <summary>
        /// 将指定的邮件通过指定邮件服务器发送至指定的收件人以及抄送人。
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="userName">发件人邮箱用户名</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="fromDisplayName">发件人显示名称</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="toDisplayName">收件人显示名称</param>
        /// <param name="cc">抄送人邮箱地址</param>
        /// <param name="ccDisplayName">抄送人显示名称</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns>返回是否发送成功</returns>
        public static bool SendEmail(string host, string userName, string password, string from, string fromDisplayName, string to,
            string toDisplayName, string cc, string ccDisplayName, string subject, string body)
        {
            MailAddress addrFrom = new MailAddress(from, fromDisplayName);
            List<MailAddress> addrTo = new List<MailAddress>() { new MailAddress(to, toDisplayName) };
            List<MailAddress> addrCc = new List<MailAddress>() { new MailAddress(cc, ccDisplayName) };
            return EmailHelper.SendEmail(host, userName, password, addrFrom, addrTo, addrCc, subject, body);
        }
    }
}