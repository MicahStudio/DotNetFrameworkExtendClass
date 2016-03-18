using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringX
    {
        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="text">要追加的字符串</param>
        /// <returns>连接后的心字符串</returns>
        public static string Append(this string str, string text)
        {
            return str += text;
        }
        /// <summary>
        ///转换为 ASCIIEncoding.ASCII的byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }
        /// <summary>
        /// 计算MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5(this string str)
        {
            StringBuilder hash = new StringBuilder();
            if (!str.IsNullOrWhiteSpace())
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(str.ToBytes());
                foreach (byte b in buffer)
                {
                    hash.AppendFormat("{0:X2}", b);
                }
            }
            return hash.ToString();
        }
        /// <summary>
        /// 通过密钥对文本进行加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="SecretKey">密钥</param>
        /// <param name="SingleLine">True：单行；False：所有</param>
        /// <param name="Standard">是否使用标准方式</param>
        /// <returns>加密后的文本</returns>
        public static string Encrypt(this string str, string SecretKey, bool SingleLine = true, bool Standard = false)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            if (Standard)
            {
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
            }
            des.Key = SecretKey.ToMD5().Substring(0, 0x08).ToBytes();
            des.IV = SecretKey.ToMD5().Substring(0, 0x08).ToBytes();
            global::System.IO.MemoryStream ms = new global::System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            global::System.IO.StreamWriter sw = new global::System.IO.StreamWriter(encStream);
            if (SingleLine)
                sw.WriteLine(str);
            else
                sw.Write(str);
            sw.Close();
            encStream.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            StringBuilder hash = new StringBuilder();
            foreach (byte b in buffer.ToArray())
            {
                hash.AppendFormat("{0:X2}", b);
            }
            return hash.ToString();
        }
        /// <summary>
        /// 通过密钥，对字符串形式的加密数据进行解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="SecretKey">密钥</param>
        /// <param name="SingleLine">True：解析单行；False：解析所有</param>
        /// <param name="Standard">是否使用标准方式</param>
        /// <returns>原始文本</returns>
        public static string Decrypt(this string str, string SecretKey, bool SingleLine = true, bool Standard = false)
        {
            #region 数据还原
            int len = str.Length / 2;
            byte[] buffer = new byte[len];
            try
            {
                for (int i = 0; i < len; i++)
                {
                    buffer[i] = Convert.ToByte(str.Substring(i * 0x02, 0x02), 0x10);
                }
            }
            catch
            {
                return "不是有效的加密数据。";
            }
            #endregion
            return buffer.Decrypt(SecretKey, SingleLine, Standard);
        }
    }
}