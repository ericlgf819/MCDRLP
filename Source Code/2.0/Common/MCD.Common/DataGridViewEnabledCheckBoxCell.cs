using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MCD.Common
{
    /// <summary>
    /// DataGridView可以启用或禁用的CheckBox单元格。
    /// </summary>
    public class DataGridViewEnabledCheckBoxCell : DataGridViewCheckBoxCell
    {
        //Fields
        private bool enabledValue;
        
        //Properties
        /// <summary>
        /// 获取或设置是否启用该CheckBox。
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabledValue;
            }
            set
            {
                this.enabledValue = value;
            }
        }
        #region ctor

        // By default, enable the button cell.
        public DataGridViewEnabledCheckBoxCell()
        {
            this.enabledValue = true;
        }
        #endregion

        //Methods
        /// <summary>
        /// Override the Clone method so that the Enabled property is copied.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DataGridViewEnabledCheckBoxCell cell = (DataGridViewEnabledCheckBoxCell)base.Clone();
            cell.Enabled = this.Enabled;
            //
            return cell;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="elementState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (!this.enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    //
                    cellBackground.Dispose();
                }
                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }
                // Paint background
                graphics.FillRectangle(Brushes.White, cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height -1);

                // draw a inactive checkbox
                //ControlPaint.DrawCheckBox(graphics,
                //    cellBounds.X + this.ContentBounds.X - 2,
                //    cellBounds.Y + this.ContentBounds.Y,
                //    16, 16,
                //    ButtonState.All);
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}