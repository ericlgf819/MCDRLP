using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get;
            set;
        }
    }
}