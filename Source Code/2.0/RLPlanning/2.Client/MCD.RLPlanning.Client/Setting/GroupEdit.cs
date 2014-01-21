using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Client.UUPGroupService;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GroupEdit : BaseEdit
    {
        //Fields
        private GroupEntity groupEntity = new GroupEntity();
        /// <summary>
        /// 未选择的用户
        /// </summary>
        public DataTable TableUsers = null;
        /// <summary>
        /// 已选择的用户
        /// </summary>
        public DataTable TableSelectedUsers = null;
        #region ctor

        /// <summary>
        /// 新增时
        /// </summary>
        /// <param name="systemCode"></param>
        public GroupEdit(string systemCode)
        {
            base.IsAddNew = true;
            this.groupEntity.ID = Guid.NewGuid();
            this.groupEntity.SystemCode = systemCode;
            //
            InitializeComponent();
        }

        /// <summary>
        /// 编辑时
        /// </summary>
        /// <param name="entity"></param>
        public GroupEdit(GroupEntity entity)
        {
            base.IsAddNew = false;
            this.groupEntity = entity;
            //
            InitializeComponent();
        }
        #endregion

        private void GroupEdit_Load(object sender, EventArgs e)
        {
            this.BindFormControl();
        }
        #region 按钮事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.groupEntity].EndCurrentEdit();
            //
            if (this.txtGroupName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(base.GetMessage("GroupNameEmpty"));
                return;
            }
            else if (base.GroupService.IsExistsUserGroup(this.groupEntity.SystemCode, this.groupEntity.GroupName, this.groupEntity.ID))
            {
                MessageBox.Show(base.GetMessage("GroupNameExists"));
                return;
            }
            if (base.ExecuteBoolean(() => {
                if (base.IsAddNew)
                {
                    return base.GroupService.InsertGroups(this.groupEntity, this.TableSelectedUsers);
                }
                else
                {
                    return base.GroupService.UpdateGroups(this.groupEntity, this.TableSelectedUsers);
                }
            }, "保存用户组错误", "用户组", base.GetMessage("SaveFailure"), base.GetMessage("SaveSuccess")))
            {
                base.RemoveDataTable("UserGroup");
                base.Close();
            }
            //
            base.btnSave_Click(sender, e);
        }
        #endregion

        /// <summary>
        /// 绑定界面控件
        /// </summary>
        public override void BindFormControl()
        {
            this.BindUserBox();
            // 移除绑定信息
            this.txtGroupName.DataBindings.Clear();
            this.txtRemark.DataBindings.Clear();
            // 设置绑定
            this.txtGroupName.DataBindings.Add("Text", this.groupEntity, "GroupName");
            this.txtRemark.DataBindings.Add("Text", this.groupEntity, "Remark");
            base.BindFormControl();
            this.Height = this.btnSave.Height + this.btnSave.Top + 50;
            //
            ControlHelper.AlignCenterForm(this);
        }
        /// <summary>
        /// 绑定用户下拉框
        /// </summary>
        private void BindUserBox()
        {
            if (!base.ExecuteAction(() => {
                DataSet ds = base.GroupService.GetUsersByGroupID(this.groupEntity.ID, this.groupEntity.SystemCode);
                //
                this.TableUsers = ds.Tables[1];
                this.TableSelectedUsers = ds.Tables[0];
                this.TableUsers.PrimaryKey = new DataColumn[] { this.TableUsers.Columns["ID"] };
                this.TableSelectedUsers.PrimaryKey = new DataColumn[] { this.TableSelectedUsers.Columns["ID"] };
            } , "访问用户数据错误!", "用户组"))
            {
                return;
            }
            // 设置选择用户下拉框的值
            this.lboxUsers.DataSource = this.TableUsers;
            this.lboxUsers.DisplayMember = "DisplayName";
            this.lboxUsers.DisplayMember = "UserID";
            this.lBoxSelected.DataSource = this.TableSelectedUsers;
            this.lBoxSelected.DisplayMember = "DisplayName";
            this.lBoxSelected.DisplayMember = "UserID";
        }
    }
}