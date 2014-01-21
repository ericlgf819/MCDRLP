using System;
using MCD.DockContainer.Docking;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFormHandler
    {
        /// <summary>
        /// 检查窗体是否已经打开，如果打开，返回已经打开的窗体
        /// </summary>
        /// <param name="checkContent"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        DockContent ExistDockContent(DockContent checkContent, DockPanel panel);

        void OpenForm(string name, DockPanel panel);
        void CloseForm(string classType, DockPanel panel);

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="classType">命名空间.类名</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="panel">DockPanel</param>
        void OpenForm(string classType, string displayName, DockPanel panel);

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="displayName"></param>
        /// <param name="panel"></param>
        /// <param name="method"></param>
        void OpenForm(string classType, string displayName, DockPanel panel, FORM_OPEN method);

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sender"></param>
        void MenuClick(string name, object sender);
    }
}