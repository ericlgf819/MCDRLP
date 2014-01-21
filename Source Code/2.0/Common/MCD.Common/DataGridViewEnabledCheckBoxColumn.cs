using System;
using System.Windows.Forms;

namespace MCD.Common
{
    /// <summary>
    /// DataGridView可以启用或禁用的CheckBox列。
    /// </summary>
    public class DataGridViewEnabledCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        #region ctor

        public DataGridViewEnabledCheckBoxColumn()
        {
            base.CellTemplate = new DataGridViewEnabledCheckBoxCell();
        }
        #endregion
    }
}