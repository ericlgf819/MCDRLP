using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.UUPGroupService;

namespace MCD.RLPlanning.Client.Master
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserEdit : BaseEdit
    {
        //Fields
        private UserBLL UserBLL = new UserBLL();
        private DeptBLL DeptBLL = new DeptBLL();
        private CompanyBLL CompanyBLL = new CompanyBLL();
        private UserEntity user = null;
        #region ctor

        public UserEdit(Guid userID)
        {
            this.user = this.UserBLL.SelectSingleUser(userID);
            //
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, new EventArgs());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCompany_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    this.SetChildNodesChecked(e.Node.Nodes, e.Node.Checked);
                }
                //
                this.SetParentNodeChecked(e.Node);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.user].EndCurrentEdit();
            // Check        
            if (!UIChecker.VerifyTextBoxNull(this.txtEnglishName, base.GetMessage("EnglishNameNULL"), base.GetMessage("Caption")))
            {
                return;
            }
            else if (!UIChecker.VerifyComboxNull(this.cbbDepartment, base.GetMessage("DeptNULL"), base.GetMessage("Caption")))
            {
                return;
            }
            else if ((this.cbbDepartment.SelectedItem as DataRowView)["Status"].ToString().ToUpper() == "I")
            {
                base.MessageError(base.GetMessage("DeptInactive"), base.GetMessage("Caption"));
                return;
            }
            else if (!UIChecker.VerifyComboxNull(this.cbbGroup, base.GetMessage("GroupNULL"), base.GetMessage("Caption")))
            {
                return;
            }
            else if (!this.txtEmail.Check(CheckType.Email))
            {
                base.MessageError(base.GetMessage("EmailError"), base.GetMessage("Caption"));
                //
                this.txtEmail.Focus();
                return;
            }
            // 检测用户公司
            List<TreeNode> checkedNodes = new List<TreeNode>();
            this.GetCheckedNodes(this.tvCompany.Nodes, ref checkedNodes);
            if (checkedNodes.Count == 0)
            {
                base.MessageError(base.GetMessage("CompanyNULL"), base.GetMessage("Caption"));
                return;
            }//检测公司状态?? A
            // >Save
            this.user.EnglishName = this.txtEnglishName.Text.Trim();
            this.user.DeptName = this.cbbDepartment.Text;
            this.user.ChineseName = this.txtChineseName.Text.Trim();
            string companycodes = string.Empty;
            foreach (TreeNode node in checkedNodes)
            {
                companycodes += node.ToolTipText + ',';
            }
            user.CompanyCodes = companycodes.TrimEnd(',');
            this.user.Disabled = !this.rbtnDisableY.Checked;
            this.user.Locked = !this.rbtnLockedY.Checked;
            this.user.Remark = this.txtRemark.Text.Trim();
            this.user.OperationID = AppCode.SysEnvironment.CurrentUser.ID;
            //当前最后更新时间user已初始化赋值
            //
            int res = this.UserBLL.UpdateSingleUser(this.user);
            if (res == 0)
            {
                base.MessageError(base.GetMessage("CheckerError"), base.GetMessage("Caption"));
                return;
            }
            else if (res == 1)
            {
                base.MessageError(base.GetMessage("ReviewError"), base.GetMessage("Caption"));
                return;
            }
            else if (res == 2)
            {
                base.MessageError(base.GetMessage("SaveFailure"), base.GetMessage("Caption"));
                return;
            }
            else
            {
                base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                this.Close();
            }
            //
            base.btnSave_Click(sender, e);
        }
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UserBLL.Dispose();
            this.DeptBLL.Dispose();
            this.CompanyBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }

        /// <summary>
        /// 绑定界面控件
        /// </summary>
        public override void BindFormControl()
        {
            // 绑定所属部门 -- 同步表,可缓存
            DataTable dtValue = base.GetDataTable("Department", () => {
                DataSet dsDepartment = this.DeptBLL.SelectActiveDept();
                if (dsDepartment != null && dsDepartment.Tables.Count == 1)
                {
                    return dsDepartment.Tables[0];
                }
                else
                {
                    return null;
                }
            });
            ControlHelper.BindComboBox(this.cbbDepartment, dtValue, "DeptName", "DeptCode");            
            // 绑定用户组
            dtValue = base.GroupService.SelectGroupsBySystemCode(AppCode.SysEnvironment.SystemCode, string.Empty);
            ControlHelper.BindComboBox(this.cbbGroup, dtValue, "GroupName", "ID");
            // 获取公司树
            DataSet dsCompany = this.CompanyBLL.SelectAllComapnyWithArea(new CompanyEntity());
            if (dsCompany != null && dsCompany.Tables.Count == 1)
            {
                dtValue = dsCompany.Tables[0];
            }
            else
            {
                dtValue = null;
            }
            this.BindTreeView(this.tvCompany, dtValue);
            // >User Data
            this.lblUserNameValue.DataBindings.Add("Text", this.user, "UserName");
            this.txtEnglishName.DataBindings.Add("Text", this.user, "EnglishName");
            this.cbbDepartment.DataBindings.Add("SelectedValue", this.user, "DeptCode");
            this.cbbGroup.DataBindings.Add("SelectedValue", this.user, "GroupID");
            this.txtChineseName.DataBindings.Add("Text", this.user, "ChineseName");
            this.txtEmail.DataBindings.Add("Text", this.user, "Email");
            //是否禁用
            if (!user.Disabled)
            {
                this.rbtnDisableY.Checked = true;
            }
            else
            {
                this.rbtnDisableN.Checked = true;
            }
            //是否锁定
            if (!user.Locked)
            {
                this.rbtnLockedY.Checked = true;
            }
            else
            {
                this.rbtnLockedN.Checked = true;
            }
            this.txtRemark.DataBindings.Add("Text", this.user, "Remark");
            //
            base.BindFormControl();
        }
        private void BindTreeView(TreeView tree, DataTable dt)
        {
            if (tree != null && dt != null)
            {
                TreeNode rootNode = new TreeNode(base.GetMessage("TreeRootNode")), areaNode = null, companyNode = null;
                //
                string areaId = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    if (areaId == string.Empty || areaId != row["AreaID"].ToString())
                    {
                        areaId = row["AreaID"].ToString();
                        //
                        if (areaNode != null)
                        {
                            if (this.IsCheckedAll(areaNode)) 
                            {
                                areaNode.Checked = true;
                            }
                            //
                            rootNode.Nodes.Add(areaNode);
                        }
                        areaNode = new TreeNode(row["AreaName"].ToString());
                    }
                    //
                    companyNode = new TreeNode(string.Format("{0} {1}", row["CompanyCode"], row["CompanyName"]));
                    companyNode.ToolTipText = row["CompanyCode"].ToString();
                    if (string.Format(",{0},", user.CompanyCodes).IndexOf(string.Format(",{0},",companyNode.ToolTipText)) != -1)
                    {
                        companyNode.Checked = true;
                    }
                    areaNode.Nodes.Add(companyNode);
                }
                if (this.IsCheckedAll(areaNode))
                {
                    areaNode.Checked = true;
                }
                rootNode.Nodes.Add(areaNode);
                //
                if (this.IsCheckedAll(rootNode))
                {
                    rootNode.Checked = true;
                }
                tree.Nodes.Add(rootNode);
                //
                rootNode.Expand();
            }
        }
        private void GetCheckedNodes(TreeNodeCollection nodes, ref List<TreeNode> checkedNodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Level == 2)
                {
                    if (node.Checked)
                    {
                        checkedNodes.Add(node);
                    }
                }
                else if (node.Nodes.Count > 0)
                {
                    this.GetCheckedNodes(node.Nodes, ref checkedNodes);
                }
            }
        }

        private void SetChildNodesChecked(TreeNodeCollection nodes, bool check)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = check;
                //
                if (node.Nodes.Count > 0)
                {
                    this.SetChildNodesChecked(node.Nodes, check);
                }
            }
        }
        private void SetParentNodeChecked(TreeNode node)
        {
            if (node.Parent != null)
            {
                if (!node.Checked)
                {
                    if (node.Parent.Checked)
                    {
                        node.Parent.Checked = false;
                        this.SetParentNodeChecked(node.Parent);
                    }
                }
                else
                {
                    bool checkedall = true;
                    foreach (TreeNode tnode in node.Parent.Nodes)
                    {
                        if (!tnode.Checked)
                        {
                            checkedall = false;
                            break;
                        }
                    }
                    //
                    if (checkedall)
                    {
                        node.Parent.Checked = true;
                        this.SetParentNodeChecked(node.Parent);
                    }
                }
            }
        }
        private bool IsCheckedAll(TreeNode node)
        {
            foreach (TreeNode tnode in node.Nodes)
            {
                if (!tnode.Checked)
                {
                    return false;
                }
            }
            return true;
        }
    }
}