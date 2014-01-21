using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCD.Common;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class ToolPanel : UserControl
    {
        public ToolPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置按钮控制的百分比周期控件。
        /// </summary>
        public RatioCyclePanel RatioCycleControl { get; set; }

        private void toollbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.RatioCycleControl == null)
                return;

            if (this.toollbl.Text == "已启用")
            {
                //禁用规则
                this.RatioCycleControl.Visible = false;

                //禁用关联的实体
                if (this.RatioCycleControl is IEntityEnabled)
                {
                    (this.RatioCycleControl as IEntityEnabled).EntityEnabled = false;
                }

                ControlHelper.FindControl(this.RatioCycleControl, ctrl =>
                {
                    return ctrl is IEntityEnabled;
                }).ForEach(item =>
                {
                    (item as IEntityEnabled).EntityEnabled = false;
                });

                //if (this.RatioCycleControl.CurrentCycleSetting != null)
                //{
                //    this.RatioCycleControl.RatioRuleBLL.DeleteSingleRatioCycleSetting(this.RatioCycleControl.CurrentCycleSetting);
                //}

                this.toollbl.Image = Properties.Resources.已禁用;
                this.toollbl.Text = "已禁用";
            }
            else
            {
                //启用规则
                this.RatioCycleControl.Visible = true;

                //启用关联的实体
                if (this.RatioCycleControl is IEntityEnabled)
                {
                    (this.RatioCycleControl as IEntityEnabled).EntityEnabled = true;
                }

                //添加周期记录到数据库
                (this.RatioCycleControl as IRentRule).Enable();

                ControlHelper.FindControl(this.RatioCycleControl, ctrl =>
                {
                    return ctrl is RatioTimeIntervalPanel;
                }).ForEach(item =>
                {
                    (item as IRentRule).Enable();
                });

                ControlHelper.FindControl(this.RatioCycleControl, ctrl =>
                {
                    return ctrl is IEntityEnabled;
                }).ForEach(item =>
                {
                    (item as IEntityEnabled).EntityEnabled = true;
                });

                this.toollbl.Image = Properties.Resources.已启用;
                this.toollbl.Text = "已启用";
            }

            ////可以禁用变成启用, 不可从启用变成禁用
            //if (AppCode.SysEnvironment.ContractCopyType == ContractCopyType.变更 && this.toollbl.Enabled)
            //{
            //    this.toollbl.Enabled = false;
            //}
        }

        public void InitStatus(bool enable)
        {
            if (!enable)
            {
                toollbl_LinkClicked(this.toollbl, null);
            }
        }
    }
}
