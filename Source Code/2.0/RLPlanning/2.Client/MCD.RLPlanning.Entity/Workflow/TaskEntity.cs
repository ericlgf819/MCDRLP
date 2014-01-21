using System;
using System.Runtime.Serialization;

using MCD.Framework.Entity;

namespace MCD.RLPlanning.Entity.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskEntity : BaseEntity
    {
        [DataMember]
        public override string ConfigFileName
        {
            get
            {
                return "WF_Work_Items.xml";
            }
            set { }
        }

        /// <summary>
        /// ProcID
        /// </summary>
        [DataMember]
        public string ProcID { get; set; }

        /// <summary>
        /// TaskID
        /// </summary>
        [DataMember]
        public int TaskID { get; set; }

        /// <summary>
        /// AppCode
        /// </summary>
        [DataMember]
        public int AppCode { get; set; }

        /// <summary>
        /// TaskName
        /// </summary>
        [DataMember]
        public string TaskName { get; set; }

        /// <summary>
        /// TaskSeq
        /// </summary>
        [DataMember]
        public int TaskSeq { get; set; }

        /// <summary>
        /// LevelCode
        /// </summary>
        [DataMember]
        public string LevelCode { get; set; }

        /// <summary>
        /// PartRank
        /// </summary>
        [DataMember]
        public string PartRank { get; set; }

        /// <summary>
        /// PartID
        /// </summary>
        [DataMember]
        public string PartID { get; set; }

        /// <summary>
        /// PartName
        /// </summary>
        [DataMember]
        public string PartName { get; set; }

        /// <summary>
        /// PartDeptID
        /// </summary>
        [DataMember]
        public string PartDeptID { get; set; }

        /// <summary>
        /// PartDeptName
        /// </summary>
        [DataMember]
        public string PartDeptName { get; set; }

        /// <summary>
        /// ParticipantType
        /// </summary>
        [DataMember]
        public int ParticipantType { get; set; }

        /// <summary>
        /// TaskBase
        /// </summary>
        [DataMember]
        public string TaskBase { get; set; }

        /// <summary>
        /// RoleID
        /// </summary>
        [DataMember]
        public string RoleID { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>
        [DataMember]
        public string RoleName { get; set; }

        /// <summary>
        /// ReceTime
        /// </summary>
        [DataMember]
        public System.DateTime ReceTime { get; set; }

        /// <summary>
        /// AcceptTime
        /// </summary>
        [DataMember]
        public System.DateTime AcceptTime { get; set; }

        /// <summary>
        /// ReadTime
        /// </summary>
        [DataMember]
        public System.DateTime ReadTime { get; set; }

        /// <summary>
        /// PauseTime
        /// </summary>
        [DataMember]
        public System.DateTime PauseTime { get; set; }

        /// <summary>
        /// AlertTime
        /// </summary>
        [DataMember]
        public System.DateTime AlertTime { get; set; }

        /// <summary>
        /// FinishTime
        /// </summary>
        [DataMember]
        public System.DateTime FinishTime { get; set; }

        /// <summary>
        /// ExpiredTime
        /// </summary>
        [DataMember]
        public System.DateTime ExpiredTime { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        [DataMember]
        public int Interval { get; set; }

        /// <summary>
        /// AutoFinish
        /// </summary>
        [DataMember]
        public bool AutoFinish { get; set; }

        /// <summary>
        /// TaskStat
        /// </summary>
        [DataMember]
        public int TaskStat { get; set; }

        /// <summary>
        /// UserChoice
        /// </summary>
        [DataMember]
        public string UserChoice { get; set; }

        /// <summary>
        /// PartComment
        /// </summary>
        [DataMember]
        public string PartComment { get; set; }

        /// <summary>
        /// OpinionType
        /// </summary>
        [DataMember]
        public int OpinionType { get; set; }

        /// <summary>
        /// 代理人ID
        /// </summary>
        [DataMember]
        public string AssigneeID { get; set; }

        /// <summary>
        /// 代理人名称
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
        /// AssigneeRank
        /// </summary>
        [DataMember]
        public string AssigneeRank { get; set; }

        /// <summary>
        /// CurrentActi
        /// </summary>
        [DataMember]
        public string CurrentActi { get; set; }

        /// <summary>
        /// EnableCalculate
        /// </summary>
        [DataMember]
        public bool EnableCalculate { get; set; }

        /// <summary>
        /// ReseVal1
        /// </summary>
        [DataMember]
        public int IsReject { get; set; }

        /// <summary>
        /// ReseVal2
        /// </summary>
        [DataMember]
        public decimal ReseVal2 { get; set; }

        /// <summary>
        /// ReseVal3
        /// </summary>
        [DataMember]
        public string RejectType { get; set; }

        /// <summary>
        /// ReseVal4
        /// </summary>
        [DataMember]
        public string ReseVal4 { get; set; }

        /// <summary>
        /// CorTaskID
        /// </summary>
        [DataMember]
        public int CorTaskID { get; set; }

        /// <summary>
        /// KeyLabel
        /// </summary>
        [DataMember]
        public string KeyLabel { get; set; }

        /// <summary>
        /// KeyInfo
        /// </summary>
        [DataMember]
        public string KeyInfo { get; set; }

        /// <summary>
        /// NotePaper
        /// </summary>
        [DataMember]
        public string NotePaper { get; set; }

        /// <summary>
        /// PasserID
        /// </summary>
        [DataMember]
        public string PasserID { get; set; }

        /// <summary>
        /// PasserName
        /// </summary>
        [DataMember]
        public string PasserName { get; set; }

        /// <summary>
        /// DealTimeSpan
        /// </summary>
        [DataMember]
        public int DealTimeSpan { get; set; }
    }
}