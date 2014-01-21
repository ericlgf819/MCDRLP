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
    public partial class UserAdd : BaseEdit
    {
        //Fields
        private UserBLL UserBLL = new UserBLL();
        private DeptBLL DeptBLL = new DeptBLL();
        private CompanyBLL CompanyBLL = new CompanyBLL();
        #region ctor

        public UserAdd()
        {
            InitializeComponent();
        }
        #endregion

        //Fields
        private void cbbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            ControlHelper.SelectComboItemByBindValue(sender, e);
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
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            UserEntity user = new UserEntity();
            //
            this.BindingContext[user].EndCurrentEdit();
            user.UserName = this.txtUserName.Text.Trim();
            user.EnglishName = this.txtEnglishName.Text.Trim();
            user.ChineseName = this.txtChineseName.Text.Trim();
            user.Email = this.txtEmail.Text.Trim();
            user.Remark = this.txtRemark.Text.Trim();
            // 检测用户名是否为空
            if (!UIChecker.VerifyTextBoxNull(this.txtUserName, base.GetMessage("UserNameNULL"), base.MessageTitle))
            {
                this.txtUserName.Focus();
                return;
            }
            // 检测用户名不可以为中文名
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(user.UserName);
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)
                {
                    base.MessageError(base.GetMessage("UserNameError"), base.GetMessage("Caption"));
                    //
                    this.txtUserName.Focus();
                    return;
                }
            }
            // 检测用户名最小长度为6
            if (user.UserName.Length < 6)
            {
                base.MessageError(base.GetMessage("UserNameLengthError"), base.GetMessage("Caption"));
                this.txtUserName.Focus();
                return;
            }
            // 检测英文名不能为空
            else if (!UIChecker.VerifyTextBoxNull(this.txtEnglishName, base.GetMessage("EnglishNameNULL"), base.MessageTitle))
            {
                this.txtEnglishName.Focus();
                return;
            }
            // 检测部门不能为空
            else if (this.cbbDepartment.SelectedValue == null)
            {
                base.MessageError(base.GetMessage("DeptNULL"), base.GetMessage("Caption"));
                return;
            }
            // 检测用户组不能为空
            else if (this.cbbGroup.SelectedValue == null)
            {
                base.MessageError(base.GetMessage("GroupNULL"), base.GetMessage("Caption"));
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
            }
            // >Pass
            user.DeptCode = this.cbbDepartment.SelectedValue.ToString();
            user.DeptName = this.cbbDepartment.Text;
            user.GroupID = new Guid(this.cbbGroup.SelectedValue.ToString());
            string companycodes = string.Empty;
            foreach (TreeNode node in checkedNodes)
            {
                companycodes += node.ToolTipText + ',';
            }
            user.CompanyCodes = companycodes.TrimEnd(',');
            user.OperationID = AppCode.SysEnvironment.CurrentUser.ID;
            // >Save
            string UserID = this.UserBLL.InsertSingleUser(user);
            if (UserID == "0")
            {
                base.MessageError(base.GetMessage("UserNameExists"), base.GetMessage("Caption"));
                return;
            }
            else if (UserID.Length == 36)
            {
                base.MessageInformation(base.GetMessage("SaveSuccess"), base.GetMessage("Caption"));
                this.Close();
            }
            else
            {
                base.MessageError(base.GetMessage("SaveFailure"), base.GetMessage("Caption"));
                return;
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
        /// 绑定界面控件信息
        /// </summary>
        public override void BindFormControl()
        {
            base.BindFormControl();
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
            this.cbbDepartment.SelectedValue = "90020104";
            // 绑定用户组
            dtValue = base.GroupService.SelectGroupsBySystemCode(AppCode.SysEnvironment.SystemCode, string.Empty);
            ControlHelper.BindComboBox(this.cbbGroup, dtValue, "GroupName", "ID");
            this.cbbGroup.SelectedIndex = this.cbbGroup.Items.Count - 1;//
            // 获取公司树
            DataSet dsCompany = this.CompanyBLL.SelectAllComapnyWithArea(new CompanyEntity() {
                Status = "A"
            });
            if (dsCompany != null && dsCompany.Tables.Count == 1)
            {
                dtValue = dsCompany.Tables[0];
            }
            else
            {
                dtValue = null;
            }
            this.BindTreeView(this.tvCompany, dtValue);
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
                            rootNode.Nodes.Add(areaNode);
                        }
                        areaNode = new TreeNode(row["AreaName"].ToString());
                    }
                    //
                    companyNode = new TreeNode(string.Format("{0} {1}",row["CompanyCode"], row["CompanyName"]));
                    companyNode.ToolTipText = row["CompanyCode"].ToString();
                    areaNode.Nodes.Add(companyNode);
                }
                rootNode.Nodes.Add(areaNode);
                //
                rootNode.Expand();
                tree.Nodes.Add(rootNode);
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
    }
}