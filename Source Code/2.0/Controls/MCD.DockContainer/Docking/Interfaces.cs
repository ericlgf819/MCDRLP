using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCD.DockContainer.Docking
{
    /// <summary>
    /// 
    /// </summary>
	public interface IDockContent
    {
        //Properties
        /// <summary>
        /// 
        /// </summary>
        DockContentHandler DockHandler { get; }
	}

    /// <summary>
    /// 
    /// </summary>
	public interface INestedPanesContainer
    {
        //Properties
        /// <summary>
        /// 
        /// </summary>
        DockState DockState { get; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle DisplayingRectangle { get; }

        /// <summary>
        /// 
        /// </summary>
        NestedPaneCollection NestedPanes { get; }

        /// <summary>
        /// 
        /// </summary>
        VisibleNestedPaneCollection VisibleNestedPanes { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFloat { get; }
	}

    /// <summary>
    /// 
    /// </summary>
    internal interface IDragSource
    {
        //Properties
        /// <summary>
        /// 
        /// </summary>
        Control DragControl { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal interface IDockDragSource : IDragSource
    {
        //Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptMouse"></param>
        /// <returns></returns>
        Rectangle BeginDrag(Point ptMouse);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dockState"></param>
        /// <returns></returns>
        bool IsDockStateValid(DockState dockState);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pane"></param>
        /// <returns></returns>
        bool CanDockTo(DockPane pane);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="floatWindowBounds"></param>
        void FloatAt(Rectangle floatWindowBounds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pane"></param>
        /// <param name="dockStyle"></param>
        /// <param name="contentIndex"></param>
        void DockTo(DockPane pane, DockStyle dockStyle, int contentIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="dockStyle"></param>
        void DockTo(DockPanel panel, DockStyle dockStyle);
    }

    /// <summary>
    /// 
    /// </summary>
    internal interface ISplitterDragSource : IDragSource
    {
        //Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectSplitter"></param>
        void BeginDrag(Rectangle rectSplitter);

        /// <summary>
        /// 
        /// </summary>
        void EndDrag();

        /// <summary>
        /// 
        /// </summary>
        bool IsVertical { get; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle DragLimitBounds { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        void MoveSplitter(int offset);
    }
}