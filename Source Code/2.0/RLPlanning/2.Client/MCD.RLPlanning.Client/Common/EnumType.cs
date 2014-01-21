using System;

namespace MCD.RLPlanning.Client.Common
{
    /// <summary>
    /// 编辑类型,目前只用于附件
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 
        /// </summary>
        New = 0,
        /// <summary>
        /// 
        /// </summary>
        Edit,
        /// <summary>
        /// 
        /// </summary>
        View
    }

    /// <summary>
    /// 语言版本
    /// </summary>
    public enum LANGUAGES
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        SimpleChinese = 0,
        /// <summary>
        /// 繁体中文
        /// </summary>
        TraditionalChinese,
        /// <summary>
        /// 英文
        /// </summary>
        English,
        /// <summary>
        /// 空值
        /// </summary>
        NONE
    }
}