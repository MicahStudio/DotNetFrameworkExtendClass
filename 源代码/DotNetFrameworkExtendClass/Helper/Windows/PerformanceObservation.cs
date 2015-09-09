using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 性能观察
    /// </summary>
    public class PerformanceObservation : Singleton<PerformanceObservation>
    {
        private PerformanceObservation() { }
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private long start = 0, end = 0;
        /// <summary>
        /// 开始记录内存消耗
        /// </summary>
        public void TotalMemoryBegin()
        {
            start = GC.GetTotalMemory(true);
        }
        /// <summary>
        /// 停止记录内存消耗量
        /// </summary>
        public void TotalMemoryEnd()
        {
            GC.Collect();
            GC.WaitForFullGCComplete();
            end = GC.GetTotalMemory(true);
        }
        /// <summary>
        /// 计算内存消耗量(Kb)
        /// </summary>
        public long UseMemory
        {
            get
            {
                return (end - start) / 1024;
            }
        }
        /// <summary>
        /// 计时开始
        /// </summary>
        public void BeginTime()
        {
            sw.Reset();
            sw.Start();
        }
        /// <summary>
        /// 计时结束
        /// </summary>
        public void EndTime()
        {
            sw.Stop();
        }
        /// <summary>
        /// 计算消耗时间（毫秒）
        /// </summary>
        public long Useup
        {
            get
            {
                return sw.ElapsedMilliseconds;
            }
        }
    }
}
