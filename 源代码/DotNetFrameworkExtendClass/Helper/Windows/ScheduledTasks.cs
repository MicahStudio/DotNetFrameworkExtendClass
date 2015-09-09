using System.Diagnostics;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// Windows 计划任务
    /// </summary>
    public static class ScheduledTasks
    {
        /// <summary>
        /// 创建一个计划任务（按分钟执行）
        /// </summary>
        /// <param name="TaskName">任务计划的名字</param>
        /// <param name="Minute">间隔的分钟数</param>
        /// <param name="FullPath">完整的要执行的应用的路径</param>
        public static void CreateTask(string TaskName, int Minute, string FullPath)
        {
            Process Proc = new Process();
            Proc.StartInfo.CreateNoWindow = true;
            Proc.StartInfo.FileName = "CMD.EXE";
            Proc.StartInfo.UseShellExecute = false;
            Proc.StartInfo.RedirectStandardInput = true;
            Proc.StartInfo.RedirectStandardOutput = true;
            Proc.StartInfo.RedirectStandardError = true;
            Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Proc.OutputDataReceived += (se, ex) =>
            {
            };
            Proc.Start();
            Proc.StandardInput.WriteLine(@"SCHTASKS /Create /SC MINUTE /MO ""{0}"" /TN ""{1}"" /TR ""\""{2}\""""", Minute, TaskName, FullPath);
            Proc.StandardInput.WriteLine(@"SCHTASKS /RUN /TN ""{0}""", TaskName);
            Proc.BeginOutputReadLine();
            Proc.BeginErrorReadLine();
            Proc.Close();
        }
        /// <summary>
        /// 删除计划任务
        /// </summary>
        /// <param name="TaskName">要删除的任务的名字</param>
        public static void DeleteTask(string TaskName)
        {
            Process Proc = new Process();
            Proc.StartInfo.CreateNoWindow = true;
            Proc.StartInfo.FileName = "CMD.EXE";
            Proc.StartInfo.UseShellExecute = false;
            Proc.StartInfo.RedirectStandardInput = true;
            Proc.StartInfo.RedirectStandardOutput = true;
            Proc.StartInfo.RedirectStandardError = true;
            Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Proc.OutputDataReceived += (se, ex) =>
            {
            };
            Proc.Start();
            Proc.StandardInput.WriteLine(@"SCHTASKS /Delete /TN ""{0}"" /F", TaskName);
            Proc.BeginOutputReadLine();
            Proc.BeginErrorReadLine();
            Proc.Close();
        }
    }
}
