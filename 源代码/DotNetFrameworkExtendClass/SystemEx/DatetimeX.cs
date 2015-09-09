using System;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// System.Datetime的扩展
    /// </summary>
    public static class DatetimeX
    {
        /// <summary>
        /// 获取此实例所表示的日期为该月中的第几周。
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        [Obsolete("由于计算公式的差异性，该方法已经暂停使用，只会返回-1", true)]
        public static int WeekOfMonth(this DateTime utc)
        {
            return -1;
        }
        /// <summary>
        /// 返回该日期属于一年中的第几周
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime utc)
        {
            return (utc.DayOfYear / 7) + 1;
        }
        /// <summary>
        /// 返回Unix时间戳
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static Int64 UnixTimestamp(this DateTime utc)
        {
            DateTime gw = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            return (Int64)(DateTime.Now - gw).TotalSeconds;
        }

        /// <summary>
        /// 返回Unix时间戳
        /// </summary>
        /// <param name="utc"></param>
        /// <param name="datetime">要进行计算的时间</param>
        /// <returns></returns>
        public static Int64 UnixTimestamp(this DateTime utc, DateTime datetime)
        {
            DateTime gw = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return (Int64)(datetime - gw).TotalSeconds;
        }
    }
}
