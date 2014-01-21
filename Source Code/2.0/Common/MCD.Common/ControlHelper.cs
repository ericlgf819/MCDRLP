using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

using MCD.Controls;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlHelper
    {
        /// <summary>
        /// 设置所有控件为不可写状态
        /// </summary>
        /// <param name="parent"></param>
        public static void DisEnabledControl(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                }
                else if (ctrl is RadioButton
                    || ctrl is CheckBox
                    || ctrl is ComboBox
                    || ctrl is DateTimePicker
                    || ctrl is UCButton
                    || ctrl is ListBox)
                {
                    ctrl.Enabled = false;
                }
                else if (ctrl.Controls.Count > 0)
                {
                    ControlHelper.DisEnabledControl(ctrl);
                }
            }
        }
        /// <summary>
        /// 递归设置指定控件以及控件的所有子控件的启用状态。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="enabled"></param>
        public static void EnabledControl(Control ctrl, bool enabled)
        {
            if (ctrl is NumericUpDown)
            {
                if (!enabled)
                {
                    (ctrl as NumericUpDown).ReadOnly = true;
                    (ctrl as NumericUpDown).Increment = 0;
                }
            }
            if (ctrl is LinkLabel)// || ctrl is ComboBox || ctrl is DateTimePicker)
            {
                ctrl.Enabled = enabled;
            }
            else if (ctrl is GroupBox || ctrl is Label || ctrl is Panel || ctrl is TabControl
                || ctrl is TabPage || ctrl is SplitContainer || ctrl is UserControl
                || ctrl is DataGridView || ctrl.Controls.Count > 0)
            {
                ctrl.Enabled = true;
            }
            else if (ctrl is TextBox)
            {
                (ctrl as TextBox).ReadOnly = !enabled;
            }
            else if (ctrl is RichTextBox)
            {
                (ctrl as RichTextBox).ReadOnly = !enabled;
            }
            else if (ctrl is PictureBox && ctrl.Name == "picExpandCollapse")//折叠按钮可用
            {
                ctrl.Enabled = true;
            }
            else
            {
                ctrl.Enabled = enabled;
            }
            //
            foreach (Control c in ctrl.Controls)
            {
                ControlHelper.EnabledControl(c, enabled);
            }
        }

        /// <summary>
        /// 移除控件中所有控件
        /// </summary>
        /// <param name="ctrl"></param>
        public static void RemoveAllControls(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                ctrl.Controls.Remove(c);
            }
        }

        /// <summary>
        /// 给控件增加控件
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="ctrl">子控件</param>
        public static void AddControlToControl(Control parent, Control ctrl)
        {
            ControlHelper.AddControlToControl(parent, ctrl, DockStyle.None);
        }
        /// <summary>
        /// 给控件增加控件
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="ctrl">子控件</param>
        /// <param name="dock">停靠父控件方式</param>
        public static void AddControlToControl(Control parent, Control ctrl, DockStyle dock)
        {
            ControlHelper.RemoveAllControls(parent);// 移除父控件中所有的子控件
            //
            parent.Controls.Add(ctrl);
            if (dock != DockStyle.None)
            {
                ctrl.Dock = dock;
            }
        }

        /// <summary>
        /// 设置子控件在父控件容器中居中显示
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="ctrl"></param>
        public static void AlignCenterControl(Control parent, Control ctrl)
        {
            ctrl.Left = (parent.Width - ctrl.Width) / 2;
            ctrl.Top = (parent.Height - ctrl.Height) / 2;
        }
        /// <summary>
        /// 使窗体居中
        /// </summary>
        /// <param name="frm"></param>
        public static void AlignCenterForm(Form frm)
        {
            // 高度居中
            frm.Top = Math.Max((Screen.PrimaryScreen.WorkingArea.Size.Height - frm.Height) / 2, 0);
            // 宽度居中
            frm.Left = Math.Max((Screen.PrimaryScreen.WorkingArea.Size.Width - frm.Width) / 2, 0);
        }

        /// <summary>
        /// 绑定 ComboBox 控件
        /// </summary>
        /// <param name="cbb">ComboBox控件</param>
        /// <param name="DataSource">数据源</param>
        /// <param name="displayMember">显示的属性</param>
        /// <param name="valueMember">实际值的属性</param>
        public static void BindComboBox(ComboBox cbb, object dataSource, string displayMember, string valueMember)
        {
            cbb.Items.Clear();
            //
            cbb.DisplayMember = displayMember;
            cbb.ValueMember = valueMember;
            cbb.DataSource = dataSource;
        }
        /// <summary>
        /// 绑定 ComboBox 控件
        /// </summary>
        /// <param name="cbb">ComboBox控件</param>
        /// <param name="DataSource">数据源</param>
        public static void BindComboBox(ComboBox cbb, DataSet dataSource)
        {
            if (dataSource != null && dataSource.Tables.Count > 0)
            {
                cbb.Items.Clear();
                //
                cbb.Items.Add("");//
                foreach (DataRow dr in dataSource.Tables[0].Rows)
                {
                    cbb.Items.Add(dr["txt"]);//
                }
            }
        }
        /// <summary>
        /// 绑定 ListBox 控件
        /// </summary>
        /// <param name="lbox"></param>
        /// <param name="dataSource"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        public static void BindListBox(ListBox lbox, object dataSource, string displayMember, string valueMember)
        {
            lbox.Items.Clear();
            //
            lbox.DataSource = dataSource;
            lbox.DisplayMember = displayMember;
            lbox.ValueMember = valueMember;
        }

        /// <summary>
        /// 获取表单的值
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetTextValue(TextBox txt, ref int? value)
        {
            int intValue = 0;
            if (txt.Text.Trim() == string.Empty)
            {
                value = null;
            }
            else if (!int.TryParse(txt.Text.Trim(), out intValue))
            {
                return false;
            }
            else
            {
                value = intValue;
            }
            return true;
        }

        /// <summary>
        /// 检测 combs 是否选中值
        /// </summary>
        /// <param name="combs"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool HasComboBoxSelected(ComboBox[] combs, out string val)
        {
            val = string.Empty;
            foreach (ComboBox cmb in combs)
            {
                if (cmb.SelectedItem == null || cmb.Text == "System.Data.DataRowView") continue;
                if (cmb.Text != ((DataRowView)cmb.SelectedItem)[cmb.DisplayMember] + string.Empty)
                {
                    val = cmb.Text;
                    cmb.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通过绑定值选中ComboBox的项
        /// 按下回车时处理ComboBox的KeyDown事件，通过输入文本查找绑定值并设置选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SelectComboItemByBindValue(object sender, KeyEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string code = cmb.Text;
                    string valueMember = cmb.ValueMember;
                    foreach (DataRowView item in cmb.Items)
                    {
                        if (item[valueMember].ToString() == code)
                        {
                            cmb.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

       /// <summary>
       /// 验证输入ComboBox的值
       /// </summary>
       /// <param name="dataTable">dataTable</param>
       /// <param name="name">绑定的字段名称</param>
        /// <param name="inputText">输入的文字</param>
       /// <returns></returns>
        public static bool ValComboText(DataTable dataTable, string name,string inputText)
        {
            bool falg = false;
            if (inputText == "--请选择--" || inputText == "请选择")
            {
                falg = true;
            }
            //
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (inputText == "--请选择--" || inputText == "请选择")
                {
                    falg = true;
                    break;
                }
                //
                if (inputText == dataTable.Rows[i][name].ToString())
                {
                    falg = true;
                    break;
                }
                else
                {
                    falg = false;
                }

            }
            return falg;
        }

        /// <summary>
        /// 按条件查找子控件。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<Control> FindControl(Control container, Func<Control, bool> func)
        {
            List<Control> controls = new List<Control>();
            foreach (Control ctrl in container.Controls)
            {
                if (func(ctrl))
                {
                    controls.Add(ctrl);
                }
                controls.AddRange(ControlHelper.FindControl(ctrl, func));
            }
            return controls;
        }

        /// <summary>
        /// 对指定的控件以及其子控件调用指定的委托，并指示是否递归调用。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="action"></param>
        /// <param name="foreachChildren">是否递归遍历子控件</param>
        public static void ForeachControl(Control ctrl, Action<Control> action, bool foreachChildren)
        {
            action(ctrl);
            //
            if (foreachChildren)
            {
                foreach (Control c in ctrl.Controls)
                {
                    ControlHelper.ForeachControl(c, action, foreachChildren);
                }
            }
        }

        #region ComboBox 只读

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        public const int GW_CHILD = 5;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public const int EM_SETREADONLY = 0xcf;

        /// <summary>
        /// 获取或设置指定的ComboBox的只读状态。
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="readOnly"></param>
        public static void ComboBoxReadOnly(ComboBox cmb, bool readOnly)
        {
            cmb.DropDownStyle = ComboBoxStyle.DropDown;
            IntPtr editHandle = ControlHelper.GetWindow(cmb.Handle, ControlHelper.GW_CHILD);
            //
            ControlHelper.SendMessage(editHandle, ControlHelper.EM_SETREADONLY, readOnly ? 1 : 0, 0);
        }
        #endregion
    }
}