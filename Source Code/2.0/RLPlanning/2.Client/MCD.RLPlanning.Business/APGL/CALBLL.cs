using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.RLPlanning.IServices.APGL;

namespace MCD.RLPlanning.BLL.APGL
{
    /// <summary>
    /// 
    /// </summary>
    public class CALBLL : BaseBLL<ICALService>
    {
        /// <summary>
        /// 检查小周期百分比公式是否正确
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public bool CheckFormulaStringSmallCycle(string formula)
        {
            return base.WCFService.CheckFormulaStringSmallCycle(formula);
        }

        /// <summary>
        /// 检查大周期百分比公式是否正确
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public bool CheckFormulaStringBigCycle(string formula)
        {
            return base.WCFService.CheckFormulaStringBigCycle(formula);
        }

        /// <summary>
        /// 计算四则表达式的值,可以支持MAX
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public decimal CalFormula(string formula)
        {
            return base.WCFService.CalFormula(formula);
        }

        /// <summary>
        /// 检查SQL解释是否正确
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool CheckSQLCondition(string condition)
        {
            return base.WCFService.CheckSQLCondition(condition);
        }

        /// <summary>
        /// 获取凭证文件名称的流水号。
        /// </summary>
        /// <returns></returns>
        public string GetCertificateFlowNumber()
        {
            return base.WCFService.GetCertificateFlowNumber();
        }

        /// <summary>
        /// 重写计算指定流程ID的AP GL流程，多个流程ID以";"分隔。
        /// </summary>
        /// <param name="procIDList">以";"分隔的流程ID</param>
        /// <param name="totalCount">总条数</param>
        /// <param name="successCount">计算成功的条数</param>
        /// <param name="failCount">计算失败的条数</param>
        /// <param name="successProcIDList">计算成功的ProcID字符串</param>
        public void ReCaculateAPGL(string procIDList, out int totalCount, out int successCount, out int failCount, out string successProcIDList)
        {
            base.WCFService.ReCaculateAPGL(procIDList, out totalCount, out successCount, out failCount, out successProcIDList);
        }

        /// <summary>
        /// 重写计算指定流程ID的AP GL流程并返回计算是否成功。
        /// </summary>
        /// <param name="procID">待计算的流程</param>
        /// <returns></returns>
        public bool ReCaculateSingleAPGL(string procID)
        {
            return base.WCFService.ReCaculateSingleAPGL(procID);
        }

        /// <summary>
        /// 按指定的场景来计算指定的直线GL。
        /// stage:1-场景一，2-场景二，3-场景三，4-最后一个月特殊情况
        /// </summary>
        /// <param name="glRecordID"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public bool ReCaculateSingleZXGLByStage(string glRecordID, int stage)
        {
            return base.WCFService.ReCaculateSingleZXGLByStage(glRecordID, stage);
        }

        /// <summary>
        /// 获取指定的一个AP、GL流程生成凭时证时所用到的数据（表0：凭证表；表1：计算结果；表2：计算过程公式及结果；表3：审核人、审批人、条码；表4：待办任务列表）
        /// </summary>
        /// <param name="procIDList">流程ID字符序列，多个之间用分号";"隔开</param>
        /// <param name="type">AP或者GL</param>
        /// <returns>返回DataSet，包含5个表，表0：凭证表；表1：计算结果；表2：计算过程公式及结果；表3：审核人、审批人、条码；表4：待办任务列表</returns>
        public DataSet GetCertificateAllData(string procIDList, string type)
        {
            return base.WCFService.GetCertificateAllData(procIDList, type);
        }

        public bool RecaculateAPGLAndRunworkflow(string procID, string userChoice, string partComment, string rejectTypeName, string currentUserID, string nextUserIDList)
        {
            return base.WCFService.RecaculateAPGLAndRunworkflow(procID, userChoice, partComment, rejectTypeName, currentUserID, nextUserIDList);
        }
    }
}