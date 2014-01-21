using System;
using System.Runtime.Serialization;

namespace MCD.Framework.DALCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class EnvironmentVariables
    {
        #region IEnvironmentVariables 成员

        /// <summary>
        /// 登录当前系统的用户 ID
        /// </summary>
        [DataMember]
        public static Guid UserID { get; set; }

        /// <summary>
        /// 登录当前系统的英文名
        /// </summary>
        [DataMember]
        public static string EnglishName { get; set; }
        #endregion
    }
}