using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapsiblePanelActionList : DesignerActionList
    {
        //Properties
        public string Title
        {
            get
            {
                return ((CollapsiblePanel)this.Component).Title;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["Title"];
                property.SetValue(this.Component, value);
            }
        }

        public bool UseAnimation
        {
            get
            {
                return ((CollapsiblePanel)this.Component).UseAnimation;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["UseAnimation"];
                property.SetValue(this.Component, value);
            }
        }

        public bool Collapsed
        {
            get
            {
                return ((CollapsiblePanel)this.Component).Collapse;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["Collapse"];
                property.SetValue(this.Component, value);
            }
        }

        public bool ShowSeparator
        {
            get
            {
                return ((CollapsiblePanel)this.Component).ShowHeaderSeparator;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["ShowHeaderSeparator"];
                property.SetValue(this.Component, value);
            }
        }

        public bool UseRoundedCorner
        {
            get
            {
                return ((CollapsiblePanel)this.Component).RoundedCorners;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["RoundedCorners"];
                property.SetValue(this.Component, value);
            }
        }

        public int HeaderCornersRadius
        {
            get
            {
                return ((CollapsiblePanel)this.Component).HeaderCornersRadius;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["HeaderCornersRadius"];
                property.SetValue(this.Component, value);
            }
        }

        public Image HeaderImage
        {
            get
            {
                return ((CollapsiblePanel)this.Component).HeaderImage;
            }
            set
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(this.Component)["HeaderImage"];
                property.SetValue(this.Component, value);
            }
        }
        #region ctor

        public CollapsiblePanelActionList(IComponent component)
            : base(component)
        {

        }
        #endregion

        //Methods
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionHeaderItem("Header Parameters"));
            items.Add(new DesignerActionPropertyItem("Title", "标题"));
            items.Add(new DesignerActionPropertyItem("HeaderImage", "标题栏图标"));
            items.Add(new DesignerActionPropertyItem("UseAnimation", "动态折叠/展开"));
            items.Add(new DesignerActionPropertyItem("Collapsed", "折叠"));
            items.Add(new DesignerActionPropertyItem("ShowSeparator", "显示分割线"));
            items.Add(new DesignerActionPropertyItem("UseRoundedCorner", "标题栏显示圆角"));
            items.Add(new DesignerActionPropertyItem("HeaderCornersRadius", "圆角弧度[1,15]"));
            //
            return items;
        }
    }
}