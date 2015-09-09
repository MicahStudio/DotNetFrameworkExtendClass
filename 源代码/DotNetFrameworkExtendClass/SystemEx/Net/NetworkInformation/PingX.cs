using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class PingX
    {
        /// <summary>
        /// 与目标地址间的通信是否通畅
        /// </summary>
        /// <param name="pingSender"></param>
        /// <param name="IPAddress">目标地址</param>
        /// <param name="Timeout">超时时间（毫秒）</param>
        /// <returns></returns>
        public static bool IsWork(this global::System.Net.NetworkInformation.Ping pingSender, string IPAddress, int Timeout = 300)
        {
            try
            {
                PingReply reply = new Ping().Send(IPAddress, Timeout, "is work?".ToBytes(), new global::System.Net.NetworkInformation.PingOptions { DontFragment = true });
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 目标连通性
        /// </summary>
        /// <param name="pingSender"></param>
        /// <param name="IPAddress">目标地址</param>
        /// <param name="Timeout">超时时间（毫秒）</param>
        /// <returns></returns>
        public static PingReply Reply(this global::System.Net.NetworkInformation.Ping pingSender, string IPAddress, int Timeout = 300)
        {
            return new Ping().Send(IPAddress, Timeout, "is work?".ToBytes(), new global::System.Net.NetworkInformation.PingOptions { DontFragment = true });
        }
    }
}
