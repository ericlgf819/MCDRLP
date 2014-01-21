using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.WebServices.Data;

namespace MCD.EmailCenter
{
    public class EmailService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version">OUTLOOK版本</param>
        /// <param name="serviceURL">OUTLOOK版本所对应的邮箱EWS服务器地址</param>
        /// <param name="sysUser">系统邮箱地址</param>
        /// <param name="sysPwd">密码</param>
        public EmailService(string serviceURL,ExchangeVersion version, string sysUser, string sysPwd)
        {
            /// <summary>
            /// 全局信任域
            /// </summary>
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            es = new ExchangeService(version);
            es.Credentials = new NetworkCredential(sysUser, sysPwd);
            es.Url = new Uri(serviceURL);
        }

        /// <summary>
        /// 当前服务对象
        /// </summary>
        private ExchangeService es;

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="recipients">收件人列表</param>
        public void SendEmail(string subject, string body, List<string> recipients)
        {
            EmailMessage message = new EmailMessage(es);
            message.Subject = subject;
            message.Body = body;
            recipients.ForEach(value => message.ToRecipients.Add(value));
            message.SendAndSaveCopy();
        }
    }
}
