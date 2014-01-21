using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace MCD.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlCompare : IComparer
    {
        //Fields
        private static ControlCompare instance;
        
        //Properties
        /// <summary>
        /// 对象唯一实例
        /// </summary>
        public static ControlCompare Instance
        {
            get
            {
                if (ControlCompare.instance == null)
                {
                    ControlCompare.instance = new ControlCompare();
                }
                return ControlCompare.instance;
            }
        }

        #region IComparer 成员

        /// <summary>
        /// 比较两个控件的位置大小
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            Control ctrlX = x as Control;
            Control ctrlY = y as Control;
            if (x == null || y == null) return 0;
            //
            if (ctrlX.Top != ctrlY.Top)
            {
                return ctrlX.Top - ctrlY.Top;
            }
            return ctrlX.Left - ctrlY.Left;
        }
        #endregion
    }
}