using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MCD.RLPlanning.IServices.ForecastSales;


namespace MCD.RLPlanning.Services.ForecastSales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SalesService" in code, svc and config file together.
    public class SalesService : ISalesService
    {
        private static readonly object m_validatehandle = new object();
        private static readonly object m_importhandle = new object();
        private static readonly object m_importparthandle = new object();

        #region Sales数据导入相关方法
        /// <summary>
        /// 导入Sales数据
        /// </summary>
        /// <param name="dt"></param>
        public byte[] ImportSales(byte[] byteTbl)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            DataSet ds = null;

            //Import_Sales表的互斥操作 --Begin
            bool bImportResInUse = true;
            DbCommand cmdReadImportRes = db.GetSqlStringCommand("SELECT Value FROM RLPlanning_Env WHERE ValName='ImportMuxResInUse'");
            DbCommand cmdInsertImportRes = db.GetSqlStringCommand("INSERT INTO RLPlanning_Env VALUES('ImportMuxResInUse', '1')");
            DbCommand cmdUpdateImportRes = db.GetSqlStringCommand("UPDATE RLPlanning_Env SET Value='1' WHERE ValName='ImportMuxResInUse'");
            DbCommand cmdUpdateFreeImportRes = db.GetSqlStringCommand("UPDATE RLPlanning_Env SET Value='0' WHERE ValName='ImportMuxResInUse'");
            string strVal = string.Empty;

            while (bImportResInUse)
            {
                //读取和修改互斥标志位的地方需要互斥
                lock (m_importhandle)
                {
                    using (SqlDataReader dr = db.ExecuteReader(cmdReadImportRes) as SqlDataReader)
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
                                        db.ExecuteNonQuery(cmdUpdateImportRes);
                                        //之后可以进行计算
                                        bImportResInUse = false;
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
                            db.ExecuteNonQuery(cmdInsertImportRes);
                            //之后可以进行计算
                            bImportResInUse = false;
                        }
                    }
                }

                //如果上一次数据查询的结果是需要等其他用户使用完资源的，则需要主动sleep当前线程
                if (bImportResInUse)
                {
                    Thread.Sleep(100);
                }
            }
            //Import_Sales表的互斥操作 --End

            using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                DataSet dataset = DeSerilize(byteTbl);
                DataTable dt = dataset.Tables[0];

                copy.BulkCopyTimeout = 500;
                try
                {
                    // 先清空Import_Sales再导入
                    db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_Sales");
                    copy.DestinationTableName = "Import_Sales";
                    copy.WriteToServer(dt);

                    //判断当前需要导入的实体，哪些是正在导入的
                    DbCommand cmdCheckEntityInImport = db.GetStoredProcCommand("[dbo].[RLPlanning_Import_CheckWhetherInImport]");
                    DataSet dsCheckEntityInImport = db.ExecuteDataSet(cmdCheckEntityInImport);
                    if (null == dsCheckEntityInImport || dsCheckEntityInImport.Tables.Count == 0)
                    {
                        //释放锁
                        db.ExecuteNonQuery(cmdUpdateFreeImportRes);

                        ds = new DataSet();
                        ds.Tables.Add();
                        ds.Tables[0].Columns.Add();
                        ds.Tables[0].Rows.Add("[dbo].[RLPlanning_Import_CheckWhetherInImport] 错误。");
                        return Serilize(ds);
                    }

                    if (dsCheckEntityInImport.Tables[0].Rows.Count > 0)
                    {
                        //释放锁
                        db.ExecuteNonQuery(cmdUpdateFreeImportRes);

                        dsCheckEntityInImport.Tables.Add();    //多加一个表，用来表示该错误是由于导入别人正在导入的实体造成的
                        return Serilize(dsCheckEntityInImport);
                    }

                    //执行导入，将临时表的数据导入到正式表中
                    DbCommand cmd = db.GetStoredProcCommand("RLPlanning_Import_Sales");
                    //导入时间会很长
                    cmd.CommandTimeout = 3600;
                    //执行导入获取导入sales的错误信息
                    ds = db.ExecuteDataSet(cmd);

                    //如果导入没问题，就调用Dispatch
                    if (null == ds || ds.Tables.Count == 0)
                    {
                        ds = new DataSet();
                        ds.Tables.Add();
                        ds.Tables[0].Columns.Add();
                        ds.Tables[0].Rows.Add("[dbo].[RLPlanning_Import_Sales] 错误。");
                        return Serilize(ds);
                    }

                    //调用Dispatch方法
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        //Forecast_Sales_ImportPart表的互斥 -- Begin
                        bool bImportPartResInUse = true;
                        DbCommand cmdReadImportPartRes = db.GetSqlStringCommand("SELECT Value FROM RLPlanning_Env WHERE ValName='ImportPartMuxResInUse'");
                        DbCommand cmdInsertImportPartRes = db.GetSqlStringCommand("INSERT INTO RLPlanning_Env VALUES('ImportPartMuxResInUse', '1')");
                        DbCommand cmdUpdateImportPartRes = db.GetSqlStringCommand("UPDATE RLPlanning_Env SET Value='1' WHERE ValName='ImportPartMuxResInUse'");
                        strVal = string.Empty;

                        while (bImportPartResInUse)
                        {
                            //读取和修改互斥标志位的地方需要互斥
                            lock (m_importparthandle)
                            {
                                using (SqlDataReader dr = db.ExecuteReader(cmdReadImportPartRes) as SqlDataReader)
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
                                                    db.ExecuteNonQuery(cmdUpdateImportPartRes);
                                                    //之后可以进行计算
                                                    bImportPartResInUse = false;
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
                                        db.ExecuteNonQuery(cmdInsertImportPartRes);
                                        //之后可以进行计算
                                        bImportPartResInUse = false;
                                    }
                                }
                            }

                            //如果上一次数据查询的结果是需要等其他用户使用完资源的，则需要主动sleep当前线程
                            if (bImportPartResInUse)
                            {
                                Thread.Sleep(100);
                            }
                        }
                        //Forecast_Sales_ImportPart表的互斥 -- End

                        //导入Forecast_Sales_ImportPart表
                        using (SqlBulkCopy copyImportPart = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                        {
                            //验证之前 RLPlanning_Import_Sales是否返回了正确的结果
                            if (null == ds || ds.Tables.Count != 2)
                            {
                                ds = new DataSet();
                                ds.Tables.Add();
                                ds.Tables[0].Columns.Add();
                                ds.Tables[0].Rows.Add("[dbo].[RLPlanning_Import_Sales] 返回数据错误。");
                                return Serilize(ds);
                            }

                            dt = ds.Tables[1];
                            copyImportPart.BulkCopyTimeout = 500;

                            // 先清空Forecast_Sales_ImportPart再导入
                            db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Forecast_Sales_ImportPart");
                            copyImportPart.DestinationTableName = "Forecast_Sales_ImportPart";
                            copyImportPart.WriteToServer(dt);

                            //调用Dispatch存储过程
                            cmd = db.GetStoredProcCommand("RLPlanning_Dispatch_Sales");
                            //导入时间会很长
                            cmd.CommandTimeout = 3600;
                            //执行导入获取导入sales的错误信息
                            db.ExecuteDataSet(cmd);
                        }
                    }

                    return Serilize(ds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // 写入数据库 或者 更新时出错
                }
            }

            return null;
        }

        /// <summary>
        /// 验证导入公司是否用户有权限
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] ValidateImportCompany(byte[] byteTbl, Guid UserID)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(byteTbl);
                        DataTable dt = dataset.Tables[0];
                        // 先清空Import_SalesValidation再导入
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        //执行验证
                        DbCommand cmd = db.GetStoredProcCommand("RLPlanning_Sales_ImportValidatePrivilege");
                        db.AddInParameter(cmd, "@UserID", DbType.Guid, UserID);
                        //时间放长
                        cmd.CommandTimeout = 3600;
                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
                return null;
            }   
        }

        /// <summary>
        /// 验证Kiosk是否独立结算租金，将非独立结算sales的kiosk给剔除，并且返回名称
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public byte[] ValidateKiosk(byte[] byteTbl)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");

                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(byteTbl);
                        DataTable dt = dataset.Tables[0];
                        // 先清空RLPlanning_Cal_TmpTbl再导入需要计算的参数集合
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        // 再执行计算存储过程
                        DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Sales_ImportValidateKiosk");
                        // 计算可能比较耗时，需要将连接时间延长
                        cmd.CommandTimeout = 3600;

                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
            }

            return null;
        }

        /// 验证导入的Sales所在年是否在开店年前，如果是剔除
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public byte[] ValidateRentStartDate(byte[] byteTbl)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");

                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(byteTbl);
                        DataTable dt = dataset.Tables[0];
                        // 先清空RLPlanning_Cal_TmpTbl再导入需要计算的参数集合
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        // 再执行计算存储过程
                        DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Sales_ImportValidateRentStartDate");
                        // 计算可能比较耗时，需要将连接时间延长
                        cmd.CommandTimeout = 3600;

                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 验证导入的实体是否存在，或者是否有效，如果不存在/无效 则剔除
        /// </summary>
        /// <param name="byteTbl"></param>
        /// <returns></returns>
        public byte[] ValidateEntityExistence(byte[] byteTbl)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");

                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(byteTbl);
                        DataTable dt = dataset.Tables[0];
                        // 先清空RLPlanning_Cal_TmpTbl再导入需要计算的参数集合
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        // 再执行计算存储过程
                        DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_ImportValidateEntityExistence");
                        // 计算可能比较耗时，需要将连接时间延长
                        cmd.CommandTimeout = 3600;

                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 验证公司编号与餐厅编号是否有关联
        /// </summary>
        /// <param name="byteTbl"></param>
        /// <returns></returns>
        public byte[] ValidateCompanyCodeAndStoreNo(byte[] byteTbl)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");

                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(byteTbl);
                        DataTable dt = dataset.Tables[0];
                        // 先清空RLPlanning_Cal_TmpTbl再导入需要计算的参数集合
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        // 再执行计算存储过程
                        DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_ImportValidateCompanyCodeAndStoreNo");
                        // 计算可能比较耗时，需要将连接时间延长
                        cmd.CommandTimeout = 3600;

                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 更新Sales数据
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateSales(DataTable dt)
        { }

        /// <summary>
        /// 根据类型、餐厅名称或编号返回餐厅、甜品店信息
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strStoreNoOrName"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageCount"></param>
        /// <returns></returns>
        public byte[] SelectStoreOrKiosk(string strType, string strStoreNoOrName, string strCompanyCode, string strStatus, 
                string strUserID, string strAreaID, int iPageIndex, int iPageSize, out int iRecordCount)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Sales_SelectStoreOrKiosk");
            db.AddInParameter(cmd, "@Type", DbType.String, strType);
            db.AddInParameter(cmd, "@StoreNoOrName", DbType.String, strStoreNoOrName);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, strCompanyCode);
            db.AddInParameter(cmd, "@Status", DbType.String, strStatus);
            db.AddInParameter(cmd, "@UserID", DbType.String, strUserID);
            db.AddInParameter(cmd, "@AreaID", DbType.String, strAreaID);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, iPageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, iPageSize);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 0);
            DataSet ds = db.ExecuteDataSet(cmd);

            iRecordCount = cmd.Parameters["@RecordCount"].Value == DBNull.Value ? 0 : (int)cmd.Parameters["@RecordCount"].Value;

            return Serilize(ds);
        }

        /// <summary>
        /// 根据storeNo和kioskNo来查询Sales数据
        /// </summary>
        /// <param name="storeNo"></param>
        /// <param name="kioskNo"></param>
        /// <returns></returns>
        public byte[] SelectSales(string storeNo, string kioskNo)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Select_Sales");
            db.AddInParameter(cmd, "@StoreNo", DbType.String, storeNo);
            db.AddInParameter(cmd, "@KioskNo", DbType.String, kioskNo);
            DataSet ds = db.ExecuteDataSet(cmd);
          
            return Serilize(ds);
        }

        /// <summary>
        /// 返回Excel模板的Dataset
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public byte[] SelectImportSalesTemplate(string strUserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DBConnection");
            //
            DbCommand cmd = db.GetStoredProcCommand("dbo.RLPlanning_Select_SalesTemplate");
            db.AddInParameter(cmd, "@UserID", DbType.String, strUserID);
            DataSet ds = db.ExecuteDataSet(cmd);

            return Serilize(ds);
        }

        /// <summary>
        /// 返回当前导入的数据的最小合同时间或者开店时间
        /// </summary>
        /// <returns></returns>
        public byte[] GetMinRentStartDateOrOpenningDate(byte[] bytetbl)
        {
            lock (m_validatehandle)
            {
                Database db = DatabaseFactory.CreateDatabase("DBConnection");
                using (SqlBulkCopy copy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    copy.BulkCopyTimeout = 500;
                    try
                    {
                        DataSet dataset = DeSerilize(bytetbl);
                        DataTable dt = dataset.Tables[0];

                        // 先清空Import_SalesValidation再导入
                        db.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE Import_SalesValidation");
                        copy.DestinationTableName = "Import_SalesValidation";
                        copy.WriteToServer(dt);

                        //执行验证
                        DbCommand cmd = db.GetStoredProcCommand("RLPlanning_Sales_ImportGetMinRentStartDateOrOpenningDate");
                        DataSet ds = db.ExecuteDataSet(cmd);

                        return Serilize(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // 写入数据库 或者 更新时出错
                    }
                }
                return null;
            }   
        }

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
        #endregion

        #region IBaseService
        public DateTime TestMethod()
        {
            return DateTime.Now;
        }
        #endregion
    }
}