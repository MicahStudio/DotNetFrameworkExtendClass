using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// WinRAR操作
    /// </summary>
    public class WinRAR
    {
        AutoResetEvent UnZiping = new AutoResetEvent(false);
        RegistryKey RarKey;
        Object RarObj;
        string RarPath;
        /// <summary>
        /// 构造方法
        /// </summary>
        public WinRAR()
        {
            Regex regex = new Regex(@"[a-zA-Z]:[\\].*.[\\]");
            RarKey = Registry.ClassesRoot.OpenSubKey(@"WinRAR.ZIP\shell\open\command");
            RarObj = RarKey.GetValue("");
            RarPath = RarObj.ToString();
            RarPath = regex.Match(RarPath).Value;
            if (string.IsNullOrWhiteSpace(RarPath))
            {
                throw new NullReferenceException("该主机未安装WinRAR软件。");
            }
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zipFilePath">压缩文件的路径</param>
        /// <param name="UnZipPath">要解压的位置</param>
        /// <returns></returns>
        public bool UnRar(string zipFilePath, string UnZipPath)
        {
            bool result = false;
            string args = @"Rar.exe x ""{0}"" ""{1}""";
            Process Proc = new Process();
            Proc.StartInfo.CreateNoWindow = true;
            Proc.StartInfo.FileName = RarPath;
            Proc.StartInfo.Arguments = string.Format(args, zipFilePath, UnZipPath);
            Proc.StartInfo.UseShellExecute = false;
            Proc.StartInfo.RedirectStandardInput = true;
            Proc.StartInfo.RedirectStandardOutput = true;
            Proc.StartInfo.RedirectStandardError = true;
            Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Proc.OutputDataReceived += (se, ex) =>
            {
                if (ex.Data.Contains("全部完成"))
                {
                    result = true;
                    UnZiping.Set();
                }
                else
                {
                    result = false;
                    UnZiping.Set();
                }
            };
            Proc.Start();
            Proc.BeginOutputReadLine();
            Proc.BeginErrorReadLine();
            UnZiping.WaitOne();
            return result;
        }
    }
}
