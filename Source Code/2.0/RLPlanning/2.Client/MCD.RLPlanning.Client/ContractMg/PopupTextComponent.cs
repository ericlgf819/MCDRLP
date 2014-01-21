using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MCD.RLPlanning.Client.ContractMg
{
    public partial class PopupTextComponent : Component
    {
        public PopupTextComponent()
        {
            InitializeComponent();
        }

        public PopupTextComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        private PopupTextForm m_PopupTextForm;
        [Browsable(false)]
        private PopupTextForm PopupTextForm
        {
            get
            {
                if (this.m_PopupTextForm == null || this.m_PopupTextForm.IsDisposed)
                {
                    this.PopupTextForm = new PopupTextForm();
                }
                return this.m_PopupTextForm;
            }
            set
            {
                if (this.m_PopupTextForm != value)
                {
                    //添加事件处理方法
                    this.HandlePopupTextFormEvent(this.m_PopupTextForm, false);
                    this.m_PopupTextForm = value;
                    this.HandlePopupTextFormEvent(this.m_PopupTextForm, true);
                }
            }
        }


        private TextBox m_TargetTextBox;
        [Description("目标文本框，该文本框将具备双击弹出文本窗体的行为。")]
        public TextBox TargetTextBox
        {
            get
            {
                return this.m_TargetTextBox;
            }
            set
            {
                if (this.m_TargetTextBox != value)
                {
                    this.HandleTextBoxEvent(this.m_TargetTextBox, false);
                    this.m_TargetTextBox = value;
                    this.HandleTextBoxEvent(this.m_TargetTextBox, true);
                }
            }
        }


        private void HandlePopupTextFormEvent(PopupTextForm form, bool handleFlag)
        {
            //throw new NotImplementedException();
        }


        private void HandleTextBoxEvent(TextBox textBox, bool handleFlag)
        {
            if (!base.DesignMode)
            {
                if (textBox != null && !textBox.IsDisposed)
                {
                    if (handleFlag)
                    {
                        textBox.MouseDoubleClick += new MouseEventHandler(textBox_MouseDoubleClick);

                    }
                    else
                    {
                        textBox.MouseDoubleClick -= new MouseEventHandler(textBox_MouseDoubleClick);
                    }
                }
            }
        }

        void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.PopupTextForm.Content = this.TargetTextBox.Text;
            
            Point location = new Point();
            Rectangle bound = this.TargetTextBox.Parent.RectangleToScreen(this.TargetTextBox.Bounds);
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            if (bound.X + this.PopupTextForm.Width > workingArea.Left + workingArea.Width)
            {
                location.X = bound.X + bound.Width - this.PopupTextForm.Width;
            }
            else
            {
                location.X = bound.X;
            }

            if (bound.Y + this.PopupTextForm.Height > workingArea.Top + workingArea.Height)
            {
                location.Y = bound.Y - this.PopupTextForm.Height;
            }
            else
            {
                location.Y = bound.Y + bound.Height;
            }

            this.PopupTextForm.Location = location;
            this.PopupTextForm.ReadOnly = (this.TargetTextBox.ReadOnly || !this.TargetTextBox.Enabled);

            if (this.PopupTextForm.ShowDialog() == DialogResult.OK)
            {
                if (this.TargetTextBox != null)
                {
                    this.TargetTextBox.Text = this.PopupTextForm.Content.Replace("\r\n","");
                }
            }
        }


    }
}
