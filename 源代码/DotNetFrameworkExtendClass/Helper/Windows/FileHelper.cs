using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 文件辅助方法
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 获取文件的MD5
        /// </summary>
        /// <param name="FileName">文件路径</param>
        /// <param name="Separator">MD5分隔符</param>
        /// <param name="Upper">结果大写/小写</param>
        /// <returns></returns>
        public static string GetMD5Hash(string FileName, string Separator = "", bool Upper = true)
        {
            FileStream file = new FileStream(FileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(file);
            file.Close();
            return string.Join(Separator, bytes.Select(t => t.ToString(Upper ? "X2" : "x2")));
        }
    }
}
