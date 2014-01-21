using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

namespace MCD.RLPlanning.IServices.APGL
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICALService : IBaseService
    {
        /// <summary>
        /// 检查小周期百分比公式是否正确
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckFormulaStringSmallCycle(string formula);

        /// <summary>
        /// 检查大周期百分比公式是否正确
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckFormulaStringBigCycle(string formula);

        /// <summary>
        /// 计算四则表达式的值,可以支持MAX
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        [OperationContract]
        decimal CalFormula(string formula);

        /// <summary>
        /// 检查SQL解释是否正确
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckSQLCondition(string condition);

        [OperationContract]
        string GetCertificateFlowNumber();

        [OperationContract]
        bool ReCaculateSingleAPGL(string procID);

        /// <summary>
        /// 按指定的场景来计算直线GL
        /// </summary>
        /// <param name="procID"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        [OperationContract]
        bool ReCaculateSingleZXGLByStage(string glRecordID, int stage);

        [OperationContract]
        void ReCaculateAPGL(string procIDList, out int totalCount, out int successCount, out int failCount, out string successProcIDList);

        /// <summary>
        /// 获取指定的一个AP、GL流程生成凭时证时所用到的数据（表0：凭证表；表1：计算结果；表2：计算过程公式及结果；表3：审核人、审批人、条码；表4：待办任务列表）
        /// </summary>
        /// <param name="procIDList"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetCertificateAllData(string procIDList, string type);

        [OperationContract]
        bool RecaculateAPGLAndRunworkflow(string procID, string userChoice, string partComment, string rejectTypeName, string currentUserID, string nextUserIDList);
    }
}