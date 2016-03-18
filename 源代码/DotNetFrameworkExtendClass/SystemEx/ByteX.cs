using System.Security.Cryptography;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class ByteX
    {
        /// <summary>
        /// 通过密钥，对字符串形式的加密数据进行解密
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="SecretKey">密钥</param>
        /// <param name="SingleLine">True：解析单行；False：解析所有</param>
        /// <param name="Standard">是否使用标准方式</param>
        /// <returns></returns>
        public static string Decrypt(this byte[] bytes, string SecretKey, bool SingleLine = true, bool Standard = false)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            if (Standard)
            {
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
            }
            des.Key = SecretKey.ToMD5().Substring(0, 0x08).ToBytes();
            des.IV = SecretKey.ToMD5().Substring(0, 0x08).ToBytes();
            global::System.IO.MemoryStream ms = new global::System.IO.MemoryStream(bytes);
            string val = string.Empty;
            try
            {
                CryptoStream encStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                global::System.IO.StreamReader sr = new global::System.IO.StreamReader(encStream);
                val = SingleLine ? sr.ReadLine() : sr.ReadToEnd();
                sr.Close();
                encStream.Close();
            }
            catch
            {
                val = "密钥错误，解密失败。";
            }
            finally
            {
                ms.Close();
            }
            return val;
        }
    }
}
