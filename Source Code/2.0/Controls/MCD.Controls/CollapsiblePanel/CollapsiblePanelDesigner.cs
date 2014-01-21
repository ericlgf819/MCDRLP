using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class CollapsiblePanelDesigner : ParentControlDesigner
    {
        //Properties
        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection collection = new DesignerActionListCollection();
                if (this.Control != null && this.Control is CollapsiblePanel)
                {
                    CollapsiblePanel panel = (CollapsiblePanel)this.Control;
                    if (!String.IsNullOrEmpty(panel.Name))
                    {
                        if (String.IsNullOrEmpty(panel.Title))
                        {
                            panel.Title = panel.Name;
                        }
                    }
                }
                collection.Add(new CollapsiblePanelActionList(this.Control));
                //
                return collection;
            }
        }
    }
}