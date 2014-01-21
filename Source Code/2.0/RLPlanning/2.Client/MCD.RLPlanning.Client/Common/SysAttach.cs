using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.IO;

using MCD.Common;
using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Setting;
using MCD.RLPlanning.BLL;
using MCD.RLPlanning.BLL.Setting;
using MCD.RLPlanning.Client.Properties;

namespace MCD.RLPlanning.Client.Common
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SysAttach : BaseUserControl
    {
        //Fields
        private ActionType _cmdType;
        private string m_GroupTitle;
        private AttachmentsBLL attachmentsBLL;
        
        //Properties
        public ActionType CmdType
        {
            get
            {
                return this._cmdType;
            }
            set
            {
                if (!base.DesignMode)
                {
                    if (value == ActionType.View)
                    {
                        this.btnAddAttach.Visible = false;
                        this.btnDelAttach.Visible = false;
                    }
                }
                this._cmdType = value;
            }
        }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 临时业务主键,用于未保存数据上传附件
        /// </summary>
        public string TempObjectID { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public CategoryType Category { get; set; }
        private AttachmentsEntity Entity { get; set; }

        /// <summary>
        /// 获取当前附件列表中是否有附件。
        /// </summary>
        public bool HasFile
        {
            get
            {
                return this.dataGridView1.Rows.Count > 0;
            }
        }
        /// <summary>
        /// 附件服务器地址
        /// </summary>
        private string AttachURL
        {
            get
            {
                return ConfigurationManager.AppSettings["AttachURL"] + string.Empty;
            }
        }
        /// <summary>
        /// 附件大小限制
        /// </summary>
        private int AttachMaxSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["AttachMaxSize"]);
            }
        }
        /// <summary>
        /// 文件后缀名,以逗号分开
        /// </summary>
        private string AttachFileTypeList
        {
            get
            {
                return AppCode.SysEnvironment.AttachFileTypeList;
            }
        }
        /// <summary>
        /// 显示文件图标的文件
        /// </summary>
        private Dictionary<string, Image> ShowIconType
        {
            get
            {
                Dictionary<string, Image> result = new Dictionary<string, Image>();
                result.Add(".doc", Resources.icon_file_doc);
                result.Add(".docx", Resources.icon_file_doc);
                result.Add(".gif", Resources.icon_file_gif);
                result.Add(".jpg", Resources.icon_file_jpg);
                result.Add(".bmp", Resources.icon_file_jpg);
                result.Add(".pdf", Resources.icon_file_pdf);
                result.Add(".ppt", Resources.icon_file_ppt);
                result.Add(".pptx", Resources.icon_file_ppt);
                result.Add(".txt", Resources.icon_file_txt);
                result.Add(".xls", Resources.icon_file_xls);
                result.Add(".xlsx", Resources.icon_file_xls);
                result.Add(".unknow", Resources.icon_file_unknow);
                //
                return result;
            }
        }
        /// <summary>
        /// GroupBox的标题
        /// </summary>
        public string GroupTitle
        {
            get
            {
                return this.m_GroupTitle;
            }
            set
            {
                if (this.m_GroupTitle != value)
                {
                    this.m_GroupTitle = value;
                    if (!string.IsNullOrEmpty(this.m_GroupTitle) && this.m_GroupTitle.Trim().Length > 0)
                    {
                        this.groupBoxTitle.Text = value;
                    }
                }
            }
        }
        #region ctor

        public SysAttach()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void SysAttach_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.Inits();
                //
                if (string.IsNullOrEmpty(this.ObjectID))
                {
                    this.TempObjectID = Guid.NewGuid().ToString();
                }
                this.attachmentsBLL = new AttachmentsBLL();
                this.Entity = new AttachmentsEntity();
                this.dataGridView1.AutoGenerateColumns = false;
                this.BindGridView();
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //
            string id = this.dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            string fileName = this.dataGridView1.CurrentRow.Cells["FileName"].Value.ToString();
            string filePath = this.dataGridView1.CurrentRow.Cells["FilePath"].Value.ToString();
            this.DownloadFile(fileName, filePath);
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            e.Graphics.DrawImage(this.GetImage(e.RowIndex), 
                e.RowBounds.Left + this.dataGridView1.RowHeadersWidth - 20, e.RowBounds.Top + 4, 16, 16);//绘制图标 e.
        }

        private void btnAddAttach_Click(object sender, EventArgs e)
        {
            string filter = "所有类型|*.*";
            if (!string.IsNullOrEmpty(this.AttachFileTypeList))
            {
                this.AttachFileTypeList.Split('|').ToList<string>().ForEach(str =>
                {
                    filter = string.Format("{0}|{1}文件|*{2}", filter, str.TrimStart('.'), "." + str.TrimStart('.'));
                });
            }
            //
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo info = new FileInfo(dialog.FileName);
                if (this.CheckFile(info))
                {
                    Guid fileID = Guid.NewGuid();
                    //
                    this.UpLoadFile(info, fileID);
                }
            }
        }
        private void btnDelAttach_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                this.MessageError(base.GetMessage("ChooseAtt"));
            }
            else
            {
                string createUserID = this.dataGridView1.SelectedRows[0].Cells["CreateUserID"].Value.ToString();
                if (createUserID != AppCode.SysEnvironment.CurrentUser.ID.ToString())
                {
                    this.MessageError(base.GetMessage("OnlyDeleteOwnAtt"));
                }
                else
                {
                    string id = this.dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                    string path = this.dataGridView1.SelectedRows[0].Cells["FilePath"].Value.ToString();
                    this.DeleteFile(id);
                    AttachmentsBLL.DeleteFileByPath(path);
                    //
                    this.BindGridView();
                }
            }
        }

        public void UpdateAttachmentObjectID()
        {
            if (string.IsNullOrEmpty(this.ObjectID))
            {
                throw new Exception("附件关联ID为空");
            }
            //有临时才更新
            if (!string.IsNullOrEmpty(this.TempObjectID))
            {
                attachmentsBLL.UpdateAttachmentObjectID(this.Category, this.ObjectID, this.TempObjectID);
                this.TempObjectID = null;
                BindGridView();
            }
        }

        //Methods
        private void Inits()
        {
            if (this.CmdType == ActionType.View)
            {
                if (string.IsNullOrEmpty(this.ObjectID))
                {
                    throw new Exception("关联业务主键不能为空");
                }
            }
        }
        public void BindGridView()
        {
            this.dataGridView1.Columns.Clear();
            //
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "FileName", base.GetMessage("FileName"), 300);
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "FileSize", base.GetMessage("FileSize"));
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "CreateUserName", base.GetMessage("CreateUserName"));
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "CreateTime", base.GetMessage("CreateTime"));
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "ID", "ID");
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "FilePath", "FilePath");
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "CreateUserID", "CreateUserID");
            GridViewHelper.AppendColumnToDataGridView(this.dataGridView1, "FileType", "FileType");
            this.dataGridView1.Columns[4].Visible = false;
            this.dataGridView1.Columns[5].Visible = false;
            this.dataGridView1.Columns[6].Visible = false;
            this.dataGridView1.Columns[7].Visible = false;
            if (this.attachmentsBLL == null)
            {
                this.attachmentsBLL = new AttachmentsBLL();
            }
            //
            this.dataGridView1.DataSource = this.attachmentsBLL.SelectAttachment(this.Category, 
                string.IsNullOrEmpty(this.ObjectID) ? this.TempObjectID : this.ObjectID).Tables[0];
        }
        /// <summary>
        /// 下载文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        private void DownloadFile(string fileName, string filePath)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.FileName = fileName;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                UpDownloadFrm download = new UpDownloadFrm();
                download.SavePath = saveDlg.FileName;
                download.DownLoadUrl = string.Format("{0}/DownloadHandler.ashx?FileName={1}&FilePath={2}", this.AttachURL, fileName, filePath);
                download.DownLoadFileAsync();
                download.Show();
            }
        }
        private Image GetImage(int rowIndex)
        {
            string filetype = Convert.ToString(this.dataGridView1.Rows[rowIndex].Cells[7].Value);
            if (this.ShowIconType.ContainsKey(filetype))
            {
                return this.ShowIconType[filetype];
            }
            else
            {
                return this.ShowIconType[".unknow"];
            }
        }

        private bool CheckFile(FileInfo info)
        {
            if (info.Length > this.AttachMaxSize * 1024 * 1024)
            {
                MessageBox.Show(string.Format(base.GetMessage("SizeError"), this.AttachMaxSize));
                return false;
            }
            //
            string ext = Path.GetExtension(info.Name);
            bool flag = this.AttachFileTypeList.Split('|').Any(str => { return ("." + str.TrimStart('.')).ToLower() == ext.ToLower(); });
            if (!flag)
            {
                this.MessageError(string.Format(base.GetMessage("ExtensionError"), ext));
                return false;
            }
            return true;
        }
        /// <summary>
        /// 上传文件。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="fileID"></param>
        private void UpLoadFile(FileInfo info, Guid fileID)
        {
            string ext = Path.GetExtension(info.Name);
            string filePath = string.Format(@"{0}\{1}\{2}", this.Category, this.ObjectID, fileID.ToString() + ext);
            UpDownloadFrm upload = new UpDownloadFrm();
            upload.UploadFile = info;
            upload.UploadUrl = string.Format("{0}/UploadHandler.ashx?Path={1}", this.AttachURL, filePath);
            upload.UploadCompleted += (object sender, EventArgs e) =>
            {
                this.InsertIntoDatabase(info, fileID);
                //
                this.BindGridView();
                base.MessageInformation(base.GetMessage("UploadSucess"));
            };
            upload.UploadFileAsync();
            upload.ShowDialog();
        }
        private void InsertIntoDatabase(FileInfo info, Guid fileID)
        {
            string ext = Path.GetExtension(info.Name);
            string filePath = string.Format(@"{0}\{1}\{2}", this.Category, this.ObjectID, fileID.ToString() + ext);
            //
            this.Entity.Category = this.Category;
            this.Entity.ObjectID = string.IsNullOrEmpty(this.ObjectID) ? this.TempObjectID : this.ObjectID;
            this.Entity.FileName = info.Name;
            this.Entity.FileType = ext;
            this.Entity.FileSize = Convert.ToInt32(info.Length);
            this.Entity.FilePath = filePath;
            this.Entity.CreateUserID = AppCode.SysEnvironment.CurrentUser.ID.ToString();
            this.Entity.CreateUserName = AppCode.SysEnvironment.CurrentUser.EnglishName;
            //
            this.attachmentsBLL.InsertAttachment(this.Entity);
        }
        private void DeleteFile(string id)
        {
            if (MessageBox.Show(base.GetMessage("ConfirmDelete"), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.attachmentsBLL.DeleteAttachment(id);
                this.BindGridView();
            }
        }
    }
}
