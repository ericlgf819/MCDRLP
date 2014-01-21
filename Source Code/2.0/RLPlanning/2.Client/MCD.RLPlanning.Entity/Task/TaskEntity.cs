using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Task
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskEntity : BaseEntity
    {
        public override string ConfigFileName
        {
            get
            {
                return string.Empty;
            }
            set
            {
                ;
            }
        }
    }
}