using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCD.Common;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 定义了点击规则右侧的启用、禁用按钮时当对规则的添加、删除操作。
    /// </summary>
    public interface IRentRule
    {
        /// <summary>
        /// 禁用规则。
        /// </summary>
        /// <returns></returns>
        bool Disable();

        /// <summary>
        /// 启用规则。
        /// </summary>
        /// <returns></returns>
        bool Enable();

        /// <summary>
        /// 获取或设置当前规则是否启用。
        /// </summary>
        /// <returns></returns>
        bool IsEnable { get; set; }
    }
}
