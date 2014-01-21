using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.IO.Compression;

using MCD.RLPlanning.IServices;

namespace MCD.RLPlanning.BLL
{
    /// <summary>
    /// WCF 客户端工厂方法类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseBLL<T> : IDisposable where T : IBaseService
    {
        //Fields
        private T wcfService = default(T);
        /// <summary>
        /// 创建通道的工厂
        /// </summary>
        private ChannelFactory<T> factory = null;

        //Properties
        /// <summary>
        /// 创建 WCF 服务器端对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="?"></param>
        /// <returns></returns>
        protected T WCFService
        {
            get
            {
                if (this.wcfService == null)
                {
                    try
                    {
                        this.factory = new ChannelFactory<T>(typeof(T).Name);
                        this.wcfService = this.factory.CreateChannel();
                    }
                    catch //(Exception ex)
                    {
                        //string s = ex.Message;
                    }
                }
                else
                {
                    try
                    {
                        // 通道连接失败
                        this.wcfService.TestMethod();
                    }
                    catch (Exception)
                    {
                        this.factory = new ChannelFactory<T>(typeof(T).Name);
                        this.wcfService = factory.CreateChannel();
                    }
                }
                return this.wcfService;
            }
        }

        //Methods
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetMd5Value(string value)
        {
            byte[] result = Encoding.Default.GetBytes(value);
            //
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", string.Empty);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public DataSet DeSerilize(byte[] binaryData)
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
        /// <summary>
        /// 把 DataSet 序列化为二进制数组并压缩
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns></returns>
        public byte[] Serilize(DataSet ds)
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
        #region IDisposable

        public void CloseService()
        {
            if (this.wcfService != null)
            {
                ((IClientChannel)this.wcfService).Close();
                ((IDisposable)this.wcfService).Dispose();
                //
                this.wcfService = default(T);
            }
        }

        /// <summary>
        /// 对象注销时，关闭 WCF 对象
        /// </summary>
        public virtual void Dispose()
        {
            try
            {
                if (this.factory != null && this.factory.State != CommunicationState.Faulted)
                {
                    this.factory.Close();
                }
            }
            catch
            {
                this.factory.Abort();
            }
        }
        #endregion

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns>返回的是string，如果是null表面获取失败</returns>
        public virtual DateTime GetServerTime()
        {
            if (null == this.WCFService)
            {
                throw new Exception("获取服务器时间错误");
            }
            return this.WCFService.TestMethod();
        }
    }
}