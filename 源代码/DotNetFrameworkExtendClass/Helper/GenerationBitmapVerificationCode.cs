using System;
using System.Drawing;
using System.Linq;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 生成图片验证码
    /// </summary>
    public sealed class GenerationBitmapVerificationCode : Singleton<GenerationBitmapVerificationCode>
    {
        private GenerationBitmapVerificationCode()
        {
        }
        private static Random random = new Random();
        private string _SEED = "ABCDEFGHJKMNPQRSTUVWXYZ2345678";
        /// <summary>
        /// 种子
        /// </summary>
        public string SEED
        {
            get
            {
                return _SEED;
            }
            set
            {
                if (!_SEED.Equals(value)) _SEED = value;
            }
        }
        /// <summary>
        /// 输出验证码
        /// </summary>
        /// <param name="FontColor">字体颜色</param>
        /// <param name="BackgroundColor">背景颜色</param>
        /// <param name="Width">宽度</param>
        /// <param name="Height">高度</param>
        /// <param name="FontSize">字号</param>
        /// <param name="Code">显示的字符串（默认随记）</param>
        /// <param name="Length">显示的字符长度（默认4位）</param>
        /// <returns></returns>
        public Image Out(Color FontColor, Color BackgroundColor, int Width = 200, int Height = 75, float FontSize = 13, string Code = "", int Length = 4)
        {
            string _code = string.Empty;
            if (Code.IsNullOrWhiteSpace())
            {
                for (int i = 0; i < Length; i++)
                {
                    _code += SEED[random.Next(0, SEED.Length - 1)];
                }
            }
            else { _code = Code; }
            using (Bitmap bmp = new Bitmap(Width, Height))
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(BackgroundColor);
                Pen p = new Pen(FontColor);
                FontFamily family = FontFamily.Families.FirstOrDefault(t => t.Name.Equals("微软雅黑")) ?? FontFamily.Families[0];
                Font font = new Font(family, FontSize, FontStyle.Bold);
                g.DrawString(_code, font, new SolidBrush(FontColor), new Point(4, 4));
                return Image.FromHbitmap(bmp.GetHbitmap());
            }
        }
    }
}
