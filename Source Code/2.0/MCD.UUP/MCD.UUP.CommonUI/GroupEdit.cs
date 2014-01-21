using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.UUP.CommonUI.GroupService;

namespace MCD.UUP.CommonUI
{
    public partial class GroupEdit : BaseFrm
    {
        private bool IsAddNew = true;
        GroupService.GroupServiceClient groupService = new GroupServiceClient();
        /// <summary>
        /// 新增时
        /// </summary>
        /// <param name="systemCode"></param>
        public GroupEdit(string systemCode)
        {
            groupEntity.SystemCode = systemCode;
            groupEntity.ID = Guid.NewGuid();
            this.IsAddNew = true;
            InitializeComponent();
            this.Text = "新建用户组";
        }

        /// <summary>
        /// 编辑时
        /// </summary>
        /// <param name="entity"></param>
        public GroupEdit(GroupEntity entity)
        {
            this.IsAddNew = false;
            this.groupEntity = entity;
            InitializeComponent();
            this.Text = "编辑用户组";
        }

        private GroupEntity groupEntity = new GroupEntity();

        /// <summary>
        /// 未选择的用户
        /// </summary>
        public DataTable TableUsers = null;
        /// <summary>
        /// 已选择的用户
        /// </summary>
        public DataTable TableSelectedUsers = null;

        /// <summary>
        /// 绑定界面控件
        /// </summary>
        public void BindFormControl()
        {
            BindUserBox();

            // 移除绑定信息
            this.txtGroupName.DataBindings.Clear();
            this.txtRemark.DataBindings.Clear();
            // 设置绑定
            this.txtGroupName.DataBindings.Add("Text", groupEntity, "GroupName");
            this.txtRemark.DataBindings.Add("Text", groupEntity, "Remark");
        }

        /// <summary>
        /// 绑定用户下拉框
        /// </summary>
        private void BindUserBox()
        {
            
            if (!ExecuteAction(() =>
            {
                DataSet ds = groupService.GetUsersByGroupID(groupEntity.ID, groupEntity.SystemCode);
                TableUsers = ds.Tables[1];
                TableSelectedUsers = ds.Tables[0];
                TableUsers.PrimaryKey = new DataColumn[] { TableUsers.Columns["ID"] };
                TableSelectedUsers.PrimaryKey = new DataColumn[] { TableSelectedUsers.Columns["ID"] };
            }
                 , "访问用户数据错误!"))
            {
                return;
            }

            // 设置选择用户下拉框的值
            this.lboxUsers.DataSource = TableUsers;
            this.lboxUsers.DisplayMember = "DisplayName";
            this.lboxUsers.DisplayMember = "UserID";
            this.lBoxSelected.DataSource = TableSelectedUsers;
            this.lBoxSelected.DisplayMember = "DisplayName";
            this.lBoxSelected.DisplayMember = "UserID";
        }

        #region 按钮事件 --------------
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.BindingContext[groupEntity].EndCurrentEdit();

            if (this.txtGroupName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入用户组名称!");
                return;
            }
            if (groupService.IsExistsUserGroup(groupEntity.SystemCode, groupEntity.GroupName, groupEntity.ID))
            {
                MessageBox.Show("该用户组名称已经存在!");
                return;
            }
            ExecuteBoolean(() =>
            {
                if (IsAddNew)
                {
                    return groupService.InsertGroups(groupEntity, TableSelectedUsers);
                }
                else
                {
                    return groupService.UpdateGroups(groupEntity, TableSelectedUsers);
                }
            }, "保存用户组错误", "保存失败", "保存成功!");

        }

        /// <summary>
        /// 添加所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            DataRow newRow;
            foreach (DataRow row in this.TableUsers.Rows)
            {
                newRow = this.TableSelectedUsers.NewRow();
                foreach (DataColumn col in this.TableUsers.Columns)
                {
                    newRow[col.ColumnName] = row[col.ColumnName];
                }
                this.TableSelectedUsers.Rows.Add(newRow);
            }
            this.TableUsers.Clear();
        }

        /// <summary>
        /// 添加单个用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOne_Click(object sender, EventArgs e)
        {
            if (this.lboxUsers.SelectedValue == null) return;
            DataRow row = ((DataRowView)this.lboxUsers.SelectedValue).Row;
            DataRow newRow = this.TableSelectedUsers.NewRow();
            foreach (DataColumn col in this.TableSelectedUsers.Columns)
            {
                newRow[col.ColumnName] = row[col.ColumnName];
            }
            this.TableSelectedUsers.Rows.Add(newRow);

            this.TableUsers.Rows.Remove(row);
        }

        /// <summary>
        /// 移除单个选定行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveOne_Click(object sender, EventArgs e)
        {
            if (this.lBoxSelected.SelectedValue == null) return;
            DataRow row = ((DataRowView)this.lBoxSelected.SelectedValue).Row;
            DataRow newRow = this.TableUsers.NewRow();
            foreach (DataColumn col in this.TableUsers.Columns)
            {
                newRow[col.ColumnName] = row[col.ColumnName];
            }
            this.TableUsers.Rows.Add(newRow);

            this.TableSelectedUsers.Rows.Remove(row);
        }

        /// <summary>
        /// 移除所有选定行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            DataRow newRow;
            foreach (DataRow row in this.TableSelectedUsers.Rows)
            {
                newRow = this.TableUsers.NewRow();
                foreach (DataColumn col in this.TableUsers.Columns)
                {
                    newRow[col.ColumnName] = row[col.ColumnName];
                }
                this.TableUsers.Rows.Add(newRow);
            }
            this.TableSelectedUsers.Clear();
        }
        #endregion

        private void GroupEdit_Load(object sender, EventArgs e)
        {
            BindFormControl();
        }

        /// <summary>
        /// 双击已选用户，进行移除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lBoxSelected_DoubleClick(object sender, EventArgs e)
        {
            btnRemoveOne_Click(sender, e);
        }

        /// <summary>
        /// 双击用户列表，进行选择操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lboxUsers_DoubleClick(object sender, EventArgs e)
        {
            btnAddOne_Click(sender, e);
        }

    }
}
