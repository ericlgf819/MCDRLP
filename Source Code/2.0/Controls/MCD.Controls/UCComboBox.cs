using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MCD.Controls
{
    /// <summary>
    /// Summary description for ACComboBox.
    /// </summary>
    public class UCComboBox : System.Windows.Forms.ComboBox
    {
        //Fields
        private bool autoComplete;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        //Properties
        [DefaultValue(true),
        Description("Auto-completes text if a match is found in the items collection."),
        Category("Behavior")]
        public bool AutoComplete
        {
            get { return this.autoComplete; }
            set { this.autoComplete = value; }
        }
        #region ctor

        public UCComboBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // Add any initialization after the InitComponent call
            this.autoComplete = true;
            base.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
            base.TextChanged += new EventHandler(this.UCComboBox_TextChanged);
        }
        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
        #endregion

        //Events
        void UCComboBox_TextChanged(object sender, EventArgs e)
        {
            if (base.DisplayMember == string.Empty || base.ValueMember == string.Empty) return;
            //
            try
            {
                foreach (var item in this.Items)
                {
                    if (((DataRowView)item)[base.DisplayMember] + string.Empty == base.Text)
                    {
                        base.SelectedValue = ((DataRowView)item)[base.ValueMember];
                        break;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.AutoComplete)
            {
                UCComboBox acComboBox = (UCComboBox)sender;
                if (!e.KeyChar.Equals((char)8))
                {
                    acComboBox.DroppedDown = true;
                    //
                    this.SearchItems(acComboBox, ref e);
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Searches the combo box item list for a match and selects it.
        /// If no match is found, then selected index defaults to -1.
        /// </summary>
        /// <param name="acComboBox"></param>
        /// <param name="e"></param>
        private void SearchItems(UCComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            //这里确保截取安全
            selectionStart = selectionStart < 0 ? 0 : selectionStart;//如果小于0当0
            selectionStart = selectionStart > acComboBox.Text.Trim().Length ? acComboBox.Text.Trim().Length : selectionStart;//如果大于最大长度当最大长度
            selectionEnd = selectionEnd < 0 ? 0 : selectionEnd;//如果小于0当0
            selectionEnd = selectionEnd > acComboBox.Text.Trim().Length ? acComboBox.Text.Trim().Length : selectionEnd;//如果大于最大长度当最大长度

            sb.Append(acComboBox.Text.Trim().Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Trim().Substring(selectionEnd < 0 ? 0 : selectionEnd));
            index = acComboBox.FindString(sb.ToString());
            if (index == -1)
            {
                e.Handled = false;
            }
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                //
                e.Handled = true;
            }
        }
    }
}