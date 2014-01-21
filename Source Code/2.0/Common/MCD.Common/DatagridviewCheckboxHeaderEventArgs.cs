using System;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DatagridviewCheckboxHeaderEventArgs : EventArgs
    {
        //Fields
        private bool checkedState = false;

        //Properties
        public bool CheckedState
        {
            get
            {
                return this.checkedState;
            }
            set
            {
                this.checkedState = value;
            }
        }
    }
}