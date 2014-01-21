using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanceEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "WF_Proc_Inst.xml";
            }
            set { }
        }

        /// <summary>
        /// ProcID
        /// </summary>
        [DataMember]
        public string ProcID { get; set; }

        /// <summary>
        /// AppCode
        /// </summary>
        [DataMember]
        public int AppCode { get; set; }

        /// <summary>
        /// ProcName
        /// </summary>
        [DataMember]
        public string ProcName { get; set; }

        /// <summary>
        /// AppName
        /// </summary>
        [DataMember]
        public string AppName { get; set; }

        /// <summary>
        /// WorkflowID
        /// </summary>
        [DataMember]
        public int WorkflowID { get; set; }

        /// <summary>
        /// WorkflowVersion
        /// </summary>
        [DataMember]
        public string WorkflowVersion { get; set; }

        /// <summary>
        /// StartTime
        /// </summary>
        [DataMember]
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// EndTime
        /// </summary>
        [DataMember]
        public System.DateTime EndTime { get; set; }

        /// <summary>
        /// ImpoLevel
        /// </summary>
        [DataMember]
        public int ImpoLevel { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        [DataMember]
        public int Secret { get; set; }

        /// <summary>
        /// FlowTag
        /// </summary>
        [DataMember]
        public int FlowTag { get; set; }

        /// <summary>
        /// CreatorID
        /// </summary>
        [DataMember]
        public string CreatorID { get; set; }

        /// <summary>
        /// CreatorName
        /// </summary>
        [DataMember]
        public string CreatorName { get; set; }

        /// <summary>
        /// CreatorDeptID
        /// </summary>
        [DataMember]
        public string CreatorDeptID { get; set; }

        /// <summary>
        /// CreatorDeptName
        /// </summary>
        [DataMember]
        public string CreatorDeptName { get; set; }

        /// <summary>
        /// AssigneeID
        /// </summary>
        [DataMember]
        public string AssigneeID { get; set; }

        /// <summary>
        /// AssigneeName
        /// </summary>
        [DataMember]
        public string AssigneeName { get; set; }

        /// <summary>
        /// AssigneeDeptID
        /// </summary>
        [DataMember]
        public string AssigneeDeptID { get; set; }

        /// <summary>
        /// AssigneeDeptName
        /// </summary>
        [DataMember]
        public string AssigneeDeptName { get; set; }

        /// <summary>
        /// AlertCount
        /// </summary>
        [DataMember]
        public int AlertCount { get; set; }

        /// <summary>
        /// DataLocator
        /// </summary>
        [DataMember]
        public string DataLocator { get; set; }

        /// <summary>
        /// WorkflowNewSeq
        /// </summary>
        [DataMember]
        public int WorkflowNewSeq { get; set; }

        /// <summary>
        /// WorkflowNewTask
        /// </summary>
        [DataMember]
        public int WorkflowNewTask { get; set; }

        /// <summary>
        /// CurrentActi
        /// </summary>
        [DataMember]
        public string CurrentActi { get; set; }

        /// <summary>
        /// CurrentParticipant
        /// </summary>
        [DataMember]
        public string CurrentParticipant { get; set; }

        /// <summary>
        /// RelateItems
        /// </summary>
        [DataMember]
        public string RelateItems { get; set; }

        /// <summary>
        /// Memo
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
    }
}