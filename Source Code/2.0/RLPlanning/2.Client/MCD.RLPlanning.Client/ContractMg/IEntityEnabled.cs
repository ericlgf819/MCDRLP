using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.RLPlanning.Client.ContractMg
{
    /// <summary>
    /// 是否启用当前实体。
    /// </summary>
    public interface IEntityEnabled
    {
        /// <summary>
        /// 获取或设置是否启用当前实体。
        /// </summary>
        bool EntityEnabled { get; set; }
    }
}
