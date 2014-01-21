using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace MCD.Common
{
    /// <summary>
    /// 对指定字符串进行加密/解密
    /// </summary>
    static class DataCrypt
    {
        //Fields
        private static SymmetricAlgorithm mobjCryptoService;
        private static string Key;

        #region ctor

        /// <summary>
        /// 对称加密类的构造函数
        /// </summary>
        static DataCrypt()
        {
            DataCrypt.mobjCryptoService = new RijndaelManaged();
            DataCrypt.Key = "McdCommonDataCrypt2011";
        }
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = DataCrypt.GetLegalKey();
            mobjCryptoService.IV = DataCrypt.GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            //
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public static string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = DataCrypt.GetLegalKey();
            mobjCryptoService.IV = DataCrypt.GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey()
        {
            string sTemp = Key;
            //
            DataCrypt.mobjCryptoService.GenerateKey();
            byte[] bytTemp = DataCrypt.mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
            {
                sTemp = sTemp.Substring(0, KeyLength);
            }
            else if (sTemp.Length < KeyLength)
            {
                sTemp = sTemp.PadRight(KeyLength, ' ');
            }
            //
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private static byte[] GetLegalIV()
        {
            string sTemp = "bingosoft";
            //
            DataCrypt.mobjCryptoService.GenerateIV();
            byte[] bytTemp = DataCrypt.mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
            {
                sTemp = sTemp.Substring(0, IVLength);
            }
            else if (sTemp.Length < IVLength)
            {
                sTemp = sTemp.PadRight(IVLength, ' ');
            }
            //
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
    }
}