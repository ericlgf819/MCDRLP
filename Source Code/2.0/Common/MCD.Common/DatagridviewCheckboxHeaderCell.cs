using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DatagridviewCheckboxHeaderCell : DataGridViewColumnHeaderCell
    {
        //Fields
        private CheckBoxState _cbState = CheckBoxState.UncheckedNormal;
        private Point _cellLocation = new Point();
        private bool _checked = false;
        private Point checkBoxLocation;
        private Size checkBoxSize;

        public event DatagridviewcheckboxHeaderEventHander OnCheckBoxClicked;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point point = new Point(e.X + this._cellLocation.X, e.Y + this._cellLocation.Y);
            if ((((point.X >= this.checkBoxLocation.X) && (point.X <= (this.checkBoxLocation.X + this.checkBoxSize.Width))) && 
                (point.Y >= this.checkBoxLocation.Y)) && (point.Y <= (this.checkBoxLocation.Y + this.checkBoxSize.Height)))
            {
                this._checked = !this._checked;
                //
                DatagridviewCheckboxHeaderEventArgs args = new DatagridviewCheckboxHeaderEventArgs();
                args.CheckedState = this._checked;
                object sender = new object();
                if (this.OnCheckBoxClicked != null)
                {
                    this.OnCheckBoxClicked(sender, args);
                    base.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="dataGridViewElementState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            //
            Point point = new Point();
            Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
            point.X = ((cellBounds.Location.X + (cellBounds.Width / 2)) - (glyphSize.Width / 2)) - 1;
            point.Y = (cellBounds.Location.Y + (cellBounds.Height / 2)) - (glyphSize.Height / 2);
            this._cellLocation = cellBounds.Location;
            this.checkBoxLocation = point;
            this.checkBoxSize = glyphSize;
            if (this._checked)
            {
                this._cbState = CheckBoxState.CheckedNormal;
            }
            else
            {
                this._cbState = CheckBoxState.UncheckedNormal;
            }
            CheckBoxRenderer.DrawCheckBox(graphics, this.checkBoxLocation, this._cbState);
        }
    }
}