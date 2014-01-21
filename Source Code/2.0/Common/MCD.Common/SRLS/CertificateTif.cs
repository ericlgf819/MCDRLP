using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 提供根据AP、GL凭证生成tif影像文件的方法。
    /// </summary>
    public class CertificateTif
    {
        //Fields
        private readonly int imgWidth = 910;//生成图片的宽度
        private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"\temp\";//根目录
        private readonly string tPath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\T\";//PaymentType为T的文件路径
        private readonly string mPath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\M\";//PaymentType为M的文件路径

        //Properties
        /// <summary>
        /// 获取或设置生成的凭证类型。
        /// </summary>
        public CaculateType CaculateType { get; set; }
        /// <summary>
        /// 获取或设置返回文件名流水号的方法。
        /// </summary>
        public Func<string> FuncFlowNumber { get; set; }
        /// <summary>
        /// 获取或设置生成凭证用到的所有数据(包含5个表：表0：凭证表；表1：计算结果；表2：计算过程公式及结果；表3：审核人、审批人、条码；表4：待办任务列表；表5：流程步骤提交时间表)。
        /// </summary>
        public DataSet DataSource { get; set; }

        #region 生成凭证

        /// <summary>
        /// 表示一个GL凭证文件的类型。
        /// </summary>
        internal class GLCertificateEntity
        {
            public string CompanyCode { get; set; }
            public string RentType { get; set; }
            public string GLType { get; set; }
        }

        /// <summary>
        /// 根据PaymentType生成不同类型的Tif文件。
        /// </summary>
        /// <param name="fileName"></param>
        public void CreateTif()
        {
            if (this.DataSource == null)
            {
                return;
            }
            //
            if (this.CaculateType == CaculateType.AP)
            {
                this.WriteAPTif();
            }
            else
            {
                //同一个公司同一种租金类型生成一个Tif文件
                List<GLCertificateEntity> list = new List<GLCertificateEntity>();
                foreach (DataRow dr in this.DataSource.Tables[0].Rows)
                {
                    string glType = dr["GLType"] == DBNull.Value ? string.Empty : dr["GLType"].ToString();
                    if (!list.Exists(item => item.GLType.Equals(glType)))
                    {
                        string companyCode = dr["CompanyCode"] == DBNull.Value ? string.Empty : dr["CompanyCode"].ToString();
                        string rentType = dr["RentType"] == DBNull.Value ? string.Empty : dr["RentType"].ToString();
                        //
                        list.Add(new GLCertificateEntity() { CompanyCode = companyCode, RentType = rentType, GLType = glType });
                    }
                }
                //
                foreach (GLCertificateEntity entity in list)
                {
                    this.WriteGLTif(entity.CompanyCode, entity.RentType, entity.GLType);
                }
            }
        }

        private void WriteAPTif()
        {
            string fileName = null;
            ImageWriter img = null;
            try
            {
                DataRow[] tasks = this.DataSource.Tables[4].Select(null, "CompanyCode DESC,StoreOrDeptNo ASC,APAmount ASC");
                if (tasks == null || tasks.Length <= 0)
                {
                    return;
                }
                //计算结果基础信息
                DataTable dtBaseSource = new DataTable();
                dtBaseSource.Columns.Add("公司编号");
                dtBaseSource.Columns.Add("vendor编号");
                dtBaseSource.Columns.Add("vendor名称");
                dtBaseSource.Columns.Add("Payment Type");
                dtBaseSource.Columns.Add("租金期间");
                dtBaseSource.Columns.Add("餐厅Sales金额");
                dtBaseSource.Columns.Add("Kiosk Sales金额");
                dtBaseSource.Columns.Add("租金金额");
                //计算过程公式及值
                DataTable dtProcessResultSource = new DataTable();
                dtProcessResultSource.Columns.Add("过程");
                dtProcessResultSource.Columns.Add("值");
                //流程步骤提交时间
                //DataTable dtWFProc = new DataTable();
                //dtWFProc.Columns.Add("步骤");
                //dtWFProc.Columns.Add("提交时间");
                //
                foreach (DataRow task in tasks)
                {
                    //金额为0则无需生成
                    if (task["APAmount"] == DBNull.Value || task["APAmount"] == null || Convert.ToInt32(task["APAmount"]) == 0)
                    {
                        continue;
                    }
                    //
                    DataRow other = null;//租金计算负责人、审核人、审批人、条码数据行
                    string id = task["APRecordID"].ToString();
                    string title = string.Format("合同编号：{0} 餐厅编号：{1} 租金类型：{2} 期间：{3}",
                        task["ContractNo"], task["StoreOrDeptNo"], task["RentType"], task["RentDateDiff"]);
                    img = new TifWriter(imgWidth, 1200); // (imgWidth, this.CaculateImageHeight(this.CaculateType, task));
                    //从数据源中提取计算结果基础信息
                    DataRow[] tempRows = this.DataSource.Tables[1].Select("APRecordID='" + id + "'");
                    dtBaseSource.Rows.Clear();
                    foreach (DataRow ap in tempRows)
                    {
                        dtBaseSource.Rows.Add(
                            ap["CompanyCode"],
                            ap["VendorNo"],
                            ap["VendorName"],
                            ap["PayMentType"],
                            ap["RentDateDiff"],
                            ap["StoreSales"],
                            ap["KioskSales"],
                            ap["APAmount"]
                        );
                    }
                    //从数据源中提取计算过程公式及结果
                    tempRows = this.DataSource.Tables[2].Select("APRecordID='" + id + "'");
                    dtProcessResultSource.Rows.Clear();
                    foreach (DataRow ap in tempRows)
                    {
                        dtProcessResultSource.Rows.Add(
                            ap["ParameterName"],
                            ap["ParameterValue"]
                        );
                    }
                    //从数据源中提取当前AP流程提交过程
                    //tempRows = this.DataSource.Tables[5].Select("APRecordID='" + id + "'");
                    //dtWFProc.Rows.Clear();
                    //foreach (DataRow ap in tempRows)
                    //{
                    //    dtWFProc.Rows.Add(ap["TaskName"], ap["FinishTime"]);
                    //}
                    //从数据源中提取租金计算负责人、审核人、审批人、条码数据行
                    tempRows = this.DataSource.Tables[3].Select("APGLRecordID='" + id + "'");
                    if (tempRows != null && tempRows.Length > 0)
                    {
                        other = tempRows[0];
                    }

                    //写入条形码
                    img.DrawBarCode("", other["BarCode"] + "");

                    //写入标题
                    img.DrawString(title, HorizontalAlign.Center, "黑体", 13, true);

                    //写入计算结果基础信息
                    img.DrawString("计算结果基础信息", HorizontalAlign.Left, "黑体", 12, false);
                    img.DrawTable(dtBaseSource, null, null, 40, 40);

                    //写入计算过程公式及结果
                    img.DrawString("计算过程公式及结果", HorizontalAlign.Left, "黑体", 12, false);
                    img.DrawTable(dtProcessResultSource, null, new int[] { 350 }, 25, 40);

                    //写入流程各个步骤提交时间
                    //img.DrawString("流程步骤提交时间", HorizontalAlign.Left, "黑体", 12, false);
                    //img.DrawTable(dtWFProc, null, new int[] { 130, 150 }, 25, 25);

                    //写入租金计算负责人信息到tif
                    img.DrawString(string.Format("租金计算负责人：{0}", other["ChargeUser"]));

                    //写入审核人信息到tif
                    img.DrawString(string.Format("租金计算审核人：{0}  审核时间：{1}", other["AuditUser"], other["AuditDate"]));

                    //写入AP审批人信息到tif
                    img.DrawString(string.Format("租金计算审批人：{0}  审批时间：{1}", other["ApprovalUser"], other["ApprovalDate"]));

                    //文件名
                    fileName = string.Format(@"{0}\{1}{2}{3:yyyyMMdd}{4}.tif", (task["PaymentType"].ToString() == "M" ? mPath : tPath), this.CaculateType, task["PaymentType"], DateTime.Now, this.FuncFlowNumber());

                    //保存
                    img.SaveAs(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                }
            }
            catch (Exception e)
            {
                img.DrawString(e.Message);
                img.DrawString(e.Source.ToString());
                img.SaveAs(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
            }
            finally
            {
                if (img != null)
                {
                    img.Dispose();
                }
            }
        }
        /// <summary>
        /// 将指定公司、租金类型的租金计算过程写入到一个tif影像文件。
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="rentType"></param>
        /// <param name="glType"></param>
        private void WriteGLTif(string companyCode, string rentType, string glType)
        {
            string fileName = null;
            ImageWriter img = null;
            try
            {
                DataRow[]  tasks = this.DataSource.Tables[4].Select("GLType='" + glType + "'", "CompanyCode,AC_Mo,ScCd,StoreOrDeptNo,GLAmount");
                if (tasks == null || tasks.Length <= 0)
                {
                    return;
                }
                //计算结果基础信息
                DataTable dtBaseSource = new DataTable();
                dtBaseSource.Columns.Add("公司编号");
                dtBaseSource.Columns.Add("餐厅（部门）编号");
                dtBaseSource.Columns.Add("对应区间");
                dtBaseSource.Columns.Add("餐厅预估Sales金额");
                dtBaseSource.Columns.Add("Kiosk预估Sales金额");
                dtBaseSource.Columns.Add("月预提金额");
                //计算过程公式及值
                DataTable dtProcessResultSource = new DataTable();
                dtProcessResultSource.Columns.Add("过程");
                dtProcessResultSource.Columns.Add("值");
                //流程步骤提交时间
                //DataTable dtWFProc = new DataTable();
                //dtWFProc.Columns.Add("步骤");
                //dtWFProc.Columns.Add("提交时间");
                //
                //实例化绘图类
                img = new TifWriter(imgWidth, 1200); //(imgWidth, this.CaculateImageHeight(this.CaculateType, tasks));
                int index = 0;
                foreach (DataRow task in tasks)
                {
                    //金额为0则无需生成
                    if (task["GLAmount"] == DBNull.Value || task["GLAmount"] == null || Convert.ToInt32(task["GLAmount"]) == 0)
                    {
                        continue;
                    }
                    //
                    DataRow other = null;//租金计算负责人、审核人、审批人、条码数据行
                    string id = task["GLRecordID"].ToString();
                    string title = string.Format("合同编号：{0} 租金类型：{1} 期间：{2}", task["ContractNo"], glType, task["RentDateDiff"]);
                    //从数据源中提取计算结果基础信息
                    DataRow[] tempRows = this.DataSource.Tables[1].Select("GLRecordID='" + id + "'");
                    dtBaseSource.Rows.Clear();
                    foreach (DataRow ap in tempRows)
                    {
                        dtBaseSource.Rows.Add(
                            ap["CompanyCode"],
                            ap["StoreOrDeptNo"],
                            ap["RentDateDiff"],
                            ap["StoreEstimateSales"],
                            ap["KioskEstimateSales"],
                            ap["GLAmount"]
                        );
                    }
                    //从数据源中提取计算过程公式及结果
                    tempRows = this.DataSource.Tables[2].Select("GLRecordID='" + id + "'");
                    dtProcessResultSource.Rows.Clear();
                    foreach (DataRow ap in tempRows)
                    {
                        dtProcessResultSource.Rows.Add(
                            ap["ParameterName"],
                            ap["ParameterValue"]
                        );
                    }
                    //从数据源中提取当前AP流程提交过程
                    //tempRows = this.DataSource.Tables[5].Select("GLRecordID='" + id + "'");
                    //dtWFProc.Rows.Clear();
                    //foreach (DataRow ap in tempRows)
                    //{
                    //    dtWFProc.Rows.Add(ap["TaskName"], ap["FinishTime"]);
                    //}
                    //
                    //从数据源中提取租金计算负责人、审核人、审批人、条码数据行
                    tempRows = this.DataSource.Tables[3].Select("APGLRecordID='" + id + "'");
                    if (tempRows != null && tempRows.Length > 0)
                    {
                        other = tempRows[0];
                    }

                    //写入标题
                    img.DrawString(title, HorizontalAlign.Center, "黑体", 13, true);

                    //写入计算结果基础信息
                    img.DrawString("计算结果基础信息", HorizontalAlign.Left, "黑体", 12, false);
                    img.DrawTable(dtBaseSource, null, null, 40, 40);

                    //写入计算过程公式及结果
                    //img.DrawString("计算过程公式及结果", HorizontalAlign.Left, "黑体", 12, false);
                    //img.DrawTable(dtProcessResultSource, null, new int[] { 350 }, 25, 40);

                    //写入流程各个步骤提交时间
                    //img.DrawString("流程步骤提交时间", HorizontalAlign.Left, "黑体", 12, false);
                    //img.DrawTable(dtWFProc, null, new int[] { 130, 150 }, 25, 25);

                    //写入租金计算负责人信息到pdf
                    img.DrawString(string.Format("租金计算负责人：{0}", other["ChargeUser"]));

                    //写入审核人信息到pdf
                    img.DrawString(string.Format("租金计算审核人：{0}  审核时间：{1}", other["AuditUser"], other["AuditDate"]));

                    //插入一条分割线
                    if (tasks.Length > 1 && index < tasks.Length - 1)
                    {
                        img.DrawSplitLine();
                    }
                    index++;
                }
                //文件名
                fileName = string.Format(@"{0}{1}{2}{3}.tif", 
                    path, (glType.IndexOf("直线租金") > -1 ? "SL" : "GL"), DateTime.Now.ToString("yyyyMMdd"), this.FuncFlowNumber());

                //保存
                img.SaveAs(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
            }
            catch (Exception e)
            {
                img.DrawString(e.Message);
                img.SaveAs(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
            }
            finally
            {
                if (img != null)
                {
                    img.Dispose();
                }
            }
        }

        #region CaculateImageHeight
        ///// <summary>
        ///// 根据待生成影像文件的数据粗略计算影像文件高度。
        ///// </summary>
        ///// <returns></returns>
        //private int CaculateImageHeight(CaculateType type, DataRow task)
        //{
        //    string id = null;
        //    int height, rcount;
        //    DataRow[] tempRows = null;

        //    height = 100;//上下边距之和

        //    height += 100;//条码高度

        //    height += 30;//title

        //    //读取业务数据主键
        //    if (this.CaculateType == CaculateType.AP)
        //        id = task["APRecordID"].ToString();
        //    else
        //        id = task["GLRecordID"].ToString();

        //    //计算结果基础信息高度
        //    height += 25;//计算结果基础信息title
        //    if (this.CaculateType == CaculateType.AP)
        //        tempRows = this.DataSource.Tables[1].Select("APRecordID='" + id + "'");
        //    else
        //        tempRows = this.DataSource.Tables[1].Select("GLRecordID='" + id + "'");
        //    rcount = (tempRows == null ? 0 : tempRows.Length);
        //    height += (rcount + 1) * 40;

        //    //AP计算过程公式及结果高度
        //    if (type == CaculateType.AP)
        //    {
        //        height += 25;//计算过程title
        //        tempRows = this.DataSource.Tables[2].Select("APRecordID='" + id + "'");
        //        rcount = (tempRows == null ? 0 : tempRows.Length);
        //        height += (rcount + 1) * 40;
        //    }

        //    //流程步骤提交时间表高度
        //    //height += 25;//title
        //    //if (this.CaculateType == CaculateType.AP)
        //    //    tempRows = this.DataSource.Tables[5].Select("APRecordID='" + id + "'");
        //    //else
        //    //    tempRows = this.DataSource.Tables[5].Select("GLRecordID='" + id + "'");
        //    //rcount = (tempRows == null ? 0 : tempRows.Length);
        //    //height += (rcount + 1) * 25;

        //    //AP审核人、审批人、负责人高度
        //    height += 2 * 20;
        //    if (type == CaculateType.AP)
        //    {
        //        height += 20;
        //    }
        //    return height;
        //}
        ///// <summary>
        ///// 根据待生成影像文件的数据粗略计算影像文件高度。
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="tasks"></param>
        ///// <returns></returns>
        //private int CaculateImageHeight(CaculateType type, DataRow[] tasks)
        //{
        //    int height = 0;
        //    foreach (DataRow dr in tasks)
        //    {
        //        height += this.CaculateImageHeight(type, dr);
        //        height += 12;//分隔线高度
        //    }
        //    return height;
        //}
        #endregion
        #endregion
    }
}