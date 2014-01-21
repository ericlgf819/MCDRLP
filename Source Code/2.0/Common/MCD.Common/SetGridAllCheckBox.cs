using System;
using System.Collections;
using System.Windows.Forms;
    
namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SetGridAllCheckBox
    {
        //Fields
        private int colums;
        private DataGridView grid;
        private int topCount = int.MaxValue;//默认只选中前100条
        public CustomCallBack cb = null;

        #region ctor

        public SetGridAllCheckBox(DataGridView grid, int colums)
        {
            this.grid = grid;
            this.colums = colums;
        }
        public SetGridAllCheckBox(DataGridView grid, int colums, int maxCount)
        {
            this.grid = grid;
            this.colums = colums;
            this.topCount = int.MaxValue;//maxCount
        }
        #endregion

        //Events
        private void ch_OnCheckBoxClicked(object sender, DatagridviewCheckboxHeaderEventArgs e)
        {
            this.grid.EndEdit();
            //
            try
            {
                int index = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.grid.Rows)
                {
                    if (index < this.topCount)
                    {
                        row.Cells[this.colums].Value = e.CheckedState;
                    }
                    index++;
                }

                //added by Eric -- Begin
                if (null != cb)
                    cb();
                //added by Eric -- End
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //Methods
        public void SetSelectAllCheckBox()
        {
            try
            {
                DatagridviewCheckboxHeaderCell cell = new DatagridviewCheckboxHeaderCell();
                cell.OnCheckBoxClicked += new DatagridviewcheckboxHeaderEventHander(this.ch_OnCheckBoxClicked);
                //
                DataGridViewCheckBoxColumn column = this.grid.Columns[this.colums] as DataGridViewCheckBoxColumn;
                column.HeaderCell = cell;
                column.HeaderCell.Value = string.Empty;
            }
            catch (Exception)
            {
                //
            }
        }
    }
}