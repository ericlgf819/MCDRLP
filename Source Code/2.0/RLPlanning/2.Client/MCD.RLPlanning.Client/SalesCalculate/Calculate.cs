using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common.SRLS;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.Master;
using MCD.RLPlanning.Client.Workflow.Controls;

namespace MCD.RLPlanning.Client.SalesCalculate
{
    /// <summary>
    /// 待办任务分类树。
    /// </summary>
    public partial class Calculate : BaseFrm
    {
        //Fields
        private CompanyBLL CompanyBLL = new CompanyBLL();
        private UserCompanyBLL UserCompanyBLL = new UserCompanyBLL();

        /// <summary>
        /// 右侧租金计算界面
        /// </summary>
        private TCalculate list = null;

        //Properties
        #region ctor

        public Calculate()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 窗体加载时加载待办数量。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 获取公司树
            DataSet dsCompany = this.CompanyBLL.SelectAllComapnyWithArea(new CompanyEntity()
            {
                UserID = AppCode.SysEnvironment.CurrentUser.ID.ToString(),
                Status = "A"
            });
            DataTable dtValue;
            if (dsCompany != null && dsCompany.Tables.Count == 1)
            {
                dtValue = dsCompany.Tables[0];
            }
            else
            {
                dtValue = null;
            }
            this.BindTreeView(this.tvCompany, dtValue);

            //右侧租金计算界面载入
            list = new TCalculate();
            list.Dock = DockStyle.Fill;
            ClearControls(splitContainer1.Panel2);
            this.splitContainer1.Panel2.Controls.Add(list);

            //将CompanyCode全部存入hashset中
            FillTheCompanyCodeHashSet(dsCompany.Tables[0]);
        }

        /// <summary>
        /// 情况右侧控件
        /// </summary>
        /// <param name="panel"></param>
        protected void ClearControls(Panel panel)
        {
            foreach (Control control in this.splitContainer1.Panel2.Controls)
            {
                control.Dispose();
            }
            panel.Controls.Clear();
        }

        private void tvCompany_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                //记录/删除 选中/反选的companycode
                AddOrDeleteCompanyCodeFromHashSet(e.Node, e.Node.Checked);

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
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //// 检测用户公司
            //List<TreeNode> checkedNodes = new List<TreeNode>();
            //this.GetCheckedNodes(this.tvCompany.Nodes, ref checkedNodes);
            //if (checkedNodes.Count == 0)
            //{
                
            //    return;
            //}
            ////
            //string companycodes = string.Empty;
            //foreach (TreeNode node in checkedNodes)
            //{
            //    companycodes += node.ToolTipText + ',';
            //}
            //companycodes = companycodes.TrimEnd(',');
        }

        //Methods
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
                        areaNode.Checked = true;
                    }
                    //
                    companyNode = new TreeNode(string.Format("{0} {1}", row["CompanyCode"], row["CompanyName"]));
                    companyNode.Checked = true;
                    companyNode.ToolTipText = row["CompanyCode"].ToString();
                    areaNode.Nodes.Add(companyNode);
                }

                if (null != areaNode)
                {
                    rootNode.Nodes.Add(areaNode);
                    rootNode.Checked = true;
                    //
                    rootNode.Expand();
                    tree.Nodes.Add(rootNode);
                }
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
                //记录/删除 选中/反选的companycode
                AddOrDeleteCompanyCodeFromHashSet(node, node.Checked);
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

        /// <summary>
        /// 是否从CompanyCode的HashSet中添加或者删除CompanyCode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="bAdd">true为添加，false为删除</param>
        private void AddOrDeleteCompanyCodeFromHashSet(TreeNode node, bool bAdd)
        {
            // Company节点一定是叶子节点
            if (0 != node.Nodes.Count)
                return;

            // CompanyCode length
            int ciCompanyCodeLength = node.Text.IndexOf(' ');

            // 如果node.Text长度小于ciCompanyCodeLength，则说明不是companycode，可以直接返回
            if (node.Text.Length < ciCompanyCodeLength)
                return;

            string strCompanyCode = node.Text.Substring(0, ciCompanyCodeLength);

            // 如果得到的字符串不能转换为数字，说明不是company code
            int iCompanyCode;
            try
            {
                iCompanyCode = int.Parse(strCompanyCode);
            }
            catch
            {
                return;
            }

            // 如果Panel为空或者Panel里的m_setCompanyCodes为空，则直接返回
            if (null == list || null == list.m_setCompanyCodes)
                return;

            //根据标志位添加或者删除companycode
            if (bAdd)
            {
                list.m_setCompanyCodes.Add(strCompanyCode);
            }
            else
            {
                list.m_setCompanyCodes.Remove(strCompanyCode);
            }
        }

        /// <summary>
        /// 初始化时，公司是全部勾选上的，所以需要将所有的公司信息存入hashset中
        /// </summary>
        private void FillTheCompanyCodeHashSet(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                try
                {
                    list.m_setCompanyCodes.Add(item["CompanyCode"].ToString());
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}