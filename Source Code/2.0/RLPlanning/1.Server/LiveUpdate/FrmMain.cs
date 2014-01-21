using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading;

using LiveUpdate.App;

namespace LiveUpdate
{
    /// <summary>
    /// 更新下载窗体。
    /// </summary>
    public partial class FrmMain : Form
    {
        //Fields
        /// <summary>
        /// 当前下载文件的索引。
        /// </summary>
        private int Current = 0;

        //Properties
        /// <summary>
        /// 获取或设置更新客户端。
        /// </summary>
        public UpdateClient Client { get; set; }
        #region ctor

        /// <summary>
        /// 初始化FrmMain类的新实例。
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            //
            this.lblMsg.Text = "请稍后，正在检测更新…";
            this.lblFileProg.Text = string.Empty;
            this.lblDownProg.Text = string.Empty;
            this.lblCurrent.Text = string.Empty;
            this.prgBarDown.Maximum = this.prgBarFile.Maximum = 100;
            this.prgBarDown.Minimum = this.prgBarFile.Minimum = 0;
        }

        /// <summary>
        /// 初始化FrmMain类的新实例，并指导更新客户端对象。
        /// </summary>
        /// <param name="client"></param>
        public FrmMain(UpdateClient client)
            : this()
        {
            this.Client = client;
        }
        #endregion

        //Events
        /// <summary>
        /// 窗体加载时下载更新。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    this.Run();
                }
                catch (Exception ex)
                {
                    SystemLog.WriteLog("下载更新失败：{0}", ex.Message);
                }
            }));
            thread.Start();
        }
        /// <summary>
        /// 退出更新。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }

        //Methods
        /// <summary>
        /// 运行版本更新程序。
        /// </summary>
        private void Run()
        {
            if (!this.Client.NeedUpdate)
            {
                this.SetControlText(this.lblMsg, "本地文件已经是最新版本，无需下载更新！");
                this.CloseForm();
            }
            else
            {
                this.SetControlText(this.lblMsg, "检测更新成功，有{0}个文件需要更新", this.Client.UpdateFiles.Count);
                //
                SystemLog.WriteLog(@"====================检测到新版本，有{0}个文件需要更新，开始下载====================", this.Client.UpdateFiles.Count);
                //开始下载
                this.DownloadFile();
            }
        }
        /// <summary>
        /// 递归下载待更新文件集合中的文件。
        /// </summary>
        private void DownloadFile()
        {
            UpdateFile file = this.Client.UpdateFiles[this.Current];

            //显示当前下载的文件的索引
            this.SetControlText(this.lblFileProg, "{0}/{1}", this.Current + 1, this.Client.UpdateFiles.Count);

            //开始下载
            this.Client.DownloadFile(file, 
                prg => {
                    //更新当前文件下载进度条
                    this.SetProgress(this.prgBarDown, prg.ProgressPercentage);
                    this.SetControlText(this.lblDownProg, "{0}%", prg.ProgressPercentage);
                }, 
                arg => {
                    //文件下载完后更新下载文件的最后修改时间
                    UpdateFile update = arg.UserState as UpdateFile;
                    if (update != null)
                    {
                        if (arg.Error == null)
                        {
                            SystemLog.WriteLog("{0}，下载成功", update.FilePath);
                        }
                        else
                        {
                            SystemLog.WriteLog("{0}，下载失败，原因：{1}", update.FilePath, arg.Error.Message);
                        }
                    }

                    //更新文件下载数目进度条
                    this.SetProgress(this.prgBarFile, Convert.ToInt32(decimal.Divide(this.Current + 1, this.Client.UpdateFiles.Count) * 100));
                    if (this.Current == this.Client.UpdateFiles.Count - 1)
                    {
                        //所有文件下载完毕后检测并执行自身更新
                        //this.UpdateMySelf();
                        this.SetControlText(this.lblCurrent, "更新完毕！");
                        Thread.Sleep(400);
                        //
                        this.CloseForm();
                    }

                    //若未下载完，则继续下载下一个
                    if (this.Current < this.Client.UpdateFiles.Count - 1)
                    {
                        this.Current++;
                        this.DownloadFile();
                    }
                });
        }
        /// <summary>
        /// 更新自身(自动更新程序)指定的文件。
        /// </summary>
        /// <param name="update"></param>
        private void UpdateMySelf(UpdateFile update)
        {
            try
            {
                FileInfo myself = new FileInfo(Application.StartupPath.TrimEnd('\\') + "\\" + update.FileName);
                if (myself != null && myself.Exists && !myself.LastAccessTime.Equals(update.LastUpdateTime))
                {
                    if (File.Exists(update.SavePath))
                    {
                        File.Delete(myself.FullName + ".bak");
                        myself.MoveTo(myself.FullName + ".bak");
                        //

                        File.Move(update.SavePath, update.FileName);
                        File.Move(update.SavePath, Application.StartupPath.TrimEnd('\\') + "\\" + update.FileName);
                    }
                }
            }
            catch
            {
                //
            }
        }
        /// <summary>
        /// 检测并更新自身程序(自动更新程序)。
        /// </summary>
        private void UpdateMySelf()
        {
            if (this.Client == null || this.Client.UpdateFiles == null || this.Client.UpdateFiles.Count <= 0)
            {
                return;
            }

            //更新配置文件
            UpdateFile temp = this.Client.UpdateFiles.Find(item => item.FileName.ToLower().Equals("liveupdate.exe.config"));
            if (temp != null)
            {
                this.UpdateMySelf(temp);
            }

            //更新主程序
            temp = this.Client.UpdateFiles.Find(item => item.FileName.ToLower().Equals("liveupdate.exe"));
            if (temp != null)
            {
                this.UpdateMySelf(temp);
            }
        }
        /// <summary>
        /// 在线程中设置控件的文本。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="msg"></param>
        /// <param name="arg"></param>
        private void SetControlText(Control ctrl, string msg, params object[] arg)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new MethodInvoker(() => {
                    ctrl.Text = string.Format(msg, arg);
                }));
            }
            else
            {
                ctrl.Text = string.Format(msg, arg);
            }
        }
        /// <summary>
        /// 在线程中设置进度条的值。
        /// </summary>
        /// <param name="prog"></param>
        /// <param name="value"></param>
        private void SetProgress(ProgressBar prog, int value)
        {
            if (prog.InvokeRequired)
            {
                prog.BeginInvoke(new MethodInvoker(() => {
                    prog.Value = value;
                }));
            }
            else
            {
                prog.Value = value;
            }
        }

        /// <summary>
        /// 关闭当前窗体并返回DialogResult结果。
        /// </summary>
        private void CloseForm()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(() => {
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }));
            }
            else
            {
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }
    }
}