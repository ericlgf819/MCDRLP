using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MCD.Common;

using MCD.RLPlanning.IServices.SalesCalculate;

namespace MCD.RLPlanning.Services.SalesCalculate
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SalesCalculate" in code, svc and config file together.
    public class SalesCalculate : ISalesCalculate
    {
        private static readonly object m_writehandle = new object();
        private static readonly object m_selecthandle = new object();

        #region 租金计算相关

        /// <summary>
        /// 筛选Store、Kiosk
        /// </summary>
        /// <param name="strStoreNo"></param>
        /// <param name="strStoreName"></param>
        /// <param name="strKioskNo"></param>
        /// <param name="strKioskName"></param>
        /// <param name="companyCodesDT"></param>
        /// <returns></returns>
        public byte[] SelectStoreOrKiosk(string strStoreNo, string strStoreName, string strKioskNo,
                    string strKioskName, DataTable companyCodesDT)
        {
            lock (m_selecthandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                DataSet ds = null;
                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        // 先清空RLPlanning_Cal_TmpCompanyCodeTbl再导入CompanyCode集合
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE RLPlanning_Cal_TmpCompanyCodeTbl");
                        copy.DestinationTableName = "RLPlanning_Cal_TmpCompanyCodeTbl";
                        copy.WriteToServer(companyCodesDT);

                        // 再执行查询的存储过程
                        DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Cal_SelectEntityByNameOrNo");
                        db.AddInParameter(cmd, "@StoreNo", DbType.String, strStoreNo);
                        db.AddInParameter(cmd, "@StoreName", DbType.String, strStoreName);
                        db.AddInParameter(cmd, "@KioskNo", DbType.String, strKioskNo);
                        db.AddInParameter(cmd, "@KioskName", DbType.String, strKioskName);

                        //将timeout时间放长
                        cmd.CommandTimeout = 300;

                        ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 租金计算
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calCollectionTb"></param>
        /// <returns>0:成功，1:sqlserver超时，2:服务器忙碌，有其他用户正在计算，3:未知错误</returns>
        public int Calculate(Guid userID, byte[] calCollectionByte, out byte[] entitysInCal, out string exceptionMsg)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            entitysInCal = null;
            exceptionMsg = null;

            //用sleep和数据库中的全局表来实现部分互斥--Begin
            bool bResInUse = true;      //false才能继续向下计算, 否则sleep+while循环
            DbCommand cmdReadRes = db.GetSqlStringCommand("SELECT Value FROM RLPlanning_Env WHERE ValName='CalMuxResInUse'");
            DbCommand cmdInsertRes = db.GetSqlStringCommand("INSERT INTO RLPlanning_Env VALUES('CalMuxResInUse', '1')");
            DbCommand cmdUpdateRes = db.GetSqlStringCommand("UPDATE RLPlanning_Env SET Value='1' WHERE ValName='CalMuxResInUse'");
            string strVal = string.Empty;
            while (bResInUse)
            {
                //读取和修改互斥标志位的地方需要互斥
                lock (m_writehandle)
                {
                    using (SqlDataReader dr = db.ExecuteReader(cmdReadRes) as SqlDataReader)
                    {
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                strVal = dr[0].ToString();
                                break;
                            }

                            switch (strVal)
                            {
                                //0 表面，当前互斥资源没有用户使用
                                case "0":
                                    {
                                        //抢占资源
                                        db.ExecuteNonQuery(cmdUpdateRes);
                                        //之后可以进行计算
                                        bResInUse = false;
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                        // 如果没有CalMusResInUse全局变量，则插入该条目，并且设置值为1，表面正在使用中
                        else
                        {
                            //抢占资源
                            db.ExecuteNonQuery(cmdInsertRes);
                            //之后可以进行计算
                            bResInUse = false;
                        }
                    }
                }

                //如果上一次数据查询的结果是需要等其他用户使用完资源的，则需要主动sleep当前线程
                if (bResInUse)
                {
                    Thread.Sleep(100);
                }
            }
            //用sleep和数据库中的全局表来实现部分互斥--End

            using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                copy.BulkCopyTimeout = 500;
                try
                {
                    DataSet ds = DeSerilize(calCollectionByte);
                    DataTable calCollectionTb = ds.Tables[0];
                    // 先清空RLPlanning_Cal_TmpTbl再导入需要计算的参数集合
                    db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE RLPlanning_Cal_TmpTbl");
                    copy.DestinationTableName = "RLPlanning_Cal_TmpTbl";
                    copy.WriteToServer(calCollectionTb);

                    // 判断当前需要计算的实体中，哪些实体正在计算
                    DbCommand cmdCheckEntityInCal = db.GetStoredProcCommand("[dbo].[RLPlanning_Cal_CheckWhetherEntityInCal]");
                    DataSet dsCheckEntityInCal = db.ExecuteDataSet(cmdCheckEntityInCal);
                    if (null == dsCheckEntityInCal || dsCheckEntityInCal.Tables.Count == 0)
                    {
                        return (int)SalesCalRetCode.EN_CAL_CHECKENTITYINCALERR; //判断计算实体重复错误
                    }

                    if (dsCheckEntityInCal.Tables[0].Rows.Count > 0)
                    {
                        //释放全局互斥变量
                        DbCommand cmdUpdateFreeRes = db.GetSqlStringCommand("UPDATE RLPlanning_Env SET Value='0' WHERE ValName='CalMuxResInUse'");
                        db.ExecuteNonQuery(cmdUpdateFreeRes);

                        //获取正在计算中的实体信息
                        entitysInCal = Serilize(dsCheckEntityInCal);

                        return (int)SalesCalRetCode.EN_CAL_HASENTITYINCAL; //有实体在计算中
                    }

                    // 再执行计算存储过程
                    DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Cal_SalesCalculate");
                    // 计算可能比较耗时，需要将连接时间延长
                    cmd.CommandTimeout = 3600;  //1小时都超时，就没必要在客户端计算了
                    db.AddInParameter(cmd, "@OperatorUserID", DbType.Guid, userID);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // 写入数据库 或者 更新时出错

                    if (ex.Message.Contains("Timeout"))
                    {
                        return (int)SalesCalRetCode.EN_CAL_TIMEOUT;   //超时错误
                    }
                    else
                    {
                        exceptionMsg = ex.Message;
                        return (int)SalesCalRetCode.EN_CAL_UNKNOWNERR;   //未知错误
                    }
                }
            }

            return (int)SalesCalRetCode.EN_CAL_SUCCESS;   //成功
        }

        /// <summary>
        /// 租金计算错误结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="calStartTime"></param>
        /// <returns></returns>
        public byte[] SelectCalculateResult(Guid userID, DateTime calStartTime)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Cal_CalculateResult");
            db.AddInParameter(cmd, "@UserID", DbType.Guid, userID);
            db.AddInParameter(cmd, "@OperateTime", DbType.DateTime, calStartTime);
            DataSet ds = db.ExecuteDataSet(cmd);

            return Serilize(ds);
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            DbCommand cmd = db.GetSqlStringCommand("SELECT GETDATE()");

            string strDateTime = string.Empty;
            
            using (SqlDataReader dr = db.ExecuteReader(cmd) as SqlDataReader)
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strDateTime = dr[0].ToString();
                    }
                }
            }

            DateTime dbTime = DateTime.Now;

            try
            {
                dbTime = DateTime.Parse(strDateTime);
            }
            catch
            {
                dbTime = DateTime.Now;
            }


            return dbTime;
        }

        #endregion

        /// <summary>
        /// 把 DataSet 序列化为二进制数组并压缩
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns></returns>
        private byte[] Serilize(DataSet ds)
        {
            // 序列化为二进制
            ds.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            bFormatter.Serialize(mStream, ds);
            byte[] bytes = mStream.ToArray();
            // 压缩 
            MemoryStream oStream = new MemoryStream();
            DeflateStream zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            //返回
            return oStream.ToArray();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        private DataSet DeSerilize(byte[] binaryData)
        {
            DataSet dsResult = new DataSet();

            if (null == binaryData)
                return dsResult;

            // 初始化流，设置读取位置
            using (MemoryStream mStream = new MemoryStream(binaryData))
            {
                mStream.Seek(0, SeekOrigin.Begin);
                // 解压缩
                using (DeflateStream unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true))
                {
                    // 反序列化得到数据集
                    dsResult.RemotingFormat = SerializationFormat.Binary;
                    //
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    dsResult = (DataSet)bFormatter.Deserialize(unZipStream);
                }
            }
            return dsResult;
        }

        #region IBaseService
        public DateTime TestMethod()
        {
            return DateTime.Now;
        }
        #endregion
    }
}
