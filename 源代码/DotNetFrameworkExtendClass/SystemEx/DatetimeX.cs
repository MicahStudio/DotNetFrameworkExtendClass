using System;
using System.Globalization;

namespace DotNetFrameworkExtendClass
{

    /// <summary>
    /// 甲子表
    /// </summary>
    public enum ChineseEra
    {
        /// <summary>
        /// 
        /// </summary>
        甲子 = 1,
        /// <summary>
        /// 
        /// </summary>
        乙丑 = 2,
        /// <summary>
        /// 
        /// </summary>
        丙寅 = 3,
        /// <summary>
        /// 
        /// </summary>
        丁卯 = 4,
        /// <summary>
        /// 
        /// </summary>
        戊辰 = 5,
        /// <summary>
        /// 
        /// </summary>
        己巳 = 6,
        /// <summary>
        /// 
        /// </summary>
        庚午 = 7,
        /// <summary>
        /// 
        /// </summary>
        辛未 = 8,
        /// <summary>
        /// 
        /// </summary>
        壬申 = 9,
        /// <summary>
        /// 
        /// </summary>
        癸酉 = 10,
        /// <summary>
        /// 
        /// </summary>
        甲戌 = 11,
        /// <summary>
        /// 
        /// </summary>
        乙亥 = 12,
        /// <summary>
        /// 
        /// </summary>
        丙子 = 13,
        /// <summary>
        /// 
        /// </summary>
        丁丑 = 14,
        /// <summary>
        /// 
        /// </summary>
        戊寅 = 15,
        /// <summary>
        /// 
        /// </summary>
        己卯 = 16,
        /// <summary>
        /// 
        /// </summary>
        庚辰 = 17,
        /// <summary>
        /// 
        /// </summary>
        辛巳 = 18,
        /// <summary>
        /// 
        /// </summary>
        壬午 = 19,
        /// <summary>
        /// 
        /// </summary>
        癸未 = 20,
        /// <summary>
        /// 
        /// </summary>
        甲申 = 21,
        /// <summary>
        /// 
        /// </summary>
        乙酉 = 22,
        /// <summary>
        /// 
        /// </summary>
        丙戌 = 23,
        /// <summary>
        /// 
        /// </summary>
        丁亥 = 24,
        /// <summary>
        /// 
        /// </summary>
        戊子 = 25,
        /// <summary>
        /// 
        /// </summary>
        己丑 = 26,
        /// <summary>
        /// 
        /// </summary>
        庚寅 = 27,
        /// <summary>
        /// 
        /// </summary>
        辛卯 = 28,
        /// <summary>
        /// 
        /// </summary>
        壬辰 = 29,
        /// <summary>
        /// 
        /// </summary>
        癸巳 = 30,
        /// <summary>
        /// 
        /// </summary>
        甲午 = 31,
        /// <summary>
        /// 
        /// </summary>
        乙未 = 32,
        /// <summary>
        /// 
        /// </summary>
        丙申 = 33,
        /// <summary>
        /// 
        /// </summary>
        丁酉 = 34,
        /// <summary>
        /// 
        /// </summary>
        戊戌 = 35,
        /// <summary>
        /// 
        /// </summary>
        己亥 = 36,
        /// <summary>
        /// 
        /// </summary>
        庚子 = 37,
        /// <summary>
        /// 
        /// </summary>
        辛丑 = 38,
        /// <summary>
        /// 
        /// </summary>
        壬寅 = 39,
        /// <summary>
        /// 
        /// </summary>
        癸卯 = 40,
        /// <summary>
        /// 
        /// </summary>
        甲辰 = 41,
        /// <summary>
        /// 
        /// </summary>
        乙巳 = 42,
        /// <summary>
        /// 
        /// </summary>
        丙午 = 43,
        /// <summary>
        /// 
        /// </summary>
        丁未 = 44,
        /// <summary>
        /// 
        /// </summary>
        戊申 = 45,
        /// <summary>
        /// 
        /// </summary>
        己酉 = 46,
        /// <summary>
        /// 
        /// </summary>
        庚戌 = 47,
        /// <summary>
        /// 
        /// </summary>
        辛亥 = 48,
        /// <summary>
        /// 
        /// </summary>
        壬子 = 49,
        /// <summary>
        /// 
        /// </summary>
        癸丑 = 50,
        /// <summary>
        /// 
        /// </summary>
        甲寅 = 51,
        /// <summary>
        /// 
        /// </summary>
        乙卯 = 52,
        /// <summary>
        /// 
        /// </summary>
        丙辰 = 53,
        /// <summary>
        /// 
        /// </summary>
        丁巳 = 54,
        /// <summary>
        /// 
        /// </summary>
        戊午 = 55,
        /// <summary>
        /// 
        /// </summary>
        己未 = 56,
        /// <summary>
        /// 
        /// </summary>
        庚申 = 57,
        /// <summary>
        /// 
        /// </summary>
        辛酉 = 58,
        /// <summary>
        /// 
        /// </summary>
        壬戌 = 59,
        /// <summary>
        /// 
        /// </summary>
        癸亥 = 60
    }
    /// <summary>
    /// 地支表
    /// </summary>
    public enum Earthly
    {
        子 = 1,
        /// <summary>
        /// 
        /// </summary>
        丑 = 2,
        /// <summary>
        /// 
        /// </summary>
        寅 = 3,
        /// <summary>
        /// 
        /// </summary>
        卯 = 4,
        /// <summary>
        /// 
        /// </summary>
        辰 = 5,
        /// <summary>
        /// 
        /// </summary>
        巳 = 6,
        /// <summary>
        /// 
        /// </summary>
        午 = 7,
        /// <summary>
        /// 
        /// </summary>
        未 = 8,
        /// <summary>
        /// 
        /// </summary>
        申 = 9,
        /// <summary>
        /// 
        /// </summary>
        酉 = 10,
        /// <summary>
        /// 
        /// </summary>
        戌 = 11,
        /// <summary>
        /// 
        /// </summary>
        亥 = 12
    }

    /// <summary>
    /// System.Datetime的扩展
    /// </summary>
    public static class DatetimeX
    {
        private static ChineseLunisolarCalendar chinese = new ChineseLunisolarCalendar();
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
        /// <summary>
        /// 返回中国的农历日期
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static DateTime ToChineseCalendar(this DateTime utc)
        {
            return new DateTime(chinese.GetYear(utc), chinese.GetMonth(utc), chinese.GetDayOfMonth(utc));
        }

        /// <summary>
        /// 返回甲子（60年）循环中对应的甲子年int值
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static int GetSexagenaryYear(this DateTime utc)
        {
            return chinese.GetSexagenaryYear(utc);
        }
        /// <summary>
        /// 返回甲子（60年）循环中对应的甲子年中文
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static string GetSexagenaryYearString(this DateTime utc)
        {
            return Enum.GetName(typeof(ChineseEra), chinese.GetSexagenaryYear(utc));
        }
        /// <summary>
        /// 返回甲子（60年）循环中对应的地址Int值
        /// </summary>
        /// <returns></returns>
        public static int GetTerrestrialBranch(this DateTime utc)
        {
            return chinese.GetTerrestrialBranch(chinese.GetSexagenaryYear(utc));
        }
        /// <summary>
        /// 返回甲子（60年）循环中对应的地址Int值
        /// </summary>
        /// <returns></returns>
        public static string GetTerrestrialBranchString(this DateTime utc)
        {
            return Enum.GetName(typeof(Earthly), chinese.GetTerrestrialBranch(chinese.GetSexagenaryYear(utc)));
        }
        /// <summary>
        /// 返回年份中有多少天
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static int GetDaysInYear(this DateTime utc)
        {
            return chinese.GetDaysInYear(utc.Year);
        }
    }
}
