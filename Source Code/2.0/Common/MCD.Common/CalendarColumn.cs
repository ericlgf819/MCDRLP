using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCD.Common
{
    /// <summary>
    /// DataGridView中使用日期控件
    /// </summary>
    public class CalendarColumn : DataGridViewColumn
    {
        //Properties
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
        #region ctor

        public CalendarColumn(): base(new CalendarCell())
        {
            //
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class CalendarCell : DataGridViewTextBoxCell
    {
        //Properties
        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }
        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
        #region ctor

        public CalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }
        #endregion

        //Methods
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            //
            CalendarEditingControl ctl = DataGridView.EditingControl as CalendarEditingControl;
            try
            {
                ctl.Value = (DateTime)this.Value;
            }
            catch { }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        //Fields
        int rowIndex;
        DataGridView dataGridView;
        private bool valueChanged = false;

        //Properties
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingControlRowIndex property.
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.RepositionEditingControlOnValueChange property.
        /// </summary>
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingControlDataGridView property.
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingControlValueChanged property.
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return this.valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingPanelCursor property.
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingControlFormattedValue property.
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    this.Value = DateTime.Parse((String)value);
                }
            }
        }
        #region ctor

        public CalendarEditingControl()
        {
            base.Format = DateTimePickerFormat.Short;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell have changed.
            this.EditingControlValueChanged = true;
            //
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }

        /// <summary>
        /// Implements the IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.Font = dataGridViewCellStyle.Font;
            base.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            base.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.EditingControlWantsInputKey method.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }
        /// <summary>
        /// Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit method.
        /// </summary>
        /// <param name="selectAll"></param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }
    }
}