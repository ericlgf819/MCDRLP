using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace MCD.RLPlanning.Client.Common
{
    /// <summary>
    /// 提供异步上传或者下载文件的方法，同时显示上传或者下载的进度。
    /// </summary>
    public partial class UpDownloadFrm : BaseFrm
    {
        //Fields
        /// <summary>
        /// 上传下载对象。
        /// </summary>
        private WebClient client = null;
        /// <summary>
        /// 当异步上传完成后执行的事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public event EventHandler UploadCompleted = null;

        /// <summary>
        /// 获取或设置接收文件的站点地址。
        /// </summary>
        public string UploadUrl { get; set; }
        /// <summary>
        /// 获取或设置上传的文件对象。
        /// </summary>
        public FileInfo UploadFile { get; set; }
        /// <summary>
        /// 获取或设置待下载文件的url。
        /// </summary>
        public string DownLoadUrl { get; set; }
        /// <summary>
        /// 获取或设置下载文件保存文件的文件名。
        /// </summary>
        public string SavePath { get; set; }
        #region ctor

        public UpDownloadFrm()
        {
            InitializeComponent();
            //
            this.progBar.Maximum = 100;
            this.progBar.Minimum = 0;
            this.lbFileInfo.Text = this.lbProgress.Text = this.lbRemainTime.Text = string.Empty;
        }
        #endregion

        /// <summary>
        /// 窗体显示的时候显示出上传或者下载的文件信息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressFrm_Load(object sender, EventArgs e)
        {
            if (this.UploadFile != null)
            {
                //上传
                this.Text = "Upload File";
                this.lbFileInfo.Text = string.Format("{0} 大小:{1}", this.UploadFile.Name, this.GetFileSize(this.UploadFile.Length));
                this.lbPath.Text = string.Format("From '{0}' To '{1}'", this.UploadFile.Directory.Name, new Uri(this.UploadUrl).Host);
            }
            else
            {
                //下载
                this.Text = "Download File";
                this.lbFileInfo.Text = string.Format("{0} 正在连接…", Path.GetFileName(this.SavePath));
                this.lbPath.Text = string.Format("From '{0}' To '{1}'", new Uri(this.DownLoadUrl).Host, Path.GetDirectoryName(this.SavePath));
            }
        }
        /// <summary>
        /// 取消上传或者下载。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.client != null && this.client.IsBusy)
            {
                if (base.MessageConfirm("确认终止传输吗？") != DialogResult.OK)
                {
                    return;
                }
                this.client.CancelAsync();
            }
            base.Close();
        }
        /// <summary>
        /// 关闭窗体时释放资源。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpDownloadFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.client != null)
            {
                this.client.Dispose();
            }
        }

        /// <summary>
        /// 开始异步上传文件。
        /// </summary>
        public void UploadFileAsync()
        {
            DateTime startTime = DateTime.Now;//开始时间
            this.client = new WebClient();
            //异步上传进度
            this.client.UploadProgressChanged += (object sender, UploadProgressChangedEventArgs e) =>
            {
                double rate = (double)e.BytesSent / (DateTime.Now - startTime).TotalSeconds;//上传速度 KB/s
                double percent = (double)e.BytesSent / (double)e.TotalBytesToSend;//完成百分比
                int p = Convert.ToInt32(percent * 100);
                if (p > 100) p = 100;
                //
                this.progBar.Value = p;
                this.lbProgress.Text = percent.ToString("P0");
                if (rate > 0)
                {
                    this.lbRemainTime.Text = string.Format("剩余时间 {0:F0}s,上传速度 {1:F1}KB/s", 
                        (e.TotalBytesToSend - e.BytesSent) / rate, rate / 1024);
                }
            };
            //异步上传完成
            this.client.UploadFileCompleted += (object sender, UploadFileCompletedEventArgs e) =>
            {
                if (e.Error != null)
                {
                    base.MessageError("Error:" + e.Error.Message);
                    base.Close();
                    return;
                }
                if (!e.Cancelled)
                {
                    this.OnUploadCompleted(EventArgs.Empty);
                }
                base.Close();
            };
            //开始异步上传
            this.client.UploadFileAsync(new Uri(this.UploadUrl), "POST", this.UploadFile.FullName);
        }
        /// <summary>
        /// 触发UploadCompleted事件。
        /// </summary>
        /// <param name="e"></param>
        protected void OnUploadCompleted(EventArgs e)
        {
            if (this.UploadCompleted != null)
            {
                this.UploadCompleted(this, e);
            }
        }
        /// <summary>
        /// 开始异步下载文件。
        /// </summary>
        public void DownLoadFileAsync()
        {
            DateTime startTime = DateTime.Now;//开始时间
            //确认目录是否创建
            string dir = Path.GetDirectoryName(this.SavePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            this.client = new WebClient();
            //报告下载进度
            this.client.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
            {
                this.lbFileInfo.Text = string.Format("{0} 大小:{1}", Path.GetFileName(this.SavePath), this.GetFileSize(e.TotalBytesToReceive));
                double rate = (double)e.BytesReceived / (DateTime.Now - startTime).TotalSeconds;//下载速度 KB/s
                double percent = (double)e.BytesReceived / (double)e.TotalBytesToReceive;//完成百分比
                int p = Convert.ToInt32(percent * 100);
                if (p > 100) p = 100;
                //
                this.progBar.Value = p;
                this.lbProgress.Text = percent.ToString("P0");
                if (rate > 0)
                {
                    this.lbRemainTime.Text = string.Format("剩余时间 {0:F0}s,下载速度 {1:F1}KB/s", 
                        (e.TotalBytesToReceive - e.BytesReceived) / rate, rate / 1024);
                }
            };
            //下载完成
            this.client.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
            {
                if (e.Error != null)
                {
                    base.MessageError("Error:" + e.Error.Message);
                    base.Close();
                    return;
                }
                if (!e.Cancelled)
                {
                    base.MessageInformation("下载完成！");
                }
                base.Close();
            };
            //开始异步下载
            this.client.DownloadFileAsync(new Uri(this.DownLoadUrl), this.SavePath);
        }
        /// <summary>
        /// 获取带单位的文件大小。
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetFileSize(long size)
        {
            if (size > 1024 * 1024)
            {
                return ((double)size / (1024D * 1024D)).ToString("F2") + "MB";
            }
            else if (size > 1024)
            {
                return ((double)size / 1024D).ToString("F2") + "KB";
            }
            else
            {
                return size + "B";
            }
        }
    }
}