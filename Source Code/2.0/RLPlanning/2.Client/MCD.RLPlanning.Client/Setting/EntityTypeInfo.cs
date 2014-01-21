using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MCD.Common;
using MCD.RLPlanning.BLL.Setting;

namespace MCD.RLPlanning.Client.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EntityTypeInfo : BaseList
    {
        //Fields
        private EntityTypeBLL EntityTypeBLL = new EntityTypeBLL();
        #region ctor

        public EntityTypeInfo()
        {
            InitializeComponent();
        }
        #endregion

        //Events
        /// <summary>
        /// 窗体关闭时，消亡 WCF 对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.EntityTypeBLL.Dispose();
            //
            base.BaseFrm_FormClosing(sender, e);
        }
        
        //Methods
        protected override void BindFormControl()
        {
            base.btnReset.Visible = false;
        }
        protected override void BindGridList()
        {
            //实体类型表 -- 同步表,可缓存
            DataTable dtValue = base.GetDataTable("EntityType", () => {
                return this.EntityTypeBLL.SelectEntityType();
            });
            base.dgvList.DataSource = dtValue;
            //
            base.dgvList.Columns.Clear();
            base.dgvList.AutoGenerateColumns = false;
            base.dgvList.AllowUserToAddRows = false;//
            base.dgvList.AllowUserToOrderColumns = false;
            base.dgvList.ReadOnly = true;//
            GridViewHelper.PaintRowIndexToHeaderCell(base.dgvList);
            GridViewHelper.AppendColumnToDataGridView(base.dgvList, "val", base.GetMessage("EntityTypeName"), 200);
         }
    }
}