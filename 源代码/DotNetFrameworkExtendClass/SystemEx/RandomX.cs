using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class RandomX
    {
        private static readonly Random rand = new Random();
        /// <summary>
        /// 生成随机的字符串
        /// </summary>
        /// <param name="ran"></param>
        /// <param name="Length">要生成随机字符串的长度</param>
        /// <param name="AllowedRepeat">是否允许出现重复的字符</param>
        /// <returns></returns>
        public static string NextString(this Random ran, int Length = 6, bool AllowedRepeat = true)
        {
            List<char> chars = new List<char>();
            List<char> seed = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#$%&*@!:;|\/><^~".ToList<char>();
            int step = Length;
            do
            {
                char _temp = seed[rand.Next(seed.Count)];
                chars.Add(_temp);
                if (!AllowedRepeat) seed.Remove(_temp);
                if (seed.Count > 0)
                {
                    step--;
                }
                else
                {
                    break;
                }
            } while (step > 0);
            return new string(chars.ToArray());
        }
    }
}
