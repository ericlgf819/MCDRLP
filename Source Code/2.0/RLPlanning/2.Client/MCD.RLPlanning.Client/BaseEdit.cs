using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.RLPlanning.Client.UUPGroupService;

namespace MCD.RLPlanning.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseEdit : BaseFrm
    {
        //Fields
        /// <summary>
        /// 新增 Or 编辑   True表示新增；False表示编辑
        /// 默认值为 True
        /// </summary>
        protected bool IsAddNew = true;
        private EDIT_STATUS currentState = EDIT_STATUS.AddNew;
        
        //Properties
        /// <summary>
        /// 当前状态   编辑/新增/另存为
        /// </summary>
        public EDIT_STATUS CurrentState
        {
            get { return this.currentState; }
            set { this.currentState = value; }
        }
        /// <summary>
        /// 调用该窗口的主窗口
        /// </summary>
        public BaseFrm ParentFrm { get; set; }
        #region ctor

        public BaseEdit()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        private void BaseEdit_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.BindFormControl();
            }
        }
        /// <summary>
        /// 回车事件,触发保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BaseEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidInput())
            {
                if (this.SaveData())
                {
                    if (this.ParentFrm != null)
                    {
                        this.ParentFrm.RefreshList = true;
                    }
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 窗体关闭时方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ParentFrm != null && this.ParentFrm.RefreshList)
            {
                this.ParentFrm.RefreshFrm();
            }
        }

        //Methods
        /// <summary>
        /// 窗体加载时绑定界面控件。
        /// </summary>
        public virtual void BindFormControl()
        {
            //
        }
        /// <summary>
        /// 在派生类中重写以保存窗体的数据，若保存成功则返回True，同时刷新父窗体并关闭当前窗体。
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveData()
        {
            return true;
        }
        /// <summary>
        /// 在派生类中重写以以检验用户输入的数据，若检验通过则返回True，同时执行保存数据的方法。
        /// </summary>
        /// <returns></returns>
        public virtual bool ValidInput()
        {
            return true;
        }
    }

    /// <summary>
    /// 编辑状态
    /// </summary>
    public enum EDIT_STATUS
    {
        /// <summary>
        /// 查看
        /// </summary>
        View = 0,
        /// <summary>
        /// 新增
        /// </summary>
        AddNew = 1,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 2,
        /// <summary>
        /// 另存为
        /// </summary>
        SaveAS = 3,
        /// <summary>
        /// 审核
        /// </summary>
        Check = 4,
        /// <summary>
        /// 复核
        /// </summary>
        //Review = 5,
        /// <summary>
        /// 事项反馈
        /// </summary>
        FeedBack = 6,
        /// <summary>
        /// 空
        /// </summary>
        None = -1
    }
}