using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// Windows进程辅助
    /// </summary>
    public class ApplicationProcess
    {
        /// <summary>
        /// 获取所有进程
        /// </summary>
        /// <returns></returns>
        public string[] ProcessNames()
        {
            List<string> items = new List<string>();
            foreach (Process p in Process.GetProcesses().OrderBy(t => t.ProcessName))
            {
                items.Add(p.ProcessName);
            }
            return items.ToArray();
        }
        /// <summary>
        /// 查找进程是否已启动
        /// </summary>
        /// <param name="ProcessName">进程名</param>
        /// <returns></returns>
        public bool FindProcessExist(string ProcessName)
        {
            Process p = Process.GetProcesses().FirstOrDefault(t => t.ProcessName.Equals(ProcessName));
            return p != null;
        }
    }
}
